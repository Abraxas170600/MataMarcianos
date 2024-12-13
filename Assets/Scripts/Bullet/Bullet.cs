using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private TriggerDetector triggerDetector;

    public UltEvent<Entity> damageEvent { get; private set; }
    private void Start()
    {
        triggerDetector = GetComponent<TriggerDetector>();
    }
    private void Update()
    {
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x) > 10f)
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
        Entity target = triggerDetector.LastElementDetected.GetComponent<Entity>();
        damageEvent.Invoke(target);
        gameObject.SetActive(false);
    }
}