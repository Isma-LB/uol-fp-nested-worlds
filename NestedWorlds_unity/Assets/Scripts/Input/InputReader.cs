using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace IsmaLB.Input
{
    [CreateAssetMenu(fileName = "Input", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, GameInputActions.IPlayerActions
    {
        // Player actions
        public UnityAction interactEvent;
        public UnityAction jumpEvent;
        public UnityAction<Vector2> lookEvent;
        public UnityAction<Vector2> moveEvent;
        public UnityAction previousEvent;
        public UnityAction nextEvent;

        GameInputActions gameInput;
        void OnEnable()
        {
            if (gameInput == null)
            {
                gameInput = new GameInputActions();
                gameInput.Player.SetCallbacks(this);
                EnablePlayerInput();
            }
        }
        void OnDisable()
        {
            DisableAllInput();
        }
        public void DisableAllInput()
        {
            gameInput.Player.Disable();
            gameInput.UI.Disable();
        }
        public void EnablePlayerInput()
        {
            gameInput.Player.Enable();
        }

        #region Callbacks

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
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
        #endregion
    }
}
