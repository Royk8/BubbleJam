using UnityEngine;

public class OrbitCameraWithClickAndZoom : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // El objeto que la cámara seguirá.

    [Header("Orbit Settings")]
    public float distance = 10.0f; // Distancia entre la cámara y el objetivo.
    public float xSpeed = 120.0f; // Velocidad de rotación horizontal.
    public float ySpeed = 120.0f; // Velocidad de rotación vertical.
    public Vector2 yAngleLimits = new Vector2(-20, 80); // Límites para la rotación vertical.

    [Header("Zoom Settings")]
    public float zoomSpeed = 2.0f; // Velocidad de zoom.
    public float minZoom = 5.0f; // Distancia mínima de la cámara.
    public float maxZoom = 20.0f; // Distancia máxima de la cámara.

    private float xAngle = 0.0f; // Ángulo horizontal acumulado.
    private float yAngle = 0.0f; // Ángulo vertical acumulado.

    void Start()
    {
        // Inicializa los ángulos basados en la rotación inicial de la cámara.
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Solo rotar si se mantiene presionado el botón izquierdo del mouse.
            if (Input.GetMouseButton(1)) // 1 es el botón derecho del mouse.
            {
                // Obtén la entrada del mouse.
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // Ajusta los ángulos con la velocidad y la entrada.
                xAngle += mouseX * xSpeed * Time.deltaTime;
                yAngle -= mouseY * ySpeed * Time.deltaTime;

                // Limita el ángulo vertical.
                yAngle = Mathf.Clamp(yAngle, yAngleLimits.x, yAngleLimits.y);
            }

            // Control del zoom con la rueda del ratón (scroll).
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom); // Limitar la distancia de zoom.

            // Calcula la posición de la cámara en función de los ángulos y la distancia.
            Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * distance);

            // Actualiza la posición y rotación de la cámara.
            transform.position = position;
            transform.LookAt(target);
        }
    }
}
