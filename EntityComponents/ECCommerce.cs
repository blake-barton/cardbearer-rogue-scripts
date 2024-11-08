using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECCommerce : EntityComponent
{
    [SerializeField] int value;

    public ECCommerce(Entity _entity) : base(_entity)
    {
    }

    public ECCommerce(Entity _entity, JToken token) : base(_entity, token)
    {
        Value = (int)token["value"];
    }

    public int Value { get => value; set => this.value = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
