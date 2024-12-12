namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Core.Extensions;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormNewPayment : DmeForm, IParameters
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

        public FormNewPayment()
        {
            this.InitializeComponent();
        }

        private static Binding AddColumnBinding(Control Control, DataColumn Column, bool ReadOnly)
        {
            Binding binding = new Binding("Text", Column.Table.DefaultView, Column.ColumnName) {
                FormattingEnabled = true,
                FormatString = "0.00",
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged,
                DataSourceNullValue = DBNull.Value,
                DataSourceUpdateMode = ReadOnly ? DataSourceUpdateMode.Never : DataSourceUpdateMode.OnPropertyChanged,
                NullValue = ""
            };
            Control.DataBindings.Clear();
            Control.DataBindings.Add(binding);
            return binding;
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
            this.Text = "New Payment";
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
            this.lblCheckDate = new Label();
            this.lblCheckNumber = new Label();
            this.lblPostingDate = new Label();
            this.dtbCheckDate = new UltraDateTimeEditor();
            this.cmbInsuranceCompany = new ComboBox();
            this.txtCheckNumber = new TextBox();
            this.lblInsuranceCompany = new Label();
            this.btnCancel = new Button();
            this.txtBalance = new Label();
            this.lblBalanceCaption = new Label();
            this.txtAllowableAmount = new Label();
            this.txtBillableAmount = new Label();
            this.lblAllowableAmount = new Label();
            this.lblBillableAmountCaption = new Label();
            this.dtbPostingDate = new UltraDateTimeEditor();
            this.btnOK = new Button();
            this.txtExpectedPaidAmount = new Label();
            this.lblExpected = new Label();
            this.lblAllowed = new Label();
            this.lblDeductible = new Label();
            this.lblCoins = new Label();
            this.txtPostingPaidAmount = new Label();
            this.lblActual = new Label();
            this.lblPaid = new Label();
            this.txtEnteredAllowableAmount = new TextBox();
            this.txtEnteredPaidAmount = new TextBox();
            this.txtEnteredCoinsAmount = new TextBox();
            this.txtEnteredDeductibleAmount = new TextBox();
            this.ErrorProvider1 = new ErrorProvider(this.components);
            this.txtComments = new TextBox();
            this.lblComments = new Label();
            this.lblClaimControlNumber = new Label();
            this.txtClaimControlNumber = new TextBox();
            ((ISupportInitialize) this.ErrorProvider1).BeginInit();
            base.SuspendLayout();
            this.lblCheckDate.BorderStyle = BorderStyle.FixedSingle;
            this.lblCheckDate.Location = new Point(8, 0x70);
            this.lblCheckDate.Margin = new Padding(4, 1, 4, 1);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new Size(80, 20);
            this.lblCheckDate.TabIndex = 0x16;
            this.lblCheckDate.Text = "Check Date";
            this.lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblCheckNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblCheckNumber.Location = new Point(8, 0x88);
            this.lblCheckNumber.Margin = new Padding(4, 1, 4, 1);
            this.lblCheckNumber.Name = "lblCheckNumber";
            this.lblCheckNumber.Size = new Size(80, 20);
            this.lblCheckNumber.TabIndex = 0x18;
            this.lblCheckNumber.Text = "Check #";
            this.lblCheckNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblPostingDate.BorderStyle = BorderStyle.FixedSingle;
            this.lblPostingDate.Location = new Point(8, 0x58);
            this.lblPostingDate.Margin = new Padding(4, 1, 4, 1);
            this.lblPostingDate.Name = "lblPostingDate";
            this.lblPostingDate.Size = new Size(80, 20);
            this.lblPostingDate.TabIndex = 20;
            this.lblPostingDate.Text = "Posting Date";
            this.lblPostingDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbCheckDate.Location = new Point(0x60, 0x70);
            this.dtbCheckDate.Name = "dtbCheckDate";
            this.dtbCheckDate.Size = new Size(0x80, 0x15);
            this.dtbCheckDate.TabIndex = 0x17;
            this.cmbInsuranceCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbInsuranceCompany.Location = new Point(0x60, 8);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new Size(0x240, 0x15);
            this.cmbInsuranceCompany.TabIndex = 1;
            this.txtCheckNumber.Location = new Point(0x60, 0x88);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new Size(0x80, 20);
            this.txtCheckNumber.TabIndex = 0x19;
            this.txtCheckNumber.WordWrap = false;
            this.lblInsuranceCompany.BorderStyle = BorderStyle.FixedSingle;
            this.lblInsuranceCompany.Location = new Point(8, 8);
            this.lblInsuranceCompany.Margin = new Padding(4, 1, 4, 1);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new Size(80, 20);
            this.lblInsuranceCompany.TabIndex = 0;
            this.lblInsuranceCompany.Text = "Payer";
            this.lblInsuranceCompany.TextAlign = ContentAlignment.MiddleRight;
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.Location = new Point(0x250, 0x145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0x1f;
            this.btnCancel.Text = "Cancel";
            this.txtBalance.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.txtBalance.BorderStyle = BorderStyle.FixedSingle;
            this.txtBalance.Location = new Point(0xe0, 0x38);
            this.txtBalance.Margin = new Padding(3);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new Size(60, 20);
            this.txtBalance.TabIndex = 7;
            this.txtBalance.TextAlign = ContentAlignment.MiddleRight;
            this.lblBalanceCaption.BorderStyle = BorderStyle.FixedSingle;
            this.lblBalanceCaption.Location = new Point(0xe0, 0x20);
            this.lblBalanceCaption.Name = "lblBalanceCaption";
            this.lblBalanceCaption.Size = new Size(60, 0x16);
            this.lblBalanceCaption.TabIndex = 6;
            this.lblBalanceCaption.Text = "Balance";
            this.lblBalanceCaption.TextAlign = ContentAlignment.MiddleCenter;
            this.txtAllowableAmount.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.txtAllowableAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtAllowableAmount.Location = new Point(160, 0x38);
            this.txtAllowableAmount.Margin = new Padding(3);
            this.txtAllowableAmount.Name = "txtAllowableAmount";
            this.txtAllowableAmount.Size = new Size(60, 20);
            this.txtAllowableAmount.TabIndex = 5;
            this.txtAllowableAmount.TextAlign = ContentAlignment.MiddleRight;
            this.txtBillableAmount.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.txtBillableAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtBillableAmount.Location = new Point(0x60, 0x38);
            this.txtBillableAmount.Margin = new Padding(3);
            this.txtBillableAmount.Name = "txtBillableAmount";
            this.txtBillableAmount.Size = new Size(60, 20);
            this.txtBillableAmount.TabIndex = 3;
            this.txtBillableAmount.TextAlign = ContentAlignment.MiddleRight;
            this.lblAllowableAmount.BorderStyle = BorderStyle.FixedSingle;
            this.lblAllowableAmount.Location = new Point(160, 0x20);
            this.lblAllowableAmount.Name = "lblAllowableAmount";
            this.lblAllowableAmount.Size = new Size(60, 0x16);
            this.lblAllowableAmount.TabIndex = 4;
            this.lblAllowableAmount.Text = "Allowable";
            this.lblAllowableAmount.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBillableAmountCaption.BorderStyle = BorderStyle.FixedSingle;
            this.lblBillableAmountCaption.Location = new Point(0x60, 0x20);
            this.lblBillableAmountCaption.Name = "lblBillableAmountCaption";
            this.lblBillableAmountCaption.Size = new Size(60, 0x16);
            this.lblBillableAmountCaption.TabIndex = 2;
            this.lblBillableAmountCaption.Text = "Billable";
            this.lblBillableAmountCaption.TextAlign = ContentAlignment.MiddleCenter;
            this.dtbPostingDate.Location = new Point(0x60, 0x58);
            this.dtbPostingDate.Name = "dtbPostingDate";
            this.dtbPostingDate.Size = new Size(0x80, 0x15);
            this.dtbPostingDate.TabIndex = 0x15;
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.Location = new Point(0x200, 0x145);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 30;
            this.btnOK.Text = "OK";
            this.txtExpectedPaidAmount.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.txtExpectedPaidAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtExpectedPaidAmount.Location = new Point(0x120, 0x38);
            this.txtExpectedPaidAmount.Margin = new Padding(3);
            this.txtExpectedPaidAmount.Name = "txtExpectedPaidAmount";
            this.txtExpectedPaidAmount.Size = new Size(60, 20);
            this.txtExpectedPaidAmount.TabIndex = 9;
            this.txtExpectedPaidAmount.TextAlign = ContentAlignment.MiddleRight;
            this.lblExpected.BorderStyle = BorderStyle.FixedSingle;
            this.lblExpected.Location = new Point(0x120, 0x20);
            this.lblExpected.Name = "lblExpected";
            this.lblExpected.Size = new Size(60, 0x16);
            this.lblExpected.TabIndex = 8;
            this.lblExpected.Text = "Expected";
            this.lblExpected.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAllowed.BorderStyle = BorderStyle.FixedSingle;
            this.lblAllowed.Location = new Point(0x160, 0x20);
            this.lblAllowed.Margin = new Padding(4, 1, 4, 1);
            this.lblAllowed.Name = "lblAllowed";
            this.lblAllowed.Size = new Size(60, 0x16);
            this.lblAllowed.TabIndex = 10;
            this.lblAllowed.Text = "Allowed";
            this.lblAllowed.TextAlign = ContentAlignment.MiddleCenter;
            this.lblDeductible.BorderStyle = BorderStyle.FixedSingle;
            this.lblDeductible.Location = new Point(0x1a0, 0x20);
            this.lblDeductible.Margin = new Padding(4, 1, 4, 1);
            this.lblDeductible.Name = "lblDeductible";
            this.lblDeductible.Size = new Size(60, 0x16);
            this.lblDeductible.TabIndex = 12;
            this.lblDeductible.Text = "Deductible";
            this.lblDeductible.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCoins.BorderStyle = BorderStyle.FixedSingle;
            this.lblCoins.Location = new Point(480, 0x20);
            this.lblCoins.Margin = new Padding(4, 1, 4, 1);
            this.lblCoins.Name = "lblCoins";
            this.lblCoins.Size = new Size(60, 0x16);
            this.lblCoins.TabIndex = 14;
            this.lblCoins.Text = "Coins";
            this.lblCoins.TextAlign = ContentAlignment.MiddleCenter;
            this.txtPostingPaidAmount.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.txtPostingPaidAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtPostingPaidAmount.Location = new Point(0x260, 0x38);
            this.txtPostingPaidAmount.Margin = new Padding(3);
            this.txtPostingPaidAmount.Name = "txtPostingPaidAmount";
            this.txtPostingPaidAmount.Size = new Size(60, 20);
            this.txtPostingPaidAmount.TabIndex = 0x13;
            this.txtPostingPaidAmount.TextAlign = ContentAlignment.MiddleRight;
            this.lblActual.BorderStyle = BorderStyle.FixedSingle;
            this.lblActual.Location = new Point(0x260, 0x20);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new Size(60, 0x16);
            this.lblActual.TabIndex = 0x12;
            this.lblActual.Text = "Actual";
            this.lblActual.TextAlign = ContentAlignment.MiddleCenter;
            this.lblPaid.BorderStyle = BorderStyle.FixedSingle;
            this.lblPaid.Location = new Point(0x220, 0x20);
            this.lblPaid.Margin = new Padding(4, 1, 4, 1);
            this.lblPaid.Name = "lblPaid";
            this.lblPaid.Size = new Size(60, 0x16);
            this.lblPaid.TabIndex = 0x10;
            this.lblPaid.Text = "Paid";
            this.lblPaid.TextAlign = ContentAlignment.MiddleCenter;
            this.txtEnteredAllowableAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtEnteredAllowableAmount.Location = new Point(0x160, 0x38);
            this.txtEnteredAllowableAmount.Name = "txtEnteredAllowableAmount";
            this.txtEnteredAllowableAmount.Size = new Size(60, 20);
            this.txtEnteredAllowableAmount.TabIndex = 11;
            this.txtEnteredAllowableAmount.TextAlign = HorizontalAlignment.Right;
            this.txtEnteredPaidAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtEnteredPaidAmount.Location = new Point(0x220, 0x38);
            this.txtEnteredPaidAmount.Name = "txtEnteredPaidAmount";
            this.txtEnteredPaidAmount.Size = new Size(60, 20);
            this.txtEnteredPaidAmount.TabIndex = 0x11;
            this.txtEnteredPaidAmount.TextAlign = HorizontalAlignment.Right;
            this.txtEnteredCoinsAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtEnteredCoinsAmount.Location = new Point(480, 0x38);
            this.txtEnteredCoinsAmount.Name = "txtEnteredCoinsAmount";
            this.txtEnteredCoinsAmount.Size = new Size(60, 20);
            this.txtEnteredCoinsAmount.TabIndex = 15;
            this.txtEnteredCoinsAmount.TextAlign = HorizontalAlignment.Right;
            this.txtEnteredDeductibleAmount.BorderStyle = BorderStyle.FixedSingle;
            this.txtEnteredDeductibleAmount.Location = new Point(0x1a0, 0x38);
            this.txtEnteredDeductibleAmount.Name = "txtEnteredDeductibleAmount";
            this.txtEnteredDeductibleAmount.Size = new Size(60, 20);
            this.txtEnteredDeductibleAmount.TabIndex = 13;
            this.txtEnteredDeductibleAmount.TextAlign = HorizontalAlignment.Right;
            this.ErrorProvider1.ContainerControl = this;
            this.txtComments.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtComments.Location = new Point(0x60, 0xb8);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new Size(0x240, 0x85);
            this.txtComments.TabIndex = 0x1d;
            this.lblComments.BackColor = Color.Transparent;
            this.lblComments.BorderStyle = BorderStyle.FixedSingle;
            this.lblComments.Location = new Point(8, 0xb8);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new Size(80, 0x15);
            this.lblComments.TabIndex = 0x1c;
            this.lblComments.Text = "Comments";
            this.lblComments.TextAlign = ContentAlignment.MiddleRight;
            this.lblClaimControlNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblClaimControlNumber.Location = new Point(8, 160);
            this.lblClaimControlNumber.Margin = new Padding(4, 1, 4, 1);
            this.lblClaimControlNumber.Name = "lblClaimControlNumber";
            this.lblClaimControlNumber.Size = new Size(80, 20);
            this.lblClaimControlNumber.TabIndex = 0x1a;
            this.lblClaimControlNumber.Text = "ICN/CCN";
            this.lblClaimControlNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtClaimControlNumber.Location = new Point(0x60, 160);
            this.txtClaimControlNumber.Name = "txtClaimControlNumber";
            this.txtClaimControlNumber.Size = new Size(0x80, 20);
            this.txtClaimControlNumber.TabIndex = 0x1b;
            this.txtClaimControlNumber.WordWrap = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(680, 0x165);
            base.Controls.Add(this.lblClaimControlNumber);
            base.Controls.Add(this.txtClaimControlNumber);
            base.Controls.Add(this.txtComments);
            base.Controls.Add(this.lblComments);
            base.Controls.Add(this.txtEnteredDeductibleAmount);
            base.Controls.Add(this.txtEnteredCoinsAmount);
            base.Controls.Add(this.txtEnteredPaidAmount);
            base.Controls.Add(this.txtEnteredAllowableAmount);
            base.Controls.Add(this.lblPaid);
            base.Controls.Add(this.txtPostingPaidAmount);
            base.Controls.Add(this.lblActual);
            base.Controls.Add(this.lblCoins);
            base.Controls.Add(this.lblDeductible);
            base.Controls.Add(this.lblAllowed);
            base.Controls.Add(this.txtExpectedPaidAmount);
            base.Controls.Add(this.lblExpected);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.dtbPostingDate);
            base.Controls.Add(this.txtBalance);
            base.Controls.Add(this.lblBalanceCaption);
            base.Controls.Add(this.txtAllowableAmount);
            base.Controls.Add(this.txtBillableAmount);
            base.Controls.Add(this.lblAllowableAmount);
            base.Controls.Add(this.lblBillableAmountCaption);
            base.Controls.Add(this.lblCheckNumber);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.txtCheckNumber);
            base.Controls.Add(this.lblCheckDate);
            base.Controls.Add(this.lblInsuranceCompany);
            base.Controls.Add(this.cmbInsuranceCompany);
            base.Controls.Add(this.lblPostingDate);
            base.Controls.Add(this.dtbCheckDate);
            base.Name = "FormNewPayment";
            this.Text = "New Payment";
            ((ISupportInitialize) this.ErrorProvider1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadData(int CustomerID, int InvoiceID, int InvoiceDetailsID)
        {
            TablePayers dataTable = new TablePayers("Payers") {
                CustomerID = new int?(CustomerID),
                InvoiceID = new int?(InvoiceID),
                InvoiceDetailsID = new int?(InvoiceDetailsID)
            };
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.SelectCommand.CommandText = "SELECT\r\n  InsuranceCompanyID\r\n, InsuranceCompanyName\r\n, CustomerInsuranceID\r\n, BillingCode\r\n, BillableAmount\r\n, AllowableAmount\r\n, TotalWriteoffAmount\r\n, TotalPaymentAmount\r\n, Quantity\r\n, Percent\r\n, PaymentExists\r\n, CurrentPayer\r\n, Basis\r\n, Balance\r\nFROM (\r\n    SELECT\r\n      tbl_insurancecompany.ID as InsuranceCompanyID\r\n    , CASE WHEN (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins1: ', tbl_insurancecompany.Name)\r\n           WHEN (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins2: ', tbl_insurancecompany.Name)\r\n           WHEN (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins3: ', tbl_insurancecompany.Name)\r\n           WHEN (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID) THEN CONCAT('Ins4: ', tbl_insurancecompany.Name)\r\n           ELSE 'Patient' END as InsuranceCompanyName\r\n    , CASE WHEN (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID) THEN stats.Insurance1_ID\r\n           WHEN (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID) THEN stats.Insurance2_ID\r\n           WHEN (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID) THEN stats.Insurance3_ID\r\n           WHEN (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID) THEN stats.Insurance4_ID\r\n           ELSE NULL END as CustomerInsuranceID\r\n    , stats.BillingCode\r\n    , stats.BillableAmount\r\n    , stats.AllowableAmount\r\n    , stats.WriteoffAmount as TotalWriteoffAmount\r\n    , stats.PaymentAmount as TotalPaymentAmount\r\n    , stats.Quantity\r\n    , stats.Percent\r\n    , CASE WHEN (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID) AND (stats.Payments & 01 != 0) THEN 1\r\n           WHEN (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID) AND (stats.Payments & 02 != 0) THEN 1\r\n           WHEN (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID) AND (stats.Payments & 04 != 0) THEN 1\r\n           WHEN (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID) AND (stats.Payments & 08 != 0) THEN 1\r\n           ELSE 0 END as PaymentExists\r\n    , CASE WHEN (stats.InsuranceCompany1_ID = tbl_insurancecompany.ID) THEN 'Ins1'\r\n           WHEN (stats.InsuranceCompany2_ID = tbl_insurancecompany.ID) THEN 'Ins2'\r\n           WHEN (stats.InsuranceCompany3_ID = tbl_insurancecompany.ID) THEN 'Ins3'\r\n           WHEN (stats.InsuranceCompany4_ID = tbl_insurancecompany.ID) THEN 'Ins4'\r\n           ELSE null END as CurrentPayer\r\n    , stats.Basis\r\n    , stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount as Balance\r\n    FROM view_invoicetransaction_statistics as stats\r\n         INNER JOIN tbl_insurancecompany ON ((stats.InsuranceCompany1_ID = tbl_insurancecompany.ID))\r\n                                         OR ((stats.InsuranceCompany2_ID = tbl_insurancecompany.ID))\r\n                                         OR ((stats.InsuranceCompany3_ID = tbl_insurancecompany.ID))\r\n                                         OR ((stats.InsuranceCompany4_ID = tbl_insurancecompany.ID))\r\n    WHERE (stats.CustomerID       = :CustomerID      )\r\n      AND (stats.InvoiceID        = :InvoiceID       )\r\n      AND (stats.InvoiceDetailsID = :InvoiceDetailsID)\r\n\r\n    UNION ALL\r\n\r\n    SELECT\r\n      NULL      as InsuranceCompanyID\r\n    , 'Patient' as InsuranceCompanyName\r\n    , NULL      as CustomerInsuranceID\r\n    , stats.BillingCode\r\n    , stats.BillableAmount\r\n    , stats.AllowableAmount\r\n    , stats.WriteoffAmount as TotalWriteoffAmount\r\n    , stats.PaymentAmount  as TotalPaymentAmount\r\n    , stats.Quantity\r\n    , stats.Percent\r\n    , 0 as PaymentExists\r\n    , 'Patient' as CurrentPayer\r\n    , stats.Basis\r\n    , stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount as Balance\r\n    FROM view_invoicetransaction_statistics as stats\r\n    WHERE (stats.CustomerID       = :CustomerID      )\r\n      AND (stats.InvoiceID        = :InvoiceID       )\r\n      AND (stats.InvoiceDetailsID = :InvoiceDetailsID)\r\n) as tmp\r\nORDER BY InsuranceCompanyName";
                adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID;
                adapter.SelectCommand.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                adapter.SelectCommand.Parameters.Add("InvoiceDetailsID", MySqlType.Int).Value = InvoiceDetailsID;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.cmbInsuranceCompany.DataSource = dataTable.DefaultView;
            this.cmbInsuranceCompany.DisplayMember = dataTable.Col_InsuranceCompanyName.ColumnName;
            AddColumnBinding(this.txtAllowableAmount, dataTable.Col_AllowableAmount, true);
            AddColumnBinding(this.txtBillableAmount, dataTable.Col_BillableAmount, true);
            AddColumnBinding(this.txtBalance, dataTable.Col_Balance, true);
            AddColumnBinding(this.txtExpectedPaidAmount, dataTable.Col_ExpectedPaidAmount, true);
            AddColumnBinding(this.txtEnteredAllowableAmount, dataTable.Col_EnteredAllowableAmount, false);
            AddColumnBinding(this.txtEnteredCoinsAmount, dataTable.Col_EnteredCoinsAmount, false);
            AddColumnBinding(this.txtEnteredDeductibleAmount, dataTable.Col_EnteredDeductibleAmount, false);
            AddColumnBinding(this.txtEnteredPaidAmount, dataTable.Col_EnteredPaidAmount, false);
            AddColumnBinding(this.txtPostingPaidAmount, dataTable.Col_PostingPaidAmount, true);
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
                    object obj2 = row[table.Col_InsuranceCompanyID];
                    DateTime time = Conversions.ToDate(Functions.GetDateBoxValue(this.dtbCheckDate));
                    DateTime time2 = Conversions.ToDate(Functions.GetDateBoxValue(this.dtbPostingDate));
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        connection.Open();
                        PaymentExtraData data = new PaymentExtraData {
                            PaymentMethod = 1,
                            CheckNumber = this.txtCheckNumber.Text,
                            CheckDate = new DateTime?(time),
                            ClaimControlNumber = this.txtClaimControlNumber.Text,
                            Billable = row.GetDecimal(table.Col_BillableAmount),
                            Paid = row.GetDecimal(table.Col_PostingPaidAmount),
                            Allowable = row.GetDecimal(table.Col_EnteredAllowableAmount),
                            Deductible = row.GetDecimal(table.Col_EnteredDeductibleAmount),
                            Coins = row.GetDecimal(table.Col_EnteredCoinsAmount)
                        };
                        MySqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            using (MySqlCommand command = new MySqlCommand("", connection, transaction))
                            {
                                invoiceDetailsID = table.InvoiceDetailsID;
                                command.Parameters.Add("P_InvoiceDetailsID", MySqlType.Int).Value = invoiceDetailsID.Value;
                                command.Parameters.Add("P_InsuranceCompanyID", MySqlType.Int).Value = obj2;
                                command.Parameters.Add("P_TransactionDate", MySqlType.Date).Value = time2;
                                command.Parameters.Add("P_Extra", MySqlType.Text).Value = data.ToString();
                                command.Parameters.Add("P_Comments", MySqlType.Text).Value = this.txtComments.Text;
                                command.Parameters.Add("P_Options", MySqlType.Text).Value = "Adjust Allowable";
                                command.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                command.Parameters.Add("P_Result", MySqlType.VarChar, 0xff).Direction = ParameterDirection.Output;
                                command.ExecuteProcedure("InvoiceDetails_AddPayment");
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
                    int customerID = NullableConvert.ToInt32(Params["CustomerID"]).Value;
                    this.LoadData(customerID, NullableConvert.ToInt32(Params["InvoiceID"]).Value, NullableConvert.ToInt32(Params["InvoiceDetailsID"]).Value);
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
            if (Functions.GetDateBoxValue(this.dtbCheckDate) is DateTime)
            {
                SetError(this.ErrorProvider1, this.dtbCheckDate, "");
            }
            else
            {
                SetError(this.ErrorProvider1, this.dtbCheckDate, "You must select check date.");
            }
            if (Functions.GetDateBoxValue(this.dtbPostingDate) is DateTime)
            {
                SetError(this.ErrorProvider1, this.dtbPostingDate, "");
            }
            else
            {
                SetError(this.ErrorProvider1, this.dtbPostingDate, "You must select posting date.");
            }
            if (this.txtCheckNumber.Text.TrimEnd(new char[0]).Length == 0)
            {
                SetError(this.ErrorProvider1, this.txtCheckNumber, "You must enter check number.");
            }
            else
            {
                SetError(this.ErrorProvider1, this.txtCheckNumber, "");
            }
            StringBuilder builder = new StringBuilder("There are some errors in input data");
            if (0 < Functions.EnumerateErrors(this, this.ErrorProvider1, builder))
            {
                throw new UserNotifyException(builder.ToString());
            }
        }

        [field: AccessedThroughProperty("lblCheckDate")]
        private Label lblCheckDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCheckNumber")]
        private Label lblCheckNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPostingDate")]
        private Label lblPostingDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbCheckDate")]
        private UltraDateTimeEditor dtbCheckDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInsuranceCompany")]
        private ComboBox cmbInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCheckNumber")]
        private TextBox txtCheckNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceCompany")]
        private Label lblInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBalance")]
        private Label txtBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBalanceCaption")]
        private Label lblBalanceCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAllowableAmount")]
        private Label txtAllowableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBillableAmount")]
        private Label txtBillableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAllowableAmount")]
        private Label lblAllowableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillableAmountCaption")]
        private Label lblBillableAmountCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbPostingDate")]
        private UltraDateTimeEditor dtbPostingDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtExpectedPaidAmount")]
        private Label txtExpectedPaidAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblExpected")]
        private Label lblExpected { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAllowed")]
        private Label lblAllowed { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDeductible")]
        private Label lblDeductible { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCoins")]
        private Label lblCoins { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPostingPaidAmount")]
        private Label txtPostingPaidAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblActual")]
        private Label lblActual { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPaid")]
        private Label lblPaid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEnteredAllowableAmount")]
        private TextBox txtEnteredAllowableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEnteredPaidAmount")]
        private TextBox txtEnteredPaidAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEnteredCoinsAmount")]
        private TextBox txtEnteredCoinsAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEnteredDeductibleAmount")]
        private TextBox txtEnteredDeductibleAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider1")]
        private ErrorProvider ErrorProvider1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtComments")]
        private TextBox txtComments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblComments")]
        private Label lblComments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblClaimControlNumber")]
        private Label lblClaimControlNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtClaimControlNumber")]
        private TextBox txtClaimControlNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public class TablePayers : TableBase
        {
            public DataColumn Col_CustomerInsuranceID;
            public DataColumn Col_InsuranceCompanyID;
            public DataColumn Col_InsuranceCompanyName;
            public DataColumn Col_BillableAmount;
            public DataColumn Col_AllowableAmount;
            public DataColumn Col_TotalWriteoffAmount;
            public DataColumn Col_TotalPaymentAmount;
            public DataColumn Col_Quantity;
            public DataColumn Col_Percent;
            public DataColumn Col_Basis;
            public DataColumn Col_PaymentExists;
            public DataColumn Col_CurrentPayer;
            public DataColumn Col_Balance;
            public DataColumn Col_ExpectedPaidAmount;
            public DataColumn Col_EnteredAllowableAmount;
            public DataColumn Col_EnteredPaidAmount;
            public DataColumn Col_EnteredCoinsAmount;
            public DataColumn Col_EnteredDeductibleAmount;
            public DataColumn Col_PostingPaidAmount;

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
                this.Col_BillableAmount = base.Columns["BillableAmount"];
                this.Col_AllowableAmount = base.Columns["AllowableAmount"];
                this.Col_TotalWriteoffAmount = base.Columns["TotalWriteoffAmount"];
                this.Col_TotalPaymentAmount = base.Columns["TotalPaymentAmount"];
                this.Col_Quantity = base.Columns["Quantity"];
                this.Col_Percent = base.Columns["Percent"];
                this.Col_Basis = base.Columns["Basis"];
                this.Col_CurrentPayer = base.Columns["CurrentPayer"];
                this.Col_Balance = base.Columns["Balance"];
                this.Col_ExpectedPaidAmount = base.Columns["ExpectedPaidAmount"];
                this.Col_EnteredAllowableAmount = base.Columns["EnteredAllowableAmount"];
                this.Col_EnteredPaidAmount = base.Columns["EnteredPaidAmount"];
                this.Col_EnteredCoinsAmount = base.Columns["EnteredCoinsAmount"];
                this.Col_EnteredDeductibleAmount = base.Columns["EnteredDeductibleAmount"];
                this.Col_PostingPaidAmount = base.Columns["PostingPaidAmount"];
            }

            protected override void InitializeClass()
            {
                this.Col_CustomerInsuranceID = base.Columns.Add("CustomerInsuranceID", typeof(int));
                this.Col_InsuranceCompanyID = base.Columns.Add("InsuranceCompanyID", typeof(int));
                this.Col_InsuranceCompanyName = base.Columns.Add("InsuranceCompanyName", typeof(string));
                this.Col_BillableAmount = base.Columns.Add("BillableAmount", typeof(double));
                this.Col_BillableAmount.AllowDBNull = false;
                this.Col_AllowableAmount = base.Columns.Add("AllowableAmount", typeof(double));
                this.Col_AllowableAmount.AllowDBNull = false;
                this.Col_TotalWriteoffAmount = base.Columns.Add("TotalWriteoffAmount", typeof(double));
                this.Col_TotalWriteoffAmount.AllowDBNull = false;
                this.Col_TotalPaymentAmount = base.Columns.Add("TotalPaymentAmount", typeof(double));
                this.Col_TotalPaymentAmount.AllowDBNull = false;
                this.Col_Quantity = base.Columns.Add("Quantity", typeof(double));
                this.Col_Quantity.AllowDBNull = false;
                this.Col_Percent = base.Columns.Add("Percent", typeof(double));
                this.Col_Percent.AllowDBNull = false;
                this.Col_Basis = base.Columns.Add("Basis", typeof(string));
                this.Col_Basis.AllowDBNull = false;
                this.Col_PaymentExists = base.Columns.Add("PaymentExists", typeof(bool));
                this.Col_PaymentExists.AllowDBNull = false;
                this.Col_CurrentPayer = base.Columns.Add("CurrentPayer", typeof(string));
                this.Col_CurrentPayer.AllowDBNull = false;
                this.Col_Balance = base.Columns.Add("Balance", typeof(double));
                this.Col_Balance.AllowDBNull = true;
                this.Col_ExpectedPaidAmount = base.Columns.Add("ExpectedPaidAmount", typeof(double));
                this.Col_EnteredAllowableAmount = base.Columns.Add("EnteredAllowableAmount", typeof(double));
                this.Col_EnteredPaidAmount = base.Columns.Add("EnteredPaidAmount", typeof(double));
                this.Col_EnteredDeductibleAmount = base.Columns.Add("EnteredDeductibleAmount", typeof(double));
                this.Col_EnteredCoinsAmount = base.Columns.Add("EnteredCoinsAmount", typeof(double));
                this.Col_PostingPaidAmount = base.Columns.Add("PostingPaidAmount", typeof(double));
                this.Col_ExpectedPaidAmount.Expression = "IIF([CurrentPayer] = 'Ins1' AND [PaymentExists] = 0,    0.01 * [Percent] * IIF([Basis] = 'Bill', [BillableAmount], ISNULL([EnteredAllowableAmount], [AllowableAmount])) - ISNULL([EnteredDeductibleAmount], 0),    IIF(0.01 <= [TotalPaymentAmount],        [BillableAmount] - [TotalWriteoffAmount] - [TotalPaymentAmount],        (1 - 0.01 * [Percent]) * IIF([Basis] = 'Bill', [BillableAmount], [AllowableAmount])       )   )";
                this.Col_PostingPaidAmount.Expression = "ISNULL([EnteredPaidAmount], [ExpectedPaidAmount])";
            }

            protected override void OnColumnChanged(DataColumnChangeEventArgs e)
            {
                if (ReferenceEquals(e.Column, this.Col_EnteredAllowableAmount) || (ReferenceEquals(e.Column, this.Col_EnteredPaidAmount) || (ReferenceEquals(e.Column, this.Col_EnteredCoinsAmount) || ReferenceEquals(e.Column, this.Col_EnteredDeductibleAmount))))
                {
                    e.Row.BeginEdit();
                    e.Row.EndEdit();
                }
                base.OnColumnChanged(e);
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

