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
        if (other.CompareTag("Player"))  // Cuando el jugador sale del �rea del trigger
        {
            if (catPaw.isAttacking && !catPaw.hasPushedPlayer)  // Si est� atacando y no ha empujado al jugador
            {
                catPaw.ReturnToInitialPosition();  // Regresa la pata a su posici�n inicial
                Debug.Log("El jugador sali� del trigger, devolviendo la pata.");
            }
        }
    }

}
