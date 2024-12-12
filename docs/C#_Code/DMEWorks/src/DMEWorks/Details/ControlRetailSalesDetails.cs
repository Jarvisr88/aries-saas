namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Billing;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ControlRetailSalesDetails : ControlOrderDetailsBase
    {
        private IContainer components;

        public ControlRetailSalesDetails()
        {
            this.InitializeComponent();
        }

        private void Appearance_DataError(object sender, GridDataErrorEventArgs e)
        {
        }

        private void cmbInventoryItem_ClickEdit(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters("ID", this.cmbInventoryItem.SelectedValue);
            ClassGlobalObjects.ShowForm(FormFactories.FormInventoryItem(), @params);
        }

        private void cmbInventoryItem_ClickNew(object sender, EventArgs e)
        {
            ClassGlobalObjects.ShowForm(FormFactories.FormInventoryItem());
        }

        private void cmbInventoryItem_InitDialog(object sender, InitDialogEventArgs e)
        {
            e.Appearance.Columns.Clear();
            e.Appearance.AddTextColumn("InventoryItem", "Inventory Item", 160);
            e.Appearance.AddTextColumn("PriceCode", "Price Code", 60);
            e.Appearance.AddTextColumn("Barcode", "Barcode", 60);
            e.Appearance.AddTextColumn("InventoryCode", "Inv Code", 80);
            e.Appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(Cache.InventoryItem_CellFormatting);
        }

        public static bool Confirm(string Text, string Caption) => 
            MessageBox.Show(Text, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected override FormDetails CreateDialog(object param)
        {
            FormRetailSalesDetail dialog = new FormRetailSalesDetail(this);
            return base.AddDialog(dialog);
        }

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableRetailSalesDetails();

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
            DataRow selectedRow = this.cmbInventoryItem.SelectedRow;
            if ((selectedRow != null) && (!NullableConvert.ToBoolean(selectedRow["Inactive"], false) || Confirm("Selected item is inactive. Are you sure you want to add it?", "RetailSales")))
            {
                int? nullable2 = NullableConvert.ToInt32(selectedRow["InventoryItemID"]);
                int? nullable3 = NullableConvert.ToInt32(selectedRow["PriceCodeID"]);
                if ((nullable2 != null) && (nullable3 != null))
                {
                    int? nullable;
                    using (DialogWarehouse warehouse = new DialogWarehouse())
                    {
                        if (warehouse.ShowDialog() == DialogResult.OK)
                        {
                            nullable = NullableConvert.ToInt32(warehouse.WarehouseID);
                            if (nullable == null)
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    int? quantity = null;
                    if (base.InternalAddNewItem(nullable2.Value, nullable3.Value, new int?(nullable.Value), quantity, true) == null)
                    {
                        MessageBox.Show("Selected pair Price Code - Inventory Item does not have record in pricing table");
                    }
                    else
                    {
                        this.cmbInventoryItem.SelectedValue = DBNull.Value;
                    }
                }
            }
        }

        public void DoScan()
        {
            this.ProcessBarcode(Interaction.InputBox("Please enter barcode manually or use scaner", "Barcode", "", -1, -1));
        }

        protected TableRetailSalesDetails GetTable() => 
            (TableRetailSalesDetails) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.btnStart = new Button();
            this.btnScan = new Button();
            this.btnSave = new Button();
            this.cmbInventoryItem = new Combobox();
            base.F_TableDetails.BeginInit();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.Panel1.Controls.Add(this.btnScan);
            this.Panel1.Controls.Add(this.btnSave);
            this.Panel1.Controls.Add(this.btnStart);
            this.Panel1.Controls.Add(this.cmbInventoryItem);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Size = new Size(680, 0x34);
            this.Panel1.TabIndex = 0;
            this.Panel1.Controls.SetChildIndex(this.cmbInventoryItem, 0);
            this.Panel1.Controls.SetChildIndex(this.btnStart, 0);
            this.Panel1.Controls.SetChildIndex(this.btnSave, 0);
            this.Panel1.Controls.SetChildIndex(this.btnScan, 0);
            this.Panel1.Controls.SetChildIndex(this.btnAdd, 0);
            this.Panel1.Controls.SetChildIndex(this.btnDelete, 0);
            this.btnDelete.Image = null;
            this.btnDelete.Location = new Point(0x1f0, 8);
            this.btnDelete.Size = new Size(0x30, 0x24);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete F4";
            this.btnAdd.Image = null;
            this.btnAdd.Location = new Point(440, 8);
            this.btnAdd.Size = new Size(0x30, 0x24);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add F3";
            this.Grid.Location = new Point(0, 0x34);
            this.Grid.Size = new Size(680, 0x10c);
            this.Grid.TabIndex = 1;
            this.btnStart.FlatStyle = FlatStyle.Flat;
            this.btnStart.Location = new Point(8, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(0x30, 0x24);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start F2";
            this.btnScan.FlatStyle = FlatStyle.Flat;
            this.btnScan.Location = new Point(0x260, 8);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new Size(0x30, 0x24);
            this.btnScan.TabIndex = 5;
            this.btnScan.Text = "Scan F8";
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Location = new Point(0x228, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x30, 0x24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save F6";
            this.cmbInventoryItem.Location = new Point(0x3e, 14);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.Size = new Size(0x174, 0x15);
            this.cmbInventoryItem.TabIndex = 1;
            base.Name = "ControlRetailSalesDetails";
            base.Size = new Size(680, 320);
            base.F_TableDetails.EndInit();
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AllowEdit = true;
            Appearance.AddTextColumn("InventoryItemName", "Inv Item", 80);
            Appearance.AddTextColumn("PriceCodeName", "Price Code", 80);
            Appearance.AddTextColumn("SaleRentType", "Sale/Rent Type", 80);
            Appearance.AddTextColumn("DeliveryQuantity", "Delivery Qty", 80, Appearance.IntegerStyle()).ReadOnly = false;
            Appearance.AddTextColumn("BillablePrice", "Billable", 60, Appearance.PriceStyle()).ReadOnly = false;
            Appearance.AddTextColumn("AllowablePrice", "Allowable", 60, Appearance.PriceStyle()).ReadOnly = false;
            Appearance.AddBoolColumn("Taxable", "Taxable", 50).ReadOnly = false;
            Appearance.AddTextColumn("Amount", "Amount", 60, Appearance.PriceStyle());
            Appearance.DataError += new EventHandler<GridDataErrorEventArgs>(this.Appearance_DataError);
        }

        [HandleDatabaseChanged(new string[] { "tbl_inventoryitem", "tbl_pricecode", "tbl_pricecode_item" })]
        private void Load_Table_InventoryItem_PriceCode()
        {
            DataTable dataTable = new DataTable("tbl_inventoryitem");
            DataRow row = dataTable.NewRow();
            dataTable.Rows.Add(row);
            row.AcceptChanges();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  tbl_inventoryitem.ID as InventoryItemID,\r\n  tbl_inventoryitem.Name as InventoryItem,\r\n  view_pricecode.ID as PriceCodeID,\r\n  view_pricecode.Name as PriceCode,\r\n  tbl_inventoryitem.Barcode,\r\n  tbl_inventoryitem.InventoryCode,\r\n  tbl_inventoryitem.ModelNumber,\r\n  tbl_inventoryitem.Inactive\r\nFROM tbl_pricecode_item\r\n     INNER JOIN view_pricecode ON tbl_pricecode_item.PriceCodeID = view_pricecode.ID\r\n     INNER JOIN tbl_inventoryitem ON tbl_pricecode_item.InventoryItemID = tbl_inventoryitem.ID\r\nWHERE view_pricecode.IsRetail = 1\r\n  AND IFNULL(tbl_inventoryitem.Inactive, 0) = 0\r\nORDER BY tbl_inventoryitem.Name", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            dataTable.Columns.Add("Name", typeof(string), "[InventoryItem] + ' - ' + [PriceCode]");
            Functions.AssignDatasource(this.cmbInventoryItem, dataTable, "Name", "InventoryItemID");
            this.cmbInventoryItem.DrawItem += new ComboboxDrawItemEventHandler(Cache.DropdownInventoryItemEvents.DrawItem);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SafeInvoke(new Action(this.Load_Table_InventoryItem_PriceCode));
        }

        public void ProcessBarcode(string Barcode)
        {
            Barcode ??= "";
            this.cmbInventoryItem.SelectItem($"[Barcode] = '{Barcode.Replace("'", "''")}'");
        }

        public void SaveGrid(MySqlConnection cnn, MySqlTransaction tran, int OrderID, int CustomerID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                this.SaveOrderDetails(cnn, tran, OrderID, CustomerID);
                RunStoredProcedures(cnn, tran, OrderID, CustomerID);
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

        protected void SaveOrderDetails(MySqlConnection cnn, MySqlTransaction tran, int OrderID, int CustomerID)
        {
            TableRetailSalesDetails table = this.GetTable();
            TableRetailSalesDetails changes = (TableRetailSalesDetails) table.GetChanges(DataRowState.Added);
            if (changes != null)
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                {
                    adapter.ContinueUpdateOnError = false;
                    adapter.InsertCommand = new MySqlCommand();
                    adapter.InsertCommand.Connection = cnn;
                    adapter.InsertCommand.Transaction = tran;
                    GenerateInsertCommand_OrderDetails(adapter.InsertCommand, OrderID, CustomerID);
                    using (new DataAdapterEvents_OrderDetails(adapter))
                    {
                        adapter.Update(changes);
                        table.Merge(changes);
                    }
                }
            }
        }

        [field: AccessedThroughProperty("btnStart")]
        public virtual Button btnStart { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnScan")]
        public virtual Button btnScan { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        private Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnSave")]
        public virtual Button btnSave { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private class DataAdapterEvents_OrderDetails : MySqlDataAdapterEventsBase
        {
            public DataAdapterEvents_OrderDetails(MySqlDataAdapter da) : base(da)
            {
            }

            protected override void ProcessRowUpdated(MySqlRowUpdatedEventArgs e)
            {
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && ((e.RecordsAffected == 1) && (e.Row.Table is ControlRetailSalesDetails.TableRetailSalesDetails))))
                {
                    ControlRetailSalesDetails.TableRetailSalesDetails table = (ControlRetailSalesDetails.TableRetailSalesDetails) e.Row.Table;
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

        public class TableRetailSalesDetails : ControlOrderDetailsBase.TableOrderDetailsBase
        {
            public DataColumn Col_Amount;

            public TableRetailSalesDetails() : this("tbl_retailsalesdetails")
            {
            }

            public TableRetailSalesDetails(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_Amount = base.Columns["Amount"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                this.Col_Amount = base.Columns.Add("Amount", typeof(double));
            }

            public void Merge(ControlRetailSalesDetails.TableRetailSalesDetails table)
            {
                int num2 = table.Rows.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    DataRow row = table.Rows[i];
                    DataRow row2 = base.Rows.Find(row["RowID"]);
                    if (row2 != null)
                    {
                        row2[base.Col_ID] = row[table.Col_ID];
                    }
                }
            }

            protected override void OnColumnChanged(DataColumnChangeEventArgs e)
            {
                if (ReferenceEquals(e.Column, base.Col_DeliveryQuantity) || (ReferenceEquals(e.Column, base.Col_DeliveryConverter) || ReferenceEquals(e.Column, base.Col_BilledConverter)))
                {
                    DataRowVersion version = !e.Row.HasVersion(DataRowVersion.Proposed) ? DataRowVersion.Default : DataRowVersion.Proposed;
                    double deliveredConverter = e.Row.IsNull(base.Col_DeliveryConverter, version) ? 1.0 : Conversions.ToDouble(e.Row[base.Col_DeliveryConverter, version]);
                    double billedConverter = e.Row.IsNull(base.Col_BilledConverter, version) ? 1.0 : Conversions.ToDouble(e.Row[base.Col_BilledConverter, version]);
                    double deliveryQty = e.Row.IsNull(base.Col_DeliveryQuantity, version) ? 0.0 : Conversions.ToDouble(e.Row[base.Col_DeliveryQuantity, version]);
                    double num4 = 0.0;
                    try
                    {
                        num4 = Converter.DeliveredQty2BilledQty(deliveryQty, deliveredConverter, billedConverter);
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        num4 = 0.0;
                        ProjectData.ClearProjectError();
                    }
                    e.Row.BeginEdit();
                    e.Row[base.Col_BilledQuantity] = num4;
                    e.Row.EndEdit();
                }
                else if (ReferenceEquals(e.Column, base.Col_BillablePrice) || (ReferenceEquals(e.Column, base.Col_AllowablePrice) || (ReferenceEquals(e.Column, base.Col_BilledQuantity) || ReferenceEquals(e.Column, base.Col_Taxable))))
                {
                    double num7;
                    DataRowVersion version = !e.Row.HasVersion(DataRowVersion.Proposed) ? DataRowVersion.Default : DataRowVersion.Proposed;
                    bool flag = false;
                    if (!e.Row.IsNull(base.Col_Taxable, version))
                    {
                        flag = Conversions.ToBoolean(e.Row[base.Col_Taxable, version]);
                    }
                    double num5 = 0.0;
                    if (flag)
                    {
                        if (!e.Row.IsNull(base.Col_AllowablePrice, version))
                        {
                            num5 = Conversions.ToDouble(e.Row[base.Col_AllowablePrice, version]);
                        }
                    }
                    else if (!e.Row.IsNull(base.Col_BillablePrice, version))
                    {
                        num5 = Conversions.ToDouble(e.Row[base.Col_BillablePrice, version]);
                    }
                    double num6 = 0.0;
                    if (!e.Row.IsNull(base.Col_BilledQuantity, version))
                    {
                        num6 = Conversions.ToDouble(e.Row[base.Col_BilledQuantity, version]);
                    }
                    try
                    {
                        num7 = num6 * num5;
                    }
                    catch (Exception exception2)
                    {
                        ProjectData.SetProjectError(exception2);
                        num7 = 0.0;
                        ProjectData.ClearProjectError();
                    }
                    e.Row.BeginEdit();
                    e.Row[this.Col_Amount] = num7;
                    e.Row.EndEdit();
                }
                base.OnColumnChanged(e);
            }
        }
    }
}

