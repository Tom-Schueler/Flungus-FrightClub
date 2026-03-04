using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Team3.Enemys.Common;

public class Debug_StatTester : MonoBehaviour
{
    [SerializeField]
    private TMP_Text iceStacks;
    [SerializeField]
    private TMP_Text fireStacks;
    [SerializeField]
    private TMP_Text waterStacks;
    [SerializeField]
    private TMP_Text aistate;
    [SerializeField]
    private Slider healthbar;
    [SerializeField]
    private EnemyStats dummyStats;
    [SerializeField]
    // private Enemy_AI enemeyAIData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iceStacks.SetText("Ice Stacks: "+ dummyStats.iceStacks);
        fireStacks.SetText("Fire Stacks: " + dummyStats.fireStacks);
        waterStacks.SetText("Water Stacks: " + dummyStats.waterStacks);
        healthbar.value = ((float)dummyStats.currentHealth.Value / (float)dummyStats.originalHealth);
        // aistate.SetText("AI State: " + enemeyAIData.CurrentAIState.ToString());
    }
}
