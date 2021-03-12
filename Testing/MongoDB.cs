using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLBenchmarker
{
    class MongoDB : IDatabase
    {
        private const string uri = "mongodb://localhost/test";
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Document> collection;
        public MongoDB()
        {
            client = new MongoClient(uri);
            db = client.GetDatabase("test");
            collection = db.GetCollection<Document>("testCollection");
        }

        public void CleanDatabase()
        {
            collection.DeleteMany(Builders<Document>.Filter.Empty);
        }
        public void BulkDelete(List<Document> documents)
        {
            documents.ForEach(delegate (Document document)
            {
                var deletionFilter = Builders<Document>.Filter.Eq(d => d.ID, document.ID);
                collection.DeleteOne(deletionFilter);
            });
        }

        public void BulkInsert(List<Document> documents)
        {
            documents.ForEach(delegate (Document document)
            {
                collection.InsertOne(document);
            });
        }

        public void BulkSelect(List<Document> documents)
        {
            documents.ForEach(delegate (Document document)
            {
                var selectionFilter = Builders<Document>.Filter.Eq(d => d.ID, document.ID);
                collection.Find(selectionFilter);
            });
        }

        public void BulkUpdate(List<Document> documents)
        {
            documents.ForEach(delegate (Document document)
            {
                collection.ReplaceOne(d => d.ID == document.ID, document);
            });
        }

        public void SingleDelete(Document document)
        {
            var deletionFilter = Builders<Document>.Filter.Eq(d => d.ID, document.ID);
            collection.DeleteOne(deletionFilter);
        }

        public void SingleInsert(Document document)
        {
            collection.InsertOne(document);
        }

        public void SingleSelect(Document document)
        {
            var selectionFilter = Builders<Document>.Filter.Eq(d => d.ID, document.ID);
            collection.Find(selectionFilter);
        }

        public void SingleUpdate(Document document)
        {
            collection.ReplaceOne(d => d.ID == document.ID, document);
        }
    }

}
