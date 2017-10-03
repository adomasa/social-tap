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
                Console.WriteLine("Bar name: " + barName + "\n");
                Console.WriteLine("Beverage level: " + beverageLevel + "\n");
                Console.WriteLine("Comment: " + comment + "\n");
                Console.WriteLine("User recommends: " + (recommends ? "true" : "false") + "\n");

                BarInfo stats = new BarInfo(0, 0);
                Reader(ref stats);

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
                double sum = (double)stats.sum;
                double amount = (double)stats.amount;
                if (beverageLevel >= (sum / amount))
                    Console.WriteLine("Įpylė geriau nei vidurkis");
                else
                    Console.WriteLine("Įpylė blogiau nei vidurkis");

                Console.WriteLine(sum + " " + amount);

                string value = stats.amount.ToString();

                Match match = Regex.Match(value, @"^[0-9]{2}$");  //regex 
                if (match.Success)
                {
                    Console.WriteLine(value + "Programelė pasinaudojo jau dviženklį kartų skaičių");
                }

                Writter(barName, beverageLevel, comment, recommends, sum, amount); // nusiunčiami duomenys įrašymui į txt fail'us
            }
        }

       private void Writter(String barName, int beverageLevel, String comment, Boolean recommends, double sum, double amount)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("rez.txt", true); //true neperrašo failo iš naujo kiekvieną kartą. Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            System.IO.StreamWriter evaluations = new System.IO.StreamWriter("rez1.txt", true); //Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            System.IO.StreamWriter info = new System.IO.StreamWriter("info.txt", false); //Pateikia paskutinės užklauso rezultatą. Failo location'as:  ...Source\Repos\social-tap\social-tap\bin\Debug
            file.WriteLine("Baro pavadinimas: " + barName + " " + "\n");
            file.WriteLine("Kiek įpylė (0/10)  " + beverageLevel + " " + "\n");
            file.WriteLine("Komentaras: " + comment + " " + "\n");
            if (recommends)
            {
                file.WriteLine("Ar rekomenduoja: Taip! \n");
            }
            else file.WriteLine("Ar rekomenduoja: Ne... \n");

            evaluations.WriteLine(beverageLevel);

            if (beverageLevel >= (sum / amount))
                info.WriteLine("Įpylė daugiau nei vidutiniškai. Vidurkis: " + Math.Round((sum / amount), 2));
            else
                info.WriteLine("Įpylė mažiau nei vidutiniškai. Vidurkis: " + Math.Round((sum / amount), 2));

            file.Close();
            evaluations.Close();
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
