using System;
using System.Collections.Generic;
using Team3.SavingLoading.SaveData;
using Team3.SavingLoading.DataStructs;
using Team3.UserInterface.Settings;
using UnityEngine;
using UnityEngine.Audio;

namespace Team3.SavingLoading.SaveableBehaviours
{
    public class SaveableVolume : SaveableBehaviour
    {
        [SerializeField] private AudioChannel channel;
        [SerializeField] private Volume volume;
        [SerializeField] private SODefaultMixer audioMixerReference;
        [SerializeField] private string floatName;

        public override void Save()
        {
            if (!SettingsData.Singleton.AudioChannelExists(channel))
            {
                throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
            }

            SettingsData.Singleton.SetAudioSetting(channel, new SoundData(volume.Value, volume.IsEnabled));
        }

        public override void Load()
        {
            if (!SettingsData.Singleton.AudioChannelExists(channel))
            {
                throw new KeyNotFoundException($"The key does not exist. this should never happen unless you forgot to add it to the dictionarry.");
            }

            SoundData soundData = SettingsData.Singleton.GetAudioSetting(channel);

            if (soundData == null)
            {
                // load default
                Debug.LogError("Faild to load defualt setting");
                throw new NotImplementedException($"Class: {nameof(SaveableVolume)}, Method: {nameof(Load)} has not yet implemented, what happens if it cant find the setting");
            }

            if (volume != null)
            {
                volume.Load(soundData.volume, soundData.enabled);
            }

            if (soundData.enabled)
            {
                if (soundData.volume == 0f)
                {
                    audioMixerReference.AudioMixer.SetFloat(floatName, -80f); // mute
                }
                else
                {
                    audioMixerReference.AudioMixer.SetFloat(floatName, Mathf.Log10(soundData.volume / 100f) * 20f);
                }
            }
            else
            {
                audioMixerReference.AudioMixer.SetFloat(floatName, -80f); // mute
            }
        }
    }
}
