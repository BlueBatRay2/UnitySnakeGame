using System.Collections.Generic;
using Map.MapGeneration.Entities;

namespace Map.MapGeneration
{
    public interface IMapGenerationStrategy
    {
        Map GenerateMap(int width, int height, List<IEntity> entities = null);
    }
}