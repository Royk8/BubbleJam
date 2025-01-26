using UnityEngine;

public class GanchoController : MonoBehaviour
{
    public LineRenderer lineaGancho;   // Referencia al LineRenderer del gancho
    public Transform puntoGancho;     // El punto desde donde sale el gancho (puede ser un objeto separado)
    public Transform jugador;         // Referencia al jugador
    public float velocidadGancho = 15f; // Velocidad del movimiento hacia el gancho
    public float distanciaMaxima = 20f; // Distancia máxima para enganchar
    private bool enganchado = false;   // Si el gancho está enganchado o no
    private Vector3 objetivo;          // El punto donde se engancha el gancho
    private Rigidbody rbJugador;       // El Rigidbody del jugador

    void Start()
    {
        lineaGancho.enabled = false; // Desactivar la línea del gancho al inicio
        rbJugador = jugador.GetComponent<Rigidbody>();  // Obtener el Rigidbody del jugador
    }

    void Update()
    {
        // Si el gancho está enganchado, dibuja la línea
        if (lineaGancho.enabled)
        {
            DibujaLineaGancho();
        }

        // Si el gancho está enganchado, mueve al jugador hacia el objetivo
        if (enganchado)
        {
            MoverJugadorHaciaGancho();
        }

        // Lanzar el gancho
        if (Input.GetMouseButtonDown(0) && !enganchado) // Click izquierdo para lanzar
        {
            LanzarGancho();
        }
        // Soltar el gancho
        else if (Input.GetMouseButtonUp(0) && enganchado) // Click izquierdo para soltar
        {
            SoltarGancho();
        }
    }

    void LanzarGancho()
    {
        RaycastHit hit;
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Si el raycast impacta en algo dentro de la distancia máxima
        if (Physics.Raycast(rayo, out hit, distanciaMaxima))
        {
            objetivo = hit.point;  // Definir el punto de enganche
            enganchado = true;     // Activar el estado enganchado
            lineaGancho.enabled = true; // Activar la línea del gancho
        }
    }

    void MoverJugadorHaciaGancho()
    {
        // Calcular la dirección hacia el objetivo
        Vector3 direccion = (objetivo - jugador.position).normalized;

        // Mover al jugador hacia el objetivo
        rbJugador.MovePosition(Vector3.MoveTowards(jugador.position, objetivo, velocidadGancho * Time.deltaTime));

        // Si el jugador llega al objetivo, soltar el gancho
        if (Vector3.Distance(jugador.position, objetivo) < 0.5f)
        {
            SoltarGancho();
        }
    }

    void SoltarGancho()
    {
        enganchado = false;              // Desactivar el estado enganchado
        lineaGancho.enabled = false;     // Desactivar la línea del gancho
    }

    void DibujaLineaGancho()
    {
        // Actualiza las posiciones de la línea: el punto de origen y el punto de enganche
        lineaGancho.SetPosition(0, puntoGancho.position); // El origen (punto del gancho)
        lineaGancho.SetPosition(1, objetivo);            // El destino (punto enganchado)
    }
}
