using System;

namespace QoiDotnet.QoiChunks;

/*
.- QOI_OP_LUMA -------------------------------------.
|         Byte[0]         |         Byte[1]         |
|  7  6  5  4  3  2  1  0 |  7  6  5  4  3  2  1  0 |
|-------+-----------------+-------------+-----------|
|  1  0 |  green diff     |   dr - dg   |  db - dg  |
`---------------------------------------------------`
2-bit tag b10
6-bit green channel difference from the previous pixel -32..31
4-bit   red channel difference minus green channel difference -8..7
4-bit  blue channel difference minus green channel difference -8..7

The green channel is used to indicate the general direction of change and is
encoded in 6 bits. The red and blue channels (dr and db) base their diffs off
of the green channel difference and are encoded in 4 bits. I.e.:
	dr_dg = (last_px.r - cur_px.r) - (last_px.g - cur_px.g)
	db_dg = (last_px.b - cur_px.b) - (last_px.g - cur_px.g)
The difference to the current channel values are using a wraparound operation,
so "10 - 13" will result in 253, while "250 + 7" will result in 1.
Values are stored as unsigned integers with a bias of 32 for the green channel
and a bias of 8 for the red and blue channel.
The alpha value remains unchanged from the previous pixel.
 */

internal record struct Luma(byte GreenDiff, byte DrDg, byte DbDg) : IChunk
{
    public byte Tag => 0x2;

    public IEnumerable<byte> ToBytes()
    {
        return new byte[0];
    }
}
