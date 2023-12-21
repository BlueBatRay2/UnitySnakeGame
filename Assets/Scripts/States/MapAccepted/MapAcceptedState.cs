using GamePlay;
using Managers;
using Map.MapGeneration;
using States.Play;
using UnityEngine;

namespace States.MapAccepted
{
    public class MapAcceptedState : IGameState
    {
        public void Enter()
        {
            VisualMap map = GameManager.Instance.Map;
            GameManager.Instance.CollisionManager = new CollisionManager(map);
            GameManager.Instance.AppleManager.VisualMap = map;
            GameManager.Instance.AppleManager.SpawnApple();
            
            Vector2Int snakeStartPos = new Vector2Int(map.DataMap.Width / 2, map.DataMap.Height / 2);
            GameManager.Instance.SnakeManager.VisualMap = map;
            GameManager.Instance.SnakeManager.DestroySnake(); //clean up
            GameManager.Instance.SnakeManager.SpawnSnake(snakeStartPos);
            
            GameManager.Instance.ChangeToState<PlayState>();
        }
        public void Exit(){}
        public void Update(){}
        public void HandleInput(GameInputAction action){}
    }
}