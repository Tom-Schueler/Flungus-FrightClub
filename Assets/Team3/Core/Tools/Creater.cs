using UnityEngine;

namespace Team3.Multiplayer
{
    public class Creater : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        void Start()
        {
            Instantiate(prefab);

            Destroy(gameObject);
        }
    }
}
