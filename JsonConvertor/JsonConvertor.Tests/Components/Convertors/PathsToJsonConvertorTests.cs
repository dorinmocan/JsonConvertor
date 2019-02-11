using JsonConvertor.Components.Convertors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace JsonConvertor.Tests.Components.Convertors
{
    [TestClass]
    public class PathsToJsonConvertorTests
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
            var convertor = new PathsToJsonConvertor();

            //Act
            var actual = convertor.FitsConversionType(args);

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
            var convertor = new PathsToJsonConvertor();

            //Act
            var actual = convertor.FitsConversionType(args);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Convert_InputNull_ArgumentNullException()
        {
            //Arrange
            var input = null as string;
            var convertor = new PathsToJsonConvertor();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => convertor.Convert(input));
        }

        [TestMethod]
        public void Convert_InputNotInTheRightFormat_FormatException()
        {
            //Arrange
            var input = "test";
            var convertor = new PathsToJsonConvertor();

            //Assert
            Assert.ThrowsException<FormatException>(
                () => convertor.Convert(input));
        }

        [TestMethod]
        public void Convert_EmptyString_JsonWithNoProperties()
        {
            //Arrange
            var input = new StringBuilder("");

            var expected = new StringBuilder("{}");

            var convertor = new PathsToJsonConvertor();

            //Act
            var actual = convertor.Convert(input.ToString());

            //Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void Convert_ConvertsPathsWithSingleKey()
        {
            //Arrange
            var input = new StringBuilder("", 100);
            input.AppendLine("\"key1\"	val1");
            input.AppendLine("\"key2\"	val2");

            var expected = new StringBuilder("", 100);
            expected.Append("{");
            expected.Append("\"key1\":\"val1\",");
            expected.Append("\"key2\":\"val2\"");
            expected.Append("}");

            var convertor = new PathsToJsonConvertor();

            //Act
            var actual = convertor.Convert(input.ToString());

            //Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void Convert_ConvertsPathsWithMultipleKeys()
        {
            //Arrange
            var input = new StringBuilder("", 100);
            input.AppendLine("\"key1\".\"key1\"	val1");
            input.AppendLine("\"key1\".\"key2\"	val2");
            input.AppendLine("\"key2\"	val2");

            var expected = new StringBuilder("", 100);
            expected.Append("{");
            expected.Append("\"key1\":{");
            expected.Append("\"key1\":\"val1\",");
            expected.Append("\"key2\":\"val2\"");
            expected.Append("},");
            expected.Append("\"key2\":\"val2\"");
            expected.Append("}");

            var convertor = new PathsToJsonConvertor();

            //Act
            var actual = convertor.Convert(input.ToString());

            //Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
