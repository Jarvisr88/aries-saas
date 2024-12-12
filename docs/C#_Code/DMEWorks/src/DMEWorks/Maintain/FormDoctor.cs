namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using SODA;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonMissing=true)]
    public class FormDoctor : FormAutoIncrementMaintain
    {
        private IContainer components;
        private string F_MIR;
        private FormMirHelper F_MirHelper;

        public FormDoctor()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_doctor" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.chbPecosEnrolled);
            base.ChangesTracker.Subscribe(this.cmbDoctorType);
            base.ChangesTracker.Subscribe(this.CName);
            base.ChangesTracker.Subscribe(this.dtbLicenseExpired);
            base.ChangesTracker.Subscribe(this.txtContact);
            base.ChangesTracker.Subscribe(this.txtDEANumber);
            base.ChangesTracker.Subscribe(this.txtFax);
            base.ChangesTracker.Subscribe(this.txtFEDTaxID);
            base.ChangesTracker.Subscribe(this.txtLicenseNumber);
            base.ChangesTracker.Subscribe(this.txtMedicaidNumber);
            base.ChangesTracker.Subscribe(this.txtNPI);
            base.ChangesTracker.Subscribe(this.txtOtherID);
            base.ChangesTracker.Subscribe(this.txtPhone);
            base.ChangesTracker.Subscribe(this.txtPhone2);
            base.ChangesTracker.Subscribe(this.txtTitle);
            base.ChangesTracker.Subscribe(this.txtUPINNumber);
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtFax);
            Functions.AttachPhoneAutoInput(this.txtPhone2);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.F_MIR = "";
            Functions.SetComboBoxText(this.CName.cmbCourtesy, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtFirstName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtMiddleName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtLastName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtSuffix, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtFax, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone2, DBNull.Value);
            Functions.SetTextBoxText(this.txtUPINNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtMedicaidNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtNPI, DBNull.Value);
            Functions.SetTextBoxText(this.txtLicenseNumber, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbLicenseExpired, DBNull.Value);
            Functions.SetTextBoxText(this.txtOtherID, DBNull.Value);
            Functions.SetTextBoxText(this.txtFEDTaxID, DBNull.Value);
            Functions.SetTextBoxText(this.txtDEANumber, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPecosEnrolled, false);
            Functions.SetComboBoxValue(this.cmbDoctorType, DBNull.Value);
            Functions.SetTextBoxText(this.txtContact, DBNull.Value);
            Functions.SetTextBoxText(this.txtTitle, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int, 4).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_doctor"))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
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

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete doctor '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}'?";
            messages.DeletedSuccessfully = $"Doctor '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_doctor", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.CName.cmbCourtesy, table, "Courtesy");
            }
            Cache.InitDropdown(this.cmbDoctorType, "tbl_doctortype", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormDoctor));
            this.lblPhone2 = new Label();
            this.lblFax = new Label();
            this.lblPhone = new Label();
            this.txtTitle = new TextBox();
            this.txtContact = new TextBox();
            this.lblTitle = new Label();
            this.lblContact = new Label();
            this.lblDoctorType = new Label();
            this.cmbDoctorType = new Combobox();
            this.txtOtherID = new TextBox();
            this.txtLicenseNumber = new TextBox();
            this.txtMedicaidNumber = new TextBox();
            this.txtUPINNumber = new TextBox();
            this.lblOtherID = new Label();
            this.lblLicenseNumber = new Label();
            this.lblMedicaidNumber = new Label();
            this.lblUPIN = new Label();
            this.txtPhone2 = new TextBox();
            this.txtPhone = new TextBox();
            this.txtFax = new TextBox();
            this.CAddress = new ControlAddress();
            this.lblLicenseExpired = new Label();
            this.lblFEDTaxID = new Label();
            this.txtFEDTaxID = new TextBox();
            this.lblDEANumber = new Label();
            this.txtDEANumber = new TextBox();
            this.dtbLicenseExpired = new UltraDateTimeEditor();
            this.txtNPI = new TextBox();
            this.lblNPI = new Label();
            this.mnuGotoImages = new MenuItem();
            this.mnuGotoNewImage = new MenuItem();
            this.TabControl1 = new TabControl();
            this.tpAddress = new TabPage();
            this.tpNumbers = new TabPage();
            this.lnkPecosEnrolled = new LinkLabel();
            this.lblPecosEnrolled = new Label();
            this.chbPecosEnrolled = new CheckBox();
            this.tpMarketing = new TabPage();
            this.ImageList1 = new ImageList(this.components);
            this.CName = new ControlName();
            this.Panel1 = new Panel();
            base.tpWorkArea.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tpAddress.SuspendLayout();
            this.tpNumbers.SuspendLayout();
            this.tpMarketing.SuspendLayout();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x1d0, 0x142);
            MenuItem[] items = new MenuItem[] { this.mnuGotoImages, this.mnuGotoNewImage };
            base.cmnuGoto.MenuItems.AddRange(items);
            this.lblPhone2.Location = new Point(8, 0x70);
            this.lblPhone2.Name = "lblPhone2";
            this.lblPhone2.Size = new Size(0x48, 0x15);
            this.lblPhone2.TabIndex = 3;
            this.lblPhone2.Text = "Phone 2";
            this.lblPhone2.TextAlign = ContentAlignment.MiddleRight;
            this.lblFax.Location = new Point(8, 0x88);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new Size(0x48, 0x15);
            this.lblFax.TabIndex = 5;
            this.lblFax.Text = "Fax";
            this.lblFax.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone.Location = new Point(8, 0x58);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new Size(0x48, 0x15);
            this.lblPhone.TabIndex = 1;
            this.lblPhone.Text = "Phone";
            this.lblPhone.TextAlign = ContentAlignment.MiddleRight;
            this.txtTitle.Location = new Point(0x58, 0x38);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(0x148, 20);
            this.txtTitle.TabIndex = 5;
            this.txtContact.Location = new Point(0x58, 0x20);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new Size(0x148, 20);
            this.txtContact.TabIndex = 3;
            this.lblTitle.Location = new Point(8, 0x38);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0x48, 0x16);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = ContentAlignment.MiddleRight;
            this.lblContact.Location = new Point(8, 0x20);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new Size(0x48, 0x16);
            this.lblContact.TabIndex = 2;
            this.lblContact.Text = "Contact";
            this.lblContact.TextAlign = ContentAlignment.MiddleRight;
            this.lblDoctorType.Location = new Point(8, 8);
            this.lblDoctorType.Name = "lblDoctorType";
            this.lblDoctorType.Size = new Size(0x48, 0x16);
            this.lblDoctorType.TabIndex = 0;
            this.lblDoctorType.Text = "Doctor Type";
            this.lblDoctorType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbDoctorType.Location = new Point(0x58, 8);
            this.cmbDoctorType.Name = "cmbDoctorType";
            this.cmbDoctorType.Size = new Size(0x148, 0x15);
            this.cmbDoctorType.TabIndex = 1;
            this.txtOtherID.Location = new Point(0x68, 0x80);
            this.txtOtherID.MaxLength = 0;
            this.txtOtherID.Name = "txtOtherID";
            this.txtOtherID.Size = new Size(0xb0, 20);
            this.txtOtherID.TabIndex = 11;
            this.txtLicenseNumber.Location = new Point(0x68, 80);
            this.txtLicenseNumber.MaxLength = 0;
            this.txtLicenseNumber.Name = "txtLicenseNumber";
            this.txtLicenseNumber.Size = new Size(0xb0, 20);
            this.txtLicenseNumber.TabIndex = 7;
            this.txtMedicaidNumber.Location = new Point(0x68, 0x20);
            this.txtMedicaidNumber.MaxLength = 0;
            this.txtMedicaidNumber.Name = "txtMedicaidNumber";
            this.txtMedicaidNumber.Size = new Size(0xb0, 20);
            this.txtMedicaidNumber.TabIndex = 3;
            this.txtUPINNumber.Location = new Point(0x68, 8);
            this.txtUPINNumber.MaxLength = 0;
            this.txtUPINNumber.Name = "txtUPINNumber";
            this.txtUPINNumber.Size = new Size(0xb0, 20);
            this.txtUPINNumber.TabIndex = 1;
            this.lblOtherID.Location = new Point(8, 0x80);
            this.lblOtherID.Name = "lblOtherID";
            this.lblOtherID.Size = new Size(0x58, 0x15);
            this.lblOtherID.TabIndex = 10;
            this.lblOtherID.Text = "Other ID";
            this.lblOtherID.TextAlign = ContentAlignment.MiddleRight;
            this.lblLicenseNumber.Location = new Point(8, 80);
            this.lblLicenseNumber.Name = "lblLicenseNumber";
            this.lblLicenseNumber.Size = new Size(0x58, 0x15);
            this.lblLicenseNumber.TabIndex = 6;
            this.lblLicenseNumber.Text = "License #";
            this.lblLicenseNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblMedicaidNumber.Location = new Point(8, 0x20);
            this.lblMedicaidNumber.Name = "lblMedicaidNumber";
            this.lblMedicaidNumber.Size = new Size(0x58, 0x15);
            this.lblMedicaidNumber.TabIndex = 2;
            this.lblMedicaidNumber.Text = "Medicaid #";
            this.lblMedicaidNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblUPIN.Location = new Point(8, 8);
            this.lblUPIN.Name = "lblUPIN";
            this.lblUPIN.Size = new Size(0x58, 0x15);
            this.lblUPIN.TabIndex = 0;
            this.lblUPIN.Text = "UPIN";
            this.lblUPIN.TextAlign = ContentAlignment.MiddleRight;
            this.txtPhone2.Location = new Point(0x58, 0x70);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new Size(0x148, 20);
            this.txtPhone2.TabIndex = 4;
            this.txtPhone.Location = new Point(0x58, 0x58);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(0x148, 20);
            this.txtPhone.TabIndex = 2;
            this.txtFax.Location = new Point(0x58, 0x88);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(0x148, 20);
            this.txtFax.TabIndex = 6;
            this.CAddress.Location = new Point(0x10, 8);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(400, 0x48);
            this.CAddress.TabIndex = 0;
            this.lblLicenseExpired.Location = new Point(8, 0x68);
            this.lblLicenseExpired.Name = "lblLicenseExpired";
            this.lblLicenseExpired.Size = new Size(0x58, 0x15);
            this.lblLicenseExpired.TabIndex = 8;
            this.lblLicenseExpired.Text = "License Expired";
            this.lblLicenseExpired.TextAlign = ContentAlignment.MiddleRight;
            this.lblFEDTaxID.Location = new Point(8, 0x98);
            this.lblFEDTaxID.Name = "lblFEDTaxID";
            this.lblFEDTaxID.Size = new Size(0x58, 0x15);
            this.lblFEDTaxID.TabIndex = 12;
            this.lblFEDTaxID.Text = "Federal Tax ID";
            this.lblFEDTaxID.TextAlign = ContentAlignment.MiddleRight;
            this.txtFEDTaxID.Location = new Point(0x68, 0x98);
            this.txtFEDTaxID.Name = "txtFEDTaxID";
            this.txtFEDTaxID.Size = new Size(0xb0, 20);
            this.txtFEDTaxID.TabIndex = 13;
            this.lblDEANumber.Location = new Point(8, 0xb0);
            this.lblDEANumber.Name = "lblDEANumber";
            this.lblDEANumber.Size = new Size(0x58, 0x15);
            this.lblDEANumber.TabIndex = 14;
            this.lblDEANumber.Text = "DEA Number";
            this.lblDEANumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtDEANumber.Location = new Point(0x68, 0xb0);
            this.txtDEANumber.Name = "txtDEANumber";
            this.txtDEANumber.Size = new Size(0xb0, 20);
            this.txtDEANumber.TabIndex = 15;
            this.dtbLicenseExpired.Location = new Point(0x68, 0x68);
            this.dtbLicenseExpired.Name = "dtbLicenseExpired";
            this.dtbLicenseExpired.Size = new Size(0x70, 0x15);
            this.dtbLicenseExpired.TabIndex = 9;
            this.txtNPI.Location = new Point(0x68, 0x38);
            this.txtNPI.MaxLength = 10;
            this.txtNPI.Name = "txtNPI";
            this.txtNPI.Size = new Size(0xb0, 20);
            this.txtNPI.TabIndex = 5;
            this.lblNPI.BackColor = Color.Transparent;
            this.lblNPI.Cursor = Cursors.Default;
            this.lblNPI.ForeColor = SystemColors.ControlText;
            this.lblNPI.Location = new Point(8, 0x38);
            this.lblNPI.Name = "lblNPI";
            this.lblNPI.Size = new Size(0x58, 0x15);
            this.lblNPI.TabIndex = 4;
            this.lblNPI.Text = "NPI";
            this.lblNPI.TextAlign = ContentAlignment.MiddleRight;
            this.mnuGotoImages.Index = 0;
            this.mnuGotoImages.Text = "Images";
            this.mnuGotoNewImage.Index = 1;
            this.mnuGotoNewImage.Text = "New Image";
            this.TabControl1.Controls.Add(this.tpAddress);
            this.TabControl1.Controls.Add(this.tpNumbers);
            this.TabControl1.Controls.Add(this.tpMarketing);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.ImageList = this.ImageList1;
            this.TabControl1.Location = new Point(0, 0x40);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x1d0, 0x102);
            this.TabControl1.TabIndex = 1;
            this.tpAddress.Controls.Add(this.CAddress);
            this.tpAddress.Controls.Add(this.lblPhone2);
            this.tpAddress.Controls.Add(this.lblPhone);
            this.tpAddress.Controls.Add(this.txtPhone2);
            this.tpAddress.Controls.Add(this.lblFax);
            this.tpAddress.Controls.Add(this.txtPhone);
            this.tpAddress.Controls.Add(this.txtFax);
            this.tpAddress.Location = new Point(4, 0x17);
            this.tpAddress.Name = "tpAddress";
            this.tpAddress.Padding = new Padding(3);
            this.tpAddress.Size = new Size(0x1c8, 0xe7);
            this.tpAddress.TabIndex = 0;
            this.tpAddress.Text = "Address";
            this.tpAddress.UseVisualStyleBackColor = true;
            this.tpNumbers.Controls.Add(this.lnkPecosEnrolled);
            this.tpNumbers.Controls.Add(this.lblPecosEnrolled);
            this.tpNumbers.Controls.Add(this.chbPecosEnrolled);
            this.tpNumbers.Controls.Add(this.lblUPIN);
            this.tpNumbers.Controls.Add(this.txtNPI);
            this.tpNumbers.Controls.Add(this.txtOtherID);
            this.tpNumbers.Controls.Add(this.lblNPI);
            this.tpNumbers.Controls.Add(this.lblOtherID);
            this.tpNumbers.Controls.Add(this.dtbLicenseExpired);
            this.tpNumbers.Controls.Add(this.txtMedicaidNumber);
            this.tpNumbers.Controls.Add(this.lblDEANumber);
            this.tpNumbers.Controls.Add(this.lblMedicaidNumber);
            this.tpNumbers.Controls.Add(this.txtDEANumber);
            this.tpNumbers.Controls.Add(this.txtLicenseNumber);
            this.tpNumbers.Controls.Add(this.lblFEDTaxID);
            this.tpNumbers.Controls.Add(this.lblLicenseNumber);
            this.tpNumbers.Controls.Add(this.txtFEDTaxID);
            this.tpNumbers.Controls.Add(this.txtUPINNumber);
            this.tpNumbers.Controls.Add(this.lblLicenseExpired);
            this.tpNumbers.Location = new Point(4, 0x17);
            this.tpNumbers.Name = "tpNumbers";
            this.tpNumbers.Padding = new Padding(3);
            this.tpNumbers.Size = new Size(0x1c8, 0xe7);
            this.tpNumbers.TabIndex = 1;
            this.tpNumbers.Text = "Numbers";
            this.tpNumbers.UseVisualStyleBackColor = true;
            this.lnkPecosEnrolled.Location = new Point(0x80, 200);
            this.lnkPecosEnrolled.Name = "lnkPecosEnrolled";
            this.lnkPecosEnrolled.Size = new Size(0x30, 0x15);
            this.lnkPecosEnrolled.TabIndex = 0x12;
            this.lnkPecosEnrolled.TabStop = true;
            this.lnkPecosEnrolled.Text = "Check";
            this.lnkPecosEnrolled.TextAlign = ContentAlignment.MiddleLeft;
            this.lblPecosEnrolled.Location = new Point(8, 200);
            this.lblPecosEnrolled.Name = "lblPecosEnrolled";
            this.lblPecosEnrolled.Size = new Size(0x58, 0x15);
            this.lblPecosEnrolled.TabIndex = 0x10;
            this.lblPecosEnrolled.Text = "PECOS enrolled";
            this.lblPecosEnrolled.TextAlign = ContentAlignment.MiddleRight;
            this.chbPecosEnrolled.Location = new Point(0x68, 200);
            this.chbPecosEnrolled.Name = "chbPecosEnrolled";
            this.chbPecosEnrolled.Size = new Size(0x18, 0x15);
            this.chbPecosEnrolled.TabIndex = 0x11;
            this.chbPecosEnrolled.UseVisualStyleBackColor = true;
            this.tpMarketing.Controls.Add(this.cmbDoctorType);
            this.tpMarketing.Controls.Add(this.lblDoctorType);
            this.tpMarketing.Controls.Add(this.lblTitle);
            this.tpMarketing.Controls.Add(this.txtContact);
            this.tpMarketing.Controls.Add(this.lblContact);
            this.tpMarketing.Controls.Add(this.txtTitle);
            this.tpMarketing.Location = new Point(4, 0x17);
            this.tpMarketing.Name = "tpMarketing";
            this.tpMarketing.Padding = new Padding(3);
            this.tpMarketing.Size = new Size(0x1c8, 0xe7);
            this.tpMarketing.TabIndex = 2;
            this.tpMarketing.Text = "Marketing Information";
            this.tpMarketing.UseVisualStyleBackColor = true;
            this.ImageList1.ImageStream = (ImageListStreamer) manager.GetObject("ImageList1.ImageStream");
            this.ImageList1.TransparentColor = Color.Magenta;
            this.ImageList1.Images.SetKeyName(0, "");
            this.CName.Location = new Point(8, 8);
            this.CName.Name = "CName";
            this.CName.Size = new Size(0x1a8, 0x30);
            this.CName.TabIndex = 0;
            this.Panel1.Controls.Add(this.CName);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x1d0, 0x40);
            this.Panel1.TabIndex = 2;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x1d8, 0x18d);
            base.Name = "FormDoctor";
            this.Text = "Maintain Doctor";
            base.tpWorkArea.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.tpAddress.ResumeLayout(false);
            this.tpAddress.PerformLayout();
            this.tpNumbers.ResumeLayout(false);
            this.tpNumbers.PerformLayout();
            this.tpMarketing.ResumeLayout(false);
            this.tpMarketing.PerformLayout();
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "Doctor", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        private void lnkPecosEnrolled_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!Functions.IsNpiValid(this.txtNPI.Text))
                {
                    MessageBox.Show("NPI is not valid", "PECOS enrollment check", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    Resource<pecos_entry> resource = new SodaClient("https://data.cms.gov/", "1LYr1GLtDMMLpnTK7pP8zBSDW").GetResource<pecos_entry>("iadr-zq3i");
                    SoqlQuery soqlQuery = new SoqlQuery();
                    object[] args = new object[] { this.txtNPI.Text.Trim() };
                    soqlQuery.Where("npi = {0}", args);
                    soqlQuery.Limit(10);
                    pecos_entry[] _entryArray = resource.Query(soqlQuery).ToArray<pecos_entry>();
                    if ((_entryArray == null) || (_entryArray.Length == 0))
                    {
                        MessageBox.Show("Doctor is not enrolled with PECOS", "PECOS enrollment check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.chbPecosEnrolled.Checked = false;
                    }
                    else if (_entryArray.Length == 1)
                    {
                        MessageBox.Show("Doctor is enrolled with PECOS", "PECOS enrollment check", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.chbPecosEnrolled.Checked = true;
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine("Search returned multiple matches:");
                        pecos_entry[] _entryArray2 = _entryArray;
                        int index = 0;
                        while (true)
                        {
                            if (index >= _entryArray2.Length)
                            {
                                MessageBox.Show(builder.ToString(), "PECOS enrollment check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            }
                            pecos_entry _entry = _entryArray2[index];
                            builder.AppendFormat("{0}, {1} - {2}", _entry.last_name, _entry.first_name, _entry.npi).AppendLine();
                            index++;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Check PECOS enrollment");
                ProjectData.ClearProjectError();
            }
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_doctor WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.F_MIR = NullableConvert.ToString(reader["MIR"]);
                            Functions.SetComboBoxText(this.CName.cmbCourtesy, reader["Courtesy"]);
                            Functions.SetTextBoxText(this.CName.txtFirstName, reader["FirstName"]);
                            Functions.SetTextBoxText(this.CName.txtMiddleName, reader["MiddleName"]);
                            Functions.SetTextBoxText(this.CName.txtLastName, reader["LastName"]);
                            Functions.SetTextBoxText(this.CName.txtSuffix, reader["Suffix"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtFax, reader["Fax"]);
                            Functions.SetTextBoxText(this.txtPhone2, reader["Phone2"]);
                            Functions.SetTextBoxText(this.txtUPINNumber, reader["UPINNumber"]);
                            Functions.SetTextBoxText(this.txtMedicaidNumber, reader["MedicaidNumber"]);
                            Functions.SetTextBoxText(this.txtNPI, reader["NPI"]);
                            Functions.SetTextBoxText(this.txtLicenseNumber, reader["LicenseNumber"]);
                            Functions.SetDateBoxValue(this.dtbLicenseExpired, reader["LicenseExpired"]);
                            Functions.SetTextBoxText(this.txtOtherID, reader["OtherID"]);
                            Functions.SetTextBoxText(this.txtFEDTaxID, reader["FEDTaxID"]);
                            Functions.SetTextBoxText(this.txtDEANumber, reader["DEANumber"]);
                            Functions.SetCheckBoxChecked(this.chbPecosEnrolled, reader["PecosEnrolled"]);
                            Functions.SetComboBoxValue(this.cmbDoctorType, reader["TypeID"]);
                            Functions.SetTextBoxText(this.txtContact, reader["Contact"]);
                            Functions.SetTextBoxText(this.txtTitle, reader["Title"]);
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

        private void mnuGotoImages_Click(object sender, EventArgs args)
        {
            FormParameters @params = new FormParameters {
                ["DoctorID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImageSearch(), @params);
        }

        private void mnuGotoNewImage_Click(object sender, EventArgs args)
        {
            FormParameters @params = new FormParameters {
                ["DoctorID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImage(), @params);
        }

        private void mnuPrintItem_Click(object sender, EventArgs args)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_doctor.ID}"] = this.ObjectID
                };
                try
                {
                    ClassGlobalObjects.ShowReport(item.ReportFileName, @params);
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
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_doctor" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                bool flag2;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Courtesy", MySqlType.Char, 4).Value = this.CName.cmbCourtesy.Text;
                    command.Parameters.Add("FirstName", MySqlType.VarChar, 0x19).Value = this.CName.txtFirstName.Text;
                    command.Parameters.Add("LastName", MySqlType.VarChar, 30).Value = this.CName.txtLastName.Text;
                    command.Parameters.Add("MiddleName", MySqlType.Char, 1).Value = this.CName.txtMiddleName.Text;
                    command.Parameters.Add("Suffix", MySqlType.VarChar, 4).Value = this.CName.txtSuffix.Text;
                    command.Parameters.Add("Address1", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress1.Text;
                    command.Parameters.Add("Address2", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress2.Text;
                    command.Parameters.Add("City", MySqlType.VarChar, 0x19).Value = this.CAddress.txtCity.Text;
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("Fax", MySqlType.VarChar, 50).Value = this.txtFax.Text;
                    command.Parameters.Add("Phone", MySqlType.VarChar, 50).Value = this.txtPhone.Text;
                    command.Parameters.Add("Phone2", MySqlType.VarChar, 50).Value = this.txtPhone2.Text;
                    command.Parameters.Add("Contact", MySqlType.VarChar, 50).Value = this.txtContact.Text;
                    command.Parameters.Add("LicenseNumber", MySqlType.VarChar, 0x10).Value = this.txtLicenseNumber.Text;
                    command.Parameters.Add("LicenseExpired", MySqlType.Date, 8).Value = Functions.GetDateBoxValue(this.dtbLicenseExpired);
                    command.Parameters.Add("NPI", MySqlType.VarChar, 10).Value = this.txtNPI.Text;
                    command.Parameters.Add("MedicaidNumber", MySqlType.VarChar, 0x10).Value = this.txtMedicaidNumber.Text;
                    command.Parameters.Add("OtherID", MySqlType.VarChar, 0x10).Value = this.txtOtherID.Text;
                    command.Parameters.Add("FEDTaxID", MySqlType.VarChar, 9).Value = this.txtFEDTaxID.Text;
                    command.Parameters.Add("DEANumber", MySqlType.VarChar, 20).Value = this.txtDEANumber.Text;
                    command.Parameters.Add("PecosEnrolled", MySqlType.Bit).Value = this.chbPecosEnrolled.Checked;
                    command.Parameters.Add("Title", MySqlType.VarChar, 50).Value = this.txtTitle.Text;
                    command.Parameters.Add("TypeID", MySqlType.Int, 4).Value = this.cmbDoctorType.SelectedValue;
                    command.Parameters.Add("UPINNumber", MySqlType.VarChar, 11).Value = this.txtUPINNumber.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int, 4).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag2 = 0 < command.ExecuteUpdate("tbl_doctor", whereParameters);
                        flag2 ??= (0 < command.ExecuteInsert("tbl_doctor"));
                    }
                    else
                    {
                        flag2 = 0 < command.ExecuteInsert("tbl_doctor");
                        if (flag2)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                }
                try
                {
                    using (MySqlCommand command2 = new MySqlCommand("", connection))
                    {
                        command2.Parameters.Add("ID", MySqlType.Int, 4).Value = ID;
                        command2.ExecuteProcedure("mir_update_doctor");
                    }
                    using (MySqlCommand command3 = new MySqlCommand("", connection))
                    {
                        command3.CommandText = "SELECT MIR FROM tbl_doctor WHERE ID = :ID";
                        command3.Parameters.Add("ID", MySqlType.Int, 4).Value = ID;
                        this.F_MIR = NullableConvert.ToString(command3.ExecuteScalar());
                    }
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    TraceHelper.TraceException(ex);
                    ProjectData.ClearProjectError();
                }
                return flag2;
            }
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID,\r\n       LastName,\r\n       FirstName,\r\n       Address1,\r\n       City,\r\n       State,\r\n       UPINNumber\r\nFROM tbl_doctor\r\nORDER BY LastName, FirstName", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("LastName", "Last Name", 100);
            appearance.AddTextColumn("FirstName", "First Name", 100);
            appearance.AddTextColumn("Address1", "Address", 100);
            appearance.AddTextColumn("City", "City", 100);
            appearance.AddTextColumn("State", "State", 40);
            appearance.AddTextColumn("UPINNumber", "UPIN", 100);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__171-0 e$__- = new _Closure$__171-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            base.ProcessParameter_ID(Params);
            base.ProcessParameter_ShowMissing(Params);
        }

        protected override void ShowMissingInformation(bool Show)
        {
            if (Show)
            {
                this.MirHelper.ShowMissingInformation(base.MissingData, this.F_MIR);
            }
            else
            {
                this.MirHelper.ShowMissingInformation(base.MissingData, "");
            }
            this.ShowMissingInformation(Show, this.tpAddress);
            this.ShowMissingInformation(Show, this.tpNumbers);
            this.ShowMissingInformation(Show, this.tpMarketing);
        }

        private void ShowMissingInformation(bool Show, TabPage tabPage)
        {
            if (this.TabControl1.TabPages.IndexOf(tabPage) >= 0)
            {
                if (Show && (0 < Functions.EnumerateErrors(tabPage, base.MissingData)))
                {
                    tabPage.ImageIndex = 0;
                }
                else
                {
                    tabPage.ImageIndex = -1;
                }
            }
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            base.ValidationErrors.SetError(this.txtPhone, Functions.PhoneValidate(this.txtPhone.Text));
            base.ValidationErrors.SetError(this.txtPhone2, Functions.PhoneValidate(this.txtPhone2.Text));
            base.ValidationErrors.SetError(this.txtFax, Functions.PhoneValidate(this.txtFax.Text));
            base.ValidationErrors.SetError(this.txtNPI, Functions.NpiValidate(this.txtNPI.Text, false));
        }

        [field: AccessedThroughProperty("lblContact")]
        private Label lblContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDoctorType")]
        private Label lblDoctorType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFax")]
        private Label lblFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLicenseNumber")]
        private Label lblLicenseNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMedicaidNumber")]
        private Label lblMedicaidNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOtherID")]
        private Label lblOtherID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone")]
        private Label lblPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone2")]
        private Label lblPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTitle")]
        private Label lblTitle { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUPIN")]
        private Label lblUPIN { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtContact")]
        private TextBox txtContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLicenseNumber")]
        private TextBox txtLicenseNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMedicaidNumber")]
        private TextBox txtMedicaidNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOtherID")]
        private TextBox txtOtherID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTitle")]
        private TextBox txtTitle { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUPINNumber")]
        private TextBox txtUPINNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CName")]
        private ControlName CName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctorType")]
        private Combobox cmbDoctorType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLicenseExpired")]
        private Label lblLicenseExpired { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDEANumber")]
        private Label lblDEANumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDEANumber")]
        private TextBox txtDEANumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFEDTaxID")]
        private Label lblFEDTaxID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFEDTaxID")]
        private TextBox txtFEDTaxID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNPI")]
        private TextBox txtNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNPI")]
        private Label lblNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoImages")]
        private MenuItem mnuGotoImages { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoNewImage")]
        private MenuItem mnuGotoNewImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbLicenseExpired")]
        private UltraDateTimeEditor dtbLicenseExpired { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpAddress")]
        private TabPage tpAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpNumbers")]
        private TabPage tpNumbers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lnkPecosEnrolled")]
        private LinkLabel lnkPecosEnrolled { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPecosEnrolled")]
        private Label lblPecosEnrolled { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPecosEnrolled")]
        private CheckBox chbPecosEnrolled { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpMarketing")]
        private TabPage tpMarketing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ImageList1")]
        private ImageList ImageList1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private FormMirHelper MirHelper
        {
            get
            {
                if (this.F_MirHelper == null)
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.F_MirHelper.Add("FirstName", this.CName.txtFirstName, "First Name is required for invoice");
                    this.F_MirHelper.Add("LastName", this.CName.txtLastName, "Last Name is required for invoice");
                    this.F_MirHelper.Add("Address1", this.CAddress.txtAddress1, "Address-line-1 is required for invoice");
                    this.F_MirHelper.Add("City", this.CAddress.txtCity, "City is required for invoice");
                    this.F_MirHelper.Add("State", this.CAddress.txtState, "State is required for invoice");
                    this.F_MirHelper.Add("Zip", this.CAddress.txtZip, "Zip is required for invoice");
                    this.F_MirHelper.Add("UPINNumber", this.txtUPINNumber, "UPINNumber is required for invoice");
                    this.F_MirHelper.Add("NPI", this.txtNPI, "NPI is required for invoice");
                    this.F_MirHelper.Add("Phone", this.txtFax, "At least one phone number is required");
                    this.F_MirHelper.Add("Phone", this.txtPhone, "At least one phone number is required");
                    this.F_MirHelper.Add("Phone", this.txtPhone2, "At least one phone number is required");
                }
                return this.F_MirHelper;
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__171-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }

        private class pecos_entry
        {
            public string first_name { get; set; }

            public string last_name { get; set; }

            public string npi { get; set; }
        }
    }
}

