using Map.MapGeneration.Entities;
using UnityEngine;

namespace Map.MapGeneration
{
    public interface IEntityPlacer
    {
        void RotateEntity(IRotatableEntity entity, int times = 1);
        bool CanPlaceEntity(IEntity entity, Vector2Int position);
        void PlaceEntity(IEntity entity, Vector2Int position);
        void DeleteEntity(Vector2Int position);
    }
}