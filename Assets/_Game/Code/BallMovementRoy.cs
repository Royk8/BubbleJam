using UnityEngine;

public class BallMovementRoy : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Transform cameraTransform; // Asigna aquí la cámara principal en el inspector.
    public Vector3 directionOfMovement;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Dirección relativa a la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignorar el eje Y para evitar movimiento vertical
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calcular la dirección del movimiento
        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        // Aplicar la fuerza en la dirección calculada
        rb.AddForce(moveDirection * speed);
        if (rb.velocity.magnitude > 10)
        {
            rb.velocity = rb.velocity.normalized * 10;
        }
        // Saves the direction of movement in the variable directionOfMovement
        directionOfMovement = moveDirection;
    }
}
