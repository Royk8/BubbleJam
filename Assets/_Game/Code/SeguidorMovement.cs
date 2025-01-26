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
        // Asegurarse de que la direcci�n tenga magnitud (evitar errores si es Vector3.zero)
        if (direction == Vector3.zero) return;

        // Calcular el �ngulo de rotaci�n hacia la direcci�n en el plano XZ
        float targetYAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Conservar la rotaci�n actual en X y Z
        Vector3 currentRotation = transform.localEulerAngles;

        // Aplicar solo la rotaci�n en Y
        transform.localEulerAngles = new Vector3(currentRotation.x, targetYAngle, currentRotation.z);
    }

    public void StabilizeRotation()
    {
        if (!canStabilized) return;
        if (TimeToStabilize > 0) return;
        //if (ball.directionOfMovement.sqrMagnitude < 0.01f)
        //    return; // Si la direcci�n es muy peque�a, no ajustar.

        // Obt�n la rotaci�n actual
        Quaternion currentRotation = transform.rotation;

        // Calcula la rotaci�n objetivo basada en la direcci�n de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(ball.directionOfMovement, Vector3.up);

        // Aseg�rate de estabilizar X y Z a 0, solo rotando sobre el eje Y
        Vector3 targetEuler = targetRotation.eulerAngles;
        targetEuler.x = 0f; // Estabiliza X
        targetEuler.z = 0f; // Estabiliza Z

        // Interpola suavemente hacia la rotaci�n objetivo
        Quaternion stabilizedRotation = Quaternion.Euler(targetEuler);
        transform.rotation = Quaternion.Lerp(currentRotation, stabilizedRotation, stabilizationSpeed * Time.deltaTime);
    }
}




