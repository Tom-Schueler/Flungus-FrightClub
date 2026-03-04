using Unity.Netcode;
using UnityEngine;

public class AudioManager : MonoBehaviour   
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        // Singleton init
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional if persistent
    }

    public void PlaySoundLocal(int sfxID)
    {
        audioSource.PlayOneShot(PerkDatabase.Instance.GetSFXByID(sfxID).SFX);
    }

    public void PlayTrack(AudioClip clip)
    {
        gameObject.GetComponent<AudioListener>().enabled = false;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
