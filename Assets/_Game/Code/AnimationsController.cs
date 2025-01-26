using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationsController : MonoBehaviour
{
    public Animator animatorController;

    public void LetGo(bool value)
    {
        animatorController.SetBool("LetGo", value);
    }

    public void Run(float run)
    {
        animatorController.SetFloat("Run", run);
    }
}
