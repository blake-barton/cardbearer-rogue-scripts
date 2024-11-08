using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECMonobehavior : EntityComponent
{
    GameObject entityGameObject;
    EntityMonobehavior entityMonobehavior;

    public ECMonobehavior(Entity _entity, string objectName) : base(_entity)
    {
        // Create gameobject
        entityGameObject = new(objectName);
        entityGameObject.transform.position = new Vector3(0.5f, 0.5f, 0);
    }

    public ECMonobehavior(Entity _entity, GameObject _entityGameObject) : base(_entity)
    {
        entityGameObject = _entityGameObject;
    }

    public ECMonobehavior(Entity _entity, JToken token) : base(_entity, token)
    {
        if (token != null)
        {
            string objectName = (string)token["objectName"];

            // Create gameobject
            entityGameObject = new(objectName);
            entityGameObject.transform.position = new Vector3(0.5f, 0.5f, 0);
        }
        else
        {
            Debug.LogError("ECMonobehavior: Received null JToken.");
        }
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    public override void Start()
    {
        // Attach monobehavior to entity
        entityMonobehavior = entityGameObject.AddComponent<EntityMonobehavior>();
        entityMonobehavior.RegisterEntity(entity);
    }
}
