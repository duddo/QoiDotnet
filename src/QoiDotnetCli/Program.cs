using QoiDotnet;
using QoiDotnet.QoiFile;
using StbImageSharp;

namespace QoiDotnetCli;

public class Program
{
    public static void Main(string[] args)
    {
        //if (args.Length < 1)
        //    return;
        //string inputPath = args[0].Trim();

        string fileName = "4px-gray";
        string inputPath = $"/Users/davide/src/qoi_test_images/{fileName}.png";
        Console.WriteLine("Reading: " + inputPath);

        FileInfo fileInput = new FileInfo(inputPath);

        if (fileInput.Exists == false)
        {
            Console.WriteLine("File not found!");
            return;
        }

        using Stream inputStream = fileInput.OpenRead();

        ImageResult stbImageResult = ImageResult.FromStream(inputStream, ColorComponents.RedGreenBlueAlpha);

        QoiFile qoiFile = new QoiFile(
            stbImageResult.Width,
            stbImageResult.Height,
            QoiDotnet.QoiFile.Channels.RGBA,
            stbImageResult.Data);

        IEnumerable<byte> encodedBytes = Encoder.Encode(qoiFile);

        var toWrite = encodedBytes.ToArray();
        string outputFile = "/Users/davide/src/qoi_test_images/Output.qoi";
        File.WriteAllBytes(outputFile, toWrite);
        Console.WriteLine($"Written {toWrite.Length} bytes to Output.qoi");

        Console.WriteLine("End.");

        //print example bytes
        Console.WriteLine("");
        Console.Write("Reference:");
        string f1 = $"/Users/davide/src/qoi_test_images/{fileName}.qoi";
        var bb1 = File.ReadAllBytes(f1).Skip(14).SkipLast(8);
        foreach (var b1 in bb1)
            Console.Write(b1.ToString("X") + " ");

        //print output bytes
        Console.WriteLine("");
        Console.Write("Output:   ");
        var bb = File.ReadAllBytes(outputFile).Skip(14).SkipLast(8);
        foreach (var b in bb)
            Console.Write(b.ToString("X") + " ");
        return;
    }
}
