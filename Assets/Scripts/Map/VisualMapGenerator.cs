using System.Collections.Generic;
using Map.MapGeneration;
using Map.MapGeneration.Entities;
using Map.MapGeneration.Strategy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class VisualMapGenerator : MonoBehaviour
    {
        public EntityPrefabMapping entityPrefabMapping;
        public GameObject baseMapGameObject;
        public GameObject wallObject;
        public Camera mapCamera;
        
        private VisualMap _currentMap;
        private GameObject _mapHolder;
        
        public void CreateVisualMap(int width, int height)
        {
            //if map exists already, clean up
             if(_mapHolder != null)
                 Destroy(_mapHolder);
            
            _currentMap = CreateMap(width, height);
        }
    
        private VisualMap CreateMap(int width, int height)
        {
            //get the Entity possibilities 
            var entitiesList = GetEntityPossibilities();

            //generate underlying data map
            DataMap newDataMap = MapGenerator.GenerateDataMap(width, height, new RandomMapGenerationStrategy(), entitiesList);

            //create visual map from data map
            VisualMap newVisualMap = GenerateVisualMap(newDataMap);
            
            //adjust camera
            AdjustCamera(newVisualMap);

            return newVisualMap;
        }

        private void AdjustCamera(VisualMap visualMap)
        {
            int buffer = 2;
            
            // Calculate map size based on tile count and size
            float mapWidth = visualMap.Datamap.Width;
            float mapHeight = visualMap.Datamap.Height;

            // Calculate orthographic size for both width and height
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float orthographicSizeWidth = mapWidth / screenRatio / 2;
            float orthographicSizeHeight = mapHeight / 2;

            // Use the larger value to ensure the entire map is visible
            mapCamera.orthographicSize = Mathf.Max(orthographicSizeWidth, orthographicSizeHeight) + buffer;
            
            //
            // int tileSize = 1;
            // // Calculate map size based on tile count and size
            // float mapWidth = visualMap.Datamap.Width * tileSize;
            // float mapHeight = visualMap.Datamap.Height * tileSize;
            //
            // // Determine larger dimension
            // float largestDimension = Mathf.Max(mapWidth, mapHeight);
            //
            // // Calculate orthographic size
            // float screenRatio = (float)Screen.width / (float)Screen.height;
            // float orthographicSize = largestDimension / screenRatio / 2;
            // mapCamera.orthographicSize = orthographicSize + 2;

        }
        private List<IEntity> GetEntityPossibilities()
        {
            List<IEntity> entitiesList = new List<IEntity>();

            foreach (var mapping in entityPrefabMapping.mappings)
            {
                int instanceAmount = Random.Range(mapping.lowerRange, mapping.upperRange + 1);
                for (int i = 0; i < instanceAmount; i++)
                {
                    entitiesList.Add(mapping.entityType.Entity);
                }
            }
            return entitiesList;
        }
        
        private GameObject GenerateBaseMap(int width, int height)
        {
            //container
            _mapHolder = new GameObject("MapHolder");
            _mapHolder.transform.position = new Vector3(-width/2, height/2, 0);
            
            //map
            GameObject mapObj = Instantiate(baseMapGameObject,new Vector3(0 , 0, -2), Quaternion.identity, _mapHolder.transform);
            mapObj.transform.localScale = new Vector3(width, height, 1f);
            mapObj.transform.localPosition = new Vector3(0, 0, 0);
            mapObj.name = "mapObj";
            return _mapHolder;
        }
        
        private VisualMap GenerateVisualMap(DataMap generatedDataMap)
        {
            if(_currentMap != null)
                Destroy(_currentMap.EntireVisualMap);

            var baseMap = GenerateBaseMap(generatedDataMap.Width, generatedDataMap.Height);

            
            generatedDataMap.ForEachTile((tile, x, y) =>
            {
                if (!generatedDataMap.IsTileEmpty(x, y))
                {
                    GameObject newObj = Instantiate(wallObject,new Vector3(0 , 0, -2), Quaternion.identity, baseMap.transform);
                    newObj.transform.localPosition = new Vector3(x, -y, -1);
                }
            });
            
            //set the entities
            VisualMap newVisualMap =  new VisualMap(generatedDataMap, baseMap);
            
            
            Debug.Log(newVisualMap.Datamap.ToString());
            
            return newVisualMap;
        }
    }
}
