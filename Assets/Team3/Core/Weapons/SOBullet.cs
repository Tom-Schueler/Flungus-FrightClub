using System.Collections.Generic;
using UnityEngine;
using Team3.CustomStructs;
namespace Team3.Weapons {
    [CreateAssetMenu(fileName = "Bullet", menuName = "Team3/Weapons/Bullet")]
    public class SOBullet : ScriptableObject
{
        [SerializeField]
        private List<DamageTypeValue> damageTypeFlat = new List<DamageTypeValue>();

        [SerializeField]
        private BulletType type;

        [SerializeField]
        private float force;

        [SerializeField]
        private float gravityMultiplier;

        [SerializeField]
        private float lifeTime;

        [SerializeField]
        private bool isDestroyedOnImpact;

        [SerializeField]
        private GameObject trailFX;
        
        [SerializeField]
        private GameObject impactFX;

        [SerializeField]
        private GameObject spawnFX;

        [SerializeField]
        private GameObject projectileMesh;

        [SerializeField]
        private float collisionRadius;


        public BulletType Type => type;
        public List<DamageTypeValue> DamageTypeFlat => damageTypeFlat;
        public float GravityMultiplier => gravityMultiplier;
        public float Force => force;
        public float LifeTime => lifeTime;
        public bool IsDestroyedOnImpact => isDestroyedOnImpact;
        public GameObject TrailFx => trailFX;
        public GameObject ImpactFX => impactFX;
        public GameObject SpawnFX => spawnFX;
        public GameObject ProjectileMesh => projectileMesh;
        public float CollisionRadius => collisionRadius;    

    }
}