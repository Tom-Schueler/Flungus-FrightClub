using Team3.Characters;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Team3.Combat
{
    public abstract class SOProjectilePerk : ScriptableObject
    {
        public int ID = -1;

        [SerializeField]
        private SOVFX vFX;

        [SerializeField]
        private float cooldown = 0;
        public SOVFX VFX => vFX;

        public float CoolDown => cooldown;

        public abstract void TriggerPerk(ProjectileCore projectile = null, ulong OwnerID = 420, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), NetworkObjectReference hitRef = default, Collision collision = null);

        public abstract void TriggerDeathPerk(ProjectileCore projectile = null, ulong OwnerID = 420, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), NetworkObjectReference hitRef = default, Collision collision = null);

        public abstract void AdjustStats(ProjectileCore projectile);

        public abstract void ServerTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1, NetworkObjectReference hitRef = default);
        public abstract void ClientTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1);

        public abstract void PerkUpdate();
        public abstract void PerkApply(Vector3 position, Vector3 rotation, NetworkObjectReference hitRef = default);
    }

}