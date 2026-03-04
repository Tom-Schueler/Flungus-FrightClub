using Unity.Netcode;
using UnityEngine;

public class host : MonoBehaviour
{
    public void OnHost()
    {
        NetworkManager.Singleton.StartHost();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
