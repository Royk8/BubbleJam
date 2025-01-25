using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reboundFlat : MonoBehaviour
{
    public float fuerza;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 normal = collision.contacts[0].normal;
            float fuerzaNeg = collision.relativeVelocity.magnitude;
            rb.AddForce(-normal * fuerza, ForceMode.Impulse);

            Debug.Log("Se ha detectado");
        }
        Debug.Log("Se ha detectado");
    }
}
