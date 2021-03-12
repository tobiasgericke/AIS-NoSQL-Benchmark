using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoSQLBenchmarker
{

    /// <summary>
    /// Concrete Implementation for PostgreSQL/Marten
    /// </summary>
    class PostGreSQL : IDatabase
    {
        private readonly DocumentStore store = DocumentStore
            .For("host=localhost;database=test;password=admin;username=postgres");

        public void CleanDatabase()
        {
            //store.Advanced.Clean.CompletelyRemoveAll();
            store.Advanced.Clean.DeleteDocumentsFor(typeof(Document));
            //store.Advanced.Clean.DeleteAllDocuments();
        }

        public void BulkDelete(List<Document> documents)
        {
            using (var session = store.OpenSession())
            {
                documents.ForEach(delegate (Document document)
                {
                    session.Delete(document);
                });
                session.SaveChanges();
            }
        }

        public void BulkInsert(List<Document> documents)
        {
            using (var session = store.OpenSession())
            {
                store.BulkInsert(documents); // (data, mode) might need to ignoreDuplicates
                session.SaveChanges();
            }
        }

        public void BulkSelect(List<Document> documents)
        {
            using (var session = store.OpenSession())
            {
                documents.ForEach(delegate (Document document)
                {
                    var selectedDocument = session.Query<Document>().Where(d => d.ID == document.ID);
                });
            }
        }

        public void BulkUpdate(List<Document> documents)
        {
            using (var session = store.OpenSession())
            {
                documents.ForEach(delegate (Document document)
                {
                    session.Store(document);
                });
                session.SaveChanges();
            }
        }

        public void SingleDelete(Document document)
        {
            using (var session = store.OpenSession())
            {
                session.Delete(document);
                session.SaveChanges();
            }
        }

        public void SingleInsert(Document document)
        {
            using (var session = store.OpenSession())
            {
                session.Insert(document);
                session.SaveChanges();
            }
        }

        public void SingleSelect(Document document)
        {
            using (var session = store.OpenSession())
            {
                var selectedDocument = session.Query<Document>().Where(d => d.ID == document.ID);
            }
        }

        public void SingleUpdate(Document document)
        {
            using (var session = store.OpenSession())
            {
                session.Store(document);
                session.SaveChanges();
            }
        }
    }

}
