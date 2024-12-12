namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormInvoiceTransaction : DmeForm, IParameters
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private int F_TransactionID;
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

        public FormInvoiceTransaction()
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

        private void ClearData()
        {
            this.Text = "Invoice Transation";
            this.txtInsuranceCompany.Text = "";
            this.txtTransactionType.Text = "";
            this.txtTransactionDate.Text = "";
            this.txtAmount.Text = "";
            this.txtTaxes.Text = "";
            this.txtQuantity.Text = "";
            this.txtBatchNumber.Text = "";
            this.txtComments.Text = "";
            this.PropertyGrid1.SelectedObject = new PaymentExtraData();
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
            this.txtComments = new TextBox();
            this.lblComments = new Label();
            this.lblTaxes = new Label();
            this.lblQuantity = new Label();
            this.lblTransactionType = new Label();
            this.lblAmount = new Label();
            this.lblTransactionDate = new Label();
            this.lblInsuranceCompany = new Label();
            this.txtInsuranceCompany = new Label();
            this.txtTransactionType = new Label();
            this.txtTransactionDate = new Label();
            this.txtAmount = new Label();
            this.txtQuantity = new Label();
            this.txtTaxes = new Label();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.txtBatchNumber = new Label();
            this.lblBatchNumber = new Label();
            this.TabControl1 = new TabControl();
            this.tpGeneral = new TabPage();
            this.tpExtra = new TabPage();
            this.PropertyGrid1 = new PropertyGrid();
            this.TabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpExtra.SuspendLayout();
            base.SuspendLayout();
            this.txtComments.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtComments.Location = new Point(0x60, 0xb0);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new Size(0xf8, 0x70);
            this.txtComments.TabIndex = 0x11;
            this.lblComments.BackColor = Color.Transparent;
            this.lblComments.BorderStyle = BorderStyle.FixedSingle;
            this.lblComments.Location = new Point(8, 0xb0);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new Size(80, 0x15);
            this.lblComments.TabIndex = 0x10;
            this.lblComments.Text = "Comments";
            this.lblComments.TextAlign = ContentAlignment.MiddleRight;
            this.lblTaxes.BorderStyle = BorderStyle.FixedSingle;
            this.lblTaxes.Location = new Point(8, 0x80);
            this.lblTaxes.Name = "lblTaxes";
            this.lblTaxes.Size = new Size(80, 0x15);
            this.lblTaxes.TabIndex = 12;
            this.lblTaxes.Text = "Taxes";
            this.lblTaxes.TextAlign = ContentAlignment.MiddleRight;
            this.lblQuantity.BorderStyle = BorderStyle.FixedSingle;
            this.lblQuantity.Location = new Point(8, 0x68);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new Size(80, 0x15);
            this.lblQuantity.TabIndex = 10;
            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.TextAlign = ContentAlignment.MiddleRight;
            this.lblTransactionType.BorderStyle = BorderStyle.FixedSingle;
            this.lblTransactionType.Location = new Point(8, 0x20);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Size = new Size(80, 0x15);
            this.lblTransactionType.TabIndex = 4;
            this.lblTransactionType.Text = "Tran Type";
            this.lblTransactionType.TextAlign = ContentAlignment.MiddleRight;
            this.lblAmount.BorderStyle = BorderStyle.FixedSingle;
            this.lblAmount.Location = new Point(8, 80);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new Size(80, 0x15);
            this.lblAmount.TabIndex = 8;
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextAlign = ContentAlignment.MiddleRight;
            this.lblTransactionDate.BorderStyle = BorderStyle.FixedSingle;
            this.lblTransactionDate.Location = new Point(8, 0x38);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Size = new Size(80, 0x15);
            this.lblTransactionDate.TabIndex = 6;
            this.lblTransactionDate.Text = "Tran Date";
            this.lblTransactionDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblInsuranceCompany.BorderStyle = BorderStyle.FixedSingle;
            this.lblInsuranceCompany.Location = new Point(8, 8);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new Size(80, 0x15);
            this.lblInsuranceCompany.TabIndex = 2;
            this.lblInsuranceCompany.Text = "Ins. Company";
            this.lblInsuranceCompany.TextAlign = ContentAlignment.MiddleRight;
            this.txtInsuranceCompany.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtInsuranceCompany.BorderStyle = BorderStyle.Fixed3D;
            this.txtInsuranceCompany.Location = new Point(0x60, 8);
            this.txtInsuranceCompany.Name = "txtInsuranceCompany";
            this.txtInsuranceCompany.Size = new Size(0xf8, 0x15);
            this.txtInsuranceCompany.TabIndex = 3;
            this.txtInsuranceCompany.TextAlign = ContentAlignment.MiddleLeft;
            this.txtTransactionType.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtTransactionType.BorderStyle = BorderStyle.Fixed3D;
            this.txtTransactionType.Location = new Point(0x60, 0x20);
            this.txtTransactionType.Name = "txtTransactionType";
            this.txtTransactionType.Size = new Size(0xf8, 0x15);
            this.txtTransactionType.TabIndex = 5;
            this.txtTransactionType.TextAlign = ContentAlignment.MiddleLeft;
            this.txtTransactionDate.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtTransactionDate.BorderStyle = BorderStyle.Fixed3D;
            this.txtTransactionDate.Location = new Point(0x60, 0x38);
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.Size = new Size(0xf8, 0x15);
            this.txtTransactionDate.TabIndex = 7;
            this.txtTransactionDate.TextAlign = ContentAlignment.MiddleLeft;
            this.txtAmount.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtAmount.BorderStyle = BorderStyle.Fixed3D;
            this.txtAmount.Location = new Point(0x60, 80);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new Size(0xf8, 0x15);
            this.txtAmount.TabIndex = 9;
            this.txtAmount.TextAlign = ContentAlignment.MiddleLeft;
            this.txtQuantity.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtQuantity.BorderStyle = BorderStyle.Fixed3D;
            this.txtQuantity.Location = new Point(0x60, 0x68);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new Size(0xf8, 0x15);
            this.txtQuantity.TabIndex = 11;
            this.txtQuantity.TextAlign = ContentAlignment.MiddleLeft;
            this.txtTaxes.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtTaxes.BorderStyle = BorderStyle.Fixed3D;
            this.txtTaxes.Location = new Point(0x60, 0x80);
            this.txtTaxes.Name = "txtTaxes";
            this.txtTaxes.Size = new Size(0xf8, 0x15);
            this.txtTaxes.TabIndex = 13;
            this.txtTaxes.TextAlign = ContentAlignment.MiddleLeft;
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x120, 0x150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.Location = new Point(0xd0, 0x150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.txtBatchNumber.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtBatchNumber.BorderStyle = BorderStyle.Fixed3D;
            this.txtBatchNumber.Location = new Point(0x60, 0x98);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new Size(0xf8, 0x15);
            this.txtBatchNumber.TabIndex = 15;
            this.txtBatchNumber.TextAlign = ContentAlignment.MiddleLeft;
            this.lblBatchNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblBatchNumber.Location = new Point(8, 0x98);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new Size(80, 0x15);
            this.lblBatchNumber.TabIndex = 14;
            this.lblBatchNumber.Text = "Batch#";
            this.lblBatchNumber.TextAlign = ContentAlignment.MiddleRight;
            this.TabControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.TabControl1.Controls.Add(this.tpGeneral);
            this.TabControl1.Controls.Add(this.tpExtra);
            this.TabControl1.Location = new Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(360, 320);
            this.TabControl1.TabIndex = 0;
            this.tpGeneral.Controls.Add(this.txtBatchNumber);
            this.tpGeneral.Controls.Add(this.lblBatchNumber);
            this.tpGeneral.Controls.Add(this.txtTaxes);
            this.tpGeneral.Controls.Add(this.txtQuantity);
            this.tpGeneral.Controls.Add(this.txtAmount);
            this.tpGeneral.Controls.Add(this.txtTransactionDate);
            this.tpGeneral.Controls.Add(this.txtTransactionType);
            this.tpGeneral.Controls.Add(this.txtInsuranceCompany);
            this.tpGeneral.Controls.Add(this.txtComments);
            this.tpGeneral.Controls.Add(this.lblComments);
            this.tpGeneral.Controls.Add(this.lblTaxes);
            this.tpGeneral.Controls.Add(this.lblQuantity);
            this.tpGeneral.Controls.Add(this.lblTransactionType);
            this.tpGeneral.Controls.Add(this.lblAmount);
            this.tpGeneral.Controls.Add(this.lblTransactionDate);
            this.tpGeneral.Controls.Add(this.lblInsuranceCompany);
            this.tpGeneral.Location = new Point(4, 0x16);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new Padding(3);
            this.tpGeneral.Size = new Size(0x160, 0x126);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            this.tpExtra.Controls.Add(this.PropertyGrid1);
            this.tpExtra.Location = new Point(4, 0x16);
            this.tpExtra.Name = "tpExtra";
            this.tpExtra.Padding = new Padding(3);
            this.tpExtra.Size = new Size(0x160, 0x126);
            this.tpExtra.TabIndex = 1;
            this.tpExtra.Text = "Extra";
            this.tpExtra.UseVisualStyleBackColor = true;
            this.PropertyGrid1.Dock = DockStyle.Fill;
            this.PropertyGrid1.Location = new Point(3, 3);
            this.PropertyGrid1.Name = "PropertyGrid1";
            this.PropertyGrid1.Size = new Size(0x15a, 0x120);
            this.PropertyGrid1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x178, 370);
            base.Controls.Add(this.TabControl1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Name = "FormInvoiceTransaction";
            this.Text = "Invoice Transaction";
            this.TabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.tpExtra.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  tbl_customer.ID as CustomerID\r\n, CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName\r\n, tbl_insurancecompany.ID   as InsuranceCompanyID\r\n, tbl_insurancecompany.Name as InsuranceCompanyName\r\n, tbl_invoice_transactiontype.ID   as TransactionTypeID\r\n, tbl_invoice_transactiontype.Name as TransactionTypeName\r\n, tbl_invoice_transaction.ID as TransactionID\r\n, tbl_invoice_transaction.TransactionDate\r\n, tbl_invoice_transaction.Amount\r\n, tbl_invoice_transaction.Quantity\r\n, tbl_invoice_transaction.Taxes\r\n, tbl_invoice_transaction.BatchNumber\r\n, tbl_invoice_transaction.Comments\r\n, tbl_invoice_transaction.Extra\r\nFROM tbl_invoice_transaction\r\n     LEFT JOIN tbl_customer ON tbl_invoice_transaction.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_insurancecompany ON tbl_invoice_transaction.InsuranceCompanyID = tbl_insurancecompany.ID\r\n     LEFT JOIN tbl_invoice_transactiontype ON tbl_invoice_transaction.TransactionTypeID = tbl_invoice_transactiontype.ID\r\nWHERE (tbl_invoice_transaction.ID = :TransactionID)";
                    command.Parameters.Add("TransactionID", MySqlType.Int).Value = this.TransactionID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.Text = $"Invoice Transation #{reader["TransactionID"]}";
                            this.txtInsuranceCompany.Text = reader.IsDBNull(reader.GetOrdinal("InsuranceCompanyID")) ? "Patient" : $"({reader["InsuranceCompanyID"]}) {reader["InsuranceCompanyName"]}";
                            this.txtTransactionType.Text = $"({reader["TransactionTypeID"]}) {reader["TransactionTypeName"]}";
                            this.txtTransactionDate.Text = $"{reader["TransactionDate"]:g}";
                            this.txtAmount.Text = $"{reader["Amount"]:0.00}";
                            this.txtTaxes.Text = $"{reader["Taxes"]:0.00}";
                            this.txtQuantity.Text = $"{reader["Quantity"]:0}";
                            this.txtBatchNumber.Text = $"{reader["BatchNumber"]}";
                            this.txtComments.Text = $"{reader["Comments"]}";
                            SqlString str = new SqlString(Convert.ToString(reader["TransactionTypeName"], CultureInfo.InvariantCulture));
                            if (str.Equals("Payment") || str.Equals("Denied"))
                            {
                                this.PropertyGrid1.SelectedObject = PaymentExtraData.Parse(Convert.ToString(reader["Extra"]));
                                this.tpExtra.Parent = this.TabControl1;
                            }
                            else if (str.Equals("Voided Submission"))
                            {
                                this.PropertyGrid1.SelectedObject = VoidedSubmissionExtraData.Parse(Convert.ToString(reader["Extra"]));
                                this.tpExtra.Parent = this.TabControl1;
                            }
                            else
                            {
                                this.PropertyGrid1.SelectedObject = null;
                                this.tpExtra.Parent = null;
                            }
                        }
                    }
                }
            }
        }

        private void SaveData()
        {
            object selectedObject = this.PropertyGrid1.SelectedObject;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    if ((this.tpExtra.Parent != null) && (selectedObject != null))
                    {
                        command.Parameters.Add("Extra", MySqlType.Text).Value = selectedObject.ToString();
                    }
                    command.Parameters.Add("Comments", MySqlType.Text).Value = this.txtComments.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                    command.Parameters.Add("ID", MySqlType.Int).Value = this.TransactionID;
                    string[] whereParameters = new string[] { "ID" };
                    if (command.ExecuteUpdate("tbl_invoice_transaction", whereParameters) == 0)
                    {
                        throw new UserNotifyException("Transaction #" + Conversions.ToString(this.TransactionID) + " were not updated");
                    }
                }
            }
            this.raise_Saved(this, EventArgs.Empty);
        }

        public void SetParameters(FormParameters Params)
        {
            try
            {
                if (Params != null)
                {
                    this.TransactionID = Conversions.ToInteger(Params["ID"]);
                    this.LoadData();
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

        [field: AccessedThroughProperty("txtComments")]
        private TextBox txtComments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblComments")]
        private Label lblComments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxes")]
        private Label lblTaxes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuantity")]
        private Label lblQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTransactionType")]
        private Label lblTransactionType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmount")]
        private Label lblAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTransactionDate")]
        private Label lblTransactionDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceCompany")]
        private Label lblInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInsuranceCompany")]
        private Label txtInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTransactionType")]
        private Label txtTransactionType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTransactionDate")]
        private Label txtTransactionDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAmount")]
        private Label txtAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtQuantity")]
        private Label txtQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxes")]
        private Label txtTaxes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBatchNumber")]
        private Label txtBatchNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBatchNumber")]
        private Label lblBatchNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpGeneral")]
        private TabPage tpGeneral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PropertyGrid1")]
        private PropertyGrid PropertyGrid1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpExtra")]
        private TabPage tpExtra { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public int TransactionID
        {
            get => 
                this.F_TransactionID;
            set => 
                this.F_TransactionID = value;
        }
    }
}

