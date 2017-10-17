using System;
using System.IO;

namespace social_tap
{
    public static class FileWriter
    {
        public static void BarData(String barName, int beverageLevel, String comment, Boolean recommends)
        {
            //true neperrašo failo iš naujo kiekvieną kartą. Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            StreamWriter file = new StreamWriter("rez.txt", true);
            //Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            StreamWriter evaluations = new StreamWriter("rez1.txt", true); 

            file.WriteLine("Baro pavadinimas: " + barName + " " + "\n");
            file.WriteLine("Kiek įpylė (0/10)  " + beverageLevel + " " + "\n");
            file.WriteLine("Komentaras: " + comment + " " + "\n");
            file.WriteLine("Ar rekomenduoja: " + (recommends ? "Taip!" : "Ne...") + "\n");

            evaluations.WriteLine(beverageLevel);

            file.Close();
            evaluations.Close();

        }

        public static void BeverageData(int beverageLevel, double sum, double amount)
        {
            //Pateikia paskutinės užklauso rezultatą. Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            StreamWriter info = new StreamWriter("info.txt", false); 
            if (beverageLevel >= (sum / amount))
                info.WriteLine("Įpylė daugiau nei vidutiniškai. Vidurkis: " + Math.Round((sum / amount), 2) + " Jau vertino " + amount + " žmonės");
            else
                info.WriteLine("Įpylė mažiau nei vidutiniškai. Vidurkis: " + Math.Round((sum / amount), 2) + " Jau vertino " + amount + " žmonės");

            info.Close();
        }
    }
}
