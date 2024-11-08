using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ECHealth : EntityComponent
{
    GameplayStat health = new("health", 100, 100);

    public GameplayStat Health { get => health; set => health = value; }

    public ECHealth(Entity _entity, int _health, int _maxHealth) : base(_entity)
    {
        Health.SetBaseValue(_health);
        Health.MaxValue.SetBaseValue(_maxHealth);

        // Add stats
        statsToAddToEntity.Add(health);
    }

    public ECHealth(Entity _entity, JToken token) : base(_entity, token)
    {
        Health.MaxValue.SetBaseValue((int)token["maxHealth"]);
        Health.SetBaseValue((int)token["health"]);

        // Add stats
        statsToAddToEntity.Add(health);
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
