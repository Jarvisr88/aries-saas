namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    [DesignerGenerated]
    public class DialogReorder : Form
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private int? FCustomerID;
        private const string Key_MultiplePurchaseOrders = "DialogReorder.MultiplePurchaseOrders";

        public DialogReorder()
        {
            base.Load += new EventHandler(this.DialogReorder_Load);
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TableGridData tableSource = this.Grid.GetTableSource<TableGridData>();
            if (tableSource != null)
            {
                if (this.chbMultiplePurchaseOrders.Checked)
                {
                    foreach (DataRow row in tableSource.Select())
                    {
                        if (Convert.ToBoolean(row[tableSource.Col_Checked]))
                        {
                            FormParameters @params = new FormParameters();
                            using (StringWriter writer = new StringWriter())
                            {
                                XmlTextWriter writer2 = new XmlTextWriter(writer);
                                writer2.WriteStartElement("Order");
                                writer2.WriteStartElement("Item");
                                writer2.WriteElementString("InventoryItemID", Convert.ToString(row[tableSource.Col_InventoryItemID]));
                                if (this.FCustomerID != null)
                                {
                                    writer2.WriteElementString("CustomerID", Convert.ToString(this.FCustomerID.Value));
                                }
                                writer2.WriteElementString("WarehouseID", Convert.ToString(row[tableSource.Col_WarehouseID]));
                                writer2.WriteElementString("Quantity", Convert.ToString(row[tableSource.Col_DeliveryQuantity]));
                                writer2.WriteEndElement();
                                writer2.WriteEndElement();
                                @params["OrderXml"] = writer.ToString();
                                ClassGlobalObjects.ShowForm(FormFactories.FormPurchaseOrder(), @params);
                            }
                        }
                    }
                }
                else
                {
                    int num2 = 0;
                    FormParameters @params = new FormParameters();
                    using (StringWriter writer3 = new StringWriter())
                    {
                        XmlTextWriter writer4 = new XmlTextWriter(writer3);
                        writer4.WriteStartElement("Order");
                        DataRow[] rowArray2 = tableSource.Select();
                        int index = 0;
                        while (true)
                        {
                            if (index >= rowArray2.Length)
                            {
                                writer4.WriteEndElement();
                                @params["OrderXml"] = writer3.ToString();
                                break;
                            }
                            DataRow row2 = rowArray2[index];
                            if (Convert.ToBoolean(row2[tableSource.Col_Checked]))
                            {
                                num2++;
                                writer4.WriteStartElement("Item");
                                writer4.WriteElementString("InventoryItemID", Convert.ToString(row2[tableSource.Col_InventoryItemID]));
                                if (this.FCustomerID != null)
                                {
                                    writer4.WriteElementString("CustomerID", Convert.ToString(this.FCustomerID.Value));
                                }
                                writer4.WriteElementString("WarehouseID", Convert.ToString(row2[tableSource.Col_WarehouseID]));
                                writer4.WriteElementString("Quantity", Convert.ToString(row2[tableSource.Col_DeliveryQuantity]));
                                writer4.WriteEndElement();
                            }
                            index++;
                        }
                    }
                    if (0 < num2)
                    {
                        ClassGlobalObjects.ShowForm(FormFactories.FormPurchaseOrder(), @params);
                    }
                }
            }
            base.Close();
        }

        private void chbShowAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            MultiplePurchaseOrders = this.chbMultiplePurchaseOrders.Checked;
        }

        private void DialogReorder_Load(object sender, EventArgs e)
        {
            this.chbMultiplePurchaseOrders.Checked = MultiplePurchaseOrders;
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public static TableGridData FillDetailsTable(ControlOrderDetailsBase.TableOrderDetailsBase Details, bool AllItems)
        {
            if (Details == null)
            {
                throw new ArgumentNullException("Table");
            }
            TableGridData data = new TableGridData();
            foreach (DataRow row in Details.Select())
            {
                data.ImportRow(row);
            }
            if (AllItems)
            {
                DataRow[] rowArray2 = data.Select();
                for (int i = 0; i < rowArray2.Length; i++)
                {
                    rowArray2[i][data.Col_Checked] = true;
                }
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        using (MySqlCommand command2 = new MySqlCommand("", connection))
                        {
                            command.CommandText = "CALL `inventory_refresh`(:WarehouseID, :InventoryItemID)";
                            command.Parameters.Add("WarehouseID", MySqlType.Int);
                            command.Parameters.Add("InventoryItemID", MySqlType.Int);
                            command2.CommandText = "SELECT\r\n  tbl_inventory.OnHand\r\n, tbl_inventoryitem.Service\r\nFROM tbl_inventory\r\n     LEFT JOIN tbl_inventoryitem ON tbl_inventory.InventoryItemID = tbl_inventoryitem.ID\r\nWHERE (tbl_inventory.WarehouseID = :WarehouseID)\r\n  AND (tbl_inventory.InventoryItemID = :InventoryItemID)";
                            command2.Parameters.Add("WarehouseID", MySqlType.Int);
                            command2.Parameters.Add("InventoryItemID", MySqlType.Int);
                            foreach (DataRow row2 in data.Select())
                            {
                                bool flag = false;
                                if (row2.IsNull(data.Col_ID) && (!row2.IsNull(data.Col_WarehouseID) && !row2.IsNull(data.Col_InventoryItemID)))
                                {
                                    command.Parameters["WarehouseID"].Value = row2[data.Col_WarehouseID];
                                    command.Parameters["InventoryItemID"].Value = row2[data.Col_InventoryItemID];
                                    command.ExecuteNonQuery();
                                    command2.Parameters["WarehouseID"].Value = row2[data.Col_WarehouseID];
                                    command2.Parameters["InventoryItemID"].Value = row2[data.Col_InventoryItemID];
                                    using (MySqlDataReader reader = command2.ExecuteReader(CommandBehavior.Default))
                                    {
                                        flag = !reader.Read() || (!ToBoolean(reader["Service"], false) && (ToInt64(reader["OnHand"], 0L) < ToInt64(row2[data.Col_DeliveryQuantity], 0L)));
                                    }
                                }
                                row2[data.Col_Checked] = flag;
                            }
                        }
                    }
                }
            }
            data.AcceptChanges();
            return data;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Grid = new FilteredGrid();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.chbMultiplePurchaseOrders = new CheckBox();
            Label label = new Label();
            base.SuspendLayout();
            label.Location = new Point(8, 8);
            label.Name = "Label1";
            label.Size = new Size(0x178, 0x18);
            label.TabIndex = 0;
            label.Text = "Items that will be included into new purchase order";
            this.Grid.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.Grid.Location = new Point(8, 0x20);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x178, 0xd0);
            this.Grid.TabIndex = 1;
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.Location = new Point(0xe8, 0xf8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x138, 0xf8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x17);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.chbMultiplePurchaseOrders.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.chbMultiplePurchaseOrders.AutoSize = true;
            this.chbMultiplePurchaseOrders.Location = new Point(8, 0xf8);
            this.chbMultiplePurchaseOrders.Name = "chbMultiplePurchaseOrders";
            this.chbMultiplePurchaseOrders.Size = new Size(0x90, 0x11);
            this.chbMultiplePurchaseOrders.TabIndex = 2;
            this.chbMultiplePurchaseOrders.Text = "Multiple Purchase Orders";
            this.chbMultiplePurchaseOrders.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x188, 0x115);
            base.Controls.Add(this.chbMultiplePurchaseOrders);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(label);
            base.Controls.Add(this.Grid);
            base.Name = "DialogReorder";
            base.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "Reorder";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void InitializeDialog(int? CustomerID, TableGridData Details)
        {
            if (Details == null)
            {
                throw new ArgumentNullException("Table");
            }
            this.FCustomerID = CustomerID;
            this.Grid.GridSource = Details.ToGridSource();
        }

        protected void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.AllowEdit = true;
            Appearance.Columns.Clear();
            Appearance.AddBoolColumn("Checked", "...", 30).ReadOnly = false;
            Appearance.AddTextColumn("ID", "ID", 40);
            Appearance.AddTextColumn("WarehouseName", "Warehouse", 80);
            Appearance.AddTextColumn("InventoryItemName", "Inventory Item", 100);
            Appearance.AddTextColumn("BillingCode", "Billing Code", 80);
            Appearance.AddTextColumn("PriceCodeName", "Price Code", 80);
            Appearance.AddTextColumn("SaleRentType", "Sale/Rent Type", 90);
        }

        private static bool ToBoolean(object value, bool @default)
        {
            if (value is IConvertible)
            {
                try
                {
                    return ((IConvertible) value).ToBoolean(CultureInfo.InvariantCulture);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException exception = ex;
                    ProjectData.ClearProjectError();
                }
            }
            return @default;
        }

        private static long ToInt64(object value, long @default)
        {
            if (value is IConvertible)
            {
                try
                {
                    return ((IConvertible) value).ToInt64(CultureInfo.InvariantCulture);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException exception = ex;
                    ProjectData.ClearProjectError();
                }
            }
            return @default;
        }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbMultiplePurchaseOrders")]
        private CheckBox chbMultiplePurchaseOrders { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public static bool MultiplePurchaseOrders
        {
            get => 
                UserSettings.GetBool("DialogReorder.MultiplePurchaseOrders", true);
            set => 
                UserSettings.SetBool("DialogReorder.MultiplePurchaseOrders", value);
        }

        public class TableGridData : TableBase
        {
            public DataColumn Col_Checked;
            public DataColumn Col_ID;
            public DataColumn Col_InventoryItemID;
            public DataColumn Col_InventoryItem;
            public DataColumn Col_PriceCodeID;
            public DataColumn Col_PriceCode;
            public DataColumn Col_SaleRentType;
            public DataColumn Col_SerialID;
            public DataColumn Col_BillingCode;
            public DataColumn Col_WarehouseID;
            public DataColumn Col_WarehouseName;
            public DataColumn Col_BillablePrice;
            public DataColumn Col_AllowablePrice;
            public DataColumn Col_DeliveryQuantity;

            public TableGridData() : this("tbl_details")
            {
            }

            public TableGridData(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                this.Col_Checked = base.Columns["Checked"];
                this.Col_ID = base.Columns["ID"];
                this.Col_InventoryItemID = base.Columns["InventoryItemID"];
                this.Col_InventoryItem = base.Columns["InventoryItemName"];
                this.Col_PriceCodeID = base.Columns["PriceCodeID"];
                this.Col_PriceCode = base.Columns["PriceCodeName"];
                this.Col_SaleRentType = base.Columns["SaleRentType"];
                this.Col_SerialID = base.Columns["SerialID"];
                this.Col_BillingCode = base.Columns["BillingCode"];
                this.Col_WarehouseID = base.Columns["WarehouseID"];
                this.Col_WarehouseName = base.Columns["WarehouseName"];
                this.Col_BillablePrice = base.Columns["BillablePrice"];
                this.Col_AllowablePrice = base.Columns["AllowablePrice"];
                this.Col_DeliveryQuantity = base.Columns["DeliveryQuantity"];
            }

            protected override void InitializeClass()
            {
                this.Col_Checked = base.Columns.Add("Checked", typeof(bool));
                this.Col_Checked.AllowDBNull = false;
                this.Col_Checked.DefaultValue = "True";
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_InventoryItemID = base.Columns.Add("InventoryItemID", typeof(int));
                this.Col_InventoryItem = base.Columns.Add("InventoryItemName", typeof(string));
                this.Col_PriceCodeID = base.Columns.Add("PriceCodeID", typeof(int));
                this.Col_PriceCode = base.Columns.Add("PriceCodeName", typeof(string));
                this.Col_SaleRentType = base.Columns.Add("SaleRentType", typeof(string));
                this.Col_SerialID = base.Columns.Add("SerialID", typeof(int));
                this.Col_BillingCode = base.Columns.Add("BillingCode", typeof(string));
                this.Col_WarehouseID = base.Columns.Add("WarehouseID", typeof(int));
                this.Col_WarehouseName = base.Columns.Add("WarehouseName", typeof(string));
                this.Col_BillablePrice = base.Columns.Add("BillablePrice", typeof(double));
                this.Col_AllowablePrice = base.Columns.Add("AllowablePrice", typeof(double));
                this.Col_DeliveryQuantity = base.Columns.Add("DeliveryQuantity", typeof(int));
            }
        }
    }
}

