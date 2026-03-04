using UnityEngine;
using TMPro;
using Unity.Netcode;
public class UpdateUIPlayerHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text player1txt;
    [SerializeField] private TMP_Text player2txt;
    bool playerSet = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {

        var player1 = PlayerRegistry.GetStats(0);
        var player2 = PlayerRegistry.GetStats(1);


        if(player1 != null && player2 != null) {
            if (!playerSet) { 
                playerSet = true;
        player1.currentHealth.OnValueChanged += UpdatePlayer1;
        player2.currentHealth.OnValueChanged += UpdatePlayer2;
        }
        }
    }


    public void UpdatePlayer1(float old, float current)
    {
        player1txt.SetText(current.ToString());
    }

    public void UpdatePlayer2(float old, float current)
    {
        player2txt.SetText(current.ToString());
    }
}
