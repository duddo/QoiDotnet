using System;
using QoiDotnet.QoiChunks;
using QoiDotnet.QoiFile;

/* 
 * TODO 
 * async version of encode
 * encode from stream
 * Header e EOF come record
 * creare eccezioni
 * IChunk.ToBytes() potrebbe essere uno yield?
 * 
*/

/*
      Console.WriteLine((0b11000001).ToString("X"));
      Console.WriteLine(((0x3 << 6)+0x1).ToString("X")); 
 */

namespace QoiDotnet;

public static class Encoder
{
    public static IEnumerable<byte> Encode(QoiFile.QoiFile fileToEncode)
    {
        Rgba previousPixel = GetZeroPixel();
        Rgba[] previouslySeen = new Rgba[64];

        int runLength = -1; // The run-length is stored with a bias of -1

        // loop all pixels
        foreach (Rgba pixel in EnumeratePixelsRgbA(fileToEncode.FileData.InputBytes))
        {
            //check if previously seen
            bool seen = false;
            int index = GetIndexPosition(pixel);
            if (previouslySeen[index] == pixel)
                seen = true;
            else
                previouslySeen[index] = pixel;

            //check for a run
            if (pixel == previousPixel)
            {
                runLength++;
                continue;
            }
            else if (runLength > 0)
            {
                //**** run endend, save it ****//
                IChunk run = new Run(runLength);
                fileToEncode.FileData.Add(run);
            }

            if(seen)
            {
                //**** prev seen, save as Index it ****//
                IChunk run = new QoiChunks.Index(index);
                fileToEncode.FileData.Add(run);
                continue;
            }

            //**** none of above applicable, save complete pixel ****//
            if(previousPixel.A == pixel.A) //alpha channel unchanged, save as RGB
                fileToEncode.FileData.Add(new Rgb(pixel)); 
            else
                fileToEncode.FileData.Add(pixel);

            //cleanup
            runLength = -1;
            previousPixel = pixel;
        }

        //**** save last run if needed ****//
        if (runLength > 0)
        {
            IChunk run = new Run(runLength);
            fileToEncode.FileData.Add(run);
        }

        //**** encoding end ****//
        return fileToEncode.ToBytes();
    }

    private static Rgba GetZeroPixel()
    {
        return new Rgba(0, 0, 0, 255);
    }

    private static int GetIndexPosition(Rgba pixel)
    {
        return (
            pixel.R * 3 +
            pixel.G * 5 +
            pixel.B * 7 +
            pixel.A * 11)
            % 64;
    }

    private static IEnumerable<Rgba> EnumeratePixelsRgbA(IEnumerable<byte> byteArray)
    {
        int i = 0;
        byte r = 0;
        byte g = 0;
        byte b = 0;

        foreach (byte byteElem in byteArray)
        {
            int mod = i % 4;
            if (mod == 0)
                r = byteElem;
            else if (mod == 1)
                g = byteElem;
            else if (mod == 2)
                b = byteElem;
            else if (mod == 3)
                yield return new Rgba(r, g, b, byteElem);

            i++;
        }
    }

}


