using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECCharacter : EntityComponent
{
    int level;
    string ancestry;
    string heritage;
    string background;
    string culture;
    string characterClass;

    // Attributes
    GameplayStat strength = new("strength", 0, -10, 10);
    GameplayStat dexterity = new("dexterity", 0, -10, 10);
    GameplayStat endurance = new("endurance", 0, -10, 10);
    GameplayStat recall = new("recall", 0, -10, 10);
    GameplayStat enlightenment = new("enlightenment", 0, -10, 10);
    GameplayStat personality = new("personality", 0, -10, 10);

    // Skills
    GameplayStat acrobatics = new("acrobatics", 0, -20, 20);
    GameplayStat alchemy = new("alchemy", 0, -20, 20);
    GameplayStat arcana = new("arcama", 0, -20, 20);
    GameplayStat athletics = new("athletics", 0, -20, 20);
    GameplayStat crafting = new("crafting", 0, -20, 20);
    GameplayStat deception = new("deception", 0, -20, 20);
    GameplayStat ecology = new("ecology", 0, -20, 20);
    GameplayStat faith = new("faith", 0, -20, 20);
    GameplayStat intimidation = new("intimidation", 0, -20, 20);
    GameplayStat intuition = new("intuition", 0, -20, 20);
    GameplayStat leadership = new("leadership", 0, -20, 20);
    GameplayStat medicine = new("medicine", 0, -20, 20);
    GameplayStat perception = new("perception", 0, -20, 20);
    GameplayStat performance = new("performance", 0, -20, 20);
    GameplayStat persuasion = new("persuasion", 0, -20, 20);
    GameplayStat society = new("society", 0, -20, 20);
    GameplayStat stealth = new("stealth", 0, -20, 20);
    GameplayStat survival = new("survival", 0, -20, 20);
    GameplayStat thievery = new("thievery", 0, -20, 20);

    // Skill Proficiencies
    GameplayStat acrobaticsProficiency = new("acrobaticsProficiency", 0, 0, 5);
    GameplayStat alchemyProficiency = new("alchemyProficiency", 0, 0, 5);
    GameplayStat arcanaProficiency = new("arcanaProficiency", 0, 0, 5);
    GameplayStat athleticsProficiency = new("athleticsProficiency", 0, 0, 5);
    GameplayStat craftingProficiency = new("craftingProficiency", 0, 0, 5);
    GameplayStat deceptionProficiency = new("deceptionProficiency", 0, 0, 5);
    GameplayStat ecologyProficiency = new("ecologyProficiency", 0, 0, 5);
    GameplayStat faithProficiency = new("faithProficiency", 0, 0, 5);
    GameplayStat intimidationProficiency = new("intimidationProficiency", 0, 0, 5);
    GameplayStat intuitionProficiency = new("intuitionProficiency", 0, 0, 5);
    GameplayStat leadershipProficiency = new("leadershipProficiency", 0, 0, 5);
    GameplayStat medicineProficiency = new("medicineProficiency", 0, 0, 5);
    GameplayStat perceptionProficiency = new("perceptionProficiency", 0, 0, 5);
    GameplayStat performanceProficiency = new("performanceProficiency", 0, 0, 5);
    GameplayStat persuasionProficiency = new("persuasionProficiency", 0, 0, 5);
    GameplayStat societyProficiency = new("societyProficiency", 0, 0, 5);
    GameplayStat stealthProficiency = new("stealthProficiency", 0, 0, 5);
    GameplayStat survivalProficiency = new("survivalProficiency", 0, 0, 5);
    GameplayStat thieveryProficiency = new("thieveryProficiency", 0, 0, 5);

    // Action Points
    GameplayStat actionPoints = new("actionPoints", 3, 0, 3);

    public ECCharacter(Entity _entity) : base(_entity)
    {
        AddCharacterStats();
    }

    public ECCharacter(Entity _entity, JToken token) : base(_entity, token)
    {
        strength.SetBaseValue((int)token["strength"]);
        dexterity.SetBaseValue((int)token["dexterity"]);
        endurance.SetBaseValue((int)token["endurance"]);
        recall.SetBaseValue((int)token["recall"]);
        enlightenment.SetBaseValue((int)token["enlightenment"]);
        personality.SetBaseValue((int)token["personality"]);

        AddCharacterStats();
    }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    private void AddCharacterStats()
    {
        // Add attributes to array
        statsToAddToEntity.Add(strength);
        statsToAddToEntity.Add(dexterity);
        statsToAddToEntity.Add(endurance);
        statsToAddToEntity.Add(recall);
        statsToAddToEntity.Add(enlightenment);
        statsToAddToEntity.Add(personality);

        // Add derived stats to skills
        acrobatics.AddDerivedStats(new List<GameplayStat>() { dexterity, acrobaticsProficiency });
        alchemy.AddDerivedStats(new List<GameplayStat>() { recall, alchemyProficiency });
        arcana.AddDerivedStats(new List<GameplayStat>() { recall, arcanaProficiency });
        athletics.AddDerivedStats(new List<GameplayStat>() { strength, athleticsProficiency });
        crafting.AddDerivedStats(new List<GameplayStat>() { recall, craftingProficiency });
        deception.AddDerivedStats(new List<GameplayStat>() { personality, deceptionProficiency });
        ecology.AddDerivedStats(new List<GameplayStat>() { enlightenment, ecologyProficiency });
        faith.AddDerivedStats(new List<GameplayStat>() { enlightenment, faithProficiency });
        intimidation.AddDerivedStats(new List<GameplayStat>() { personality, intimidationProficiency });
        intuition.AddDerivedStats(new List<GameplayStat>() { enlightenment, intuitionProficiency });
        leadership.AddDerivedStats(new List<GameplayStat>() { personality, leadershipProficiency });
        medicine.AddDerivedStats(new List<GameplayStat>() { recall, medicineProficiency });
        perception.AddDerivedStats(new List<GameplayStat>() { enlightenment, perceptionProficiency });
        performance.AddDerivedStats(new List<GameplayStat>() { personality, performanceProficiency });
        persuasion.AddDerivedStats(new List<GameplayStat>() { personality, persuasionProficiency });
        society.AddDerivedStats(new List<GameplayStat>() { enlightenment, societyProficiency });
        stealth.AddDerivedStats(new List<GameplayStat>() { dexterity, stealthProficiency });
        survival.AddDerivedStats(new List<GameplayStat>() { endurance, survivalProficiency });
        thievery.AddDerivedStats(new List<GameplayStat>() { dexterity, thieveryProficiency });

        // Add skills to array
        statsToAddToEntity.Add(acrobatics);
        statsToAddToEntity.Add(alchemy);
        statsToAddToEntity.Add(arcana);
        statsToAddToEntity.Add(athletics);
        statsToAddToEntity.Add(crafting);
        statsToAddToEntity.Add(deception);
        statsToAddToEntity.Add(ecology);
        statsToAddToEntity.Add(faith);
        statsToAddToEntity.Add(intimidation);
        statsToAddToEntity.Add(intuition);
        statsToAddToEntity.Add(leadership);
        statsToAddToEntity.Add(medicine);
        statsToAddToEntity.Add(perception);
        statsToAddToEntity.Add(performance);
        statsToAddToEntity.Add(persuasion);
        statsToAddToEntity.Add(society);
        statsToAddToEntity.Add(stealth);
        statsToAddToEntity.Add(survival);
        statsToAddToEntity.Add(thievery);

        // Add proficiencies to array
        statsToAddToEntity.Add(acrobaticsProficiency);
        statsToAddToEntity.Add(alchemyProficiency);
        statsToAddToEntity.Add(arcanaProficiency);
        statsToAddToEntity.Add(athleticsProficiency);
        statsToAddToEntity.Add(craftingProficiency);
        statsToAddToEntity.Add(deceptionProficiency);
        statsToAddToEntity.Add(ecologyProficiency);
        statsToAddToEntity.Add(faithProficiency);
        statsToAddToEntity.Add(intimidationProficiency);
        statsToAddToEntity.Add(intuitionProficiency);
        statsToAddToEntity.Add(leadershipProficiency);
        statsToAddToEntity.Add(medicineProficiency);
        statsToAddToEntity.Add(perceptionProficiency);
        statsToAddToEntity.Add(performanceProficiency);
        statsToAddToEntity.Add(persuasionProficiency);
        statsToAddToEntity.Add(societyProficiency);
        statsToAddToEntity.Add(stealthProficiency);
        statsToAddToEntity.Add(survivalProficiency);
        statsToAddToEntity.Add(thieveryProficiency);

        // Add action points to array
        statsToAddToEntity.Add(actionPoints);
    }
}
