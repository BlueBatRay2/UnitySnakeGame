using System.Collections.Generic;
using Map.MapGeneration.Entities.Tiles;

namespace Map.MapGeneration.Entities.Concrete
{
    public class ThreeBlockWallEntity : IRotatableEntity
    {
        public List<List<ITile>> EntityGrid { get; set; } = new()
        {
            new List<ITile> { new SolidTile()},
            new List<ITile> { new SolidTile()},
            new List<ITile> { new SolidTile()}
        };

        public int Rotations { get; set; }
    }
}