using UnityEngine;

public class HamsterElevator : MonoBehaviour
{
    private bool isOnElevator = false;
    private Elevator elevatorScript;

    void Start()
    {
        elevatorScript = FindObjectOfType<Elevator>(); // Encuentra el script del elevador
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            isOnElevator = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            isOnElevator = false;
        }
    }

    void Update()
    {
        if (isOnElevator)
        {
            // Mover al hámster con el elevador si está sobre él
            transform.position = new Vector3(transform.position.x, elevatorScript.transform.position.y, transform.position.z);
        }
    }
}
