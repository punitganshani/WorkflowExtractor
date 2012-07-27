using System;

namespace WFE.Lib
{
    public static class Log
    {
        internal static void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        internal static void Error(string message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        internal static void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
