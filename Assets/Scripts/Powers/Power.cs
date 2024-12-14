using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    [SerializeField] private float activeTime = 5f;
    protected Player player;

    public float ActiveTime { get => activeTime; set => activeTime = value; }

    protected abstract void Activate();
    protected abstract void Deactivate();
}
