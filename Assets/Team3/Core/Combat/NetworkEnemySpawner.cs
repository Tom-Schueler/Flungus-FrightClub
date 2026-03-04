using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkEnemySpawner : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3[] spawnPositions;
    public void Start()
    {
        this.GetComponent<NetworkObject>().Spawn();
    }
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        foreach (var pos in spawnPositions)
        {
            var enemy = Instantiate(enemyPrefab, gameObject.scene);
            enemy.GetComponent<Transform>().position = pos;
            enemy.GetComponent<NetworkObject>().Spawn();
        }
    }
}