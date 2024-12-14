using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : Enemy
{
    private float amplitude;
    private float frequency;
    private float initialY;

    protected override void Start()
    {
        base.Start();
        initialY = transform.position.y;
        amplitude = Random.Range(1f, 3f);
        frequency = Random.Range(1f, 3f);
    }

    protected override void Movement()
    {
        if (isActiveAndEnabled)
        {
            float newY = initialY + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position += new Vector3(-speed * Time.deltaTime, newY - transform.position.y, 0);

            if (transform.position.x < -10f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
