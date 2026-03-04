using UnityEngine;

namespace Team3.MOBA
{
    public class Pitcher : MonoBehaviour
    {
        [SerializeField]
        AudioSource audioSource;
       public void ChangePitch()
        {
           audioSource.pitch = Random.Range(0.8f, 1.2f);
        }
    }
}