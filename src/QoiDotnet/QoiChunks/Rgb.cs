using System;

namespace QoiDotnet.QoiChunks;

/*
.- QOI_OP_RGB ------------------------------------------.
|         Byte[0]         | Byte[1] | Byte[2] | Byte[3] |
|  7  6  5  4  3  2  1  0 | 7 .. 0  | 7 .. 0  | 7 .. 0  |
|-------------------------+---------+---------+---------|
|  1  1  1  1  1  1  1  0 |   red   |  green  |  blue   |
`-------------------------------------------------------`
8-bit tag b11111110
8-bit   red channel value
8-bit green channel value
8-bit  blue channel value

The alpha value remains unchanged from the previous pixel.
 */

internal record struct Rgb(byte R, byte G, byte B) : IChunk
{
    public byte Tag => 0xFE;

    public Rgb(Rgba pixel)
        : this(pixel.R, pixel.G, pixel.B)
    {
    }

    public IEnumerable<byte> ToBytes()
    {
        return new byte[] { Tag, R, G, B };
    }
}
