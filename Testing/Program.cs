using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Marten;
using Newtonsoft.Json;
using MongoDB.Driver;

namespace NoSQLBenchmarker
{
    /// <summary>
    /// Main Function
    /// </summary>
    class MainApp
    {
        //Einstieg ins Programm
        static void Main(string[] args)
        {
            System.Runtime.GCSettings.LatencyMode = System.Runtime.GCLatencyMode.LowLatency;
            var generatorFor1kb = new GeneratorFor1kb();
            var generatorFor10kb = new GeneratorFor10kb();
            var generatorFor100kb = new GeneratorFor100kb();
            var generatorFor1000kb = new GeneratorFor1000kb();
            var databaseMongo = new MongoDB();
            var databasePost = new PostGreSQL();
            var databaseMemory = new InMemory(); 
            var store = new MeasurementStore();
            var resultLogger = new CSVResultLogger();
            var export = new Export(store, resultLogger);
            var logger = new ConsoleLogger();

            List<IMeasureAlgorithm> CreateAlgorithmForDatabase(IDatabase db, ILogger logger, IDocumentGenerator documentGenerator, string documentSize)
            {
                var algorithms = new List<IMeasureAlgorithm>();
                algorithms.Add(new SingleInsert(db, logger));
                algorithms.Add(new SingleUpdate(db, logger));
                algorithms.Add(new SingleDelete(db, logger));
                algorithms.Add(new SingleSelect(db, logger));
                algorithms.Add(new BulkInsert(db, logger));
                algorithms.Add(new BulkUpdate(db, logger));
                algorithms.Add(new BulkDelete(db, logger));
                algorithms.Add(new BulkSelect(db, logger));

                var testAlgorithms = new List<IMeasureAlgorithm>();
                testAlgorithms.Add(new SingleInsert(db, logger));
                testAlgorithms.Add(new SingleUpdate(db, logger));
                testAlgorithms.Add(new SingleDelete(db, logger));
                testAlgorithms.Add(new SingleSelect(db, logger));

                SetUpEmptyDB(db);
                foreach (var testAlgorithm in testAlgorithms)
                {
                    testAlgorithm.StartMeasure(generatorFor1kb.GetDocumentsForMeasurement);
                    testAlgorithm.CleanUp(db, documentGenerator);
                }

                string database = "";
                if (db is MongoDB)
                {
                    database = "MongoDB";
                }
                if (db is PostGreSQL)
                {
                    database = "PostGreSQL";
                }
                if (db is InMemory)
                {
                    database = "InMemory";
                }

                SetUpEmptyDB(db);
                foreach (var algorithm in algorithms)
                {
                    store.SaveResult($"{algorithm}:Empty", algorithm
                        .StartMeasure(documentGenerator.GetDocumentsForMeasurement), documentSize, database);
                    algorithm.CleanUp(db, documentGenerator);
                }

                SetUpFilledDB(db);
                foreach (var algorithm in algorithms)
                {
                    store.SaveResult($"{algorithm}:Filled", algorithm
                        .StartMeasure(documentGenerator.GetDocumentsForMeasurement), documentSize, database);
                    algorithm.CleanUp(db, documentGenerator);
                }
                return algorithms;
            }

            var jsonString1kb = JsonConvert.SerializeObject(generatorFor1kb.GetDocumentsForMeasurement[0]);
            var jsonString10kb = JsonConvert.SerializeObject(generatorFor10kb.GetDocumentsForMeasurement[0]);
            var jsonString100kb = JsonConvert.SerializeObject(generatorFor100kb.GetDocumentsForMeasurement[0]);
            var jsonString1000kb = JsonConvert.SerializeObject(generatorFor1000kb.GetDocumentsForMeasurement[0]);

            Console.WriteLine($"One Document of 1kb Generator.List is size of {jsonString1kb.Length * sizeof(char)} Bytes or {jsonString1kb.Length * sizeof(char) / 1000} Kilobytes");
            Console.WriteLine($"One Document of 10kb Generator.List is size of {jsonString10kb.Length * sizeof(char)} Bytes or {jsonString10kb.Length * sizeof(char) / 1000} Kilobytes");
            Console.WriteLine($"One Document of 100kb Generator.List is size of {jsonString100kb.Length * sizeof(char)} Bytes or {jsonString100kb.Length * sizeof(char) / 1000} Kilobytes");
            Console.WriteLine($"One Document of 1000kb Generator.List is size of {jsonString1000kb.Length * sizeof(char)} Bytes or {jsonString1000kb.Length * sizeof(char) / 1000} Kilobytes");

            Console.WriteLine("\nStarting Tests for 1kb");
            Console.WriteLine("\nPostgres:");
            CreateAlgorithmForDatabase(databasePost, logger, generatorFor1kb, "1kb");
            Console.WriteLine("\nMongoDB:");
            CreateAlgorithmForDatabase(databaseMongo, logger, generatorFor1kb, "1kb");
            Console.WriteLine("\nIn Memory:");
            CreateAlgorithmForDatabase(databaseMemory, logger, generatorFor1kb, "1kb");

            Console.WriteLine("\nStarting Tests for 10kb");
            Console.WriteLine("\nPostgres:");
            CreateAlgorithmForDatabase(databasePost, logger, generatorFor10kb, "10kb");
            Console.WriteLine("\nMongoDB:");
            CreateAlgorithmForDatabase(databaseMongo, logger, generatorFor10kb, "10kb");
            Console.WriteLine("\nIn Memory:");
            CreateAlgorithmForDatabase(databaseMemory, logger, generatorFor10kb, "10kb");

            Console.WriteLine("\nStarting Tests for 100kb");
            Console.WriteLine("\nPostgres:");
            CreateAlgorithmForDatabase(databasePost, logger, generatorFor100kb, "100kb");
            Console.WriteLine("\nMongoDB:");
            CreateAlgorithmForDatabase(databaseMongo, logger, generatorFor100kb, "100kb");
            Console.WriteLine("\nIn Memory:");
            CreateAlgorithmForDatabase(databaseMemory, logger, generatorFor100kb, "100kb");

            Console.WriteLine("\nStarting Tests for 1000kb");
            Console.WriteLine("\nPostgres:");
            CreateAlgorithmForDatabase(databasePost, logger, generatorFor1000kb, "1000kb");
            Console.WriteLine("\nMongoDB:");
            CreateAlgorithmForDatabase(databaseMongo, logger, generatorFor1000kb, "1000kb");
            Console.WriteLine("\nIn Memory:");
            CreateAlgorithmForDatabase(databaseMemory, logger, generatorFor1000kb, "1000kb");

            //Export the CSV File into folder
            export.ConfigureResult();
            
            void SetUpEmptyDB(IDatabase db)
            {
                db.CleanDatabase();
                Console.WriteLine("Database is empty");
            }

            void SetUpFilledDB(IDatabase db)
            {
                db.BulkInsert(generatorFor1kb.GetDocumentsPopulateDatabase);
                Console.WriteLine("Database is highly Filled");
            }
        }
    }

    /// <summary>
    /// Interface for Storing the Measureresults
    /// </summary>
    public interface IMeasurementStore
    {
        void SaveResult(string measureOnDatabase, double time, string documentSize, string database);
        public List<LogResult> getLogResults
        {
            get;
        }
    }

    /// <summary>
    /// Concrete Implementation of a Store for Measurements
    /// </summary>
    class MeasurementStore : IMeasurementStore
    {
        private List<LogResult> logResults = new List<LogResult>();

        public List<LogResult> getLogResults
        {
            get { return logResults; }
        }

        public void SaveResult( string measureOnDatabase, double timeResult, string documentSize, string database)
        {
            logResults.Add(new LogResult()
            {
                _measureOnDatabase = measureOnDatabase,
                _timeResult = timeResult,
                _documentSize = documentSize,
                _database = database
            });
        }
    }

    public class LogResult
    {
        public string _database { get; set; }
        public string _measureOnDatabase { get; set; }
        public double _timeResult { get; set; }
        public string _documentSize { get; set; }
    }

    /// <summary>
    /// Concrete Implementation for the Export of the Measurementresults
    /// </summary>
    public class Export
    {
        private IMeasurementStore _measurementStore;
        private IResultLogger _resultLogger;
        private string loggingString;

        public Export(IMeasurementStore measurementStore, IResultLogger resultLogger)
        {
            if (measurementStore == null || resultLogger == null)
            {
                throw new NullReferenceException();
            }
            this._measurementStore = measurementStore;
            this._resultLogger = resultLogger;
        }

        public void ConfigureResult()
        {
            var groupedDatabases = _measurementStore.getLogResults
            .GroupBy(d => d._database)
            .Select(grp => grp)
            .ToList();

            foreach (var groupedDatabase in groupedDatabases)
            {
                var groupedResults = groupedDatabase
                    .GroupBy(r => r._measureOnDatabase)
                    .Select(grp => grp)
                    .ToList();

                loggingString += $"{groupedDatabase.Key}\n";
                foreach (var groupresult in groupedResults)
                {
                    string finalresult = groupresult.Key;
                    foreach (var result in groupresult)
                    {
                        finalresult += "," + result._timeResult;
                    }
                    loggingString += finalresult + Environment.NewLine;
                }
            }
            loggingString += "\n";

            _resultLogger.LogResult(loggingString);
        }
    }
}



