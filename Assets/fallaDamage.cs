using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallThreshold = 10f;  // Altura mínima de caída para recibir daño (en unidades del mundo)
    public float damagePerMeter = 5f;  // Cantidad de daño por metro de caída
    public float currentHealth = 100f; // Salud actual del jugador (ajustar según lo que uses en tu juego)

    private float lastYPosition;  // Última posición Y conocida al empezar la caída
    private bool isFalling = false; // Estado de si el jugador está cayendo
    private Rigidbody rb;          // El Rigidbody del jugador

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el Rigidbody
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
                ApplyFallDamage(fallDistance);  // Aplica el daño si la caída fue lo suficientemente alta
            }

            // Reseteamos el estado de la caída
            isFalling = false;
        }
    }

    private void ApplyFallDamage(float fallDistance)
    {
        // Calculamos el daño por la distancia de la caída
        fallDistance = Mathf.Abs(fallDistance); // Nos aseguramos de que la distancia sea positiva

        // Calculamos el daño basado en la distancia de caída
        float damage = (fallDistance - fallThreshold) * damagePerMeter;

        if (damage > 0)
        {
            currentHealth -= damage;  // Aplicamos el daño
            Debug.Log("¡Daño por caída! " + damage + " daño recibido. Salud restante: " + currentHealth);
        }
        else
        {
            Debug.Log("La caída no fue lo suficientemente alta para causar daño.");
        }
    }
}
