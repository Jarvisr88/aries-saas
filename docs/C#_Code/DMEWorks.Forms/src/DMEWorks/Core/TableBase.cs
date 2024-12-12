namespace DMEWorks.Core
{
    using System;
    using System.Data;

    public abstract class TableBase : DataTable
    {
        protected TableBase() : this("")
        {
        }

        protected TableBase(string tableName) : base(tableName)
        {
            this.InitializeClass();
            this.Initialize();
        }

        public override DataTable Clone()
        {
            DataTable table1 = base.Clone();
            ((TableBase) table1).Initialize();
            return table1;
        }

        public void CopyFrom(DataTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            base.Clear();
            base.AcceptChanges();
            foreach (DataRow row in table.Rows)
            {
                base.ImportRow(row);
            }
        }

        public void CopyTo(DataTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            table.Clear();
            table.AcceptChanges();
            foreach (DataRow row in base.Rows)
            {
                table.ImportRow(row);
            }
        }

        protected abstract void Initialize();
        protected abstract void InitializeClass();
    }
}

