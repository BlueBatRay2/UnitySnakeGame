using Map.MapGeneration.Entities.Concrete;
using UnityEngine;

namespace Map.MapGeneration.Entities.EntitySO
{
    [CreateAssetMenu(fileName = "ThreeBlockWallEntity", menuName = "ScriptableObjects/Entities/ThreeBlockWallEntity", order = 1)]
    public class ThreeBlockWallEntitySo : BaseEntitySo
    {
        public ThreeBlockWallEntitySo() => Entity = new ThreeBlockWallEntity();
    }
}