namespace DMEWorks.Data
{
    using DMEWorks.Core;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Data;

    public class TableInvoiceTransactionType : TableBase
    {
        public DataColumn Col_ID;
        public DataColumn Col_Name;
        public DataColumn Col_Balance;
        public DataColumn Col_Allowable;
        public DataColumn Col_Amount;
        public DataColumn Col_Taxes;

        public TableInvoiceTransactionType() : this("tbl_invoice_transactiontype")
        {
        }

        public TableInvoiceTransactionType(string TableName)
        {
            base.TableName = TableName;
        }

        public int FindIDByName(string Name)
        {
            DataRow[] rowArray = base.Select($"name = '{Name.Replace("'", "''")}'");
            if (rowArray == null)
            {
                throw new Exception();
            }
            if (rowArray.Length < 0)
            {
                throw new Exception();
            }
            if (1 < rowArray.Length)
            {
                throw new Exception();
            }
            return Conversions.ToInteger(rowArray[0][this.Col_ID]);
        }

        protected override void Initialize()
        {
            this.Col_ID = base.Columns["ID"];
            this.Col_Name = base.Columns["Name"];
            this.Col_Balance = base.Columns["Balance"];
            this.Col_Allowable = base.Columns["Allowable"];
            this.Col_Amount = base.Columns["Amount"];
            this.Col_Taxes = base.Columns["Taxes"];
        }

        protected override void InitializeClass()
        {
            DataView defaultView = base.DefaultView;
            defaultView.AllowDelete = false;
            defaultView.AllowEdit = false;
            defaultView.AllowNew = false;
            this.Col_ID = base.Columns.Add("ID", typeof(int));
            this.Col_ID.AllowDBNull = false;
            this.Col_Name = base.Columns.Add("Name", typeof(string));
            this.Col_Name.AllowDBNull = false;
            this.Col_Balance = base.Columns.Add("Balance", typeof(int));
            this.Col_Balance.AllowDBNull = false;
            this.Col_Allowable = base.Columns.Add("Allowable", typeof(int));
            this.Col_Allowable.AllowDBNull = false;
            this.Col_Amount = base.Columns.Add("Amount", typeof(int));
            this.Col_Amount.AllowDBNull = false;
            this.Col_Taxes = base.Columns.Add("Taxes", typeof(int));
            this.Col_Taxes.AllowDBNull = false;
        }
    }
}

