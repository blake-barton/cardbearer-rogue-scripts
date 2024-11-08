using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ECEquipment : EntityComponent
{
    Dictionary<string, ECEquippable> slots = new();

    public ECEquipment(Entity _entity) : base(_entity)
    {
    }

    public ECEquipment(Entity _entity, JToken token) : base(_entity, token)
    {
        // Load slots from json
        JArray slotsArray = (JArray)token["slots"];
        foreach (JToken slotToken in slotsArray)
        {
            slots.Add((string)slotToken, null);
        }
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    /// <summary>
    /// Equip the equippable in the given slot. Returns true if successfully equipped or if this item was already equipped and is now unequipped.
    /// </summary>
    public bool Equip(ECEquippable equippable, GameplayTag slot)
    {
        // Null check
        if (equippable is null)
        {
            LogManager.Instance.LogError("Tried to equip a null equippable.");
            return false;
        }

        // Check that equippable can go to this slot
        if (!equippable.PossibleSlots.Contains(slot))
        {
            LogManager.Instance.LogWarning("Cannot equip " + equippable.Entity.Name + " in slot: " + slot);
            return false;
        }

        // Check that slot exists
        if (!slots.ContainsKey(slot.ToString()))
        {
            LogManager.Instance.LogWarning("No slot found: " + slot);
            return false;
        }

        // Unequip and return if this equippable is already in the slot
        if (slots[slot.ToString()] == equippable)
        {
            Unequip(slot.ToString());
            return true;
        }

        // Unequip equippable in slot if there is one
        Unequip(slot.ToString());

        // Check if the weapon is equipped in another slot
        if (slots.ContainsValue(equippable))
        {
            // Unequip in that slot
            string otherSlot = slots.FirstOrDefault(x => x.Value == equippable).Key;
            Unequip(otherSlot);
        }

        // Equip
        slots[slot.ToString()] = equippable;

        // Mark equippable equipped
        equippable.CurrentlyEquippedSlot = slot;

        return true;
    }

    /// <summary>
    /// Unequip the equippable at the given slot. Returns the equippable that was unequipped.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public ECEquippable Unequip(string slot)
    {
        // Check slot exists
        if (slots.ContainsKey(slot))
        {
            // Check slot is occupied
            if (slots[slot] is null)
            {
                return null;
            }

            // Remove equippable from slot
            ECEquippable unequipped = slots[slot];
            slots[slot] = null;

            // Mark equippable unequipped
            unequipped.CurrentlyEquippedSlot = null;

            // Return the unequipped equippable.
            return unequipped;
        }

        LogManager.Instance.LogWarning("No slot found: " + slot);
        return null;
    }

    /// <summary>
    /// Return the equippable in the given slot.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public ECEquippable GetEquippableInSlot(GameplayTag slot)
    {
        // Check slot exists
        if (slots.ContainsKey(slot.ToString()))
        {
            // Return value at slot
            return slots[slot.ToString()];
        }

        LogManager.Instance.LogWarning("No slot found: " + slot);
        return null;
    }

    public override string ToString()
    {
        string outString = "-- EQUIPMENT --\n";

        foreach (var slot in slots)
        {
            outString += slot.Key.ToString();

            if (slot.Value is null)
            {
                outString += "[EMPTY]";
            }
            else
            {
                outString += "[" + slot.Value.Entity.Name + "]";
            }

            outString += "\n";
        }

        return outString;
    }
}
