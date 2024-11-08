using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECPosition : EntityComponent
{
    [SerializeField] Vector2Int position = new();

    public ECPosition(Entity _entity, Vector2Int _position) : base(_entity)
    {
        position = _position;

        EntityComponentSystemManager.Instance.GetSystem<ECPositionSystem>().RegisterComponent(this);
    }

    public ECPosition(Entity _entity, JToken token) : base(_entity, token)
    {
        int x = (int)token["x"];
        int y = (int)token["y"];

        position = new(x, y);

        EntityComponentSystemManager.Instance.GetSystem<ECPositionSystem>().RegisterComponent(this);
    }

    public Vector2Int Position { get => position; set => position = value; }

    public override void Start()
    {
        base.Start();

        // Move to initial position if instantiated in world
        if (entity.EntityMB)
        {
            entity.EntityMB.transform.position = new Vector3(Position.x + 0.5f, Position.y + 0.5f, 0);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        // Remove self from position system
        EntityComponentSystemManager.Instance.GetSystem<ECPositionSystem>().UnregisterComponent(this);
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
