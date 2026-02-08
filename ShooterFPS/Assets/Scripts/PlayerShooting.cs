using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject impactPrefab;
    public float damage = 20f;
    public Camera cam;

    public void OnAttack(InputValue value)
    {
        if (value.isPressed) Shoot();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
        {
            // Instanciar impacto si lo tienes
            if (impactPrefab != null)
            {
                GameObject impact = Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.5f);
            }

            // Buscamos el componente de IA
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}