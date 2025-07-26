using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{

    [SerializeField] private float maxStamina = 10f;
    private float currentStamina;
    [SerializeField] private float idleStaminaGain = 0.01f;
    [SerializeField] private float walkingStaminaGain = 0.1f;
    [SerializeField] private float runningStaminaLoss = 0.3f;
    [SerializeField] private float jumpingStaminaLoss = 0.5f;

    void Start()
    {
        currentStamina = maxStamina;
    }

    public float getCurrentStamina() => currentStamina;

    public bool hasLackOfStamina()
    {
        return currentStamina < maxStamina;
    }

    public void GainStamina(float amount)
    {
        if (currentStamina + amount > maxStamina || currentStamina == maxStamina)
        {
            currentStamina = maxStamina;
            return;
        }
        currentStamina += amount;
    }

    public void LoseStamina(float amount)
    {
        if (currentStamina - amount < 0 || currentStamina == 0)
        {
            currentStamina = 0;
            return;
        }
        currentStamina -= amount;
    }
}
