using UnityEngine;

public class BallMovementRoy : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float topSpeed;
    public Transform cameraTransform; // Asigna aqu� la c�mara principal en el inspector.
    public Vector3 directionOfMovement;
    public bool isTooFast;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Direcci�n relativa a la c�mara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignorar el eje Y para evitar movimiento vertical
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calcular la direcci�n del movimiento
        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        // Aplicar la fuerza en la direcci�n calculada
        rb.AddForce(moveDirection * speed);

        if (rb.velocity.magnitude > (topSpeed * 0.6f))
        {
            isTooFast = true;
        }
        else
        {
            isTooFast = false;
        }

        //Caps the speed of the ball
        if (rb.velocity.magnitude > topSpeed)
        {
            rb.velocity = rb.velocity.normalized * topSpeed;
        }
        // Saves the direction of movement in the variable directionOfMovement
        directionOfMovement = moveDirection;
    }
}
