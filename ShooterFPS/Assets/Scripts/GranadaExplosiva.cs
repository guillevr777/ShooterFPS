using UnityEngine;

public class GranadaExplosiva : MonoBehaviour
{
    public float radio = 5f;
    public float fuerzaExplosion = 700f;
    public float dano = 100f;
    public GameObject efectoExplosion;

    private bool haExplotado = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!haExplotado)
        {
            Explotar();
            haExplotado = true;
        }
    }

    void Explotar()
    {
        if (efectoExplosion != null)
        {
            Instantiate(efectoExplosion, transform.position, transform.rotation);
        }
        // El ~0 le dice a Unity: "Busca en TODAS las capas sin excepción"
        Collider[] objetosCercanos = Physics.OverlapSphere(transform.position, radio, ~0, QueryTriggerInteraction.Collide);

        foreach (Collider col in objetosCercanos)
        {
            // Buscamos el script en el objeto que tocamos O en sus padres
            EnemyAI enemigo = col.GetComponentInParent<EnemyAI>();

            if (enemigo != null)
            {
                Debug.Log("¡Granada detectó a: " + enemigo.name + "!");
                enemigo.TakeDamage(dano);
            }

            // Empuje físico
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(fuerzaExplosion, transform.position, radio);
            }
        }

        Destroy(gameObject);
    }
} // Esta es la llave que faltaba en la línea 33