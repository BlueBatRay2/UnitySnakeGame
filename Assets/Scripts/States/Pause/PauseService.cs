using UnityEngine;
using Zenject;

namespace States.Pause
{
    public class PauseService : IPauseService
    {
        [Inject] private GameObject _pauseDialog;
        public void ShowPauseScreen() => _pauseDialog.SetActive(true);
        public void HidePauseScreen() => _pauseDialog.SetActive(false);
    }
}