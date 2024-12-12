namespace DevExpress.Xpo.Logger
{
    using System;

    public interface ILogger
    {
        void ClearLog();
        void Log(LogMessage message);
        void Log(LogMessage[] messages);

        int Count { get; }

        int LostMessageCount { get; }

        bool IsServerActive { get; }

        bool Enabled { get; set; }

        int Capacity { get; }
    }
}

