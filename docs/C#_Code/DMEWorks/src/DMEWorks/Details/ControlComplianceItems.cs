namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class ControlComplianceItems : DmeUserControl
    {
        private IContainer components;

        public event ChangedEventHandler Changed;

        public ControlComplianceItems()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
            this.FTable = new TableItems();
            this.Grid.GridSource = this.FTable.ToGridSource();
            this.cmbInventoryItem.EditButton = false;
            this.cmbInventoryItem.NewButton = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbInventoryItem.SelectedValue is int)
                {
                    DataRow row = this.FTable.NewRow();
                    row[this.FTable.Col_ID] = this.cmbInventoryItem.SelectedValue;
                    row[this.FTable.Col_Name] = this.cmbInventoryItem.Text;
                    this.FTable.Rows.Add(row);
                    this.cmbInventoryItem.SelectedValue = DBNull.Value;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataRow dataRow = this.Grid.CurrentRow.GetDataRow();
            if (dataRow != null)
            {
                dataRow.Delete();
            }
        }

        public void ClearGrid()
        {
            this.FTable.Clear();
            this.FTable.AcceptChanges();
            this.cmbInventoryItem.SelectedValue = DBNull.Value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            ChangedEventHandler changedEvent = this.ChangedEvent;
            if (changedEvent != null)
            {
                changedEvent(this, EventArgs.Empty);
            }
        }

        private void FTable_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            ChangedEventHandler changedEvent = this.ChangedEvent;
            if (changedEvent != null)
            {
                changedEvent(this, EventArgs.Empty);
            }
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbInventoryItem, "tbl_inventoryitem", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.gbItems = new GroupBox();
            this.Grid = new FilteredGrid();
            this.cmbInventoryItem = new Combobox();
            this.btnAdd = new Button();
            this.btnRemove = new Button();
            this.gbItems.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[] { this.Grid, this.cmbInventoryItem, this.btnAdd, this.btnRemove };
            this.gbItems.Controls.AddRange(controls);
            this.gbItems.Dock = DockStyle.Fill;
            this.gbItems.Name = "gbItems";
            this.gbItems.Size = new Size(0xe8, 0x120);
            this.gbItems.TabIndex = 0;
            this.gbItems.TabStop = false;
            this.gbItems.Text = "Inventory Items";
            this.Grid.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.Grid.Location = new Point(8, 40);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x98, 240);
            this.Grid.TabIndex = 3;
            this.cmbInventoryItem.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbInventoryItem.Location = new Point(8, 0x10);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.Size = new Size(0xd0, 0x15);
            this.cmbInventoryItem.TabIndex = 0;
            this.btnAdd.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnAdd.Location = new Point(0xa8, 0x30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x17);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnRemove.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnRemove.Location = new Point(0xa8, 80);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new Size(0x38, 0x17);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            Control[] controlArray2 = new Control[] { this.gbItems };
            base.Controls.AddRange(controlArray2);
            base.Name = "ControlComplianceItems";
            base.Size = new Size(0xe8, 0x120);
            this.gbItems.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "ID", 40);
            Appearance.AddTextColumn("Name", "Name", 400);
        }

        public void LoadGrid(MySqlConnection cnn, int ComplianceID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT tbl_inventoryitem.ID,\r\n       tbl_inventoryitem.Name\r\nFROM tbl_compliance_items\r\n     LEFT JOIN tbl_inventoryitem ON tbl_inventoryitem.ID = tbl_compliance_items.InventoryItemID\r\nWHERE tbl_compliance_items.ComplianceID = :ComplianceID", cnn))
            {
                adapter.SelectCommand.Parameters.Add("ComplianceID", MySqlType.Int).Value = ComplianceID;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.AcceptChangesDuringFill = true;
                this.FTable.Clear();
                adapter.Fill(this.FTable);
            }
            this.cmbInventoryItem.SelectedValue = DBNull.Value;
        }

        public void LoadGridFromOrder(MySqlConnection cnn, int OrderID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT tbl_inventoryitem.ID,\r\n       tbl_inventoryitem.Name\r\nFROM (tbl_orderdetails\r\n      LEFT JOIN tbl_inventoryitem ON tbl_inventoryitem.ID = tbl_orderdetails.InventoryItemID)\r\nWHERE tbl_orderdetails.OrderID = :OrderID", cnn))
            {
                adapter.SelectCommand.Parameters.Add("OrderID", MySqlType.Int).Value = OrderID;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.AcceptChangesDuringFill = true;
                this.FTable.Clear();
                adapter.Fill(this.FTable);
            }
            this.cmbInventoryItem.SelectedValue = DBNull.Value;
        }

        public void SaveGrid(MySqlConnection cnn, int ComplianceID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                MySqlTransaction transaction = cnn.BeginTransaction();
                try
                {
                    MySqlCommand command = new MySqlCommand("", cnn, transaction) {
                        Transaction = transaction,
                        CommandText = "DELETE FROM tbl_compliance_items WHERE ComplianceID = :ComplianceID"
                    };
                    command.Parameters.Add("ComplianceID", MySqlType.Int).Value = ComplianceID;
                    command.ExecuteNonQuery();
                    DataRow[] rowArray = this.FTable.Select("", "", DataViewRowState.CurrentRows);
                    if (0 < rowArray.Length)
                    {
                        command.CommandText = "INSERT INTO tbl_compliance_items (ComplianceID, InventoryItemID) VALUES (:ComplianceID, :InventoryItemID)";
                        command.Parameters.Clear();
                        command.Parameters.Add("ComplianceID", MySqlType.Int).Value = ComplianceID;
                        command.Parameters.Add("InventoryItemID", MySqlType.Int);
                        foreach (DataRow row in rowArray)
                        {
                            command.Parameters["InventoryItemID"].Value = row[this.FTable.Col_ID];
                            command.ExecuteNonQuery();
                            row.AcceptChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    transaction.Rollback();
                    ProjectData.ClearProjectError();
                }
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

        [field: AccessedThroughProperty("FTable")]
        private TableItems FTable { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbItems")]
        private GroupBox gbItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnAdd")]
        private Button btnAdd { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        private Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnRemove")]
        private Button btnRemove { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public delegate void ChangedEventHandler(object sender, EventArgs e);

        public class TableItems : DataTable
        {
            public readonly DataColumn Col_ID;
            public readonly DataColumn Col_Name;

            public TableItems()
            {
                base.TableName = "tbl_items";
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_ID.AllowDBNull = false;
                this.Col_Name = base.Columns.Add("Name", typeof(string));
                this.Col_Name.AllowDBNull = false;
                base.PrimaryKey = new DataColumn[] { this.Col_ID };
            }
        }
    }
}

