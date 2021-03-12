using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLBenchmarker
{
    /// <summary>
    /// Interface for Databases
    /// </summary>
    public interface IDatabase
    {
        public void CleanDatabase();
        public void SingleInsert(Document document);
        public void SingleUpdate(Document document);
        public void SingleDelete(Document document);
        public void SingleSelect(Document document);
        public void BulkInsert(List<Document> documents);
        public void BulkUpdate(List<Document> documents);
        public void BulkDelete(List<Document> documents);
        public void BulkSelect(List<Document> documents);

    }
}
