namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;

    public class CacheRecord
    {
        private string[] tablesInStatement;
        private SelectStatement statement;
        public CacheRecord Prev;
        public CacheRecord Next;
        public readonly string HashString;
        private SelectStatementResult queryResult;

        public CacheRecord(SelectStatement statement)
        {
            this.statement = statement;
            this.HashString = QueryStatementToStringFormatter.GetString(statement);
        }

        public override bool Equals(object obj)
        {
            CacheRecord record = obj as CacheRecord;
            return ((record != null) ? (this.HashString == record.HashString) : false);
        }

        public override int GetHashCode() => 
            this.HashString.GetHashCode();

        public override string ToString() => 
            this.HashString;

        public string[] TablesInStatement
        {
            get
            {
                if (this.tablesInStatement == null)
                {
                    this.tablesInStatement = this.statement.GetTablesNames();
                    this.statement = null;
                }
                return this.tablesInStatement;
            }
        }

        public string TableName =>
            ((this.statement == null) || (this.statement.Table is DBProjection)) ? this.tablesInStatement[0] : this.statement.Table.Name;

        public SelectStatementResult QueryResult
        {
            get => 
                this.queryResult.Clone();
            set => 
                this.queryResult = value.Clone();
        }
    }
}

