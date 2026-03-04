using Team3.Multiplayer.Lobby;
using Unity.Netcode;
using UnityEngine.Events;

namespace Team3.Multiplayer
{
    public class ClientSceneChangeNotifier : NetworkBehaviour
    {
        public UnityEvent OnChangeScene;

        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
            RoundStarter.OnStartWorldScene += StartWorldScene;
        }

        private void OnDisable()
        {
            RoundStarter.OnStartWorldScene -= StartWorldScene;
        }

        public void StartWorldScene()
        {
            StartWorldClientRpc();
            OnChangeScene?.Invoke();
        }

        [ClientRpc]
        private void StartWorldClientRpc()
        {
            OnChangeScene?.Invoke();
        }
    }
}
