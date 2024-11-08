using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles use of abilities.
/// </summary>
[System.Serializable]
public class ECAbility : EntityComponent
{
    List<GameplayAbility> abilities = new();

    public ECAbility(Entity _entity) : base(_entity)
    {
    }

    public ECAbility(Entity _entity, JToken token) : base(_entity, token)
    {
        // Initialize list of abilities from json
        abilities = StreamingAssetsLoader.LoadGameplayAbilityListFromToken(token["abilities"], this);
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
