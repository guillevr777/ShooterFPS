using UnityEngine;
using UnityEngine.AI; // Imprescindible para usar el NavMesh

public class EnemyAI : MonoBehaviour
{
    public Transform target; // Aquí arrastraremos al Jugador
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target.position);

        // Si está a menos de 1.5 metros, te ataca
        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(0.1f); // Daño por tiempo
        }
    }
}