using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using Team3.Characters;

public class EnemyTurret : NetworkBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator animator;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed = 3;
    [SerializeField] private List<ulong> players = new();
    [SerializeField] private float turnSpeed;

    [SerializeField] private Transform attackSpawn;

    private float lastAttack;

    public override void OnNetworkSpawn()
    {
        PlayerStats.OnPlayerDeath += PlayerDeath;
    }

    public override void OnNetworkDespawn()
    {
        PlayerStats.OnPlayerDeath -= PlayerDeath;
    }

    public void PlayerDeath(ulong playerID)
    {
        players.Remove(playerID);
    }

    private void Start()
    {
        players = new List<ulong>();
    }
    private void Update()
    {
        if (players.Count > 0)
        {
            var dir = (PlayerRegistry.GetStats(players[0]).gameObject.transform.position - attackSpawn.position).normalized;
            attackSpawn.forward = dir;

            transform.forward = Vector3.Slerp(transform.forward, new Vector3(dir.x, transform.forward.y, dir.z).normalized, turnSpeed * Time.deltaTime);
            

            if ((Time.time - lastAttack) > attackSpeed)
            {
                lastAttack = Time.time;
                Attack();
            }


        }
    }

    public void TriggerEnter(Collider other)
    {

        if (other.TryGetComponent<NetworkObject>(out var no))
        {
            players.Add(no.OwnerClientId);
        }

    }

    public void TriggerExit(Collider other)
    {

        if (other.TryGetComponent<NetworkObject>(out var no))
        {
            players.Remove(no.OwnerClientId);
        }
    }

    public void StartAttack()
    {
        SpawnClientProjectileClientRpc(attackSpawn.position, attackSpawn.forward);
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
        
    }



    [ClientRpc]
    public void SpawnClientProjectileClientRpc(Vector3 attackSpawn, Vector3 dir)
    {
        Instantiate(projectile, attackSpawn, Quaternion.LookRotation(dir));
    }

}
