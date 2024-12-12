namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;

    public class MySqlStatementExecuteEventArgs : DbStatementExecuteEventArgs
    {
        private readonly Devart.Data.MySql.MySqlStatementType a;

        internal MySqlStatementExecuteEventArgs(string A_0, int A_1, int A_2, int A_3, int A_4, SqlStatementType A_5, Devart.Data.MySql.MySqlStatementType A_6);

        public MySqlDataReader Reader { get; set; }

        public Devart.Data.MySql.MySqlStatementType MySqlStatementType { get; }
    }
}

