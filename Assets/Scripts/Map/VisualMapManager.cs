using System.Collections.Generic;
using Map.MapGeneration;
using Map.MapGeneration.Entities;
using Map.MapGeneration.Entities.Concrete;
using Map.MapGeneration.Entities.Tiles;
using Map.MapGeneration.EntityPlacement;
using Map.MapGeneration.Strategy;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Map
{
    public class VisualMapManager : MonoBehaviour
    {
        public EntityPrefabMapping entityPrefabMapping;
        public GameObject mapTile;
        public GameObject wallTile;
        public Camera mapCamera;
        
        public VisualMap CurrentMap;
        private GameObject _mapHolder;

        [Inject]
        private IDataMap _newDataMap;

        [Inject]
        private IEntityPlacerFactory _entityPlacerFactory;

        [Inject] 
        private IEntityLayoutStrategyFactory _entityLayoutStrategyFactory;
        
        public void CreateVisualMap(int width, int height)
        {
            //if map exists already, clean up
             if(_mapHolder != null)
                 Destroy(_mapHolder);
            
             CurrentMap = CreateMap(width, height);
             GameManager.Instance.Map = CurrentMap;
        }
    
        private VisualMap CreateMap(int width, int height)
        {
            //get the Entity possibilities 
            var entitiesList = GetEntityPossibilities();

            _newDataMap.InitializeTiles(width, height);
            
            IEntityPlacer entityPlacer = _entityPlacerFactory.Create(_newDataMap);
            
            //place snake starting area in middle of map before walls are placed
            entityPlacer.PlaceEntity(new SnakeStartEntity(), new(_newDataMap.Width/2-2, _newDataMap.Height/2));
            
            IEntityLayoutStrategy layoutStrategy = _entityLayoutStrategyFactory.Create(entityPlacer);
            layoutStrategy.LayoutEntities(_newDataMap, entitiesList);
            
            //create visual map from data map
            VisualMap newVisualMap = GenerateVisualMap(_newDataMap);
            
            //adjust camera
            AdjustCamera(newVisualMap);

            return newVisualMap;
        }

        private void AdjustCamera(VisualMap visualMap)
        {
            int buffer = 2;
            
            // Calculate map size based on tile count and size
            float mapWidth = visualMap.DataMap.Width;
            float mapHeight = visualMap.DataMap.Height;

            // Calculate orthographic size for both width and height
            float screenRatio = (float)Screen.width / Screen.height;
            float orthographicSizeWidth = mapWidth / screenRatio / 2;
            float orthographicSizeHeight = mapHeight / 2;

            // Use the larger value to ensure the entire map is visible
            mapCamera.orthographicSize = Mathf.Max(orthographicSizeWidth, orthographicSizeHeight) + buffer;
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
            GameObject mapObj = Instantiate(mapTile,new Vector3(0 , 0, -2), Quaternion.identity, _mapHolder.transform);
            mapObj.transform.localScale = new Vector3(width, height, 1f);
            mapObj.transform.localPosition = new Vector3(0, 0, 0);
            mapObj.name = "mapObj";
            return _mapHolder;
        }
        
        private VisualMap GenerateVisualMap(IDataMap generatedDataMap)
        {
            if(CurrentMap != null)
                Destroy(CurrentMap.EntireVisualMap);

            var visualBaseMap = GenerateBaseMap(generatedDataMap.Width, generatedDataMap.Height);
            
            generatedDataMap.ForEachTile((tile, x, y) =>
            {
                if (generatedDataMap.IsTileEmpty(x, y))
                    return;
                
                if(tile is SolidTile)
                {
                    GameObject newObj = Instantiate(wallTile,new Vector3(0 , 0, -2), Quaternion.identity, visualBaseMap.transform);
                    newObj.transform.localPosition = new Vector3(x, -y, -1);
                }
            });
            
            //set the entities
            VisualMap newVisualMap = new VisualMap(generatedDataMap, visualBaseMap);
            
            return newVisualMap;
        }
    }
}
