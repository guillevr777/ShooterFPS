using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el nuevo sistema

public class GranadaSuelo : MonoBehaviour
{
    private bool jugadorCerca = false;

    // Se activa cuando el jugador entra en el círculo/caja de la granada
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Jugador cerca de la granada. Pulsa E.");
        }
    }

    // Se activa cuando el jugador se aleja
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = false;
    }

    void Update()
    {
        // Detecta la tecla E usando el New Input System
        if (jugadorCerca && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Recoger();
        }
    }

    void Recoger()
    {
        // Busca el inventario en el objeto con el Tag "Player"
        PlayerInventory inventario = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();

        if (inventario != null)
        {
            inventario.RecogerGranada();
            Debug.Log("Granada recogida correctamente.");
            Destroy(gameObject); // Elimina la granada del suelo
        }
        else
        {
            Debug.LogError("No se encontró el script PlayerInventory en el Jugador.");
        }
    }
}