using System.Collections.Generic;
using Team3.Weapons;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Game/Ability")]
public class SOAbility : ScriptableObject
{
    public ushort abilityId;     // unique key
    public string displayName;
    public float cooldown;
    public float abilityScaling;
    public float baseDamage;
    public Sprite icon;
    public GameObject activationFX;
}