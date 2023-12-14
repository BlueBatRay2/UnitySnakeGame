using System.Collections.Generic;

namespace Map.MapGeneration.Entities
{
    public class Wall : IEntity
    {
        public List<List<ITile>> EntityGrid { get; set; } = new()
        {
            new List<ITile> { new ConcreteTile(), new ConcreteTile() },
            new List<ITile> { null, new ConcreteTile() },
            new List<ITile> { null, new ConcreteTile() }
        };
    }
}