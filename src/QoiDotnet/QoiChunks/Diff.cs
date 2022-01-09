using System;

namespace QoiDotnet.QoiChunks;

/*
.- QOI_OP_DIFF -----------.
|         Byte[0]         |
|  7  6  5  4  3  2  1  0 |
|-------+-----+-----+-----|
|  0  1 |  dr |  dg |  db |
`-------------------------`
2-bit tag b01
2-bit   red channel difference from the previous pixel between -2..1
2-bit green channel difference from the previous pixel between -2..1
2-bit  blue channel difference from the previous pixel between -2..1

The difference to the current channel values are using a wraparound operation,
so "1 - 2" will result in 255, while "255 + 1" will result in 0.
Values are stored as unsigned integers with a bias of 2. E.g. -2 is stored as
0 (b00). 1 is stored as 3 (b11).
The alpha value remains unchanged from the previous pixel.
 */

internal record struct Diff(byte Rdiff, byte Gdiff, byte Bdiff) : IChunk
{
    public byte Tag => 0x1;

    public IEnumerable<byte> ToBytes()
    {
        return new byte[0];
    }
}
