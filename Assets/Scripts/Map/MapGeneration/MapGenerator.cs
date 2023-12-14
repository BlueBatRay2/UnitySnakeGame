using System.Collections.Generic;
using Map.MapGeneration.Entities;

namespace Map.MapGeneration
{
    public class MapGenerator
    {
        public static Map GenerateMap(int width, int height, IMapGenerationStrategy mapGenerationStrategy, List<IEntity> entities = null)
        {
            return mapGenerationStrategy.GenerateMap(width, height, entities);
        }
    }
}