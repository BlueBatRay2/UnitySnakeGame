using States.GameOver;
using Zenject;

namespace States
{
    public class GameOverState : IGameState
    {
        [Inject]
        private IGameOverService _gameOverService;
        public void Enter() => _gameOverService.ShowGameOverScreen();
        public void Exit() => _gameOverService.HideGameOverScreen();
        public void Update(){}

        public void HandleInput(GameInputAction action){}
    }
}