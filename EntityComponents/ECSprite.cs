using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Required component for entities that must be seen in the world.
/// </summary>
[System.Serializable]
public class ECSprite : EntityComponent
{
    [SerializeField] Sprite sprite;
    [SerializeField] string sortingLayer = "Entities";
    [SerializeField] int orderInLayer = 0;

    public ECSprite(Entity _entity) : base(_entity)
    {
    }

    public ECSprite(Entity _entity, JToken token) : base(_entity, token)
    {
        string spritePath = (string)token["spritePath"];
        string _sortingLayer = (string)token["sortingLayer"];
        int _orderInLayer = (int)token["orderInLayer"];
        int width = (int)token["width"];
        int height = (int)token["height"];

        Sprite = StreamingAssetsLoader.LoadSpriteFromPath(spritePath, width, height);
        SortingLayer = _sortingLayer;
        OrderInLayer = _orderInLayer;
    }

    public ECSprite(Entity _entity, Sprite _sprite, string _sortingLayer, int _orderInLayer) : base(_entity)
    {
        Sprite = _sprite;
        SortingLayer = _sortingLayer;
        OrderInLayer = _orderInLayer;
    }

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int OrderInLayer { get => orderInLayer; set => orderInLayer = value; }
    public string SortingLayer { get => sortingLayer; set => sortingLayer = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    public override void Start()
    {
        // Update sprite renderer
        new SetSpriteAction(entity, Sprite).Perform();
    }
}
