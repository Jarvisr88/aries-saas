namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormSerial : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormSerial()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_serial", "tbl_customer", "tbl_inventoryitem" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.cmbCurrentCustomer);
            base.ChangesTracker.Subscribe(this.cmbLastCustomer);
            base.ChangesTracker.Subscribe(this.cmbManufacturer);
            base.ChangesTracker.Subscribe(this.cmbOwnRent);
            base.ChangesTracker.Subscribe(this.cmbVendor);
            base.ChangesTracker.Subscribe(this.cmbWarehouse);
            base.ChangesTracker.Subscribe(this.dtbFirstRented);
            base.ChangesTracker.Subscribe(this.dtbNextMaintenanceDate);
            base.ChangesTracker.Subscribe(this.dtbPurchaseDate);
            base.ChangesTracker.Subscribe(this.dtbSoldDate);
            base.ChangesTracker.Subscribe(this.nmbPurchaseAmount);
            base.ChangesTracker.Subscribe(this.txtLengthOfWarranty);
            base.ChangesTracker.Subscribe(this.txtLotNumber);
            base.ChangesTracker.Subscribe(this.txtManufaturerSerialNumber);
            base.ChangesTracker.Subscribe(this.txtModelNumber);
            base.ChangesTracker.Subscribe(this.txtMonthsRented);
            base.ChangesTracker.Subscribe(this.txtSerialNumber);
            base.ChangesTracker.Subscribe(this.txtStatus);
            base.ChangesTracker.Subscribe(this.txtWarranty);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            int? nullable = null;
            this.ControlSerialMaintenance1.SerialID = nullable;
            nullable = null;
            this.ControlSerialTransactions1.SerialID = nullable;
            Functions.SetComboBoxValue(this.cmbCurrentCustomer, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbLastCustomer, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbInventoryItem, DBNull.Value);
            Functions.SetTextBoxText(this.txtLengthOfWarranty, DBNull.Value);
            Functions.SetTextBoxText(this.txtLotNumber, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbManufacturer, DBNull.Value);
            Functions.SetTextBoxText(this.txtManufaturerSerialNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtModelNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtMonthsRented, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbNextMaintenanceDate, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbPurchaseAmount, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbPurchaseDate, DBNull.Value);
            Functions.SetTextBoxText(this.txtSerialNumber, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbSoldDate, DBNull.Value);
            Functions.SetTextBoxText(this.txtStatus, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbVendor, DBNull.Value);
            Functions.SetTextBoxText(this.txtWarranty, DBNull.Value);
            this.cmbWarehouse.SelectedValue = DBNull.Value;
            Functions.SetDateBoxValue(this.dtbFirstRented, DBNull.Value);
            Functions.SetComboBoxText(this.cmbOwnRent, DBNull.Value);
            this.ControlSerialMaintenance1.LoadList();
            this.ControlSerialTransactions1.LoadList();
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_serial"))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete serial '{this.txtSerialNumber.Text}'?";
            messages.DeletedSuccessfully = $"Serial '{this.txtSerialNumber.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_serial", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbOwnRent, table, "OwnRent");
            }
            Cache.InitDropdown(this.cmbManufacturer, "tbl_manufacturer", null);
            Cache.InitDropdown(this.cmbCurrentCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbLastCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbInventoryItem, "tbl_inventorycode", null);
            Cache.InitDropdown(this.cmbVendor, "tbl_vendor", null);
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblCurrentCustomer = new Label();
            this.lblInventoryCode = new Label();
            this.lblLastCustomer = new Label();
            this.lblLengthOfWarranty = new Label();
            this.txtLotNumber = new TextBox();
            this.lblLotNumber = new Label();
            this.lblManufacturer = new Label();
            this.txtManufaturerSerialNumber = new TextBox();
            this.lblManufaturerSerialNumber = new Label();
            this.txtModelNumber = new TextBox();
            this.lblModelNumber = new Label();
            this.lblMonthsRented = new Label();
            this.lblNextMaintenanceDate = new Label();
            this.dtbNextMaintenanceDate = new UltraDateTimeEditor();
            this.nmbPurchaseAmount = new NumericBox();
            this.lblPurchaseAmount = new Label();
            this.txtSerialNumber = new TextBox();
            this.lblSerialNumber = new Label();
            this.dtbPurchaseDate = new UltraDateTimeEditor();
            this.lblPurchaseDate = new Label();
            this.dtbSoldDate = new UltraDateTimeEditor();
            this.lblSoldDate = new Label();
            this.lblStatus = new Label();
            this.txtWarranty = new TextBox();
            this.lblWarranty = new Label();
            this.lblVendor = new Label();
            this.txtStatus = new TextBox();
            this.txtLengthOfWarranty = new TextBox();
            this.txtMonthsRented = new TextBox();
            this.lblWarehouse = new Label();
            this.cmbWarehouse = new Combobox();
            this.cmbInventoryItem = new Combobox();
            this.cmbVendor = new Combobox();
            this.cmbCurrentCustomer = new Combobox();
            this.cmbLastCustomer = new Combobox();
            this.cmbManufacturer = new Combobox();
            this.lblFirstRented = new Label();
            this.lblOwnRent = new Label();
            this.cmbOwnRent = new ComboBox();
            this.dtbFirstRented = new UltraDateTimeEditor();
            this.Panel1 = new Panel();
            this.TabControl1 = new TabControl();
            this.tpGeneral = new TabPage();
            this.tpMaintenance = new TabPage();
            this.ControlSerialMaintenance1 = new ControlSerialMaintenance();
            this.tpTransactions = new TabPage();
            this.ControlSerialTransactions1 = new ControlSerialTransactions();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            this.Panel1.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpMaintenance.SuspendLayout();
            this.tpTransactions.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x29a, 0x167);
            base.tpWorkArea.Visible = true;
            this.lblCurrentCustomer.Location = new Point(0x150, 40);
            this.lblCurrentCustomer.Name = "lblCurrentCustomer";
            this.lblCurrentCustomer.Size = new Size(0x60, 0x15);
            this.lblCurrentCustomer.TabIndex = 0x1a;
            this.lblCurrentCustomer.Text = "Current Customer";
            this.lblCurrentCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.lblInventoryCode.BackColor = Color.Transparent;
            this.lblInventoryCode.Location = new Point(8, 0x20);
            this.lblInventoryCode.Name = "lblInventoryCode";
            this.lblInventoryCode.Size = new Size(0x68, 0x15);
            this.lblInventoryCode.TabIndex = 2;
            this.lblInventoryCode.Text = "Inventory Code";
            this.lblInventoryCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblLastCustomer.Location = new Point(0x150, 0x40);
            this.lblLastCustomer.Name = "lblLastCustomer";
            this.lblLastCustomer.Size = new Size(0x60, 0x15);
            this.lblLastCustomer.TabIndex = 0x1c;
            this.lblLastCustomer.Text = "Last Customer";
            this.lblLastCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.lblLengthOfWarranty.Location = new Point(8, 8);
            this.lblLengthOfWarranty.Name = "lblLengthOfWarranty";
            this.lblLengthOfWarranty.Size = new Size(0x68, 0x15);
            this.lblLengthOfWarranty.TabIndex = 4;
            this.lblLengthOfWarranty.Text = "Length Of Warranty";
            this.lblLengthOfWarranty.TextAlign = ContentAlignment.MiddleRight;
            this.txtLotNumber.Location = new Point(440, 0x70);
            this.txtLotNumber.Name = "txtLotNumber";
            this.txtLotNumber.Size = new Size(0xd0, 20);
            this.txtLotNumber.TabIndex = 0x21;
            base.ToolTip1.SetToolTip(this.txtLotNumber, "for oxygen tanks");
            this.lblLotNumber.Location = new Point(0x150, 0x70);
            this.lblLotNumber.Name = "lblLotNumber";
            this.lblLotNumber.Size = new Size(0x60, 0x15);
            this.lblLotNumber.TabIndex = 0x20;
            this.lblLotNumber.Text = "Lot Number";
            this.lblLotNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblManufacturer.Location = new Point(8, 0x38);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new Size(0x68, 0x15);
            this.lblManufacturer.TabIndex = 8;
            this.lblManufacturer.Text = "Manufacturer";
            this.lblManufacturer.TextAlign = ContentAlignment.MiddleRight;
            this.txtManufaturerSerialNumber.Location = new Point(120, 80);
            this.txtManufaturerSerialNumber.Name = "txtManufaturerSerialNumber";
            this.txtManufaturerSerialNumber.Size = new Size(0xd0, 20);
            this.txtManufaturerSerialNumber.TabIndex = 11;
            base.ToolTip1.SetToolTip(this.txtManufaturerSerialNumber, "maunfacturers serial number alpha numeric");
            this.lblManufaturerSerialNumber.Location = new Point(8, 80);
            this.lblManufaturerSerialNumber.Name = "lblManufaturerSerialNumber";
            this.lblManufaturerSerialNumber.Size = new Size(0x68, 0x15);
            this.lblManufaturerSerialNumber.TabIndex = 10;
            this.lblManufaturerSerialNumber.Text = "Manufacturer Serial Number";
            this.lblManufaturerSerialNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtModelNumber.Location = new Point(120, 0x68);
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.Size = new Size(0xd0, 20);
            this.txtModelNumber.TabIndex = 13;
            base.ToolTip1.SetToolTip(this.txtModelNumber, "model # of equipment");
            this.lblModelNumber.Location = new Point(8, 0x68);
            this.lblModelNumber.Name = "lblModelNumber";
            this.lblModelNumber.Size = new Size(0x68, 0x15);
            this.lblModelNumber.TabIndex = 12;
            this.lblModelNumber.Text = "Model #";
            this.lblModelNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblMonthsRented.Location = new Point(0x150, 0x10);
            this.lblMonthsRented.Name = "lblMonthsRented";
            this.lblMonthsRented.Size = new Size(0x60, 0x15);
            this.lblMonthsRented.TabIndex = 0x18;
            this.lblMonthsRented.Text = "Months Rented";
            this.lblMonthsRented.TextAlign = ContentAlignment.MiddleRight;
            this.lblNextMaintenanceDate.Location = new Point(8, 240);
            this.lblNextMaintenanceDate.Name = "lblNextMaintenanceDate";
            this.lblNextMaintenanceDate.Size = new Size(0x80, 0x15);
            this.lblNextMaintenanceDate.TabIndex = 0x26;
            this.lblNextMaintenanceDate.Text = "Next Maintenance Date";
            this.lblNextMaintenanceDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbNextMaintenanceDate.Location = new Point(0x90, 240);
            this.dtbNextMaintenanceDate.Name = "dtbNextMaintenanceDate";
            this.dtbNextMaintenanceDate.Size = new Size(0xb8, 0x15);
            this.dtbNextMaintenanceDate.TabIndex = 0x27;
            this.nmbPurchaseAmount.Location = new Point(120, 160);
            this.nmbPurchaseAmount.Name = "nmbPurchaseAmount";
            this.nmbPurchaseAmount.Size = new Size(0xd0, 0x15);
            this.nmbPurchaseAmount.TabIndex = 0x11;
            base.ToolTip1.SetToolTip(this.nmbPurchaseAmount, "how much user paid for this equipment");
            this.lblPurchaseAmount.Location = new Point(8, 160);
            this.lblPurchaseAmount.Name = "lblPurchaseAmount";
            this.lblPurchaseAmount.Size = new Size(0x68, 0x15);
            this.lblPurchaseAmount.TabIndex = 0x10;
            this.lblPurchaseAmount.Text = "Purchase Amount";
            this.lblPurchaseAmount.TextAlign = ContentAlignment.MiddleRight;
            this.txtSerialNumber.Location = new Point(120, 8);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new Size(0xd0, 20);
            this.txtSerialNumber.TabIndex = 1;
            base.ToolTip1.SetToolTip(this.txtSerialNumber, "assest or serial number alpha numeric");
            this.lblSerialNumber.BackColor = Color.Transparent;
            this.lblSerialNumber.Location = new Point(8, 8);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new Size(0x68, 0x15);
            this.lblSerialNumber.TabIndex = 0;
            this.lblSerialNumber.Text = "Serial Number";
            this.lblSerialNumber.TextAlign = ContentAlignment.MiddleRight;
            this.dtbPurchaseDate.Location = new Point(120, 0xb8);
            this.dtbPurchaseDate.Name = "dtbPurchaseDate";
            this.dtbPurchaseDate.Size = new Size(0xd0, 0x15);
            this.dtbPurchaseDate.TabIndex = 0x13;
            base.ToolTip1.SetToolTip(this.dtbPurchaseDate, "when the user purchased the equipment");
            this.lblPurchaseDate.Location = new Point(8, 0xb8);
            this.lblPurchaseDate.Name = "lblPurchaseDate";
            this.lblPurchaseDate.Size = new Size(0x68, 0x15);
            this.lblPurchaseDate.TabIndex = 0x12;
            this.lblPurchaseDate.Text = "Purchase Date";
            this.lblPurchaseDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbSoldDate.Location = new Point(120, 0xd0);
            this.dtbSoldDate.Name = "dtbSoldDate";
            this.dtbSoldDate.Size = new Size(0xd0, 0x15);
            this.dtbSoldDate.TabIndex = 0x15;
            base.ToolTip1.SetToolTip(this.dtbSoldDate, "if serial number is sold either purchase or capped or rent to sale");
            this.lblSoldDate.Location = new Point(8, 0xd0);
            this.lblSoldDate.Name = "lblSoldDate";
            this.lblSoldDate.Size = new Size(0x68, 0x15);
            this.lblSoldDate.TabIndex = 20;
            this.lblSoldDate.Text = "Sold Date";
            this.lblSoldDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblStatus.BackColor = Color.Transparent;
            this.lblStatus.Location = new Point(0x150, 0x20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x60, 0x15);
            this.lblStatus.TabIndex = 0x16;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = ContentAlignment.MiddleRight;
            this.txtWarranty.Location = new Point(120, 0x20);
            this.txtWarranty.Name = "txtWarranty";
            this.txtWarranty.Size = new Size(0xd0, 20);
            this.txtWarranty.TabIndex = 7;
            this.lblWarranty.Location = new Point(8, 0x20);
            this.lblWarranty.Name = "lblWarranty";
            this.lblWarranty.Size = new Size(0x68, 0x15);
            this.lblWarranty.TabIndex = 6;
            this.lblWarranty.Text = "Warranty";
            this.lblWarranty.TextAlign = ContentAlignment.MiddleRight;
            this.lblVendor.Location = new Point(0x150, 0x58);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new Size(0x60, 0x15);
            this.lblVendor.TabIndex = 30;
            this.lblVendor.Text = "Vendor";
            this.lblVendor.TextAlign = ContentAlignment.MiddleRight;
            this.txtStatus.Location = new Point(440, 0x20);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new Size(0xd0, 20);
            this.txtStatus.TabIndex = 0x17;
            this.txtLengthOfWarranty.Location = new Point(120, 8);
            this.txtLengthOfWarranty.Name = "txtLengthOfWarranty";
            this.txtLengthOfWarranty.Size = new Size(0xd0, 20);
            this.txtLengthOfWarranty.TabIndex = 5;
            this.txtMonthsRented.Location = new Point(440, 0x10);
            this.txtMonthsRented.Name = "txtMonthsRented";
            this.txtMonthsRented.Size = new Size(0xd0, 20);
            this.txtMonthsRented.TabIndex = 0x19;
            base.ToolTip1.SetToolTip(this.txtMonthsRented, "running total of months rented");
            this.lblWarehouse.Location = new Point(8, 0x88);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x68, 0x15);
            this.lblWarehouse.TabIndex = 14;
            this.lblWarehouse.Text = "Warehouse";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.cmbWarehouse.Location = new Point(120, 0x88);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(0xd0, 0x15);
            this.cmbWarehouse.TabIndex = 15;
            this.cmbInventoryItem.Location = new Point(120, 0x20);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.Size = new Size(0xd0, 0x15);
            this.cmbInventoryItem.TabIndex = 3;
            this.cmbVendor.Location = new Point(440, 0x58);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.Size = new Size(0xd0, 0x15);
            this.cmbVendor.TabIndex = 0x1f;
            this.cmbCurrentCustomer.Location = new Point(440, 40);
            this.cmbCurrentCustomer.Name = "cmbCurrentCustomer";
            this.cmbCurrentCustomer.Size = new Size(0xd0, 0x15);
            this.cmbCurrentCustomer.TabIndex = 0x1b;
            this.cmbLastCustomer.Location = new Point(440, 0x40);
            this.cmbLastCustomer.Name = "cmbLastCustomer";
            this.cmbLastCustomer.Size = new Size(0xd0, 0x15);
            this.cmbLastCustomer.TabIndex = 0x1d;
            this.cmbManufacturer.Location = new Point(120, 0x38);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new Size(0xd0, 0x15);
            this.cmbManufacturer.TabIndex = 9;
            this.lblFirstRented.Location = new Point(0x150, 0x90);
            this.lblFirstRented.Name = "lblFirstRented";
            this.lblFirstRented.Size = new Size(0x60, 0x15);
            this.lblFirstRented.TabIndex = 0x22;
            this.lblFirstRented.Text = "First Rented";
            this.lblFirstRented.TextAlign = ContentAlignment.MiddleRight;
            this.lblOwnRent.Location = new Point(0x150, 0xa8);
            this.lblOwnRent.Name = "lblOwnRent";
            this.lblOwnRent.Size = new Size(0x60, 0x15);
            this.lblOwnRent.TabIndex = 0x24;
            this.lblOwnRent.Text = "Own/Rent";
            this.lblOwnRent.TextAlign = ContentAlignment.MiddleRight;
            this.cmbOwnRent.Location = new Point(440, 0xa8);
            this.cmbOwnRent.Name = "cmbOwnRent";
            this.cmbOwnRent.Size = new Size(0x79, 0x15);
            this.cmbOwnRent.TabIndex = 0x25;
            this.dtbFirstRented.Location = new Point(440, 0x90);
            this.dtbFirstRented.Name = "dtbFirstRented";
            this.dtbFirstRented.Size = new Size(120, 0x15);
            this.dtbFirstRented.TabIndex = 0x23;
            this.Panel1.Controls.Add(this.txtSerialNumber);
            this.Panel1.Controls.Add(this.lblInventoryCode);
            this.Panel1.Controls.Add(this.lblSerialNumber);
            this.Panel1.Controls.Add(this.lblStatus);
            this.Panel1.Controls.Add(this.txtStatus);
            this.Panel1.Controls.Add(this.cmbInventoryItem);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x29a, 0x40);
            this.Panel1.TabIndex = 0x2a;
            this.TabControl1.Controls.Add(this.tpGeneral);
            this.TabControl1.Controls.Add(this.tpMaintenance);
            this.TabControl1.Controls.Add(this.tpTransactions);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.Location = new Point(0, 0x40);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x29a, 0x127);
            this.TabControl1.TabIndex = 0x2b;
            this.tpGeneral.Controls.Add(this.dtbFirstRented);
            this.tpGeneral.Controls.Add(this.txtManufaturerSerialNumber);
            this.tpGeneral.Controls.Add(this.cmbOwnRent);
            this.tpGeneral.Controls.Add(this.lblOwnRent);
            this.tpGeneral.Controls.Add(this.lblLengthOfWarranty);
            this.tpGeneral.Controls.Add(this.lblFirstRented);
            this.tpGeneral.Controls.Add(this.cmbLastCustomer);
            this.tpGeneral.Controls.Add(this.lblManufacturer);
            this.tpGeneral.Controls.Add(this.cmbCurrentCustomer);
            this.tpGeneral.Controls.Add(this.cmbManufacturer);
            this.tpGeneral.Controls.Add(this.cmbVendor);
            this.tpGeneral.Controls.Add(this.lblManufaturerSerialNumber);
            this.tpGeneral.Controls.Add(this.txtMonthsRented);
            this.tpGeneral.Controls.Add(this.lblModelNumber);
            this.tpGeneral.Controls.Add(this.txtLotNumber);
            this.tpGeneral.Controls.Add(this.lblNextMaintenanceDate);
            this.tpGeneral.Controls.Add(this.lblVendor);
            this.tpGeneral.Controls.Add(this.lblPurchaseAmount);
            this.tpGeneral.Controls.Add(this.lblMonthsRented);
            this.tpGeneral.Controls.Add(this.cmbWarehouse);
            this.tpGeneral.Controls.Add(this.lblLotNumber);
            this.tpGeneral.Controls.Add(this.dtbNextMaintenanceDate);
            this.tpGeneral.Controls.Add(this.lblLastCustomer);
            this.tpGeneral.Controls.Add(this.nmbPurchaseAmount);
            this.tpGeneral.Controls.Add(this.lblCurrentCustomer);
            this.tpGeneral.Controls.Add(this.lblWarehouse);
            this.tpGeneral.Controls.Add(this.lblPurchaseDate);
            this.tpGeneral.Controls.Add(this.dtbPurchaseDate);
            this.tpGeneral.Controls.Add(this.txtLengthOfWarranty);
            this.tpGeneral.Controls.Add(this.lblSoldDate);
            this.tpGeneral.Controls.Add(this.txtWarranty);
            this.tpGeneral.Controls.Add(this.dtbSoldDate);
            this.tpGeneral.Controls.Add(this.txtModelNumber);
            this.tpGeneral.Controls.Add(this.lblWarranty);
            this.tpGeneral.Location = new Point(4, 0x16);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new Padding(3);
            this.tpGeneral.Size = new Size(0x292, 0x10d);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            this.tpMaintenance.Controls.Add(this.ControlSerialMaintenance1);
            this.tpMaintenance.Location = new Point(4, 0x16);
            this.tpMaintenance.Name = "tpMaintenance";
            this.tpMaintenance.Padding = new Padding(3);
            this.tpMaintenance.Size = new Size(0x292, 0x10d);
            this.tpMaintenance.TabIndex = 1;
            this.tpMaintenance.Text = "Maintenance";
            this.tpMaintenance.UseVisualStyleBackColor = true;
            this.ControlSerialMaintenance1.Dock = DockStyle.Fill;
            this.ControlSerialMaintenance1.Location = new Point(3, 3);
            this.ControlSerialMaintenance1.Name = "ControlSerialMaintenance1";
            int? nullable = null;
            this.ControlSerialMaintenance1.SerialID = nullable;
            this.ControlSerialMaintenance1.Size = new Size(0x28c, 0x107);
            this.ControlSerialMaintenance1.TabIndex = 0;
            this.tpTransactions.Controls.Add(this.ControlSerialTransactions1);
            this.tpTransactions.Location = new Point(4, 0x16);
            this.tpTransactions.Name = "tpTransactions";
            this.tpTransactions.Padding = new Padding(3);
            this.tpTransactions.Size = new Size(0x292, 0x10d);
            this.tpTransactions.TabIndex = 2;
            this.tpTransactions.Text = "Transactions";
            this.tpTransactions.UseVisualStyleBackColor = true;
            this.ControlSerialTransactions1.Dock = DockStyle.Fill;
            this.ControlSerialTransactions1.Location = new Point(3, 3);
            this.ControlSerialTransactions1.Name = "ControlSerialTransactions1";
            nullable = null;
            this.ControlSerialTransactions1.SerialID = nullable;
            this.ControlSerialTransactions1.Size = new Size(0x28c, 0x107);
            this.ControlSerialTransactions1.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x2a2, 0x1aa);
            base.Name = "FormSerial";
            this.Text = "Maintain Serial";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.tpMaintenance.ResumeLayout(false);
            this.tpTransactions.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [HandleDatabaseChanged("tbl_serial_maintenance")]
        private void Load_Table_Serial_Maintenance()
        {
            this.ControlSerialMaintenance1.LoadList();
        }

        [HandleDatabaseChanged("tbl_serial_transaction")]
        private void Load_Table_Serial_Transaction()
        {
            this.ControlSerialTransactions1.LoadList();
            if (Versioned.IsNumeric(this.ObjectID))
            {
                base.OpenObject(this.ObjectID);
            }
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "CALL serial_refresh(:P_SerialID)";
                    command.Parameters.Add("P_SerialID", MySqlType.Int).Value = ID;
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.CommandText = "SELECT * FROM tbl_serial WHERE ID = :ID";
                    command2.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.ControlSerialMaintenance1.SerialID = NullableConvert.ToInt32(this.ObjectID);
                            this.ControlSerialTransactions1.SerialID = NullableConvert.ToInt32(this.ObjectID);
                            Functions.SetComboBoxValue(this.cmbCurrentCustomer, reader["CurrentCustomerID"]);
                            Functions.SetComboBoxValue(this.cmbLastCustomer, reader["LastCustomerID"]);
                            Functions.SetComboBoxValue(this.cmbInventoryItem, reader["InventoryItemID"]);
                            Functions.SetTextBoxText(this.txtLengthOfWarranty, reader["LengthOfWarranty"]);
                            Functions.SetTextBoxText(this.txtLotNumber, reader["LotNumber"]);
                            Functions.SetComboBoxValue(this.cmbManufacturer, reader["ManufacturerID"]);
                            Functions.SetTextBoxText(this.txtManufaturerSerialNumber, reader["ManufaturerSerialNumber"]);
                            Functions.SetTextBoxText(this.txtModelNumber, reader["ModelNumber"]);
                            Functions.SetTextBoxText(this.txtMonthsRented, reader["MonthsRented"]);
                            Functions.SetDateBoxValue(this.dtbNextMaintenanceDate, reader["NextMaintenanceDate"]);
                            Functions.SetNumericBoxValue(this.nmbPurchaseAmount, reader["PurchaseAmount"]);
                            Functions.SetDateBoxValue(this.dtbPurchaseDate, reader["PurchaseDate"]);
                            Functions.SetTextBoxText(this.txtSerialNumber, reader["SerialNumber"]);
                            Functions.SetDateBoxValue(this.dtbSoldDate, reader["SoldDate"]);
                            Functions.SetTextBoxText(this.txtStatus, reader["Status"]);
                            Functions.SetComboBoxValue(this.cmbVendor, reader["VendorID"]);
                            Functions.SetTextBoxText(this.txtWarranty, reader["Warranty"]);
                            this.cmbWarehouse.SelectedValue = reader["WarehouseID"];
                            Functions.SetDateBoxValue(this.dtbFirstRented, reader["FirstRented"]);
                            Functions.SetComboBoxText(this.cmbOwnRent, reader["OwnRent"]);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            this.ControlSerialMaintenance1.LoadList();
            this.ControlSerialTransactions1.LoadList();
            return true;
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_serial" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("CurrentCustomerID", MySqlType.Int).Value = this.cmbCurrentCustomer.SelectedValue;
                    command.Parameters.Add("InventoryItemID", MySqlType.Int).Value = this.cmbInventoryItem.SelectedValue;
                    command.Parameters.Add("LastCustomerID", MySqlType.Int).Value = this.cmbLastCustomer.SelectedValue;
                    command.Parameters.Add("LengthOfWarranty", MySqlType.VarChar, 50).Value = this.txtLengthOfWarranty.Text;
                    command.Parameters.Add("LotNumber", MySqlType.VarChar, 50).Value = this.txtLotNumber.Text;
                    command.Parameters.Add("ManufacturerID", MySqlType.Int).Value = this.cmbManufacturer.SelectedValue;
                    command.Parameters.Add("ManufaturerSerialNumber", MySqlType.VarChar, 50).Value = this.txtManufaturerSerialNumber.Text;
                    command.Parameters.Add("ModelNumber", MySqlType.VarChar, 50).Value = this.txtModelNumber.Text;
                    command.Parameters.Add("MonthsRented", MySqlType.VarChar, 50).Value = this.txtMonthsRented.Text;
                    command.Parameters.Add("NextMaintenanceDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbNextMaintenanceDate);
                    command.Parameters.Add("PurchaseAmount", MySqlType.Double).Value = this.nmbPurchaseAmount.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("PurchaseDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbPurchaseDate);
                    command.Parameters.Add("SerialNumber", MySqlType.VarChar, 50).Value = this.txtSerialNumber.Text;
                    command.Parameters.Add("SoldDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbSoldDate);
                    command.Parameters.Add("Status", MySqlType.VarChar, 0x10).Value = this.txtStatus.Text;
                    command.Parameters.Add("VendorID", MySqlType.Int).Value = this.cmbVendor.SelectedValue;
                    command.Parameters.Add("Warranty", MySqlType.VarChar, 50).Value = this.txtWarranty.Text;
                    command.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.cmbWarehouse.SelectedValue;
                    command.Parameters.Add("FirstRented", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbFirstRented);
                    command.Parameters.Add("OwnRent", MySqlType.VarChar, 10).Value = this.cmbOwnRent.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_serial", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_serial"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_serial");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                }
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT tbl_serial.ID,\r\n       tbl_serial.SerialNumber,\r\n       tbl_serial.ManufaturerSerialNumber,\r\n       tbl_serial.Status, \r\n       CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) AS CurrentCustomer,\r\n       tbl_serial.LotNumber,\r\n       tbl_serial.ModelNumber,\r\n       tbl_inventoryitem.Name as Description,\r\n       tbl_inventoryitem.InventoryCode\r\nFROM tbl_serial\r\n     LEFT JOIN tbl_customer ON tbl_serial.CurrentCustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_inventoryitem ON tbl_serial.InventoryItemID = tbl_inventoryitem.ID\r\nORDER BY tbl_serial.SerialNumber", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 40);
            appearance.AddTextColumn("SerialNumber", "Serial Number", 110);
            appearance.AddTextColumn("ManufaturerSerialNumber", "Manufacturer Serial", 110);
            appearance.AddTextColumn("Status", "Status", 70);
            appearance.AddTextColumn("CurrentCustomer", "Current Customer", 110);
            appearance.AddTextColumn("LotNumber", "Lot #", 70);
            appearance.AddTextColumn("ModelNumber", "Model #", 70);
            appearance.AddTextColumn("Description", "Description", 110);
            appearance.AddTextColumn("InventoryCode", "Inventory Code", 80);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__195-0 e$__- = new _Closure$__195-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (!Versioned.IsNumeric(this.cmbInventoryItem.SelectedValue))
            {
                base.ValidationErrors.SetError(this.cmbInventoryItem, "You must select inventory item");
            }
        }

        [field: AccessedThroughProperty("cmbCurrentCustomer")]
        private Combobox cmbCurrentCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbLastCustomer")]
        private Combobox cmbLastCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbManufacturer")]
        private Combobox cmbManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtStatus")]
        private TextBox txtStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbNextMaintenanceDate")]
        private UltraDateTimeEditor dtbNextMaintenanceDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbPurchaseDate")]
        private UltraDateTimeEditor dtbPurchaseDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbSoldDate")]
        private UltraDateTimeEditor dtbSoldDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCurrentCustomer")]
        private Label lblCurrentCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryCode")]
        private Label lblInventoryCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLastCustomer")]
        private Label lblLastCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLengthOfWarranty")]
        private Label lblLengthOfWarranty { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLotNumber")]
        private Label lblLotNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblManufacturer")]
        private Label lblManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblManufaturerSerialNumber")]
        private Label lblManufaturerSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblModelNumber")]
        private Label lblModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMonthsRented")]
        private Label lblMonthsRented { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNextMaintenanceDate")]
        private Label lblNextMaintenanceDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPurchaseAmount")]
        private Label lblPurchaseAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPurchaseDate")]
        private Label lblPurchaseDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSerialNumber")]
        private Label lblSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSoldDate")]
        private Label lblSoldDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblStatus")]
        private Label lblStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblVendor")]
        private Label lblVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarranty")]
        private Label lblWarranty { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPurchaseAmount")]
        private NumericBox nmbPurchaseAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLengthOfWarranty")]
        private TextBox txtLengthOfWarranty { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLotNumber")]
        private TextBox txtLotNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtManufaturerSerialNumber")]
        private TextBox txtManufaturerSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModelNumber")]
        private TextBox txtModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMonthsRented")]
        private TextBox txtMonthsRented { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSerialNumber")]
        private TextBox txtSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtWarranty")]
        private TextBox txtWarranty { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        private Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        private Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbVendor")]
        private Combobox cmbVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFirstRented")]
        private Label lblFirstRented { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOwnRent")]
        private Label lblOwnRent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbOwnRent")]
        private ComboBox cmbOwnRent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpGeneral")]
        private TabPage tpGeneral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpMaintenance")]
        private TabPage tpMaintenance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlSerialMaintenance1")]
        private ControlSerialMaintenance ControlSerialMaintenance1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpTransactions")]
        private TabPage tpTransactions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlSerialTransactions1")]
        private ControlSerialTransactions ControlSerialTransactions1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbFirstRented")]
        private UltraDateTimeEditor dtbFirstRented { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__195-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

