using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacles cannot be moved on or over.
/// </summary>
[System.Serializable]
public class ECObstacle : EntityComponent
{
    public ECObstacle(Entity _entity) : base(_entity)
    {
    }

    public ECObstacle(Entity _entity, JToken token) : base(_entity, token)
    {
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
