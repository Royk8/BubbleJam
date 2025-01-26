using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 3;                // M�xima cantidad de vidas
    int currentHealth;                      // Vidas actuales del jugador
    public Transform vidasContainer;        // Contenedor de las im�genes de vidas (en el Canvas)

    public int ReceiveDamage(int damage = 1)
    {
        currentHealth -= damage;

        // Actualiza la UI eliminando un coraz�n
        if (currentHealth >= 0)
        {
            ActualizarUI();
        }

        SingletonManager.instance.cameraEffects.ShakeCamera();

        return currentHealth;
    }

    public void Start()
    {
        currentHealth = maxHealth;

        // Aseg�rate de que la UI tenga el n�mero correcto de corazones
        ActualizarUI();
    }

    // Actualiza las im�genes de vidas en la UI
    void ActualizarUI()
    {
        for (int i = 0; i < vidasContainer.childCount; i++)
        {
            GameObject vida = vidasContainer.GetChild(i).gameObject;
            if (i < currentHealth)
            {
                vida.SetActive(true);  // Muestra los corazones restantes
            }
            else
            {
                vida.SetActive(false); // Oculta los corazones perdidos
            }
        }
    }
}
