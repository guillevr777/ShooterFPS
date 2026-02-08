using UnityEngine;
using UnityEngine.SceneManagement; // Imprescindible para cambiar de escena

public class MenuPrincipal : MonoBehaviour
{
    public void JugarJuego()
    {
        // Carga la siguiente escena en la lista
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}