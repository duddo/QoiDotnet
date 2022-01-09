using System;

namespace QoiDotnet.QoiChunks;

/*
.- QOI_OP_RUN ------------.
|         Byte[0]         |
|  7  6  5  4  3  2  1  0 |
|-------+-----------------|
|  1  1 |       run       |
`-------------------------`
2-bit tag b11
6-bit run-length repeating the previous pixel: 1..62

The run-length is stored with a bias of -1. Note that the run-lengths 63 and 64
(b111110 and b111111) are illegal as they are occupied by the QOI_OP_RGB and
QOI_OP_RGBA tags.
 */

internal record struct Run(int RunLength) : IChunk
{
    public byte Tag => 0x3;

    public IEnumerable<byte> ToBytes()
    {
        if (RunLength < 1 || RunLength > 62)
            throw new Exception("Run Length exceeds range: [1, 62]");

        byte res = (byte)(Tag << 6);
        res += (byte)RunLength;

        return new byte[] { res };
    }
}
