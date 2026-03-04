using System;
using UnityEngine;

public class SavePrompter : MonoBehaviour
{
    public static Action OnPromptSave;

    public void PromptSave()
    {
        OnPromptSave?.Invoke();
    }
}
