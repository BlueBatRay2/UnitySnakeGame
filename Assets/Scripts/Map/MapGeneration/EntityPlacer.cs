using System;
using System.Collections.Generic;
using System.Drawing;
using Map.MapGeneration.Entities;
using Map.MapGeneration.Entities.Tiles;
using UnityEngine;

namespace Map.MapGeneration
{
    public class EntityPlacer : IEntityPlacer
    {
        private readonly DataMap _dataMap;
        

        public EntityPlacer(DataMap dataMap)
        {
            _dataMap = dataMap;
        }

        public void RotateEntity(IRotatableEntity entity, int times=1)
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

            entity.Rotations = (entity.Rotations + times) % 4;
        }

        public void PlaceEntity(IEntity entity, Vector2Int position)
        {
            if (!CanPlaceEntity(entity, position))
                return;

            _dataMap.EntityCoordinates.Add(new Tuple<IEntity, Vector2Int>(entity, position));
            
            foreach (var row in entity.EntityGrid)
            {
                foreach (var tile in row)
                {
                    if (tile != null)
                        _dataMap.SetTile(position, tile);

                    position.x++;
                }

                position.y++;
                position.x -= row.Count; // Reset x to the starting column for the next row
            }
        }

        public bool CanPlaceEntity(IEntity entity, Vector2Int position)
        {
            if (entity?.EntityGrid == null)
                return false;

            foreach (var row in entity.EntityGrid)
            {
                foreach (var tile in row)
                {
                    // Check if the tile is outside the map bounds
                    if (position.x < 0 || position.x >= _dataMap.Width || position.y < 0 || position.y >= _dataMap.Height)
                        return false;

                    // Check if the tile position is already occupied
                    if (tile != null && !_dataMap.IsTileEmpty(position.x, position.y))
                        return false;

                    position.x++;
                }

                position.y++;
                position.x -= row.Count; // Reset x to the starting column for the next row
            }

            return true;
        }

    }
}