using System;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.UserInterface.Settings
{
    public class ToggleButton : BoolValue
    {
        [SerializeField] private Toggle toggle;

        public override bool Value => toggle.isOn;

        public override void Load(bool value)
        {
            toggle.isOn = value;
        }

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(Set);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(Set);
        }

        private void Set(bool isEnabled)
        {
            if (isEnabled)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }
    }
}
