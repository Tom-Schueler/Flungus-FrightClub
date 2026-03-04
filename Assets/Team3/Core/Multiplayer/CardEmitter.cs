using System;
using Team3.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.Multiplayer
{
    public class CardEmitter : MonoBehaviour
    {
        [SerializeField] private CardGenerator cardGenerator;
        [SerializeField] private Image selectedImage;

        public static Action<int> OnCardChosen;
        public static Action<int> OnCardInstantiated;

        private static Action OnCardSelected;

        private void Start()
        {
            OnCardSelected += CardSelected;
            OnCardInstantiated?.Invoke(cardGenerator.Card.ID);
        }

        private void OnDestroy()
        {
            OnCardSelected -= CardSelected;
        }

        public void ChoseCard()
        {
            OnCardChosen?.Invoke(cardGenerator.Card.ID);
            OnCardSelected?.Invoke();
            selectedImage.gameObject.SetActive(true);
        }

        private void CardSelected()
        {
            selectedImage.gameObject.SetActive(false);
        }
    }
}
