using System;

namespace Team3.SavingLoading.DataStructs
{
    [Serializable]
    public class SoundData
    {
        public float volume;
        public bool enabled;

        public SoundData(float volume, bool enabled)
        {
            this.volume = volume;
            this.enabled = enabled;
        }
    }
}
