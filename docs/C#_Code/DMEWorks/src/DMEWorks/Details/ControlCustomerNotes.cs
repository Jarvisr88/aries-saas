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

    [DesignerGenerated]
    public class ControlCustomerNotes : ControlDetails
    {
        private IContainer components;

        public ControlCustomerNotes()
        {
            this.InitializeComponent();
        }

        public void ClearGrid()
        {
            base.CloseDialogs();
            TableCustomerNotes table = this.GetTable();
            table.Clear();
            table.AcceptChanges();
        }

        protected override FormDetails CreateDialog(object param) => 
            base.AddDialog(new FormCustomerNotes(this));

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableCustomerNotes();

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected static void GenerateDeleteCommand_CustomerNotes(MySqlCommand DeleteCommand, int CustomerID)
        {
            DeleteCommand.Parameters.Clear();
            DeleteCommand.Parameters.Add("CustomerID", MySqlType.Int, 4).Value = CustomerID;
            DeleteCommand.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            DeleteCommand.GenerateDeleteCommand("tbl_customer_notes");
        }

        protected static void GenerateInsertCommand_CustomerNotes(MySqlCommand InsertCommand, int CustomerID)
        {
            InsertCommand.Parameters.Clear();
            InsertCommand.Parameters.Add("Notes", MySqlType.Text, 0x10000, "Notes");
            InsertCommand.Parameters.Add("Active", MySqlType.Bit, 1, "Active");
            InsertCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            InsertCommand.Parameters.Add("CreatedBy", MySqlType.SmallInt).Value = Globals.CompanyUserID;
            InsertCommand.Parameters.Add("CreatedAt", MySqlType.DateTime).Value = DateTime.Now;
            InsertCommand.Parameters.Add("CustomerID", MySqlType.Int, 4).Value = CustomerID;
            InsertCommand.GenerateInsertCommand("tbl_customer_notes");
        }

        protected static void GenerateUpdateCommand_CustomerNotes(MySqlCommand UpdateCommand, int CustomerID)
        {
            UpdateCommand.Parameters.Clear();
            UpdateCommand.Parameters.Add("Notes", MySqlType.Text, 0x10000, "Notes");
            UpdateCommand.Parameters.Add("Active", MySqlType.Bit, 1, "Active");
            UpdateCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            UpdateCommand.Parameters.Add("CustomerID", MySqlType.Int, 4).Value = CustomerID;
            UpdateCommand.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            string[] whereParameters = new string[] { "CustomerID", "ID" };
            UpdateCommand.GenerateUpdateCommand("tbl_customer_notes", whereParameters);
        }

        protected TableCustomerNotes GetTable() => 
            (TableCustomerNotes) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            base.SuspendLayout();
            this.btnDelete.Enabled = false;
            this.btnDelete.Visible = false;
            base.Name = "ControlCustomerNotes2";
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("Active", "Active", 40);
            Appearance.AddTextColumn("CreatedAt", "Created At", 100, Appearance.DateTimeStyle());
            Appearance.AddTextColumn("CreatedBy", "Created By", 80);
            Appearance.AddTextColumn("Notes", "Notes", 220);
        }

        public void LoadGrid(MySqlConnection cnn, int CustomerID)
        {
            base.CloseDialogs();
            TableCustomerNotes table = this.GetTable();
            table.Clear();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  tbl_customer_notes.ID\r\n, tbl_customer_notes.CreatedAt\r\n, tbl_user.Login as CreatedBy\r\n, tbl_customer_notes.Notes\r\n, tbl_customer_notes.Active\r\nFROM tbl_customer_notes\r\n     LEFT JOIN tbl_user ON tbl_customer_notes.CreatedBy = tbl_user.ID\r\nWHERE tbl_customer_notes.CustomerID = :CustomerID\r\nORDER BY tbl_customer_notes.CreatedAt DESC", cnn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("CustomerID", CustomerID);
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.Fill(table);
            }
            table.AcceptChanges();
        }

        public void SaveGrid(MySqlConnection cnn, int CustomerID)
        {
            TableCustomerNotes table = this.GetTable();
            TableCustomerNotes changes = (TableCustomerNotes) table.GetChanges();
            if (changes != null)
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                {
                    adapter.InsertCommand = new MySqlCommand();
                    GenerateInsertCommand_CustomerNotes(adapter.InsertCommand, CustomerID);
                    adapter.UpdateCommand = new MySqlCommand();
                    GenerateUpdateCommand_CustomerNotes(adapter.UpdateCommand, CustomerID);
                    adapter.DeleteCommand = new MySqlCommand();
                    GenerateDeleteCommand_CustomerNotes(adapter.DeleteCommand, CustomerID);
                    using (new DataAdapterEvents(adapter))
                    {
                        if (cnn.State == ConnectionState.Closed)
                        {
                            cnn.Open();
                        }
                        try
                        {
                            adapter.InsertCommand.Connection = cnn;
                            adapter.UpdateCommand.Connection = cnn;
                            adapter.DeleteCommand.Connection = cnn;
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
                                cnn.Close();
                            }
                        }
                    }
                }
            }
        }

        private class DataAdapterEvents : MySqlDataAdapterEventsBase
        {
            public DataAdapterEvents(MySqlDataAdapter da) : base(da)
            {
            }

            protected override void ProcessRowUpdated(MySqlRowUpdatedEventArgs e)
            {
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && ((e.RecordsAffected == 1) && (e.Row.Table is ControlCustomerNotes.TableCustomerNotes))))
                {
                    ControlCustomerNotes.TableCustomerNotes table = (ControlCustomerNotes.TableCustomerNotes) e.Row.Table;
                    base.cmdSelectIdentity.Connection = e.Command.Connection;
                    try
                    {
                        base.cmdSelectIdentity.Transaction = e.Command.Transaction;
                        try
                        {
                            e.Row[table.Col_ID] = Conversions.ToInteger(base.cmdSelectIdentity.ExecuteScalar());
                        }
                        finally
                        {
                            base.cmdSelectIdentity.Transaction = null;
                        }
                    }
                    finally
                    {
                        base.cmdSelectIdentity.Connection = null;
                    }
                }
            }
        }

        public class TableCustomerNotes : ControlDetails.TableDetails
        {
            public DataColumn Col_ID;
            public DataColumn Col_CreatedAt;
            public DataColumn Col_CreatedBy;
            public DataColumn Col_Notes;
            public DataColumn Col_Active;

            public TableCustomerNotes() : this("tbl_notes")
            {
            }

            public TableCustomerNotes(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_ID = base.Columns["ID"];
                this.Col_CreatedAt = base.Columns["CreatedAt"];
                this.Col_CreatedBy = base.Columns["CreatedBy"];
                this.Col_Notes = base.Columns["Notes"];
                this.Col_Active = base.Columns["Active"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_CreatedAt = base.Columns.Add("CreatedAt", typeof(DateTime));
                this.Col_CreatedBy = base.Columns.Add("CreatedBy", typeof(string));
                this.Col_Notes = base.Columns.Add("Notes", typeof(string));
                this.Col_Active = base.Columns.Add("Active", typeof(bool));
            }

            public void MergeKeys(ControlCustomerNotes.TableCustomerNotes table)
            {
                int num2 = table.Rows.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    DataRow row = table.Rows[i];
                    DataRow row2 = base.Rows.Find(row["RowID"]);
                    if (row2 != null)
                    {
                        row2[this.Col_ID] = row["ID"];
                    }
                }
            }
        }
    }
}

