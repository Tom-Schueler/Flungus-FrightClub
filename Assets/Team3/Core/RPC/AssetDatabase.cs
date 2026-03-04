using System.Collections.Generic;
using UnityEngine;

public static class AssetDB
{
    public static readonly Dictionary<ushort, SOWeapon_Object> Weapons = new();
    public static readonly Dictionary<ushort, SOAbility> Abilities = new();

    // Auto-populate once, before any scene runs
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BuildDB()
    {
        foreach (var w in Resources.LoadAll<SOWeapon_Object>(""))
            Weapons[w.weaponId] = w;

        foreach (var a in Resources.LoadAll<SOAbility>(""))
            Abilities[a.abilityId] = a;
    }
}
