using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Multiplayer.Lobby
{
    public class ReadyTracker : NetworkBehaviour
    {
        private static Dictionary<ulong, bool> readyBook = new Dictionary<ulong, bool>();

        public static int PlayerCount => readyBook.Count;
        private static bool isActive = false;

        public override void OnNetworkSpawn()
        {
            if (!IsServer && IsOwner)
            {
                Debug.LogWarning($"The {nameof(ReadyTracker)} must only be owned by server");
                throw new System.Exception($"{nameof(ReadyTracker)} must be owned by server");
            }

            if (isActive)
            {
                Debug.LogWarning($"Duplicate {nameof(ReadyTracker)} detected. Destroying it.");
                Destroy(gameObject);
                return;
            }

            isActive = true;


            if (NetworkManager.Singleton.IsClient)
            {
                RoundStarter.OnClientToggledReady += TogglePlayerReadyServerRpc;
            }

            if (!NetworkManager.Singleton.IsServer)
            { return; }

            NetworkManager.Singleton.OnClientConnectedCallback += AddPlayer;
            NetworkManager.Singleton.OnClientDisconnectCallback += RemovePlayer;
        }

        public override void OnNetworkDespawn()
        {
            isActive = false;


            if (NetworkManager.Singleton.IsClient)
            {
                RoundStarter.OnClientToggledReady -= TogglePlayerReadyServerRpc;
            }

            if (!NetworkManager.Singleton.IsServer)
            { return; }

            NetworkManager.Singleton.OnClientConnectedCallback -= AddPlayer;
            NetworkManager.Singleton.OnClientDisconnectCallback -= RemovePlayer;
        }

        private void AddPlayer(ulong clientId)
        {
            if (!NetworkManager.Singleton.IsServer)
            { return; }

            if (clientId == NetworkManager.LocalClientId)
            { return; }

            if (readyBook.TryAdd(clientId, false))
            {
                AddEnrtyClientRpc(clientId);
            }
        }

        [ClientRpc]
        private void AddEnrtyClientRpc(ulong clientId)
        {
            readyBook.TryAdd(clientId, false);
        }


        private void RemovePlayer(ulong clientId)
        {
            if (!NetworkManager.Singleton.IsServer)
            { return; }

            if (clientId == NetworkManager.LocalClientId)
            { return; }

            readyBook.Remove(clientId);
            RemoveEntryClientRpc(clientId);
        }

        [ClientRpc]
        private void RemoveEntryClientRpc(ulong clientId)
        {
            readyBook.Remove(clientId);
        }


        [ServerRpc(RequireOwnership = false)]
        private void TogglePlayerReadyServerRpc(ulong clientId, bool isReady)
        {
            if (readyBook.ContainsKey(clientId))
            {
                readyBook[clientId] = isReady;
                TogglePlayerReadyClientRpc(clientId, isReady);
            }
            else
            {
                throw new KeyNotFoundException($"Expected key: '{clientId}' was expected, but not fund.");
            }
        }

        [ClientRpc]
        private void TogglePlayerReadyClientRpc(ulong clientId, bool isReady)
        {
            readyBook[clientId] = isReady;
        }

        public static bool AllReady()
        {
            if (!isActive)
            { return false; }

            foreach (bool readyMarker in readyBook.Values)
            {
                if (readyMarker == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsClientReady(ulong clientId)
        {
            if (readyBook.ContainsKey(clientId))
            {
                return readyBook[clientId];
            }

            return false;
        }

    }
}
