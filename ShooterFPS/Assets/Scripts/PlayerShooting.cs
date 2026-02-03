using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject impactPrefab; // Arrastra aquí tu prefab de impacto
    public float damage = 20f;
    public Camera cam;

    // Se activa cuando haces click izquierdo (según tu Player Input)
    public void OnAttack(InputValue value)
    {
        if (value.isPressed) Shoot();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
        {
            // Crea el efecto en el punto exacto del choque
            GameObject impact = Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.5f); // Se borra tras medio segundo para no llenar el PC de basura

            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null) enemy.TakeDamage(damage);
        }
    }
}