using System;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Enemys
{
    public class NetworkEnemy : NetworkBehaviour
    {
        [SerializeField] private GameObject movement;

        public Action<NetworkEnemy> OnDeath;
        public Action<NetworkEnemy> OnDespawned;

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                movement.SetActive(false);
            }
        }

        public void Die()
        {
            OnDeath?.Invoke(this);

            InternalDie();
        }

        public void Despawn()
        {
            OnDeath?.Invoke(this);

            InternalDespawn();
        }

        protected virtual void InternalDespawn() {}
        protected virtual void InternalDie() {}
    }
}
