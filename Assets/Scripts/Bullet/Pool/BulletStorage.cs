using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStorage : MonoBehaviour
{
    [System.Serializable]
    public class BulletPoolConfig
    {
        public BulletType bulletType;
        public Bullet bulletPrefab;
        public int initialPoolSize = 10;
    }

    public enum BulletType
    {
        Basic
    }

    public List<BulletPoolConfig> bulletConfigs;
    private Dictionary<BulletType, Queue<Bullet>> bulletDictionary;

    void Awake()
    {
        bulletDictionary = new Dictionary<BulletType, Queue<Bullet>>();
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        Queue<Bullet> pool = bulletDictionary[bulletType];

        if (pool.Count > 0)
        {
            Bullet bullet = pool.Dequeue();
            bullet.gameObject.SetActive(true);
            pool.Enqueue(bullet);
            return bullet;
        }
        else
        {
            Bullet newBullet = Instantiate(GetPrefab(bulletType));
            bulletDictionary[bulletType].Enqueue(newBullet);
            return newBullet;
        }
    }

    public void CreatePool(BulletType bulletType)
    {
        if (bulletDictionary.ContainsKey(bulletType)) return;

        BulletPoolConfig config = GetConfig(bulletType);
        if (config == null)
        {
            Debug.LogError($"No se encontró configuración para el tipo de bala: {bulletType}");
            return;
        }

        Queue<Bullet> pool = new Queue<Bullet>();

        for (int i = 0; i < config.initialPoolSize; i++)
        {
            Bullet bullet = Instantiate(config.bulletPrefab);
            bullet.gameObject.SetActive(false);
            pool.Enqueue(bullet);
        }

        bulletDictionary.Add(bulletType, pool);
    }

    private BulletPoolConfig GetConfig(BulletType bulletType)
    {
        return bulletConfigs.Find(config => config.bulletType == bulletType);
    }

    private Bullet GetPrefab(BulletType bulletType)
    {
        BulletPoolConfig config = GetConfig(bulletType);
        return config != null ? config.bulletPrefab : null;
    }
}
