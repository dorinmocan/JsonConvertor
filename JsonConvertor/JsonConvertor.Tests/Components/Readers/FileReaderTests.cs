using JsonConvertor.Components.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace JsonConvertor.Tests.Components.Readers
{
    [TestClass]
    public class FileReaderTests
    {
        [TestMethod]
        public void FitsConversionType_InFileNotNull_True()
        {
            //Arrange
            var args = new ConsoleArgs
            {
                InFile = new FileInfo("test")
            };
            var reader = new FileReader();

            //Act
            var actual = reader.FitsConversionType(args);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FitsConversionType_InFileNull_False()
        {
            //Arrange
            var args = new ConsoleArgs
            {
                InFile = null
            };
            var reader = new FileReader();

            //Act
            var actual = reader.FitsConversionType(args);

            //Assert
            Assert.IsFalse(actual);
        }
    }
}
