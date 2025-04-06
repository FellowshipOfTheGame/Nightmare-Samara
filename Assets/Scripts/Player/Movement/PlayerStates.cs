using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStates : MonoBehaviour
{
    protected MovementController player;

    protected virtual void Awake()
    {
        player = GetComponent<MovementController>();
    }

    public virtual void Enter() { this.enabled = true; }
    public virtual void Exit() { this.enabled = false; }
}
