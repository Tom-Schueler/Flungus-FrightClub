using UnityEngine;

namespace Team3.UserInterface.Settings
{
    public abstract class IntegerValue : MonoBehaviour
    {
        public abstract int Value { get; }
        public abstract void Load(int value);
    }
}
