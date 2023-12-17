using UnityEngine;

namespace Map.MapGeneration
{
    public class VisualMap
    {
        public DataMap Datamap { get; }
        public GameObject EntireVisualMap { get; set; }
        public VisualMap(DataMap datamap, GameObject entireVisualMap)
        {
            Datamap = datamap;
            EntireVisualMap = entireVisualMap;
        }
    }
}