namespace Map.MapGeneration.Entities.Tiles
{
    public class SolidTile : ITile
    {
        public bool CanEnter { get; } = false;
    }
}