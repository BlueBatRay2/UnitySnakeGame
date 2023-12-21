using System.Collections.Generic;
using GamePlay;
using Map.MapGeneration;
using Map.MapGeneration.Entities;
using Map.MapGeneration.Entities.Concrete;
using Map.MapGeneration.EntityPlacement;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class AppleManager : MonoBehaviour
    {
        public GameObject tileToUse;
        
        [Inject]
        private IEntityPlacerFactory _entityPlacerFactory;
        private IEntityPlacer _entityPlacer;
        private Vector2Int _currentApplePos;
        private VisualMap _visualMap;
        private GameObject _currentAppleTile;
        private IEntity _appleEntity;
        public VisualMap VisualMap
        {
            get => _visualMap;
            set
            {
                _visualMap = value;
                _entityPlacer = _entityPlacerFactory.Create(_visualMap.DataMap);
            } 
        }

        public void SpawnApple()
        {
            List<Vector2Int> openTilesPos = new List<Vector2Int>();
            
            VisualMap.DataMap.ForEachTile((tile, x, y) =>
            {
                if(tile.CanEnter)
                    openTilesPos.Add(new Vector2Int(x,y));
                
            });

            if (openTilesPos.Count == 0)
            {
                //todo they won, there's no space to spawn apple
                return;
            }
            
            if (_currentAppleTile == null)
                _currentAppleTile = Instantiate(tileToUse,new Vector3(0 , 0, -2), Quaternion.identity, VisualMap.EntireVisualMap.transform);
            else
                _entityPlacer.DeleteEntity(_currentApplePos);
            
            _currentApplePos = openTilesPos[Random.Range(0, openTilesPos.Count)];
            _appleEntity = new AppleEntity();
            _entityPlacer.PlaceEntity(_appleEntity, _currentApplePos);
            
            _currentAppleTile.transform.localPosition = new Vector3(_currentApplePos.x, -_currentApplePos.y, -1);
        }

        public bool CheckApple(Vector2Int loc) => _currentApplePos == loc;
    }
}