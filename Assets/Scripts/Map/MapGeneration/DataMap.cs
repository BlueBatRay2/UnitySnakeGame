using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Map.MapGeneration.Entities.Tiles;
using UnityEngine;

namespace Map.MapGeneration
{
    public class DataMap : IDataMap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        private List<List<ITile>> _tiles;
        
        public void InitializeTiles(int width, int height)
        {
            Width = width;
            Height = height;
            
            _tiles = new List<List<ITile>>();

            for (int y = 0; y < Height; y++)
            {
                List<ITile> row = new List<ITile>();

                for (int x = 0; x < Width; x++)
                {
                    ITile tile = new EmptyTile();

                    row.Add(tile);
                }

                _tiles.Add(row);
            }
        }
        
        public bool IsTileEmpty(ITile tile)
        {
            return tile == null || tile is EmptyTile;
        }
        
        public bool IsTileEmpty(int x, int y)
        {
            return _tiles[y][x] == null || _tiles[y][x] is EmptyTile;
        }

        public void SetTile(Vector2Int position, ITile tile)
        {
            _tiles[position.y][position.x] = tile;
        }

        public ITile GetTile(Vector2Int position)
        {
            return _tiles[position.y][position.x];
        }

        public IEnumerator<ITile> GetEnumerator()
        {
            return new DataMapEnumerator(_tiles);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public void ForEachTile(Action<ITile, int, int> action)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    action(_tiles[y][x], x, y);
                }
            }
        }
        
        public override string ToString()
        {
            StringBuilder mapString = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    ITile tile = _tiles[y][x];
                    mapString.Append(tile); // '#' represents a wall
                }
                mapString.AppendLine(); // New line at the end of each row
            }
            return mapString.ToString();
        }
        

        private class DataMapEnumerator : IEnumerator<ITile>
        {
            private readonly List<List<ITile>> _tiles;
            private int _x = -1;
            private int _y = 0;

            public DataMapEnumerator(List<List<ITile>> tiles)
            {
                _tiles = tiles;
            }

            public bool MoveNext()
            {
                _x++;
                if (_x >= _tiles[_y].Count)
                {
                    _x = 0;
                    _y++;
                }
                return _y < _tiles.Count;
            }

            public void Reset()
            {
                _x = -1;
                _y = 0;
            }

            public ITile Current => _tiles[_y][_x];

            object IEnumerator.Current => Current;

            public void Dispose(){}
        }
    }
}