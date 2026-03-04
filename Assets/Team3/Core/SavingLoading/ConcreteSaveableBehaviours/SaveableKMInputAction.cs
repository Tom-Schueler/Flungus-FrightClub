using System;
using System.Collections.Generic;
using Team3.SavingLoading.DataStructs;
using Team3.SavingLoading.SaveData;
using Team3.UserInterface.Settings;
using UnityEngine;

namespace Team3.SavingLoading.SaveableBehaviours
{
    public class SaveableKMInputAction : SaveableBehaviour
    {
        [SerializeField] private KeyBoardMousePlayerAction keyBoardMousePlayerAction;
        [SerializeField] private KeybordMouseInputSetting keybordMouseInputSetting;

        public override void Load()
        {
            if (!SettingsData.Singleton.KMActionExists(keyBoardMousePlayerAction))
            {
                throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
            }

            InputData data = SettingsData.Singleton.GetKMAction(keyBoardMousePlayerAction);

            if (data == null)
            {
                // load default
                Debug.LogError("Faild to load defualt setting");
                throw new NotImplementedException($"Class: {nameof(SaveableKMInputAction)}, Method: {nameof(Load)} has not yet implemented, what happens if it cant find the setting");
            }

            if (keybordMouseInputSetting != null)
            {
                keybordMouseInputSetting.Load(data.path, data.displayText);
            }
        }

        public override void Save()
        {
            if (!SettingsData.Singleton.KMActionExists(keyBoardMousePlayerAction))
            {
                throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
            }

            SettingsData.Singleton.SetKMAction(keyBoardMousePlayerAction, new InputData(keybordMouseInputSetting.Path, keybordMouseInputSetting.DisplayText));
        }
    }
}
