namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormSerialMaintenance : DmeForm
    {
        private IContainer components;
        private int? F_ID;
        private int? F_SerialID;

        public FormSerialMaintenance()
        {
            this.InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
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
            }
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
            this.F_ID = null;
            this.F_SerialID = null;
            Functions.SetControlText(this.txtAdditionalEquipment, DBNull.Value);
            Functions.SetControlText(this.txtDescriptionOfProblem, DBNull.Value);
            Functions.SetControlText(this.txtDescriptionOfWork, DBNull.Value);
            Functions.SetControlText(this.txtInventoryItem, DBNull.Value);
            Functions.SetControlText(this.txtLaborHours, DBNull.Value);
            Functions.SetControlText(this.txtMaintenanceRecord, DBNull.Value);
            Functions.SetControlText(this.txtManufacturer, DBNull.Value);
            Functions.SetControlText(this.txtModelNumber, DBNull.Value);
            Functions.SetControlText(this.txtSerialNumber, DBNull.Value);
            Functions.SetControlText(this.txtStatus, DBNull.Value);
            Functions.SetControlText(this.txtTechnician, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbMaintenanceDue, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbMaintenanceCost, DBNull.Value);
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblMaintenanceRecord = new Label();
            this.txtMaintenanceRecord = new TextBox();
            this.txtDescriptionOfProblem = new TextBox();
            this.txtAdditionalEquipment = new TextBox();
            this.txtDescriptionOfWork = new TextBox();
            this.lblDesriptionOfWork = new Label();
            this.lblAdditionalEquipment = new Label();
            this.lblDescriptionOfProblem = new Label();
            this.lblLaborHours = new Label();
            this.lblTechnician = new Label();
            this.txtTechnician = new TextBox();
            this.txtLaborHours = new TextBox();
            this.lblSerialNumber = new Label();
            this.txtSerialNumber = new Label();
            this.lblModelNumber = new Label();
            this.lblStatus = new Label();
            this.lblManufacturer = new Label();
            this.txtInventoryItem = new Label();
            this.txtModelNumber = new Label();
            this.txtStatus = new Label();
            this.txtManufacturer = new Label();
            this.lblMaintenanceDue = new Label();
            this.dtbMaintenanceDue = new UltraDateTimeEditor();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.btnApply = new Button();
            this.Label1 = new Label();
            this.nmbMaintenanceCost = new NumericBox();
            base.SuspendLayout();
            this.lblMaintenanceRecord.Location = new Point(8, 440);
            this.lblMaintenanceRecord.Name = "lblMaintenanceRecord";
            this.lblMaintenanceRecord.Size = new Size(0x268, 0x17);
            this.lblMaintenanceRecord.TabIndex = 0x17;
            this.lblMaintenanceRecord.Text = "Maintenance Record";
            this.lblMaintenanceRecord.TextAlign = ContentAlignment.MiddleLeft;
            this.txtMaintenanceRecord.BorderStyle = BorderStyle.FixedSingle;
            this.txtMaintenanceRecord.Location = new Point(8, 0x1d0);
            this.txtMaintenanceRecord.Multiline = true;
            this.txtMaintenanceRecord.Name = "txtMaintenanceRecord";
            this.txtMaintenanceRecord.ScrollBars = ScrollBars.Vertical;
            this.txtMaintenanceRecord.Size = new Size(0x268, 0x40);
            this.txtMaintenanceRecord.TabIndex = 0x18;
            this.txtDescriptionOfProblem.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescriptionOfProblem.Location = new Point(8, 0xa8);
            this.txtDescriptionOfProblem.Multiline = true;
            this.txtDescriptionOfProblem.Name = "txtDescriptionOfProblem";
            this.txtDescriptionOfProblem.ScrollBars = ScrollBars.Vertical;
            this.txtDescriptionOfProblem.Size = new Size(0x268, 0x40);
            this.txtDescriptionOfProblem.TabIndex = 12;
            this.txtAdditionalEquipment.BorderStyle = BorderStyle.FixedSingle;
            this.txtAdditionalEquipment.Location = new Point(8, 0x120);
            this.txtAdditionalEquipment.Multiline = true;
            this.txtAdditionalEquipment.Name = "txtAdditionalEquipment";
            this.txtAdditionalEquipment.ScrollBars = ScrollBars.Vertical;
            this.txtAdditionalEquipment.Size = new Size(0x268, 0x40);
            this.txtAdditionalEquipment.TabIndex = 20;
            this.txtDescriptionOfWork.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescriptionOfWork.Location = new Point(8, 0x178);
            this.txtDescriptionOfWork.Multiline = true;
            this.txtDescriptionOfWork.Name = "txtDescriptionOfWork";
            this.txtDescriptionOfWork.ScrollBars = ScrollBars.Vertical;
            this.txtDescriptionOfWork.Size = new Size(0x268, 0x40);
            this.txtDescriptionOfWork.TabIndex = 0x16;
            this.lblDesriptionOfWork.Location = new Point(8, 0x160);
            this.lblDesriptionOfWork.Name = "lblDesriptionOfWork";
            this.lblDesriptionOfWork.Size = new Size(0x268, 0x17);
            this.lblDesriptionOfWork.TabIndex = 0x15;
            this.lblDesriptionOfWork.Text = "Desription of Work Completed:";
            this.lblDesriptionOfWork.TextAlign = ContentAlignment.MiddleLeft;
            this.lblAdditionalEquipment.Location = new Point(8, 0x108);
            this.lblAdditionalEquipment.Name = "lblAdditionalEquipment";
            this.lblAdditionalEquipment.Size = new Size(0x268, 0x17);
            this.lblAdditionalEquipment.TabIndex = 0x13;
            this.lblAdditionalEquipment.Text = "Additional Equipment Needed:";
            this.lblAdditionalEquipment.TextAlign = ContentAlignment.MiddleLeft;
            this.lblDescriptionOfProblem.Location = new Point(8, 0x90);
            this.lblDescriptionOfProblem.Name = "lblDescriptionOfProblem";
            this.lblDescriptionOfProblem.Size = new Size(0x268, 0x17);
            this.lblDescriptionOfProblem.TabIndex = 11;
            this.lblDescriptionOfProblem.Text = "Description of problem:";
            this.lblDescriptionOfProblem.TextAlign = ContentAlignment.MiddleLeft;
            this.lblLaborHours.Location = new Point(0x158, 240);
            this.lblLaborHours.Name = "lblLaborHours";
            this.lblLaborHours.Size = new Size(0x48, 0x17);
            this.lblLaborHours.TabIndex = 15;
            this.lblLaborHours.Text = "Labor Hours:";
            this.lblTechnician.Location = new Point(8, 240);
            this.lblTechnician.Name = "lblTechnician";
            this.lblTechnician.Size = new Size(0x48, 0x17);
            this.lblTechnician.TabIndex = 13;
            this.lblTechnician.Text = "Technician:";
            this.txtTechnician.BorderStyle = BorderStyle.FixedSingle;
            this.txtTechnician.Location = new Point(80, 240);
            this.txtTechnician.Name = "txtTechnician";
            this.txtTechnician.Size = new Size(0x100, 20);
            this.txtTechnician.TabIndex = 14;
            this.txtLaborHours.BorderStyle = BorderStyle.FixedSingle;
            this.txtLaborHours.Location = new Point(0x1a0, 240);
            this.txtLaborHours.Name = "txtLaborHours";
            this.txtLaborHours.Size = new Size(0x4c, 20);
            this.txtLaborHours.TabIndex = 0x10;
            this.lblSerialNumber.Location = new Point(8, 8);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new Size(0x60, 0x17);
            this.lblSerialNumber.TabIndex = 0;
            this.lblSerialNumber.Text = "Serial Number:";
            this.lblSerialNumber.TextAlign = ContentAlignment.TopRight;
            this.txtSerialNumber.Location = new Point(0x70, 8);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new Size(0x70, 0x17);
            this.txtSerialNumber.TabIndex = 1;
            this.txtSerialNumber.Text = "06KLA100766";
            this.lblModelNumber.Location = new Point(8, 0x20);
            this.lblModelNumber.Name = "lblModelNumber";
            this.lblModelNumber.Size = new Size(0x60, 0x17);
            this.lblModelNumber.TabIndex = 3;
            this.lblModelNumber.Text = "Model Number:";
            this.lblModelNumber.TextAlign = ContentAlignment.TopRight;
            this.lblStatus.Location = new Point(8, 0x38);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x60, 0x17);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status:";
            this.lblStatus.TextAlign = ContentAlignment.TopRight;
            this.lblManufacturer.Location = new Point(8, 80);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new Size(0x60, 0x17);
            this.lblManufacturer.TabIndex = 7;
            this.lblManufacturer.Text = "Manufacturer:";
            this.lblManufacturer.TextAlign = ContentAlignment.TopRight;
            this.txtInventoryItem.Location = new Point(0xe8, 8);
            this.txtInventoryItem.Name = "txtInventoryItem";
            this.txtInventoryItem.Size = new Size(0x128, 0x17);
            this.txtInventoryItem.TabIndex = 2;
            this.txtInventoryItem.Text = "LIFT,HOYER,HYDRAULIC,INVACARE";
            this.txtModelNumber.Location = new Point(0x70, 0x20);
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.Size = new Size(0x70, 0x17);
            this.txtModelNumber.TabIndex = 4;
            this.txtModelNumber.Text = "Technician:";
            this.txtStatus.Location = new Point(0x70, 0x38);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new Size(0x70, 0x17);
            this.txtStatus.TabIndex = 6;
            this.txtStatus.Text = "Rented";
            this.txtManufacturer.Location = new Point(0x70, 80);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new Size(0x70, 0x17);
            this.txtManufacturer.TabIndex = 8;
            this.txtManufacturer.Text = "INVACARE";
            this.lblMaintenanceDue.Location = new Point(8, 120);
            this.lblMaintenanceDue.Name = "lblMaintenanceDue";
            this.lblMaintenanceDue.Size = new Size(0x60, 0x17);
            this.lblMaintenanceDue.TabIndex = 9;
            this.lblMaintenanceDue.Text = "Maintenance Due";
            this.dtbMaintenanceDue.Location = new Point(0x70, 120);
            this.dtbMaintenanceDue.Name = "dtbMaintenanceDue";
            this.dtbMaintenanceDue.Size = new Size(120, 0x15);
            this.dtbMaintenanceDue.TabIndex = 10;
            this.btnCancel.Location = new Point(0x228, 0x38);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0x1b;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(0x228, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x19;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnApply.Location = new Point(0x228, 0x20);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x4b, 0x17);
            this.btnApply.TabIndex = 0x1a;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.Label1.Location = new Point(0x1f8, 240);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(40, 0x17);
            this.Label1.TabIndex = 0x11;
            this.Label1.Text = "Cost:";
            this.nmbMaintenanceCost.BorderStyle = BorderStyle.FixedSingle;
            this.nmbMaintenanceCost.Location = new Point(0x220, 240);
            this.nmbMaintenanceCost.Name = "nmbMaintenanceCost";
            this.nmbMaintenanceCost.Size = new Size(80, 20);
            this.nmbMaintenanceCost.TabIndex = 0x12;
            this.nmbMaintenanceCost.TextAlign = HorizontalAlignment.Left;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x215);
            base.Controls.Add(this.nmbMaintenanceCost);
            base.Controls.Add(this.Label1);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.dtbMaintenanceDue);
            base.Controls.Add(this.lblMaintenanceDue);
            base.Controls.Add(this.txtManufacturer);
            base.Controls.Add(this.txtStatus);
            base.Controls.Add(this.txtModelNumber);
            base.Controls.Add(this.txtInventoryItem);
            base.Controls.Add(this.lblManufacturer);
            base.Controls.Add(this.lblStatus);
            base.Controls.Add(this.lblModelNumber);
            base.Controls.Add(this.txtSerialNumber);
            base.Controls.Add(this.lblSerialNumber);
            base.Controls.Add(this.txtLaborHours);
            base.Controls.Add(this.txtTechnician);
            base.Controls.Add(this.lblTechnician);
            base.Controls.Add(this.lblLaborHours);
            base.Controls.Add(this.lblDescriptionOfProblem);
            base.Controls.Add(this.lblAdditionalEquipment);
            base.Controls.Add(this.lblDesriptionOfWork);
            base.Controls.Add(this.txtDescriptionOfWork);
            base.Controls.Add(this.txtAdditionalEquipment);
            base.Controls.Add(this.txtDescriptionOfProblem);
            base.Controls.Add(this.txtMaintenanceRecord);
            base.Controls.Add(this.lblMaintenanceRecord);
            base.Name = "FormSerialMaintenance";
            this.Text = "Serial Maintenance";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public static FormSerialMaintenance Load(int ID)
        {
            FormSerialMaintenance maintenance1 = new FormSerialMaintenance();
            maintenance1.LoadObject(ID);
            return maintenance1;
        }

        private void LoadObject(int ID)
        {
            this.ClearObject();
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  tbl_serial_maintenance.ID\r\n, tbl_serial_maintenance.AdditionalEquipment\r\n, tbl_serial_maintenance.DescriptionOfProblem\r\n, tbl_serial_maintenance.DescriptionOfWork\r\n, tbl_serial_maintenance.LaborHours\r\n, tbl_serial_maintenance.MaintenanceRecord\r\n, tbl_serial_maintenance.Technician\r\n, tbl_serial_maintenance.MaintenanceDue\r\n, tbl_serial_maintenance.MaintenanceCost\r\n, tbl_serial.ID as SerialID\r\n, tbl_serial.SerialNumber\r\n, tbl_serial.ModelNumber\r\n, tbl_serial.Status\r\n, tbl_inventoryitem.Name as InventoryItemName\r\n, tbl_manufacturer.Name as ManufacturerName\r\nFROM tbl_serial_maintenance\r\n     LEFT JOIN tbl_serial        ON tbl_serial_maintenance.SerialID = tbl_serial.ID\r\n     LEFT JOIN tbl_inventoryitem ON tbl_serial.InventoryItemID      = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_manufacturer  ON tbl_serial.ManufacturerID       = tbl_manufacturer.ID\r\nWHERE tbl_serial_maintenance.ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.F_ID = NullableConvert.ToInt32(reader["ID"]);
                            Functions.SetControlText(this.txtAdditionalEquipment, reader["AdditionalEquipment"]);
                            Functions.SetControlText(this.txtDescriptionOfProblem, reader["DescriptionOfProblem"]);
                            Functions.SetControlText(this.txtDescriptionOfWork, reader["DescriptionOfWork"]);
                            Functions.SetControlText(this.txtLaborHours, reader["LaborHours"]);
                            Functions.SetControlText(this.txtMaintenanceRecord, reader["MaintenanceRecord"]);
                            Functions.SetControlText(this.txtTechnician, reader["Technician"]);
                            Functions.SetDateBoxValue(this.dtbMaintenanceDue, reader["MaintenanceDue"]);
                            Functions.SetNumericBoxValue(this.nmbMaintenanceCost, reader["MaintenanceCost"]);
                            this.F_SerialID = NullableConvert.ToInt32(reader["SerialID"]);
                            Functions.SetControlText(this.txtInventoryItem, reader["InventoryItemName"]);
                            Functions.SetControlText(this.txtManufacturer, reader["ManufacturerName"]);
                            Functions.SetControlText(this.txtModelNumber, reader["ModelNumber"]);
                            Functions.SetControlText(this.txtSerialNumber, reader["SerialNumber"]);
                            Functions.SetControlText(this.txtStatus, reader["Status"]);
                        }
                    }
                }
            }
        }

        public static FormSerialMaintenance New(int SerialID)
        {
            FormSerialMaintenance maintenance1 = new FormSerialMaintenance();
            maintenance1.NewObject(SerialID);
            return maintenance1;
        }

        private void NewObject(int SerialID)
        {
            this.ClearObject();
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  tbl_serial.ID as SerialID\r\n, tbl_serial.SerialNumber\r\n, tbl_serial.ModelNumber\r\n, tbl_serial.Status\r\n, tbl_inventoryitem.Name as InventoryItemName\r\n, tbl_manufacturer.Name as ManufacturerName\r\nFROM tbl_serial\r\n     LEFT JOIN tbl_inventoryitem ON tbl_serial.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_manufacturer ON tbl_serial.ManufacturerID = tbl_manufacturer.ID\r\nWHERE tbl_serial.ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = SerialID;
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.F_SerialID = NullableConvert.ToInt32(reader["SerialID"]);
                            Functions.SetControlText(this.txtInventoryItem, reader["InventoryItemName"]);
                            Functions.SetControlText(this.txtManufacturer, reader["ManufacturerName"]);
                            Functions.SetControlText(this.txtModelNumber, reader["ModelNumber"]);
                            Functions.SetControlText(this.txtSerialNumber, reader["SerialNumber"]);
                            Functions.SetControlText(this.txtStatus, reader["Status"]);
                        }
                    }
                }
            }
        }

        private void SaveObject()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("AdditionalEquipment", MySqlType.Text, 0xffff).Value = this.txtAdditionalEquipment.Text;
                    command.Parameters.Add("DescriptionOfProblem", MySqlType.Text, 0xffff).Value = this.txtDescriptionOfProblem.Text;
                    command.Parameters.Add("DescriptionOfWork", MySqlType.Text, 0xffff).Value = this.txtDescriptionOfWork.Text;
                    command.Parameters.Add("MaintenanceRecord", MySqlType.Text, 0xffff).Value = this.txtMaintenanceRecord.Text;
                    command.Parameters.Add("MaintenanceCost", MySqlType.Decimal).Value = this.nmbMaintenanceCost.AsDecimal.GetValueOrDefault(0M);
                    command.Parameters.Add("LaborHours", MySqlType.VarChar, 0xff).Value = this.txtLaborHours.Text;
                    command.Parameters.Add("Technician", MySqlType.VarChar, 0xff).Value = this.txtTechnician.Text;
                    command.Parameters.Add("MaintenanceDue", MySqlType.Date).Value = this.dtbMaintenanceDue.Value;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (this.F_ID == null)
                    {
                        command.Parameters.Add("SerialID", MySqlType.Int).Value = this.F_SerialID.Value;
                        command.ExecuteInsert("tbl_serial_maintenance");
                        this.F_ID = new int?(command.GetLastIdentity());
                    }
                    else
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = this.F_ID.Value;
                        string[] whereParameters = new string[] { "ID" };
                        if (command.ExecuteUpdate("tbl_serial_maintenance", whereParameters) == 0)
                        {
                            command.Parameters.Add("SerialID", MySqlType.Int).Value = this.F_SerialID.Value;
                            command.ExecuteInsert("tbl_serial_maintenance");
                        }
                    }
                }
            }
            string[] tableNames = new string[] { "tbl_serial_maintenance" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        [field: AccessedThroughProperty("txtMaintenanceRecord")]
        private TextBox txtMaintenanceRecord { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDescriptionOfProblem")]
        private TextBox txtDescriptionOfProblem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAdditionalEquipment")]
        private TextBox txtAdditionalEquipment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDescriptionOfWork")]
        private TextBox txtDescriptionOfWork { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMaintenanceRecord")]
        private Label lblMaintenanceRecord { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDesriptionOfWork")]
        private Label lblDesriptionOfWork { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAdditionalEquipment")]
        private Label lblAdditionalEquipment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDescriptionOfProblem")]
        private Label lblDescriptionOfProblem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLaborHours")]
        private Label lblLaborHours { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTechnician")]
        private Label lblTechnician { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSerialNumber")]
        private Label lblSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSerialNumber")]
        private Label txtSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblModelNumber")]
        private Label lblModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblStatus")]
        private Label lblStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblManufacturer")]
        private Label lblManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInventoryItem")]
        private Label txtInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModelNumber")]
        private Label txtModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtStatus")]
        private Label txtStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtManufacturer")]
        private Label txtManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMaintenanceDue")]
        private Label lblMaintenanceDue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbMaintenanceDue")]
        private UltraDateTimeEditor dtbMaintenanceDue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTechnician")]
        private TextBox txtTechnician { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLaborHours")]
        private TextBox txtLaborHours { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnApply")]
        private Button btnApply { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbMaintenanceCost")]
        internal virtual NumericBox nmbMaintenanceCost { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

