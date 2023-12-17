using System.Collections.Generic;
using Map.MapGeneration.Entities.Tiles;

namespace Map.MapGeneration.Entities
{
    public interface IEntity
    {
        List<List<ITile>> EntityGrid { get; set; }
        
    }
}