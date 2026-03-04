using Team3.Combat;
using UnityEngine;
using System.Collections.Generic;

namespace Team3.UserInterface
{
    public class CardDecks : MonoBehaviour
    {
        [SerializeField]
        private List<SOCombatCards> Heaven = new();

        [SerializeField]
        private List<SOCombatCards> Hell = new();

        [SerializeField]
        private List<SOCombatCards> Purge = new();


        [SerializeField] private Transform heavenCard;
        [SerializeField] private Transform hellCard;
        [SerializeField] private Transform purgeCard;

        [SerializeField]
        private GameObject uiCardPrefab;


        public SOCombatCards PickCard(List<SOCombatCards> cardList)
        {
            int cardNumber = Random.Range(0, cardList.Count);
            SOCombatCards card = cardList[cardNumber];
            card = PerkDatabase.Instance.GetCardByID(card.ID);

            //If Card can only be Picked once, remove it from list
            if (card.IsUnique)
            {
                cardList.RemoveAt(cardNumber);
            }


            // Check if a Card is part of an Exclusive Group where you can only have 1 from
            if (card.PerkGroup != PerkGroup.None)
            {
                List<SOCombatCards> cardsToRemove = new();

                foreach (var checkCard in cardList)
                {
                    if (checkCard.PerkGroup == card.PerkGroup)
                    {
                        cardsToRemove.Add(card);
                    }
                }

                foreach (var removeCard in cardsToRemove)
                {
                    cardList.Remove(removeCard);
                }
            }



            return card;
        }


        [ContextMenu(itemName: "Setup Cards")]
        public void SetCards()
        {
            var newCard = Instantiate(uiCardPrefab, heavenCard);
            newCard.GetComponent<CardGenerator>().Card = PickCard(Heaven);
            newCard.GetComponent<CardGenerator>().SetupCard();

            newCard = Instantiate(uiCardPrefab, hellCard);
            newCard.GetComponent<CardGenerator>().Card = PickCard(Hell);
            newCard.GetComponent<CardGenerator>().SetupCard();

            newCard = Instantiate(uiCardPrefab, purgeCard);
            newCard.GetComponent<CardGenerator>().Card = PickCard(Purge);
            newCard.GetComponent<CardGenerator>().SetupCard();
        }
    }
}
