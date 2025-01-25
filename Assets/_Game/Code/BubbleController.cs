using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HamsterModeHandler hamsterModeHandler = SingletonManager.instance.hamsterModeHandler;
            if (!hamsterModeHandler.isBubbleMode)
            {
                hamsterModeHandler.ActivateBubbleMode();
                gameObject.SetActive(false);
            }
        }
    }
}
