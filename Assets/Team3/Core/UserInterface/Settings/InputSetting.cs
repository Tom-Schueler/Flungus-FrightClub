using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace Team3.UserInterface.Settings
{
    public abstract class InputSetting : MonoBehaviour
    {
        [SerializeField] protected TMP_Text buttonLabel;
        [SerializeField] private Button button;
        [SerializeField] private CanvasGroup allGroup;
        [Tooltip("The Delay is in ms."), Range(150, 500),
        SerializeField] private int activationDelay;

        private string path;
        public string Path => path;
        public string DisplayText => buttonLabel.text;

        public void Load(string path, string displayText)
        {
            this.path = path;
            buttonLabel.text = displayText;
        }

        private IDisposable eventListener;

        private void OnEnable()
        {
            button.onClick.AddListener(ListenForAction);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ListenForAction);
        }

        private void ListenForAction()
        {
            allGroup.interactable = false;
            eventListener = InputSystem.onAnyButtonPress.Call(RegisterInput);
        }

        private async void RegisterInput(InputControl control)
        {
            eventListener?.Dispose();

            if (control != Keyboard.current.escapeKey)
            { 
                path = control.path;

                OnAnyInput(control);
            }

            await Task.Delay(activationDelay);

            allGroup.interactable = true;
        }

        protected abstract void OnAnyInput(InputControl control);
    }
}
