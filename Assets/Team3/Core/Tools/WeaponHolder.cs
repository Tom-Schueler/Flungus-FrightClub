using Team3.UserInterface.MainMenu;

namespace Team3.Tools
{
    public static class WeaponHolder
    {
        private static int weaponId = 3;
        private static WeaponDisplayInfo weaponDisplayInfo;
        
        public static int WeaponId => weaponId;
        public static WeaponDisplayInfo WeaponDisplayInfo => weaponDisplayInfo;

        private static bool isSet = false;
        public static bool IsSet => isSet;

        public static void Set(int weaponId, WeaponDisplayInfo weaponDisplayInfo)
        {
            WeaponHolder.weaponId = weaponId;
            WeaponHolder.weaponDisplayInfo = weaponDisplayInfo;

            isSet = true;
        }
    }
}
