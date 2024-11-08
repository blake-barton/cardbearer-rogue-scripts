using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECFieldOfView : EntityComponent
{
    [SerializeField] int fieldOfViewRange = 8;
    [SerializeField] List<Vector2Int> fieldOfView = new();

    public ECFieldOfView(Entity _entity, int _fovRange) : base(_entity)
    {
        FieldOfViewRange = _fovRange;
    }

    public ECFieldOfView(Entity _entity, JToken token) : base(_entity, token)
    {
        FieldOfViewRange = (int)token["fovRange"];
    }

    public int FieldOfViewRange { get => fieldOfViewRange; set => fieldOfViewRange = value; }
    public List<Vector2Int> FieldOfView { get => fieldOfView; set => fieldOfView = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    public override void Start()
    {
        base.Start();

        new UpdateFOVAction(entity).Perform();
    }
}
