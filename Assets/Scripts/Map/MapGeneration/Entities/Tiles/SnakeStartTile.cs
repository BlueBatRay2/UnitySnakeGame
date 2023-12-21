namespace Map.MapGeneration.Entities.Tiles
{
    public class SnakeStartTile : ITile
    {
        public bool CanEnter { get;} = true;
        public override string ToString()
        {
            return "+";
        }
    }
}