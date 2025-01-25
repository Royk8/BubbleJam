using UnityEngine;

public class BouncingInsideSphere : MonoBehaviour
{
    public Transform parentSphere; // Referencia a la esfera exterior
    public float speed = 5f;       // Velocidad del objeto interno
    private Vector3 direction;    // Dirección de movimiento

    private float radius;         // Radio de la esfera exterior
    private float innerRadius;    // Radio del objeto interno

    void Start()
    {
        // Establece una dirección inicial aleatoria
        direction = Random.onUnitSphere;

        // Calcula el radio de la esfera exterior (escala local)
        radius = parentSphere.localScale.x / 2f;

        // Calcula el radio del objeto interno (escala local)
        innerRadius = transform.localScale.x / 2f;
    }

    void Update()
    {
        // Calcula la nueva posición
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // Calcula la distancia del centro de la esfera exterior
        Vector3 offset = newPosition - parentSphere.position;
        float distance = offset.magnitude;

        // Comprueba si el objeto interno alcanza el límite
        if (distance + innerRadius > radius)
        {
            // Ajusta la dirección usando la normal de colisión
            direction = Vector3.Reflect(direction, offset.normalized);

            // Corrige la posición para que no atraviese el borde
            newPosition = parentSphere.position + offset.normalized * (radius - innerRadius);
        }

        // Actualiza la posición del objeto interno
        transform.position = newPosition;
    }
}
