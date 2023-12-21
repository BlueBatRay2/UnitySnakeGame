using System.Collections.Generic;
using Map.MapGeneration.Entities;
using UnityEngine;

namespace Map.MapGeneration.Strategy
{
    public class RandomEntityLayoutStrategy : IEntityLayoutStrategy
    {
        private readonly IEntityPlacer _entityPlacer;

        public RandomEntityLayoutStrategy(IEntityPlacer entityPlacer)
        {
            _entityPlacer = entityPlacer;
        }
        
        //go through entity list and randomly pick an entity. Randomly pick a rotation. Place them.
        public void LayoutEntities(IDataMap dataMap, List<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity == null) continue;
                
                bool placed = false;
                int attempts = 0;

                while (!placed && attempts < 100)
                {
                    placed = PlaceEntityRandom(entity, dataMap);
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
        private static Vector2Int GetRandomMapCoordinates(IDataMap generatedDataMap)
        {
            return new Vector2Int(Random.Range(0, generatedDataMap.Width), Random.Range(0, generatedDataMap.Height));
        }
        private bool PlaceEntityRandom(IEntity entity, IDataMap generatedDataMap)
        {
            Vector2Int entityCoordinate = GetRandomMapCoordinates(generatedDataMap);
            
            //randomly start angle
            RotateEntity(entity, Random.Range(0, 3));
            
            //try 4 different angles
            for (int i = 0; i < 4; i++)
            {
                if (_entityPlacer.CanPlaceEntity(entity, entityCoordinate))
                {
                    _entityPlacer.PlaceEntity(entity, entityCoordinate);
                    return true;
                }

                RotateEntity(entity,1);
            }

            return false;
        }
    }
}