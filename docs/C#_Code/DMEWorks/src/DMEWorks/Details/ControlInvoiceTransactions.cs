namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlInvoiceTransactions : UserControl
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private static readonly object EVENT_BALANCECHANGED = new object();
        private double FBalance;

        public event EventHandler BalanceChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_BALANCECHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_BALANCECHANGED, value);
            }
            raise
            {
                EventHandler handler = base.Events[EVENT_BALANCECHANGED] as EventHandler;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
        }

        public ControlInvoiceTransactions()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
        }

        public void ClearGrid()
        {
            TableInvoiceTransactions table = new TableInvoiceTransactions();
            int? nullable = null;
            table.CustomerID = nullable;
            nullable = null;
            table.InvoiceID = nullable;
            nullable = null;
            table.InvoiceDetailsID = nullable;
            this.Grid.GridSource = table.ToGridSource();
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void Form_Saved(object sender, EventArgs e)
        {
            TableInvoiceTransactions tableSource = this.Grid.GetTableSource<TableInvoiceTransactions>();
            if ((tableSource != null) && ((tableSource.CustomerID != null) && ((tableSource.InvoiceID != null) && (tableSource.InvoiceDetailsID != null))))
            {
                this.LoadGrid(tableSource.CustomerID.Value, tableSource.InvoiceID.Value, tableSource.InvoiceDetailsID.Value);
            }
        }

        private void Grid_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    TableInvoiceTransactions table = dataRow.Table as TableInvoiceTransactions;
                    if (table != null)
                    {
                        this.ShowTransaction(table.CustomerID.Value, table.InvoiceID.Value, table.InvoiceDetailsID.Value, NullableConvert.ToInt32(dataRow[table.Col_ID]).Value);
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, this.Text);
                ProjectData.ClearProjectError();
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ControlInvoiceTransactions));
            this.Grid = new FilteredGrid();
            this.ToolStrip1 = new ToolStrip();
            this.tsbAddPayment = new ToolStripButton();
            this.tsbAddWriteoff = new ToolStripButton();
            this.tsbAddTransaction = new ToolStripButton();
            this.tsbAddChangePayer = new ToolStripButton();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x19);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(640, 340);
            this.Grid.TabIndex = 1;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbAddPayment, this.tsbAddWriteoff, this.tsbAddChangePayer, this.tsbAddTransaction };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(640, 0x19);
            this.ToolStrip1.TabIndex = 2;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbAddPayment.Image = (Image) manager.GetObject("tsbAddPayment.Image");
            this.tsbAddPayment.Name = "tsbAddPayment";
            this.tsbAddPayment.Size = new Size(0x45, 0x16);
            this.tsbAddPayment.Text = "Payment";
            this.tsbAddWriteoff.Image = (Image) manager.GetObject("tsbAddWriteoff.Image");
            this.tsbAddWriteoff.Name = "tsbAddWriteoff";
            this.tsbAddWriteoff.Size = new Size(70, 0x16);
            this.tsbAddWriteoff.Text = "Write off";
            this.tsbAddTransaction.Image = My.Resources.Resources.ImageNew;
            this.tsbAddTransaction.Name = "tsbAddTransaction";
            this.tsbAddTransaction.Size = new Size(0x53, 0x16);
            this.tsbAddTransaction.Text = "Transaction";
            this.tsbAddChangePayer.Image = (Image) manager.GetObject("tsbAddChangePayer.Image");
            this.tsbAddChangePayer.Name = "tsbAddChangePayer";
            this.tsbAddChangePayer.Size = new Size(0x5f, 0x16);
            this.tsbAddChangePayer.Text = "Change Payer";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "ControlInvoiceTransactions";
            base.Size = new Size(640, 0x16d);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "#", 40);
            Appearance.AddTextColumn("TransactionType", "Type", 140);
            Appearance.AddTextColumn("InsuranceCompany", "Ins Company", 180);
            Appearance.AddTextColumn("TransactionDate", "Date", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("Amount", "Amount", 60, Appearance.PriceStyle());
        }

        public void LoadGrid(int CustomerID, int InvoiceID, int InvoiceDetailsID)
        {
            double? nullable;
            TableInvoiceTransactions dataTable = new TableInvoiceTransactions {
                CustomerID = new int?(CustomerID),
                InvoiceID = new int?(InvoiceID),
                InvoiceDetailsID = new int?(InvoiceDetailsID)
            };
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                {
                    adapter.SelectCommand.CommandText = "SELECT\r\n  tbl_invoice_transaction.ID\r\n, tbl_insurancecompany.ID as InsuranceCompanyID\r\n, CASE WHEN (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins1: ', tbl_insurancecompany.Name)\r\n       WHEN (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins2: ', tbl_insurancecompany.Name)\r\n       WHEN (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins3: ', tbl_insurancecompany.Name)\r\n       WHEN (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins4: ', tbl_insurancecompany.Name)\r\n       ELSE tbl_insurancecompany.Name END as InsuranceCompany\r\n, tbl_invoice_transactiontype.ID AS TransactionTypeID\r\n, tbl_invoice_transactiontype.Name AS TransactionType\r\n, tbl_invoice_transaction.TransactionDate\r\n, tbl_invoice_transaction.Amount\r\n, tbl_invoice_transaction.Quantity\r\n, tbl_invoice_transaction.Taxes\r\n, tbl_invoice_transaction.BatchNumber\r\n, tbl_invoice_transaction.Comments\r\n, tbl_invoice_transaction.Extra\r\n, tbl_invoice_transaction.Approved\r\nFROM view_invoicetransaction_statistics as stats\r\n     INNER JOIN tbl_invoice_transaction ON stats.CustomerID       = tbl_invoice_transaction.CustomerID\r\n                                       AND stats.InvoiceID        = tbl_invoice_transaction.InvoiceID\r\n                                       AND stats.InvoiceDetailsID = tbl_invoice_transaction.InvoiceDetailsID\r\n     LEFT JOIN tbl_invoice_transactiontype ON tbl_invoice_transaction.TransactionTypeID = tbl_invoice_transactiontype.ID\r\n     LEFT JOIN tbl_insurancecompany ON tbl_invoice_transaction.InsuranceCompanyID = tbl_insurancecompany.ID\r\nWHERE (stats.CustomerID       = :CustomerID      )\r\n  AND (stats.InvoiceID        = :InvoiceID       )\r\n  AND (stats.InvoiceDetailsID = :InvoiceDetailsID)\r\nORDER BY tbl_invoice_transaction.ID";
                    adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                    adapter.SelectCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                    adapter.SelectCommand.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                    adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(dataTable);
                }
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount as Balance\r\nFROM view_invoicetransaction_statistics as stats\r\nWHERE (stats.CustomerID       = :CustomerID      )\r\n  AND (stats.InvoiceID        = :InvoiceID       )\r\n  AND (stats.InvoiceDetailsID = :InvoiceDetailsID)";
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                    command.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                    command.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        nullable = !reader.Read() ? null : NullableConvert.ToDouble(reader["Balance"]);
                    }
                }
            }
            this.Balance = (nullable != null) ? nullable.Value : 0.0;
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        private void ShowNewPayment(int CustomerID, int InvoiceID, int InvoiceDetailsID)
        {
            FormParameters @params = new FormParameters {
                ["CustomerID"] = CustomerID,
                ["InvoiceID"] = InvoiceID,
                ["InvoiceDetailsID"] = InvoiceDetailsID
            };
            FormNewPayment payment1 = new FormNewPayment();
            payment1.MdiParent = ClassGlobalObjects.frmMain;
            payment1.SetParameters(@params);
            payment1.Saved += new EventHandler(this.Form_Saved);
            payment1.Show();
        }

        private void ShowNewTransaction(int CustomerID, int InvoiceID, int InvoiceDetailsID)
        {
            FormParameters @params = new FormParameters {
                ["CustomerID"] = CustomerID,
                ["InvoiceID"] = InvoiceID,
                ["InvoiceDetailsID"] = InvoiceDetailsID
            };
            FormNewInvoiceTransaction transaction1 = new FormNewInvoiceTransaction();
            transaction1.MdiParent = ClassGlobalObjects.frmMain;
            transaction1.SetParameters(@params);
            transaction1.Saved += new EventHandler(this.Form_Saved);
            transaction1.Show();
        }

        private void ShowTransaction(int CustomerID, int InvoiceID, int InvoiceDetailsID, int ID)
        {
            FormParameters @params = new FormParameters {
                ["ID"] = ID
            };
            FormInvoiceTransaction transaction1 = new FormInvoiceTransaction();
            transaction1.MdiParent = ClassGlobalObjects.frmMain;
            transaction1.SetParameters(@params);
            transaction1.Saved += new EventHandler(this.Form_Saved);
            transaction1.Show();
        }

        private void tsbAddChangePayer_Click(object sender, EventArgs e)
        {
            try
            {
                TableInvoiceTransactions tableSource = this.Grid.GetTableSource<TableInvoiceTransactions>();
                if (tableSource != null)
                {
                    FormParameters @params = new FormParameters {
                        ["CustomerID"] = tableSource.CustomerID.Value,
                        ["InvoiceID"] = tableSource.InvoiceID.Value,
                        ["InvoiceDetailsID"] = tableSource.InvoiceDetailsID.Value,
                        ["TransactionType"] = "Change Current Payee"
                    };
                    FormNewInvoiceTransaction transaction1 = new FormNewInvoiceTransaction();
                    transaction1.MdiParent = ClassGlobalObjects.frmMain;
                    transaction1.SetParameters(@params);
                    transaction1.Saved += new EventHandler(this.Form_Saved);
                    transaction1.Show();
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, this.Text);
                ProjectData.ClearProjectError();
            }
        }

        private void tsbAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                TableInvoiceTransactions tableSource = this.Grid.GetTableSource<TableInvoiceTransactions>();
                if (tableSource != null)
                {
                    this.ShowNewPayment(tableSource.CustomerID.Value, tableSource.InvoiceID.Value, tableSource.InvoiceDetailsID.Value);
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, this.Text);
                ProjectData.ClearProjectError();
            }
        }

        private void tsbAddTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                TableInvoiceTransactions tableSource = this.Grid.GetTableSource<TableInvoiceTransactions>();
                if (tableSource != null)
                {
                    FormParameters @params = new FormParameters {
                        ["CustomerID"] = tableSource.CustomerID.Value,
                        ["InvoiceID"] = tableSource.InvoiceID.Value,
                        ["InvoiceDetailsID"] = tableSource.InvoiceDetailsID.Value
                    };
                    FormNewInvoiceTransaction transaction1 = new FormNewInvoiceTransaction();
                    transaction1.MdiParent = ClassGlobalObjects.frmMain;
                    transaction1.SetParameters(@params);
                    transaction1.Saved += new EventHandler(this.Form_Saved);
                    transaction1.Show();
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, this.Text);
                ProjectData.ClearProjectError();
            }
        }

        private void tsbAddWriteoff_Click(object sender, EventArgs e)
        {
            try
            {
                TableInvoiceTransactions tableSource = this.Grid.GetTableSource<TableInvoiceTransactions>();
                if (tableSource != null)
                {
                    FormParameters @params = new FormParameters {
                        ["CustomerID"] = tableSource.CustomerID.Value,
                        ["InvoiceID"] = tableSource.InvoiceID.Value,
                        ["InvoiceDetailsID"] = tableSource.InvoiceDetailsID.Value,
                        ["TransactionType"] = "Writeoff"
                    };
                    FormNewInvoiceTransaction transaction1 = new FormNewInvoiceTransaction();
                    transaction1.MdiParent = ClassGlobalObjects.frmMain;
                    transaction1.SetParameters(@params);
                    transaction1.Saved += new EventHandler(this.Form_Saved);
                    transaction1.Show();
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, this.Text);
                ProjectData.ClearProjectError();
            }
        }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAddPayment")]
        private ToolStripButton tsbAddPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAddWriteoff")]
        private ToolStripButton tsbAddWriteoff { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAddTransaction")]
        private ToolStripButton tsbAddTransaction { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAddChangePayer")]
        private ToolStripButton tsbAddChangePayer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public double Balance
        {
            get => 
                this.FBalance;
            private set
            {
                if (0.01 <= Math.Abs((double) (this.FBalance - value)))
                {
                    this.FBalance = value;
                    this.raise_BalanceChanged(this, EventArgs.Empty);
                }
            }
        }

        public class TableInvoiceTransactions : TableBase
        {
            public DataColumn Col_ID;
            public DataColumn Col_InsuranceCompanyID;
            public DataColumn Col_InsuranceCompany;
            public DataColumn Col_InsuranceCompanyBasis;
            public DataColumn Col_CustomerInsuranceID;
            public DataColumn Col_TransactionTypeID;
            public DataColumn Col_TransactionType;
            public DataColumn Col_TransactionDate;
            public DataColumn Col_Amount;
            public DataColumn Col_Quantity;
            public DataColumn Col_Taxes;
            public DataColumn Col_Comments;
            public DataColumn Col_Extra;

            public TableInvoiceTransactions() : this("tbl_invoice_transaction")
            {
            }

            public TableInvoiceTransactions(string TableName) : base(TableName)
            {
            }

            protected int? GetProperty(string Name) => 
                NullableConvert.ToInt32(base.ExtendedProperties[Name]);

            protected override void Initialize()
            {
                this.Col_ID = base.Columns["ID"];
                this.Col_InsuranceCompanyID = base.Columns["InsuranceCompanyID"];
                this.Col_InsuranceCompany = base.Columns["InsuranceCompany"];
                this.Col_InsuranceCompanyBasis = base.Columns["InsuranceCompanyBasis"];
                this.Col_CustomerInsuranceID = base.Columns["CustomerInsuranceID"];
                this.Col_TransactionTypeID = base.Columns["TransactionTypeID"];
                this.Col_TransactionType = base.Columns["TransactionType"];
                this.Col_TransactionDate = base.Columns["TransactionDate"];
                this.Col_Amount = base.Columns["Amount"];
                this.Col_Quantity = base.Columns["Quantity"];
                this.Col_Taxes = base.Columns["Taxes"];
                this.Col_Comments = base.Columns["Comments"];
                this.Col_Extra = base.Columns["Extra"];
            }

            protected override void InitializeClass()
            {
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_InsuranceCompanyID = base.Columns.Add("InsuranceCompanyID", typeof(int));
                this.Col_InsuranceCompany = base.Columns.Add("InsuranceCompany", typeof(string));
                this.Col_InsuranceCompanyBasis = base.Columns.Add("InsuranceCompanyBasis", typeof(string));
                this.Col_CustomerInsuranceID = base.Columns.Add("CustomerInsuranceID", typeof(int));
                this.Col_TransactionTypeID = base.Columns.Add("TransactionTypeID", typeof(int));
                this.Col_TransactionType = base.Columns.Add("TransactionType", typeof(string));
                this.Col_TransactionDate = base.Columns.Add("TransactionDate", typeof(DateTime));
                this.Col_Amount = base.Columns.Add("Amount", typeof(double));
                this.Col_Quantity = base.Columns.Add("Quantity", typeof(double));
                this.Col_Taxes = base.Columns.Add("Taxes", typeof(double));
                this.Col_Comments = base.Columns.Add("Comments", typeof(string));
                this.Col_Extra = base.Columns.Add("Extra", typeof(string));
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

