using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kauffman.Logger
{
    public class Logger
    {
        public static void Log(string functionName, string message)
        {
            string path = string.Empty; //AppDomain.CurrentDomain.BaseDirectory + "/bin";
            //string strPath = HttpContext.Current.Server.MapPath("~/bin");
            //System.Web.HttpRuntime.BinDirectory

            if (!string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["LogFilePath"])))
                path = Convert.ToString(ConfigurationManager.AppSettings["LogFilePath"]);

            bool isLoggingEnabled = false;
            if (!string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["isLoggingEnabled"])))
                isLoggingEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["isLoggingEnabled"]);

            if(string.IsNullOrEmpty(path))
                isLoggingEnabled = false;

            string fileName = DateTime.Now.ToString("MM-dd-yyy") + "_Logs.txt";
            string folderName = "Logs";
            path = Path.Combine(path, folderName);
            path = Path.Combine(path, fileName);

            if (isLoggingEnabled)
            {
                if (!System.IO.File.Exists(path))
                {
                    FileStream fs = System.IO.File.Create(path);//.Dispose();
                    using (TextWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("Method		: " + functionName);
                        sw.WriteLine("Message		: " + message);
                        ; sw.WriteLine("Time		: " + DateTime.Now.ToLongTimeString());
                        sw.WriteLine("Date		: " + DateTime.Now.ToShortDateString());
                        sw.WriteLine("==========================================================================");
                        sw.Close();
                    }

                }

                else if (System.IO.File.Exists(path))
                {
                    using (TextWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine("Method		: " + functionName);
                        sw.WriteLine("Message		: " + message);
                        sw.WriteLine("Time		: " + DateTime.Now.ToLongTimeString());
                        sw.WriteLine("Date		: " + DateTime.Now.ToShortDateString());
                        sw.WriteLine("==========================================================================");
                        sw.Close();
                    }
                }
            }
        }
    }
}
