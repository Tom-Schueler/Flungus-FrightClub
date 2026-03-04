using UnityEngine;
using Unity.Mathematics;
using Unity.Netcode;
using static UnityEngine.InputSystem.InputAction;
using Team3.Characters;
using Team3.Weapons;

namespace Team3.Skills
{
    [CreateAssetMenu(fileName = "Riccochet", menuName = "Team3/Abilities/Riccochet")]
    public class Skill_Riccochet : Skill
    {
        [SerializeField]
        private float initialAngleOffset = 45f;
        [SerializeField]
        private GameObject riccochetProjectile;

        public override void ActivateSkill(SkillContext skillContext, PlayerStats char_Stats, float damage)
        {
            if (skillContext.SpawnPoint == null) return;

            Ray ray = new Ray(skillContext.PlayerCamera.transform.position, skillContext.PlayerCamera.transform.forward);
            if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, 100f, LayerMask.GetMask("Enemy")))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Vector3 spawnPos = skillContext.SpawnPoint.position;
                    Vector3 launchDir = Quaternion.AngleAxis(initialAngleOffset, Vector3.up) * skillContext.PlayerCamera.transform.forward;
                   
                    ulong targetId = hit.collider.GetComponent<NetworkObject>().NetworkObjectId;

                    Debug.Log(targetId);
                    
                    //skillContext.SkillBookOwner.SpawnRiccochetProjectileServerRpc(
                    //    spawnPos, launchDir, targetId,
                    //    damage,
                    //    (int)char_Stats.currentMainSkillType,
                    //    (int)char_Stats.currentMainSkillAffix
                    //);
                }
            }
        }
    }



    /*
    Ray ray = new Ray(skillContext.PlayerCamera.transform.position, skillContext.PlayerCamera.transform.forward);
    float radius = 0.5f;
    float maxDistance = 100f;
    RaycastHit hit;



    if (Physics.SphereCast(ray, radius, out hit, maxDistance, LayerMask.GetMask("Enemy")))
    {



        if (hit.collider.CompareTag("Enemy"))
        {
            Vector3 spawnPos = ProjectileSpawn.position;
            Quaternion rotation = Quaternion.LookRotation(ProjectileSpawn.forward);



            GameObject instance = Instantiate(riccochetProjectile, spawnPos, rotation);
            RiccochetProjectile ricco = instance.GetComponent<RiccochetProjectile>();
            ricco.damage = damage;
            ricco.type = char_Stats.currentMainSkillType;
            ricco.affix = char_Stats.currentMainSkillAffix;
            //NetworkObject netObj = instance.GetComponent<NetworkObject>();
           // netObj.Spawn();

            Vector3 launchDirection = Quaternion.AngleAxis(initialAngleOffset, Vector3.up) * skillContext.PlayerCamera.transform.forward;

            var riccochetScript = instance.GetComponent<RiccochetProjectile>();
            riccochetScript.AddTarget(hit.collider.gameObject);
            riccochetScript.Initialize(launchDirection, hit.collider.transform);

        }
    }
} */


}

