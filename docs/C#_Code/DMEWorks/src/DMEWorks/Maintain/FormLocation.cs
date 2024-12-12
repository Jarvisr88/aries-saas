namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
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
    public class FormLocation : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormLocation()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_location", "tbl_user_location" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            Functions.AttachPhoneAutoInput(this.txtFax);
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtPhone2);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.chbPrintInfoOnDelPupTicket);
            base.ChangesTracker.Subscribe(this.chbPrintInfoOnInvoiceAcctStatements);
            base.ChangesTracker.Subscribe(this.chbPrintInfoOnPartProvider);
            base.ChangesTracker.Subscribe(this.cmbPOSType);
            base.ChangesTracker.Subscribe(this.cmbTaxIDType);
            base.ChangesTracker.Subscribe(this.cmbTaxRate);
            base.ChangesTracker.Subscribe(this.cmbWarehouse);
            base.ChangesTracker.Subscribe(this.txtCode);
            base.ChangesTracker.Subscribe(this.txtContact);
            base.ChangesTracker.Subscribe(this.txtEmail);
            base.ChangesTracker.Subscribe(this.txtFax);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtNPI);
            base.ChangesTracker.Subscribe(this.txtPhone);
            base.ChangesTracker.Subscribe(this.txtPhone2);
            base.ChangesTracker.Subscribe(this.txtTaxID);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.txtNPI, DBNull.Value);
            Functions.SetTextBoxText(this.txtCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtContact, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtEmail, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtFax, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone2, DBNull.Value);
            Functions.SetTextBoxText(this.txtTaxID, DBNull.Value);
            Functions.SetComboBoxText(this.cmbTaxIDType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPOSType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbWarehouse, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbTaxRate, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrintInfoOnInvoiceAcctStatements, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrintInfoOnDelPupTicket, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrintInfoOnPartProvider, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_location"))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
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

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete location '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Location '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_location", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbTaxIDType, table, "TaxIDType");
            }
            Cache.InitDropdown(this.cmbPOSType, "tbl_postype", null);
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            Cache.InitDropdown(this.cmbTaxRate, "tbl_taxrate", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.lblName = new Label();
            this.lblPhone2 = new Label();
            this.lblFax = new Label();
            this.lblPhone = new Label();
            this.lblFedTaxId = new Label();
            this.chbPrintInfoOnInvoiceAcctStatements = new CheckBox();
            this.chbPrintInfoOnDelPupTicket = new CheckBox();
            this.chbPrintInfoOnPartProvider = new CheckBox();
            this.CAddress = new ControlAddress();
            this.txtPhone2 = new TextBox();
            this.txtPhone = new TextBox();
            this.txtFax = new TextBox();
            this.txtTaxID = new TextBox();
            this.lblCode = new Label();
            this.txtCode = new TextBox();
            this.lblContact = new Label();
            this.txtContact = new TextBox();
            this.lblTaxIDType = new Label();
            this.cmbTaxIDType = new ComboBox();
            this.txtEmail = new TextBox();
            this.lblEmail = new Label();
            this.cmbWarehouse = new Combobox();
            this.TabControl1 = new TabControl();
            this.tpContact = new TabPage();
            this.tpMisc = new TabPage();
            this.lblWarehouse = new Label();
            this.lblNPI = new Label();
            this.txtNPI = new TextBox();
            this.tpPrint = new TabPage();
            this.Label2 = new Label();
            this.Panel1 = new Panel();
            this.cmbTaxRate = new Combobox();
            this.lblTaxRate = new Label();
            this.cmbPOSType = new ComboBox();
            this.lblPOSType = new Label();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            this.TabControl1.SuspendLayout();
            this.tpContact.SuspendLayout();
            this.tpMisc.SuspendLayout();
            this.tpPrint.SuspendLayout();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x164, 0x116);
            base.tpWorkArea.Visible = true;
            this.txtName.Location = new Point(0x60, 8);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 20);
            this.txtName.TabIndex = 1;
            this.lblName.Location = new Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(80, 0x16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Location Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone2.Location = new Point(8, 160);
            this.lblPhone2.Name = "lblPhone2";
            this.lblPhone2.Size = new Size(80, 0x16);
            this.lblPhone2.TabIndex = 7;
            this.lblPhone2.Text = "Phone 2";
            this.lblPhone2.TextAlign = ContentAlignment.MiddleRight;
            this.lblFax.Location = new Point(8, 0x88);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new Size(80, 0x16);
            this.lblFax.TabIndex = 5;
            this.lblFax.Text = "Fax";
            this.lblFax.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone.Location = new Point(8, 0x70);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new Size(80, 0x16);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Phone ";
            this.lblPhone.TextAlign = ContentAlignment.MiddleRight;
            this.lblFedTaxId.Location = new Point(8, 0x38);
            this.lblFedTaxId.Name = "lblFedTaxId";
            this.lblFedTaxId.Size = new Size(80, 0x16);
            this.lblFedTaxId.TabIndex = 4;
            this.lblFedTaxId.Text = "Federal Tax ID";
            this.lblFedTaxId.TextAlign = ContentAlignment.MiddleRight;
            this.chbPrintInfoOnInvoiceAcctStatements.Location = new Point(8, 0x38);
            this.chbPrintInfoOnInvoiceAcctStatements.Name = "chbPrintInfoOnInvoiceAcctStatements";
            this.chbPrintInfoOnInvoiceAcctStatements.Size = new Size(0xb0, 0x15);
            this.chbPrintInfoOnInvoiceAcctStatements.TabIndex = 2;
            this.chbPrintInfoOnInvoiceAcctStatements.Text = "Invoices / Account Statements";
            this.chbPrintInfoOnDelPupTicket.Location = new Point(8, 0x20);
            this.chbPrintInfoOnDelPupTicket.Name = "chbPrintInfoOnDelPupTicket";
            this.chbPrintInfoOnDelPupTicket.Size = new Size(0xb0, 0x15);
            this.chbPrintInfoOnDelPupTicket.TabIndex = 1;
            this.chbPrintInfoOnDelPupTicket.Text = "Delivery / Pickup Tickets";
            this.chbPrintInfoOnPartProvider.Location = new Point(8, 80);
            this.chbPrintInfoOnPartProvider.Name = "chbPrintInfoOnPartProvider";
            this.chbPrintInfoOnPartProvider.Size = new Size(0xb0, 0x15);
            this.chbPrintInfoOnPartProvider.TabIndex = 3;
            this.chbPrintInfoOnPartProvider.Text = "Participating Provider";
            this.CAddress.BackColor = SystemColors.Control;
            this.CAddress.Location = new Point(0x18, 0x20);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x138, 0x48);
            this.CAddress.TabIndex = 2;
            this.txtPhone2.Location = new Point(0x60, 160);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new Size(240, 20);
            this.txtPhone2.TabIndex = 8;
            this.txtPhone.Location = new Point(0x60, 0x70);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(240, 20);
            this.txtPhone.TabIndex = 4;
            this.txtFax.Location = new Point(0x60, 0x88);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(240, 20);
            this.txtFax.TabIndex = 6;
            this.txtTaxID.Location = new Point(0x60, 0x38);
            this.txtTaxID.MaxLength = 10;
            this.txtTaxID.Name = "txtTaxID";
            this.txtTaxID.Size = new Size(240, 20);
            this.txtTaxID.TabIndex = 5;
            this.lblCode.Location = new Point(8, 8);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new Size(80, 0x16);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Code";
            this.lblCode.TextAlign = ContentAlignment.MiddleRight;
            this.txtCode.Location = new Point(0x60, 8);
            this.txtCode.MaxLength = 40;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new Size(240, 20);
            this.txtCode.TabIndex = 1;
            this.lblContact.Location = new Point(8, 8);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new Size(80, 0x16);
            this.lblContact.TabIndex = 0;
            this.lblContact.Text = "Contact";
            this.lblContact.TextAlign = ContentAlignment.MiddleRight;
            this.txtContact.Location = new Point(0x60, 8);
            this.txtContact.MaxLength = 50;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new Size(240, 20);
            this.txtContact.TabIndex = 1;
            this.lblTaxIDType.Location = new Point(8, 80);
            this.lblTaxIDType.Name = "lblTaxIDType";
            this.lblTaxIDType.Size = new Size(80, 0x16);
            this.lblTaxIDType.TabIndex = 6;
            this.lblTaxIDType.Text = "Tax ID Type";
            this.lblTaxIDType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbTaxIDType.Location = new Point(0x60, 80);
            this.cmbTaxIDType.Name = "cmbTaxIDType";
            this.cmbTaxIDType.Size = new Size(0x79, 0x15);
            this.cmbTaxIDType.TabIndex = 7;
            this.txtEmail.Location = new Point(0x60, 0xb8);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(240, 20);
            this.txtEmail.TabIndex = 10;
            this.lblEmail.Location = new Point(8, 0xb8);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(80, 0x16);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = ContentAlignment.MiddleRight;
            this.cmbWarehouse.Location = new Point(0x60, 0x88);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(240, 0x15);
            this.cmbWarehouse.TabIndex = 11;
            this.TabControl1.Controls.Add(this.tpContact);
            this.TabControl1.Controls.Add(this.tpMisc);
            this.TabControl1.Controls.Add(this.tpPrint);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.Location = new Point(0, 40);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x164, 0xee);
            this.TabControl1.TabIndex = 1;
            this.tpContact.Controls.Add(this.txtPhone);
            this.tpContact.Controls.Add(this.lblPhone);
            this.tpContact.Controls.Add(this.txtEmail);
            this.tpContact.Controls.Add(this.lblFax);
            this.tpContact.Controls.Add(this.lblEmail);
            this.tpContact.Controls.Add(this.lblPhone2);
            this.tpContact.Controls.Add(this.CAddress);
            this.tpContact.Controls.Add(this.txtFax);
            this.tpContact.Controls.Add(this.lblContact);
            this.tpContact.Controls.Add(this.txtPhone2);
            this.tpContact.Controls.Add(this.txtContact);
            this.tpContact.Location = new Point(4, 0x16);
            this.tpContact.Name = "tpContact";
            this.tpContact.Padding = new Padding(3);
            this.tpContact.Size = new Size(0x15c, 0xd4);
            this.tpContact.TabIndex = 0;
            this.tpContact.Text = "Contact";
            this.tpContact.UseVisualStyleBackColor = true;
            this.tpMisc.Controls.Add(this.cmbTaxRate);
            this.tpMisc.Controls.Add(this.lblTaxRate);
            this.tpMisc.Controls.Add(this.cmbPOSType);
            this.tpMisc.Controls.Add(this.lblPOSType);
            this.tpMisc.Controls.Add(this.lblWarehouse);
            this.tpMisc.Controls.Add(this.lblNPI);
            this.tpMisc.Controls.Add(this.txtNPI);
            this.tpMisc.Controls.Add(this.lblFedTaxId);
            this.tpMisc.Controls.Add(this.cmbWarehouse);
            this.tpMisc.Controls.Add(this.lblCode);
            this.tpMisc.Controls.Add(this.txtCode);
            this.tpMisc.Controls.Add(this.cmbTaxIDType);
            this.tpMisc.Controls.Add(this.txtTaxID);
            this.tpMisc.Controls.Add(this.lblTaxIDType);
            this.tpMisc.Location = new Point(4, 0x16);
            this.tpMisc.Name = "tpMisc";
            this.tpMisc.Padding = new Padding(3);
            this.tpMisc.Size = new Size(0x15c, 0xd4);
            this.tpMisc.TabIndex = 1;
            this.tpMisc.Text = "Misc";
            this.tpMisc.UseVisualStyleBackColor = true;
            this.lblWarehouse.Location = new Point(8, 0x88);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(80, 0x16);
            this.lblWarehouse.TabIndex = 10;
            this.lblWarehouse.Text = "Warehouse";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.lblNPI.Location = new Point(8, 0x20);
            this.lblNPI.Name = "lblNPI";
            this.lblNPI.Size = new Size(80, 0x16);
            this.lblNPI.TabIndex = 2;
            this.lblNPI.Text = "NPI";
            this.lblNPI.TextAlign = ContentAlignment.MiddleRight;
            this.txtNPI.Location = new Point(0x60, 0x20);
            this.txtNPI.MaxLength = 20;
            this.txtNPI.Name = "txtNPI";
            this.txtNPI.Size = new Size(240, 20);
            this.txtNPI.TabIndex = 3;
            this.tpPrint.Controls.Add(this.Label2);
            this.tpPrint.Controls.Add(this.chbPrintInfoOnInvoiceAcctStatements);
            this.tpPrint.Controls.Add(this.chbPrintInfoOnDelPupTicket);
            this.tpPrint.Controls.Add(this.chbPrintInfoOnPartProvider);
            this.tpPrint.Location = new Point(4, 0x16);
            this.tpPrint.Name = "tpPrint";
            this.tpPrint.Padding = new Padding(3);
            this.tpPrint.Size = new Size(0x15c, 0xd4);
            this.tpPrint.TabIndex = 2;
            this.tpPrint.Text = "Print";
            this.tpPrint.UseVisualStyleBackColor = true;
            this.Label2.Location = new Point(8, 8);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x8b, 0x16);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Print Location Info On";
            this.Label2.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel1.Controls.Add(this.txtName);
            this.Panel1.Controls.Add(this.lblName);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x164, 40);
            this.Panel1.TabIndex = 0;
            this.cmbTaxRate.Location = new Point(0x60, 160);
            this.cmbTaxRate.Name = "cmbTaxRate";
            this.cmbTaxRate.Size = new Size(240, 0x15);
            this.cmbTaxRate.TabIndex = 13;
            this.lblTaxRate.Location = new Point(8, 160);
            this.lblTaxRate.Name = "lblTaxRate";
            this.lblTaxRate.Size = new Size(80, 0x15);
            this.lblTaxRate.TabIndex = 12;
            this.lblTaxRate.Text = "Tax Rate";
            this.lblTaxRate.TextAlign = ContentAlignment.MiddleRight;
            this.cmbPOSType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPOSType.Location = new Point(0x60, 0x70);
            this.cmbPOSType.Name = "cmbPOSType";
            this.cmbPOSType.Size = new Size(240, 0x15);
            this.cmbPOSType.TabIndex = 9;
            this.lblPOSType.Location = new Point(8, 0x70);
            this.lblPOSType.Name = "lblPOSType";
            this.lblPOSType.Size = new Size(80, 0x15);
            this.lblPOSType.TabIndex = 8;
            this.lblPOSType.Text = "POS Type";
            this.lblPOSType.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x16c, 0x159);
            base.Name = "FormLocation";
            this.Text = "Maintain Location";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.tpContact.ResumeLayout(false);
            this.tpContact.PerformLayout();
            this.tpMisc.ResumeLayout(false);
            this.tpMisc.PerformLayout();
            this.tpPrint.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_location WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.txtNPI, reader["NPI"]);
                            Functions.SetTextBoxText(this.txtCode, reader["Code"]);
                            Functions.SetTextBoxText(this.txtContact, reader["Contact"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtEmail, reader["Email"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtFax, reader["Fax"]);
                            Functions.SetTextBoxText(this.txtPhone2, reader["Phone2"]);
                            Functions.SetTextBoxText(this.txtTaxID, reader["FEDTaxID"]);
                            this.txtTaxID.Text = this.txtTaxID.Text.Replace("-", string.Empty);
                            Functions.SetComboBoxText(this.cmbTaxIDType, reader["TaxIDType"]);
                            Functions.SetComboBoxValue(this.cmbPOSType, reader["POSTypeID"]);
                            Functions.SetComboBoxValue(this.cmbWarehouse, reader["WarehouseID"]);
                            Functions.SetComboBoxValue(this.cmbTaxRate, reader["TaxRateID"]);
                            Functions.SetCheckBoxChecked(this.chbPrintInfoOnInvoiceAcctStatements, reader["PrintInfoOnInvoiceAcctStatements"]);
                            Functions.SetCheckBoxChecked(this.chbPrintInfoOnDelPupTicket, reader["PrintInfoOnDelPupTicket"]);
                            Functions.SetCheckBoxChecked(this.chbPrintInfoOnPartProvider, reader["PrintInfoOnPartProvider"]);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_location" };
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
                    command.Parameters.Add("Address1", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress1.Text;
                    command.Parameters.Add("Address2", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress2.Text;
                    command.Parameters.Add("City", MySqlType.VarChar, 0x19).Value = this.CAddress.txtCity.Text;
                    command.Parameters.Add("Email", MySqlType.VarChar, 50).Value = this.txtEmail.Text;
                    command.Parameters.Add("Fax", MySqlType.VarChar, 50).Value = this.txtFax.Text;
                    command.Parameters.Add("FEDTaxID", MySqlType.VarChar, 50).Value = this.txtTaxID.Text;
                    command.Parameters.Add("TaxIDType", MySqlType.VarChar, 10).Value = this.cmbTaxIDType.Text;
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("NPI", MySqlType.VarChar, 20).Value = this.txtNPI.Text;
                    command.Parameters.Add("Code", MySqlType.VarChar, 40).Value = this.txtCode.Text;
                    command.Parameters.Add("Contact", MySqlType.VarChar, 50).Value = this.txtContact.Text;
                    command.Parameters.Add("Phone", MySqlType.VarChar, 50).Value = this.txtPhone.Text;
                    command.Parameters.Add("Phone2", MySqlType.VarChar, 50).Value = this.txtPhone2.Text;
                    command.Parameters.Add("PrintInfoOnDelPupTicket", MySqlType.Bit).Value = this.chbPrintInfoOnDelPupTicket.Checked;
                    command.Parameters.Add("PrintInfoOnInvoiceAcctStatements", MySqlType.Bit).Value = this.chbPrintInfoOnInvoiceAcctStatements.Checked;
                    command.Parameters.Add("PrintInfoOnPartProvider", MySqlType.Bit).Value = this.chbPrintInfoOnPartProvider.Checked;
                    command.Parameters.Add("POSTypeID", MySqlType.Int).Value = this.cmbPOSType.SelectedValue;
                    command.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.cmbWarehouse.SelectedValue;
                    command.Parameters.Add("TaxRateID", MySqlType.Int).Value = this.cmbTaxRate.SelectedValue;
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_location", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_location"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_location");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            using (MySqlCommand command2 = connection.CreateCommand())
                            {
                                command2.CommandText = "INSERT IGNORE INTO tbl_user_location (LocationID, UserID) VALUES (:LocationID, :UserID)";
                                command2.Parameters.Add("LocationID", MySqlType.Int).Value = ID;
                                command2.Parameters.Add("UserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                command2.ExecuteScalar();
                            }
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  ID\r\n, Name\r\n, Address1\r\n, City\r\n, State\r\nFROM tbl_location\r\nWHERE ID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + Globals.CompanyUserID.ToString() + ")\r\nORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 50);
            appearance.AddTextColumn("Name", "Name", 100);
            appearance.AddTextColumn("Address1", "Address", 150);
            appearance.AddTextColumn("City", "City", 100);
            appearance.AddTextColumn("State", "State", 50);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__151-0 e$__- = new _Closure$__151-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        public void txtTaxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar != '\b') && ((e.KeyChar != '\x0018') && (this.txtTaxID.SelectionStart == 2))) && (this.txtTaxID.Text.Length == 2))
                {
                    char[] chArray1 = new char[] { '-', e.KeyChar };
                    this.txtTaxID.SelectedText = new string(chArray1);
                    e.Handled = true;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            base.ValidationErrors.SetError(this.txtPhone, Functions.PhoneValidate(this.txtPhone.Text));
            base.ValidationErrors.SetError(this.txtPhone2, Functions.PhoneValidate(this.txtPhone2.Text));
            base.ValidationErrors.SetError(this.txtFax, Functions.PhoneValidate(this.txtFax.Text));
        }

        [field: AccessedThroughProperty("lblPhone2")]
        private Label lblPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFax")]
        private Label lblFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone")]
        private Label lblPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFedTaxId")]
        private Label lblFedTaxId { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrintInfoOnInvoiceAcctStatements")]
        private CheckBox chbPrintInfoOnInvoiceAcctStatements { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrintInfoOnDelPupTicket")]
        private CheckBox chbPrintInfoOnDelPupTicket { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrintInfoOnPartProvider")]
        private CheckBox chbPrintInfoOnPartProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone2")]
        private TextBox txtPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFax")]
        private TextBox txtFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxID")]
        private TextBox txtTaxID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCode")]
        private Label lblCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCode")]
        private TextBox txtCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblContact")]
        private Label lblContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtContact")]
        private TextBox txtContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxIDType")]
        private Label lblTaxIDType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbTaxIDType")]
        private ComboBox cmbTaxIDType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEmail")]
        private TextBox txtEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEmail")]
        private Label lblEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        internal virtual Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpContact")]
        private TabPage tpContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpMisc")]
        private TabPage tpMisc { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNPI")]
        private Label lblNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNPI")]
        private TextBox txtNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpPrint")]
        private TabPage tpPrint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbTaxRate")]
        private Combobox cmbTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxRate")]
        private Label lblTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPOSType")]
        private ComboBox cmbPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPOSType")]
        private Label lblPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__151-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

