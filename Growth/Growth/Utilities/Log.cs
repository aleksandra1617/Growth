using System.Diagnostics;

namespace Growth.Utilities
{
    public class Log
    {
        public enum E_LOG_EVENT_IDS { Unidentified, NewDataEntry, DataDeleted }

        public static string GetFormattedCallInfo()
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            return "<<"+ className +"::"+ methodInfo.Name +">>";
        }
    }
}
