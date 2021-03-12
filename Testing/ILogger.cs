using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLBenchmarker
{
    public interface ILogger
    {
        void Log(string log);
    }


    class ConsoleLogger : ILogger
    {
        public void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}
