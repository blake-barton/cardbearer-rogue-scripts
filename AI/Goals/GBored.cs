using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBored : Goal
{
    public GBored(ECBehavior _behavior) : base(_behavior)
    {
    }

    public override bool Finished()
    {
        // This goal will never be considered finished.
        return false;
    }

    public override void PerformAction(GameplayEvent e)
    {
        // Fire "IAmBored" event. Return if a component responds.
        if (FireEvent(new GameplayEvent("IAmBored"))) { return; }

        // No components care that I'm bored. Let's log that I'm bored!
        LogManager.Instance.Log(TMProTagUtility.ColorText(Behavior.Entity.Name, Color.green) + " is bored!");
    }
}
