using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The tile component signifies that this entity is connected to a tile on a unity tilemap.
/// </summary>
[System.Serializable]
public class ECTile : EntityComponent
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] Vector2Int tilePosition;

    public ECTile(Entity _entity, JToken token) : base(_entity, token)
    {
    }

    public ECTile(Entity _entity, Tilemap _tileMap, Vector2Int _tilePosition) : base(_entity)
    {
        Tilemap = _tileMap;
        TilePosition = _tilePosition;
    }

    public Tilemap Tilemap { get => tilemap; set => tilemap = value; }
    public Vector2Int TilePosition { get => tilePosition; set => tilePosition = value; }

    public override bool FireEvent(GameplayEvent e)
    {
        return false;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        // Remove tile from map.
        MapManager.Instance.RemoveTileFromMap(this);
    }
}
