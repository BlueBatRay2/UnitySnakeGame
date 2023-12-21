using System.Collections.Generic;
using Map.MapGeneration.Entities.Tiles;

namespace Map.MapGeneration.Entities.Concrete
{
    public class SnakeStartEntity : IEntity
    {
        public List<List<ITile>> EntityGrid { get; set; } = new()
        {
            new List<ITile> { new SnakeStartTile(), new SnakeStartTile(), new SnakeStartTile(),new SnakeStartTile(),new SnakeStartTile()}
        };
    }
}