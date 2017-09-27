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
        bool d = true;
        int height = myImage.Height;
        int width = myImage.Width;
        Color firstpixelColor = myBitmap.GetPixel(0, 0);
        float count = 0;
        Color temporary = Color.FromArgb(0, 0, 0);

        for (i = 0; i < height; i++)
        {
            for (j = 0; j < width; j++)
            {
                if (myBitmap.GetPixel(i, j) != firstpixelColor && d)
                {
                    temporary = myBitmap.GetPixel(i, j);
                    d = false;
                    // testai?
                    //Console.WriteLine(temporary);
                    //Console.WriteLine(firstpixelColor);
                }
                if (temporary.B <= myBitmap.GetPixel(i, j).B + 200 && myBitmap.GetPixel(i, j).B != firstpixelColor.B)
                {
                    count++;
                }
            }
        }
        return count / (height * width) * 100;
    }
}
