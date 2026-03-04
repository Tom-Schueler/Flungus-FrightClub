using System;
using WebSocketSharp;

namespace Team3.SavingLoading.DataStructs
{
    [Serializable]
    public class InputData
    {
        public string path;
        public string displayText;

        public InputData(string path, string displayText)
        {
            this.path = path;
            this.displayText = displayText;
        }
    }
}