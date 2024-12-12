namespace DMEWorks.Details
{
    using DMEWorks.Billing;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormRetailSalesDetail : FormOrderDetailBase
    {
        private IContainer components;

        public FormRetailSalesDetail(ControlRetailSalesDetails Parent) : base(Parent)
        {
            this.InitializeComponent();
            this.txtOrderedUnits.ReadOnly = true;
            this.nmbOrderedQuantity.ReadOnly = true;
            this.nmbDeliveryQuantity.ReadOnly = false;
        }

        private void CalculateQuantities()
        {
            try
            {
                this.nmbBilledQuantity.AsDouble = new double?(Converter.DeliveredQty2BilledQty(this.nmbDeliveryQuantity.AsDouble.GetValueOrDefault(0.0), this.nmbDeliveryConverter.AsDouble.GetValueOrDefault(0.0), this.nmbBilledConverter.AsDouble.GetValueOrDefault(0.0)));
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.nmbBilledQuantity.AsDouble = 0.0;
                ProjectData.ClearProjectError();
            }
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
            this.gbBillItemOn.SuspendLayout();
            ((ISupportInitialize) this.MissingProvider).BeginInit();
            this.gbDOS1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlPricing.SuspendLayout();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            ((ISupportInitialize) this.ValidationWarnings).BeginInit();
            base.SuspendLayout();
            this.MissingProvider.SetIconPadding(this.cmbSaleRentType, -16);
            this.MissingProvider.SetIconPadding(this.txtBillingCode, -16);
            this.MissingProvider.SetIconPadding(this.txtModifier1, -16);
            this.MissingProvider.SetIconPadding(this.txtModifier2, -16);
            this.MissingProvider.SetIconPadding(this.txtModifier3, -16);
            this.ToolTip1.SetToolTip(this.txtReasonForPickup, "ReasonForPickup");
            this.MissingProvider.SetIconPadding(this.txtAuthorizationNumber, -16);
            this.MissingProvider.SetIconAlignment(this.chbMissingInformation, ErrorIconAlignment.MiddleRight);
            this.MissingProvider.SetIconPadding(this.chbMissingInformation, 0);
            this.MissingProvider.SetIconPadding(this.txtPriceCode, -16);
            this.MissingProvider.SetIconPadding(this.txtPriceCode, 0);
            this.MissingProvider.SetIconPadding(this.txtInventoryItem, -16);
            this.MissingProvider.SetIconPadding(this.txtInventoryItem, 0);
            this.pnlBottom.Size = new Size(0x1fc, 0xd9);
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
            this.Panel1.Location = new Point(4, 0x24d);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x204, 0x275);
            base.Name = "FormRetailSalesDetail";
            this.Text = "RetailSales Detail";
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
            base.ResumeLayout(false);
        }

        protected override void LoadFromRow(DataRow Row)
        {
            base.LoadFromRow(Row);
            this.CalculateQuantities();
        }

        private void nmbDeliveryQuantity_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateQuantities();
        }

        protected override NumericBox nmbDeliveryQuantity
        {
            get => 
                base.nmbDeliveryQuantity;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler handler = new EventHandler(this.nmbDeliveryQuantity_ValueChanged);
                NumericBox nmbDeliveryQuantity = base.nmbDeliveryQuantity;
                if (nmbDeliveryQuantity != null)
                {
                    nmbDeliveryQuantity.ValueChanged -= handler;
                }
                base.nmbDeliveryQuantity = value;
                nmbDeliveryQuantity = base.nmbDeliveryQuantity;
                if (nmbDeliveryQuantity != null)
                {
                    nmbDeliveryQuantity.ValueChanged += handler;
                }
            }
        }
    }
}

