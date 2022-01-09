using System;

namespace QoiDotnet.QoiFile;

public interface ISerializableToBytes
{
    public IEnumerable<byte> ToBytes();
}
