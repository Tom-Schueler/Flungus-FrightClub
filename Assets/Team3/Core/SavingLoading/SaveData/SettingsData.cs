using System.Collections.Generic;
using System.IO;
using Team3.Configs;
using Team3.SavingLoading.DataStructs;
using Newtonsoft.Json;
using UnityEngine;

namespace Team3.SavingLoading.SaveData
{
    public class SettingsData : ISaveData
    {
        // Sigelton Bheaviour
        private static SettingsData singleton;

        public static SettingsData Singleton
        {
            get
            {
                singleton ??= new SettingsData();
                return singleton;
            }
        }

        private SettingsData() { }

        public bool TryRead()
        {
            if (File.Exists(PathConfigs.Files.Settings))
            {
                try
                {
                    string content = File.ReadAllText(PathConfigs.Files.Settings);
                    singleton = JsonConvert.DeserializeObject<SettingsData>(content);

                    return true;
                }
                catch
                {
                    return ReadDefault();
                }
            }
            else
            {
                return ReadDefault();
            }
        }

        private bool ReadDefault()
        {
            try
            {
                TextAsset defaultSettingsJSON = Resources.Load<TextAsset>("DefaultSettings");
                singleton = JsonConvert.DeserializeObject<SettingsData>(defaultSettingsJSON.text);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Write()
        {
            string directory = Path.GetDirectoryName(PathConfigs.Files.Settings);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string jsonString = JsonConvert.SerializeObject(singleton, Formatting.Indented);

            File.WriteAllText(PathConfigs.Files.Settings, jsonString);
        }






        // General Settings
        [JsonProperty]
        private readonly Dictionary<DropDownValue, int?> dropDownSettings = new Dictionary<DropDownValue, int?>()
        {
            { DropDownValue.DisplayMode, null },
            { DropDownValue.ResolutionMode, null },
            { DropDownValue.FpsMode, null }
        };

        public bool DropDownValueExists(DropDownValue dropDownValue) => dropDownSettings.ContainsKey(dropDownValue);

        public void SetDropDownValue(DropDownValue dropDownValue, int value)
        {
            if (!DropDownValueExists(dropDownValue))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(dropDownSettings)} does not contain the key, make sure you check beforehand!");
            }

            dropDownSettings[dropDownValue] = value;
        }

        public int? GetDropDownValue(DropDownValue dropDownValue)
        {
            if (!DropDownValueExists(dropDownValue))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(dropDownSettings)} does not contain the key, make sure you check beforehand!");
            }

            return dropDownSettings[dropDownValue];
        }

        public bool? vsyncEnabled;
        public float? sensitivity;

        // Controll Settings
        [JsonProperty]
        private Dictionary<KeyBoardMousePlayerAction, InputData> keyBoardMouseActions = new Dictionary<KeyBoardMousePlayerAction, InputData>()
        {
            {KeyBoardMousePlayerAction.Jump, null},
            {KeyBoardMousePlayerAction.Reload, null},
            {KeyBoardMousePlayerAction.Shoot, null},
            {KeyBoardMousePlayerAction.Sprint, null},
            {KeyBoardMousePlayerAction.StriveLeft, null},
            {KeyBoardMousePlayerAction.StriveRight, null},
            {KeyBoardMousePlayerAction.Walk, null},
            {KeyBoardMousePlayerAction.WalkBack, null}
        };

        public bool KMActionExists(KeyBoardMousePlayerAction action) => keyBoardMouseActions.ContainsKey(action);

        public void SetKMAction(KeyBoardMousePlayerAction action, InputData path)
        {
            if (!KMActionExists(action))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(keyBoardMouseActions)} does not contain the key, make sure you check beforehand!");
            }

            keyBoardMouseActions[action] = path;
        }

        public InputData GetKMAction(KeyBoardMousePlayerAction action)
        {
            if (!KMActionExists(action))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(keyBoardMouseActions)} does not contain the key, make sure you check beforehand!");
            }

            return keyBoardMouseActions[action];
        }

        [JsonProperty]
        private Dictionary<GamePadPlayerAction, InputData> gamepadActions = new Dictionary<GamePadPlayerAction, InputData>()
        {
            {GamePadPlayerAction.Jump, null},
            {GamePadPlayerAction.Reload, null},
            {GamePadPlayerAction.Shoot, null},
            {GamePadPlayerAction.Sprint, null},
        };

        public bool GPActionExists(GamePadPlayerAction action) => gamepadActions.ContainsKey(action);

        public void SetGPAction(GamePadPlayerAction action, InputData path)
        {
            if (!GPActionExists(action))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(gamepadActions)} does not contain the key, make sure you check beforehand!");
            }

            gamepadActions[action] = path;
        }

        public InputData GetGPAction(GamePadPlayerAction action)
        {
            if (!GPActionExists(action))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(gamepadActions)} does not contain the key, make sure you check beforehand!");
            }

            return gamepadActions[action];
        }

        // Audio Settings
        [JsonProperty]
        private Dictionary<AudioChannel, SoundData> audioSettings = new Dictionary<AudioChannel, SoundData>()
        {
            { AudioChannel.Master, null },
            { AudioChannel.Music , null },
            { AudioChannel.SFX , null },
            { AudioChannel.Ambient , null },
            { AudioChannel.UI , null }
        };

        public bool AudioChannelExists(AudioChannel audioChannel) => audioSettings.ContainsKey(audioChannel);

        public void SetAudioSetting(AudioChannel audioChannel, SoundData soundData)
        {
            if (!AudioChannelExists(audioChannel))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(audioSettings)} does not contain the key, make sure you check beforehand!");
            }

            audioSettings[audioChannel] = soundData;
        }

        public SoundData GetAudioSetting(AudioChannel audioChannel)
        {
            if (!AudioChannelExists(audioChannel))
            {
                throw new KeyNotFoundException($"The Dictionary {nameof(audioSettings)} does not contain the key, make sure you check beforehand!");
            }

            return audioSettings[audioChannel];
        }
    }
}
