using System;
using System.Collections.Generic;
using Team3.Characters;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Team3.Multiplayer
{
    public class PlayerSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject characterPrefab;

        int iterator = 0;
        private Dictionary<ulong, NetworkObject> players = new Dictionary<ulong, NetworkObject>();

        public void Spawn(IReadOnlyDictionary<ulong, int> playerWeapons)
        {
        

            if (!NetworkManager.Singleton.IsServer)
            { throw new Exception("Must be called by server"); }

            foreach (KeyValuePair<ulong, int> playerWeapon in playerWeapons)
            {
                if (iterator == PlayerSpawnPoint.AllPoints.Count)
                {
                    iterator = 0;
                }

                NetworkObject playerObject = SpawnPlayerForClient(playerWeapon.Key, playerWeapon.Value,iterator);
                players.Add(playerWeapon.Key, playerObject);

                Transform spawnPoint = PlayerSpawnPoint.AllPoints[iterator].transform;
                SetPositionClientRpc(playerObject,spawnPoint.position);
                iterator++;
            }
        }

        private NetworkObject SpawnPlayerForClient(ulong clientId, int weaponId, int iterator)
        {
            NetworkObject instance = (NetworkObject)Instantiate(characterPrefab, gameObject.scene);
            instance.transform.rotation = Quaternion.identity;
            instance.transform.position = PlayerSpawnPoint.AllPoints[iterator].transform.position;
            instance.GetComponent<PlayerStats>().Combat.localGunID = weaponId;

            instance.SpawnWithOwnership(clientId);

            return instance;
        }

        public void Respawn(IReadOnlyList<ulong> connectedClientsIds)
        {
            Debug.LogError("Respawn ENABLE");
            foreach (ulong playerId in connectedClientsIds)
            {
                if (iterator == PlayerSpawnPoint.AllPoints.Count)
                {
                    iterator = 0;
                }

                Transform spawnPoint = PlayerSpawnPoint.AllPoints[iterator].transform;

                NetworkObject playerObject = players[playerId];

                SetPositionClientRpc(playerObject, spawnPoint.position);

                PlayerStats stats = PlayerRegistry.GetStats(playerId);
                stats.currentHealth.Value = stats.health;
                stats.Combat.isAllowedToShoot.Value = true;
                // reload

                stats.showBody.Value = true;
                SetVisaulsClientRpc(playerId);

                iterator++;
            }
        }

        [ClientRpc(RequireOwnership = false)]
        private void SetVisaulsClientRpc(ulong id)
        {

            PlayerStats stats = PlayerRegistry.GetStats(id);
            if (IsOwner)
                stats.currentHealth.Value = stats.health;

            //for (int i = 0; i < NetworkManager.Singleton.ConnectedClients.Count; i++)
            //{
            //    PlayerRegistry.GetStats((ulong)i).enabled = false;
            //    PlayerRegistry.GetStats((ulong)i).isDead = false;
            //}

            //stats.isDead = false;
        }

        [ClientRpc]
        private void SetPositionClientRpc(NetworkObjectReference playerObjectReference, Vector3 position)
        {
            NetworkObject playerObject = playerObjectReference;

            playerObject.gameObject.SetActive(false);
         



            if (playerObject.OwnerClientId == NetworkManager.Singleton.LocalClientId)
            {
                playerObject.transform.SetPositionAndRotation(position, Quaternion.identity);
            }

            playerObject.gameObject.SetActive(true);

            Debug.LogError("ENABLE");

            //Just to be sure, Teleport Player Aswell
            if (playerObject.OwnerClientId == NetworkManager.Singleton.LocalClientId) { 
                playerObject.gameObject.GetComponent<NetworkTransform>().Teleport(position, Quaternion.identity, transform.localScale);
                playerObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            }

            
        }

        public void DisableRemainingPlayers()
        {
            Debug.LogError("DISABLE");
            foreach (var player in players)
            {

                PlayerStats stats = PlayerRegistry.GetStats(player.Key);

                stats.isDead = true;
                stats.Combat.isAllowedToShoot.Value = false;
            }
        }
    }
}
