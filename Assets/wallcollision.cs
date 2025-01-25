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

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Cuando el jugador sale del área del trigger
        {
            if (catPaw.isAttacking && !catPaw.hasPushedPlayer)  // Si está atacando y no ha empujado al jugador
            {
                catPaw.ReturnToInitialPosition();  // Regresa la pata a su posición inicial
                Debug.Log("El jugador salió del trigger, devolviendo la pata.");
            }
        }
    }

}
