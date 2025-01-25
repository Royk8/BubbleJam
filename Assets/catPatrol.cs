using UnityEngine;

public class RandomMovementAndAttack : MonoBehaviour
{
    public float moveSpeed = 5f;           // Velocidad de movimiento del objeto
    public float roamRange = 10f;          // Rango de movimiento aleatorio
    public float detectionRange = 5f;      // Distancia de activación del jugador
    public float launchForce = 10f;        // Fuerza del lanzamiento parabólico
    public float gravityScale = 1f;        // Escala de gravedad (para la parábola)
    public Transform player;               // Referencia al jugador
    public float rotationSpeed = 5f;       // Velocidad de rotación (ajustable)

    private Rigidbody rb;
    private Vector3 targetPosition;        // La posición objetivo a la que se mueve
    private bool isNearPlayer = false;     // Estado para saber si el objeto está cerca del jugador
    private bool isLaunching = false;      // Estado para saber si el objeto está lanzando
    private float launchTime;              // Tiempo en que se lanzó el objeto
    private float returnToPatrolTime = 2f; // Tiempo antes de volver al patrullaje si no toca al jugador

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewRandomTarget();               // Establece una nueva posición aleatoria para el movimiento
    }

    private void Update()
    {
        if (!isNearPlayer && !isLaunching)
        {
            // Mover aleatoriamente el objeto hacia la nueva posición
            MoveRandomly();

            // Comprobar si el objeto está cerca del jugador
            if (Vector3.Distance(transform.position, player.position) <= detectionRange)
            {
                isNearPlayer = true;
                LaunchAtPlayer();
            }
        }

        // Si el objeto está lanzando, comprobar si ha pasado el tiempo sin tocar al jugador
        if (isLaunching && Time.time - launchTime > returnToPatrolTime)
        {
            ReturnToPatrol(); // Regresa al patrullaje si no ha tocado al jugador
        }
    }

    private void MoveRandomly()
    {
        // Interpolación de rotación suave hacia el objetivo
        Vector3 targetPositionWithoutY = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

        // Calculamos la dirección hacia la nueva posición
        Vector3 directionToTarget = (targetPositionWithoutY - transform.position).normalized;

        // Rotación suave hacia la dirección del movimiento
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Mueve el objeto hacia la nueva posición manteniendo el eje Y constante
        transform.position = Vector3.MoveTowards(transform.position, targetPositionWithoutY, moveSpeed * Time.deltaTime);

        // Si el objeto llega a la posición objetivo, se establece una nueva posición aleatoria
        if (transform.position == targetPositionWithoutY)
        {
            SetNewRandomTarget();
        }
    }

    private void SetNewRandomTarget()
    {
        // Asegúrate de que las posiciones aleatorias no se salgan del rango esperado
        float randomX = Random.Range(-roamRange, roamRange);
        float randomZ = Random.Range(-roamRange, roamRange);

        targetPosition = new Vector3(
            transform.position.x + randomX,
            transform.position.y,  // Mantener Y constante
            transform.position.z + randomZ
        );

        Debug.Log($"Nueva posición aleatoria establecida: {targetPosition}");
    }

    private void LaunchAtPlayer()
    {
        // Desactivar el movimiento aleatorio y preparar el lanzamiento parabólico
        isLaunching = true;  // Activamos el estado de lanzamiento
        launchTime = Time.time; // Guardamos el tiempo en que comenzó el lanzamiento
        targetPosition = transform.position;  // Detenemos el movimiento aleatorio
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 launchDirection = directionToPlayer + Vector3.up * gravityScale; // Añadir componente hacia arriba para la parábola

        // Lanza el objeto con una fuerza parabólica
        rb.velocity = launchDirection * launchForce;

        // Rotar hacia la dirección del lanzamiento
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = targetRotation;

        Debug.Log("¡Lanzando hacia el jugador!");
    }

    private void ReturnToPatrol()
    {
        // Si el objeto no tocó al jugador, regresa a su estado de patrullaje
        isLaunching = false;
        isNearPlayer = false;
        SetNewRandomTarget();  // Establece una nueva posición aleatoria y comienza a patrullar
        Debug.Log("Regresando al patrullaje...");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Desactivar las fuerzas de movimiento del jugador
                playerRb.velocity = Vector3.zero;  // Anula cualquier velocidad existente

                // Aplicar el impulso en la dirección del impacto
                Vector3 hitDirection = (other.transform.position - transform.position).normalized;
                playerRb.AddForce(hitDirection * launchForce, ForceMode.Impulse);  // Aumenta la fuerza si es necesario

                // Desactivar las fuerzas del jugador durante un breve momento si es necesario
                // Esto puede ser útil si el jugador está siendo impulsado y quieres que no se mueva por su propio movimiento
                // playerRb.isKinematic = true; // Solo usa esto si deseas que el jugador no reciba fuerzas mientras está impulsado

                // Al regresar al patrullaje, reactivar el movimiento del jugador
                isNearPlayer = false;
                isLaunching = false;
                Invoke("RestartPatrol", 1f);  // Espera 1 segundo antes de reanudar el patrullaje
            }
        }
    }



    private void RestartPatrol()
    {
        // Reanudar el patrullaje
        SetNewRandomTarget();
    }
}
