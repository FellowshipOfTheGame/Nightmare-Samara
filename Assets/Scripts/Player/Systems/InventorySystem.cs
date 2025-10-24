using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    Player player;

    public Dictionary<ItemType, int> quantities = new Dictionary<ItemType, int>();

    private List<ItemType> cycleOrder = new List<ItemType> { ItemType.Hand, ItemType.WoddenBat, ItemType.PoisonPot };
    private int currentCycleIndex = 0;

    private const int USES_PER_BAT = 3;

    public InventorySystem()
    {
        // Inicializa o inventário
        quantities[ItemType.Hand] = 1; // 1 "mão" (sempre disponível)
        quantities[ItemType.WoddenBat] = 0;
        quantities[ItemType.PoisonPot] = 0;
        quantities[ItemType.HealthItem] = 0;
    }

    public void AddItem(ItemType type, int amountPickedUp)
    {
        if (quantities.ContainsKey(type) == false)
        {
            quantities.Add(type, 0);
        }

        int amountToAdd = 0;

        switch (type)
        {
            case ItemType.WoddenBat:
                amountToAdd = amountPickedUp * USES_PER_BAT;
                break;

            case ItemType.PoisonPot:
            case ItemType.HealthItem:
            default:
                amountToAdd = amountPickedUp;
                break;
        }

        quantities[type] += amountToAdd;
        Debug.Log($"Adicionou {amountPickedUp} de {type}. Total de *usos/itens* agora é: {quantities[type]}");
    }

    public void UseItem(ItemType type)
    {
        if (type == ItemType.Hand) return;

        if (quantities.ContainsKey(type) && quantities[type] > 0)
        {
            quantities[type]--;
            Debug.Log($"Usou {type}. Restam (usos/itens): {quantities[type]}");
        }
    }

    public int GetItemCount(ItemType type)
    {
        if (quantities.ContainsKey(type))
        {
            return quantities[type];
        }
        return 0;
    }

    public ItemType GetEquippedItem()
    {
        return cycleOrder[currentCycleIndex];
    }

    public void GoToNext()
    {
        currentCycleIndex++;
        if (currentCycleIndex >= cycleOrder.Count)
        {
            currentCycleIndex = 0; 
        }
        Debug.Log($"Item equipado: {GetEquippedItem()}");
    }

    public void GoToLast()
    {
        currentCycleIndex--;
        if (currentCycleIndex < 0)
        {
            currentCycleIndex = cycleOrder.Count - 1;
        }
        Debug.Log($"Item equipado: {GetEquippedItem()}");
    }

}
