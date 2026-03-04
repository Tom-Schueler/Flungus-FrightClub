using UnityEngine;

namespace Team3.UserInterface.Settings
{
    public class FPSApplyer : DropdownApplyer
    {
        public override void Set(int value)
        {
            switch (value)
            {
                case 0: Application.targetFrameRate = -1; break;
                case 1: Application.targetFrameRate = 360; break;
                case 2: Application.targetFrameRate = 240; break;
                case 3: Application.targetFrameRate = 144; break;
                case 4: Application.targetFrameRate = 60; break;
                case 5: Application.targetFrameRate = 30; break;
                case 6: Application.targetFrameRate = 5; break;
            }
        }
    }
}
