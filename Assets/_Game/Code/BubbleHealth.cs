using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 3;
    int currentHealth;

    public int ReceiveDamage(int damage = 1)
    {
        currentHealth -= damage;
        SingletonManager.instance.cameraEffects.ShakeCamera();
        return currentHealth;
    }

    public void Start()
    {
        currentHealth = maxHealth;
    }


}
