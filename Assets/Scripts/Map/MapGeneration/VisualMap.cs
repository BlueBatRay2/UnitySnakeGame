using UnityEngine;

namespace Map.MapGeneration
{
    public class VisualMap
    {
        public IDataMap DataMap { get; }
        public GameObject EntireVisualMap { get; set; }
        public VisualMap(IDataMap dataMap, GameObject entireVisualMap)
        {
            DataMap = dataMap;
            EntireVisualMap = entireVisualMap;
        }
    }
}