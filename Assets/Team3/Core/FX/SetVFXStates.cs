using Team3.Characters;
using UnityEngine;

public class SetVFXStates : MonoBehaviour
{
    [SerializeField]
    private PlayerStats stats;

    [SerializeField]
    private GameObject fireFX;

    // Update is called once per frame
    void Update()
    {
        if (stats.isOnFire)
        {
            fireFX.SetActive(true);
        }
        else
        {
            fireFX.SetActive(false);
        }
    }
}
