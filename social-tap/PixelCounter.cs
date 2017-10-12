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
        Console.WriteLine(height + "" + width);
        Console.WriteLine(r);
        Console.WriteLine("\n");
        Console.WriteLine(g);
        Console.WriteLine("\n");
        Console.WriteLine(b);
        Console.WriteLine("\n");
        float count1 = 0;
        float beerx;
        float beerxmax;
        int beery;
        float mugPixels = width * height;
        for (i = 0; i < width; i++) // plotis
        {
            //     Console.WriteLine("i " +i);
            for (j = 0; j < height; j++) //aukštis
            {
                //         Console.WriteLine("j " +j);
                if (myBitmap.GetPixel(i, j) == firstpixelColor) // GetPixel (plotis, aukštis)
                {
                    mugPixels--;
                }
                else if ((myBitmap.GetPixel(i, j).R < (r + 100) && myBitmap.GetPixel(i, j).R > (r - 100)) && (myBitmap.GetPixel(i, j).G < (g + 100) && myBitmap.GetPixel(i, j).G > (g - 100)) && (myBitmap.GetPixel(i, j).B < (b + 100) && myBitmap.GetPixel(i, j).B > (b - 100)))
                {
                    //  Count++; // NE alaus pixeliai
                    // Console.WriteLine(myBitmap.GetPixel(i, j));
                }
                else
                {

                    beerx = i;
                    beerxmax = i;
                    beery = j;

                    beery = beery + 50;
                    i = i + 30;
                    while (!((myBitmap.GetPixel(i, j).R < (r + 100) && myBitmap.GetPixel(i, j).R > (r - 100)) && (myBitmap.GetPixel(i, j).G < (g + 100) && myBitmap.GetPixel(i, j).G > (g - 100)) && (myBitmap.GetPixel(i, j).B < (b + 100) && myBitmap.GetPixel(i, j).B > (b - 100))))
                    {
                        beerxmax++;
                        i++;
                    }
                    Console.WriteLine(beerx);
                    Console.WriteLine(beerxmax);
                    beerx = beerxmax / 2 + beerx / 2;
                    i = (int)beerx;
                    Console.WriteLine(i);
                    while (!((myBitmap.GetPixel(i, beery).R < (r + 100) && myBitmap.GetPixel(i, beery).R > (r - 100)) && (myBitmap.GetPixel(i, beery).G < (g + 100) && myBitmap.GetPixel(i, beery).G > (g - 100)) && (myBitmap.GetPixel(i, beery).B < (b + 100) && myBitmap.GetPixel(i, beery).B > (b - 100))))
                    {
                        count++;
                        beery++;
                    }

                    beery = j;
                    while (!((myBitmap.GetPixel(i, beery).R < (r + 100) && myBitmap.GetPixel(i, beery).R > (r - 100)) && (myBitmap.GetPixel(i, beery).G < (g + 100) && myBitmap.GetPixel(i, beery).G > (g - 100)) && (myBitmap.GetPixel(i, beery).B < (b + 100) && myBitmap.GetPixel(i, beery).B > (b - 100))))
                    {
                        count++;
                        beery--;
                    }
                    while (myBitmap.GetPixel(i, beery) != firstpixelColor)
                    {
                        count1++;
                        beery--;
                    }

                    i = 2100;
                    j = 3000;
                    //b iki 130!!!
                    //randa pirmą alaus reikia imti liniją per 50px į šoną
                    //   Console.WriteLine(myBitmap.GetPixel(i, j));
                }
            }
        }
        // Console.WriteLine(Count);
        // return Count / (height * width) * 100;
        return 100 / (count + count1) * count;
    }
}