namespace Map.MapGeneration.EntityPlacement
{
    public class EntityPlacerFactory : IEntityPlacerFactory
    {
        public IEntityPlacer Create(IDataMap dataMap) => new EntityPlacer(dataMap);
    }
}