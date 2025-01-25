using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Transform cameraTransform; // Transform de la cámara que se usará para la rotación.
    public float rotationSpeed = 5.0f; // Velocidad de rotación del objeto.
    public Transform internalObject; // Referencia al objeto interno que no debe rotar.

    void LateUpdate()
    {
        // Obtener la rotación de la cámara en el eje Y (horizontal).
        float targetYRotation = cameraTransform.eulerAngles.y;

        // Crear una nueva rotación solo en el eje Y.
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

        // Rotar suavemente el objeto externo en el eje Y hacia la rotación de la cámara.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Asegurarse de que el objeto interno no se vea afectado por la rotación del objeto externo.
        if (internalObject != null)
        {
            // Restablecer la rotación del objeto interno para que no se vea afectado por el padre.
            internalObject.rotation = Quaternion.identity; // O puedes asignar cualquier otra rotación fija que desees.
        }
    }
}
