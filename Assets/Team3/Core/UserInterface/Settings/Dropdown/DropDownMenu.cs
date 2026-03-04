using TMPro;
using UnityEngine;

namespace Team3.UserInterface.Settings
{
    public class DropDownMenu : IntegerValue
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private DropdownApplyer dropdownApplyer;

        public override int Value => dropdown.value;

        public override void Load(int value)
        {
            dropdown.value = value;
        }

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(dropdownApplyer.Set);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(dropdownApplyer.Set);
        }
    }
}
