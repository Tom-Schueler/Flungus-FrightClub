using Team3.SavingLoading.SaveData;
using TMPro;
using UnityEngine;

namespace Team3.UserInterface
{
    public class NameDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameLabel;

        private void Start()
        {
            nameLabel.text = GameData.Singleton.name;   
        }
    }
}
