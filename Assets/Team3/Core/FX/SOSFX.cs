using UnityEngine;


[CreateAssetMenu(fileName = "SFX", menuName = "Team3/SFX")]
public class SOSFX : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private AudioClip sfx;

    public int ID => id;
    public AudioClip SFX => sfx;

    public void SpawnFX(Vector3 position)
    {
        Instantiate(sfx,position,Quaternion.identity);
    }
}
