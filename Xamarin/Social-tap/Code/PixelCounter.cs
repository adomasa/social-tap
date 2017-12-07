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

    const int darkerBeerRmin = 70;
    const int darkerBeerRmax = 140;
    const int darkerBeerGmin = 25;
    const int darkerBeerGmax = 70;
    const int darkerBeerBmax = 40;

    Color checkPixel;


    /*
R    G   B       R-G     R-B     G-B
99	52	18		1,904	5,500	2,889
83	48	16		1,729	5,188	3,000
113	68	27		1,662	4,185	2,519
						
106	56	19		1,893	5,579	2,947
114	65	33		1,754	3,455	1,970
146	83	30		1,759	4,867	2,767
141	81	27		1,741	5,222	3,000
97	52	21		1,865	4,619	2,476
						
26	21	17		1,238	1,529	1,235
40	20	13		2,000	3,077	1,538
27	17	15		1,588	1,800	1,133
61	23	12		2,652	5,083	1,917
						
108	63	22		1,714	4,909	2,864
172	117	37		1,470	4,649	3,162
101	61	35		1,656	2,886	1,743
120	71	31		1,690	3,871	2,290
123	88	32		1,398	3,844	2,750
						
98,647	58,000	23,824		1,748	4,133	2,365
						
		B > 120				

        */

    /*
      220       110-130   12-50
      180-230   160-200   0-25
      220-240   140-180   0-70
      170-250   130-200   30-90 
   */

    public int? GetPercentageOfTargetPixels()
    {
        int height = bitmap.Height; // aukštis
        int width = bitmap.Width; // plotis
        int startY = height / 2; // vieta, nuo kurios pradedam ieškoti box'o
        int? pixelX = null, pixelY = null; // inside box pikselio X ir Y koodinatės

        FindBox(startY, height, width, ref pixelX, ref pixelY); // susirandam insideBox pikselio koordinates

        try
        {
            BoxExistsException(pixelX, pixelY); // tikrinam, ar nubrėžtas box'as
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("{0} First exception caught.", e);
            return null;
        }
       
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
        int times = 0; // kiek kartų įvyko

        FindBeerLevelLager(height, width, xLeft, yDown, yUp, ref levelofBearDown, ref levelofBearUp, ref proc, ref times); // surandam kur prasideda ir kur baigiasi alus, išsaugom procentus
     //   Console.WriteLine("Times" + times + "d" + levelofBearDown + "u" + levelofBearUp);
        if (!levelofBearDown.HasValue || !levelofBearUp.HasValue) // Naudosim, jei bus kitokios spalvos alus
        {
      //      FindBeerLevelDarker(height, width, xLeft, yDown, yUp, ref levelofBearDown, ref levelofBearUp, ref proc, ref times);
        }

        try
        {
            BeerExistsException(levelofBearDown, levelofBearUp); // patikrinam, ar nuotraukoj išvis yra alus
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("{0} First exception caught.", e);
            return 0;
        }

        //T
        DrawLineUp((int)levelofBearUp, xLeft, width); // Nubrėžia liniją, iki kur įpilta alaus

        //T
        DrawLineDown((int)levelofBearDown, xLeft, width); // Nubrėžia liniją, nuo kur prasideda alus

        int bestProcIndex = BestProcIndex(times, proc); // grąžina tiksliausios reikšmės indeksą

        int numberOfBestProc = 0; // išsaugo, kiek buvo tiksliausių reikšmių
        int finalResult = FinalResult(times, bestProcIndex, proc, ref numberOfBestProc); // grąžina tiksliausių reikšmių vidurkį
        finalResult = finalResult / numberOfBestProc; // apskaičiuoja galutinį alaus procentą
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

    public int FinalResult(int times, int bestProcIndex, int[] proc, ref int numberOfBestProc)
    {
        int finalResult = 0;
        for (int i = 0; i < times; i++)
        {
            if ((proc[bestProcIndex] - 5) < proc[i] && (proc[bestProcIndex] + 5) > proc[i])
            {
                finalResult = finalResult + proc[i];
                numberOfBestProc++;
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

    public void FindBeerLevelLager(int height, int width, int xLeft, int yDown, int yUp, ref int? levelofBearDown, ref int? levelofBearUp, ref int[] proc, ref int times)
    {
        bool startBear = false; // patikrinam ar prasidėjo alus


       bool correctPixel = true; // naudoti jei norėsim nutraukti 


        int count = 0; // naudojamas tikrinimui ar nuotraukoje blogas apšvietimas ar jau baigėsi alus
        int distanceX = width / 20; // kas kokį pločio atstumą tikrinam alaus lygį
        int distanceY = height / 10; // aukščio paklaidos dydis
        int distanceYDown = height / 15; // aukščio paklaidos dydis pirmajam pikseliui

        int k = 0; // some variables apibrežti
        int j1;
        int up = 0;
        int down = 0;
        int counting = 0;


        for (int i = (xLeft + distanceX*4); i < (width + xLeft - distanceX*3); i = (i + distanceX)) // plotis (tikrinam jo 20---80 dalį)
        {
            for (int j = yDown; j > yUp; j--) // aukštis
            {
                //  Console.WriteLine("i: " + i + "j:" + j);
                if (Color.FromArgb(bitmap.GetPixel(i, j)).R > lagerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).G > lagerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < lagerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < lagerBeerBmax && !startBear) // ieškomas pirmas alaus pikselis nuo dugno
                {
                    while (k < distanceY && !(Color.FromArgb(bitmap.GetPixel(i, j)).R == outsideBox.R && Color.FromArgb(bitmap.GetPixel(i, j)).G == outsideBox.G && Color.FromArgb(bitmap.GetPixel(i, j)).B == outsideBox.B)) // tikrinam ar ten tikrai prasidėjo alus ar buvo atsitiktinis pikselis
                        startBear = true; // reiškias baigėsi dugnas ir prasideda alus
                    down = j; //TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                    up = j;
                }
                if (Color.FromArgb(bitmap.GetPixel(i, j)).R > lagerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).G > lagerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < lagerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < lagerBeerBmax)
                {
                    up = j;
                    bitmap.SetPixel(i, j, Android.Graphics.Color.Red); // for testing
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
                    if ((count * 2) >= distanceY) // jei bent pusė pikselių buvo alaus, tada ok
                    {
                        correctPixel = true;
                        up = j;
                    }
                    else
                    {
                        correctPixel = false; // tai čia galima ir nutraukti
                    }
                }
                /*   if (!correctPixel)
                   {
                       j = yUp; // baisu dėl šito
                   }*/
            }
            if (times ==2)
            {
                levelofBearUp = up;//TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                levelofBearDown = down; //TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                counting++;
            }
            if (times>2 && times<9)
            {
                levelofBearUp = levelofBearUp + up;//TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                levelofBearDown = levelofBearDown + down; //TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                counting++;
            }
  
            correctPixel = true;
            startBear = false;
            // Console.WriteLine("beer level: " + (levelofBearUp) + " dugnas " + levelofBearDown);
            //  proc[times] = 100 * ((int)levelofBearDown - (int)levelofBearUp) / ((int)levelofBearDown - yUp); taip buvo
            proc[times] = 100 * (down - up) / (down - yUp);
            times++;
        }
        levelofBearUp = levelofBearUp / counting;

        levelofBearDown = levelofBearDown / counting;
    }

    public void FindBeerLevelDarker(int height, int width, int xLeft, int yDown, int yUp, ref int? levelofBearDown, ref int? levelofBearUp, ref int[] proc, ref int times)
    {
        bool startBear = false;
        // correctPixel niekada nenaudojamas
        bool correctPixel = true;
        int count = 0; // !
        int distanceX = width / 10; // paklaidos dydis
        int distanceY = height / 10;
        int k;
        int j1;
        int up = 0;
        int down = 0;
        int counting = 0;
        levelofBearDown = 0; levelofBearUp = 0; // nebeveiks exception, kad nėra alaus
        for (int i = (xLeft + distanceX); i < (width + xLeft); i = (i + distanceX)) // plotis
        {
            for (int j = yDown; j > yUp; j--) // aukštis
            {
                //  Console.WriteLine("i: " + i + "j:" + j);
                if (Color.FromArgb(bitmap.GetPixel(i, j)).R > darkerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).R < darkerBeerRmax && Color.FromArgb(bitmap.GetPixel(i, j)).G > darkerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < darkerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < darkerBeerBmax && !startBear)
                {
                    startBear = true; // reiškias baigėsi dugnas ir prasideda alus
                    down = j; //TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                    up = j;
                }
                if (Color.FromArgb(bitmap.GetPixel(i, j)).R > darkerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).R < darkerBeerRmax && Color.FromArgb(bitmap.GetPixel(i, j)).G > darkerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < darkerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < darkerBeerBmax)
                {
                    up = j;
                }
                else
                {
                    count = 0;
                    k = 0;
                    j1 = j;
                    while (k < distanceY && !(Color.FromArgb(bitmap.GetPixel(i, j)).R == outsideBox.R && Color.FromArgb(bitmap.GetPixel(i, j)).G == outsideBox.G && Color.FromArgb(bitmap.GetPixel(i, j)).B == outsideBox.B)) // ar gerai?
                    {
                        if (Color.FromArgb(bitmap.GetPixel(i, j)).R > darkerBeerRmin && Color.FromArgb(bitmap.GetPixel(i, j)).R < darkerBeerRmax && Color.FromArgb(bitmap.GetPixel(i, j)).G > darkerBeerGmin && Color.FromArgb(bitmap.GetPixel(i, j)).G < darkerBeerGmax && Color.FromArgb(bitmap.GetPixel(i, j)).B < darkerBeerBmax)
                        {
                            count++;
                        }
                        j1--;
                        k++;
                    }
                    if ((count * 2) >= distanceY)
                    {
                        correctPixel = true;
                        up = j;
                    }
                    else
                    {
                        correctPixel = false; // tai čia galima ir nutraukti
                    }
                }
                /*   if (!correctPixel)
                   {
                       j = yUp; // baisu dėl šito
                   }*/
            }
            if (times > 1 && times < 9)
            {
                levelofBearUp = levelofBearUp + up;//TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                levelofBearDown = levelofBearDown + down; //TIKSLINTI TIK SU TINKAMAIS DUOMENIMIS
                counting++;
            }
            correctPixel = true;
            startBear = false;
            // Console.WriteLine("beer level: " + (levelofBearUp) + " dugnas " + levelofBearDown);
            //  proc[times] = 100 * ((int)levelofBearDown - (int)levelofBearUp) / ((int)levelofBearDown - yUp); taip buvo
            proc[times] = 100 * (down - up) / (down - yUp);
            times++;
        }
        levelofBearUp = levelofBearUp / counting;

        levelofBearDown = levelofBearDown / counting;
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