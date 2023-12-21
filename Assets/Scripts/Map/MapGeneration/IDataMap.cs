using System;
using System.Collections.Generic;
using Map.MapGeneration.Entities;
using Map.MapGeneration.Entities.Tiles;
using UnityEngine;

namespace Map.MapGeneration
{
    public interface IDataMap : IEnumerable<ITile>
    {
        int Width { get; }
        int Height { get; }
        void InitializeTiles(int width, int height);
        bool IsTileEmpty(ITile tile);
        bool IsTileEmpty(int x, int y);
        void SetTile(Vector2Int position, ITile tile);
        ITile GetTile(Vector2Int position);
        string ToString();
        void ForEachTile(Action<ITile, int, int> action);
    }
}