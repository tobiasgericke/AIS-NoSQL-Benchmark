using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLBenchmarker
{
    /// <summary>
    /// Interface for the MeasureAlgorithms
    /// </summary>
    public interface IMeasureAlgorithm
    {
        double StartMeasure(List<Document> documents);
        void CleanUp(IDatabase db, IDocumentGenerator dg);
    }

    public class SingleInsert : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public SingleInsert(IDatabase database, ILogger logger) 
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            if(documents == null)
            {
                throw new NullReferenceException();
            }

            var logger = new ConsoleLogger();
            var start = DateTime.Now;
            for(int i = 0; i < documents.Count; ++i)
            {
                _database.SingleInsert(documents[i]);
            }

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"SingleInsert took {finish} seconds.");

            return finish;
        }

        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

    class BulkInsert : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public BulkInsert(IDatabase database, ILogger logger)
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            var start = DateTime.Now;

            _database.BulkInsert(documents);

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"BulkInsert took {finish} seconds.");

            return finish;
        }
        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

    public class SingleUpdate : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public SingleUpdate(IDatabase database, ILogger logger)
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            if (documents == null)
            {
                throw new NullReferenceException();
            }
            var logger = new ConsoleLogger();
            for (int i = 0; i < documents.Count; ++i)
            {
                //can be Bulkinsert later
                _database.SingleInsert(documents[i]);
            }

            var start = DateTime.Now;

            for (int i = 0; i < documents.Count; ++i)
            {
                _database.SingleUpdate(documents[i]);
            }

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"SingleUpdate took {finish} seconds.");
            return finish;
        }
        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

    class BulkUpdate : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public BulkUpdate(IDatabase database, ILogger logger)
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            var start = DateTime.Now;

            _database.BulkUpdate(documents);

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"BulkUpdate took {finish} seconds.");

            return finish;
        }
        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

    public class SingleDelete : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public SingleDelete(IDatabase database, ILogger logger)
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            if (documents == null)
            {
                throw new NullReferenceException();
            }
            var start = DateTime.Now;

            for (int i = 0; i < documents.Count; ++i)
            {
                _database.SingleDelete(documents[i]);
            }

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"SingleDelete took {finish} seconds.");
            return finish;
        }
        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

    class BulkDelete : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public BulkDelete(IDatabase database, ILogger logger)
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            var start = DateTime.Now;

            _database.BulkDelete(documents);

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"BulkDelete took {finish} seconds.");

            return finish;
        }
        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

    class SingleSelect : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public SingleSelect(IDatabase database, ILogger logger)
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            var start = DateTime.Now;

            for (int i = 0; i < documents.Count; ++i)
            {
                _database.SingleSelect(documents[i]);
            }

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"SingleSelect took {finish} seconds.");

            return finish;
        }
        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

    class BulkSelect : IMeasureAlgorithm
    {
        private IDatabase _database;
        private ILogger _logger;
        public BulkSelect(IDatabase database, ILogger logger)
        {
            if (database == null || logger == null)
            {
                throw new NullReferenceException();
            }
            this._database = database;
            this._logger = logger;
        }
        public double StartMeasure(List<Document> documents)
        {
            var start = DateTime.Now;

            _database.BulkSelect(documents);

            var finish = (DateTime.Now - start).TotalSeconds;
            _logger.Log($"BulkSelect took {finish} seconds.");

            return finish;
        }
        public void CleanUp(IDatabase db, IDocumentGenerator dg)
        {
            db.BulkDelete(dg.GetDocumentsForMeasurement);
        }
    }

}
