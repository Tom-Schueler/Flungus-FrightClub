using System;
using System.Collections.Generic;
using Team3.SavingLoading;
using Team3.SavingLoading.SaveData;
using Team3.UserInterface.Settings;
using UnityEngine;

public class SaveableInt : SaveableBehaviour
{
    [SerializeField] private DropDownValue dropDownValue;
    [SerializeField] private IntegerValue integerValue;
    [SerializeField] private DropdownApplyer dropdownApplyer;

    public override void Save()
    {
        if (!SettingsData.Singleton.DropDownValueExists(dropDownValue))
        {
            throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
        }

        SettingsData.Singleton.SetDropDownValue(dropDownValue, integerValue.Value);
    }

    public override void Load()
    {
        if (!SettingsData.Singleton.DropDownValueExists(dropDownValue))
        {
            Debug.Log(dropDownValue);
            throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
        }

        int? value = SettingsData.Singleton.GetDropDownValue(dropDownValue);

        if (value == null)
        {
            // load default
            Debug.LogError("Faild to load defualt setting");
            throw new NotImplementedException($"Class: {nameof(SaveableInt)}, Method: {nameof(Load)} has not yet implemented, what happens if it cant find the setting");
        }

        if (integerValue != null)
        {
            integerValue.Load((int)value);
        }

        if (dropdownApplyer != null)
        {
            dropdownApplyer.Set((int) value);
        }
    }
}
