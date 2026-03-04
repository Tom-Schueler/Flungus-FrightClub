using UnityEngine;

public class PerkManager : MonoBehaviour
{
    [SerializeField] private PerkDatabase perkDatabase;

    private void Awake()
    {
        perkDatabase.Initialize(); // sets PerkDatabase.Instance
    }
}