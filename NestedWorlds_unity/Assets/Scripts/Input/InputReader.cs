using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace IsmaLB.Input
{
    [CreateAssetMenu(fileName = "Input", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInputActions.IPlayerActions, GameInputActions.IPuzzleActions, GameInputActions.IPauseActions
    {
        // Player actions
        public UnityAction interactEvent;
        public UnityAction jumpEvent;
        public UnityAction<Vector2> lookEvent;
        public UnityAction<Vector2> moveEvent;
        public UnityAction previousEvent;
        public UnityAction nextEvent;

        // Puzzle actions
        public UnityAction<Vector2> pointerPositionEvent;
        public UnityAction grabPressedEvent;
        public UnityAction grabReleasedEvent;
        public UnityAction restartEvent;
        public UnityAction quitEvent;

        // pause actions
        public UnityAction pauseMenuEvent;

        // internal input
        GameInputActions gameInput;

        bool isExploration = true;

        void OnEnable()
        {
            if (gameInput == null)
            {
                gameInput = new GameInputActions();
                gameInput.Player.SetCallbacks(this);
                gameInput.Puzzle.SetCallbacks(this);
                gameInput.Pause.SetCallbacks(this);
                EnableExplorationInput();
            }
        }
        void OnDisable()
        {
            DisableAllInput();
        }
        public void PauseInput()
        {
            gameInput.Player.Disable();
            gameInput.Puzzle.Disable();
        }
        public void RestoreLastInputMode()
        {
            if (isExploration) EnableExplorationInput();
            else EnablePuzzleInput();
        }
        public void DisableAllInput()
        {
            gameInput.Pause.Disable();
            gameInput.Player.Disable();
            gameInput.Puzzle.Disable();
        }
        public void EnableExplorationInput()
        {
            isExploration = true;
            gameInput.Pause.Enable();
            gameInput.Player.Enable();
            gameInput.Puzzle.Disable();
        }
        public void EnablePuzzleInput()
        {
            isExploration = false;
            gameInput.Pause.Enable();
            gameInput.Player.Disable();
            gameInput.Puzzle.Enable();
        }


        #region Callbacks

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                interactEvent?.Invoke();
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                jumpEvent?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                nextEvent?.Invoke();
            }
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                previousEvent?.Invoke();
            }
        }

        public void OnGrab(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                grabPressedEvent?.Invoke();
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                grabReleasedEvent?.Invoke();
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            pointerPositionEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnQuit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                quitEvent?.Invoke();
            }
        }

        public void OnRestart(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                restartEvent?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                pauseMenuEvent?.Invoke();
            }
        }
        #endregion
    }
}
