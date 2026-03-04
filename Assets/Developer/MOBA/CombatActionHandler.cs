using Team3.Characters;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;
using UnityEngine.Animations;

namespace Team3.MOBA
{
    public class CombatActionHandler : NetworkBehaviour
    {

        [Space(10), Header("References")]

        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private Camera fpsCam;



        [Space(10), Header("Player Setup")]
        [SerializeField] private GameObject playerCharacter;
        [SerializeField] private Material[] playerMaterial;
        [SerializeField] private GameObject playerModel;

        [Space(10), Header("Gun Setup")]
        [SerializeField] private GameObject weaponSocket;
        [SerializeField] private GameObject weaponObject;
        public int localGunID;
        [SerializeField] private Camera fpsCamera;
        [SerializeField] private Recoil recoil;
        [SerializeField] private Transform cameraSocket;
        [SerializeField] private CameraShake shake;
        
        public GameObject WeaponObject => weaponObject;

        public NetworkVariable<bool> isAllowedToShoot = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

        public void InputShootAction(CallbackContext callbackContext)
        {
            if (isAllowedToShoot.Value)
            {
                weaponObject.GetComponent<IWeapon>().InputShootAction(callbackContext);
            }
        }

        public void InputReloadAction(CallbackContext callbackContext)
        {
            weaponObject.GetComponent<IWeapon>().InputReloadAction(callbackContext);
        }

        public void ForceStopAttacking(bool oldValue, bool newValue)
        {
            if (newValue == false)
            {
                StopAttacking();
            }
        }

        public void ForceReload()
        {
            weaponObject.GetComponent<IWeapon>().ForceReload();
        }

        public override void OnNetworkSpawn()
        {
            playerCharacter.GetComponent<SkinnedMeshRenderer>().material = playerMaterial[(int)OwnerClientId]; //playerMaterial[(int)OwnerClientId]
            isAllowedToShoot.OnValueChanged += ForceStopAttacking;

            if (IsServer)
            {
                SetupGunServerRpc(OwnerClientId, localGunID);
            }
        }
        public void StopAttacking()
        {
            weaponObject.GetComponent<WeaponBase>().isAttackPressed = false;
        }

        [ServerRpc(RequireOwnership = false)]
        void SetupGunServerRpc(ulong ownerID, int gunId)
        {
            var instantiatedItem = Instantiate(PerkDatabase.Instance.GetWeaponByID(gunId).Weapon);
            var networkObject = instantiatedItem.GetComponent<NetworkObject>();
            networkObject.SpawnWithOwnership(ownerID);

            SetupGunClientRpc(networkObject);
        }

        [ClientRpc]
        void SetupGunClientRpc(NetworkObjectReference weaponRef)
        {
            if (!weaponRef.TryGet(out NetworkObject weaponNetObj))
            { return; }

            var constraintSource = new ConstraintSource()
            {
                sourceTransform = weaponSocket.transform,
                weight = 1
            };

            weaponNetObj.gameObject.GetComponent<PositionConstraint>().AddSource(constraintSource);
            weaponNetObj.gameObject.GetComponent<RotationConstraint>().AddSource(constraintSource);
            weaponObject = weaponNetObj.gameObject;

            if (IsOwner)
            {
                weaponObject.layer = 11;
                var children = weaponObject.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
                children.ForEach(c => c.layer = 11);

                playerModel.layer = 13;
                children = playerModel.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
                children.ForEach(c => c.layer = 13);

                weaponObject.GetComponent<WeaponBase>().SetupWeapon(cameraSocket, recoil, fpsCamera, OwnerClientId, shake);
                playerStats.AddPerk(weaponObject.GetComponent<WeaponBase>().GunCard.ID);
            }
        }
    }
}
