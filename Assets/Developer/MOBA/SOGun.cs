using Team3.CustomStructs;
using UnityEngine;
using Team3.Combat;
using Unity.Cinemachine;

namespace Team3.MOBA
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Team3/MOBA/Gun")]
    public class SOGun : ScriptableObject
    {


        [Space(10), Header("Basic Information")]
        [SerializeField]    private string weaponName;
        [SerializeField]    private int id = -1;
        [SerializeField]    private Sprite icon;


        [Space(10), Header("Technical Data")]
        [SerializeField]    private float damage ;
        [SerializeField]    private int magazineSize;
        [SerializeField]    private float spread;
        [SerializeField]    private float fireRate;
        [SerializeField]    private float reloadTime;
        [SerializeField]    private float attackDamageScaling = 1;

        [Space(10), Header("Burst Fire")]
        [SerializeField]    private int bulletsPerClick;
        [SerializeField]    private float timeBetweenBullets;


        [Space(10), Header("Engine")]
        [SerializeField]    private Transform bulletSpawn;
        [SerializeField]    private Animator animator;


        [Space(10), Header("UX")]
        [SerializeField]    private Vector3 recoil;
        [SerializeField]    private float snappiness;
        [SerializeField]    private float returnSpeed;
        [SerializeField]    private AnimationCurve zoom;
        [SerializeField]    private NoiseSettings cmHeadBob;
        [SerializeField]    private SOVFX trailFx;
        [SerializeField]    private SOVFX impactFx;
        [SerializeField]    private SOVFX muzzleFlashFx;
     

        //################# GETTING ###################

        public string WeaponName => weaponName;
        public int ID => id;
        public Sprite Icon => icon;


        public float Damage => damage;
        public int MagazineSize => magazineSize;
        public float Spread => spread;
        public float FireRate => fireRate;
        public float ReloadTime => reloadTime;
        public float AttackDamageScaling => attackDamageScaling;


        public int BulletsPerClick => bulletsPerClick;
        public float TimeBetweenBullets => timeBetweenBullets;


        public Transform BulletSpawn => bulletSpawn;
        public Animator Animator => animator;


        public Vector3 Recoil => recoil;
        public float Snappiness => snappiness;
        public float ReturnSpeed => returnSpeed;
        public AnimationCurve Zoom => zoom;
        public NoiseSettings CMHeadBob => cmHeadBob;
        public SOVFX TrailFx => trailFx;
        public SOVFX ImpactFx => impactFx;
        public SOVFX MuzzleFlashFx => muzzleFlashFx;
        
    }
}