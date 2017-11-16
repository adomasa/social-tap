using System;
using Android.Graphics;
using Color = System.Drawing.Color;
using System.ComponentModel;

public class PixelCounter : IDisposable
{
    Bitmap bitmap;

    private bool disposed = false; // Track whether Dispose has been called.

    public PixelCounter()
    {
    }

    public bool Testukas(int a) // Ištrinsiu
    {
        return a == 5;
    }

    public PixelCounter(Bitmap image)
    {
        bitmap = image.Copy(Bitmap.Config.Argb8888, true);
    }

    static readonly Color outsideBox = Color.FromArgb(0, 50, 100); // kvadrato išorinio pixelio spalva 0 50 100                                                                
    static readonly Color insideBox = Color.FromArgb(100, 50, 0); // kvadrato vidinio pixelio splava  100 50 0
    const int lagerBeerRmin = 170;
    const int lagerBeerGmin = 110;
    const int lagerBeerGmax = 200;
    const int lagerBeerBmax = 90;
    Color checkPixel;

    /*
      220       110-130   12-50
      180-230   160-200   0-25
      220-240   140-180   0-70
      170-250   130-200   30-90 
   */

    public int GetPercentageOfTargetPixels()
    {
        int height = bitmap.Height; // aukštis
        int width = bitmap.Width; // plotis
        int startY = height / 2; // vieta, nuo kurios pradedam ieškoti box'o
        int? pixelX = null, pixelY = null; // inside box pikselio X ir Y koodinatės

        FindBox(startY, height, width, ref pixelX, ref pixelY); // susirandam insideBox pikselio koordinates

        BoxExistsException(pixelX, pixelY); // tikrinam, ar nubrėžtas box'as

        int xLeft = 0, xRight = 0, yUp = 0, yDown = 0; // insideBox kraštinių kordinatės

        xLeft = (int)pixelX; // kairė

        checkPixel = Color.FromArgb(bitmap.GetPixel((int)pixelX, (int)pixelY));
        yUp = YcoordUp(yUp, ref pixelX, ref pixelY); // viršus

        checkPixel = Color.FromArgb(bitmap.GetPixel((int)pixelX, (int)pixelY));
        xRight = XcoordRight(xRight, ref pixelX, ref pixelY); // dešinė

        checkPixel = Color.FromArgb(bitmap.GetPixel((int)pixelX, (int)pixelY));
        yDown = YcoordDown(yDown, ref pixelX, ref pixelY); // apačia

        height = yDown - yUp; // insideBox aukštis
        width = xRight - xLeft; // insideBox plotis

        int? levelofBearDown = null, levelofBearUp = null; // pixelis, kuriame prasideda alus ir kuriame baigiasi
        int[] proc = new int[10]; // išsaugom, kiek proc alaus yra
        int times = 0; // TIKRINTI!!! kiek kartų įvyko. Gal galima pakeisti konstanta
        FindBeerLevel(height, width, xLeft, yDown, yUp, ref levelofBearDown, ref levelofBearUp, ref proc, ref times); // surandam kur prasideda ir kur baigiasi alus
        BeerExistsException(levelofBearDown, levelofBearUp); // patikrinam, ar nuotrauko išvis yra alus

        DrawLineUp((int)levelofBearUp, xLeft, width); // TAISYTI!!! nubrėžia liniją, iki kur įpilta alaus

        DrawLineDown((int)levelofBearDown, xLeft, width); // TAISYTI!!! nubrėžia liniją, nuo kur prasideda alus

        int bestProcIndex = BestProcIndex(times, proc); // grąžina tiksliausios reikšmės indeksą

        int best = 0;
        int finalResult = FinalResult(times, bestProcIndex, proc, ref best);
        finalResult = finalResult / best;
        return finalResult;
    }


    public void FindBox(int startY, int height, int width, ref int? pixelX, ref int? pixelY)
    {
        bool up = true;
        int addNumber = 1;
        while (startY < height)
        {
            for (int i = 0; i < width; i++)
            {
                checkPixel = Color.FromArgb(bitmap.GetPixel(i, startY));
                if (checkPixel.R == outsideBox.R && checkPixel.G == outsideBox.G && checkPixel.B == outsideBox.B)
                {
                    checkPixel = Color.FromArgb(bitmap.GetPixel((i + 1), startY));
                    if (checkPixel.R == insideBox.R && checkPixel.G == insideBox.G && checkPixel.B == insideBox.B)
                    {
                        pixelX = i + 1;
                        pixelY = startY; // tinkamas pixelis X ir Y 
                    }
                }
            }
            if (up)
            {
                startY = startY + addNumber;
                up = false;
            }
            else
            {
                startY = startY - addNumber;
                up = true;
            }
            addNumber++;
            if (pixelX.HasValue || pixelY.HasValue)
            {
                startY = height;
            }
        }
    }

    public int YcoordUp(int yUp, ref int? pixelX, ref int? pixelY)
    {
        while (checkPixel.R == insideBox.R && checkPixel.G == insideBox.G && checkPixel.B == insideBox.B)
        {
            yUp = (int)pixelY;
            pixelY--;
            checkPixel = Color.FromArgb(bitmap.GetPixel((int)pixelX, (int)pixelY));
        }
        pixelY++;
        return yUp;
    }

    public int XcoordRight(int xRight, ref int? pixelX, ref int? pixelY)
    {
        while (checkPixel.R == insideBox.R && checkPixel.G == insideBox.G && checkPixel.B == insideBox.B)
        {
            xRight = (int)pixelX;
            pixelX++;
            checkPixel = Color.FromArgb(bitmap.GetPixel((int)pixelX, (int)pixelY));
        }
        pixelX--;
        return xRight;
    }

    public int YcoordDown(int yDown, ref int? pixelX, ref int? pixelY)
    {
        while (checkPixel.R == insideBox.R && checkPixel.G == insideBox.G && checkPixel.B == insideBox.B)
        {
            yDown = (int)pixelY;
            pixelY++;
            checkPixel = Color.FromArgb(bitmap.GetPixel((int)pixelX, (int)pixelY));
        }
        return yDown;
    }

    public int BestProcIndex(int times, int[] proc)
    {
        int bestProcIndex = 0;
        int best = 0, bestMax = 0;
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < times; j++)
            {
                if (i != j && (proc[i] - 5) < proc[j] && (proc[i] + 5) > proc[j])
                {
                    best++;
                }
            }
            if (best > bestMax)
            {
                bestProcIndex = i;
                bestMax = best;
            }
            best = 0;
        }
        return bestProcIndex;
    }

    public int FinalResult(int times, int bestProcIndex, int[] proc, ref int best)
    {
        int finalResult = 0;
        for (int i = 0; i < times; i++)
        {
            if ((proc[bestProcIndex] - 5) < proc[i] && (proc[bestProcIndex] + 5) > proc[i])
            {
                finalResult = finalResult + proc[i];
                best++;
            }
        }
        return finalResult;
    }

    public void BoxExistsException(int? pixelX, int? pixelY)
    {
        if (!pixelX.HasValue || !pixelY.HasValue)
        {
            throw new ArgumentException("There is no box in photo", "pixelX and PixelY");
        }
    }

    public void BeerExistsException(int? levelofBearUp, int? levelofBearDown)
    {
        if (!levelofBearDown.HasValue || !levelofBearUp.HasValue)
        {
            throw new ArgumentException("There is no beer in photo", "pixelX and PixelY");
        }
    }

    public void DrawLineUp(int? levelofBearUp, int xLeft, int width)
    {
        for (int i = ((int)levelofBearUp - 1); i < ((int)levelofBearUp + 2); i++)
        {
            for (int j = xLeft; j < (width + xLeft); j++)
            {
                bitmap.SetPixel(j, i, Android.Graphics.Color.Red);
            }
        }

    }

    public void DrawLineDown(int? levelofBearDown, int xLeft, int width)
    {
        for (int i = ((int)levelofBearDown - 1); i < ((int)levelofBearDown + 2); i++)
        {
            for (int j = xLeft; j < (width + xLeft); j++)
            {
                bitmap.SetPixel(j, i, Android.Graphics.Color.Red);
            }
        }

    }

    public Bitmap getProcessedImage()
    {
        return bitmap;
    }

    public void FindBeerLevel(int height, int width, int xLeft, int yDown, int yUp, ref int? levelofBearDown, ref int? levelofBearUp, ref int[] proc, ref int times)
    {
        bool startBear = false;
        bool correctPixel = true;
        int count = 0; // !
        int distanceX = width / 10; // paklaidos dydis
        int distanceY = height / 10;
        int k;
        int j1;

        for (int i = (xLeft + distanceX); i < (width + xLeft); i = (i + distanceX)) // plotis
        {
            for (int j = yDown; j > yUp; j--) // aukštis
            {
                //  Console.WriteLine("i: " + i + "j:" + j);
                if (Color.FromArgb(bitmap.GetPixel(i, j)).R > lagerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).G > lagerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < lagerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < lagerBeerBmax && !startBear)
                {
                    startBear = true; // reiškias baigėsi dugnas ir prasideda alus
                    levelofBearDown = j;
                    levelofBearUp = j;
                }
                if (Color.FromArgb(bitmap.GetPixel(i, j)).R > lagerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).G > lagerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < lagerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < lagerBeerBmax)
                {
                    levelofBearUp = j;
                }
                else
                {
                    count = 0;
                    k = 0;
                    j1 = j;
                    while (k < distanceY && !(Color.FromArgb(bitmap.GetPixel(i, j)).R == outsideBox.R && Color.FromArgb(bitmap.GetPixel(i, j)).G == outsideBox.G && Color.FromArgb(bitmap.GetPixel(i, j)).B == outsideBox.B)) // ar gerai?
                    {
                        if (Color.FromArgb(bitmap.GetPixel(i, j)).R > lagerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).G > lagerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < lagerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < lagerBeerBmax)
                        {
                            count++;
                        }
                        j1--;
                        k++;
                    }
                    if ((count * 2) >= distanceY)
                    {
                        correctPixel = true;
                        levelofBearUp = j;
                    }
                    else
                    {
                        correctPixel = false;
                    }
                }
                /*   if (!correctPixel)
                   {
                       j = yUp; // baisu dėl šito
                   }*/
            }
            correctPixel = true;
            startBear = false;
            // Console.WriteLine("beer level: " + (levelofBearUp) + " dugnas " + levelofBearDown);
            proc[times] = 100 * ((int)levelofBearDown - (int)levelofBearUp) / ((int)levelofBearDown - yUp);
            times++;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                bitmap.Dispose();
            }
            disposed = true; // Note disposing has been done.
        }
    }

    ~PixelCounter()
    {
        Dispose(false);
    }
}