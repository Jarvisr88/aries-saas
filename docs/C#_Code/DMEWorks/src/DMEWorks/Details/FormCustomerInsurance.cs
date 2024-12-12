namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormCustomerInsurance : FormDetails
    {
        private IContainer components;
        private string F_MIR;
        private FormMirHelper F_MirHelper;

        public FormCustomerInsurance(ControlCustomerInsurance Parent) : base(Parent)
        {
            this.InitializeComponent();
            this.SetControlState();
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtMobile);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.chbRequestEligibility);
            base.ChangesTracker.Subscribe(this.cmbInsuranceCompany);
            base.ChangesTracker.Subscribe(this.cmbRelationship);
            base.ChangesTracker.Subscribe(this.dtbDateofBirth);
            base.ChangesTracker.Subscribe(this.dtbInactiveDate);
            base.ChangesTracker.Subscribe(this.dtbRequestEligibilityOn);
            base.ChangesTracker.Subscribe(this.nmbPaymentPercent);
            base.ChangesTracker.Subscribe(this.txtEmployer);
            base.ChangesTracker.Subscribe(this.txtFirstName);
            base.ChangesTracker.Subscribe(this.txtGroupNumber);
            base.ChangesTracker.Subscribe(this.txtLastName);
            base.ChangesTracker.Subscribe(this.txtMiddleName);
            base.ChangesTracker.Subscribe(this.txtSuffix);
            base.ChangesTracker.Subscribe(this.txtMobile);
            base.ChangesTracker.Subscribe(this.txtPhone);
            base.ChangesTracker.Subscribe(this.txtPolicyNumber);
        }

        private void chbMissingInformation_CheckedChanged(object sender, EventArgs e)
        {
            this.InternalShowMissingInformation(this.chbMissingInformation.Checked);
        }

        protected override void Clear()
        {
            this.F_MIR = "";
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetComboBoxText(this.cmbBasis, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDateofBirth, DBNull.Value);
            Functions.SetComboBoxText(this.cmbGender, DBNull.Value);
            Functions.SetTextBoxText(this.txtGroupNumber, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbInactiveDate, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbInsuranceCompany, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbInsuranceType, DBNull.Value);
            Functions.SetTextBoxText(this.txtFirstName, DBNull.Value);
            Functions.SetTextBoxText(this.txtLastName, DBNull.Value);
            Functions.SetTextBoxText(this.txtMiddleName, DBNull.Value);
            Functions.SetTextBoxText(this.txtSuffix, DBNull.Value);
            Functions.SetTextBoxText(this.txtEmployer, DBNull.Value);
            Functions.SetTextBoxText(this.txtMobile, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbPaymentPercent, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtPolicyNumber, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbRelationship, "18");
            Functions.SetCheckBoxChecked(this.chbRequestEligibility, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbRequestEligibilityOn, DBNull.Value);
            this.SetControlState();
        }

        private void cmbInsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow selectedRow = this.cmbInsuranceCompany.SelectedRow;
                if (selectedRow != null)
                {
                    Functions.SetComboBoxText(this.cmbBasis, selectedRow["Basis"]);
                    Functions.SetNumericBoxValue(this.nmbPaymentPercent, selectedRow["Percent"]);
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

        private void cmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetControlState();
        }

        private void cmbRelationship_TextChanged(object sender, EventArgs e)
        {
            this.SetControlState();
        }

        protected override void Dispose(bool Disposing)
        {
            if (Disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(Disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormCustomerInsurance));
            this.cmbBasis = new ComboBox();
            this.cmbGender = new ComboBox();
            this.cmbRelationship = new ComboBox();
            this.txtGroupNumber = new TextBox();
            this.txtPolicyNumber = new TextBox();
            this.lblMobile = new Label();
            this.lblGender = new Label();
            this.lblEmployer = new Label();
            this.lblInactiveDate = new Label();
            this.lblDateofBirth = new Label();
            this.lblPhone = new Label();
            this.lblRelationship = new Label();
            this.lblBasis = new Label();
            this.lblPaymentPercent = new Label();
            this.lblGroupNumber = new Label();
            this.lblPolicyNumber = new Label();
            this.dtbDateofBirth = new UltraDateTimeEditor();
            this.dtbInactiveDate = new UltraDateTimeEditor();
            this.lblInsuranceCompany = new Label();
            this.nmbPaymentPercent = new NumericBox();
            this.CAddress = new ControlAddress();
            this.txtPhone = new TextBox();
            this.txtMobile = new TextBox();
            this.lblInsuranceType = new Label();
            this.cmbInsuranceCompany = new Combobox();
            this.cmbInsuranceType = new Combobox();
            this.MissingProvider = new ErrorProvider(this.components);
            this.chbMissingInformation = new CheckBox();
            this.txtEmployer = new TextBox();
            this.gbInsured = new GroupBox();
            this.lblRequestEligibilityOn = new Label();
            this.dtbRequestEligibilityOn = new UltraDateTimeEditor();
            this.chbRequestEligibility = new CheckBox();
            this.EligibilityValidationErrors = new ErrorProvider(this.components);
            this.txtSuffix = new TextBox();
            this.lblSuffix = new Label();
            this.txtLastName = new TextBox();
            this.lblLastName = new Label();
            this.txtMiddleName = new TextBox();
            this.txtFirstName = new TextBox();
            this.lblMiddleName = new Label();
            this.lblFirstName = new Label();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            ((ISupportInitialize) this.ValidationWarnings).BeginInit();
            ((ISupportInitialize) this.MissingProvider).BeginInit();
            this.gbInsured.SuspendLayout();
            ((ISupportInitialize) this.EligibilityValidationErrors).BeginInit();
            base.SuspendLayout();
            this.btnCancel.Location = new Point(0x11c, 0x1c8);
            this.btnCancel.TabIndex = 0x16;
            this.btnOK.Location = new Point(0xcc, 0x1c8);
            this.btnOK.TabIndex = 0x15;
            this.cmbBasis.Location = new Point(0x54, 400);
            this.cmbBasis.Name = "cmbBasis";
            this.cmbBasis.Size = new Size(0x60, 0x15);
            this.cmbBasis.TabIndex = 14;
            this.cmbGender.Location = new Point(0x10c, 0x5c);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new Size(0x60, 0x15);
            this.cmbGender.TabIndex = 13;
            this.cmbRelationship.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRelationship.Location = new Point(0x98, 0x5c);
            this.cmbRelationship.Name = "cmbRelationship";
            this.cmbRelationship.Size = new Size(0xa4, 0x15);
            this.cmbRelationship.TabIndex = 9;
            this.txtGroupNumber.Location = new Point(0xfc, 0x40);
            this.txtGroupNumber.Name = "txtGroupNumber";
            this.txtGroupNumber.Size = new Size(120, 20);
            this.txtGroupNumber.TabIndex = 7;
            this.txtPolicyNumber.AcceptsReturn = true;
            this.txtPolicyNumber.Location = new Point(80, 0x40);
            this.txtPolicyNumber.MaxLength = 0;
            this.txtPolicyNumber.Name = "txtPolicyNumber";
            this.txtPolicyNumber.Size = new Size(0x60, 20);
            this.txtPolicyNumber.TabIndex = 5;
            this.lblMobile.Location = new Point(8, 220);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new Size(0x48, 0x15);
            this.lblMobile.TabIndex = 0x11;
            this.lblMobile.Text = "Mobile";
            this.lblMobile.TextAlign = ContentAlignment.MiddleRight;
            this.lblGender.Location = new Point(0xd4, 0x5c);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new Size(0x31, 0x15);
            this.lblGender.TabIndex = 12;
            this.lblGender.Text = "Gender";
            this.lblGender.TextAlign = ContentAlignment.MiddleRight;
            this.lblEmployer.Location = new Point(8, 0x40);
            this.lblEmployer.Name = "lblEmployer";
            this.lblEmployer.RightToLeft = RightToLeft.No;
            this.lblEmployer.Size = new Size(0x48, 0x15);
            this.lblEmployer.TabIndex = 8;
            this.lblEmployer.Text = "Employer";
            this.lblEmployer.TextAlign = ContentAlignment.MiddleRight;
            this.lblInactiveDate.Location = new Point(8, 0x1ac);
            this.lblInactiveDate.Name = "lblInactiveDate";
            this.lblInactiveDate.Size = new Size(0x48, 0x15);
            this.lblInactiveDate.TabIndex = 15;
            this.lblInactiveDate.Text = "Inactive Date";
            this.lblInactiveDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblDateofBirth.Location = new Point(8, 0x5c);
            this.lblDateofBirth.Name = "lblDateofBirth";
            this.lblDateofBirth.Size = new Size(0x48, 0x15);
            this.lblDateofBirth.TabIndex = 10;
            this.lblDateofBirth.Text = "DOB";
            this.lblDateofBirth.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone.Location = new Point(8, 0xc4);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new Size(0x48, 0x15);
            this.lblPhone.TabIndex = 15;
            this.lblPhone.Text = "Phone";
            this.lblPhone.TextAlign = ContentAlignment.MiddleRight;
            this.lblRelationship.Location = new Point(4, 0x5c);
            this.lblRelationship.Name = "lblRelationship";
            this.lblRelationship.Size = new Size(140, 0x15);
            this.lblRelationship.TabIndex = 8;
            this.lblRelationship.Text = "Relationship to the Insured";
            this.lblRelationship.TextAlign = ContentAlignment.MiddleRight;
            this.lblBasis.Location = new Point(8, 400);
            this.lblBasis.Name = "lblBasis";
            this.lblBasis.Size = new Size(0x48, 0x15);
            this.lblBasis.TabIndex = 13;
            this.lblBasis.Text = "Basis";
            this.lblBasis.TextAlign = ContentAlignment.MiddleRight;
            this.lblPaymentPercent.Location = new Point(8, 0x174);
            this.lblPaymentPercent.Name = "lblPaymentPercent";
            this.lblPaymentPercent.Size = new Size(0x48, 0x15);
            this.lblPaymentPercent.TabIndex = 11;
            this.lblPaymentPercent.Text = "Payment %";
            this.lblPaymentPercent.TextAlign = ContentAlignment.MiddleRight;
            this.lblGroupNumber.Location = new Point(0xb8, 0x40);
            this.lblGroupNumber.Name = "lblGroupNumber";
            this.lblGroupNumber.Size = new Size(0x40, 0x15);
            this.lblGroupNumber.TabIndex = 6;
            this.lblGroupNumber.Text = "Group #";
            this.lblGroupNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblPolicyNumber.Location = new Point(4, 0x40);
            this.lblPolicyNumber.Name = "lblPolicyNumber";
            this.lblPolicyNumber.Size = new Size(0x48, 0x15);
            this.lblPolicyNumber.TabIndex = 4;
            this.lblPolicyNumber.Text = "Policy #";
            this.lblPolicyNumber.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDateofBirth.Location = new Point(0x54, 0x5c);
            this.dtbDateofBirth.Name = "dtbDateofBirth";
            this.dtbDateofBirth.Size = new Size(0x60, 0x15);
            this.dtbDateofBirth.TabIndex = 11;
            this.dtbInactiveDate.Location = new Point(0x54, 0x1ac);
            this.dtbInactiveDate.Name = "dtbInactiveDate";
            this.dtbInactiveDate.Size = new Size(0x60, 0x15);
            this.dtbInactiveDate.TabIndex = 0x10;
            this.lblInsuranceCompany.Location = new Point(4, 8);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new Size(0x48, 0x15);
            this.lblInsuranceCompany.TabIndex = 0;
            this.lblInsuranceCompany.Text = "Ins Company";
            this.lblInsuranceCompany.TextAlign = ContentAlignment.MiddleRight;
            this.nmbPaymentPercent.Location = new Point(0x54, 0x174);
            this.nmbPaymentPercent.Name = "nmbPaymentPercent";
            this.nmbPaymentPercent.Size = new Size(0x60, 0x15);
            this.nmbPaymentPercent.TabIndex = 12;
            this.CAddress.Location = new Point(12, 120);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x160, 0x48);
            this.CAddress.TabIndex = 14;
            this.txtPhone.Location = new Point(0x54, 0xc4);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(0xb0, 20);
            this.txtPhone.TabIndex = 0x10;
            this.txtMobile.Location = new Point(0x54, 220);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new Size(0xb0, 20);
            this.txtMobile.TabIndex = 0x12;
            this.lblInsuranceType.Location = new Point(4, 0x20);
            this.lblInsuranceType.Name = "lblInsuranceType";
            this.lblInsuranceType.Size = new Size(0x48, 0x15);
            this.lblInsuranceType.TabIndex = 2;
            this.lblInsuranceType.Text = "Ins Type";
            this.lblInsuranceType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbInsuranceCompany.Location = new Point(80, 8);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new Size(0x124, 0x15);
            this.cmbInsuranceCompany.TabIndex = 1;
            this.cmbInsuranceType.Location = new Point(80, 0x20);
            this.cmbInsuranceType.Name = "cmbInsuranceType";
            this.cmbInsuranceType.Size = new Size(0x124, 0x15);
            this.cmbInsuranceType.TabIndex = 3;
            this.MissingProvider.ContainerControl = this;
            this.MissingProvider.DataMember = "";
            this.MissingProvider.Icon = (Icon) manager.GetObject("MissingProvider.Icon");
            this.chbMissingInformation.CheckAlign = ContentAlignment.MiddleRight;
            this.chbMissingInformation.Location = new Point(0x84, 0x1c8);
            this.chbMissingInformation.Name = "chbMissingInformation";
            this.chbMissingInformation.Size = new Size(0x40, 0x18);
            this.chbMissingInformation.TabIndex = 20;
            this.chbMissingInformation.Text = "Missing";
            this.txtEmployer.Location = new Point(0x54, 0x40);
            this.txtEmployer.MaxLength = 0;
            this.txtEmployer.Name = "txtEmployer";
            this.txtEmployer.Size = new Size(280, 20);
            this.txtEmployer.TabIndex = 9;
            this.gbInsured.Controls.Add(this.txtSuffix);
            this.gbInsured.Controls.Add(this.lblSuffix);
            this.gbInsured.Controls.Add(this.txtLastName);
            this.gbInsured.Controls.Add(this.lblLastName);
            this.gbInsured.Controls.Add(this.txtMiddleName);
            this.gbInsured.Controls.Add(this.txtFirstName);
            this.gbInsured.Controls.Add(this.lblMiddleName);
            this.gbInsured.Controls.Add(this.lblFirstName);
            this.gbInsured.Controls.Add(this.cmbGender);
            this.gbInsured.Controls.Add(this.dtbDateofBirth);
            this.gbInsured.Controls.Add(this.txtEmployer);
            this.gbInsured.Controls.Add(this.lblGender);
            this.gbInsured.Controls.Add(this.lblEmployer);
            this.gbInsured.Controls.Add(this.lblDateofBirth);
            this.gbInsured.Controls.Add(this.CAddress);
            this.gbInsured.Controls.Add(this.txtPhone);
            this.gbInsured.Controls.Add(this.txtMobile);
            this.gbInsured.Controls.Add(this.lblPhone);
            this.gbInsured.Controls.Add(this.lblMobile);
            this.gbInsured.Location = new Point(4, 0x74);
            this.gbInsured.Name = "gbInsured";
            this.gbInsured.Size = new Size(0x174, 0xf8);
            this.gbInsured.TabIndex = 10;
            this.gbInsured.TabStop = false;
            this.lblRequestEligibilityOn.Location = new Point(0xc4, 400);
            this.lblRequestEligibilityOn.Name = "lblRequestEligibilityOn";
            this.lblRequestEligibilityOn.Size = new Size(0x84, 0x15);
            this.lblRequestEligibilityOn.TabIndex = 0x12;
            this.lblRequestEligibilityOn.Text = "Eligibility Requested On";
            this.lblRequestEligibilityOn.TextAlign = ContentAlignment.MiddleLeft;
            this.dtbRequestEligibilityOn.Location = new Point(0xc0, 0x1ac);
            this.dtbRequestEligibilityOn.Name = "dtbRequestEligibilityOn";
            this.dtbRequestEligibilityOn.Size = new Size(0x60, 0x15);
            this.dtbRequestEligibilityOn.TabIndex = 0x13;
            this.chbRequestEligibility.Location = new Point(0xc4, 0x174);
            this.chbRequestEligibility.Name = "chbRequestEligibility";
            this.chbRequestEligibility.Size = new Size(0xb0, 0x15);
            this.chbRequestEligibility.TabIndex = 0x11;
            this.chbRequestEligibility.Text = "Request Eligibility Information";
            this.EligibilityValidationErrors.ContainerControl = this;
            this.EligibilityValidationErrors.DataMember = "";
            this.txtSuffix.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.txtSuffix.Location = new Point(320, 0x24);
            this.txtSuffix.MaxLength = 4;
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new Size(0x2c, 20);
            this.txtSuffix.TabIndex = 7;
            this.lblSuffix.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblSuffix.Location = new Point(0x110, 0x24);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new Size(40, 0x15);
            this.lblSuffix.TabIndex = 6;
            this.lblSuffix.Text = "Suffix";
            this.lblSuffix.TextAlign = ContentAlignment.MiddleRight;
            this.txtLastName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtLastName.Location = new Point(0x30, 12);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new Size(0x13c, 20);
            this.txtLastName.TabIndex = 1;
            this.lblLastName.Location = new Point(8, 12);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new Size(0x20, 0x15);
            this.lblLastName.TabIndex = 0;
            this.lblLastName.Text = "Last";
            this.lblLastName.TextAlign = ContentAlignment.MiddleRight;
            this.txtMiddleName.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.txtMiddleName.Location = new Point(0xf8, 0x24);
            this.txtMiddleName.MaxLength = 1;
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new Size(0x18, 20);
            this.txtMiddleName.TabIndex = 5;
            this.txtFirstName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtFirstName.Location = new Point(0x30, 0x24);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new Size(0xa4, 20);
            this.txtFirstName.TabIndex = 3;
            this.lblMiddleName.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblMiddleName.Location = new Point(0xd8, 0x24);
            this.lblMiddleName.Name = "lblMiddleName";
            this.lblMiddleName.Size = new Size(0x18, 0x16);
            this.lblMiddleName.TabIndex = 4;
            this.lblMiddleName.Text = "MI";
            this.lblMiddleName.TextAlign = ContentAlignment.MiddleRight;
            this.lblFirstName.Location = new Point(8, 0x24);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new Size(0x20, 0x15);
            this.lblFirstName.TabIndex = 2;
            this.lblFirstName.Text = "First";
            this.lblFirstName.TextAlign = ContentAlignment.MiddleRight;
            base.AcceptButton = null;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = null;
            base.ClientSize = new Size(0x17e, 0x1ef);
            base.Controls.Add(this.chbRequestEligibility);
            base.Controls.Add(this.lblRequestEligibilityOn);
            base.Controls.Add(this.dtbRequestEligibilityOn);
            base.Controls.Add(this.gbInsured);
            base.Controls.Add(this.chbMissingInformation);
            base.Controls.Add(this.cmbInsuranceType);
            base.Controls.Add(this.cmbInsuranceCompany);
            base.Controls.Add(this.lblInsuranceType);
            base.Controls.Add(this.nmbPaymentPercent);
            base.Controls.Add(this.lblInsuranceCompany);
            base.Controls.Add(this.cmbBasis);
            base.Controls.Add(this.cmbRelationship);
            base.Controls.Add(this.txtGroupNumber);
            base.Controls.Add(this.txtPolicyNumber);
            base.Controls.Add(this.lblRelationship);
            base.Controls.Add(this.lblBasis);
            base.Controls.Add(this.lblPaymentPercent);
            base.Controls.Add(this.lblGroupNumber);
            base.Controls.Add(this.lblPolicyNumber);
            base.Controls.Add(this.lblInactiveDate);
            base.Controls.Add(this.dtbInactiveDate);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormCustomerInsurance";
            base.ShowInTaskbar = false;
            this.Text = "Policy Information";
            base.Controls.SetChildIndex(this.dtbInactiveDate, 0);
            base.Controls.SetChildIndex(this.lblInactiveDate, 0);
            base.Controls.SetChildIndex(this.lblPolicyNumber, 0);
            base.Controls.SetChildIndex(this.lblGroupNumber, 0);
            base.Controls.SetChildIndex(this.lblPaymentPercent, 0);
            base.Controls.SetChildIndex(this.lblBasis, 0);
            base.Controls.SetChildIndex(this.lblRelationship, 0);
            base.Controls.SetChildIndex(this.txtPolicyNumber, 0);
            base.Controls.SetChildIndex(this.txtGroupNumber, 0);
            base.Controls.SetChildIndex(this.cmbRelationship, 0);
            base.Controls.SetChildIndex(this.cmbBasis, 0);
            base.Controls.SetChildIndex(this.lblInsuranceCompany, 0);
            base.Controls.SetChildIndex(this.nmbPaymentPercent, 0);
            base.Controls.SetChildIndex(this.lblInsuranceType, 0);
            base.Controls.SetChildIndex(this.cmbInsuranceCompany, 0);
            base.Controls.SetChildIndex(this.cmbInsuranceType, 0);
            base.Controls.SetChildIndex(this.chbMissingInformation, 0);
            base.Controls.SetChildIndex(this.gbInsured, 0);
            base.Controls.SetChildIndex(this.dtbRequestEligibilityOn, 0);
            base.Controls.SetChildIndex(this.lblRequestEligibilityOn, 0);
            base.Controls.SetChildIndex(this.chbRequestEligibility, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            ((ISupportInitialize) this.ValidationWarnings).EndInit();
            ((ISupportInitialize) this.MissingProvider).EndInit();
            this.gbInsured.ResumeLayout(false);
            this.gbInsured.PerformLayout();
            ((ISupportInitialize) this.EligibilityValidationErrors).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void InternalShowMissingInformation(bool Show)
        {
            if (Show)
            {
                this.MirHelper.ShowMissingInformation(this.MissingProvider, this.F_MIR);
            }
            else
            {
                this.MirHelper.ShowMissingInformation(this.MissingProvider, "");
            }
        }

        public override void LoadComboBoxes()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_customer_insurance", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbBasis, table, "Basis");
                Functions.LoadComboBoxItems(this.cmbGender, table, "Gender");
            }
            Cache.InitDropdown(this.cmbInsuranceCompany, "tbl_insurancecompany", null);
            Cache.InitDropdown(this.cmbRelationship, "tbl_relationship", null);
            Cache.InitDropdown(this.cmbInsuranceType, "tbl_insurancetype", null);
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlCustomerInsurance.TableCustomerInsurance)
            {
                ControlCustomerInsurance.TableCustomerInsurance table = (ControlCustomerInsurance.TableCustomerInsurance) Row.Table;
                this.F_MIR = NullableConvert.ToString(Row[table.Col_MIR]);
                Functions.SetTextBoxText(this.CAddress.txtAddress1, Row[table.Col_Address1]);
                Functions.SetTextBoxText(this.CAddress.txtAddress2, Row[table.Col_Address2]);
                Functions.SetTextBoxText(this.CAddress.txtCity, Row[table.Col_City]);
                Functions.SetTextBoxText(this.CAddress.txtState, Row[table.Col_State]);
                Functions.SetTextBoxText(this.CAddress.txtZip, Row[table.Col_Zip]);
                Functions.SetComboBoxText(this.cmbBasis, Row[table.Col_Basis]);
                Functions.SetDateBoxValue(this.dtbDateofBirth, Row[table.Col_DateofBirth]);
                Functions.SetComboBoxText(this.cmbGender, Row[table.Col_Gender]);
                Functions.SetTextBoxText(this.txtGroupNumber, Row[table.Col_GroupNumber]);
                Functions.SetDateBoxValue(this.dtbInactiveDate, Row[table.Col_InactiveDate]);
                Functions.SetComboBoxValue(this.cmbInsuranceCompany, Row[table.Col_InsuranceCompanyID]);
                Functions.SetComboBoxValue(this.cmbInsuranceType, Row[table.Col_InsuranceType]);
                Functions.SetTextBoxText(this.txtFirstName, Row[table.Col_FirstName]);
                Functions.SetTextBoxText(this.txtLastName, Row[table.Col_LastName]);
                Functions.SetTextBoxText(this.txtMiddleName, Row[table.Col_MiddleName]);
                Functions.SetTextBoxText(this.txtSuffix, Row[table.Col_Suffix]);
                Functions.SetTextBoxText(this.txtEmployer, Row[table.Col_Employer]);
                Functions.SetTextBoxText(this.txtMobile, Row[table.Col_Mobile]);
                Functions.SetNumericBoxValue(this.nmbPaymentPercent, Row[table.Col_PaymentPercent]);
                Functions.SetTextBoxText(this.txtPhone, Row[table.Col_Phone]);
                Functions.SetTextBoxText(this.txtPolicyNumber, Row[table.Col_PolicyNumber]);
                Functions.SetComboBoxValue(this.cmbRelationship, Row[table.Col_RelationshipCode]);
                Functions.SetCheckBoxChecked(this.chbRequestEligibility, Row[table.Col_RequestEligibility]);
                Functions.SetDateBoxValue(this.dtbRequestEligibilityOn, Row[table.Col_RequestEligibilityOn]);
                this.SetControlState();
            }
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlCustomerInsurance.TableCustomerInsurance)
            {
                ControlCustomerInsurance.TableCustomerInsurance table = (ControlCustomerInsurance.TableCustomerInsurance) Row.Table;
                Row.BeginEdit();
                try
                {
                    Row[table.Col_Address1] = this.CAddress.txtAddress1.Text;
                    Row[table.Col_Address2] = this.CAddress.txtAddress2.Text;
                    Row[table.Col_City] = this.CAddress.txtCity.Text;
                    Row[table.Col_State] = this.CAddress.txtState.Text;
                    Row[table.Col_Zip] = this.CAddress.txtZip.Text;
                    Row[table.Col_Basis] = this.cmbBasis.Text;
                    Row[table.Col_DateofBirth] = Functions.GetDateBoxValue(this.dtbDateofBirth);
                    Row[table.Col_Gender] = this.cmbGender.Text;
                    Row[table.Col_GroupNumber] = this.txtGroupNumber.Text;
                    Row[table.Col_InactiveDate] = Functions.GetDateBoxValue(this.dtbInactiveDate);
                    Row[table.Col_InsuranceCompanyID] = this.cmbInsuranceCompany.SelectedValue;
                    Row[table.Col_InsuranceCompanyName] = this.cmbInsuranceCompany.Text;
                    Row[table.Col_InsuranceType] = this.cmbInsuranceType.SelectedValue;
                    Row[table.Col_InsuranceTypeName] = this.cmbInsuranceType.Text;
                    Row[table.Col_FirstName] = this.txtFirstName.Text;
                    Row[table.Col_LastName] = this.txtLastName.Text;
                    Row[table.Col_MiddleName] = this.txtMiddleName.Text;
                    Row[table.Col_Suffix] = this.txtSuffix.Text;
                    Row[table.Col_Employer] = this.txtEmployer.Text;
                    Row[table.Col_Mobile] = this.txtMobile.Text;
                    Row[table.Col_PaymentPercent] = this.nmbPaymentPercent.AsInt32.GetValueOrDefault(0);
                    Row[table.Col_Phone] = this.txtPhone.Text;
                    Row[table.Col_PolicyNumber] = this.txtPolicyNumber.Text;
                    Row[table.Col_RelationshipCode] = this.cmbRelationship.SelectedValue;
                    Row[table.Col_RequestEligibility] = this.chbRequestEligibility.Checked;
                    Row[table.Col_RequestEligibilityOn] = Functions.GetDateBoxValue(this.dtbRequestEligibilityOn);
                    Row.EndEdit();
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Row.CancelEdit();
                    throw;
                }
            }
        }

        private void SetControlState()
        {
            bool flag = string.Compare(this.cmbRelationship.Text, "Self", true) != 0;
            this.gbInsured.Enabled = flag;
            this.lblFirstName.Enabled = flag;
            this.txtFirstName.Enabled = flag;
            this.lblLastName.Enabled = flag;
            this.txtLastName.Enabled = flag;
            this.lblMiddleName.Enabled = flag;
            this.txtMiddleName.Enabled = flag;
            this.lblSuffix.Enabled = flag;
            this.txtSuffix.Enabled = flag;
            this.txtEmployer.Enabled = flag;
            this.lblDateofBirth.Enabled = flag;
            this.dtbDateofBirth.Enabled = flag;
            this.lblGender.Enabled = flag;
            this.cmbGender.Enabled = flag;
            this.CAddress.Enabled = flag;
            this.lblPhone.Enabled = flag;
            this.txtPhone.Enabled = flag;
            this.lblMobile.Enabled = flag;
            this.txtMobile.Enabled = flag;
        }

        public void ShowMissingInformation(bool Show)
        {
            this.chbMissingInformation.Checked = Show;
        }

        [IteratorStateMachine(typeof(VB$StateMachine_184_ValidateEligibility))]
        private IEnumerable<Tuple<Control, string>> ValidateEligibility()
        {
            VB$StateMachine_184_ValidateEligibility eligibility1 = new VB$StateMachine_184_ValidateEligibility(-2);
            eligibility1.$VB$Me = this;
            return eligibility1;
        }

        protected override void ValidateObject()
        {
            if (base.Visible)
            {
                this.ValidationErrors.SetError(this.txtPhone, Functions.PhoneValidate(this.txtPhone.Text));
                this.ValidationErrors.SetError(this.txtMobile, Functions.PhoneValidate(this.txtMobile.Text));
                if (!Versioned.IsNumeric(this.cmbInsuranceCompany.SelectedValue))
                {
                    this.ValidationErrors.SetError(this.cmbInsuranceCompany, "You must select insurance company");
                }
                else
                {
                    this.ValidationErrors.SetError(this.cmbInsuranceCompany, "");
                }
                if (!(this.cmbInsuranceType.SelectedValue is string))
                {
                    this.ValidationErrors.SetError(this.cmbInsuranceType, "You must select insurance type");
                }
                else
                {
                    this.ValidationErrors.SetError(this.cmbInsuranceType, "");
                }
            }
        }

        [field: AccessedThroughProperty("cmbBasis")]
        private ComboBox cmbBasis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbGender")]
        private ComboBox cmbGender { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbInactiveDate")]
        private UltraDateTimeEditor dtbInactiveDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBasis")]
        private Label lblBasis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblGender")]
        private Label lblGender { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblGroupNumber")]
        private Label lblGroupNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInactiveDate")]
        private Label lblInactiveDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceCompany")]
        private Label lblInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMobile")]
        private Label lblMobile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPaymentPercent")]
        private Label lblPaymentPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone")]
        private Label lblPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPolicyNumber")]
        private Label lblPolicyNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblRelationship")]
        private Label lblRelationship { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPaymentPercent")]
        private NumericBox nmbPaymentPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtGroupNumber")]
        private TextBox txtGroupNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPolicyNumber")]
        private TextBox txtPolicyNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMobile")]
        private TextBox txtMobile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceType")]
        private Label lblInsuranceType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInsuranceCompany")]
        private Combobox cmbInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDateofBirth")]
        private UltraDateTimeEditor dtbDateofBirth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDateofBirth")]
        private Label lblDateofBirth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInsuranceType")]
        private Combobox cmbInsuranceType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("MissingProvider")]
        private ErrorProvider MissingProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbMissingInformation")]
        private CheckBox chbMissingInformation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbRelationship")]
        private ComboBox cmbRelationship { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEmployer")]
        private Label lblEmployer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEmployer")]
        private TextBox txtEmployer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbInsured")]
        private GroupBox gbInsured { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblRequestEligibilityOn")]
        private Label lblRequestEligibilityOn { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbRequestEligibilityOn")]
        private UltraDateTimeEditor dtbRequestEligibilityOn { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbRequestEligibility")]
        private CheckBox chbRequestEligibility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("EligibilityValidationErrors")]
        private ErrorProvider EligibilityValidationErrors { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSuffix")]
        public virtual TextBox txtSuffix { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSuffix")]
        public virtual Label lblSuffix { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLastName")]
        public virtual TextBox txtLastName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLastName")]
        public virtual Label lblLastName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMiddleName")]
        public virtual TextBox txtMiddleName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFirstName")]
        public virtual TextBox txtFirstName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMiddleName")]
        public virtual Label lblMiddleName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFirstName")]
        public virtual Label lblFirstName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override AllowStateEnum AllowState
        {
            get => 
                base.AllowState;
            set
            {
                base.AllowState = value;
                bool flag = (value & AllowStateEnum.AllowEdit) != AllowStateEnum.AllowNone;
                this.btnOK.Enabled = flag | ((value & AllowStateEnum.AllowNew) == AllowStateEnum.AllowNew);
            }
        }

        public FormMirHelper MirHelper
        {
            get
            {
                if (this.F_MirHelper == null)
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.F_MirHelper.Add("FirstName", this.txtFirstName, "First Name is required for invoice");
                    this.F_MirHelper.Add("LastName", this.txtLastName, "Last Name is required for invoice");
                    this.F_MirHelper.Add("Address1", this.CAddress.txtAddress1, "Address-line-1 is required for invoice");
                    this.F_MirHelper.Add("City", this.CAddress.txtCity, "City is required for invoice");
                    this.F_MirHelper.Add("State", this.CAddress.txtState, "State is required for invoice");
                    this.F_MirHelper.Add("Zip", this.CAddress.txtZip, "Zip is required for invoice");
                    this.F_MirHelper.Add("Gender", this.cmbGender, "Gender is required for invoice");
                    this.F_MirHelper.Add("DateofBirth", this.dtbDateofBirth, "Date of Birth is required for invoice");
                    this.F_MirHelper.Add("InsuranceCompanyID", this.cmbInsuranceCompany, "Insurance Company is required for invoice");
                    this.F_MirHelper.Add("InsuranceCompany", this.cmbInsuranceCompany, "Insurance Company contains missing information");
                    this.F_MirHelper.Add("InsuranceType", this.cmbInsuranceType, "Insurance Type is required for invoice");
                    this.F_MirHelper.Add("PolicyNumber", this.txtPolicyNumber, "Policy Number is required for invoice");
                    this.F_MirHelper.Add("RelationshipCode", this.cmbRelationship, "Relationship To Insured is required for invoice");
                }
                return this.F_MirHelper;
            }
        }

        [CompilerGenerated]
        private sealed class VB$StateMachine_184_ValidateEligibility : IEnumerable<Tuple<Control, string>>, IEnumerable, IEnumerator<Tuple<Control, string>>, IDisposable, IEnumerator
        {
            public int $State;
            public Tuple<Control, string> $Current;
            public int $InitialThreadId;
            internal FormCustomerInsurance $VB$Me;
            internal MySqlDataReader $VB$ResumableLocal_reader$0;
            internal MySqlCommand $VB$ResumableLocal_cmd$1;
            internal MySqlConnection $VB$ResumableLocal_cnn$2;

            public VB$StateMachine_184_ValidateEligibility(int $State)
            {
                this.$State = $State;
                this.$InitialThreadId = Environment.CurrentManagedThreadId;
            }

            private void Dispose()
            {
                this.$State = (this.$State != 1) ? -1 : 2;
                this.MoveNext();
            }

            private IEnumerator<Tuple<Control, string>> GetEnumerator()
            {
                FormCustomerInsurance.VB$StateMachine_184_ValidateEligibility eligibility;
                if ((this.$State == -2) && (this.$InitialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.$State = 0;
                    eligibility = this;
                }
                else
                {
                    eligibility = new FormCustomerInsurance.VB$StateMachine_184_ValidateEligibility(0) {
                        $VB$Me = this.$VB$Me
                    };
                }
                return eligibility;
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            [CompilerGenerated]
            private bool MoveNext()
            {
                int num = this.$State;
                if (num == 0)
                {
                    this.$State = num = -1;
                    if (!this.$VB$Me.chbRequestEligibility.Checked || (NullableConvert.ToInt32(this.$VB$Me.cmbInsuranceCompany.SelectedValue) == null))
                    {
                        goto TR_0001;
                    }
                }
                else if ((num - 1) > 1)
                {
                    return false;
                }
                try
                {
                    if ((num - 1) > 1)
                    {
                        this.$VB$ResumableLocal_cnn$2 = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql);
                    }
                    try
                    {
                        if ((num - 1) > 1)
                        {
                            this.$VB$ResumableLocal_cnn$2.Open();
                            this.$VB$ResumableLocal_cmd$1 = new MySqlCommand("", this.$VB$ResumableLocal_cnn$2);
                        }
                        try
                        {
                            if ((num - 1) > 1)
                            {
                                int? nullable;
                                this.$VB$ResumableLocal_cmd$1.CommandText = "SELECT aep.SearchOptions\r\nFROM tbl_insurancecompany as ic\r\n     INNER JOIN tbl_ability_eligibility_payer as aep ON aep.Id = ic.AbilityEligibilityPayerId\r\nWHERE ic.Id = :InsuranceCompanyId";
                                this.$VB$ResumableLocal_cmd$1.Parameters.Add("InsuranceCompanyId", MySqlType.Int).Value = nullable;
                                this.$VB$ResumableLocal_reader$0 = this.$VB$ResumableLocal_cmd$1.ExecuteReader();
                            }
                            try
                            {
                                if (num == 1)
                                {
                                    this.$State = num = -1;
                                }
                                else if (num != 2)
                                {
                                    if (!this.$VB$ResumableLocal_reader$0.Read())
                                    {
                                        this.$Current = new Tuple<Control, string>(this.$VB$Me.cmbInsuranceCompany, "Insurance company must have Ability Eligibility Payer Assigned");
                                        this.$State = num = 1;
                                        return true;
                                    }
                                }
                                else
                                {
                                    this.$State = num = -1;
                                    return true;
                                }
                            }
                            finally
                            {
                                if ((num < 0) && (this.$VB$ResumableLocal_reader$0 != null))
                                {
                                    ((IDisposable) this.$VB$ResumableLocal_reader$0).Dispose();
                                }
                            }
                        }
                        finally
                        {
                            if ((num < 0) && (this.$VB$ResumableLocal_cmd$1 != null))
                            {
                                this.$VB$ResumableLocal_cmd$1.Dispose();
                            }
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (this.$VB$ResumableLocal_cnn$2 != null))
                        {
                            this.$VB$ResumableLocal_cnn$2.Dispose();
                        }
                    }
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.$VB$Me.ShowException(exception);
                    ProjectData.ClearProjectError();
                }
            TR_0001:
                return false;
            }

            private void Reset()
            {
                throw new NotSupportedException();
            }

            private Tuple<Control, string> Current =>
                this.$Current;

            object IEnumerator.Current =>
                this.$Current;
        }
    }
}

