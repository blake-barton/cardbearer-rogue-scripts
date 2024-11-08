using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows an entity to contain other entities.
/// </summary>
[System.Serializable]
public class ECInventory : EntityComponent
{
    [SerializeField] List<Pair<Entity, int>> inventory = new();

    public ECInventory(Entity _entity) : base(_entity)
    {
    }

    public ECInventory(Entity _entity, JToken token) : base(_entity, token)
    {
        // Load initial items into inventory
        JArray itemNames = (JArray)token["inventory"];

        foreach (JToken itemToken in itemNames) 
        {
            string itemName = (string)itemToken;
            Entity item = EntityPrefabManager.Instance.GetPrefab(itemName, EntityPrefabManager.PrefabType.Item);

            if (item != null)
            {
                AddItem(item);
            }
            else
            {
                Debug.Log("ECInventory: Received a null item when looking for item with name: " + itemName);
            }
        }
    }

    public List<Pair<Entity, int>> Inventory { get => inventory; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    /// <summary>
    /// Add the given entity to the inventory. If there identical items, add it to the existing stack. Returns destination pair. Quantity must be positive.
    /// </summary>
    public Pair<Entity, int> AddItem(Entity entity, int quantity = 1)
    {
        // Null check
        if (entity is null)
        {
            Debug.LogError("Tried to add null entity to inventory.");
            return null;
        }

        // Non-positive quantity check
        if (quantity < 1)
        {
            Debug.LogWarning("Tried to add less than 1 item. Setting quantity to 1.");
            quantity = 1;
        }

        // Look for existing stacks of the same name
        List<Pair<Entity, int>> existingStacks = Inventory.FindAll(e => e.First.Name.Equals(entity.Name));
        if (existingStacks.Count > 0)
        {
            // Check if any of these existing stacks are EXACTLY the same
            foreach (var stack in existingStacks)
            {
                // If this item is identical, add its quantity to the stack.
                if (stack.First.ValueEquals(entity))
                {
                    stack.Second += quantity;
                    return stack;
                }
            }
        }

        // No fitting stack. Create a new one.
        Pair<Entity, int> newStack = new(entity, quantity);
        Inventory.Add(newStack);

        return newStack;
    }

    /// <summary>
    /// Returns the first entity from a stack with the given name.
    /// </summary>
    public Entity FindItem(string entityName)
    {
        return Inventory.Find(p => p.First.Name.Equals(entityName)).First;
    }

    public override string ToString()
    {
        string outString = "-- INVENTORY --\n";

        foreach (var stack in Inventory)
        {
            // Mark equipped items
            ECEquippable equippable = stack.First.GetComponent<ECEquippable>();
            if (equippable != null)
            {
                if (equippable.CurrentlyEquippedSlot != null)
                {
                    outString += "--o-- ";
                }
            }

            if (stack.Second > 1)
            {
                outString += stack.First.Name + " (" + stack.Second + ")\n";
            }
            else
            {
                outString += stack.First.Name + "\n";
            }
        }

        outString += "\nStack Count = " + Inventory.Count + "\n---------------";

        return outString;
    }
}
