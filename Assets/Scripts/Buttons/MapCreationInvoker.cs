using Commands;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class MapCreationInvoker : MonoBehaviour
    {
        public TMP_InputField widthInputField;
        public TMP_InputField heightInputField;
        public TMP_Text errorMessage;
        
        private ICommand _createMapCommand;

        void Start()
        {
            Button mapCreationButton = GetComponent<Button>();
            mapCreationButton.onClick.AddListener(TryExecuteCreateMapCommand);
        }
        private void TryExecuteCreateMapCommand()
        {
            if (!ValidateInput(out int width, out int height))
            {
                ShowErrorMessage("Error: Input fields are invalid");
                return;
            }

            VisualMapGenerator mapGenerator = FindObjectOfType<VisualMapGenerator>();
            _createMapCommand = new CreateVisualMapCommand(mapGenerator, width, height);
            _createMapCommand.Execute();
        }

        private bool ValidateInput(out int width, out int height)
        {
            bool isWidthValid = int.TryParse(widthInputField.text, out width);
            bool isHeightValid = int.TryParse(heightInputField.text, out height);
        
            return isWidthValid && isHeightValid;
        }
        private void ShowErrorMessage(string message)
        {
            errorMessage.text = message;
        }
    }
}