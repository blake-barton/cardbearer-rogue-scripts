using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECFaction : EntityComponent
{
    [SerializeField] Faction entityFaction;

    public ECFaction(Entity _entity, Faction _faction) : base(_entity)
    {
        EntityFaction = _faction;

        // Register
        EntityComponentSystemManager.Instance.GetSystem<ECFactionSystem>().RegisterComponent(this);
    }

    public ECFaction(Entity _entity, JToken token) : base(_entity, token)
    {
        // Get faction name from Json
        string factionName = (string)token["name"];

        // Get faction from faction manager
        Faction faction = FactionManager.Instance.GetFactionByName(factionName);

        if (faction != null)
        {
            entityFaction = faction;
        }
        else
        {
            Debug.LogError("ECFaction: Received a null faction during construction.");
        }

        // Register
        EntityComponentSystemManager.Instance.GetSystem<ECFactionSystem>().RegisterComponent(this);
    }

    public Faction EntityFaction { get => entityFaction; set => entityFaction = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
