using System;
using System.Collections.Generic;
using System.Text;
using Map.MapGeneration.Entities;

namespace Map
{
    public class Map
    {
        public int Width { get; }
        public int Height { get; }
        
        private List<List<ITile>> _tiles;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            InitializeTiles();
        }

        private void InitializeTiles()
        {
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
        ITile GetTile(int x, int y)
        {
            return _tiles[x][y];
        }
        
        public bool IsTileEmpty(int x, int y)
        {
            return _tiles[y][x] == null || _tiles[x][y] is EmptyTile;
        }

        public void SetTile(int x, int y, ITile tile)
        {
            _tiles[y][x] = tile;
        }

        public override string ToString()
        {
            StringBuilder mapString = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    ITile tile = _tiles[y][x];
                    Console.Write(tile.CanEnter ? '-' : '#');
                    mapString.Append(tile.CanEnter ? '-' : '#'); // '#' represents a wall
                }
                Console.WriteLine();
                mapString.AppendLine(); // New line at the end of each row
            }

            return mapString.ToString();
        }

    }
}