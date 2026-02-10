using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;  // <- Cambia este using

public class VolverAJugar : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)  // <- Y esta línea
        {
            Jugar();
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}