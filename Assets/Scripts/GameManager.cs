using Managers;
using Map.MapGeneration;
using States;
using States.MainMenu;
using States.Play;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public delegate void StateChangeHandler(IGameState previousState, IGameState newState);
    public static event StateChangeHandler OnStateChange;
    public static GameManager Instance { get; private set; }
    private IGameState _currentState;
    
    [Inject]
    public StateFactory _stateFactory;

    public VisualMap Map { get; set; }
    
    public AppleManager AppleManager;
    public SnakeManager SnakeManager;
    public CollisionManager CollisionManager { get; set; }
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Start() {
        ChangeToState<MainMenuState>();
    }

    public void ChangeToState<T>() where T : IGameState
    {
        IGameState newState;
        if (typeof(T) == typeof(PlayState))
        {
            newState = _stateFactory.CreateWithArgs<PlayState>(
                SnakeManager, AppleManager, CollisionManager);
        }
        else
        {
            newState = _stateFactory.Create<T>();
        }

        ChangeState(newState);
    }
    
    private void ChangeState(IGameState newState) {
        _currentState?.Exit();
        OnStateChange?.Invoke(_currentState, newState);
        _currentState = newState;
        _currentState.Enter();
    }

    void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }
}
