using JsonConvertor.Components.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace JsonConvertor.Tests.Components.Writers
{
    [TestClass]
    public class PathsWriterTests
    {
        [TestMethod]
        public void FitsConversionType_InFileJsonAndOutFileTxt_True()
        {
            //Arrange
            var args = new ConsoleArgs
            {
                InFile = new FileInfo("test.json"),
                OutFile = new FileInfo("test.txt")
            };
            var writer = new PathsWriter();

            //Act
            var actual = writer.FitsConversionType(args);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FitsConversionType_InFileNotJsonOrOutFileNotTxt_False()
        {
            //Arrange
            var args = new ConsoleArgs
            {
                InFile = new FileInfo("test.txt"),
                OutFile = new FileInfo("test.json")
            };
            var writer = new PathsWriter();

            //Act
            var actual = writer.FitsConversionType(args);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Write_OutputNull_ArgumentNullException()
        {
            //Arrange
            var output = null as string;
            var args = new ConsoleArgs { OutFile = new FileInfo("test.json") };

            var writer = new PathsWriter();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => writer.Write(output, args));
        }
    }
}
