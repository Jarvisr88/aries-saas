namespace DevExpress.Xpo.Logger
{
    using System;

    public class DummyServer : ILogger
    {
        public void ClearLog()
        {
        }

        public void Log(LogMessage message)
        {
        }

        public void Log(LogMessage[] messages)
        {
        }

        public int Count =>
            0;

        public int LostMessageCount =>
            0;

        public bool IsServerActive =>
            false;

        public bool Enabled
        {
            get => 
                false;
            set
            {
            }
        }

        public int Capacity =>
            0;
    }
}

