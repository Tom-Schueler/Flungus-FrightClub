using Team3.Weapons;
using UnityEngine;
using System.Collections.Generic;
using Team3.Characters;

public abstract class SOWeapon_Object : ScriptableObject
{
    public ushort weaponId;      // unique key (0-65535)
    public string displayName;
    public float damageScaling;
    public float baseDamage;
    public float force;
    public GameObject defaultBullet;
    public List<SOBulletPerk> bulletPerkList;
    public GameObject templateBullet;
    public Sprite icon;
    public AnimationClip fire;
    public AnimationClip reload;
    public GameObject activationFX;


    public abstract void Activate(CharacterClass user, Transform spawnPoint);
}