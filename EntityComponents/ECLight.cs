using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECLight : EntityComponent
{
    [SerializeField] int lightRange = 8;
    [SerializeField] List<Vector2Int> litTiles = new();

    // Components
    [SerializeField] ECPosition positionComponent;

    public ECLight(Entity _entity, int _lightRange) : base(_entity)
    {
        CanTick = true;

        LightRange = _lightRange;

        positionComponent = entity.GetComponent<ECPosition>();
    }

    public ECLight(Entity _entity, JToken token) : base(_entity, token)
    {
        CanTick = true;

        LightRange = (int)token["lightRange"];
    }

    public int LightRange { get => lightRange; set => lightRange = value; }
    public List<Vector2Int> LitTiles { get => litTiles; set => litTiles = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    public override void Start()
    {
        base.Start();

        if (positionComponent != null)
        {
            new LightAreaAction(entity, this, positionComponent).Perform();
        }
    }

    public override void Tick()
    {
        if (positionComponent != null)
        {
            new LightAreaAction(entity, this, positionComponent).Perform();
        }
    }
}
