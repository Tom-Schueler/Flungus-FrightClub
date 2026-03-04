using UnityEngine;
using System.Collections.Generic;
using Team3.Combat;
using Team3.MOBA;

[CreateAssetMenu(fileName = "PerkDatabase", menuName = "Team3/Databases/PerkDatabase")]
public class PerkDatabase : ScriptableObject
{
    public List<SOProjectilePerk> AllPerks;
    public List<SOCombatCards> AllCombatCards;
    public List<Team3.Combat.SOGun> AllGuns;
    public List<SOVFX> AllVFXs;
    public List<SOSFX> AllSFXs;
    public List<SOWeapon> AllWeapons;



    private static PerkDatabase _instance;
    public static PerkDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                // Load from Resources (recommended for runtime singleton SOs)
                _instance = Resources.Load<PerkDatabase>("PerkDatabase");
                if (_instance == null)
                    Debug.LogError("PerkDatabase not found in Resources folder!");
            }
            return _instance;
        }
    }

    private Dictionary<int, SOProjectilePerk> _perkLookup;
    private Dictionary<int, SOCombatCards> _CombatCardsLookup;
    private Dictionary<int, Team3.Combat.SOGun> _GunLookup;
    private Dictionary<int, SOVFX> _VFXLookup;
    private Dictionary<int, SOSFX> _SFXLookup;
    private Dictionary<int, SOWeapon> _WeaponLookup;

    public void Initialize()
    {
        _perkLookup = new Dictionary<int, SOProjectilePerk>();
        foreach (var perk in AllPerks)
        {
            if (perk != null && perk.ID != -1)
                _perkLookup[perk.ID] = perk;
        }

        _CombatCardsLookup = new Dictionary<int, SOCombatCards>();
        foreach (var card in AllCombatCards)
        {
            if (card != null && card.ID != -1)
                _CombatCardsLookup[card.ID] = card;
        }


        _GunLookup = new Dictionary<int, Team3.Combat.SOGun>();
        foreach (var gun in AllGuns)
        {
            if (gun != null && gun.ID != -1)
                _GunLookup[gun.ID] = gun;
        }

        _VFXLookup = new Dictionary<int, SOVFX>();
        foreach (var vfx in AllVFXs)
        {
            if (vfx != null && vfx.ID != -1)
                _VFXLookup[vfx.ID] = vfx;
        }

        _SFXLookup = new Dictionary<int, SOSFX>();
        foreach (var sfx in AllSFXs)
        {
            if (sfx != null && sfx.ID != -1)
                _SFXLookup[sfx.ID] = sfx;
        }

        _WeaponLookup = new Dictionary<int, SOWeapon>();
        foreach (var weapon in AllWeapons)
        {
            if (weapon != null && weapon.ID != -1)
                _WeaponLookup[weapon.ID] = weapon;
        }
    }

    public SOProjectilePerk GetPerkByID(int id)
    {
        if (_perkLookup == null)
            Initialize();

        return _perkLookup.TryGetValue(id, out var perk) ? perk : null;
    }

    public SOCombatCards GetCardByID(int id)
    {
        if (_CombatCardsLookup == null)
            Initialize();

        return _CombatCardsLookup.TryGetValue(id, out var card) ? card : null;
    }


    public Team3.Combat.SOGun GetGunByID(int id)
    {
        if (_GunLookup == null)
            Initialize();

        return _GunLookup.TryGetValue(id, out var gun) ? gun : null;
    }


    public SOVFX GetVFXByID(int id)
    {
        if (_VFXLookup == null)
            Initialize();

        return _VFXLookup.TryGetValue(id, out var vfx) ? vfx : null;
    }

    public SOSFX GetSFXByID(int id)
    {
        if (_SFXLookup == null)
            Initialize();

        return _SFXLookup.TryGetValue(id, out var sfx) ? sfx : null;
    }

    public SOWeapon GetWeaponByID(int id)
    {
        if (_WeaponLookup == null)
            Initialize();

        return _WeaponLookup.TryGetValue(id, out var weapon) ? weapon : null;
    }
}