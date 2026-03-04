using UnityEngine;


[CreateAssetMenu(fileName = "VFX", menuName = "Team3/VFX")]
public class SOVFX : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private GameObject vfx;

    public int ID => id;
    public GameObject VFX => vfx;

    public void SpawnFX(Vector3 position)
    {
        Instantiate(vfx,position,Quaternion.identity);
    }
}
