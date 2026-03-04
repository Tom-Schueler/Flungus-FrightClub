using UnityEngine;

namespace Team3.UserInterface.MainMenu
{
    public struct WeaponDisplayInfo
    {
        private Sprite backgroundImage;
        private Sprite weaponImage;
        private Sprite borderImage;
        private string weaponName;
        private RuntimeAnimatorController runtimeAnimatorController;

        public Sprite BackgroundImage => backgroundImage;
        public Sprite WeaponImage => weaponImage;
        public Sprite BorderImage => borderImage;
        public string WeaponName => weaponName;
        public RuntimeAnimatorController RuntimeAnimatorController => runtimeAnimatorController;

        public WeaponDisplayInfo(Sprite backgroundImage, Sprite weaponImage, Sprite borderImage, string weaponName, RuntimeAnimatorController runtimeAnimatorController)
        {
            this.backgroundImage = backgroundImage;
            this.weaponImage = weaponImage;
            this.borderImage = borderImage;
            this.weaponName = weaponName;
            this.runtimeAnimatorController = runtimeAnimatorController;
        }
    }
}
