using System;

namespace ajf.ns_planner.shared2.ViewModels
{
    public class LogItemViewModel
    {
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public string TimeString
        {
            get { return Time.ToLongTimeString(); }
        }

        public LogItemViewModel()
        {
            Time = DateTime.Now;
        }
        public static LogItemViewModel CreateInfo(string message)
        {
            return new LogItemViewModel
            {
                Type="Info",
                Message = message
            };
        }

        public static LogItemViewModel CreateError(string message)
        {
            return new LogItemViewModel
            {
                Type = "Fejl",
                Message = message
            };
        }

        public static LogItemViewModel CreateWarning(string message)
        {
            return new LogItemViewModel
            {
                Type = "Advarsel",
                Message = message
            };
        }
    }
}