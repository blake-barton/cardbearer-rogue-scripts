using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ECEquippable : EntityComponent
{
    List<GameplayTag> possibleSlots = new();
    GameplayTag currentlyEquippedSlot = null;

    public ECEquippable(Entity _entity) : base(_entity)
    {
    }

    public ECEquippable(Entity _entity, JToken token) : base(_entity, token)
    {
        // Load possible slots from json
        JArray possibleSlotsArray = (JArray)token["possibleSlots"];
        foreach (JToken possibleSlotToken in possibleSlotsArray)
        {
            possibleSlots.Add(new((string)possibleSlotToken));
        }
    }

    public List<GameplayTag> PossibleSlots { get => possibleSlots; set => possibleSlots = value; }
    public GameplayTag CurrentlyEquippedSlot { get => currentlyEquippedSlot; set => currentlyEquippedSlot = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
