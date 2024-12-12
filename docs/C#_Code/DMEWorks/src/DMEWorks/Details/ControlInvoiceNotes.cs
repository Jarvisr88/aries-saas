namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;

    public class ControlInvoiceNotes : ControlDetails
    {
        private IContainer components;

        public ControlInvoiceNotes()
        {
            this.InitializeComponent();
        }

        public void ClearGrid()
        {
            base.CloseDialogs();
            int? nullable = null;
            TableInvoiceNotes table = this.GetTable();
            table.CustomerID = nullable;
            nullable = null;
            table.InvoiceID = nullable;
            nullable = null;
            table.InvoiceDetailsID = nullable;
            table.Clear();
            table.AcceptChanges();
        }

        protected override FormDetails CreateDialog(object param) => 
            base.AddDialog(new FormInvoiceNotes(this));

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableInvoiceNotes();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected TableInvoiceNotes GetTable() => 
            (TableInvoiceNotes) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.Name = "ControlInvoiceNotes";
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "ID", 40);
            Appearance.AddTextColumn("Done", "Done", 40);
            Appearance.AddTextColumn("CallbackDate", "Callback Date", 100);
            Appearance.AddTextColumn("Operator", "Operator", 60);
            Appearance.AddTextColumn("Notes", "Notes", 240);
        }

        public void LoadGrid(int CustomerID, int InvoiceID, int InvoiceDetailsID)
        {
            TableInvoiceNotes table = this.GetTable();
            base.CloseDialogs();
            int? nullable = null;
            table.CustomerID = nullable;
            nullable = null;
            table.InvoiceID = nullable;
            nullable = null;
            table.InvoiceDetailsID = nullable;
            table.Clear();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandText = "SELECT\r\n  tbl_invoicenotes.ID\r\n, tbl_invoicenotes.CallbackDate\r\n, tbl_invoicenotes.Done\r\n, tbl_invoicenotes.Notes\r\n, tbl_user.Login as Operator\r\nFROM tbl_invoicenotes\r\n     LEFT JOIN tbl_user ON tbl_invoicenotes.LastUpdateUserID = tbl_user.ID\r\nWHERE (tbl_invoicenotes.CustomerID       = :CustomerID      )\r\n  AND (tbl_invoicenotes.InvoiceID        = :InvoiceID       )\r\n  AND (tbl_invoicenotes.InvoiceDetailsID = :InvoiceDetailsID)\r\nORDER BY tbl_invoicenotes.LastUpdateDatetime DESC";
                adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                adapter.SelectCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                adapter.SelectCommand.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(table);
            }
            table.CustomerID = new int?(CustomerID);
            table.InvoiceID = new int?(InvoiceID);
            table.InvoiceDetailsID = new int?(InvoiceDetailsID);
            table.AcceptChanges();
        }

        public void SaveGrid(int CustomerID, int InvoiceID, int InvoiceDetailsID)
        {
            TableInvoiceNotes table = this.GetTable();
            if (!ValuesAreEqual(table.CustomerID, new int?(CustomerID)) || (!ValuesAreEqual(table.InvoiceID, new int?(InvoiceID)) || !ValuesAreEqual(table.InvoiceDetailsID, new int?(InvoiceDetailsID))))
            {
                throw new Exception("Stored keys differ from parameters");
            }
            TableInvoiceNotes changes = (TableInvoiceNotes) table.GetChanges();
            if (changes != null)
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                    {
                        adapter.InsertCommand = new MySqlCommand("", connection);
                        adapter.InsertCommand.Parameters.Add("CallbackDate", MySqlType.Date, 0, "CallbackDate");
                        adapter.InsertCommand.Parameters.Add("Done", MySqlType.Bit, 0, "Done");
                        adapter.InsertCommand.Parameters.Add("Notes", MySqlType.Text, 0xffffff, "Notes");
                        adapter.InsertCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                        adapter.InsertCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                        adapter.InsertCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                        adapter.InsertCommand.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                        adapter.InsertCommand.GenerateInsertCommand("tbl_invoicenotes");
                        adapter.UpdateCommand = new MySqlCommand("", connection);
                        adapter.UpdateCommand.Parameters.Add("CallbackDate", MySqlType.Date, 0, "CallbackDate");
                        adapter.UpdateCommand.Parameters.Add("Done", MySqlType.Bit, 0, "Done");
                        adapter.UpdateCommand.Parameters.Add("Notes", MySqlType.Text, 0xffffff, "Notes");
                        adapter.UpdateCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                        adapter.UpdateCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                        adapter.UpdateCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                        adapter.UpdateCommand.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                        adapter.UpdateCommand.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
                        string[] whereParameters = new string[] { "CustomerID", "InvoiceID", "InvoiceDetailsID", "ID" };
                        adapter.UpdateCommand.GenerateUpdateCommand("tbl_invoicenotes", whereParameters);
                        adapter.DeleteCommand = new MySqlCommand("", connection);
                        adapter.DeleteCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                        adapter.DeleteCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                        adapter.DeleteCommand.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                        adapter.DeleteCommand.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
                        adapter.DeleteCommand.GenerateDeleteCommand("tbl_invoicenotes");
                        using (new DataAdapterEvents(adapter))
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }
                            try
                            {
                                adapter.InsertCommand.Connection = connection;
                                adapter.UpdateCommand.Connection = connection;
                                adapter.DeleteCommand.Connection = connection;
                                adapter.ContinueUpdateOnError = false;
                                adapter.Update(changes);
                                table.MergeKeys(changes);
                                table.AcceptChanges();
                            }
                            finally
                            {
                                bool flag;
                                if (flag)
                                {
                                    connection.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool ValuesAreEqual(int? X, int? Y) => 
            !((X != null) & (Y != null)) ? ((X != null) == (Y != null)) : (X.Value == Y.Value);

        private class DataAdapterEvents : IDisposable
        {
            private readonly MySqlCommand cmdSelectIdentity;
            private MySqlDataAdapter dataAdapter;

            public DataAdapterEvents(MySqlDataAdapter da)
            {
                this.dataAdapter = da;
                this.dataAdapter.RowUpdating += new MySqlRowUpdatingEventHandler(this.OnRowUpdating);
                this.dataAdapter.RowUpdated += new MySqlRowUpdatedEventHandler(this.OnRowUpdated);
                this.cmdSelectIdentity = new MySqlCommand();
                this.cmdSelectIdentity.CommandType = CommandType.Text;
                this.cmdSelectIdentity.CommandText = "SELECT LAST_INSERT_ID()";
            }

            public void Dispose()
            {
                this.cmdSelectIdentity.Dispose();
                this.dataAdapter.RowUpdating -= new MySqlRowUpdatingEventHandler(this.OnRowUpdating);
                this.dataAdapter.RowUpdated -= new MySqlRowUpdatedEventHandler(this.OnRowUpdated);
                this.dataAdapter = null;
            }

            public void OnRowUpdated(object sender, MySqlRowUpdatedEventArgs e)
            {
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && (e.RecordsAffected == 1)))
                {
                    this.cmdSelectIdentity.Connection = e.Command.Connection;
                    try
                    {
                        this.cmdSelectIdentity.Transaction = e.Command.Transaction;
                        try
                        {
                            if (e.Row.Table is ControlInvoiceNotes.TableInvoiceNotes)
                            {
                                ControlInvoiceNotes.TableInvoiceNotes table = (ControlInvoiceNotes.TableInvoiceNotes) e.Row.Table;
                                e.Row[table.Col_ID] = Conversions.ToInteger(this.cmdSelectIdentity.ExecuteScalar());
                            }
                        }
                        finally
                        {
                            this.cmdSelectIdentity.Transaction = null;
                        }
                    }
                    finally
                    {
                        this.cmdSelectIdentity.Connection = null;
                    }
                }
            }

            public void OnRowUpdating(object sender, MySqlRowUpdatingEventArgs e)
            {
            }
        }

        public class TableInvoiceNotes : ControlDetails.TableDetails
        {
            public DataColumn Col_ID;
            public DataColumn Col_CallbackDate;
            public DataColumn Col_Operator;
            public DataColumn Col_Notes;
            public DataColumn Col_Done;

            public TableInvoiceNotes() : this("tbl_invoicenotes")
            {
            }

            public TableInvoiceNotes(string TableName) : base(TableName)
            {
            }

            protected int? GetProperty(string Name) => 
                NullableConvert.ToInt32(base.ExtendedProperties[Name]);

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_ID = base.Columns["ID"];
                this.Col_CallbackDate = base.Columns["CallbackDate"];
                this.Col_Operator = base.Columns["Operator"];
                this.Col_Notes = base.Columns["Notes"];
                this.Col_Done = base.Columns["Done"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                base.Columns.Add("ID", typeof(int));
                base.Columns.Add("CallbackDate", typeof(DateTime));
                base.Columns.Add("Operator", typeof(string));
                base.Columns.Add("Notes", typeof(string));
                base.Columns.Add("Done", typeof(bool)).AllowDBNull = false;
            }

            public void MergeKeys(ControlInvoiceNotes.TableInvoiceNotes table)
            {
                int num = table.Rows.Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    DataRow row = table.Rows[i];
                    DataRow row2 = base.Rows.Find(row["RowID"]);
                    if (row2 != null)
                    {
                        row2[this.Col_ID] = row["ID"];
                    }
                }
            }

            protected void SetProperty(string Name, int? Value)
            {
                if (Value != null)
                {
                    base.ExtendedProperties[Name] = Value.Value;
                }
                else
                {
                    base.ExtendedProperties.Remove(Name);
                }
            }

            public int? InvoiceDetailsID
            {
                get => 
                    this.GetProperty("InvoiceDetailsID");
                set => 
                    this.SetProperty("InvoiceDetailsID", value);
            }

            public int? InvoiceID
            {
                get => 
                    this.GetProperty("InvoiceID");
                set => 
                    this.SetProperty("InvoiceID", value);
            }

            public int? CustomerID
            {
                get => 
                    this.GetProperty("CustomerID");
                set => 
                    this.SetProperty("CustomerID", value);
            }
        }
    }
}

