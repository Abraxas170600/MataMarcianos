using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Vector2 boundsOffsetMin = Vector2.zero;
    [SerializeField] private Vector2 boundsOffsetMax = Vector2.zero;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private Vector2 movementInput;

    [SerializeField] private float invulnerabilityDuration = 2f;
    private bool isInvulnerable = false;
    protected override void Start()
    {
        base.Start();
        GetLimits();
    }
    private void GetLimits()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y) + boundsOffsetMin;
        maxBounds = new Vector2(topRight.x, topRight.y) + boundsOffsetMax;
    }
    protected override void Update()
    {
        if (!isDeath)
        {
            InputActions();
            base.Update();
            if (!isInvulnerable)
            {
                Shoot();
            }
        }
    }
    protected override void Defeat()
    {
        base.Defeat();
        GetComponent<CircleCollider2D>().enabled = false;
    }
    public void AttackEnemy(Entity enemy)
    {
        Attack(enemy);
    }
    protected override void TakeDamage(int damageAmount)
    {
        if (isInvulnerable) return;

        entityAnimator.Play("Damaged");
        base.TakeDamage(damageAmount);
        StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerabilityDuration);

        isInvulnerable = false;
    }
    private void InputActions()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.Normalize();
    }
    protected override void Movement()
    {
        if (!isDeath)
        {
            Vector3 newPosition = transform.position + (Vector3)(movementInput * speed * Time.deltaTime);

            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

            transform.position = newPosition;
        }
    }
    private void Shoot()
    {
        if (Input.GetKey(KeyCode.K))
        {
            entityAnimator.Play("Attack");
        }
    }
}
