using System;
using System.Collections;
using Team3.SavingLoading.SaveData;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Multiplayer.Player
{
    public class MultiplayerPlayer : NetworkBehaviour
    {
        public static Action<MultiplayerPlayer> OnPlayerJoinedLobby;
        public static Action<ulong> OnUpdateLatencyDisplay;
        public Action OnDisconnect;

        public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>(
            string.Empty,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );

        public bool IsOwnedByHost => NetworkObject.OwnerClientId == NetworkManager.ServerClientId;

        public override void OnNetworkSpawn()
        {
            if (IsClient && IsOwner)
            {
                playerName.Value = GameData.Singleton.name;
                StartCoroutine(PingServer());
            }

            OnPlayerJoinedLobby?.Invoke(this);
        }

        public override void OnDestroy()
        {
            OnDisconnect?.Invoke();
        }

        private IEnumerator PingServer()
        {
            while (true)
            {
                PingServerRpc();
                ulong rtt = NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetCurrentRtt(NetworkManager.ServerClientId);
                OnUpdateLatencyDisplay?.Invoke(rtt);
                yield return new WaitForSeconds(1f);
            }
        }

        [ServerRpc]
        private void PingServerRpc() { }
    }
}
