namespace States
{
    public interface IGameState
    {
        void Enter();
        void Exit();
        void Update();
        void HandleInput(GameInputAction action);
    }
}