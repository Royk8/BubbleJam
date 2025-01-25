using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawCollision : MonoBehaviour
{
    public CatPaw padre;
    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && padre.isAttacking)  // Verifica si está atacando y colisiona con el jugador
        {
            padre.isAttacking = false;  // Detiene el ataque
            padre.pawRigidbody.velocity = Vector3.zero;  // Detiene el movimiento de la pata

            padre.PushPlayer(other.collider);  // Aplica el empuje
            padre.ReturnToInitialPosition();  // Regresa la pata a su posición inicial
        }
    }
}
