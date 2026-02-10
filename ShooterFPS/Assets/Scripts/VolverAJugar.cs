using UnityEngine.SceneManagement; // Imprescindible para cambiar de escena
using UnityEngine;

public class VolverAJugar : MonoBehaviour
{
    // Esta función la llamará el botón al hacer clic
    public void Jugar()
    {
        // Cargamos la escena del nivel principal
        SceneManager.LoadScene("SampleScene");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Esto solo funciona en el juego ya exportado (.exe)
    }
}