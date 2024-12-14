using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private TriggerDetector triggerDetector;

    public UltEvent<Entity> damageEvent;
    private void Start()
    {
        triggerDetector = GetComponent<TriggerDetector>();
    }
    private void Update()
    {
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x) > 7f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void SetTransformValues(Transform bulletSpawnerTarget)
    {
        transform.position = bulletSpawnerTarget.position;
        transform.rotation = bulletSpawnerTarget.rotation;
    }
    public void Damage()
    {
        Enemy target = triggerDetector.LastElementDetected.GetComponent<Enemy>();
        if(target)
        {
            damageEvent.Invoke(target);
            gameObject.SetActive(false);
        }
    }
}
