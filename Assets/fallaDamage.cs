using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallThreshold = 10f;  // Altura m�nima de ca�da para recibir da�o (en unidades del mundo)
    public float damagePerMeter = 5f;  // Cantidad de da�o por metro de ca�da
    public float currentHealth = 100f; // Salud actual del jugador (ajustar seg�n lo que uses en tu juego)

    private float lastYPosition;  // �ltima posici�n Y conocida al empezar la ca�da
    private bool isFalling = false; // Estado de si el jugador est� cayendo
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
            // Iniciamos la ca�da
            isFalling = true;
            lastYPosition = transform.position.y;  // Guardamos la posici�n Y cuando comienza la ca�da
        }
        else if (rb.velocity.y >= 0 && isFalling)
        {
            // Si el jugador deja de caer, calculamos el da�o de la ca�da
            float fallDistance = lastYPosition - transform.position.y;

            if (fallDistance >= fallThreshold)
            {
                ApplyFallDamage(fallDistance);  // Aplica el da�o si la ca�da fue lo suficientemente alta
            }

            // Reseteamos el estado de la ca�da
            isFalling = false;
        }
    }

    private void ApplyFallDamage(float fallDistance)
    {
        // Calculamos el da�o por la distancia de la ca�da
        fallDistance = Mathf.Abs(fallDistance); // Nos aseguramos de que la distancia sea positiva

        // Calculamos el da�o basado en la distancia de ca�da
        float damage = (fallDistance - fallThreshold) * damagePerMeter;

        if (damage > 0)
        {
            currentHealth -= damage;  // Aplicamos el da�o
            Debug.Log("�Da�o por ca�da! " + damage + " da�o recibido. Salud restante: " + currentHealth);
        }
        else
        {
            Debug.Log("La ca�da no fue lo suficientemente alta para causar da�o.");
        }
    }
}
