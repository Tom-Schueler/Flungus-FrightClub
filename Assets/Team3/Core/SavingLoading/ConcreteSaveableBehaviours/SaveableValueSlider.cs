using System;
using Team3.SavingLoading.SaveData;
using Team3.UserInterface.Settings;
using UnityEngine;

namespace Team3.SavingLoading.SaveableBehaviours
{
    public class SaveableValueSlider : SaveableBehaviour
    {
        [SerializeField] private ValueSlider valueSlider;
        
        public override void Load()
        {
            if (SettingsData.Singleton.vsyncEnabled == null)
            {
                throw new NotImplementedException($"Not implemented what happens if no value fund... Load dafualt");
            }

            if (valueSlider != null)
            {
                valueSlider.Load((float) SettingsData.Singleton.sensitivity);
            }
        }

        public override void Save()
        {
            SettingsData.Singleton.sensitivity = valueSlider.Value;
        }
    }
}
