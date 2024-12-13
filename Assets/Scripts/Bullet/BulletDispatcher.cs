using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDispatcher : MonoBehaviour
{
    [SerializeField] private BulletStorage.BulletType selectedBulletType;
    [SerializeField] private BulletStorage bulletStorage;
    [SerializeField] private Transform bulletSpawnerTarget;
    private void Start()
    {
        bulletStorage.CreatePool(selectedBulletType);
    }
    public void FireBullet()
    {
        Bullet bullet = bulletStorage.GetBullet(selectedBulletType);
        if (bullet != null)
        {
            bullet.SetTransformValues(bulletSpawnerTarget);
        }
    }
}
