using Team3.SavingLoading.SaveData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NameSaver : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;

    public UnityEvent OnNameSaved;

    public void SaveName()
    {
        GameData.Singleton.name = nameInput.text;
        OnNameSaved?.Invoke();
    }
}
