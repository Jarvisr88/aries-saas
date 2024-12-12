namespace DMEWorks.CMN
{
    using DMEWorks.Controls;
    using DMEWorks.Forms;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class Control_Header : Control_CMNBase
    {
        private IContainer components;

        public Control_Header()
        {
            this.InitializeComponent();
            this.eddICD9_1.NewButton = false;
            this.eddICD9_1.EditButton = false;
            this.eddICD9_2.NewButton = false;
            this.eddICD9_2.EditButton = false;
            this.eddICD9_3.NewButton = false;
            this.eddICD9_3.EditButton = false;
            this.eddICD9_4.NewButton = false;
            this.eddICD9_4.EditButton = false;
            Functions.AttachPhoneAutoInput(this.txtCompanyPhone);
            Functions.AttachPhoneAutoInput(this.txtCustomerPhone);
            Functions.AttachPhoneAutoInput(this.txtDoctorPhone);
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void cmbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void cmbPOSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dtbInitialDate_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void dtbRecertification_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void dtbRevisedDate_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void eddICD9_1_Changed(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void eddICD9_2_Changed(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void eddICD9_3_Changed(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void eddICD9_4_Changed(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Panel2 = new Panel();
            this.Panel5 = new Panel();
            this.Label46 = new Label();
            this.eddICD9_4 = new ExtendedDropdown();
            this.eddICD9_3 = new ExtendedDropdown();
            this.eddICD9_2 = new ExtendedDropdown();
            this.eddICD9_1 = new ExtendedDropdown();
            this.Panel4 = new Panel();
            this.nmbEstimatedLength = new NumericBox();
            this.Label41 = new Label();
            this.Label47 = new Label();
            this.Label19 = new Label();
            this.Panel9 = new Panel();
            this.pnlDoctor = new Panel();
            this.txtDoctorPhone = new TextBox();
            this.Label43 = new Label();
            this.txtDoctorAddress2 = new TextBox();
            this.txtDoctorAddress1 = new TextBox();
            this.txtDoctorZip = new TextBox();
            this.txtDoctorState = new TextBox();
            this.txtDoctorCity = new TextBox();
            this.Label42 = new Label();
            this.txtDoctorAccount = new TextBox();
            this.cmbDoctor = new Combobox();
            this.Label32 = new Label();
            this.Label30 = new Label();
            this.Label31 = new Label();
            this.Label29 = new Label();
            this.Label33 = new Label();
            this.pnlFacility = new Panel();
            this.Label20 = new Label();
            this.txtFacilityZip = new TextBox();
            this.Label24 = new Label();
            this.txtFacilityAddress1 = new TextBox();
            this.Label23 = new Label();
            this.txtFacilityAddress2 = new TextBox();
            this.Label22 = new Label();
            this.cmbFacility = new Combobox();
            this.Label21 = new Label();
            this.txtFacilityCity = new TextBox();
            this.txtFacilityState = new TextBox();
            this.Panel7 = new Panel();
            this.pnlCompany = new Panel();
            this.txtCompanyPhone = new TextBox();
            this.txtCompanyAccount = new TextBox();
            this.txtCompanyZip = new TextBox();
            this.txtCompanyState = new TextBox();
            this.Label17 = new Label();
            this.txtCompanyAddress1 = new TextBox();
            this.Label15 = new Label();
            this.txtCompanyAddress2 = new TextBox();
            this.Label14 = new Label();
            this.txtCompanyName = new TextBox();
            this.Label13 = new Label();
            this.Label16 = new Label();
            this.txtCompanyCity = new TextBox();
            this.Label12 = new Label();
            this.Label11 = new Label();
            this.pnlCustomer = new Panel();
            this.txtCustomerPhone = new TextBox();
            this.txtCustomerZip = new TextBox();
            this.txtCustomerState = new TextBox();
            this.txtCustomerCity = new TextBox();
            this.cmbCustomer = new Combobox();
            this.txtCustomerHICN = new TextBox();
            this.Label10 = new Label();
            this.Label9 = new Label();
            this.Label8 = new Label();
            this.txtCustomerAddress2 = new TextBox();
            this.Label7 = new Label();
            this.txtCustomerAddress1 = new TextBox();
            this.Label6 = new Label();
            this.Label5 = new Label();
            this.Label4 = new Label();
            this.Panel6 = new Panel();
            this.dtbRecertification = new UltraDateTimeEditor();
            this.Label40 = new Label();
            this.dtbRevisedDate = new UltraDateTimeEditor();
            this.Label3 = new Label();
            this.dtbInitialDate = new UltraDateTimeEditor();
            this.Label2 = new Label();
            this.Label1 = new Label();
            this.Label45 = new Label();
            this.Panel1 = new Panel();
            this.Panel12 = new Panel();
            this.txtCustomerGender = new TextBox();
            this.nmbCustomer_Weight = new NumericBox();
            this.nmbCustomer_Height = new NumericBox();
            this.Label28 = new Label();
            this.Label27 = new Label();
            this.Label26 = new Label();
            this.dtbCustomerDateofBirth = new UltraDateTimeEditor();
            this.Label25 = new Label();
            this.Panel14 = new Panel();
            this.cmbPOSType = new Combobox();
            this.Label18 = new Label();
            this.Panel2.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel9.SuspendLayout();
            this.pnlDoctor.SuspendLayout();
            this.pnlFacility.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.pnlCompany.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel12.SuspendLayout();
            this.Panel14.SuspendLayout();
            base.SuspendLayout();
            this.Panel2.Controls.Add(this.Panel5);
            this.Panel2.Controls.Add(this.Panel4);
            this.Panel2.Dock = DockStyle.Top;
            this.Panel2.Location = new Point(0, 0x165);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new Size(0x330, 0x1f);
            this.Panel2.TabIndex = 5;
            this.Panel5.BorderStyle = BorderStyle.FixedSingle;
            this.Panel5.Controls.Add(this.Label46);
            this.Panel5.Controls.Add(this.eddICD9_4);
            this.Panel5.Controls.Add(this.eddICD9_3);
            this.Panel5.Controls.Add(this.eddICD9_2);
            this.Panel5.Controls.Add(this.eddICD9_1);
            this.Panel5.Dock = DockStyle.Fill;
            this.Panel5.Location = new Point(0x180, 0);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new Size(0x1b0, 0x1f);
            this.Panel5.TabIndex = 1;
            this.Label46.BackColor = Color.Transparent;
            this.Label46.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label46.Location = new Point(0, 4);
            this.Label46.Name = "Label46";
            this.Label46.Size = new Size(0x60, 20);
            this.Label46.TabIndex = 0;
            this.Label46.Text = "Diagnostic codes";
            this.Label46.TextAlign = ContentAlignment.MiddleLeft;
            this.eddICD9_4.BorderStyle = BorderStyle.FixedSingle;
            this.eddICD9_4.EditButton = false;
            this.eddICD9_4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.eddICD9_4.Location = new Point(0x150, 4);
            this.eddICD9_4.Name = "eddICD9_4";
            this.eddICD9_4.NewButton = false;
            this.eddICD9_4.Size = new Size(0x48, 0x15);
            this.eddICD9_4.TabIndex = 4;
            this.eddICD9_4.TextMember = "Code";
            this.eddICD9_3.BorderStyle = BorderStyle.FixedSingle;
            this.eddICD9_3.EditButton = false;
            this.eddICD9_3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.eddICD9_3.Location = new Point(0x100, 4);
            this.eddICD9_3.Name = "eddICD9_3";
            this.eddICD9_3.NewButton = false;
            this.eddICD9_3.Size = new Size(0x48, 0x15);
            this.eddICD9_3.TabIndex = 3;
            this.eddICD9_3.TextMember = "Code";
            this.eddICD9_2.BorderStyle = BorderStyle.FixedSingle;
            this.eddICD9_2.EditButton = false;
            this.eddICD9_2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.eddICD9_2.Location = new Point(180, 4);
            this.eddICD9_2.Name = "eddICD9_2";
            this.eddICD9_2.NewButton = false;
            this.eddICD9_2.Size = new Size(0x48, 0x15);
            this.eddICD9_2.TabIndex = 2;
            this.eddICD9_2.TextMember = "Code";
            this.eddICD9_1.BorderStyle = BorderStyle.FixedSingle;
            this.eddICD9_1.EditButton = false;
            this.eddICD9_1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.eddICD9_1.Location = new Point(100, 4);
            this.eddICD9_1.Name = "eddICD9_1";
            this.eddICD9_1.NewButton = false;
            this.eddICD9_1.Size = new Size(0x48, 0x15);
            this.eddICD9_1.TabIndex = 1;
            this.eddICD9_1.TextMember = "Code";
            this.Panel4.BorderStyle = BorderStyle.FixedSingle;
            this.Panel4.Controls.Add(this.nmbEstimatedLength);
            this.Panel4.Controls.Add(this.Label41);
            this.Panel4.Controls.Add(this.Label47);
            this.Panel4.Dock = DockStyle.Left;
            this.Panel4.Location = new Point(0, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new Size(0x180, 0x1f);
            this.Panel4.TabIndex = 0;
            this.nmbEstimatedLength.BorderStyle = BorderStyle.FixedSingle;
            this.nmbEstimatedLength.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.nmbEstimatedLength.Location = new Point(0xe4, 4);
            this.nmbEstimatedLength.Name = "nmbEstimatedLength";
            this.nmbEstimatedLength.Size = new Size(40, 0x12);
            this.nmbEstimatedLength.TabIndex = 1;
            this.Label41.BackColor = Color.Transparent;
            this.Label41.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label41.Location = new Point(0x110, 4);
            this.Label41.Name = "Label41";
            this.Label41.Size = new Size(0x6c, 0x12);
            this.Label41.TabIndex = 2;
            this.Label41.Text = "1-99 (99=LIFETIME)";
            this.Label41.TextAlign = ContentAlignment.MiddleLeft;
            this.Label47.BackColor = Color.Transparent;
            this.Label47.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label47.Location = new Point(0, 4);
            this.Label47.Name = "Label47";
            this.Label47.Size = new Size(0xe0, 0x12);
            this.Label47.TabIndex = 0;
            this.Label47.Text = "EST. LENGTH OF NEED (# OF MONTHS):";
            this.Label47.TextAlign = ContentAlignment.MiddleLeft;
            this.Label19.BackColor = Color.Transparent;
            this.Label19.BorderStyle = BorderStyle.FixedSingle;
            this.Label19.Dock = DockStyle.Top;
            this.Label19.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label19.Location = new Point(0, 0x151);
            this.Label19.Name = "Label19";
            this.Label19.Size = new Size(0x330, 20);
            this.Label19.TabIndex = 4;
            this.Label19.Text = "SECTION B            Information in this Section May Not Be Completed by the Supplier of the Items/Supplies.";
            this.Label19.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel9.Controls.Add(this.pnlDoctor);
            this.Panel9.Controls.Add(this.pnlFacility);
            this.Panel9.Dock = DockStyle.Top;
            this.Panel9.Location = new Point(0, 0xc3);
            this.Panel9.Name = "Panel9";
            this.Panel9.Size = new Size(0x330, 0x8e);
            this.Panel9.TabIndex = 3;
            this.pnlDoctor.BorderStyle = BorderStyle.FixedSingle;
            this.pnlDoctor.Controls.Add(this.txtDoctorPhone);
            this.pnlDoctor.Controls.Add(this.Label43);
            this.pnlDoctor.Controls.Add(this.txtDoctorAddress2);
            this.pnlDoctor.Controls.Add(this.txtDoctorAddress1);
            this.pnlDoctor.Controls.Add(this.txtDoctorZip);
            this.pnlDoctor.Controls.Add(this.txtDoctorState);
            this.pnlDoctor.Controls.Add(this.txtDoctorCity);
            this.pnlDoctor.Controls.Add(this.Label42);
            this.pnlDoctor.Controls.Add(this.txtDoctorAccount);
            this.pnlDoctor.Controls.Add(this.cmbDoctor);
            this.pnlDoctor.Controls.Add(this.Label32);
            this.pnlDoctor.Controls.Add(this.Label30);
            this.pnlDoctor.Controls.Add(this.Label31);
            this.pnlDoctor.Controls.Add(this.Label29);
            this.pnlDoctor.Controls.Add(this.Label33);
            this.pnlDoctor.Dock = DockStyle.Fill;
            this.pnlDoctor.Location = new Point(0x180, 0);
            this.pnlDoctor.Name = "pnlDoctor";
            this.pnlDoctor.Size = new Size(0x1b0, 0x8e);
            this.pnlDoctor.TabIndex = 1;
            this.txtDoctorPhone.BackColor = SystemColors.Window;
            this.txtDoctorPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtDoctorPhone.Location = new Point(100, 0x74);
            this.txtDoctorPhone.Name = "txtDoctorPhone";
            this.txtDoctorPhone.ReadOnly = true;
            this.txtDoctorPhone.Size = new Size(140, 20);
            this.txtDoctorPhone.TabIndex = 12;
            this.Label43.BackColor = Color.Transparent;
            this.Label43.Dock = DockStyle.Top;
            this.Label43.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label43.Location = new Point(0, 0);
            this.Label43.Name = "Label43";
            this.Label43.Size = new Size(430, 0x10);
            this.Label43.TabIndex = 0;
            this.Label43.Text = "PHYSICIAN NAME, ADDRESS, TELEPHONE, and UPIN NUMBER  ";
            this.Label43.TextAlign = ContentAlignment.MiddleLeft;
            this.txtDoctorAddress2.BackColor = SystemColors.Window;
            this.txtDoctorAddress2.BorderStyle = BorderStyle.FixedSingle;
            this.txtDoctorAddress2.Location = new Point(100, 0x44);
            this.txtDoctorAddress2.Name = "txtDoctorAddress2";
            this.txtDoctorAddress2.ReadOnly = true;
            this.txtDoctorAddress2.Size = new Size(0x100, 20);
            this.txtDoctorAddress2.TabIndex = 6;
            this.txtDoctorAddress1.BackColor = SystemColors.Window;
            this.txtDoctorAddress1.BorderStyle = BorderStyle.FixedSingle;
            this.txtDoctorAddress1.Location = new Point(100, 0x2c);
            this.txtDoctorAddress1.Name = "txtDoctorAddress1";
            this.txtDoctorAddress1.ReadOnly = true;
            this.txtDoctorAddress1.Size = new Size(0x100, 20);
            this.txtDoctorAddress1.TabIndex = 4;
            this.txtDoctorZip.BackColor = SystemColors.Window;
            this.txtDoctorZip.BorderStyle = BorderStyle.FixedSingle;
            this.txtDoctorZip.Location = new Point(0x120, 0x5c);
            this.txtDoctorZip.Name = "txtDoctorZip";
            this.txtDoctorZip.ReadOnly = true;
            this.txtDoctorZip.Size = new Size(0x44, 20);
            this.txtDoctorZip.TabIndex = 10;
            this.txtDoctorState.BackColor = SystemColors.Window;
            this.txtDoctorState.BorderStyle = BorderStyle.FixedSingle;
            this.txtDoctorState.Location = new Point(0xf4, 0x5c);
            this.txtDoctorState.Name = "txtDoctorState";
            this.txtDoctorState.ReadOnly = true;
            this.txtDoctorState.Size = new Size(40, 20);
            this.txtDoctorState.TabIndex = 9;
            this.txtDoctorCity.BackColor = SystemColors.Window;
            this.txtDoctorCity.BorderStyle = BorderStyle.FixedSingle;
            this.txtDoctorCity.Location = new Point(100, 0x5c);
            this.txtDoctorCity.Name = "txtDoctorCity";
            this.txtDoctorCity.ReadOnly = true;
            this.txtDoctorCity.Size = new Size(140, 20);
            this.txtDoctorCity.TabIndex = 8;
            this.Label42.BackColor = Color.Transparent;
            this.Label42.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label42.Location = new Point(8, 0x5c);
            this.Label42.Name = "Label42";
            this.Label42.Size = new Size(0x58, 0x13);
            this.Label42.TabIndex = 7;
            this.Label42.Text = "City state zip";
            this.Label42.TextAlign = ContentAlignment.MiddleLeft;
            this.txtDoctorAccount.BackColor = SystemColors.Window;
            this.txtDoctorAccount.BorderStyle = BorderStyle.FixedSingle;
            this.txtDoctorAccount.Location = new Point(0x120, 0x74);
            this.txtDoctorAccount.Name = "txtDoctorAccount";
            this.txtDoctorAccount.ReadOnly = true;
            this.txtDoctorAccount.Size = new Size(0x44, 20);
            this.txtDoctorAccount.TabIndex = 14;
            this.cmbDoctor.Location = new Point(100, 20);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new Size(0x100, 0x15);
            this.cmbDoctor.TabIndex = 2;
            this.Label32.BackColor = Color.Transparent;
            this.Label32.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label32.Location = new Point(8, 0x74);
            this.Label32.Name = "Label32";
            this.Label32.Size = new Size(0x58, 0x13);
            this.Label32.TabIndex = 11;
            this.Label32.Text = "Phone";
            this.Label32.TextAlign = ContentAlignment.MiddleLeft;
            this.Label30.BackColor = Color.Transparent;
            this.Label30.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label30.Location = new Point(8, 0x2c);
            this.Label30.Name = "Label30";
            this.Label30.Size = new Size(0x58, 0x13);
            this.Label30.TabIndex = 3;
            this.Label30.Text = "Address1";
            this.Label30.TextAlign = ContentAlignment.MiddleLeft;
            this.Label31.BackColor = Color.Transparent;
            this.Label31.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label31.Location = new Point(8, 0x44);
            this.Label31.Name = "Label31";
            this.Label31.Size = new Size(0x58, 0x13);
            this.Label31.TabIndex = 5;
            this.Label31.Text = "Address2";
            this.Label31.TextAlign = ContentAlignment.MiddleLeft;
            this.Label29.BackColor = Color.Transparent;
            this.Label29.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label29.Location = new Point(8, 20);
            this.Label29.Name = "Label29";
            this.Label29.Size = new Size(0x58, 0x15);
            this.Label29.TabIndex = 1;
            this.Label29.Text = "Doctor name";
            this.Label29.TextAlign = ContentAlignment.MiddleLeft;
            this.Label33.BackColor = Color.Transparent;
            this.Label33.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label33.Location = new Point(0xf4, 0x74);
            this.Label33.Name = "Label33";
            this.Label33.Size = new Size(40, 20);
            this.Label33.TabIndex = 13;
            this.Label33.Text = "PIN";
            this.Label33.TextAlign = ContentAlignment.MiddleLeft;
            this.pnlFacility.BorderStyle = BorderStyle.FixedSingle;
            this.pnlFacility.Controls.Add(this.Label20);
            this.pnlFacility.Controls.Add(this.txtFacilityZip);
            this.pnlFacility.Controls.Add(this.Label24);
            this.pnlFacility.Controls.Add(this.txtFacilityAddress1);
            this.pnlFacility.Controls.Add(this.Label23);
            this.pnlFacility.Controls.Add(this.txtFacilityAddress2);
            this.pnlFacility.Controls.Add(this.Label22);
            this.pnlFacility.Controls.Add(this.cmbFacility);
            this.pnlFacility.Controls.Add(this.Label21);
            this.pnlFacility.Controls.Add(this.txtFacilityCity);
            this.pnlFacility.Controls.Add(this.txtFacilityState);
            this.pnlFacility.Dock = DockStyle.Left;
            this.pnlFacility.Location = new Point(0, 0);
            this.pnlFacility.Name = "pnlFacility";
            this.pnlFacility.Size = new Size(0x180, 0x8e);
            this.pnlFacility.TabIndex = 0;
            this.Label20.BackColor = Color.Transparent;
            this.Label20.Dock = DockStyle.Top;
            this.Label20.Font = new Font("Arial", 8.25f, FontStyle.Bold);
            this.Label20.Location = new Point(0, 0);
            this.Label20.Name = "Label20";
            this.Label20.Size = new Size(0x17e, 0x10);
            this.Label20.TabIndex = 0;
            this.Label20.Text = "NAME and ADDRESS of FACILITY if applicable (SeeReverse)";
            this.txtFacilityZip.BackColor = SystemColors.Window;
            this.txtFacilityZip.BorderStyle = BorderStyle.FixedSingle;
            this.txtFacilityZip.Location = new Point(0x120, 0x5c);
            this.txtFacilityZip.Name = "txtFacilityZip";
            this.txtFacilityZip.ReadOnly = true;
            this.txtFacilityZip.Size = new Size(0x44, 20);
            this.txtFacilityZip.TabIndex = 10;
            this.Label24.BackColor = Color.Transparent;
            this.Label24.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label24.Location = new Point(8, 0x5c);
            this.Label24.Name = "Label24";
            this.Label24.Size = new Size(0x58, 0x13);
            this.Label24.TabIndex = 7;
            this.Label24.Text = "City state zip";
            this.Label24.TextAlign = ContentAlignment.MiddleLeft;
            this.txtFacilityAddress1.BackColor = SystemColors.Window;
            this.txtFacilityAddress1.BorderStyle = BorderStyle.FixedSingle;
            this.txtFacilityAddress1.Location = new Point(100, 0x2c);
            this.txtFacilityAddress1.Name = "txtFacilityAddress1";
            this.txtFacilityAddress1.ReadOnly = true;
            this.txtFacilityAddress1.Size = new Size(0x100, 20);
            this.txtFacilityAddress1.TabIndex = 4;
            this.Label23.BackColor = Color.Transparent;
            this.Label23.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label23.Location = new Point(8, 0x44);
            this.Label23.Name = "Label23";
            this.Label23.Size = new Size(0x58, 0x13);
            this.Label23.TabIndex = 5;
            this.Label23.Text = "Address2";
            this.Label23.TextAlign = ContentAlignment.MiddleLeft;
            this.txtFacilityAddress2.BackColor = SystemColors.Window;
            this.txtFacilityAddress2.BorderStyle = BorderStyle.FixedSingle;
            this.txtFacilityAddress2.Location = new Point(100, 0x44);
            this.txtFacilityAddress2.Name = "txtFacilityAddress2";
            this.txtFacilityAddress2.ReadOnly = true;
            this.txtFacilityAddress2.Size = new Size(0x100, 20);
            this.txtFacilityAddress2.TabIndex = 6;
            this.Label22.BackColor = Color.Transparent;
            this.Label22.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label22.Location = new Point(8, 0x2c);
            this.Label22.Name = "Label22";
            this.Label22.Size = new Size(0x58, 0x13);
            this.Label22.TabIndex = 3;
            this.Label22.Text = "Address1";
            this.Label22.TextAlign = ContentAlignment.MiddleLeft;
            this.cmbFacility.Location = new Point(100, 20);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new Size(0x100, 0x15);
            this.cmbFacility.TabIndex = 2;
            this.Label21.BackColor = Color.Transparent;
            this.Label21.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label21.Location = new Point(8, 20);
            this.Label21.Name = "Label21";
            this.Label21.Size = new Size(0x58, 0x15);
            this.Label21.TabIndex = 1;
            this.Label21.Text = "Facility name";
            this.Label21.TextAlign = ContentAlignment.MiddleLeft;
            this.txtFacilityCity.BackColor = SystemColors.Window;
            this.txtFacilityCity.BorderStyle = BorderStyle.FixedSingle;
            this.txtFacilityCity.Location = new Point(100, 0x5c);
            this.txtFacilityCity.Name = "txtFacilityCity";
            this.txtFacilityCity.ReadOnly = true;
            this.txtFacilityCity.Size = new Size(140, 20);
            this.txtFacilityCity.TabIndex = 8;
            this.txtFacilityState.BackColor = SystemColors.Window;
            this.txtFacilityState.BorderStyle = BorderStyle.FixedSingle;
            this.txtFacilityState.Location = new Point(0xf4, 0x5c);
            this.txtFacilityState.Name = "txtFacilityState";
            this.txtFacilityState.ReadOnly = true;
            this.txtFacilityState.Size = new Size(40, 20);
            this.txtFacilityState.TabIndex = 9;
            this.Panel7.Controls.Add(this.pnlCompany);
            this.Panel7.Controls.Add(this.pnlCustomer);
            this.Panel7.Dock = DockStyle.Top;
            this.Panel7.Location = new Point(0, 0x15);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new Size(0x330, 0x8e);
            this.Panel7.TabIndex = 1;
            this.pnlCompany.BorderStyle = BorderStyle.FixedSingle;
            this.pnlCompany.Controls.Add(this.txtCompanyPhone);
            this.pnlCompany.Controls.Add(this.txtCompanyAccount);
            this.pnlCompany.Controls.Add(this.txtCompanyZip);
            this.pnlCompany.Controls.Add(this.txtCompanyState);
            this.pnlCompany.Controls.Add(this.Label17);
            this.pnlCompany.Controls.Add(this.txtCompanyAddress1);
            this.pnlCompany.Controls.Add(this.Label15);
            this.pnlCompany.Controls.Add(this.txtCompanyAddress2);
            this.pnlCompany.Controls.Add(this.Label14);
            this.pnlCompany.Controls.Add(this.txtCompanyName);
            this.pnlCompany.Controls.Add(this.Label13);
            this.pnlCompany.Controls.Add(this.Label16);
            this.pnlCompany.Controls.Add(this.txtCompanyCity);
            this.pnlCompany.Controls.Add(this.Label12);
            this.pnlCompany.Controls.Add(this.Label11);
            this.pnlCompany.Dock = DockStyle.Fill;
            this.pnlCompany.Location = new Point(0x180, 0);
            this.pnlCompany.Name = "pnlCompany";
            this.pnlCompany.Size = new Size(0x1b0, 0x8e);
            this.pnlCompany.TabIndex = 1;
            this.txtCompanyPhone.BackColor = SystemColors.Window;
            this.txtCompanyPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyPhone.Location = new Point(100, 0x74);
            this.txtCompanyPhone.Name = "txtCompanyPhone";
            this.txtCompanyPhone.ReadOnly = true;
            this.txtCompanyPhone.Size = new Size(140, 20);
            this.txtCompanyPhone.TabIndex = 12;
            this.txtCompanyAccount.BackColor = SystemColors.Window;
            this.txtCompanyAccount.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyAccount.Location = new Point(0x120, 0x74);
            this.txtCompanyAccount.Name = "txtCompanyAccount";
            this.txtCompanyAccount.ReadOnly = true;
            this.txtCompanyAccount.Size = new Size(0x44, 20);
            this.txtCompanyAccount.TabIndex = 14;
            this.txtCompanyZip.BackColor = SystemColors.Window;
            this.txtCompanyZip.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyZip.Location = new Point(0x120, 0x5c);
            this.txtCompanyZip.Name = "txtCompanyZip";
            this.txtCompanyZip.ReadOnly = true;
            this.txtCompanyZip.Size = new Size(0x44, 20);
            this.txtCompanyZip.TabIndex = 10;
            this.txtCompanyState.BackColor = SystemColors.Window;
            this.txtCompanyState.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyState.Location = new Point(0xf4, 0x5c);
            this.txtCompanyState.Name = "txtCompanyState";
            this.txtCompanyState.ReadOnly = true;
            this.txtCompanyState.Size = new Size(40, 20);
            this.txtCompanyState.TabIndex = 9;
            this.Label17.BackColor = Color.Transparent;
            this.Label17.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label17.Location = new Point(0xf4, 0x74);
            this.Label17.Name = "Label17";
            this.Label17.Size = new Size(40, 20);
            this.Label17.TabIndex = 13;
            this.Label17.Text = "NSC #";
            this.Label17.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCompanyAddress1.BackColor = SystemColors.Window;
            this.txtCompanyAddress1.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyAddress1.Location = new Point(100, 0x2c);
            this.txtCompanyAddress1.Name = "txtCompanyAddress1";
            this.txtCompanyAddress1.ReadOnly = true;
            this.txtCompanyAddress1.Size = new Size(0x100, 20);
            this.txtCompanyAddress1.TabIndex = 4;
            this.Label15.BackColor = Color.Transparent;
            this.Label15.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label15.Location = new Point(8, 0x5c);
            this.Label15.Name = "Label15";
            this.Label15.Size = new Size(0x58, 0x13);
            this.Label15.TabIndex = 7;
            this.Label15.Text = "City state zip";
            this.Label15.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCompanyAddress2.BackColor = SystemColors.Window;
            this.txtCompanyAddress2.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyAddress2.Location = new Point(100, 0x44);
            this.txtCompanyAddress2.Name = "txtCompanyAddress2";
            this.txtCompanyAddress2.ReadOnly = true;
            this.txtCompanyAddress2.Size = new Size(0x100, 20);
            this.txtCompanyAddress2.TabIndex = 6;
            this.Label14.BackColor = Color.Transparent;
            this.Label14.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label14.Location = new Point(8, 0x44);
            this.Label14.Name = "Label14";
            this.Label14.Size = new Size(0x58, 0x13);
            this.Label14.TabIndex = 5;
            this.Label14.Text = "Address2";
            this.Label14.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCompanyName.BackColor = SystemColors.Window;
            this.txtCompanyName.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyName.Location = new Point(100, 20);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.ReadOnly = true;
            this.txtCompanyName.Size = new Size(0x100, 20);
            this.txtCompanyName.TabIndex = 2;
            this.Label13.BackColor = Color.Transparent;
            this.Label13.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label13.Location = new Point(8, 0x2c);
            this.Label13.Name = "Label13";
            this.Label13.Size = new Size(0x58, 0x13);
            this.Label13.TabIndex = 3;
            this.Label13.Text = "Address1";
            this.Label13.TextAlign = ContentAlignment.MiddleLeft;
            this.Label16.BackColor = Color.Transparent;
            this.Label16.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label16.Location = new Point(8, 0x74);
            this.Label16.Name = "Label16";
            this.Label16.Size = new Size(0x58, 20);
            this.Label16.TabIndex = 11;
            this.Label16.Text = "Phone";
            this.Label16.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCompanyCity.BackColor = SystemColors.Window;
            this.txtCompanyCity.BorderStyle = BorderStyle.FixedSingle;
            this.txtCompanyCity.Location = new Point(100, 0x5c);
            this.txtCompanyCity.Name = "txtCompanyCity";
            this.txtCompanyCity.ReadOnly = true;
            this.txtCompanyCity.Size = new Size(140, 20);
            this.txtCompanyCity.TabIndex = 8;
            this.Label12.BackColor = Color.Transparent;
            this.Label12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label12.Location = new Point(8, 20);
            this.Label12.Name = "Label12";
            this.Label12.Size = new Size(0x58, 0x15);
            this.Label12.TabIndex = 1;
            this.Label12.Text = "Company name";
            this.Label12.TextAlign = ContentAlignment.MiddleLeft;
            this.Label11.BackColor = Color.Transparent;
            this.Label11.Dock = DockStyle.Top;
            this.Label11.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label11.Location = new Point(0, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new Size(430, 0x10);
            this.Label11.TabIndex = 0;
            this.Label11.Text = "Supplier name, address, telephone and NSC number";
            this.Label11.TextAlign = ContentAlignment.MiddleLeft;
            this.pnlCustomer.BorderStyle = BorderStyle.FixedSingle;
            this.pnlCustomer.Controls.Add(this.txtCustomerPhone);
            this.pnlCustomer.Controls.Add(this.txtCustomerZip);
            this.pnlCustomer.Controls.Add(this.txtCustomerState);
            this.pnlCustomer.Controls.Add(this.txtCustomerCity);
            this.pnlCustomer.Controls.Add(this.cmbCustomer);
            this.pnlCustomer.Controls.Add(this.txtCustomerHICN);
            this.pnlCustomer.Controls.Add(this.Label10);
            this.pnlCustomer.Controls.Add(this.Label9);
            this.pnlCustomer.Controls.Add(this.Label8);
            this.pnlCustomer.Controls.Add(this.txtCustomerAddress2);
            this.pnlCustomer.Controls.Add(this.Label7);
            this.pnlCustomer.Controls.Add(this.txtCustomerAddress1);
            this.pnlCustomer.Controls.Add(this.Label6);
            this.pnlCustomer.Controls.Add(this.Label5);
            this.pnlCustomer.Controls.Add(this.Label4);
            this.pnlCustomer.Dock = DockStyle.Left;
            this.pnlCustomer.Location = new Point(0, 0);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new Size(0x180, 0x8e);
            this.pnlCustomer.TabIndex = 0;
            this.txtCustomerPhone.BackColor = SystemColors.Window;
            this.txtCustomerPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerPhone.Location = new Point(100, 0x74);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.ReadOnly = true;
            this.txtCustomerPhone.Size = new Size(140, 20);
            this.txtCustomerPhone.TabIndex = 12;
            this.txtCustomerZip.BackColor = SystemColors.Window;
            this.txtCustomerZip.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerZip.Location = new Point(0x120, 0x5c);
            this.txtCustomerZip.Name = "txtCustomerZip";
            this.txtCustomerZip.ReadOnly = true;
            this.txtCustomerZip.Size = new Size(0x44, 20);
            this.txtCustomerZip.TabIndex = 10;
            this.txtCustomerState.BackColor = SystemColors.Window;
            this.txtCustomerState.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerState.Location = new Point(0xf4, 0x5c);
            this.txtCustomerState.Name = "txtCustomerState";
            this.txtCustomerState.ReadOnly = true;
            this.txtCustomerState.Size = new Size(40, 20);
            this.txtCustomerState.TabIndex = 9;
            this.txtCustomerCity.BackColor = SystemColors.Window;
            this.txtCustomerCity.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerCity.Location = new Point(100, 0x5c);
            this.txtCustomerCity.Name = "txtCustomerCity";
            this.txtCustomerCity.ReadOnly = true;
            this.txtCustomerCity.Size = new Size(140, 20);
            this.txtCustomerCity.TabIndex = 8;
            this.cmbCustomer.Location = new Point(100, 20);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0x100, 0x15);
            this.cmbCustomer.TabIndex = 2;
            this.txtCustomerHICN.BackColor = SystemColors.Window;
            this.txtCustomerHICN.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerHICN.Location = new Point(0x120, 0x74);
            this.txtCustomerHICN.Name = "txtCustomerHICN";
            this.txtCustomerHICN.ReadOnly = true;
            this.txtCustomerHICN.Size = new Size(0x44, 20);
            this.txtCustomerHICN.TabIndex = 14;
            this.Label10.BackColor = Color.Transparent;
            this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(0xf4, 0x74);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(40, 20);
            this.Label10.TabIndex = 13;
            this.Label10.Text = "HICN";
            this.Label10.TextAlign = ContentAlignment.MiddleLeft;
            this.Label9.BackColor = Color.Transparent;
            this.Label9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(8, 120);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x58, 20);
            this.Label9.TabIndex = 11;
            this.Label9.Text = "Phone";
            this.Label9.TextAlign = ContentAlignment.MiddleLeft;
            this.Label8.BackColor = Color.Transparent;
            this.Label8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label8.Location = new Point(8, 0x5c);
            this.Label8.Name = "Label8";
            this.Label8.Size = new Size(0x58, 0x13);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "City state zip";
            this.Label8.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCustomerAddress2.BackColor = SystemColors.Window;
            this.txtCustomerAddress2.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerAddress2.Location = new Point(100, 0x44);
            this.txtCustomerAddress2.Name = "txtCustomerAddress2";
            this.txtCustomerAddress2.ReadOnly = true;
            this.txtCustomerAddress2.Size = new Size(0x100, 20);
            this.txtCustomerAddress2.TabIndex = 6;
            this.Label7.BackColor = Color.Transparent;
            this.Label7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label7.Location = new Point(8, 0x44);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(0x58, 0x13);
            this.Label7.TabIndex = 5;
            this.Label7.Text = "Address2";
            this.Label7.TextAlign = ContentAlignment.MiddleLeft;
            this.txtCustomerAddress1.BackColor = SystemColors.Window;
            this.txtCustomerAddress1.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerAddress1.Location = new Point(100, 0x2c);
            this.txtCustomerAddress1.Name = "txtCustomerAddress1";
            this.txtCustomerAddress1.ReadOnly = true;
            this.txtCustomerAddress1.Size = new Size(0x100, 20);
            this.txtCustomerAddress1.TabIndex = 4;
            this.Label6.BackColor = Color.Transparent;
            this.Label6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label6.Location = new Point(8, 0x2c);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x58, 0x13);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "Address1";
            this.Label6.TextAlign = ContentAlignment.MiddleLeft;
            this.Label5.BackColor = Color.Transparent;
            this.Label5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label5.Location = new Point(8, 20);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x58, 0x15);
            this.Label5.TabIndex = 1;
            this.Label5.Text = "Customer name";
            this.Label5.TextAlign = ContentAlignment.MiddleLeft;
            this.Label4.BackColor = Color.Transparent;
            this.Label4.Dock = DockStyle.Top;
            this.Label4.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(0, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x17e, 0x10);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Patient name, address,  telephone and HIC number";
            this.Label4.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel6.BorderStyle = BorderStyle.FixedSingle;
            this.Panel6.Controls.Add(this.dtbRecertification);
            this.Panel6.Controls.Add(this.Label40);
            this.Panel6.Controls.Add(this.dtbRevisedDate);
            this.Panel6.Controls.Add(this.Label3);
            this.Panel6.Controls.Add(this.dtbInitialDate);
            this.Panel6.Controls.Add(this.Label2);
            this.Panel6.Controls.Add(this.Label1);
            this.Panel6.Controls.Add(this.Label45);
            this.Panel6.Dock = DockStyle.Top;
            this.Panel6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel6.Location = new Point(0, 0);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new Size(0x330, 0x15);
            this.Panel6.TabIndex = 0;
            this.dtbRecertification.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbRecertification.Location = new Point(0x278, 0);
            this.dtbRecertification.Name = "dtbRecertification";
            this.dtbRecertification.Size = new Size(0x5c, 0x13);
            this.dtbRecertification.TabIndex = 7;
            this.Label40.BackColor = Color.Transparent;
            this.Label40.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label40.Location = new Point(0x224, 0);
            this.Label40.Name = "Label40";
            this.Label40.Size = new Size(80, 0x12);
            this.Label40.TabIndex = 6;
            this.Label40.Text = "Re-certification";
            this.Label40.TextAlign = ContentAlignment.MiddleLeft;
            this.dtbRevisedDate.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbRevisedDate.Location = new Point(0x1c4, 0);
            this.dtbRevisedDate.Name = "dtbRevisedDate";
            this.dtbRevisedDate.Size = new Size(0x5c, 0x13);
            this.dtbRevisedDate.TabIndex = 5;
            this.Label3.BackColor = Color.Transparent;
            this.Label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label3.Location = new Point(0x18c, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x31, 0x12);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Revised";
            this.Label3.TextAlign = ContentAlignment.MiddleLeft;
            this.dtbInitialDate.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbInitialDate.Location = new Point(300, 0);
            this.dtbInitialDate.Name = "dtbInitialDate";
            this.dtbInitialDate.Size = new Size(0x5c, 0x13);
            this.dtbInitialDate.TabIndex = 3;
            this.Label2.BackColor = Color.Transparent;
            this.Label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label2.Location = new Point(0xf8, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x30, 0x12);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "INITIAL";
            this.Label2.TextAlign = ContentAlignment.MiddleLeft;
            this.Label1.BackColor = Color.Transparent;
            this.Label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label1.Location = new Point(0x60, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x90, 0x12);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Certifaction type/date:";
            this.Label1.TextAlign = ContentAlignment.MiddleLeft;
            this.Label45.BackColor = Color.Transparent;
            this.Label45.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label45.Location = new Point(0, 0);
            this.Label45.Name = "Label45";
            this.Label45.Size = new Size(0x5c, 0x12);
            this.Label45.TabIndex = 0;
            this.Label45.Text = "SECTION A";
            this.Label45.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel1.Controls.Add(this.Panel12);
            this.Panel1.Controls.Add(this.Panel14);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0xa3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x330, 0x20);
            this.Panel1.TabIndex = 2;
            this.Panel12.BorderStyle = BorderStyle.FixedSingle;
            this.Panel12.Controls.Add(this.txtCustomerGender);
            this.Panel12.Controls.Add(this.nmbCustomer_Weight);
            this.Panel12.Controls.Add(this.nmbCustomer_Height);
            this.Panel12.Controls.Add(this.Label28);
            this.Panel12.Controls.Add(this.Label27);
            this.Panel12.Controls.Add(this.Label26);
            this.Panel12.Controls.Add(this.dtbCustomerDateofBirth);
            this.Panel12.Controls.Add(this.Label25);
            this.Panel12.Dock = DockStyle.Fill;
            this.Panel12.Location = new Point(0x180, 0);
            this.Panel12.Name = "Panel12";
            this.Panel12.Size = new Size(0x1b0, 0x20);
            this.Panel12.TabIndex = 1;
            this.txtCustomerGender.BackColor = SystemColors.Window;
            this.txtCustomerGender.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomerGender.Location = new Point(200, 4);
            this.txtCustomerGender.Name = "txtCustomerGender";
            this.txtCustomerGender.ReadOnly = true;
            this.txtCustomerGender.Size = new Size(0x34, 20);
            this.txtCustomerGender.TabIndex = 3;
            this.nmbCustomer_Weight.BorderStyle = BorderStyle.FixedSingle;
            this.nmbCustomer_Weight.Location = new Point(0x174, 4);
            this.nmbCustomer_Weight.Name = "nmbCustomer_Weight";
            this.nmbCustomer_Weight.Size = new Size(40, 0x13);
            this.nmbCustomer_Weight.TabIndex = 7;
            this.nmbCustomer_Height.BorderStyle = BorderStyle.FixedSingle;
            this.nmbCustomer_Height.Location = new Point(0x124, 4);
            this.nmbCustomer_Height.Name = "nmbCustomer_Height";
            this.nmbCustomer_Height.Size = new Size(40, 0x13);
            this.nmbCustomer_Height.TabIndex = 5;
            this.Label28.BackColor = Color.Transparent;
            this.Label28.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label28.Location = new Point(340, 4);
            this.Label28.Name = "Label28";
            this.Label28.Size = new Size(0x1c, 0x13);
            this.Label28.TabIndex = 6;
            this.Label28.Text = "WT.";
            this.Label28.TextAlign = ContentAlignment.MiddleLeft;
            this.Label27.BackColor = Color.Transparent;
            this.Label27.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label27.Location = new Point(0x108, 4);
            this.Label27.Name = "Label27";
            this.Label27.Size = new Size(0x18, 0x13);
            this.Label27.TabIndex = 4;
            this.Label27.Text = "HT.";
            this.Label27.TextAlign = ContentAlignment.MiddleLeft;
            this.Label26.BackColor = Color.Transparent;
            this.Label26.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label26.Location = new Point(0x9c, 4);
            this.Label26.Name = "Label26";
            this.Label26.Size = new Size(0x2c, 0x13);
            this.Label26.TabIndex = 2;
            this.Label26.Text = "Gender";
            this.Label26.TextAlign = ContentAlignment.MiddleLeft;
            this.dtbCustomerDateofBirth.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbCustomerDateofBirth.Location = new Point(0x38, 4);
            this.dtbCustomerDateofBirth.Name = "dtbCustomerDateofBirth";
            this.dtbCustomerDateofBirth.Size = new Size(0x5c, 0x13);
            this.dtbCustomerDateofBirth.TabIndex = 1;
            this.Label25.BackColor = Color.Transparent;
            this.Label25.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label25.Location = new Point(4, 4);
            this.Label25.Name = "Label25";
            this.Label25.Size = new Size(0x30, 0x13);
            this.Label25.TabIndex = 0;
            this.Label25.Text = "PT DOB";
            this.Label25.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel14.BorderStyle = BorderStyle.FixedSingle;
            this.Panel14.Controls.Add(this.cmbPOSType);
            this.Panel14.Controls.Add(this.Label18);
            this.Panel14.Dock = DockStyle.Left;
            this.Panel14.Location = new Point(0, 0);
            this.Panel14.Name = "Panel14";
            this.Panel14.Size = new Size(0x180, 0x20);
            this.Panel14.TabIndex = 0;
            this.cmbPOSType.Location = new Point(100, 4);
            this.cmbPOSType.Name = "cmbPOSType";
            this.cmbPOSType.Size = new Size(0x100, 0x15);
            this.cmbPOSType.TabIndex = 1;
            this.Label18.BackColor = Color.Transparent;
            this.Label18.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label18.Location = new Point(8, 4);
            this.Label18.Name = "Label18";
            this.Label18.Size = new Size(0x58, 0x15);
            this.Label18.TabIndex = 0;
            this.Label18.Text = "Place of service";
            base.Controls.Add(this.Panel2);
            base.Controls.Add(this.Label19);
            base.Controls.Add(this.Panel9);
            base.Controls.Add(this.Panel1);
            base.Controls.Add(this.Panel7);
            base.Controls.Add(this.Panel6);
            base.Name = "Control_Header";
            base.Size = new Size(0x330, 0x184);
            this.Panel2.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel9.ResumeLayout(false);
            this.pnlDoctor.ResumeLayout(false);
            this.pnlDoctor.PerformLayout();
            this.pnlFacility.ResumeLayout(false);
            this.pnlFacility.PerformLayout();
            this.Panel7.ResumeLayout(false);
            this.pnlCompany.ResumeLayout(false);
            this.pnlCompany.PerformLayout();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel12.ResumeLayout(false);
            this.Panel12.PerformLayout();
            this.Panel14.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void nmbEstimatedLength_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("Panel2")]
        public virtual Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel5")]
        public virtual Panel Panel5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label46")]
        public virtual Label Label46 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_4")]
        public virtual ExtendedDropdown eddICD9_4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_3")]
        public virtual ExtendedDropdown eddICD9_3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_2")]
        public virtual ExtendedDropdown eddICD9_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_1")]
        public virtual ExtendedDropdown eddICD9_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel4")]
        public virtual Panel Panel4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbEstimatedLength")]
        public virtual NumericBox nmbEstimatedLength { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label41")]
        public virtual Label Label41 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label47")]
        public virtual Label Label47 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label19")]
        public virtual Label Label19 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel9")]
        public virtual Panel Panel9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlDoctor")]
        public virtual Panel pnlDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDoctorPhone")]
        public virtual TextBox txtDoctorPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label43")]
        public virtual Label Label43 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDoctorAddress2")]
        public virtual TextBox txtDoctorAddress2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDoctorAddress1")]
        public virtual TextBox txtDoctorAddress1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDoctorZip")]
        public virtual TextBox txtDoctorZip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDoctorState")]
        public virtual TextBox txtDoctorState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDoctorCity")]
        public virtual TextBox txtDoctorCity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label42")]
        public virtual Label Label42 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDoctorAccount")]
        public virtual TextBox txtDoctorAccount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctor")]
        public virtual Combobox cmbDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label32")]
        public virtual Label Label32 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label30")]
        public virtual Label Label30 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label31")]
        public virtual Label Label31 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label29")]
        public virtual Label Label29 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label33")]
        public virtual Label Label33 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlFacility")]
        public virtual Panel pnlFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label20")]
        public virtual Label Label20 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFacilityZip")]
        public virtual TextBox txtFacilityZip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label24")]
        public virtual Label Label24 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFacilityAddress1")]
        public virtual TextBox txtFacilityAddress1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label23")]
        public virtual Label Label23 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFacilityAddress2")]
        public virtual TextBox txtFacilityAddress2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label22")]
        public virtual Label Label22 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbFacility")]
        public virtual Combobox cmbFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label21")]
        public virtual Label Label21 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFacilityCity")]
        public virtual TextBox txtFacilityCity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFacilityState")]
        public virtual TextBox txtFacilityState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel7")]
        public virtual Panel Panel7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlCompany")]
        public virtual Panel pnlCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyPhone")]
        public virtual TextBox txtCompanyPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyAccount")]
        public virtual TextBox txtCompanyAccount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyZip")]
        public virtual TextBox txtCompanyZip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyState")]
        public virtual TextBox txtCompanyState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label17")]
        public virtual Label Label17 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyAddress1")]
        public virtual TextBox txtCompanyAddress1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label15")]
        public virtual Label Label15 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyAddress2")]
        public virtual TextBox txtCompanyAddress2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label14")]
        public virtual Label Label14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyName")]
        public virtual TextBox txtCompanyName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label13")]
        public virtual Label Label13 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label16")]
        public virtual Label Label16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyCity")]
        public virtual TextBox txtCompanyCity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label12")]
        public virtual Label Label12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label11")]
        public virtual Label Label11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlCustomer")]
        public virtual Panel pnlCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerPhone")]
        public virtual TextBox txtCustomerPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerZip")]
        public virtual TextBox txtCustomerZip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerState")]
        public virtual TextBox txtCustomerState { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerCity")]
        public virtual TextBox txtCustomerCity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        public virtual Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label10")]
        public virtual Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label9")]
        public virtual Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label8")]
        public virtual Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerAddress2")]
        public virtual TextBox txtCustomerAddress2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        public virtual Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerAddress1")]
        public virtual TextBox txtCustomerAddress1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label6")]
        public virtual Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        public virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        public virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel6")]
        public virtual Panel Panel6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbRecertification")]
        public virtual UltraDateTimeEditor dtbRecertification { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label40")]
        public virtual Label Label40 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbRevisedDate")]
        public virtual UltraDateTimeEditor dtbRevisedDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        public virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbInitialDate")]
        public virtual UltraDateTimeEditor dtbInitialDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        public virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        public virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        public virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel14")]
        public virtual Panel Panel14 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPOSType")]
        public virtual Combobox cmbPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label18")]
        public virtual Label Label18 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel12")]
        public virtual Panel Panel12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerGender")]
        public virtual TextBox txtCustomerGender { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCustomer_Weight")]
        public virtual NumericBox nmbCustomer_Weight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCustomer_Height")]
        public virtual NumericBox nmbCustomer_Height { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label28")]
        public virtual Label Label28 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label27")]
        public virtual Label Label27 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label26")]
        public virtual Label Label26 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbCustomerDateofBirth")]
        public virtual UltraDateTimeEditor dtbCustomerDateofBirth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label25")]
        public virtual Label Label25 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label45")]
        public virtual Label Label45 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerHICN")]
        public virtual TextBox txtCustomerHICN { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override FormMirHelper MirHelper
        {
            get
            {
                if (base.F_MirHelper == null)
                {
                    base.F_MirHelper = new FormMirHelper();
                    base.F_MirHelper.Add("InitialDate", this.dtbInitialDate, "Initial Date is the required field");
                    base.F_MirHelper.Add("CustomerID", this.cmbCustomer, "Customer is the required field");
                    base.F_MirHelper.Add("Customer", this.cmbCustomer, "Customer contains missing information");
                    base.F_MirHelper.Add("DoctorID", this.cmbDoctor, "Doctor is the required field");
                    base.F_MirHelper.Add("Doctor", this.cmbDoctor, "Doctor contains missing information");
                    base.F_MirHelper.Add("POSTypeID", this.cmbPOSType, "POSType is the required field");
                    base.F_MirHelper.Add("ICD9_1.Required", this.eddICD9_1, "Diagnosis code is required for invoice");
                    base.F_MirHelper.Add("ICD9_1.Unknown", this.eddICD9_1, "Diagnosis code #1 is unknown");
                    base.F_MirHelper.Add("ICD9_1.Inactive", this.eddICD9_1, "Diagnosis code #1 was not active at the date of form filling");
                    base.F_MirHelper.Add("ICD9_2.Unknown", this.eddICD9_2, "Diagnosis code #2 is unknown");
                    base.F_MirHelper.Add("ICD9_2.Inactive", this.eddICD9_2, "Diagnosis code #2 was not active at the date of form filling");
                    base.F_MirHelper.Add("ICD9_3.Unknown", this.eddICD9_3, "Diagnosis code #3 is unknown");
                    base.F_MirHelper.Add("ICD9_3.Inactive", this.eddICD9_3, "Diagnosis code #3 was not active at the date of form filling");
                    base.F_MirHelper.Add("ICD9_4.Unknown", this.eddICD9_4, "Diagnosis code #4 is unknown");
                    base.F_MirHelper.Add("ICD9_4.Inactive", this.eddICD9_4, "Diagnosis code #4 was not active at the date of form filling");
                    base.F_MirHelper.Add("EstimatedLengthOfNeed", this.nmbEstimatedLength, "Estimated Length of need is the required field");
                }
                return base.F_MirHelper;
            }
        }
    }
}

