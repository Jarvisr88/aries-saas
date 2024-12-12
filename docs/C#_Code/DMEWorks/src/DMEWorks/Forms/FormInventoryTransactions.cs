namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormInventoryTransactions : DmeForm, IParameters
    {
        private IContainer components;
        private int F_InventoryItemID;
        private int F_WarehouseID;

        public FormInventoryTransactions()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
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
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    this.ShowTransaction(dataRow["ID"]);
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Grid = new FilteredGrid();
            this.Panel1 = new Panel();
            this.cvWarehouse = new ControlValue();
            this.cvInventoryItem = new ControlValue();
            this.pnlValues = new Panel();
            this.cvTotalCost = new ControlValue();
            this.cvBackOrdered = new ControlValue();
            this.cvReorderPoint = new ControlValue();
            this.cvOnOrder = new ControlValue();
            this.cvOnHand = new ControlValue();
            this.cvCommitted = new ControlValue();
            this.cvUnavailable = new ControlValue();
            this.cvSold = new ControlValue();
            this.cvRented = new ControlValue();
            this.cvCostPerUnit = new ControlValue();
            this.ToolStrip1 = new ToolStrip();
            this.tsbAdjust = new ToolStripButton();
            this.tsbTransferTo = new ToolStripButton();
            this.tsbTransferFrom = new ToolStripButton();
            this.tsbShowDetails = new ToolStripButton();
            this.tsbRefresh = new ToolStripButton();
            this.Panel1.SuspendLayout();
            this.pnlValues.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0xb1);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x278, 0x114);
            this.Grid.TabIndex = 0;
            this.Panel1.Controls.Add(this.cvWarehouse);
            this.Panel1.Controls.Add(this.cvInventoryItem);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0x19);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x278, 0x38);
            this.Panel1.TabIndex = 1;
            this.cvWarehouse.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cvWarehouse.Caption = "Warehouse";
            this.cvWarehouse.Location = new Point(8, 8);
            this.cvWarehouse.Name = "cvWarehouse";
            this.cvWarehouse.Size = new Size(0x268, 0x15);
            this.cvWarehouse.TabIndex = 14;
            this.cvWarehouse.ValueAlign = ContentAlignment.MiddleLeft;
            this.cvInventoryItem.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cvInventoryItem.Caption = "Inventory Item";
            this.cvInventoryItem.Location = new Point(8, 0x20);
            this.cvInventoryItem.Name = "cvInventoryItem";
            this.cvInventoryItem.Size = new Size(0x268, 0x15);
            this.cvInventoryItem.TabIndex = 15;
            this.cvInventoryItem.ValueAlign = ContentAlignment.MiddleLeft;
            this.pnlValues.Controls.Add(this.cvTotalCost);
            this.pnlValues.Controls.Add(this.cvBackOrdered);
            this.pnlValues.Controls.Add(this.cvReorderPoint);
            this.pnlValues.Controls.Add(this.cvOnOrder);
            this.pnlValues.Controls.Add(this.cvOnHand);
            this.pnlValues.Controls.Add(this.cvCommitted);
            this.pnlValues.Controls.Add(this.cvUnavailable);
            this.pnlValues.Controls.Add(this.cvSold);
            this.pnlValues.Controls.Add(this.cvRented);
            this.pnlValues.Controls.Add(this.cvCostPerUnit);
            this.pnlValues.Dock = DockStyle.Top;
            this.pnlValues.Location = new Point(0, 0x51);
            this.pnlValues.Name = "pnlValues";
            this.pnlValues.Size = new Size(0x278, 0x60);
            this.pnlValues.TabIndex = 2;
            this.pnlValues.Visible = false;
            this.cvTotalCost.Caption = "Total Cost";
            this.cvTotalCost.Format = "0.00";
            this.cvTotalCost.Location = new Point(0x138, 0);
            this.cvTotalCost.Name = "cvTotalCost";
            this.cvTotalCost.Size = new Size(0x90, 0x15);
            this.cvTotalCost.TabIndex = 20;
            this.cvBackOrdered.Caption = "Back Ordered";
            this.cvBackOrdered.Location = new Point(160, 0);
            this.cvBackOrdered.Name = "cvBackOrdered";
            this.cvBackOrdered.Size = new Size(0x90, 20);
            this.cvBackOrdered.TabIndex = 0x10;
            this.cvReorderPoint.Caption = "Reorder Point";
            this.cvReorderPoint.Location = new Point(160, 0x30);
            this.cvReorderPoint.Name = "cvReorderPoint";
            this.cvReorderPoint.Size = new Size(0x90, 20);
            this.cvReorderPoint.TabIndex = 0x12;
            this.cvOnOrder.Caption = "On Order";
            this.cvOnOrder.Location = new Point(160, 0x18);
            this.cvOnOrder.Name = "cvOnOrder";
            this.cvOnOrder.Size = new Size(0x90, 20);
            this.cvOnOrder.TabIndex = 0x11;
            this.cvOnHand.Caption = "On Hand";
            this.cvOnHand.Location = new Point(8, 0);
            this.cvOnHand.Name = "cvOnHand";
            this.cvOnHand.Size = new Size(0x90, 20);
            this.cvOnHand.TabIndex = 0x16;
            this.cvCommitted.Caption = "Committed";
            this.cvCommitted.Location = new Point(160, 0x48);
            this.cvCommitted.Name = "cvCommitted";
            this.cvCommitted.Size = new Size(0x90, 20);
            this.cvCommitted.TabIndex = 0x13;
            this.cvUnavailable.Caption = "Unavailable";
            this.cvUnavailable.Location = new Point(8, 0x48);
            this.cvUnavailable.Name = "cvUnavailable";
            this.cvUnavailable.Size = new Size(0x90, 20);
            this.cvUnavailable.TabIndex = 0x19;
            this.cvSold.Caption = "Sold";
            this.cvSold.Location = new Point(8, 0x30);
            this.cvSold.Name = "cvSold";
            this.cvSold.Size = new Size(0x90, 20);
            this.cvSold.TabIndex = 0x18;
            this.cvRented.Caption = "Rented";
            this.cvRented.Location = new Point(8, 0x18);
            this.cvRented.Name = "cvRented";
            this.cvRented.Size = new Size(0x90, 20);
            this.cvRented.TabIndex = 0x17;
            this.cvCostPerUnit.Caption = "Cost Per Unit";
            this.cvCostPerUnit.Format = "0.00";
            this.cvCostPerUnit.Location = new Point(0x138, 0x18);
            this.cvCostPerUnit.Name = "cvCostPerUnit";
            this.cvCostPerUnit.Size = new Size(0x90, 0x15);
            this.cvCostPerUnit.TabIndex = 0x15;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbAdjust, this.tsbTransferTo, this.tsbTransferFrom, this.tsbShowDetails, this.tsbRefresh };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x278, 0x19);
            this.ToolStrip1.TabIndex = 0x13;
            this.tsbAdjust.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbAdjust.ImageTransparentColor = Color.Magenta;
            this.tsbAdjust.Name = "tsbAdjust";
            this.tsbAdjust.Size = new Size(0x2a, 0x16);
            this.tsbAdjust.Text = "Adjust";
            this.tsbTransferTo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbTransferTo.ImageTransparentColor = Color.Magenta;
            this.tsbTransferTo.Name = "tsbTransferTo";
            this.tsbTransferTo.Size = new Size(0x43, 0x16);
            this.tsbTransferTo.Text = "Transfer To";
            this.tsbTransferFrom.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbTransferFrom.ImageTransparentColor = Color.Magenta;
            this.tsbTransferFrom.Name = "tsbTransferFrom";
            this.tsbTransferFrom.Size = new Size(0x4f, 0x16);
            this.tsbTransferFrom.Text = "Transfer From";
            this.tsbShowDetails.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbShowDetails.ImageTransparentColor = Color.Magenta;
            this.tsbShowDetails.Name = "tsbShowDetails";
            this.tsbShowDetails.Size = new Size(0x2b, 0x16);
            this.tsbShowDetails.Text = "Details";
            this.tsbRefresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbRefresh.ImageTransparentColor = Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new Size(0x31, 0x16);
            this.tsbRefresh.Text = "Refresh";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.pnlValues);
            base.Controls.Add(this.Panel1);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormInventoryTransactions";
            this.Text = "Inventory Transactions";
            this.Panel1.ResumeLayout(false);
            this.pnlValues.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("RowID", "Row #", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("ID", "#", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("Date", "Date", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("TypeName", "Type", 80);
            Appearance.AddTextColumn("Quantity", "Qty", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("Description", "Description", 100);
        }

        [HandleDatabaseChanged("tbl_inventory_transaction")]
        private void LoadGrid()
        {
            string[] textArray1 = new string[] { "SELECT\r\n   tbl_inventory_transaction.ID\r\n  ,tbl_inventory_transaction.InventoryItemID\r\n  ,tbl_inventory_transaction.WarehouseID\r\n  ,tbl_inventory_transaction_type.ID   as TypeID\r\n  ,tbl_inventory_transaction_type.Name as TypeName\r\n  ,tbl_inventory_transaction.Date\r\n  ,tbl_inventory_transaction.Quantity\r\n  ,tbl_inventory_transaction.Cost\r\n  ,tbl_inventory_transaction.Description\r\n  ,tbl_inventory_transaction.SerialID\r\n  ,tbl_inventory_transaction.VendorID\r\n  ,tbl_inventory_transaction.CustomerID\r\n  ,tbl_inventory_transaction.LastUpdateUserID\r\n  ,tbl_inventory_transaction.LastUpdateDatetime\r\n  ,tbl_inventory_transaction.PurchaseOrderID\r\n  ,tbl_inventory_transaction.PurchaseOrderDetailsID\r\n  ,tbl_inventory_transaction.InvoiceID\r\n  ,tbl_inventory_transaction.ManufacturerID\r\n  ,tbl_inventory_transaction.OrderDetailsID\r\n  ,tbl_inventory_transaction.OrderID\r\nFROM tbl_inventory_transaction\r\n     LEFT JOIN tbl_inventory_transaction_type ON tbl_inventory_transaction.TypeID = tbl_inventory_transaction_type.ID\r\nWHERE (tbl_inventory_transaction.WarehouseID = ", Conversions.ToString(this.WarehouseID), ")\r\n  AND (tbl_inventory_transaction.InventoryItemID = ", Conversions.ToString(this.InventoryItemID), ")\r\nORDER BY tbl_inventory_transaction.ID DESC" };
            string selectCommandText = string.Concat(textArray1);
            DataTable dataTable = new DataTable("");
            DataColumn column1 = dataTable.Columns.Add("RowID", typeof(int));
            column1.AutoIncrement = true;
            column1.AutoIncrementSeed = 1L;
            column1.AutoIncrementStep = 1L;
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommandText, ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        [HandleDatabaseChanged("tbl_inventoryitem")]
        private void LoadInventoryItem()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT *\r\nFROM tbl_inventoryitem\r\nWHERE (ID = " + Conversions.ToString(this.InventoryItemID) + ")";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        this.cvInventoryItem.Value = !reader.Read() ? "" : ("(" + reader["ID"] + ") " + reader["Name"]);
                    }
                }
            }
        }

        [HandleDatabaseChanged("tbl_inventory_transaction")]
        private void LoadValues()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.WarehouseID;
                    command.Parameters.Add("InventoryItemID", MySqlType.Int).Value = this.InventoryItemID;
                    command.ExecuteProcedure("inventory_refresh");
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.CommandText = "SELECT *\r\nFROM tbl_inventory\r\nWHERE (InventoryItemID = :InventoryItemID)\r\n  AND (WarehouseID     = :WarehouseID)";
                    command2.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.WarehouseID;
                    command2.Parameters.Add("InventoryItemID", MySqlType.Int).Value = this.InventoryItemID;
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.cvOnHand.Value = reader["OnHand"];
                            this.cvRented.Value = reader["Rented"];
                            this.cvSold.Value = reader["Sold"];
                            this.cvUnavailable.Value = reader["UnAvailable"];
                            this.cvCommitted.Value = reader["Committed"];
                            this.cvOnOrder.Value = reader["OnOrder"];
                            this.cvBackOrdered.Value = reader["BackOrdered"];
                            this.cvReorderPoint.Value = reader["ReOrderPoint"];
                            this.cvCostPerUnit.Value = reader["CostPerUnit"];
                            this.cvTotalCost.Value = reader["TotalCost"];
                        }
                    }
                }
            }
        }

        [HandleDatabaseChanged("tbl_warehouse")]
        private void LoadWarehouse()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT *\r\nFROM tbl_warehouse\r\nWHERE (ID = " + Conversions.ToString(this.WarehouseID) + ")";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        this.cvWarehouse.Value = !reader.Read() ? "" : ("(" + reader["ID"] + ") " + reader["Name"]);
                    }
                }
            }
        }

        protected void SetParameters(FormParameters Params)
        {
            try
            {
                if (Params != null)
                {
                    this.InventoryItemID = Conversions.ToInteger(Params["InventoryItemID"]);
                    this.WarehouseID = Conversions.ToInteger(Params["WarehouseID"]);
                    this.LoadInventoryItem();
                    this.LoadWarehouse();
                    this.LoadValues();
                    this.LoadGrid();
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

        private void ShowTransaction(object ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  tbl_inventory_transaction.ID\r\n, tbl_inventory_transaction_type.ID   as TranTypeID\r\n, tbl_inventory_transaction_type.Name as TranTypeName\r\n, tbl_inventoryitem.ID   as InventoryItemID\r\n, tbl_inventoryitem.Name as InventoryItemName\r\n, tbl_warehouse.ID   as WarehouseID\r\n, tbl_warehouse.Name as WarehouseName\r\n, tbl_inventory_transaction.Date\r\n, tbl_inventory_transaction.Quantity\r\n, tbl_inventory_transaction.Cost\r\n, tbl_inventory_transaction.Description\r\n, tbl_serial.ID           as SerialID\r\n, tbl_serial.SerialNumber as SerialNumber\r\n, tbl_vendor.ID   as VendorID\r\n, tbl_vendor.Name as VendorName\r\n, tbl_customer.ID   as CustomerID\r\n, CONCAT(tbl_customer.FirstName, ' ', tbl_customer.LastName) as CustomerName\r\n, tbl_manufacturer.ID   as ManufacturerID\r\n, tbl_manufacturer.Name as ManufacturerName\r\n, tbl_user.ID    as UserID\r\n, tbl_user.Login as UserName\r\n, tbl_inventory_transaction.LastUpdateDatetime\r\n, tbl_inventory_transaction.PurchaseOrderID\r\n, tbl_inventory_transaction.PurchaseOrderDetailsID\r\n, tbl_inventory_transaction.InvoiceID\r\n, tbl_inventory_transaction.OrderDetailsID\r\n, tbl_inventory_transaction.OrderID\r\nFROM tbl_inventory_transaction\r\n     LEFT JOIN tbl_inventory_transaction_type ON tbl_inventory_transaction.TypeID           = tbl_inventory_transaction_type.ID\r\n     LEFT JOIN tbl_inventoryitem              ON tbl_inventory_transaction.InventoryItemID  = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_warehouse                  ON tbl_inventory_transaction.WarehouseID      = tbl_warehouse.ID\r\n     LEFT JOIN tbl_serial                     ON tbl_inventory_transaction.SerialID         = tbl_serial.ID\r\n     LEFT JOIN tbl_vendor                     ON tbl_inventory_transaction.VendorID         = tbl_vendor.ID\r\n     LEFT JOIN tbl_customer                   ON tbl_inventory_transaction.CustomerID       = tbl_customer.ID\r\n     LEFT JOIN tbl_manufacturer               ON tbl_inventory_transaction.ManufacturerID   = tbl_manufacturer.ID\r\n     LEFT JOIN tbl_user                       ON tbl_inventory_transaction.LastUpdateUserID = tbl_user.ID\r\nWHERE (tbl_inventory_transaction.ID = :ID)";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            using (StringWriter writer = new StringWriter())
                            {
                                object[] args = new object[] { reader["ID"] };
                                WriteLine(writer, "Transaction #{0}", args);
                                object[] objArray2 = new object[] { reader["InventoryItemID"], reader["InventoryItemName"] };
                                WriteLine(writer, "Inventory Item : ({0}) {1}", objArray2);
                                object[] objArray3 = new object[] { reader["WarehouseID"], reader["WarehouseName"] };
                                WriteLine(writer, "Warehouse : ({0}) {1}", objArray3);
                                object[] objArray4 = new object[] { reader["TranTypeID"], reader["TranTypeName"] };
                                WriteLine(writer, "Type : ({0}) {1}", objArray4);
                                object[] objArray5 = new object[] { reader["Date"] };
                                WriteLine(writer, "Date : {0}", objArray5);
                                object[] objArray6 = new object[] { reader["Quantity"] };
                                WriteLine(writer, "Quantity : {0}", objArray6);
                                object[] objArray7 = new object[] { reader["Cost"] };
                                WriteLine(writer, "Cost : {0}", objArray7);
                                object[] objArray8 = new object[] { reader["Description"] };
                                WriteLine(writer, "Description : {0}", objArray8);
                                object[] objArray9 = new object[] { reader["SerialID"], reader["SerialNumber"] };
                                WriteLine(writer, "Serial : ({0}) {1}", objArray9);
                                object[] objArray10 = new object[] { reader["VendorID"], reader["VendorName"] };
                                WriteLine(writer, "Vendor : ({0}) {1}", objArray10);
                                object[] objArray11 = new object[] { reader["CustomerID"], reader["CustomerName"] };
                                WriteLine(writer, "Customer : ({0}) {1}", objArray11);
                                object[] objArray12 = new object[] { reader["ManufacturerID"], reader["ManufacturerName"] };
                                WriteLine(writer, "Manufacturer : ({0}) {1}", objArray12);
                                object[] objArray13 = new object[] { reader["UserID"], reader["UserName"] };
                                WriteLine(writer, "User : ({0}) {1}", objArray13);
                                object[] objArray14 = new object[] { reader["LastUpdateDatetime"] };
                                WriteLine(writer, "Updated : {0}", objArray14);
                                object[] objArray15 = new object[] { reader["PurchaseOrderID"] };
                                WriteLine(writer, "Purchase Order : #{0}", objArray15);
                                object[] objArray16 = new object[] { reader["PurchaseOrderDetailsID"] };
                                WriteLine(writer, "Purchase Order Line : #{0}", objArray16);
                                object[] objArray17 = new object[] { reader["SerialID"] };
                                WriteLine(writer, "Order : #{0}", objArray17);
                                object[] objArray18 = new object[] { reader["SerialID"] };
                                WriteLine(writer, "Order Line : #{0}", objArray18);
                                object[] objArray19 = new object[] { reader["InvoiceID"] };
                                WriteLine(writer, "Invoice : #{0}", objArray19);
                                MessageBox.Show(writer.ToString(), "View Transaction", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                        }
                    }
                }
            }
        }

        private void tsbAdjust_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["WarehouseID"] = this.WarehouseID,
                ["InventoryItemID"] = this.InventoryItemID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormInventoryAdjustment(), @params);
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void tsbShowDetails_Click(object sender, EventArgs e)
        {
            this.pnlValues.Visible = !this.pnlValues.Visible;
            this.tsbShowDetails.Checked = this.pnlValues.Visible;
        }

        private void tsbTransferFrom_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["SrcWarehouseID"] = this.WarehouseID,
                ["InventoryItemID"] = this.InventoryItemID
            };
            ClassGlobalObjects.ShowForm(FormFactories.WizardInventoryTransfer(), @params, true);
        }

        private void tsbTransferTo_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["DstWarehouseID"] = this.WarehouseID,
                ["InventoryItemID"] = this.InventoryItemID
            };
            ClassGlobalObjects.ShowForm(FormFactories.WizardInventoryTransfer(), @params, true);
        }

        private static void WriteLine(TextWriter Writer, string Format, params object[] args)
        {
            if ((args != null) && (args.Length != 0))
            {
                object[] objArray = new object[(args.Length - 1) + 1];
                int num = objArray.Length - 1;
                int index = 0;
                while (true)
                {
                    if (index > num)
                    {
                        string str = string.Format(Format, objArray);
                        string str2 = string.Format(Format, args);
                        if (str2.Length != str.Length)
                        {
                            Writer.WriteLine(str2);
                        }
                        break;
                    }
                    objArray[index] = DBNull.Value;
                    index++;
                }
            }
        }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlValues")]
        private Panel pnlValues { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvWarehouse")]
        private ControlValue cvWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvInventoryItem")]
        private ControlValue cvInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvTotalCost")]
        private ControlValue cvTotalCost { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvBackOrdered")]
        private ControlValue cvBackOrdered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvReorderPoint")]
        private ControlValue cvReorderPoint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvOnOrder")]
        private ControlValue cvOnOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvOnHand")]
        private ControlValue cvOnHand { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvCommitted")]
        private ControlValue cvCommitted { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvUnavailable")]
        private ControlValue cvUnavailable { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvSold")]
        private ControlValue cvSold { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvRented")]
        private ControlValue cvRented { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvCostPerUnit")]
        private ControlValue cvCostPerUnit { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("tsbShowDetails")]
        private ToolStripButton tsbShowDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbRefresh")]
        private ToolStripButton tsbRefresh { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public int InventoryItemID
        {
            get => 
                this.F_InventoryItemID;
            set => 
                this.F_InventoryItemID = value;
        }

        public int WarehouseID
        {
            get => 
                this.F_WarehouseID;
            set => 
                this.F_WarehouseID = value;
        }
    }
}

