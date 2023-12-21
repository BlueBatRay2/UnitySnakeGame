namespace Map.MapGeneration.Entities.Tiles
{
    public class AppleTile : ITile
    {
        public bool CanEnter { get;} = true;
        public override string ToString()
        {
            return "@";
        }
    }
}