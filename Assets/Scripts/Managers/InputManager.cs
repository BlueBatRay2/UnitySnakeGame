using System;
using System.Collections.Generic;
using Commands;
using States;
using States.MainMenu;
using States.Pause;
using States.Play;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public delegate void InputActionHandler(GameInputAction action);
        public static event InputActionHandler OnInputAction;

        private UserInput _input;
        
        private Vector2 _startMousePosition;
        private Vector2 _currentMousePosition;
        private bool _isSwiping = false;
        private readonly Queue<Vector2> _swipeDirectionQueue = new(2);
        private ICommand _startGameCommand;
        
        private void Awake()
        {
            GameManager.OnStateChange += GameManagerOnStateChange;
            
            _input = new UserInput();
            _input.Enable();
        }

        // Start is called before the first frame update
        private void Start()
        {
            //
            //todo abstract this all out
            //
            _input.GamePlay.LeftClick.started += context => OnLeftClickStarted();
            _input.GamePlay.LeftClick.canceled += context => OnLeftClickCanceled();
            _input.GamePlay.Swipe.performed += context => OnSwipePerformed(context.ReadValue<Vector2>());
            _input.GamePlay.Pause.performed += context => OnPausePerformed();
            _input.GamePlay.Up.performed += context => OnUpPerformed();
            _input.GamePlay.Right.performed += context => OnRightPerformed();
            _input.GamePlay.Down.performed += context => OnDownPerformed();
            _input.GamePlay.Left.performed += context => OnLeftPerformed();
            
            _input.MainMenu.Enter.performed += context => OnEnterPerformed();

            _input.Pause.Unpause.performed += context => OnUnpausePerformed();
            
        }

        private void OnLeftClickCanceled()
        {
            _isSwiping = false;

            //we didn't get enough data to figure out swipe direction
            if (_swipeDirectionQueue.Count < 2)
                return;

            GameInputAction directionInput = GetDirectionFromVectors(_swipeDirectionQueue.Dequeue(), _swipeDirectionQueue.Dequeue());

            OnInputAction?.Invoke(directionInput);
        }

        private GameInputAction GetDirectionFromVectors(Vector2 start, Vector2 end)
        {
            Vector2 directionVec = end - start;
            
            if (Mathf.Abs(directionVec.x) > Mathf.Abs(directionVec.y))
            {
                // Horizontal swipe
                return directionVec.x > 0 ? GameInputAction.Right : GameInputAction.Left;
            }
            //vertical
            return directionVec.y > 0 ? GameInputAction.Up : GameInputAction.Down;
        }

        private void OnLeftClickStarted()
        {
            _isSwiping = true;
        }

        private void OnLeftPerformed()
        {
            OnInputAction?.Invoke(GameInputAction.Left);
        }

        private void OnDownPerformed()
        {
            OnInputAction?.Invoke(GameInputAction.Down);
        }

        private void OnRightPerformed()
        {
            OnInputAction?.Invoke(GameInputAction.Right);
        }

        private void OnUpPerformed()
        {
            OnInputAction?.Invoke(GameInputAction.Up);
        }

        private void OnPausePerformed()
        {
            OnInputAction?.Invoke(GameInputAction.Pause);
        }
        private void OnUnpausePerformed()
        {
            OnInputAction?.Invoke(GameInputAction.Pause);
        }
        private void OnEnterPerformed()
        {
            OnInputAction?.Invoke(GameInputAction.Enter);
        }
        private void GameManagerOnStateChange(IGameState oldState, IGameState newState)
        {
            if(oldState != null)
                HandleInputMapActions(oldState, actions => actions.Disable());
            
            HandleInputMapActions(newState, actions => actions.Enable());
        }
                
        //todo abstract this out
        private void HandleInputMapActions(IGameState state, Action<InputActionMap> action)
        {
            InputActionMap actions = new InputActionMap();
            
            switch (state)
            {
                case PlayState:
                    actions = _input.GamePlay;
                    break;
                case PausedState:
                    actions = _input.Pause;
                    break;
                case MainMenuState:
                    actions = _input.MainMenu;
                    break;
                case GameOverState:
                    break;
            }
            
            action.Invoke(actions);
        }
        
        private void OnSwipePerformed(Vector2 delta)
        {
            if (!_isSwiping) return;
            
            if (_swipeDirectionQueue.Count >= 2)
            {
                _swipeDirectionQueue.Dequeue();
            }
            _swipeDirectionQueue.Enqueue(delta);
        }
    }
}
