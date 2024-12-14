using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : Enemy
{
    private Vector2 randomDirection;
    private float changeInterval = 0.5f;
    private float timer;

    protected override void Start()
    {
        base.Start();
        randomDirection = new Vector2(Random.Range(-1f, 0f), Random.Range(-1f, 1f)).normalized;
        timer = changeInterval;
    }

    protected override void Movement()
    {
        if (isActiveAndEnabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                randomDirection = new Vector2(Random.Range(-1f, 0f), Random.Range(-1f, 1f)).normalized;
                timer = changeInterval;
            }

            transform.position += new Vector3(randomDirection.x, randomDirection.y, 0) * speed * Time.deltaTime;

            if (transform.position.x < -10f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
