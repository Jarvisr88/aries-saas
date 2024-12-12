namespace DevExpress.Office.Services
{
    using System;
    using System.Collections.Generic;

    public interface ILogService
    {
        void Clear();
        void LogMessage(LogCategory category, string message);

        bool IsEmpty { get; }

        IEnumerable<LogEntry> Entries { get; }
    }
}

