using UnityEngine;
using UnityEngine.InputSystem;

namespace Team3.UserInterface.Settings
{
    public class GamepadInputSetting : InputSetting
    {
        protected override void OnAnyInput(InputControl control)
        {
            if (control.device is Keyboard or Mouse)
            { return; }

            string inputString = control.name;
            inputString = char.ToUpper(inputString[0]) + inputString[1..];
            buttonLabel.text = inputString;
        }
    }
}
