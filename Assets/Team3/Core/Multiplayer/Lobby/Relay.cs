using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Team3.Multiplayer.Lobby
{
    public class Relay : MonoBehaviour
    {
        [SerializeField] private UnityTransport transport;
        [SerializeField, Range(2, 8)] private int maxPlayers;
        [SerializeField] private CanvasGroup allGroup;
        [SerializeField] private GameObject playerList;
        [SerializeField] private TMP_Text lobbyCodeDisplay;

        public UnityEvent OnJoinGameFailed;

        private async void Awake()
        {
            if (!LobbyInstructions.IsSet)
            {
                throw new ArgumentException($"The instructions for the lobby have not been set. Make sure you only open a lobby through the prelobby-scene");
            }

            if (transport == null)
            {
                throw new ArgumentNullException($"{nameof(transport)} is null");
            }

            allGroup.interactable = false;

            await Authenticate();

            if (LobbyInstructions.LobbyInstruction == LobbyInstructionType.Host)
            {
                await HostGame();
            }
            else
            {
                await TryJoinGame(LobbyInstructions.LobbyCode);
            }

            allGroup.interactable = true;
        }

        private static async Task Authenticate()
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            { 
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        private async Task HostGame()
        {
            Allocation a = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            lobbyCodeDisplay.text = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
            transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

            NetworkManager.Singleton.StartHost();
        }

        private async Task TryJoinGame(string joinCode)
        {
            JoinAllocation a;
            try
            {
                a = await RelayService.Instance.JoinAllocationAsync(joinCode);
            }
            catch (Exception ex)
            {
                LobbyInstructions.ClearInstructions();
                Debug.Log($"Relay, Join faild: {ex}");
                OnJoinGameFailed?.Invoke();

                AuthenticationService.Instance.SignOut();
                return;
            }

            transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);

            NetworkManager.Singleton.StartClient();

            lobbyCodeDisplay.text = joinCode;
        }
    }
}
