using UnityEngine;
using UnityEngine.InputSystem; // ¡Importante añadir esta línea!

public class PlayerInventory : MonoBehaviour
{
    public int cantidadGranadas = 0;
    public GameObject granadaPrefab;
    public Transform throwPoint;
    public float fuerzaLanzamiento = 15f;

    void Update()
    {
        // Nueva forma de detectar la tecla T
        if (Keyboard.current.tKey.wasPressedThisFrame && cantidadGranadas > 0)
        {
            LanzarGranada();
        }
    }

    public void RecogerGranada()
    {
        cantidadGranadas++;
        Debug.Log("Granadas actuales: " + cantidadGranadas);
    }

    void LanzarGranada()
    {
        cantidadGranadas--;
        GameObject g = Instantiate(granadaPrefab, throwPoint.position, throwPoint.rotation);

        // Intentamos obtener el Rigidbody
        Rigidbody rb = g.GetComponent<Rigidbody>();

        // Si la granada TIENE Rigidbody, la lanzamos
        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * fuerzaLanzamiento + Vector3.up * 2f, ForceMode.Impulse);
        }
        else
        {
            // Si NO tiene, nos avisa con un error amigable en la consola
            Debug.LogError("¡Cuidado! El prefab de la granada no tiene un componente Rigidbody.");
        }
    }
}