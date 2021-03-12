using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoSQLBenchmarker;

namespace NoSQLBenchmarker.UnitTest
{
    class DatabaseMock : IDatabase
    {
        public void BulkDelete(List<Document> documents)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert(List<Document> documents)
        {
            throw new NotImplementedException();
        }

        public void BulkSelect(List<Document> documents)
        {
            throw new NotImplementedException();
        }

        public void BulkUpdate(List<Document> documents)
        {
            throw new NotImplementedException();
        }

        public void CleanDatabase()
        {
            throw new NotImplementedException();
        }

        public List<Document> documentsFromSingleDelete = new List<Document>();

        public void SingleDelete(Document document)
        {
            documentsFromSingleDelete.Remove(document);
        }

        public List<Document> documentsFromSingleInsert = new List<Document>();

        public void SingleInsert(Document document)
        {
            documentsFromSingleInsert.Add(document);
        }

        public void SingleSelect(Document document)
        {
            throw new NotImplementedException();
        }

        public List<Document> documentsFromSingleUpdate = new List<Document>();
        public void SingleUpdate(Document document)
        {
            documentsFromSingleUpdate.Add(document);
        }
    }

    public class ConsoleLogMock : ILogger
    {
        public void Log(string log)
        {

        }
    }

    public class MeasurementStoreMock : IMeasurementStore
    {
        private List<LogResult> logResults = new List<LogResult>() {
                new LogResult()
                {
                    _database = "MongoDB",
                    _measureOnDatabase = "SingleInsert:Filled",
                    _timeResult = 0.234,
                    _documentSize = "1kb"
                },
                new LogResult()
                {
                    _database = "MongoDB",
                    _measureOnDatabase = "SingleInsert:Empty",
                    _timeResult = 0.134,
                    _documentSize = "1kb"
                },
                new LogResult()
                {
                    _database = "MongoDB",
                    _measureOnDatabase = "SingleInsert:Filled",
                    _timeResult = 1.43,
                    _documentSize = "10kb"
                },
                new LogResult()
                {
                    _database = "MongoDB",
                    _measureOnDatabase = "SingleInsert:Empty",
                    _timeResult = 1.02,
                    _documentSize = "10kb"
                },
                new LogResult()
                {
                    _database = "Postgres",
                    _measureOnDatabase = "SingleInsert:Filled",
                    _timeResult = 4.123,
                    _documentSize = "1kb"
                },
                new LogResult()
                {
                    _database = "Postgres",
                    _measureOnDatabase = "SingleInsert:Empty",
                    _timeResult = 4.987,
                    _documentSize = "1kb"
                },
                new LogResult()
                {
                    _database = "Postgres",
                    _measureOnDatabase = "SingleInsert:Filled",
                    _timeResult = 4.456,
                    _documentSize = "10kb"
                },
                new LogResult()
                {
                    _database = "Postgres",
                    _measureOnDatabase = "SingleInsert:Empty",
                    _timeResult = 4.534,
                    _documentSize = "10kb"
                },
        };

        public void SaveResult(string measureOnDatabase, double time, string documentSize, string database)
        {

        }

        public List<LogResult> getLogResults
        {
            get { return logResults; }
        }
    }

    public class ResultLoggerMock : IResultLogger
    {
        public string GetLoggingString
        {
            get; set;
        }
        public void LogResult(string loggingString)
        {
            GetLoggingString = loggingString;
        }
    }

    [TestClass]
    public class MeasureAlgorithmTests
    {
        /// <summary>
        /// SingleInsert
        /// </summary>
        [TestMethod]
        public void StartMeasure_EveryDocumentGetsInserted_InsertListHasEveryDocument()
        {
            //Arrange
            var database = new DatabaseMock();
            var logger = new ConsoleLogMock();
            SingleInsert singleInsert = new SingleInsert(database, logger);

            var operatorenList = new List<Operatoren>();
            operatorenList.Add(new Operatoren() { Name = "Lance", Shoesize = 21 });

            var documentList = new List<Document>() {
                new Document()
                {
                    ID = 1,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                },
                new Document()
                {
                    ID = 2,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                },
                new Document()
                {
                    ID = 3,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                }};

            //Act
            singleInsert.StartMeasure(documentList);

            Assert.IsTrue(database.documentsFromSingleInsert[0] == documentList[0]
                && database.documentsFromSingleInsert[1] == documentList[1]
                && database.documentsFromSingleInsert[2] == documentList[2]);
            Assert.AreEqual(database.documentsFromSingleInsert.Count, documentList.Count);
        }

        [TestMethod]
        public void SingleInsertStartMeasure_DocumentListIsNull_ThrowsException()
        {
            var database = new DatabaseMock();
            var logger = new ConsoleLogMock();
            SingleInsert singleInsert = new SingleInsert(database, logger);
            List<Document> documentList = null;

            Assert.ThrowsException<NullReferenceException>(() =>
            singleInsert.StartMeasure(documentList));
        }

        [TestMethod]
        public void SingleInsert_DatabaseIsNull_ThrowsException()
        {
            DatabaseMock database = null;
            var logger = new ConsoleLogMock();
            SingleInsert singleInsert;

            Assert.ThrowsException<NullReferenceException>(() =>
            singleInsert = new SingleInsert(database, logger));
        }

        /// <summary>
        /// SingleUpdate
        /// </summary>
        [TestMethod]
        public void StartMeasure_EveryDocumentGetsUpdated_UpdateListHasEveryDocument()
        {
            //Arrange
            var database = new DatabaseMock();
            var logger = new ConsoleLogMock();
            SingleUpdate singleUpdate = new SingleUpdate(database, logger);

            var operatorenList = new List<Operatoren>();
            operatorenList.Add(new Operatoren() { Name = "Lance", Shoesize = 21 });

            var documentList = new List<Document>() {
                new Document()
                {
                    ID = 1,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                },
                new Document()
                {
                    ID = 2,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                },
                new Document()
                {
                    ID = 3,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                }};

            //Act
            singleUpdate.StartMeasure(documentList);

            Assert.IsTrue(database.documentsFromSingleUpdate[0] == documentList[0]
                && database.documentsFromSingleUpdate[1] == documentList[1]
                && database.documentsFromSingleUpdate[2] == documentList[2]);
            Assert.AreEqual(database.documentsFromSingleInsert.Count, documentList.Count);

        }

        [TestMethod]
        public void SingleUpdateStartMeasure_DocumentListIsNull_ThrowsException()
        {
            var database = new DatabaseMock();
            var logger = new ConsoleLogMock();
            SingleUpdate singleUpdate = new SingleUpdate(database, logger);
            List<Document> documentList = null;

            Assert.ThrowsException<NullReferenceException>(() =>
            singleUpdate.StartMeasure(documentList));
        }

        [TestMethod]
        public void SingleUpdate_DatabaseIsNull_ThrowsException()
        {
            DatabaseMock database = null;
            var logger = new ConsoleLogMock();
            SingleUpdate singleUpdate;

            Assert.ThrowsException<NullReferenceException>(() =>
            singleUpdate = new SingleUpdate(database, logger));
        }

        /// <summary>
        /// SingleDelete
        /// </summary>
        [TestMethod]
        public void StartMeasure_EveryDocumentGetsDeleted_DeleteListHasNoDocument()
        {
            //Arrange
            var database = new DatabaseMock();
            var logger = new ConsoleLogMock();
            SingleDelete singleDelete = new SingleDelete(database, logger);
            var documentList = new List<Document>() {
                new Document()
                {
                    ID = 1,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                },
                new Document()
                {
                    ID = 2,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                },
                new Document()
                {
                    ID = 3,
                    machineName = "Rolle",
                    Operatoren = new List<Operatoren>() {
                        new Operatoren() { Name = "Lance", Shoesize = 21 }}
                }};

            //Act
            foreach(var document in documentList)
            {
                database.documentsFromSingleDelete.Add(document);
            }
            singleDelete.StartMeasure(documentList);

            Assert.IsTrue(database.documentsFromSingleDelete.Count == 0);
        }

        [TestMethod]
        public void SingleDeleteStartMeasure_DocumentListIsNull_ThrowsException()
        {
            var database = new DatabaseMock();
            var logger = new ConsoleLogMock();
            SingleDelete singleDelete = new SingleDelete(database, logger);
            List<Document> documentList = null;

            Assert.ThrowsException<NullReferenceException>(() =>
            singleDelete.StartMeasure(documentList));
        }

        [TestMethod]
        public void SingleDelete_DatabaseIsNull_ThrowsException()
        {
            DatabaseMock database = null;
            var logger = new ConsoleLogMock();
            SingleDelete singleDelete;

            Assert.ThrowsException<NullReferenceException>(() =>
            singleDelete = new SingleDelete(database, logger));
        }

    }

    [TestClass]
    public class ExportTests
    {
        [TestMethod]
        public void Export_MeasurementStoreIsNull_ThrowsException()
        {
            MeasurementStoreMock measurementStoreMock = null;
            IResultLogger resultLoggerMock = new ResultLoggerMock();
            Export export;
            Assert.ThrowsException<NullReferenceException>(() =>
            export = new Export(measurementStoreMock, resultLoggerMock));
        }

        [TestMethod]
        public void Export_resultLoggerIsNull_ThrowsException()
        {
            IMeasurementStore measurementStoreMock = new MeasurementStoreMock();
            ResultLoggerMock resultLoggerMock = null;
            Export export;
            Assert.ThrowsException<NullReferenceException>(() =>
            export = new Export(measurementStoreMock, resultLoggerMock));
        }

        [TestMethod]
        public void ResultLog()
        {
            MeasurementStoreMock measurementStoreMock = new MeasurementStoreMock();
            var resultLoggerMock = new ResultLoggerMock();
            Export export = new Export(measurementStoreMock, resultLoggerMock);

            export.ConfigureResult();
            string actualResult = resultLoggerMock.GetLoggingString;
            string shouldBeResult = "MongoDB\nSingleInsert:Filled,0.234,1.43" +
                "\r\nSingleInsert:Empty,0.134,1.02\r\n" +
                "Postgres\nSingleInsert:Filled,4.123,4.456" +
                "\r\nSingleInsert:Empty,4.987,4.534\r\n\n";

            Assert.AreEqual(shouldBeResult, actualResult);
        }
    }
}
