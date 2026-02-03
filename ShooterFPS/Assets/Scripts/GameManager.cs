using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI counterText; // Arrastra aquí tu texto de la UI

    void Update()
    {
        // Busca cuántos objetos tienen el Tag "Enemy" en la escena
        int enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesLeft > 0)
        {
            counterText.text = "Enemigos restantes: " + enemiesLeft;
        }
        else
        {
            counterText.text = "¡ZONA DESPEJADA! Victoria.";
            // Aquí podrías añadir lógica para cargar el siguiente nivel
        }
    }
}