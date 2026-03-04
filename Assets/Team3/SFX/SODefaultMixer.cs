using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "DefaultMixer", menuName = "Team3/DefaultMixerHolder")]
public class SODefaultMixer : ScriptableObject
{
    [SerializeField] private AudioMixer audioMixer;
    public AudioMixer AudioMixer => audioMixer;
}
