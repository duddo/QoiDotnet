using System;

namespace QoiDotnet.QoiFile;

// The byte stream's end is marked with 7 0x00 bytes followed by a single 0x01 byte.

internal record EndMarker : ISerializableToBytes
{
    public IEnumerable<byte> ToBytes()
    {
        for (int i = 0; i < 7; i++)
            yield return 0x0;

        yield return 0x1;
    }
}
