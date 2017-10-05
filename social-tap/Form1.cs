using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace social_tap
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public struct BarInfo
        {
            public int amount, sum;

            public BarInfo(int n, int beverageLevel)
            {
                amount = n;
                sum = beverageLevel;
            }
            public void count(int n, int beverageLevel)
            {
                amount += 1;
                sum += beverageLevel;
            }
        }

        Image<Bgr, byte> image;

        private void UploadPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                ImageRecognition imageRecognition = new ImageRecognition(image = new Image<Bgr, byte>(Openfile.FileName));
                PixelCounter pixelCounter = new PixelCounter(image);
                pictureBox.Image = imageRecognition.GetProccessedImg();
                pixelPercentageLabel.Text = pixelCounter.GetPercentageOfTargetPixels() + "%";
            }
        }

        private void SubmitClicked(object sender, EventArgs e)
        {
            String barName = barNameTextBox.Text;
            int beverageLevel = trackBar.Value;
            String comment = commentRichTextBox.Text;
            Boolean recommends = yesRadioButton.Checked;
            if (barName == "" || (!noRadioButton.Checked && !yesRadioButton.Checked)) // patikrina, ar įvesta informacija
            {
                somethingWrong.Visible = true;
                allGood.Visible = false;
            }
            else
            {
                allGood.Visible = true;
                somethingWrong.Visible = false;

                BarInfo stats = new BarInfo(0, 0);

                var evaluations = new List<int>();
                int result = 0;
                evaluations.Add(beverageLevel);
                IComparer myComparer = new Comparer();
                foreach (var evaluation in evaluations)
                {
                    result = myComparer.Compare(evaluation, 10);
                    if (result == -1)
                        Console.WriteLine("Įpilta mažai");
                    else if (result == 0)
                        Console.WriteLine("Įpilta vidutiniškai");
                    else
                        Console.WriteLine("Įpilta gerai");
                }
        
  
               
                
                Writter(barName, beverageLevel, comment, recommends); // nusiunčiami duomenys įrašymui į txt fail'us
                Reader(ref stats);

                double sum = (double)stats.sum;
                double amount = (double)stats.amount;
                Writter(beverageLevel, sum, amount);

                string value = stats.amount.ToString();
                Match match = Regex.Match(value, @"^[0-9]{2}$");  //regex 
                if (match.Success)
                {
                    Console.WriteLine(value + "Programelė pasinaudojo jau dviženklį kartų skaičių");
                }
            }
        }

       private void Writter(String barName, int beverageLevel, String comment, Boolean recommends)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("rez.txt", true); //true neperrašo failo iš naujo kiekvieną kartą. Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            System.IO.StreamWriter evaluations = new System.IO.StreamWriter("rez1.txt", true); //Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
           
            file.WriteLine("Baro pavadinimas: " + barName + " " + "\n");
            file.WriteLine("Kiek įpylė (0/10)  " + beverageLevel + " " + "\n");
            file.WriteLine("Komentaras: " + comment + " " + "\n");
            file.WriteLine("Ar rekomenduoja: " + (recommends ? "Taip!" : "Ne...") + "\n");

            evaluations.WriteLine(beverageLevel);

            file.Close();
            evaluations.Close();
    
        }

        private void Writter(int beverageLevel, double sum, double amount)
        {
            System.IO.StreamWriter info = new System.IO.StreamWriter("info.txt", false); //Pateikia paskutinės užklauso rezultatą. Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            if(beverageLevel >= (sum / amount))
                info.WriteLine("Įpylė daugiau nei vidutiniškai. Vidurkis: " + Math.Round((sum / amount), 2) + " Jau vertino " + amount + " žmonės");
            else
                info.WriteLine("Įpylė mažiau nei vidutiniškai. Vidurkis: " + Math.Round((sum / amount), 2) + " Jau vertino " + amount + " žmonės");

            info.Close();

        }

        private void Reader(ref BarInfo stats)
        {
            System.IO.StreamReader dataStream = new System.IO.StreamReader("rez1.txt"); //Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            string datasample;
            int number = 0; ;
            while ((datasample = dataStream.ReadLine()) != null)
            {
                stats.amount++;
                int.TryParse(datasample, out number);
                stats.sum += number;
            }

            dataStream.Close();
        }

        private void barNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
