using System;
using Team3.UserInterface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Team3.Characters
{
    public class InventoryMovementBlocker : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private void OnEnable()
        {
            InventroyStateEmitter.OnEntered += Disable;
            InventroyStateEmitter.OnExited += Enable;
        }

        private void OnDisable()
        {
            InventroyStateEmitter.OnEntered -= Disable;
            InventroyStateEmitter.OnExited -= Enable;
        }

        private void Enable()
        {
            playerInput.ActivateInput();
        }

        private void Disable()
        {
            playerInput.DeactivateInput();
        }
    }
}
