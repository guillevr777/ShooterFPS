using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 60f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); // Esto elimina al enemigo de la escena
        }
    }
}