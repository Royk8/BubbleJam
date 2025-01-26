using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    public float timeToChangeScene = 0.5f;
    private void OnTriggerExit(Collider other)
    {
        timeToChangeScene = 0.5f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeToChangeScene -= Time.deltaTime;
            if (timeToChangeScene <= 0)
            {
                SceneLoader.instance.GoToPlayScene();
            }
        }
    }
}
