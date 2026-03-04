using System.Collections.Generic;
using Team3.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.UserInterface
{
    public class CardGenerator : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private Image background;

        [SerializeField]
        private TMP_Text Name;

        [SerializeField]
        private Image frame;

        [SerializeField]
        private Sprite heavenFrame;
        [SerializeField]
        private Sprite heavenBG;
        [SerializeField]
        private Sprite hellFrame;
        [SerializeField]
        private Sprite hellBG;
        [SerializeField]
        private Sprite purgeFrame;
        [SerializeField]
        private Sprite purgeBG;

        public SOCombatCards Card;


        public void SetupCard()
        {
            switch (Card.Alignment)
            {
                case CardAlignment.Heaven:
                    frame.sprite = heavenFrame;
                    background.sprite = heavenBG;
                    break;
                case CardAlignment.Hell:
                    frame.sprite = hellFrame;
                    background.sprite = hellBG;
                    break;
                case CardAlignment.Purgatory:
                    frame.sprite = purgeFrame;
                    background.sprite = purgeBG;
                    break;
                default:
                    break;
            }


            icon.sprite = Card.Icon;
            Name.SetText(Card.CardName);
            //background.sprite = Card.Image;



        }
    }
}