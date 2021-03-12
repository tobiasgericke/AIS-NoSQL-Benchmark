using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLBenchmarker
{

    //TODO wie anbinden?
    class CouchDB : IDatabase
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

        public void SingleDelete(Document document)
        {
            throw new NotImplementedException();
        }

        public void SingleInsert(Document document)
        {
            throw new NotImplementedException();
        }

        public void SingleSelect(Document document)
        {
            throw new NotImplementedException();
        }

        public void SingleUpdate(Document document)
        {
            throw new NotImplementedException();
        }
    }

}
