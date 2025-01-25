using UnityEngine;

public class CameraFollowWithZoom : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // El objeto que la c�mara seguir�.

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0, 5, -10); // Desplazamiento inicial de la c�mara respecto al objetivo.
    public float smoothSpeed = 0.125f; // Velocidad de interpolaci�n para el movimiento suave.

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f; // Velocidad de zoom con la rueda del mouse.
    public float minDistance = 5f; // Distancia m�nima de la c�mara al objetivo.
    public float maxDistance = 20f; // Distancia m�xima de la c�mara al objetivo.

    private float currentDistance; // Distancia actual de la c�mara al objetivo.

    void Start()
    {
        currentDistance = offset.magnitude; // Inicializa la distancia de la c�mara.
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Zoom con la rueda del mouse.
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            currentDistance -= scrollInput * zoomSpeed; // Ajusta la distancia actual.
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance); // Limita la distancia.

            // Calcula la nueva posici�n deseada con el zoom.
            Vector3 desiredPosition = target.position + offset.normalized * currentDistance;

            // Interpola suavemente entre la posici�n actual y la deseada.
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualiza la posici�n de la c�mara.
            transform.position = smoothedPosition;

            // Opcional: Haz que la c�mara mire siempre al objetivo.
            transform.LookAt(target);
        }
    }
}
