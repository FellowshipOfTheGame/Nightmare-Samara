
using UnityEngine;

public class StaminaSystem
{

    Player player;

    public StaminaSystem(Player player)
    {
        this.player = player;
    }

    public void GainStamina(float amount)
    {
        player.currentStamina = Mathf.Min(player.currentStamina + amount * Time.fixedDeltaTime, player.maxStamina);
    }

    public void LoseStamina(float amount)
    {
        // Garante que o valor não desça de zero.
        player.currentStamina = Mathf.Max(player.currentStamina - amount * Time.fixedDeltaTime, 0);
    }
}
