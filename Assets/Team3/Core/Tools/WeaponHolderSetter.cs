using Team3.MOBA;
using Team3.UserInterface.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.Tools
{
    public class WeaponHolderSetter : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image weaponImage;
        [SerializeField] private Image borderImage;
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private SOWeapon weapon;
        [SerializeField] private Animator animator;

        private Sprite borderSprite;

        private void Start()
        {
            borderSprite = borderImage.sprite;
        } 

        public void SetWeaponHolder()
        {
            WeaponHolder.Set(weapon.ID, new WeaponDisplayInfo(backgroundImage.sprite, weaponImage.sprite, borderSprite, weaponName.text, animator.runtimeAnimatorController));
        }
    }
}
