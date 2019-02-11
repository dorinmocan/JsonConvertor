using JsonConvertor.Components.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using JsonWriter = JsonConvertor.Components.Writers.JsonWriter;

namespace JsonConvertor.Tests.Components.Writers
{
    [TestClass]
    public class JsonWriterTests
    {
        [TestMethod]
        public void FitsConversionType_InFileTxtAndOutFileJson_True()
        {
            //Arrange
            var args = new ConsoleArgs
            {
                InFile = new FileInfo("test.txt"),
                OutFile = new FileInfo("test.json")
            };
            var writer = new JsonWriter();

            //Act
            var actual = writer.FitsConversionType(args);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FitsConversionType_InFileNotTxtOrOutFileNotJson_False()
        {
            //Arrange
            var args = new ConsoleArgs
            {
                InFile = new FileInfo("test.json"),
                OutFile = new FileInfo("test.txt")
            };
            var writer = new JsonWriter();

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

            var writer = new JsonWriter();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => writer.Write(output, args));
        }

        [TestMethod]
        public void Write_OutputNotJson_JsonReaderException()
        {
            //Arrange
            var output = "test";
            var args = new ConsoleArgs { OutFile = new FileInfo("test.json") };

            var writer = new JsonWriter();

            //Assert
            Assert.ThrowsException<JsonReaderException>(
                () => writer.Write(output, args));
        }
    }
}
