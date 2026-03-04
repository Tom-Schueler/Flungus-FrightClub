using System.Collections;
using TMPro;
using UnityEngine;  

namespace Team3.Tools
{
    public class CopyToClipbord : MonoBehaviour
    {
        [SerializeField] private TMP_Text lobbyCodeDisplay;
        [SerializeField] private GameObject infoField;
        [SerializeField, Tooltip("In seconds")] private float infoFieldShowTime;

        public void Copy()
        {
            GUIUtility.systemCopyBuffer = lobbyCodeDisplay.text;
            StartCoroutine(ShowInfoField());
        }

        private IEnumerator ShowInfoField()
        {
            infoField.SetActive(true);
            yield return new WaitForSeconds(infoFieldShowTime);
            infoField.SetActive(false);
        }
    }
}
