using System.Collections;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;   // Objeto que será lanzado (como una pata).
    public Transform spawnPoint;       // Posición donde el objeto será instanciado.
    public Transform playerPoint;      // Posición del jugador (a dónde debe ir el objeto).
    public float forceMagnitude = 10f; // Magnitud de la fuerza que se aplica al jugador.
    public float returnSpeed = 5f;     // Velocidad con la que el objeto regresa.
    public float offsetY = -0.2f;     // Ajuste vertical para evitar golpear el objeto muy arriba o abajo.
    public float hitDuration = 0.5f;  // Tiempo que el objeto permanece tocando al jugador antes de regresar.

    // Este método se llama cuando el jugador entra en el trigger de la pared.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Instanciar el objeto (la pata o lo que sea)
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            // Agregar un Rigidbody si no tiene
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = spawnedObject.AddComponent<Rigidbody>();
            }

            rb.useGravity = false;  // Desactivar la gravedad si no deseas que caiga
            rb.freezeRotation = true; // Congelar la rotación

            // Llamar a la rutina que mueve el objeto y le aplica la fuerza
            StartCoroutine(MoveAndHitPlayer(spawnedObject, rb, other.gameObject));
        }
    }

    // Aplicar la fuerza y luego mover el objeto hacia el jugador
    private IEnumerator MoveAndHitPlayer(GameObject obj, Rigidbody rb, GameObject player)
    {
        Vector3 targetPosition = playerPoint.position + new Vector3(0, offsetY, 0);

        // Aplica una fuerza hacia el jugador para que lo golpee
        Vector3 directionToPlayer = (player.transform.position - obj.transform.position).normalized;
        rb.AddForce(directionToPlayer * forceMagnitude, ForceMode.Impulse);

        // Deja que el objeto viaje hacia el jugador con la fuerza aplicada
        yield return new WaitForSeconds(0.5f); // Esperar un momento mientras el objeto impacta al jugador

        // Detener el objeto después del golpe
        rb.velocity = Vector3.zero;

        // Esperar un momento más antes de regresar
        yield return new WaitForSeconds(hitDuration);

        // Iniciar el regreso del objeto a su posición original
        StartCoroutine(ReturnObjectToStart(obj, rb));
    }

    // Regresar el objeto a la posición inicial (la pared)
    private IEnumerator ReturnObjectToStart(GameObject obj, Rigidbody rb)
    {
        Vector3 startPosition = spawnPoint.position;

        // Mover el objeto de regreso
        while (Vector3.Distance(obj.transform.position, startPosition) > 0.1f)
        {
            Vector3 direction = (startPosition - obj.transform.position).normalized;
            rb.MovePosition(obj.transform.position + direction * returnSpeed * Time.deltaTime);
            yield return null;
        }

        // Detener el objeto y asegurarse que regrese a la posición inicial
        rb.velocity = Vector3.zero;
        obj.transform.position = startPosition;  // Asegurar que se quede en la posición inicial
        obj.transform.rotation = Quaternion.identity;  // Restaurar la rotación original
        Destroy(obj); // Destruir el objeto después de regresar
    }
}
