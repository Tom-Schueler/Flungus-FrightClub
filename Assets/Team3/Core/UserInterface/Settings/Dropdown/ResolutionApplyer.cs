using UnityEngine;

namespace Team3.UserInterface.Settings
{
    public class ResolutionApplyer : DropdownApplyer
    {
        public override void Set(int value)
        {
            switch (value)
            {
                case 0: Screen.SetResolution(3840, 2160, Screen.fullScreenMode); break;
                case 1: Screen.SetResolution(2560, 1400, Screen.fullScreenMode); break;
                case 2: Screen.SetResolution(1920, 1080, Screen.fullScreenMode); break;
                case 3: Screen.SetResolution(1768, 992, Screen.fullScreenMode); break;
                case 4: Screen.SetResolution(1600, 900, Screen.fullScreenMode); break;
                case 5: Screen.SetResolution(1280, 720, Screen.fullScreenMode); break;
            }
        }
    }
}