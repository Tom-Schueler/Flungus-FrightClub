using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Events;

namespace Team3.Multiplayer.Lobby
{
    public class Disconnector : MonoBehaviour
    {
        public UnityEvent OnDisconnected;

        public void Disconnect()
        {
            // TODO: this will throw if there is not network manager
            
            NetworkManager.Singleton.Shutdown();
            AuthenticationService.Instance.SignOut();

            Destroy(NetworkManager.Singleton.gameObject);

            LobbyInstructions.ClearInstructions();

            OnDisconnected?.Invoke();
        }
    }
}
