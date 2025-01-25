using System.Collections;
using UnityEngine;

public class CatPaw : MonoBehaviour
{
    public GameObject pawPrefab;           // Prefab de la pata
    public Transform[] spawnPoints;        // Puntos donde la pata puede aparecer
    public float moveSpeed = 20f;          // Velocidad con la que se mueve la pata
    private GameObject currentPaw;         // La pata instanciada en la escena
    public bool isAttacking = false;      // Estado para saber si la pata está en ataque
    public Rigidbody pawRigidbody;        // Rigidbody de la pata

    // Variables de control de la fuerza
    public float pushForce = 15f;  // Fuerza de empuje ajustada
    private Transform playerTransform;     // Transform del jugador
    private Vector3 initialPosition;       // Posición inicial de la pata

    public bool hasPushedPlayer = false;  // Estado para controlar si ya empujó al jugador

    private void Update()
    {
        // Si la pata está atacando y tiene Rigidbody, se mueve hacia el jugador usando físicas
        if (isAttacking && currentPaw != null)
        {
            MovePaw();
        }
    }

    // Activar el ataque, instanciando la pata en un lugar aleatorio de la pared
    public void StartAttack(Transform player)
    {
        // Instanciamos la pata en un punto aleatorio de la pared
        int randomIndex = Random.Range(0, spawnPoints.Length); // Selecciona aleatoriamente un punto de aparición
        Transform spawnPoint = spawnPoints[randomIndex];  // Selecciona el punto de aparición

        currentPaw = Instantiate(pawPrefab, spawnPoint.position, Quaternion.identity);  // Instancia la pata
        pawRigidbody = currentPaw.GetComponent<Rigidbody>();  // Obtén el Rigidbody de la pata
        pawCollision currentPawCollision = currentPaw.GetComponent<pawCollision>();
        currentPawCollision.padre = this;

        initialPosition = currentPaw.transform.position; // Guardamos la posición inicial de la pata
        playerTransform = player;  // Guarda la referencia al transform del jugador
        isAttacking = true;         // La pata empieza a moverse hacia el jugador
    }

    private void MovePaw()
    {
        if (playerTransform != null && pawRigidbody != null)
        {
            // Mueve la pata hacia el jugador
            Vector3 direction = (playerTransform.position - currentPaw.transform.position).normalized;
            pawRigidbody.velocity = direction * moveSpeed;  // Mueve la pata hacia el jugador usando Rigidbody
        }
    }

    // Este método se llama cuando la pata entra en contacto con el jugador
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && isAttacking)
        {
            isAttacking = false;
            hasPushedPlayer = true;  // Marca que el jugador fue empujado
            pawRigidbody.velocity = Vector3.zero;

            PushPlayer(other.collider);
            ReturnToInitialPosition();
        }
    }



    // Aplica el empujón al jugador cuando lo toca
    public void PushPlayer(Collider playerCollider)
    {
        Rigidbody playerRb = playerCollider.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            // Calcula la dirección contraria a la que la pata tocó al jugador
            Vector3 hitDirection = playerCollider.transform.position - currentPaw.transform.position;
            Vector3 pushDirection = hitDirection.normalized;  // Invierte la dirección para empujar al jugador hacia atrás

            // Aplica una fuerza para empujar al jugador
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);  // Usamos "Impulse" para un empujón inmediato

            Debug.Log("¡El jugador fue golpeado y empujado!");
        }
    }

    public void StopAttack()
    {
        if (currentPaw != null)
        {
            isAttacking = false;  // Detén el estado de ataque
            pawRigidbody.velocity = Vector3.zero;  // Detén cualquier movimiento actual
            ReturnToInitialPosition();  // Asegura que la pata regrese a su posición inicial
        }
    }




    // Regresa la pata a su posición inicial y la destruye después
    public void ReturnToInitialPosition()
    {
        // Mueve la pata a su posición inicial antes de destruirla
        StartCoroutine(MovePawBackAndDestroy());
    }

    private IEnumerator MovePawBackAndDestroy()
    {
        if (currentPaw == null || !isAttacking)  // Verifica si la pata ya ha sido destruida
        {
            yield break;  // Si la pata ya no existe o el ataque ha terminado, salimos de la corutina
        }

        float duration = 1f;  // Tiempo para regresar a la posición inicial
        float elapsedTime = 0f;
        Vector3 startPosition = currentPaw.transform.position;

        while (elapsedTime < duration)
        {
            if (currentPaw == null)  // Si la pata se destruye durante la corutina
            {
                yield break;  // Detiene la corutina si el objeto ya no existe
            }

            currentPaw.transform.position = Vector3.Lerp(startPosition, initialPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (currentPaw != null)  // Verifica que el objeto aún exista antes de destruirlo
        {
            currentPaw.transform.position = initialPosition;  // Asegura la posición final
            Destroy(currentPaw);  // Destruye la pata
            isAttacking = false;  // Resetea el estado de ataque
        }
    }


}