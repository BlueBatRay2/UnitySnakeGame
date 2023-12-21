using GamePlay.SnakeObjects;
using Managers;
using States.Pause;
using UnityEngine;

namespace States.Play
{
    public class PlayState : IGameState
    {
        private const float TickInterval = 0.2f; // Seconds between each snake move
        private float _tickTimer = 0f;
        
        private readonly SnakeManager _snakeManager;
        private readonly AppleManager _appleManager;
        private readonly CollisionManager _collisionManager;
        
        public PlayState(SnakeManager snakeManager, AppleManager appleManager, CollisionManager collisionManager)
        {
            _snakeManager = snakeManager;
            _appleManager = appleManager;
            _collisionManager = collisionManager;
        }
        public void Enter() => InputManager.OnInputAction += HandleInput;
        public void Exit() => InputManager.OnInputAction -= HandleInput;

        public void Update()
        {
            _tickTimer += Time.deltaTime;
  
            //things that should be controlled by speed
            if (_tickTimer >= TickInterval)
            {
                Vector2Int snakeNextPos = _snakeManager.CurrentSnake.GetNextMovePosition();
                
                //check collisions
                HandleCollisions(snakeNextPos, _snakeManager.CurrentSnake);
                
                //move 
                HandleMove(snakeNextPos);
                
                _tickTimer = 0f;
            }
        }

        private void HandleCollisions(Vector2Int snakeNextPos, Snake snake)
        {
            if (_collisionManager.IsCollided(snakeNextPos, snake))
            {
                GameManager.Instance.ChangeToState<GameOverState>();
            }
        }
        private void HandleMove(Vector2Int snakeNextPos)
        {
            if (_appleManager.CheckApple(snakeNextPos))
            {
                _snakeManager.MoveAndGrow();
                _appleManager.SpawnApple();
                
            }
            else
            {
                _snakeManager.Move();
            }
        }
        public void HandleInput(GameInputAction action)
        {
            switch (action)
            {
                case GameInputAction.Pause:
                    GameManager.Instance.ChangeToState<PausedState>();
                    break;
                case GameInputAction.Up:
                    _snakeManager.ChangeSnakeDirection(Direction.Up);
                    break;
                case GameInputAction.Right:
                    _snakeManager.ChangeSnakeDirection(Direction.Right);
                    break;
                case GameInputAction.Down:
                    _snakeManager.ChangeSnakeDirection(Direction.Down);
                    break;
                case GameInputAction.Left:
                    _snakeManager.ChangeSnakeDirection(Direction.Left);
                    break;
            }
        }
    }
}