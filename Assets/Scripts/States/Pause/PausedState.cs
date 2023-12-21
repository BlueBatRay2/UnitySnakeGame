using Managers;
using States.Play;
using Zenject;

namespace States.Pause
{
    public class PausedState : IGameState
    {
        [Inject]
        private IPauseService _pauseService;
        public void Enter()
        {
            _pauseService.ShowPauseScreen();
            InputManager.OnInputAction += HandleInput;
        }

        public void Exit()
        {
            _pauseService.HidePauseScreen();
            InputManager.OnInputAction -= HandleInput;
        }

        public void Update(){}
        public void HandleInput(GameInputAction action)
        {
            switch (action)
            {
                case GameInputAction.Pause:
                    GameManager.Instance.ChangeToState<PlayState>();
                    break;
            }
        }
    }
}