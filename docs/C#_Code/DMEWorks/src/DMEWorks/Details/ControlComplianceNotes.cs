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

    public class ControlComplianceNotes : ControlDetails
    {
        private IContainer components;

        public ControlComplianceNotes()
        {
            this.InitializeComponent();
            base.btnDelete.Visible = false;
        }

        public void ClearGrid()
        {
            base.CloseDialogs();
            TableComplianceNotes table = this.GetTable();
            table.Clear();
            table.AcceptChanges();
        }

        protected override FormDetails CreateDialog(object param) => 
            base.AddDialog(new FormComplianceNotes(this));

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableComplianceNotes();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected static void GenerateDeleteCommand_CustomerNotes(MySqlCommand DeleteCommand, int ComplianceID)
        {
            DeleteCommand.Parameters.Clear();
            DeleteCommand.Parameters.Add("ComplianceID", MySqlType.Int, 4).Value = ComplianceID;
            DeleteCommand.Parameters.Add("ID", MySqlType.Int, 4, "ID").SourceVersion = DataRowVersion.Original;
            DeleteCommand.GenerateDeleteCommand("tbl_compliance_notes");
        }

        protected static void GenerateInsertCommand_CustomerNotes(MySqlCommand InsertCommand, int ComplianceID)
        {
            InsertCommand.Parameters.Clear();
            InsertCommand.Parameters.Add("Notes", MySqlType.VarChar, 0x10000, "Notes");
            InsertCommand.Parameters.Add("Date", MySqlType.Date, 1, "Date");
            InsertCommand.Parameters.Add("Done", MySqlType.Bit, 1, "Done");
            InsertCommand.Parameters.Add("AssignedToUserID", MySqlType.SmallInt, 2, "AssignedToId");
            InsertCommand.Parameters.Add("CreatedByUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            InsertCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            InsertCommand.Parameters.Add("ComplianceID", MySqlType.Int, 4).Value = ComplianceID;
            InsertCommand.GenerateInsertCommand("tbl_compliance_notes");
        }

        protected static void GenerateUpdateCommand_CustomerNotes(MySqlCommand UpdateCommand, int ComplianceID)
        {
            UpdateCommand.Parameters.Clear();
            UpdateCommand.Parameters.Add("Done", MySqlType.Bit, 1, "Done");
            UpdateCommand.Parameters.Add("AssignedToUserID", MySqlType.SmallInt, 2, "AssignedToId");
            UpdateCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            UpdateCommand.Parameters.Add("ComplianceID", MySqlType.Int, 4).Value = ComplianceID;
            UpdateCommand.Parameters.Add("ID", MySqlType.Int, 4, "ID").SourceVersion = DataRowVersion.Original;
            string[] whereParameters = new string[] { "ComplianceID", "ID" };
            UpdateCommand.GenerateUpdateCommand("tbl_compliance_notes", whereParameters);
        }

        protected TableComplianceNotes GetTable() => 
            (TableComplianceNotes) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.Name = "ControlComplianceNotes";
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("Done", "Done", 40);
            Appearance.AddTextColumn("Date", "Date", 100);
            Appearance.AddTextColumn("AssignedToName", "Assigned To", 80);
            Appearance.AddTextColumn("Notes", "Notes", 200);
        }

        public void LoadGrid(MySqlConnection cnn, int ComplianceID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT
  tbl_compliance_notes.ID
, tbl_compliance_notes.Date
, tbl_compliance_notes.Done
, tbl_compliance_notes.Notes
, tbl_user.ID as AssignedToId
, tbl_user.Login as AssignedToName
FROM tbl_compliance_notes
     LEFT JOIN tbl_user ON tbl_compliance_notes.AssignedToUserID = tbl_user.ID
WHERE tbl_compliance_notes.ComplianceID = {ComplianceID}
ORDER BY tbl_compliance_notes.LastUpdateDatetime DESC", cnn))
            {
                TableComplianceNotes table = this.GetTable();
                base.CloseDialogs();
                table.Clear();
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.Fill(table);
                table.AcceptChanges();
            }
        }

        public void SaveGrid(MySqlConnection cnn, int ComplianceID)
        {
            TableComplianceNotes table = this.GetTable();
            TableComplianceNotes changes = (TableComplianceNotes) table.GetChanges();
            if (changes != null)
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                {
                    adapter.InsertCommand = new MySqlCommand();
                    GenerateInsertCommand_CustomerNotes(adapter.InsertCommand, ComplianceID);
                    adapter.UpdateCommand = new MySqlCommand();
                    GenerateUpdateCommand_CustomerNotes(adapter.UpdateCommand, ComplianceID);
                    adapter.DeleteCommand = new MySqlCommand();
                    GenerateDeleteCommand_CustomerNotes(adapter.DeleteCommand, ComplianceID);
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
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && ((e.RecordsAffected == 1) && (e.Row.Table is ControlComplianceNotes.TableComplianceNotes))))
                {
                    ControlComplianceNotes.TableComplianceNotes table = (ControlComplianceNotes.TableComplianceNotes) e.Row.Table;
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

        public class TableComplianceNotes : ControlDetails.TableDetails
        {
            public DataColumn Col_ID;
            public DataColumn Col_Date;
            public DataColumn Col_AssignedToName;
            public DataColumn Col_AssignedToId;
            public DataColumn Col_Notes;
            public DataColumn Col_Done;

            public TableComplianceNotes() : this("tbl_notes")
            {
            }

            public TableComplianceNotes(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_ID = base.Columns["ID"];
                this.Col_Date = base.Columns["Date"];
                this.Col_AssignedToId = base.Columns["AssignedToId"];
                this.Col_AssignedToName = base.Columns["AssignedToName"];
                this.Col_Notes = base.Columns["Notes"];
                this.Col_Done = base.Columns["Done"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                base.Columns.Add("ID", typeof(int));
                base.Columns.Add("Date", typeof(DateTime)).AllowDBNull = false;
                base.Columns.Add("AssignedToId", typeof(short));
                base.Columns.Add("AssignedToName", typeof(string));
                base.Columns.Add("Notes", typeof(string));
                base.Columns.Add("Done", typeof(bool)).AllowDBNull = false;
            }

            public void MergeKeys(ControlComplianceNotes.TableComplianceNotes table)
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

