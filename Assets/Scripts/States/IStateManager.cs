namespace States
{
    public interface IStateManager
    {
        void ChangeToState<T>() where T : IGameState;
        IGameState GetCurrentState();
    }
}