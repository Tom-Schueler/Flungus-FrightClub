using UnityEngine.InputSystem;

namespace Team3.UserInterface.Settings
{
    public class KeybordMouseInputSetting : InputSetting
    {
        protected override void OnAnyInput(InputControl control)
        {
            if (control.device is Gamepad)
            { return; }
            
            if (control.device is Keyboard)
            {                
                buttonLabel.text = control.displayName;
            }
            else if (control.device is Mouse)
            {
                buttonLabel.text = control.shortDisplayName;
            }
        }
    }
}
