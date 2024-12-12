namespace DMEWorks.Details
{
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormPurchaseOrderDetail : FormDetails
    {
        private IContainer components;

        public FormPurchaseOrderDetail() : this(null)
        {
        }

        public FormPurchaseOrderDetail(ControlPurchaseOrderItems Parent) : base(Parent)
        {
            this.InitializeComponent();
        }

        private void CalculateBackOrder()
        {
            this.nmbBackOrder.AsInt32 = new int?(this.nmbOrdered.AsInt32.GetValueOrDefault(0) - this.nmbReceived.AsInt32.GetValueOrDefault(0));
        }

        protected override void Clear()
        {
            Functions.SetNumericBoxValue(this.nmbBackOrder, DBNull.Value);
            Functions.SetTextBoxText(this.txtCustomer, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDatePromised, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDateReceived, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbDropShipToCustomer, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbInventoryItem, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbPrice, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbOrdered, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbReceived, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbWarehouse, DBNull.Value);
        }

        private void cmbInventoryItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow selectedRow = this.cmbInventoryItem.SelectedRow;
                if (selectedRow != null)
                {
                    Functions.SetNumericBoxValue(this.nmbPrice, selectedRow["PurchasePrice"]);
                }
                else
                {
                    Functions.SetNumericBoxValue(this.nmbPrice, DBNull.Value);
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
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
            this.lblItem = new Label();
            this.lblOrdered = new Label();
            this.lblDateReceived = new Label();
            this.lblDatePromised = new Label();
            this.lblWarehouse = new Label();
            this.dtbDateReceived = new UltraDateTimeEditor();
            this.dtbDatePromised = new UltraDateTimeEditor();
            this.chbDropShipToCustomer = new CheckBox();
            this.cmbWarehouse = new Combobox();
            this.nmbOrdered = new NumericBox();
            this.nmbBackOrder = new NumericBox();
            this.lblBackOrder = new Label();
            this.nmbReceived = new NumericBox();
            this.lblReceived = new Label();
            this.txtCustomer = new TextBox();
            this.lblCustomer = new Label();
            this.cmbInventoryItem = new Combobox();
            this.nmbPrice = new NumericBox();
            this.lblPrice = new Label();
            base.SuspendLayout();
            this.btnCancel.Location = new Point(280, 0x110);
            this.btnCancel.Visible = true;
            this.btnOK.Location = new Point(200, 0x110);
            this.btnOK.Visible = true;
            this.lblItem.Location = new Point(8, 40);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new Size(100, 0x15);
            this.lblItem.TabIndex = 2;
            this.lblItem.Text = "Item";
            this.lblItem.TextAlign = ContentAlignment.MiddleRight;
            this.lblOrdered.Location = new Point(8, 0x60);
            this.lblOrdered.Name = "lblOrdered";
            this.lblOrdered.Size = new Size(100, 0x15);
            this.lblOrdered.TabIndex = 6;
            this.lblOrdered.Text = "Ordered";
            this.lblOrdered.TextAlign = ContentAlignment.MiddleRight;
            this.lblDateReceived.Location = new Point(8, 0xb0);
            this.lblDateReceived.Name = "lblDateReceived";
            this.lblDateReceived.Size = new Size(100, 0x15);
            this.lblDateReceived.TabIndex = 12;
            this.lblDateReceived.Text = "Date Received";
            this.lblDateReceived.TextAlign = ContentAlignment.MiddleRight;
            this.lblDatePromised.Location = new Point(8, 200);
            this.lblDatePromised.Name = "lblDatePromised";
            this.lblDatePromised.Size = new Size(100, 0x15);
            this.lblDatePromised.TabIndex = 14;
            this.lblDatePromised.Text = "Date Promised";
            this.lblDatePromised.TextAlign = ContentAlignment.MiddleRight;
            this.lblWarehouse.Location = new Point(8, 0xe0);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(100, 0x15);
            this.lblWarehouse.TabIndex = 0x10;
            this.lblWarehouse.Text = "Warehouse";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDateReceived.Location = new Point(120, 0xb0);
            this.dtbDateReceived.Name = "dtbDateReceived";
            this.dtbDateReceived.Size = new Size(0x60, 0x15);
            this.dtbDateReceived.TabIndex = 13;
            this.dtbDatePromised.Location = new Point(120, 200);
            this.dtbDatePromised.Name = "dtbDatePromised";
            this.dtbDatePromised.Size = new Size(0x60, 0x15);
            this.dtbDatePromised.TabIndex = 15;
            this.chbDropShipToCustomer.CheckAlign = ContentAlignment.MiddleRight;
            this.chbDropShipToCustomer.Location = new Point(8, 0xf8);
            this.chbDropShipToCustomer.Name = "chbDropShipToCustomer";
            this.chbDropShipToCustomer.Size = new Size(0x98, 0x18);
            this.chbDropShipToCustomer.TabIndex = 0x12;
            this.chbDropShipToCustomer.Text = "Drop Ship To Customer";
            this.chbDropShipToCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.cmbWarehouse.Location = new Point(120, 0xe0);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(0xe0, 0x15);
            this.cmbWarehouse.TabIndex = 0x11;
            this.nmbOrdered.BorderStyle = BorderStyle.Fixed3D;
            this.nmbOrdered.Location = new Point(120, 0x60);
            this.nmbOrdered.Name = "nmbOrdered";
            this.nmbOrdered.Size = new Size(0x60, 0x15);
            this.nmbOrdered.TabIndex = 7;
            this.nmbOrdered.TextAlign = HorizontalAlignment.Left;
            this.nmbBackOrder.BorderStyle = BorderStyle.Fixed3D;
            this.nmbBackOrder.Location = new Point(120, 0x90);
            this.nmbBackOrder.Name = "nmbBackOrder";
            this.nmbBackOrder.Size = new Size(0x60, 0x15);
            this.nmbBackOrder.TabIndex = 11;
            this.nmbBackOrder.TextAlign = HorizontalAlignment.Left;
            this.lblBackOrder.Location = new Point(8, 0x90);
            this.lblBackOrder.Name = "lblBackOrder";
            this.lblBackOrder.Size = new Size(100, 0x15);
            this.lblBackOrder.TabIndex = 10;
            this.lblBackOrder.Text = "Back Order";
            this.lblBackOrder.TextAlign = ContentAlignment.MiddleRight;
            this.nmbReceived.BorderStyle = BorderStyle.Fixed3D;
            this.nmbReceived.Location = new Point(120, 120);
            this.nmbReceived.Name = "nmbReceived";
            this.nmbReceived.Size = new Size(0x60, 0x15);
            this.nmbReceived.TabIndex = 9;
            this.nmbReceived.TextAlign = HorizontalAlignment.Left;
            this.lblReceived.Location = new Point(8, 120);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new Size(100, 0x15);
            this.lblReceived.TabIndex = 8;
            this.lblReceived.Text = "Received";
            this.lblReceived.TextAlign = ContentAlignment.MiddleRight;
            this.txtCustomer.Location = new Point(120, 8);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new Size(240, 20);
            this.txtCustomer.TabIndex = 1;
            this.txtCustomer.Text = "";
            this.lblCustomer.Location = new Point(8, 8);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(100, 0x15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.cmbInventoryItem.Location = new Point(120, 40);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.Size = new Size(0xe0, 0x15);
            this.cmbInventoryItem.TabIndex = 3;
            this.nmbPrice.BorderStyle = BorderStyle.Fixed3D;
            this.nmbPrice.Location = new Point(120, 0x48);
            this.nmbPrice.Name = "nmbPrice";
            this.nmbPrice.Size = new Size(0x60, 0x15);
            this.nmbPrice.TabIndex = 5;
            this.nmbPrice.TextAlign = HorizontalAlignment.Left;
            this.lblPrice.Location = new Point(8, 0x48);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new Size(100, 0x15);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "Price";
            this.lblPrice.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x170, 0x135);
            Control[] controls = new Control[0x15];
            controls[0] = this.btnCancel;
            controls[1] = this.btnOK;
            controls[2] = this.nmbPrice;
            controls[3] = this.lblPrice;
            controls[4] = this.cmbInventoryItem;
            controls[5] = this.txtCustomer;
            controls[6] = this.lblCustomer;
            controls[7] = this.nmbOrdered;
            controls[8] = this.cmbWarehouse;
            controls[9] = this.chbDropShipToCustomer;
            controls[10] = this.dtbDatePromised;
            controls[11] = this.dtbDateReceived;
            controls[12] = this.lblWarehouse;
            controls[13] = this.lblDatePromised;
            controls[14] = this.lblDateReceived;
            controls[15] = this.lblOrdered;
            controls[0x10] = this.lblItem;
            controls[0x11] = this.nmbReceived;
            controls[0x12] = this.lblReceived;
            controls[0x13] = this.nmbBackOrder;
            controls[20] = this.lblBackOrder;
            base.Controls.AddRange(controls);
            base.Name = "FormPurchaseOrderDetail2";
            this.Text = "FormPurchaseOrderDetail";
            base.ResumeLayout(false);
        }

        public override void LoadComboBoxes()
        {
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            Cache.InitDropdown(this.cmbInventoryItem, "tbl_inventoryitem", null);
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlPurchaseOrderItems.TablePurchaseOrderDetails)
            {
                ControlPurchaseOrderItems.TablePurchaseOrderDetails table = (ControlPurchaseOrderItems.TablePurchaseOrderDetails) Row.Table;
                Functions.SetNumericBoxValue(this.nmbBackOrder, Row[table.Col_BackOrder]);
                Functions.SetTextBoxText(this.txtCustomer, Row[table.Col_Customer]);
                Functions.SetDateBoxValue(this.dtbDatePromised, Row[table.Col_DatePromised]);
                Functions.SetDateBoxValue(this.dtbDateReceived, Row[table.Col_DateReceived]);
                Functions.SetCheckBoxChecked(this.chbDropShipToCustomer, Row[table.Col_DropShipToCustomer]);
                Functions.SetComboBoxValue(this.cmbInventoryItem, Row[table.Col_InventoryItemID]);
                Functions.SetNumericBoxValue(this.nmbPrice, Row[table.Col_Price]);
                Functions.SetNumericBoxValue(this.nmbOrdered, Row[table.Col_Ordered]);
                Functions.SetNumericBoxValue(this.nmbReceived, Row[table.Col_Received]);
                Functions.SetComboBoxValue(this.cmbWarehouse, Row[table.Col_WarehouseID]);
            }
        }

        private void nmbOrdered_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateBackOrder();
        }

        private void nmbReceived_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateBackOrder();
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlPurchaseOrderItems.TablePurchaseOrderDetails)
            {
                ControlPurchaseOrderItems.TablePurchaseOrderDetails table = (ControlPurchaseOrderItems.TablePurchaseOrderDetails) Row.Table;
                Row.BeginEdit();
                try
                {
                    Row[table.Col_BackOrder] = this.nmbBackOrder.AsInt32.GetValueOrDefault(0);
                    Row[table.Col_Customer] = this.txtCustomer.Text;
                    Row[table.Col_DatePromised] = Functions.GetDateBoxValue(this.dtbDatePromised);
                    Row[table.Col_DateReceived] = Functions.GetDateBoxValue(this.dtbDateReceived);
                    Row[table.Col_DropShipToCustomer] = this.chbDropShipToCustomer.Checked;
                    Row[table.Col_InventoryItemID] = this.cmbInventoryItem.SelectedValue;
                    Row[table.Col_InventoryItemName] = this.cmbInventoryItem.Text;
                    Row[table.Col_Price] = this.nmbPrice.AsDouble.GetValueOrDefault(0.0);
                    Row[table.Col_Ordered] = this.nmbOrdered.AsInt32.GetValueOrDefault(0);
                    Row[table.Col_Received] = this.nmbReceived.AsInt32.GetValueOrDefault(0);
                    Row[table.Col_WarehouseID] = this.cmbWarehouse.SelectedValue;
                    Row[table.Col_WarehouseName] = this.cmbWarehouse.Text;
                    Row.EndEdit();
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Row.CancelEdit();
                    throw;
                }
            }
        }

        protected override void ValidateObject()
        {
            if (!Versioned.IsNumeric(this.cmbWarehouse.SelectedValue))
            {
                this.ValidationErrors.SetError(this.cmbWarehouse, "You must select warehouse");
            }
            else
            {
                this.ValidationErrors.SetError(this.cmbWarehouse, "");
            }
            if (!Versioned.IsNumeric(this.cmbInventoryItem.SelectedValue))
            {
                this.ValidationErrors.SetError(this.cmbInventoryItem, "You must select inventory Item");
            }
            else
            {
                this.ValidationErrors.SetError(this.cmbInventoryItem, "");
            }
            if (this.nmbPrice.AsDouble.GetValueOrDefault(0.0) <= 0.0)
            {
                this.ValidationErrors.SetError(this.nmbPrice, "Price must be greater than zero");
            }
            else
            {
                this.ValidationErrors.SetError(this.nmbPrice, "");
            }
            if (this.nmbOrdered.AsInt32.GetValueOrDefault(0) <= 0)
            {
                this.ValidationErrors.SetError(this.nmbOrdered, "Number of ordered units must be greater than zero");
            }
            else
            {
                this.ValidationErrors.SetError(this.nmbOrdered, "");
            }
            if (this.nmbReceived.AsInt32.GetValueOrDefault(0) < 0)
            {
                this.ValidationErrors.SetError(this.nmbReceived, "Number of received units must be greater than zero");
            }
            else if (this.nmbOrdered.AsInt32.GetValueOrDefault(0) < this.nmbReceived.AsInt32.GetValueOrDefault(0))
            {
                this.ValidationErrors.SetError(this.nmbReceived, "Number of received units cannot excced number of ordered units");
            }
            else
            {
                this.ValidationErrors.SetError(this.nmbReceived, "");
            }
        }

        [field: AccessedThroughProperty("lblItem")]
        private Label lblItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrdered")]
        private Label lblOrdered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDateReceived")]
        private Label lblDateReceived { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDatePromised")]
        private Label lblDatePromised { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDateReceived")]
        private UltraDateTimeEditor dtbDateReceived { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDatePromised")]
        private UltraDateTimeEditor dtbDatePromised { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbDropShipToCustomer")]
        private CheckBox chbDropShipToCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        private Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbOrdered")]
        private NumericBox nmbOrdered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBackOrder")]
        private NumericBox nmbBackOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBackOrder")]
        private Label lblBackOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbReceived")]
        private NumericBox nmbReceived { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReceived")]
        private Label lblReceived { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomer")]
        private TextBox txtCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        private Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPrice")]
        private NumericBox nmbPrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPrice")]
        private Label lblPrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override AllowStateEnum AllowState
        {
            get => 
                base.AllowState;
            set
            {
                base.AllowState = value;
                bool flag = (base.AllowState & AllowStateEnum.AllowEdit07) == AllowStateEnum.AllowEdit07;
                this.txtCustomer.Enabled = flag;
                this.cmbInventoryItem.Enabled = flag;
                this.nmbOrdered.Enabled = flag;
                this.nmbPrice.Enabled = flag;
                this.cmbWarehouse.Enabled = flag;
                this.chbDropShipToCustomer.Enabled = flag;
            }
        }
    }
}

