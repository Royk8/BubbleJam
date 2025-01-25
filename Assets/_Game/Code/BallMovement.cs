using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementWithCameraPhysics : MonoBehaviour
{
    [Header("Movement Settings")]

    private Rigidbody rb; // Referencia al Rigidbody del jugador.
    private Vector3 moveDirection; // Direcci�n calculada para el movimiento.

    void Start()
    {
        // Obtiene el componente Rigidbody.
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        // Calcular la direcci�n del movimiento
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        rb.AddForce(moveDirection);
    }
    void FixedUpdate()
    {
        //// Obt�n las entradas del teclado.
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");


        //float moveDirection = horizontal * vertical;


        // Calcula la direcci�n basada en la c�mara.
        //Vector3 forward = cameraTransform.forward; // Direcci�n hacia adelante de la c�mara.
        //Vector3 right = cameraTransform.right; // Direcci�n hacia la derecha de la c�mara.

        //// Ignorar la inclinaci�n vertical de la c�mara.
        //forward.y = 0;
        //right.y = 0;
        //forward.Normalize();
        //right.Normalize();

        //// Calcula la direcci�n de movimiento.
        //moveDirection = (forward * vertical + right * horizontal).normalized;

        //// Mueve al jugador usando las f�sicas (con AddForce).
        //Vector3 velocity = moveDirection * moveSpeed;
        //Vector3 newVelocity = new Vector3(velocity.x, rb.velocity.y, velocity.z); // Mant�n la velocidad vertical actual.
        //rb.AddForce(newVelocity);
    }
}
