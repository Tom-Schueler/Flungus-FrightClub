using UnityEngine;
using Team3.CustomStructs;
using System.Collections.Generic;
using Team3.Skills;
using Team3.Weapons;

namespace Team3.Perks
{
    [CreateAssetMenu(fileName = "Perk Card", menuName = "Team3/Cards/Base Card")]
    public class SOPerkBase : ScriptableObject
    {
        //Card Name / Title
        [SerializeField]
        private string cardName;

        //Visuals
        [SerializeField]
        private Texture2D image;
        [SerializeField]
        private Sprite icon;

        //Change Bullet / Ability Type
        [SerializeField] private DamageType newBulletType;
        [SerializeField] private DamageType newBulletAffix;
        [SerializeField] private DamageType newMainSkillType;
        [SerializeField] private DamageType newMainSkillAffix;
        [SerializeField] private DamageType newOffSkillType;
        [SerializeField] private DamageType newOffSkillAffix;

        //Card Type and Rarity
        [SerializeField]
        private PerkType type;
        [SerializeField]
        private PerkRarity rarity;
        [SerializeField]
        private bool isUnique;

        //Bullet Damage, Bullet Percentage
        [SerializeField]
        private List<DamageTypeValue> bulletTypeDamage = new List<DamageTypeValue>();
        [SerializeField]
        private List<DamageTypeValue> bulletTypeDamageBuff = new List<DamageTypeValue>();

        //Bullet Behaviour
        [SerializeField]
        private List<SOBulletPerk> bulletPerk = new List<SOBulletPerk>();

        //Gun Behaviour
        [SerializeField]
        private List<SOGunPerk> gunPerk = new List<SOGunPerk>();

        //Gun Buffs
        [SerializeField]
        private List<StatTypeBoost> gunStatBoost = new List<StatTypeBoost>();



        //Skill Damage, SKill Percentage
        [SerializeField]
        private List<DamageTypeValue> skillTypeDamage = new List<DamageTypeValue>();
        [SerializeField]
        private List<DamageTypeValue> skillTypeDamageBuff = new List<DamageTypeValue>();

        //Skillbook Buffs
        [SerializeField]
        private List<StatTypeBoost> skillbookStatBoost = new List<StatTypeBoost>();

        //Character Buffs
        [SerializeField]
        private List<StatTypeBoost> characterStatBoost = new List<StatTypeBoost>();

        public string CardName => cardName;
        public Texture2D Image => image;
        public Sprite Icon => icon;
        public PerkType Type => type;
        public PerkRarity Rarity => rarity;
        public bool IsUnique => isUnique;
        public DamageType NewBulletType => newBulletType;
        public DamageType NewBulletAffix => newBulletAffix;
        public DamageType NewMainSkillType => newMainSkillType;
        public DamageType NewMainSkillAffix => newMainSkillAffix;
        public DamageType NewOffSkillType => newOffSkillType;
        public DamageType NewOffSkillAffix => newOffSkillAffix;


        public List<DamageTypeValue> BulletTypeDamage => bulletTypeDamage;
        public List<DamageTypeValue> BulletTypeDamageBuff => bulletTypeDamageBuff;
        public List<SOBulletPerk> BulletPerk => bulletPerk;
        public List<SOGunPerk> GunPerk => gunPerk;
        public List<StatTypeBoost> GunStatBoost => gunStatBoost;
        public List<DamageTypeValue> SkillTypeDamage => skillTypeDamage;
        public List<DamageTypeValue> SkillTypeDamageBuff => skillTypeDamageBuff;
        public List<StatTypeBoost> SkillbookStatBoost => skillbookStatBoost;
        public List<StatTypeBoost> CharacterStatBoost => characterStatBoost;
    }
}