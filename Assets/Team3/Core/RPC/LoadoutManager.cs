using Team3.Characters;
using Unity.Netcode;

public class LoadoutManager : NetworkBehaviour
{
    public void ServerAssignLoadout(ulong clientId,
                                    CharacterClasses cls,
                                    SOWeapon_Object wp,
                                    SOAbility a1,
                                    SOAbility a2)
    {
        var player = NetworkManager.Singleton
                     .ConnectedClients[clientId]
                     .PlayerObject.GetComponent<CharacterClass>();

        player.loadout.Value = new LoadoutData
        {
            characterClass = cls,
            weaponId = wp.weaponId,
            ability1Id = a1.abilityId,
            ability2Id = a2.abilityId
        };
    }
}