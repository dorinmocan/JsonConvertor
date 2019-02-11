using JsonConvertor.Components.Convertors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace JsonConvertor.Tests.Components.Convertors
{
    [TestClass]
    public class JsonToPathsConvertorTests
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
            var convertor = new JsonToPathsConvertor();

            //Act
            var actual = convertor.FitsConversionType(args);

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
            var convertor = new JsonToPathsConvertor();

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
            var convertor = new JsonToPathsConvertor();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => convertor.Convert(input));
        }

        [TestMethod]
        public void Convert_InputNotJson_JsonReaderException()
        {
            //Arrange
            var input = "test";
            var convertor = new JsonToPathsConvertor();

            //Assert
            Assert.ThrowsException<JsonReaderException>(
                () => convertor.Convert(input));
        }

        [TestMethod]
        public void Convert_BrokenJson_JsonReaderException()
        {
            //Arrange
            var input = new StringBuilder("", 100);
            input.AppendLine("{");
            input.AppendLine("    \"key1\": \"val1\"");
            input.AppendLine("    \"key2\": \"val2\"");
            input.AppendLine("}");

            var convertor = new JsonToPathsConvertor();

            //Assert
            Assert.ThrowsException<JsonReaderException>(
                () => convertor.Convert(input.ToString()));
        }

        [TestMethod]
        public void Convert_JsonWithNoProperties_EmptyString()
        {
            //Arrange
            var input = new StringBuilder("");
            input.AppendLine("{");
            input.AppendLine("}");

            var expected = new StringBuilder("");

            var convertor = new JsonToPathsConvertor();

            //Act
            var actual = convertor.Convert(input.ToString());

            //Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void Convert_ConvertsJsonWithSimpleProperties()
        {
            //Arrange
            var input = new StringBuilder("", 100);
            input.AppendLine("{");
            input.AppendLine("    \"key1\": \"val1\",");
            input.AppendLine("    \"key2\": \"val2\"");
            input.AppendLine("}");

            var expected = new StringBuilder("", 100);
            expected.AppendLine("\"key1\"	val1");
            expected.AppendLine("\"key2\"	val2");

            var convertor = new JsonToPathsConvertor();

            //Act
            var actual = convertor.Convert(input.ToString());

            //Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void Convert_ConvertsJsonWithNestedProperties()
        {
            //Arrange
            var input = new StringBuilder("", 100);
            input.AppendLine("{");
            input.AppendLine("    \"key1\": {");
            input.AppendLine("        \"key1\": \"val1\",");
            input.AppendLine("        \"key2\": \"val2\"");
            input.AppendLine("    },");
            input.AppendLine("    \"key2\": \"val2\"");
            input.AppendLine("}");

            var expected = new StringBuilder("", 100);
            expected.AppendLine("\"key1\".\"key1\"	val1");
            expected.AppendLine("\"key1\".\"key2\"	val2");
            expected.AppendLine("\"key2\"	val2");

            var convertor = new JsonToPathsConvertor();

            //Act
            var actual = convertor.Convert(input.ToString());

            //Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
