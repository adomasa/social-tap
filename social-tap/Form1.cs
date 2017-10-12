using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace social_tap
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
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
        
  
               
                
                FileWriter.BarData(barName, beverageLevel, comment, recommends); // nusiunčiami duomenys įrašymui į txt fail'us
                FileReader.ReadBarInfo(ref stats);

                double sum = (double)stats.sum;
                double amount = (double)stats.amount;
                FileWriter.BeverageData(beverageLevel, sum, amount);

                string value = stats.amount.ToString();
                Match match = Regex.Match(value, @"^[0-9]{2}$");  //regex 
                if (match.Success)
                {
                    Console.WriteLine(value + "Programelė pasinaudojo jau dviženklį kartų skaičių");
                }
            }
        }
    }
}
