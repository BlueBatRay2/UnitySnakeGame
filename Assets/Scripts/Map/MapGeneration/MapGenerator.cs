using System.Collections.Generic;
using Map.MapGeneration.Entities;
using Map.MapGeneration.Strategy;

namespace Map.MapGeneration
{
    public class MapGenerator
    {
        public static DataMap GenerateDataMap(int width, int height, IMapGenerationStrategy mapGenerationStrategy, List<IEntity> entities = null)
        {
            return mapGenerationStrategy.GenerateMap(width, height, entities);
        }
    }
}