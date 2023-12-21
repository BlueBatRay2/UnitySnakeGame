using UnityEngine;
using Zenject;

namespace States.MainMenu
{
    public class MainMenuService : IMainMenuService
    {
        [Inject] private GameObject _mainMenuDialog;
        public void ShowMainMenu() => _mainMenuDialog.SetActive(true);
        public void HideMainMenu() => _mainMenuDialog.SetActive(false);
    }
}