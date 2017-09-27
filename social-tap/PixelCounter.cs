using System;

public class PixelCounter
{
	public PixelCounter()
	{
        System.Drawing.Image myImage = image.ToBitmap();

        Bitmap myBitmap = new Bitmap(myImage);

        int i, j;
        bool d = true;
        int height = myImage.Height;
        int width = myImage.Width;
        Color firstpixelColor = myBitmap.GetPixel(0, 0);
        float count = 0; // man jau nusibodo 
        Color temporary = Color.FromArgb(0, 0, 0); // o gal visai ir nieko 
        for (i = 0; i < height; i++)
        {
            for (j = 0; j < width; j++)
            {
                if (myBitmap.GetPixel(i, j) != firstpixelColor && d)
                {
                    temporary = myBitmap.GetPixel(i, j);
                    d = false;
                    Console.WriteLine(temporary);
                    Console.WriteLine(firstpixelColor);
                }
                if (temporary.B <= myBitmap.GetPixel(i, j).B + 200 && myBitmap.GetPixel(i, j).B != firstpixelColor.B)
                {
                    count++;
                }
            }
        }

    }
}
