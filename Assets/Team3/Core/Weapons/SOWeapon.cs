using UnityEngine;

namespace Team3.Weapons
{
    [CreateAssetMenu(fileName = "Weapon",menuName = "Team3/Weapons/Weapon")]
    public class SOWeapon : ScriptableObject
    {

        [SerializeField]
        private string weaponName;

        [SerializeField]
        private int bulletsPerClick;
        [SerializeField]
        private int splitDamageBetweenBullets;

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
        private bool holdDownToShoot;

        [SerializeField]
        private GameObject asset;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private AnimationClip reloadAnimation;

        [SerializeField]
        private AnimationClip shootAnimation;

        [SerializeField]
        private DamageType type;
        [SerializeField]
        private DamageType affix;

        [SerializeField]
        private float baseDamage;


        public string WeaponName => weaponName;
        public int SplitDamageBetweenBullets => splitDamageBetweenBullets;
        public int BulletsPerClick => bulletsPerClick;
        public int MagazineSize => magazineSize;
        public float Spread => spread;
        public float FireRate => fireRate;
        public float ReloadTime => reloadTime;
        public bool HoldDownToShoot => holdDownToShoot;

        public float TimeBetweenBullets => timeBetweenBullets;

        public GameObject Asset => asset;
        public Sprite Icon => icon;
        public AnimationClip ShootAnimation => shootAnimation;
        public AnimationClip ReloadAnimation => reloadAnimation;


        
        public DamageType Type => type;
        public DamageType Affix => affix;
        public float BaseDamage => baseDamage;
    }
}   