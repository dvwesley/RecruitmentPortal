using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Hosting;
using System.Configuration;


namespace RecruitmentPortal.Common
{
    public class Logger
    {
        private const string divider = "========================================================";
        private const string dividerException = "======================= EXCEPTION ======================";
        private const string dividerMessageException = "================== MESSAGE & EXCEPTION =================";

        private static String BuildExceptionText(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine(dividerException);
            sb.AppendLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"));
            sb.AppendLine("Information:" + ex.Message);
            sb.AppendLine("Source:" + ex.Source);
            sb.AppendLine("Stack Trace: " + ex.StackTrace);
            Exception inner = ex.InnerException;
            while (inner != null)
            {
                sb.AppendLine("More information:" + inner.Message);
                sb.AppendLine("Source:" + inner.Source);
                sb.AppendLine("Stack Trace: " + inner.StackTrace);
                inner = inner.InnerException;
            }
            sb.AppendLine(divider);
            return sb.ToString();
        }

        public static void LogException(Exception exp)
        {
            LogMessage(BuildExceptionText(exp));
        }

        public static void LogMessage(String message)
        {
            StreamWriter sw = null;
            String logFolder = Path.Combine(HostingEnvironment.MapPath("~/"), ConfigurationManager.AppSettings["LogFolder"]);
            String logFilename = "RecruitmentPortal_log_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            String filePath = "";
            try
            {
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }
                filePath = Path.Combine(logFolder, logFilename);
                sw = new StreamWriter(filePath, true);
                sw.WriteLine(message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }        
    }
}
