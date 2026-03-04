using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BootstrapGame : MonoBehaviour
{
    public UnityEvent OnGameStart;

    public void Start()
    {
        OnGameStart?.Invoke();
    }

    public void FreeBootstrap()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    private void Update()
    {
        Debug.Log("Hello");
    }
}
