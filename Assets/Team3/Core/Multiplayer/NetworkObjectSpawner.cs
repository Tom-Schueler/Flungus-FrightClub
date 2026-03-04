using Unity.Netcode;
using UnityEngine;

namespace Team3.Multiplayer
{
    public class NetworkObjectSpawner : MonoBehaviour
    {
        [SerializeField] private NetworkObjectSpawnInfo[] NetworkObjectSpawnInfo;

        private void Start()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                SpawnNetworkObjects();
            }
            else if (NetworkManager.Singleton.IsClient)
            {
                Destroy(gameObject);
            }
            else
            {
                NetworkManager.Singleton.OnServerStarted += SpawnNetworkObjects;
            }
        }

        private void OnDestroy()
        {
            NetworkManager.Singleton.OnServerStarted -= SpawnNetworkObjects;
        }

        private void SpawnNetworkObjects()
        {
            foreach (NetworkObjectSpawnInfo networkObjectToSpawn in NetworkObjectSpawnInfo)
            {
                NetworkObject networkObject;
                
                if (networkObjectToSpawn.parentTransform == null)
                {
                    networkObject = (NetworkObject)Instantiate(networkObjectToSpawn.networkObject, gameObject.scene);
                }
                else
                {
                    networkObject = Instantiate(networkObjectToSpawn.networkObject, networkObjectToSpawn.parentTransform);
                }
                
                networkObject.Spawn();
            }

            Destroy(gameObject);
        }
    }
}
