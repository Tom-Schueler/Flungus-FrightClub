using System;
using Unity.Netcode;

namespace Team3.Multiplayer
{
    [Serializable]
    public class ScoreboardInfo : INetworkSerializable
    {
        private string name;
        private int points;

        public string Name => name;
        public int Points => points;

        public ScoreboardInfo() {}

        public ScoreboardInfo(string name, int points)
        {
            this.name = name;
            this.points = points;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref name);
            serializer.SerializeValue(ref points);
        }
    }
}
