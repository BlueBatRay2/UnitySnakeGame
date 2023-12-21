using UnityEngine;
using Zenject;

namespace States.GameOver
{
    public class GameOverService : IGameOverService
    {
        [Inject] private GameObject _gameOverDialog;
        public void ShowGameOverScreen() => _gameOverDialog.SetActive(true);
        public void HideGameOverScreen() => _gameOverDialog.SetActive(false);
    }
}