namespace Devart.Data.MySql
{
    using System;

    public class MySqlErrorEventArgs : EventArgs
    {
        private readonly int a;
        private readonly string b;
        private readonly string c;

        internal MySqlErrorEventArgs(int A_0, string A_1, string A_2);

        public string Message { get; }

        public int Code { get; }

        public string SqlState { get; }
    }
}

