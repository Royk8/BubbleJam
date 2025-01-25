using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiroDescontrolado : MonoBehaviour
{
    public float speed = 10.0f;
    private bool girando = false;

    private void Update()
    {
        if (girando)
        {
            GirarDescontrolado();
        }
    }

    public void EmpezarAGirar()
    {
        girando = true;
    }

    public void GirarDescontrolado()
    {
        transform.Rotate(Vector3.left, speed * Time.deltaTime);
    }

    public void PararGiro()
    {
        transform.Rotate(Vector3.right, 0f);
        girando = false;
    }
}
