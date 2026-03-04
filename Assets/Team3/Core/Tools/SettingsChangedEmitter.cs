using System;
using UnityEngine;

namespace Team3.Tools
{
    public class SettingsChangedEmitter : MonoBehaviour
    {
        public static Action OnSettingsChanged;

        public void Emit()
        {
            OnSettingsChanged?.Invoke();
        }
    }
}
