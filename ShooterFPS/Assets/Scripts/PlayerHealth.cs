using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 2;
    private int vidaActual;
    // Cambiamos el nombre aquí para que coincida con tu escena
    public string nombreEscenaMenu = "MenuInicio";

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("¡Ay! Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("GAME OVER - Volviendo al Menú de Inicio");
        // Cargará la escena llamada MenuInicio
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}