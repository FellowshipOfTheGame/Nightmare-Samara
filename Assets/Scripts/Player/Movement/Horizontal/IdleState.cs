using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerStates
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) player.ChangeVerticalState<JumpingState>();
    }

    private void FixedUpdate()
    {
        HandleInput();
        if (player.getMoveInput() != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                player.ChangeHorizontalState<RunningState>();
            }
            else
            {
                player.ChangeHorizontalState<WalkingState>();
            }

        }
    }
}

