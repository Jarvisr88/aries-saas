namespace DMEWorks.Details
{
    using DMEWorks.Billing;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormOrderDetail : FormOrderDetailBase
    {
        private IContainer components;
        private int UpdateCount;
        private bool Calculating;

        public FormOrderDetail() : this(null)
        {
        }

        public FormOrderDetail(ControlOrderDetails Parent) : base(Parent)
        {
            this.UpdateCount = 0;
            this.Calculating = false;
            this.InitializeComponent();
            this.txtBillingMonth.ReadOnly = !Permissions.FormOrder_ChangeBillingMonth.Allow_ADD_EDIT;
        }

        protected override void BeginUpdate()
        {
            if (0 <= this.UpdateCount)
            {
                ref int numRef;
                *(numRef = ref this.UpdateCount) = numRef + 1;
            }
            else
            {
                this.UpdateCount = 1;
            }
        }

        private void CalculateQuantities()
        {
            if ((0 >= this.UpdateCount) && !this.Calculating)
            {
                this.Calculating = true;
                try
                {
                    Actualizer actualizer;
                    if ((base.AllowState & AllowStateEnum.AllowEdit07) == AllowStateEnum.AllowEdit07)
                    {
                        actualizer = new Actualizer(this.cmbSaleRentType.Text, base.BillItemOn, this.cmbOrderedWhen.Text, this.cmbBilledWhen.Text);
                        if (!SaleRentTypeHelper.IsSale(actualizer.ActualSaleRentType))
                        {
                            this.nmbDeliveryQuantity.AsDouble = this.nmbOrderedQuantity.AsDouble;
                            this.nmbBilledQuantity.AsDouble = this.nmbOrderedQuantity.AsDouble;
                            goto TR_0005;
                        }
                        else
                        {
                            try
                            {
                                double? asDouble = this.nmbBilledConverter.AsDouble;
                                this.nmbDeliveryQuantity.AsDouble = new double?(Converter.OrderedQty2DeliveredQty(this.dtbDOSFrom.DateTime, this.dtbDOSTo.DateTime, this.nmbOrderedQuantity.AsDouble.GetValueOrDefault(0.0), actualizer.ActualOrderedWhen, actualizer.ActualBilledWhen, this.nmbOrderedConverter.AsDouble.GetValueOrDefault(0.0), this.nmbDeliveryConverter.AsDouble.GetValueOrDefault(0.0), asDouble.GetValueOrDefault(0.0)));
                            }
                            catch (Exception exception1)
                            {
                                ProjectData.SetProjectError(exception1);
                                this.nmbDeliveryQuantity.AsDouble = 0.0;
                                ProjectData.ClearProjectError();
                            }
                        }
                        goto TR_0008;
                    }
                    return;
                TR_0005:
                    if (actualizer.ActualBilledWhen != BilledWhenEnum.Custom)
                    {
                        try
                        {
                            object obj2 = this.dtbDOSFrom.Value;
                            object obj3 = !(obj2 is DateTime) ? ((object) DBNull.Value) : ((object) actualizer.ActualDosTo(Conversions.ToDate(obj2), Conversions.ToDate(obj2)));
                            Functions.SetDateBoxValue(this.dtbDOSTo, obj3);
                        }
                        catch (Exception exception3)
                        {
                            Exception ex = exception3;
                            ProjectData.SetProjectError(ex);
                            Exception exception = ex;
                            Functions.SetDateBoxValue(this.dtbDOSTo, DateTime.Today);
                            ProjectData.ClearProjectError();
                        }
                    }
                    return;
                TR_0008:
                    try
                    {
                        this.nmbBilledQuantity.AsDouble = new double?(Converter.OrderedQty2BilledQty(this.dtbDOSFrom.DateTime, this.dtbDOSTo.DateTime, this.nmbOrderedQuantity.AsDouble.GetValueOrDefault(0.0), actualizer.ActualOrderedWhen, actualizer.ActualBilledWhen, this.nmbOrderedConverter.AsDouble.GetValueOrDefault(0.0), this.nmbDeliveryConverter.AsDouble.GetValueOrDefault(0.0), this.nmbBilledConverter.AsDouble.GetValueOrDefault(0.0)));
                    }
                    catch (Exception exception2)
                    {
                        ProjectData.SetProjectError(exception2);
                        this.nmbBilledQuantity.AsDouble = 0.0;
                        ProjectData.ClearProjectError();
                    }
                    goto TR_0005;
                }
                finally
                {
                    this.Calculating = false;
                }
            }
        }

        protected override void Clear()
        {
            base.Clear();
            Functions.SetCheckBoxChecked(this.chbAcceptAssignment, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns1, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns2, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns3, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillIns4, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbNopayIns1, false);
            Functions.SetDateBoxValue(this.dtbEndDate, DBNull.Value);
            this.expDrugIdentification.Collapsed = true;
            Functions.SetTextBoxText(this.txtDrugNoteField, DBNull.Value);
            Functions.SetTextBoxText(this.txtDrugControlNumber, DBNull.Value);
            this.expUserFields.Collapsed = true;
            Functions.SetTextBoxText(this.txtUserField1, DBNull.Value);
            Functions.SetTextBoxText(this.txtUserField2, DBNull.Value);
        }

        private void cmbBilledWhen_TextChanged(object sender, EventArgs e)
        {
            this.CalculateQuantities();
        }

        private void cmbOrderedWhen_TextChanged(object sender, EventArgs e)
        {
            this.CalculateQuantities();
        }

        private void cmbSaleRentType_TextChanged(object sender, EventArgs e)
        {
            this.CalculateQuantities();
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

        private void dtbDOSFrom_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateQuantities();
        }

        private void dtbDOSTo_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateQuantities();
        }

        protected override void EndUpdate()
        {
            if (0 >= this.UpdateCount)
            {
                this.UpdateCount = 0;
            }
            else
            {
                ref int numRef;
                *(numRef = ref this.UpdateCount) = numRef - 1;
                if (this.UpdateCount == 0)
                {
                    this.CalculateQuantities();
                }
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.chbBillIns4 = new CheckBox();
            this.chbBillIns3 = new CheckBox();
            this.chbBillIns2 = new CheckBox();
            this.chbBillIns1 = new CheckBox();
            this.dtbEndDate = new UltraDateTimeEditor();
            this.lblEndDate = new Label();
            this.chbAcceptAssignment = new CheckBox();
            this.pnlDrugIdentification = new Panel();
            this.txtDrugControlNumber = new TextBox();
            this.txtDrugNoteField = new TextBox();
            this.lblDrugControlNumber = new Label();
            this.lblDrugNoteField = new Label();
            this.chbNopayIns1 = new CheckBox();
            this.lblBilling = new Label();
            this.expDrugIdentification = new Expander();
            this.pnlUserFields = new Panel();
            this.txtUserField2 = new TextBox();
            this.txtUserField1 = new TextBox();
            this.lblUserField2 = new Label();
            this.lblUserField1 = new Label();
            this.expUserFields = new Expander();
            this.gbBillItemOn.SuspendLayout();
            ((ISupportInitialize) this.MissingProvider).BeginInit();
            this.gbDOS1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlPricing.SuspendLayout();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            ((ISupportInitialize) this.ValidationWarnings).BeginInit();
            this.pnlDrugIdentification.SuspendLayout();
            this.pnlUserFields.SuspendLayout();
            base.SuspendLayout();
            this.MissingProvider.SetIconPadding(this.cmbSaleRentType, -16);
            this.MissingProvider.SetIconPadding(this.txtBillingCode, -16);
            this.MissingProvider.SetIconPadding(this.txtModifier1, -16);
            this.MissingProvider.SetIconPadding(this.txtModifier2, -16);
            this.MissingProvider.SetIconPadding(this.txtModifier3, -16);
            this.txtReasonForPickup.Size = new Size(0x98, 20);
            this.ToolTip1.SetToolTip(this.txtReasonForPickup, "ReasonForPickup");
            this.MissingProvider.SetIconPadding(this.txtAuthorizationNumber, -16);
            this.MissingProvider.SetIconAlignment(this.chbMissingInformation, ErrorIconAlignment.MiddleRight);
            this.MissingProvider.SetIconAlignment(this.chbMissingInformation, ErrorIconAlignment.MiddleLeft);
            this.MissingProvider.SetIconPadding(this.chbMissingInformation, 0);
            this.MissingProvider.SetIconPadding(this.chbMissingInformation, -16);
            this.MissingProvider.SetIconPadding(this.txtPriceCode, -16);
            this.MissingProvider.SetIconPadding(this.txtPriceCode, 0);
            this.MissingProvider.SetIconPadding(this.txtInventoryItem, -16);
            this.MissingProvider.SetIconPadding(this.txtInventoryItem, 0);
            this.pnlTop.Controls.Add(this.lblBilling);
            this.pnlTop.Controls.Add(this.chbNopayIns1);
            this.pnlTop.Controls.Add(this.chbBillIns4);
            this.pnlTop.Controls.Add(this.chbBillIns3);
            this.pnlTop.Controls.Add(this.chbBillIns2);
            this.pnlTop.Controls.Add(this.chbBillIns1);
            this.pnlTop.Size = new Size(0x1fc, 0x120);
            this.pnlTop.Controls.SetChildIndex(this.btnFindHao, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtInventoryItem, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtPriceCode, 0);
            this.pnlTop.Controls.SetChildIndex(this.chbBillIns1, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblSaleRentType, 0);
            this.pnlTop.Controls.SetChildIndex(this.chbBillIns2, 0);
            this.pnlTop.Controls.SetChildIndex(this.cmbSaleRentType, 0);
            this.pnlTop.Controls.SetChildIndex(this.chbBillIns3, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblInventoryItem, 0);
            this.pnlTop.Controls.SetChildIndex(this.chbBillIns4, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtModifier1, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblPriceCode, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtHaoDescription, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblHaoDescription, 0);
            this.pnlTop.Controls.SetChildIndex(this.chbNopayIns1, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtBillingCode, 0);
            this.pnlTop.Controls.SetChildIndex(this.gbDOS1, 0);
            this.pnlTop.Controls.SetChildIndex(this.nmbAllowablePrice, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtModifier4, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblAllowablePrice, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblMod, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblBillablePrice, 0);
            this.pnlTop.Controls.SetChildIndex(this.lbllBillCode, 0);
            this.pnlTop.Controls.SetChildIndex(this.nmbBillablePrice, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtModifier3, 0);
            this.pnlTop.Controls.SetChildIndex(this.chbTaxable, 0);
            this.pnlTop.Controls.SetChildIndex(this.chbFlatRate, 0);
            this.pnlTop.Controls.SetChildIndex(this.txtModifier2, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblSerial, 0);
            this.pnlTop.Controls.SetChildIndex(this.cmbSerial, 0);
            this.pnlTop.Controls.SetChildIndex(this.cmbWarehouse, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblWarehouse, 0);
            this.pnlTop.Controls.SetChildIndex(this.gbBillItemOn, 0);
            this.pnlTop.Controls.SetChildIndex(this.lblBilling, 0);
            this.pnlBottom.Controls.Add(this.chbAcceptAssignment);
            this.pnlBottom.Controls.Add(this.lblEndDate);
            this.pnlBottom.Controls.Add(this.dtbEndDate);
            this.pnlBottom.Dock = DockStyle.Top;
            this.pnlBottom.Location = new Point(4, 0x188);
            this.pnlBottom.Size = new Size(0x1fc, 0xa8);
            this.pnlBottom.Controls.SetChildIndex(this.dtbAuthorizationExpirationDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblAuthorizationExpirationDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.dtbEndDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblEndDate, 0);
            this.pnlBottom.Controls.SetChildIndex(this.chbAcceptAssignment, 0);
            this.pnlBottom.Controls.SetChildIndex(this.ControlCmnID1, 0);
            this.pnlBottom.Controls.SetChildIndex(this.txtReviewCode, 0);
            this.pnlBottom.Controls.SetChildIndex(this.txtReasonForPickup, 0);
            this.pnlBottom.Controls.SetChildIndex(this.txtSpecialCode, 0);
            this.pnlBottom.Controls.SetChildIndex(this.chbSendCMN_RX_w_invoice, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblReviewCode, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblSpecialCode, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblReasonForPickup, 0);
            this.pnlBottom.Controls.SetChildIndex(this.cmbAuthorizationType, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblAuthorizationType, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblAuthorizationNumber, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblDXPointer9, 0);
            this.pnlBottom.Controls.SetChildIndex(this.chbMedicallyUnnecessary, 0);
            this.pnlBottom.Controls.SetChildIndex(this.txtAuthorizationNumber, 0);
            this.pnlBottom.Controls.SetChildIndex(this.lblDXPointer10, 0);
            this.pnlBottom.Controls.SetChildIndex(this.txtDXPointer9, 0);
            this.pnlBottom.Controls.SetChildIndex(this.txtDXPointer10, 0);
            this.MissingProvider.SetIconPadding(this.txtBilledUnits, -16);
            this.MissingProvider.SetIconPadding(this.txtBilledUnits, 0);
            this.MissingProvider.SetIconPadding(this.txtOrderedUnits, -16);
            this.MissingProvider.SetIconPadding(this.txtOrderedUnits, 0);
            this.MissingProvider.SetIconPadding(this.cmbOrderedWhen, -16);
            this.MissingProvider.SetIconPadding(this.cmbOrderedWhen, 0);
            this.MissingProvider.SetIconPadding(this.cmbBilledWhen, -16);
            this.MissingProvider.SetIconPadding(this.cmbBilledWhen, 0);
            this.MissingProvider.SetIconPadding(this.nmbOrderedQuantity, 0);
            this.MissingProvider.SetIconPadding(this.nmbOrderedQuantity, -16);
            this.MissingProvider.SetIconPadding(this.nmbBilledQuantity, 0);
            this.MissingProvider.SetIconPadding(this.nmbBilledQuantity, -16);
            this.MissingProvider.SetIconPadding(this.nmbOrderedConverter, 0);
            this.MissingProvider.SetIconPadding(this.nmbOrderedConverter, -16);
            this.MissingProvider.SetIconPadding(this.nmbBilledConverter, 0);
            this.MissingProvider.SetIconPadding(this.nmbBilledConverter, -16);
            this.MissingProvider.SetIconPadding(this.nmbDeliveryConverter, 0);
            this.MissingProvider.SetIconPadding(this.nmbDeliveryConverter, -16);
            this.MissingProvider.SetIconPadding(this.nmbDeliveryQuantity, 0);
            this.MissingProvider.SetIconPadding(this.nmbDeliveryQuantity, -16);
            this.MissingProvider.SetIconPadding(this.txtDeliveryUnits, -16);
            this.MissingProvider.SetIconPadding(this.txtDeliveryUnits, 0);
            this.MissingProvider.SetIconPadding(this.ControlCmnID1, -16);
            this.MissingProvider.SetIconPadding(this.ControlCmnID1, 0);
            this.MissingProvider.SetIconPadding(this.txtDXPointer10, -16);
            this.MissingProvider.SetIconPadding(this.txtDXPointer10, 0);
            this.MissingProvider.SetIconPadding(this.txtDXPointer9, -16);
            this.MissingProvider.SetIconPadding(this.txtDXPointer9, 0);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(4, 0x290);
            this.Panel1.TabIndex = 9;
            this.pnlPricing.Location = new Point(4, 0x124);
            this.chbBillIns4.Location = new Point(0x108, 0x108);
            this.chbBillIns4.Name = "chbBillIns4";
            this.chbBillIns4.Size = new Size(0x34, 0x15);
            this.chbBillIns4.TabIndex = 0x20;
            this.chbBillIns4.Text = "Ins 4";
            this.chbBillIns3.Location = new Point(0xd0, 0x108);
            this.chbBillIns3.Name = "chbBillIns3";
            this.chbBillIns3.Size = new Size(0x34, 0x15);
            this.chbBillIns3.TabIndex = 0x1f;
            this.chbBillIns3.Text = "Ins 3";
            this.chbBillIns2.Location = new Point(0x98, 0x108);
            this.chbBillIns2.Name = "chbBillIns2";
            this.chbBillIns2.Size = new Size(0x34, 0x15);
            this.chbBillIns2.TabIndex = 30;
            this.chbBillIns2.Text = "Ins 2";
            this.chbBillIns1.Location = new Point(0x60, 0x108);
            this.chbBillIns1.Name = "chbBillIns1";
            this.chbBillIns1.Size = new Size(0x34, 0x15);
            this.chbBillIns1.TabIndex = 0x1d;
            this.chbBillIns1.Text = "Ins 1";
            this.dtbEndDate.Location = new Point(0x158, 120);
            this.dtbEndDate.Name = "dtbEndDate";
            this.dtbEndDate.Size = new Size(0x60, 0x15);
            this.dtbEndDate.TabIndex = 0x13;
            this.lblEndDate.Location = new Point(0xe8, 120);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new Size(0x68, 0x15);
            this.lblEndDate.TabIndex = 0x12;
            this.lblEndDate.Text = "End Date";
            this.lblEndDate.TextAlign = ContentAlignment.MiddleRight;
            this.chbAcceptAssignment.Location = new Point(240, 0x48);
            this.chbAcceptAssignment.Name = "chbAcceptAssignment";
            this.chbAcceptAssignment.Size = new Size(0x108, 0x15);
            this.chbAcceptAssignment.TabIndex = 0x11;
            this.chbAcceptAssignment.Text = "Accept Assignment";
            this.pnlDrugIdentification.Controls.Add(this.txtDrugControlNumber);
            this.pnlDrugIdentification.Controls.Add(this.txtDrugNoteField);
            this.pnlDrugIdentification.Controls.Add(this.lblDrugControlNumber);
            this.pnlDrugIdentification.Controls.Add(this.lblDrugNoteField);
            this.pnlDrugIdentification.Dock = DockStyle.Top;
            this.pnlDrugIdentification.Location = new Point(4, 0x248);
            this.pnlDrugIdentification.Name = "pnlDrugIdentification";
            this.pnlDrugIdentification.Size = new Size(0x1fc, 0x18);
            this.pnlDrugIdentification.TabIndex = 4;
            this.txtDrugControlNumber.Location = new Point(320, 0);
            this.txtDrugControlNumber.Name = "txtDrugControlNumber";
            this.txtDrugControlNumber.Size = new Size(0xb8, 20);
            this.txtDrugControlNumber.TabIndex = 3;
            this.txtDrugNoteField.Location = new Point(80, 0);
            this.txtDrugNoteField.Name = "txtDrugNoteField";
            this.txtDrugNoteField.Size = new Size(0xa8, 20);
            this.txtDrugNoteField.TabIndex = 1;
            this.lblDrugControlNumber.Location = new Point(0x100, 0);
            this.lblDrugControlNumber.Name = "lblDrugControlNumber";
            this.lblDrugControlNumber.Size = new Size(0x38, 0x15);
            this.lblDrugControlNumber.TabIndex = 2;
            this.lblDrugControlNumber.Text = "Control #";
            this.lblDrugControlNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblDrugNoteField.Location = new Point(8, 0);
            this.lblDrugNoteField.Name = "lblDrugNoteField";
            this.lblDrugNoteField.Size = new Size(60, 0x15);
            this.lblDrugNoteField.TabIndex = 0;
            this.lblDrugNoteField.Text = "Note Field";
            this.lblDrugNoteField.TextAlign = ContentAlignment.MiddleRight;
            this.chbNopayIns1.Location = new Point(320, 0x108);
            this.chbNopayIns1.Name = "chbNopayIns1";
            this.chbNopayIns1.Size = new Size(0x68, 0x15);
            this.chbNopayIns1.TabIndex = 0x21;
            this.chbNopayIns1.Text = "No Pay Ins 1";
            this.lblBilling.Location = new Point(0, 0x108);
            this.lblBilling.Name = "lblBilling";
            this.lblBilling.Size = new Size(0x58, 0x15);
            this.lblBilling.TabIndex = 0x1c;
            this.lblBilling.Text = "Billing";
            this.lblBilling.TextAlign = ContentAlignment.MiddleRight;
            this.expDrugIdentification.Content = this.pnlDrugIdentification;
            this.expDrugIdentification.Dock = DockStyle.Top;
            this.expDrugIdentification.Header = " Drug Identification";
            this.expDrugIdentification.Location = new Point(4, 560);
            this.expDrugIdentification.Name = "expDrugIdentification";
            this.expDrugIdentification.Size = new Size(0x1fc, 0x18);
            this.expDrugIdentification.TabIndex = 3;
            this.pnlUserFields.Controls.Add(this.txtUserField2);
            this.pnlUserFields.Controls.Add(this.txtUserField1);
            this.pnlUserFields.Controls.Add(this.lblUserField2);
            this.pnlUserFields.Controls.Add(this.lblUserField1);
            this.pnlUserFields.Dock = DockStyle.Top;
            this.pnlUserFields.Location = new Point(4, 0x278);
            this.pnlUserFields.Name = "pnlUserFields";
            this.pnlUserFields.Size = new Size(0x1fc, 0x18);
            this.pnlUserFields.TabIndex = 8;
            this.txtUserField2.Location = new Point(320, 0);
            this.txtUserField2.MaxLength = 100;
            this.txtUserField2.Name = "txtUserField2";
            this.txtUserField2.Size = new Size(0xb8, 20);
            this.txtUserField2.TabIndex = 3;
            this.txtUserField1.Location = new Point(80, 0);
            this.txtUserField1.MaxLength = 100;
            this.txtUserField1.Name = "txtUserField1";
            this.txtUserField1.Size = new Size(0xa8, 20);
            this.txtUserField1.TabIndex = 1;
            this.lblUserField2.Location = new Point(0x100, 0);
            this.lblUserField2.Name = "lblUserField2";
            this.lblUserField2.Size = new Size(0x38, 0x15);
            this.lblUserField2.TabIndex = 2;
            this.lblUserField2.Text = "Field #2";
            this.lblUserField2.TextAlign = ContentAlignment.MiddleRight;
            this.lblUserField1.Location = new Point(8, 0);
            this.lblUserField1.Name = "lblUserField1";
            this.lblUserField1.Size = new Size(0x40, 0x15);
            this.lblUserField1.TabIndex = 0;
            this.lblUserField1.Text = "Field #1";
            this.lblUserField1.TextAlign = ContentAlignment.MiddleRight;
            this.expUserFields.Content = this.pnlUserFields;
            this.expUserFields.Dock = DockStyle.Top;
            this.expUserFields.Header = " User Fields";
            this.expUserFields.Location = new Point(4, 0x260);
            this.expUserFields.Name = "expUserFields";
            this.expUserFields.Size = new Size(0x1fc, 0x18);
            this.expUserFields.TabIndex = 7;
            this.AutoScaleBaseSize = new Size(5, 13);
            this.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(0x204, 0x2b7);
            base.Controls.Add(this.pnlUserFields);
            base.Controls.Add(this.expUserFields);
            base.Controls.Add(this.pnlDrugIdentification);
            base.Controls.Add(this.expDrugIdentification);
            this.MinimumSize = new Size(0x214, 0);
            base.Name = "FormOrderDetail";
            this.Text = "Order Detail";
            base.Controls.SetChildIndex(this.pnlTop, 0);
            base.Controls.SetChildIndex(this.pnlPricing, 0);
            base.Controls.SetChildIndex(this.pnlBottom, 0);
            base.Controls.SetChildIndex(this.expDrugIdentification, 0);
            base.Controls.SetChildIndex(this.pnlDrugIdentification, 0);
            base.Controls.SetChildIndex(this.expUserFields, 0);
            base.Controls.SetChildIndex(this.pnlUserFields, 0);
            base.Controls.SetChildIndex(this.Panel1, 0);
            this.gbBillItemOn.ResumeLayout(false);
            ((ISupportInitialize) this.MissingProvider).EndInit();
            this.gbDOS1.ResumeLayout(false);
            this.gbDOS1.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.pnlPricing.ResumeLayout(false);
            this.pnlPricing.PerformLayout();
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            ((ISupportInitialize) this.ValidationWarnings).EndInit();
            this.pnlDrugIdentification.ResumeLayout(false);
            this.pnlDrugIdentification.PerformLayout();
            this.pnlUserFields.ResumeLayout(false);
            this.pnlUserFields.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override void InitializeMirHelper(FormMirHelper Helper)
        {
            base.InitializeMirHelper(Helper);
            Helper.Add("EndDate.Invalid", this.dtbEndDate, "End date must be greater than bill date");
            Helper.Add("EndDate.Unconfirmed", this.dtbEndDate, "Pickup must be confirmed");
        }

        protected override void InternalLoadPriceListInformation(IDataRecord record)
        {
            base.InternalLoadPriceListInformation(record);
            Functions.SetCheckBoxChecked(this.chbBillIns1, record["BillIns1"]);
            Functions.SetCheckBoxChecked(this.chbBillIns2, record["BillIns2"]);
            Functions.SetCheckBoxChecked(this.chbBillIns3, record["BillIns3"]);
            Functions.SetCheckBoxChecked(this.chbBillIns4, record["BillIns4"]);
            Functions.SetCheckBoxChecked(this.chbNopayIns1, false);
        }

        protected override void LoadFromRow(DataRow Row)
        {
            base.LoadFromRow(Row);
            if (Row.Table is ControlOrderDetails.TableOrderDetails)
            {
                ControlOrderDetails.TableOrderDetails table = (ControlOrderDetails.TableOrderDetails) Row.Table;
                Functions.SetCheckBoxChecked(this.chbAcceptAssignment, Row[table.Col_AcceptAssignment]);
                Functions.SetCheckBoxChecked(this.chbBillIns1, Row[table.Col_BillIns1]);
                Functions.SetCheckBoxChecked(this.chbBillIns2, Row[table.Col_BillIns2]);
                Functions.SetCheckBoxChecked(this.chbBillIns3, Row[table.Col_BillIns3]);
                Functions.SetCheckBoxChecked(this.chbBillIns4, Row[table.Col_BillIns4]);
                Functions.SetCheckBoxChecked(this.chbNopayIns1, Row[table.Col_NopayIns1]);
                Functions.SetDateBoxValue(this.dtbEndDate, Row[table.Col_EndDate]);
                Functions.SetTextBoxText(this.txtDrugNoteField, Row[table.Col_DrugNoteField]);
                Functions.SetTextBoxText(this.txtDrugControlNumber, Row[table.Col_DrugControlNumber]);
                Functions.SetTextBoxText(this.txtUserField1, Row[table.Col_UserField1]);
                Functions.SetTextBoxText(this.txtUserField2, Row[table.Col_UserField2]);
                this.expDrugIdentification.Collapsed = string.IsNullOrEmpty(this.txtDrugNoteField.Text) && string.IsNullOrEmpty(this.txtDrugControlNumber.Text);
                this.expUserFields.Collapsed = string.IsNullOrEmpty(this.txtUserField1.Text) && string.IsNullOrEmpty(this.txtUserField2.Text);
                this.CalculateQuantities();
            }
        }

        private void nmbOrderedQuantity_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateQuantities();
        }

        private void rbBillItemOn_Changed(object sender, EventArgs e)
        {
            this.CalculateQuantities();
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlOrderDetails.TableOrderDetails)
            {
                int num;
                ControlOrderDetails.TableOrderDetails table = (ControlOrderDetails.TableOrderDetails) Row.Table;
                if ((((Row.RowState == DataRowState.Added) || ((Row.RowState == DataRowState.Modified) || (Row.RowState == DataRowState.Unchanged))) && !this.txtBillingMonth.ReadOnly) && (int.TryParse(this.txtBillingMonth.Text, out num) && ((NullableConvert.ToInt32(Row[table.Col_BillingMonth], 0) != num) && (MessageBox.Show("Are you sure you want to change Billing Month?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))))
                {
                    Row[table.Col_BillingMonth] = num;
                }
                base.SaveToRow(Row);
                Row[table.Col_AcceptAssignment] = this.chbAcceptAssignment.Checked;
                Row[table.Col_BillIns1] = this.chbBillIns1.Checked;
                Row[table.Col_BillIns2] = this.chbBillIns2.Checked;
                Row[table.Col_BillIns3] = this.chbBillIns3.Checked;
                Row[table.Col_BillIns4] = this.chbBillIns4.Checked;
                Row[table.Col_NopayIns1] = this.chbNopayIns1.Checked;
                Row[table.Col_EndDate] = NullableConvert.ToDb(this.dtbEndDate.Value);
                Row[table.Col_DrugNoteField] = this.txtDrugNoteField.Text;
                Row[table.Col_DrugControlNumber] = this.txtDrugControlNumber.Text;
                Row[table.Col_UserField1] = this.txtUserField1.Text;
                Row[table.Col_UserField2] = this.txtUserField2.Text;
            }
        }

        private void txtHaoDescription_TextChanged(object sender, EventArgs e)
        {
            string str = Regex.Replace(this.txtHaoDescription.Text, @"\\s+", " ").Trim();
            this.ToolTip1.SetToolTip(this.txtHaoDescription, $"{str.Length} character(s) long");
        }

        protected override void ValidateObject()
        {
            if (base.Visible)
            {
                if (NullableConvert.ToInt32(this.cmbWarehouse.SelectedValue) == null)
                {
                    this.ValidationErrors.SetError(this.cmbWarehouse, "You must select warehouse");
                }
                if (!(Functions.GetDateBoxValue(this.dtbDOSFrom) is DateTime))
                {
                    this.ValidationErrors.SetError(this.dtbDOSFrom, "You must select Date Of Service From");
                }
                Actualizer actualizer = new Actualizer(this.cmbSaleRentType.Text, base.BillItemOn, this.cmbOrderedWhen.Text, this.cmbBilledWhen.Text);
                this.ValidationErrors.SetError(this.cmbSaleRentType, actualizer.Error_SaleRentType);
                this.ValidationErrors.SetError(this.gbBillItemOn, actualizer.Error_BillItemOn);
                this.ValidationErrors.SetError(this.cmbOrderedWhen, actualizer.Error_OrderedWhen);
                this.ValidationErrors.SetError(this.cmbBilledWhen, actualizer.Error_BilledWhen);
                if (!string.IsNullOrWhiteSpace(this.txtDXPointer9.Text) && !Regex.IsMatch(this.txtDXPointer9.Text, @"^(\s*[1-4]\s*)(,\s*[1-4]\s*)*$"))
                {
                    this.ValidationErrors.SetError(this.txtDXPointer9, "Must be comma separated list of numbers between 1 and 4");
                }
                if (!string.IsNullOrWhiteSpace(this.txtDXPointer10.Text) && !Regex.IsMatch(this.txtDXPointer10.Text, @"^(\s*([1-9]|1[0-2])\s*)(,\s*([1-9]|1[0-2])\s*)*$"))
                {
                    this.ValidationErrors.SetError(this.txtDXPointer10, "Must be comma separated list of numbers between 1 and 12");
                }
                if (!string.IsNullOrWhiteSpace(this.txtAuthorizationNumber.Text) && !(Functions.GetDateBoxValue(this.dtbAuthorizationExpirationDate) is DateTime))
                {
                    this.ValidationWarnings.SetError(this.dtbAuthorizationExpirationDate, "Authorization Expiration Date is needed for notifications");
                }
                string str = Regex.Replace(this.txtHaoDescription.Text, @"\\s+", " ").Trim();
                if (80 < str.Length)
                {
                    this.ValidationWarnings.SetError(this.txtHaoDescription, $"Length of the HAO ({str.Length}) exceeds 80 characters. Exceeding characters will be truncated upon EDI transmission");
                }
                if (!(Functions.GetDateBoxValue(this.dtbDOSFrom) is DateTime))
                {
                    this.ValidationErrors.SetError(this.dtbDOSFrom, "You must select Date Of Service From");
                }
            }
        }

        internal static string[] ValidateRow(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }
            ControlOrderDetailsBase.TableOrderDetailsBase table = row.Table as ControlOrderDetailsBase.TableOrderDetailsBase;
            if (table == null)
            {
                throw new ArgumentException("row.Table must be TableOrderDetailsBase", "row");
            }
            List<string> list = new List<string>();
            if (NullableConvert.ToInt32(row[table.Col_WarehouseID]) == null)
            {
                list.Add("You must select warehouse");
            }
            if (NullableConvert.ToDateTime(row[table.Col_DOSFrom]) == null)
            {
                list.Add("You must select Date Of Service From");
            }
            Actualizer actualizer = new Actualizer(Convert.ToString(row[table.Col_SaleRentType]), Convert.ToString(row[table.Col_BillItemOn]), Convert.ToString(row[table.Col_OrderedWhen]), Convert.ToString(row[table.Col_BilledWhen]));
            list.Add(actualizer.Error_SaleRentType);
            list.Add(actualizer.Error_BillItemOn);
            list.Add(actualizer.Error_OrderedWhen);
            list.Add(actualizer.Error_BilledWhen);
            string str = Convert.ToString(row[table.Col_DXPointer9]);
            if (!string.IsNullOrWhiteSpace(str) && !Regex.IsMatch(str, @"^(\s*[1-4]\s*)(,\s*[1-4]\s*)*$"))
            {
                list.Add("Must be comma separated list of numbers between 1 and 4");
            }
            string str2 = Convert.ToString(row[table.Col_DXPointer10]);
            if (!string.IsNullOrWhiteSpace(str2) && !Regex.IsMatch(str2, @"^(\s*([1-9]|1[0-2])\s*)(,\s*([1-9]|1[0-2])\s*)*$"))
            {
                list.Add("Must be comma separated list of numbers between 1 and 12");
            }
            list.RemoveAll((_Closure$__.$I92-0 == null) ? (_Closure$__.$I92-0 = new Predicate<string>(_Closure$__.$I._Lambda$__92-0)) : _Closure$__.$I92-0);
            return list.ToArray();
        }

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

        [field: AccessedThroughProperty("dtbEndDate")]
        private UltraDateTimeEditor dtbEndDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbAcceptAssignment")]
        private CheckBox chbAcceptAssignment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlDrugIdentification")]
        private Panel pnlDrugIdentification { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("lblEndDate")]
        private Label lblEndDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbNopayIns1")]
        private CheckBox chbNopayIns1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBilling")]
        private Label lblBilling { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("expDrugIdentification")]
        private Expander expDrugIdentification { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlUserFields")]
        private Panel pnlUserFields { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUserField2")]
        private TextBox txtUserField2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUserField1")]
        private TextBox txtUserField1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUserField2")]
        private Label lblUserField2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUserField1")]
        private Label lblUserField1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("expUserFields")]
        private Expander expUserFields { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        protected override NumericBox nmbOrderedQuantity
        {
            get => 
                base.nmbOrderedQuantity;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.nmbOrderedQuantity_ValueChanged);
                NumericBox nmbOrderedQuantity = base.nmbOrderedQuantity;
                if (nmbOrderedQuantity != null)
                {
                    nmbOrderedQuantity.ValueChanged -= handler;
                }
                base.nmbOrderedQuantity = value;
                nmbOrderedQuantity = base.nmbOrderedQuantity;
                if (nmbOrderedQuantity != null)
                {
                    nmbOrderedQuantity.ValueChanged += handler;
                }
            }
        }

        protected override ComboBox cmbOrderedWhen
        {
            get => 
                base.cmbOrderedWhen;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.cmbOrderedWhen_TextChanged);
                ComboBox cmbOrderedWhen = base.cmbOrderedWhen;
                if (cmbOrderedWhen != null)
                {
                    cmbOrderedWhen.TextChanged -= handler;
                }
                base.cmbOrderedWhen = value;
                cmbOrderedWhen = base.cmbOrderedWhen;
                if (cmbOrderedWhen != null)
                {
                    cmbOrderedWhen.TextChanged += handler;
                }
            }
        }

        protected override ComboBox cmbBilledWhen
        {
            get => 
                base.cmbBilledWhen;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.cmbBilledWhen_TextChanged);
                ComboBox cmbBilledWhen = base.cmbBilledWhen;
                if (cmbBilledWhen != null)
                {
                    cmbBilledWhen.TextChanged -= handler;
                }
                base.cmbBilledWhen = value;
                cmbBilledWhen = base.cmbBilledWhen;
                if (cmbBilledWhen != null)
                {
                    cmbBilledWhen.TextChanged += handler;
                }
            }
        }

        protected override ComboBox cmbSaleRentType
        {
            get => 
                base.cmbSaleRentType;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.cmbSaleRentType_TextChanged);
                ComboBox cmbSaleRentType = base.cmbSaleRentType;
                if (cmbSaleRentType != null)
                {
                    cmbSaleRentType.TextChanged -= handler;
                }
                base.cmbSaleRentType = value;
                cmbSaleRentType = base.cmbSaleRentType;
                if (cmbSaleRentType != null)
                {
                    cmbSaleRentType.TextChanged += handler;
                }
            }
        }

        protected override RadioButton rbDayOfDelivery
        {
            get => 
                base.rbDayOfDelivery;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.rbBillItemOn_Changed);
                EventHandler handler2 = new EventHandler(this.rbBillItemOn_Changed);
                RadioButton rbDayOfDelivery = base.rbDayOfDelivery;
                if (rbDayOfDelivery != null)
                {
                    rbDayOfDelivery.Click -= handler;
                    rbDayOfDelivery.CheckedChanged -= handler2;
                }
                base.rbDayOfDelivery = value;
                rbDayOfDelivery = base.rbDayOfDelivery;
                if (rbDayOfDelivery != null)
                {
                    rbDayOfDelivery.Click += handler;
                    rbDayOfDelivery.CheckedChanged += handler2;
                }
            }
        }

        protected override RadioButton rbLastDayOfThePeriod
        {
            get => 
                base.rbLastDayOfThePeriod;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.rbBillItemOn_Changed);
                EventHandler handler2 = new EventHandler(this.rbBillItemOn_Changed);
                RadioButton rbLastDayOfThePeriod = base.rbLastDayOfThePeriod;
                if (rbLastDayOfThePeriod != null)
                {
                    rbLastDayOfThePeriod.Click -= handler;
                    rbLastDayOfThePeriod.CheckedChanged -= handler2;
                }
                base.rbLastDayOfThePeriod = value;
                rbLastDayOfThePeriod = base.rbLastDayOfThePeriod;
                if (rbLastDayOfThePeriod != null)
                {
                    rbLastDayOfThePeriod.Click += handler;
                    rbLastDayOfThePeriod.CheckedChanged += handler2;
                }
            }
        }

        protected override UltraDateTimeEditor dtbDOSFrom
        {
            get => 
                base.dtbDOSFrom;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.dtbDOSFrom_ValueChanged);
                UltraDateTimeEditor dtbDOSFrom = base.dtbDOSFrom;
                if (dtbDOSFrom != null)
                {
                    dtbDOSFrom.ValueChanged -= handler;
                }
                base.dtbDOSFrom = value;
                dtbDOSFrom = base.dtbDOSFrom;
                if (dtbDOSFrom != null)
                {
                    dtbDOSFrom.ValueChanged += handler;
                }
            }
        }

        protected override UltraDateTimeEditor dtbDOSTo
        {
            get => 
                base.dtbDOSTo;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.dtbDOSTo_ValueChanged);
                UltraDateTimeEditor dtbDOSTo = base.dtbDOSTo;
                if (dtbDOSTo != null)
                {
                    dtbDOSTo.ValueChanged -= handler;
                }
                base.dtbDOSTo = value;
                dtbDOSTo = base.dtbDOSTo;
                if (dtbDOSTo != null)
                {
                    dtbDOSTo.ValueChanged += handler;
                }
            }
        }

        protected override TextBox txtHaoDescription
        {
            get => 
                base.txtHaoDescription;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.txtHaoDescription_TextChanged);
                TextBox txtHaoDescription = base.txtHaoDescription;
                if (txtHaoDescription != null)
                {
                    txtHaoDescription.TextChanged -= handler;
                }
                base.txtHaoDescription = value;
                txtHaoDescription = base.txtHaoDescription;
                if (txtHaoDescription != null)
                {
                    txtHaoDescription.TextChanged += handler;
                }
            }
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormOrderDetail._Closure$__ $I = new FormOrderDetail._Closure$__();
            public static Predicate<string> $I92-0;

            internal bool _Lambda$__92-0(string s) => 
                string.IsNullOrEmpty(s);
        }
    }
}

