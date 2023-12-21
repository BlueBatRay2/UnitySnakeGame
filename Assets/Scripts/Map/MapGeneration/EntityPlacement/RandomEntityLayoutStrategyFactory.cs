using Map.MapGeneration.Strategy;

namespace Map.MapGeneration.EntityPlacement
{
    public class RandomEntityLayoutStrategyFactory : IEntityLayoutStrategyFactory
    {
        public IEntityLayoutStrategy Create(IEntityPlacer entityPlacer) => new RandomEntityLayoutStrategy(entityPlacer);
    }
}