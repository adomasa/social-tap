using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace social_tapTests
{
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
