using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallThreshold = 10f;  // Altura m�nima de ca�da para recibir da�o (en unidades del mundo)
    public IHealth health;

    private float lastYPosition;  // �ltima posici�n Y conocida al empezar la ca�da
    private bool isFalling = false; // Estado de si el jugador est� cayendo
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
                health.ReceiveDamage();
            }

            // Reseteamos el estado de la ca�da
            isFalling = false;
        }
    }
}
