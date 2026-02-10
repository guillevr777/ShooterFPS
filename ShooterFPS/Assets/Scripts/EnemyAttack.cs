using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float tiempoEntreAtaques = 1.0f;
    private float proximoAtaque = 0f;

    private void OnTriggerStay(Collider other)
    {
        // Buscamos en el objeto que tocamos, en sus padres y en sus hijos
        // Usamos el Tag del objeto principal (root) por si acaso
        if (other.transform.root.CompareTag("Player") && Time.time >= proximoAtaque)
        {
            // Buscamos el componente en toda la estructura del Jugador
            PlayerHealth salud = other.transform.root.GetComponentInChildren<PlayerHealth>();

            if (salud != null)
            {
                salud.RecibirDano(1);
                proximoAtaque = Time.time + tiempoEntreAtaques;
            }
            else
            {
                // Este log nos dirá exactamente dónde está buscando
                Debug.LogError("No hay script en: " + other.transform.root.name);
            }
        }
    }
}