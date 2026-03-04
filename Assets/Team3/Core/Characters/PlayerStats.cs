using System.Collections.Generic;
using Team3.Combat;
using Team3.Weapons;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using System;
using Unity.Netcode;
using Team3.Enemys.Common;
using Team3.MOBA;
using Team3.Multiplayer;
using UnityEngine.AI;
using Unity.Netcode.Components;

namespace Team3.Characters
{
    public class PlayerStats : CharacterStats
    {
        [SerializeField] public GameObject bodyVisual;
        [HideInInspector] public NetworkVariable<bool> showBody = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        [SerializeField] CharacterMovement movement;
        [SerializeField] MOBA.CombatActionHandler combat;
        public Action<Sprite, ulong> NewPerkAdded;

        public static Action<ulong> OnPlayerSpawn;
        public static Action<ulong> OnPlayerDeath;

        
      

        public List<int> serverPerkIDs = new();
        public List<int> serverCardIDs = new();


        //####### List of all Cards Collected So Far #######
        public List<SOCombatCards> collectedCards = new List<SOCombatCards>();


        //####### List for each Type of Perk #######
        public List<SOProjectilePerk> OnHitPerks = new List<SOProjectilePerk>();
        public List<float> OnHitPerksTrackCooldown = new List<float>();

        public List<SOProjectilePerk> OnImpactPerks = new List<SOProjectilePerk>();
        public List<float> OnImpactPerksTrackCooldown = new List<float>();

        public List<SOProjectilePerk> OnShootPerks = new List<SOProjectilePerk>();
        public List<float> OnShootPerksTrackCooldown = new List<float>();

        public List<SOProjectilePerk> EnemyDeathPerks = new List<SOProjectilePerk>();
        public List<float> EnemyDeathPerksTrackCooldown = new List<float>();

        public List<SOProjectilePerk> EnemyDebuffPerks = new List<SOProjectilePerk>();
        public List<float> EnemyDebuffPerksTrackCooldown = new List<float>();


        public List<SOProjectilePerk> OnBulletDestroy = new List<SOProjectilePerk>();
        public List<float> OnBulletDestroyTrackCooldown = new List<float>();


        //####### Current Types and Affixes #######
        public DamageType SkillDamageType;
        public DamageType BulletDamageType;

        public List<DamageTypeValue> currentBulletDamagePerType = new List<DamageTypeValue>();
        public List<DamageTypeValue> currentSkillDamagePerType = new List<DamageTypeValue>();

        private SOCombatCards lastAddedCard;

        private float bulletDamage;
        public float BulletDamage => bulletDamage;
        public MOBA.CombatActionHandler Combat => combat;

        private float skillDamage;
        public float SkillDamage => skillDamage;

        public float accelerationModifier;
        public float decelerationModifier;

        public float FrozenGroundActive;
        public bool isOnFire = false;

        [SerializeField]
        SOVFX hurtEffect;
        [SerializeField]
        SOSFX hitSound;
        [SerializeField]
        SOSFX killSound;
        [SerializeField]
        AudioSource audioSource;
        [SerializeField]
        AudioClip clip;
        [SerializeField]
        ShowCrosshair hitmarker;
        [SerializeField]
        GameObject deathFX;

        [SerializeField]
        GameObject deathScreen;

        GameObject matchCycle;

        [SerializeField] Material transparent;
        //####### Stat Modifier #######
        public float SaltDamageModifier = 1f;
        public float movementSpeedModifier = 1f;
        public float magSizeModifier = 1f;
        public float healthModifier = 1f;
        
        
        [HideInInspector] public bool isDead = false;

        

        public override void OnNetworkSpawn()
        {
            EnemyStats.OnEnemyDeath += GetPoints;
            showBody.OnValueChanged += MakeVisable;
            

            base.OnNetworkSpawn();

            PlayerRegistry.Register(OwnerClientId, this);

            currentSpeed = originalSpeed;
            if (IsServer)
            {
                currentHealth.Value = originalHealth;
                OnPlayerSpawn?.Invoke(OwnerClientId);
            }
        }


        private void GetPoints(int player)
        {
            //Debug.LogError(player + " killer " + " Owner " + OwnerClientId + " LocalID " + NetworkManager.Singleton.LocalClientId);
            if ((ulong)player == OwnerClientId)
            {
                //Debug.Log("You " + OwnerClientId + " Earned Points " + 30);
            }
        }


        public void Start()
        {
            AudioManager.Instance.PlayTrack(clip);
        }

        public void MakeVisable(bool oldValue, bool newValue)
        {
            //bodyVisual.SetActive(newValue);
        }

        public override void OnDeath()
        {
            doStuffClientRpc();
        }

        [ClientRpc]
        void doStuffClientRpc()
        {
            Instantiate(deathFX,transform.position,Quaternion.identity);

            Debug.LogError("I DID DIE NOW " + OwnerClientId);
         
                gameObject.GetComponent<NetworkTransform>().Teleport(new Vector3(101.5f, 6f, 100f), Quaternion.identity, transform.localScale);
                // gameObject.transform.position = new Vector3(100f, 6f, 100f);
            

           
            isDead = true;
            combat.StopAttacking();
            //bodyVisual.SetActive(false);
            showBody.Value = false;
            // have it not be a ai target

            if (IsOwner)
            {
                deathScreen.GetComponent<ShowDeathIcon>().ActivateHitmarker();
                InvokeDeathServerRpc(OwnerClientId);
            }
        }
        [ServerRpc]
        private void InvokeDeathServerRpc(ulong clientId)
        {
            Debug.LogError("I DID DIE SERVER RPC " + OwnerClientId);
            OnPlayerDeath?.Invoke(clientId);
            Combat.isAllowedToShoot.Value = false;
        }


        public void Update()
        {
            
            if (FrozenGroundActive > 0)
            {
                FrozenGroundActive -= Time.deltaTime;
            }
            else
            {
                decelerationModifier = 0;
                accelerationModifier = 0;
            }


            if (fireStacks <= 0)
            {
                isOnFire = false;
            }
            else
            {
                currentHealth.Value -= fireStacks * Time.deltaTime;

                if (currentHealth.Value <= 0 && !isDead)
                {
                    doStuffClientRpc();
                    isDead = true;
                    OnDeath();
                }
            }


            //if (currentHealth.Value <= 0 && !isDead && IsOwner)
            //{
            //    isDead = true;
            //    OnDeath();

            //}

            if (IsServer)
            {
                currentHealth.Value = Mathf.Max(-1, currentHealth.Value);
            }
          
            
            //####### DEBUG FUNCTION DELETE LATER TODO #######
            //for (int i = 1; i <= 9; i++)
            //{
            //    if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha0 + i))
            //    {
            //        debugPerkCall(i);
            //    }
            //}
        }

        public void debugPerkCall(int i)
        {
            //switch (i)
            //{
            //    case 1:
            //        AddPerk(104);
            //        break;
            //    case 2:
            //        AddPerk(105);
            //        break;
            //    case 3:
            //        AddPerk(103);
            //        break;
            //    case 4:
            //        AddPerk(107);
            //        break;
            //    case 5:
            //        AddPerk(108);
            //        break;
            //    case 6:
            //        AddPerk(101);
            //        break;
            //    case 7:
            //        AddPerk(20);
            //        break;
            //    case 8:
            //        AddPerk(112);
            //        break;
            //    case 9:
            //        AddPerk(106);
            //        break;

            //    default:
            //        break;
            //}
        }

        public void debugCall(CallbackContext context)
        {
            //if (context.performed)
            //{
            //    if (fliflop)
            //    {
            //        //int num = UnityEngine.Random.Range(0, PerkDatabase.Instance.AllCombatCards.Count);
            //        AddPerk(104);

            //        fliflop = !fliflop;
            //    }
            //    else
            //    {
            //        int num = UnityEngine.Random.Range(0, PerkDatabase.Instance.AllCombatCards.Count);
            //        AddPerk(PerkDatabase.Instance.AllCombatCards[num].ID);

            //        fliflop = !fliflop;
            //    }
            //}
        }

        public void FrozenGround()
        {
            FrozenGroundActive = 0.5f;
            decelerationModifier = 50;
            accelerationModifier = 30;
        }

        public void FireGround()
        {
            if (!isOnFire)
            {
                isOnFire = true;
                ApplyFire(5, 10);
            }
        }


        public void SetCollectedPerks()
        {
            if (collectedCards != null)
            {
                OnHitPerks = new List<SOProjectilePerk>();
                OnHitPerksTrackCooldown = new();

                OnImpactPerks = new List<SOProjectilePerk>();
                OnImpactPerksTrackCooldown = new();

                OnShootPerks = new List<SOProjectilePerk>();
                OnShootPerksTrackCooldown = new();

                EnemyDeathPerks = new List<SOProjectilePerk>();
                EnemyDeathPerksTrackCooldown = new();

                EnemyDebuffPerks = new List<SOProjectilePerk>();
                EnemyDebuffPerksTrackCooldown = new();

                OnBulletDestroy = new List<SOProjectilePerk>();
                OnBulletDestroyTrackCooldown = new();

                foreach (var card in collectedCards)
                {
                    if (card.OnHitPerks.Count > 0)
                    {
                        foreach (var entry in card.OnHitPerks)
                        {
                            OnHitPerks.Add(entry);
                            OnHitPerksTrackCooldown.Add(0);
                        }
                    }

                    if (card.OnImpactPerks.Count > 0)
                    {
                        foreach (var entry in card.OnImpactPerks)
                        {
                            OnImpactPerks.Add(entry);
                            OnImpactPerksTrackCooldown.Add(0);

                        }
                    }

                    if (card.OnShootPerks.Count > 0)
                    {
                       
                        foreach (var entry in card.OnShootPerks)
                        {   
                            
                            OnShootPerks.Add(entry);
                            OnShootPerksTrackCooldown.Add(0);
                             
                        }
                    }

                    if (card.EnemyDeathPerks.Count > 0)
                    {
                        foreach (var entry in card.EnemyDeathPerks)
                        {
                            EnemyDeathPerks.Add(entry);
                            EnemyDeathPerksTrackCooldown.Add(0);
                        }
                    }

                    if (card.EnemyDebuffPerks.Count > 0)
                    {
                        foreach (var entry in card.EnemyDebuffPerks)
                        {
                            EnemyDebuffPerks.Add(entry);
                            EnemyDebuffPerksTrackCooldown.Add(0);
                        }
                    }

                    if (card.OnBulletDestroy.Count > 0)
                    {
                        foreach (var entry in card.OnBulletDestroy)
                        {
                            OnBulletDestroy.Add(entry);
                            OnBulletDestroyTrackCooldown.Add(0);
                        }
                    }

                    if (card.CharacterStatBoost.Count > 0)
                    {
                        movementSpeedModifier = 1;
                        magSizeModifier = 1;
                        healthModifier = 1;
                        SaltDamageModifier = 1;

                        foreach (var entry in card.CharacterStatBoost)
                        {
                            switch (entry.type)
                            {
                                case StatType.None:
                                    break;

                                case StatType.AttackSpeed:
                                    break;

                                case StatType.MovementSpeed:
                                    movementSpeedModifier += entry.value;
                                    movement.IncreaseMovementSpeed(movementSpeedModifier);
                                    break;

                                case StatType.DashRange:
                                    break;

                                case StatType.DashCooldown:
                                    break;

                                case StatType.Health:
                                case StatType.BonusHealth:
                                    healthModifier += entry.value;
                                    health = originalHealth * healthModifier;
                                    break;
                           
                                case StatType.BonusSanity:
                                    break;

                                case StatType.ReloadeTime:
                                    break;

                                case StatType.MagazineSize:
                                    magSizeModifier += entry.value;
                                   
                                    break;

                                case StatType.KnockBack:
                                    break;

                                case StatType.SaltDamage:
                                    SaltDamageModifier += entry.value;
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }




        public void CalculateBulletDamage()
        {
            if (collectedCards != null)
            {
                currentBulletDamagePerType = new List<DamageTypeValue>();

                foreach (var card in collectedCards)
                {
                    if (card.AttackTypeDamage.Count > 0)
                    {
                        currentBulletDamagePerType = MergeDamageStats(currentBulletDamagePerType, card.AttackTypeDamage);
                    }
                }
            }

            if (IsOwner) { 
            DamageTypeValue[] damageArray = currentBulletDamagePerType.ToArray();
            SetBulletDamageServerRpc(damageArray);
            }
        }



        [ServerRpc]
        public void SetBulletDamageServerRpc(DamageTypeValue[] damageList)
        {
            List<DamageTypeValue> damageListConverted = new List<DamageTypeValue>(damageList);
            this.currentBulletDamagePerType = damageListConverted;
        }


       
        
        public void CalculateSkillDamage()
        {

            if (collectedCards != null)
            {
                currentSkillDamagePerType = new List<DamageTypeValue>();
                foreach (var card in collectedCards)
                {
                    if (card.SkillTypeDamage.Count > 0)
                    {
                        currentSkillDamagePerType = MergeDamageStats(currentSkillDamagePerType, card.SkillTypeDamage);
                    }
                }
            }
        }





        public void AddPerk(int id)
        {
            ///Check if the Perk that the Client wants to add is already in the List of Perks the client has, only applies
            ///for unique perks that can only be obtained once per run
            var addedCard = PerkDatabase.Instance.GetCardByID(id);

            if (addedCard.IsUnique)
            {
                foreach (var card in collectedCards)
                {
                    if (addedCard.name.Equals(card.name))
                    {
                        print("CARD ALREADY IN THE DECK");
                        return;
                    }
                }
            }

            NewPerkAdded?.Invoke(addedCard.Icon, OwnerClientId);

            ///Send the Request to the stupid server to add the Perk to a list that the server actually knows
            ///problem is you cant send Scriptables objects over, so its just an int that the serv uses to look up the
            ///scriptable object inside the Database
            if (IsOwner) AddCardServerRpc(addedCard.ID);
            if(!IsHost) updateCards(addedCard.ID);
        }


        public void AddServerCard(int id)
        {
            serverCardIDs.Add(id);
        }


        [ServerRpc]
        void AddCardServerRpc(int id, ServerRpcParams rpcParams = default)
        {
            /// Lookup the correct player
            /// get the client that requested to add the perk and add the perk to the list of perks the sender has
            /// so it doesnt get added to the other weirdos playing the game

            var clientId = rpcParams.Receive.SenderClientId;
            var stats = PlayerRegistry.GetStats(clientId);

            stats.AddServerCard(id);
            
            var targetClient = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new[] { rpcParams.Receive.SenderClientId }
                }
            };

            //AddCardIconToUIClientRpc(id, targetClient);

            updateCards(id);
        }

        [ClientRpc]
        void AddCardIconToUIClientRpc(int id, ClientRpcParams clientRpcParams = default)
        {
            var perk = PerkDatabase.Instance.GetCardByID(id);
            NewPerkAdded?.Invoke(perk.Icon, OwnerClientId);
            lastAddedCard = perk;
            if (IsHost) return;
            updateCards(lastAddedCard.ID);
        }



        public List<DamageTypeValue> MergeDamageStats(List<DamageTypeValue> listA, List<DamageTypeValue> listB)
        {
            Dictionary<DamageType, float> mergedList = new();


            // Add from first list
            foreach (var entry in listA)
            {
                if (!mergedList.ContainsKey(entry.type))
                {
                    mergedList[entry.type] = 0f;
                }

                mergedList[entry.type] += entry.value;
            }

            // Add from second list
            foreach (var entry in listB)
            {
                if (!mergedList.ContainsKey(entry.type))
                    mergedList[entry.type] = 0f;

                mergedList[entry.type] += entry.value;
            }

            // Convert to list
            List<DamageTypeValue> result = new();
            foreach (var pair in mergedList)
            {
                result.Add(new DamageTypeValue { type = pair.Key, value = pair.Value });
            }

            return result;
        }


        public void updateCards(int id)
        {
            var newCard = PerkDatabase.Instance.GetCardByID(id);
            collectedCards.Add(newCard);

            if (newCard.NewBulletType != DamageType.None) { BulletDamageType = newCard.NewBulletType; }
            if (newCard.NewMainSkillType != DamageType.None) { SkillDamageType = newCard.NewMainSkillType; }

            SetCollectedPerks();
            CalculateBulletDamage();
            CalculateSkillDamage();
            GetActiveBulletDamage();
            GetActiveSkillDamage();
        }



        public void GetActiveBulletDamage()
        {

            bulletDamage = 0;
            foreach (var entry in currentBulletDamagePerType)
            {
                    bulletDamage += entry.value;
            }
        }

        public void GetActiveSkillDamage()
        {

            skillDamage = 0;
            foreach (var entry in currentSkillDamagePerType)
            {
                if (entry.type == SkillDamageType)
                {
                    skillDamage = entry.value;
                }
            }
        }

        public override void TakeDamage(float damage, DamageType affix = DamageType.None, int stackSize = 1, float saltModifier = 1, int owner = -1, Vector3? hitPosition = null)
        {
            //if (!IsServer && IsOwner) {
            //    takeDamageServerRpc(damage,OwnerClientId);
            //}
            if(IsServer) { 
                currentHealth.Value -= damage;
            }

            if (hitPosition != null)
            {
                HurtVFXClientRpc(OwnerClientId, (Vector3)hitPosition, hurtEffect.ID);
            }

            


            base.TakeDamage(damage, affix, stackSize, saltModifier);

            if (currentHealth.Value <= 0f)
            {
                OnDeath();
            }
        }

        [ServerRpc]
        void takeDamageServerRpc(float damage, ulong id)
        {
            PlayerRegistry.GetStats(id).currentHealth.Value -= damage;
        }

        [ClientRpc]
        void HurtVFXClientRpc(ulong shooterClientId,Vector3 pos, int id)
        {
            if (shooterClientId == NetworkManager.Singleton.LocalClientId)
            {
                return;
            }

            PlayHurtEffect(pos,id);
        }

        public void PlayHurtEffect(Vector3 pos, int id)
        {
            GameObject fx = ObjectPoolManager.Instance.GetPooledObject(id);

            fx.transform.localPosition = pos;
            fx.transform.localRotation = Quaternion.identity;
            fx.GetComponent<Pitcher>()?.ChangePitch();
        }

        public void PlayHitmarker(bool killingBlow)
        {
            hitmarker.ActivateHitmarker();
            gameObject.GetComponent<Pitcher>().ChangePitch();
            if(!killingBlow)
                audioSource.PlayOneShot(hitSound.SFX);
            else
                audioSource.PlayOneShot(killSound.SFX);
        }

        public void PlayVFXAtLocation(int fxID, Vector3 location)
        {
            PlayVfx(fxID,location);
            PlayVFXAtLocationServerRpc(fxID, location, OwnerClientId);
        }

        public void PlayVfx(int fxID, Vector3 location)
        {
            GameObject fx = ObjectPoolManager.Instance.GetPooledObject(fxID);

            fx.transform.position = location;
            fx.transform.localRotation = Quaternion.identity;
            fx.GetComponent<Pitcher>()?.ChangePitch();
        }


        [ServerRpc]
        public void PlayVFXAtLocationServerRpc(int fxID, Vector3 location,ulong owner)
        {
            PlayVFXAtLocationClientRpc(fxID, location, owner);
        }

        

        [ClientRpc]
        public void PlayVFXAtLocationClientRpc(int fxID, Vector3 location, ulong owner)
        {
            if (owner == NetworkManager.Singleton.LocalClientId)
                return;

            PlayVfx(fxID, location);
        }
    }
}