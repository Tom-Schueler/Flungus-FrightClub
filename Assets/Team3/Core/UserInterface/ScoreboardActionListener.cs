using Team3.Input;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

namespace Team3.UserInterface
{
    public class ScoreboardActionListener : MonoBehaviour
    {
        public UnityEvent OnMenuRequest;

        private PlayerInputActions inputActions;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            inputActions.Enable();
            inputActions.UI.Cancel.performed += OnCancel;
        }

        private void OnDisable()
        {
            inputActions.UI.Cancel.performed -= OnCancel;
            inputActions.Disable();
        }

        public void OnCancel(CallbackContext context)
        {
            OnMenuRequest?.Invoke();
        }
    }
}
