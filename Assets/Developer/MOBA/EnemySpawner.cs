using Team3.Enemys.Common;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using Team3.Enemys;
using System.Collections.Generic;
using System.Collections;

namespace Team3.MOBA
{
    public class EnemySpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject enemy;
        [SerializeField] private float spawnRadius;
        [SerializeField] private List<Waypoint> waypoints;
        [SerializeField] private int enemysPerWave;
        [SerializeField] private float timeBetweenEnemySpawns;

        [SerializeField] private int timeBetweenWaves = 10;
        [SerializeField] private int timeAfterLastDied = 10;
        [SerializeField] private float timeWhenLastDied = 0;
        [SerializeField] private bool spawnOnlyAfterClear = true;
        [SerializeField] private bool isMinions = false;
        [SerializeField] private string SpawnerGroupName;
        [SerializeField] private GameObject PerkBubble;
        [SerializeField] private bool canSpawnPerk = false;


        int spawnCount = 0;

        public override void OnNetworkSpawn()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                base.OnNetworkSpawn();
                EnemyStats.OnDeathToSpawner += EnemyDied;
            }

        }

        public void Start()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                if (isMinions)
                {
                    var waypointGroup = GameObject.Find(SpawnerGroupName);
                    waypoints.AddRange(waypointGroup.GetComponentsInChildren<Waypoint>());
                }
                WaveSpawnTest();
            }


        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            EnemyStats.OnDeathToSpawner -= EnemyDied;
        }
        public override void OnDestroy()
        {
            EnemyStats.OnDeathToSpawner -= EnemyDied;
        }

        public void SpawnEnemyAtLocation(Vector3 spawnPos)
        {   

            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPos, out hit, 3f, NavMesh.AllAreas))
            {
                var newEnemy = (NetworkObject)Instantiate(enemy, gameObject.scene);
                newEnemy.GetComponent<NavMeshAgent>().Warp(hit.position);

                newEnemy.Spawn();
                if (isMinions)
                    newEnemy.GetComponent<PatrolState>().waypoints = waypoints;
                newEnemy.GetComponent<EnemyStats>().SpawnerID = gameObject.GetInstanceID();
            }
        }

        [ContextMenu(itemName: "Spawn a Wave")]
        public void WaveSpawnTest()
        {
            StartCoroutine(WaveSpawn());
        }

        private IEnumerator WaveSpawn()
        {
            spawnCount = enemysPerWave;

            for (int i = 0; i < enemysPerWave; i++)
            {
                var spawnPoint = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));


                SpawnEnemyAtLocation(spawnPoint);
                yield return new WaitForSeconds(timeBetweenEnemySpawns);
            }

            if (!spawnOnlyAfterClear)
            {
                Invoke(nameof(WaveSpawnTest), timeBetweenWaves);
            }
        }


        public void EnemyDied(int ID)
        {
            if (ID == gameObject.GetInstanceID())
            {
                spawnCount--;
                if (spawnCount <= 0)
                {
                    if (canSpawnPerk) { 
                        var perk = (GameObject)Instantiate(PerkBubble, gameObject.scene);
                        perk.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                        perk.GetComponent<NetworkObject>().Spawn(true);
                    }

                    if (spawnOnlyAfterClear)
                    {
                        Invoke(nameof(WaveSpawnTest), timeAfterLastDied);
                    }
                }
            }
        }
    }
}