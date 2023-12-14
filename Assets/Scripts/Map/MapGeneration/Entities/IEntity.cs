using System.Collections.Generic;

namespace Map.MapGeneration.Entities
{
    public interface IEntity
    {
        List<List<ITile>> EntityGrid { get; set; }
    }
}