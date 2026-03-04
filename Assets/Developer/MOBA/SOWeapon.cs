using UnityEngine;

namespace Team3.MOBA
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Team3/Weapon")]
    public class SOWeapon : ScriptableObject
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private GameObject weapon;

        public int ID => id;
        public GameObject Weapon => weapon;

    }
}