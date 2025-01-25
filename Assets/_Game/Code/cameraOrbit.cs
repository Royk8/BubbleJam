using UnityEngine;

public class OrbitCameraWithClickAndZoom : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // El objeto que la c�mara seguir�.

    [Header("Orbit Settings")]
    public float distance = 10.0f; // Distancia entre la c�mara y el objetivo.
    public float xSpeed = 120.0f; // Velocidad de rotaci�n horizontal.
    public float ySpeed = 120.0f; // Velocidad de rotaci�n vertical.
    public Vector2 yAngleLimits = new Vector2(-20, 80); // L�mites para la rotaci�n vertical.

    [Header("Zoom Settings")]
    public float zoomSpeed = 2.0f; // Velocidad de zoom.
    public float minZoom = 5.0f; // Distancia m�nima de la c�mara.
    public float maxZoom = 20.0f; // Distancia m�xima de la c�mara.

    private float xAngle = 0.0f; // �ngulo horizontal acumulado.
    private float yAngle = 0.0f; // �ngulo vertical acumulado.

    void Start()
    {
        // Inicializa los �ngulos basados en la rotaci�n inicial de la c�mara.
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Solo rotar si se mantiene presionado el bot�n izquierdo del mouse.
            if (Input.GetMouseButton(1)) // 1 es el bot�n derecho del mouse.
            {
                // Obt�n la entrada del mouse.
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // Ajusta los �ngulos con la velocidad y la entrada.
                xAngle += mouseX * xSpeed * Time.deltaTime;
                yAngle -= mouseY * ySpeed * Time.deltaTime;

                // Limita el �ngulo vertical.
                yAngle = Mathf.Clamp(yAngle, yAngleLimits.x, yAngleLimits.y);
            }

            // Control del zoom con la rueda del rat�n (scroll).
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom); // Limitar la distancia de zoom.

            // Calcula la posici�n de la c�mara en funci�n de los �ngulos y la distancia.
            Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * distance);

            // Actualiza la posici�n y rotaci�n de la c�mara.
            transform.position = position;
            transform.LookAt(target);
        }
    }
}
