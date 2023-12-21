using Managers;
using States.MapAccepted;
using Zenject;

namespace States.MainMenu
{
    public class MainMenuState : IGameState
    {
        [Inject]
        private IMainMenuService _mainMenuService;
        
        public void Enter()
        {
            _mainMenuService.ShowMainMenu();
            InputManager.OnInputAction += HandleInput;
        }
        public void Exit()
        {
            _mainMenuService.HideMainMenu();
            InputManager.OnInputAction -= HandleInput;
        }

        public void Update(){}

        public void HandleInput(GameInputAction action)
        {
            switch (action)
            {
                case GameInputAction.Enter:
                    GameManager.Instance.ChangeToState<MapAcceptedState>();
                    break;
            }
        }
    }
}