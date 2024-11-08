using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECBehavior : EntityComponent
{
    readonly Stack<Goal> goals = new();

    public ECBehavior(Entity _entity) : base(_entity)
    {
    }

    public ECBehavior(Entity _entity, JToken token) : base(_entity, token)
    {
        // Todo: JSON lists starting goals.
    }

    public override bool FireEvent(GameplayEvent e)
    {
        switch (e.Name)
        {
            case "PerformAction":
                PerformAction(e);
                return true;
        }

        return false;
    }

    /// <summary>
    /// Clear all finished goals and perform the action of the next goal.
    /// </summary>
    private void PerformAction(GameplayEvent e) 
    {
        // Clear completed actions from stack
        if (goals.Count > 0)
        {
            while (goals.Peek().Finished())
            {
                goals.Pop();
            }
        }

        // Append a "Bored" goal if the stack is empty
        if (goals.Count == 0)
        {
            PushGoal(new GBored(this));
        }

        if (goals.Count > 0)
        {
            // Take action of top goal
            goals.Peek().PerformAction(e);
        }
    }

    public void PushGoal(Goal goal)
    {
        goals.Push(goal);
    }
}
