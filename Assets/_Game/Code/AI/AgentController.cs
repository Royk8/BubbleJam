using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    // Start is called before the first frame update

    public enum EnemyState
    {
        Patrolling,
        Chasing,
        Attacking,
        Attacked
    }

    public EnemyState currentState = EnemyState.Patrolling;

    [Header("Patrolling")]
    public Transform[] patrolPoints; // Puntos de patrulla
    private int currentPatrolIndex = 0;

    [Header("Chasing")]
    public Transform player;

    [Header("Attacking")]
    public float attackSpeedMultiplier = 2f;
    public float attackForce = 10f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    [Header("Attacked")]
    public float spinSpeed = 450f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[0].position;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;

            case EnemyState.Chasing:
                Chase();
                break;

            case EnemyState.Attacking:
                Attack();
                break;

            case EnemyState.Attacked:
                Attacked();
                break;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        // Si llega al punto actual, avanzar al siguiente
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }

        // Transición a Chasing si el jugador está cerca
        if (Vector3.Distance(transform.position, player.position) < 10f)
        {
            currentState = EnemyState.Chasing;
        }
    }

    private void Chase()
    {
        // Actualiza el destino del agente al jugador
        agent.destination = player.position;
        Debug.Log("Chasing");

        // Transición a Attacking si está lo suficientemente cerca del jugador
        if (Vector3.Distance(transform.position, player.position) < 2f)
        {
            currentState = EnemyState.Attacking;
        }
        // Volver a Patrolling si el jugador está demasiado lejos
        else if (Vector3.Distance(transform.position, player.position) > 15f)
        {
            currentState = EnemyState.Patrolling;
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        // Aumentar la velocidad del agente para embestir
        agent.speed *= attackSpeedMultiplier;

        // Aplicar fuerza al jugador (requiere Rigidbody en el jugador)
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 attackDirection = (player.position - transform.position).normalized;
            playerRb.AddForce(attackDirection * attackForce, ForceMode.Impulse);
        }

        // Resetear la velocidad y volver a Chasing
        agent.speed /= attackSpeedMultiplier;
        currentState = EnemyState.Chasing;
        lastAttackTime = Time.time;
    }

    public void EnterAttacked()
    {
        currentState = EnemyState.Attacked;
        agent.enabled = false;

    }

    private void Attacked()
    {
    }

    public void LeavingAttacked()
    {
        agent.enabled = true;
        currentState = EnemyState.Patrolling;
    }

    private void OnDrawGizmos()
    {
        // Dibujar puntos de patrulla para visualización
        Gizmos.color = Color.green;
        if (patrolPoints != null)
        {
            foreach (Transform point in patrolPoints)
            {
                Gizmos.DrawSphere(point.position, 0.3f);
            }
        }

        // Dibujar radio de detección y ataque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f); // Radio de detección
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f); // Radio de ataque
    }
}
