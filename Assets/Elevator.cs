using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 2f; // Velocidad del elevador
    public float height = 5f; // Altura máxima (la cantidad de movimiento hacia arriba)
    private Vector3 startPos; // Posición inicial del elevador
    private bool isPlayerOnElevator = false; // ¿Está el jugador en la plataforma?

    private void Start()
    {
        startPos = transform.position; // Guardamos la posición inicial del elevador
    }

    private void Update()
    {
        // Solo mueve la plataforma si el jugador está sobre ella
        if (isPlayerOnElevator)
        {
            // Mueve hacia arriba cuando el jugador está sobre la plataforma
            if (transform.position.y < startPos.y + height)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
        else
        {
            // Mueve hacia abajo cuando el jugador sale de la plataforma
            if (transform.position.y > startPos.y)
            {
                transform.position -= Vector3.up * speed * Time.deltaTime;
            }
        }
    }

    // Detecta si el jugador está en la plataforma
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador toca el elevador
        {
            isPlayerOnElevator = true; // El jugador está sobre la plataforma
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador deja el elevador
        {
            isPlayerOnElevator = false; // El jugador ya no está sobre la plataforma
        }
    }
}
