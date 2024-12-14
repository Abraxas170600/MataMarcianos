using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : Enemy
{
    private float amplitude;
    private float frequency;

    protected override void Start()
    {
        base.Start();
        amplitude = Random.Range(1f, 3f);
        frequency = Random.Range(1f, 3f);
    }
    protected override void Movement()
    {
        if (isActiveAndEnabled)
        {
            float waveY = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position += new Vector3(-speed * Time.deltaTime, waveY * Time.deltaTime, 0);

            if (transform.position.x < -10f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
