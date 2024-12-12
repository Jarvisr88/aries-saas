namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ControlOrderDetails : ControlOrderDetailsBase
    {
        private IContainer components;

        public ControlOrderDetails()
        {
            base.Changed += new EventHandler(this.MyBase_Changed);
            this.InitializeComponent();
        }

        private void Appearance_CellFormatting(object sender, GridCellFormattingEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    DataColumn column = dataRow.Table.Columns["Returned"];
                    if ((column != null) && NullableConvert.ToBoolean(dataRow[column], false))
                    {
                        e.CellStyle.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private void btnAddKit_Click(object sender, EventArgs e)
        {
            using (WizardSelectKit kit = new WizardSelectKit())
            {
                List<int> list;
                if (kit.ShowDialog() == DialogResult.OK)
                {
                    IEnumerator<WizardSelectKit.ItemToAdd> enumerator;
                    list = new List<int>();
                    try
                    {
                        enumerator = kit.Items.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            WizardSelectKit.ItemToAdd current = enumerator.Current;
                            DataRow row = base.InternalAddNewItem(current.InventoryItemID, current.PriceCodeID, current.WarehouseID, new int?(current.Quantity), false);
                            string[] strArray = FormOrderDetail.ValidateRow(row);
                            if (0 < strArray.Length)
                            {
                                list.Add(Conversions.ToInteger(row["RowID"]));
                            }
                        }
                    }
                    finally
                    {
                        if (enumerator != null)
                        {
                            enumerator.Dispose();
                        }
                    }
                }
                else
                {
                    return;
                }
                foreach (int num in list)
                {
                    base.EditRow(num);
                }
            }
        }

        public void ClearGrid()
        {
            base.ClearGrid_Delivery();
        }

        protected override FormDetails CreateDialog(object param)
        {
            FormOrderDetail dialog = new FormOrderDetail(this);
            return base.AddDialog(dialog);
        }

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableOrderDetails();

        protected internal override void DeletingCompleted(DataRow row)
        {
            try
            {
                if (((row != null) && (row.RowState == DataRowState.Deleted)) && (base.OrderID != null))
                {
                    if (base.CustomerID != null)
                    {
                        TableOrderDetails table = this.GetTable();
                        if (ReferenceEquals(row.Table, table))
                        {
                            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                            {
                                connection.Open();
                                DataRow[] changes = new DataRow[] { row };
                                SaveChanges(table, changes, connection, base.OrderID.Value, base.CustomerID.Value);
                            }
                        }
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

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void DoAdd(object param)
        {
            int? inventoryItemID = this.ControlPricing1.InventoryItemID;
            int? priceCodeID = this.ControlPricing1.PriceCodeID;
            if ((inventoryItemID != null) && (priceCodeID != null))
            {
                int? quantity = null;
                DataRow row = base.InternalAddNewItem(inventoryItemID.Value, priceCodeID.Value, ClassGlobalObjects.DefaultWarehouseID, quantity, false);
                if (row == null)
                {
                    MessageBox.Show("Selected pair Price Code - Inventory Item does not have record in pricing table");
                }
                else
                {
                    base.EditRow(Conversions.ToInteger(row["RowID"]));
                    this.ControlPricing1.Clear();
                }
            }
        }

        protected internal override void EditingCompleted(DataRow row)
        {
            try
            {
                if (((row != null) && (row.RowState == DataRowState.Modified)) && (base.OrderID != null))
                {
                    if (base.CustomerID != null)
                    {
                        TableOrderDetails table = this.GetTable();
                        if (ReferenceEquals(row.Table, table))
                        {
                            if (Globals.AutoReorderInventory)
                            {
                                TableOrderDetails details = (TableOrderDetails) table.Clone();
                                details.ImportRow(row);
                                base.ShowReorderDialog(details, false);
                            }
                            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                            {
                                connection.Open();
                                DataRow[] changes = new DataRow[] { row };
                                SaveChanges(table, changes, connection, base.OrderID.Value, base.CustomerID.Value);
                            }
                        }
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

        protected TableOrderDetails GetTable() => 
            (TableOrderDetails) base.F_TableDetails;

        private void Grid_ContextPopup(object Sender, GridContextMenuNeededEventArgs e)
        {
            DataRow dataRow = e.Row.GetDataRow();
            if (dataRow != null)
            {
                TableOrderDetails table = dataRow.Table as TableOrderDetails;
                if (table != null)
                {
                    e.ContextMenu = this.cmDelivery;
                    this.cmDelivery.Tag = dataRow;
                    try
                    {
                        OrderDetailsState state = (OrderDetailsState) Conversions.ToInteger(Enum.Parse(typeof(OrderDetailsState), Conversions.ToString(dataRow[table.Col_State]), true));
                        this.mnuPickup.Visible = (state == OrderDetailsState.Approved) | (state == OrderDetailsState.New);
                        this.mnuConfirm.Visible = state == OrderDetailsState.Pickup;
                        this.mnuChangeState_Approved.Checked = state == OrderDetailsState.Approved;
                        this.mnuChangeState_Canceled.Checked = state == OrderDetailsState.Canceled;
                        this.mnuChangeState_Closed.Checked = state == OrderDetailsState.Closed;
                        this.mnuChangeState_New.Checked = state == OrderDetailsState.New;
                        this.mnuChangeState_Pickup.Checked = state == OrderDetailsState.Pickup;
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        this.mnuPickup.Visible = true;
                        this.mnuConfirm.Visible = true;
                        this.mnuChangeState_Approved.Checked = false;
                        this.mnuChangeState_Canceled.Checked = false;
                        this.mnuChangeState_Closed.Checked = false;
                        this.mnuChangeState_New.Checked = false;
                        this.mnuChangeState_Pickup.Checked = false;
                        ProjectData.ClearProjectError();
                    }
                    this.mnuChangeState.Visible = Permissions.FormOrder_ChangeState.Allow_ADD_EDIT;
                }
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmDelivery = new ContextMenuStrip(this.components);
            this.mnuPickup = new ToolStripMenuItem();
            this.mnuExchange = new ToolStripMenuItem();
            this.mnuChangeState = new ToolStripMenuItem();
            this.mnuChangeState_New = new ToolStripMenuItem();
            this.mnuChangeState_Approved = new ToolStripMenuItem();
            this.mnuChangeState_Pickup = new ToolStripMenuItem();
            this.mnuChangeState_Closed = new ToolStripMenuItem();
            this.mnuChangeState_Canceled = new ToolStripMenuItem();
            this.mnuConfirm = new ToolStripMenuItem();
            this.btnAddKit = new Button();
            this.ControlPricing1 = new ControlPricing();
            this.StatusStrip1 = new StatusStrip();
            this.tsslTotalBillable = new ToolStripStatusLabel();
            this.tsslTotalAllowable = new ToolStripStatusLabel();
            base.F_TableDetails.BeginInit();
            this.Panel1.SuspendLayout();
            this.cmDelivery.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Panel1.Controls.Add(this.ControlPricing1);
            this.Panel1.Controls.Add(this.btnAddKit);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Size = new Size(600, 60);
            this.Panel1.TabIndex = 0;
            this.Panel1.Controls.SetChildIndex(this.btnAdd, 0);
            this.Panel1.Controls.SetChildIndex(this.btnDelete, 0);
            this.Panel1.Controls.SetChildIndex(this.btnAddKit, 0);
            this.Panel1.Controls.SetChildIndex(this.ControlPricing1, 0);
            this.btnDelete.Image = null;
            this.btnDelete.Location = new Point(0x1b0, 8);
            this.btnDelete.Size = new Size(0x30, 0x2d);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnAdd.Image = null;
            this.btnAdd.Location = new Point(0x178, 8);
            this.btnAdd.Size = new Size(0x30, 0x2d);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.Grid.Location = new Point(0, 60);
            this.Grid.Size = new Size(600, 160);
            this.Grid.TabIndex = 1;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.mnuPickup, this.mnuExchange, this.mnuChangeState, this.mnuConfirm };
            this.cmDelivery.Items.AddRange(toolStripItems);
            this.cmDelivery.Name = "cmDelivery";
            this.cmDelivery.Size = new Size(0xa7, 0x72);
            this.mnuPickup.Name = "mnuPickup";
            this.mnuPickup.Size = new Size(0xa6, 0x16);
            this.mnuPickup.Text = "Mark 'For pickup'";
            this.mnuExchange.Name = "mnuExchange";
            this.mnuExchange.Size = new Size(0xa6, 0x16);
            this.mnuExchange.Text = "Exchange";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.mnuChangeState_New, this.mnuChangeState_Approved, this.mnuChangeState_Pickup, this.mnuChangeState_Closed, this.mnuChangeState_Canceled };
            this.mnuChangeState.DropDownItems.AddRange(itemArray2);
            this.mnuChangeState.Name = "mnuChangeState";
            this.mnuChangeState.Size = new Size(0xa6, 0x16);
            this.mnuChangeState.Text = "Change State";
            this.mnuChangeState_New.Name = "mnuChangeState_New";
            this.mnuChangeState_New.Size = new Size(0x7e, 0x16);
            this.mnuChangeState_New.Text = "New";
            this.mnuChangeState_Approved.Name = "mnuChangeState_Approved";
            this.mnuChangeState_Approved.Size = new Size(0x7e, 0x16);
            this.mnuChangeState_Approved.Text = "Approved";
            this.mnuChangeState_Pickup.Name = "mnuChangeState_Pickup";
            this.mnuChangeState_Pickup.Size = new Size(0x7e, 0x16);
            this.mnuChangeState_Pickup.Text = "Pickup";
            this.mnuChangeState_Closed.Name = "mnuChangeState_Closed";
            this.mnuChangeState_Closed.Size = new Size(0x7e, 0x16);
            this.mnuChangeState_Closed.Text = "Closed";
            this.mnuChangeState_Canceled.Name = "mnuChangeState_Canceled";
            this.mnuChangeState_Canceled.Size = new Size(0x7e, 0x16);
            this.mnuChangeState_Canceled.Text = "Canceled";
            this.mnuConfirm.Name = "mnuConfirm";
            this.mnuConfirm.Size = new Size(0xa6, 0x16);
            this.mnuConfirm.Text = "Confirm Pickup";
            this.btnAddKit.FlatStyle = FlatStyle.Flat;
            this.btnAddKit.Location = new Point(0x1e8, 8);
            this.btnAddKit.Name = "btnAddKit";
            this.btnAddKit.Size = new Size(0x30, 0x2d);
            this.btnAddKit.TabIndex = 3;
            this.btnAddKit.Text = "Add Kit";
            this.ControlPricing1.Location = new Point(8, 8);
            this.ControlPricing1.Name = "ControlPricing1";
            this.ControlPricing1.Size = new Size(360, 0x30);
            this.ControlPricing1.TabIndex = 0;
            this.StatusStrip1.AutoSize = false;
            ToolStripItem[] itemArray3 = new ToolStripItem[] { this.tsslTotalBillable, this.tsslTotalAllowable };
            this.StatusStrip1.Items.AddRange(itemArray3);
            this.StatusStrip1.Location = new Point(0, 220);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new Size(600, 20);
            this.StatusStrip1.SizingGrip = false;
            this.StatusStrip1.TabIndex = 2;
            this.tsslTotalBillable.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslTotalBillable.Margin = new Padding(0, 1, 0, 0);
            this.tsslTotalBillable.Name = "tsslTotalBillable";
            this.tsslTotalBillable.Size = new Size(0x52, 0x13);
            this.tsslTotalBillable.Text = "Billable: $0.00";
            this.tsslTotalAllowable.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslTotalAllowable.Margin = new Padding(0, 1, 0, 0);
            this.tsslTotalAllowable.Name = "tsslTotalAllowable";
            this.tsslTotalAllowable.Size = new Size(0x60, 0x13);
            this.tsslTotalAllowable.Text = "Allowable: $0.00";
            base.Controls.Add(this.StatusStrip1);
            base.Name = "ControlOrderDetails";
            base.Size = new Size(600, 240);
            base.Controls.SetChildIndex(this.StatusStrip1, 0);
            base.Controls.SetChildIndex(this.Panel1, 0);
            base.Controls.SetChildIndex(this.Grid, 0);
            base.F_TableDetails.EndInit();
            this.Panel1.ResumeLayout(false);
            this.cmDelivery.ResumeLayout(false);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "ID", 40);
            Appearance.AddTextColumn("WarehouseName", "Warehouse", 80);
            Appearance.AddTextColumn("InventoryItemName", "Inventory Item", 100);
            Appearance.AddTextColumn("BillingCode", "Billing Code", 80);
            Appearance.AddTextColumn("PriceCodeName", "Price Code", 80);
            Appearance.AddTextColumn("SaleRentType", "Sale/Rent Type", 90);
            Appearance.AddTextColumn("State", "State", 60);
            Appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(this.Appearance_CellFormatting);
        }

        public void LoadGrid(MySqlConnection cnn, int OrderID)
        {
            this.LoadGrid_Delivery(cnn, OrderID);
        }

        public void LoadGrid_Delivery(MySqlConnection cnn, int OrderID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT
  tbl_inventoryitem.Name as InventoryItemName
, tbl_pricecode.Name as PriceCodeName
, IF(tbl_orderdetails.NextOrderID IS NULL, 0, 1) as Returned
, tbl_pricecode_item.Sale_AllowablePrice
, tbl_pricecode_item.Rent_AllowablePrice
, tbl_pricecode_item.Sale_BillablePrice
, tbl_pricecode_item.Rent_BillablePrice
, tbl_pricecode_item.DefaultCMNType
, tbl_warehouse.Name as WarehouseName
, tbl_orderdetails.*
FROM tbl_orderdetails
     LEFT JOIN tbl_inventoryitem ON tbl_orderdetails.InventoryItemID = tbl_inventoryitem.ID
     LEFT JOIN tbl_pricecode ON tbl_orderdetails.PriceCodeID = tbl_pricecode.ID
     LEFT JOIN tbl_pricecode_item ON tbl_orderdetails.InventoryItemID = tbl_pricecode_item.InventoryItemID
                                 AND tbl_orderdetails.PriceCodeID     = tbl_pricecode_item.PriceCodeID
     LEFT JOIN tbl_warehouse ON tbl_orderdetails.WarehouseID = tbl_warehouse.ID
WHERE (tbl_orderdetails.OrderID = {OrderID})", cnn))
            {
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                base.CloseDialogs();
                ControlOrderDetailsBase.TableOrderDetailsBase table = this.GetTable();
                table.BeginUpdate();
                try
                {
                    table.Clear();
                    adapter.Fill(table);
                    table.AcceptChanges();
                }
                finally
                {
                    table.EndUpdate();
                }
            }
        }

        private void mnuChangeState_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmDelivery.Tag as DataRow;
            if (tag != null)
            {
                TableOrderDetails table = (TableOrderDetails) tag.Table;
                try
                {
                    OrderDetailsState state = OrderDetailsState.New;
                    try
                    {
                        state = (OrderDetailsState) Conversions.ToInteger(Enum.Parse(typeof(OrderDetailsState), Conversions.ToString(tag[table.Col_State])));
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                    OrderDetailsState approved = OrderDetailsState.New;
                    if (sender == this.mnuChangeState_New)
                    {
                        approved = OrderDetailsState.New;
                    }
                    else if (sender == this.mnuChangeState_Approved)
                    {
                        approved = OrderDetailsState.Approved;
                    }
                    else if (sender == this.mnuChangeState_Pickup)
                    {
                        approved = OrderDetailsState.Pickup;
                    }
                    else if (sender == this.mnuChangeState_Closed)
                    {
                        approved = OrderDetailsState.Closed;
                    }
                    else if (sender == this.mnuChangeState_Canceled)
                    {
                        approved = OrderDetailsState.Canceled;
                    }
                    if (state != approved)
                    {
                        tag[table.Col_State] = approved.ToString();
                    }
                }
                catch (Exception exception2)
                {
                    Exception ex = exception2;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.ShowException(exception);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void mnuConfirm_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmDelivery.Tag as DataRow;
            if (tag != null)
            {
                TableOrderDetails table = (TableOrderDetails) tag.Table;
                try
                {
                    DateTime time;
                    OrderDetailsState state = OrderDetailsState.New;
                    try
                    {
                        state = (OrderDetailsState) Conversions.ToInteger(Enum.Parse(typeof(OrderDetailsState), Conversions.ToString(tag[table.Col_State]), true));
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                    if (state != OrderDetailsState.Pickup)
                    {
                        throw new UserNotifyException("Line Item must have Pickup state");
                    }
                    using (VBDateBox box = new VBDateBox())
                    {
                        box.Text = "Confirm pickup";
                        box.Prompt = "Select end date";
                        box.Value = NullableConvert.ToDateTime(tag[table.Col_EndDate]);
                        if (box.ShowDialog() == DialogResult.OK)
                        {
                            if (box.Value == null)
                            {
                                throw new UserNotifyException("You should select end date");
                            }
                            time = box.Value.Value;
                        }
                        else
                        {
                            return;
                        }
                    }
                    tag[table.Col_EndDate] = time;
                }
                catch (Exception exception2)
                {
                    Exception ex = exception2;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.ShowException(exception);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void mnuExchange_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmDelivery.Tag as DataRow;
            if (tag != null)
            {
                TableOrderDetails table = (TableOrderDetails) tag.Table;
                try
                {
                    OrderDetailsState state = OrderDetailsState.New;
                    try
                    {
                        state = (OrderDetailsState) Conversions.ToInteger(Enum.Parse(typeof(OrderDetailsState), Conversions.ToString(tag[table.Col_State])));
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                    if ((state != OrderDetailsState.Approved) && (state != OrderDetailsState.New))
                    {
                        MessageBox.Show("Line Item must have either New or Active state");
                    }
                    else
                    {
                        tag[table.Col_State] = 2.ToString();
                        DataRow row = table.NewRow();
                        row[table.Col_AcceptAssignment] = tag[table.Col_AcceptAssignment];
                        row[table.Col_AllowablePrice] = tag[table.Col_AllowablePrice];
                        row[table.Col_AuthorizationNumber] = tag[table.Col_AuthorizationNumber];
                        row[table.Col_AuthorizationTypeID] = tag[table.Col_AuthorizationTypeID];
                        row[table.Col_BillablePrice] = tag[table.Col_BillablePrice];
                        row[table.Col_BilledConverter] = tag[table.Col_BilledConverter];
                        row[table.Col_BilledQuantity] = tag[table.Col_BilledQuantity];
                        row[table.Col_BilledUnits] = tag[table.Col_BilledUnits];
                        row[table.Col_BilledWhen] = tag[table.Col_BilledWhen];
                        row[table.Col_BillingCode] = tag[table.Col_BillingCode];
                        row[table.Col_BillingMonth] = tag[table.Col_BillingMonth];
                        row[table.Col_BillIns1] = tag[table.Col_BillIns1];
                        row[table.Col_BillIns2] = tag[table.Col_BillIns2];
                        row[table.Col_BillIns3] = tag[table.Col_BillIns3];
                        row[table.Col_BillIns4] = tag[table.Col_BillIns4];
                        row[table.Col_BillItemOn] = tag[table.Col_BillItemOn];
                        row[table.Col_CMNFormID] = tag[table.Col_CMNFormID];
                        row[table.Col_DefaultCMNType] = tag[table.Col_DefaultCMNType];
                        row[table.Col_DeliveryConverter] = tag[table.Col_DeliveryConverter];
                        row[table.Col_DeliveryQuantity] = tag[table.Col_DeliveryQuantity];
                        row[table.Col_DeliveryUnits] = tag[table.Col_DeliveryUnits];
                        row[table.Col_DOSFrom] = tag[table.Col_DOSFrom];
                        row[table.Col_DOSTo] = tag[table.Col_DOSTo];
                        row[table.Col_DrugControlNumber] = tag[table.Col_DrugControlNumber];
                        row[table.Col_DrugNoteField] = tag[table.Col_DrugNoteField];
                        row[table.Col_DXPointer9] = tag[table.Col_DXPointer9];
                        row[table.Col_DXPointer10] = tag[table.Col_DXPointer10];
                        row[table.Col_EndDate] = DBNull.Value;
                        row[table.Col_FlatRate] = tag[table.Col_FlatRate];
                        row[table.Col_HaoDescription] = tag[table.Col_HaoDescription];
                        row[table.Col_InventoryItemName] = tag[table.Col_InventoryItemName];
                        row[table.Col_InventoryItemID] = tag[table.Col_InventoryItemID];
                        row[table.Col_MedicallyUnnecessary] = tag[table.Col_MedicallyUnnecessary];
                        row[table.Col_Modifier1] = tag[table.Col_Modifier1];
                        row[table.Col_Modifier2] = tag[table.Col_Modifier2];
                        row[table.Col_Modifier3] = tag[table.Col_Modifier3];
                        row[table.Col_Modifier4] = tag[table.Col_Modifier4];
                        row[table.Col_NopayIns1] = tag[table.Col_NopayIns1];
                        row[table.Col_OrderedConverter] = tag[table.Col_OrderedConverter];
                        row[table.Col_OrderedQuantity] = tag[table.Col_OrderedQuantity];
                        row[table.Col_OrderedUnits] = tag[table.Col_OrderedUnits];
                        row[table.Col_OrderedWhen] = tag[table.Col_OrderedWhen];
                        row[table.Col_PickupDate] = tag[table.Col_PickupDate];
                        row[table.Col_PriceCodeName] = tag[table.Col_PriceCodeName];
                        row[table.Col_PriceCodeID] = tag[table.Col_PriceCodeID];
                        row[table.Col_ReasonForPickup] = tag[table.Col_ReasonForPickup];
                        row[table.Col_Rent_AllowablePrice] = tag[table.Col_Rent_AllowablePrice];
                        row[table.Col_Rent_BillablePrice] = tag[table.Col_Rent_BillablePrice];
                        row[table.Col_ReviewCode] = tag[table.Col_ReviewCode];
                        row[table.Col_Sale_AllowablePrice] = tag[table.Col_Sale_AllowablePrice];
                        row[table.Col_Sale_BillablePrice] = tag[table.Col_Sale_BillablePrice];
                        row[table.Col_SaleRentType] = tag[table.Col_SaleRentType];
                        row[table.Col_SendCMN_RX_w_invoice] = tag[table.Col_SendCMN_RX_w_invoice];
                        row[table.Col_SerialID] = DBNull.Value;
                        row[table.Col_SerialNumber] = DBNull.Value;
                        row[table.Col_ShowSpanDates] = tag[table.Col_ShowSpanDates];
                        row[table.Col_SpecialCode] = tag[table.Col_SpecialCode];
                        row[table.Col_State] = 0.ToString();
                        row[table.Col_Taxable] = tag[table.Col_Taxable];
                        row[table.Col_WarehouseID] = tag[table.Col_WarehouseID];
                        row[table.Col_WarehouseName] = tag[table.Col_WarehouseName];
                        table.Rows.Add(row);
                    }
                }
                catch (Exception exception2)
                {
                    Exception ex = exception2;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.ShowException(exception);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void mnuPickup_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmDelivery.Tag as DataRow;
            if (tag != null)
            {
                TableOrderDetails table = (TableOrderDetails) tag.Table;
                try
                {
                    OrderDetailsState state = OrderDetailsState.New;
                    try
                    {
                        state = (OrderDetailsState) Conversions.ToInteger(Enum.Parse(typeof(OrderDetailsState), Conversions.ToString(tag[table.Col_State]), true));
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                    if ((state != OrderDetailsState.Approved) && (state != OrderDetailsState.New))
                    {
                        MessageBox.Show("Line Item must have either New or Active state");
                    }
                    else
                    {
                        tag[table.Col_State] = 2.ToString();
                    }
                }
                catch (Exception exception2)
                {
                    Exception ex = exception2;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.ShowException(exception);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void MyBase_Changed(object sender, EventArgs e)
        {
            double totalAllowable = 0.0;
            double totalBillable = 0.0;
            base.CalculateTotal(ref totalBillable, ref totalAllowable);
            this.tsslTotalAllowable.Text = $"Allowable: ${totalAllowable:0.00}";
            this.tsslTotalBillable.Text = $"Billable: ${totalBillable:0.00}";
        }

        private static void SaveChanges(TableOrderDetails table, DataRow[] changes, MySqlConnection cnn, int orderID, int customerID)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (changes == null)
            {
                throw new ArgumentNullException("changes");
            }
            if (cnn == null)
            {
                throw new ArgumentNullException("cnn");
            }
            using (MySqlDataAdapter adapter = new MySqlDataAdapter())
            {
                adapter.InsertCommand = cnn.CreateCommand();
                GenerateInsertCommand_OrderDetails(adapter.InsertCommand, orderID, customerID);
                adapter.UpdateCommand = cnn.CreateCommand();
                GenerateUpdateCommand_OrderDetails(adapter.UpdateCommand, orderID, customerID);
                adapter.DeleteCommand = cnn.CreateCommand();
                GenerateDeleteCommand_OrderDetails(adapter.DeleteCommand, orderID, customerID);
                MySqlTransaction transaction = cnn.BeginTransaction();
                try
                {
                    adapter.InsertCommand.Transaction = transaction;
                    adapter.UpdateCommand.Transaction = transaction;
                    adapter.DeleteCommand.Transaction = transaction;
                    adapter.ContinueUpdateOnError = false;
                    using (new DataAdapterEvents(adapter))
                    {
                        adapter.Update(changes);
                    }
                    transaction.Commit();
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception innerException = ex;
                    transaction.Rollback();
                    if (innerException is DBConcurrencyException)
                    {
                        DBConcurrencyException exception2 = (DBConcurrencyException) innerException;
                        if (exception2.Row.RowState == DataRowState.Deleted)
                        {
                            throw new UserNotifyException("Line cannot be deleted", innerException);
                        }
                        if (exception2.Row.RowState == DataRowState.Modified)
                        {
                            throw new UserNotifyException("Line cannot be modified", innerException);
                        }
                    }
                    throw;
                }
            }
            RunStoredProcedures(cnn, null, orderID, customerID);
        }

        public void SaveGrid(MySqlConnection cnn, int OrderID, int CustomerID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                this.SaveGrid_Delivery(cnn, OrderID, CustomerID);
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        public void SaveGrid_Delivery(MySqlConnection cnn, int OrderID, int CustomerID)
        {
            TableOrderDetails table = this.GetTable();
            List<DataRow> list = new List<DataRow>();
            int num = table.Rows.Count - 1;
            for (int i = 0; i <= num; i++)
            {
                DataRow item = table.Rows[i];
                DataRowState rowState = item.RowState;
                if ((rowState == DataRowState.Added) || ((rowState == DataRowState.Deleted) || (rowState == DataRowState.Modified)))
                {
                    list.Add(item);
                }
            }
            if (list.Count != 0)
            {
                SaveChanges(table, list.ToArray(), cnn, OrderID, CustomerID);
            }
        }

        public void ShowDetails(object OrderDetailsID)
        {
            try
            {
                DataTable tableSource = this.Grid.GetTableSource<DataTable>();
                if (tableSource != null)
                {
                    foreach (DataRow row in tableSource.Select($"[ID] = {OrderDetailsID}", "", DataViewRowState.CurrentRows))
                    {
                        base.EditRow(Conversions.ToInteger(row["RowID"]));
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

        [field: AccessedThroughProperty("cmDelivery")]
        private ContextMenuStrip cmDelivery { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuPickup")]
        private ToolStripMenuItem mnuPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuExchange")]
        private ToolStripMenuItem mnuExchange { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuConfirm")]
        private ToolStripMenuItem mnuConfirm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuChangeState")]
        private ToolStripMenuItem mnuChangeState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuChangeState_New")]
        private ToolStripMenuItem mnuChangeState_New { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuChangeState_Approved")]
        private ToolStripMenuItem mnuChangeState_Approved { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuChangeState_Pickup")]
        private ToolStripMenuItem mnuChangeState_Pickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuChangeState_Closed")]
        private ToolStripMenuItem mnuChangeState_Closed { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuChangeState_Canceled")]
        private ToolStripMenuItem mnuChangeState_Canceled { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnAddKit")]
        private Button btnAddKit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("StatusStrip1")]
        private StatusStrip StatusStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslTotalBillable")]
        private ToolStripStatusLabel tsslTotalBillable { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslTotalAllowable")]
        private ToolStripStatusLabel tsslTotalAllowable { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlPricing1")]
        private ControlPricing ControlPricing1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        protected override FilteredGrid Grid
        {
            get => 
                base.Grid;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler<GridContextMenuNeededEventArgs> handler = new EventHandler<GridContextMenuNeededEventArgs>(this.Grid_ContextPopup);
                FilteredGrid grid = base.Grid;
                if (grid != null)
                {
                    grid.RowContextMenuNeeded -= handler;
                }
                base.Grid = value;
                grid = base.Grid;
                if (grid != null)
                {
                    grid.RowContextMenuNeeded += handler;
                }
            }
        }

        private class DataAdapterEvents : MySqlDataAdapterEventsBase
        {
            public DataAdapterEvents(MySqlDataAdapter da) : base(da)
            {
            }

            protected override void ProcessRowUpdated(MySqlRowUpdatedEventArgs e)
            {
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && ((e.RecordsAffected == 1) && (e.Row.Table is ControlOrderDetails.TableOrderDetails))))
                {
                    ControlOrderDetails.TableOrderDetails table = (ControlOrderDetails.TableOrderDetails) e.Row.Table;
                    base.cmdSelectIdentity.Connection = e.Command.Connection;
                    try
                    {
                        base.cmdSelectIdentity.Transaction = e.Command.Transaction;
                        try
                        {
                            e.Row[table.Col_ID] = Conversions.ToInteger(base.cmdSelectIdentity.ExecuteScalar());
                        }
                        finally
                        {
                            base.cmdSelectIdentity.Transaction = null;
                        }
                    }
                    finally
                    {
                        base.cmdSelectIdentity.Connection = null;
                    }
                }
            }
        }

        private enum OrderDetailsState
        {
            New,
            Approved,
            Pickup,
            Closed,
            Canceled
        }

        public class TableOrderDetails : ControlOrderDetailsBase.TableOrderDetailsBase
        {
            public TableOrderDetails() : this("tbl_orderdetails")
            {
            }

            public TableOrderDetails(string TableName) : base(TableName)
            {
            }
        }
    }
}

