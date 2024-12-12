namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Billing;
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
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    [DesignerGenerated]
    public class FormOrderDetailBase : FormDetails
    {
        private IContainer components;
        private int? FID;
        private int? FInventoryItemID;
        private int? FPriceCodeID;
        private double? FSale_AllowablePrice;
        private double? FSale_BillablePrice;
        private double? FRent_AllowablePrice;
        private double? FRent_BillablePrice;
        private string F_MIR;
        private FormMirHelper F_MirHelper;

        public FormOrderDetailBase() : this(null)
        {
        }

        public FormOrderDetailBase(ControlOrderDetailsBase Parent) : base(Parent)
        {
            this.InitializeComponent();
            this.nmbOrderedConverter.ReadOnly = true;
            this.nmbBilledConverter.ReadOnly = true;
            this.nmbDeliveryConverter.ReadOnly = true;
            this.nmbBilledQuantity.ReadOnly = true;
            this.nmbDeliveryQuantity.ReadOnly = true;
            this.cmbSerial.NewButton = false;
            this.cmbSerial.EditButton = false;
            this.cmbWarehouse.NewButton = false;
            this.cmbWarehouse.EditButton = false;
            if (Parent != null)
            {
                Parent.CustomerIDChanged += new EventHandler(this.Parent_CustomerIDChanged);
                Parent.OrderIDChanged += new EventHandler(this.Parent_OrderIDChanged);
            }
        }

        protected virtual void BeginUpdate()
        {
        }

        private void btnFindHao_Click(object sender, EventArgs e)
        {
            if (this.fdHAO.ShowDialog() == DialogResult.OK)
            {
                Functions.SetTextBoxText(this.txtHaoDescription, this.fdHAO.SelectedRow["Description"]);
            }
        }

        private void btnGotoPricing_Click(object sender, EventArgs e)
        {
            if ((this.FInventoryItemID != null) && (this.FPriceCodeID != null))
            {
                FormParameters @params = new FormParameters {
                    ["InventoryItemID"] = this.FInventoryItemID.Value,
                    ["PriceCodeID"] = this.FPriceCodeID.Value
                };
                ClassGlobalObjects.ShowForm(FormFactories.FormPricing(), @params);
            }
        }

        private void btnReget_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.FInventoryItemID != null) && (this.FPriceCodeID != null))
                {
                    this.LoadPriceListInformation(this.FInventoryItemID.Value, this.FPriceCodeID.Value);
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

        private void btnReorder_Click(object sender, EventArgs e)
        {
            if (this.FInventoryItemID != null)
            {
                FormParameters @params = new FormParameters();
                using (StringWriter writer = new StringWriter())
                {
                    XmlTextWriter writer2 = new XmlTextWriter(writer);
                    writer2.WriteStartElement("Order");
                    writer2.WriteStartElement("Item");
                    writer2.WriteElementString("InventoryItemID", Convert.ToString(this.FInventoryItemID.Value));
                    if (this.CustomerID != null)
                    {
                        writer2.WriteElementString("CustomerID", Convert.ToString(this.CustomerID.Value));
                    }
                    writer2.WriteElementString("WarehouseID", Convert.ToString(this.cmbWarehouse.SelectedValue));
                    writer2.WriteElementString("Quantity", NullableConvert.ToString(this.nmbDeliveryQuantity.AsDouble));
                    writer2.WriteEndElement();
                    writer2.WriteEndElement();
                    @params["OrderXml"] = writer.ToString();
                }
                ClassGlobalObjects.ShowForm(FormFactories.FormPurchaseOrder(), @params);
            }
        }

        private void chbMissingInformation_CheckedChanged(object sender, EventArgs e)
        {
            this.InternalShowMissingInformation(this.chbMissingInformation.Checked & this.chbMissingInformation.Visible);
        }

        private void chbMissingInformation_VisibleChanged(object sender, EventArgs e)
        {
            this.InternalShowMissingInformation(this.chbMissingInformation.Checked & this.chbMissingInformation.Visible);
        }

        protected override void Clear()
        {
            this.BeginUpdate();
            try
            {
                this.F_MIR = "";
                this.FID = null;
                this.FInventoryItemID = null;
                this.FPriceCodeID = null;
                int? serialID = null;
                this.Load_Table_Serial(this.FInventoryItemID, serialID);
                Functions.SetTextBoxText(this.txtInventoryItem, DBNull.Value);
                Functions.SetTextBoxText(this.txtPriceCode, DBNull.Value);
                Functions.SetComboBoxText(this.cmbSaleRentType, DBNull.Value);
                Functions.SetComboBoxValue(this.cmbSerial, DBNull.Value);
                Functions.SetComboBoxValue(this.cmbWarehouse, DBNull.Value);
                Functions.SetDateBoxValue(this.dtbDOSFrom, DateTime.Today);
                Functions.SetDateBoxValue(this.dtbDOSTo, DateTime.Today.AddMonths(1).AddDays(-1.0));
                Functions.SetCheckBoxChecked(this.chbShowSpanDates, DBNull.Value);
                Functions.SetTextBoxText(this.txtBillingMonth, "1");
                Functions.SetNumericBoxValue(this.nmbBillablePrice, DBNull.Value);
                Functions.SetNumericBoxValue(this.nmbAllowablePrice, DBNull.Value);
                this.FSale_AllowablePrice = null;
                this.FSale_BillablePrice = null;
                this.FRent_AllowablePrice = null;
                this.FRent_BillablePrice = null;
                Functions.SetCheckBoxChecked(this.chbFlatRate, DBNull.Value);
                Functions.SetCheckBoxChecked(this.chbTaxable, DBNull.Value);
                Functions.SetNumericBoxValue(this.nmbOrderedQuantity, DBNull.Value);
                Functions.SetTextBoxText(this.txtOrderedUnits, DBNull.Value);
                Functions.SetComboBoxText(this.cmbOrderedWhen, DBNull.Value);
                Functions.SetNumericBoxValue(this.nmbOrderedConverter, DBNull.Value);
                Functions.SetNumericBoxValue(this.nmbBilledQuantity, DBNull.Value);
                Functions.SetTextBoxText(this.txtBilledUnits, DBNull.Value);
                Functions.SetComboBoxText(this.cmbBilledWhen, DBNull.Value);
                Functions.SetNumericBoxValue(this.nmbBilledConverter, DBNull.Value);
                Functions.SetNumericBoxValue(this.nmbDeliveryQuantity, DBNull.Value);
                Functions.SetTextBoxText(this.txtDeliveryUnits, DBNull.Value);
                Functions.SetNumericBoxValue(this.nmbDeliveryConverter, DBNull.Value);
                Functions.SetTextBoxText(this.txtBillingCode, DBNull.Value);
                Functions.SetTextBoxText(this.txtModifier1, DBNull.Value);
                Functions.SetTextBoxText(this.txtModifier2, DBNull.Value);
                Functions.SetTextBoxText(this.txtModifier3, DBNull.Value);
                Functions.SetTextBoxText(this.txtModifier4, DBNull.Value);
                Functions.SetTextBoxText(this.txtDXPointer9, DBNull.Value);
                Functions.SetTextBoxText(this.txtDXPointer10, DBNull.Value);
                this.BillItemOn = "Day of Delivery";
                Functions.SetComboBoxValue(this.cmbAuthorizationType, DBNull.Value);
                Functions.SetTextBoxText(this.txtAuthorizationNumber, DBNull.Value);
                Functions.SetDateBoxValue(this.dtbAuthorizationExpirationDate, DBNull.Value);
                Functions.SetTextBoxText(this.txtReasonForPickup, DBNull.Value);
                Functions.SetTextBoxText(this.txtSpecialCode, DBNull.Value);
                Functions.SetTextBoxText(this.txtReviewCode, DBNull.Value);
                Functions.SetCheckBoxChecked(this.chbSendCMN_RX_w_invoice, DBNull.Value);
                Functions.SetCheckBoxChecked(this.chbMedicallyUnnecessary, DBNull.Value);
                Functions.SetLabelText(this.lblState, "New");
                this.ControlCmnID1.DefaultCmnType = null;
                this.ControlCmnID1.CustomerID = this.CustomerID;
                this.ControlCmnID1.OrderID = this.OrderID;
                serialID = null;
                this.ControlCmnID1.CmnID = serialID;
                Functions.SetTextBoxText(this.txtHaoDescription, DBNull.Value);
                this.lblReturned.Visible = false;
            }
            finally
            {
                this.EndUpdate();
            }
            this.UpdateEnabledState();
            this.InternalShowMissingInformation(this.chbMissingInformation.Checked & this.chbMissingInformation.Visible);
        }

        private void cmbSaleRentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshPricing();
        }

        private void cmbSaleRentType_TextChanged(object sender, EventArgs e)
        {
            this.RefreshPricing();
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual void EndUpdate()
        {
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormOrderDetailBase));
            this.cmbSaleRentType = new ComboBox();
            this.txtSpecialCode = new TextBox();
            this.txtReasonForPickup = new TextBox();
            this.dtbDOSTo = new UltraDateTimeEditor();
            this.dtbDOSFrom = new UltraDateTimeEditor();
            this.lblOrdered = new Label();
            this.lblPriceCode = new Label();
            this.lblInventoryItem = new Label();
            this.txtBilledUnits = new TextBox();
            this.chbFlatRate = new CheckBox();
            this.chbTaxable = new CheckBox();
            this.txtOrderedUnits = new TextBox();
            this.txtAuthorizationNumber = new TextBox();
            this.chbShowSpanDates = new CheckBox();
            this.txtBillingCode = new TextBox();
            this.txtModifier1 = new TextBox();
            this.txtModifier2 = new TextBox();
            this.txtModifier3 = new TextBox();
            this.txtModifier4 = new TextBox();
            this.chbSendCMN_RX_w_invoice = new CheckBox();
            this.txtReviewCode = new TextBox();
            this.chbMedicallyUnnecessary = new CheckBox();
            this.lblBilled = new Label();
            this.lblQty = new Label();
            this.lblWhen = new Label();
            this.lblUnits = new Label();
            this.lblAuthorizationNumber = new Label();
            this.lblReasonForPickup = new Label();
            this.lblDOSTo = new Label();
            this.lbllBillCode = new Label();
            this.lblMod = new Label();
            this.lblDXPointer9 = new Label();
            this.lblDOSFrom = new Label();
            this.lblSpecialCode = new Label();
            this.lblReviewCode = new Label();
            this.lblAllowablePrice = new Label();
            this.lblBillablePrice = new Label();
            this.lblSaleRentType = new Label();
            this.gbDOS1 = new GroupBox();
            this.txtBillingMonth = new TextBox();
            this.lblBillingMonth = new Label();
            this.gbBillItemOn = new GroupBox();
            this.lnkLastDayOfTheMonth = new LinkLabel();
            this.lnkDayOfPickup = new LinkLabel();
            this.rbLastDayOfThePeriod = new RadioButton();
            this.rbDayOfDelivery = new RadioButton();
            this.lblHaoDescription = new Label();
            this.lblAuthorizationType = new Label();
            this.cmbAuthorizationType = new Combobox();
            this.nmbBilledQuantity = new NumericBox();
            this.nmbOrderedQuantity = new NumericBox();
            this.nmbAllowablePrice = new NumericBox();
            this.nmbBillablePrice = new NumericBox();
            this.cmbBilledWhen = new ComboBox();
            this.cmbOrderedWhen = new ComboBox();
            this.ToolTip1 = new ToolTip(this.components);
            this.dtbAuthorizationExpirationDate = new UltraDateTimeEditor();
            this.lblAuthorizationExpirationDate = new Label();
            this.MissingProvider = new ErrorProvider(this.components);
            this.chbMissingInformation = new CheckBox();
            this.lblReturned = new Label();
            this.txtPriceCode = new TextBox();
            this.txtInventoryItem = new TextBox();
            this.nmbOrderedConverter = new NumericBox();
            this.nmbBilledConverter = new NumericBox();
            this.lblConverter = new Label();
            this.nmbDeliveryConverter = new NumericBox();
            this.nmbDeliveryQuantity = new NumericBox();
            this.txtDeliveryUnits = new TextBox();
            this.lblDelivery = new Label();
            this.btnRefresh = new Button();
            this.btnGotoPricing = new Button();
            this.pnlTop = new Panel();
            this.btnFindHao = new Button();
            this.txtHaoDescription = new TextBox();
            this.lblWarehouse = new Label();
            this.cmbWarehouse = new Combobox();
            this.cmbSerial = new Combobox();
            this.lblSerial = new Label();
            this.btnReorder = new Button();
            this.ControlCmnID1 = new ControlCmnID();
            this.pnlBottom = new Panel();
            this.txtDXPointer10 = new TextBox();
            this.txtDXPointer9 = new TextBox();
            this.lblDXPointer10 = new Label();
            this.lblState = new Label();
            this.Panel1 = new Panel();
            this.pnlPricing = new Panel();
            this.fdHAO = new FindDialog();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            ((ISupportInitialize) this.ValidationWarnings).BeginInit();
            this.gbDOS1.SuspendLayout();
            this.gbBillItemOn.SuspendLayout();
            ((ISupportInitialize) this.MissingProvider).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlPricing.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.Location = new Point(0x1a0, 8);
            this.btnCancel.TabIndex = 4;
            this.btnOK.Location = new Point(0x150, 8);
            this.btnOK.TabIndex = 3;
            this.cmbSaleRentType.Location = new Point(0x60, 0x90);
            this.cmbSaleRentType.Name = "cmbSaleRentType";
            this.cmbSaleRentType.Size = new Size(0xf8, 0x15);
            this.cmbSaleRentType.TabIndex = 15;
            this.txtSpecialCode.Location = new Point(0x60, 120);
            this.txtSpecialCode.Name = "txtSpecialCode";
            this.txtSpecialCode.Size = new Size(0x80, 20);
            this.txtSpecialCode.TabIndex = 11;
            this.txtReasonForPickup.Location = new Point(0x158, 0x90);
            this.txtReasonForPickup.Name = "txtReasonForPickup";
            this.txtReasonForPickup.Size = new Size(160, 20);
            this.txtReasonForPickup.TabIndex = 0x15;
            this.ToolTip1.SetToolTip(this.txtReasonForPickup, "ReasonForPickup");
            this.dtbDOSTo.Location = new Point(0x30, 40);
            this.dtbDOSTo.Name = "dtbDOSTo";
            this.dtbDOSTo.Size = new Size(0x60, 0x15);
            this.dtbDOSTo.TabIndex = 3;
            this.dtbDOSFrom.Location = new Point(0x30, 0x10);
            this.dtbDOSFrom.Name = "dtbDOSFrom";
            this.dtbDOSFrom.Size = new Size(0x60, 0x15);
            this.dtbDOSFrom.TabIndex = 1;
            this.lblOrdered.Location = new Point(0, 0x18);
            this.lblOrdered.Name = "lblOrdered";
            this.lblOrdered.Size = new Size(0x58, 0x15);
            this.lblOrdered.TabIndex = 4;
            this.lblOrdered.Text = "Ordered";
            this.lblOrdered.TextAlign = ContentAlignment.MiddleRight;
            this.lblPriceCode.Location = new Point(0, 120);
            this.lblPriceCode.Name = "lblPriceCode";
            this.lblPriceCode.Size = new Size(0x58, 0x15);
            this.lblPriceCode.TabIndex = 12;
            this.lblPriceCode.Text = "Price Code";
            this.lblPriceCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblInventoryItem.Location = new Point(0, 0);
            this.lblInventoryItem.Name = "lblInventoryItem";
            this.lblInventoryItem.Size = new Size(0x58, 0x15);
            this.lblInventoryItem.TabIndex = 0;
            this.lblInventoryItem.Text = "Inventory Item";
            this.lblInventoryItem.TextAlign = ContentAlignment.MiddleRight;
            this.txtBilledUnits.AcceptsReturn = true;
            this.txtBilledUnits.BackColor = SystemColors.Window;
            this.txtBilledUnits.ForeColor = SystemColors.WindowText;
            this.txtBilledUnits.Location = new Point(160, 0x30);
            this.txtBilledUnits.MaxLength = 0;
            this.txtBilledUnits.Name = "txtBilledUnits";
            this.txtBilledUnits.RightToLeft = RightToLeft.No;
            this.txtBilledUnits.Size = new Size(0x58, 20);
            this.txtBilledUnits.TabIndex = 11;
            this.chbFlatRate.Location = new Point(0xa8, 0xd8);
            this.chbFlatRate.Name = "chbFlatRate";
            this.chbFlatRate.Size = new Size(0x48, 0x15);
            this.chbFlatRate.TabIndex = 0x16;
            this.chbFlatRate.Text = "Flat Rate";
            this.chbFlatRate.UseVisualStyleBackColor = false;
            this.chbTaxable.Location = new Point(0xa8, 240);
            this.chbTaxable.Name = "chbTaxable";
            this.chbTaxable.Size = new Size(0x48, 0x15);
            this.chbTaxable.TabIndex = 0x19;
            this.chbTaxable.Text = "Taxable";
            this.chbTaxable.UseVisualStyleBackColor = false;
            this.txtOrderedUnits.AcceptsReturn = true;
            this.txtOrderedUnits.BackColor = SystemColors.Window;
            this.txtOrderedUnits.ForeColor = SystemColors.WindowText;
            this.txtOrderedUnits.Location = new Point(160, 0x18);
            this.txtOrderedUnits.MaxLength = 0;
            this.txtOrderedUnits.Name = "txtOrderedUnits";
            this.txtOrderedUnits.RightToLeft = RightToLeft.No;
            this.txtOrderedUnits.Size = new Size(0x58, 20);
            this.txtOrderedUnits.TabIndex = 6;
            this.txtAuthorizationNumber.AcceptsReturn = true;
            this.txtAuthorizationNumber.BackColor = SystemColors.Window;
            this.txtAuthorizationNumber.ForeColor = SystemColors.WindowText;
            this.txtAuthorizationNumber.Location = new Point(0x60, 0x48);
            this.txtAuthorizationNumber.MaxLength = 0;
            this.txtAuthorizationNumber.Name = "txtAuthorizationNumber";
            this.txtAuthorizationNumber.RightToLeft = RightToLeft.No;
            this.txtAuthorizationNumber.Size = new Size(0x80, 20);
            this.txtAuthorizationNumber.TabIndex = 7;
            this.chbShowSpanDates.Location = new Point(8, 0x40);
            this.chbShowSpanDates.Name = "chbShowSpanDates";
            this.chbShowSpanDates.Size = new Size(0x88, 0x15);
            this.chbShowSpanDates.TabIndex = 4;
            this.chbShowSpanDates.Text = "Show Span Dates";
            this.chbShowSpanDates.UseVisualStyleBackColor = false;
            this.txtBillingCode.Location = new Point(0x60, 0x18);
            this.txtBillingCode.MaxLength = 0;
            this.txtBillingCode.Name = "txtBillingCode";
            this.txtBillingCode.RightToLeft = RightToLeft.No;
            this.txtBillingCode.Size = new Size(0x80, 20);
            this.txtBillingCode.TabIndex = 3;
            this.txtModifier1.Location = new Point(0x60, 0x30);
            this.txtModifier1.MaxLength = 0;
            this.txtModifier1.Name = "txtModifier1";
            this.txtModifier1.RightToLeft = RightToLeft.No;
            this.txtModifier1.Size = new Size(30, 20);
            this.txtModifier1.TabIndex = 5;
            this.txtModifier2.Location = new Point(0x80, 0x30);
            this.txtModifier2.MaxLength = 0;
            this.txtModifier2.Name = "txtModifier2";
            this.txtModifier2.RightToLeft = RightToLeft.No;
            this.txtModifier2.Size = new Size(30, 20);
            this.txtModifier2.TabIndex = 6;
            this.txtModifier3.Location = new Point(160, 0x30);
            this.txtModifier3.MaxLength = 0;
            this.txtModifier3.Name = "txtModifier3";
            this.txtModifier3.RightToLeft = RightToLeft.No;
            this.txtModifier3.Size = new Size(30, 20);
            this.txtModifier3.TabIndex = 7;
            this.txtModifier4.Location = new Point(0xc0, 0x30);
            this.txtModifier4.MaxLength = 0;
            this.txtModifier4.Name = "txtModifier4";
            this.txtModifier4.RightToLeft = RightToLeft.No;
            this.txtModifier4.Size = new Size(30, 20);
            this.txtModifier4.TabIndex = 8;
            this.chbSendCMN_RX_w_invoice.Location = new Point(240, 0x18);
            this.chbSendCMN_RX_w_invoice.Name = "chbSendCMN_RX_w_invoice";
            this.chbSendCMN_RX_w_invoice.Size = new Size(0x108, 0x15);
            this.chbSendCMN_RX_w_invoice.TabIndex = 15;
            this.chbSendCMN_RX_w_invoice.Text = "Send CMN/RX with this invoice";
            this.txtReviewCode.BackColor = SystemColors.Window;
            this.txtReviewCode.ForeColor = SystemColors.WindowText;
            this.txtReviewCode.Location = new Point(0x60, 0x90);
            this.txtReviewCode.MaxLength = 0;
            this.txtReviewCode.Name = "txtReviewCode";
            this.txtReviewCode.RightToLeft = RightToLeft.No;
            this.txtReviewCode.Size = new Size(0x80, 20);
            this.txtReviewCode.TabIndex = 13;
            this.chbMedicallyUnnecessary.Location = new Point(240, 0x30);
            this.chbMedicallyUnnecessary.Name = "chbMedicallyUnnecessary";
            this.chbMedicallyUnnecessary.Size = new Size(0x108, 0x15);
            this.chbMedicallyUnnecessary.TabIndex = 0x10;
            this.chbMedicallyUnnecessary.Text = "Medically Unnecessary";
            this.lblBilled.Location = new Point(0, 0x30);
            this.lblBilled.Name = "lblBilled";
            this.lblBilled.Size = new Size(0x58, 0x15);
            this.lblBilled.TabIndex = 9;
            this.lblBilled.Text = "Billed";
            this.lblBilled.TextAlign = ContentAlignment.MiddleRight;
            this.lblQty.Location = new Point(0x60, 0);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new Size(0x38, 0x15);
            this.lblQty.TabIndex = 0;
            this.lblQty.Text = "Quantity";
            this.lblQty.TextAlign = ContentAlignment.MiddleCenter;
            this.lblWhen.Location = new Point(0x100, 0);
            this.lblWhen.Name = "lblWhen";
            this.lblWhen.Size = new Size(0x58, 0x15);
            this.lblWhen.TabIndex = 2;
            this.lblWhen.Text = "When";
            this.lblWhen.TextAlign = ContentAlignment.MiddleCenter;
            this.lblUnits.Location = new Point(160, 0);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new Size(0x58, 0x15);
            this.lblUnits.TabIndex = 1;
            this.lblUnits.Text = "Units";
            this.lblUnits.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAuthorizationNumber.Location = new Point(0, 0x48);
            this.lblAuthorizationNumber.Name = "lblAuthorizationNumber";
            this.lblAuthorizationNumber.Size = new Size(0x58, 0x15);
            this.lblAuthorizationNumber.TabIndex = 6;
            this.lblAuthorizationNumber.Text = "Prior Auth #";
            this.lblAuthorizationNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblReasonForPickup.Location = new Point(0xe8, 0x90);
            this.lblReasonForPickup.Name = "lblReasonForPickup";
            this.lblReasonForPickup.Size = new Size(0x68, 0x15);
            this.lblReasonForPickup.TabIndex = 20;
            this.lblReasonForPickup.Text = "Reason for Pickup";
            this.lblReasonForPickup.TextAlign = ContentAlignment.MiddleRight;
            this.lblDOSTo.Location = new Point(8, 40);
            this.lblDOSTo.Name = "lblDOSTo";
            this.lblDOSTo.Size = new Size(0x20, 0x15);
            this.lblDOSTo.TabIndex = 2;
            this.lblDOSTo.Text = "To";
            this.lblDOSTo.TextAlign = ContentAlignment.MiddleRight;
            this.lbllBillCode.Location = new Point(0, 0x18);
            this.lbllBillCode.Name = "lbllBillCode";
            this.lbllBillCode.Size = new Size(0x58, 0x15);
            this.lbllBillCode.TabIndex = 2;
            this.lbllBillCode.Text = "Billing Code";
            this.lbllBillCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblMod.Location = new Point(0, 0x30);
            this.lblMod.Name = "lblMod";
            this.lblMod.Size = new Size(0x58, 0x15);
            this.lblMod.TabIndex = 4;
            this.lblMod.Text = "Modifiers";
            this.lblMod.TextAlign = ContentAlignment.MiddleRight;
            this.lblDXPointer9.Location = new Point(0, 0);
            this.lblDXPointer9.Name = "lblDXPointer9";
            this.lblDXPointer9.Size = new Size(0x58, 0x15);
            this.lblDXPointer9.TabIndex = 0;
            this.lblDXPointer9.Text = "DX Pointer 9";
            this.lblDXPointer9.TextAlign = ContentAlignment.MiddleRight;
            this.lblDOSFrom.Location = new Point(8, 0x10);
            this.lblDOSFrom.Name = "lblDOSFrom";
            this.lblDOSFrom.Size = new Size(0x20, 0x15);
            this.lblDOSFrom.TabIndex = 0;
            this.lblDOSFrom.Text = "From";
            this.lblDOSFrom.TextAlign = ContentAlignment.MiddleRight;
            this.lblSpecialCode.Location = new Point(0, 120);
            this.lblSpecialCode.Name = "lblSpecialCode";
            this.lblSpecialCode.Size = new Size(0x58, 0x15);
            this.lblSpecialCode.TabIndex = 10;
            this.lblSpecialCode.Text = "Special Code";
            this.lblSpecialCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblReviewCode.Location = new Point(0, 0x90);
            this.lblReviewCode.Name = "lblReviewCode";
            this.lblReviewCode.Size = new Size(0x58, 0x15);
            this.lblReviewCode.TabIndex = 12;
            this.lblReviewCode.Text = "Review Code";
            this.lblReviewCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblAllowablePrice.Location = new Point(0, 240);
            this.lblAllowablePrice.Name = "lblAllowablePrice";
            this.lblAllowablePrice.Size = new Size(0x58, 0x15);
            this.lblAllowablePrice.TabIndex = 0x17;
            this.lblAllowablePrice.Text = "Allowable";
            this.lblAllowablePrice.TextAlign = ContentAlignment.MiddleRight;
            this.lblBillablePrice.Location = new Point(0, 0xd8);
            this.lblBillablePrice.Name = "lblBillablePrice";
            this.lblBillablePrice.Size = new Size(0x58, 0x15);
            this.lblBillablePrice.TabIndex = 20;
            this.lblBillablePrice.Text = "Billable";
            this.lblBillablePrice.TextAlign = ContentAlignment.MiddleRight;
            this.lblSaleRentType.Location = new Point(0, 0x90);
            this.lblSaleRentType.Name = "lblSaleRentType";
            this.lblSaleRentType.Size = new Size(0x58, 0x15);
            this.lblSaleRentType.TabIndex = 14;
            this.lblSaleRentType.Text = "Sale/Rent Type";
            this.lblSaleRentType.TextAlign = ContentAlignment.MiddleRight;
            this.gbDOS1.Controls.Add(this.txtBillingMonth);
            this.gbDOS1.Controls.Add(this.lblBillingMonth);
            this.gbDOS1.Controls.Add(this.chbShowSpanDates);
            this.gbDOS1.Controls.Add(this.lblDOSTo);
            this.gbDOS1.Controls.Add(this.lblDOSFrom);
            this.gbDOS1.Controls.Add(this.dtbDOSTo);
            this.gbDOS1.Controls.Add(this.dtbDOSFrom);
            this.gbDOS1.Location = new Point(0x160, 0);
            this.gbDOS1.Name = "gbDOS1";
            this.gbDOS1.Size = new Size(0x98, 0x72);
            this.gbDOS1.TabIndex = 0x1a;
            this.gbDOS1.TabStop = false;
            this.gbDOS1.Text = "Date Of Service";
            this.txtBillingMonth.BackColor = SystemColors.Window;
            this.txtBillingMonth.Location = new Point(0x58, 0x58);
            this.txtBillingMonth.Name = "txtBillingMonth";
            this.txtBillingMonth.ReadOnly = true;
            this.txtBillingMonth.Size = new Size(0x38, 20);
            this.txtBillingMonth.TabIndex = 6;
            this.txtBillingMonth.Text = "1";
            this.txtBillingMonth.TextAlign = HorizontalAlignment.Right;
            this.lblBillingMonth.Location = new Point(8, 0x58);
            this.lblBillingMonth.Name = "lblBillingMonth";
            this.lblBillingMonth.Size = new Size(0x48, 0x15);
            this.lblBillingMonth.TabIndex = 5;
            this.lblBillingMonth.Text = "Billing Month";
            this.lblBillingMonth.TextAlign = ContentAlignment.MiddleRight;
            this.gbBillItemOn.BackColor = SystemColors.Control;
            this.gbBillItemOn.Controls.Add(this.lnkLastDayOfTheMonth);
            this.gbBillItemOn.Controls.Add(this.lnkDayOfPickup);
            this.gbBillItemOn.Controls.Add(this.rbLastDayOfThePeriod);
            this.gbBillItemOn.Controls.Add(this.rbDayOfDelivery);
            this.gbBillItemOn.Location = new Point(0x160, 120);
            this.gbBillItemOn.Name = "gbBillItemOn";
            this.gbBillItemOn.Size = new Size(0x98, 0x70);
            this.gbBillItemOn.TabIndex = 0x1b;
            this.gbBillItemOn.TabStop = false;
            this.gbBillItemOn.Text = "Bill Item On:";
            this.lnkLastDayOfTheMonth.Location = new Point(0x18, 0x58);
            this.lnkLastDayOfTheMonth.Name = "lnkLastDayOfTheMonth";
            this.lnkLastDayOfTheMonth.Size = new Size(0x70, 0x15);
            this.lnkLastDayOfTheMonth.TabIndex = 3;
            this.lnkLastDayOfTheMonth.TabStop = true;
            this.lnkLastDayOfTheMonth.Text = "Last day of the Month";
            this.lnkLastDayOfTheMonth.TextAlign = ContentAlignment.MiddleLeft;
            this.lnkDayOfPickup.Location = new Point(0x18, 0x40);
            this.lnkDayOfPickup.Name = "lnkDayOfPickup";
            this.lnkDayOfPickup.Size = new Size(0x70, 0x15);
            this.lnkDayOfPickup.TabIndex = 2;
            this.lnkDayOfPickup.TabStop = true;
            this.lnkDayOfPickup.Text = "Bill at Pick-Up";
            this.lnkDayOfPickup.TextAlign = ContentAlignment.MiddleLeft;
            this.rbLastDayOfThePeriod.Location = new Point(8, 40);
            this.rbLastDayOfThePeriod.Name = "rbLastDayOfThePeriod";
            this.rbLastDayOfThePeriod.Size = new Size(0x88, 0x15);
            this.rbLastDayOfThePeriod.TabIndex = 1;
            this.rbLastDayOfThePeriod.TabStop = true;
            this.rbLastDayOfThePeriod.Text = "Last day of the Period";
            this.rbDayOfDelivery.Location = new Point(8, 0x10);
            this.rbDayOfDelivery.Name = "rbDayOfDelivery";
            this.rbDayOfDelivery.Size = new Size(0x88, 0x15);
            this.rbDayOfDelivery.TabIndex = 0;
            this.rbDayOfDelivery.TabStop = true;
            this.rbDayOfDelivery.Text = "Day of Delivery";
            this.lblHaoDescription.Location = new Point(0, 0x48);
            this.lblHaoDescription.Name = "lblHaoDescription";
            this.lblHaoDescription.Size = new Size(0x58, 0x15);
            this.lblHaoDescription.TabIndex = 9;
            this.lblHaoDescription.Text = "HAO";
            this.lblHaoDescription.TextAlign = ContentAlignment.MiddleRight;
            this.lblAuthorizationType.Location = new Point(0, 0x30);
            this.lblAuthorizationType.Name = "lblAuthorizationType";
            this.lblAuthorizationType.Size = new Size(0x58, 0x15);
            this.lblAuthorizationType.TabIndex = 4;
            this.lblAuthorizationType.Text = "Prior Auth Type";
            this.lblAuthorizationType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbAuthorizationType.EditButton = false;
            this.cmbAuthorizationType.FindButton = false;
            this.cmbAuthorizationType.Location = new Point(0x60, 0x30);
            this.cmbAuthorizationType.Name = "cmbAuthorizationType";
            this.cmbAuthorizationType.NewButton = false;
            this.cmbAuthorizationType.Size = new Size(0x80, 0x15);
            this.cmbAuthorizationType.TabIndex = 5;
            this.nmbBilledQuantity.Location = new Point(0x60, 0x30);
            this.nmbBilledQuantity.Name = "nmbBilledQuantity";
            this.nmbBilledQuantity.Size = new Size(0x38, 0x15);
            this.nmbBilledQuantity.TabIndex = 10;
            this.nmbOrderedQuantity.Location = new Point(0x60, 0x18);
            this.nmbOrderedQuantity.Name = "nmbOrderedQuantity";
            this.nmbOrderedQuantity.Size = new Size(0x38, 0x15);
            this.nmbOrderedQuantity.TabIndex = 5;
            this.nmbAllowablePrice.Location = new Point(0x60, 240);
            this.nmbAllowablePrice.Name = "nmbAllowablePrice";
            this.nmbAllowablePrice.Size = new Size(0x40, 0x15);
            this.nmbAllowablePrice.TabIndex = 0x18;
            this.nmbBillablePrice.Location = new Point(0x60, 0xd8);
            this.nmbBillablePrice.Name = "nmbBillablePrice";
            this.nmbBillablePrice.Size = new Size(0x40, 0x15);
            this.nmbBillablePrice.TabIndex = 0x15;
            this.cmbBilledWhen.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbBilledWhen.Location = new Point(0x100, 0x30);
            this.cmbBilledWhen.Name = "cmbBilledWhen";
            this.cmbBilledWhen.Size = new Size(0x58, 0x15);
            this.cmbBilledWhen.TabIndex = 12;
            this.cmbOrderedWhen.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbOrderedWhen.Location = new Point(0x100, 0x18);
            this.cmbOrderedWhen.Name = "cmbOrderedWhen";
            this.cmbOrderedWhen.Size = new Size(0x58, 0x15);
            this.cmbOrderedWhen.TabIndex = 7;
            this.dtbAuthorizationExpirationDate.Location = new Point(0x60, 0x60);
            this.dtbAuthorizationExpirationDate.Name = "dtbAuthorizationExpirationDate";
            this.dtbAuthorizationExpirationDate.Size = new Size(0x80, 0x15);
            this.dtbAuthorizationExpirationDate.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.dtbAuthorizationExpirationDate, "Prior Auth Expiration Date");
            this.lblAuthorizationExpirationDate.Location = new Point(0, 0x60);
            this.lblAuthorizationExpirationDate.Name = "lblAuthorizationExpirationDate";
            this.lblAuthorizationExpirationDate.Size = new Size(0x58, 0x15);
            this.lblAuthorizationExpirationDate.TabIndex = 8;
            this.lblAuthorizationExpirationDate.Text = "Prior Auth Expir.";
            this.lblAuthorizationExpirationDate.TextAlign = ContentAlignment.MiddleRight;
            this.ToolTip1.SetToolTip(this.lblAuthorizationExpirationDate, "Prior Auth Expiration Date");
            this.MissingProvider.ContainerControl = this;
            this.MissingProvider.Icon = (Icon) manager.GetObject("MissingProvider.Icon");
            this.chbMissingInformation.CheckAlign = ContentAlignment.MiddleRight;
            this.MissingProvider.SetIconAlignment(this.chbMissingInformation, ErrorIconAlignment.MiddleLeft);
            this.MissingProvider.SetIconPadding(this.chbMissingInformation, -16);
            this.chbMissingInformation.Location = new Point(0x100, 8);
            this.chbMissingInformation.Name = "chbMissingInformation";
            this.chbMissingInformation.Size = new Size(0x40, 0x18);
            this.chbMissingInformation.TabIndex = 2;
            this.chbMissingInformation.Text = "Missing";
            this.lblReturned.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.lblReturned.ForeColor = Color.Red;
            this.lblReturned.Location = new Point(8, 8);
            this.lblReturned.Name = "lblReturned";
            this.lblReturned.Size = new Size(100, 0x17);
            this.lblReturned.TabIndex = 0;
            this.lblReturned.Text = "Returned";
            this.lblReturned.TextAlign = ContentAlignment.MiddleCenter;
            this.txtPriceCode.BackColor = SystemColors.Window;
            this.txtPriceCode.Location = new Point(0x60, 120);
            this.txtPriceCode.Name = "txtPriceCode";
            this.txtPriceCode.ReadOnly = true;
            this.txtPriceCode.Size = new Size(0xf8, 20);
            this.txtPriceCode.TabIndex = 13;
            this.txtInventoryItem.BackColor = SystemColors.Window;
            this.txtInventoryItem.Location = new Point(0x60, 0);
            this.txtInventoryItem.Name = "txtInventoryItem";
            this.txtInventoryItem.ReadOnly = true;
            this.txtInventoryItem.Size = new Size(0xf8, 20);
            this.txtInventoryItem.TabIndex = 1;
            this.nmbOrderedConverter.Location = new Point(0x160, 0x18);
            this.nmbOrderedConverter.Name = "nmbOrderedConverter";
            this.nmbOrderedConverter.Size = new Size(0x38, 0x15);
            this.nmbOrderedConverter.TabIndex = 8;
            this.nmbBilledConverter.Location = new Point(0x160, 0x30);
            this.nmbBilledConverter.Name = "nmbBilledConverter";
            this.nmbBilledConverter.Size = new Size(0x38, 0x15);
            this.nmbBilledConverter.TabIndex = 13;
            this.lblConverter.Location = new Point(0x160, 0);
            this.lblConverter.Name = "lblConverter";
            this.lblConverter.Size = new Size(0x38, 0x15);
            this.lblConverter.TabIndex = 3;
            this.lblConverter.Text = "Converter";
            this.lblConverter.TextAlign = ContentAlignment.MiddleCenter;
            this.nmbDeliveryConverter.Location = new Point(0x160, 0x48);
            this.nmbDeliveryConverter.Name = "nmbDeliveryConverter";
            this.nmbDeliveryConverter.Size = new Size(0x38, 0x15);
            this.nmbDeliveryConverter.TabIndex = 0x11;
            this.nmbDeliveryQuantity.Location = new Point(0x60, 0x48);
            this.nmbDeliveryQuantity.Name = "nmbDeliveryQuantity";
            this.nmbDeliveryQuantity.Size = new Size(0x38, 0x15);
            this.nmbDeliveryQuantity.TabIndex = 15;
            this.txtDeliveryUnits.AcceptsReturn = true;
            this.txtDeliveryUnits.BackColor = SystemColors.Window;
            this.txtDeliveryUnits.ForeColor = SystemColors.WindowText;
            this.txtDeliveryUnits.Location = new Point(160, 0x48);
            this.txtDeliveryUnits.MaxLength = 0;
            this.txtDeliveryUnits.Name = "txtDeliveryUnits";
            this.txtDeliveryUnits.RightToLeft = RightToLeft.No;
            this.txtDeliveryUnits.Size = new Size(0x58, 20);
            this.txtDeliveryUnits.TabIndex = 0x10;
            this.lblDelivery.Location = new Point(0, 0x48);
            this.lblDelivery.Name = "lblDelivery";
            this.lblDelivery.Size = new Size(0x58, 0x15);
            this.lblDelivery.TabIndex = 14;
            this.lblDelivery.Text = "Delivery";
            this.lblDelivery.TextAlign = ContentAlignment.MiddleRight;
            this.btnRefresh.Location = new Point(0x1a0, 0x48);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(80, 0x15);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "Refresh";
            this.btnGotoPricing.Location = new Point(0x1a0, 0x18);
            this.btnGotoPricing.Name = "btnGotoPricing";
            this.btnGotoPricing.Size = new Size(80, 0x15);
            this.btnGotoPricing.TabIndex = 0x12;
            this.btnGotoPricing.Text = "Goto Pricing";
            this.pnlTop.Controls.Add(this.btnFindHao);
            this.pnlTop.Controls.Add(this.txtHaoDescription);
            this.pnlTop.Controls.Add(this.gbBillItemOn);
            this.pnlTop.Controls.Add(this.lblWarehouse);
            this.pnlTop.Controls.Add(this.cmbWarehouse);
            this.pnlTop.Controls.Add(this.cmbSerial);
            this.pnlTop.Controls.Add(this.lblSerial);
            this.pnlTop.Controls.Add(this.txtModifier2);
            this.pnlTop.Controls.Add(this.chbFlatRate);
            this.pnlTop.Controls.Add(this.chbTaxable);
            this.pnlTop.Controls.Add(this.txtModifier3);
            this.pnlTop.Controls.Add(this.nmbBillablePrice);
            this.pnlTop.Controls.Add(this.lbllBillCode);
            this.pnlTop.Controls.Add(this.lblBillablePrice);
            this.pnlTop.Controls.Add(this.lblMod);
            this.pnlTop.Controls.Add(this.lblAllowablePrice);
            this.pnlTop.Controls.Add(this.txtModifier4);
            this.pnlTop.Controls.Add(this.nmbAllowablePrice);
            this.pnlTop.Controls.Add(this.gbDOS1);
            this.pnlTop.Controls.Add(this.txtBillingCode);
            this.pnlTop.Controls.Add(this.lblHaoDescription);
            this.pnlTop.Controls.Add(this.lblPriceCode);
            this.pnlTop.Controls.Add(this.txtModifier1);
            this.pnlTop.Controls.Add(this.lblInventoryItem);
            this.pnlTop.Controls.Add(this.cmbSaleRentType);
            this.pnlTop.Controls.Add(this.lblSaleRentType);
            this.pnlTop.Controls.Add(this.txtPriceCode);
            this.pnlTop.Controls.Add(this.txtInventoryItem);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(4, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new Size(0x1fc, 0x10c);
            this.pnlTop.TabIndex = 0;
            this.btnFindHao.FlatStyle = FlatStyle.Flat;
            this.btnFindHao.Image = My.Resources.Resources.ImageSpyglass2;
            this.btnFindHao.Location = new Point(0x40, 0x5f);
            this.btnFindHao.Name = "btnFindHao";
            this.btnFindHao.Size = new Size(0x15, 0x15);
            this.btnFindHao.TabIndex = 11;
            this.btnFindHao.TabStop = false;
            this.txtHaoDescription.Location = new Point(0x60, 0x48);
            this.txtHaoDescription.MaxLength = 100;
            this.txtHaoDescription.Multiline = true;
            this.txtHaoDescription.Name = "txtHaoDescription";
            this.txtHaoDescription.Size = new Size(0xf8, 0x2c);
            this.txtHaoDescription.TabIndex = 10;
            this.lblWarehouse.Location = new Point(0, 0xc0);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x58, 0x15);
            this.lblWarehouse.TabIndex = 0x12;
            this.lblWarehouse.Text = "Warehouse";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.cmbWarehouse.Location = new Point(0x60, 0xc0);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(0xf8, 0x15);
            this.cmbWarehouse.TabIndex = 0x13;
            this.cmbSerial.Location = new Point(0x60, 0xa8);
            this.cmbSerial.Name = "cmbSerial";
            this.cmbSerial.Size = new Size(0xf8, 0x15);
            this.cmbSerial.TabIndex = 0x11;
            this.lblSerial.Location = new Point(0, 0xa8);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new Size(0x58, 0x15);
            this.lblSerial.TabIndex = 0x10;
            this.lblSerial.Text = "Serial";
            this.lblSerial.TextAlign = ContentAlignment.MiddleRight;
            this.btnReorder.Location = new Point(0x1a0, 0x30);
            this.btnReorder.Name = "btnReorder";
            this.btnReorder.Size = new Size(80, 0x15);
            this.btnReorder.TabIndex = 0x13;
            this.btnReorder.Text = "Reorder";
            this.ControlCmnID1.Location = new Point(240, 0);
            this.ControlCmnID1.Margin = new Padding(0);
            this.ControlCmnID1.Name = "ControlCmnID1";
            this.ControlCmnID1.Size = new Size(0x108, 0x15);
            this.ControlCmnID1.TabIndex = 14;
            this.pnlBottom.Controls.Add(this.lblAuthorizationExpirationDate);
            this.pnlBottom.Controls.Add(this.dtbAuthorizationExpirationDate);
            this.pnlBottom.Controls.Add(this.txtDXPointer10);
            this.pnlBottom.Controls.Add(this.txtDXPointer9);
            this.pnlBottom.Controls.Add(this.lblDXPointer10);
            this.pnlBottom.Controls.Add(this.txtAuthorizationNumber);
            this.pnlBottom.Controls.Add(this.chbMedicallyUnnecessary);
            this.pnlBottom.Controls.Add(this.lblDXPointer9);
            this.pnlBottom.Controls.Add(this.lblAuthorizationNumber);
            this.pnlBottom.Controls.Add(this.lblAuthorizationType);
            this.pnlBottom.Controls.Add(this.cmbAuthorizationType);
            this.pnlBottom.Controls.Add(this.lblReasonForPickup);
            this.pnlBottom.Controls.Add(this.lblSpecialCode);
            this.pnlBottom.Controls.Add(this.lblReviewCode);
            this.pnlBottom.Controls.Add(this.chbSendCMN_RX_w_invoice);
            this.pnlBottom.Controls.Add(this.txtSpecialCode);
            this.pnlBottom.Controls.Add(this.txtReasonForPickup);
            this.pnlBottom.Controls.Add(this.txtReviewCode);
            this.pnlBottom.Controls.Add(this.ControlCmnID1);
            this.pnlBottom.Dock = DockStyle.Fill;
            this.pnlBottom.Location = new Point(4, 0x174);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new Size(0x1fc, 0xab);
            this.pnlBottom.TabIndex = 2;
            this.txtDXPointer10.AcceptsReturn = true;
            this.txtDXPointer10.Location = new Point(0x60, 0x18);
            this.txtDXPointer10.MaxLength = 0;
            this.txtDXPointer10.Name = "txtDXPointer10";
            this.txtDXPointer10.RightToLeft = RightToLeft.No;
            this.txtDXPointer10.Size = new Size(0x80, 20);
            this.txtDXPointer10.TabIndex = 3;
            this.txtDXPointer9.AcceptsReturn = true;
            this.txtDXPointer9.Location = new Point(0x60, 0);
            this.txtDXPointer9.MaxLength = 0;
            this.txtDXPointer9.Name = "txtDXPointer9";
            this.txtDXPointer9.RightToLeft = RightToLeft.No;
            this.txtDXPointer9.Size = new Size(0x80, 20);
            this.txtDXPointer9.TabIndex = 1;
            this.lblDXPointer10.Location = new Point(0, 0x18);
            this.lblDXPointer10.Name = "lblDXPointer10";
            this.lblDXPointer10.Size = new Size(0x58, 0x15);
            this.lblDXPointer10.TabIndex = 2;
            this.lblDXPointer10.Text = "DX Pointer 10";
            this.lblDXPointer10.TextAlign = ContentAlignment.MiddleRight;
            this.lblState.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.lblState.Location = new Point(120, 8);
            this.lblState.Name = "lblState";
            this.lblState.Size = new Size(100, 0x17);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "State";
            this.lblState.TextAlign = ContentAlignment.MiddleCenter;
            this.Panel1.Controls.Add(this.lblReturned);
            this.Panel1.Controls.Add(this.chbMissingInformation);
            this.Panel1.Controls.Add(this.lblState);
            this.Panel1.Controls.Add(this.btnOK);
            this.Panel1.Controls.Add(this.btnCancel);
            this.Panel1.Dock = DockStyle.Bottom;
            this.Panel1.Location = new Point(4, 0x21f);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x1fc, 0x24);
            this.Panel1.TabIndex = 5;
            this.Panel1.Controls.SetChildIndex(this.btnCancel, 0);
            this.Panel1.Controls.SetChildIndex(this.btnOK, 0);
            this.Panel1.Controls.SetChildIndex(this.lblState, 0);
            this.Panel1.Controls.SetChildIndex(this.chbMissingInformation, 0);
            this.Panel1.Controls.SetChildIndex(this.lblReturned, 0);
            this.pnlPricing.Controls.Add(this.btnReorder);
            this.pnlPricing.Controls.Add(this.lblQty);
            this.pnlPricing.Controls.Add(this.lblUnits);
            this.pnlPricing.Controls.Add(this.lblWhen);
            this.pnlPricing.Controls.Add(this.lblBilled);
            this.pnlPricing.Controls.Add(this.lblOrdered);
            this.pnlPricing.Controls.Add(this.cmbOrderedWhen);
            this.pnlPricing.Controls.Add(this.cmbBilledWhen);
            this.pnlPricing.Controls.Add(this.nmbOrderedQuantity);
            this.pnlPricing.Controls.Add(this.nmbBilledQuantity);
            this.pnlPricing.Controls.Add(this.lblConverter);
            this.pnlPricing.Controls.Add(this.nmbBilledConverter);
            this.pnlPricing.Controls.Add(this.nmbOrderedConverter);
            this.pnlPricing.Controls.Add(this.lblDelivery);
            this.pnlPricing.Controls.Add(this.txtOrderedUnits);
            this.pnlPricing.Controls.Add(this.txtBilledUnits);
            this.pnlPricing.Controls.Add(this.txtDeliveryUnits);
            this.pnlPricing.Controls.Add(this.nmbDeliveryQuantity);
            this.pnlPricing.Controls.Add(this.nmbDeliveryConverter);
            this.pnlPricing.Controls.Add(this.btnRefresh);
            this.pnlPricing.Controls.Add(this.btnGotoPricing);
            this.pnlPricing.Dock = DockStyle.Top;
            this.pnlPricing.Location = new Point(4, 0x110);
            this.pnlPricing.Name = "pnlPricing";
            this.pnlPricing.Size = new Size(0x1fc, 100);
            this.pnlPricing.TabIndex = 1;
            base.AcceptButton = null;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = null;
            base.ClientSize = new Size(0x204, 0x247);
            base.Controls.Add(this.pnlBottom);
            base.Controls.Add(this.Panel1);
            base.Controls.Add(this.pnlPricing);
            base.Controls.Add(this.pnlTop);
            base.Name = "FormOrderDetailBase";
            base.Padding = new Padding(4);
            this.Text = "Order Detail Base";
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            ((ISupportInitialize) this.ValidationWarnings).EndInit();
            this.gbDOS1.ResumeLayout(false);
            this.gbDOS1.PerformLayout();
            this.gbBillItemOn.ResumeLayout(false);
            ((ISupportInitialize) this.MissingProvider).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.pnlPricing.ResumeLayout(false);
            this.pnlPricing.PerformLayout();
            base.ResumeLayout(false);
        }

        protected virtual void InitializeMirHelper(FormMirHelper Helper)
        {
            Helper.Add("InventoryItemID", this.txtInventoryItem, "Inventory Item is required for billing");
            Helper.Add("PriceCodeID", this.txtPriceCode, "Price Code is required for invoice");
            Helper.Add("SaleRentType", this.cmbSaleRentType, "Valid Sale/Rent type is required for billing");
            Helper.Add("OrderedQuantity", this.nmbOrderedQuantity, "Ordered quantity is required for billing");
            Helper.Add("OrderedUnits", this.txtOrderedUnits, "Ordered units is required for billing");
            Helper.Add("OrderedWhen", this.cmbOrderedWhen, "Ordered when is required for billing");
            Helper.Add("OrderedConverter", this.nmbOrderedConverter, "Ordered converter is required for billing");
            Helper.Add("BilledQuantity", this.nmbBilledQuantity, "Billed quantity is required for billing");
            Helper.Add("BilledUnits", this.txtBilledUnits, "Billed units is required for billing");
            Helper.Add("BilledWhen", this.cmbBilledWhen, "Billed when is required for billing");
            Helper.Add("BilledConverter", this.nmbBilledConverter, "Billed converter is required for billing");
            Helper.Add("DeliveryQuantity", this.nmbDeliveryQuantity, "Delivery quantity is required for billing");
            Helper.Add("DeliveryUnits", this.txtDeliveryUnits, "Delivery units is required for billing");
            Helper.Add("DeliveryConverter", this.nmbDeliveryConverter, "Delivery converter is required for billing");
            Helper.Add("BillingCode", this.txtBillingCode, "Billing code is required for billing");
            Helper.Add("BillItemOn", this.txtBillingCode, "Bill Item On code is required for billing");
            Helper.Add("DXPointer9", this.txtDXPointer9, "Valid DX Pointer is required for billing");
            Helper.Add("DXPointer10", this.txtDXPointer10, "Valid DX Pointer is required for billing");
            Helper.Add("Modifier1", this.txtModifier1, "Modifier 1 is required for billing");
            Helper.Add("Modifier2", this.txtModifier2, "Modifier 2 is required for billing");
            Helper.Add("Modifier3", this.txtModifier3, "Modifier 3 is required for billing");
            Helper.Add("AuthorizationNumber.Expired", this.txtAuthorizationNumber, "Prior Auth# is expired");
            Helper.Add("AuthorizationNumber.Expires", this.txtAuthorizationNumber, "Prior Auth# is expires");
            Helper.Add("CMNForm.Required", this.ControlCmnID1, "CMN/RX Form is required for billing");
            Helper.Add("CMNForm.RecertificationDate", this.ControlCmnID1, "Recertification date is the required");
            Helper.Add("CMNForm.FormExpired", this.ControlCmnID1, "CMN/RX form is expired");
            Helper.Add("CMNForm.MIR", this.ControlCmnID1, "CMN/RX Form contains missing information");
        }

        protected virtual void InternalLoadPriceListInformation(IDataRecord record)
        {
            this.FSale_AllowablePrice = NullableConvert.ToDouble(record["Sale_AllowablePrice"]);
            this.FSale_BillablePrice = NullableConvert.ToDouble(record["Sale_BillablePrice"]);
            this.FRent_AllowablePrice = NullableConvert.ToDouble(record["Rent_AllowablePrice"]);
            this.FRent_BillablePrice = NullableConvert.ToDouble(record["Rent_BillablePrice"]);
            Functions.SetComboBoxText(this.cmbSaleRentType, record["SaleRentType"]);
            Functions.SetCheckBoxChecked(this.chbFlatRate, record["FlatRate"]);
            Functions.SetCheckBoxChecked(this.chbTaxable, record["Taxable"]);
            Functions.SetNumericBoxValue(this.nmbOrderedQuantity, record["OrderedQuantity"]);
            Functions.SetTextBoxText(this.txtOrderedUnits, record["OrderedUnits"]);
            Functions.SetComboBoxText(this.cmbOrderedWhen, record["OrderedWhen"]);
            Functions.SetNumericBoxValue(this.nmbOrderedConverter, record["OrderedConverter"]);
            Functions.SetTextBoxText(this.txtBilledUnits, record["BilledUnits"]);
            Functions.SetComboBoxText(this.cmbBilledWhen, record["BilledWhen"]);
            Functions.SetNumericBoxValue(this.nmbBilledConverter, record["BilledConverter"]);
            Functions.SetTextBoxText(this.txtDeliveryUnits, record["DeliveryUnits"]);
            Functions.SetNumericBoxValue(this.nmbDeliveryConverter, record["DeliveryConverter"]);
            this.BillItemOn = Convert.ToString(record["BillItemOn"]);
            this.ControlCmnID1.DefaultCmnType = record["DefaultCMNType"] as string;
            Functions.SetTextBoxText(this.txtModifier1, record["Modifier1"]);
            Functions.SetTextBoxText(this.txtModifier2, record["Modifier2"]);
            Functions.SetTextBoxText(this.txtModifier3, record["Modifier3"]);
            Functions.SetTextBoxText(this.txtModifier4, record["Modifier4"]);
        }

        public void InternalShowMissingInformation(bool Show)
        {
            if (Show)
            {
                this.MirHelper.ShowMissingInformation(this.MissingProvider, this.F_MIR);
            }
            else
            {
                this.MirHelper.ShowMissingInformation(this.MissingProvider, "");
            }
        }

        private void lnkDayOfPickup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.cmbSaleRentType.Text = "One Time Rental";
                this.rbLastDayOfThePeriod.Checked = true;
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

        private void lnkLastDayOfTheMonth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.cmbBilledWhen.Text = "Calendar Monthly";
                this.rbLastDayOfThePeriod.Checked = true;
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

        protected void Load_Table_Serial(int? InventoryItemID, int? SerialID)
        {
            new SerialDropdownHelper(InventoryItemID, SerialID).InitDropdown(this.cmbSerial, null);
        }

        public override void LoadComboBoxes()
        {
            Cache.InitDropdown(this.cmbAuthorizationType, "tbl_authorizationtype", null);
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            Cache.InitDialog(this.fdHAO, "tbl_hao", null);
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_orderdetails", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.Fill(table);
                    Functions.LoadComboBoxItems(this.cmbSaleRentType, table, "SaleRentType");
                    Functions.LoadComboBoxItems(this.cmbOrderedWhen, table, "OrderedWhen");
                    Functions.LoadComboBoxItems(this.cmbBilledWhen, table, "BilledWhen");
                }
            }
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlOrderDetailsBase.TableOrderDetailsBase)
            {
                ControlOrderDetailsBase.TableOrderDetailsBase table = (ControlOrderDetailsBase.TableOrderDetailsBase) Row.Table;
                this.BeginUpdate();
                try
                {
                    this.F_MIR = NullableConvert.ToString(Row[table.Col_MIR]);
                    this.FID = NullableConvert.ToInt32(Row[table.Col_ID]);
                    this.FInventoryItemID = NullableConvert.ToInt32(Row[table.Col_InventoryItemID]);
                    this.FPriceCodeID = NullableConvert.ToInt32(Row[table.Col_PriceCodeID]);
                    this.Load_Table_Serial(this.FInventoryItemID, NullableConvert.ToInt32(Row[table.Col_SerialID]));
                    Functions.SetTextBoxText(this.txtInventoryItem, Row[table.Col_InventoryItemName]);
                    Functions.SetTextBoxText(this.txtPriceCode, Row[table.Col_PriceCodeName]);
                    Functions.SetComboBoxText(this.cmbSaleRentType, Row[table.Col_SaleRentType]);
                    Functions.SetComboBoxValue(this.cmbSerial, Row[table.Col_SerialID]);
                    Functions.SetComboBoxValue(this.cmbWarehouse, Row[table.Col_WarehouseID]);
                    Functions.SetDateBoxValue(this.dtbDOSFrom, Row[table.Col_DOSFrom]);
                    Functions.SetDateBoxValue(this.dtbDOSTo, Row[table.Col_DOSTo]);
                    Functions.SetCheckBoxChecked(this.chbShowSpanDates, Row[table.Col_ShowSpanDates]);
                    Functions.SetTextBoxText(this.txtBillingMonth, Row[table.Col_BillingMonth]);
                    Functions.SetNumericBoxValue(this.nmbBillablePrice, Row[table.Col_BillablePrice]);
                    Functions.SetNumericBoxValue(this.nmbAllowablePrice, Row[table.Col_AllowablePrice]);
                    this.FSale_AllowablePrice = NullableConvert.ToDouble(Row[table.Col_Sale_AllowablePrice]);
                    this.FSale_BillablePrice = NullableConvert.ToDouble(Row[table.Col_Sale_BillablePrice]);
                    this.FRent_AllowablePrice = NullableConvert.ToDouble(Row[table.Col_Rent_AllowablePrice]);
                    this.FRent_BillablePrice = NullableConvert.ToDouble(Row[table.Col_Rent_BillablePrice]);
                    Functions.SetCheckBoxChecked(this.chbFlatRate, Row[table.Col_FlatRate]);
                    Functions.SetCheckBoxChecked(this.chbTaxable, Row[table.Col_Taxable]);
                    Functions.SetNumericBoxValue(this.nmbOrderedQuantity, Row[table.Col_OrderedQuantity]);
                    Functions.SetTextBoxText(this.txtOrderedUnits, Row[table.Col_OrderedUnits]);
                    Functions.SetComboBoxText(this.cmbOrderedWhen, Row[table.Col_OrderedWhen]);
                    Functions.SetNumericBoxValue(this.nmbOrderedConverter, Row[table.Col_OrderedConverter]);
                    Functions.SetNumericBoxValue(this.nmbBilledQuantity, Row[table.Col_BilledQuantity]);
                    Functions.SetTextBoxText(this.txtBilledUnits, Row[table.Col_BilledUnits]);
                    Functions.SetComboBoxText(this.cmbBilledWhen, Row[table.Col_BilledWhen]);
                    Functions.SetNumericBoxValue(this.nmbBilledConverter, Row[table.Col_BilledConverter]);
                    Functions.SetNumericBoxValue(this.nmbDeliveryQuantity, Row[table.Col_DeliveryQuantity]);
                    Functions.SetTextBoxText(this.txtDeliveryUnits, Row[table.Col_DeliveryUnits]);
                    Functions.SetNumericBoxValue(this.nmbDeliveryConverter, Row[table.Col_DeliveryConverter]);
                    Functions.SetTextBoxText(this.txtBillingCode, Row[table.Col_BillingCode]);
                    Functions.SetTextBoxText(this.txtModifier1, Row[table.Col_Modifier1]);
                    Functions.SetTextBoxText(this.txtModifier2, Row[table.Col_Modifier2]);
                    Functions.SetTextBoxText(this.txtModifier3, Row[table.Col_Modifier3]);
                    Functions.SetTextBoxText(this.txtModifier4, Row[table.Col_Modifier4]);
                    Functions.SetTextBoxText(this.txtDXPointer9, Row[table.Col_DXPointer9]);
                    Functions.SetTextBoxText(this.txtDXPointer10, Row[table.Col_DXPointer10]);
                    this.BillItemOn = Convert.ToString(Row[table.Col_BillItemOn]);
                    Functions.SetComboBoxValue(this.cmbAuthorizationType, Row[table.Col_AuthorizationTypeID]);
                    Functions.SetTextBoxText(this.txtAuthorizationNumber, Row[table.Col_AuthorizationNumber]);
                    Functions.SetDateBoxValue(this.dtbAuthorizationExpirationDate, Row[table.Col_AuthorizationExpirationDate]);
                    Functions.SetTextBoxText(this.txtReasonForPickup, Row[table.Col_ReasonForPickup]);
                    Functions.SetTextBoxText(this.txtSpecialCode, Row[table.Col_SpecialCode]);
                    Functions.SetTextBoxText(this.txtReviewCode, Row[table.Col_ReviewCode]);
                    Functions.SetCheckBoxChecked(this.chbSendCMN_RX_w_invoice, Row[table.Col_SendCMN_RX_w_invoice]);
                    Functions.SetCheckBoxChecked(this.chbMedicallyUnnecessary, Row[table.Col_MedicallyUnnecessary]);
                    Functions.SetLabelText(this.lblState, Row[table.Col_State]);
                    this.ControlCmnID1.DefaultCmnType = Row[table.Col_DefaultCMNType] as string;
                    this.ControlCmnID1.CustomerID = this.CustomerID;
                    this.ControlCmnID1.OrderID = this.OrderID;
                    this.ControlCmnID1.CmnID = NullableConvert.ToInt32(Row[table.Col_CMNFormID]);
                    Functions.SetTextBoxText(this.txtHaoDescription, Row[table.Col_HaoDescription]);
                    this.lblReturned.Visible = NullableConvert.ToBoolean(Row[table.Col_Returned], false);
                }
                finally
                {
                    this.EndUpdate();
                }
                this.InternalShowMissingInformation(this.chbMissingInformation.Checked & this.chbMissingInformation.Visible);
            }
        }

        private void LoadPriceListInformation(int InventoryItemID, int PriceCodeID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT
  tbl_pricecode.ID   as PriceCodeID
, tbl_pricecode.Name as PriceCodeName
, tbl_inventoryitem.ID   as InventoryItemID
, tbl_inventoryitem.Name as InventoryItemName
, IF(pricing.DefaultOrderType = 'Rental'
    ,IF(IFNULL(pricing.RentalType, '') != '', pricing.RentalType, 'Monthly Rental')
    ,IF(pricing.ReoccuringSale = 0, 'One Time Sale', 'Re-occurring Sale')) as SaleRentType
, IF(pricing.DefaultOrderType = 'Rental', pricing.Rent_AllowablePrice, pricing.Sale_AllowablePrice) as AllowablePrice
, IF(pricing.DefaultOrderType = 'Rental', pricing.Rent_BillablePrice, pricing.Sale_BillablePrice) as BillablePrice
, pricing.Sale_AllowablePrice
, pricing.Rent_AllowablePrice
, pricing.Sale_BillablePrice
, pricing.Rent_BillablePrice
, pricing.DefaultCMNType
, pricing.BillingCode
, pricing.Modifier1
, pricing.Modifier2
, pricing.Modifier3
, pricing.Modifier4
, pricing.FlatRate
, pricing.Taxable
, pricing.OrderedQuantity
, pricing.OrderedUnits
, pricing.OrderedWhen
, pricing.OrderedConverter
, pricing.BilledUnits
, pricing.BilledWhen
, pricing.BilledConverter
, pricing.DeliveryUnits
, pricing.DeliveryConverter
, pricing.BillItemOn
, pricing.BillInsurance as BillIns1
, pricing.BillInsurance as BillIns2
, pricing.BillInsurance as BillIns3
, pricing.BillInsurance as BillIns4
FROM tbl_pricecode_item as pricing
     INNER JOIN tbl_pricecode ON pricing.PriceCodeID = tbl_pricecode.ID
     INNER JOIN tbl_inventoryitem ON pricing.InventoryItemID = tbl_inventoryitem.ID
WHERE (tbl_inventoryitem.ID = {InventoryItemID})
  AND (tbl_pricecode.ID = {PriceCodeID})";
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.InternalLoadPriceListInformation(reader);
                            this.RefreshPricing();
                        }
                    }
                }
            }
        }

        protected virtual void Parent_CustomerIDChanged(object sender, EventArgs e)
        {
            this.ControlCmnID1.CustomerID = this.CustomerID;
        }

        protected virtual void Parent_OrderIDChanged(object sender, EventArgs e)
        {
            this.ControlCmnID1.OrderID = this.OrderID;
        }

        private void RefreshPricing()
        {
            SaleRentTypeEnum enum2 = SaleRentTypeHelper.ToEnum(this.cmbSaleRentType.Text);
            if (SaleRentTypeHelper.IsSale(enum2))
            {
                Functions.SetNumericBoxValue(this.nmbAllowablePrice, this.FSale_AllowablePrice);
                Functions.SetNumericBoxValue(this.nmbBillablePrice, this.FSale_BillablePrice);
            }
            else if (SaleRentTypeHelper.IsRental(enum2))
            {
                Functions.SetNumericBoxValue(this.nmbAllowablePrice, this.FRent_AllowablePrice);
                Functions.SetNumericBoxValue(this.nmbBillablePrice, this.FRent_BillablePrice);
            }
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlOrderDetailsBase.TableOrderDetailsBase)
            {
                ControlOrderDetailsBase.TableOrderDetailsBase table = (ControlOrderDetailsBase.TableOrderDetailsBase) Row.Table;
                Row[table.Col_SaleRentType] = this.cmbSaleRentType.Text;
                Row[table.Col_SerialID] = this.cmbSerial.SelectedValue;
                Row[table.Col_WarehouseID] = this.cmbWarehouse.SelectedValue;
                Row[table.Col_DOSFrom] = Functions.GetDateBoxValue(this.dtbDOSFrom);
                Row[table.Col_DOSTo] = Functions.GetDateBoxValue(this.dtbDOSTo);
                Row[table.Col_ShowSpanDates] = this.chbShowSpanDates.Checked;
                Row[table.Col_BillablePrice] = this.nmbBillablePrice.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_AllowablePrice] = this.nmbAllowablePrice.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_FlatRate] = this.chbFlatRate.Checked;
                Row[table.Col_Taxable] = this.chbTaxable.Checked;
                Row[table.Col_OrderedQuantity] = this.nmbOrderedQuantity.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_OrderedUnits] = this.txtOrderedUnits.Text;
                Row[table.Col_OrderedWhen] = this.cmbOrderedWhen.Text;
                Row[table.Col_OrderedConverter] = this.nmbOrderedConverter.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_BilledQuantity] = this.nmbBilledQuantity.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_BilledUnits] = this.txtBilledUnits.Text;
                Row[table.Col_BilledWhen] = this.cmbBilledWhen.Text;
                Row[table.Col_BilledConverter] = this.nmbBilledConverter.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_DeliveryQuantity] = this.nmbDeliveryQuantity.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_DeliveryUnits] = this.txtDeliveryUnits.Text;
                Row[table.Col_DeliveryConverter] = this.nmbDeliveryConverter.AsDouble.GetValueOrDefault(0.0);
                Row[table.Col_BillingCode] = this.txtBillingCode.Text;
                Row[table.Col_Modifier1] = this.txtModifier1.Text;
                Row[table.Col_Modifier2] = this.txtModifier2.Text;
                Row[table.Col_Modifier3] = this.txtModifier3.Text;
                Row[table.Col_Modifier4] = this.txtModifier4.Text;
                Row[table.Col_DXPointer9] = this.txtDXPointer9.Text;
                Row[table.Col_DXPointer10] = this.txtDXPointer10.Text;
                Row[table.Col_BillItemOn] = this.BillItemOn;
                Row[table.Col_AuthorizationTypeID] = this.cmbAuthorizationType.SelectedValue;
                Row[table.Col_AuthorizationNumber] = this.txtAuthorizationNumber.Text;
                Row[table.Col_AuthorizationExpirationDate] = Functions.GetDateBoxValue(this.dtbAuthorizationExpirationDate);
                Row[table.Col_ReasonForPickup] = this.txtReasonForPickup.Text;
                Row[table.Col_SpecialCode] = this.txtSpecialCode.Text;
                Row[table.Col_ReviewCode] = this.txtReviewCode.Text;
                Row[table.Col_SendCMN_RX_w_invoice] = this.chbSendCMN_RX_w_invoice.Checked;
                Row[table.Col_MedicallyUnnecessary] = this.chbMedicallyUnnecessary.Checked;
                Row[table.Col_CMNFormID] = NullableConvert.ToDb(this.ControlCmnID1.CmnID);
                Row[table.Col_HaoDescription] = this.txtHaoDescription.Text;
            }
        }

        public void ShowMissingInformation(bool Show)
        {
            this.chbMissingInformation.Checked = Show;
        }

        private void UpdateEnabledState()
        {
            bool flag = (base.AllowState & AllowStateEnum.AllowEdit07) == AllowStateEnum.AllowEdit07;
            this.cmbSaleRentType.Enabled = flag;
            this.gbBillItemOn.Enabled = flag;
            this.cmbOrderedWhen.Enabled = flag;
            this.cmbBilledWhen.Enabled = flag;
            this.cmbWarehouse.Enabled = flag | (this.FID == null);
            this.nmbOrderedQuantity.Enabled = flag;
        }

        protected override void ValidateObject()
        {
            if (base.Visible)
            {
                if (NullableConvert.ToInt32(this.cmbWarehouse.SelectedValue) == null)
                {
                    this.ValidationErrors.SetError(this.cmbWarehouse, "You must select warehouse");
                }
                else
                {
                    this.ValidationErrors.SetError(this.cmbWarehouse, "");
                }
                if (NullableConvert.ToDateTime(this.dtbDOSFrom.Value) == null)
                {
                    this.ValidationErrors.SetError(this.dtbDOSFrom, "You must select Date Of Service From");
                }
                else
                {
                    this.ValidationErrors.SetError(this.dtbDOSFrom, "");
                }
                Actualizer actualizer = new Actualizer(this.cmbSaleRentType.Text, this.BillItemOn, this.cmbOrderedWhen.Text, this.cmbBilledWhen.Text);
                this.ValidationErrors.SetError(this.cmbSaleRentType, actualizer.Error_SaleRentType);
                this.ValidationErrors.SetError(this.gbBillItemOn, actualizer.Error_BillItemOn);
                this.ValidationErrors.SetError(this.cmbOrderedWhen, actualizer.Error_OrderedWhen);
                this.ValidationErrors.SetError(this.cmbBilledWhen, actualizer.Error_BilledWhen);
                if (!string.IsNullOrWhiteSpace(this.txtAuthorizationNumber.Text) && !(Functions.GetDateBoxValue(this.dtbAuthorizationExpirationDate) is DateTime))
                {
                    this.ValidationWarnings.SetError(this.dtbAuthorizationExpirationDate, "Authorization Expiration Date is needed for notifications");
                }
                else
                {
                    this.ValidationWarnings.SetError(this.dtbAuthorizationExpirationDate, "");
                }
            }
        }

        [field: AccessedThroughProperty("lblPriceCode")]
        protected virtual Label lblPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryItem")]
        protected virtual Label lblInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbFlatRate")]
        protected virtual CheckBox chbFlatRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbTaxable")]
        protected virtual CheckBox chbTaxable { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lbllBillCode")]
        protected virtual Label lbllBillCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMod")]
        protected virtual Label lblMod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDXPointer9")]
        protected virtual Label lblDXPointer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAllowablePrice")]
        protected virtual Label lblAllowablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillablePrice")]
        protected virtual Label lblBillablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbSaleRentType")]
        protected virtual ComboBox cmbSaleRentType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSaleRentType")]
        protected virtual Label lblSaleRentType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbShowSpanDates")]
        protected virtual CheckBox chbShowSpanDates { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBillingCode")]
        protected virtual TextBox txtBillingCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier1")]
        protected virtual TextBox txtModifier1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier2")]
        protected virtual TextBox txtModifier2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier3")]
        protected virtual TextBox txtModifier3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier4")]
        protected virtual TextBox txtModifier4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbBillItemOn")]
        protected virtual GroupBox gbBillItemOn { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lnkDayOfPickup")]
        protected virtual LinkLabel lnkDayOfPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbLastDayOfThePeriod")]
        protected virtual RadioButton rbLastDayOfThePeriod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbDayOfDelivery")]
        protected virtual RadioButton rbDayOfDelivery { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSpecialCode")]
        protected virtual TextBox txtSpecialCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtReasonForPickup")]
        protected virtual TextBox txtReasonForPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSendCMN_RX_w_invoice")]
        protected virtual CheckBox chbSendCMN_RX_w_invoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtReviewCode")]
        protected virtual TextBox txtReviewCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbMedicallyUnnecessary")]
        protected virtual CheckBox chbMedicallyUnnecessary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReasonForPickup")]
        protected virtual Label lblReasonForPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSpecialCode")]
        protected virtual Label lblSpecialCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReviewCode")]
        protected virtual Label lblReviewCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDOSTo")]
        protected virtual UltraDateTimeEditor dtbDOSTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDOSFrom")]
        protected virtual UltraDateTimeEditor dtbDOSFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDOSTo")]
        protected virtual Label lblDOSTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDOSFrom")]
        protected virtual Label lblDOSFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBillablePrice")]
        protected virtual NumericBox nmbBillablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAllowablePrice")]
        protected virtual NumericBox nmbAllowablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolTip1")]
        protected virtual ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("lblHaoDescription")]
        protected virtual Label lblHaoDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("MissingProvider")]
        protected virtual ErrorProvider MissingProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbMissingInformation")]
        protected virtual CheckBox chbMissingInformation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPriceCode")]
        protected virtual TextBox txtPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInventoryItem")]
        protected virtual TextBox txtInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReturned")]
        protected virtual Label lblReturned { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbDOS1")]
        protected virtual GroupBox gbDOS1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlTop")]
        protected virtual Panel pnlTop { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlBottom")]
        protected virtual Panel pnlBottom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrdered")]
        protected virtual Label lblOrdered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQty")]
        protected virtual Label lblQty { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWhen")]
        protected virtual Label lblWhen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUnits")]
        protected virtual Label lblUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBilledUnits")]
        protected virtual TextBox txtBilledUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOrderedUnits")]
        protected virtual TextBox txtOrderedUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbOrderedWhen")]
        protected virtual ComboBox cmbOrderedWhen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBilledWhen")]
        protected virtual ComboBox cmbBilledWhen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbOrderedQuantity")]
        protected virtual NumericBox nmbOrderedQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBilledQuantity")]
        protected virtual NumericBox nmbBilledQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBilled")]
        protected virtual Label lblBilled { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbOrderedConverter")]
        protected virtual NumericBox nmbOrderedConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBilledConverter")]
        protected virtual NumericBox nmbBilledConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblConverter")]
        protected virtual Label lblConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDeliveryConverter")]
        protected virtual NumericBox nmbDeliveryConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDeliveryQuantity")]
        protected virtual NumericBox nmbDeliveryQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDeliveryUnits")]
        protected virtual TextBox txtDeliveryUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDelivery")]
        protected virtual Label lblDelivery { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnRefresh")]
        protected virtual Button btnRefresh { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnGotoPricing")]
        protected virtual Button btnGotoPricing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbSerial")]
        protected virtual Combobox cmbSerial { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSerial")]
        protected virtual Label lblSerial { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillingMonth")]
        protected virtual Label lblBillingMonth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBillingMonth")]
        protected virtual TextBox txtBillingMonth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblState")]
        protected virtual Label lblState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        protected virtual Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lnkLastDayOfTheMonth")]
        protected virtual LinkLabel lnkLastDayOfTheMonth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlCmnID1")]
        protected virtual ControlCmnID ControlCmnID1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnReorder")]
        protected virtual Button btnReorder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        protected virtual Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDXPointer10")]
        protected virtual Label lblDXPointer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDXPointer10")]
        protected virtual TextBox txtDXPointer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDXPointer9")]
        protected virtual TextBox txtDXPointer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        protected virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlPricing")]
        protected virtual Panel pnlPricing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtHaoDescription")]
        protected virtual TextBox txtHaoDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("fdHAO")]
        protected virtual FindDialog fdHAO { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnFindHao")]
        protected virtual Button btnFindHao { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAuthorizationExpirationDate")]
        protected virtual Label lblAuthorizationExpirationDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbAuthorizationExpirationDate")]
        protected virtual UltraDateTimeEditor dtbAuthorizationExpirationDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override AllowStateEnum AllowState
        {
            get => 
                base.AllowState;
            set
            {
                base.AllowState = value;
                this.UpdateEnabledState();
            }
        }

        protected string BillItemOn
        {
            get => 
                !this.rbDayOfDelivery.Checked ? (!this.rbLastDayOfThePeriod.Checked ? string.Empty : "Last day of the Period") : "Day of Delivery";
            set
            {
                StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
                this.rbDayOfDelivery.Checked = ordinalIgnoreCase.Equals(value, "Day of Delivery");
                this.rbLastDayOfThePeriod.Checked = ordinalIgnoreCase.Equals(value, "Last day of the Period");
            }
        }

        private int? CustomerID
        {
            get
            {
                ControlOrderDetailsBase base2 = base.F_Parent as ControlOrderDetailsBase;
                return base2?.CustomerID;
            }
        }

        private int? OrderID
        {
            get
            {
                ControlOrderDetailsBase base2 = base.F_Parent as ControlOrderDetailsBase;
                return base2?.OrderID;
            }
        }

        public FormMirHelper MirHelper
        {
            get
            {
                if (this.F_MirHelper == null)
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.InitializeMirHelper(this.F_MirHelper);
                }
                return this.F_MirHelper;
            }
        }

        private class SerialDropdownHelper : DropdownHelperBase
        {
            private readonly int? InventoryItemID;
            private readonly int? SerialID;

            public SerialDropdownHelper(int? InventoryItemID, int? SerialID)
            {
                this.InventoryItemID = InventoryItemID;
                this.SerialID = SerialID;
            }

            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "SerialNumber", "ID");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "SerialNumber", "ID");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormSerial(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormSerial(), CreateHash_New(source));
            }

            public override DataTable GetTable()
            {
                DataTable table = new DataTable("tbl_serial");
                Cache.LoadTable(table, this.Query(), true);
                return table;
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select serial";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("ID", "#", 50);
                e.Appearance.AddTextColumn("SerialNumber", "Serial #", 240);
            }

            private string Query()
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("SELECT ID, SerialNumber");
                builder.AppendLine("FROM tbl_serial");
                builder.AppendLine("WHERE (1 != 1)");
                if (this.InventoryItemID != null)
                {
                    builder.AppendFormat("   OR ((Status = 'On Hand') AND (InventoryItemID = {0}))", this.InventoryItemID.Value);
                    builder.AppendLine();
                }
                if (this.SerialID != null)
                {
                    builder.AppendFormat("   OR (ID = {0})", this.SerialID.Value);
                    builder.AppendLine();
                }
                return builder.ToString();
            }
        }
    }
}

