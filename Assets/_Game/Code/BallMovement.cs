using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementWithCameraPhysics : MonoBehaviour
{
    [Header("Movement Settings")]

    private Rigidbody rb; // Referencia al Rigidbody del jugador.
    private Vector3 moveDirection; // Dirección calculada para el movimiento.

    void Start()
    {
        // Obtiene el componente Rigidbody.
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        // Calcular la dirección del movimiento
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        rb.AddForce(moveDirection);
    }
    void FixedUpdate()
    {
        //// Obtén las entradas del teclado.
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");


        //float moveDirection = horizontal * vertical;


        // Calcula la dirección basada en la cámara.
        //Vector3 forward = cameraTransform.forward; // Dirección hacia adelante de la cámara.
        //Vector3 right = cameraTransform.right; // Dirección hacia la derecha de la cámara.

        //// Ignorar la inclinación vertical de la cámara.
        //forward.y = 0;
        //right.y = 0;
        //forward.Normalize();
        //right.Normalize();

        //// Calcula la dirección de movimiento.
        //moveDirection = (forward * vertical + right * horizontal).normalized;

        //// Mueve al jugador usando las físicas (con AddForce).
        //Vector3 velocity = moveDirection * moveSpeed;
        //Vector3 newVelocity = new Vector3(velocity.x, rb.velocity.y, velocity.z); // Mantén la velocidad vertical actual.
        //rb.AddForce(newVelocity);
    }
}
