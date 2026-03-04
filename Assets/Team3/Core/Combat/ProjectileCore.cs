using System.Collections.Generic;
using Team3.Characters;
using Team3.Combat;
using Team3.Weapons;
using Unity.Netcode;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ProjectileCore : NetworkBehaviour
{
    //public List<SOProjectilePerk> OnShootPerks = new List<SOProjectilePerk>();


    public List<SOProjectilePerk> OnHitPerks = new List<SOProjectilePerk>();
    public List<SOProjectilePerk> OnImpactPerks = new List<SOProjectilePerk>();
    public List<SOProjectilePerk> EnemyDeathPerks = new List<SOProjectilePerk>();
    public List<SOProjectilePerk> EnemyDebuffPerks = new List<SOProjectilePerk>();



    //Special Conditions
    //public int numberOfBounces = 0;
    //public bool destroyedOnImpact = true;


    public ulong ownerID;
    public DamageType Type;
    public DamageType Affix;
    public float TotalDamage;
    public float AttackDamageScaling;
    public bool SplitDamagePerBullet;
    public int NumberOfBullets;
    public bool TriggerPerks;

 

    public ProjectileMovement ProjectileMovement;



    //public float Force;
    //public float ProjectileGravity;
    //public float ProjectileLifetime;
    //public float ProjectileSize;
    //public Rigidbody rb;


    public void OnEnable()
    {
        ProjectileMovement = GetComponent<ProjectileMovement>();
    }

    public void Update()
    {
      
    }
    public void OnTriggerEnter(Collider other)
    {
        
        bool otherPlayer = false;
       

        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<NetworkObject>().OwnerClientId != ownerID)
            {
                otherPlayer = true;
            }
        }
            
            
        if (other.CompareTag("Enemy") || otherPlayer)
        {
          
            if (other.gameObject.TryGetComponent<CharacterStats>(out CharacterStats stats))
            {
                stats.TakeDamage(TotalDamage, Affix);
            }
            
            foreach (var perk in OnHitPerks)
            {
                if (!TriggerPerks) break;
                perk.TriggerPerk(this);
            }

            foreach(var perk in EnemyDeathPerks) { 
            if(other.gameObject.TryGetComponent<EnemyPerkHandler>(out EnemyPerkHandler enemyPerkHandler))
                {
                enemyPerkHandler.ApplyDeathPerkEffects(perk);
            }
            }
        }

        else
        {
            
            foreach (var perk in OnImpactPerks)
            {
                if (!TriggerPerks) break;
                perk.TriggerPerk(this);
            }
                
        }
    }

}
