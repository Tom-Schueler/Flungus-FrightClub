using System;
using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.Multiplayer.Lobby
{
    public class RoundStarter : MonoBehaviour
    {
        [SerializeField] private TMP_Text buttonTextDisplay;
        [SerializeField] private Image buttonImage;

        [Space]
        [Header("Warning Labels")]

        [SerializeField] private GameObject belowPlayerThresholdWarning;
        [SerializeField] private GameObject playerNotReadyWarning;
        [SerializeField, Range(0.5f, 5f)] private float displayTime;

        public static Action OnStartWorldScene;
        public static Action<ulong, bool> OnClientToggledReady;

        private bool isReady;

        private void OnEnable()
        {
            NetworkManager.OnInstantiated += StartListening;
        }

        private void StartListening(NetworkManager manager)
        {
            manager.OnServerStarted += SetButtonText;
            manager.OnClientStarted += SetButtonText;
        }

        private void OnDisable()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnServerStarted -= SetButtonText;
                NetworkManager.Singleton.OnClientStarted -= SetButtonText;
            }
        }

        private void SetButtonText()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                buttonTextDisplay.text = "Start";
            }
            else if (NetworkManager.Singleton.IsClient)
            {
                buttonTextDisplay.text = "ReadyUp";
            }
        }

        public void StartRound()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                #if !UNITY_EDITOR
                
                // player count at least 2
                if (ReadyTracker.PlayerCount < 1) // < 1 cuz host isnt considered player
                {
                    StartCoroutine(DisplayWarning(belowPlayerThresholdWarning));
                    return;
                }

                #endif

                if (!ReadyTracker.AllReady())
                {
                    StartCoroutine(DisplayWarning(playerNotReadyWarning));
                    return;
                }

                OnStartWorldScene?.Invoke();
            }
            else if (NetworkManager.Singleton.IsClient)
            {
                isReady = !isReady;

                if (isReady)
                {
                    buttonTextDisplay.text = "Cancel Ready";
                    buttonImage.color = Color.red;
                }
                else
                {
                    buttonTextDisplay.text = "ReadyUp";
                    buttonImage.color = Color.white;
                }

                OnClientToggledReady?.Invoke(NetworkManager.Singleton.LocalClientId, isReady);
            }
        }

        private IEnumerator DisplayWarning(GameObject warning)
        {
            warning.SetActive(true);

            yield return new WaitForSeconds(displayTime);

            warning.SetActive(false);
        }
    }
}
