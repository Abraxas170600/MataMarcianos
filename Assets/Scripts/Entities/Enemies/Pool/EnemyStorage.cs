using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStorage : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPoolConfig
    {
        public EnemyType enemyType;
        public Enemy enemyPrefab;
        public int initialPoolSize = 10;
    }

    public enum EnemyType
    {
        SquareEnemy,
        CircleEnemy,
        TriangleEnemy
    }

    public List<EnemyPoolConfig> enemyConfigs;
    private Dictionary<EnemyType, Queue<Enemy>> enemyDictionary;

    void Awake()
    {
        enemyDictionary = new Dictionary<EnemyType, Queue<Enemy>>();
    }

    public Enemy GetEnemy(EnemyType enemyType)
    {
        if (!enemyDictionary.ContainsKey(enemyType))
        {
            CreatePool(enemyType);
        }

        Queue<Enemy> pool = enemyDictionary[enemyType];

        if (pool.Count > 0)
        {
            Enemy enemy = pool.Dequeue();
            enemy.gameObject.SetActive(true);
            pool.Enqueue(enemy);
            return enemy;
        }
        else
        {
            Enemy newEnemy = Instantiate(GetPrefab(enemyType));
            enemyDictionary[enemyType].Enqueue(newEnemy);
            return newEnemy;
        }
    }

    public void CreatePool(EnemyType enemyType)
    {
        if (enemyDictionary.ContainsKey(enemyType)) return;

        EnemyPoolConfig config = GetConfig(enemyType);
        if (config == null)
        {
            Debug.LogError($"No se encontró configuración para el tipo de enemigo: {enemyType}");
            return;
        }

        Queue<Enemy> pool = new Queue<Enemy>();

        for (int i = 0; i < config.initialPoolSize; i++)
        {
            Enemy enemy = Instantiate(config.enemyPrefab);
            enemy.gameObject.SetActive(false);
            pool.Enqueue(enemy);
        }

        enemyDictionary.Add(enemyType, pool);
    }

    private EnemyPoolConfig GetConfig(EnemyType bulletType)
    {
        return enemyConfigs.Find(config => config.enemyType == bulletType);
    }

    private Enemy GetPrefab(EnemyType bulletType)
    {
        EnemyPoolConfig config = GetConfig(bulletType);
        return config != null ? config.enemyPrefab : null;
    }
}
