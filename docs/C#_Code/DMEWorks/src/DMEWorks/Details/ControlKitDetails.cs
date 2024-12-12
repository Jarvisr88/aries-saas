namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlKitDetails : ControlDetails
    {
        private IContainer components;
        public const string PriceCodeMustBeSelected = "-- Must be selected later --";

        public ControlKitDetails()
        {
            this.InitializeComponent();
        }

        public void ClearGrid()
        {
            base.CloseDialogs();
            TableKitDetails table = this.GetTable();
            table.Clear();
            table.AcceptChanges();
        }

        public void CloneGrid()
        {
            TableKitDetails table = this.GetTable();
            base.CloseDialogs();
            table.AcceptChanges();
            DataRow[] rowArray = table.Select();
            for (int i = 0; i < rowArray.Length; i++)
            {
                DataRow row1 = rowArray[i];
                row1[table.Col_ID] = DBNull.Value;
                row1.AcceptChanges();
                row1.SetAdded();
            }
        }

        protected override FormDetails CreateDialog(object param)
        {
            FormKitDetails dialog = new FormKitDetails(this);
            return base.AddDialog(dialog);
        }

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableKitDetails();

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
            if (inventoryItemID != null)
            {
                DataRow row = this.InternalAddNewItem(inventoryItemID.Value, priceCodeID, ClassGlobalObjects.DefaultWarehouseID);
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

        protected static void GenerateDeleteCommand_KitDetails(MySqlCommand cmd, int KitID)
        {
            cmd.Parameters.Add("KitID", MySqlType.Int).Value = KitID;
            cmd.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            cmd.GenerateDeleteCommand("tbl_kitdetails");
        }

        protected static void GenerateInsertCommand_KitDetails(MySqlCommand cmd, int KitID)
        {
            cmd.Parameters.Add("InventoryItemID", MySqlType.Int, 0, "InventoryItemID");
            cmd.Parameters.Add("PriceCodeID", MySqlType.Int, 0, "PriceCodeID");
            cmd.Parameters.Add("WarehouseID", MySqlType.Int, 0, "WarehouseID");
            cmd.Parameters.Add("Quantity", MySqlType.Int, 0, "Quantity");
            cmd.Parameters.Add("KitID", MySqlType.Int).Value = KitID;
            cmd.GenerateInsertCommand("tbl_kitdetails");
        }

        protected static void GenerateUpdateCommand_KitDetails(MySqlCommand cmd, int KitID)
        {
            cmd.Parameters.Add("InventoryItemID", MySqlType.Int, 0, "InventoryItemID");
            cmd.Parameters.Add("PriceCodeID", MySqlType.Int, 0, "PriceCodeID");
            cmd.Parameters.Add("WarehouseID", MySqlType.Int, 0, "WarehouseID");
            cmd.Parameters.Add("Quantity", MySqlType.Int, 0, "Quantity");
            cmd.Parameters.Add("KitID", MySqlType.Int).Value = KitID;
            cmd.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            string[] whereParameters = new string[] { "KitID", "ID" };
            cmd.GenerateUpdateCommand("tbl_kitdetails", whereParameters);
        }

        protected TableKitDetails GetTable() => 
            (TableKitDetails) base.F_TableDetails;

        private void Grid_RowContextMenuNeeded(object Sender, GridContextMenuNeededEventArgs e)
        {
            e.ContextMenu = this.cmsGoto;
            this.cmsGoto.Tag = e.Row;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ControlPricing1 = new ControlPricing();
            this.cmsGoto = new ContextMenuStrip(this.components);
            this.mnuInventoryItem = new ToolStripMenuItem();
            this.mnuPriceCode = new ToolStripMenuItem();
            this.mnuWarehouse = new ToolStripMenuItem();
            this.mnuPricing = new ToolStripMenuItem();
            base.F_TableDetails.BeginInit();
            this.Panel1.SuspendLayout();
            this.cmsGoto.SuspendLayout();
            base.SuspendLayout();
            this.Panel1.Controls.Add(this.ControlPricing1);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Size = new Size(480, 0x3f);
            this.Panel1.TabIndex = 0;
            this.Panel1.Controls.SetChildIndex(this.btnDelete, 0);
            this.Panel1.Controls.SetChildIndex(this.btnAdd, 0);
            this.Panel1.Controls.SetChildIndex(this.ControlPricing1, 0);
            this.btnDelete.Image = null;
            this.btnDelete.Location = new Point(0x1a8, 8);
            this.btnDelete.Size = new Size(0x30, 0x2d);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnAdd.Image = null;
            this.btnAdd.Location = new Point(370, 8);
            this.btnAdd.Size = new Size(0x30, 0x2d);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.Grid.Location = new Point(0, 0x3f);
            this.Grid.Size = new Size(480, 0xb1);
            this.Grid.TabIndex = 1;
            this.ControlPricing1.Location = new Point(4, 8);
            this.ControlPricing1.Name = "ControlPricing1";
            this.ControlPricing1.Size = new Size(360, 0x30);
            this.ControlPricing1.TabIndex = 0;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.mnuInventoryItem, this.mnuPriceCode, this.mnuWarehouse, this.mnuPricing };
            this.cmsGoto.Items.AddRange(toolStripItems);
            this.cmsGoto.Name = "cmDelivery";
            this.cmsGoto.Size = new Size(0x99, 0x72);
            this.mnuInventoryItem.Name = "mnuInventoryItem";
            this.mnuInventoryItem.Size = new Size(0x98, 0x16);
            this.mnuInventoryItem.Text = "Inventory Item";
            this.mnuPriceCode.Name = "mnuPriceCode";
            this.mnuPriceCode.Size = new Size(0x98, 0x16);
            this.mnuPriceCode.Text = "Price Code";
            this.mnuWarehouse.Name = "mnuWarehouse";
            this.mnuWarehouse.Size = new Size(0x98, 0x16);
            this.mnuWarehouse.Text = "Warehouse";
            this.mnuPricing.Name = "mnuPricing";
            this.mnuPricing.Size = new Size(0x98, 0x16);
            this.mnuPricing.Text = "Pricing";
            base.Name = "ControlKitDetails";
            base.Size = new Size(480, 240);
            base.Controls.SetChildIndex(this.Panel1, 0);
            base.Controls.SetChildIndex(this.Grid, 0);
            base.F_TableDetails.EndInit();
            this.Panel1.ResumeLayout(false);
            this.cmsGoto.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "ID", 40);
            Appearance.AddTextColumn("InventoryItem", "InventoryItem", 100);
            Appearance.AddTextColumn("PriceCode", "PriceCode", 100);
            Appearance.AddTextColumn("Warehouse", "Warehouse", 100);
            Appearance.AddTextColumn("Quantity", "Quantity", 60);
        }

        protected DataRow InternalAddNewItem(int InventoryItemID, int? PriceCodeID, int? WarehouseID)
        {
            DataRow row;
            TableKitDetails table = this.GetTable();
            string filterExpression = (PriceCodeID == null) ? $"([InventoryItemID] = {InventoryItemID}) AND ([PriceCodeID] IS NULL)" : $"([InventoryItemID] = {InventoryItemID}) AND ([PriceCodeID] = {PriceCodeID.Value})";
            DataRow[] rowArray = table.Select(filterExpression, "", DataViewRowState.CurrentRows);
            if (rowArray.Length != 0)
            {
                row = rowArray[0];
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        if (PriceCodeID == null)
                        {
                            command.CommandText = "SELECT cast(null as signed)   as PriceCodeID,\r\n       '-- Must be selected later --' as PriceCode,\r\n       tbl_inventoryitem.ID   as InventoryItemID,\r\n       tbl_inventoryitem.Name as InventoryItem,\r\n       tbl_warehouse.ID       as WarehouseID,\r\n       tbl_warehouse.Name     as Warehouse\r\nFROM tbl_inventoryitem\r\n     LEFT JOIN tbl_warehouse ON tbl_warehouse.ID = :WarehouseID\r\nWHERE (tbl_inventoryitem.ID = :InventoryItemID)";
                            command.Parameters.Add(":InventoryItemID", MySqlType.Int).Value = InventoryItemID;
                            command.Parameters.Add(":WarehouseID", MySqlType.Int).Value = NullableConvert.ToDb(WarehouseID);
                        }
                        else
                        {
                            command.CommandText = "SELECT tbl_pricecode.ID       as PriceCodeID,\r\n       tbl_pricecode.Name     as PriceCode,\r\n       tbl_inventoryitem.ID   as InventoryItemID,\r\n       tbl_inventoryitem.Name as InventoryItem,\r\n       tbl_warehouse.ID       as WarehouseID,\r\n       tbl_warehouse.Name     as Warehouse\r\nFROM tbl_pricecode_item\r\n     INNER JOIN tbl_pricecode ON tbl_pricecode_item.PriceCodeID = tbl_pricecode.ID\r\n     INNER JOIN tbl_inventoryitem ON tbl_pricecode_item.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_warehouse ON tbl_warehouse.ID = :WarehouseID\r\nWHERE (tbl_inventoryitem.ID = :InventoryItemID)\r\n  AND (tbl_pricecode.ID = :PriceCodeID)";
                            command.Parameters.Add(":InventoryItemID", MySqlType.Int).Value = InventoryItemID;
                            command.Parameters.Add(":PriceCodeID", MySqlType.Int).Value = PriceCodeID.Value;
                            command.Parameters.Add(":WarehouseID", MySqlType.Int).Value = NullableConvert.ToDb(WarehouseID);
                        }
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                row = null;
                            }
                            else
                            {
                                DataRow row2 = table.NewRow();
                                row2[table.Col_PriceCodeID] = reader["PriceCodeID"];
                                row2[table.Col_PriceCode] = reader["PriceCode"];
                                row2[table.Col_InventoryItemID] = reader["InventoryItemID"];
                                row2[table.Col_InventoryItem] = reader["InventoryItem"];
                                row2[table.Col_WarehouseID] = reader["WarehouseID"];
                                row2[table.Col_Warehouse] = reader["Warehouse"];
                                row2[table.Col_Quantity] = 1;
                                table.Rows.Add(row2);
                                row = row2;
                            }
                        }
                    }
                }
            }
            return row;
        }

        public void LoadGrid(MySqlConnection cnn, int KitID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT tbl_inventoryitem.Name as InventoryItem,
       IFNULL(tbl_pricecode.Name, '-- Must be selected later --') as PriceCode,
       tbl_warehouse.Name as Warehouse,
       tbl_kitdetails.*
FROM tbl_kitdetails
     LEFT JOIN tbl_inventoryitem ON tbl_kitdetails.InventoryItemID = tbl_inventoryitem.ID
     LEFT JOIN tbl_pricecode ON tbl_kitdetails.PriceCodeID = tbl_pricecode.ID
     LEFT JOIN tbl_warehouse ON tbl_kitdetails.WarehouseID = tbl_warehouse.ID
WHERE (tbl_kitdetails.KitID = {KitID})
ORDER BY tbl_kitdetails.ID", cnn))
            {
                TableKitDetails table = this.GetTable();
                base.CloseDialogs();
                table.Clear();
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.Fill(table);
                table.AcceptChanges();
            }
        }

        private void mnuInventoryItem_Click(object sender, EventArgs e)
        {
            DataRow tag = this.cmsGoto.Tag as DataRow;
            if (tag != null)
            {
                TableKitDetails table = tag.Table as TableKitDetails;
                if (table != null)
                {
                    int? nullable = NullableConvert.ToInt32(tag[table.Col_InventoryItemID]);
                    int? nullable2 = NullableConvert.ToInt32(tag[table.Col_PriceCodeID]);
                    int? nullable3 = NullableConvert.ToInt32(tag[table.Col_WarehouseID]);
                    FormParameters @params = new FormParameters();
                    if (sender == this.mnuInventoryItem)
                    {
                        @params["ID"] = nullable;
                        ClassGlobalObjects.ShowForm(FormFactories.FormInventoryItem(), @params);
                    }
                    else if (sender == this.mnuPriceCode)
                    {
                        @params["ID"] = nullable2;
                        ClassGlobalObjects.ShowForm(FormFactories.FormPriceCode(), @params);
                    }
                    else if (sender == this.mnuWarehouse)
                    {
                        @params["ID"] = nullable3;
                        ClassGlobalObjects.ShowForm(FormFactories.FormWarehouse(), @params);
                    }
                    else if (sender == this.mnuPricing)
                    {
                        @params["InventoryItemID"] = nullable;
                        @params["PriceCodeID"] = nullable2;
                        ClassGlobalObjects.ShowForm(FormFactories.FormPricing(), @params);
                    }
                }
            }
        }

        public void SaveGrid(MySqlConnection cnn, int KitID)
        {
            TableKitDetails table = this.GetTable();
            TableKitDetails changes = (TableKitDetails) table.GetChanges();
            if (changes != null)
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                {
                    adapter.InsertCommand = new MySqlCommand();
                    GenerateInsertCommand_KitDetails(adapter.InsertCommand, KitID);
                    adapter.UpdateCommand = new MySqlCommand();
                    GenerateUpdateCommand_KitDetails(adapter.UpdateCommand, KitID);
                    adapter.DeleteCommand = new MySqlCommand();
                    GenerateDeleteCommand_KitDetails(adapter.DeleteCommand, KitID);
                    using (new DataAdapterEvents(adapter))
                    {
                        MySqlTransaction transaction = cnn.BeginTransaction();
                        try
                        {
                            adapter.InsertCommand.Connection = cnn;
                            adapter.InsertCommand.Transaction = transaction;
                            adapter.UpdateCommand.Connection = cnn;
                            adapter.UpdateCommand.Transaction = transaction;
                            adapter.DeleteCommand.Connection = cnn;
                            adapter.DeleteCommand.Transaction = transaction;
                            adapter.ContinueUpdateOnError = false;
                            adapter.Update(changes);
                            table.MergeKeys(changes);
                            table.AcceptChanges();
                            transaction.Commit();
                        }
                        catch (Exception exception1)
                        {
                            Exception ex = exception1;
                            ProjectData.SetProjectError(ex);
                            Exception innerException = ex;
                            transaction.Rollback();
                            throw new Exception("SaveGrid", innerException);
                        }
                    }
                }
            }
        }

        [field: AccessedThroughProperty("ControlPricing1")]
        internal virtual ControlPricing ControlPricing1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGoto")]
        private ContextMenuStrip cmsGoto { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuInventoryItem")]
        private ToolStripMenuItem mnuInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuPriceCode")]
        private ToolStripMenuItem mnuPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWarehouse")]
        private ToolStripMenuItem mnuWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuPricing")]
        private ToolStripMenuItem mnuPricing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Browsable(false), DefaultValue(0x3ff), EditorBrowsable(EditorBrowsableState.Advanced)]
        public override AllowStateEnum AllowState
        {
            get => 
                base.AllowState;
            set
            {
                base.AllowState = value;
                this.Panel1.Visible = ((value & AllowStateEnum.AllowDelete) == AllowStateEnum.AllowDelete) || ((value & AllowStateEnum.AllowNew) == AllowStateEnum.AllowNew);
            }
        }

        protected override FilteredGrid Grid
        {
            get => 
                base.Grid;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler<GridContextMenuNeededEventArgs> handler = new EventHandler<GridContextMenuNeededEventArgs>(this.Grid_RowContextMenuNeeded);
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
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && ((e.RecordsAffected == 1) && (e.Row.Table is ControlKitDetails.TableKitDetails))))
                {
                    ControlKitDetails.TableKitDetails table = (ControlKitDetails.TableKitDetails) e.Row.Table;
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

        public class TableKitDetails : ControlDetails.TableDetails
        {
            public DataColumn Col_ID;
            public DataColumn Col_InventoryItemID;
            public DataColumn Col_InventoryItem;
            public DataColumn Col_PriceCodeID;
            public DataColumn Col_PriceCode;
            public DataColumn Col_WarehouseID;
            public DataColumn Col_Warehouse;
            public DataColumn Col_Quantity;

            public TableKitDetails() : this("tbl_kitdetails")
            {
            }

            public TableKitDetails(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_ID = base.Columns["ID"];
                this.Col_InventoryItemID = base.Columns["InventoryItemID"];
                this.Col_InventoryItem = base.Columns["InventoryItem"];
                this.Col_PriceCodeID = base.Columns["PriceCodeID"];
                this.Col_PriceCode = base.Columns["PriceCode"];
                this.Col_WarehouseID = base.Columns["WarehouseID"];
                this.Col_Warehouse = base.Columns["Warehouse"];
                this.Col_Quantity = base.Columns["Quantity"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_InventoryItemID = base.Columns.Add("InventoryItemID", typeof(int));
                this.Col_InventoryItemID.AllowDBNull = false;
                this.Col_InventoryItem = base.Columns.Add("InventoryItem", typeof(string));
                this.Col_PriceCodeID = base.Columns.Add("PriceCodeID", typeof(int));
                this.Col_PriceCode = base.Columns.Add("PriceCode", typeof(string));
                this.Col_WarehouseID = base.Columns.Add("WarehouseID", typeof(int));
                this.Col_Warehouse = base.Columns.Add("Warehouse", typeof(string));
                this.Col_Quantity = base.Columns.Add("Quantity", typeof(int));
                this.Col_Quantity.AllowDBNull = false;
            }

            public void MergeKeys(ControlKitDetails.TableKitDetails table)
            {
                int num2 = table.Rows.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    DataRow row = table.Rows[i];
                    DataRow row2 = base.Rows.Find(row["RowID"]);
                    if (row2 != null)
                    {
                        row2[this.Col_ID] = row["ID"];
                    }
                }
            }
        }
    }
}

