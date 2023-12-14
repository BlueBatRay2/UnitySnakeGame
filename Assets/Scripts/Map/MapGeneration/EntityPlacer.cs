using System.Collections.Generic;
using Map.MapGeneration.Entities;

namespace Map.MapGeneration
{
    public class EntityPlacer : IEntityPlacer
    {
        private readonly Map _map;

        public EntityPlacer(Map map)
        {
            _map = map;
        }

        public void RotateEntity(IEntity entity, int times=1)
        {
            if (entity?.EntityGrid == null)
                return;

            for (int timesIter = 0; timesIter < times; timesIter++)
            {
                var newGrid = new List<List<ITile>>();
                int rows = entity.EntityGrid.Count;
                int cols = entity.EntityGrid[0].Count; 

                for (int j = 0; j < cols; j++)
                {
                    var newRow = new List<ITile>();
                    for (int i = rows - 1; i >= 0; i--)
                    {
                        newRow.Add(entity.EntityGrid[i][j]);
                    }
                    newGrid.Add(newRow);
                }

                entity.EntityGrid = newGrid;
            }
        }

        public void PlaceEntity(IEntity entity, int x, int y)
        {
            if (!CanPlaceEntity(entity, x, y))
                return;

            foreach (var row in entity.EntityGrid)
            {
                foreach (var tile in row)
                {
                    if (tile != null)
                        _map.SetTile(x, y, tile);

                    x++;
                }

                y++;
                x -= row.Count; // Reset x to the starting column for the next row
            }
        }

        public bool CanPlaceEntity(IEntity entity, int x, int y)
        {
            if (entity?.EntityGrid == null)
                return false;

            int startY = y;

            foreach (var row in entity.EntityGrid)
            {
                foreach (var tile in row)
                {
                    // Check if the tile is outside the map bounds
                    if (x < 0 || x >= _map.Width || y < 0 || y >= _map.Height)
                        return false;

                    // Check if the tile position is already occupied
                    if (tile != null && !_map.IsTileEmpty(x, y))
                        return false;

                    x++;
                }

                y++;
                x = x - row.Count; // Reset x to the starting column for the next row
            }

            return true;
        }

    }
}