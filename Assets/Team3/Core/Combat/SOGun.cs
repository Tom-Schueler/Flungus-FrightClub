using Team3.CustomStructs;
using UnityEngine;

namespace Team3.Combat
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Team3/Guns/Gun")]
    public class SOGun : ScriptableObject
    {

        [SerializeField]
        private string weaponName;
        [SerializeField]
        private int id = -1;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private Texture2D artwork;

        [SerializeField]
        private int bulletsPerClick;

        [SerializeField]
        private float scaleDamage = 1;
        [SerializeField]
        private bool splitBulletTriggersPerk;

        [SerializeField]
        private int magazineSize;

        [SerializeField]
        private float spread;

        [SerializeField]
        private float fireRate;

        [SerializeField]
        private float timeBetweenBullets;

        [SerializeField]
        private float reloadTime;

        [SerializeField]
        private GameObject gunAsset;

        [SerializeField]
        private AnimationClip reloadAnimation;

        [SerializeField]
        private AnimationClip shootAnimation;

        [SerializeField]
        private SOCombatCards characterCard;

        [SerializeField]
        private float attackDamageScaling;

        [SerializeField]
        private GameObject shootFx;

        [SerializeField]
        private float projectileForce;

        [SerializeField]
        private float projectileLifetime;

        [SerializeField] 
        private float projectileSize;

        [SerializeField]
        private float projectileGravity;

        [SerializeField]
        private GameObject projectileTrailFXAsset;

        [SerializeField]
        private GameObject projectileImpactFXAsset;

        [SerializeField]
        private GameObject projectileMesh;

        //################# GETTING ###################

        public string WeaponName => weaponName;

        public int ID => id;
        public float ScaleDamage => scaleDamage;
        public int BulletsPerClick => bulletsPerClick;
        public int MagazineSize => magazineSize;
        public float Spread => spread;
        public float FireRate => fireRate;
        public float ReloadTime => reloadTime;
        public float TimeBetweenBullets => timeBetweenBullets;
        public bool SplitBulletTriggersPerk => splitBulletTriggersPerk;
        public GameObject GunAsset => gunAsset;
        public Sprite Icon => icon;
        public AnimationClip ShootAnimation => shootAnimation;
        public AnimationClip ReloadAnimation => reloadAnimation;
        public SOCombatCards CharacterCard => characterCard;
        public Texture2D Artwork => artwork;
        public GameObject ShootFX => shootFx;
        public float ProjectileForce => projectileForce;
        public  float ProjectileLifetime => projectileLifetime;
        public float ProjectileGravity => projectileGravity;
        public GameObject ProjectileTrailFXAsset => projectileTrailFXAsset;
        public GameObject ProjectileImpactFXAsset => projectileImpactFXAsset;
        public GameObject ProjectileMesh => projectileMesh;
        public float ProjectileSize => projectileSize;
        public float AttackDamageScaling => attackDamageScaling;
    }
}