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
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonMissing=true)]
    public class FormInsuranceCompany : FormAutoIncrementMaintain
    {
        private IContainer components;
        private string F_MIR;
        private FormMirHelper F_MirHelper;

        public FormInsuranceCompany()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_insurancecompany", "tbl_insurancecompanygroup" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.CAddress);
            base.ChangesTracker.Subscribe(this.chbPrintHAOOnInvoice);
            base.ChangesTracker.Subscribe(this.chbPrintInvOnInvoice);
            base.ChangesTracker.Subscribe(this.chbParticipatingProvider);
            base.ChangesTracker.Subscribe(this.chbExtractOrderingPhysician);
            base.ChangesTracker.Subscribe(this.chbExtractReferringPhysician);
            base.ChangesTracker.Subscribe(this.chbExtractRenderingProvider);
            base.ChangesTracker.Subscribe(this.cmbAbilityEligibilityPayer);
            base.ChangesTracker.Subscribe(this.cmbBasis);
            base.ChangesTracker.Subscribe(this.cmbECSFormat);
            base.ChangesTracker.Subscribe(this.cmbInvoiceForm);
            base.ChangesTracker.Subscribe(this.cmbGroup);
            base.ChangesTracker.Subscribe(this.cmbPriceCode);
            base.ChangesTracker.Subscribe(this.cmbType);
            base.ChangesTracker.Subscribe(this.nmbExpectedPercent);
            base.ChangesTracker.Subscribe(this.txtAbilityNumber);
            base.ChangesTracker.Subscribe(this.txtAvailityNumber);
            base.ChangesTracker.Subscribe(this.txtClaimMdNumber);
            base.ChangesTracker.Subscribe(this.txtContact);
            base.ChangesTracker.Subscribe(this.txtFax);
            base.ChangesTracker.Subscribe(this.txtMedicaidNumber);
            base.ChangesTracker.Subscribe(this.txtMedicareNumber);
            base.ChangesTracker.Subscribe(this.txtMedicareNumber);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtOfficeAllyNumber);
            base.ChangesTracker.Subscribe(this.txtPhone);
            base.ChangesTracker.Subscribe(this.txtPhone2);
            base.ChangesTracker.Subscribe(this.txtTitle);
            base.ChangesTracker.Subscribe(this.txtZirmedNumber);
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtFax);
            Functions.AttachPhoneAutoInput(this.txtPhone2);
        }

        private void ChbAppendPrefixToTaxonomyCode_CheckedChanged(object sender, EventArgs e)
        {
            this.lblTaxonomyCodePrefix.Visible = this.chbAppendPrefixToTaxonomyCode.Checked;
            this.txtTaxonomyCodePrefix.Visible = this.chbAppendPrefixToTaxonomyCode.Checked;
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.F_MIR = "";
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtFax, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone2, DBNull.Value);
            Functions.SetTextBoxText(this.txtContact, DBNull.Value);
            Functions.SetTextBoxText(this.txtTitle, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPriceCode, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbInvoiceForm, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbExpectedPercent, DBNull.Value);
            Functions.SetComboBoxText(this.cmbBasis, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrintInvOnInvoice, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrintHAOOnInvoice, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbParticipatingProvider, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbExtractOrderingPhysician, true);
            Functions.SetCheckBoxChecked(this.chbExtractReferringPhysician, false);
            Functions.SetCheckBoxChecked(this.chbExtractRenderingProvider, false);
            Functions.SetTextBoxText(this.txtTaxonomyCodePrefix, DBNull.Value);
            this.chbAppendPrefixToTaxonomyCode.Checked = !string.IsNullOrWhiteSpace(this.txtTaxonomyCodePrefix.Text);
            Functions.SetComboBoxValue(this.cmbType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbGroup, DBNull.Value);
            Functions.SetComboBoxText(this.cmbECSFormat, DBNull.Value);
            Functions.SetTextBoxText(this.txtAbilityNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtAvailityNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtMedicaidNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtMedicareNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtOfficeAllyNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtZirmedNumber, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbAbilityEligibilityPayer, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_insurancecompany"))
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
            messages.ConfirmDeleting = $"Are you really want to delete insurance company '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Insurance company '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_insurancecompany", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbECSFormat, table, "ECSFormat");
                Functions.LoadComboBoxItems(this.cmbBasis, table, "Basis");
            }
            Cache.InitDropdown(this.cmbPriceCode, "tbl_pricecode", null);
            Cache.InitDropdown(this.cmbGroup, "tbl_insurancecompanygroup", null);
            Cache.InitDropdown(this.cmbType, "tbl_insurancecompanytype", null);
            Cache.InitDropdown(this.cmbInvoiceForm, "tbl_invoiceform", null);
            Cache.InitDropdown(this.cmbAbilityEligibilityPayer, "tbl_ability_eligibility_payer", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormInsuranceCompany));
            this.txtName = new TextBox();
            this.lblName = new Label();
            this.txtTitle = new TextBox();
            this.txtContact = new TextBox();
            this.lblTitle = new Label();
            this.lblContact = new Label();
            this.lblFax = new Label();
            this.lblPhone2 = new Label();
            this.lblPhone1 = new Label();
            this.chbPrintHAOOnInvoice = new CheckBox();
            this.chbPrintInvOnInvoice = new CheckBox();
            this.txtMedicareNumber = new TextBox();
            this.cmbBasis = new ComboBox();
            this.lblMedicareNumber = new Label();
            this.lblExpPer = new Label();
            this.lblPriceCode = new Label();
            this.cmbECSFormat = new ComboBox();
            this.cmbType = new ComboBox();
            this.lblEcsFormat = new Label();
            this.lblType = new Label();
            this.nmbExpectedPercent = new NumericBox();
            this.CAddress = new ControlAddress();
            this.cmbPriceCode = new Combobox();
            this.txtPhone2 = new TextBox();
            this.txtPhone = new TextBox();
            this.txtFax = new TextBox();
            this.txtZirmedNumber = new TextBox();
            this.lblZirmedNumber = new Label();
            this.cmbInvoiceForm = new Combobox();
            this.lblInvoiceForm = new Label();
            this.txtMedicaidNumber = new TextBox();
            this.lblMedicaidNumber = new Label();
            this.miGotoEligibility = new MenuItem();
            this.PageControl2 = new TabControl();
            this.tpContact = new TabPage();
            this.tpBilling = new TabPage();
            this.cmbGroup = new Combobox();
            this.lblGroup = new Label();
            this.tpEdi = new TabPage();
            this.txtClaimMdNumber = new TextBox();
            this.lblAvailityNumber = new Label();
            this.lblClaimMdNumber = new Label();
            this.txtAvailityNumber = new TextBox();
            this.txtOfficeAllyNumber = new TextBox();
            this.txtAbilityNumber = new TextBox();
            this.lblOfficeAllyNumber = new Label();
            this.lblAbilityNumber = new Label();
            this.tp837 = new TabPage();
            this.lblTaxonomyCodePrefix = new Label();
            this.txtTaxonomyCodePrefix = new TextBox();
            this.chbAppendPrefixToTaxonomyCode = new CheckBox();
            this.chbExtractRenderingProvider = new CheckBox();
            this.chbParticipatingProvider = new CheckBox();
            this.chbExtractOrderingPhysician = new CheckBox();
            this.chbExtractReferringPhysician = new CheckBox();
            this.tpEligibility = new TabPage();
            this.cmbAbilityEligibilityPayer = new Combobox();
            this.lblAbilityEligibilityPayer = new Label();
            this.ImageList1 = new ImageList(this.components);
            this.Panel1 = new Panel();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            ((ISupportInitialize) base.MissingData).BeginInit();
            this.PageControl2.SuspendLayout();
            this.tpContact.SuspendLayout();
            this.tpBilling.SuspendLayout();
            this.tpEdi.SuspendLayout();
            this.tp837.SuspendLayout();
            this.tpEligibility.SuspendLayout();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.PageControl2);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x180, 0x117);
            base.tpWorkArea.Visible = true;
            MenuItem[] items = new MenuItem[] { this.miGotoEligibility };
            base.cmnuGoto.MenuItems.AddRange(items);
            this.txtName.Location = new Point(0x60, 8);
            this.txtName.MaxLength = 0;
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 20);
            this.txtName.TabIndex = 1;
            this.lblName.Location = new Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(80, 0x16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.txtTitle.Location = new Point(0x110, 0x9f);
            this.txtTitle.MaxLength = 0;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(0x38, 20);
            this.txtTitle.TabIndex = 10;
            this.txtContact.Location = new Point(0x58, 0x9f);
            this.txtContact.MaxLength = 0;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new Size(0x90, 20);
            this.txtContact.TabIndex = 8;
            this.lblTitle.Location = new Point(0xe8, 0x9f);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(40, 0x15);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Title:";
            this.lblTitle.TextAlign = ContentAlignment.MiddleRight;
            this.lblContact.Location = new Point(8, 0x9f);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new Size(0x48, 0x15);
            this.lblContact.TabIndex = 7;
            this.lblContact.Text = "Contact:";
            this.lblContact.TextAlign = ContentAlignment.MiddleRight;
            this.lblFax.Location = new Point(8, 0x83);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new Size(0x48, 0x15);
            this.lblFax.TabIndex = 5;
            this.lblFax.Text = "Fax";
            this.lblFax.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone2.Location = new Point(8, 0x6b);
            this.lblPhone2.Name = "lblPhone2";
            this.lblPhone2.Size = new Size(0x48, 0x15);
            this.lblPhone2.TabIndex = 3;
            this.lblPhone2.Text = "Phone 2:";
            this.lblPhone2.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone1.Location = new Point(8, 0x53);
            this.lblPhone1.Name = "lblPhone1";
            this.lblPhone1.Size = new Size(0x48, 0x15);
            this.lblPhone1.TabIndex = 1;
            this.lblPhone1.Text = "Phone:";
            this.lblPhone1.TextAlign = ContentAlignment.MiddleRight;
            this.chbPrintHAOOnInvoice.Location = new Point(0x58, 80);
            this.chbPrintHAOOnInvoice.Name = "chbPrintHAOOnInvoice";
            this.chbPrintHAOOnInvoice.Size = new Size(0xd8, 0x16);
            this.chbPrintHAOOnInvoice.TabIndex = 6;
            this.chbPrintHAOOnInvoice.Text = "Print HAOCode Record on Invoice";
            this.chbPrintInvOnInvoice.Location = new Point(0x58, 0x38);
            this.chbPrintInvOnInvoice.Name = "chbPrintInvOnInvoice";
            this.chbPrintInvOnInvoice.Size = new Size(0xd8, 0x16);
            this.chbPrintInvOnInvoice.TabIndex = 5;
            this.chbPrintInvOnInvoice.Text = "Print Inventory Description on Invoice";
            this.txtMedicareNumber.Location = new Point(0x58, 0x88);
            this.txtMedicareNumber.MaxLength = 50;
            this.txtMedicareNumber.Name = "txtMedicareNumber";
            this.txtMedicareNumber.Size = new Size(0xa8, 20);
            this.txtMedicareNumber.TabIndex = 11;
            this.cmbBasis.Location = new Point(0xa8, 0x20);
            this.cmbBasis.Name = "cmbBasis";
            this.cmbBasis.Size = new Size(0x58, 0x15);
            this.cmbBasis.TabIndex = 4;
            this.lblMedicareNumber.Location = new Point(8, 0x88);
            this.lblMedicareNumber.Name = "lblMedicareNumber";
            this.lblMedicareNumber.Size = new Size(0x48, 0x15);
            this.lblMedicareNumber.TabIndex = 10;
            this.lblMedicareNumber.Text = "Medicare #";
            this.lblMedicareNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblExpPer.Location = new Point(8, 0x20);
            this.lblExpPer.Name = "lblExpPer";
            this.lblExpPer.Size = new Size(0x48, 0x16);
            this.lblExpPer.TabIndex = 2;
            this.lblExpPer.Text = "Expected %";
            this.lblExpPer.TextAlign = ContentAlignment.MiddleRight;
            this.lblPriceCode.Location = new Point(8, 8);
            this.lblPriceCode.Name = "lblPriceCode";
            this.lblPriceCode.Size = new Size(0x48, 0x16);
            this.lblPriceCode.TabIndex = 0;
            this.lblPriceCode.Text = "Price Code";
            this.lblPriceCode.TextAlign = ContentAlignment.MiddleRight;
            this.cmbECSFormat.ForeColor = SystemColors.WindowText;
            this.cmbECSFormat.Location = new Point(0x58, 8);
            this.cmbECSFormat.Name = "cmbECSFormat";
            this.cmbECSFormat.Size = new Size(0xa8, 0x15);
            this.cmbECSFormat.TabIndex = 1;
            this.cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbType.Location = new Point(0x58, 0x68);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new Size(0xa8, 0x15);
            this.cmbType.TabIndex = 8;
            this.lblEcsFormat.Location = new Point(8, 8);
            this.lblEcsFormat.Name = "lblEcsFormat";
            this.lblEcsFormat.Size = new Size(0x48, 0x15);
            this.lblEcsFormat.TabIndex = 0;
            this.lblEcsFormat.Text = "ECS Format:";
            this.lblEcsFormat.TextAlign = ContentAlignment.MiddleRight;
            this.lblType.Location = new Point(8, 0x68);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(0x48, 0x16);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "Type";
            this.lblType.TextAlign = ContentAlignment.MiddleRight;
            this.nmbExpectedPercent.Location = new Point(0x58, 0x20);
            this.nmbExpectedPercent.Name = "nmbExpectedPercent";
            this.nmbExpectedPercent.Size = new Size(0x48, 20);
            this.nmbExpectedPercent.TabIndex = 3;
            this.CAddress.Location = new Point(0x10, 8);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x138, 0x48);
            this.CAddress.TabIndex = 0;
            this.cmbPriceCode.Location = new Point(0x58, 8);
            this.cmbPriceCode.Name = "cmbPriceCode";
            this.cmbPriceCode.Size = new Size(0xe8, 0x15);
            this.cmbPriceCode.TabIndex = 1;
            this.txtPhone2.Location = new Point(0x58, 0x6b);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new Size(240, 20);
            this.txtPhone2.TabIndex = 4;
            this.txtPhone.Location = new Point(0x58, 0x53);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(240, 20);
            this.txtPhone.TabIndex = 2;
            this.txtFax.Location = new Point(0x58, 0x83);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(240, 20);
            this.txtFax.TabIndex = 6;
            this.txtZirmedNumber.Location = new Point(0x58, 0xb8);
            this.txtZirmedNumber.MaxLength = 50;
            this.txtZirmedNumber.Name = "txtZirmedNumber";
            this.txtZirmedNumber.Size = new Size(0xa8, 20);
            this.txtZirmedNumber.TabIndex = 15;
            this.lblZirmedNumber.Location = new Point(8, 0xb8);
            this.lblZirmedNumber.Name = "lblZirmedNumber";
            this.lblZirmedNumber.Size = new Size(0x48, 0x15);
            this.lblZirmedNumber.TabIndex = 14;
            this.lblZirmedNumber.Text = "Zirmed #";
            this.lblZirmedNumber.TextAlign = ContentAlignment.MiddleRight;
            this.cmbInvoiceForm.Location = new Point(0x58, 0x98);
            this.cmbInvoiceForm.Name = "cmbInvoiceForm";
            this.cmbInvoiceForm.Size = new Size(0xe8, 0x15);
            this.cmbInvoiceForm.TabIndex = 12;
            this.lblInvoiceForm.Location = new Point(8, 0x98);
            this.lblInvoiceForm.Name = "lblInvoiceForm";
            this.lblInvoiceForm.Size = new Size(0x48, 0x16);
            this.lblInvoiceForm.TabIndex = 11;
            this.lblInvoiceForm.Text = "Invoice Form";
            this.lblInvoiceForm.TextAlign = ContentAlignment.MiddleRight;
            this.txtMedicaidNumber.Location = new Point(0x58, 0x70);
            this.txtMedicaidNumber.MaxLength = 50;
            this.txtMedicaidNumber.Name = "txtMedicaidNumber";
            this.txtMedicaidNumber.Size = new Size(0xa8, 20);
            this.txtMedicaidNumber.TabIndex = 9;
            this.lblMedicaidNumber.Location = new Point(8, 0x70);
            this.lblMedicaidNumber.Name = "lblMedicaidNumber";
            this.lblMedicaidNumber.Size = new Size(0x48, 0x15);
            this.lblMedicaidNumber.TabIndex = 8;
            this.lblMedicaidNumber.Text = "Medicaid #";
            this.lblMedicaidNumber.TextAlign = ContentAlignment.MiddleRight;
            this.miGotoEligibility.Index = 0;
            this.miGotoEligibility.Text = "Eligibility";
            this.PageControl2.Controls.Add(this.tpContact);
            this.PageControl2.Controls.Add(this.tpBilling);
            this.PageControl2.Controls.Add(this.tpEdi);
            this.PageControl2.Controls.Add(this.tp837);
            this.PageControl2.Controls.Add(this.tpEligibility);
            this.PageControl2.Dock = DockStyle.Fill;
            this.PageControl2.ImageList = this.ImageList1;
            this.PageControl2.Location = new Point(0, 0x20);
            this.PageControl2.Name = "PageControl2";
            this.PageControl2.SelectedIndex = 0;
            this.PageControl2.Size = new Size(0x180, 0xf7);
            this.PageControl2.TabIndex = 1;
            this.tpContact.Controls.Add(this.CAddress);
            this.tpContact.Controls.Add(this.lblPhone1);
            this.tpContact.Controls.Add(this.txtPhone2);
            this.tpContact.Controls.Add(this.lblPhone2);
            this.tpContact.Controls.Add(this.txtPhone);
            this.tpContact.Controls.Add(this.lblFax);
            this.tpContact.Controls.Add(this.txtFax);
            this.tpContact.Controls.Add(this.lblContact);
            this.tpContact.Controls.Add(this.txtTitle);
            this.tpContact.Controls.Add(this.lblTitle);
            this.tpContact.Controls.Add(this.txtContact);
            this.tpContact.Location = new Point(4, 0x17);
            this.tpContact.Name = "tpContact";
            this.tpContact.Padding = new Padding(3);
            this.tpContact.Size = new Size(0x178, 220);
            this.tpContact.TabIndex = 0;
            this.tpContact.Text = "Contact";
            this.tpContact.UseVisualStyleBackColor = true;
            this.tpBilling.Controls.Add(this.cmbGroup);
            this.tpBilling.Controls.Add(this.lblGroup);
            this.tpBilling.Controls.Add(this.cmbPriceCode);
            this.tpBilling.Controls.Add(this.lblPriceCode);
            this.tpBilling.Controls.Add(this.cmbInvoiceForm);
            this.tpBilling.Controls.Add(this.lblExpPer);
            this.tpBilling.Controls.Add(this.lblInvoiceForm);
            this.tpBilling.Controls.Add(this.cmbBasis);
            this.tpBilling.Controls.Add(this.chbPrintInvOnInvoice);
            this.tpBilling.Controls.Add(this.chbPrintHAOOnInvoice);
            this.tpBilling.Controls.Add(this.lblType);
            this.tpBilling.Controls.Add(this.cmbType);
            this.tpBilling.Controls.Add(this.nmbExpectedPercent);
            this.tpBilling.Location = new Point(4, 0x17);
            this.tpBilling.Name = "tpBilling";
            this.tpBilling.Padding = new Padding(3);
            this.tpBilling.Size = new Size(0x178, 220);
            this.tpBilling.TabIndex = 1;
            this.tpBilling.Text = "Billing";
            this.tpBilling.UseVisualStyleBackColor = true;
            this.cmbGroup.Location = new Point(0x58, 0x80);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new Size(0xe8, 0x15);
            this.cmbGroup.TabIndex = 10;
            this.lblGroup.Location = new Point(8, 0x80);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new Size(0x48, 0x16);
            this.lblGroup.TabIndex = 9;
            this.lblGroup.Text = "Group";
            this.lblGroup.TextAlign = ContentAlignment.MiddleRight;
            this.tpEdi.Controls.Add(this.txtClaimMdNumber);
            this.tpEdi.Controls.Add(this.lblEcsFormat);
            this.tpEdi.Controls.Add(this.lblAvailityNumber);
            this.tpEdi.Controls.Add(this.lblClaimMdNumber);
            this.tpEdi.Controls.Add(this.txtAvailityNumber);
            this.tpEdi.Controls.Add(this.txtMedicaidNumber);
            this.tpEdi.Controls.Add(this.txtOfficeAllyNumber);
            this.tpEdi.Controls.Add(this.txtAbilityNumber);
            this.tpEdi.Controls.Add(this.lblMedicareNumber);
            this.tpEdi.Controls.Add(this.txtZirmedNumber);
            this.tpEdi.Controls.Add(this.lblOfficeAllyNumber);
            this.tpEdi.Controls.Add(this.lblAbilityNumber);
            this.tpEdi.Controls.Add(this.lblMedicaidNumber);
            this.tpEdi.Controls.Add(this.txtMedicareNumber);
            this.tpEdi.Controls.Add(this.cmbECSFormat);
            this.tpEdi.Controls.Add(this.lblZirmedNumber);
            this.tpEdi.Location = new Point(4, 0x17);
            this.tpEdi.Name = "tpEdi";
            this.tpEdi.Padding = new Padding(3);
            this.tpEdi.Size = new Size(0x178, 220);
            this.tpEdi.TabIndex = 3;
            this.tpEdi.Text = "Edi";
            this.tpEdi.UseVisualStyleBackColor = true;
            this.txtClaimMdNumber.Location = new Point(0x58, 0x58);
            this.txtClaimMdNumber.MaxLength = 50;
            this.txtClaimMdNumber.Name = "txtClaimMdNumber";
            this.txtClaimMdNumber.Size = new Size(0xa8, 20);
            this.txtClaimMdNumber.TabIndex = 7;
            this.lblAvailityNumber.Location = new Point(8, 0x40);
            this.lblAvailityNumber.Name = "lblAvailityNumber";
            this.lblAvailityNumber.Size = new Size(0x48, 0x15);
            this.lblAvailityNumber.TabIndex = 4;
            this.lblAvailityNumber.Text = "Availity #";
            this.lblAvailityNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblClaimMdNumber.Location = new Point(8, 0x58);
            this.lblClaimMdNumber.Name = "lblClaimMdNumber";
            this.lblClaimMdNumber.Size = new Size(0x48, 0x15);
            this.lblClaimMdNumber.TabIndex = 6;
            this.lblClaimMdNumber.Text = "Claim.MD #";
            this.lblClaimMdNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtAvailityNumber.Location = new Point(0x58, 0x40);
            this.txtAvailityNumber.MaxLength = 50;
            this.txtAvailityNumber.Name = "txtAvailityNumber";
            this.txtAvailityNumber.Size = new Size(0xa8, 20);
            this.txtAvailityNumber.TabIndex = 5;
            this.txtOfficeAllyNumber.Location = new Point(0x58, 160);
            this.txtOfficeAllyNumber.MaxLength = 50;
            this.txtOfficeAllyNumber.Name = "txtOfficeAllyNumber";
            this.txtOfficeAllyNumber.Size = new Size(0xa8, 20);
            this.txtOfficeAllyNumber.TabIndex = 13;
            this.txtAbilityNumber.Location = new Point(0x58, 40);
            this.txtAbilityNumber.MaxLength = 50;
            this.txtAbilityNumber.Name = "txtAbilityNumber";
            this.txtAbilityNumber.Size = new Size(0xa8, 20);
            this.txtAbilityNumber.TabIndex = 3;
            this.lblOfficeAllyNumber.Location = new Point(8, 160);
            this.lblOfficeAllyNumber.Name = "lblOfficeAllyNumber";
            this.lblOfficeAllyNumber.Size = new Size(0x48, 0x15);
            this.lblOfficeAllyNumber.TabIndex = 12;
            this.lblOfficeAllyNumber.Text = "Office Ally #";
            this.lblOfficeAllyNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblAbilityNumber.Location = new Point(8, 40);
            this.lblAbilityNumber.Name = "lblAbilityNumber";
            this.lblAbilityNumber.Size = new Size(0x48, 0x15);
            this.lblAbilityNumber.TabIndex = 2;
            this.lblAbilityNumber.Text = "Ability #";
            this.lblAbilityNumber.TextAlign = ContentAlignment.MiddleRight;
            this.tp837.Controls.Add(this.lblTaxonomyCodePrefix);
            this.tp837.Controls.Add(this.txtTaxonomyCodePrefix);
            this.tp837.Controls.Add(this.chbAppendPrefixToTaxonomyCode);
            this.tp837.Controls.Add(this.chbExtractRenderingProvider);
            this.tp837.Controls.Add(this.chbParticipatingProvider);
            this.tp837.Controls.Add(this.chbExtractOrderingPhysician);
            this.tp837.Controls.Add(this.chbExtractReferringPhysician);
            this.tp837.Location = new Point(4, 0x17);
            this.tp837.Name = "tp837";
            this.tp837.Padding = new Padding(3);
            this.tp837.Size = new Size(0x178, 220);
            this.tp837.TabIndex = 4;
            this.tp837.Text = "837";
            this.tp837.UseVisualStyleBackColor = true;
            this.lblTaxonomyCodePrefix.Location = new Point(0x18, 0x80);
            this.lblTaxonomyCodePrefix.Name = "lblTaxonomyCodePrefix";
            this.lblTaxonomyCodePrefix.Size = new Size(0x30, 0x15);
            this.lblTaxonomyCodePrefix.TabIndex = 9;
            this.lblTaxonomyCodePrefix.Text = "Prefix:";
            this.lblTaxonomyCodePrefix.TextAlign = ContentAlignment.MiddleRight;
            this.txtTaxonomyCodePrefix.Location = new Point(80, 0x80);
            this.txtTaxonomyCodePrefix.MaxLength = 10;
            this.txtTaxonomyCodePrefix.Name = "txtTaxonomyCodePrefix";
            this.txtTaxonomyCodePrefix.Size = new Size(0x60, 20);
            this.txtTaxonomyCodePrefix.TabIndex = 10;
            this.chbAppendPrefixToTaxonomyCode.Location = new Point(8, 0x68);
            this.chbAppendPrefixToTaxonomyCode.Name = "chbAppendPrefixToTaxonomyCode";
            this.chbAppendPrefixToTaxonomyCode.Size = new Size(200, 0x16);
            this.chbAppendPrefixToTaxonomyCode.TabIndex = 4;
            this.chbAppendPrefixToTaxonomyCode.Text = "Append prefix to Taxonomy Code";
            this.chbExtractRenderingProvider.Location = new Point(8, 80);
            this.chbExtractRenderingProvider.Name = "chbExtractRenderingProvider";
            this.chbExtractRenderingProvider.Size = new Size(200, 0x16);
            this.chbExtractRenderingProvider.TabIndex = 3;
            this.chbExtractRenderingProvider.Text = "Extract Rendering Provider";
            this.chbParticipatingProvider.Location = new Point(8, 8);
            this.chbParticipatingProvider.Name = "chbParticipatingProvider";
            this.chbParticipatingProvider.Size = new Size(200, 0x16);
            this.chbParticipatingProvider.TabIndex = 0;
            this.chbParticipatingProvider.Text = "Participating Provider";
            this.chbExtractOrderingPhysician.Location = new Point(8, 0x20);
            this.chbExtractOrderingPhysician.Name = "chbExtractOrderingPhysician";
            this.chbExtractOrderingPhysician.Size = new Size(200, 0x16);
            this.chbExtractOrderingPhysician.TabIndex = 1;
            this.chbExtractOrderingPhysician.Text = "Extract Ordering Physician";
            this.chbExtractReferringPhysician.Location = new Point(8, 0x38);
            this.chbExtractReferringPhysician.Name = "chbExtractReferringPhysician";
            this.chbExtractReferringPhysician.Size = new Size(200, 0x16);
            this.chbExtractReferringPhysician.TabIndex = 2;
            this.chbExtractReferringPhysician.Text = "Extract Referring Physician";
            this.tpEligibility.Controls.Add(this.cmbAbilityEligibilityPayer);
            this.tpEligibility.Controls.Add(this.lblAbilityEligibilityPayer);
            this.tpEligibility.Location = new Point(4, 0x17);
            this.tpEligibility.Name = "tpEligibility";
            this.tpEligibility.Padding = new Padding(3);
            this.tpEligibility.Size = new Size(0x178, 220);
            this.tpEligibility.TabIndex = 5;
            this.tpEligibility.Text = "Eligibility";
            this.cmbAbilityEligibilityPayer.EditButton = false;
            this.cmbAbilityEligibilityPayer.Location = new Point(0x60, 8);
            this.cmbAbilityEligibilityPayer.Name = "cmbAbilityEligibilityPayer";
            this.cmbAbilityEligibilityPayer.NewButton = false;
            this.cmbAbilityEligibilityPayer.Size = new Size(0xa8, 0x15);
            this.cmbAbilityEligibilityPayer.TabIndex = 1;
            this.lblAbilityEligibilityPayer.Location = new Point(8, 8);
            this.lblAbilityEligibilityPayer.Name = "lblAbilityEligibilityPayer";
            this.lblAbilityEligibilityPayer.Size = new Size(80, 0x15);
            this.lblAbilityEligibilityPayer.TabIndex = 0;
            this.lblAbilityEligibilityPayer.Text = "Ability Payer:";
            this.lblAbilityEligibilityPayer.TextAlign = ContentAlignment.MiddleRight;
            this.ImageList1.ImageStream = (ImageListStreamer) manager.GetObject("ImageList1.ImageStream");
            this.ImageList1.TransparentColor = Color.Magenta;
            this.ImageList1.Images.SetKeyName(0, "");
            this.Panel1.Controls.Add(this.txtName);
            this.Panel1.Controls.Add(this.lblName);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x180, 0x20);
            this.Panel1.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x188, 0x162);
            base.Name = "FormInsuranceCompany";
            this.Text = "Maintain Insurance Company";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            ((ISupportInitialize) base.MissingData).EndInit();
            this.PageControl2.ResumeLayout(false);
            this.tpContact.ResumeLayout(false);
            this.tpContact.PerformLayout();
            this.tpBilling.ResumeLayout(false);
            this.tpEdi.ResumeLayout(false);
            this.tpEdi.PerformLayout();
            this.tp837.ResumeLayout(false);
            this.tp837.PerformLayout();
            this.tpEligibility.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "Insurance", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT ic.*\r\n, IFNULL(pr.ParticipatingProvider, 1) as ParticipatingProvider\r\n, IFNULL(pr.ExtractOrderingPhysician, 1) as ExtractOrderingPhysician\r\n, IFNULL(pr.ExtractReferringPhysician, 0) as ExtractReferringPhysician\r\n, IFNULL(pr.ExtractRenderingProvider, 0) as ExtractRenderingProvider\r\n, pr.TaxonomyCodePrefix\r\nFROM tbl_insurancecompany as ic\r\n     LEFT JOIN tbl_payer as pr ON pr.InsuranceCompanyID = ic.ID\r\nWHERE ic.ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.F_MIR = NullableConvert.ToString(reader["MIR"]);
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtFax, reader["Fax"]);
                            Functions.SetTextBoxText(this.txtPhone2, reader["Phone2"]);
                            Functions.SetTextBoxText(this.txtContact, reader["Contact"]);
                            Functions.SetTextBoxText(this.txtTitle, reader["Title"]);
                            Functions.SetComboBoxValue(this.cmbPriceCode, reader["PriceCodeID"]);
                            Functions.SetComboBoxValue(this.cmbInvoiceForm, reader["InvoiceFormID"]);
                            Functions.SetNumericBoxValue(this.nmbExpectedPercent, reader["ExpectedPercent"]);
                            Functions.SetComboBoxText(this.cmbBasis, reader["Basis"]);
                            Functions.SetCheckBoxChecked(this.chbPrintInvOnInvoice, reader["PrintInvOnInvoice"]);
                            Functions.SetCheckBoxChecked(this.chbPrintHAOOnInvoice, reader["PrintHAOOnInvoice"]);
                            Functions.SetCheckBoxChecked(this.chbParticipatingProvider, reader["ParticipatingProvider"]);
                            Functions.SetCheckBoxChecked(this.chbExtractOrderingPhysician, reader["ExtractOrderingPhysician"]);
                            Functions.SetCheckBoxChecked(this.chbExtractReferringPhysician, reader["ExtractReferringPhysician"]);
                            Functions.SetCheckBoxChecked(this.chbExtractRenderingProvider, reader["ExtractRenderingProvider"]);
                            Functions.SetTextBoxText(this.txtTaxonomyCodePrefix, reader["TaxonomyCodePrefix"]);
                            this.chbAppendPrefixToTaxonomyCode.Checked = !string.IsNullOrWhiteSpace(this.txtTaxonomyCodePrefix.Text);
                            Functions.SetComboBoxValue(this.cmbType, reader["Type"]);
                            Functions.SetComboBoxValue(this.cmbGroup, reader["GroupID"]);
                            Functions.SetComboBoxText(this.cmbECSFormat, reader["ECSFormat"]);
                            Functions.SetTextBoxText(this.txtAbilityNumber, reader["AbilityNumber"]);
                            Functions.SetTextBoxText(this.txtAvailityNumber, reader["AvailityNumber"]);
                            Functions.SetTextBoxText(this.txtMedicaidNumber, reader["MedicaidNumber"]);
                            Functions.SetTextBoxText(this.txtMedicareNumber, reader["MedicareNumber"]);
                            Functions.SetTextBoxText(this.txtOfficeAllyNumber, reader["OfficeAllyNumber"]);
                            Functions.SetTextBoxText(this.txtZirmedNumber, reader["ZirmedNumber"]);
                            Functions.SetComboBoxValue(this.cmbAbilityEligibilityPayer, reader["AbilityEligibilityPayerId"]);
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

        private void miGotoEligibility_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["InsuranceCompanyID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormEligibility(), @params);
        }

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_insurancecompany.ID}"] = this.ObjectID
                };
                ClassGlobalObjects.ShowReport(item.ReportFileName, @params);
            }
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_insurancecompany" };
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
                    command.Parameters.Add("Basis", MySqlType.Char, 7).Value = this.cmbBasis.Text;
                    command.Parameters.Add("City", MySqlType.VarChar, 0x19).Value = this.CAddress.txtCity.Text;
                    command.Parameters.Add("Contact", MySqlType.VarChar, 50).Value = this.txtContact.Text;
                    command.Parameters.Add("ExpectedPercent", MySqlType.Double).Value = this.nmbExpectedPercent.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Fax", MySqlType.VarChar, 50).Value = this.txtFax.Text;
                    command.Parameters.Add("GroupID", MySqlType.Int).Value = this.cmbGroup.SelectedValue;
                    command.Parameters.Add("InvoiceFormID", MySqlType.Int).Value = this.cmbInvoiceForm.SelectedValue;
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("Phone", MySqlType.VarChar, 50).Value = this.txtPhone.Text;
                    command.Parameters.Add("Phone2", MySqlType.VarChar, 50).Value = this.txtPhone2.Text;
                    command.Parameters.Add("PriceCodeID", MySqlType.Int).Value = this.cmbPriceCode.SelectedValue;
                    command.Parameters.Add("PrintHAOOnInvoice", MySqlType.Bit).Value = this.chbPrintHAOOnInvoice.Checked;
                    command.Parameters.Add("PrintInvOnInvoice", MySqlType.Bit).Value = this.chbPrintInvOnInvoice.Checked;
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Title", MySqlType.VarChar, 50).Value = this.txtTitle.Text;
                    command.Parameters.Add("Type", MySqlType.Int).Value = this.cmbType.SelectedValue;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("ECSFormat", MySqlType.Char, 20).Value = this.cmbECSFormat.Text;
                    command.Parameters.Add("AbilityNumber", MySqlType.VarChar, 50).Value = this.txtAbilityNumber.Text;
                    command.Parameters.Add("AvailityNumber", MySqlType.VarChar, 50).Value = this.txtAvailityNumber.Text;
                    command.Parameters.Add("MedicaidNumber", MySqlType.VarChar, 50).Value = this.txtMedicaidNumber.Text;
                    command.Parameters.Add("MedicareNumber", MySqlType.VarChar, 50).Value = this.txtMedicareNumber.Text;
                    command.Parameters.Add("OfficeAllyNumber", MySqlType.VarChar, 50).Value = this.txtOfficeAllyNumber.Text;
                    command.Parameters.Add("ZirmedNumber", MySqlType.VarChar, 50).Value = this.txtZirmedNumber.Text;
                    command.Parameters.Add("AbilityEligibilityPayerId", MySqlType.VarChar, 50).Value = this.cmbAbilityEligibilityPayer.SelectedValue;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_insurancecompany", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_insurancecompany"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_insurancecompany");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.Parameters.Add("ParticipatingProvider", MySqlType.Bit).Value = this.chbParticipatingProvider.Checked;
                    command2.Parameters.Add("ExtractOrderingPhysician", MySqlType.Bit).Value = this.chbExtractOrderingPhysician.Checked;
                    command2.Parameters.Add("ExtractReferringPhysician", MySqlType.Bit).Value = this.chbExtractReferringPhysician.Checked;
                    command2.Parameters.Add("ExtractRenderingProvider", MySqlType.Bit).Value = this.chbExtractRenderingProvider.Checked;
                    command2.Parameters.Add("TaxonomyCodePrefix", MySqlType.VarChar, 10).Value = this.chbAppendPrefixToTaxonomyCode.Checked ? this.txtTaxonomyCodePrefix.Text : string.Empty;
                    command2.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    command2.Parameters.Add("InsuranceCompanyID", MySqlType.Int).Value = ID;
                    string[] whereParameters = new string[] { "InsuranceCompanyID" };
                    if (0 >= command2.ExecuteUpdate("tbl_payer", whereParameters))
                    {
                        flag = 0 < command2.ExecuteInsert("tbl_payer");
                    }
                }
                try
                {
                    using (MySqlCommand command3 = new MySqlCommand("", connection))
                    {
                        command3.Parameters.Add("P_InsuranceCompanyID", MySqlType.Int).Value = ID;
                        command3.ExecuteProcedure("mir_update_insurancecompany");
                    }
                    using (MySqlCommand command4 = new MySqlCommand("", connection))
                    {
                        command4.CommandText = "SELECT MIR FROM tbl_insurancecompany WHERE ID = :ID";
                        command4.Parameters.Add("ID", MySqlType.Int, 4).Value = ID;
                        this.F_MIR = NullableConvert.ToString(command4.ExecuteScalar());
                    }
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    TraceHelper.TraceException(ex);
                    ProjectData.ClearProjectError();
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  tbl_insurancecompany.ID\r\n, tbl_insurancecompany.Name\r\n, tbl_insurancecompany.Address1\r\n, tbl_insurancecompany.City\r\n, tbl_insurancecompany.State\r\n, tbl_insurancecompany.Zip\r\n, tbl_insurancecompany.Phone\r\n, IFNULL(tbl_insurancecompanygroup.Name, '') as `Group`\r\nFROM tbl_insurancecompany\r\n     LEFT JOIN tbl_insurancecompanygroup ON tbl_insurancecompany.GroupID = tbl_insurancecompanygroup.ID\r\nORDER BY IFNULL(tbl_insurancecompanygroup.Name, ''), tbl_insurancecompany.Name", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("Group", "Group", 60);
            appearance.AddTextColumn("Name", "Name", 100);
            appearance.AddTextColumn("Address1", "Address", 150);
            appearance.AddTextColumn("City", "City", 100);
            appearance.AddTextColumn("State", "State", 50);
            appearance.AddTextColumn("Zip", "Zip", 50);
            appearance.AddTextColumn("Phone", "Phone", 80);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__247-0 e$__- = new _Closure$__247-0 {
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
            this.ShowMissingInformation(Show, this.tpContact);
            this.ShowMissingInformation(Show, this.tpBilling);
            this.ShowMissingInformation(Show, this.tpEdi);
            this.ShowMissingInformation(Show, this.tp837);
        }

        private void ShowMissingInformation(bool Show, TabPage tabPage)
        {
            if (this.PageControl2.TabPages.IndexOf(tabPage) >= 0)
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
        }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTitle")]
        private TextBox txtTitle { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtContact")]
        private TextBox txtContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTitle")]
        private Label lblTitle { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblContact")]
        private Label lblContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFax")]
        private Label lblFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone2")]
        private Label lblPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone1")]
        private Label lblPhone1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMedicareNumber")]
        private Label lblMedicareNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblExpPer")]
        private Label lblExpPer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPriceCode")]
        private Label lblPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEcsFormat")]
        private Label lblEcsFormat { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblType")]
        private Label lblType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrintHAOOnInvoice")]
        private CheckBox chbPrintHAOOnInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrintInvOnInvoice")]
        private CheckBox chbPrintInvOnInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMedicareNumber")]
        private TextBox txtMedicareNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbECSFormat")]
        private ComboBox cmbECSFormat { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbType")]
        private ComboBox cmbType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbExpectedPercent")]
        private NumericBox nmbExpectedPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBasis")]
        private ComboBox cmbBasis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPriceCode")]
        private Combobox cmbPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("txtZirmedNumber")]
        private TextBox txtZirmedNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblZirmedNumber")]
        private Label lblZirmedNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInvoiceForm")]
        private Combobox cmbInvoiceForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMedicaidNumber")]
        private TextBox txtMedicaidNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMedicaidNumber")]
        private Label lblMedicaidNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoiceForm")]
        private Label lblInvoiceForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("miGotoEligibility")]
        private MenuItem miGotoEligibility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PageControl2")]
        private TabControl PageControl2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpContact")]
        private TabPage tpContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpBilling")]
        private TabPage tpBilling { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbGroup")]
        private Combobox cmbGroup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblGroup")]
        private Label lblGroup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ImageList1")]
        private ImageList ImageList1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAvailityNumber")]
        private TextBox txtAvailityNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAvailityNumber")]
        private Label lblAvailityNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOfficeAllyNumber")]
        private Label lblOfficeAllyNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOfficeAllyNumber")]
        private TextBox txtOfficeAllyNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbParticipatingProvider")]
        private CheckBox chbParticipatingProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbExtractOrderingPhysician")]
        private CheckBox chbExtractOrderingPhysician { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbExtractReferringPhysician")]
        private CheckBox chbExtractReferringPhysician { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAbilityNumber")]
        private TextBox txtAbilityNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAbilityNumber")]
        private Label lblAbilityNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbExtractRenderingProvider")]
        private CheckBox chbExtractRenderingProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtClaimMdNumber")]
        private TextBox txtClaimMdNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblClaimMdNumber")]
        private Label lblClaimMdNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpEdi")]
        private TabPage tpEdi { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tp837")]
        private TabPage tp837 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbAppendPrefixToTaxonomyCode")]
        private CheckBox chbAppendPrefixToTaxonomyCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxonomyCodePrefix")]
        private Label lblTaxonomyCodePrefix { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxonomyCodePrefix")]
        private TextBox txtTaxonomyCodePrefix { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpEligibility")]
        private TabPage tpEligibility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbAbilityEligibilityPayer")]
        private Combobox cmbAbilityEligibilityPayer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAbilityEligibilityPayer")]
        private Label lblAbilityEligibilityPayer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public FormMirHelper MirHelper
        {
            get
            {
                if (this.F_MirHelper == null)
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.F_MirHelper.Add("MedicareNumber", this.txtMedicareNumber, "Medicare Number is required");
                }
                return this.F_MirHelper;
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__247-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

