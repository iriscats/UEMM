

using System;
using System.Globalization;
using System.IO;
using System.Windows;

namespace UEMM.Code
{
    internal class UnhandledException
    {
        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            if (args.ExceptionObject is not Exception exception) return;

            DisplayBox(exception, WriteToLog(exception));
        }

        protected static string WriteToLog(Exception exception)
        {
            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "uemm-error.txt");
            var reportMessage = BuildReportMessage(exception);

            if (File.Exists(path))
                File.AppendAllText(path, "\n\n" + reportMessage);
            else
                File.WriteAllText(path, reportMessage);

            return path;
        }

        protected static void DisplayBox(Exception exception, string reportPath = "")
        {
            var message = "Message: " + exception.Message + "\nHash: " + exception.StackTrace?.GetHashCode();
            message += "\nSupport: https://github.com/lepoco/uemm/";

            if (!String.IsNullOrEmpty(reportPath))
                message += "\n\nReport saved to:\n" + reportPath;

            MessageBox.Show(message, "Whoa! Cyberpunk 2077 Mod Manager has flatlined.");
        }

        protected static string BuildReportMessage(Exception exception)
        {
            var message = "--------------------------------------------------------------------------------";
            message += "\n" + DateTime.Now.ToString(new CultureInfo("en-US")) + " CYBERPUNK 2077 MOD MANAGER ERROR";
            message += "\nSupport: https://github.com/lepoco/uemm/";

            message += "\n" + exception.Message;
            message += "\n" + exception.Source;
            message += "\n" + exception.StackTrace?.GetHashCode();
            message += "\n\n" + exception;

            return message;
        }
    }
}
