using System;
using System.Collections.Generic;
using Team3.SavingLoading.DataStructs;
using Team3.SavingLoading.SaveData;
using Team3.UserInterface.Settings;
using UnityEngine;

namespace Team3.SavingLoading.SaveableBehaviours
{
    public class SaveableGPInputAction : SaveableBehaviour
    {
        [SerializeField] private GamePadPlayerAction gamePadPlayerAction;
        [SerializeField] private GamepadInputSetting gamepadInputSetting;

        public override void Load()
        {
            if (!SettingsData.Singleton.GPActionExists(gamePadPlayerAction))
            {
                throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
            }

            InputData data = SettingsData.Singleton.GetGPAction(gamePadPlayerAction);

            if (data == null)
            {
                // load default
                Debug.LogError("Faild to load defualt setting");
                throw new NotImplementedException($"Class: {nameof(SaveableGPInputAction)}, Method: {nameof(Load)} has not yet implemented, what happens if it cant find the setting");
            }

            if (gamepadInputSetting != null)
            {
                gamepadInputSetting.Load(data.path, data.displayText);
            }
        }

        public override void Save()
        {
            if (!SettingsData.Singleton.GPActionExists(gamePadPlayerAction))
            {
                throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
            }

            SettingsData.Singleton.SetGPAction(gamePadPlayerAction, new InputData(gamepadInputSetting.Path, gamepadInputSetting.DisplayText));
        }
    }
}
