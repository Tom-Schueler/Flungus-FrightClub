 using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Team3.UserInterface.Settings
{
    public class Volume : MonoBehaviour
    {
        [SerializeField] private TMP_InputField valueField;
        [SerializeField] private Slider valueSlider;
        [SerializeField] private Toggle muteToggle;
        [SerializeField] private SODefaultMixer audioMixerReference;
        [SerializeField] private string floatName;

        public float Value => valueSlider.value;
        public bool IsEnabled => muteToggle.isOn;

        public void Load(float value, bool enabled)
        {
            valueSlider.value = value;
            muteToggle.isOn = enabled;
        }

        private void OnEnable()
        {
            valueField.onSubmit.AddListener(OnValueInput);
            valueField.onEndEdit.AddListener(OnValueInput);
            valueSlider.onValueChanged.AddListener(OnValueChanged);
            muteToggle.onValueChanged.AddListener(OnMuteToggled);
        }

        private void OnDisable()
        {
            valueField.onSubmit.RemoveListener(OnValueInput);
            valueField.onEndEdit.RemoveListener(OnValueInput);
            valueSlider.onValueChanged.RemoveListener(OnValueChanged);
            muteToggle.onValueChanged.RemoveListener(OnMuteToggled);
        }

        private void OnValueInput(string newValue)
        {
            valueSlider.value = float.Parse(newValue);
        }

        private void OnValueChanged(float newValue)
        {
            valueField.text = newValue.ToString();
            ApplySetting(newValue);
        }

        private void OnMuteToggled(bool isEnabled)
        {
            if (isEnabled)
            {
                ApplySetting(valueSlider.value);
            }
            else
            {
                audioMixerReference.AudioMixer.SetFloat(floatName, -80f); // mute
            }
        }

        private void ApplySetting(float newValue)
        {
            if (muteToggle.isOn)
            {
                if (newValue == 0)
                {
                    audioMixerReference.AudioMixer.SetFloat(floatName, -80f); // mute
                }
                else
                {
                    audioMixerReference.AudioMixer.SetFloat(floatName, Mathf.Log10(newValue / 100f) * 20f);
                }
            }
        }
    }
}
