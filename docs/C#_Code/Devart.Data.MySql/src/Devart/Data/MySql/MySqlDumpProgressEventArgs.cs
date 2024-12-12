namespace Devart.Data.MySql
{
    using System;

    public sealed class MySqlDumpProgressEventArgs : EventArgs
    {
        private readonly string a;
        private readonly string b;
        private readonly int c;
        private readonly int d;
        private readonly long e;
        private readonly long f;

        internal MySqlDumpProgressEventArgs(string A_0, string A_1, int A_2, int A_3, long A_4, long A_5);

        public string ObjectName { get; }

        public string ObjectType { get; }

        public int ObjectNumber { get; }

        public int ObjectCount { get; }

        public long Progress { get; }

        public long MaxProgress { get; }
    }
}

