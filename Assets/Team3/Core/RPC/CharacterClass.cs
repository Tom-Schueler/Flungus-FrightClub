using Unity.Netcode;
using UnityEngine;

namespace Team3.Characters
{
    public class CharacterClass : NetworkBehaviour
    {

        public NetworkVariable<LoadoutData> loadout =
            new(default,
                NetworkVariableReadPermission.Everyone,
                NetworkVariableWritePermission.Server);





        public override void OnNetworkSpawn()
        {
            loadout.OnValueChanged += (_, newVal) => ApplyLoadout(newVal);
            ApplyLoadout(loadout.Value);          // also works for late-joiners
        }



        private void ApplyLoadout(LoadoutData data)
        {
            SOWeapon_Object wp = AssetDB.Weapons[data.weaponId];
            SOAbility ab1 = AssetDB.Abilities[data.ability1Id];
            SOAbility ab2 = AssetDB.Abilities[data.ability2Id];

            CharacterClasses cls = data.characterClass;


            // Use class to trigger class-specific effects or visuals
            switch (cls)
            {
                
                case CharacterClasses.VampireHunter:
                    break;
                case CharacterClasses.Monk:
                    break;
                case CharacterClasses.Fortuneteller:
                    break;
                case CharacterClasses.Exorcist:
                    break;
                case CharacterClasses.Priest:
                    break;
                case CharacterClasses.Survivor:
                    break;
                case CharacterClasses.GhostHunter:
                    break;
                case CharacterClasses.Gamer:
                    break;
                default:
                    Debug.Log("Class not Found");
                    break;
            }
        }
    }
}

