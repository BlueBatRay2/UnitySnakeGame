using Map.MapGeneration.Entities.Concrete;
using UnityEngine;

namespace Map.MapGeneration.Entities.EntitySO
{
    [CreateAssetMenu(fileName = "LWallEntity", menuName = "ScriptableObjects/Entities/LWallEntity", order = 1)]
    public class LWallEntitySo : BaseEntitySo
    {
        public LWallEntitySo() => Entity = new LWallEntity();
    }
}