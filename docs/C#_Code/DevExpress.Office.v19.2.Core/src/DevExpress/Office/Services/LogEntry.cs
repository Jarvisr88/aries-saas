namespace DevExpress.Office.Services
{
    using System;
    using System.Runtime.CompilerServices;

    public class LogEntry
    {
        public LogEntry(LogCategory category, string message)
        {
            this.Category = category;
            this.Message = message;
        }

        public override string ToString() => 
            $"{this.Category.ToString()}: {this.Message}";

        public LogCategory Category { get; private set; }

        public string Message { get; private set; }
    }
}

