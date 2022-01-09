using System;
using QoiDotnet.QoiChunks;

namespace QoiDotnet.QoiFile;

internal record FileData(IEnumerable<byte> InputBytes) : ISerializableToBytes
{
    private readonly List<IChunk> EncodedData = new List<IChunk>();

    public void Add(IChunk chunk)
    {
        EncodedData.Add(chunk);
    }

    public IEnumerable<byte> ToBytes()
    {
        int i = 0;
        foreach (IChunk chunk in EncodedData)
        {
            Console.WriteLine(i + ": " + chunk);
            i++;
            foreach (byte byteData in chunk.ToBytes())
                yield return byteData;
        }
    }
}
