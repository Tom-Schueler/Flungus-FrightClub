using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;


[System.Serializable]
public struct DamageTypeValue : INetworkSerializable
{
    public DamageType type;
    public float value;

    public DamageTypeValue(DamageType typ, float val)
    {
        type = typ;
        value = val;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref type);
        serializer.SerializeValue(ref value);
    }
}