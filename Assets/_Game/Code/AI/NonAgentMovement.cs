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
    private bool AlreadyReactivated = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agentController = GetComponent<AgentController>();
    }

    private void Update()
    {
        if (Time.time - agentDeactivationTime > recoveryTime && !AlreadyReactivated)
        {
            ActivateAgent();
            AlreadyReactivated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Attacker"))
        {
            Debug.Log("Player");
            agentController.EnterAttacked();
            agentDeactivationTime = Time.time;
            recovering = true;
            giroDescontrolado.EmpezarAGirar();
            AlreadyReactivated = false;
        }
    }

    private void ActivateAgent()
    {
        agentController.LeavingAttacked();
        rb.velocity = Vector3.zero;
        recovering = false;
        giroDescontrolado.PararGiro();
    }
}
