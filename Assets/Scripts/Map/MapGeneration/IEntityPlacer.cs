using Map.MapGeneration.Entities;

namespace Map.MapGeneration
{
    public interface IEntityPlacer
    {
        void RotateEntity(IEntity entity, int times = 1);
        bool CanPlaceEntity(IEntity entity, int x, int y);
        void PlaceEntity(IEntity entity, int x, int y);
    }
}