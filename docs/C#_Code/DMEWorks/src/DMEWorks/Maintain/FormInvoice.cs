namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Core.Extensions;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using DMEWorks.Misc;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonClone=false, ButtonDelete=false, ButtonNew=false, ButtonReload=true)]
    public class FormInvoice : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormInvoice()
        {
            this.InitializeComponent();
            base.cmnuFilter.Popup += new EventHandler(this.cmnuFilter_Popup);
            if (PagedGrids)
            {
                base.AddPagedNavigator(new SearchNavigatorEventsHandler(this));
            }
            else
            {
                base.AddSimpleNavigator(new SearchNavigatorEventsHandler(this));
            }
            TabPage page = !PagedGrids ? base.AddSimpleNavigator(new DetailsNavigatorEventsHandler(this)) : base.AddPagedNavigator(new DetailsNavigatorEventsHandler(this));
            base.ChangesTracker.Subscribe(this.chbAcceptAssignment);
            base.ChangesTracker.Subscribe(this.chbApproved);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance1);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance2);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance3);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance4);
            base.ChangesTracker.Subscribe(this.cmbDoctor);
            base.ChangesTracker.Subscribe(this.cmbFacility);
            base.ChangesTracker.Subscribe(this.cmbPosType);
            base.ChangesTracker.Subscribe(this.cmbReferral);
            base.ChangesTracker.Subscribe(this.cmbSalesRep);
            base.ChangesTracker.Subscribe(this.cmbTaxRate);
            base.ChangesTracker.Subscribe(this.dtbInvoiceDate);
            base.ChangesTracker.Subscribe(this.eddICD10_01);
            base.ChangesTracker.Subscribe(this.eddICD10_02);
            base.ChangesTracker.Subscribe(this.eddICD10_03);
            base.ChangesTracker.Subscribe(this.eddICD10_04);
            base.ChangesTracker.Subscribe(this.eddICD10_05);
            base.ChangesTracker.Subscribe(this.eddICD10_06);
            base.ChangesTracker.Subscribe(this.eddICD10_07);
            base.ChangesTracker.Subscribe(this.eddICD10_08);
            base.ChangesTracker.Subscribe(this.eddICD10_09);
            base.ChangesTracker.Subscribe(this.eddICD10_10);
            base.ChangesTracker.Subscribe(this.eddICD10_11);
            base.ChangesTracker.Subscribe(this.eddICD10_12);
            base.ChangesTracker.Subscribe(this.eddICD9_1);
            base.ChangesTracker.Subscribe(this.eddICD9_2);
            base.ChangesTracker.Subscribe(this.eddICD9_3);
            base.ChangesTracker.Subscribe(this.eddICD9_4);
            base.ChangesTracker.Subscribe(this.txtClaimNote);
            this.ControlInvoiceDetails1.Changed += new EventHandler(this.HandleControlChanged);
            base.SwitchToTabPage(page);
        }

        private static void Alert(string Text, string Caption)
        {
            MessageBox.Show(Text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private static bool AnswerYes(string Text, string Caption) => 
            MessageBox.Show(Text, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        private void btnRecalculateBalance_Click(object sender, EventArgs e)
        {
            try
            {
                this.FetchBalance();
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private void ClearCustomer()
        {
            Functions.SetTextBoxText(this.caShip.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtZip, DBNull.Value);
            this.InternalLoadCustomerInsurance(new CustomerInsuranceTable());
            this.cmbCustomerInsurance1.SelectedValue = DBNull.Value;
            this.cmbCustomerInsurance2.SelectedValue = DBNull.Value;
            this.cmbCustomerInsurance3.SelectedValue = DBNull.Value;
            this.cmbCustomerInsurance4.SelectedValue = DBNull.Value;
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.OrderID = DBNull.Value;
            Functions.SetComboBoxValue(this.cmbCustomer, DBNull.Value);
            this.ClearCustomer();
            this.eddICD9_1.Text = "";
            this.eddICD9_2.Text = "";
            this.eddICD9_3.Text = "";
            this.eddICD9_4.Text = "";
            this.eddICD10_01.Text = "";
            this.eddICD10_02.Text = "";
            this.eddICD10_03.Text = "";
            this.eddICD10_04.Text = "";
            this.eddICD10_05.Text = "";
            this.eddICD10_06.Text = "";
            this.eddICD10_07.Text = "";
            this.eddICD10_08.Text = "";
            this.eddICD10_09.Text = "";
            this.eddICD10_10.Text = "";
            this.eddICD10_11.Text = "";
            this.eddICD10_12.Text = "";
            Functions.SetComboBoxValue(this.cmbDoctor, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbReferral, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbSalesRep, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbFacility, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbTaxRate, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPosType, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbInvoiceBalance, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbInvoiceDate, DBNull.Value);
            Functions.SetTextBoxText(this.txtClaimNote, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbAcceptAssignment, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbApproved, DBNull.Value);
            this.ApprovedState = false;
            this.ControlInvoiceDetails1.ClearGrid();
        }

        private void cmnuFilter_Popup(object sender, EventArgs e)
        {
            this.mnuFilterPagedGrids.Checked = PagedGrids;
        }

        private static bool Confirm(string Text, string Caption) => 
            MessageBox.Show(Text, Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;

        private void ControlInvoiceDetails1_Changed(object sender, EventArgs e)
        {
            try
            {
                this.FetchBalance();
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
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

        private void FetchBalance()
        {
            object objectID = this.ObjectID;
            if (!Versioned.IsNumeric(objectID))
            {
                Functions.SetNumericBoxValue(this.nmbInvoiceBalance, DBNull.Value);
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.CommandText = "SELECT SUM(stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount) as Balance\r\nFROM view_invoicetransaction_statistics as stats\r\nWHERE (stats.InvoiceID = :InvoiceID)";
                        command.Parameters.Add("InvoiceID", MySqlType.Int).Value = objectID;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Functions.SetNumericBoxValue(this.nmbInvoiceBalance, reader["Balance"]);
                            }
                            else
                            {
                                Functions.SetNumericBoxValue(this.nmbInvoiceBalance, DBNull.Value);
                            }
                        }
                    }
                }
            }
        }

        public static string GetQuery()
        {
            QueryOptions options = new QueryOptions();
            options.FilterUserID = null;
            options.FilterLocationID = null;
            options.FilterArchived = YesNoAny.Any;
            options.SearchTerms = null;
            return SearchNavigatorEventsHandler.GetQuery(options);
        }

        private static int[] GetSelectedDetailIds(GridBase grid) => 
            grid.GetSelectedRows().Get<DetailInfo>().Select<DetailInfo, int>(((_Closure$__.$I419-0 == null) ? (_Closure$__.$I419-0 = new Func<DetailInfo, int>(_Closure$__.$I._Lambda$__419-0)) : _Closure$__.$I419-0)).ToArray<int>();

        private static int[] GetSelectedObjectIds(GridBase grid) => 
            grid.GetSelectedRows().Get<InvoiceInfo>().Select<InvoiceInfo, int>(((_Closure$__.$I422-0 == null) ? (_Closure$__.$I422-0 = new Func<InvoiceInfo, int>(_Closure$__.$I._Lambda$__422-0)) : _Closure$__.$I422-0)).ToArray<int>();

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.cmbDoctor, "tbl_doctor", null);
            Cache.InitDropdown(this.cmbReferral, "tbl_referral", null);
            Cache.InitDropdown(this.cmbSalesRep, "tbl_salesrep", null);
            Cache.InitDropdown(this.cmbTaxRate, "tbl_taxrate", null);
            Cache.InitDropdown(this.cmbPosType, "tbl_postype", null);
            Cache.InitDropdown(this.cmbFacility, "tbl_facility", null);
            Cache.InitDropdown(this.eddICD9_1, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD9_2, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD9_3, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD9_4, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD10_01, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_02, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_03, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_04, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_05, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_06, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_07, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_08, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_09, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_10, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_11, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_12, "tbl_icd10", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormInvoice));
            this.Label15 = new Label();
            this.dtbInvoiceDate = new UltraDateTimeEditor();
            this.Label4 = new Label();
            this.nmbInvoiceBalance = new NumericBox();
            this.Label1 = new Label();
            this.cmbCustomer = new Combobox();
            this.eddICD9_1 = new ExtendedDropdown();
            this.eddICD9_2 = new ExtendedDropdown();
            this.eddICD9_3 = new ExtendedDropdown();
            this.eddICD9_4 = new ExtendedDropdown();
            this.lblICD9_4 = new Label();
            this.lblICD9_3 = new Label();
            this.lblICD9_2 = new Label();
            this.lblICD9_1 = new Label();
            this.cmbFacility = new Combobox();
            this.lblFacility = new Label();
            this.lblReferral = new Label();
            this.cmbSalesRep = new Combobox();
            this.lblSalesrep = new Label();
            this.cmbReferral = new Combobox();
            this.lblDoctor = new Label();
            this.cmbDoctor = new Combobox();
            this.lblPosType = new Label();
            this.cmbPosType = new Combobox();
            this.caShip = new ControlAddress();
            this.cmbTaxRate = new Combobox();
            this.lblTaxRate = new Label();
            this.cmbCustomerInsurance4 = new ComboBox();
            this.cmbCustomerInsurance3 = new ComboBox();
            this.cmbCustomerInsurance2 = new ComboBox();
            this.cmbCustomerInsurance1 = new ComboBox();
            this.lblCustomerInsurance4 = new Label();
            this.lblCustomerInsurance3 = new Label();
            this.lblCustomerInsurance2 = new Label();
            this.lblCustomerInsurance1 = new Label();
            this.TabControl1 = new TabControl();
            this.tpLineItems = new TabPage();
            this.ControlInvoiceDetails1 = new ControlInvoiceDetails();
            this.tpClaim = new TabPage();
            this.lblClaimNote = new Label();
            this.txtClaimNote = new TextBox();
            this.chbApproved = new CheckBox();
            this.Panel1 = new Panel();
            this.chbAcceptAssignment = new CheckBox();
            this.lnkOrderID = new LinkLabel();
            this.btnRecalculateBalance = new Button();
            this.lblInvoiceID = new Label();
            this.cmsGridDetails = new ContextMenuStrip(this.components);
            this.tsmiGridDetailsReflag = new ToolStripMenuItem();
            this.tsmiGridDetailsWriteoffBalance = new ToolStripMenuItem();
            this.cmsGridSearch = new ContextMenuStrip(this.components);
            this.tsmiGridSearchReflag = new ToolStripMenuItem();
            this.tsmiGridSearchArchive = new ToolStripMenuItem();
            this.tsmiGridSearchUnarchive = new ToolStripMenuItem();
            this.tsmiGridSearchWriteoffBalance = new ToolStripMenuItem();
            this.mnuGotoImages = new MenuItem();
            this.mnuGotoNewImage = new MenuItem();
            this.mnuActionsAutoSubmit = new MenuItem();
            this.mnuActionsVoidSubmission = new MenuItem();
            this.mnuFilterArchived_No = new MenuItem();
            this.mnuFilterArchived_Any = new MenuItem();
            this.mnuFilterArchived_Yes = new MenuItem();
            this.mnuFilterSeparator = new MenuItem();
            this.mnuFilterPagedGrids = new MenuItem();
            this.TabControl2 = new TabControl();
            this.TabPage1 = new TabPage();
            this.TabPage2 = new TabPage();
            this.TabPage3 = new TabPage();
            this.eddICD10_10 = new ExtendedDropdown();
            this.eddICD10_06 = new ExtendedDropdown();
            this.eddICD10_08 = new ExtendedDropdown();
            this.eddICD10_12 = new ExtendedDropdown();
            this.Label16 = new Label();
            this.eddICD10_07 = new ExtendedDropdown();
            this.Label17 = new Label();
            this.Label18 = new Label();
            this.eddICD10_05 = new ExtendedDropdown();
            this.eddICD10_11 = new ExtendedDropdown();
            this.Label19 = new Label();
            this.Label20 = new Label();
            this.Label21 = new Label();
            this.eddICD10_02 = new ExtendedDropdown();
            this.eddICD10_09 = new ExtendedDropdown();
            this.Label22 = new Label();
            this.eddICD10_04 = new ExtendedDropdown();
            this.Label23 = new Label();
            this.Label24 = new Label();
            this.eddICD10_03 = new ExtendedDropdown();
            this.Label25 = new Label();
            this.eddICD10_01 = new ExtendedDropdown();
            this.Label26 = new Label();
            this.Label27 = new Label();
            base.tpWorkArea.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tpLineItems.SuspendLayout();
            this.tpClaim.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.cmsGridDetails.SuspendLayout();
            this.cmsGridSearch.SuspendLayout();
            this.TabControl2.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.TabPage3.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.TabControl2);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x270, 0x21a);
            MenuItem[] items = new MenuItem[] { this.mnuGotoImages, this.mnuGotoNewImage };
            base.cmnuGoto.MenuItems.AddRange(items);
            MenuItem[] itemArray2 = new MenuItem[] { this.mnuActionsAutoSubmit, this.mnuActionsVoidSubmission };
            base.cmnuActions.MenuItems.AddRange(itemArray2);
            MenuItem[] itemArray3 = new MenuItem[] { this.mnuFilterArchived_No, this.mnuFilterArchived_Any, this.mnuFilterArchived_Yes, this.mnuFilterSeparator, this.mnuFilterPagedGrids };
            base.cmnuFilter.MenuItems.AddRange(itemArray3);
            this.Label15.BackColor = Color.Transparent;
            this.Label15.Location = new Point(8, 8);
            this.Label15.Name = "Label15";
            this.Label15.Size = new Size(0x40, 0x15);
            this.Label15.TabIndex = 0;
            this.Label15.Text = "Customer";
            this.Label15.TextAlign = ContentAlignment.MiddleRight;
            this.dtbInvoiceDate.Location = new Point(0x68, 8);
            this.dtbInvoiceDate.Name = "dtbInvoiceDate";
            this.dtbInvoiceDate.Size = new Size(0x70, 0x15);
            this.dtbInvoiceDate.TabIndex = 1;
            this.Label4.Location = new Point(8, 8);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x58, 0x15);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Invoiced on";
            this.Label4.TextAlign = ContentAlignment.MiddleRight;
            this.nmbInvoiceBalance.Location = new Point(0x68, 0x20);
            this.nmbInvoiceBalance.Name = "nmbInvoiceBalance";
            this.nmbInvoiceBalance.Size = new Size(80, 0x15);
            this.nmbInvoiceBalance.TabIndex = 3;
            this.Label1.Location = new Point(8, 0x20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x58, 0x15);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Invoice balance";
            this.Label1.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomer.BackColor = SystemColors.Control;
            this.cmbCustomer.Location = new Point(80, 8);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0xe0, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.eddICD9_1.Location = new Point(0x40, 8);
            this.eddICD9_1.Name = "eddICD9_1";
            this.eddICD9_1.Size = new Size(0xc0, 0x15);
            this.eddICD9_1.TabIndex = 0x18;
            this.eddICD9_1.TextMember = "";
            this.eddICD9_2.Location = new Point(0x40, 0x20);
            this.eddICD9_2.Name = "eddICD9_2";
            this.eddICD9_2.Size = new Size(0xc0, 0x15);
            this.eddICD9_2.TabIndex = 0x1a;
            this.eddICD9_2.TextMember = "";
            this.eddICD9_3.Location = new Point(0x40, 0x38);
            this.eddICD9_3.Name = "eddICD9_3";
            this.eddICD9_3.Size = new Size(0xc0, 0x15);
            this.eddICD9_3.TabIndex = 0x1c;
            this.eddICD9_3.TextMember = "";
            this.eddICD9_4.Location = new Point(0x40, 80);
            this.eddICD9_4.Name = "eddICD9_4";
            this.eddICD9_4.Size = new Size(0xc0, 0x15);
            this.eddICD9_4.TabIndex = 30;
            this.eddICD9_4.TextMember = "";
            this.lblICD9_4.Location = new Point(8, 80);
            this.lblICD9_4.Name = "lblICD9_4";
            this.lblICD9_4.Size = new Size(0x30, 0x15);
            this.lblICD9_4.TabIndex = 0x1d;
            this.lblICD9_4.Text = "ICD9 4";
            this.lblICD9_4.TextAlign = ContentAlignment.MiddleRight;
            this.lblICD9_3.Location = new Point(8, 0x38);
            this.lblICD9_3.Name = "lblICD9_3";
            this.lblICD9_3.Size = new Size(0x30, 0x15);
            this.lblICD9_3.TabIndex = 0x1b;
            this.lblICD9_3.Text = "ICD9 3";
            this.lblICD9_3.TextAlign = ContentAlignment.MiddleRight;
            this.lblICD9_2.Location = new Point(8, 0x20);
            this.lblICD9_2.Name = "lblICD9_2";
            this.lblICD9_2.Size = new Size(0x30, 0x15);
            this.lblICD9_2.TabIndex = 0x19;
            this.lblICD9_2.Text = "ICD9 2";
            this.lblICD9_2.TextAlign = ContentAlignment.MiddleRight;
            this.lblICD9_1.Location = new Point(8, 8);
            this.lblICD9_1.Name = "lblICD9_1";
            this.lblICD9_1.Size = new Size(0x30, 0x15);
            this.lblICD9_1.TabIndex = 0x17;
            this.lblICD9_1.Text = "ICD9 1";
            this.lblICD9_1.TextAlign = ContentAlignment.MiddleRight;
            this.cmbFacility.Location = new Point(0x180, 0x20);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new Size(0xe0, 0x15);
            this.cmbFacility.TabIndex = 8;
            this.lblFacility.Location = new Point(0x13e, 0x20);
            this.lblFacility.Name = "lblFacility";
            this.lblFacility.Size = new Size(0x38, 0x15);
            this.lblFacility.TabIndex = 7;
            this.lblFacility.Text = "Facility";
            this.lblFacility.TextAlign = ContentAlignment.MiddleRight;
            this.lblReferral.Location = new Point(0x13e, 0x38);
            this.lblReferral.Name = "lblReferral";
            this.lblReferral.Size = new Size(0x38, 0x15);
            this.lblReferral.TabIndex = 9;
            this.lblReferral.Text = "Referral";
            this.lblReferral.TextAlign = ContentAlignment.MiddleRight;
            this.cmbSalesRep.Location = new Point(0x180, 80);
            this.cmbSalesRep.Name = "cmbSalesRep";
            this.cmbSalesRep.Size = new Size(0xe0, 0x15);
            this.cmbSalesRep.TabIndex = 12;
            this.lblSalesrep.Location = new Point(0x13e, 80);
            this.lblSalesrep.Name = "lblSalesrep";
            this.lblSalesrep.Size = new Size(0x38, 0x15);
            this.lblSalesrep.TabIndex = 11;
            this.lblSalesrep.Text = "Sales rep";
            this.lblSalesrep.TextAlign = ContentAlignment.MiddleRight;
            this.cmbReferral.Location = new Point(0x180, 0x38);
            this.cmbReferral.Name = "cmbReferral";
            this.cmbReferral.Size = new Size(0xe0, 0x15);
            this.cmbReferral.TabIndex = 10;
            this.lblDoctor.Location = new Point(320, 0x80);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new Size(0x38, 0x15);
            this.lblDoctor.TabIndex = 3;
            this.lblDoctor.Text = "Doctor";
            this.lblDoctor.TextAlign = ContentAlignment.MiddleRight;
            this.cmbDoctor.Location = new Point(0x180, 0x80);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new Size(0xe0, 0x15);
            this.cmbDoctor.TabIndex = 4;
            this.lblPosType.Location = new Point(320, 8);
            this.lblPosType.Name = "lblPosType";
            this.lblPosType.Size = new Size(0x38, 0x15);
            this.lblPosType.TabIndex = 5;
            this.lblPosType.Text = "POS Type";
            this.lblPosType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbPosType.Location = new Point(0x180, 8);
            this.cmbPosType.Name = "cmbPosType";
            this.cmbPosType.Size = new Size(0xe0, 0x15);
            this.cmbPosType.TabIndex = 6;
            this.caShip.Location = new Point(8, 0x20);
            this.caShip.Name = "caShip";
            this.caShip.Size = new Size(0x128, 0x48);
            this.caShip.TabIndex = 2;
            this.cmbTaxRate.Location = new Point(0x180, 0x68);
            this.cmbTaxRate.Name = "cmbTaxRate";
            this.cmbTaxRate.Size = new Size(0xe0, 0x15);
            this.cmbTaxRate.TabIndex = 14;
            this.lblTaxRate.Location = new Point(0x13e, 0x68);
            this.lblTaxRate.Name = "lblTaxRate";
            this.lblTaxRate.Size = new Size(0x38, 0x15);
            this.lblTaxRate.TabIndex = 13;
            this.lblTaxRate.Text = "Tax Rate";
            this.lblTaxRate.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomerInsurance4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance4.Location = new Point(80, 0xb8);
            this.cmbCustomerInsurance4.Name = "cmbCustomerInsurance4";
            this.cmbCustomerInsurance4.Size = new Size(0xb0, 0x15);
            this.cmbCustomerInsurance4.TabIndex = 0x16;
            this.cmbCustomerInsurance3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance3.Location = new Point(80, 160);
            this.cmbCustomerInsurance3.Name = "cmbCustomerInsurance3";
            this.cmbCustomerInsurance3.Size = new Size(0xb0, 0x15);
            this.cmbCustomerInsurance3.TabIndex = 20;
            this.cmbCustomerInsurance2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance2.Location = new Point(80, 0x88);
            this.cmbCustomerInsurance2.Name = "cmbCustomerInsurance2";
            this.cmbCustomerInsurance2.Size = new Size(0xb0, 0x15);
            this.cmbCustomerInsurance2.TabIndex = 0x12;
            this.cmbCustomerInsurance1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance1.Location = new Point(80, 0x70);
            this.cmbCustomerInsurance1.Name = "cmbCustomerInsurance1";
            this.cmbCustomerInsurance1.Size = new Size(0xb0, 0x15);
            this.cmbCustomerInsurance1.TabIndex = 0x10;
            this.lblCustomerInsurance4.Location = new Point(0x10, 0xb8);
            this.lblCustomerInsurance4.Name = "lblCustomerInsurance4";
            this.lblCustomerInsurance4.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance4.TabIndex = 0x15;
            this.lblCustomerInsurance4.Text = "Policy 4";
            this.lblCustomerInsurance4.TextAlign = ContentAlignment.MiddleRight;
            this.lblCustomerInsurance3.Location = new Point(0x10, 160);
            this.lblCustomerInsurance3.Name = "lblCustomerInsurance3";
            this.lblCustomerInsurance3.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance3.TabIndex = 0x13;
            this.lblCustomerInsurance3.Text = "Policy 3";
            this.lblCustomerInsurance3.TextAlign = ContentAlignment.MiddleRight;
            this.lblCustomerInsurance2.Location = new Point(0x10, 0x88);
            this.lblCustomerInsurance2.Name = "lblCustomerInsurance2";
            this.lblCustomerInsurance2.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance2.TabIndex = 0x11;
            this.lblCustomerInsurance2.Text = "Policy 2";
            this.lblCustomerInsurance2.TextAlign = ContentAlignment.MiddleRight;
            this.lblCustomerInsurance1.Location = new Point(0x10, 0x70);
            this.lblCustomerInsurance1.Name = "lblCustomerInsurance1";
            this.lblCustomerInsurance1.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance1.TabIndex = 15;
            this.lblCustomerInsurance1.Text = "Policy 1";
            this.lblCustomerInsurance1.TextAlign = ContentAlignment.MiddleRight;
            this.TabControl1.Controls.Add(this.tpLineItems);
            this.TabControl1.Controls.Add(this.tpClaim);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.Location = new Point(0, 0x128);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x270, 0xf2);
            this.TabControl1.TabIndex = 2;
            this.tpLineItems.Controls.Add(this.ControlInvoiceDetails1);
            this.tpLineItems.Location = new Point(4, 0x16);
            this.tpLineItems.Name = "tpLineItems";
            this.tpLineItems.Padding = new Padding(3);
            this.tpLineItems.Size = new Size(0x268, 0xd8);
            this.tpLineItems.TabIndex = 0;
            this.tpLineItems.Text = "Lines";
            this.tpLineItems.UseVisualStyleBackColor = true;
            int? nullable = null;
            this.ControlInvoiceDetails1.CustomerID = nullable;
            this.ControlInvoiceDetails1.Dock = DockStyle.Fill;
            this.ControlInvoiceDetails1.Location = new Point(3, 3);
            this.ControlInvoiceDetails1.Name = "ControlInvoiceDetails1";
            nullable = null;
            this.ControlInvoiceDetails1.OrderID = nullable;
            this.ControlInvoiceDetails1.Size = new Size(610, 210);
            this.ControlInvoiceDetails1.TabIndex = 0;
            this.tpClaim.Controls.Add(this.lblClaimNote);
            this.tpClaim.Controls.Add(this.txtClaimNote);
            this.tpClaim.Location = new Point(4, 0x16);
            this.tpClaim.Name = "tpClaim";
            this.tpClaim.Padding = new Padding(3);
            this.tpClaim.Size = new Size(0x268, 0xe0);
            this.tpClaim.TabIndex = 1;
            this.tpClaim.Text = "Claim";
            this.tpClaim.UseVisualStyleBackColor = true;
            this.lblClaimNote.Location = new Point(8, 8);
            this.lblClaimNote.Name = "lblClaimNote";
            this.lblClaimNote.Size = new Size(0x30, 0x15);
            this.lblClaimNote.TabIndex = 0;
            this.lblClaimNote.Text = "Note";
            this.lblClaimNote.TextAlign = ContentAlignment.MiddleRight;
            this.txtClaimNote.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtClaimNote.Location = new Point(0x40, 8);
            this.txtClaimNote.MaxLength = 80;
            this.txtClaimNote.Name = "txtClaimNote";
            this.txtClaimNote.Size = new Size(0x220, 20);
            this.txtClaimNote.TabIndex = 1;
            this.chbApproved.Location = new Point(0xe8, 8);
            this.chbApproved.Name = "chbApproved";
            this.chbApproved.Size = new Size(80, 0x15);
            this.chbApproved.TabIndex = 5;
            this.chbApproved.Text = "Approved";
            this.Panel1.Controls.Add(this.chbAcceptAssignment);
            this.Panel1.Controls.Add(this.lnkOrderID);
            this.Panel1.Controls.Add(this.btnRecalculateBalance);
            this.Panel1.Controls.Add(this.lblInvoiceID);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.dtbInvoiceDate);
            this.Panel1.Controls.Add(this.nmbInvoiceBalance);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.chbApproved);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x270, 0x38);
            this.Panel1.TabIndex = 0;
            this.chbAcceptAssignment.Location = new Point(0xe8, 0x20);
            this.chbAcceptAssignment.Name = "chbAcceptAssignment";
            this.chbAcceptAssignment.Size = new Size(120, 0x15);
            this.chbAcceptAssignment.TabIndex = 6;
            this.chbAcceptAssignment.Text = "Accept Assignment";
            this.lnkOrderID.BorderStyle = BorderStyle.FixedSingle;
            this.lnkOrderID.LinkArea = new LinkArea(8, 13);
            this.lnkOrderID.Location = new Point(0x180, 0x20);
            this.lnkOrderID.Name = "lnkOrderID";
            this.lnkOrderID.Size = new Size(100, 0x15);
            this.lnkOrderID.TabIndex = 8;
            this.lnkOrderID.TabStop = true;
            this.lnkOrderID.Text = "Order # 00000";
            this.lnkOrderID.TextAlign = ContentAlignment.MiddleCenter;
            this.lnkOrderID.UseCompatibleTextRendering = true;
            this.btnRecalculateBalance.FlatStyle = FlatStyle.Flat;
            this.btnRecalculateBalance.Image = (Image) manager.GetObject("btnRecalculateBalance.Image");
            this.btnRecalculateBalance.Location = new Point(0xc0, 0x20);
            this.btnRecalculateBalance.Name = "btnRecalculateBalance";
            this.btnRecalculateBalance.Size = new Size(0x15, 0x15);
            this.btnRecalculateBalance.TabIndex = 4;
            this.lblInvoiceID.BorderStyle = BorderStyle.FixedSingle;
            this.lblInvoiceID.Location = new Point(0x180, 8);
            this.lblInvoiceID.Name = "lblInvoiceID";
            this.lblInvoiceID.Size = new Size(100, 0x15);
            this.lblInvoiceID.TabIndex = 7;
            this.lblInvoiceID.Text = "Invoice # 00000";
            this.lblInvoiceID.TextAlign = ContentAlignment.MiddleCenter;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiGridDetailsReflag, this.tsmiGridDetailsWriteoffBalance };
            this.cmsGridDetails.Items.AddRange(toolStripItems);
            this.cmsGridDetails.Name = "cmsGridDetails";
            this.cmsGridDetails.Size = new Size(0xa2, 0x30);
            this.tsmiGridDetailsReflag.Name = "tsmiGridDetailsReflag";
            this.tsmiGridDetailsReflag.Size = new Size(0xa1, 0x16);
            this.tsmiGridDetailsReflag.Text = "Reflag selected";
            this.tsmiGridDetailsWriteoffBalance.Name = "tsmiGridDetailsWriteoffBalance";
            this.tsmiGridDetailsWriteoffBalance.Size = new Size(0xa1, 0x16);
            this.tsmiGridDetailsWriteoffBalance.Text = "Writeoff Balance";
            ToolStripItem[] itemArray5 = new ToolStripItem[] { this.tsmiGridSearchReflag, this.tsmiGridSearchArchive, this.tsmiGridSearchUnarchive, this.tsmiGridSearchWriteoffBalance };
            this.cmsGridSearch.Items.AddRange(itemArray5);
            this.cmsGridSearch.Name = "cmsGridSearch";
            this.cmsGridSearch.Size = new Size(0xa2, 0x5c);
            this.tsmiGridSearchReflag.Name = "tsmiGridSearchReflag";
            this.tsmiGridSearchReflag.Size = new Size(0xa1, 0x16);
            this.tsmiGridSearchReflag.Text = "Reflag selected";
            this.tsmiGridSearchArchive.Name = "tsmiGridSearchArchive";
            this.tsmiGridSearchArchive.Size = new Size(0xa1, 0x16);
            this.tsmiGridSearchArchive.Text = "Archive";
            this.tsmiGridSearchUnarchive.Name = "tsmiGridSearchUnarchive";
            this.tsmiGridSearchUnarchive.Size = new Size(0xa1, 0x16);
            this.tsmiGridSearchUnarchive.Text = "Unarchive";
            this.tsmiGridSearchWriteoffBalance.Name = "tsmiGridSearchWriteoffBalance";
            this.tsmiGridSearchWriteoffBalance.Size = new Size(0xa1, 0x16);
            this.tsmiGridSearchWriteoffBalance.Text = "Writeoff Balance";
            this.mnuGotoImages.Index = 0;
            this.mnuGotoImages.Text = "Images";
            this.mnuGotoNewImage.Index = 1;
            this.mnuGotoNewImage.Text = "New Image";
            this.mnuActionsAutoSubmit.Index = 0;
            this.mnuActionsAutoSubmit.Text = "Auto-submit";
            this.mnuActionsVoidSubmission.Index = 1;
            this.mnuActionsVoidSubmission.Text = "Void Submission";
            this.mnuFilterArchived_No.Checked = true;
            this.mnuFilterArchived_No.Index = 0;
            this.mnuFilterArchived_No.Text = "Archived : No";
            this.mnuFilterArchived_Any.Index = 1;
            this.mnuFilterArchived_Any.Text = "Archived : Any";
            this.mnuFilterArchived_Yes.Index = 2;
            this.mnuFilterArchived_Yes.Text = "Archived : Yes";
            this.mnuFilterSeparator.Index = 3;
            this.mnuFilterSeparator.Text = "-";
            this.mnuFilterPagedGrids.Index = 4;
            this.mnuFilterPagedGrids.Text = "Paged Grids";
            this.TabControl2.Controls.Add(this.TabPage1);
            this.TabControl2.Controls.Add(this.TabPage2);
            this.TabControl2.Controls.Add(this.TabPage3);
            this.TabControl2.Dock = DockStyle.Top;
            this.TabControl2.Location = new Point(0, 0x38);
            this.TabControl2.Name = "TabControl2";
            this.TabControl2.SelectedIndex = 0;
            this.TabControl2.Size = new Size(0x270, 240);
            this.TabControl2.TabIndex = 0x1f;
            this.TabPage1.Controls.Add(this.caShip);
            this.TabPage1.Controls.Add(this.lblDoctor);
            this.TabPage1.Controls.Add(this.lblReferral);
            this.TabPage1.Controls.Add(this.cmbDoctor);
            this.TabPage1.Controls.Add(this.lblFacility);
            this.TabPage1.Controls.Add(this.lblPosType);
            this.TabPage1.Controls.Add(this.cmbFacility);
            this.TabPage1.Controls.Add(this.cmbPosType);
            this.TabPage1.Controls.Add(this.lblSalesrep);
            this.TabPage1.Controls.Add(this.cmbReferral);
            this.TabPage1.Controls.Add(this.cmbTaxRate);
            this.TabPage1.Controls.Add(this.cmbSalesRep);
            this.TabPage1.Controls.Add(this.lblTaxRate);
            this.TabPage1.Controls.Add(this.cmbCustomer);
            this.TabPage1.Controls.Add(this.cmbCustomerInsurance4);
            this.TabPage1.Controls.Add(this.Label15);
            this.TabPage1.Controls.Add(this.cmbCustomerInsurance3);
            this.TabPage1.Controls.Add(this.lblCustomerInsurance1);
            this.TabPage1.Controls.Add(this.cmbCustomerInsurance2);
            this.TabPage1.Controls.Add(this.lblCustomerInsurance2);
            this.TabPage1.Controls.Add(this.cmbCustomerInsurance1);
            this.TabPage1.Controls.Add(this.lblCustomerInsurance3);
            this.TabPage1.Controls.Add(this.lblCustomerInsurance4);
            this.TabPage1.Location = new Point(4, 0x16);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new Size(0x268, 0xd6);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Info";
            this.TabPage1.UseVisualStyleBackColor = true;
            this.TabPage2.Controls.Add(this.lblICD9_1);
            this.TabPage2.Controls.Add(this.eddICD9_4);
            this.TabPage2.Controls.Add(this.eddICD9_2);
            this.TabPage2.Controls.Add(this.lblICD9_2);
            this.TabPage2.Controls.Add(this.eddICD9_3);
            this.TabPage2.Controls.Add(this.lblICD9_4);
            this.TabPage2.Controls.Add(this.lblICD9_3);
            this.TabPage2.Controls.Add(this.eddICD9_1);
            this.TabPage2.Location = new Point(4, 0x16);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new Size(0x268, 0xd6);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "ICD 9";
            this.TabPage2.UseVisualStyleBackColor = true;
            this.TabPage3.Controls.Add(this.eddICD10_10);
            this.TabPage3.Controls.Add(this.eddICD10_06);
            this.TabPage3.Controls.Add(this.eddICD10_08);
            this.TabPage3.Controls.Add(this.eddICD10_12);
            this.TabPage3.Controls.Add(this.Label16);
            this.TabPage3.Controls.Add(this.eddICD10_07);
            this.TabPage3.Controls.Add(this.Label17);
            this.TabPage3.Controls.Add(this.Label18);
            this.TabPage3.Controls.Add(this.eddICD10_05);
            this.TabPage3.Controls.Add(this.eddICD10_11);
            this.TabPage3.Controls.Add(this.Label19);
            this.TabPage3.Controls.Add(this.Label20);
            this.TabPage3.Controls.Add(this.Label21);
            this.TabPage3.Controls.Add(this.eddICD10_02);
            this.TabPage3.Controls.Add(this.eddICD10_09);
            this.TabPage3.Controls.Add(this.Label22);
            this.TabPage3.Controls.Add(this.eddICD10_04);
            this.TabPage3.Controls.Add(this.Label23);
            this.TabPage3.Controls.Add(this.Label24);
            this.TabPage3.Controls.Add(this.eddICD10_03);
            this.TabPage3.Controls.Add(this.Label25);
            this.TabPage3.Controls.Add(this.eddICD10_01);
            this.TabPage3.Controls.Add(this.Label26);
            this.TabPage3.Controls.Add(this.Label27);
            this.TabPage3.Location = new Point(4, 0x16);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new Padding(3);
            this.TabPage3.Size = new Size(0x268, 0xd6);
            this.TabPage3.TabIndex = 3;
            this.TabPage3.Text = "ICD 10";
            this.TabPage3.UseVisualStyleBackColor = true;
            this.eddICD10_10.Location = new Point(360, 80);
            this.eddICD10_10.Name = "eddICD10_10";
            this.eddICD10_10.Size = new Size(200, 0x15);
            this.eddICD10_10.TabIndex = 0x2b;
            this.eddICD10_10.TextMember = "";
            this.eddICD10_06.Location = new Point(0x38, 0x80);
            this.eddICD10_06.Name = "eddICD10_06";
            this.eddICD10_06.Size = new Size(200, 0x15);
            this.eddICD10_06.TabIndex = 0x23;
            this.eddICD10_06.TextMember = "";
            this.eddICD10_08.Location = new Point(360, 0x20);
            this.eddICD10_08.Name = "eddICD10_08";
            this.eddICD10_08.Size = new Size(200, 0x15);
            this.eddICD10_08.TabIndex = 0x27;
            this.eddICD10_08.TextMember = "";
            this.eddICD10_12.Location = new Point(360, 0x80);
            this.eddICD10_12.Name = "eddICD10_12";
            this.eddICD10_12.Size = new Size(200, 0x15);
            this.eddICD10_12.TabIndex = 0x2f;
            this.eddICD10_12.TextMember = "";
            this.Label16.Location = new Point(8, 0x68);
            this.Label16.Name = "Label16";
            this.Label16.Size = new Size(0x20, 20);
            this.Label16.TabIndex = 0x20;
            this.Label16.Text = "# 5";
            this.Label16.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_07.Location = new Point(360, 8);
            this.eddICD10_07.Name = "eddICD10_07";
            this.eddICD10_07.Size = new Size(200, 0x15);
            this.eddICD10_07.TabIndex = 0x25;
            this.eddICD10_07.TextMember = "";
            this.Label17.Location = new Point(0x138, 0x38);
            this.Label17.Name = "Label17";
            this.Label17.Size = new Size(0x20, 20);
            this.Label17.TabIndex = 40;
            this.Label17.Text = "# 9";
            this.Label17.TextAlign = ContentAlignment.MiddleRight;
            this.Label18.Location = new Point(8, 0x80);
            this.Label18.Name = "Label18";
            this.Label18.Size = new Size(0x20, 20);
            this.Label18.TabIndex = 0x22;
            this.Label18.Text = "# 6";
            this.Label18.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_05.Location = new Point(0x38, 0x68);
            this.eddICD10_05.Name = "eddICD10_05";
            this.eddICD10_05.Size = new Size(200, 0x15);
            this.eddICD10_05.TabIndex = 0x21;
            this.eddICD10_05.TextMember = "";
            this.eddICD10_11.Location = new Point(360, 0x68);
            this.eddICD10_11.Name = "eddICD10_11";
            this.eddICD10_11.Size = new Size(200, 0x15);
            this.eddICD10_11.TabIndex = 0x2d;
            this.eddICD10_11.TextMember = "";
            this.Label19.Location = new Point(0x138, 8);
            this.Label19.Name = "Label19";
            this.Label19.Size = new Size(0x20, 20);
            this.Label19.TabIndex = 0x24;
            this.Label19.Text = "# 7";
            this.Label19.TextAlign = ContentAlignment.MiddleRight;
            this.Label20.Location = new Point(0x138, 0x20);
            this.Label20.Name = "Label20";
            this.Label20.Size = new Size(0x20, 20);
            this.Label20.TabIndex = 0x26;
            this.Label20.Text = "# 8";
            this.Label20.TextAlign = ContentAlignment.MiddleRight;
            this.Label21.Location = new Point(0x138, 80);
            this.Label21.Name = "Label21";
            this.Label21.Size = new Size(0x20, 20);
            this.Label21.TabIndex = 0x2a;
            this.Label21.Text = "# 10";
            this.Label21.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_02.Location = new Point(0x38, 0x20);
            this.eddICD10_02.Name = "eddICD10_02";
            this.eddICD10_02.Size = new Size(200, 0x15);
            this.eddICD10_02.TabIndex = 0x1b;
            this.eddICD10_02.TextMember = "";
            this.eddICD10_09.Location = new Point(360, 0x38);
            this.eddICD10_09.Name = "eddICD10_09";
            this.eddICD10_09.Size = new Size(200, 0x15);
            this.eddICD10_09.TabIndex = 0x29;
            this.eddICD10_09.TextMember = "";
            this.Label22.Location = new Point(0x138, 0x80);
            this.Label22.Name = "Label22";
            this.Label22.Size = new Size(0x20, 20);
            this.Label22.TabIndex = 0x2e;
            this.Label22.Text = "# 12";
            this.Label22.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_04.Location = new Point(0x38, 80);
            this.eddICD10_04.Name = "eddICD10_04";
            this.eddICD10_04.Size = new Size(200, 0x15);
            this.eddICD10_04.TabIndex = 0x1f;
            this.eddICD10_04.TextMember = "";
            this.Label23.Location = new Point(0x138, 0x68);
            this.Label23.Name = "Label23";
            this.Label23.Size = new Size(0x20, 20);
            this.Label23.TabIndex = 0x2c;
            this.Label23.Text = "# 11";
            this.Label23.TextAlign = ContentAlignment.MiddleRight;
            this.Label24.Location = new Point(8, 8);
            this.Label24.Name = "Label24";
            this.Label24.Size = new Size(0x20, 20);
            this.Label24.TabIndex = 0x18;
            this.Label24.Text = "# 1";
            this.Label24.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_03.Location = new Point(0x38, 0x38);
            this.eddICD10_03.Name = "eddICD10_03";
            this.eddICD10_03.Size = new Size(200, 0x15);
            this.eddICD10_03.TabIndex = 0x1d;
            this.eddICD10_03.TextMember = "";
            this.Label25.Location = new Point(8, 0x20);
            this.Label25.Name = "Label25";
            this.Label25.Size = new Size(0x20, 20);
            this.Label25.TabIndex = 0x1a;
            this.Label25.Text = "# 2";
            this.Label25.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_01.Location = new Point(0x38, 8);
            this.eddICD10_01.Name = "eddICD10_01";
            this.eddICD10_01.Size = new Size(200, 0x15);
            this.eddICD10_01.TabIndex = 0x19;
            this.eddICD10_01.TextMember = "";
            this.Label26.Location = new Point(8, 0x38);
            this.Label26.Name = "Label26";
            this.Label26.Size = new Size(0x20, 20);
            this.Label26.TabIndex = 0x1c;
            this.Label26.Text = "# 3";
            this.Label26.TextAlign = ContentAlignment.MiddleRight;
            this.Label27.Location = new Point(8, 80);
            this.Label27.Name = "Label27";
            this.Label27.Size = new Size(0x20, 20);
            this.Label27.TabIndex = 30;
            this.Label27.Text = "# 4";
            this.Label27.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x278, 0x265);
            base.Name = "FormInvoice";
            this.Text = "Form Invoice";
            base.tpWorkArea.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.tpLineItems.ResumeLayout(false);
            this.tpClaim.ResumeLayout(false);
            this.tpClaim.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.cmsGridDetails.ResumeLayout(false);
            this.cmsGridSearch.ResumeLayout(false);
            this.TabControl2.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu {
                MenuItems = { new MenuItem("Selected", new EventHandler(this.mnuPrintSelected_Click)) }
            };
            if (Permissions.FormPrintInvoices.Allow_VIEW)
            {
                menu.MenuItems.Add(new MenuItem("All Invoices", new EventHandler(this.mnuPrintAll_Click)));
            }
            Cache.AddCategory(menu, "Invoice", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        private void InternalLoadCustomerInsurance(CustomerInsuranceTable table)
        {
            this.cmbCustomerInsurance1.DataSource = new DataView(table);
            this.cmbCustomerInsurance1.ValueMember = "ID";
            this.cmbCustomerInsurance1.DisplayMember = "Name";
            this.cmbCustomerInsurance2.DataSource = new DataView(table);
            this.cmbCustomerInsurance2.ValueMember = "ID";
            this.cmbCustomerInsurance2.DisplayMember = "Name";
            this.cmbCustomerInsurance3.DataSource = new DataView(table);
            this.cmbCustomerInsurance3.ValueMember = "ID";
            this.cmbCustomerInsurance3.DisplayMember = "Name";
            this.cmbCustomerInsurance4.DataSource = new DataView(table);
            this.cmbCustomerInsurance4.ValueMember = "ID";
            this.cmbCustomerInsurance4.DisplayMember = "Name";
        }

        private void lnkOrderID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (Versioned.IsNumeric(e.Link.LinkData))
                {
                    FormParameters @params = new FormParameters {
                        ["ID"] = Conversions.ToLong(e.Link.LinkData)
                    };
                    ClassGlobalObjects.ShowForm(FormFactories.FormOrder(), @params);
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

        private void LoadCustomer(int CustomerID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT
  IF(ShipActive, ShipAddress1, Address1) as Address1
, IF(ShipActive, ShipAddress2, Address2) as Address2
, IF(ShipActive, ShipCity,     City    ) as City
, IF(ShipActive, ShipState,    State   ) as State
, IF(ShipActive, ShipZip,      Zip     ) as Zip
FROM tbl_customer
WHERE (ID = {CustomerID})";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            this.ClearCustomer();
                        }
                        else
                        {
                            Functions.SetTextBoxText(this.caShip.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.caShip.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.caShip.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.caShip.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.caShip.txtZip, reader["Zip"]);
                        }
                    }
                }
                CustomerInsuranceTable dataTable = new CustomerInsuranceTable();
                DataRow row = dataTable.NewRow();
                dataTable.Rows.Add(row);
                row.AcceptChanges();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT tbl_customer_insurance.ID,
       tbl_insurancecompany.Name
FROM tbl_customer_insurance
     LEFT JOIN tbl_insurancecompany ON tbl_customer_insurance.InsuranceCompanyID = tbl_insurancecompany.ID
WHERE (tbl_customer_insurance.CustomerID = {CustomerID})", connection))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                    adapter.Fill(dataTable);
                }
                this.InternalLoadCustomerInsurance(dataTable);
                DataView view = new DataView(dataTable, "(ID IS NOT NULL)", "Rank ASC", DataViewRowState.OriginalRows);
                this.cmbCustomerInsurance1.SelectedValue = (1 > view.Count) ? DBNull.Value : view[0]["ID"];
                this.cmbCustomerInsurance2.SelectedValue = (2 > view.Count) ? DBNull.Value : view[1]["ID"];
                this.cmbCustomerInsurance3.SelectedValue = (3 > view.Count) ? DBNull.Value : view[2]["ID"];
                this.cmbCustomerInsurance4.SelectedValue = (4 > view.Count) ? DBNull.Value : view[3]["ID"];
            }
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                bool flag;
                int num;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_invoice WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.OrderID = reader["OrderID"];
                            Functions.SetComboBoxValue(this.cmbCustomer, reader["CustomerID"]);
                            if (Versioned.IsNumeric(reader["CustomerID"]))
                            {
                                num = Conversions.ToInteger(reader["CustomerID"]);
                                this.LoadCustomer(num);
                                this.ControlInvoiceDetails1.CustomerID = new int?(num);
                            }
                            else
                            {
                                this.ClearCustomer();
                                this.ControlInvoiceDetails1.CustomerID = null;
                            }
                            this.eddICD9_1.Text = NullableConvert.ToString(reader["ICD9_1"]);
                            this.eddICD9_2.Text = NullableConvert.ToString(reader["ICD9_2"]);
                            this.eddICD9_3.Text = NullableConvert.ToString(reader["ICD9_3"]);
                            this.eddICD9_4.Text = NullableConvert.ToString(reader["ICD9_4"]);
                            this.eddICD10_01.Text = NullableConvert.ToString(reader["ICD10_01"]);
                            this.eddICD10_02.Text = NullableConvert.ToString(reader["ICD10_02"]);
                            this.eddICD10_03.Text = NullableConvert.ToString(reader["ICD10_03"]);
                            this.eddICD10_04.Text = NullableConvert.ToString(reader["ICD10_04"]);
                            this.eddICD10_05.Text = NullableConvert.ToString(reader["ICD10_05"]);
                            this.eddICD10_06.Text = NullableConvert.ToString(reader["ICD10_06"]);
                            this.eddICD10_07.Text = NullableConvert.ToString(reader["ICD10_07"]);
                            this.eddICD10_08.Text = NullableConvert.ToString(reader["ICD10_08"]);
                            this.eddICD10_09.Text = NullableConvert.ToString(reader["ICD10_09"]);
                            this.eddICD10_10.Text = NullableConvert.ToString(reader["ICD10_10"]);
                            this.eddICD10_11.Text = NullableConvert.ToString(reader["ICD10_11"]);
                            this.eddICD10_12.Text = NullableConvert.ToString(reader["ICD10_12"]);
                            Functions.SetComboBoxValue(this.cmbDoctor, reader["DoctorID"]);
                            Functions.SetComboBoxValue(this.cmbPosType, reader["PosTypeID"]);
                            Functions.SetComboBoxValue(this.cmbFacility, reader["FacilityID"]);
                            Functions.SetComboBoxValue(this.cmbReferral, reader["ReferralID"]);
                            Functions.SetComboBoxValue(this.cmbSalesRep, reader["SalesRepID"]);
                            Functions.SetComboBoxValue(this.cmbTaxRate, reader["TaxRateID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance1, reader["CustomerInsurance1_ID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance2, reader["CustomerInsurance2_ID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance3, reader["CustomerInsurance3_ID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance4, reader["CustomerInsurance4_ID"]);
                            Functions.SetNumericBoxValue(this.nmbInvoiceBalance, reader["InvoiceBalance"]);
                            Functions.SetDateBoxValue(this.dtbInvoiceDate, reader["InvoiceDate"]);
                            Functions.SetTextBoxText(this.txtClaimNote, reader["ClaimNote"]);
                            Functions.SetCheckBoxChecked(this.chbAcceptAssignment, reader["AcceptAssignment"]);
                            Functions.SetCheckBoxChecked(this.chbApproved, reader["Approved"]);
                            this.ApprovedState = this.chbApproved.Checked;
                            goto TR_000C;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
                return flag;
            TR_000C:
                this.ControlInvoiceDetails1.LoadGrid(connection, num, ID);
                return true;
            }
        }

        private void mnuActionsAutoSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                object objectID = this.ObjectID;
                if (objectID is int)
                {
                    string str;
                    List<string> list = new List<string> { 
                        "Ins2",
                        "Ins3",
                        "Ins4"
                    };
                    using (VBSelectBox box = new VBSelectBox())
                    {
                        box.Text = "Auto-submit";
                        box.Prompt = "Select policy";
                        box.Values = list.ToArray();
                        box.Owner = this;
                        box.StartPosition = FormStartPosition.CenterParent;
                        if (box.ShowDialog() == DialogResult.OK)
                        {
                            str = Conversions.ToString(box.Value);
                        }
                        else
                        {
                            return;
                        }
                    }
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.Parameters.Add("P_InvoiceID", objectID);
                            command.Parameters.Add("P_AutoSubmittedTo", str);
                            command.Parameters.Add("P_LastUpdateUserID", Globals.CompanyUserID);
                            connection.Open();
                            command.ExecuteProcedure("Invoice_AddAutoSubmit");
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

        private void mnuActionsVoidSubmission_Click(object sender, EventArgs e)
        {
            try
            {
                object objectID = this.ObjectID;
                if (objectID is int)
                {
                    VoidedSubmissionExtraData data;
                    using (DialogVoidSubmission submission = new DialogVoidSubmission())
                    {
                        submission.Icon = base.Icon;
                        submission.Owner = this;
                        submission.StartPosition = FormStartPosition.CenterParent;
                        if (submission.ShowDialog() == DialogResult.OK)
                        {
                            VoidedSubmissionExtraData data1 = new VoidedSubmissionExtraData();
                            data1.ClaimNumber = submission.ClaimNumber;
                            data1.VoidMethod = new VoidMethod?(submission.VoidMethod);
                            data = data1;
                        }
                        else
                        {
                            return;
                        }
                    }
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            MySqlParameter parameter1 = new MySqlParameter("P_InvoiceID", MySqlType.Int);
                            parameter1.Value = objectID;
                            command.Parameters.Add(parameter1);
                            MySqlParameter parameter2 = new MySqlParameter("P_Extra", MySqlType.Text);
                            parameter2.Value = data.ToString();
                            command.Parameters.Add(parameter2);
                            MySqlParameter parameter3 = new MySqlParameter("P_LastUpdateUserID", MySqlType.Int);
                            parameter3.Value = Globals.CompanyUserID;
                            command.Parameters.Add(parameter3);
                            connection.Open();
                            command.ExecuteProcedure("Invoice_Reflag");
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

        private void mnuFilterArchived_Any_Click(object sender, EventArgs e)
        {
            this.mnuFilterArchived_No.Checked = false;
            this.mnuFilterArchived_Any.Checked = true;
            this.mnuFilterArchived_Yes.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuFilterArchived_No_Click(object sender, EventArgs e)
        {
            this.mnuFilterArchived_No.Checked = true;
            this.mnuFilterArchived_Any.Checked = false;
            this.mnuFilterArchived_Yes.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuFilterArchived_Yes_Click(object sender, EventArgs e)
        {
            this.mnuFilterArchived_No.Checked = false;
            this.mnuFilterArchived_Any.Checked = false;
            this.mnuFilterArchived_Yes.Checked = true;
            this.InvalidateObjectList();
        }

        private void mnuFilterPagedGrids_Click(object sender, EventArgs e)
        {
            this.mnuFilterPagedGrids.Checked = !this.mnuFilterPagedGrids.Checked;
            PagedGrids = this.mnuFilterPagedGrids.Checked;
            ClassGlobalObjects.ShowForm(FormFactories.FormInvoice());
            base.Close();
        }

        private void mnuGotoImages_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["CustomerID"] = this.cmbCustomer.SelectedValue,
                ["OrderID"] = this.OrderID,
                ["InvoiceID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImageSearch(), @params);
        }

        private void mnuGotoNewImage_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["CustomerID"] = this.cmbCustomer.SelectedValue,
                ["OrderID"] = this.OrderID,
                ["InvoiceID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImage(), @params);
        }

        private void mnuPrintAll_Click(object sender, EventArgs e)
        {
            ClassGlobalObjects.ShowForm(FormFactories.FormPrintInvoices());
        }

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_invoice.ID}"] = this.ObjectID
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

        private void mnuPrintSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (Versioned.IsNumeric(this.ObjectID))
                {
                    if (base.HasUnsavedChanges)
                    {
                        if (AnswerYes("Current invoice has changes that was not saved in the database. Whould you like to save invoice and print?", "Invoice Printing"))
                        {
                            base.DoSaveClick();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (AnswerYes("Current invoice was not saved. In order to print invoice it should be saved. Whould you like to save invoice and print?", "Invoice Printing"))
                {
                    base.DoSaveClick();
                }
                else
                {
                    return;
                }
                if (PrintInvoice(Conversions.ToInteger(this.ObjectID)))
                {
                    string[] tableNames = new string[] { "tbl_invoice", "tbl_invoicedetails" };
                    ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
                    base.OpenObject(Conversions.ToInteger(this.ObjectID));
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

        private static bool ObjectEquals(object obj1, object obj2) => 
            (obj1 is int) && ((obj2 is int) && (Conversions.ToInteger(obj1) == Conversions.ToInteger(obj2)));

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_invoice", "tbl_invoicedetails", "tbl_invoicenotes" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        public static bool PrintInvoice(int InvoiceID)
        {
            bool flag;
            bool valueOrDefault;
            bool flag3;
            string str;
            string str2;
            string str3;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT DISTINCT
  statistics.CurrentPayer as SubmitTo
, statistics.InvoiceSubmitted
, CASE WHEN statistics.CurrentPayer = 'Ins1'
        AND insco.ECSFormat IN ('Region A', 'Region B', 'Region C', 'Region D', 'Zirmed', 'Medi-Cal', 'Availity', 'Office Ally', 'Ability')
       THEN 1 ELSE 0 END as IsEdiInvoice
, IF(statistics.CurrentPayer = 'Patient', tbl_customerform.Name          , tbl_inscoform.Name          ) as InvoiceFormName
, IF(statistics.CurrentPayer = 'Patient', tbl_customerform.ReportFileName, tbl_inscoform.ReportFileName) as ReportFileName
FROM view_invoicetransaction_statistics as statistics
     INNER JOIN tbl_customer ON tbl_customer.ID = statistics.CustomerID
     LEFT JOIN tbl_insurancecompany as insco ON insco.ID = statistics.CurrentInsuranceCompanyID
     LEFT JOIN tbl_invoiceform as tbl_inscoform ON tbl_inscoform.ID = insco.InvoiceFormID
     LEFT JOIN tbl_invoiceform as tbl_customerform ON tbl_customerform.ID = tbl_customer.InvoiceFormID
WHERE (statistics.InvoiceID = {InvoiceID})";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return false;
                        }
                        else
                        {
                            valueOrDefault = NullableConvert.ToBoolean(reader["InvoiceSubmitted"]).GetValueOrDefault(false);
                            flag3 = NullableConvert.ToBoolean(reader["IsEdiInvoice"]).GetValueOrDefault(false);
                            str = NullableConvert.ToString(reader["SubmitTo"]);
                            str2 = NullableConvert.ToString(reader["InvoiceFormName"]).TrimEnd(new char[0]);
                            str3 = NullableConvert.ToString(reader["ReportFileName"]).TrimEnd(new char[0]);
                        }
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (!string.IsNullOrWhiteSpace(str3))
                {
                    if (flag3 | valueOrDefault)
                    {
                        string text = !(flag3 & valueOrDefault) ? (!flag3 ? "Invoice you want to print has been already printed.\r\n\r\nAre you sure that you want to print it again?" : "Invoice you want to print must be submitted by EDI.\r\n\r\nAre you sure that you want to print it?") : "Invoice you want to print must be submitted by EDI and it has been already submitted.\r\n\r\nAre you sure that you want to print it?";
                        if (!AnswerYes(text, "Invoice Printing"))
                        {
                            return false;
                        }
                    }
                    if (!Confirm($"Prepare form '{str2}' for printing.", "Invoice Printing"))
                    {
                        flag = false;
                    }
                    else
                    {
                        ReportParameters @params = new ReportParameters {
                            ["{?tbl_invoice.ID}"] = InvoiceID,
                            ["{?SubmitTo}"] = str
                        };
                        ClassGlobalObjects.ShowReport(str3, @params, true);
                        using (MySqlConnection connection2 = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            connection2.Open();
                            using (MySqlCommand command2 = new MySqlCommand("", connection2))
                            {
                                command2.CommandText = "CALL Invoice_AddSubmitted(:InvoiceID, :SubmittedTo, 'Paper', Null, :LastUpdateUserID)";
                                command2.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                                command2.Parameters.Add("SubmittedTo", MySqlType.VarChar, 50).Value = str;
                                command2.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                command2.ExecuteNonQuery();
                            }
                        }
                        flag = true;
                    }
                }
                else
                {
                    if (string.Equals(str, "Patient", StringComparison.OrdinalIgnoreCase))
                    {
                        Alert("Please check customer. He does not have default form assigned.", "Invoice Printing");
                    }
                    else
                    {
                        Alert("Please check insurance company. It does not have default form assigned.", "Invoice Printing");
                    }
                    flag = false;
                }
            }
            else
            {
                Alert("Cannot print invoice. All payers have already paid.", "Invoice Printing");
                flag = false;
            }
            return flag;
        }

        public static void PrintPatientInvoice(int InvoiceID)
        {
            string str = "";
            string str2 = "";
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT IFNULL(tbl_invoiceform.Name          , '') as InvoiceFormName
     , IFNULL(tbl_invoiceform.ReportFileName, '') as ReportFileName
FROM tbl_invoice
     INNER JOIN tbl_customer ON tbl_customer.ID = tbl_invoice.CustomerID
     LEFT JOIN tbl_invoiceform ON tbl_invoiceform.ID = tbl_customer.InvoiceFormID
WHERE (tbl_invoice.ID = {InvoiceID})";
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            str = NullableConvert.ToString(reader["InvoiceFormName"]).TrimEnd(new char[0]);
                            str2 = NullableConvert.ToString(reader["ReportFileName"]).TrimEnd(new char[0]);
                        }
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(str2))
            {
                Alert("Please check customer. He does not have default form assigned.", "Invoice Printing");
            }
            else if (Confirm($"Prepare form '{str}' for printing.", "Invoice Printing"))
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_invoice.ID}"] = InvoiceID,
                    ["{?SubmitTo}"] = ""
                };
                ClassGlobalObjects.ShowReport(str2, @params, true);
                using (MySqlConnection connection2 = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    using (MySqlCommand command2 = new MySqlCommand("", connection2))
                    {
                        connection2.Open();
                        command2.CommandText = "CALL Invoice_AddSubmitted(:InvoiceID, :SubmittedTo, 'Paper', Null, :LastUpdateUserID)";
                        command2.Parameters.Clear();
                        command2.Parameters.Add("InvoiceID", MySqlType.Int).Value = InvoiceID;
                        command2.Parameters.Add("SubmittedTo", MySqlType.VarChar, 50).Value = "Patient";
                        command2.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                        command2.ExecuteNonQuery();
                    }
                }
            }
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            if (IsNew)
            {
                throw new UserNotifyException("You cannot manually add invoices.");
            }
            if (!Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
            {
                throw new UserNotifyException("You must select customer before saving order.");
            }
            int customerID = Conversions.ToInteger(this.cmbCustomer.SelectedValue);
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = this.cmbCustomer.SelectedValue;
                    command.Parameters.Add("AcceptAssignment", MySqlType.Bit).Value = this.chbAcceptAssignment.Checked;
                    command.Parameters.Add("Approved", MySqlType.Bit).Value = this.chbApproved.Checked;
                    command.Parameters.Add("InvoiceDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbInvoiceDate);
                    command.Parameters.Add("InvoiceBalance", MySqlType.Double).Value = this.nmbInvoiceBalance.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("ClaimNote", MySqlType.VarChar, 80).Value = this.txtClaimNote.Text;
                    command.Parameters.Add("DoctorID", MySqlType.Int).Value = this.cmbDoctor.SelectedValue;
                    command.Parameters.Add("POSTypeID", MySqlType.Int).Value = this.cmbPosType.SelectedValue;
                    command.Parameters.Add("FacilityID", MySqlType.Int).Value = this.cmbFacility.SelectedValue;
                    command.Parameters.Add("ReferralID", MySqlType.Int).Value = this.cmbReferral.SelectedValue;
                    command.Parameters.Add("SalesrepID", MySqlType.Int).Value = this.cmbSalesRep.SelectedValue;
                    command.Parameters.Add("TaxRateID", MySqlType.Int).Value = this.cmbTaxRate.SelectedValue;
                    command.Parameters.Add("CustomerInsurance1_ID", MySqlType.Int).Value = this.cmbCustomerInsurance1.SelectedValue;
                    command.Parameters.Add("CustomerInsurance2_ID", MySqlType.Int).Value = this.cmbCustomerInsurance2.SelectedValue;
                    command.Parameters.Add("CustomerInsurance3_ID", MySqlType.Int).Value = this.cmbCustomerInsurance3.SelectedValue;
                    command.Parameters.Add("CustomerInsurance4_ID", MySqlType.Int).Value = this.cmbCustomerInsurance4.SelectedValue;
                    command.Parameters.Add("ICD9_1", MySqlType.VarChar, 6).Value = this.eddICD9_1.Text;
                    command.Parameters.Add("ICD9_2", MySqlType.VarChar, 6).Value = this.eddICD9_2.Text;
                    command.Parameters.Add("ICD9_3", MySqlType.VarChar, 6).Value = this.eddICD9_3.Text;
                    command.Parameters.Add("ICD9_4", MySqlType.VarChar, 6).Value = this.eddICD9_4.Text;
                    command.Parameters.Add("ICD10_01", MySqlType.VarChar, 10).Value = this.eddICD10_01.Text;
                    command.Parameters.Add("ICD10_02", MySqlType.VarChar, 10).Value = this.eddICD10_02.Text;
                    command.Parameters.Add("ICD10_03", MySqlType.VarChar, 10).Value = this.eddICD10_03.Text;
                    command.Parameters.Add("ICD10_04", MySqlType.VarChar, 10).Value = this.eddICD10_04.Text;
                    command.Parameters.Add("ICD10_05", MySqlType.VarChar, 10).Value = this.eddICD10_05.Text;
                    command.Parameters.Add("ICD10_06", MySqlType.VarChar, 10).Value = this.eddICD10_06.Text;
                    command.Parameters.Add("ICD10_07", MySqlType.VarChar, 10).Value = this.eddICD10_07.Text;
                    command.Parameters.Add("ICD10_08", MySqlType.VarChar, 10).Value = this.eddICD10_08.Text;
                    command.Parameters.Add("ICD10_09", MySqlType.VarChar, 10).Value = this.eddICD10_09.Text;
                    command.Parameters.Add("ICD10_10", MySqlType.VarChar, 10).Value = this.eddICD10_10.Text;
                    command.Parameters.Add("ICD10_11", MySqlType.VarChar, 10).Value = this.eddICD10_11.Text;
                    command.Parameters.Add("ICD10_12", MySqlType.VarChar, 10).Value = this.eddICD10_12.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    string[] whereParameters = new string[] { "ID" };
                    if (command.ExecuteUpdate("tbl_invoice", whereParameters) == 0)
                    {
                        throw new UserNotifyException("Invoice was deleted.");
                    }
                }
                this.ControlInvoiceDetails1.SaveGrid(connection, customerID, ID);
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.CommandText = $"Call Invoice_UpdateBalance({ID}, true)";
                    command2.ExecuteNonQuery();
                }
                using (MySqlCommand command3 = new MySqlCommand("", connection))
                {
                    command3.CommandText = $"Call Invoice_UpdatePendingSubmissions({ID})";
                    command3.ExecuteNonQuery();
                }
                this.ApprovedState = this.chbApproved.Checked;
                this.ControlInvoiceDetails1.LoadGrid(connection, customerID, ID);
                return true;
            }
        }

        private void tsmiGridDetailsReflag_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridDetails.SourceControl<GridBase>();
                if (grid != null)
                {
                    int[] selectedDetailIds = GetSelectedDetailIds(grid);
                    if (selectedDetailIds.Length != 0)
                    {
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand("Call InvoiceDetails_Reflag(:P_InvoiceID, :P_InvoiceDetailsID, :P_LastUpdateUserID)", connection))
                            {
                                command.Parameters.Add("P_InvoiceID", MySqlType.Text, 0x10000).Value = DBNull.Value;
                                command.Parameters.Add("P_InvoiceDetailsID", MySqlType.Text, 0x10000).Value = string.Join<int>(",", selectedDetailIds);
                                command.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                this.InvalidateObjectList();
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

        private void tsmiGridDetailsWriteoffBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (AnswerYes("Are you sure that you want to writeoff balance of the selected lines?", "Writeoff Balance"))
                {
                    GridBase grid = this.cmsGridDetails.SourceControl<GridBase>();
                    if (grid != null)
                    {
                        int[] selectedDetailIds = GetSelectedDetailIds(grid);
                        if (selectedDetailIds.Length != 0)
                        {
                            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                            {
                                connection.Open();
                                using (MySqlCommand command = new MySqlCommand("Call InvoiceDetails_WriteoffBalance(:P_InvoiceID, :P_InvoiceDetailsID, :P_LastUpdateUserID)", connection))
                                {
                                    command.Parameters.Add("P_InvoiceID", MySqlType.Text, 0x10000).Value = DBNull.Value;
                                    command.Parameters.Add("P_InvoiceDetailsID", MySqlType.Text, 0x10000).Value = string.Join<int>(",", selectedDetailIds);
                                    command.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                this.InvalidateObjectList();
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

        private void tsmiGridSearchArchive_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                if (grid != null)
                {
                    int[] selectedObjectIds = GetSelectedObjectIds(grid);
                    if (selectedObjectIds.Length != 0)
                    {
                        SearchGridProcessor processor = new SearchGridProcessor(ClassGlobalObjects.ConnectionString_MySql, selectedObjectIds);
                        using (DialogBackgroundWorker worker = new DialogBackgroundWorker("Archive invoices", new DoWorkEventHandler(processor.Archive)))
                        {
                            worker.ShowDialog();
                        }
                        this.InvalidateObjectList();
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

        private void tsmiGridSearchReflag_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                if (grid != null)
                {
                    int[] selectedObjectIds = GetSelectedObjectIds(grid);
                    if (selectedObjectIds.Length != 0)
                    {
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand("Call InvoiceDetails_Reflag(:P_InvoiceID, :P_InvoiceDetailsID, :P_LastUpdateUserID)", connection))
                            {
                                command.Parameters.Add("P_InvoiceID", MySqlType.Text, 0x10000).Value = string.Join<int>(",", selectedObjectIds);
                                command.Parameters.Add("P_InvoiceDetailsID", MySqlType.Text, 0x10000).Value = DBNull.Value;
                                command.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                this.InvalidateObjectList();
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

        private void tsmiGridSearchUnarchive_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                if (grid != null)
                {
                    int[] selectedObjectIds = GetSelectedObjectIds(grid);
                    if (selectedObjectIds.Length != 0)
                    {
                        SearchGridProcessor processor = new SearchGridProcessor(ClassGlobalObjects.ConnectionString_MySql, selectedObjectIds);
                        using (DialogBackgroundWorker worker = new DialogBackgroundWorker("Unarchive invoices", new DoWorkEventHandler(processor.Unarchive)))
                        {
                            worker.ShowDialog();
                        }
                        this.InvalidateObjectList();
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

        private void tsmiGridSearchWriteoffBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (AnswerYes("Are you sure that you want to writeoff balance of the selected invoices?", "Writeoff Balance"))
                {
                    GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                    if (grid != null)
                    {
                        int[] selectedObjectIds = GetSelectedObjectIds(grid);
                        if (selectedObjectIds.Length != 0)
                        {
                            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                            {
                                connection.Open();
                                using (MySqlCommand command = new MySqlCommand("Call InvoiceDetails_WriteoffBalance(:P_InvoiceID, :P_InvoiceDetailsID, :P_LastUpdateUserID)", connection))
                                {
                                    command.Parameters.Add("P_InvoiceID", MySqlType.Text, 0x10000).Value = string.Join<int>(",", selectedObjectIds);
                                    command.Parameters.Add("P_InvoiceDetailsID", MySqlType.Text, 0x10000).Value = DBNull.Value;
                                    command.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                this.InvalidateObjectList();
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

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (!Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
            {
                base.ValidationErrors.SetError(this.cmbCustomer, "You Must Select Customer");
            }
            object selectedValue = this.cmbCustomerInsurance2.SelectedValue;
            object obj3 = this.cmbCustomerInsurance3.SelectedValue;
            object obj4 = this.cmbCustomerInsurance4.SelectedValue;
            object obj1 = this.cmbCustomerInsurance1.SelectedValue;
            if (ObjectEquals(obj1, selectedValue))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance1, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance2, "You must use distinct values.");
            }
            object local1 = obj1;
            if (ObjectEquals(local1, obj3))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance1, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance3, "You must use distinct values.");
            }
            if (ObjectEquals(local1, obj4))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance1, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance4, "You must use distinct values.");
            }
            if (ObjectEquals(selectedValue, obj3))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance2, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance3, "You must use distinct values.");
            }
            if (ObjectEquals(selectedValue, obj4))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance2, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance4, "You must use distinct values.");
            }
            if (ObjectEquals(obj3, obj4))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance3, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance4, "You must use distinct values.");
            }
        }

        [field: AccessedThroughProperty("Label15")]
        private Label Label15 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_1")]
        private ExtendedDropdown eddICD9_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_2")]
        private ExtendedDropdown eddICD9_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_3")]
        private ExtendedDropdown eddICD9_3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_4")]
        private ExtendedDropdown eddICD9_4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_4")]
        private Label lblICD9_4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_3")]
        private Label lblICD9_3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_2")]
        private Label lblICD9_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_1")]
        private Label lblICD9_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbFacility")]
        private Combobox cmbFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFacility")]
        private Label lblFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReferral")]
        private Label lblReferral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbSalesRep")]
        private Combobox cmbSalesRep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSalesrep")]
        private Label lblSalesrep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbReferral")]
        private Combobox cmbReferral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbInvoiceDate")]
        private UltraDateTimeEditor dtbInvoiceDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbInvoiceBalance")]
        private NumericBox nmbInvoiceBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbApproved")]
        private CheckBox chbApproved { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance4")]
        private ComboBox cmbCustomerInsurance4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance3")]
        private ComboBox cmbCustomerInsurance3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance2")]
        private ComboBox cmbCustomerInsurance2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance1")]
        private ComboBox cmbCustomerInsurance1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance4")]
        private Label lblCustomerInsurance4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance3")]
        private Label lblCustomerInsurance3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance2")]
        private Label lblCustomerInsurance2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance1")]
        private Label lblCustomerInsurance1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlInvoiceDetails1")]
        private ControlInvoiceDetails ControlInvoiceDetails1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnRecalculateBalance")]
        private Button btnRecalculateBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridSearch")]
        private ContextMenuStrip cmsGridSearch { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridDetails")]
        private ContextMenuStrip cmsGridDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchReflag")]
        private ToolStripMenuItem tsmiGridSearchReflag { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridDetailsReflag")]
        private ToolStripMenuItem tsmiGridDetailsReflag { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoiceID")]
        private Label lblInvoiceID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lnkOrderID")]
        private LinkLabel lnkOrderID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbTaxRate")]
        private Combobox cmbTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxRate")]
        private Label lblTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caShip")]
        private ControlAddress caShip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoImages")]
        private MenuItem mnuGotoImages { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoNewImage")]
        private MenuItem mnuGotoNewImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPosType")]
        private Label lblPosType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPosType")]
        private Combobox cmbPosType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbAcceptAssignment")]
        private CheckBox chbAcceptAssignment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDoctor")]
        private Label lblDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctor")]
        private Combobox cmbDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsAutoSubmit")]
        private MenuItem mnuActionsAutoSubmit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsVoidSubmission")]
        private MenuItem mnuActionsVoidSubmission { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpLineItems")]
        private TabPage tpLineItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpClaim")]
        private TabPage tpClaim { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblClaimNote")]
        private Label lblClaimNote { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtClaimNote")]
        private TextBox txtClaimNote { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterArchived_No")]
        private MenuItem mnuFilterArchived_No { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterArchived_Any")]
        private MenuItem mnuFilterArchived_Any { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterArchived_Yes")]
        private MenuItem mnuFilterArchived_Yes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterSeparator")]
        private MenuItem mnuFilterSeparator { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterPagedGrids")]
        private MenuItem mnuFilterPagedGrids { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchArchive")]
        private ToolStripMenuItem tsmiGridSearchArchive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchUnarchive")]
        private ToolStripMenuItem tsmiGridSearchUnarchive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl2")]
        private TabControl TabControl2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabPage1")]
        private TabPage TabPage1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabPage2")]
        private TabPage TabPage2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabPage3")]
        private TabPage TabPage3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_10")]
        private ExtendedDropdown eddICD10_10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_06")]
        private ExtendedDropdown eddICD10_06 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_08")]
        private ExtendedDropdown eddICD10_08 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_12")]
        private ExtendedDropdown eddICD10_12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label16")]
        private Label Label16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_07")]
        private ExtendedDropdown eddICD10_07 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label17")]
        private Label Label17 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label18")]
        private Label Label18 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_05")]
        private ExtendedDropdown eddICD10_05 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_11")]
        private ExtendedDropdown eddICD10_11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label19")]
        private Label Label19 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label20")]
        private Label Label20 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label21")]
        private Label Label21 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_02")]
        private ExtendedDropdown eddICD10_02 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_09")]
        private ExtendedDropdown eddICD10_09 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label22")]
        private Label Label22 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_04")]
        private ExtendedDropdown eddICD10_04 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label23")]
        private Label Label23 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label24")]
        private Label Label24 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_03")]
        private ExtendedDropdown eddICD10_03 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label25")]
        private Label Label25 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_01")]
        private ExtendedDropdown eddICD10_01 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label26")]
        private Label Label26 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label27")]
        private Label Label27 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridDetailsWriteoffBalance")]
        private ToolStripMenuItem tsmiGridDetailsWriteoffBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchWriteoffBalance")]
        internal virtual ToolStripMenuItem tsmiGridSearchWriteoffBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private static bool PagedGrids
        {
            get => 
                RegistrySettings.GetUserBool("FormInvoice:PagedGrids").GetValueOrDefault(false);
            set => 
                RegistrySettings.SetUserBool("FormInvoice:PagedGrids", value);
        }

        private static bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        private YesNoAny ArchivedFilter =>
            !this.mnuFilterArchived_Yes.Checked ? (!this.mnuFilterArchived_No.Checked ? YesNoAny.Any : YesNoAny.No) : YesNoAny.Yes;

        private bool ApprovedState
        {
            get => 
                !this.chbApproved.Enabled;
            set
            {
                this.chbApproved.Enabled = !value || Permissions.FormInvoice_Approved.Allow_ADD_EDIT;
                this.cmbCustomer.Enabled = !value & this.IsNew;
                this.cmbTaxRate.Enabled = !value;
                if (value)
                {
                    this.ControlInvoiceDetails1.AllowState = AllowStateEnum.AllowEdit00;
                }
                else
                {
                    this.ControlInvoiceDetails1.AllowState = AllowStateEnum.AllowAll;
                }
            }
        }

        protected override object ObjectID
        {
            get => 
                base.ObjectID;
            set
            {
                base.ObjectID = value;
                if (Versioned.IsNumeric(value))
                {
                    this.lblInvoiceID.Text = "Invoice # " + Conversions.ToString(Conversions.ToLong(value));
                }
                else
                {
                    this.lblInvoiceID.Text = "Invoice #";
                }
            }
        }

        protected override bool IsNew
        {
            get => 
                base.IsNew;
            set
            {
                base.IsNew = value;
                base.IsSaveAllowed = !value;
                this.cmbCustomer.Enabled = this.IsNew;
            }
        }

        protected object OrderID
        {
            get => 
                this.ControlInvoiceDetails1.OrderID;
            set
            {
                this.lnkOrderID.Links.Clear();
                if (!Versioned.IsNumeric(value))
                {
                    this.ControlInvoiceDetails1.OrderID = null;
                    this.lnkOrderID.Text = "Order #";
                }
                else
                {
                    this.ControlInvoiceDetails1.OrderID = new int?(Conversions.ToInteger(value));
                    StringBuilder builder = new StringBuilder("Order # ");
                    int length = builder.Length;
                    builder.Append(Conversions.ToInteger(value));
                    this.lnkOrderID.Links.Add(length, builder.Length - length, Conversions.ToInteger(value));
                    this.lnkOrderID.Text = builder.ToString();
                }
            }
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormInvoice._Closure$__ $I = new FormInvoice._Closure$__();
            public static Func<FormInvoice.DetailInfo, int> $I419-0;
            public static Func<FormInvoice.InvoiceInfo, int> $I422-0;

            internal int _Lambda$__419-0(FormInvoice.DetailInfo r) => 
                r.ID;

            internal int _Lambda$__422-0(FormInvoice.InvoiceInfo r) => 
                r.ID;
        }

        public class CustomerInsuranceTable : DataTable
        {
            public readonly DataColumn Col_ID;
            public readonly DataColumn Col_Name;
            public readonly DataColumn Col_Rank;

            public CustomerInsuranceTable() : base("tbl_customer_insurance")
            {
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_Name = base.Columns.Add("Name", typeof(string));
                this.Col_Rank = base.Columns.Add("Rank", typeof(int));
            }
        }

        private class DetailInfo
        {
            public DetailInfo()
            {
            }

            public DetailInfo(IDataRecord record)
            {
                this.ID = record.GetInt32("ID").Value;
                this.InvoiceID = record.GetInt32("InvoiceID").Value;
                this.OrderID = record.GetInt32("OrderID").Value;
                this.LastName = record.GetString("LastName");
                this.FirstName = record.GetString("FirstName");
                this.DateOfBirth = record.GetDateTime("DateOfBirth");
                this.AccountNumber = record.GetString("AccountNumber");
                this.DOSFrom = record.GetDateTime("DOSFrom");
                this.BillableAmount = record.GetDecimal("BillableAmount");
                this.BillingCode = record.GetString("BillingCode");
                this.CurrentPayer = record.GetString("CurrentPayer");
                this.Balance = record.GetDecimal("Balance");
                this.InsuranceCompany = record.GetString("InsuranceCompany");
            }

            public int ID { get; set; }

            public int InvoiceID { get; set; }

            public int OrderID { get; set; }

            public string LastName { get; set; }

            public string FirstName { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public string AccountNumber { get; set; }

            public DateTime? DOSFrom { get; set; }

            public decimal? BillableAmount { get; set; }

            public string BillingCode { get; set; }

            public string CurrentPayer { get; set; }

            public decimal? Balance { get; set; }

            public string InsuranceCompany { get; set; }
        }

        private class DetailsNavigatorEventsHandler : NavigatorEventsHandler
        {
            private readonly FormInvoice Form;

            public DetailsNavigatorEventsHandler(FormInvoice form)
            {
                if (form == null)
                {
                    throw new ArgumentNullException("form");
                }
                this.Form = form;
            }

            public override void CreateSource(object sender, CreateSourceEventArgs args)
            {
                args.Source = new List<FormInvoice.DetailInfo>().ToGridSource<FormInvoice.DetailInfo>(new string[0]);
            }

            private IEnumerable<FormInvoice.DetailInfo> FetchData(PagedFilter SearchTerms)
            {
                IEnumerable<FormInvoice.DetailInfo> enumerable;
                FormInvoice.QueryOptions options1 = new FormInvoice.QueryOptions();
                options1.FilterLocationID = Globals.LocationID;
                options1.FilterUserID = new short?(Globals.CompanyUserID);
                options1.FilterArchived = this.Form.ArchivedFilter;
                options1.SearchTerms = SearchTerms;
                FormInvoice.QueryOptions options = options1;
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    using (MySqlCommand command = new MySqlCommand(GetQuery(options), connection))
                    {
                        connection.Open();
                        enumerable = command.ExecuteList<FormInvoice.DetailInfo>((_Closure$__.$I12-0 == null) ? (_Closure$__.$I12-0 = new Func<IDataRecord, FormInvoice.DetailInfo>(_Closure$__.$I._Lambda$__12-0)) : _Closure$__.$I12-0);
                    }
                }
                return enumerable;
            }

            public override void FillSource(object sender, FillSourceEventArgs args)
            {
                ((ArrayGridSource<FormInvoice.DetailInfo>) args.Source).AppendRange(this.FetchData(null));
            }

            public override void FillSource(object sender, PagedFillSourceEventArgs args)
            {
                ((ArrayGridSource<FormInvoice.DetailInfo>) args.Source).AppendRange(this.FetchData(args.Filter));
            }

            public static string GetQuery(FormInvoice.QueryOptions options)
            {
                string str = !FormInvoice.IsDemoVersion ? "1 = 1" : "tbl_customer.ID BETWEEN 1 and 50";
                string str2 = (options.FilterLocationID == null) ? ((options.FilterUserID == null) ? "1 = 1" : ("tbl_order.LocationID IS NULL OR tbl_order.LocationID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + options.FilterUserID.Value.ToString() + ")")) : ("tbl_order.LocationID = " + options.FilterLocationID.Value.ToString());
                string str3 = (options.FilterArchived != YesNoAny.Yes) ? ((options.FilterArchived != YesNoAny.No) ? "1 = 1" : "tbl_invoice.Archived = 0") : "tbl_invoice.Archived = 1";
                StringBuilder builder = new StringBuilder();
                string[] textArray1 = new string[] { "SELECT\r\n  tbl_invoicedetails.ID\r\n, tbl_invoicedetails.InvoiceID\r\n, tbl_invoicedetails.OrderID\r\n, tbl_customer.LastName\r\n, tbl_customer.FirstName\r\n, tbl_customer.DateOfBirth\r\n, tbl_customer.AccountNumber\r\n, tbl_invoicedetails.DOSFrom\r\n, tbl_invoicedetails.BillableAmount\r\n, tbl_invoicedetails.BillingCode\r\n, tbl_invoicedetails.CurrentPayer\r\n, tbl_invoicedetails.BillableAmount - tbl_invoicedetails.PaymentAmount - tbl_invoicedetails.WriteoffAmount as Balance\r\n, IFNULL(tbl_insurancecompany.Name, '') as InsuranceCompany\r\nFROM tbl_invoicedetails\r\n     LEFT JOIN tbl_invoice ON tbl_invoicedetails.InvoiceID  = tbl_invoice.ID\r\n                          AND tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID\r\n     LEFT JOIN tbl_customer ON tbl_invoicedetails.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_order ON tbl_order.ID  = tbl_invoicedetails.OrderID\r\n                        AND tbl_order.CustomerID = tbl_invoicedetails.CustomerID\r\n     LEFT JOIN tbl_company ON tbl_company.ID = 1\r\n     LEFT JOIN tbl_insurancecompany ON tbl_invoicedetails.CurrentInsuranceCompanyID = tbl_insurancecompany.ID\r\nWHERE (", str3, ")\r\n  AND (", str, ")\r\n  AND (", str2, ")\r\n  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))\r\n" };
                builder.Append(string.Concat(textArray1));
                if ((options.SearchTerms != null) && !string.IsNullOrEmpty(options.SearchTerms.Filter))
                {
                    QueryExpression[] expressions = new QueryExpression[12];
                    expressions[0] = new QueryExpression("tbl_invoicedetails.InvoiceID", MySqlType.Int);
                    expressions[1] = new QueryExpression("tbl_invoicedetails.OrderID", MySqlType.Int);
                    expressions[2] = new QueryExpression("tbl_customer.LastName", MySqlType.VarChar);
                    expressions[3] = new QueryExpression("tbl_customer.FirstName", MySqlType.VarChar);
                    expressions[4] = new QueryExpression("tbl_customer.DateOfBirth", MySqlType.Date);
                    expressions[5] = new QueryExpression("tbl_customer.AccountNumber", MySqlType.VarChar);
                    expressions[6] = new QueryExpression("tbl_invoicedetails.DOSFrom", MySqlType.Date);
                    expressions[7] = new QueryExpression("tbl_invoicedetails.BillableAmount", MySqlType.Float);
                    expressions[8] = new QueryExpression("tbl_invoicedetails.BillingCode", MySqlType.VarChar);
                    expressions[9] = new QueryExpression("tbl_invoicedetails.CurrentPayer", MySqlType.VarChar);
                    expressions[10] = new QueryExpression("IFNULL(tbl_insurancecompany.Name, '')", MySqlType.VarChar);
                    expressions[11] = new QueryExpression("tbl_invoicedetails.BillableAmount - tbl_invoicedetails.PaymentAmount - tbl_invoicedetails.WriteoffAmount", MySqlType.Float);
                    string str4 = MySqlFilterUtilities.BuildFilter(expressions, options.SearchTerms.Filter);
                    if (!string.IsNullOrEmpty(str4))
                    {
                        builder.Append("  AND (").Append(str4).Append(")\r\n");
                    }
                }
                builder.Append("ORDER BY tbl_invoicedetails.InvoiceID DESC, tbl_invoicedetails.ID DESC\r\n");
                if (options.SearchTerms != null)
                {
                    builder.AppendFormat("LIMIT {0}, {1}", options.SearchTerms.Start, options.SearchTerms.Count);
                }
                return builder.ToString();
            }

            public override void InitializeAppearance(GridAppearanceBase appearance)
            {
                appearance.AutoGenerateColumns = false;
                appearance.Columns.Clear();
                appearance.MultiSelect = true;
                appearance.AddTextColumn("InvoiceID", "Invoice#", 50, appearance.IntegerStyle());
                appearance.AddTextColumn("OrderID", "Order#", 50, appearance.IntegerStyle());
                appearance.AddTextColumn("LastName", "Last Name", 80);
                appearance.AddTextColumn("FirstName", "First Name", 80);
                appearance.AddTextColumn("DateOfBirth", "Birthday", 80, appearance.DateStyle());
                appearance.AddTextColumn("AccountNumber", "Account#", 60);
                appearance.AddTextColumn("DOSFrom", "DOS From", 70, appearance.DateStyle());
                appearance.AddTextColumn("BillingCode", "B. Code", 60);
                appearance.AddTextColumn("CurrentPayer", "Payer", 0x2d);
                appearance.AddTextColumn("InsuranceCompany", "Ins. Co.", 80);
                appearance.AddTextColumn("BillableAmount", "Billable", 0x37, appearance.PriceStyle());
                appearance.AddTextColumn("Balance", "Balance", 0x37, appearance.PriceStyle());
                appearance.ContextMenuStrip = this.Form.cmsGridDetails;
            }

            public override void NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
            {
                FormInvoice.DetailInfo info = args.GridRow.Get<FormInvoice.DetailInfo>();
                if (info != null)
                {
                    this.Form.OpenObject(info.InvoiceID);
                    this.Form.ControlInvoiceDetails1.ShowDetails(info.ID);
                }
            }

            public override string Caption =>
                "Details";

            public override bool Switchable =>
                false;

            public override IEnumerable<string> TableNames =>
                new string[] { "tbl_invoicedetails", "tbl_invoice", "tbl_customer", "tbl_order", "tbl_insurancecompany", "tbl_company", "tbl_user_location" };

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly FormInvoice.DetailsNavigatorEventsHandler._Closure$__ $I = new FormInvoice.DetailsNavigatorEventsHandler._Closure$__();
                public static Func<IDataRecord, FormInvoice.DetailInfo> $I12-0;

                internal FormInvoice.DetailInfo _Lambda$__12-0(IDataRecord r) => 
                    new FormInvoice.DetailInfo(r);
            }
        }

        private class InvoiceInfo
        {
            public InvoiceInfo()
            {
            }

            public InvoiceInfo(IDataRecord record)
            {
                this.ID = record.GetInt32("ID").Value;
                this.OrderID = record.GetInt32("OrderID").Value;
                this.CustomerID = record.GetInt32("CustomerID").Value;
                this.LastName = record.GetString("LastName");
                this.FirstName = record.GetString("FirstName");
                this.DateOfBirth = record.GetDateTime("DateOfBirth");
                this.AccountNumber = record.GetString("AccountNumber");
                this.InvoiceDate = record.GetDateTime("InvoiceDate");
                this.SubmittedTo = record.GetString("SubmittedTo");
                this.SubmittedToCompany = record.GetString("SubmittedToCompany");
                this.SubmittedDate = record.GetDateTime("SubmittedDate");
            }

            public int ID { get; set; }

            public int OrderID { get; set; }

            public int CustomerID { get; set; }

            public string LastName { get; set; }

            public string FirstName { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public string AccountNumber { get; set; }

            public DateTime? InvoiceDate { get; set; }

            public string SubmittedTo { get; set; }

            public string SubmittedToCompany { get; set; }

            public DateTime? SubmittedDate { get; set; }
        }

        private class QueryOptions
        {
            public int? FilterLocationID;
            public short? FilterUserID;
            public YesNoAny FilterArchived;
            public PagedFilter SearchTerms;
        }

        private class SearchGridProcessor
        {
            private readonly string cnnString;
            private readonly int[] ids;

            public SearchGridProcessor(string cnnString, int[] ids)
            {
                this.cnnString = cnnString;
                this.ids = ids;
            }

            public void Archive(object sender, DoWorkEventArgs e)
            {
                if ((this.ids != null) && (this.ids.Length != 0))
                {
                    BackgroundWorker worker = (BackgroundWorker) sender;
                    using (MySqlConnection connection = new MySqlConnection(this.cnnString))
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.CommandText = "CALL InvoiceDetails_RecalculateInternals_Single(:ID, NULL)";
                            command.Parameters.Add("ID", MySqlType.Int);
                            using (MySqlCommand command2 = new MySqlCommand("", connection))
                            {
                                command2.CommandText = "UPDATE tbl_invoice SET Archived = 1\r\nWHERE IFNULL(Archived, 0) != 1\r\n  AND ID = :ID\r\n  AND NOT EXISTS (SELECT * FROM tbl_invoicedetails\r\n                  WHERE InvoiceID = :ID\r\n                    AND ABS(BillableAmount - PaymentAmount - WriteoffAmount) >= 0.01)";
                                command2.Parameters.Add("ID", MySqlType.Int);
                                connection.Open();
                                int num = Math.Max(1, this.ids.Length / 50);
                                int num2 = 0;
                                int num3 = this.ids.Length - 1;
                                for (int i = 0; (i <= num3) && !worker.CancellationPending; i++)
                                {
                                    command.Parameters["ID"].Value = this.ids[i];
                                    command.ExecuteNonQuery();
                                    command2.Parameters["ID"].Value = this.ids[i];
                                    num2 += command2.ExecuteNonQuery();
                                    if ((i % num) == 0)
                                    {
                                        worker.ReportProgress((int) Math.Round((double) ((100.0 * (i + 1)) / ((double) this.ids.Length))), $"{num2} invoices archived");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public void Unarchive(object sender, DoWorkEventArgs e)
            {
                if ((this.ids != null) && (this.ids.Length != 0))
                {
                    BackgroundWorker worker = (BackgroundWorker) sender;
                    using (MySqlConnection connection = new MySqlConnection(this.cnnString))
                    {
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.CommandText = "UPDATE tbl_invoice SET Archived = 0\r\nWHERE IFNULL(Archived, 0) != 0\r\n  AND ID = :ID";
                            command.Parameters.Add("ID", MySqlType.Int);
                            connection.Open();
                            int num = Math.Max(1, this.ids.Length / 50);
                            int num2 = 0;
                            int num3 = this.ids.Length - 1;
                            for (int i = 0; (i <= num3) && !worker.CancellationPending; i++)
                            {
                                command.Parameters["ID"].Value = this.ids[i];
                                num2 += command.ExecuteNonQuery();
                                if ((i % num) == 0)
                                {
                                    worker.ReportProgress((int) Math.Round((double) ((100.0 * (i + 1)) / ((double) this.ids.Length))), $"{num2} invoices unarchived");
                                }
                            }
                        }
                    }
                }
            }
        }

        private class SearchNavigatorEventsHandler : NavigatorEventsHandler
        {
            private readonly FormInvoice Form;

            public SearchNavigatorEventsHandler(FormInvoice form)
            {
                if (form == null)
                {
                    throw new ArgumentNullException("form");
                }
                this.Form = form;
            }

            public override void CreateSource(object sender, CreateSourceEventArgs args)
            {
                args.Source = new List<FormInvoice.InvoiceInfo>().ToGridSource<FormInvoice.InvoiceInfo>(new string[0]);
            }

            private IEnumerable<FormInvoice.InvoiceInfo> FetchData(PagedFilter SearchTerms)
            {
                IEnumerable<FormInvoice.InvoiceInfo> enumerable;
                FormInvoice.QueryOptions options1 = new FormInvoice.QueryOptions();
                options1.FilterLocationID = Globals.LocationID;
                options1.FilterUserID = new short?(Globals.CompanyUserID);
                options1.FilterArchived = this.Form.ArchivedFilter;
                options1.SearchTerms = SearchTerms;
                FormInvoice.QueryOptions options = options1;
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    MySqlCommand command1 = new MySqlCommand(GetQuery(options), connection);
                    command1.FetchAll = true;
                    using (MySqlCommand command = command1)
                    {
                        connection.Open();
                        enumerable = command.ExecuteList<FormInvoice.InvoiceInfo>((_Closure$__.$I8-0 == null) ? (_Closure$__.$I8-0 = new Func<IDataRecord, FormInvoice.InvoiceInfo>(_Closure$__.$I._Lambda$__8-0)) : _Closure$__.$I8-0);
                    }
                }
                return enumerable;
            }

            public override void FillSource(object sender, FillSourceEventArgs args)
            {
                ((ArrayGridSource<FormInvoice.InvoiceInfo>) args.Source).AppendRange(this.FetchData(null));
            }

            public override void FillSource(object sender, PagedFillSourceEventArgs args)
            {
                ((ArrayGridSource<FormInvoice.InvoiceInfo>) args.Source).AppendRange(this.FetchData(args.Filter));
            }

            public static string GetQuery(FormInvoice.QueryOptions Options)
            {
                string str = !FormInvoice.IsDemoVersion ? "1 = 1" : "tbl_customer.ID BETWEEN 1 and 50";
                string str2 = (Options.FilterLocationID == null) ? ((Options.FilterUserID == null) ? "1 = 1" : ("tbl_order.LocationID IS NULL OR tbl_order.LocationID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + Options.FilterUserID.Value.ToString() + ")")) : ("tbl_order.LocationID = " + Options.FilterLocationID.Value.ToString());
                string str3 = (Options.FilterArchived != YesNoAny.Yes) ? ((Options.FilterArchived != YesNoAny.No) ? "1 = 1" : "tbl_invoice.Archived = 0") : "tbl_invoice.Archived = 1";
                StringBuilder builder = new StringBuilder();
                string[] textArray1 = new string[] { "SELECT\r\n  ID\r\n, OrderID\r\n, CustomerID\r\n, LastName\r\n, FirstName\r\n, DateOfBirth\r\n, AccountNumber\r\n, InvoiceDate\r\n, SubmittedTo\r\n, SubmittedToCompany\r\n, SubmittedDate\r\nFROM (\r\n  SELECT\r\n    tbl_invoicedetails.InvoiceID as ID\r\n  , tbl_invoicedetails.OrderID\r\n  , tbl_invoicedetails.CustomerID\r\n  , tbl_customer.LastName\r\n  , tbl_customer.FirstName\r\n  , tbl_customer.DateOfBirth\r\n  , tbl_customer.AccountNumber\r\n  , tbl_invoice.InvoiceDate\r\n  , MIN(tbl_invoicedetails.CurrentPayer) as SubmittedTo\r\n  , CASE WHEN Min(tbl_invoicedetails.CurrentPayer) = 'Ins1' THEN insco1.Name\r\n         WHEN Min(tbl_invoicedetails.CurrentPayer) = 'Ins2' THEN insco2.Name\r\n         WHEN Min(tbl_invoicedetails.CurrentPayer) = 'Ins3' THEN insco3.Name\r\n         WHEN Min(tbl_invoicedetails.CurrentPayer) = 'Ins4' THEN insco4.Name\r\n         ELSE '' END as SubmittedToCompany\r\n  , MAX(tbl_invoicedetails.SubmittedDate) as SubmittedDate\r\n  FROM tbl_invoice\r\n       LEFT JOIN tbl_order ON tbl_order.ID  = tbl_invoice.OrderID\r\n                          AND tbl_order.CustomerID = tbl_invoice.CustomerID\r\n       LEFT JOIN tbl_customer_insurance as policy1 ON policy1.ID         = tbl_invoice.CustomerInsurance1_ID\r\n                                                  AND policy1.CustomerID = tbl_invoice.CustomerID\r\n       LEFT JOIN tbl_customer_insurance as policy2 ON policy2.ID         = tbl_invoice.CustomerInsurance2_ID\r\n                                                  AND policy2.CustomerID = tbl_invoice.CustomerID\r\n       LEFT JOIN tbl_customer_insurance as policy3 ON policy3.ID         = tbl_invoice.CustomerInsurance3_ID\r\n                                                  AND policy3.CustomerID = tbl_invoice.CustomerID\r\n       LEFT JOIN tbl_customer_insurance as policy4 ON policy4.ID         = tbl_invoice.CustomerInsurance4_ID\r\n                                                  AND policy4.CustomerID = tbl_invoice.CustomerID\r\n       LEFT JOIN tbl_insurancecompany as insco1 ON insco1.ID = policy1.InsuranceCompanyID\r\n       LEFT JOIN tbl_insurancecompany as insco2 ON insco2.ID = policy2.InsuranceCompanyID\r\n       LEFT JOIN tbl_insurancecompany as insco3 ON insco3.ID = policy3.InsuranceCompanyID\r\n       LEFT JOIN tbl_insurancecompany as insco4 ON insco4.ID = policy4.InsuranceCompanyID\r\n       LEFT JOIN tbl_invoicedetails ON tbl_invoicedetails.InvoiceID  = tbl_invoice.ID\r\n                                   AND tbl_invoicedetails.CustomerID = tbl_invoice.CustomerID\r\n       LEFT JOIN tbl_customer ON tbl_invoice.CustomerID = tbl_customer.ID\r\n       LEFT JOIN tbl_company ON tbl_company.ID = 1\r\n  WHERE (", str3, ")\r\n    AND (", str, ")\r\n    AND (", str2, ")\r\n    AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))\r\n  GROUP BY tbl_invoicedetails.InvoiceID, tbl_customer.LastName, tbl_customer.FirstName, tbl_invoice.InvoiceDate, insco1.Name, insco2.Name, insco3.Name, insco4.Name\r\n) as tmp\r\nWHERE (1 = 1)\r\n" };
                builder.Append(string.Concat(textArray1));
                if ((Options.SearchTerms != null) && !string.IsNullOrEmpty(Options.SearchTerms.Filter))
                {
                    QueryExpression[] expressions = new QueryExpression[9];
                    expressions[0] = new QueryExpression("ID", MySqlType.Int);
                    expressions[1] = new QueryExpression("LastName", MySqlType.VarChar);
                    expressions[2] = new QueryExpression("FirstName", MySqlType.VarChar);
                    expressions[3] = new QueryExpression("DateOfBirth", MySqlType.Date);
                    expressions[4] = new QueryExpression("AccountNumber", MySqlType.VarChar);
                    expressions[5] = new QueryExpression("InvoiceDate", MySqlType.Date);
                    expressions[6] = new QueryExpression("SubmittedTo", MySqlType.VarChar);
                    expressions[7] = new QueryExpression("SubmittedToCompany", MySqlType.VarChar);
                    expressions[8] = new QueryExpression("SubmittedDate", MySqlType.Date);
                    string str4 = MySqlFilterUtilities.BuildFilter(expressions, Options.SearchTerms.Filter);
                    if (!string.IsNullOrEmpty(str4))
                    {
                        builder.Append("  AND (").Append(str4).Append(")\r\n");
                    }
                }
                builder.Append("ORDER BY ID DESC\r\n");
                if (Options.SearchTerms != null)
                {
                    builder.AppendFormat("LIMIT {0}, {1}", Options.SearchTerms.Start, Options.SearchTerms.Count);
                }
                return builder.ToString();
            }

            public override void InitializeAppearance(GridAppearanceBase appearance)
            {
                appearance.AutoGenerateColumns = false;
                appearance.Columns.Clear();
                appearance.MultiSelect = true;
                appearance.AddTextColumn("ID", "Invoice#", 50, appearance.IntegerStyle());
                appearance.AddTextColumn("LastName", "Last Name", 80);
                appearance.AddTextColumn("FirstName", "First Name", 80);
                appearance.AddTextColumn("DateOfBirth", "Birthday", 80, appearance.DateStyle());
                appearance.AddTextColumn("AccountNumber", "Account#", 60);
                appearance.AddTextColumn("InvoiceDate", "Inv. Date", 70, appearance.DateStyle());
                appearance.AddTextColumn("SubmittedTo", "Payer", 0x2d);
                appearance.AddTextColumn("SubmittedToCompany", "Ins. Co.", 120);
                appearance.AddTextColumn("SubmittedDate", "Submitted", 70, appearance.DateStyle());
                appearance.ContextMenuStrip = this.Form.cmsGridSearch;
            }

            public override void NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
            {
                _Closure$__10-0 e$__- = new _Closure$__10-0 {
                    $VB$Local_args = args
                };
                this.Form.OpenObject(new Func<object>(e$__-._Lambda$__0));
            }

            public override IEnumerable<string> TableNames =>
                new string[] { "tbl_invoice", "tbl_order", "tbl_customer_insurance", "tbl_insurancecompany", "tbl_invoicedetails", "tbl_customer", "tbl_company", "tbl_user_location" };

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly FormInvoice.SearchNavigatorEventsHandler._Closure$__ $I = new FormInvoice.SearchNavigatorEventsHandler._Closure$__();
                public static Func<IDataRecord, FormInvoice.InvoiceInfo> $I8-0;

                internal FormInvoice.InvoiceInfo _Lambda$__8-0(IDataRecord r) => 
                    new FormInvoice.InvoiceInfo(r);
            }

            [CompilerGenerated]
            internal sealed class _Closure$__10-0
            {
                public NavigatorRowClickEventArgs $VB$Local_args;

                internal object _Lambda$__0() => 
                    this.$VB$Local_args.GridRow.Get<FormInvoice.InvoiceInfo>().ID;
            }
        }
    }
}

