using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float attackDuration = 0.5f;
    private float startTime;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        ItemType equippedItem = player.inventorySystem.GetEquippedItem();

        int itemCount = player.inventorySystem.GetItemCount(equippedItem);

        switch (equippedItem)
        {
            case ItemType.Hand:
                Debug.Log("Ataque com a mão");
                player.TriggerNormalAttack();
                break;

            case ItemType.WoddenBat:
                if (itemCount > 0)
                {
                    Debug.Log($"Ataque com bastão. Usos restantes: {itemCount - 1}");
                    player.TriggerNormalAttack();

                    player.inventorySystem.UseItem(ItemType.PoisonPot);
                }
                break;

            case ItemType.PoisonPot:
                if (itemCount > 0)
                {
                    Debug.Log("Ataque com veneno");
                    // player.ThrowPoisonPot();
                    player.inventorySystem.UseItem(ItemType.PoisonPot);
                }
                else
                {
                    Debug.Log("Tentou jogar veneno, mas não tem");
                }
                break;
        }

        startTime = Time.time;
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
    }

  

    public override void FrameUpdate()
    {
        if (Time.time >= startTime + attackDuration)
        {
            if (player.isGrounded)
            {
                if (player.inputSystem.MoveInput.x != 0)
                {
                    if (player.inputSystem.RunInput)
                    {
                        stateMachine.ChangeState(player.runningState);
                        return;
                    }
                    else
                    {
                        stateMachine.ChangeState(player.walkingState);
                        return;
                    }
                }
                else
                {
                    stateMachine.ChangeState(player.idleState);
                    return;
                }
            }
            else
            {
                stateMachine.ChangeState(player.fallingState);
                return;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
    }

    public override void Exit()
    {
        Debug.Log("Ataque Fim");
    }
}
