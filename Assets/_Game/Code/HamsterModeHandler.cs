using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterModeHandler : MonoBehaviour

{
    public OrbitCameraWithClickAndZoom orbitCamera;
    [Header("Bubble Mode")]
    public GameObject ball;
    public GameObject seguidor;
    public Transform bubbleModeCameraTarget;
    public bool isBubbleMode = true;
    [Header("Hamster Mode")]
    public GameObject hamster;
    public Transform hamsterModeCameraTarget;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (isBubbleMode)
            {
                ActivateHamsterMode();
                isBubbleMode = false;
            }
            else
            {
                ActivateBubbleMode();
                isBubbleMode = true;
            }
        }
    }

    public void ActivateBubbleMode()
    {
        // Activa la burbuja y desactiva el h�mster
        ball.SetActive(true);
        seguidor.SetActive(true);
        hamster.SetActive(false);

        // Cambiar el objetivo de la c�mara
        orbitCamera.target = bubbleModeCameraTarget;

        // Sincroniza la posici�n y velocidad
        Vector3 hamsterPosition = hamster.transform.position;
        ball.transform.position = hamsterPosition;

        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.velocity = Vector3.zero; // Detener la bola al activar

        Rigidbody hamsterRb = hamster.GetComponentInChildren<Rigidbody>();
        hamsterRb.velocity = Vector3.zero;

        // Ajusta la rotaci�n de la bola para coincidir con la rotaci�n del h�mster
        Vector3 hamsterRotation = hamster.transform.rotation.eulerAngles;
        ball.transform.rotation = Quaternion.Euler(0, hamsterRotation.y, 0);
    }


    public void ActivateHamsterMode()
    {
        // Activa el h�mster y desactiva la burbuja
        ball.SetActive(false);
        seguidor.SetActive(false);
        hamster.SetActive(true);

        // Cambiar el objetivo de la c�mara
        orbitCamera.target = hamsterModeCameraTarget;

        // Sincroniza la posici�n y velocidad
        Vector3 ballPosition = ball.transform.position;
        hamster.transform.position = ballPosition - Vector3.up * 1.5f;

        Rigidbody hamsterRb = hamster.GetComponentInChildren<Rigidbody>();
        BallMovementRoy ballMovement = ball.GetComponent<BallMovementRoy>();
        hamsterRb.velocity = ballMovement.rb.velocity;

        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.velocity = Vector3.zero; // Detener la bola al desactivar

        // Ajusta la rotaci�n del h�mster para coincidir con la rotaci�n de la bola
        Vector3 ballRotation = ball.transform.rotation.eulerAngles;
        hamster.transform.rotation = Quaternion.Euler(0, ballRotation.y, 0);
    }

}
