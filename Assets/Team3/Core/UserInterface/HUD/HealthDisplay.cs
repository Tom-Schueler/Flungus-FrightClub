using Team3.Characters;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.UserInterface.HUD
{
    public class HealthDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthLabel;
        [SerializeField] private Slider healthSlider;
        
        private PlayerStats stats;

        private void Start()
        {
            stats = PlayerRegistry.GetStats(NetworkManager.Singleton.LocalClientId);
        }

        private void Update()
        {
            float health = stats.health;
            float currentHealth = stats.currentHealth.Value;

            healthLabel.text = $"{currentHealth:F0} / {health:F0}";
            healthSlider.value = currentHealth / health;
        }
    }
}
