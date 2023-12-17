using System.Collections.Generic;
using Map.MapGeneration.Entities;
using UnityEngine;

namespace Map.MapGeneration.Strategy
{
    public class RandomMapGenerationStrategy : IMapGenerationStrategy
    {
        private IEntityPlacer _entityPlacer;
        
        public DataMap GenerateMap(int width, int height, List<IEntity> entities = null)
        {
            DataMap generatedDataMap = new DataMap(width, height);

            _entityPlacer = new EntityPlacer(generatedDataMap);
            
            PlaceEntitiesRandomly(entities, generatedDataMap);
            
            return generatedDataMap;
        }

        //go through entity list and randomly pick an entity. Randomly pick a rotation. Place them.
        private void PlaceEntitiesRandomly(List<IEntity> entities, DataMap generatedDataMap)
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;
                
                bool placed = false;
                int attempts = 0;

                while (!placed && attempts < 100)
                {
                    placed = PlaceEntity(entity, generatedDataMap);
                    attempts++;
                }
            }
        }

        private void RotateEntity(IEntity entity, int times)
        {
            if (entity is IRotatableEntity rotatable)
            {
                _entityPlacer.RotateEntity(rotatable, times);
            }
        }
        private static Vector2Int GetRandomMapCoordinates(DataMap generatedDataMap)
        {
            return new Vector2Int(Random.Range(0, generatedDataMap.Width), Random.Range(0, generatedDataMap.Height));
        }
        private bool PlaceEntity(IEntity entity, DataMap generatedDataMap)
        {
            Vector2Int EntityCoordinate = GetRandomMapCoordinates(generatedDataMap);
            
            //randomly start angle
            RotateEntity(entity, UnityEngine.Random.Range(0, 3));
            
            //try 4 different angles
            for (int i = 0; i < 4; i++)
            {
                if (_entityPlacer.CanPlaceEntity(entity, EntityCoordinate))
                {
                    _entityPlacer.PlaceEntity(entity, EntityCoordinate);
                    return true;
                }

                RotateEntity(entity,1);
            }

            return false;
        }
    }
}