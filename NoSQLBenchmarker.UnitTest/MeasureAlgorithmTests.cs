using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoSQLBenchmarker;

namespace NoSQLBenchmarker.UnitTest
{
    [TestClass]
    public class MeasureAlgorithmTests
    {
        [TestMethod]
        public void StartMeasure_CoundOfInsertsEqualsDocuments_ReturnsTrue()
        {
            //Arrange
            IMeasureAlgorithm measureAlgorithm = null;

            //var documentList = new List<Document>(
            //    new Document { }
            //    );
            //Act

            //Assert
        }

        [TestMethod]
        public void StartMeasure_EveryDocumentGetsInserted_ReturnsTrue()
        {

        }

        [TestMethod]
        public void SingleInsert_DatabaseNull_ReturnsFalse()
        {

        }
    }
}
