using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ECMovement : EntityComponent
{
    GameplayStat baseSpeed = new("baseSpeed", 5, 0, 100);

    public ECMovement(Entity _entity, JToken token) : base(_entity, token)
    {
        baseSpeed.SetBaseValue((int)token["baseSpeed"]);

        // Add stats
        statsToAddToEntity.Add(baseSpeed);
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
