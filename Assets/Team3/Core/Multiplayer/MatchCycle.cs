using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Team3.Characters;
using Team3.SavingLoading.SaveData;
using Team3.Tools;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Team3.Multiplayer
{
    public class MatchCycle : NetworkBehaviour
    {
        [SerializeField] private PlayableDirector cinematic;
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private int inScoreboardTime;
        [SerializeField] private int numberOfRounds;

        [Space]

        public UnityEvent OnIntiateSession;
        public UnityEvent OnRoundStart;
        public UnityEvent OnPlayerSpawned;
        public UnityEvent OnShowScoreboard;
        public UnityEvent OnShowEndScreen;

        public static Action<int> OnUpdateTimeDisplay;
        public static Action<ScoreboardInfo[]> OnUpdateScoreboard;
        public static Action OnScoreboardEnd;


        private Dictionary<ulong, string> playerNameId = new Dictionary<ulong, string>();
        private Dictionary<ulong, int> playerWeapons = new Dictionary<ulong, int>();
        private Dictionary<ulong, DeathInfo> playersDeathInfo = new Dictionary<ulong, DeathInfo>();
        private Dictionary<ulong, int> playersPoints = new Dictionary<ulong, int>();

        private bool shouldContinueNextRound = false;
        private int playerWeaponSetCount = 0;
        private bool isLastRound;
        public static NetworkVariable<bool> isDeathzoneAktive = new NetworkVariable<bool>();

        private int deathCount = 0;

        public override void OnNetworkSpawn()
        {
            InitiateSessionClientRpc();
        }

        [ClientRpc]
        private void InitiateSessionClientRpc()
        {
            OnIntiateSession?.Invoke();
        }

        public void StartCinamatic()
        {
            cinematic.Play();
            cinematic.stopped += Continue;
        }

        public override void OnNetworkDespawn()
        {
            cinematic.stopped -= Continue;
            PlayerStats.OnPlayerDeath -= RegisterDeath;
            NetworkManager.Singleton.OnClientDisconnectCallback -= RegisterDeath;
        }

        private void Continue(PlayableDirector director)
        {
            cinematic.stopped -= Continue;
            Destroy(cinematic.gameObject);

            if (IsServer)
            {
                StartCoroutine(Continue());
            }
        }

        private IEnumerator Continue()
        {
            PlayerStats.OnPlayerDeath += RegisterDeath;
            NetworkManager.Singleton.OnClientDisconnectCallback += RegisterDeath;

            GetNames();
            GetWeaponIds();

            foreach (ulong clientId in NetworkManager.ConnectedClientsIds)
            {
                playersPoints.Add(clientId, 0);
            }

            // wait untill returned
           

            yield return new WaitUntil(() => playerWeaponSetCount == NetworkManager.ConnectedClientsIds.Count);

            playerSpawner.Spawn(playerWeapons);
            
            for (int i = 0; i < numberOfRounds; i++)
            {
                isDeathzoneAktive.Value = false;

                // Clear last round

                playersDeathInfo.Clear();
                deathCount = 0;

                // end

                isLastRound = i >= numberOfRounds - 1;

                foreach (ulong clientId in NetworkManager.ConnectedClientsIds) // populate
                {
                    playersDeathInfo.Add(clientId, new DeathInfo());
                }

                RoundStartedClientRpc();

                // let the world spawn
                yield return new WaitForSeconds(1);

                playerSpawner.Respawn(NetworkManager.ConnectedClientsIds);
                 

                PlayersSpawnedClientRpc();

                isDeathzoneAktive.Value = true;

                yield return new WaitForSeconds(1);

               // :( 

                yield return new WaitUntil(() => shouldContinueNextRound);
                shouldContinueNextRound = false;
            }
        }
        private void GetNames()
        {
            GetNameClientRpc();
        }

        private void GetWeaponIds()
        {
            GetWeaponIdClientRpc();
        }

        [ClientRpc]
        private void GetWeaponIdClientRpc()
        {
            SetWeaponIdServerRpc(NetworkManager.Singleton.LocalClientId, WeaponHolder.WeaponId);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetWeaponIdServerRpc(ulong clientId, int weaponId)
        {
            playerWeapons.Add(clientId, weaponId);

            playerWeaponSetCount++;
        }

        [ClientRpc]
        private void GetNameClientRpc()
        {
            SetNameServerRpc(NetworkManager.Singleton.LocalClientId, GameData.Singleton.name);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetNameServerRpc(ulong id, string name)
        {
            playerNameId.Add(id, name);
        }

        [ClientRpc]
        private void PlayersSpawnedClientRpc()
        {
        

            OnPlayerSpawned?.Invoke();        

        }

        [ClientRpc]
        private void RoundStartedClientRpc()
        {
            OnRoundStart?.Invoke();
        }

        [ClientRpc]
        private void ShowEndScreenClientRpc(ScoreboardInfo[] scoreboardInfos)
        {
            StartCoroutine(EndScreen(scoreboardInfos));
        }

        private IEnumerator EndScreen(ScoreboardInfo[] scoreboardInfos)
        {
            OnShowEndScreen?.Invoke();
            yield return new WaitForSeconds(1f);
            OnUpdateScoreboard?.Invoke(scoreboardInfos);
        }

        private void RegisterDeath(ulong clientId)
        {
            if (playersDeathInfo[clientId].isDead == false)
            {
                playersDeathInfo[clientId].isDead = true;
                playersDeathInfo[clientId].deathIndex = deathCount;
                deathCount++;
            }
            else
            {
                return;
                throw new Exception("Cant die twice per round?");
            }

            CheckRemainingPlayers();
        }

        private void CheckRemainingPlayers()
        {
            int aliveCount = 0;
            ulong? alivePlayer = null;
            foreach (var playerStatus in playersDeathInfo)
            {
                if (playerStatus.Value.isDead == true)
                { continue; }

                aliveCount++;
                alivePlayer = playerStatus.Key;
            }

            if (aliveCount < 2)
            {
                if (alivePlayer == null)
                {
                    throw new Exception("No player is alive, this a problem and shouldnot happen");
                }

               

                playersDeathInfo[(ulong)alivePlayer].deathIndex = deathCount;

                CalculatePlayerPoints();

                List<ScoreboardInfo> scoreboardInfos = new List<ScoreboardInfo>();
                foreach (KeyValuePair<ulong, int> playerPoints in playersPoints)
                {
                    scoreboardInfos.Add(new ScoreboardInfo(playerNameId[playerPoints.Key], playerPoints.Value));
                }

                playerSpawner.DisableRemainingPlayers();

                if (!isLastRound)
                {

                    ShowScoreboardClientRpc(scoreboardInfos.ToArray());
                    StartCoroutine(ScoreboardTimer(inScoreboardTime));
                }
                else
                {
                    shouldContinueNextRound = true;
                    ShowEndScreenClientRpc(scoreboardInfos.ToArray());
                }
            }
        }

        private void CalculatePlayerPoints()
        {
            List<ulong> orderedIds = playersDeathInfo
                .OrderBy(kvp => kvp.Value.deathIndex)
                .Select(kvp => kvp.Key)
                .ToList();

            for (int i = 0; i < orderedIds.Count; i++)
            {
                playersPoints[orderedIds[i]] += (i * i) + 1;
            }
        }

        [ClientRpc]
        private void ShowScoreboardClientRpc(ScoreboardInfo[] scoreboardInfos)
        {
            StartCoroutine(ShowScoreboard(scoreboardInfos));
        }

        private IEnumerator ShowScoreboard(ScoreboardInfo[] scoreboardInfos)
        {
            // some thing something problem with rpc and scene change. idk, dont ask me. bad code and so on
            yield return new WaitForSeconds(0.3f);
            OnShowScoreboard?.Invoke();

            yield return new WaitForSeconds(1f);
            OnUpdateScoreboard?.Invoke(scoreboardInfos);
        }

        private IEnumerator ScoreboardTimer(int time)
        {
            UpdateScoreboardTimerClientRpc(time);

            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                UpdateScoreboardTimerClientRpc(time);
            }

            ScoreboardEndClientRpc();
            shouldContinueNextRound = true;
        }

        [ClientRpc]
        private void UpdateScoreboardTimerClientRpc(int time)
        {
            OnUpdateTimeDisplay?.Invoke(time);
        }

        [ClientRpc]
        private void ScoreboardEndClientRpc()
        {
            OnScoreboardEnd?.Invoke();
        }
    }
}
