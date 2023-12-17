using System.Collections.Generic;
using Map.MapGeneration.Entities;

namespace Map.MapGeneration.Strategy
{
    public interface IMapGenerationStrategy
    {
        DataMap GenerateMap(int width, int height, List<IEntity> entities = null);
    }
}