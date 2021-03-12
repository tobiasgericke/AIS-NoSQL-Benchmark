using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLBenchmarker
{
    public interface IDocumentGenerator
    {
        List<Document> GetDocumentsForMeasurement { get; }
        List<Document> GetDocumentsPopulateDatabase { get; }
    }

    class GeneratorFor1kb : IDocumentGenerator
    {
        public List<Document> documentsForMeasurement = new List<Document>();

        private List<Document> documentsPopulateDatabase = new List<Document>();

        List<Operatoren> operatorList = new List<Operatoren>();

        public GeneratorFor1kb()
        {
            generateOperators();
            FillMeasureList(1000);
            FillPopulateList(1100000);
        }

        public List<Document> GetDocumentsForMeasurement
        {
            get { return documentsForMeasurement; }
        }
        public List<Document> GetDocumentsPopulateDatabase
        {
            get { return documentsPopulateDatabase; }
        }

        private void generateOperators()
        {
            for (int i = 0; i < 15; i++)
            {
                operatorList.Add(new Operatoren() { Name = "Lance", Shoesize = 21 });
            }
        }

        //Erzeuge X (1kb groß) Dokumente mit ID 1...10.000
        private void FillMeasureList(int maxID)
        {
            var index = 1;

            while (index <= maxID)
            {
                documentsForMeasurement.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }

        //Erzeuge X (1kb groß) Dokumente mit ID 1.000.000++
        private void FillPopulateList(int maxID)
        {
            var index = 1000000;

            while (index < maxID)
            {
                documentsPopulateDatabase.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }
    }


    class GeneratorFor10kb : IDocumentGenerator
    {

        public List<Document> documentsForMeasurement = new List<Document>();

        private List<Document> documentsPopulateDatabase = new List<Document>();

        List<Operatoren> operatorList = new List<Operatoren>();

        public GeneratorFor10kb()
        {
            generateOperators();
            FillMeasureList(1000);
            FillPopulateList(1100000);
        }

        public List<Document> GetDocumentsForMeasurement
        {
            get { return documentsForMeasurement; }
        }
        public List<Document> GetDocumentsPopulateDatabase
        {
            get { return documentsPopulateDatabase; }
        }

        private void generateOperators()
        {
            for (int i = 0; i < 160; i++)
            {
                operatorList.Add(new Operatoren() { Name = "Lance", Shoesize = 21 });
            }
        }

        //Erzeuge X (1kb groß) Dokumente mit ID 0...10.000
        private void FillMeasureList(int maxID)
        {
            var index = 1;

            while (index <= maxID)
            {
                documentsForMeasurement.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }

        //Erzeuge X (1kb groß) Dokumente mit ID 1.000.000++
        private void FillPopulateList(int maxID)
        {
            var index = 1000000;

            while (index < maxID)
            {
                documentsPopulateDatabase.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }
    }


    class GeneratorFor100kb : IDocumentGenerator
    {

        public List<Document> documentsForMeasurement = new List<Document>();

        private List<Document> documentsPopulateDatabase = new List<Document>();

        List<Operatoren> operatorList = new List<Operatoren>();

        public GeneratorFor100kb()
        {           
            generateOperators();
            FillMeasureList(1000);
            FillPopulateList(1100000);
        }

        public List<Document> GetDocumentsForMeasurement
        {
            get { return documentsForMeasurement; }
        }
        public List<Document> GetDocumentsPopulateDatabase
        {
            get { return documentsPopulateDatabase; }
        }

        private void generateOperators()
        {
            for (int i = 0; i < 1616; i++)
            {
                operatorList.Add(new Operatoren() { Name = "Lance", Shoesize = 21 });
            }
        }

        //Erzeuge X (100kb groß) Dokumente mit ID 0...10.000
        private void FillMeasureList(int maxID)
        {
            var index = 1;
            while (index <= maxID)
            {
                documentsForMeasurement.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }

        //Erzeuge X (100kb groß) Dokumente mit ID 1.000.000++
        private void FillPopulateList(int maxID)
        {
            var index = 1000000;
            while (index < maxID)
            {
                documentsPopulateDatabase.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }
    }


    class GeneratorFor1000kb : IDocumentGenerator
    {

        public List<Document> documentsForMeasurement = new List<Document>();

        private List<Document> documentsPopulateDatabase = new List<Document>();

        List<Operatoren> operatorList = new List<Operatoren>();

        public GeneratorFor1000kb()
        {
            generateOperators();
            FillMeasureList(1000);
            FillPopulateList(1100000);
        }

        public List<Document> GetDocumentsForMeasurement
        {
            get { return documentsForMeasurement; }
        }
        public List<Document> GetDocumentsPopulateDatabase
        {
            get { return documentsPopulateDatabase; }
        }

        private void generateOperators()
        {
            for (int i = 0; i < 16130; i++)
            {
                operatorList.Add(new Operatoren() { Name = "Lance", Shoesize = 21 });
            }
        }

        //Erzeuge X (1000kb groß) Dokumente mit ID 0...10.000
        private void FillMeasureList(int maxID)
        {
            var index = 1;
            while (index <= maxID)
            {
                documentsForMeasurement.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }

        //Erzeuge X (1000kb groß) Dokumente mit ID 1.000.000++
        private void FillPopulateList(int maxID)
        {
            var index = 1000000;
            while (index < maxID)
            {
                documentsPopulateDatabase.Add(new Document()
                {
                    ID = index++,
                    machineName = "Rolle",
                    Operatoren = operatorList
                });
            }
        }
    }

    public class Document
    {
        public int ID { get; set; }
        public string machineName { get; set; }
        public List<Operatoren> Operatoren { get; set; } = new List<Operatoren>();
    }

    public class Operatoren
    {
        public string Name { get; set; }
        public int Shoesize { get; set; }
    }

}
