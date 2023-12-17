using System.Collections.Generic;
using Map.MapGeneration.Entities.Tiles;

namespace Map.MapGeneration.Entities.Concrete
{
    public class LWallEntity : IRotatableEntity
    {
        public List<List<ITile>> EntityGrid { get; set; } = new()
        {
            new List<ITile> { new SolidTile(), new SolidTile() },
            new List<ITile> { new SolidTile(), null },
            new List<ITile> { new SolidTile(), null }
        };

        public int Rotations { get; set; }
    }
}