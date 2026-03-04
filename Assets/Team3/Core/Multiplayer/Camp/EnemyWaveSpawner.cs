using System.Collections;
using System.Collections.Generic;
using Team3.Enemys;
using UnityEngine;

namespace Team3.Multiplayer.EnemyCamp
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyWave[] waves;
        [SerializeField] private float spawnRadius;
        [SerializeField] private float groundCheckLength;
        [SerializeField] private LayerMask groundLayer;

        private bool isFinished;
        private List<NetworkEnemy> currentEnemys = new List<NetworkEnemy>();

        public float SpawnRadius => spawnRadius;
        public float GroundCheckLength => groundCheckLength;
        public bool IsFinished => isFinished;


        public IEnumerator SpawnWaves()
        {
            isFinished = false;

            foreach (EnemyWave wave in waves)
            {
                currentEnemys.Clear();

                foreach (EnemyInfo enemyInfo in wave.Enemys)
                {
                    Spawn(enemyInfo, out NetworkEnemy[] enemys);
                    currentEnemys.AddRange(enemys);
                }

                yield return AwaitWaveCleard(currentEnemys);
            }

            isFinished = true;
        }

        public void Reset()
        {
            foreach (NetworkEnemy enemy in currentEnemys)
            {
                enemy.Despawn();
                enemy.OnDeath -= OnEnemyDied;
            }
        }

        private IEnumerator AwaitWaveCleard(List<NetworkEnemy> enemys)
        {
            foreach (NetworkEnemy enemy in enemys)
            {
                enemy.OnDeath += OnEnemyDied;
            }

            yield return new WaitUntil(() => currentEnemys.Count == 0);
        }

        private void OnEnemyDied(NetworkEnemy enemy)
        {
            enemy.OnDeath -= OnEnemyDied;
            currentEnemys.Remove(enemy);
        }

        private void Spawn(EnemyInfo enemyInfo, out NetworkEnemy[] spawnedEnemys)
        {
            List<NetworkEnemy> objects = new List<NetworkEnemy>();

            for (int i = 0; i < enemyInfo.SpawnAmount; i++)
            {
                Vector3 spawnPosition = CalculatePosition();

                NetworkEnemy enemy = (NetworkEnemy)Instantiate(enemyInfo.Prefab, gameObject.scene);
                enemy.NetworkObject.Spawn();
                enemy.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                objects.Add(enemy);
            }

            spawnedEnemys = objects.ToArray();
        }

        private Vector3 CalculatePosition()
        {
            float radius = Random.Range(0, spawnRadius);
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            Vector2 offSet = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);

            Vector3 spawnPosition = transform.position;
            
            spawnPosition.x += offSet.x;
            spawnPosition.z += offSet.y;

            if (Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hitInfo, GroundCheckLength, groundLayer))
            {
                spawnPosition = hitInfo.point;
            }
            else
            {
                spawnPosition.y += GroundCheckLength;
            }

            return spawnPosition;
        }
    }
}
