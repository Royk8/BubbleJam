using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader.instance.GoToPlayScene();
        }
    }
}
