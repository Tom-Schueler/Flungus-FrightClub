using Team3.Characters;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character Preset")]
public class SOCharacterPresets : ScriptableObject
{
    public string displayName;
    public CharacterClasses characterClass;
    public SOWeapon_Object weapon;
    public SOAbility ability1;
    public SOAbility ability2;

    public Sprite icon; // For UI selection preview
}