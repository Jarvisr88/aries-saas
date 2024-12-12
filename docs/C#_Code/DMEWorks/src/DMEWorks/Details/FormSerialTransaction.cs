namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormSerialTransaction : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private int? F_TransactionId;
        private int? F_SerialId;

        public FormSerialTransaction()
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
                this.SaveObject();
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
        }

        private void ClearObject()
        {
            this.F_SerialId = null;
            Functions.SetControlText(this.txtInventoryItem, DBNull.Value);
            Functions.SetControlText(this.txtManufacturer, DBNull.Value);
            Functions.SetControlText(this.txtModelNumber, DBNull.Value);
            Functions.SetControlText(this.txtSerialNumber, DBNull.Value);
            Functions.SetControlText(this.txtStatus, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbCustomer, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbTransaction, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbVendor, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbWarehouse, DBNull.Value);
            Functions.SetControlText(this.txtLotNumber, DBNull.Value);
        }

        private void cmbTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object selectedValue = this.cmbTransaction.SelectedValue;
                if (!(selectedValue is TransactionType))
                {
                    this.cmbCustomer.Enabled = false;
                    this.txtLotNumber.Enabled = false;
                    this.cmbWarehouse.Enabled = false;
                    this.cmbVendor.Enabled = false;
                    this.dtbDate.Enabled = true;
                    this.dtbTime.Enabled = true;
                }
                else
                {
                    TransactionType type1;
                    if (selectedValue != null)
                    {
                        type1 = (TransactionType) selectedValue;
                    }
                    else
                    {
                        object local1 = selectedValue;
                        type1 = new TransactionType();
                    }
                    TransactionType type = type1;
                    this.cmbCustomer.Enabled = type.EnableCustomer && (this.F_TransactionId == null);
                    this.txtLotNumber.Enabled = type.EnableLotNumber;
                    this.cmbWarehouse.Enabled = type.EnableWarehouse;
                    this.cmbVendor.Enabled = type.EnableVendor;
                    this.dtbDate.Enabled = type.EnableDateTime;
                    this.dtbTime.Enabled = type.EnableDateTime;
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.cmbWarehouse = new Combobox();
            this.lblTransaction = new Label();
            this.cmbTransaction = new ComboBox();
            this.cmbVendor = new Combobox();
            this.cmbCustomer = new Combobox();
            this.txtLotNumber = new TextBox();
            this.lblLotNumber = new Label();
            this.lblCustomer = new Label();
            this.lblVendor = new Label();
            this.lblWarehouse = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.txtManufacturer = new Label();
            this.txtStatus = new Label();
            this.txtModelNumber = new Label();
            this.txtInventoryItem = new Label();
            this.lblManufacturer = new Label();
            this.lblStatus = new Label();
            this.lblModelNumber = new Label();
            this.txtSerialNumber = new Label();
            this.lblSerialNumber = new Label();
            this.lblInventoryItem = new Label();
            this.lblDateTime = new Label();
            this.dtbDate = new UltraDateTimeEditor();
            this.dtbTime = new UltraDateTimeEditor();
            base.SuspendLayout();
            this.cmbWarehouse.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbWarehouse.EditButton = false;
            this.cmbWarehouse.Location = new Point(0x70, 0xc0);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.NewButton = false;
            this.cmbWarehouse.Size = new Size(0x110, 0x15);
            this.cmbWarehouse.TabIndex = 0x10;
            this.lblTransaction.BorderStyle = BorderStyle.FixedSingle;
            this.lblTransaction.Location = new Point(8, 0x88);
            this.lblTransaction.Name = "lblTransaction";
            this.lblTransaction.Size = new Size(0x60, 0x15);
            this.lblTransaction.TabIndex = 10;
            this.lblTransaction.Text = "Transaction:";
            this.lblTransaction.TextAlign = ContentAlignment.MiddleRight;
            this.cmbTransaction.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbTransaction.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTransaction.Location = new Point(0x70, 0x88);
            this.cmbTransaction.Name = "cmbTransaction";
            this.cmbTransaction.Size = new Size(0x110, 0x15);
            this.cmbTransaction.TabIndex = 11;
            this.cmbVendor.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbVendor.EditButton = false;
            this.cmbVendor.Location = new Point(0x70, 0xd8);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.NewButton = false;
            this.cmbVendor.Size = new Size(0x110, 0x15);
            this.cmbVendor.TabIndex = 0x12;
            this.cmbCustomer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbCustomer.EditButton = false;
            this.cmbCustomer.Location = new Point(0x70, 240);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.NewButton = false;
            this.cmbCustomer.Size = new Size(0x110, 0x15);
            this.cmbCustomer.TabIndex = 20;
            this.txtLotNumber.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtLotNumber.Location = new Point(0x70, 0x108);
            this.txtLotNumber.Name = "txtLotNumber";
            this.txtLotNumber.Size = new Size(0x110, 20);
            this.txtLotNumber.TabIndex = 0x16;
            this.lblLotNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblLotNumber.Location = new Point(8, 0x108);
            this.lblLotNumber.Name = "lblLotNumber";
            this.lblLotNumber.Size = new Size(0x60, 0x15);
            this.lblLotNumber.TabIndex = 0x15;
            this.lblLotNumber.Text = "Lot Number";
            this.lblLotNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblCustomer.BorderStyle = BorderStyle.FixedSingle;
            this.lblCustomer.Location = new Point(8, 240);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(0x60, 0x15);
            this.lblCustomer.TabIndex = 0x13;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.lblVendor.BorderStyle = BorderStyle.FixedSingle;
            this.lblVendor.Location = new Point(8, 0xd8);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new Size(0x60, 0x15);
            this.lblVendor.TabIndex = 0x11;
            this.lblVendor.Text = "Vendor:";
            this.lblVendor.TextAlign = ContentAlignment.MiddleRight;
            this.lblWarehouse.BorderStyle = BorderStyle.FixedSingle;
            this.lblWarehouse.Location = new Point(8, 0xc0);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x60, 0x15);
            this.lblWarehouse.TabIndex = 15;
            this.lblWarehouse.Text = "Warehouse:";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnOK.Location = new Point(0xe8, 0x128);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCancel.Location = new Point(0x138, 0x128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0x18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.txtManufacturer.BorderStyle = BorderStyle.Fixed3D;
            this.txtManufacturer.Location = new Point(0x70, 0x68);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new Size(0x110, 0x15);
            this.txtManufacturer.TabIndex = 9;
            this.txtManufacturer.Text = "INVACARE";
            this.txtStatus.BorderStyle = BorderStyle.Fixed3D;
            this.txtStatus.Location = new Point(0x70, 80);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new Size(0x110, 0x15);
            this.txtStatus.TabIndex = 7;
            this.txtStatus.Text = "Rented";
            this.txtModelNumber.BorderStyle = BorderStyle.Fixed3D;
            this.txtModelNumber.Location = new Point(0x70, 0x38);
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.Size = new Size(0x110, 0x15);
            this.txtModelNumber.TabIndex = 5;
            this.txtModelNumber.Text = "Technician:";
            this.txtInventoryItem.BorderStyle = BorderStyle.Fixed3D;
            this.txtInventoryItem.Location = new Point(0x70, 0x20);
            this.txtInventoryItem.Name = "txtInventoryItem";
            this.txtInventoryItem.Size = new Size(0x110, 0x15);
            this.txtInventoryItem.TabIndex = 3;
            this.txtInventoryItem.Text = "LIFT,HOYER,HYDRAULIC,INVACARE";
            this.lblManufacturer.BorderStyle = BorderStyle.FixedSingle;
            this.lblManufacturer.Location = new Point(8, 0x68);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new Size(0x60, 0x15);
            this.lblManufacturer.TabIndex = 8;
            this.lblManufacturer.Text = "Manufacturer:";
            this.lblManufacturer.TextAlign = ContentAlignment.MiddleRight;
            this.lblStatus.BorderStyle = BorderStyle.FixedSingle;
            this.lblStatus.Location = new Point(8, 80);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x60, 0x15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";
            this.lblStatus.TextAlign = ContentAlignment.MiddleRight;
            this.lblModelNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblModelNumber.Location = new Point(8, 0x38);
            this.lblModelNumber.Name = "lblModelNumber";
            this.lblModelNumber.Size = new Size(0x60, 0x15);
            this.lblModelNumber.TabIndex = 4;
            this.lblModelNumber.Text = "Model Number:";
            this.lblModelNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtSerialNumber.BorderStyle = BorderStyle.Fixed3D;
            this.txtSerialNumber.Location = new Point(0x70, 8);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new Size(0x110, 0x15);
            this.txtSerialNumber.TabIndex = 1;
            this.txtSerialNumber.Text = "06KLA100766";
            this.lblSerialNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblSerialNumber.Location = new Point(8, 8);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new Size(0x60, 0x15);
            this.lblSerialNumber.TabIndex = 0;
            this.lblSerialNumber.Text = "Serial Number:";
            this.lblSerialNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblInventoryItem.BorderStyle = BorderStyle.FixedSingle;
            this.lblInventoryItem.Location = new Point(8, 0x20);
            this.lblInventoryItem.Name = "lblInventoryItem";
            this.lblInventoryItem.Size = new Size(0x60, 0x15);
            this.lblInventoryItem.TabIndex = 2;
            this.lblInventoryItem.Text = "Inventory Item:";
            this.lblInventoryItem.TextAlign = ContentAlignment.MiddleRight;
            this.lblDateTime.BorderStyle = BorderStyle.FixedSingle;
            this.lblDateTime.Location = new Point(8, 160);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new Size(0x60, 0x15);
            this.lblDateTime.TabIndex = 12;
            this.lblDateTime.Text = "Date && Time:";
            this.lblDateTime.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDate.Location = new Point(0x70, 160);
            this.dtbDate.Name = "dtbDate";
            this.dtbDate.Size = new Size(0x60, 0x15);
            this.dtbDate.TabIndex = 13;
            this.dtbTime.DropDownButtonDisplayStyle = ButtonDisplayStyle.Never;
            this.dtbTime.Location = new Point(0xd8, 160);
            this.dtbTime.MaskInput = "hh:mm";
            this.dtbTime.Name = "dtbTime";
            this.dtbTime.Size = new Size(0x60, 0x15);
            this.dtbTime.SpinButtonDisplayStyle = ButtonDisplayStyle.Always;
            this.dtbTime.TabIndex = 14;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x188, 0x145);
            base.Controls.Add(this.dtbTime);
            base.Controls.Add(this.dtbDate);
            base.Controls.Add(this.lblDateTime);
            base.Controls.Add(this.lblInventoryItem);
            base.Controls.Add(this.txtManufacturer);
            base.Controls.Add(this.txtStatus);
            base.Controls.Add(this.txtModelNumber);
            base.Controls.Add(this.txtInventoryItem);
            base.Controls.Add(this.lblManufacturer);
            base.Controls.Add(this.lblStatus);
            base.Controls.Add(this.lblModelNumber);
            base.Controls.Add(this.txtSerialNumber);
            base.Controls.Add(this.lblSerialNumber);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lblWarehouse);
            base.Controls.Add(this.lblVendor);
            base.Controls.Add(this.lblCustomer);
            base.Controls.Add(this.lblLotNumber);
            base.Controls.Add(this.txtLotNumber);
            base.Controls.Add(this.cmbCustomer);
            base.Controls.Add(this.cmbVendor);
            base.Controls.Add(this.cmbTransaction);
            base.Controls.Add(this.lblTransaction);
            base.Controls.Add(this.cmbWarehouse);
            base.Name = "FormSerialTransaction";
            this.Text = "Add Serial Transaction";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public static FormSerialTransaction Load(int TransactionID)
        {
            FormSerialTransaction transaction1 = new FormSerialTransaction();
            transaction1.Text = $"Editing Serial Transaction #{TransactionID}";
            transaction1.LoadExisting(TransactionID);
            return transaction1;
        }

        private void LoadExisting(int TransactionId)
        {
            this.ClearObject();
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbVendor, "tbl_vendor", null);
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  st.OrderID\r\n, st.OrderDetailsID\r\n, st.CustomerID\r\n, st.VendorID\r\n, st.WarehouseID\r\n, st.LotNumber\r\n, st.SerialID\r\n, st.TransactionDatetime\r\n, stt.Name as TranType\r\n, s.SerialNumber\r\n, s.ModelNumber\r\n, s.Status\r\n, ii.Name as InventoryItemName\r\n, m.Name as ManufacturerName\r\nFROM tbl_serial_transaction as st\r\n     INNER JOIN tbl_serial_transaction_type as stt ON stt.ID = st.TypeID\r\n     INNER JOIN tbl_serial as s ON s.ID = st.SerialID\r\n     LEFT JOIN tbl_inventoryitem as ii ON s.InventoryItemID = ii.ID\r\n     LEFT JOIN tbl_manufacturer as m ON s.ManufacturerID = m.ID\r\nWHERE st.ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = TransactionId;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        TransactionType? nullable2;
                        if (!reader.Read())
                        {
                            throw new UserNotifyException("Serial is not found");
                        }
                        this.F_TransactionId = new int?(TransactionId);
                        this.F_SerialId = NullableConvert.ToInt32(reader["SerialID"]);
                        Functions.SetControlText(this.txtInventoryItem, reader["InventoryItemName"]);
                        Functions.SetControlText(this.txtManufacturer, reader["ManufacturerName"]);
                        Functions.SetControlText(this.txtModelNumber, reader["ModelNumber"]);
                        Functions.SetControlText(this.txtSerialNumber, reader["SerialNumber"]);
                        Functions.SetControlText(this.txtStatus, reader["Status"]);
                        bool local1 = (NullableConvert.ToInt32(reader["OrderID"]) != null) || (NullableConvert.ToInt32(reader["OrderDetailsID"]) != null);
                        string str = Convert.ToString(reader["TranType"]);
                        TransactionType other = ((nullable2 = TransactionType.GetTypeByName(str)) != null) ? nullable2.GetValueOrDefault() : new TransactionType(str, false, true, false, false, false);
                        if ((NullableConvert.ToInt32(reader["OrderID"]) != null) || (NullableConvert.ToInt32(reader["OrderDetailsID"]) != null))
                        {
                            bool? lotNumber = null;
                            lotNumber = null;
                            other = new TransactionType(other, false, false, lotNumber, lotNumber, false);
                        }
                        TransactionType[] typeArray1 = new TransactionType[] { other };
                        this.cmbTransaction.DataSource = typeArray1;
                        Functions.SetComboBoxValue(this.cmbCustomer, reader["CustomerID"]);
                        Functions.SetComboBoxValue(this.cmbWarehouse, reader["WarehouseID"]);
                        Functions.SetComboBoxValue(this.cmbVendor, reader["VendorID"]);
                        Functions.SetTextBoxText(this.txtLotNumber, reader["LotNumber"]);
                        Functions.SetDateBoxValue(this.dtbDate, reader["TransactionDatetime"]);
                        Functions.SetDateBoxValue(this.dtbTime, reader["TransactionDatetime"]);
                    }
                }
            }
            this.cmbTransaction.Enabled = false;
        }

        private void LoadNew(int SerialId)
        {
            this.ClearObject();
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbVendor, "tbl_vendor", null);
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "CALL serial_refresh(:P_SerialID)";
                    command.Parameters.Add("P_SerialID", MySqlType.Int).Value = SerialId;
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.CommandText = "SELECT\r\n  tbl_serial.Status\r\n, tbl_serial.ID as SerialID\r\n, tbl_serial.SerialNumber\r\n, tbl_serial.ModelNumber\r\n, tbl_inventoryitem.Name as InventoryItemName\r\n, tbl_manufacturer.Name as ManufacturerName\r\nFROM tbl_serial\r\n     LEFT JOIN tbl_inventoryitem ON tbl_serial.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_manufacturer ON tbl_serial.ManufacturerID = tbl_manufacturer.ID\r\nWHERE tbl_serial.ID = :ID";
                    command2.Parameters.Add("ID", MySqlType.Int).Value = SerialId;
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new UserNotifyException("Serial is not found");
                        }
                        this.F_SerialId = NullableConvert.ToInt32(reader["SerialID"]);
                        Functions.SetControlText(this.txtInventoryItem, reader["InventoryItemName"]);
                        Functions.SetControlText(this.txtManufacturer, reader["ManufacturerName"]);
                        Functions.SetControlText(this.txtModelNumber, reader["ModelNumber"]);
                        Functions.SetControlText(this.txtSerialNumber, reader["SerialNumber"]);
                        Functions.SetControlText(this.txtStatus, reader["Status"]);
                        this.cmbTransaction.DataSource = TransactionType.GetListByStatus(Convert.ToString(reader["Status"]));
                        Functions.SetDateBoxValue(this.dtbDate, DBNull.Value);
                        Functions.SetDateBoxValue(this.dtbTime, DBNull.Value);
                    }
                }
            }
        }

        public static FormSerialTransaction New(int SerialID)
        {
            FormSerialTransaction transaction1 = new FormSerialTransaction();
            transaction1.Text = "Add Serial Transaction";
            transaction1.LoadNew(SerialID);
            return transaction1;
        }

        private void SaveObject()
        {
            TransactionType type1;
            object selectedValue = this.cmbTransaction.SelectedValue;
            if (!(selectedValue is TransactionType))
            {
                throw new UserNotifyException("Select Transaction");
            }
            object local1 = selectedValue;
            if (local1 != null)
            {
                type1 = (TransactionType) local1;
            }
            else
            {
                object local2 = local1;
                type1 = new TransactionType();
            }
            TransactionType type = type1;
            if (string.IsNullOrEmpty(type.Name))
            {
                throw new UserNotifyException("Select Transaction");
            }
            if (type.EnableCustomer && !Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
            {
                throw new UserNotifyException("You Must Select Customer");
            }
            if (type.EnableLotNumber && (this.txtLotNumber.Text.Trim().Length == 0))
            {
                throw new UserNotifyException("You Must Enter Lot Number");
            }
            if (type.EnableVendor && !Versioned.IsNumeric(this.cmbVendor.SelectedValue))
            {
                throw new UserNotifyException("You Must Select Vendor");
            }
            if (type.EnableWarehouse && !Versioned.IsNumeric(this.cmbWarehouse.SelectedValue))
            {
                throw new UserNotifyException("You Must Select Warehouse");
            }
            object obj2 = !(this.dtbDate.Value is DateTime) ? ((object) DBNull.Value) : (!(this.dtbTime.Value is DateTime) ? ((object) Conversions.ToDate(this.dtbDate.Value).Date) : ((object) (Conversions.ToDate(this.dtbDate.Value).Date + Conversions.ToDate(this.dtbTime.Value).TimeOfDay)));
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    if (this.F_TransactionId != null)
                    {
                        command.CommandText = "CALL serial_update_transaction(\r\n  :P_TransactionID    -- INT\r\n, :P_TranTime         -- DATETIME\r\n, :P_WarehouseID      -- INT\r\n, :P_VendorID         -- INT\r\n, :P_CustomerID       -- INT\r\n, :P_LotNumber        -- VARCHAR(50)\r\n, :P_LastUpdateUserID -- INT\r\n)";
                        command.Parameters.Add("P_TransactionID", MySqlType.Int).Value = this.F_TransactionId;
                        command.Parameters.Add("P_TranTime", MySqlType.DateTime).Value = obj2;
                        command.Parameters.Add("P_WarehouseID", MySqlType.Int).Value = this.cmbWarehouse.SelectedValue;
                        command.Parameters.Add("P_VendorID", MySqlType.Int).Value = this.cmbVendor.SelectedValue;
                        command.Parameters.Add("P_CustomerID", MySqlType.Int).Value = this.cmbCustomer.SelectedValue;
                        command.Parameters.Add("P_LotNumber", MySqlType.VarChar, 50).Value = this.txtLotNumber.Text;
                        command.Parameters.Add("P_LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                    }
                    else
                    {
                        command.CommandText = "CALL serial_add_transaction(\r\n  :P_TranType         -- VARCHAR(50)\r\n, :P_TranTime         -- DATETIME\r\n, :P_SerialID         -- INT\r\n, :P_WarehouseID      -- INT\r\n, :P_VendorID         -- INT\r\n, :P_CustomerID       -- INT\r\n, :P_OrderID          -- INT\r\n, :P_OrderDetailsID   -- INT\r\n, :P_LotNumber        -- VARCHAR(50)\r\n, :P_LastUpdateUserID -- INT\r\n)";
                        command.Parameters.Add("P_TranType", MySqlType.VarChar, 50).Value = type.Name;
                        command.Parameters.Add("P_TranTime", MySqlType.DateTime).Value = obj2;
                        command.Parameters.Add("P_SerialID", MySqlType.Int).Value = this.F_SerialId;
                        command.Parameters.Add("P_WarehouseID", MySqlType.Int).Value = this.cmbWarehouse.SelectedValue;
                        command.Parameters.Add("P_VendorID", MySqlType.Int).Value = this.cmbVendor.SelectedValue;
                        command.Parameters.Add("P_CustomerID", MySqlType.Int).Value = this.cmbCustomer.SelectedValue;
                        command.Parameters.Add("P_OrderID", MySqlType.Int).Value = DBNull.Value;
                        command.Parameters.Add("P_OrderDetailsID", MySqlType.Int).Value = DBNull.Value;
                        command.Parameters.Add("P_LotNumber", MySqlType.VarChar, 50).Value = this.txtLotNumber.Text;
                        command.Parameters.Add("P_LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                    }
                    command.ExecuteScalar();
                }
            }
            string[] tableNames = new string[] { "tbl_serial_transaction", "tbl_serial" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        [field: AccessedThroughProperty("cmbWarehouse")]
        private Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTransaction")]
        private Label lblTransaction { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbTransaction")]
        private ComboBox cmbTransaction { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbVendor")]
        private Combobox cmbVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLotNumber")]
        private TextBox txtLotNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLotNumber")]
        private Label lblLotNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblVendor")]
        private Label lblVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtManufacturer")]
        private Label txtManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtStatus")]
        private Label txtStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModelNumber")]
        private Label txtModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInventoryItem")]
        private Label txtInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblManufacturer")]
        private Label lblManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblStatus")]
        private Label lblStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblModelNumber")]
        private Label lblModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSerialNumber")]
        private Label txtSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSerialNumber")]
        private Label lblSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryItem")]
        private Label lblInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDateTime")]
        private Label lblDateTime { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDate")]
        private UltraDateTimeEditor dtbDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbTime")]
        private UltraDateTimeEditor dtbTime { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [StructLayout(LayoutKind.Sequential)]
        private struct TransactionType
        {
            public readonly string Name;
            public readonly bool EnableCustomer;
            public readonly bool EnableLotNumber;
            public readonly bool EnableVendor;
            public readonly bool EnableWarehouse;
            public readonly bool EnableDateTime;
            public static readonly FormSerialTransaction.TransactionType InFromMaintenance;
            public static readonly FormSerialTransaction.TransactionType Junked;
            public static readonly FormSerialTransaction.TransactionType Lost;
            public static readonly FormSerialTransaction.TransactionType O2TankInFromCustomer;
            public static readonly FormSerialTransaction.TransactionType O2TankInFromFilling;
            public static readonly FormSerialTransaction.TransactionType O2TankOutForFilling;
            public static readonly FormSerialTransaction.TransactionType O2TankOutToCustomer;
            public static readonly FormSerialTransaction.TransactionType OutForMaintenance;
            public static readonly FormSerialTransaction.TransactionType ReserveCancelled;
            public static readonly FormSerialTransaction.TransactionType Returned;
            public static readonly FormSerialTransaction.TransactionType Sold;
            public static readonly FormSerialTransaction.TransactionType TransferredIn;
            public static readonly FormSerialTransaction.TransactionType TransferredOut;
            static TransactionType()
            {
                InFromMaintenance = new FormSerialTransaction.TransactionType("In from Maintenance", false, true, false, false, true);
                Junked = new FormSerialTransaction.TransactionType("Junked", false, true, false, false, true);
                Lost = new FormSerialTransaction.TransactionType("Lost", false, true, false, false, true);
                O2TankInFromCustomer = new FormSerialTransaction.TransactionType("O2 Tank in from customer", false, true, false, false, true);
                O2TankInFromFilling = new FormSerialTransaction.TransactionType("O2 Tank in from filling", false, true, true, false, true);
                O2TankOutForFilling = new FormSerialTransaction.TransactionType("O2 Tank out for filling", false, true, false, true, false);
                O2TankOutToCustomer = new FormSerialTransaction.TransactionType("O2 Tank out to customer", true, true, false, false, false);
                OutForMaintenance = new FormSerialTransaction.TransactionType("Out for Maintenance", false, true, false, false, true);
                ReserveCancelled = new FormSerialTransaction.TransactionType("Reserve Cancelled", false, true, false, false, false);
                Returned = new FormSerialTransaction.TransactionType("Returned", false, true, false, false, true);
                Sold = new FormSerialTransaction.TransactionType("Sold", true, true, false, false, false);
                TransferredIn = new FormSerialTransaction.TransactionType("Transferred In", false, true, false, false, true);
                TransferredOut = new FormSerialTransaction.TransactionType("Transferred Out", false, true, false, false, false);
            }

            public TransactionType(string Name, bool Customer = false, bool DateTime = true, bool LotNumber = false, bool Vendor = false, bool Warehouse = false)
            {
                this = new FormSerialTransaction.TransactionType();
                this.Name = Name;
                this.EnableCustomer = Customer;
                this.EnableDateTime = DateTime;
                this.EnableLotNumber = LotNumber;
                this.EnableVendor = Vendor;
                this.EnableWarehouse = Warehouse;
            }

            public TransactionType(FormSerialTransaction.TransactionType other, bool? Customer = new bool?(), bool? DateTime = new bool?(), bool? LotNumber = new bool?(), bool? Vendor = new bool?(), bool? Warehouse = new bool?())
            {
                this = new FormSerialTransaction.TransactionType();
                this.Name = other.Name;
                this.EnableCustomer = (Customer != null) ? Customer.GetValueOrDefault() : other.EnableCustomer;
                this.EnableDateTime = (DateTime != null) ? DateTime.GetValueOrDefault() : other.EnableDateTime;
                this.EnableLotNumber = (LotNumber != null) ? LotNumber.GetValueOrDefault() : other.EnableLotNumber;
                this.EnableVendor = (Vendor != null) ? Vendor.GetValueOrDefault() : other.EnableVendor;
                this.EnableWarehouse = (Warehouse != null) ? Warehouse.GetValueOrDefault() : other.EnableWarehouse;
            }

            public override string ToString() => 
                this.Name ?? string.Empty;

            public static FormSerialTransaction.TransactionType[] GetListByStatus(string Status)
            {
                char[] trimChars = new char[] { ' ' };
                Status = (Status ?? "").TrimEnd(trimChars);
                List<FormSerialTransaction.TransactionType> list = new List<FormSerialTransaction.TransactionType>();
                FormSerialTransaction.TransactionType item = new FormSerialTransaction.TransactionType();
                list.Add(item);
                list.Add(Junked);
                list.Add(Lost);
                list.Add(Sold);
                if (!Status.Equals("Maintenance", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(InFromMaintenance);
                }
                if (Status.Equals("Empty", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(O2TankOutForFilling);
                }
                else if (Status.Equals("Maintenance", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(InFromMaintenance);
                }
                else if (Status.Equals("On Hand", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(O2TankOutToCustomer);
                    list.Add(OutForMaintenance);
                    list.Add(TransferredOut);
                }
                else if (Status.Equals("Rented", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(O2TankInFromCustomer);
                }
                else if (Status.Equals("Sent", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(O2TankInFromFilling);
                }
                else if (Status.Equals("Transfered Out", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(TransferredIn);
                }
                else if (Status.Equals("", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(InFromMaintenance);
                    list.Add(OutForMaintenance);
                    list.Add(O2TankOutForFilling);
                    list.Add(O2TankOutToCustomer);
                    list.Add(O2TankInFromCustomer);
                    list.Add(O2TankInFromFilling);
                    list.Add(TransferredIn);
                    list.Add(TransferredOut);
                }
                list.Sort(new TransactionTypeComparer());
                return list.ToArray();
            }

            [IteratorStateMachine(typeof(VB$StateMachine_26_GetTypes))]
            private static IEnumerable<FormSerialTransaction.TransactionType> GetTypes() => 
                new VB$StateMachine_26_GetTypes(-2);

            public static FormSerialTransaction.TransactionType? GetTypeByName(string type)
            {
                using (IEnumerator<FormSerialTransaction.TransactionType> enumerator = GetTypes().GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        FormSerialTransaction.TransactionType current = enumerator.Current;
                        if (StringComparer.OrdinalIgnoreCase.Equals(current.Name, type))
                        {
                            return new FormSerialTransaction.TransactionType?(current);
                        }
                    }
                }
                return null;
            }
            private class TransactionTypeComparer : IComparer<FormSerialTransaction.TransactionType>
            {
                public int Compare(FormSerialTransaction.TransactionType x, FormSerialTransaction.TransactionType y) => 
                    StringComparer.OrdinalIgnoreCase.Compare(x.ToString(), y.ToString());
            }

            [CompilerGenerated]
            private sealed class VB$StateMachine_26_GetTypes : IEnumerable<FormSerialTransaction.TransactionType>, IEnumerable, IEnumerator<FormSerialTransaction.TransactionType>, IDisposable, IEnumerator
            {
                public int $State;
                public FormSerialTransaction.TransactionType $Current;
                public int $InitialThreadId;

                public VB$StateMachine_26_GetTypes(int $State)
                {
                    this.$State = $State;
                    this.$InitialThreadId = Environment.CurrentManagedThreadId;
                }

                private void Dispose()
                {
                }

                private IEnumerator<FormSerialTransaction.TransactionType> GetEnumerator()
                {
                    FormSerialTransaction.TransactionType.VB$StateMachine_26_GetTypes types;
                    if ((this.$State != -2) || (this.$InitialThreadId != Environment.CurrentManagedThreadId))
                    {
                        types = new FormSerialTransaction.TransactionType.VB$StateMachine_26_GetTypes(0);
                    }
                    else
                    {
                        this.$State = 0;
                        types = this;
                    }
                    return types;
                }

                IEnumerator IEnumerable.GetEnumerator() => 
                    this.GetEnumerator();

                [CompilerGenerated]
                private bool MoveNext()
                {
                    int num;
                    switch (this.$State)
                    {
                        case 0:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.InFromMaintenance;
                            this.$State = num = 1;
                            return true;

                        case 1:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.Junked;
                            this.$State = num = 2;
                            return true;

                        case 2:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.Lost;
                            this.$State = num = 3;
                            return true;

                        case 3:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.O2TankInFromCustomer;
                            this.$State = num = 4;
                            return true;

                        case 4:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.O2TankInFromFilling;
                            this.$State = num = 5;
                            return true;

                        case 5:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.O2TankOutToCustomer;
                            this.$State = num = 6;
                            return true;

                        case 6:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.OutForMaintenance;
                            this.$State = num = 7;
                            return true;

                        case 7:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.Sold;
                            this.$State = num = 8;
                            return true;

                        case 8:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.TransferredIn;
                            this.$State = num = 9;
                            return true;

                        case 9:
                            this.$State = num = -1;
                            this.$Current = FormSerialTransaction.TransactionType.TransferredOut;
                            this.$State = num = 10;
                            return true;

                        case 10:
                            this.$State = num = -1;
                            return false;
                    }
                    return false;
                }

                private void Reset()
                {
                    throw new NotSupportedException();
                }

                private FormSerialTransaction.TransactionType Current =>
                    this.$Current;

                object IEnumerator.Current =>
                    this.$Current;
            }
        }
    }
}

