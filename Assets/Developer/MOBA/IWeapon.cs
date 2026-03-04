using Team3.Weapons;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Team3.MOBA
{
    public interface IWeapon
    {
        void Shoot(Vector3 hitPoint, bool hasHit, bool triggerPerks, RaycastHit hitTarget = default);
        void Reload();
        void ForceReload();
        void SetBulletElement(DamageType elementalType);
        void InputShootAction(CallbackContext callbackContext);
        void InputReloadAction(CallbackContext callbackContext);
    }
}