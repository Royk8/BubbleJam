using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguidorMovement : MonoBehaviour
{
    public Transform target;
    public float targetRotation;
    public float targetSpeed;
    public float stabilizationSpeed;
    private bool canStabilized = true;
    public BallMovementRoy ball;
    public GiroDescontrolado giroDescontrolado;
    public AnimationsController animations;
    public float TimeToStabilize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputValue = Mathf.Abs(Input.GetAxis("Vertical"));
        transform.position = target.position;
        RotateToTarget(inputValue * targetRotation);
        RotateTowardsDirection(ball.directionOfMovement);

        if( ball.isTooFast && inputValue < 0.1)
        {
            giroDescontrolado.EmpezarAGirar();
            animations.LetGo(true);
            canStabilized = false;
        }
        else
        {
            giroDescontrolado.PararGiro();
            animations.LetGo(false);
            canStabilized = true;
            TimeToStabilize = 5;
        }

        animations.Run(inputValue);
        StabilizeRotation();
        TimeToStabilize -= Time.deltaTime;
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

    public void StabilizeRotation()
    {
        if (!canStabilized) return;
        if (TimeToStabilize > 0) return;
        //if (ball.directionOfMovement.sqrMagnitude < 0.01f)
        //    return; // Si la dirección es muy pequeña, no ajustar.

        // Obtén la rotación actual
        Quaternion currentRotation = transform.rotation;

        // Calcula la rotación objetivo basada en la dirección de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(ball.directionOfMovement, Vector3.up);

        // Asegúrate de estabilizar X y Z a 0, solo rotando sobre el eje Y
        Vector3 targetEuler = targetRotation.eulerAngles;
        targetEuler.x = 0f; // Estabiliza X
        targetEuler.z = 0f; // Estabiliza Z

        // Interpola suavemente hacia la rotación objetivo
        Quaternion stabilizedRotation = Quaternion.Euler(targetEuler);
        transform.rotation = Quaternion.Lerp(currentRotation, stabilizedRotation, stabilizationSpeed * Time.deltaTime);
    }
}




