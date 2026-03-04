using System.Collections.Generic;
using Team3.Combat;
using Unity.Netcode;


public class EnemyPerkHandler : NetworkBehaviour
{
    public List<SOProjectilePerk> BehaviourPerk = new List<SOProjectilePerk>();
    public List<SOProjectilePerk> OnDeathPerk = new List<SOProjectilePerk>();
    public SOProjectilePerk LastPerk;


    public void ApplyDeathPerkEffects(SOProjectilePerk perk, NetworkObjectReference hitRef = default)
    {
        if (OnDeathPerk.Contains(perk)) return;

        OnDeathPerk.Add(perk);
        LastPerk = perk;
        ApplyPerkClientRpc(hitRef);
    }
    public void ApplyBehaviourPerkEffects(SOProjectilePerk perk, NetworkObjectReference hitRef)
    {
       
        BehaviourPerk.Add(perk);
        LastPerk = perk;
        ApplyPerkClientRpc(hitRef);
    }

    public void BehaviourPerkUpdate()
    {
        foreach (var perk in BehaviourPerk) 
        {
        perk.PerkUpdate();
        }
    }

    public void OnDeathPerks()
    {
        if (OnDeathPerk.Count > 0)
        {
            foreach (var perk in OnDeathPerk)
            {
                perk.TriggerPerk(null,420,transform.position,transform.forward);
            }
        }
    }

//###### SERVER STUFF ######
    [ClientRpc]
    public void ApplyPerkClientRpc(NetworkObjectReference hitRef)
    {
        LastPerk.PerkApply(transform.position,transform.forward,hitRef);
        print("ITEM APPLIED");
    }

}
