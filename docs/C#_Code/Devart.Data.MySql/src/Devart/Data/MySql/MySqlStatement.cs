namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;

    public class MySqlStatement : SqlStatement
    {
        private readonly Devart.Data.MySql.MySqlStatementType a;

        internal MySqlStatement(MySqlScript A_0, int A_1, int A_2, int A_3, int A_4, string A_5, SqlStatementType A_6, Devart.Data.MySql.MySqlStatementType A_7);
        public MySqlDataReader Execute();

        public Devart.Data.MySql.MySqlStatementType MySqlStatementType { get; }
    }
}

