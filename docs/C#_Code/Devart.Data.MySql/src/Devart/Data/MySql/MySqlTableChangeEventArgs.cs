namespace Devart.Data.MySql
{
    using System;

    public class MySqlTableChangeEventArgs : EventArgs
    {
        private string a;
        private MySqlCommand b;
        private string c;

        internal MySqlTableChangeEventArgs(MySqlCommand A_0, string A_1, string A_2);

        public MySqlCommand Command { get; }

        public string TableName { get; }

        public string Reason { get; }
    }
}

