using Commands;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class StartButtonInvoker : MonoBehaviour
    {
        private ICommand _startGameCommand;
        void Start()
        {
            Button startButton = GetComponent<Button>();
            startButton.onClick.AddListener(ExecuteCommand);
        }
        private void ExecuteCommand()
        {
            _startGameCommand = new StartGameCommand();
            _startGameCommand.Execute();
        }
    }
}