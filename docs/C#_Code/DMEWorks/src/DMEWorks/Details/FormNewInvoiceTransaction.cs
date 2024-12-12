namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormNewInvoiceTransaction : DmeForm, IParameters
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private static readonly object EVENT_SAVED = new object();

        public event EventHandler Saved
        {
            add
            {
                base.Events.AddHandler(EVENT_SAVED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_SAVED, value);
            }
            raise
            {
                EventHandler handler = base.Events[EVENT_SAVED] as EventHandler;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
        }

        public FormNewInvoiceTransaction()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveData();
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
                return;
            }
            base.Close();
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtComments = new TextBox();
            this.lblComments = new Label();
            this.lblTaxes = new Label();
            this.nmbTaxes = new NumericBox();
            this.lblQuantity = new Label();
            this.nmbQuantity = new NumericBox();
            this.lblTransactionType = new Label();
            this.cmbTransactionType = new ComboBox();
            this.lblAmount = new Label();
            this.nmbAmount = new NumericBox();
            this.lbTransactionDate = new Label();
            this.dtbTransactionDate = new UltraDateTimeEditor();
            this.lblInsuranceCompany = new Label();
            this.cmbInsuranceCompany = new ComboBox();
            this.lblBatchNumber = new Label();
            this.TextBox1 = new TextBox();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.TabControl1 = new TabControl();
            this.tpGeneral = new TabPage();
            this.ErrorProvider1 = new ErrorProvider(this.components);
            this.TabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            ((ISupportInitialize) this.ErrorProvider1).BeginInit();
            base.SuspendLayout();
            this.txtComments.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtComments.Location = new Point(0x60, 0xb0);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new Size(0xf6, 0x70);
            this.txtComments.TabIndex = 15;
            this.lblComments.BackColor = Color.Transparent;
            this.lblComments.BorderStyle = BorderStyle.FixedSingle;
            this.lblComments.Location = new Point(8, 0xb0);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new Size(80, 0x15);
            this.lblComments.TabIndex = 14;
            this.lblComments.Text = "Comments";
            this.lblComments.TextAlign = ContentAlignment.MiddleRight;
            this.lblTaxes.BorderStyle = BorderStyle.FixedSingle;
            this.lblTaxes.Location = new Point(8, 0x80);
            this.lblTaxes.Name = "lblTaxes";
            this.lblTaxes.Size = new Size(80, 0x15);
            this.lblTaxes.TabIndex = 10;
            this.lblTaxes.Text = "Taxes";
            this.lblTaxes.TextAlign = ContentAlignment.MiddleRight;
            this.nmbTaxes.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.nmbTaxes.BorderStyle = BorderStyle.Fixed3D;
            this.nmbTaxes.Location = new Point(0x60, 0x80);
            this.nmbTaxes.Name = "nmbTaxes";
            this.nmbTaxes.Size = new Size(0xf6, 0x15);
            this.nmbTaxes.TabIndex = 11;
            this.nmbTaxes.TextAlign = HorizontalAlignment.Left;
            this.lblQuantity.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuantity.Location = new Point(8, 0x68);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new Size(80, 0x15);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.TextAlign = ContentAlignment.MiddleRight;
            this.nmbQuantity.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.nmbQuantity.BorderStyle = BorderStyle.Fixed3D;
            this.nmbQuantity.Location = new Point(0x60, 0x68);
            this.nmbQuantity.Name = "nmbQuantity";
            this.nmbQuantity.Size = new Size(0xf6, 0x15);
            this.nmbQuantity.TabIndex = 9;
            this.nmbQuantity.TextAlign = HorizontalAlignment.Left;
            this.lblTransactionType.BorderStyle = BorderStyle.FixedSingle;
            this.lblTransactionType.Location = new Point(8, 0x20);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Size = new Size(80, 0x15);
            this.lblTransactionType.TabIndex = 2;
            this.lblTransactionType.Text = "Tran Type";
            this.lblTransactionType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbTransactionType.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbTransactionType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTransactionType.Location = new Point(0x60, 0x20);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new Size(0xf6, 0x15);
            this.cmbTransactionType.TabIndex = 3;
            this.lblAmount.BorderStyle = BorderStyle.FixedSingle;
            this.lblAmount.Location = new Point(8, 80);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new Size(80, 0x15);
            this.lblAmount.TabIndex = 6;
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextAlign = ContentAlignment.MiddleRight;
            this.nmbAmount.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.nmbAmount.BorderStyle = BorderStyle.Fixed3D;
            this.nmbAmount.Location = new Point(0x60, 80);
            this.nmbAmount.Name = "nmbAmount";
            this.nmbAmount.Size = new Size(0xf6, 0x15);
            this.nmbAmount.TabIndex = 7;
            this.nmbAmount.TextAlign = HorizontalAlignment.Left;
            this.lbTransactionDate.BorderStyle = BorderStyle.FixedSingle;
            this.lbTransactionDate.Location = new Point(8, 0x38);
            this.lbTransactionDate.Name = "lbTransactionDate";
            this.lbTransactionDate.Size = new Size(80, 0x15);
            this.lbTransactionDate.TabIndex = 4;
            this.lbTransactionDate.Text = "Tran Date";
            this.lbTransactionDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbTransactionDate.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.dtbTransactionDate.Location = new Point(0x60, 0x38);
            this.dtbTransactionDate.Name = "dtbTransactionDate";
            this.dtbTransactionDate.Size = new Size(0xf6, 0x15);
            this.dtbTransactionDate.TabIndex = 5;
            this.lblInsuranceCompany.BorderStyle = BorderStyle.FixedSingle;
            this.lblInsuranceCompany.Location = new Point(8, 8);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new Size(80, 0x15);
            this.lblInsuranceCompany.TabIndex = 0;
            this.lblInsuranceCompany.Text = "Ins. Company";
            this.lblInsuranceCompany.TextAlign = ContentAlignment.MiddleRight;
            this.cmbInsuranceCompany.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbInsuranceCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbInsuranceCompany.Location = new Point(0x60, 8);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new Size(0xf6, 0x15);
            this.cmbInsuranceCompany.TabIndex = 1;
            this.lblBatchNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblBatchNumber.Location = new Point(8, 0x98);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new Size(80, 0x15);
            this.lblBatchNumber.TabIndex = 12;
            this.lblBatchNumber.Text = "Batch#";
            this.lblBatchNumber.TextAlign = ContentAlignment.MiddleRight;
            this.TextBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.TextBox1.Location = new Point(0x60, 0x98);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new Size(0xf6, 20);
            this.TextBox1.TabIndex = 13;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x120, 0x150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnOK.Location = new Point(0xd0, 0x150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.TabControl1.Controls.Add(this.tpGeneral);
            this.TabControl1.Location = new Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x166, 320);
            this.TabControl1.TabIndex = 0;
            this.tpGeneral.Controls.Add(this.cmbInsuranceCompany);
            this.tpGeneral.Controls.Add(this.lblInsuranceCompany);
            this.tpGeneral.Controls.Add(this.TextBox1);
            this.tpGeneral.Controls.Add(this.dtbTransactionDate);
            this.tpGeneral.Controls.Add(this.lblBatchNumber);
            this.tpGeneral.Controls.Add(this.lbTransactionDate);
            this.tpGeneral.Controls.Add(this.nmbAmount);
            this.tpGeneral.Controls.Add(this.lblAmount);
            this.tpGeneral.Controls.Add(this.txtComments);
            this.tpGeneral.Controls.Add(this.lblComments);
            this.tpGeneral.Controls.Add(this.cmbTransactionType);
            this.tpGeneral.Controls.Add(this.lblTaxes);
            this.tpGeneral.Controls.Add(this.lblTransactionType);
            this.tpGeneral.Controls.Add(this.nmbTaxes);
            this.tpGeneral.Controls.Add(this.nmbQuantity);
            this.tpGeneral.Controls.Add(this.lblQuantity);
            this.tpGeneral.Location = new Point(4, 0x16);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new Padding(3);
            this.tpGeneral.Size = new Size(350, 0x126);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            this.ErrorProvider1.ContainerControl = this;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x178, 0x171);
            base.Controls.Add(this.TabControl1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Name = "FormNewInvoiceTransaction";
            this.Text = "New Invoice Transaction";
            this.TabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            ((ISupportInitialize) this.ErrorProvider1).EndInit();
            base.ResumeLayout(false);
        }

        private void LoadPayers(int CustomerID, int InvoiceID, int InvoiceDetailsID)
        {
            TablePayers dataTable = new TablePayers("Payers") {
                CustomerID = new int?(CustomerID),
                InvoiceID = new int?(InvoiceID),
                InvoiceDetailsID = new int?(InvoiceDetailsID)
            };
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.SelectCommand.CommandText = "SELECT\r\n  InsuranceCompanyID\r\n, InsuranceCompanyName\r\n, CustomerInsuranceID\r\nFROM (\r\n    SELECT\r\n      tbl_insurancecompany.ID as InsuranceCompanyID\r\n    , CASE WHEN (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins1: ', tbl_insurancecompany.Name)\r\n           WHEN (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins2: ', tbl_insurancecompany.Name)\r\n           WHEN (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins3: ', tbl_insurancecompany.Name)\r\n           WHEN (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins4: ', tbl_insurancecompany.Name)\r\n           ELSE 'Patient' END as InsuranceCompanyName\r\n    , CASE WHEN (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID) THEN stats.Insurance1_ID\r\n           WHEN (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID) THEN stats.Insurance2_ID\r\n           WHEN (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID) THEN stats.Insurance3_ID\r\n           WHEN (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID) THEN stats.Insurance4_ID\r\n           ELSE NULL END as CustomerInsuranceID\r\n    FROM view_invoicetransaction_statistics as stats\r\n         INNER JOIN tbl_insurancecompany ON (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID)\r\n                                         OR (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID)\r\n                                         OR (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID)\r\n                                         OR (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID)\r\n    WHERE (stats.CustomerID       = :CustomerID      )\r\n      AND (stats.InvoiceID        = :InvoiceID       )\r\n      AND (stats.InvoiceDetailsID = :InvoiceDetailsID)\r\n\r\n    UNION ALL\r\n\r\n    SELECT\r\n      NULL      as InsuranceCompanyID\r\n    , 'Patient' as InsuranceCompanyName\r\n    , NULL      as CustomerInsuranceID\r\n    FROM view_invoicetransaction_statistics as stats\r\n    WHERE (stats.CustomerID       = :CustomerID      )\r\n      AND (stats.InvoiceID        = :InvoiceID       )\r\n      AND (stats.InvoiceDetailsID = :InvoiceDetailsID)\r\n) as tmp\r\nORDER BY InsuranceCompanyName";
                adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                adapter.SelectCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                adapter.SelectCommand.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.cmbInsuranceCompany.DataSource = dataTable.DefaultView;
            this.cmbInsuranceCompany.DisplayMember = dataTable.Col_InsuranceCompanyName.ColumnName;
        }

        private void LoadTypes(string TransactionType)
        {
            DataView view = new DataView(Cache.Table_Invoice_TransactionType) {
                AllowDelete = false,
                AllowEdit = false,
                AllowNew = false
            };
            this.cmbTransactionType.DataSource = view;
            this.cmbTransactionType.DisplayMember = "Name";
            this.cmbTransactionType.ValueMember = "ID";
            TransactionType = (TransactionType != null) ? TransactionType.TrimEnd(new char[0]) : "";
            if (0 < TransactionType.Length)
            {
                IEnumerator enumerator;
                try
                {
                    enumerator = view.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        DataRowView current = (DataRowView) enumerator.Current;
                        if (string.Compare(current["Name"] as string, TransactionType) == 0)
                        {
                            this.cmbTransactionType.SelectedItem = current;
                            this.cmbTransactionType.Enabled = false;
                        }
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
        }

        private void SaveData()
        {
            this.ValidateData();
            DataRowView selectedItem = this.cmbInsuranceCompany.SelectedItem as DataRowView;
            if (selectedItem == null)
            {
                throw new UserNotifyException("You must select payer.");
            }
            DataRow row = selectedItem.Row;
            TablePayers table = row.Table as TablePayers;
            if ((table != null) && ((table.CustomerID != null) && (table.InvoiceID != null)))
            {
                int? invoiceDetailsID = table.InvoiceDetailsID;
                if (invoiceDetailsID != null)
                {
                    object obj1 = row[table.Col_InsuranceCompanyID];
                    DateTime time = Conversions.ToDate(Functions.GetDateBoxValue(this.dtbTransactionDate));
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        connection.Open();
                        MySqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            using (MySqlCommand command = new MySqlCommand("", connection, transaction))
                            {
                                command.Parameters.Add("InsuranceCompanyID", MySqlType.Int).Value = row[table.Col_InsuranceCompanyID];
                                command.Parameters.Add("CustomerInsuranceID", MySqlType.Int).Value = row[table.Col_CustomerInsuranceID];
                                command.Parameters.Add("TransactionTypeID", MySqlType.Int).Value = this.cmbTransactionType.SelectedValue;
                                command.Parameters.Add("TransactionDate", MySqlType.Date).Value = time;
                                command.Parameters.Add("Amount", MySqlType.Double).Value = this.nmbAmount.AsDouble.GetValueOrDefault(0.0);
                                command.Parameters.Add("Quantity", MySqlType.Double).Value = this.nmbQuantity.AsDouble.GetValueOrDefault(0.0);
                                command.Parameters.Add("Taxes", MySqlType.Double).Value = this.nmbTaxes.AsDouble.GetValueOrDefault(0.0);
                                command.Parameters.Add("Comments", MySqlType.Text).Value = this.txtComments.Text;
                                command.Parameters.Add("Extra", MySqlType.Text).Value = DBNull.Value;
                                command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                command.Parameters.Add("CustomerID", MySqlType.Int).Value = table.CustomerID.Value;
                                command.Parameters.Add("InvoiceID", MySqlType.Int).Value = table.InvoiceID.Value;
                                invoiceDetailsID = table.InvoiceDetailsID;
                                command.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = invoiceDetailsID.Value;
                                command.ExecuteInsert("tbl_invoice_transaction");
                            }
                            using (MySqlCommand command2 = new MySqlCommand("", connection, transaction))
                            {
                                command2.Parameters.Add("P_InvoiceID", MySqlType.Int).Value = table.InvoiceID.Value;
                                command2.Parameters.Add("P_Recursive", MySqlType.Int).Value = 1;
                                command2.ExecuteProcedure("Invoice_UpdateBalance");
                            }
                            transaction.Commit();
                        }
                        catch (Exception exception1)
                        {
                            ProjectData.SetProjectError(exception1);
                            transaction.Rollback();
                            throw;
                        }
                    }
                    this.raise_Saved(this, EventArgs.Empty);
                }
            }
        }

        private static void SetError(ErrorProvider provider, Control control, string value)
        {
            provider.SetError(control, value);
            provider.SetIconAlignment(control, ErrorIconAlignment.TopLeft);
            provider.SetIconPadding(control, -17);
        }

        public void SetParameters(FormParameters Params)
        {
            try
            {
                if (Params != null)
                {
                    int customerID = Conversions.ToInteger(Params["CustomerID"]);
                    this.LoadPayers(customerID, Conversions.ToInteger(Params["InvoiceID"]), Conversions.ToInteger(Params["InvoiceDetailsID"]));
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
            try
            {
                string transactionType = null;
                if (Params != null)
                {
                    transactionType = Params["TransactionType"] as string;
                }
                this.LoadTypes(transactionType);
            }
            catch (Exception exception3)
            {
                Exception ex = exception3;
                ProjectData.SetProjectError(ex);
                Exception exception2 = ex;
                this.ShowException(exception2);
                ProjectData.ClearProjectError();
            }
        }

        private void ValidateData()
        {
            if (this.cmbInsuranceCompany.SelectedItem is DataRowView)
            {
                SetError(this.ErrorProvider1, this.cmbInsuranceCompany, "");
            }
            else
            {
                SetError(this.ErrorProvider1, this.cmbInsuranceCompany, "You must select payer.");
            }
            if (this.cmbTransactionType.SelectedItem is DataRowView)
            {
                SetError(this.ErrorProvider1, this.cmbTransactionType, "");
            }
            else
            {
                SetError(this.ErrorProvider1, this.cmbTransactionType, "You must select transaction type.");
            }
            if (Functions.GetDateBoxValue(this.dtbTransactionDate) is DateTime)
            {
                SetError(this.ErrorProvider1, this.dtbTransactionDate, "");
            }
            else
            {
                SetError(this.ErrorProvider1, this.dtbTransactionDate, "You must select posting date.");
            }
            StringBuilder builder = new StringBuilder("There are some errors in input data");
            if (0 < Functions.EnumerateErrors(this, this.ErrorProvider1, builder))
            {
                throw new UserNotifyException(builder.ToString());
            }
        }

        [field: AccessedThroughProperty("txtComments")]
        private TextBox txtComments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblComments")]
        private Label lblComments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxes")]
        private Label lblTaxes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbTaxes")]
        private NumericBox nmbTaxes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuantity")]
        private Label lblQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbQuantity")]
        private NumericBox nmbQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTransactionType")]
        private Label lblTransactionType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbTransactionType")]
        private ComboBox cmbTransactionType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmount")]
        private Label lblAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAmount")]
        private NumericBox nmbAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lbTransactionDate")]
        private Label lbTransactionDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbTransactionDate")]
        private UltraDateTimeEditor dtbTransactionDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceCompany")]
        private Label lblInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInsuranceCompany")]
        private ComboBox cmbInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBatchNumber")]
        private Label lblBatchNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TextBox1")]
        internal virtual TextBox TextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpGeneral")]
        private TabPage tpGeneral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider1")]
        private ErrorProvider ErrorProvider1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public class TablePayers : TableBase
        {
            public DataColumn Col_CustomerInsuranceID;
            public DataColumn Col_InsuranceCompanyID;
            public DataColumn Col_InsuranceCompanyName;

            public TablePayers()
            {
            }

            public TablePayers(string TableName) : base(TableName)
            {
            }

            protected int? GetProperty(string Name) => 
                NullableConvert.ToInt32(base.ExtendedProperties[Name]);

            protected override void Initialize()
            {
                this.Col_CustomerInsuranceID = base.Columns["CustomerInsuranceID"];
                this.Col_InsuranceCompanyID = base.Columns["InsuranceCompanyID"];
                this.Col_InsuranceCompanyName = base.Columns["InsuranceCompanyName"];
            }

            protected override void InitializeClass()
            {
                this.Col_CustomerInsuranceID = base.Columns.Add("CustomerInsuranceID", typeof(int));
                this.Col_InsuranceCompanyID = base.Columns.Add("InsuranceCompanyID", typeof(int));
                this.Col_InsuranceCompanyName = base.Columns.Add("InsuranceCompanyName", typeof(string));
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

