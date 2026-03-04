using UnityEngine;
using TMPro;
using Team3.Characters;
using Team3.Combat;
using static System.Net.WebRequestMethods;
using UnityEngine.UI;
using Team3.Weapons;
public class PlayerDebugTexter : MonoBehaviour

{
    [SerializeField]
    TMP_Text attackdamage;
    [SerializeField]
    TMP_Text abilitydamage;

    [SerializeField]
    TMP_Text none;
    [SerializeField]
    TMP_Text salt;
    [SerializeField]
    TMP_Text silver;
    [SerializeField]
    TMP_Text runes;
    [SerializeField]
    TMP_Text holy;
    [SerializeField]
    TMP_Text fire;
    [SerializeField]
    TMP_Text water;
    [SerializeField]
    TMP_Text ice;

    [SerializeField]
    GameObject iconbox;

    [SerializeField]
    PlayerStats playerstats;
    [SerializeField]
    CombatActionHandler combbatactionhandler;

    [SerializeField]
    GameObject iconprefab;


    string sattackdamage = "current attack damage ";
    string sabilitydamage = "current ability power ";
    string snone = "none damage ";
    string ssalt = "salt damage ";
    string ssilver = "Silver damage ";
    string srunes = "runes damage ";
    string sholy = "holy damage ";
    string sfire = "fire damage ";
    string sice = "ice damage ";
    string swater = "water damage ";



    private void Update()
    {
        if (playerstats == null) return;
        attackdamage.SetText(sattackdamage + playerstats.BulletDamage);
        abilitydamage.SetText(sabilitydamage + playerstats.SkillDamage);

        foreach(var stat in playerstats.currentBulletDamagePerType) { 
         if(stat.type == DamageType.None) none.SetText(snone + stat.value);
         if(stat.type == DamageType.Fire) fire.SetText(sfire + stat.value);
         if(stat.type == DamageType.Ice) ice.SetText(sice + stat.value);
         if(stat.type == DamageType.Water) water.SetText(swater + stat.value);
        }
    }

    private void Start()
    {
        playerstats.NewPerkAdded += AddPerkIcon;
    }
    private void OnDestroy()
    {
        playerstats.NewPerkAdded -= AddPerkIcon;

    }

    private void AddPerkIcon(Sprite icon, ulong ownerID)
    {
        var newIcon = Instantiate(iconprefab, iconbox.transform);
        newIcon.GetComponent<Image>().sprite = icon;
    }
}
