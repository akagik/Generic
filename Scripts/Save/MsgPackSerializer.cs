using MsgPack;

/// <summary>
/// MsgPack によるシリアライズ・ディシリアライズ.
/// </summary>
public class MsgPackSerializer : ISaveSerializer
{
    public byte[] Serialize<T>(T target) {
        ObjectPacker packer = new ObjectPacker();
        return packer.Pack(target);
    }

    public T Deserialize<T>(byte[] ivBytes) {
        ObjectPacker packer = new ObjectPacker();
        return packer.Unpack<T>(ivBytes);
    }
}
