using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportly.Class
{
    public static class ExceptionLogging
    {

        private static String ErrorType, ExceptionType, ErrorMessage;
        private static int ErrorlineNo;

        public static void LogError(Exception ex)
        {
            var line = Environment.NewLine;
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);

            ErrorlineNo = frame.GetFileLineNumber();
            ExceptionType = ex.GetType().ToString();
            ErrorMessage = ex.Message.ToString();

            try
            {
                string filepath = Directory.GetCurrentDirectory() + "/ExceptionDetailsFile/";  //Text File Path
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                    File.Create(filepath).Dispose();
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Error Line No :" + " " + ErrorlineNo + line + "Exception Type:" + " " + ExceptionType + line + "Error Message :" + " " + ErrorMessage;
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine("Exception Details on " + " " + DateTime.Now.ToString());
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(error);
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

    }
}
