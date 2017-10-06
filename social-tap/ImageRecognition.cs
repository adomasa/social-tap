using System;
using System.Drawing;
using Emgu.CV;
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
            CvInvoke.Canny(ConvertImage(image), targetImage, 50, 150);
        }

        private Image<Gray, byte> ConvertImage(Image<Bgr, byte> image)
        {
            var processedImage = image.Convert<Gray, Byte>();
            processedImage = processedImage.SmoothGaussian(5, 5, 0, 0);
            return processedImage;
        }

        public Image GetProccessedImg()
        {
            return targetImage.Bitmap;
        }

        public void DrawContours()
        {
            CvInvoke.DrawContours(targetImage, FindContours(), -1, new MCvScalar(255, 0, 0));
        }

        private VectorOfVectorOfPoint FindContours()
        {
            var contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(targetImage, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
            return FilterContours(contours);
        }

        private VectorOfVectorOfPoint FilterContours(VectorOfVectorOfPoint contours)
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
