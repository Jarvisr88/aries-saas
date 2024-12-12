namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormRetailSales : DmeForm
    {
        private IContainer components;
        private static CultureInfo invariant = CultureInfo.InvariantCulture;
        private object FCustomerInsurance1_ID;
        private object FCustomerInsurance2_ID;
        private object FCustomerInsurance3_ID;
        private object FCustomerInsurance4_ID;

        public FormRetailSales()
        {
            base.Load += new EventHandler(this.FormOrder_Load);
            this.InitializeComponent();
            this.ControlRetailSalesDetails1.DefaultDOSFrom = DateTime.Today;
            this.ControlRetailSalesDetails1.btnSave.Click += new EventHandler(this.mnuSave_Click);
            this.ControlRetailSalesDetails1.btnScan.Click += new EventHandler(this.mnuScan_Click);
            this.ControlRetailSalesDetails1.btnStart.Click += new EventHandler(this.mnuStart_Click);
        }

        private void CalculateTotal()
        {
            double subTotal = 0.0;
            double taxTotal = 0.0;
            double disTotal = 0.0;
            this.ControlRetailSalesDetails1.CalculateTotal(NullableConvert.ToDouble(this.txtTaxPercent.Text, 0.0), this.nmbDiscount.AsDouble.GetValueOrDefault(0.0), ref subTotal, ref disTotal, ref taxTotal);
            this.txtSubtotal.Text = subTotal.ToString("0.00");
            this.txtDiscountTotal.Text = disTotal.ToString("0.00");
            this.txtTaxTotal.Text = taxTotal.ToString("0.00");
            this.txtTotal.Text = ((subTotal - disTotal) + taxTotal).ToString("0.00");
        }

        private void ClearCustomer()
        {
            Functions.SetTextBoxText(this.caShip.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtTaxPercent, 0f);
            Functions.SetTextBoxText(this.txtTaxRate, "<NO TAX>");
            this.txtTaxRate.Tag = DBNull.Value;
            this.ClearCustomerInsurance();
        }

        private void ClearCustomerInsurance()
        {
            this.FCustomerInsurance1_ID = DBNull.Value;
            this.FCustomerInsurance2_ID = DBNull.Value;
            this.FCustomerInsurance3_ID = DBNull.Value;
            this.FCustomerInsurance4_ID = DBNull.Value;
        }

        private void ClearObject()
        {
            Functions.SetComboBoxValue(this.cmbCustomer, DBNull.Value);
            this.ClearCustomer();
            Functions.SetDateBoxValue(this.dtbSalesDate, DateTime.Today);
            Functions.SetTextBoxText(this.txtSoldBy, Globals.CompanyUserName);
            Functions.SetTextBoxText(this.txtInvoiceNumber, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbDiscount, DBNull.Value);
            this.ControlRetailSalesDetails1.ClearGrid_Delivery();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
                {
                    this.LoadCustomer(Conversions.ToInteger(this.cmbCustomer.SelectedValue));
                }
                else
                {
                    this.ClearCustomer();
                }
                this.CalculateTotal();
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

        private void ControlRetailSalesDetails1_Changed(object sender, EventArgs e)
        {
            this.CalculateTotal();
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

        private void FormOrder_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearObject();
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

        private static string GetExtraString(DialogEndSale.Parameters Results)
        {
            PaymentExtraData data1 = new PaymentExtraData();
            data1.AmountLeft = 0.0M;
            data1.Paid = 0.0M;
            data1.PaymentMethod = Results.PaymentMethod;
            data1.CheckNumber = Results.CheckNumber;
            data1.CheckDate = Results.CheckDate;
            data1.CheckBirthDate = Results.Birthdate;
            data1.CheckDriverLicense = Results.DriverLicenseNumber;
            data1.CreditCardNumber = Results.CreditCardNumber;
            data1.CreditCardExpirationDate = Results.ExpirationMonth;
            return data1.ToString();
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Panel2 = new Panel();
            this.GroupBox1 = new GroupBox();
            this.txtOrderNumber = new TextBox();
            this.lblOrderNumber = new Label();
            this.nmbDiscount = new NumericBox();
            this.lblSalesDate = new Label();
            this.dtbSalesDate = new UltraDateTimeEditor();
            this.lblSoldBy = new Label();
            this.txtSoldBy = new TextBox();
            this.txtInvoiceNumber = new TextBox();
            this.lblInvoiceNumber = new Label();
            this.lblDiscount = new Label();
            this.gbDiagnosis = new GroupBox();
            this.txtTaxPercent = new TextBox();
            this.txtTaxRate = new TextBox();
            this.lblTaxPercent = new Label();
            this.lblTaxRate = new Label();
            this.txtPhone = new TextBox();
            this.cmbCustomer = new Combobox();
            this.Label16 = new Label();
            this.caShip = new ControlAddress();
            this.Label5 = new Label();
            this.Panel1 = new Panel();
            this.lblDiscountTotal = new Label();
            this.txtDiscountTotal = new TextBox();
            this.lblTaxes = new Label();
            this.txtTotal = new TextBox();
            this.lblTotal = new Label();
            this.txtTaxTotal = new TextBox();
            this.lblSubtotal = new Label();
            this.txtSubtotal = new TextBox();
            this.ErrorProvider1 = new ErrorProvider(this.components);
            this.mnuRetail = new ToolStripMenuItem();
            this.mnuStart = new ToolStripMenuItem();
            this.mnuAdd = new ToolStripMenuItem();
            this.mnuDelete = new ToolStripMenuItem();
            this.mnuSave = new ToolStripMenuItem();
            this.mnuScan = new ToolStripMenuItem();
            this.MenuStrip1 = new MenuStrip();
            this.ControlRetailSalesDetails1 = new ControlRetailSalesDetails();
            this.gbUserFields = new GroupBox();
            this.txtUserField2 = new TextBox();
            this.txtUserField1 = new TextBox();
            this.lblUserField2 = new Label();
            this.lblUserField1 = new Label();
            this.Panel2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.gbDiagnosis.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((ISupportInitialize) this.ErrorProvider1).BeginInit();
            this.MenuStrip1.SuspendLayout();
            this.gbUserFields.SuspendLayout();
            base.SuspendLayout();
            this.Panel2.Controls.Add(this.GroupBox1);
            this.Panel2.Controls.Add(this.gbDiagnosis);
            this.Panel2.Dock = DockStyle.Top;
            this.Panel2.Location = new Point(0, 0x18);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new Size(680, 0xa8);
            this.Panel2.TabIndex = 0;
            this.GroupBox1.Controls.Add(this.txtOrderNumber);
            this.GroupBox1.Controls.Add(this.lblOrderNumber);
            this.GroupBox1.Controls.Add(this.nmbDiscount);
            this.GroupBox1.Controls.Add(this.lblSalesDate);
            this.GroupBox1.Controls.Add(this.dtbSalesDate);
            this.GroupBox1.Controls.Add(this.lblSoldBy);
            this.GroupBox1.Controls.Add(this.txtSoldBy);
            this.GroupBox1.Controls.Add(this.txtInvoiceNumber);
            this.GroupBox1.Controls.Add(this.lblInvoiceNumber);
            this.GroupBox1.Controls.Add(this.lblDiscount);
            this.GroupBox1.Location = new Point(8, 0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new Size(0xc0, 160);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.txtOrderNumber.BackColor = SystemColors.Window;
            this.txtOrderNumber.Location = new Point(80, 0x40);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.ReadOnly = true;
            this.txtOrderNumber.Size = new Size(0x68, 20);
            this.txtOrderNumber.TabIndex = 5;
            this.txtOrderNumber.TextAlign = HorizontalAlignment.Right;
            this.lblOrderNumber.BackColor = Color.Transparent;
            this.lblOrderNumber.Location = new Point(0, 0x40);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new Size(0x48, 0x15);
            this.lblOrderNumber.TabIndex = 4;
            this.lblOrderNumber.Text = "Order #";
            this.lblOrderNumber.TextAlign = ContentAlignment.MiddleRight;
            this.nmbDiscount.Location = new Point(80, 0x70);
            this.nmbDiscount.Name = "nmbDiscount";
            this.nmbDiscount.Size = new Size(0x68, 0x15);
            this.nmbDiscount.TabIndex = 9;
            this.lblSalesDate.BackColor = Color.Transparent;
            this.lblSalesDate.Location = new Point(0, 0x10);
            this.lblSalesDate.Name = "lblSalesDate";
            this.lblSalesDate.Size = new Size(0x48, 0x15);
            this.lblSalesDate.TabIndex = 0;
            this.lblSalesDate.Text = "Sales date";
            this.lblSalesDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbSalesDate.DateTime = new DateTime(0x7d9, 3, 0x13, 0, 0, 0, 0);
            this.dtbSalesDate.Location = new Point(80, 0x10);
            this.dtbSalesDate.Name = "dtbSalesDate";
            this.dtbSalesDate.Size = new Size(0x68, 0x15);
            this.dtbSalesDate.TabIndex = 1;
            this.dtbSalesDate.Value = new DateTime(0x7d9, 3, 0x13, 0, 0, 0, 0);
            this.lblSoldBy.BackColor = Color.Transparent;
            this.lblSoldBy.Location = new Point(0, 40);
            this.lblSoldBy.Name = "lblSoldBy";
            this.lblSoldBy.Size = new Size(0x48, 0x15);
            this.lblSoldBy.TabIndex = 2;
            this.lblSoldBy.Text = "Sold By";
            this.lblSoldBy.TextAlign = ContentAlignment.MiddleRight;
            this.txtSoldBy.Location = new Point(80, 40);
            this.txtSoldBy.Name = "txtSoldBy";
            this.txtSoldBy.Size = new Size(0x68, 20);
            this.txtSoldBy.TabIndex = 3;
            this.txtInvoiceNumber.BackColor = SystemColors.Window;
            this.txtInvoiceNumber.Location = new Point(80, 0x58);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.ReadOnly = true;
            this.txtInvoiceNumber.Size = new Size(0x68, 20);
            this.txtInvoiceNumber.TabIndex = 7;
            this.txtInvoiceNumber.TextAlign = HorizontalAlignment.Right;
            this.lblInvoiceNumber.BackColor = Color.Transparent;
            this.lblInvoiceNumber.Location = new Point(0, 0x58);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new Size(0x48, 0x15);
            this.lblInvoiceNumber.TabIndex = 6;
            this.lblInvoiceNumber.Text = "Invoice #";
            this.lblInvoiceNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblDiscount.BackColor = Color.Transparent;
            this.lblDiscount.Location = new Point(0, 0x70);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new Size(0x48, 0x15);
            this.lblDiscount.TabIndex = 8;
            this.lblDiscount.Text = "Discount (%)";
            this.lblDiscount.TextAlign = ContentAlignment.MiddleRight;
            this.gbDiagnosis.Controls.Add(this.txtTaxPercent);
            this.gbDiagnosis.Controls.Add(this.txtTaxRate);
            this.gbDiagnosis.Controls.Add(this.lblTaxPercent);
            this.gbDiagnosis.Controls.Add(this.lblTaxRate);
            this.gbDiagnosis.Controls.Add(this.txtPhone);
            this.gbDiagnosis.Controls.Add(this.cmbCustomer);
            this.gbDiagnosis.Controls.Add(this.Label16);
            this.gbDiagnosis.Controls.Add(this.caShip);
            this.gbDiagnosis.Controls.Add(this.Label5);
            this.gbDiagnosis.Location = new Point(0xd0, 0);
            this.gbDiagnosis.Name = "gbDiagnosis";
            this.gbDiagnosis.Size = new Size(0x1d0, 160);
            this.gbDiagnosis.TabIndex = 1;
            this.gbDiagnosis.TabStop = false;
            this.gbDiagnosis.Text = "Customer";
            this.txtTaxPercent.BackColor = SystemColors.Window;
            this.txtTaxPercent.Location = new Point(280, 0x88);
            this.txtTaxPercent.Name = "txtTaxPercent";
            this.txtTaxPercent.ReadOnly = true;
            this.txtTaxPercent.Size = new Size(0x48, 20);
            this.txtTaxPercent.TabIndex = 8;
            this.txtTaxPercent.TextAlign = HorizontalAlignment.Right;
            this.txtTaxRate.BackColor = SystemColors.Window;
            this.txtTaxRate.Location = new Point(80, 0x88);
            this.txtTaxRate.Name = "txtTaxRate";
            this.txtTaxRate.ReadOnly = true;
            this.txtTaxRate.Size = new Size(160, 20);
            this.txtTaxRate.TabIndex = 6;
            this.lblTaxPercent.Location = new Point(0x100, 0x88);
            this.lblTaxPercent.Name = "lblTaxPercent";
            this.lblTaxPercent.Size = new Size(0x10, 0x15);
            this.lblTaxPercent.TabIndex = 7;
            this.lblTaxPercent.Text = "%";
            this.lblTaxPercent.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTaxRate.Location = new Point(0x10, 0x88);
            this.lblTaxRate.Name = "lblTaxRate";
            this.lblTaxRate.Size = new Size(0x38, 0x15);
            this.lblTaxRate.TabIndex = 5;
            this.lblTaxRate.Text = "Tax Rate";
            this.lblTaxRate.TextAlign = ContentAlignment.MiddleRight;
            this.txtPhone.Location = new Point(80, 0x70);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(0x110, 20);
            this.txtPhone.TabIndex = 4;
            this.cmbCustomer.Location = new Point(80, 0x10);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0x110, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.Label16.BackColor = Color.Transparent;
            this.Label16.Location = new Point(0x10, 0x70);
            this.Label16.Name = "Label16";
            this.Label16.Size = new Size(0x38, 0x15);
            this.Label16.TabIndex = 3;
            this.Label16.Text = "Phone";
            this.Label16.TextAlign = ContentAlignment.MiddleRight;
            this.caShip.BackColor = SystemColors.Control;
            this.caShip.Location = new Point(8, 40);
            this.caShip.Name = "caShip";
            this.caShip.Size = new Size(0x158, 0x48);
            this.caShip.TabIndex = 2;
            this.Label5.BackColor = Color.Transparent;
            this.Label5.Location = new Point(0x10, 0x10);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x38, 0x15);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Customer";
            this.Label5.TextAlign = ContentAlignment.MiddleRight;
            this.Panel1.Controls.Add(this.lblDiscountTotal);
            this.Panel1.Controls.Add(this.txtDiscountTotal);
            this.Panel1.Controls.Add(this.lblTaxes);
            this.Panel1.Controls.Add(this.txtTotal);
            this.Panel1.Controls.Add(this.lblTotal);
            this.Panel1.Controls.Add(this.txtTaxTotal);
            this.Panel1.Controls.Add(this.lblSubtotal);
            this.Panel1.Controls.Add(this.txtSubtotal);
            this.Panel1.Dock = DockStyle.Bottom;
            this.Panel1.Location = new Point(0, 480);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(680, 0x25);
            this.Panel1.TabIndex = 2;
            this.lblDiscountTotal.Location = new Point(0xa8, 8);
            this.lblDiscountTotal.Name = "lblDiscountTotal";
            this.lblDiscountTotal.Size = new Size(60, 0x15);
            this.lblDiscountTotal.TabIndex = 2;
            this.lblDiscountTotal.Text = "Discount";
            this.lblDiscountTotal.TextAlign = ContentAlignment.MiddleRight;
            this.txtDiscountTotal.BackColor = SystemColors.Window;
            this.txtDiscountTotal.Location = new Point(240, 8);
            this.txtDiscountTotal.Name = "txtDiscountTotal";
            this.txtDiscountTotal.ReadOnly = true;
            this.txtDiscountTotal.Size = new Size(0x48, 20);
            this.txtDiscountTotal.TabIndex = 3;
            this.txtDiscountTotal.TextAlign = HorizontalAlignment.Right;
            this.lblTaxes.Location = new Point(0x148, 8);
            this.lblTaxes.Name = "lblTaxes";
            this.lblTaxes.Size = new Size(60, 0x15);
            this.lblTaxes.TabIndex = 4;
            this.lblTaxes.Text = "Tax Total";
            this.lblTaxes.TextAlign = ContentAlignment.MiddleRight;
            this.txtTotal.BackColor = SystemColors.Window;
            this.txtTotal.Location = new Point(560, 8);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new Size(0x48, 20);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.TextAlign = HorizontalAlignment.Right;
            this.lblTotal.Location = new Point(0x1e8, 8);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new Size(60, 0x15);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Total Due";
            this.lblTotal.TextAlign = ContentAlignment.MiddleRight;
            this.txtTaxTotal.BackColor = SystemColors.Window;
            this.txtTaxTotal.Location = new Point(400, 8);
            this.txtTaxTotal.Name = "txtTaxTotal";
            this.txtTaxTotal.ReadOnly = true;
            this.txtTaxTotal.Size = new Size(0x48, 20);
            this.txtTaxTotal.TabIndex = 5;
            this.txtTaxTotal.TextAlign = HorizontalAlignment.Right;
            this.lblSubtotal.Location = new Point(8, 8);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new Size(60, 0x15);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Sub-total";
            this.lblSubtotal.TextAlign = ContentAlignment.MiddleRight;
            this.txtSubtotal.BackColor = SystemColors.Window;
            this.txtSubtotal.Location = new Point(80, 8);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new Size(0x48, 20);
            this.txtSubtotal.TabIndex = 1;
            this.txtSubtotal.TextAlign = HorizontalAlignment.Right;
            this.ErrorProvider1.ContainerControl = this;
            this.ErrorProvider1.DataMember = "";
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.mnuStart, this.mnuAdd, this.mnuDelete, this.mnuSave, this.mnuScan };
            this.mnuRetail.DropDownItems.AddRange(toolStripItems);
            this.mnuRetail.Name = "mnuRetail";
            this.mnuRetail.Size = new Size(0x30, 20);
            this.mnuRetail.Text = "Retail";
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.ShortcutKeys = Keys.F2;
            this.mnuStart.Size = new Size(0x7e, 0x16);
            this.mnuStart.Text = "Start";
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.ShortcutKeys = Keys.F3;
            this.mnuAdd.Size = new Size(0x7e, 0x16);
            this.mnuAdd.Text = "Add";
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.ShortcutKeys = Keys.F4;
            this.mnuDelete.Size = new Size(0x7e, 0x16);
            this.mnuDelete.Text = "Delete";
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeys = Keys.F6;
            this.mnuSave.Size = new Size(0x7e, 0x16);
            this.mnuSave.Text = "Save";
            this.mnuScan.Name = "mnuScan";
            this.mnuScan.ShortcutKeys = Keys.F8;
            this.mnuScan.Size = new Size(0x7e, 0x16);
            this.mnuScan.Text = "Scan";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.mnuRetail };
            this.MenuStrip1.Items.AddRange(itemArray2);
            this.MenuStrip1.Location = new Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new Size(680, 0x18);
            this.MenuStrip1.TabIndex = 4;
            this.MenuStrip1.Text = "MenuStrip1";
            this.ControlRetailSalesDetails1.Dock = DockStyle.Fill;
            this.ControlRetailSalesDetails1.Location = new Point(0, 0xc0);
            this.ControlRetailSalesDetails1.Name = "ControlRetailSalesDetails1";
            this.ControlRetailSalesDetails1.Size = new Size(680, 0x120);
            this.ControlRetailSalesDetails1.TabIndex = 3;
            this.gbUserFields.Controls.Add(this.txtUserField2);
            this.gbUserFields.Controls.Add(this.txtUserField1);
            this.gbUserFields.Controls.Add(this.lblUserField2);
            this.gbUserFields.Controls.Add(this.lblUserField1);
            this.gbUserFields.Dock = DockStyle.Bottom;
            this.gbUserFields.Location = new Point(0, 0x205);
            this.gbUserFields.Name = "gbUserFields";
            this.gbUserFields.Size = new Size(680, 0x2c);
            this.gbUserFields.TabIndex = 5;
            this.gbUserFields.TabStop = false;
            this.gbUserFields.Text = "User Fields";
            this.txtUserField2.Location = new Point(0x198, 0x10);
            this.txtUserField2.MaxLength = 100;
            this.txtUserField2.Name = "txtUserField2";
            this.txtUserField2.Size = new Size(0x108, 20);
            this.txtUserField2.TabIndex = 3;
            this.txtUserField1.Location = new Point(0x40, 0x10);
            this.txtUserField1.MaxLength = 100;
            this.txtUserField1.Name = "txtUserField1";
            this.txtUserField1.Size = new Size(0x108, 20);
            this.txtUserField1.TabIndex = 1;
            this.lblUserField2.Location = new Point(0x160, 0x10);
            this.lblUserField2.Name = "lblUserField2";
            this.lblUserField2.Size = new Size(0x30, 0x15);
            this.lblUserField2.TabIndex = 2;
            this.lblUserField2.Text = "Field #2";
            this.lblUserField2.TextAlign = ContentAlignment.MiddleRight;
            this.lblUserField1.Location = new Point(8, 0x10);
            this.lblUserField1.Name = "lblUserField1";
            this.lblUserField1.Size = new Size(0x30, 0x15);
            this.lblUserField1.TabIndex = 0;
            this.lblUserField1.Text = "Field #1";
            this.lblUserField1.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(680, 0x231);
            base.Controls.Add(this.ControlRetailSalesDetails1);
            base.Controls.Add(this.Panel1);
            base.Controls.Add(this.Panel2);
            base.Controls.Add(this.MenuStrip1);
            base.Controls.Add(this.gbUserFields);
            base.MainMenuStrip = this.MenuStrip1;
            base.Name = "FormRetailSales";
            this.Text = "Retail Sales";
            this.Panel2.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.gbDiagnosis.ResumeLayout(false);
            this.gbDiagnosis.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((ISupportInitialize) this.ErrorProvider1).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.gbUserFields.ResumeLayout(false);
            this.gbUserFields.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadCustomer(int CustomerID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                this.LoadCustomer(connection, CustomerID);
            }
        }

        private void LoadCustomer(MySqlConnection cnn, int CustomerID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                this.ClearCustomerInsurance();
                using (MySqlCommand command = new MySqlCommand("", cnn))
                {
                    command.CommandText = $"SELECT IF(tbl_customer.ShipActive, tbl_customer.ShipAddress1, tbl_customer.Address1) as Address1,
       IF(tbl_customer.ShipActive, tbl_customer.ShipAddress2, tbl_customer.Address2) as Address2,
       IF(tbl_customer.ShipActive, tbl_customer.ShipCity,     tbl_customer.City    ) as City,
       IF(tbl_customer.ShipActive, tbl_customer.ShipState,    tbl_customer.State   ) as State,
       IF(tbl_customer.ShipActive, tbl_customer.ShipZip,      tbl_customer.Zip     ) as Zip,
       tbl_customer.Phone,
       tbl_customer.ICD9_1,
       tbl_customer.ICD9_2,
       tbl_customer.ICD9_3,
       tbl_customer.ICD9_4,
       tbl_customer.ReferralID,
       tbl_customer.SalesRepID,
       tbl_taxrate.ID as TaxRateID,
       IFNULL(tbl_taxrate.Name, '<NO TAX>') as TaxRateName,
       IFNULL(tbl_taxrate.CountyTax, 0) + IFNULL(tbl_taxrate.CityTax, 0) +
       IFNULL(tbl_taxrate.StateTax, 0) + IFNULL(tbl_taxrate.OtherTax, 0) as TaxPercent
FROM tbl_customer
     LEFT JOIN tbl_taxrate ON tbl_customer.TaxRateID = tbl_taxrate.ID
WHERE (tbl_customer.ID = {CustomerID})";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Functions.SetTextBoxText(this.caShip.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.caShip.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.caShip.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.caShip.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.caShip.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtTaxPercent, reader["TaxPercent"]);
                            Functions.SetTextBoxText(this.txtTaxRate, reader["TaxRateName"]);
                            this.txtTaxRate.Tag = reader["TaxRateID"];
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                this.LoadCustomerInsurance(cnn, CustomerID);
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        private void LoadCustomerInsurance(MySqlConnection cnn, int CustomerID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                this.ClearCustomerInsurance();
                using (MySqlCommand command = new MySqlCommand("", cnn))
                {
                    command.CommandText = $"SELECT tbl_customer_insurance.ID
FROM tbl_customer_insurance
     LEFT JOIN tbl_insurancecompany ON tbl_customer_insurance.InsuranceCompanyID = tbl_insurancecompany.ID
WHERE (tbl_customer_insurance.CustomerID = {CustomerID})
ORDER BY `Rank`";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.FCustomerInsurance1_ID = reader["ID"];
                            if (reader.Read())
                            {
                                this.FCustomerInsurance2_ID = reader["ID"];
                                if (reader.Read())
                                {
                                    this.FCustomerInsurance3_ID = reader["ID"];
                                    if (reader.Read())
                                    {
                                        this.FCustomerInsurance4_ID = reader["ID"];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.ControlRetailSalesDetails1.btnAdd_Click(this, EventArgs.Empty);
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

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.ControlRetailSalesDetails1.btnDelete_Click(this, EventArgs.Empty);
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

        private void mnuSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateObject())
                {
                    StringBuilder builder = new StringBuilder("There are some errors in input data");
                    Functions.EnumerateErrors(this, this.ErrorProvider1, builder);
                    throw new UserNotifyException(builder.ToString());
                }
                using (DialogEndSale sale = new DialogEndSale())
                {
                    double num;
                    double num2;
                    double num3;
                    this.ControlRetailSalesDetails1.CalculateTotal(NullableConvert.ToDouble(this.txtTaxPercent.Text, 0.0), this.nmbDiscount.AsDouble.GetValueOrDefault(0.0), ref num, ref num2, ref num3);
                    sale.SetTotals(num, num2, num3);
                    if (sale.ShowDialog() == DialogResult.OK)
                    {
                        SaveObjectResult result = this.SaveObject(sale.Results);
                        this.txtOrderNumber.Text = result.OrderID.ToString();
                        this.txtInvoiceNumber.Text = result.InvoiceID.ToString();
                        this.ControlRetailSalesDetails1.btnSave.Enabled = false;
                        this.mnuSave.Enabled = false;
                        try
                        {
                            ClassGlobalObjects.OnPaymentAdded();
                        }
                        catch (Exception exception1)
                        {
                            Exception ex = exception1;
                            ProjectData.SetProjectError(ex);
                            Exception exception = ex;
                            this.ShowException(exception, "OnPaymentAdded");
                            ProjectData.ClearProjectError();
                        }
                        PrintRetailInvoice(sale.Results, result.InvoiceID);
                    }
                }
            }
            catch (Exception exception3)
            {
                Exception ex = exception3;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
        }

        private void mnuScan_Click(object sender, EventArgs e)
        {
            try
            {
                this.ControlRetailSalesDetails1.DoScan();
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

        private void mnuStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearObject();
                this.ControlRetailSalesDetails1.btnSave.Enabled = true;
                this.mnuSave.Enabled = true;
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

        private void nmbDiscount_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateTotal();
        }

        private void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_order", "tbl_orderdetails" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private static void PrintRetailInvoice(DialogEndSale.Parameters Results, int InvoiceID)
        {
            ReportParameters @params = new ReportParameters {
                ["{?tbl_invoice.ID}"] = InvoiceID,
                ["{?Payment Type}"] = Converter<PaymentMethod>.Default.ToString(Results.PaymentMethod),
                ["{?Amount Tendered}"] = NullableConvert.ToDb(Results.AmountTendered),
                ["{?Check Number}"] = Results.CheckNumber,
                ["{?Credit Card Number}"] = Results.CreditCardNumber
            };
            ClassGlobalObjects.ShowReport("RetailInvoice", @params, true);
        }

        private SaveObjectResult SaveObject(DialogEndSale.Parameters Results)
        {
            SaveObjectResult result;
            if (!Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
            {
                throw new UserNotifyException("You must select customer before saving.");
            }
            if (!(Functions.GetDateBoxValue(this.dtbSalesDate) is DateTime))
            {
                throw new UserNotifyException("You must set sales date");
            }
            DateTime salesDate = Conversions.ToDate(Functions.GetDateBoxValue(this.dtbSalesDate));
            int customerID = Conversions.ToInteger(this.cmbCustomer.SelectedValue);
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                MySqlTransaction tran = connection.BeginTransaction();
                try
                {
                    int num3;
                    int orderID = this.SaveOrder(connection, tran, customerID, salesDate);
                    using (MySqlCommand command = new MySqlCommand("", connection, tran))
                    {
                        command.CommandText = "CALL order_process(:P_OrderID, :P_BillingMonth, :P_BillingFlags, :P_InvoiceDate, @P_InvoiceID)";
                        command.Parameters.Add("P_OrderID", MySqlType.Int).Value = orderID;
                        command.Parameters.Add("P_BillingMonth", MySqlType.Int).Value = 1;
                        command.Parameters.Add("P_BillingFlags", MySqlType.Int).Value = 0;
                        command.Parameters.Add("P_InvoiceDate", MySqlType.Date).Value = DateTime.Today;
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand command2 = new MySqlCommand("", connection, tran))
                    {
                        command2.CommandText = "SELECT @P_InvoiceID";
                        int? nullable = NullableConvert.ToInt32(command2.ExecuteScalar());
                        if (nullable == null)
                        {
                            throw new UserNotifyException("System cannot generate invoice for this retail order");
                        }
                        num3 = nullable.Value;
                    }
                    using (MySqlCommand command3 = new MySqlCommand("", connection, tran))
                    {
                        command3.CommandText = "CALL Invoice_AddSubmitted(:InvoiceID, 'Patient', 'Paper', Null, :LastUpdateUserID)";
                        command3.Parameters.Add("InvoiceID", MySqlType.Int).Value = num3;
                        command3.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                        command3.ExecuteNonQuery();
                    }
                    using (MySqlCommand command4 = new MySqlCommand("", connection, tran))
                    {
                        command4.CommandText = "CALL retailinvoice_addpayments(:P_InvoiceID, :P_TransactionDate, :P_Extra, :P_LastUpdateUserID)";
                        command4.Parameters.Add("P_InvoiceID", MySqlType.Int).Value = num3;
                        command4.Parameters.Add("P_TransactionDate", MySqlType.Date).Value = salesDate;
                        command4.Parameters.Add("P_Extra", MySqlType.Text, 0x10000).Value = GetExtraString(Results);
                        command4.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                        command4.ExecuteNonQuery();
                    }
                    using (MySqlCommand command5 = new MySqlCommand("", connection, tran))
                    {
                        command5.CommandText = "CALL Invoice_UpdateBalance(:P_InvoiceID, true)";
                        command5.Parameters.Add("P_InvoiceID", MySqlType.Int).Value = num3;
                        command5.ExecuteNonQuery();
                    }
                    tran.Commit();
                    result = new SaveObjectResult(orderID, num3);
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    tran.Rollback();
                    throw;
                }
            }
            return result;
        }

        private int SaveOrder(MySqlConnection cnn, MySqlTransaction tran, int CustomerID, DateTime SalesDate)
        {
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.Transaction = tran;
            cmd.Parameters.Add("CustomerID", MySqlType.Int, 0).Value = CustomerID;
            cmd.Parameters.Add("Approved", MySqlType.Bit, 0).Value = true;
            cmd.Parameters.Add("OrderDate", MySqlType.Date, 0).Value = SalesDate;
            cmd.Parameters.Add("DeliveryDate", MySqlType.Date, 0).Value = SalesDate;
            cmd.Parameters.Add("BillDate", MySqlType.Date, 0).Value = SalesDate;
            cmd.Parameters.Add("EndDate", MySqlType.Date, 0).Value = SalesDate;
            cmd.Parameters.Add("UserField1", MySqlType.VarChar, 100).Value = this.txtUserField1.Text;
            cmd.Parameters.Add("UserField2", MySqlType.VarChar, 100).Value = this.txtUserField2.Text;
            cmd.Parameters.Add("ICD9_1", MySqlType.VarChar, 6).Value = "";
            cmd.Parameters.Add("ICD9_2", MySqlType.VarChar, 6).Value = "";
            cmd.Parameters.Add("ICD9_3", MySqlType.VarChar, 6).Value = "";
            cmd.Parameters.Add("ICD9_4", MySqlType.VarChar, 6).Value = "";
            cmd.Parameters.Add("ICD10_01", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_02", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_03", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_04", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_05", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_06", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_07", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_08", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_09", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_10", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_11", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("ICD10_12", MySqlType.VarChar, 10).Value = "";
            cmd.Parameters.Add("CustomerInsurance1_ID", MySqlType.Int, 0).Value = DBNull.Value;
            cmd.Parameters.Add("CustomerInsurance2_ID", MySqlType.Int, 0).Value = DBNull.Value;
            cmd.Parameters.Add("CustomerInsurance3_ID", MySqlType.Int, 0).Value = DBNull.Value;
            cmd.Parameters.Add("CustomerInsurance4_ID", MySqlType.Int, 0).Value = DBNull.Value;
            cmd.Parameters.Add("Discount", MySqlType.Double).Value = this.nmbDiscount.AsDouble.GetValueOrDefault(0.0);
            cmd.Parameters.Add("TakenBy", MySqlType.VarChar, 50).Value = this.txtSoldBy.Text;
            cmd.Parameters.Add("TakenAt", MySqlType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 0).Value = Globals.CompanyUserID;
            cmd.Parameters.Add("SaleType", MySqlType.VarChar, 50).Value = "Retail";
            cmd.ExecuteInsert("tbl_order");
            int lastIdentity = cmd.GetLastIdentity();
            this.ControlRetailSalesDetails1.SaveGrid(cnn, tran, lastIdentity, CustomerID);
            return lastIdentity;
        }

        private bool ValidateObject()
        {
            bool flag = true;
            if (Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
            {
                this.ErrorProvider1.SetError(this.cmbCustomer, "");
            }
            else
            {
                this.ErrorProvider1.SetError(this.cmbCustomer, "You must select customer");
                flag = false;
            }
            if (Functions.GetDateBoxValue(this.dtbSalesDate) is DateTime)
            {
                this.ErrorProvider1.SetError(this.dtbSalesDate, "");
            }
            else
            {
                this.ErrorProvider1.SetError(this.dtbSalesDate, "You must set sales date");
                flag = false;
            }
            return flag;
        }

        [field: AccessedThroughProperty("Panel2")]
        private Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbDiagnosis")]
        private GroupBox gbDiagnosis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caShip")]
        private ControlAddress caShip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider1")]
        private ErrorProvider ErrorProvider1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSalesDate")]
        private Label lblSalesDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbSalesDate")]
        private UltraDateTimeEditor dtbSalesDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label16")]
        private Label Label16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSoldBy")]
        private Label lblSoldBy { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoiceNumber")]
        private Label lblInvoiceNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDiscount")]
        private Label lblDiscount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSoldBy")]
        private TextBox txtSoldBy { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInvoiceNumber")]
        private TextBox txtInvoiceNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("GroupBox1")]
        private GroupBox GroupBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSubtotal")]
        private TextBox txtSubtotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSubtotal")]
        private Label lblSubtotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTotal")]
        private Label lblTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxes")]
        private Label lblTaxes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTotal")]
        private TextBox txtTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDiscount")]
        private NumericBox nmbDiscount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxRate")]
        private Label lblTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxPercent")]
        private TextBox txtTaxPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuRetail")]
        private ToolStripMenuItem mnuRetail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuAdd")]
        private ToolStripMenuItem mnuAdd { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuDelete")]
        private ToolStripMenuItem mnuDelete { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuSave")]
        private ToolStripMenuItem mnuSave { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuScan")]
        private ToolStripMenuItem mnuScan { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuStart")]
        private ToolStripMenuItem mnuStart { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxPercent")]
        private Label lblTaxPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxRate")]
        private TextBox txtTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDiscountTotal")]
        private Label lblDiscountTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDiscountTotal")]
        private TextBox txtDiscountTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxTotal")]
        private TextBox txtTaxTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOrderNumber")]
        private TextBox txtOrderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrderNumber")]
        private Label lblOrderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("MenuStrip1")]
        private MenuStrip MenuStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlRetailSalesDetails1")]
        private ControlRetailSalesDetails ControlRetailSalesDetails1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbUserFields")]
        protected virtual GroupBox gbUserFields { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUserField2")]
        protected virtual TextBox txtUserField2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUserField1")]
        protected virtual TextBox txtUserField1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUserField2")]
        protected virtual Label lblUserField2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUserField1")]
        protected virtual Label lblUserField1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public object CustomerID =>
            this.cmbCustomer.SelectedValue;

        private bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        [StructLayout(LayoutKind.Sequential)]
        private struct SaveObjectResult
        {
            public readonly int OrderID;
            public readonly int InvoiceID;
            public SaveObjectResult(int orderID, int invoiceID)
            {
                this = new FormRetailSales.SaveObjectResult();
                this.OrderID = orderID;
                this.InvoiceID = invoiceID;
            }
        }
    }
}

