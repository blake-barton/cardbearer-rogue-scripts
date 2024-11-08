using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal
{
    protected ECBehavior behavior;
    protected Goal originalIntent;

    public Goal OriginalIntent { get => originalIntent; set => originalIntent = value; }
    public ECBehavior Behavior { get => behavior; set => behavior = value; }

    public Goal(ECBehavior _behavior)
    {
        Behavior = _behavior;
    }

    /// <summary>
    ///  Returns true if this goal is complete.
    /// </summary>
    public abstract bool Finished();

    public abstract void PerformAction(GameplayEvent e);

    protected bool FireEvent(GameplayEvent e)
    {
        if (Behavior != null)
        {
            if (Behavior.Entity != null)
            {
                return Behavior.Entity.FireEvent(e);
            }

            Debug.LogError("Tried to fire an event with a null entity.");
            return false;
        }

        Debug.LogError("Tried to fire an event with a null behavior.");
        return false;
    }
}
