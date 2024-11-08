using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECWeapon : EntityComponent
{
    [SerializeField] GameplayTag type;
    [SerializeField] DamageValue damage;
    [SerializeField] int strengthRequirement;
    [SerializeField] GameplayTagContainer weaponTags = new();
    
    public ECWeapon(Entity _entity) : base(_entity)
    {
    }

    public ECWeapon(Entity _entity, JToken token) : base(_entity, token)
    {
        Type = new((string)token["type"]);
        Damage = new(new GameplayTag((string)token["damage"]["damageType"]), (int)token["damage"]["damage"]);
        StrengthRequirement = (int)token["strengthRequirement"];
        WeaponTags.AddTagsFromJArray((JArray)token["weaponTags"]);
    }

    public GameplayTag Type { get => type; set => type = value; }
    public DamageValue Damage { get => damage; set => damage = value; }
    public int StrengthRequirement { get => strengthRequirement; set => strengthRequirement = value; }
    public GameplayTagContainer WeaponTags { get => weaponTags; set => weaponTags = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }
}
