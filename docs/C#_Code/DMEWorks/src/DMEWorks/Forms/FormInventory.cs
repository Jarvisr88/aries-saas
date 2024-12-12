namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormInventory : DmeForm, IParameters
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private bool FLoadGridEnabled = true;

        public FormInventory()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
        }

        private void cmbInventoryItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
            this.ShowButton();
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
            this.ShowButton();
        }

        private void cmsGrid_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DataRow[] rowArray = this.Grid.GetSelectedRows().GetDataRows().ToArray<DataRow>();
            if (rowArray.Length == 1)
            {
                this.cmsGrid.Tag = rowArray[0];
                e.Cancel = false;
            }
        }

        private void cmsGridAdjust_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmsGrid.Tag as DataRow;
            if (tag != null)
            {
                int? nullable = NullableConvert.ToInt32(tag["InventoryItemID"]);
                int? nullable2 = NullableConvert.ToInt32(tag["WarehouseID"]);
                if ((nullable != null) && (nullable2 != null))
                {
                    FormParameters @params = new FormParameters {
                        ["WarehouseID"] = nullable2.Value,
                        ["InventoryItemID"] = nullable.Value
                    };
                    ClassGlobalObjects.ShowForm(FormFactories.FormInventoryAdjustment(), @params);
                }
            }
        }

        private void cmsGridTransferFrom_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmsGrid.Tag as DataRow;
            if (tag != null)
            {
                int? nullable = NullableConvert.ToInt32(tag["InventoryItemID"]);
                int? nullable2 = NullableConvert.ToInt32(tag["WarehouseID"]);
                if ((nullable != null) && (nullable2 != null))
                {
                    FormParameters @params = new FormParameters {
                        ["FromWarehouseID"] = nullable2.Value,
                        ["InventoryItemID"] = nullable.Value
                    };
                    ClassGlobalObjects.ShowForm(FormFactories.WizardInventoryTransfer(), @params, true);
                }
            }
        }

        private void cmsGridTransferTo_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmsGrid.Tag as DataRow;
            if (tag != null)
            {
                int? nullable = NullableConvert.ToInt32(tag["InventoryItemID"]);
                int? nullable2 = NullableConvert.ToInt32(tag["WarehouseID"]);
                if ((nullable != null) && (nullable2 != null))
                {
                    FormParameters @params = new FormParameters {
                        ["ToWarehouseID"] = nullable2.Value,
                        ["InventoryItemID"] = nullable.Value
                    };
                    ClassGlobalObjects.ShowForm(FormFactories.WizardInventoryTransfer(), @params, true);
                }
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

        private void Grid_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            DataRow dataRow = e.Row.GetDataRow();
            if (dataRow != null)
            {
                FormParameters @params = new FormParameters {
                    ["WarehouseID"] = Conversions.ToInteger(dataRow["WarehouseID"]),
                    ["InventoryItemID"] = Conversions.ToInteger(dataRow["InventoryItemID"])
                };
                ClassGlobalObjects.ShowForm(FormFactories.FormInventoryTransactions(), @params);
            }
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            Cache.InitDropdown(this.cmbInventoryItem, "tbl_inventoryitem", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormInventory));
            this.Grid = new FilteredGrid();
            this.lblWarehouse = new Label();
            this.Panel1 = new Panel();
            this.cmbInventoryItem = new Combobox();
            this.lblInventoryItem = new Label();
            this.cmbWarehouse = new Combobox();
            this.cmsGrid = new ContextMenuStrip(this.components);
            this.cmsGridAdjust = new ToolStripMenuItem();
            this.cmsGridTransferFrom = new ToolStripMenuItem();
            this.cmsGridTransferTo = new ToolStripMenuItem();
            this.ToolStrip1 = new ToolStrip();
            this.tsbAdjust = new ToolStripButton();
            this.tsbTransferFrom = new ToolStripButton();
            this.tsbTransferTo = new ToolStripButton();
            this.Panel1.SuspendLayout();
            this.cmsGrid.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x55);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x278, 0x170);
            this.Grid.TabIndex = 3;
            this.lblWarehouse.BackColor = Color.Transparent;
            this.lblWarehouse.Location = new Point(8, 8);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(80, 0x15);
            this.lblWarehouse.TabIndex = 0;
            this.lblWarehouse.Text = "Warehouse";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.Panel1.Controls.Add(this.lblWarehouse);
            this.Panel1.Controls.Add(this.cmbInventoryItem);
            this.Panel1.Controls.Add(this.lblInventoryItem);
            this.Panel1.Controls.Add(this.cmbWarehouse);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0x19);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x278, 60);
            this.Panel1.TabIndex = 2;
            this.cmbInventoryItem.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbInventoryItem.Location = new Point(0x60, 0x20);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.Size = new Size(0x210, 0x15);
            this.cmbInventoryItem.TabIndex = 3;
            this.lblInventoryItem.BackColor = Color.Transparent;
            this.lblInventoryItem.Location = new Point(8, 0x20);
            this.lblInventoryItem.Name = "lblInventoryItem";
            this.lblInventoryItem.Size = new Size(80, 0x15);
            this.lblInventoryItem.TabIndex = 2;
            this.lblInventoryItem.Text = "Inventory Item";
            this.lblInventoryItem.TextAlign = ContentAlignment.MiddleRight;
            this.cmbWarehouse.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbWarehouse.Location = new Point(0x60, 8);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(0x210, 0x15);
            this.cmbWarehouse.TabIndex = 1;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.cmsGridAdjust, this.cmsGridTransferFrom, this.cmsGridTransferTo };
            this.cmsGrid.Items.AddRange(toolStripItems);
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new Size(0x8f, 70);
            this.cmsGridAdjust.Name = "cmsGridAdjust";
            this.cmsGridAdjust.Size = new Size(0x8e, 0x16);
            this.cmsGridAdjust.Text = "Adjust";
            this.cmsGridTransferFrom.Name = "cmsGridTransferFrom";
            this.cmsGridTransferFrom.Size = new Size(0x8e, 0x16);
            this.cmsGridTransferFrom.Text = "Transfer From";
            this.cmsGridTransferTo.Name = "cmsGridTransferTo";
            this.cmsGridTransferTo.Size = new Size(0x8e, 0x16);
            this.cmsGridTransferTo.Text = "Transfer To";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.tsbAdjust, this.tsbTransferFrom, this.tsbTransferTo };
            this.ToolStrip1.Items.AddRange(itemArray2);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x278, 0x19);
            this.ToolStrip1.TabIndex = 20;
            this.ToolStrip1.Visible = false;
            this.tsbAdjust.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbAdjust.Image = (Image) manager.GetObject("tsbAdjust.Image");
            this.tsbAdjust.ImageTransparentColor = Color.Magenta;
            this.tsbAdjust.Name = "tsbAdjust";
            this.tsbAdjust.Size = new Size(0x2a, 0x16);
            this.tsbAdjust.Text = "Adjust";
            this.tsbTransferFrom.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbTransferFrom.Image = (Image) manager.GetObject("tsbTransferFrom.Image");
            this.tsbTransferFrom.ImageTransparentColor = Color.Magenta;
            this.tsbTransferFrom.Name = "tsbTransferFrom";
            this.tsbTransferFrom.Size = new Size(0x4f, 0x16);
            this.tsbTransferFrom.Text = "Transfer From";
            this.tsbTransferTo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbTransferTo.Image = (Image) manager.GetObject("tsbTransferTo.Image");
            this.tsbTransferTo.ImageTransparentColor = Color.Magenta;
            this.tsbTransferTo.Name = "tsbTransferTo";
            this.tsbTransferTo.Size = new Size(0x43, 0x16);
            this.tsbTransferTo.Text = "Transfer To";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.Panel1);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormInventory";
            this.Text = "Inventory";
            this.Panel1.ResumeLayout(false);
            this.cmsGrid.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("Warehouse", "Warehouse", 80);
            Appearance.AddTextColumn("InventoryItem", "Inventory Item", 100);
            Appearance.AddTextColumn("OnHand", "OnHand", 60, Appearance.IntegerStyle());
            Appearance.AddTextColumn("Rented", "Rented", 60, Appearance.IntegerStyle());
            Appearance.AddTextColumn("Sold", "Sold", 60, Appearance.IntegerStyle());
            Appearance.AddTextColumn("UnAvailable", "UnAvailable", 60, Appearance.IntegerStyle());
            Appearance.AddTextColumn("Committed", "Committed", 40, Appearance.IntegerStyle());
            Appearance.AddTextColumn("OnOrder", "OnOrder", 40, Appearance.IntegerStyle());
            Appearance.AddTextColumn("BackOrdered", "BackOrdered", 40, Appearance.IntegerStyle());
            Appearance.AddTextColumn("ReOrderPoint", "ReOrderPoint", 40, Appearance.IntegerStyle());
            Appearance.AddTextColumn("CostPerUnit", "Unit Cost", 60, Appearance.PriceStyle());
            Appearance.AddTextColumn("TotalCost", "Total", 60, Appearance.PriceStyle());
            Appearance.ContextMenuStrip = this.cmsGrid;
        }

        [HandleDatabaseChanged("tbl_inventory_transaction")]
        private void LoadGrid()
        {
            if (this.FLoadGridEnabled)
            {
                int? nullable = NullableConvert.ToInt32(this.cmbWarehouse.SelectedValue);
                int? nullable2 = NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);
                string selectCommandText = "SELECT\r\n  tbl_warehouse.ID as WarehouseID\r\n, tbl_warehouse.Name as Warehouse\r\n, tbl_inventoryitem.ID as InventoryItemID\r\n, tbl_inventoryitem.Name as InventoryItem\r\n, tbl_inventory.OnHand\r\n, tbl_inventory.Committed\r\n, tbl_inventory.OnOrder\r\n, tbl_inventory.UnAvailable\r\n, tbl_inventory.Rented\r\n, tbl_inventory.Sold\r\n, tbl_inventory.BackOrdered\r\n, tbl_inventory.ReOrderPoint\r\n, tbl_inventory.CostPerUnit\r\n, tbl_inventory.TotalCost\r\nFROM tbl_inventory\r\n     LEFT JOIN tbl_warehouse     ON tbl_warehouse    .ID = tbl_inventory.WarehouseID\r\n     LEFT JOIN tbl_inventoryitem ON tbl_inventoryitem.ID = tbl_inventory.InventoryItemID\r\nWHERE (1 = 1)\r\n";
                if (!((nullable != null) | (nullable2 != null)))
                {
                    selectCommandText = selectCommandText + "  AND (1 <> 1)\r\n";
                }
                else
                {
                    if (nullable != null)
                    {
                        selectCommandText = selectCommandText + "  AND (tbl_inventory.WarehouseID = " + Conversions.ToString(nullable.Value) + ")\r\n";
                    }
                    if (nullable2 != null)
                    {
                        selectCommandText = selectCommandText + "  AND (tbl_inventory.InventoryItemID = " + Conversions.ToString(nullable2.Value) + ")\r\n";
                    }
                }
                DataTable dataTable = new DataTable("tbl_inventory");
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    if (nullable2 != null)
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.CommandText = "inventory_refresh";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("WarehouseID", MySqlType.Int).Value = NullableConvert.ToDb(nullable);
                            command.Parameters.Add("InventoryItemID", MySqlType.Int).Value = NullableConvert.ToDb(nullable2);
                            command.ExecuteNonQuery();
                        }
                    }
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommandText, connection))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.MissingSchemaAction = MissingSchemaAction.Add;
                        adapter.Fill(dataTable);
                    }
                }
                this.Grid.GridSource = dataTable.ToGridSource();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SafeInvoke(new Action(this.LoadGrid));
        }

        protected void SetParameters(FormParameters Params)
        {
            try
            {
                this.FLoadGridEnabled = false;
                try
                {
                    if (Params != null)
                    {
                        int? nullable = NullableConvert.ToInt32(Params["InventoryItemID"]);
                        int? nullable2 = NullableConvert.ToInt32(Params["WarehouseID"]);
                        if (nullable != null)
                        {
                            this.cmbInventoryItem.SelectedValue = nullable.Value;
                        }
                        if (nullable2 != null)
                        {
                            this.cmbWarehouse.SelectedValue = nullable2.Value;
                        }
                    }
                }
                finally
                {
                    this.FLoadGridEnabled = true;
                }
                this.LoadGrid();
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

        private void ShowButton()
        {
            this.ToolStrip1.Visible = (NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue) != null) & (NullableConvert.ToInt32(this.cmbWarehouse.SelectedValue) != null);
        }

        private void tsbAdjust_Click(object sender, EventArgs e)
        {
            int? nullable = NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);
            int? nullable2 = NullableConvert.ToInt32(this.cmbWarehouse.SelectedValue);
            if ((nullable != null) && (nullable2 != null))
            {
                FormParameters @params = new FormParameters {
                    ["InventoryItemID"] = nullable.Value,
                    ["WarehouseID"] = nullable2.Value
                };
                ClassGlobalObjects.ShowForm(FormFactories.FormInventoryAdjustment(), @params);
            }
        }

        private void tsbTransferFrom_Click(object sender, EventArgs e)
        {
            int? nullable = NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);
            int? nullable2 = NullableConvert.ToInt32(this.cmbWarehouse.SelectedValue);
            if ((nullable != null) && (nullable2 != null))
            {
                FormParameters @params = new FormParameters {
                    ["InventoryItemID"] = nullable.Value,
                    ["FromWarehouseID"] = nullable2.Value
                };
                ClassGlobalObjects.ShowForm(FormFactories.WizardInventoryTransfer(), @params, true);
            }
        }

        private void tsbTransferTo_Click(object sender, EventArgs e)
        {
            int? nullable = NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);
            int? nullable2 = NullableConvert.ToInt32(this.cmbWarehouse.SelectedValue);
            if ((nullable != null) && (nullable2 != null))
            {
                FormParameters @params = new FormParameters {
                    ["InventoryItemID"] = nullable.Value,
                    ["ToWarehouseID"] = nullable2.Value
                };
                ClassGlobalObjects.ShowForm(FormFactories.WizardInventoryTransfer(), @params, true);
            }
        }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        private Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryItem")]
        private Label lblInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        private Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid")]
        private ContextMenuStrip cmsGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAdjust")]
        private ToolStripButton tsbAdjust { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbTransferTo")]
        private ToolStripButton tsbTransferTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbTransferFrom")]
        private ToolStripButton tsbTransferFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridTransferTo")]
        private ToolStripMenuItem cmsGridTransferTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridTransferFrom")]
        private ToolStripMenuItem cmsGridTransferFrom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridAdjust")]
        private ToolStripMenuItem cmsGridAdjust { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

