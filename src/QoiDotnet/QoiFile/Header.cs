using System;
using System.Text;

namespace QoiDotnet.QoiFile;

public enum Channels
{
    RGB = 3,
    RGBA = 4,
}

internal enum ColorSpaces
{
    LinearAlpha = 0,
    AllLinear = 1,
}

internal record Header(
    uint Width,
    uint Height,
    Channels Channel,
    ColorSpaces Colorspace = ColorSpaces.LinearAlpha) : ISerializableToBytes
{
    const string Magic = "qoif";

    public IEnumerable<byte> ToBytes()
    {
        IEnumerable<byte> magic =
            Encoding.ASCII.GetBytes(Magic);             //char4 magic bytes "qoif"
        byte[] width = BitConverter.GetBytes(Width);    //uint32_t  image width in pixels (BE)
        byte[] height = BitConverter.GetBytes(Height);  //uint32_t  image height in pixels (BE)
        byte channel = (byte)Channel;                   //uint8_t   3 = RGB, 4 = RGBA
        byte colorspace = (byte)Colorspace;             //uint8_t   0 = sRGB with linear alpha
                                                        //          1 = all channels linear
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(width);
            Array.Reverse(height);
        }

        List<byte> res = new List<byte>();
        res.AddRange(magic);
        res.AddRange(height);
        res.AddRange(width);
        res.Add(channel);
        res.Add(colorspace);
        return res;
    }
}
