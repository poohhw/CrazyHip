using System;
using System.IO;

namespace CrazyHip.Tool
{
    public enum LogType {  Daily, Monthly }
    public class LogManager
    {
        private string _path;

        public static string RootDir
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public LogManager(string path,LogType logType)
        {
            _path = path;
            _SetPath(logType);
        }

        public LogManager() 
            :this(System.IO.Path.Combine(RootDir,"Log"),LogType.Daily)
        {
           
        }

        private void _SetPath(LogType logType)
        {
            string path = string.Empty;
            string name = string.Empty;

            switch(logType)
            {
                case LogType.Daily:
                    path = string.Format(@"{0}\{1}\", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                    name = DateTime.Now.ToString("yyyyMMdd") + "_Log.txt";
                    break;
                case LogType.Monthly:
                    path = string.Format(@"{0}\", DateTime.Now.Year);
                    name = DateTime.Now.ToString("yyyyMM") + "_Log.txt";
                    break;
            }

            
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            _path = Path.Combine(_path, name);
        }

        public void Write(string data)
        {
            try
            {
                
                using (StreamWriter sw = new StreamWriter(_path, false))
                {
                    sw.Write(data);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void WriteLine(string data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_path, false))
                {
                    sw.WriteLine(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void WriteConsolAndLog(string data)
        {
            Console.WriteLine(data);
            WriteLine(data);
        }

        public void WriteWithTime(string data,string TimeType)
        {
            string result = "[" + DateTime.Now.ToString(TimeType) + "] " + data;
            WriteLine(result);
        }

        public void WriteWithTime(string data)
        {
            WriteWithTime(data, "yyyy-MM-dd HH:mm:ss");
        }

        
    }
}
