using UnityEngine;

public class CameraFollowWithZoom : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // El objeto que la cámara seguirá.

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0, 5, -10); // Desplazamiento inicial de la cámara respecto al objetivo.
    public float smoothSpeed = 0.125f; // Velocidad de interpolación para el movimiento suave.

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f; // Velocidad de zoom con la rueda del mouse.
    public float minDistance = 5f; // Distancia mínima de la cámara al objetivo.
    public float maxDistance = 20f; // Distancia máxima de la cámara al objetivo.

    private float currentDistance; // Distancia actual de la cámara al objetivo.

    void Start()
    {
        currentDistance = offset.magnitude; // Inicializa la distancia de la cámara.
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Zoom con la rueda del mouse.
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            currentDistance -= scrollInput * zoomSpeed; // Ajusta la distancia actual.
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance); // Limita la distancia.

            // Calcula la nueva posición deseada con el zoom.
            Vector3 desiredPosition = target.position + offset.normalized * currentDistance;

            // Interpola suavemente entre la posición actual y la deseada.
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualiza la posición de la cámara.
            transform.position = smoothedPosition;

            // Opcional: Haz que la cámara mire siempre al objetivo.
            transform.LookAt(target);
        }
    }
}
