using Team3.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.UserInterface.MainMenu
{
    public class WeaponDisplay : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image weaponImage;
        [SerializeField] private Image borderImage;
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private Animator animator;

        private WeaponDisplayInfo displayInfo;

        private void Start()
        {
            if (WeaponHolder.IsSet)
            {
                UpdateDisplay();
            }
        }

        private void Update()
        {
            // borderImage.sprite = displayInfo.BorderImage;
        } 

        private void UpdateDisplay()
        {
            displayInfo = WeaponHolder.WeaponDisplayInfo;

            animator.runtimeAnimatorController = displayInfo.RuntimeAnimatorController;
            backgroundImage.sprite = displayInfo.BackgroundImage;
            weaponImage.sprite = displayInfo.WeaponImage;
            borderImage.sprite = displayInfo.BorderImage;
            weaponName.text = displayInfo.WeaponName;
        }
    }
}
