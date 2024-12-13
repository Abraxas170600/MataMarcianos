using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private int currentLife;
    [SerializeField] private int maxLife;

    [Header("Events")]
    [SerializeField] private UltEvent<int> healthChangeEvent;
    [SerializeField] private UltEvent deathEvent;

    [Header("Attributes")]
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    protected bool isDeath;

    [Header("Dependences")]
    protected Animator entityAnimator;


    protected virtual void Start()
    {
        entityAnimator = GetComponent<Animator>();
        healthChangeEvent.Invoke(currentLife);
    }
    protected virtual void Update()
    {
        Movement();
    }    
    protected abstract void Movement();
    protected virtual void Defeat()
    {
        if (isDeath) return;

        isDeath = true;
        entityAnimator.Play("Death");
        deathEvent.Invoke();
    }
    protected virtual void Attack(Entity targetEntity)
    {
        targetEntity.TakeDamage(damage);
    }

    protected virtual void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount;
        if (currentLife < 0) currentLife = 0;

        entityAnimator.Play("Damaged");
        healthChangeEvent.Invoke(currentLife);

        if (currentLife <= 0 && !isDeath)
        {
            Defeat();
        }
    }
    public void AddHealth(int lifeToAdd)
    {
        if (currentLife == maxLife) return;

        currentLife += lifeToAdd;
        if (currentLife > maxLife) currentLife = maxLife;

        healthChangeEvent.Invoke(currentLife);
    }
    public void FullHealth()
    {
        currentLife = maxLife;
        healthChangeEvent.Invoke(currentLife);
    }
    public void InstaKill()
    {
        currentLife = 0;
        healthChangeEvent.Invoke(currentLife);
        Defeat();
    }
}
