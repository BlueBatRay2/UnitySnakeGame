using System.Collections.Generic;
using System.Linq;
using GamePlay.SnakeObjects;
using Map.MapGeneration;
using Map.MapGeneration.Entities.Concrete;
using Map.MapGeneration.EntityPlacement;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SnakeManager : MonoBehaviour
    {
        public GameObject bodyTile;
        public GameObject headTile;
        public Snake CurrentSnake;
        private List<GameObject> _snakeSegmentVisualList;
        
        [Inject]
        private IEntityPlacerFactory _entityPlacerFactory;
        private IEntityPlacer _entityPlacer;
        private Vector2Int _currentApplePos;
        private VisualMap _visualMap;

        public VisualMap VisualMap
        {
            get => _visualMap;
            set
            {
                _visualMap = value;
                _entityPlacer = _entityPlacerFactory.Create(_visualMap.DataMap);
            } 
        }

        public void SpawnSnake(Vector2Int snakeStartPos)
        {
            CurrentSnake = new Snake(3, snakeStartPos, Direction.Left);
            
            _entityPlacer.PlaceEntity(new SnakeEntity(), snakeStartPos);

            _snakeSegmentVisualList = new List<GameObject>();
            
            CreateHead();
            CreateBody();
        }

        private void CreateBody()
        {
            foreach (var segmentCoord in CurrentSnake.Body.Skip(1))
            {
                GameObject segment = Instantiate(bodyTile,new Vector3(0 , 0, -2), Quaternion.identity, GameManager.Instance.Map.EntireVisualMap.transform);
                segment.transform.localPosition = new Vector3(segmentCoord.x, -segmentCoord.y, -2);
                _snakeSegmentVisualList.Add(segment);
            }
        }

        private void CreateHead()
        {
            GameObject head = Instantiate(headTile,new Vector3(0 , 0, -2.1f), Quaternion.identity, GameManager.Instance.Map.EntireVisualMap.transform);
            head.transform.localPosition = new Vector3(CurrentSnake.HeadPosition.x, -CurrentSnake.HeadPosition.y, -2.1f);
            _snakeSegmentVisualList.Add(head);
            ChangeSnakeHeadDirection(CurrentSnake.CurrentDirection);
        }

        private void ChangeSnakeHeadDirection(Direction direction)
        {
            int degrees = 0;
            switch (direction)
            {
                case Direction.Left:
                    degrees = 270;
                    break;
                case Direction.Up:
                    degrees = 180;
                    break;
                case Direction.Right:
                    degrees = 90;
                    break;
                case Direction.Down:
                    degrees = 0;
                    break;
            }

            _snakeSegmentVisualList[0].transform.GetChild(0).rotation = Quaternion.Euler(0, 0, degrees);
        }
        public void ChangeSnakeDirection(Direction newDirection)
        {
            if (!CurrentSnake.CanTurn(newDirection))
                return;
            
            CurrentSnake.CurrentDirection = newDirection;
            ChangeSnakeHeadDirection(newDirection);
        }
        
        public void Move()
        {
            CurrentSnake.Move();
            MoveSnakeTiles();
        }

        public void MoveAndGrow()
        {
            var oldTail = CurrentSnake.TailPosition;
            GameObject segment = Instantiate(bodyTile,new Vector3(0 , 0, -2), Quaternion.identity, GameManager.Instance.Map.EntireVisualMap.transform);
            segment.transform.localPosition = new Vector3(oldTail.x, oldTail.y, -2);
            
            CurrentSnake.MoveAndGrow();
            
            _snakeSegmentVisualList.Add(segment);
            MoveSnakeTiles();
        }

        private void MoveSnakeTiles()
        {
            int segmentIndex = 0;
            foreach (var segment in CurrentSnake.Body)
            {
                _snakeSegmentVisualList[segmentIndex].transform.localPosition = new Vector3(segment.x, -segment.y, _snakeSegmentVisualList[segmentIndex].transform.localPosition.z);
                segmentIndex++;
            }
        }

        public void DestroySnake()
        {
            if (_snakeSegmentVisualList == null)
                return;
            
            foreach (var snakeSegmentVisual in _snakeSegmentVisualList)
            {
                Destroy(snakeSegmentVisual);
            }
        }
    }
}
