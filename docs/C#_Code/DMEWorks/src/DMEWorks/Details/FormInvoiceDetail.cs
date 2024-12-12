namespace DMEWorks.Details
{
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormInvoiceDetail : FormDetails
    {
        private IContainer components;
        private bool FApproved;

        public FormInvoiceDetail() : this(null)
        {
        }

        public FormInvoiceDetail(ControlInvoiceDetails Parent) : base(Parent)
        {
            this.FApproved = false;
            this.InitializeComponent();
            if (Parent != null)
            {
                Parent.CustomerIDChanged += new EventHandler(this.Parent_CustomerIDChanged);
                Parent.OrderIDChanged += new EventHandler(this.Parent_OrderIDChanged);
            }
        }

        private void btnFindHao_Click(object sender, EventArgs e)
        {
            if (this.fdHAO.ShowDialog() == DialogResult.OK)
            {
                Functions.SetTextBoxText(this.txtHaoDescription, this.fdHAO.SelectedRow["Description"]);
            }
        }

        private void btnRecalculateBalance_Click(object sender, EventArgs e)
        {
            this.CalculateBalance();
        }

        private void CalculateBalance()
        {
            this.nmbBalance.AsDouble = new double?(this.ControlInvoiceTransactions21.Balance);
        }

        protected override void Clear()
        {
            Functions.SetTextBoxText(this.txtInventoryItem, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chkMedicallyUnnecessary, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chkSendCMN_RX_w_invoice, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns1, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns2, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns3, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns4, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbNopayIns1, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbHardship, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbBillingMonth, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDOSFrom, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDOSTo, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbInvoiceDate, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbShowSpanDates, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbAllowableAmount, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbBillableAmount, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbBalance, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbQuantity, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbTaxes, DBNull.Value);
            Functions.SetTextBoxText(this.txtReviewCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtSpecialCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtBillingCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier1, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier2, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier3, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier4, DBNull.Value);
            Functions.SetTextBoxText(this.txtDXPointer9, DBNull.Value);
            Functions.SetTextBoxText(this.txtDXPointer10, DBNull.Value);
            Functions.SetTextBoxText(this.txtDrugNoteField, DBNull.Value);
            Functions.SetTextBoxText(this.txtDrugControlNumber, DBNull.Value);
            this.ControlCmnID1.DefaultCmnType = null;
            this.ControlCmnID1.CustomerID = this.CustomerID;
            this.ControlCmnID1.OrderID = this.OrderID;
            this.ControlCmnID1.CmnID = null;
            Functions.SetTextBoxText(this.txtHaoDescription, DBNull.Value);
            Functions.SetTextBoxText(this.txtAuthorizationNumber, DBNull.Value);
            this.cmbAuthorizationType.SelectedValue = DBNull.Value;
            this.lblReturned.Visible = false;
            this.ControlInvoiceNotes.ClearGrid();
            this.ControlInvoiceTransactions21.ClearGrid();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormInvoiceDetail));
            this.chkMedicallyUnnecessary = new CheckBox();
            this.txtReviewCode = new TextBox();
            this.lblReviewCode = new Label();
            this.txtSpecialCode = new TextBox();
            this.lblSpecialCode = new Label();
            this.chkSendCMN_RX_w_invoice = new CheckBox();
            this.dtbInvoiceDate = new UltraDateTimeEditor();
            this.lblInvoiceDate = new Label();
            this.lblBillingMonth = new Label();
            this.lblDXPointer9 = new Label();
            this.txtModifier2 = new TextBox();
            this.txtModifier3 = new TextBox();
            this.txtModifier4 = new TextBox();
            this.txtModifier1 = new TextBox();
            this.lblModifiers = new Label();
            this.txtBillingCode = new TextBox();
            this.lblBillingCode = new Label();
            this.lblQuantity = new Label();
            this.nmbTaxes = new NumericBox();
            this.lblTaxes = new Label();
            this.nmbBalance = new NumericBox();
            this.lblBalance = new Label();
            this.nmbQuantity = new NumericBox();
            this.lblInventoryItem = new Label();
            this.txtHaoDescription = new TextBox();
            this.lblHaoDescription = new Label();
            this.lblReturned = new Label();
            this.txtInventoryItem = new TextBox();
            this.PageControl = new TabControl();
            this.tpGeneral = new TabPage();
            this.btnFindHao = new Button();
            this.lblBilling = new Label();
            this.txtDXPointer10 = new TextBox();
            this.lblDXPointer10 = new Label();
            this.txtDXPointer9 = new TextBox();
            this.txtAuthorizationNumber = new TextBox();
            this.lblAuthorizationNumber = new Label();
            this.lblAuthorizationType = new Label();
            this.cmbAuthorizationType = new Combobox();
            this.chbNopayIns1 = new CheckBox();
            this.gbDrugIdentification = new GroupBox();
            this.txtDrugControlNumber = new TextBox();
            this.txtDrugNoteField = new TextBox();
            this.lblDrugControlNumber = new Label();
            this.lblDrugNoteField = new Label();
            this.ControlCmnID1 = new ControlCmnID();
            this.chbHardship = new CheckBox();
            this.chbBillIns4 = new CheckBox();
            this.chbBillIns3 = new CheckBox();
            this.chbBillIns2 = new CheckBox();
            this.chbBillIns1 = new CheckBox();
            this.nmbAllowableAmount = new NumericBox();
            this.lblAllowableAmount = new Label();
            this.nmbBillableAmount = new NumericBox();
            this.lblBillableAmount = new Label();
            this.gbDOS = new GroupBox();
            this.chbShowSpanDates = new CheckBox();
            this.Label1 = new Label();
            this.lblDOSFrom = new Label();
            this.dtbDOSTo = new UltraDateTimeEditor();
            this.dtbDOSFrom = new UltraDateTimeEditor();
            this.nmbBillingMonth = new NumericBox();
            this.tpNotes = new TabPage();
            this.ControlInvoiceNotes = new DMEWorks.Details.ControlInvoiceNotes();
            this.tpInvoiceTransactions = new TabPage();
            this.ControlInvoiceTransactions21 = new ControlInvoiceTransactions();
            this.pnlTop = new Panel();
            this.btnRecalculateBalance = new Button();
            this.pnlBottom = new Panel();
            this.fdHAO = new FindDialog();
            this.ToolTip1 = new ToolTip(this.components);
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            ((ISupportInitialize) this.ValidationWarnings).BeginInit();
            this.PageControl.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.gbDrugIdentification.SuspendLayout();
            this.gbDOS.SuspendLayout();
            this.tpNotes.SuspendLayout();
            this.tpInvoiceTransactions.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.Location = new Point(0x1f8, 0);
            this.btnCancel.TabIndex = 2;
            this.btnOK.Location = new Point(0x1a8, 0);
            this.btnOK.TabIndex = 1;
            this.chkMedicallyUnnecessary.Location = new Point(0x100, 0x110);
            this.chkMedicallyUnnecessary.Name = "chkMedicallyUnnecessary";
            this.chkMedicallyUnnecessary.Size = new Size(0xb8, 0x15);
            this.chkMedicallyUnnecessary.TabIndex = 0x29;
            this.chkMedicallyUnnecessary.Text = "Medically Unecessary";
            this.txtReviewCode.Location = new Point(0x68, 0x158);
            this.txtReviewCode.Name = "txtReviewCode";
            this.txtReviewCode.Size = new Size(0x80, 20);
            this.txtReviewCode.TabIndex = 0x26;
            this.lblReviewCode.Location = new Point(8, 0x158);
            this.lblReviewCode.Name = "lblReviewCode";
            this.lblReviewCode.Size = new Size(0x58, 0x15);
            this.lblReviewCode.TabIndex = 0x25;
            this.lblReviewCode.Text = "Review code";
            this.lblReviewCode.TextAlign = ContentAlignment.MiddleRight;
            this.txtSpecialCode.Location = new Point(0x68, 320);
            this.txtSpecialCode.Name = "txtSpecialCode";
            this.txtSpecialCode.Size = new Size(0x80, 20);
            this.txtSpecialCode.TabIndex = 0x24;
            this.lblSpecialCode.Location = new Point(8, 320);
            this.lblSpecialCode.Name = "lblSpecialCode";
            this.lblSpecialCode.Size = new Size(0x58, 0x15);
            this.lblSpecialCode.TabIndex = 0x23;
            this.lblSpecialCode.Text = "Special code";
            this.lblSpecialCode.TextAlign = ContentAlignment.MiddleRight;
            this.chkSendCMN_RX_w_invoice.Location = new Point(0x100, 0xf8);
            this.chkSendCMN_RX_w_invoice.Name = "chkSendCMN_RX_w_invoice";
            this.chkSendCMN_RX_w_invoice.Size = new Size(0xb8, 0x15);
            this.chkSendCMN_RX_w_invoice.TabIndex = 40;
            this.chkSendCMN_RX_w_invoice.Text = "Send CMN/RX w/this invoice";
            this.dtbInvoiceDate.Location = new Point(0x1e4, 8);
            this.dtbInvoiceDate.Name = "dtbInvoiceDate";
            this.dtbInvoiceDate.Size = new Size(0x5c, 0x15);
            this.dtbInvoiceDate.TabIndex = 0x13;
            this.lblInvoiceDate.Location = new Point(0x198, 8);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new Size(0x48, 0x15);
            this.lblInvoiceDate.TabIndex = 0x12;
            this.lblInvoiceDate.Text = "Invoice Date";
            this.lblInvoiceDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblBillingMonth.Location = new Point(8, 0x58);
            this.lblBillingMonth.Name = "lblBillingMonth";
            this.lblBillingMonth.Size = new Size(80, 0x15);
            this.lblBillingMonth.TabIndex = 10;
            this.lblBillingMonth.Text = "Billing Month";
            this.lblBillingMonth.TextAlign = ContentAlignment.MiddleRight;
            this.lblDXPointer9.Location = new Point(8, 0xe0);
            this.lblDXPointer9.Name = "lblDXPointer9";
            this.lblDXPointer9.Size = new Size(0x58, 0x15);
            this.lblDXPointer9.TabIndex = 0x1b;
            this.lblDXPointer9.Text = "DX Pointer 9";
            this.lblDXPointer9.TextAlign = ContentAlignment.MiddleRight;
            this.txtModifier2.Location = new Point(0x88, 0x20);
            this.txtModifier2.MaxLength = 8;
            this.txtModifier2.Name = "txtModifier2";
            this.txtModifier2.Size = new Size(0x1c, 20);
            this.txtModifier2.TabIndex = 4;
            this.txtModifier3.Location = new Point(0xa8, 0x20);
            this.txtModifier3.MaxLength = 8;
            this.txtModifier3.Name = "txtModifier3";
            this.txtModifier3.Size = new Size(0x1c, 20);
            this.txtModifier3.TabIndex = 5;
            this.txtModifier4.Location = new Point(200, 0x20);
            this.txtModifier4.MaxLength = 8;
            this.txtModifier4.Name = "txtModifier4";
            this.txtModifier4.Size = new Size(0x1c, 20);
            this.txtModifier4.TabIndex = 6;
            this.txtModifier1.Location = new Point(0x68, 0x20);
            this.txtModifier1.MaxLength = 8;
            this.txtModifier1.Name = "txtModifier1";
            this.txtModifier1.Size = new Size(0x1c, 20);
            this.txtModifier1.TabIndex = 3;
            this.lblModifiers.Location = new Point(8, 0x20);
            this.lblModifiers.Name = "lblModifiers";
            this.lblModifiers.Size = new Size(0x58, 0x15);
            this.lblModifiers.TabIndex = 2;
            this.lblModifiers.Text = "Modifiers";
            this.lblModifiers.TextAlign = ContentAlignment.MiddleRight;
            this.txtBillingCode.Location = new Point(0x68, 8);
            this.txtBillingCode.Name = "txtBillingCode";
            this.txtBillingCode.Size = new Size(0x80, 20);
            this.txtBillingCode.TabIndex = 1;
            this.lblBillingCode.Location = new Point(8, 8);
            this.lblBillingCode.Name = "lblBillingCode";
            this.lblBillingCode.Size = new Size(0x58, 0x15);
            this.lblBillingCode.TabIndex = 0;
            this.lblBillingCode.Text = "Billing Code";
            this.lblBillingCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblQuantity.Location = new Point(8, 0x98);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new Size(0x58, 0x15);
            this.lblQuantity.TabIndex = 14;
            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.TextAlign = ContentAlignment.MiddleRight;
            this.nmbTaxes.Location = new Point(0x68, 0xb0);
            this.nmbTaxes.Name = "nmbTaxes";
            this.nmbTaxes.Size = new Size(0x58, 0x15);
            this.nmbTaxes.TabIndex = 0x11;
            this.lblTaxes.Location = new Point(8, 0xb0);
            this.lblTaxes.Name = "lblTaxes";
            this.lblTaxes.Size = new Size(0x58, 0x15);
            this.lblTaxes.TabIndex = 0x10;
            this.lblTaxes.Text = "Taxes";
            this.lblTaxes.TextAlign = ContentAlignment.MiddleRight;
            this.nmbBalance.BorderStyle = BorderStyle.FixedSingle;
            this.nmbBalance.Location = new Point(0x1d8, 8);
            this.nmbBalance.Name = "nmbBalance";
            this.nmbBalance.Size = new Size(0x48, 0x15);
            this.nmbBalance.TabIndex = 3;
            this.lblBalance.Location = new Point(0x1a0, 8);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new Size(0x30, 0x15);
            this.lblBalance.TabIndex = 2;
            this.lblBalance.Text = "Balance";
            this.lblBalance.TextAlign = ContentAlignment.MiddleRight;
            this.nmbQuantity.Location = new Point(0x68, 0x98);
            this.nmbQuantity.Name = "nmbQuantity";
            this.nmbQuantity.Size = new Size(0x58, 0x15);
            this.nmbQuantity.TabIndex = 15;
            this.lblInventoryItem.Location = new Point(8, 8);
            this.lblInventoryItem.Name = "lblInventoryItem";
            this.lblInventoryItem.Size = new Size(80, 0x15);
            this.lblInventoryItem.TabIndex = 0;
            this.lblInventoryItem.Text = "Inventory Item";
            this.lblInventoryItem.TextAlign = ContentAlignment.MiddleRight;
            this.txtHaoDescription.Location = new Point(0x68, 0x38);
            this.txtHaoDescription.MaxLength = 100;
            this.txtHaoDescription.Multiline = true;
            this.txtHaoDescription.Name = "txtHaoDescription";
            this.txtHaoDescription.Size = new Size(0x128, 0x2c);
            this.txtHaoDescription.TabIndex = 8;
            this.lblHaoDescription.Location = new Point(8, 0x38);
            this.lblHaoDescription.Name = "lblHaoDescription";
            this.lblHaoDescription.Size = new Size(0x58, 0x15);
            this.lblHaoDescription.TabIndex = 7;
            this.lblHaoDescription.Text = "HAO";
            this.lblHaoDescription.TextAlign = ContentAlignment.MiddleRight;
            this.lblReturned.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.lblReturned.ForeColor = Color.Red;
            this.lblReturned.Location = new Point(0x128, 0);
            this.lblReturned.Name = "lblReturned";
            this.lblReturned.Size = new Size(100, 0x18);
            this.lblReturned.TabIndex = 0;
            this.lblReturned.Text = "Returned";
            this.lblReturned.TextAlign = ContentAlignment.MiddleCenter;
            this.txtInventoryItem.Location = new Point(0x60, 8);
            this.txtInventoryItem.Name = "txtInventoryItem";
            this.txtInventoryItem.ReadOnly = true;
            this.txtInventoryItem.Size = new Size(0x138, 20);
            this.txtInventoryItem.TabIndex = 1;
            this.PageControl.Alignment = TabAlignment.Bottom;
            this.PageControl.Controls.Add(this.tpGeneral);
            this.PageControl.Controls.Add(this.tpNotes);
            this.PageControl.Controls.Add(this.tpInvoiceTransactions);
            this.PageControl.Dock = DockStyle.Fill;
            this.PageControl.Location = new Point(0, 0x20);
            this.PageControl.Name = "PageControl";
            this.PageControl.SelectedIndex = 0;
            this.PageControl.Size = new Size(0x250, 0x1c5);
            this.PageControl.TabIndex = 1;
            this.tpGeneral.Controls.Add(this.btnFindHao);
            this.tpGeneral.Controls.Add(this.lblBilling);
            this.tpGeneral.Controls.Add(this.txtDXPointer10);
            this.tpGeneral.Controls.Add(this.lblDXPointer10);
            this.tpGeneral.Controls.Add(this.txtDXPointer9);
            this.tpGeneral.Controls.Add(this.txtAuthorizationNumber);
            this.tpGeneral.Controls.Add(this.lblAuthorizationNumber);
            this.tpGeneral.Controls.Add(this.lblAuthorizationType);
            this.tpGeneral.Controls.Add(this.cmbAuthorizationType);
            this.tpGeneral.Controls.Add(this.chbNopayIns1);
            this.tpGeneral.Controls.Add(this.gbDrugIdentification);
            this.tpGeneral.Controls.Add(this.ControlCmnID1);
            this.tpGeneral.Controls.Add(this.chbHardship);
            this.tpGeneral.Controls.Add(this.chbBillIns4);
            this.tpGeneral.Controls.Add(this.chbBillIns3);
            this.tpGeneral.Controls.Add(this.chbBillIns2);
            this.tpGeneral.Controls.Add(this.chbBillIns1);
            this.tpGeneral.Controls.Add(this.nmbAllowableAmount);
            this.tpGeneral.Controls.Add(this.lblAllowableAmount);
            this.tpGeneral.Controls.Add(this.nmbBillableAmount);
            this.tpGeneral.Controls.Add(this.lblBillableAmount);
            this.tpGeneral.Controls.Add(this.gbDOS);
            this.tpGeneral.Controls.Add(this.lblHaoDescription);
            this.tpGeneral.Controls.Add(this.txtHaoDescription);
            this.tpGeneral.Controls.Add(this.nmbQuantity);
            this.tpGeneral.Controls.Add(this.chkMedicallyUnnecessary);
            this.tpGeneral.Controls.Add(this.txtReviewCode);
            this.tpGeneral.Controls.Add(this.lblReviewCode);
            this.tpGeneral.Controls.Add(this.txtSpecialCode);
            this.tpGeneral.Controls.Add(this.lblSpecialCode);
            this.tpGeneral.Controls.Add(this.chkSendCMN_RX_w_invoice);
            this.tpGeneral.Controls.Add(this.dtbInvoiceDate);
            this.tpGeneral.Controls.Add(this.lblInvoiceDate);
            this.tpGeneral.Controls.Add(this.lblDXPointer9);
            this.tpGeneral.Controls.Add(this.txtModifier2);
            this.tpGeneral.Controls.Add(this.txtModifier3);
            this.tpGeneral.Controls.Add(this.txtModifier4);
            this.tpGeneral.Controls.Add(this.txtModifier1);
            this.tpGeneral.Controls.Add(this.lblModifiers);
            this.tpGeneral.Controls.Add(this.txtBillingCode);
            this.tpGeneral.Controls.Add(this.lblBillingCode);
            this.tpGeneral.Controls.Add(this.lblQuantity);
            this.tpGeneral.Controls.Add(this.nmbTaxes);
            this.tpGeneral.Controls.Add(this.lblTaxes);
            this.tpGeneral.Location = new Point(4, 4);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new Size(0x248, 0x1ab);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.btnFindHao.FlatStyle = FlatStyle.Flat;
            this.btnFindHao.Image = My.Resources.Resources.ImageSpyglass2;
            this.btnFindHao.Location = new Point(0x48, 0x4f);
            this.btnFindHao.Name = "btnFindHao";
            this.btnFindHao.Size = new Size(0x15, 0x15);
            this.btnFindHao.TabIndex = 9;
            this.btnFindHao.TabStop = false;
            this.lblBilling.Location = new Point(8, 200);
            this.lblBilling.Name = "lblBilling";
            this.lblBilling.Size = new Size(0x58, 0x15);
            this.lblBilling.TabIndex = 0x15;
            this.lblBilling.Text = "Billing";
            this.lblBilling.TextAlign = ContentAlignment.MiddleRight;
            this.txtDXPointer10.Location = new Point(0x68, 0xf8);
            this.txtDXPointer10.MaxLength = 8;
            this.txtDXPointer10.Name = "txtDXPointer10";
            this.txtDXPointer10.Size = new Size(0x80, 20);
            this.txtDXPointer10.TabIndex = 30;
            this.lblDXPointer10.Location = new Point(8, 0xf8);
            this.lblDXPointer10.Name = "lblDXPointer10";
            this.lblDXPointer10.Size = new Size(0x58, 0x15);
            this.lblDXPointer10.TabIndex = 0x1d;
            this.lblDXPointer10.Text = "DX Pointer 10";
            this.lblDXPointer10.TextAlign = ContentAlignment.MiddleRight;
            this.txtDXPointer9.Location = new Point(0x68, 0xe0);
            this.txtDXPointer9.MaxLength = 8;
            this.txtDXPointer9.Name = "txtDXPointer9";
            this.txtDXPointer9.Size = new Size(0x80, 20);
            this.txtDXPointer9.TabIndex = 0x1c;
            this.txtAuthorizationNumber.AcceptsReturn = true;
            this.txtAuthorizationNumber.BackColor = SystemColors.Window;
            this.txtAuthorizationNumber.ForeColor = SystemColors.WindowText;
            this.txtAuthorizationNumber.Location = new Point(0x68, 0x128);
            this.txtAuthorizationNumber.MaxLength = 0;
            this.txtAuthorizationNumber.Name = "txtAuthorizationNumber";
            this.txtAuthorizationNumber.RightToLeft = RightToLeft.No;
            this.txtAuthorizationNumber.Size = new Size(0x80, 20);
            this.txtAuthorizationNumber.TabIndex = 0x22;
            this.lblAuthorizationNumber.Location = new Point(8, 0x128);
            this.lblAuthorizationNumber.Name = "lblAuthorizationNumber";
            this.lblAuthorizationNumber.Size = new Size(0x58, 0x15);
            this.lblAuthorizationNumber.TabIndex = 0x21;
            this.lblAuthorizationNumber.Text = "Prior Auth #";
            this.lblAuthorizationNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblAuthorizationType.Location = new Point(8, 0x110);
            this.lblAuthorizationType.Name = "lblAuthorizationType";
            this.lblAuthorizationType.Size = new Size(0x58, 0x15);
            this.lblAuthorizationType.TabIndex = 0x1f;
            this.lblAuthorizationType.Text = "Prior Auth Type";
            this.lblAuthorizationType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbAuthorizationType.EditButton = false;
            this.cmbAuthorizationType.FindButton = false;
            this.cmbAuthorizationType.Location = new Point(0x68, 0x110);
            this.cmbAuthorizationType.Name = "cmbAuthorizationType";
            this.cmbAuthorizationType.NewButton = false;
            this.cmbAuthorizationType.Size = new Size(0x80, 0x15);
            this.cmbAuthorizationType.TabIndex = 0x20;
            this.chbNopayIns1.Location = new Point(0x148, 200);
            this.chbNopayIns1.Name = "chbNopayIns1";
            this.chbNopayIns1.Size = new Size(0x68, 0x15);
            this.chbNopayIns1.TabIndex = 0x1a;
            this.chbNopayIns1.Text = "No Pay Ins 1";
            this.gbDrugIdentification.Controls.Add(this.txtDrugControlNumber);
            this.gbDrugIdentification.Controls.Add(this.txtDrugNoteField);
            this.gbDrugIdentification.Controls.Add(this.lblDrugControlNumber);
            this.gbDrugIdentification.Controls.Add(this.lblDrugNoteField);
            this.gbDrugIdentification.Location = new Point(8, 0x178);
            this.gbDrugIdentification.Name = "gbDrugIdentification";
            this.gbDrugIdentification.Size = new Size(0x238, 0x2c);
            this.gbDrugIdentification.TabIndex = 0x2b;
            this.gbDrugIdentification.TabStop = false;
            this.gbDrugIdentification.Text = "Drug Identification";
            this.txtDrugControlNumber.Location = new Point(360, 0x10);
            this.txtDrugControlNumber.Name = "txtDrugControlNumber";
            this.txtDrugControlNumber.Size = new Size(200, 20);
            this.txtDrugControlNumber.TabIndex = 3;
            this.txtDrugNoteField.Location = new Point(0x60, 0x10);
            this.txtDrugNoteField.Name = "txtDrugNoteField";
            this.txtDrugNoteField.Size = new Size(0xb0, 20);
            this.txtDrugNoteField.TabIndex = 1;
            this.lblDrugControlNumber.Location = new Point(0x120, 0x10);
            this.lblDrugControlNumber.Name = "lblDrugControlNumber";
            this.lblDrugControlNumber.Size = new Size(0x40, 0x15);
            this.lblDrugControlNumber.TabIndex = 2;
            this.lblDrugControlNumber.Text = "Control #";
            this.lblDrugControlNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblDrugNoteField.Location = new Point(8, 0x10);
            this.lblDrugNoteField.Name = "lblDrugNoteField";
            this.lblDrugNoteField.Size = new Size(80, 0x15);
            this.lblDrugNoteField.TabIndex = 0;
            this.lblDrugNoteField.Text = "Note Field";
            this.lblDrugNoteField.TextAlign = ContentAlignment.MiddleRight;
            this.ControlCmnID1.Location = new Point(0x100, 0xe0);
            this.ControlCmnID1.Margin = new Padding(0);
            this.ControlCmnID1.Name = "ControlCmnID1";
            this.ControlCmnID1.Size = new Size(320, 0x17);
            this.ControlCmnID1.TabIndex = 0x27;
            this.chbHardship.Enabled = false;
            this.chbHardship.Location = new Point(0x100, 0x128);
            this.chbHardship.Name = "chbHardship";
            this.chbHardship.Size = new Size(0xb8, 0x15);
            this.chbHardship.TabIndex = 0x2a;
            this.chbHardship.Text = "Hardship";
            this.chbBillIns4.Location = new Point(0x110, 200);
            this.chbBillIns4.Name = "chbBillIns4";
            this.chbBillIns4.Size = new Size(0x38, 0x15);
            this.chbBillIns4.TabIndex = 0x19;
            this.chbBillIns4.Text = "Ins 4";
            this.chbBillIns3.Location = new Point(0xd8, 200);
            this.chbBillIns3.Name = "chbBillIns3";
            this.chbBillIns3.Size = new Size(0x38, 0x15);
            this.chbBillIns3.TabIndex = 0x18;
            this.chbBillIns3.Text = "Ins 3";
            this.chbBillIns2.Location = new Point(160, 200);
            this.chbBillIns2.Name = "chbBillIns2";
            this.chbBillIns2.Size = new Size(0x38, 0x15);
            this.chbBillIns2.TabIndex = 0x17;
            this.chbBillIns2.Text = "Ins 2";
            this.chbBillIns1.Location = new Point(0x68, 200);
            this.chbBillIns1.Name = "chbBillIns1";
            this.chbBillIns1.Size = new Size(0x38, 0x15);
            this.chbBillIns1.TabIndex = 0x16;
            this.chbBillIns1.Text = "Ins 1";
            this.nmbAllowableAmount.Location = new Point(0x68, 0x80);
            this.nmbAllowableAmount.Name = "nmbAllowableAmount";
            this.nmbAllowableAmount.Size = new Size(0x58, 0x15);
            this.nmbAllowableAmount.TabIndex = 13;
            this.lblAllowableAmount.Location = new Point(8, 0x80);
            this.lblAllowableAmount.Name = "lblAllowableAmount";
            this.lblAllowableAmount.Size = new Size(0x58, 0x15);
            this.lblAllowableAmount.TabIndex = 12;
            this.lblAllowableAmount.Text = "Allowed Amount";
            this.lblAllowableAmount.TextAlign = ContentAlignment.MiddleRight;
            this.nmbBillableAmount.Location = new Point(0x68, 0x68);
            this.nmbBillableAmount.Name = "nmbBillableAmount";
            this.nmbBillableAmount.Size = new Size(0x58, 0x15);
            this.nmbBillableAmount.TabIndex = 11;
            this.lblBillableAmount.Location = new Point(8, 0x68);
            this.lblBillableAmount.Name = "lblBillableAmount";
            this.lblBillableAmount.Size = new Size(0x58, 0x15);
            this.lblBillableAmount.TabIndex = 10;
            this.lblBillableAmount.Text = "Billable Amount";
            this.lblBillableAmount.TextAlign = ContentAlignment.MiddleRight;
            this.gbDOS.Controls.Add(this.chbShowSpanDates);
            this.gbDOS.Controls.Add(this.Label1);
            this.gbDOS.Controls.Add(this.lblDOSFrom);
            this.gbDOS.Controls.Add(this.dtbDOSTo);
            this.gbDOS.Controls.Add(this.dtbDOSFrom);
            this.gbDOS.Controls.Add(this.lblBillingMonth);
            this.gbDOS.Controls.Add(this.nmbBillingMonth);
            this.gbDOS.Location = new Point(0x198, 40);
            this.gbDOS.Name = "gbDOS";
            this.gbDOS.Size = new Size(0xa4, 120);
            this.gbDOS.TabIndex = 20;
            this.gbDOS.TabStop = false;
            this.gbDOS.Text = "Date Of Service";
            this.chbShowSpanDates.BackColor = Color.Transparent;
            this.chbShowSpanDates.ForeColor = SystemColors.ControlText;
            this.chbShowSpanDates.Location = new Point(11, 0x40);
            this.chbShowSpanDates.Name = "chbShowSpanDates";
            this.chbShowSpanDates.RightToLeft = RightToLeft.No;
            this.chbShowSpanDates.Size = new Size(0x8d, 0x15);
            this.chbShowSpanDates.TabIndex = 4;
            this.chbShowSpanDates.Text = "Show Span Dates";
            this.chbShowSpanDates.UseVisualStyleBackColor = false;
            this.Label1.BackColor = Color.Transparent;
            this.Label1.ForeColor = SystemColors.ControlText;
            this.Label1.Location = new Point(8, 40);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = RightToLeft.No;
            this.Label1.Size = new Size(40, 0x15);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "To";
            this.Label1.TextAlign = ContentAlignment.MiddleRight;
            this.lblDOSFrom.BackColor = Color.Transparent;
            this.lblDOSFrom.ForeColor = SystemColors.ControlText;
            this.lblDOSFrom.Location = new Point(8, 0x10);
            this.lblDOSFrom.Name = "lblDOSFrom";
            this.lblDOSFrom.RightToLeft = RightToLeft.No;
            this.lblDOSFrom.Size = new Size(40, 0x15);
            this.lblDOSFrom.TabIndex = 0;
            this.lblDOSFrom.Text = "From";
            this.lblDOSFrom.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDOSTo.Location = new Point(0x38, 40);
            this.dtbDOSTo.Name = "dtbDOSTo";
            this.dtbDOSTo.Size = new Size(100, 0x15);
            this.dtbDOSTo.TabIndex = 3;
            this.dtbDOSFrom.Location = new Point(0x38, 0x10);
            this.dtbDOSFrom.Name = "dtbDOSFrom";
            this.dtbDOSFrom.Size = new Size(100, 0x15);
            this.dtbDOSFrom.TabIndex = 1;
            this.nmbBillingMonth.Location = new Point(100, 0x58);
            this.nmbBillingMonth.Name = "nmbBillingMonth";
            this.nmbBillingMonth.Size = new Size(0x38, 20);
            this.nmbBillingMonth.TabIndex = 11;
            this.tpNotes.Controls.Add(this.ControlInvoiceNotes);
            this.tpNotes.Location = new Point(4, 4);
            this.tpNotes.Name = "tpNotes";
            this.tpNotes.Padding = new Padding(0, 0, 0, 3);
            this.tpNotes.Size = new Size(0x248, 0x1ab);
            this.tpNotes.TabIndex = 2;
            this.tpNotes.Text = "Notes";
            this.ControlInvoiceNotes.Dock = DockStyle.Fill;
            this.ControlInvoiceNotes.Location = new Point(0, 0);
            this.ControlInvoiceNotes.Name = "ControlInvoiceNotes";
            this.ControlInvoiceNotes.Size = new Size(0x248, 0x1a8);
            this.ControlInvoiceNotes.TabIndex = 0;
            this.tpInvoiceTransactions.Controls.Add(this.ControlInvoiceTransactions21);
            this.tpInvoiceTransactions.Location = new Point(4, 4);
            this.tpInvoiceTransactions.Name = "tpInvoiceTransactions";
            this.tpInvoiceTransactions.Padding = new Padding(3);
            this.tpInvoiceTransactions.Size = new Size(0x248, 0x1ab);
            this.tpInvoiceTransactions.TabIndex = 5;
            this.tpInvoiceTransactions.Text = "Transactions";
            this.tpInvoiceTransactions.UseVisualStyleBackColor = true;
            this.ControlInvoiceTransactions21.Dock = DockStyle.Fill;
            this.ControlInvoiceTransactions21.Location = new Point(3, 3);
            this.ControlInvoiceTransactions21.Name = "ControlInvoiceTransactions21";
            this.ControlInvoiceTransactions21.Size = new Size(0x242, 0x1a5);
            this.ControlInvoiceTransactions21.TabIndex = 0;
            this.pnlTop.Controls.Add(this.btnRecalculateBalance);
            this.pnlTop.Controls.Add(this.txtInventoryItem);
            this.pnlTop.Controls.Add(this.lblInventoryItem);
            this.pnlTop.Controls.Add(this.lblBalance);
            this.pnlTop.Controls.Add(this.nmbBalance);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new Size(0x250, 0x20);
            this.pnlTop.TabIndex = 0;
            this.btnRecalculateBalance.FlatStyle = FlatStyle.Flat;
            this.btnRecalculateBalance.Image = (Image) manager.GetObject("btnRecalculateBalance.Image");
            this.btnRecalculateBalance.Location = new Point(0x220, 8);
            this.btnRecalculateBalance.Name = "btnRecalculateBalance";
            this.btnRecalculateBalance.Size = new Size(0x15, 0x15);
            this.btnRecalculateBalance.TabIndex = 4;
            this.btnRecalculateBalance.TabStop = false;
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.lblReturned);
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Location = new Point(0, 0x1e5);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new Size(0x250, 0x20);
            this.pnlBottom.TabIndex = 2;
            this.pnlBottom.Controls.SetChildIndex(this.lblReturned, 0);
            this.pnlBottom.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlBottom.Controls.SetChildIndex(this.btnOK, 0);
            base.AcceptButton = null;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = null;
            base.ClientSize = new Size(0x250, 0x205);
            base.Controls.Add(this.PageControl);
            base.Controls.Add(this.pnlTop);
            base.Controls.Add(this.pnlBottom);
            base.Name = "FormInvoiceDetail";
            this.Text = "Invoice Detail";
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            ((ISupportInitialize) this.ValidationWarnings).EndInit();
            this.PageControl.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.gbDrugIdentification.ResumeLayout(false);
            this.gbDrugIdentification.PerformLayout();
            this.gbDOS.ResumeLayout(false);
            this.tpNotes.ResumeLayout(false);
            this.tpInvoiceTransactions.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public override void LoadComboBoxes()
        {
            Cache.InitDialog(this.fdHAO, "tbl_hao", null);
            Cache.InitDropdown(this.cmbAuthorizationType, "tbl_authorizationtype", null);
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlInvoiceDetails.TableInvoiceDetails)
            {
                ControlInvoiceDetails.TableInvoiceDetails table = (ControlInvoiceDetails.TableInvoiceDetails) Row.Table;
                Functions.SetTextBoxText(this.txtInventoryItem, Row[table.Col_InventoryItem]);
                Functions.SetCheckBoxChecked(this.chkMedicallyUnnecessary, Row[table.Col_MedicallyUnnecessary]);
                Functions.SetCheckBoxChecked(this.chkSendCMN_RX_w_invoice, Row[table.Col_SendCMN_RX_w_invoice]);
                Functions.SetCheckBoxChecked(this.chbBillIns1, Row[table.Col_BillIns1]);
                Functions.SetCheckBoxChecked(this.chbBillIns2, Row[table.Col_BillIns2]);
                Functions.SetCheckBoxChecked(this.chbBillIns3, Row[table.Col_BillIns3]);
                Functions.SetCheckBoxChecked(this.chbBillIns4, Row[table.Col_BillIns4]);
                Functions.SetCheckBoxChecked(this.chbNopayIns1, Row[table.Col_NopayIns1]);
                Functions.SetCheckBoxChecked(this.chbHardship, Row[table.Col_Hardship]);
                Functions.SetNumericBoxValue(this.nmbBillingMonth, Row[table.Col_BillingMonth]);
                Functions.SetDateBoxValue(this.dtbDOSFrom, Row[table.Col_DOSFrom]);
                Functions.SetDateBoxValue(this.dtbDOSTo, Row[table.Col_DOSTo]);
                Functions.SetDateBoxValue(this.dtbInvoiceDate, Row[table.Col_InvoiceDate]);
                Functions.SetCheckBoxChecked(this.chbShowSpanDates, Row[table.Col_ShowSpanDates]);
                Functions.SetNumericBoxValue(this.nmbAllowableAmount, Row[table.Col_AllowableAmount]);
                Functions.SetNumericBoxValue(this.nmbBillableAmount, Row[table.Col_BillableAmount]);
                Functions.SetNumericBoxValue(this.nmbBalance, Row[table.Col_BALANCE]);
                Functions.SetNumericBoxValue(this.nmbQuantity, Row[table.Col_Quantity]);
                Functions.SetNumericBoxValue(this.nmbTaxes, Row[table.Col_Taxes]);
                Functions.SetTextBoxText(this.txtReviewCode, Row[table.Col_ReviewCode]);
                Functions.SetTextBoxText(this.txtSpecialCode, Row[table.Col_SpecialCode]);
                Functions.SetTextBoxText(this.txtBillingCode, Row[table.Col_BillingCode]);
                Functions.SetTextBoxText(this.txtModifier1, Row[table.Col_Modifier1]);
                Functions.SetTextBoxText(this.txtModifier2, Row[table.Col_Modifier2]);
                Functions.SetTextBoxText(this.txtModifier3, Row[table.Col_Modifier3]);
                Functions.SetTextBoxText(this.txtModifier4, Row[table.Col_Modifier4]);
                Functions.SetTextBoxText(this.txtDXPointer9, Row[table.Col_DXPointer9]);
                Functions.SetTextBoxText(this.txtDXPointer10, Row[table.Col_DXPointer10]);
                Functions.SetTextBoxText(this.txtDrugNoteField, Row[table.Col_DrugNoteField]);
                Functions.SetTextBoxText(this.txtDrugControlNumber, Row[table.Col_DrugControlNumber]);
                this.ControlCmnID1.DefaultCmnType = null;
                this.ControlCmnID1.CustomerID = this.CustomerID;
                this.ControlCmnID1.OrderID = this.OrderID;
                this.ControlCmnID1.CmnID = NullableConvert.ToInt32(Row[table.Col_CMNFormID]);
                Functions.SetTextBoxText(this.txtHaoDescription, Row[table.Col_HaoDescription]);
                Functions.SetTextBoxText(this.txtAuthorizationNumber, Row[table.Col_AuthorizationNumber]);
                this.cmbAuthorizationType.SelectedValue = Row[table.Col_AuthorizationTypeID];
                this.lblReturned.Visible = NullableConvert.ToBoolean(Row[table.Col_Returned], false);
                int? customerID = table.CustomerID;
                int? invoiceID = table.InvoiceID;
                int? nullable3 = NullableConvert.ToInt32(Row[table.Col_ID]);
                if ((customerID != null) && ((invoiceID != null) && (nullable3 != null)))
                {
                    this.ControlInvoiceNotes.LoadGrid(customerID.Value, invoiceID.Value, nullable3.Value);
                    this.ControlInvoiceTransactions21.LoadGrid(customerID.Value, invoiceID.Value, nullable3.Value);
                }
                else
                {
                    this.ControlInvoiceNotes.ClearGrid();
                    this.ControlInvoiceTransactions21.ClearGrid();
                }
            }
        }

        private void nmbQuantity_ValueChanged(object sender, EventArgs e)
        {
            this.nmbBalance.AsDouble = new double?(this.ControlInvoiceTransactions21.Balance);
        }

        protected virtual void Parent_CustomerIDChanged(object sender, EventArgs e)
        {
            this.ControlCmnID1.CustomerID = this.CustomerID;
        }

        protected virtual void Parent_OrderIDChanged(object sender, EventArgs e)
        {
            this.ControlCmnID1.OrderID = this.OrderID;
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlInvoiceDetails.TableInvoiceDetails)
            {
                ControlInvoiceDetails.TableInvoiceDetails table = (ControlInvoiceDetails.TableInvoiceDetails) Row.Table;
                Row[table.Col_MedicallyUnnecessary] = this.chkMedicallyUnnecessary.Checked;
                Row[table.Col_SendCMN_RX_w_invoice] = this.chkSendCMN_RX_w_invoice.Checked;
                Row[table.Col_BillIns1] = this.chbBillIns1.Checked;
                Row[table.Col_BillIns2] = this.chbBillIns2.Checked;
                Row[table.Col_BillIns3] = this.chbBillIns3.Checked;
                Row[table.Col_BillIns4] = this.chbBillIns4.Checked;
                Row[table.Col_NopayIns1] = this.chbNopayIns1.Checked;
                Row[table.Col_BillingMonth] = this.nmbBillingMonth.AsInt32.GetValueOrDefault(0);
                Row[table.Col_DOSFrom] = Functions.GetDateBoxValue(this.dtbDOSFrom);
                Row[table.Col_DOSTo] = Functions.GetDateBoxValue(this.dtbDOSTo);
                Row[table.Col_InvoiceDate] = Functions.GetDateBoxValue(this.dtbInvoiceDate);
                Row[table.Col_ShowSpanDates] = this.chbShowSpanDates.Checked;
                Row[table.Col_AllowableAmount] = this.nmbAllowableAmount.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_BillableAmount] = this.nmbBillableAmount.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_BALANCE] = this.nmbBalance.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_Quantity] = this.nmbQuantity.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_Taxes] = this.nmbTaxes.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_ReviewCode] = this.txtReviewCode.Text;
                Row[table.Col_SpecialCode] = this.txtSpecialCode.Text;
                Row[table.Col_BillingCode] = this.txtBillingCode.Text;
                Row[table.Col_Modifier1] = this.txtModifier1.Text;
                Row[table.Col_Modifier2] = this.txtModifier2.Text;
                Row[table.Col_Modifier3] = this.txtModifier3.Text;
                Row[table.Col_Modifier4] = this.txtModifier4.Text;
                Row[table.Col_DXPointer9] = this.txtDXPointer9.Text;
                Row[table.Col_DXPointer10] = this.txtDXPointer10.Text;
                Row[table.Col_DrugNoteField] = this.txtDrugNoteField.Text;
                Row[table.Col_DrugControlNumber] = this.txtDrugControlNumber.Text;
                Row[table.Col_CMNFormID] = ToDatabaseInt(this.ControlCmnID1.CmnID);
                Row[table.Col_HaoDescription] = this.txtHaoDescription.Text;
                Row[table.Col_AuthorizationNumber] = this.txtAuthorizationNumber.Text;
                Row[table.Col_AuthorizationTypeID] = ToDatabaseInt(NullableConvert.ToInt32(this.cmbAuthorizationType.SelectedValue));
                int? customerID = table.CustomerID;
                int? invoiceID = table.InvoiceID;
                int? nullable3 = NullableConvert.ToInt32(Row[table.Col_ID]);
                if ((customerID != null) && ((invoiceID != null) && (nullable3 != null)))
                {
                    this.ControlInvoiceNotes.SaveGrid(customerID.Value, invoiceID.Value, nullable3.Value);
                }
            }
        }

        private static object ToDatabaseInt(int? value) => 
            (value == null) ? ((object) DBNull.Value) : ((object) value.Value);

        private void txtHaoDescription_TextChanged(object sender, EventArgs e)
        {
            string str = Regex.Replace(this.txtHaoDescription.Text, @"\\s+", " ").Trim();
            this.ToolTip1.SetToolTip(this.txtHaoDescription, $"{str.Length} character(s) long");
        }

        protected override void ValidateObject()
        {
            if (base.Visible)
            {
                string str = Regex.Replace(this.txtHaoDescription.Text, @"\\s+", " ").Trim();
                if (80 < str.Length)
                {
                    this.ValidationWarnings.SetError(this.txtHaoDescription, $"Length of the HAO ({str.Length}) exceeds 80 characters. Exceeding characters will be truncated upon EDI transmission");
                }
                else
                {
                    this.ValidationWarnings.SetError(this.txtHaoDescription, "");
                }
            }
        }

        [field: AccessedThroughProperty("txtBillingCode")]
        private TextBox txtBillingCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxes")]
        private Label lblTaxes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtReviewCode")]
        private TextBox txtReviewCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSpecialCode")]
        private TextBox txtSpecialCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbInvoiceDate")]
        private UltraDateTimeEditor dtbInvoiceDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbTaxes")]
        private NumericBox nmbTaxes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBalance")]
        private NumericBox nmbBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbQuantity")]
        private NumericBox nmbQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier2")]
        private TextBox txtModifier2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier3")]
        private TextBox txtModifier3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier4")]
        private TextBox txtModifier4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier1")]
        private TextBox txtModifier1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chkMedicallyUnnecessary")]
        private CheckBox chkMedicallyUnnecessary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chkSendCMN_RX_w_invoice")]
        private CheckBox chkSendCMN_RX_w_invoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryItem")]
        private Label lblInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReviewCode")]
        private Label lblReviewCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSpecialCode")]
        private Label lblSpecialCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoiceDate")]
        private Label lblInvoiceDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillingMonth")]
        private Label lblBillingMonth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDXPointer9")]
        private Label lblDXPointer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblModifiers")]
        private Label lblModifiers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillingCode")]
        private Label lblBillingCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuantity")]
        private Label lblQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBalance")]
        private Label lblBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblHaoDescription")]
        private Label lblHaoDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtHaoDescription")]
        private TextBox txtHaoDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInventoryItem")]
        private TextBox txtInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PageControl")]
        private TabControl PageControl { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpGeneral")]
        private TabPage tpGeneral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpNotes")]
        private TabPage tpNotes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlBottom")]
        private Panel pnlBottom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReturned")]
        private Label lblReturned { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlTop")]
        private Panel pnlTop { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBillingMonth")]
        private NumericBox nmbBillingMonth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbDOS")]
        private GroupBox gbDOS { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDOSFrom")]
        private Label lblDOSFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDOSTo")]
        private UltraDateTimeEditor dtbDOSTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDOSFrom")]
        private UltraDateTimeEditor dtbDOSFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBillableAmount")]
        private NumericBox nmbBillableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillableAmount")]
        private Label lblBillableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAllowableAmount")]
        private NumericBox nmbAllowableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAllowableAmount")]
        private Label lblAllowableAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlInvoiceNotes")]
        private DMEWorks.Details.ControlInvoiceNotes ControlInvoiceNotes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnRecalculateBalance")]
        private Button btnRecalculateBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBillIns4")]
        private CheckBox chbBillIns4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBillIns3")]
        private CheckBox chbBillIns3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBillIns2")]
        private CheckBox chbBillIns2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBillIns1")]
        private CheckBox chbBillIns1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbShowSpanDates")]
        private CheckBox chbShowSpanDates { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbHardship")]
        private CheckBox chbHardship { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbDrugIdentification")]
        private GroupBox gbDrugIdentification { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDrugControlNumber")]
        private TextBox txtDrugControlNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDrugNoteField")]
        private TextBox txtDrugNoteField { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDrugControlNumber")]
        private Label lblDrugControlNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDrugNoteField")]
        private Label lblDrugNoteField { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpInvoiceTransactions")]
        private TabPage tpInvoiceTransactions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlInvoiceTransactions21")]
        private ControlInvoiceTransactions ControlInvoiceTransactions21 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbNopayIns1")]
        private CheckBox chbNopayIns1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlCmnID1")]
        private ControlCmnID ControlCmnID1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAuthorizationNumber")]
        protected virtual TextBox txtAuthorizationNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAuthorizationNumber")]
        protected virtual Label lblAuthorizationNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAuthorizationType")]
        protected virtual Label lblAuthorizationType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbAuthorizationType")]
        protected virtual Combobox cmbAuthorizationType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDXPointer10")]
        private TextBox txtDXPointer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDXPointer10")]
        private Label lblDXPointer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDXPointer9")]
        private TextBox txtDXPointer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBilling")]
        private Label lblBilling { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("fdHAO")]
        private FindDialog fdHAO { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnFindHao")]
        private Button btnFindHao { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolTip1")]
        protected virtual ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override AllowStateEnum AllowState
        {
            get => 
                base.AllowState;
            set
            {
                base.AllowState = value;
                this.nmbBillableAmount.Enabled = (base.AllowState & AllowStateEnum.AllowEdit07) == AllowStateEnum.AllowEdit07;
                this.nmbQuantity.Enabled = (base.AllowState & AllowStateEnum.AllowEdit07) == AllowStateEnum.AllowEdit07;
            }
        }

        private int? CustomerID
        {
            get
            {
                ControlInvoiceDetails details = base.F_Parent as ControlInvoiceDetails;
                return details?.CustomerID;
            }
        }

        private int? OrderID
        {
            get
            {
                ControlInvoiceDetails details = base.F_Parent as ControlInvoiceDetails;
                return details?.OrderID;
            }
        }
    }
}

