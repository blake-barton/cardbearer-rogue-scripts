using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECPhysics : EntityComponent
{
    [SerializeField] GameplayTag category;
    [SerializeField] int heft;
    [SerializeField] bool canTake = true;

    public ECPhysics(Entity _entity) : base(_entity)
    {
    }

    public ECPhysics(Entity _entity, JToken token) : base(_entity, token)
    {
        Heft = (int)token["heft"];
        CanTake = (bool)token["canTake"];
        Category = new((string)token["category"]);
    }

    public int Heft { get => heft; set => heft = value; }
    public bool CanTake { get => canTake; set => canTake = value; }
    public GameplayTag Category { get => category; set => category = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
