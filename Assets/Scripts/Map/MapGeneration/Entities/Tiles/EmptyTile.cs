namespace Map.MapGeneration.Entities.Tiles
{
    public class EmptyTile : ITile
    {
        public bool CanEnter { get;} = true;
        public override string ToString()
        {
            return "-";
        }
    }
}