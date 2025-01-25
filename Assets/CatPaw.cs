using UnityEngine;
using System.Collections;

public class CatPaw : MonoBehaviour
{
    public GameObject pawPrefab;           // Prefab de la pata
    public Transform[] spawnPoints;        // Puntos donde la pata puede aparecer
    public float moveSpeed = 20f;          // Velocidad con la que se mueve la pata
    private GameObject currentPaw;         // La pata instanciada en la escena
    public bool isAttacking = false;      // Estado para saber si la pata est� en ataque
    public Rigidbody pawRigidbody;        // Rigidbody de la pata

    // Variables de control de la fuerza
    public float pushForce = 15f;  // Fuerza de empuje ajustada
    private Transform playerTransform;     // Transform del jugador
    private Vector3 initialPosition;       // Posici�n inicial de la pata

    private bool hasPushedPlayer = false;  // Estado para controlar si ya empuj� al jugador

    private void Update()
    {
        // Si la pata est� atacando y tiene Rigidbody, se mueve hacia el jugador usando f�sicas
        if (isAttacking && currentPaw != null)
        {
            MovePaw();
        }
    }

    // Activar el ataque, instanciando la pata en un lugar aleatorio de la pared
    public void StartAttack(Transform player)
    {
        // Instanciamos la pata en un punto aleatorio de la pared
        int randomIndex = Random.Range(0, spawnPoints.Length); // Selecciona aleatoriamente un punto de aparici�n
        Transform spawnPoint = spawnPoints[randomIndex];  // Selecciona el punto de aparici�n

        currentPaw = Instantiate(pawPrefab, spawnPoint.position, Quaternion.identity);  // Instancia la pata
        pawRigidbody = currentPaw.GetComponent<Rigidbody>();  // Obt�n el Rigidbody de la pata
        pawCollision currentPawCollision = currentPaw.GetComponent<pawCollision>();
        currentPawCollision.padre = this;

        initialPosition = currentPaw.transform.position; // Guardamos la posici�n inicial de la pata
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

    // Este m�todo se llama cuando la pata entra en contacto con el jugador
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && isAttacking)  // Verifica si est� atacando y colisiona con el jugador
        {
            isAttacking = false;  // Detiene el ataque
            pawRigidbody.velocity = Vector3.zero;  // Detiene el movimiento de la pata

            PushPlayer(other.collider);  // Aplica el empuje
            ReturnToInitialPosition();  // Regresa la pata a su posici�n inicial
        }
    }


    // Aplica el empuj�n al jugador cuando lo toca
    public void PushPlayer(Collider playerCollider)
    {
        Rigidbody playerRb = playerCollider.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            // Calcula la direcci�n contraria a la que la pata toc� al jugador
            Vector3 hitDirection = playerCollider.transform.position - currentPaw.transform.position;
            Vector3 pushDirection = hitDirection.normalized;  // Invierte la direcci�n para empujar al jugador hacia atr�s

            // Aplica una fuerza para empujar al jugador
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);  // Usamos "Impulse" para un empuj�n inmediato

            Debug.Log("�El jugador fue golpeado y empujado!");
        }
    }

    // Regresa la pata a su posici�n inicial y la destruye despu�s
    public void ReturnToInitialPosition()
    {
        // Mueve la pata a su posici�n inicial antes de destruirla
        StartCoroutine(MovePawBackAndDestroy());
    }

    private IEnumerator MovePawBackAndDestroy()
    {
        float duration = 1f;  // Tiempo para regresar a la posici�n inicial
        float elapsedTime = 0f;
        Vector3 startPosition = currentPaw.transform.position;

        while (elapsedTime < duration)
        {
            currentPaw.transform.position = Vector3.Lerp(startPosition, initialPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentPaw.transform.position = initialPosition;  // Asegura la posici�n final
        Destroy(currentPaw);  // Destruye la pata
        isAttacking = false;  // Resetea el estado de ataque
        hasPushedPlayer = false;  // Resetea el estado de empuje
    }

}
