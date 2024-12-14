using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDispatcher : MonoBehaviour
{
    [SerializeField] private EnemyStorage enemyStorage;

    public void SpawnEnemy(EnemyStorage.EnemyType enemyType, Transform spawnPoint)
    {
        Enemy enemy = enemyStorage.GetEnemy(enemyType);
        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.gameObject.SetActive(true);
        }
    }
}
