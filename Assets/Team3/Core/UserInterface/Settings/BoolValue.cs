using UnityEngine;

namespace Team3.UserInterface.Settings
{
    public abstract class BoolValue : MonoBehaviour
    {
        public abstract bool Value { get; }
        public abstract void Load(bool value);
    }
}
