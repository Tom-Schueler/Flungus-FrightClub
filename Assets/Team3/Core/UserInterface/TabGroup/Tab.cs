using System;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.UserInterface.TabGroup
{
    [Serializable]
    public class Tab
    {
        [SerializeField] private GameObject group;
        [SerializeField] private Button button;

        public GameObject Group => group;
        public Button Button => button;
    }
}
