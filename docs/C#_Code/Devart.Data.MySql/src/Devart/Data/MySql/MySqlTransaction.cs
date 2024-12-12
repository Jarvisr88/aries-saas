namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Data;
    using System.Data.Common;

    public class MySqlTransaction : DbTransactionBase
    {
        private MySqlConnection a;
        private System.Data.IsolationLevel b;
        private bool c;

        internal MySqlTransaction(MySqlConnection A_0);
        internal MySqlTransaction(MySqlConnection A_0, System.Data.IsolationLevel A_1);
        public override void Commit();
        protected override void Dispose(bool disposing);
        public void Release(string savePointName);
        public override void Rollback();
        public void Rollback(string savePointName);
        public void Save(string savePointName);

        protected override System.Data.Common.DbConnection DbConnection { get; }

        public override System.Data.IsolationLevel IsolationLevel { get; }

        public MySqlConnection Connection { get; }
    }
}

