using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECPartyMember : EntityComponent
{
    public ECPartyMember(Entity _entity, Vector2Int _position) : base(_entity)
    {
    }

    public ECPartyMember(Entity _entity, JToken token) : base(_entity, token)
    {
    }

    public override void Start()
    {
        base.Start();

        EntityComponentSystemManager.Instance.GetSystem<ECPartySystem>().RegisterComponent(this);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        // Remove self from position system
        EntityComponentSystemManager.Instance.GetSystem<ECPartySystem>().UnregisterComponent(this);
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
