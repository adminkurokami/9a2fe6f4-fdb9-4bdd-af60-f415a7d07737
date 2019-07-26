using FirebirdSql.Data.FirebirdClient;
using System;
using System.Diagnostics;
using TestFirebird.Model;

namespace TestFirebird
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FbConnectionStringBuilder s = new FbConnectionStringBuilder
            {
                Database = "MYDB.DB",
                Dialect = 3,
                ServerType = FbServerType.Embedded,
                Password = "masterkey",
                UserID = "SYSDBA",
                ClientLibrary = "fbembed.dll",
               // Pooling = true,
              //  DbCachePages = 50,
               // MaxPoolSize = 10,
              //  ConnectionLifeTime = 0,
              //  ConnectionTimeout = 15
            };

            Controller controller = new Controller(s.ConnectionString);

            string universityName = Guid.NewGuid().ToString();
            controller.AddUniversity(universityName);

            int n = 0;
            int count = 0;
            long sum = 0;
            DateTime date = DateTime.Now;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                try
                {
                    controller.test();
                   // controller.AddStudent(new Student { Name = Guid.NewGuid().ToString() }, universityName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("#" + count + " " + stopwatch.ElapsedMilliseconds + "ms avg: " + ((double)stopwatch.ElapsedMilliseconds / count).ToString("0.00") + "ms " + (int)(DateTime.Now - date).TotalSeconds);
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException.Message);
                    Console.ReadLine();
                    
                }
                n++;
                count++;
                if (n == 100)
                {
                    stopwatch.Stop();
                    sum += stopwatch.ElapsedMilliseconds;
                    Console.WriteLine("#" + count + " " + stopwatch.ElapsedMilliseconds + "ms avg: " + ((double)sum / count).ToString("0.00") + "ms " + (int)(DateTime.Now - date).TotalSeconds);
                    n = 0;
                    stopwatch.Restart();
                }
            }
        }
    }
}
