using JsonConvertor.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;

namespace JsonConvertor.Tests
{
    [TestClass]
    public class ComponentsManagerTests
    {
        [TestMethod]
        public void RegisterDefaultComponents_FillsComponentsCollections()
        {
            //Arrange
            var componentsManager = new ComponentsManager();

            //Act
            componentsManager.RegisterDefaultComponents();

            //Assert
            Assert.IsTrue(componentsManager.Readers.Count > 0 &&
                componentsManager.Convertors.Count > 0 &&
                componentsManager.Writers.Count > 0);
        }

        [TestMethod]
        public void RegisterReaders_FillsReadersCollection()
        {
            //Arrange
            var readers = new List<IInputReader>()
            {
                Substitute.For<IInputReader>(),
            };
            var componentsManager = new ComponentsManager();

            //Act
            componentsManager.RegisterReaders(readers);

            //Assert
            Assert.AreEqual(componentsManager.Readers[0], readers[0]);
        }

        [TestMethod]
        public void RegisterConvertors_FillsConvertorsCollection()
        {
            //Arrange
            var convertors = new List<IConvertor>()
            {
                Substitute.For<IConvertor>()
            };
            var componentsManager = new ComponentsManager();

            //Act
            componentsManager.RegisterConvertors(convertors);

            //Assert
            Assert.AreEqual(componentsManager.Convertors[0], convertors[0]);
        }

        [TestMethod]
        public void RegisterWriters_FillsWritersCollection()
        {
            //Arrange
            var writers = new List<IOutputWriter>()
            {
                Substitute.For<IOutputWriter>()
            };
            var componentsManager = new ComponentsManager();

            //Act
            componentsManager.RegisterWriters(writers);

            //Assert
            Assert.AreEqual(componentsManager.Writers[0], writers[0]);
        }

        [TestMethod]
        public void GetReader_NoMatchingItemsInCollection_Null()
        {
            //Arrange
            var args = new ConsoleArgs();
            var readers = new List<IInputReader>()
            {
                Substitute.For<IInputReader>()
            };

            readers[0].FitsConversionType(args).Returns(false);

            var componentsManager = new ComponentsManager();
            componentsManager.Readers = readers;

            //Act
            var actual = componentsManager.GetReader(args);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetReader_MatchingItemsInCollection_FirstMatchingItem()
        {
            //Arrange
            var args = new ConsoleArgs();
            var readers = new List<IInputReader>()
            {
                Substitute.For<IInputReader>(),
                Substitute.For<IInputReader>(),
                Substitute.For<IInputReader>()
            };

            readers[0].FitsConversionType(args).Returns(false);
            readers[1].FitsConversionType(args).Returns(true);
            readers[2].FitsConversionType(args).Returns(true);

            var componentsManager = new ComponentsManager();
            componentsManager.Readers = readers;

            //Act
            var actual = componentsManager.GetReader(args);

            //Assert
            Assert.AreEqual(readers[1], actual);
        }

        [TestMethod]
        public void GetConvertor_NoMatchingItemsInCollection_Null()
        {
            //Arrange
            var args = new ConsoleArgs();
            var convertors = new List<IConvertor>()
            {
                Substitute.For<IConvertor>()
            };

            convertors[0].FitsConversionType(args).Returns(false);

            var componentsManager = new ComponentsManager();
            componentsManager.Convertors = convertors;

            //Act
            var actual = componentsManager.GetConvertor(args);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetConvertor_MatchingItemsInCollection_FirstMatchingItem()
        {
            //Arrange
            var args = new ConsoleArgs();
            var convertors = new List<IConvertor>()
            {
                Substitute.For<IConvertor>(),
                Substitute.For<IConvertor>(),
                Substitute.For<IConvertor>()
            };

            convertors[0].FitsConversionType(args).Returns(false);
            convertors[1].FitsConversionType(args).Returns(true);
            convertors[2].FitsConversionType(args).Returns(true);

            var componentsManager = new ComponentsManager();
            componentsManager.Convertors = convertors;

            //Act
            var actual = componentsManager.GetConvertor(args);

            //Assert
            Assert.AreEqual(convertors[1], actual);
        }

        [TestMethod]
        public void GetWriter_NoMatchingItemsInCollection_Null()
        {
            //Arrange
            var args = new ConsoleArgs();
            var writers = new List<IOutputWriter>()
            {
                Substitute.For<IOutputWriter>()
            };

            writers[0].FitsConversionType(args).Returns(false);

            var componentsManager = new ComponentsManager();
            componentsManager.Writers = writers;

            //Act
            var actual = componentsManager.GetWriter(args);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetWriter_MatchingItemsInCollection_FirstMatchingItem()
        {
            //Arrange
            var args = new ConsoleArgs();
            var writers = new List<IOutputWriter>()
            {
                Substitute.For<IOutputWriter>(),
                Substitute.For<IOutputWriter>(),
                Substitute.For<IOutputWriter>()
            };

            writers[0].FitsConversionType(args).Returns(false);
            writers[1].FitsConversionType(args).Returns(true);
            writers[2].FitsConversionType(args).Returns(true);

            var componentsManager = new ComponentsManager();
            componentsManager.Writers = writers;

            //Act
            var actual = componentsManager.GetWriter(args);

            //Assert
            Assert.AreEqual(writers[1], actual);
        }
    }
}
