using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entities with Opaque block light and line of sight.
/// </summary>
[System.Serializable]
public class ECOpaque : EntityComponent
{
    public ECOpaque(Entity _entity) : base(_entity)
    {
    }

    public ECOpaque(Entity _entity, JToken token) : base(_entity, token)
    {
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
