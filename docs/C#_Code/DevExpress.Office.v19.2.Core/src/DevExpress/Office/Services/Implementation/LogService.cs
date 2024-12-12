namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using System;
    using System.Collections.Generic;

    public class LogService : ILogService
    {
        private readonly List<LogEntry> innerList = new List<LogEntry>();

        public void Clear()
        {
            this.innerList.Clear();
        }

        public void LogMessage(LogCategory category, string message)
        {
            this.innerList.Add(new LogEntry(category, message));
        }

        public bool IsEmpty =>
            this.innerList.Count == 0;

        public IEnumerable<LogEntry> Entries =>
            this.innerList;
    }
}

