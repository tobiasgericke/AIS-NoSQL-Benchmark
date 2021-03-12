using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NoSQLBenchmarker
{
    public interface IResultLogger
    {
        void LogResult(string logResults);
    }

    public class CSVResultLogger : IResultLogger
    {
        private string _path = @"MyTest.csv";

        public void LogResult(string loggingString)
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }

            using (FileStream fs = File.Create(_path))
            {
                AddText(fs, loggingString);
            }
        }

        private void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
