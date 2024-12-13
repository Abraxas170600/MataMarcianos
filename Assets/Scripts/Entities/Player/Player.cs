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
        InputActions();
        base.Update();
        Shoot();
    }
    protected override void Defeat()
    {
        base.Defeat();
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
