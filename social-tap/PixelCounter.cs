using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

public class PixelCounter
{
    Image myImage;
    Bitmap myBitmap;

    public PixelCounter(Image<Bgr, byte> image)
	{
        myImage = image.ToBitmap();
        myBitmap = new Bitmap(myImage);
    }

    public float GetPercentageOfTargetPixels()
    {
        int i, j;
        int height = myImage.Height;
        int width = myImage.Width;
        Color firstpixelColor = myBitmap.GetPixel(0, 0);
        float count = 0;
        Color temporary = Color.FromArgb(0, 0, 0); // r g b


        int r = firstpixelColor.R;
        int g = firstpixelColor.G;
        int b = firstpixelColor.B;
        Console.WriteLine(height +""+width);
        Console.WriteLine(r);
        Console.WriteLine("\n");
        Console.WriteLine(g);
        Console.WriteLine("\n");
        Console.WriteLine(b);
        Console.WriteLine("\n");
        for (i = 0; i < width; i++)
            {
       //     Console.WriteLine("i " +i);
            for (j = 0; j < height; j++)
                {
      //         Console.WriteLine("j " +j);
                if (myBitmap.GetPixel(i, j) == firstpixelColor)
                {
                    count++;
                }

                else if ((myBitmap.GetPixel(i, j).R < (r + 100) && myBitmap.GetPixel(i, j).R > (r - 100)) && (myBitmap.GetPixel(i, j).G < (g + 100) && myBitmap.GetPixel(i, j).G > (g - 100)) && (myBitmap.GetPixel(i, j).B < (b + 100) && myBitmap.GetPixel(i, j).B > (b - 100)))
                {
                    count++;
                }
                else
                {
                 //   Console.WriteLine(myBitmap.GetPixel(i, j));
                }
                }
            }
        Console.WriteLine(count);

        return count / (height * width) * 100;
    }
}
