using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Team3.UserInterface.TabGroup
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private Tab[] tabs;
        [SerializeField] private int initialTabIndex;

        private GameObject currentTab;
        private Dictionary<Tab, UnityAction> listeners = new Dictionary<Tab, UnityAction>();

        private void OnEnable()
        {
            foreach (Tab tab in tabs)
            {
                tab.Group.SetActive(false);
            }

            currentTab = (initialTabIndex < tabs.Length) ? tabs[initialTabIndex].Group : tabs[0].Group;

            foreach (Tab tab in tabs)
            {
                UnityAction listener = ShowTab(tab.Group);

                tab.Button.onClick.AddListener(listener);
                listeners.Add(tab, listener);
            }

            currentTab.SetActive(true);
        }

        private void OnDisable()
        {
            foreach (KeyValuePair<Tab, UnityAction> listener in listeners)
            {
                listener.Key.Button.onClick.RemoveListener(listener.Value);
            }

            listeners.Clear();
        }

        private UnityAction ShowTab(GameObject newTab) => () =>
        {
            currentTab.SetActive(false);
            currentTab = newTab;
            newTab.SetActive(true);
        };
    }
}
