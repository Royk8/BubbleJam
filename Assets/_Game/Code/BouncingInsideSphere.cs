using UnityEngine;

public class BouncingInsideSphere : MonoBehaviour
{
    public Transform parentSphere; // Referencia a la esfera exterior
    public float speed = 5f;       // Velocidad del objeto interno
    private Vector3 direction;    // Direcci�n de movimiento

    private float radius;         // Radio de la esfera exterior
    private float innerRadius;    // Radio del objeto interno

    void Start()
    {
        // Establece una direcci�n inicial aleatoria
        direction = Random.onUnitSphere;

        // Calcula el radio de la esfera exterior (escala local)
        radius = parentSphere.localScale.x / 2f;

        // Calcula el radio del objeto interno (escala local)
        innerRadius = transform.localScale.x / 2f;
    }

    void Update()
    {
        // Calcula la nueva posici�n
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // Calcula la distancia del centro de la esfera exterior
        Vector3 offset = newPosition - parentSphere.position;
        float distance = offset.magnitude;

        // Comprueba si el objeto interno alcanza el l�mite
        if (distance + innerRadius > radius)
        {
            // Ajusta la direcci�n usando la normal de colisi�n
            direction = Vector3.Reflect(direction, offset.normalized);

            // Corrige la posici�n para que no atraviese el borde
            newPosition = parentSphere.position + offset.normalized * (radius - innerRadius);
        }

        // Actualiza la posici�n del objeto interno
        transform.position = newPosition;
    }
}
