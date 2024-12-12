namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormInventoryAdjustment : DmeForm, IParameters
    {
        private IContainer components;
        private int F_InventoryItemID;
        private int F_WarehouseID;

        public FormInventoryAdjustment()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.WarehouseID;
                        command.Parameters.Add("InventoryItemID", MySqlType.Int).Value = this.InventoryItemID;
                        command.Parameters.Add("OnHand", MySqlType.Int).Value = NullableConvert.ToDb(this.caOnHand.ModifiedValue);
                        command.Parameters.Add("Rented", MySqlType.Int).Value = NullableConvert.ToDb(this.caRented.ModifiedValue);
                        command.Parameters.Add("Sold", MySqlType.Int).Value = NullableConvert.ToDb(this.caSold.ModifiedValue);
                        command.Parameters.Add("Unavailable", MySqlType.Int).Value = NullableConvert.ToDb(this.caUnavailable.ModifiedValue);
                        command.Parameters.Add("Committed", MySqlType.Int).Value = NullableConvert.ToDb(this.caCommitted.ModifiedValue);
                        command.Parameters.Add("OnOrder", MySqlType.Int).Value = NullableConvert.ToDb(this.caOnOrder.ModifiedValue);
                        command.Parameters.Add("BackOrdered", MySqlType.Int).Value = NullableConvert.ToDb(this.caBackOrdered.ModifiedValue);
                        command.Parameters.Add("ReOrderPoint", MySqlType.Int).Value = NullableConvert.ToDb(this.caReorderPoint.ModifiedValue);
                        command.Parameters.Add("CostPerUnit", MySqlType.Double).Value = NullableConvert.ToDb(this.caCostPerUnit.ModifiedValue);
                        command.Parameters.Add("LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                        command.ExecuteProcedure("inventory_adjust_2");
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
                return;
            }
            base.Close();
            string[] tableNames = new string[] { "tbl_inventory_transaction" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private void ClearData()
        {
            int? nullable = null;
            this.caOnHand.OriginalValue = nullable;
            nullable = null;
            this.caOnHand.ModifiedValue = nullable;
            nullable = null;
            this.caRented.OriginalValue = nullable;
            nullable = null;
            this.caRented.ModifiedValue = nullable;
            nullable = null;
            this.caSold.OriginalValue = nullable;
            nullable = null;
            this.caSold.ModifiedValue = nullable;
            nullable = null;
            this.caUnavailable.OriginalValue = nullable;
            nullable = null;
            this.caUnavailable.ModifiedValue = nullable;
            nullable = null;
            this.caCommitted.OriginalValue = nullable;
            nullable = null;
            this.caCommitted.ModifiedValue = nullable;
            nullable = null;
            this.caOnOrder.OriginalValue = nullable;
            nullable = null;
            this.caOnOrder.ModifiedValue = nullable;
            nullable = null;
            this.caBackOrdered.OriginalValue = nullable;
            nullable = null;
            this.caBackOrdered.ModifiedValue = nullable;
            nullable = null;
            this.caReorderPoint.OriginalValue = nullable;
            nullable = null;
            this.caReorderPoint.ModifiedValue = nullable;
            double? nullable2 = null;
            this.caCostPerUnit.OriginalValue = nullable2;
            nullable2 = null;
            this.caCostPerUnit.ModifiedValue = nullable2;
            nullable2 = null;
            this.caTotalCost.OriginalValue = nullable2;
            nullable2 = null;
            this.caTotalCost.ModifiedValue = nullable2;
            this.cvInventoryItem.Value = null;
            this.cvWarehouse.Value = null;
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

        private static string FormatPrice(object value)
        {
            try
            {
                if (value != null)
                {
                    return Convert.ToDouble(value).ToString("0.00");
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                ProjectData.ClearProjectError();
            }
            return "";
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.caCostPerUnit = new ControlPriceAdjustment();
            this.cvInventoryItem = new ControlValue();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.cvWarehouse = new ControlValue();
            this.caTotalCost = new ControlPriceAdjustment();
            this.caBackOrdered = new ControlQuantityAdjustment();
            this.caReorderPoint = new ControlQuantityAdjustment();
            this.caOnOrder = new ControlQuantityAdjustment();
            this.caOnHand = new ControlQuantityAdjustment();
            this.caCommitted = new ControlQuantityAdjustment();
            this.caUnavailable = new ControlQuantityAdjustment();
            this.caSold = new ControlQuantityAdjustment();
            this.caRented = new ControlQuantityAdjustment();
            base.SuspendLayout();
            this.caCostPerUnit.Caption = "Cost Per Unit";
            this.caCostPerUnit.Location = new Point(8, 0xb8);
            this.caCostPerUnit.Name = "caCostPerUnit";
            this.caCostPerUnit.Size = new Size(200, 0x15);
            this.caCostPerUnit.TabIndex = 7;
            this.cvInventoryItem.Caption = "Inventory Item";
            this.cvInventoryItem.Location = new Point(8, 0x20);
            this.cvInventoryItem.Name = "cvInventoryItem";
            this.cvInventoryItem.Size = new Size(0x198, 0x15);
            this.cvInventoryItem.TabIndex = 1;
            this.cvInventoryItem.ValueAlign = ContentAlignment.MiddleLeft;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x158, 0xb8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnOK.Location = new Point(0x108, 0xb8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.cvWarehouse.Caption = "Warehouse";
            this.cvWarehouse.Location = new Point(8, 8);
            this.cvWarehouse.Name = "cvWarehouse";
            this.cvWarehouse.Size = new Size(0x198, 0x15);
            this.cvWarehouse.TabIndex = 0;
            this.cvWarehouse.ValueAlign = ContentAlignment.MiddleLeft;
            this.caTotalCost.Caption = "Total Cost";
            this.caTotalCost.EditorVisible = false;
            this.caTotalCost.Location = new Point(8, 160);
            this.caTotalCost.Name = "caTotalCost";
            this.caTotalCost.Size = new Size(200, 0x15);
            this.caTotalCost.TabIndex = 6;
            this.caBackOrdered.Caption = "Back Ordered";
            this.caBackOrdered.Location = new Point(8, 0x38);
            this.caBackOrdered.Name = "caBackOrdered";
            this.caBackOrdered.Size = new Size(200, 20);
            this.caBackOrdered.TabIndex = 2;
            this.caReorderPoint.Caption = "Reorder Point";
            this.caReorderPoint.Location = new Point(8, 0x68);
            this.caReorderPoint.Name = "caReorderPoint";
            this.caReorderPoint.Size = new Size(200, 20);
            this.caReorderPoint.TabIndex = 4;
            this.caOnOrder.Caption = "On Order";
            this.caOnOrder.Location = new Point(8, 80);
            this.caOnOrder.Name = "caOnOrder";
            this.caOnOrder.Size = new Size(200, 20);
            this.caOnOrder.TabIndex = 3;
            this.caOnHand.Caption = "On Hand";
            this.caOnHand.Location = new Point(0xd8, 0x38);
            this.caOnHand.Name = "caOnHand";
            this.caOnHand.Size = new Size(200, 20);
            this.caOnHand.TabIndex = 8;
            this.caCommitted.Caption = "Committed";
            this.caCommitted.Location = new Point(8, 0x80);
            this.caCommitted.Name = "caCommitted";
            this.caCommitted.Size = new Size(200, 20);
            this.caCommitted.TabIndex = 5;
            this.caUnavailable.Caption = "Unavailable";
            this.caUnavailable.Location = new Point(0xd8, 0x80);
            this.caUnavailable.Name = "caUnavailable";
            this.caUnavailable.Size = new Size(200, 20);
            this.caUnavailable.TabIndex = 11;
            this.caSold.Caption = "Sold";
            this.caSold.Location = new Point(0xd8, 0x68);
            this.caSold.Name = "caSold";
            this.caSold.Size = new Size(200, 20);
            this.caSold.TabIndex = 10;
            this.caRented.Caption = "Rented";
            this.caRented.Location = new Point(0xd8, 80);
            this.caRented.Name = "caRented";
            this.caRented.Size = new Size(200, 20);
            this.caRented.TabIndex = 9;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x1a6, 0xd5);
            base.Controls.Add(this.caTotalCost);
            base.Controls.Add(this.caBackOrdered);
            base.Controls.Add(this.caReorderPoint);
            base.Controls.Add(this.caOnOrder);
            base.Controls.Add(this.caOnHand);
            base.Controls.Add(this.caCommitted);
            base.Controls.Add(this.caUnavailable);
            base.Controls.Add(this.caSold);
            base.Controls.Add(this.caRented);
            base.Controls.Add(this.caCostPerUnit);
            base.Controls.Add(this.cvInventoryItem);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cvWarehouse);
            base.Name = "FormInventoryAdjustment";
            this.Text = "Inventory Adjustment";
            base.ResumeLayout(false);
        }

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT Name FROM tbl_inventoryitem WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = this.InventoryItemID;
                    this.cvInventoryItem.Value = Convert.ToString(command.ExecuteScalar());
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.CommandText = "SELECT Name FROM tbl_warehouse WHERE ID = :ID";
                    command2.Parameters.Add("ID", MySqlType.Int).Value = this.WarehouseID;
                    this.cvWarehouse.Value = Convert.ToString(command2.ExecuteScalar());
                }
                using (MySqlCommand command3 = new MySqlCommand("", connection))
                {
                    command3.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.WarehouseID;
                    command3.Parameters.Add("InventoryItemID", MySqlType.Int).Value = this.InventoryItemID;
                    command3.ExecuteProcedure("inventory_refresh");
                }
                using (MySqlCommand command4 = new MySqlCommand("", connection))
                {
                    command4.CommandText = "SELECT *\r\nFROM tbl_inventory\r\nWHERE (InventoryItemID = :InventoryItemID)\r\n  AND (WarehouseID     = :WarehouseID)";
                    command4.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.WarehouseID;
                    command4.Parameters.Add("InventoryItemID", MySqlType.Int).Value = this.InventoryItemID;
                    using (MySqlDataReader reader = command4.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.caOnHand.OriginalValue = NullableConvert.ToInt32(reader["OnHand"]);
                            this.caRented.OriginalValue = NullableConvert.ToInt32(reader["Rented"]);
                            this.caSold.OriginalValue = NullableConvert.ToInt32(reader["Sold"]);
                            this.caUnavailable.OriginalValue = NullableConvert.ToInt32(reader["UnAvailable"]);
                            this.caCommitted.OriginalValue = NullableConvert.ToInt32(reader["Committed"]);
                            this.caOnOrder.OriginalValue = NullableConvert.ToInt32(reader["OnOrder"]);
                            this.caBackOrdered.OriginalValue = NullableConvert.ToInt32(reader["BackOrdered"]);
                            this.caReorderPoint.OriginalValue = NullableConvert.ToInt32(reader["ReOrderPoint"]);
                            this.caCostPerUnit.OriginalValue = NullableConvert.ToDouble(reader["CostPerUnit"]);
                            this.caTotalCost.OriginalValue = NullableConvert.ToDouble(reader["TotalCost"]);
                        }
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
                    this.LoadData();
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

        [field: AccessedThroughProperty("cvInventoryItem")]
        private ControlValue cvInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cvWarehouse")]
        private ControlValue cvWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caOnHand")]
        private ControlQuantityAdjustment caOnHand { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caRented")]
        private ControlQuantityAdjustment caRented { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caSold")]
        private ControlQuantityAdjustment caSold { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caUnavailable")]
        private ControlQuantityAdjustment caUnavailable { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caCommitted")]
        private ControlQuantityAdjustment caCommitted { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caBackOrdered")]
        private ControlQuantityAdjustment caBackOrdered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caReorderPoint")]
        private ControlQuantityAdjustment caReorderPoint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caOnOrder")]
        private ControlQuantityAdjustment caOnOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caCostPerUnit")]
        private ControlPriceAdjustment caCostPerUnit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caTotalCost")]
        private ControlPriceAdjustment caTotalCost { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

