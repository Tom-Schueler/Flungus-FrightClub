using System.Collections.Generic;
using Team3.CustomStructs;
using Team3.Weapons;
using UnityEngine;

namespace Team3.Combat
{
    [CreateAssetMenu(fileName = "Card", menuName = "Team3/Combat/Card")]
    public class SOCombatCards : ScriptableObject
    {
        //Card Name / Title

        [SerializeField]
        private string cardName;
        [SerializeField]
        private int id = -1;

        //Visuals

        [SerializeField]
        private Sprite image;
        [SerializeField]
        private Sprite icon;


        //Card Type and Rarity
        [SerializeField]
        private CardAlignment alignment;
        [SerializeField]
        private PerkRarity rarity;
        [SerializeField]
        private bool isUnique;
        [SerializeField]
        private PerkGroup perkGroup;


        //Change Bullet / Ability Type
        [SerializeField] private DamageType newBulletType;
       
        [SerializeField] private DamageType newMainSkillType;



        //Add Type Damage and Affix Damage
        //Skill Damage, SKill Percentage
        [SerializeField]
        private List<DamageTypeValue> attackTypeDamage = new List<DamageTypeValue>();
        [SerializeField]
        private List<DamageTypeValue> attackTypeDamageBuff = new List<DamageTypeValue>();
        [SerializeField]
        private List<DamageTypeValue> skillTypeDamage = new List<DamageTypeValue>();
        [SerializeField]
        private List<DamageTypeValue> skillTypeDamageBuff = new List<DamageTypeValue>();


        //Coole Perks für die Coolen Kugeln

        [SerializeField]
        private List<SOProjectilePerk> onHitPerks = new List<SOProjectilePerk>();
        [SerializeField]
        private List<SOProjectilePerk> onImpactPerks = new List<SOProjectilePerk>();
        [SerializeField]
        private List<SOProjectilePerk> onShootPerks = new List<SOProjectilePerk>();
        [SerializeField]
        private List<SOProjectilePerk> enemyDeathPerks = new List<SOProjectilePerk>();
        [SerializeField]
        private List<SOProjectilePerk> enemyDebuffPerks = new List<SOProjectilePerk>();
        [SerializeField]
        private List<SOProjectilePerk> onBulletDestroy = new List<SOProjectilePerk>();
  
        //Character Buffs

        [SerializeField]
        private List<StatTypeBoost> characterStatBoost = new List<StatTypeBoost>();

        public string CardName => cardName;
        public int ID => id;
        public Sprite Image => image;
        public Sprite Icon => icon;
        public PerkRarity Rarity => rarity;
        public bool IsUnique => isUnique;
        public DamageType NewBulletType => newBulletType;
        public DamageType NewMainSkillType => newMainSkillType;
        public PerkGroup PerkGroup => perkGroup;

        public CardAlignment Alignment => alignment;


        public List<DamageTypeValue> SkillTypeDamage => skillTypeDamage;
        public List<DamageTypeValue> SkillTypeDamageBuff => skillTypeDamageBuff;
        public List<StatTypeBoost> CharacterStatBoost => characterStatBoost;



        public List<SOProjectilePerk> OnHitPerks => onHitPerks;
        public List<SOProjectilePerk> OnImpactPerks => onImpactPerks;
        public List<SOProjectilePerk> OnShootPerks => onShootPerks;
        public List<SOProjectilePerk> EnemyDeathPerks => enemyDeathPerks;
        public List<SOProjectilePerk> EnemyDebuffPerks => enemyDebuffPerks;
        public List<SOProjectilePerk> OnBulletDestroy => onBulletDestroy;
        public List<DamageTypeValue> AttackTypeDamage => attackTypeDamage;
        public List<DamageTypeValue> AttackTypeDamageBuff => attackTypeDamageBuff;

    }
}