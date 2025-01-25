using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Transform cameraTransform; // Transform de la c�mara que se usar� para la rotaci�n.
    public float rotationSpeed = 5.0f; // Velocidad de rotaci�n del objeto.
    public Transform internalObject; // Referencia al objeto interno que no debe rotar.

    void LateUpdate()
    {
        // Obtener la rotaci�n de la c�mara en el eje Y (horizontal).
        float targetYRotation = cameraTransform.eulerAngles.y;

        // Crear una nueva rotaci�n solo en el eje Y.
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

        // Rotar suavemente el objeto externo en el eje Y hacia la rotaci�n de la c�mara.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Asegurarse de que el objeto interno no se vea afectado por la rotaci�n del objeto externo.
        if (internalObject != null)
        {
            // Restablecer la rotaci�n del objeto interno para que no se vea afectado por el padre.
            internalObject.rotation = Quaternion.identity; // O puedes asignar cualquier otra rotaci�n fija que desees.
        }
    }
}
