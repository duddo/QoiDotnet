using System;

namespace QoiDotnet.QoiChunks;

/*
.- QOI_OP_INDEX ----------.
|         Byte[0]         |
|  7  6  5  4  3  2  1  0 |
|-------+-----------------|
|  0  0 |     index       |
`-------------------------`
2-bit tag b00
6-bit index into the color index array: 0..63

A valid encoder must not issue 7 or more consecutive QOI_OP_INDEX chunks to the
index 0, to avoid confusion with the 8 byte end marker.
 */

internal record struct Index(byte IndexPosition) : IChunk
{
    public byte Tag => 0x0;

    public IEnumerable<byte> ToBytes()
    {
        if (IndexPosition < 0 || IndexPosition > 63)
            throw new Exception("Run Length exceeds range: [0, 63]");

        byte res = (byte)(Tag << 6);
        res += (byte)IndexPosition;

        return new byte[] { res };
    }
}
