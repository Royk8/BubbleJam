using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HamsterMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public Transform cameraTransform;
    public Transform hamsterModel;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /*
     *     * This method moves the hamster with the input of the player using rb velocity
     *         */
    void Move()
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


        rb.velocity = moveDirection * speed;

        // Rotar el modelo del hamster
        if (moveDirection != Vector3.zero)
        {
            hamsterModel.rotation = Quaternion.LookRotation(moveDirection);
        }



    }
}
