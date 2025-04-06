using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStates : MonoBehaviour
{
    protected MovementController player;
    protected Rigidbody2D rb; 

    protected virtual void Start()
    {
        player = GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Enter() { this.enabled = true; }
    public virtual void Exit() { this.enabled = false; }
    public virtual void HandleInput() {
        player.setMoveInput(0f);

        if (Input.GetKey(KeyCode.D)) player.setMoveInput(1f);
        if (Input.GetKey(KeyCode.A)) player.setMoveInput(-1f);
    }

    public virtual void HandleIdle() {
        if(player.getMoveInput() == 0f)
        {
           player.ChangeHorizontalState<IdleState>();
        }
    }
}
