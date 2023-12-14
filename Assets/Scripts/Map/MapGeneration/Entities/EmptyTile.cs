namespace Map.MapGeneration.Entities
{
    public class EmptyTile : ITile
    {
        public bool CanEnter { get;} = true;
    }
}