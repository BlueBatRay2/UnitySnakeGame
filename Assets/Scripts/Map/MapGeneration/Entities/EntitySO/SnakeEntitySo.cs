using Map.MapGeneration.Entities.Concrete;
using UnityEngine;

namespace Map.MapGeneration.Entities.EntitySO
{
    [CreateAssetMenu(fileName = "SnakeEntity", menuName = "ScriptableObjects/Entities/SnakeEntity", order = 1)]
    public class SnakeEntitySo : BaseEntitySo
    {
        public SnakeEntitySo() => Entity = new SnakeEntity();
    }
}