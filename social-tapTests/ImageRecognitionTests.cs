using Microsoft.VisualStudio.TestTools.UnitTesting;
using social_tap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace social_tap.Tests
{
    [TestClass()]
    public class ImageRecognitionTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullArgument()
        {
            new ImageRecognition(null);
        }
    }

    [TestClass()]
    public class ComparerTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Compare_NegativeNumArgument()
        {
            //Arrange
            IComparer myComparer = new Comparer();

            //Act
            myComparer.Compare(7, 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Compare_NullArgument()
        {
            //Arrange
            IComparer myComparer = new Comparer();

            //Act
            myComparer.Compare(null, null);
        }
    }


    [TestClass()]
    public class PixelCounterTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullArgument()
        {
            new PixelCounter(null);
        }
    }
}