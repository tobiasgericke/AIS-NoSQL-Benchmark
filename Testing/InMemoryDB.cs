using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoSQLBenchmarker
{
    public class InMemory : IDatabase
    {
        private List<Document> documentStore = new List<Document>();
        public void BulkDelete(List<Document> documents)
        {
            foreach (var document in documents)
            {
                documentStore.Remove(document);
            }
        }

        public void BulkInsert(List<Document> documents)
        {
            foreach (var document in documents)
            {
                documentStore.Add(document);
            }
        }

        public void BulkSelect(List<Document> documents)
        {
            foreach (var document in documents)
            {
                documentStore.Find(d => d.ID == document.ID);
            }
        }

        public void BulkUpdate(List<Document> documents)
        {
            foreach (var document in documents)
            {
                documentStore.FirstOrDefault(d => d.ID == document.ID);
            }
        }

        public void CleanDatabase()
        {
            documentStore.Clear();
        }

        public void SingleDelete(Document document)
        {
            documentStore.Remove(document);
        }

        public void SingleInsert(Document document)
        {
            documentStore.Add(document);
        }

        public void SingleSelect(Document document)
        {
            documentStore.Find(d => d.ID == document.ID);
        }

        public void SingleUpdate(Document document)
        {
            documentStore.FirstOrDefault(d => d.ID == document.ID);
        }
    }
}
