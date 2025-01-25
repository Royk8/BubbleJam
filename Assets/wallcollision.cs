using UnityEngine;

public class WallCollision : MonoBehaviour
{
    public CatPaw catPaw;  // Referencia al script de la pata

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Cuando el jugador entra en contacto con la pared
        {
            catPaw.StartAttack(other.transform);  // Inicia el ataque de la pata
        }
    }
}
