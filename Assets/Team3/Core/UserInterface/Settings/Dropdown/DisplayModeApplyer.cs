using UnityEngine;

namespace Team3.UserInterface.Settings
{
    public class DisplayModeApplyer : DropdownApplyer
    {
        public override void Set(int value)
        {
            switch (value)
            {
                case 0: // Fullscreen Window
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;

                case 1: // Windowed
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            }
        }
    }
}
