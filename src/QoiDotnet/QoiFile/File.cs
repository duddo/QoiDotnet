using System;

namespace QoiDotnet.QoiFile;

public class QoiFile : ISerializableToBytes
{
    internal readonly FileData FileData;

    private readonly Header Header;
    private readonly EndMarker EOF = new EndMarker();

    public QoiFile(int height, int width, Channels channels, IEnumerable<byte> imageData)
    {
        Header = new Header(
            (uint)width,
            (uint)height,
            channels);

        FileData = new FileData(imageData);
    }

    public IEnumerable<byte> ToBytes()
    {
        foreach (byte byteItem in Header.ToBytes())
            yield return byteItem;

        foreach (byte byteItem in FileData.ToBytes())
            yield return byteItem;

        foreach (byte byteItem in EOF.ToBytes())
            yield return byteItem;
    }
}
