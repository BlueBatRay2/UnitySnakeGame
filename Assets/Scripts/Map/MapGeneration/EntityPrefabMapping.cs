using Map.MapGeneration.Entities;
using Map.MapGeneration.Entities.EntitySO;
using UnityEngine;

namespace Map.MapGeneration
{
    [CreateAssetMenu(fileName = "EntityPrefabMapping", menuName = "ScriptableObjects/EntityPrefabMapping", order = 1)]
    public class EntityPrefabMapping : ScriptableObject {
        [System.Serializable]
        public struct EntityPrefabPair {
            public BaseEntitySo entityType;
            public int lowerRange;
            public int upperRange;
        }

        public EntityPrefabPair[] mappings;
    }

}