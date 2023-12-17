using Map.MapGeneration.Entities.Concrete;
using UnityEngine;

namespace Map.MapGeneration.Entities.EntitySO
{
    [CreateAssetMenu(fileName = "SingleBlockEntity", menuName = "ScriptableObjects/Entities/SingleBlockEntity", order = 1)]
    public class SingleBlockEntitySo : BaseEntitySo
    {
        public SingleBlockEntitySo() => Entity = new SingleBlockEntity();
    }
}