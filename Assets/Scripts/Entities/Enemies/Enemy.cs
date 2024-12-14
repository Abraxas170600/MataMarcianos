using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected int scorePoints;
    private TriggerDetector triggerDetector;
    protected override void Start()
    {
        base.Start();
        triggerDetector = GetComponent<TriggerDetector>();
    }
    protected override void Movement()
    {
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x) > 10f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void Damage()
    {
        Player player = triggerDetector.LastElementDetected.GetComponent<Player>();
        if (player)
        {
            Attack((Entity)player);
        }
    }
    public void DesactiveEnemy()
    {
        isDeath = false;
        FullHealth();
        ScoreManager.Instance.ChangeScore(scorePoints);
        gameObject.SetActive(false);
    }
}
