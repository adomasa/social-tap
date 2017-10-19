using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace social_tapTests
{
    [TestClass()]
    public class ComparerTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Compare_NegativeNumArgument()
        {
            //Arrange
            IComparer myComparer = new social_tap.Comparer();

            //Act
            myComparer.Compare(7, 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Compare_NullArgument()
        {
            //Arrange
            IComparer myComparer = new social_tap.Comparer();

            //Act
            myComparer.Compare(null, null);
        }
    }
}
