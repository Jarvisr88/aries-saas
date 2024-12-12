namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormProcessOxygen : DmeForm, IParameters
    {
        private const string CrLf = "\r\n";
        private IContainer components;

        public FormProcessOxygen()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
            this.tsbMaintain.Enabled = DMEWorks.Core.Permissions.FormSerial.Allow_VIEW;
        }

        private string BeforeProcessStatus(TankProcessType Value)
        {
            string str;
            switch (this.ProcessType)
            {
                case TankProcessType.Send:
                    str = "Empty";
                    break;

                case TankProcessType.Receive:
                    str = "Sent";
                    break;

                case TankProcessType.Rent:
                    str = "On Hand";
                    break;

                case TankProcessType.Pickup:
                    str = "Rented";
                    break;

                default:
                    str = string.Empty;
                    break;
            }
            return str;
        }

        public static void CopyStream(Stream src, Stream dst)
        {
            byte[] buffer = (byte[]) Array.CreateInstance(typeof(byte), 0x10000);
            while (true)
            {
                int count = src.Read(buffer, 0, buffer.Length);
                if (count == 0)
                {
                    dst.Flush();
                    return;
                }
                dst.Write(buffer, 0, count);
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

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbVendor, "tbl_vendor", null);
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            try
            {
                this.cmbWarehouse.SelectedValue = NullableConvert.ToDb(ClassGlobalObjects.DefaultWarehouseID);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormProcessOxygen));
            this.lblVendor = new Label();
            this.cmbVendor = new Combobox();
            this.pnlVendor = new Panel();
            this.pnlLotNumber = new Panel();
            this.lblLotNumber = new Label();
            this.txtLotNumber = new TextBox();
            this.pnlCustomer = new Panel();
            this.cmbCustomer = new Combobox();
            this.lblCustomer = new Label();
            this.pnlWarehouse = new Panel();
            this.cmbWarehouse = new Combobox();
            this.lblWarehouse = new Label();
            this.ToolStrip1 = new ToolStrip();
            this.tsbSend = new ToolStripButton();
            this.tsbReceive = new ToolStripButton();
            this.tsbRent = new ToolStripButton();
            this.tsbPickup = new ToolStripButton();
            this.ToolStripSeparator1 = new ToolStripSeparator();
            this.tsbProcess = new ToolStripButton();
            this.ToolStripSeparator3 = new ToolStripSeparator();
            this.tsdbPrint = new ToolStripDropDownButton();
            this.tsmiPrintGrid = new ToolStripMenuItem();
            this.tsmiPrintTicket = new ToolStripMenuItem();
            this.ToolStripSeparator2 = new ToolStripSeparator();
            this.tsbMaintain = new ToolStripButton();
            this.Grid = new SearchableGrid();
            this.cmsGrid = new ContextMenuStrip(this.components);
            this.tsmiGridCheckAll = new ToolStripMenuItem();
            this.tsmiGridUncheckAll = new ToolStripMenuItem();
            this.tsmiGridInvert = new ToolStripMenuItem();
            this.pnlVendor.SuspendLayout();
            this.pnlLotNumber.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.pnlWarehouse.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.cmsGrid.SuspendLayout();
            base.SuspendLayout();
            this.lblVendor.Location = new Point(8, 4);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new Size(0xc0, 0x15);
            this.lblVendor.TabIndex = 0;
            this.lblVendor.Text = "Select vendor for checked tanks";
            this.lblVendor.TextAlign = ContentAlignment.MiddleRight;
            this.cmbVendor.Location = new Point(0xd0, 4);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.Size = new Size(0x110, 0x15);
            this.cmbVendor.TabIndex = 1;
            this.pnlVendor.Controls.Add(this.lblVendor);
            this.pnlVendor.Controls.Add(this.cmbVendor);
            this.pnlVendor.Dock = DockStyle.Bottom;
            this.pnlVendor.Location = new Point(0, 0x1e5);
            this.pnlVendor.Name = "pnlVendor";
            this.pnlVendor.Size = new Size(0x2c8, 0x1c);
            this.pnlVendor.TabIndex = 5;
            this.pnlVendor.Visible = false;
            this.pnlLotNumber.Controls.Add(this.lblLotNumber);
            this.pnlLotNumber.Controls.Add(this.txtLotNumber);
            this.pnlLotNumber.Dock = DockStyle.Bottom;
            this.pnlLotNumber.Location = new Point(0, 0x1c9);
            this.pnlLotNumber.Name = "pnlLotNumber";
            this.pnlLotNumber.Size = new Size(0x2c8, 0x1c);
            this.pnlLotNumber.TabIndex = 4;
            this.pnlLotNumber.Visible = false;
            this.lblLotNumber.Location = new Point(8, 4);
            this.lblLotNumber.Name = "lblLotNumber";
            this.lblLotNumber.Size = new Size(0xc0, 0x15);
            this.lblLotNumber.TabIndex = 0;
            this.lblLotNumber.Text = "Enter lot number for checked tanks";
            this.lblLotNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtLotNumber.Location = new Point(0xd0, 4);
            this.txtLotNumber.Name = "txtLotNumber";
            this.txtLotNumber.Size = new Size(0x110, 20);
            this.txtLotNumber.TabIndex = 1;
            this.pnlCustomer.Controls.Add(this.cmbCustomer);
            this.pnlCustomer.Controls.Add(this.lblCustomer);
            this.pnlCustomer.Dock = DockStyle.Bottom;
            this.pnlCustomer.Location = new Point(0, 0x1ad);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new Size(0x2c8, 0x1c);
            this.pnlCustomer.TabIndex = 3;
            this.pnlCustomer.Visible = false;
            this.cmbCustomer.Location = new Point(0xd0, 4);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0x110, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.lblCustomer.Location = new Point(8, 4);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(0xc0, 0x15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Select patient for checked tanks";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.pnlWarehouse.Controls.Add(this.cmbWarehouse);
            this.pnlWarehouse.Controls.Add(this.lblWarehouse);
            this.pnlWarehouse.Dock = DockStyle.Bottom;
            this.pnlWarehouse.Location = new Point(0, 0x191);
            this.pnlWarehouse.Name = "pnlWarehouse";
            this.pnlWarehouse.Size = new Size(0x2c8, 0x1c);
            this.pnlWarehouse.TabIndex = 2;
            this.pnlWarehouse.Visible = false;
            this.cmbWarehouse.Location = new Point(0xd0, 4);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(0x110, 0x15);
            this.cmbWarehouse.TabIndex = 1;
            this.lblWarehouse.Location = new Point(8, 4);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0xc0, 0x15);
            this.lblWarehouse.TabIndex = 0;
            this.lblWarehouse.Text = "Select warehouse for checked tanks";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            ToolStripItem[] toolStripItems = new ToolStripItem[10];
            toolStripItems[0] = this.tsbSend;
            toolStripItems[1] = this.tsbReceive;
            toolStripItems[2] = this.tsbRent;
            toolStripItems[3] = this.tsbPickup;
            toolStripItems[4] = this.ToolStripSeparator1;
            toolStripItems[5] = this.tsbProcess;
            toolStripItems[6] = this.ToolStripSeparator3;
            toolStripItems[7] = this.tsdbPrint;
            toolStripItems[8] = this.ToolStripSeparator2;
            toolStripItems[9] = this.tsbMaintain;
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x2c8, 0x19);
            this.ToolStrip1.TabIndex = 2;
            this.tsbSend.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbSend.Image = (Image) manager.GetObject("tsbSend.Image");
            this.tsbSend.ImageTransparentColor = Color.Magenta;
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Size = new Size(0x23, 0x16);
            this.tsbSend.Text = "Send";
            this.tsbReceive.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbReceive.Image = (Image) manager.GetObject("tsbReceive.Image");
            this.tsbReceive.ImageTransparentColor = Color.Magenta;
            this.tsbReceive.Name = "tsbReceive";
            this.tsbReceive.Size = new Size(0x31, 0x16);
            this.tsbReceive.Text = "Receive";
            this.tsbRent.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbRent.Image = (Image) manager.GetObject("tsbRent.Image");
            this.tsbRent.ImageTransparentColor = Color.Magenta;
            this.tsbRent.Name = "tsbRent";
            this.tsbRent.Size = new Size(0x22, 0x16);
            this.tsbRent.Text = "Rent";
            this.tsbPickup.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbPickup.Image = (Image) manager.GetObject("tsbPickup.Image");
            this.tsbPickup.ImageTransparentColor = Color.Magenta;
            this.tsbPickup.Name = "tsbPickup";
            this.tsbPickup.Size = new Size(0x29, 0x16);
            this.tsbPickup.Text = "Pickup";
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new Size(6, 0x19);
            this.tsbProcess.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbProcess.Image = (Image) manager.GetObject("tsbProcess.Image");
            this.tsbProcess.ImageTransparentColor = Color.Magenta;
            this.tsbProcess.Name = "tsbProcess";
            this.tsbProcess.Size = new Size(0x30, 0x16);
            this.tsbProcess.Text = "Process";
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new Size(6, 0x19);
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.tsmiPrintGrid, this.tsmiPrintTicket };
            this.tsdbPrint.DropDownItems.AddRange(itemArray2);
            this.tsdbPrint.Image = My.Resources.Resources.ImagePrint;
            this.tsdbPrint.ImageTransparentColor = Color.Magenta;
            this.tsdbPrint.Name = "tsdbPrint";
            this.tsdbPrint.Size = new Size(0x3a, 0x16);
            this.tsdbPrint.Text = "Print";
            this.tsmiPrintGrid.Name = "tsmiPrintGrid";
            this.tsmiPrintGrid.Size = new Size(0x98, 0x16);
            this.tsmiPrintGrid.Text = "Grid";
            this.tsmiPrintTicket.Name = "tsmiPrintTicket";
            this.tsmiPrintTicket.Size = new Size(0x98, 0x16);
            this.tsmiPrintTicket.Text = "Ticket";
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new Size(6, 0x19);
            this.tsbMaintain.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbMaintain.Image = (Image) manager.GetObject("tsbMaintain.Image");
            this.tsbMaintain.ImageTransparentColor = Color.Magenta;
            this.tsbMaintain.Name = "tsbMaintain";
            this.tsbMaintain.Size = new Size(0x33, 0x16);
            this.tsbMaintain.Text = "Maintain";
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x19);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x2c8, 0x178);
            this.Grid.TabIndex = 6;
            ToolStripItem[] itemArray3 = new ToolStripItem[] { this.tsmiGridCheckAll, this.tsmiGridUncheckAll, this.tsmiGridInvert };
            this.cmsGrid.Items.AddRange(itemArray3);
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new Size(0x81, 70);
            this.tsmiGridCheckAll.Name = "tsmiGridCheckAll";
            this.tsmiGridCheckAll.Size = new Size(0x80, 0x16);
            this.tsmiGridCheckAll.Text = "Check All";
            this.tsmiGridUncheckAll.Name = "tsmiGridUncheckAll";
            this.tsmiGridUncheckAll.Size = new Size(0x80, 0x16);
            this.tsmiGridUncheckAll.Text = "Uncheck All";
            this.tsmiGridInvert.Name = "tsmiGridInvert";
            this.tsmiGridInvert.Size = new Size(0x80, 0x16);
            this.tsmiGridInvert.Text = "Invert";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x2c8, 0x201);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.pnlWarehouse);
            base.Controls.Add(this.pnlCustomer);
            base.Controls.Add(this.pnlLotNumber);
            base.Controls.Add(this.pnlVendor);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormProcessOxygen";
            this.Text = "Process Oxygen";
            this.pnlVendor.ResumeLayout(false);
            this.pnlLotNumber.ResumeLayout(false);
            this.pnlLotNumber.PerformLayout();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlWarehouse.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.cmsGrid.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(SearchableGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.AllowEdit = true;
            Appearance.Columns.Clear();
            Appearance.AddBoolColumn("Selected", "...", 0x18).ReadOnly = false;
            Appearance.AddTextColumn("SerialNumber", "Serial Number", 0x60);
            Appearance.AddTextColumn("Status", "Status", 60);
            Appearance.AddTextColumn("Warehouse", "WarehouseName", 120);
            Appearance.AddTextColumn("VendorName", "Vendor", 120);
            Appearance.AddTextColumn("PatientName", "Patient", 120);
            Appearance.AddTextColumn("LotNumber", "Lot #", 70);
            Appearance.AddTextColumn("ModelNumber", "Model #", 70);
            Appearance.ContextMenuStrip = this.cmsGrid;
        }

        private void LoadSerialList()
        {
            TankProcessType processType = this.ProcessType;
            DatasetProcessOxygen oxygen = new DatasetProcessOxygen();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT
  '{processType}' as Operation
, tbl_serial.ID as SerialID
, tbl_serial.SerialNumber
, tbl_serial.Status
, tbl_serial.LotNumber
, tbl_serial.ModelNumber
, CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as PatientName
, IFNULL(tbl_warehouse.Name, '') as WarehouseName
, IFNULL(tbl_vendor.Name, '') as VendorName
, 0 as Selected
FROM tbl_serial
     INNER JOIN tbl_inventoryitem ON tbl_serial.InventoryItemID = tbl_inventoryitem.ID
     LEFT JOIN tbl_customer ON tbl_serial.CurrentCustomerID = tbl_customer.ID
     LEFT JOIN tbl_warehouse ON tbl_serial.WarehouseID = tbl_warehouse.ID
     LEFT JOIN tbl_vendor ON tbl_serial.VendorID = tbl_vendor.ID
WHERE tbl_inventoryitem.O2Tank = 1
  AND tbl_serial.Status = '{this.BeforeProcessStatus(processType)}'
ORDER BY tbl_serial.SerialNumber", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.Fill(oxygen.Serials);
            }
            this.Grid.GridSource = oxygen.Serials.ToGridSource();
        }

        private void PerformRowOperation(RowOperation Operation)
        {
            try
            {
                DatasetProcessOxygen.SerialsDataTable tableSource = this.Grid.GetTableSource<DatasetProcessOxygen.SerialsDataTable>();
                if (tableSource != null)
                {
                    tableSource.AcceptChanges();
                    foreach (DatasetProcessOxygen.SerialsRow row in tableSource.Select())
                    {
                        RowOperation operation = Operation;
                        switch (operation)
                        {
                            case RowOperation.Check:
                                row.Selected = true;
                                break;

                            case RowOperation.Uncheck:
                                row.Selected = false;
                                break;

                            case RowOperation.Invert:
                                row.Selected = !row.Selected;
                                break;

                            default:
                                break;
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

        public void PrintTicket()
        {
            try
            {
                Directory.CreateDirectory(Path.GetTempPath());
                string tempFileName = Path.GetTempFileName();
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ReportOxygenTicket.rpt"))
                {
                    SaveStream(stream, tempFileName);
                }
                ReportParameters @params = new ReportParameters {
                    ["{?Start Date}"] = DateTime.Today,
                    ["{?End Date}"] = DateTime.Today
                };
                ClassGlobalObjects.ShowFileReport(tempFileName, @params, false);
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Print ticket");
                ProjectData.ClearProjectError();
            }
        }

        private bool ProcessSerialList()
        {
            bool flag;
            return flag;
        }

        private string ProcessTransaction(TankProcessType Value)
        {
            string str;
            switch (this.ProcessType)
            {
                case TankProcessType.Send:
                    str = "O2 Tank out for filling";
                    break;

                case TankProcessType.Receive:
                    str = "O2 Tank in from filling";
                    break;

                case TankProcessType.Rent:
                    str = "O2 Tank out to customer";
                    break;

                case TankProcessType.Pickup:
                    str = "O2 Tank in from customer";
                    break;

                default:
                    str = string.Empty;
                    break;
            }
            return str;
        }

        public static void SaveStream(Stream src, string destFilename)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }
            using (FileStream stream = new FileStream(destFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                CopyStream(src, stream);
            }
        }

        private void SetParameters(FormParameters Params)
        {
            if ((Params == null) || !Params.ContainsKey("Type"))
            {
                this.ProcessType = TankProcessType.None;
            }
            else
            {
                try
                {
                    string strA = Conversions.ToString(Params["Type"]);
                    this.ProcessType = (string.Compare(strA, "Send", true) != 0) ? ((string.Compare(strA, "Receive", true) != 0) ? ((string.Compare(strA, "Rent", true) != 0) ? ((string.Compare(strA, "Pickup", true) != 0) ? TankProcessType.None : TankProcessType.Pickup) : TankProcessType.Rent) : TankProcessType.Receive) : TankProcessType.Send;
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void tsbMaintain_Click(object sender, EventArgs e)
        {
            ClassGlobalObjects.ShowForm(FormFactories.FormSerial());
        }

        private void tsbPickup_Click(object sender, EventArgs e)
        {
            this.ProcessType = TankProcessType.Pickup;
        }

        private void tsbProcess_Click(object sender, EventArgs e)
        {
            try
            {
                DatasetProcessOxygen.SerialsDataTable tableSource = this.Grid.GetTableSource<DatasetProcessOxygen.SerialsDataTable>();
                if (tableSource != null)
                {
                    DataRow[] rowArray = tableSource.Select("[Selected] = true");
                    if ((rowArray == null) || (rowArray.Length == 0))
                    {
                        throw new UserNotifyException("You must select some serials");
                    }
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.CommandText = "CALL serial_add_transaction(\r\n  :P_TranType         -- VARCHAR(50)\r\n, :P_TranTime         -- DATETIME\r\n, :P_SerialID         -- INT\r\n, :P_WarehouseID      -- INT\r\n, :P_VendorID         -- INT\r\n, :P_CustomerID       -- INT\r\n, :P_OrderID          -- INT\r\n, :P_OrderDetailsID   -- INT\r\n, :P_LotNumber        -- VARCHAR(50)\r\n, :P_LastUpdateUserID -- INT\r\n)";
                            command.Parameters.Add("P_TranType", MySqlType.VarChar, 50).Value = this.ProcessTransaction(this.ProcessType);
                            command.Parameters.Add("P_TranTime", MySqlType.Date).Value = DBNull.Value;
                            command.Parameters.Add("P_SerialID", MySqlType.Int);
                            command.Parameters.Add("P_WarehouseID", MySqlType.Int).Value = this.cmbWarehouse.SelectedValue;
                            command.Parameters.Add("P_VendorID", MySqlType.Int).Value = this.cmbVendor.SelectedValue;
                            command.Parameters.Add("P_CustomerID", MySqlType.Int).Value = this.cmbCustomer.SelectedValue;
                            command.Parameters.Add("P_OrderID", MySqlType.Int).Value = DBNull.Value;
                            command.Parameters.Add("P_OrderDetailsID", MySqlType.Int).Value = DBNull.Value;
                            command.Parameters.Add("P_LotNumber", MySqlType.VarChar, 50).Value = this.txtLotNumber.Text;
                            command.Parameters.Add("P_LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                            connection.Open();
                            foreach (DatasetProcessOxygen.SerialsRow row in rowArray)
                            {
                                try
                                {
                                    command.Parameters["P_SerialID"].Value = row.SerialID;
                                    command.ExecuteScalar();
                                }
                                catch (Exception exception1)
                                {
                                    Exception ex = exception1;
                                    ProjectData.SetProjectError(ex);
                                    Exception exception = ex;
                                    ProjectData.ClearProjectError();
                                }
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
                Functions.SetComboBoxValue(this.cmbCustomer, DBNull.Value);
                Functions.SetComboBoxValue(this.cmbVendor, DBNull.Value);
                Functions.SetTextBoxText(this.txtLotNumber, DBNull.Value);
                this.LoadSerialList();
            }
            catch (Exception exception3)
            {
                Exception ex = exception3;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Process selected tanks");
                ProjectData.ClearProjectError();
            }
        }

        private void tsbReceive_Click(object sender, EventArgs e)
        {
            this.ProcessType = TankProcessType.Receive;
        }

        private void tsbRent_Click(object sender, EventArgs e)
        {
            this.ProcessType = TankProcessType.Rent;
        }

        private void tsbSend_Click(object sender, EventArgs e)
        {
            this.ProcessType = TankProcessType.Send;
        }

        private void tsmiGridCheckAll_Click(object sender, EventArgs e)
        {
            this.PerformRowOperation(RowOperation.Check);
        }

        private void tsmiGridInvert_Click(object sender, EventArgs e)
        {
            this.PerformRowOperation(RowOperation.Invert);
        }

        private void tsmiGridUncheckAll_Click(object sender, EventArgs e)
        {
            this.PerformRowOperation(RowOperation.Uncheck);
        }

        private void tsmiPrintGrid_Click(object sender, EventArgs e)
        {
            try
            {
                DatasetProcessOxygen.SerialsDataTable tableSource = this.Grid.GetTableSource<DatasetProcessOxygen.SerialsDataTable>();
                if (tableSource != null)
                {
                    DatasetProcessOxygen dataSet = tableSource.DataSet as DatasetProcessOxygen;
                    if (dataSet != null)
                    {
                        Directory.CreateDirectory(Path.GetTempPath());
                        string tempFileName = Path.GetTempFileName();
                        dataSet.WriteXml(tempFileName, XmlWriteMode.WriteSchema);
                        string destFilename = Path.GetTempFileName();
                        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ReportProcessOxygen.rpt"))
                        {
                            SaveStream(stream, destFilename);
                        }
                        ClassGlobalObjects.ShowFileReport(destFilename, tempFileName, null, false);
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Print selected tanks");
                ProjectData.ClearProjectError();
            }
        }

        private void tsmiPrintTicket_Click(object sender, EventArgs e)
        {
            this.PrintTicket();
        }

        protected PermissionsStruct Permissions =>
            DMEWorks.Core.Permissions.FormProcessOxygen;

        private bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        private TankProcessType ProcessType
        {
            get => 
                (!this.tsbSend.Checked || (this.tsbReceive.Checked || (this.tsbRent.Checked || this.tsbPickup.Checked))) ? ((this.tsbSend.Checked || (!this.tsbReceive.Checked || (this.tsbRent.Checked || this.tsbPickup.Checked))) ? ((this.tsbSend.Checked || (this.tsbReceive.Checked || (!this.tsbRent.Checked || this.tsbPickup.Checked))) ? ((this.tsbSend.Checked || (this.tsbReceive.Checked || (this.tsbRent.Checked || !this.tsbPickup.Checked))) ? TankProcessType.None : TankProcessType.Pickup) : TankProcessType.Rent) : TankProcessType.Receive) : TankProcessType.Send;
            set
            {
                this.tsbSend.Checked = value == TankProcessType.Send;
                this.tsbReceive.Checked = value == TankProcessType.Receive;
                this.tsbRent.Checked = value == TankProcessType.Rent;
                this.tsbPickup.Checked = value == TankProcessType.Pickup;
                switch (value)
                {
                    case TankProcessType.Send:
                        this.pnlWarehouse.Visible = false;
                        this.pnlLotNumber.Visible = false;
                        this.pnlVendor.Visible = true;
                        this.pnlCustomer.Visible = false;
                        break;

                    case TankProcessType.Receive:
                        this.pnlWarehouse.Visible = true;
                        this.pnlLotNumber.Visible = true;
                        this.pnlVendor.Visible = false;
                        this.pnlCustomer.Visible = false;
                        break;

                    case TankProcessType.Rent:
                        this.pnlWarehouse.Visible = false;
                        this.pnlLotNumber.Visible = false;
                        this.pnlVendor.Visible = false;
                        this.pnlCustomer.Visible = true;
                        break;

                    case TankProcessType.Pickup:
                        this.pnlWarehouse.Visible = true;
                        this.pnlLotNumber.Visible = false;
                        this.pnlVendor.Visible = false;
                        this.pnlCustomer.Visible = false;
                        break;

                    default:
                        this.pnlWarehouse.Visible = false;
                        this.pnlLotNumber.Visible = false;
                        this.pnlVendor.Visible = false;
                        this.pnlCustomer.Visible = false;
                        break;
                }
                try
                {
                    this.LoadSerialList();
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.ShowException(exception, "Set Process Type");
                    ProjectData.ClearProjectError();
                }
            }
        }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbVendor")]
        private Combobox cmbVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLotNumber")]
        private TextBox txtLotNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlVendor")]
        private Panel pnlVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlLotNumber")]
        private Panel pnlLotNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlCustomer")]
        private Panel pnlCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblVendor")]
        private Label lblVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLotNumber")]
        private Label lblLotNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlWarehouse")]
        private Panel pnlWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        private Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbSend")]
        private ToolStripButton tsbSend { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbReceive")]
        private ToolStripButton tsbReceive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbRent")]
        private ToolStripButton tsbRent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbPickup")]
        private ToolStripButton tsbPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStripSeparator1")]
        private ToolStripSeparator ToolStripSeparator1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbProcess")]
        private ToolStripButton tsbProcess { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStripSeparator2")]
        private ToolStripSeparator ToolStripSeparator2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbMaintain")]
        private ToolStripButton tsbMaintain { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Grid")]
        private SearchableGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid")]
        private ContextMenuStrip cmsGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridCheckAll")]
        private ToolStripMenuItem tsmiGridCheckAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridUncheckAll")]
        private ToolStripMenuItem tsmiGridUncheckAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridInvert")]
        private ToolStripMenuItem tsmiGridInvert { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsdbPrint")]
        private ToolStripDropDownButton tsdbPrint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiPrintGrid")]
        private ToolStripMenuItem tsmiPrintGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiPrintTicket")]
        private ToolStripMenuItem tsmiPrintTicket { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStripSeparator3")]
        private ToolStripSeparator ToolStripSeparator3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private enum RowOperation
        {
            Check,
            Uncheck,
            Invert
        }

        private enum TankProcessType
        {
            None,
            Send,
            Receive,
            Rent,
            Pickup
        }
    }
}

