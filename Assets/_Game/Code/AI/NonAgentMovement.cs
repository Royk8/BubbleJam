using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAgentMovement : MonoBehaviour
{
    private Rigidbody rb;
    private AgentController agentController;
    private float agentDeactivationTime;
    public float recoveryTime;
    public bool recovering= false;
    public GiroDescontrolado giroDescontrolado;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agentController = GetComponent<AgentController>();
    }

    private void Update()
    {
        if (Time.time - agentDeactivationTime > recoveryTime)
        {
            ActivateAgent();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            agentController.DeactivateAgent();
            agentDeactivationTime = Time.time;
            recovering = true;
            giroDescontrolado.EmpezarAGirar();
        }
    }

    private void ActivateAgent()
    {
        agentController.ActivateAgent();
        rb.velocity = Vector3.zero;
        recovering = false;
        giroDescontrolado.PararGiro();
    }
}
