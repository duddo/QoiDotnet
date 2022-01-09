using System;

namespace QoiDotnet.QoiChunks;

/*
.- QOI_OP_RGBA ---------------------------------------------------.
|         Byte[0]         | Byte[1] | Byte[2] | Byte[3] | Byte[4] |
|  7  6  5  4  3  2  1  0 | 7 .. 0  | 7 .. 0  | 7 .. 0  | 7 .. 0  |
|-------------------------+---------+---------+---------+---------|
|  1  1  1  1  1  1  1  1 |   red   |  green  |  blue   |  alpha  |
`-----------------------------------------------------------------`
8-bit tag b11111111
8-bit   red channel value
8-bit green channel value
8-bit  blue channel value
8-bit alpha channel value 
*/

internal record struct Rgba(byte R, byte G, byte B, byte A) : IChunk
{
    public byte Tag => 0xFF;

    public IEnumerable<byte> ToBytes()
    {
        return new byte[] { Tag, R, G, B, A };
    }
}
