using System.Collections.Generic;
using Map.MapGeneration.Entities;
using Random = System.Random;

namespace Map.MapGeneration
{
    public class RandomMapGenerationStrategy : IMapGenerationStrategy
    {
        private readonly Random _random = new();
        private EntityPlacer _entityPlacer;
        
        public Map GenerateMap(int width, int height, List<IEntity> entities = null)
        {
            Map generatedMap = new Map(width, height);

            _entityPlacer = new EntityPlacer(generatedMap);
            
            //go through entity list and randomly pick an entity. Randomly pick a rotation. Place them.
            PlaceEntities(entities, generatedMap);
            
            return generatedMap;
        }

        private void PlaceEntities(List<IEntity> entities, Map generatedMap)
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                    continue;
                
                bool placed = false;
                int attempts = 0;

                while (!placed && attempts < 100)
                {
                    int x = _random.Next(generatedMap.Width);
                    int y = _random.Next(generatedMap.Height);

                    //initial random rotate
                    _entityPlacer.RotateEntity(entity, _random.Next(0,3));
                        
                    //try 4 different angles
                    for (int i = 0; i < 4; i++)
                    {
                        if (_entityPlacer.CanPlaceEntity(entity, x, y))
                        {
                            _entityPlacer.PlaceEntity(entity, x, y);
                            placed = true;
                            break;
                        }
                        // Rotate entity if needed
                        _entityPlacer.RotateEntity(entity);
                    }
                    
                    attempts++;
                    
                }
            }
        }
    }
}