using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

namespace social_tap
{
    public class ImageRecognition
    {
        private UMat targetImage;
        public ImageRecognition(Image<Bgr, byte> image)
        {
            targetImage = new UMat();
            CvInvoke.Canny(convertImage(image), targetImage, 50, 150);
        }

        public ImageRecognition() // :)
        {
        }

        public Image<Gray, byte> convertImage(Image<Bgr, byte> image)
        {
            var processedImage = image.Convert<Gray, Byte>();
            processedImage = processedImage.SmoothGaussian(5, 5, 0, 0);
            return processedImage;
        }

        public Image getProccessedImg()
        {
            return targetImage.Bitmap;
        }

        public void drawContours()
        {
            CvInvoke.DrawContours(targetImage, findContours(), -1, new MCvScalar(255, 0, 0));
        }

        public VectorOfVectorOfPoint findContours()
        {
            var contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(targetImage, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
            return filterContours(contours);
        }

        public VectorOfVectorOfPoint filterContours(VectorOfVectorOfPoint contours)
        {
            var tempContours = new VectorOfVectorOfPoint();
            for (int i = 0; i < contours.Size; i++)
            {
                using (var contour = new VectorOfPoint())
                {
                    var peri = CvInvoke.ArcLength(contours[i], true);
                    CvInvoke.ApproxPolyDP(contours[i], contour, 0.1 * peri, true);
                    if ((contour != null && contour.ToArray().Length == 4 && CvInvoke.IsContourConvex(contour)))
                        tempContours.Push(contour);
                }
            }
            return tempContours;
        }
    }
}
