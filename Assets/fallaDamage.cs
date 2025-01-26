using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallThreshold = 10f;  // Altura mínima de caída para recibir daño (en unidades del mundo)
    public IHealth health;

    private float lastYPosition;  // Última posición Y conocida al empezar la caída
    private bool isFalling = false; // Estado de si el jugador está cayendo
    private Rigidbody rb;          // El Rigidbody del jugador

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el Rigidbody
        health = GetComponent<IHealth>();  // Obtener el componente de salud
    }

    private void Update()
    {
        // Detectamos si el jugador empieza a caer (cuando la velocidad Y es negativa)
        if (rb.velocity.y < 0 && !isFalling)
        {
            // Iniciamos la caída
            isFalling = true;
            lastYPosition = transform.position.y;  // Guardamos la posición Y cuando comienza la caída
        }
        else if (rb.velocity.y >= 0 && isFalling)
        {
            // Si el jugador deja de caer, calculamos el daño de la caída
            float fallDistance = lastYPosition - transform.position.y;

            if (fallDistance >= fallThreshold)
            {
                health.ReceiveDamage();
            }

            // Reseteamos el estado de la caída
            isFalling = false;
        }
    }
}
