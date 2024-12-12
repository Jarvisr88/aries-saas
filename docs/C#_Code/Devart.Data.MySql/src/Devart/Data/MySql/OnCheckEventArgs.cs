namespace Devart.Data.MySql
{
    using System;
    using System.Data;

    public class OnCheckEventArgs
    {
        private readonly MySqlCommand a;
        private readonly MySqlConnection b;
        private readonly DataTable c;
        private readonly string d;
        private string e;
        private string f;
        private MySqlCommand g;
        private MySqlDependencyCheckType h;

        internal OnCheckEventArgs(MySqlCommand A_0, MySqlConnection A_1, DataTable A_2, string A_3, string A_4, string A_5, MySqlCommand A_6, MySqlDependencyCheckType A_7);

        public MySqlCommand Command { get; }

        public MySqlConnection Connection { get; }

        public DataTable Schema { get; }

        public string TableName { get; }

        public string Hash { get; set; }

        public string Reason { get; set; }

        public MySqlCommand CheckCommand { get; set; }

        public MySqlDependencyCheckType CheckType { get; }
    }
}

