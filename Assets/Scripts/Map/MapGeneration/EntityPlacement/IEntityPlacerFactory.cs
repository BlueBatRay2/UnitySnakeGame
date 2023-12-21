namespace Map.MapGeneration.EntityPlacement
{
    public interface IEntityPlacerFactory
    {
        IEntityPlacer Create(IDataMap dataMap);
    }
}