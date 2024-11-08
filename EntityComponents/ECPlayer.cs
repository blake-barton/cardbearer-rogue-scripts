using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECPlayer : EntityComponent
{
    public ECPlayer(Entity _entity) : base(_entity)
    {
    }

    public ECPlayer(Entity _entity, JToken token) : base(_entity, token)
    {
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
