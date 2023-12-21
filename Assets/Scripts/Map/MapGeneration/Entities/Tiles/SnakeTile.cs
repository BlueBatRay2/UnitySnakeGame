namespace Map.MapGeneration.Entities.Tiles
{
    public class SnakeTile : ITile
    {
        public bool CanEnter { get;} = false;
        public override string ToString()
        {
            return "+";
        }
    }
}