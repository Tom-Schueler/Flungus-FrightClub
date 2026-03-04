using System;
using Team3.SavingLoading;
using Team3.SavingLoading.SaveData;
using Team3.UserInterface.Settings;
using UnityEngine;

public class SaveableBool : SaveableBehaviour
{
    [SerializeField] private BoolValue boolValue;

    public override void Load()
    {
        if (SettingsData.Singleton.vsyncEnabled == null)
        {
            throw new NotImplementedException($"Not implemented what happens if no value fund... Load dafualt");
        }

        if (boolValue != null)
        {
            boolValue.Load((bool)SettingsData.Singleton.vsyncEnabled);
        }
        
        if ((bool)SettingsData.Singleton.vsyncEnabled)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public override void Save()
    {
        SettingsData.Singleton.vsyncEnabled = boolValue.Value;
    }
}
