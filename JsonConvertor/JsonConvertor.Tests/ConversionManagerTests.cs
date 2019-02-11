using JsonConvertor.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace JsonConvertor.Tests
{
    [TestClass]
    public class ConversionManagerTests
    {
        [TestMethod]
        public void Convert_InvokesComponentsInAppropriateOrder()
        {
            //Arrange
            var args = new ConsoleArgs();
            var componentsManager = Substitute.For<IComponentsManager>();
            var reader = Substitute.For<IInputReader>();
            var convertor = Substitute.For<IConvertor>();
            var writer = Substitute.For<IOutputWriter>();
            var input = "input";
            var output = "output";

            componentsManager.GetReader(args).Returns(reader);
            reader.Read(args).Returns(input);
            componentsManager.GetConvertor(args).Returns(convertor);
            convertor.Convert(input).Returns(output);
            componentsManager.GetWriter(args).Returns(writer);

            var conversionManager = new ConversionManager(args, componentsManager);

            //Act
            conversionManager.Convert();

            //Assert
            componentsManager.Received().GetReader(args);
            reader.Received().Read(args);
            componentsManager.Received().GetConvertor(args);
            convertor.Received().Convert(input);
            componentsManager.Received().GetWriter(args);
            writer.Received().Write(output, args);
        }

        [TestMethod]
        public void ReadInput_ReturnsWhatReaderReturns()
        {
            //Arrange
            var args = new ConsoleArgs();
            var reader = Substitute.For<IInputReader>();
            var expected = "test";

            reader.Read(args).Returns(expected);

            var conversionManager = new ConversionManager(args, Substitute.For<IComponentsManager>());

            //Act
            var actual = conversionManager.ReadInput(reader, args);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Convert_ReturnsWhatConvertorReturns()
        {
            //Arrange
            var convertor = Substitute.For<IConvertor>();
            var input = "input";
            var expected = "test";

            convertor.Convert(input).Returns(expected);

            var conversionManager = new ConversionManager(new ConsoleArgs(), Substitute.For<IComponentsManager>());

            //Act
            var actual = conversionManager.Convert(convertor, input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteOutput_CallsWriter()
        {
            //Arrange
            var args = new ConsoleArgs();
            var writer = Substitute.For<IOutputWriter>();
            var output = "output";

            var conversionManager = new ConversionManager(args, Substitute.For<IComponentsManager>());

            //Act
            conversionManager.WriteOutput(output, writer, args);

            //Assert
            writer.Received().Write(output, args);
        }
    }
}
