using System;
using QoiDotnet.QoiFile;

namespace QoiDotnet.QoiChunks;

internal interface IChunk : ISerializableToBytes
{
    public byte Tag { get; }
}
