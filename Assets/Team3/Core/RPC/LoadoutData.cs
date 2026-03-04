using Team3.Characters;
using Unity.Netcode;

public struct LoadoutData : INetworkSerializable
{
    public CharacterClasses characterClass;
    public ushort weaponId;
    public ushort ability1Id;
    public ushort ability2Id;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        // enum to byte
        byte cls = (byte)characterClass;
        serializer.SerializeValue(ref cls);           

        // primitive ushort fields are fine
        serializer.SerializeValue(ref weaponId);
        serializer.SerializeValue(ref ability1Id);
        serializer.SerializeValue(ref ability2Id);

        // byte to enum
        if (serializer.IsReader)
            characterClass = (CharacterClasses)cls;
    }
}