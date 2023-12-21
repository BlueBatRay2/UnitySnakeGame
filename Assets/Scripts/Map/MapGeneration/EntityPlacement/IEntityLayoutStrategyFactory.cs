using Map.MapGeneration.Strategy;

namespace Map.MapGeneration.EntityPlacement
{
    public interface IEntityLayoutStrategyFactory
    {
        IEntityLayoutStrategy Create(IEntityPlacer entityPlacer);
    }
}