using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            String barName = barNameLabel.Text;
            int beverageLevel = trackBar.Value;
            String comment = commentRichTextBox.Text;
            Boolean recommends = yesRadioButton.Checked;

            Console.WriteLine("Bar name: " + barName);
            Console.WriteLine("Beverage level: " + beverageLevel/10);
            Console.WriteLine("Comment: " + comment);
            Console.WriteLine("User recommends: " + (recommends ? "true" : "false"));
        }
    }
}
