using Team3.Characters;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Team3.Weapons
{
    public interface IWeapon
    {
        void Initialize(SOWeapon weaponData);
        void Shoot(PlayerStats char_Stats, CallbackContext callbackContext);
        void Reload(PlayerStats char_Stats);


    }
}