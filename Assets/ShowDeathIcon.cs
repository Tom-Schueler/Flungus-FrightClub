using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeathIcon : MonoBehaviour
{
    [SerializeField]
    private GameObject crosshair;

    [ContextMenu("Spawn a Wave")]
    public void ActivateHitmarker()
    {
       StartCoroutine(FlashCrosshair());
    }

    public IEnumerator FlashCrosshair()
    {
        crosshair.SetActive(true); 
        yield return new WaitForSeconds(4f);
        crosshair.SetActive(false);
    }
}
