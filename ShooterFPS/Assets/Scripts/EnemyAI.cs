using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrolling, Chasing, Attacking }
    public State currentState = State.Patrolling;

    [Header("Configuración IA")]
    public float visionRange = 15f;
    public float attackRange = 2f;
    public float patrolRadius = 10f;
    public float patrolWaitTime = 2f;
    public float maxHealth = 100f;

    [Header("Ajustes de Combate")]
    public float attackRate = 2f;
    private float nextAttackTime;

    private float currentHealth;
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    private float patrolTimer;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        currentHealth = maxHealth;
        SetRandomDestination();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrolling:
                UpdateAnimation(agent.velocity.magnitude);
                agent.isStopped = false;
                LookForPlayer();
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    patrolTimer += Time.deltaTime;
                    if (patrolTimer >= patrolWaitTime)
                    {
                        SetRandomDestination();
                        patrolTimer = 0;
                    }
                }
                break;

            case State.Chasing:
                UpdateAnimation(agent.velocity.magnitude);
                agent.isStopped = false;
                agent.SetDestination(player.position);
                if (distance <= attackRange) currentState = State.Attacking;
                break;

            case State.Attacking:
                UpdateAnimation(0);
                agent.isStopped = true;

                if (player != null)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                }

                if (Time.time >= nextAttackTime)
                {
                    if (anim != null) anim.SetTrigger("attack");
                    nextAttackTime = Time.time + attackRate;
                }

                if (distance > attackRange + 0.5f)
                {
                    agent.isStopped = false;
                    currentState = State.Chasing;
                }
                break;
        }
    }

    void UpdateAnimation(float speed)
    {
        if (anim == null) return;
        float finalSpeed = speed > 0.1f ? speed : 0f;
        anim.SetFloat("Speed", finalSpeed);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // ELIMINADO: Ya no busca 'isDead' en el Animator para evitar el error

        // Desaparece el objeto de la escena inmediatamente
        Destroy(gameObject);
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius + transform.position;
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, -1))
            agent.SetDestination(navHit.position);
    }

    void LookForPlayer()
    {
        if (player == null) return;
        if (Vector3.Distance(transform.position, player.position) <= visionRange)
            currentState = State.Chasing;
    }
}