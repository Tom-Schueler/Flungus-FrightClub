using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Multiplayer
{
    public class CardHolder : MonoBehaviour
    {
        private List<int> cardIds = new List<int>(); 
        private int? chosenCardId;

        private void Start()
        {
            CardEmitter.OnCardChosen += ChooseCard;
            CardEmitter.OnCardInstantiated += CacheCard;

            MatchCycle.OnScoreboardEnd += AsignCard;
        }

        private void OnDestroy()
        {
            CardEmitter.OnCardChosen -= ChooseCard;
            CardEmitter.OnCardInstantiated -= CacheCard;

            MatchCycle.OnScoreboardEnd -= AsignCard;
        }

        private void CacheCard(int cardId)
        {
            cardIds.Add(cardId);
        }

        private void ChooseCard(int cardId)
        {
            chosenCardId = cardId;
        }

        private void AsignCard()
        {
            if (chosenCardId == null)
            {
                int randomCardIdIndex = Random.Range(0, cardIds.Count);
                chosenCardId = cardIds[randomCardIdIndex];
            }

            PlayerRegistry.GetStats(NetworkManager.Singleton.LocalClientId).AddPerk((int)chosenCardId);

            Debug.Log($"card with id:{chosenCardId} was added to player: {NetworkManager.Singleton.LocalClientId}");
        }
    }
}
