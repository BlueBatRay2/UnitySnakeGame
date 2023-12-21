using States;
using States.MapAccepted;

namespace Commands
{
    public class StartGameCommand : ICommand
    {
        public void Execute()
        {
            GameManager.Instance.ChangeToState<MapAcceptedState>();
        }
    }
}