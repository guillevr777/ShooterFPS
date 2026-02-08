using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Arrastra aqu� el prefab de tu enemigo
    public Transform[] spawnPoints; // Se llenar� autom�ticamente
    public TextMeshProUGUI counterText;

    private int waveNumber = 0;

    void Start()
    {
        // Busca todos los objetos con el tag Respawn para saber d�nde crear enemigos
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Respawn");
        spawnPoints = new Transform[gos.Length];
        for (int i = 0; i < gos.Length; i++) spawnPoints[i] = gos[i].transform;

        NextWave();
    }

    void Update()
    {
        int enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        counterText.text = "Oleada: " + waveNumber + " | Enemigos: " + enemiesLeft;

        if (enemiesLeft <= 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Creamos el enemigo
            GameObject newEnemy = Instantiate(enemyPrefab, sp.position, sp.rotation);

            // REFUERZO: Ajustar al NavMesh inmediatamente
            UnityEngine.AI.NavMeshAgent agent = newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null)
            {
                UnityEngine.AI.NavMeshHit hit;
                // Busca el punto de NavMesh m�s cercano en un radio de 2 metros
                if (UnityEngine.AI.NavMesh.SamplePosition(sp.position, out hit, 2.0f, UnityEngine.AI.NavMesh.AllAreas))
                {
                    agent.Warp(hit.position); // "Teletransporta" al enemigo a la superficie exacta
                }
            }
        }
    }
}