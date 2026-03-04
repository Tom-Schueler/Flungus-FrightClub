using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.UserInterface.Settings
{
    public class ValueSlider : MonoBehaviour
    {
        [SerializeField] private TMP_InputField valueField;
        [SerializeField] private Slider valueSlider;

        public float Value => valueSlider.value;

        public void Load(float value)
        {
            valueSlider.value = value;
        }

        private void OnEnable()
        {
            valueField.onSubmit.AddListener(OnValueInput);
            valueField.onEndEdit.AddListener(OnValueInput);
            valueSlider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            valueField.onSubmit.RemoveListener(OnValueInput);
            valueField.onEndEdit.RemoveListener(OnValueInput);
            valueSlider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueInput(string newValue)
        {
            valueSlider.value = float.Parse(newValue);
        }

        private void OnValueChanged(float newValue)
        {
            valueField.text = newValue.ToString("F2");
        }
    }
}
