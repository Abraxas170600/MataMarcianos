using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnWaveConfig
    {
        public EnemyStorage.EnemyType enemyType; // Tipo de enemigo para la oleada
        public int enemiesPerWave; // Cantidad total de enemigos en la oleada
        public float spawnInterval; // Intervalo entre enemigos
        public int enemiesPerInterval; // Cantidad de enemigos por intervalo
    }

    [SerializeField] private Transform[] spawnTargets; // Puntos de spawn
    [SerializeField] private List<SpawnWaveConfig> spawnWaveConfigs; // Configuración de oleadas
    private Coroutine spawnCoroutine;

    private void Start()
    {
        StartWaveSpawning(); // Inicia las oleadas al inicio del juego
    }

    public void StartWaveSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (true)
        {
            // Seleccionar una configuración aleatoria
            SpawnWaveConfig currentWave = spawnWaveConfigs[Random.Range(0, spawnWaveConfigs.Count)];

            for (int i = 0; i < currentWave.enemiesPerWave; i += currentWave.enemiesPerInterval)
            {
                for (int j = 0; j < currentWave.enemiesPerInterval; j++)
                {
                    if (i + j >= currentWave.enemiesPerWave) break;

                    SpawnEnemy(currentWave.enemyType);
                }

                yield return new WaitForSeconds(currentWave.spawnInterval);
            }

            // Pausa opcional entre oleadas
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void SpawnEnemy(EnemyStorage.EnemyType enemyType)
    {
        int randomIndex = Random.Range(0, spawnTargets.Length);
        Transform spawnPoint = spawnTargets[randomIndex];

        EnemyDispatcher enemyDispatcher = spawnPoint.GetComponent<EnemyDispatcher>();
        if (enemyDispatcher != null)
        {
            enemyDispatcher.SpawnEnemy(enemyType, spawnPoint);
        }
    }
}
