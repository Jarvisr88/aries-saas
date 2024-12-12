namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormProcessOrders : DmeForm
    {
        private IContainer components;

        public FormProcessOrders()
        {
            this.InitializeComponent();
        }

        private void btnProcess3_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnProcess3.Enabled = false;
                this.lblIterationCount.Enabled = false;
                this.nudIterationCount.Enabled = false;
                this.chbInvoices.Enabled = false;
                this.cmbBillingType.Enabled = false;
                this.chbReoccuringOrders.Enabled = false;
                this.chbReoccuringPurchaseOrders.Enabled = false;
                this.Process3(Math.Min(10, Convert.ToInt32(this.nudIterationCount.Value)));
                string[] tableNames = new string[] { "tbl_invoicedetails", "tbl_invoice", "tbl_orderdetails", "tbl_order" };
                ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
                base.Close();
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, this.btnProcess3.Text);
                ProjectData.ClearProjectError();
            }
        }

        private void chbInvoices_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbBillingType.Enabled = this.chbInvoices.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbBillingType, "tbl_billingtype", Filter.Instance);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.ProgressBar1 = new ProgressBar();
            this.lblIterationCount = new Label();
            this.nudIterationCount = new NumericUpDown();
            this.btnProcess3 = new Button();
            this.chbInvoices = new CheckBox();
            this.chbReoccuringOrders = new CheckBox();
            this.chbReoccuringPurchaseOrders = new CheckBox();
            this.txtLog = new TextBox();
            this.cmbBillingType = new ComboBox();
            this.nudIterationCount.BeginInit();
            base.SuspendLayout();
            this.ProgressBar1.Location = new Point(8, 0x68);
            this.ProgressBar1.Maximum = 1;
            this.ProgressBar1.Minimum = 1;
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new Size(0x110, 0x17);
            this.ProgressBar1.Step = 1;
            this.ProgressBar1.TabIndex = 5;
            this.ProgressBar1.Value = 1;
            this.lblIterationCount.Location = new Point(8, 8);
            this.lblIterationCount.Name = "lblIterationCount";
            this.lblIterationCount.Size = new Size(0x40, 20);
            this.lblIterationCount.TabIndex = 0;
            this.lblIterationCount.Text = "Iterations :";
            this.lblIterationCount.TextAlign = ContentAlignment.MiddleRight;
            this.nudIterationCount.BorderStyle = BorderStyle.FixedSingle;
            this.nudIterationCount.Location = new Point(80, 8);
            int[] bits = new int[4];
            bits[0] = 10;
            this.nudIterationCount.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.nudIterationCount.Minimum = new decimal(numArray2);
            this.nudIterationCount.Name = "nudIterationCount";
            this.nudIterationCount.Size = new Size(0x58, 20);
            this.nudIterationCount.TabIndex = 1;
            int[] numArray3 = new int[4];
            numArray3[0] = 1;
            this.nudIterationCount.Value = new decimal(numArray3);
            this.btnProcess3.Location = new Point(280, 0x68);
            this.btnProcess3.Name = "btnProcess3";
            this.btnProcess3.Size = new Size(0x38, 0x17);
            this.btnProcess3.TabIndex = 6;
            this.btnProcess3.Text = "Process";
            this.chbInvoices.Checked = true;
            this.chbInvoices.CheckState = CheckState.Checked;
            this.chbInvoices.FlatStyle = FlatStyle.Flat;
            this.chbInvoices.Location = new Point(8, 0x20);
            this.chbInvoices.Name = "chbInvoices";
            this.chbInvoices.Size = new Size(0x70, 20);
            this.chbInvoices.TabIndex = 2;
            this.chbInvoices.Text = "Invoices";
            this.chbInvoices.UseVisualStyleBackColor = true;
            this.chbReoccuringOrders.Checked = true;
            this.chbReoccuringOrders.CheckState = CheckState.Checked;
            this.chbReoccuringOrders.FlatStyle = FlatStyle.Flat;
            this.chbReoccuringOrders.Location = new Point(8, 0x38);
            this.chbReoccuringOrders.Name = "chbReoccuringOrders";
            this.chbReoccuringOrders.Size = new Size(0x70, 20);
            this.chbReoccuringOrders.TabIndex = 3;
            this.chbReoccuringOrders.Text = "Reoccuring Sales";
            this.chbReoccuringOrders.UseVisualStyleBackColor = true;
            this.chbReoccuringPurchaseOrders.Checked = true;
            this.chbReoccuringPurchaseOrders.CheckState = CheckState.Checked;
            this.chbReoccuringPurchaseOrders.FlatStyle = FlatStyle.Flat;
            this.chbReoccuringPurchaseOrders.Location = new Point(8, 80);
            this.chbReoccuringPurchaseOrders.Name = "chbReoccuringPurchaseOrders";
            this.chbReoccuringPurchaseOrders.Size = new Size(0x70, 20);
            this.chbReoccuringPurchaseOrders.TabIndex = 4;
            this.chbReoccuringPurchaseOrders.Text = "Reoccuring PO";
            this.chbReoccuringPurchaseOrders.UseVisualStyleBackColor = true;
            this.txtLog.Location = new Point(8, 0x88);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Size = new Size(0x148, 0x98);
            this.txtLog.TabIndex = 7;
            this.txtLog.WordWrap = false;
            this.cmbBillingType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbBillingType.FormattingEnabled = true;
            this.cmbBillingType.Location = new Point(120, 0x20);
            this.cmbBillingType.Name = "cmbBillingType";
            this.cmbBillingType.Size = new Size(0xd8, 0x15);
            this.cmbBillingType.TabIndex = 8;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x158, 0x12b);
            base.Controls.Add(this.cmbBillingType);
            base.Controls.Add(this.txtLog);
            base.Controls.Add(this.chbReoccuringPurchaseOrders);
            base.Controls.Add(this.chbReoccuringOrders);
            base.Controls.Add(this.chbInvoices);
            base.Controls.Add(this.btnProcess3);
            base.Controls.Add(this.nudIterationCount);
            base.Controls.Add(this.lblIterationCount);
            base.Controls.Add(this.ProgressBar1);
            base.Name = "FormProcess3";
            this.Text = "Process Orders";
            this.nudIterationCount.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Iteration_Invoice(int? BillingTypeID)
        {
            DateTime today = DateTime.Today;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                ProgressBar bar;
                connection.Open();
                DateTime utcNow = DateTime.UtcNow;
                this.Log("Updating MIR");
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "CALL mir_update();";
                    command.ExecuteNonQuery();
                }
                object[] args = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Updated MIR. Duration {0:0.00} sec", args);
                utcNow = DateTime.UtcNow;
                this.Log("Loading list of orders");
                DataTable dataTable = new DataTable("tbl_order");
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                {
                    string str = (BillingTypeID != null) ? ("BillingTypeID = " + Conversions.ToString(BillingTypeID.Value)) : "1 = 1";
                    adapter.SelectCommand.CommandText = "SELECT DISTINCT\r\n  OrderID,\r\n  BillingMonth,\r\n  BillingFlags\r\nFROM view_billinglist\r\nWHERE (" + str + ")\r\nORDER BY OrderID, BillingMonth DESC";
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(dataTable);
                }
                object[] objArray2 = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Loaded list of orders. Duration {0:0.00} sec", objArray2);
                utcNow = DateTime.UtcNow;
                this.Log("Processing orders");
                this.ProgressBar1.Minimum = 0;
                this.ProgressBar1.Maximum = dataTable.Rows.Count;
                this.ProgressBar1.Value = 0;
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (MySqlCommand command2 = new MySqlCommand("", connection, transaction))
                    {
                        IEnumerator enumerator;
                        command2.CommandText = "CALL `order_process_2`(:P_OrderID, :P_BillingMonth, :P_BillingFlags, :P_InvoiceDate)";
                        command2.Parameters.Add("P_OrderID", MySqlType.Int);
                        command2.Parameters.Add("P_BillingMonth", MySqlType.Int);
                        command2.Parameters.Add("P_BillingFlags", MySqlType.Int);
                        command2.Parameters.Add("P_InvoiceDate", MySqlType.Date).Value = today;
                        command2.Prepare();
                        try
                        {
                            enumerator = dataTable.Rows.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                DataRow current = (DataRow) enumerator.Current;
                                command2.Parameters["P_OrderID"].Value = current["OrderID"];
                                command2.Parameters["P_BillingMonth"].Value = current["BillingMonth"];
                                command2.Parameters["P_BillingFlags"].Value = current["BillingFlags"];
                                command2.ExecuteNonQuery();
                                (bar = this.ProgressBar1).Value = bar.Value + 1;
                                Application.DoEvents();
                            }
                        }
                        finally
                        {
                            if (enumerator is IDisposable)
                            {
                                (enumerator as IDisposable).Dispose();
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception innerException = ex;
                    transaction.Rollback();
                    throw new Exception("Error in transaction", innerException);
                }
                object[] objArray3 = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Processed orders. Duration {0:0.00} sec", objArray3);
                utcNow = DateTime.UtcNow;
                this.Log("Processing inventory transactions");
                this.ProgressBar1.Minimum = 0;
                this.ProgressBar1.Maximum = dataTable.Rows.Count;
                this.ProgressBar1.Value = 0;
                using (MySqlCommand command3 = new MySqlCommand("", connection))
                {
                    IEnumerator enumerator;
                    command3.CommandText = "CALL inventory_transaction_order_refresh(:P_OrderID)";
                    command3.Parameters.Add("P_OrderID", MySqlType.Int);
                    try
                    {
                        enumerator = dataTable.Rows.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            DataRow current = (DataRow) enumerator.Current;
                            command3.Parameters["P_OrderID"].Value = current["OrderID"];
                            command3.ExecuteNonQuery();
                            (bar = this.ProgressBar1).Value = bar.Value + 1;
                            Application.DoEvents();
                        }
                    }
                    finally
                    {
                        if (enumerator is IDisposable)
                        {
                            (enumerator as IDisposable).Dispose();
                        }
                    }
                }
                object[] objArray4 = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Processed inventory transactions. Duration {0:0.00} sec", objArray4);
                utcNow = DateTime.UtcNow;
                this.Log("Processing inventory");
                this.ProgressBar1.Minimum = 0;
                this.ProgressBar1.Maximum = dataTable.Rows.Count;
                this.ProgressBar1.Value = 0;
                using (MySqlCommand command4 = new MySqlCommand("", connection))
                {
                    IEnumerator enumerator;
                    command4.CommandText = "CALL inventory_order_refresh(:P_OrderID)";
                    command4.Parameters.Add("P_OrderID", MySqlType.Int);
                    try
                    {
                        enumerator = dataTable.Rows.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            DataRow current = (DataRow) enumerator.Current;
                            command4.Parameters["P_OrderID"].Value = current["OrderID"];
                            command4.ExecuteNonQuery();
                            (bar = this.ProgressBar1).Value = bar.Value + 1;
                            Application.DoEvents();
                        }
                    }
                    finally
                    {
                        if (enumerator is IDisposable)
                        {
                            (enumerator as IDisposable).Dispose();
                        }
                    }
                }
                object[] objArray5 = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Processed inventory. Duration {0:0.00} sec", objArray5);
                utcNow = DateTime.UtcNow;
                this.Log("Processing serials");
                this.ProgressBar1.Minimum = 0;
                this.ProgressBar1.Maximum = dataTable.Rows.Count;
                this.ProgressBar1.Value = 0;
                using (MySqlCommand command5 = new MySqlCommand("", connection))
                {
                    IEnumerator enumerator;
                    command5.CommandText = "CALL serial_order_refresh(:P_OrderID)";
                    command5.Parameters.Add("P_OrderID", MySqlType.Int);
                    try
                    {
                        enumerator = dataTable.Rows.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            DataRow current = (DataRow) enumerator.Current;
                            command5.Parameters["P_OrderID"].Value = current["OrderID"];
                            command5.ExecuteNonQuery();
                            (bar = this.ProgressBar1).Value = bar.Value + 1;
                            Application.DoEvents();
                        }
                    }
                    finally
                    {
                        if (enumerator is IDisposable)
                        {
                            (enumerator as IDisposable).Dispose();
                        }
                    }
                }
                object[] objArray6 = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Processed serials. Duration {0:0.00} sec", objArray6);
            }
        }

        private void Iteration_ReoccuringOrders()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                DateTime utcNow = DateTime.UtcNow;
                this.Log("Loading orders");
                DataTable dataTable = new DataTable("tbl_order");
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                {
                    adapter.SelectCommand.CommandText = "SELECT DISTINCT\r\n   OrderID\r\n  ,BilledWhen\r\n  ,BillItemOn\r\nFROM view_reoccuringlist\r\nORDER BY OrderID, BilledWhen, BillItemOn";
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(dataTable);
                }
                object[] args = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Loaded Orders. Duration {0:0.00} sec", args);
                if (dataTable.Rows.Count > 0)
                {
                    utcNow = DateTime.UtcNow;
                    this.Log("Processing orders");
                    this.ProgressBar1.Minimum = 0;
                    this.ProgressBar1.Maximum = dataTable.Rows.Count;
                    this.ProgressBar1.Value = 0;
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        IEnumerator enumerator;
                        MySqlCommand command = new MySqlCommand("", connection, transaction) {
                            CommandText = "CALL `process_reoccuring_order`(:P_OrderID, :P_BilledWhen, :P_BillItemOn)",
                            Parameters = { 
                                { 
                                    "P_OrderID",
                                    MySqlType.Int
                                },
                                { 
                                    "P_BilledWhen",
                                    MySqlType.VarChar,
                                    50
                                },
                                { 
                                    "P_BillItemOn",
                                    MySqlType.VarChar,
                                    50
                                }
                            }
                        };
                        try
                        {
                            enumerator = dataTable.Rows.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                ProgressBar bar;
                                DataRow current = (DataRow) enumerator.Current;
                                command.Parameters["P_OrderID"].Value = current["OrderID"];
                                command.Parameters["P_BilledWhen"].Value = current["BilledWhen"];
                                command.Parameters["P_BillItemOn"].Value = current["BillItemOn"];
                                command.ExecuteNonQuery();
                                (bar = this.ProgressBar1).Value = bar.Value + 1;
                                Application.DoEvents();
                            }
                        }
                        finally
                        {
                            if (enumerator is IDisposable)
                            {
                                (enumerator as IDisposable).Dispose();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception innerException = ex;
                        transaction.Rollback();
                        throw new Exception("Error in transaction", innerException);
                    }
                    object[] objArray2 = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                    this.Log("Processed orders. Duration {0:0.00} sec", objArray2);
                }
            }
        }

        private void Iteration_ReoccuringPurchaseOrders()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                DateTime utcNow = DateTime.UtcNow;
                this.Log("Loading purchase orders");
                DataTable dataTable = new DataTable("tbl_purchaseorder");
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                {
                    adapter.SelectCommand.CommandText = "SELECT ID\r\nFROM tbl_purchaseorder\r\nWHERE (Reoccuring = 1)\r\n  AND (DATE_ADD(OrderDate, INTERVAL 1 MONTH) <= NOW())\r\nORDER BY ID";
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(dataTable);
                }
                object[] args = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                this.Log("Loaded purchase orders. Duration {0:0.00} sec", args);
                if (dataTable.Rows.Count > 0)
                {
                    utcNow = DateTime.UtcNow;
                    this.Log("Processing purchase orders");
                    this.ProgressBar1.Minimum = 0;
                    this.ProgressBar1.Maximum = dataTable.Rows.Count;
                    this.ProgressBar1.Value = 0;
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        IEnumerator enumerator;
                        MySqlCommand command = new MySqlCommand("", connection, transaction) {
                            CommandText = "CALL `process_reoccuring_purchaseorder`(:P_PurchaseOrderID)",
                            Parameters = { { 
                                "P_PurchaseOrderID",
                                MySqlType.Int
                            } }
                        };
                        try
                        {
                            enumerator = dataTable.Rows.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                ProgressBar bar;
                                DataRow current = (DataRow) enumerator.Current;
                                command.Parameters["P_PurchaseOrderID"].Value = current["ID"];
                                command.ExecuteNonQuery();
                                (bar = this.ProgressBar1).Value = bar.Value + 1;
                                Application.DoEvents();
                            }
                        }
                        finally
                        {
                            if (enumerator is IDisposable)
                            {
                                (enumerator as IDisposable).Dispose();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception innerException = ex;
                        transaction.Rollback();
                        throw new Exception("Error in transaction", innerException);
                    }
                    object[] objArray2 = new object[] { (DateTime.UtcNow - utcNow).TotalSeconds };
                    this.Log("Processed purchase orders. Duration {0:0.00} sec", objArray2);
                }
            }
        }

        private void Log(string message)
        {
            Trace.WriteLine(message);
            this.txtLog.AppendText(message + Environment.NewLine);
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
        }

        private void Log(string format, params object[] args)
        {
            this.Log(string.Format(format, args));
        }

        private void Process3(int IterationCount)
        {
            try
            {
                this.txtLog.Clear();
                if (this.chbInvoices.Checked)
                {
                    int? billingTypeID = NullableConvert.ToInt32(this.cmbBillingType.SelectedValue);
                    int num = IterationCount;
                    for (int i = 1; i <= num; i++)
                    {
                        this.Log("Generate invoices. Iteration " + Conversions.ToString(i) + " of " + Conversions.ToString(IterationCount));
                        this.Iteration_Invoice(billingTypeID);
                    }
                }
                if (this.chbReoccuringOrders.Checked)
                {
                    int num3 = IterationCount;
                    for (int i = 1; i <= num3; i++)
                    {
                        this.Log("Process reoccuring sales. Iteration " + Conversions.ToString(i) + " of " + Conversions.ToString(IterationCount));
                        this.Iteration_ReoccuringOrders();
                    }
                }
                if (this.chbReoccuringPurchaseOrders.Checked)
                {
                    int num5 = IterationCount;
                    for (int i = 1; i <= num5; i++)
                    {
                        this.Log("Process reoccuring sales. Iteration " + Conversions.ToString(i) + " of " + Conversions.ToString(IterationCount));
                        this.Iteration_ReoccuringPurchaseOrders();
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
        }

        [field: AccessedThroughProperty("ProgressBar1")]
        private ProgressBar ProgressBar1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblIterationCount")]
        private Label lblIterationCount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nudIterationCount")]
        private NumericUpDown nudIterationCount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnProcess3")]
        private Button btnProcess3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbInvoices")]
        private CheckBox chbInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbReoccuringOrders")]
        private CheckBox chbReoccuringOrders { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLog")]
        private TextBox txtLog { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbReoccuringPurchaseOrders")]
        private CheckBox chbReoccuringPurchaseOrders { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBillingType")]
        internal virtual ComboBox cmbBillingType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        private class Filter : DMEWorks.Data.IFilter
        {
            public static readonly FormProcessOrders.Filter Instance = new FormProcessOrders.Filter();

            public string GetKey() => 
                base.GetType().Name;

            public DataTable Process(DataTable Source)
            {
                DataTable table = Source.Clone();
                foreach (DataRow row in Source.Select())
                {
                    table.ImportRow(row);
                }
                DataRow[] rowArray2 = table.Select("ID IS NULL");
                for (int i = 0; i < rowArray2.Length; i++)
                {
                    rowArray2[i]["Name"] = "All";
                }
                return table;
            }
        }
    }
}

