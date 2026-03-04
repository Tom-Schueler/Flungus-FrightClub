using Team3.Characters;
using Team3.MOBA;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Team3.UserInterface.HUD
{
    public class AmmunitionDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text ammunitionLabel;
        
        private PlayerStats stats;
        private WeaponBase weaponBase;

        private void Start()
        {
            stats = PlayerRegistry.GetStats(NetworkManager.Singleton.LocalClientId);
            weaponBase = stats.Combat.WeaponObject.GetComponent<WeaponBase>();
        }

        private void Update()
        {
            ammunitionLabel.text = $"{weaponBase.BulletsLeft} / {weaponBase.MagazineSize}";
        }
    }
}
