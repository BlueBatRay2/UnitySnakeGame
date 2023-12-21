using System.Collections.Generic;
using Map.MapGeneration.Entities;

namespace Map.MapGeneration.Strategy
{
    public interface IEntityLayoutStrategy
    { 
        void LayoutEntities(IDataMap dataMap, List<IEntity> entities);
    }
}