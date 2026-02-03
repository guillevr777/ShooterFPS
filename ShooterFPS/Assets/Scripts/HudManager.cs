using UnityEngine;
using TMPro; // Si usas TextMeshPro

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI enemyText;

    void Update()
    {
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyText.text = "Enemigos restantes: " + count;

        if (count == 0) enemyText.text = "¡Misión Cumplida!";
    }
}