using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguidorMovement : MonoBehaviour
{
    public Transform target;
    public float targetRotation;
    public float targetSpeed;
    public BallMovementRoy ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        RotateToTarget(Mathf.Abs(Input.GetAxis("Vertical")) * targetRotation);
        RotateTowardsDirection(ball.directionOfMovement);
    }

    /*
     * This method rotates in X axis slowly until it reaches the target rotation
     * @param targetRotation: The target rotation of the object
     */
    void RotateToTarget(float targetRotation)
    {
        float rotationSpeed = targetSpeed;
        float rotation = transform.eulerAngles.x;
        float newRotation = Mathf.LerpAngle(rotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(newRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    /*
     *     * This method rotates only the Y axis of the object to the direction of the ball
     *         */
    void RotateToBall()
    {
        Vector3 direction = ball.directionOfMovement;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    public void RotateTowardsDirection(Vector3 direction)
    {
        // Asegurarse de que la dirección tenga magnitud (evitar errores si es Vector3.zero)
        if (direction == Vector3.zero) return;

        // Calcular el ángulo de rotación hacia la dirección en el plano XZ
        float targetYAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Conservar la rotación actual en X y Z
        Vector3 currentRotation = transform.localEulerAngles;

        // Aplicar solo la rotación en Y
        transform.localEulerAngles = new Vector3(currentRotation.x, targetYAngle, currentRotation.z);
    }
}




