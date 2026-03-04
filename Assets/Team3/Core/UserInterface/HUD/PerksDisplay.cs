using System;
using System.Collections;
using System.Collections.Generic;
using Team3.Combat;
using Team3.Input;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace Team3.UserInterface.HUD
{
    public class PerksDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform perkGridRectTransform;
        [SerializeField] private GameObject perkGrid;
        [SerializeField] private float lerpUpTime;
        [SerializeField] private float lerpDownTime;

        [SerializeField] private GameObject prefabImage;

        private PlayerInputActions inputActions;
        private Coroutine currentLerp;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }

        private void Start()
        {
            List<SOCombatCards> cards = PlayerRegistry.GetStats(NetworkManager.Singleton.LocalClientId).collectedCards;

            foreach (SOCombatCards card in cards)
            {
                GameObject instance = Instantiate(prefabImage, perkGrid.transform);
                instance.GetComponent<Image>().sprite = card.Icon;
            }
        }

        private void OnEnable()
        {
            inputActions.Enable();

            inputActions.UI.Tab.started += ShowPerks;
            inputActions.UI.Tab.canceled += HidePerks;
            PlayerRegistry.GetStats(NetworkManager.Singleton.LocalClientId).NewPerkAdded += AddPerk;
        }


        private void OnDisable()
        {
            inputActions.UI.Tab.started -= ShowPerks;
            inputActions.UI.Tab.canceled -= HidePerks;
            PlayerRegistry.GetStats(NetworkManager.Singleton.LocalClientId).NewPerkAdded -= AddPerk;

            inputActions.Disable();
        }

        private void ShowPerks(CallbackContext context)
        {
            if (currentLerp != null)
            {
                StopCoroutine(currentLerp);
            }

            currentLerp = StartCoroutine(LerpPosition(rectTransform.anchoredPosition.y, perkGridRectTransform.rect.height, lerpUpTime));
        }

        private void HidePerks(CallbackContext context)
        {
            if (currentLerp != null)
            {
                StopCoroutine(currentLerp);
            }
            
            currentLerp = StartCoroutine(LerpPosition(rectTransform.anchoredPosition.y, 0, lerpDownTime));
        }

        private void AddPerk(Sprite sprite, ulong arg2)
        {
            GameObject instance = Instantiate(prefabImage, perkGrid.transform);
            instance.GetComponent<Image>().sprite = sprite;
        }
        
        private IEnumerator LerpPosition(float startPos, float targetPos, float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float newY = Mathf.Lerp(startPos, targetPos, elapsedTime / duration);
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetPos);
        }
    }
}
