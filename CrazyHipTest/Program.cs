using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrazyHip.Tool;

namespace CrazyHipTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager log = new LogManager();
            for (int idx = 0; idx < 10; idx++)
            {
               // log.WriteLine("로그 기록 테스트 = " + idx.ToString());
                log.WriteConsolAndLog("로그 기록 테스트 = " + idx.ToString());
                
            }

            log.WriteWithTime("시간테스트");
            log.WriteWithTime("시간테스트", "yyMMdd");


            Console.WriteLine(LogManager.RootDir);

            Console.ReadLine();

            LogManager log2 = new LogManager(System.IO.Path.Combine(LogManager.RootDir, "poohhw"), LogType.Daily);
            log2.WriteConsolAndLog("57575757");
        }
    }
}
