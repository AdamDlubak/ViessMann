using NUnit.Framework;
using NUnit.Framework.Internal;
using UniversityIot.VitocontrolApi;
using Adam;

namespace UniversityIot.VitocontrolApi.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void StringExtensionIsBlueAPrimaryColorTest()
        {
            // Arrange
            string color = "Blue";

            // Act
            bool actual = color.IsPrimaryColor();

            // Assert
            const bool expected = true;
            Assert.AreEqual(expected, actual);
        }
    }
}
