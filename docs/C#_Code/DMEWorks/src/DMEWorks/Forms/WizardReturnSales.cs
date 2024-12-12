namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Core.Extensions;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class WizardReturnSales : DmeForm
    {
        private const string CrLf = "\r\n";
        private IContainer components;

        public WizardReturnSales()
        {
            base.Load += new EventHandler(this.FormWizard_Load);
            this.InitializeComponent();
            base.Size = new Size(0x1f8, 0x180);
            this.InitializeGrid(this.GridOrderItems.Appearance);
            this.CustomerAddress.ReadOnly = true;
        }

        private void AddStatusString(string Status)
        {
            ArrayList list = new ArrayList(this.txtResults.Lines) {
                Status
            };
            this.txtResults.Lines = (string[]) list.ToArray(typeof(string));
            Application.DoEvents();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.MovePrev();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.pnlButtons.Enabled = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    this.MoveNext();
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            finally
            {
                this.pnlButtons.Enabled = true;
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

        private void FormWizard_Load(object sender, EventArgs e)
        {
            this.SetActivePanel(this.GetFirstStage());
        }

        private Panel GetCurrentStage() => 
            !this.pnlIntro.Visible ? (!this.pnlOrder.Visible ? (!this.pnlOrderInfo.Visible ? (!this.pnlOrderItems.Visible ? (!this.pnlSummary.Visible ? (!this.pnlFinish.Visible ? null : this.pnlFinish) : this.pnlSummary) : this.pnlOrderItems) : this.pnlOrderInfo) : this.pnlOrder) : this.pnlIntro;

        private Panel GetFirstStage() => 
            this.pnlIntro;

        private Panel GetLastStage() => 
            this.pnlFinish;

        private Panel GetNextStage() => 
            !this.pnlIntro.Visible ? (!this.pnlOrder.Visible ? (!this.pnlOrderInfo.Visible ? (!this.pnlOrderItems.Visible ? (!this.pnlSummary.Visible ? (!this.pnlFinish.Visible ? null : this.pnlFinish) : this.pnlFinish) : this.pnlSummary) : this.pnlOrderItems) : this.pnlOrderInfo) : this.pnlOrder;

        private Panel GetPreviousStage()
        {
            Panel pnlIntro;
            if (this.pnlIntro.Visible)
            {
                pnlIntro = null;
            }
            else if (this.pnlOrder.Visible)
            {
                pnlIntro = this.pnlIntro;
            }
            else if (this.pnlOrderInfo.Visible)
            {
                pnlIntro = this.pnlOrder;
            }
            else if (this.pnlOrderItems.Visible)
            {
                pnlIntro = this.pnlOrderInfo;
            }
            else if (this.pnlSummary.Visible)
            {
                pnlIntro = this.pnlOrderItems;
            }
            else
            {
                bool visible = this.pnlFinish.Visible;
                pnlIntro = null;
            }
            return pnlIntro;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(WizardReturnSales));
            this.btnNext = new Button();
            this.btnBack = new Button();
            this.btnCancel = new Button();
            this.pnlIntro = new Panel();
            this.Label10 = new Label();
            this.Label9 = new Label();
            this.Label4 = new Label();
            this.Label2 = new Label();
            this.PictureBox1 = new PictureBox();
            this.Label7 = new Label();
            this.pnlOrder = new Panel();
            this.txtOrderID = new TextBox();
            this.rbInvoiceID = new RadioButton();
            this.rbOrderID = new RadioButton();
            this.Label5 = new Label();
            this.PictureBox4 = new PictureBox();
            this.txtInvoiceID = new TextBox();
            this.lblOrderNumber = new Label();
            this.txtOrderNumber = new TextBox();
            this.pnlOrderItems = new Panel();
            this.GridOrderItems = new FilteredGrid();
            this.PictureBox5 = new PictureBox();
            this.Label21 = new Label();
            this.pnlSummary = new Panel();
            this.Label15 = new Label();
            this.PictureBox2 = new PictureBox();
            this.txtSummary = new TextBox();
            this.pnlFinish = new Panel();
            this.txtResults = new TextBox();
            this.Label18 = new Label();
            this.Label17 = new Label();
            this.PictureBox3 = new PictureBox();
            this.pnlButtons = new Panel();
            this.pnlOrderInfo = new Panel();
            this.GroupBox1 = new GroupBox();
            this.nmbDiscount = new NumericBox();
            this.lblSalesDate = new Label();
            this.dtbSalesDate = new UltraDateTimeEditor();
            this.lblTakenBy = new Label();
            this.txtTakenBy = new TextBox();
            this.txtInvoiceNumber = new TextBox();
            this.lblInvoiceNumber = new Label();
            this.lblDiscount = new Label();
            this.gbDiagnosis = new GroupBox();
            this.txtCustomerName = new TextBox();
            this.txtTaxRatePercent = new TextBox();
            this.txtTaxRateName = new TextBox();
            this.lblTaxPercent = new Label();
            this.lblTaxRate = new Label();
            this.txtCustomerPhone = new TextBox();
            this.Label16 = new Label();
            this.CustomerAddress = new ControlAddress();
            this.Label6 = new Label();
            this.PictureBox6 = new PictureBox();
            this.pnlIntro.SuspendLayout();
            ((ISupportInitialize) this.PictureBox1).BeginInit();
            this.pnlOrder.SuspendLayout();
            ((ISupportInitialize) this.PictureBox4).BeginInit();
            this.pnlOrderItems.SuspendLayout();
            ((ISupportInitialize) this.PictureBox5).BeginInit();
            this.pnlSummary.SuspendLayout();
            ((ISupportInitialize) this.PictureBox2).BeginInit();
            this.pnlFinish.SuspendLayout();
            ((ISupportInitialize) this.PictureBox3).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.pnlOrderInfo.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.gbDiagnosis.SuspendLayout();
            ((ISupportInitialize) this.PictureBox6).BeginInit();
            base.SuspendLayout();
            this.btnNext.Location = new Point(0x148, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x48, 0x19);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next >>";
            this.btnBack.Location = new Point(0x100, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(0x48, 0x19);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<< Back";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x198, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x19);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.pnlIntro.BackColor = SystemColors.Control;
            this.pnlIntro.Controls.Add(this.Label10);
            this.pnlIntro.Controls.Add(this.Label9);
            this.pnlIntro.Controls.Add(this.Label4);
            this.pnlIntro.Controls.Add(this.Label2);
            this.pnlIntro.Controls.Add(this.PictureBox1);
            this.pnlIntro.Controls.Add(this.Label7);
            this.pnlIntro.Location = new Point(0, 0);
            this.pnlIntro.Name = "pnlIntro";
            this.pnlIntro.Size = new Size(0x1ec, 0x138);
            this.pnlIntro.TabIndex = 0;
            this.pnlIntro.Visible = false;
            this.Label10.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label10.Location = new Point(0xb0, 0x48);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(0x128, 0x18);
            this.Label10.TabIndex = 2;
            this.Label10.Text = "•   Return prevously sold items";
            this.Label9.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label9.Location = new Point(0xb0, 0x60);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x128, 0x18);
            this.Label9.TabIndex = 3;
            this.Label9.Text = "•   Return payments";
            this.Label4.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label4.Location = new Point(0xb0, 0x30);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x128, 0x18);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "This wizard helps you:";
            this.Label2.Location = new Point(0xb0, 0x120);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x80, 0x17);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "To continue, click next";
            this.PictureBox1.Dock = DockStyle.Left;
            this.PictureBox1.Image = (Image) manager.GetObject("PictureBox1.Image");
            this.PictureBox1.Location = new Point(0, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new Size(0xa4, 0x138);
            this.PictureBox1.TabIndex = 5;
            this.PictureBox1.TabStop = false;
            this.Label7.Font = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label7.Location = new Point(0xa8, 8);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(320, 0x18);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "Welcome to the Return Sales Wizard";
            this.pnlOrder.BackColor = SystemColors.Control;
            this.pnlOrder.Controls.Add(this.txtOrderID);
            this.pnlOrder.Controls.Add(this.rbInvoiceID);
            this.pnlOrder.Controls.Add(this.rbOrderID);
            this.pnlOrder.Controls.Add(this.Label5);
            this.pnlOrder.Controls.Add(this.PictureBox4);
            this.pnlOrder.Controls.Add(this.txtInvoiceID);
            this.pnlOrder.Location = new Point(0x1f0, 0);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new Size(0x1ec, 0x138);
            this.pnlOrder.TabIndex = 1;
            this.pnlOrder.Visible = false;
            this.txtOrderID.Location = new Point(0x68, 0x80);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new Size(0xa8, 0x15);
            this.txtOrderID.TabIndex = 2;
            this.rbInvoiceID.Location = new Point(0x10, 160);
            this.rbInvoiceID.Name = "rbInvoiceID";
            this.rbInvoiceID.Size = new Size(80, 0x18);
            this.rbInvoiceID.TabIndex = 3;
            this.rbInvoiceID.Text = "Invoice #";
            this.rbOrderID.Checked = true;
            this.rbOrderID.Location = new Point(0x10, 0x80);
            this.rbOrderID.Name = "rbOrderID";
            this.rbOrderID.Size = new Size(80, 0x18);
            this.rbOrderID.TabIndex = 1;
            this.rbOrderID.TabStop = true;
            this.rbOrderID.Text = "Order #";
            this.Label5.BackColor = Color.Transparent;
            this.Label5.Location = new Point(8, 0x48);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(480, 0x18);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Here you must enter order #.";
            this.Label5.TextAlign = ContentAlignment.MiddleLeft;
            this.PictureBox4.Dock = DockStyle.Top;
            this.PictureBox4.Image = (Image) manager.GetObject("PictureBox4.Image");
            this.PictureBox4.Location = new Point(0, 0);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new Size(0x1ec, 60);
            this.PictureBox4.TabIndex = 8;
            this.PictureBox4.TabStop = false;
            this.txtInvoiceID.Enabled = false;
            this.txtInvoiceID.Location = new Point(0x68, 160);
            this.txtInvoiceID.Name = "txtInvoiceID";
            this.txtInvoiceID.Size = new Size(0xa8, 0x15);
            this.txtInvoiceID.TabIndex = 4;
            this.lblOrderNumber.BackColor = Color.Transparent;
            this.lblOrderNumber.Location = new Point(0, 0x58);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new Size(0x48, 0x15);
            this.lblOrderNumber.TabIndex = 6;
            this.lblOrderNumber.Text = "Order #";
            this.lblOrderNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtOrderNumber.BackColor = SystemColors.Window;
            this.txtOrderNumber.Location = new Point(80, 0x58);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.ReadOnly = true;
            this.txtOrderNumber.Size = new Size(0x58, 0x15);
            this.txtOrderNumber.TabIndex = 7;
            this.pnlOrderItems.BackColor = SystemColors.Control;
            this.pnlOrderItems.Controls.Add(this.GridOrderItems);
            this.pnlOrderItems.Controls.Add(this.PictureBox5);
            this.pnlOrderItems.Controls.Add(this.Label21);
            this.pnlOrderItems.Location = new Point(0x1f0, 0xf8);
            this.pnlOrderItems.Name = "pnlOrderItems";
            this.pnlOrderItems.Size = new Size(0x1ec, 0x138);
            this.pnlOrderItems.TabIndex = 3;
            this.pnlOrderItems.Visible = false;
            this.GridOrderItems.Location = new Point(8, 0x58);
            this.GridOrderItems.Name = "GridOrderItems";
            this.GridOrderItems.Size = new Size(480, 0xe0);
            this.GridOrderItems.TabIndex = 1;
            this.PictureBox5.Dock = DockStyle.Top;
            this.PictureBox5.Image = (Image) manager.GetObject("PictureBox5.Image");
            this.PictureBox5.Location = new Point(0, 0);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new Size(0x1ec, 60);
            this.PictureBox5.TabIndex = 9;
            this.PictureBox5.TabStop = false;
            this.Label21.BackColor = Color.Transparent;
            this.Label21.Location = new Point(0x10, 0x40);
            this.Label21.Name = "Label21";
            this.Label21.Size = new Size(0x1a0, 0x18);
            this.Label21.TabIndex = 0;
            this.Label21.Text = "Select items to return.";
            this.Label21.TextAlign = ContentAlignment.MiddleLeft;
            this.pnlSummary.BackColor = SystemColors.Control;
            this.pnlSummary.Controls.Add(this.Label15);
            this.pnlSummary.Controls.Add(this.PictureBox2);
            this.pnlSummary.Controls.Add(this.txtSummary);
            this.pnlSummary.Location = new Point(0, 0x1d8);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new Size(0x1ec, 0x138);
            this.pnlSummary.TabIndex = 4;
            this.pnlSummary.Visible = false;
            this.Label15.Font = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label15.Location = new Point(0xa8, 8);
            this.Label15.Name = "Label15";
            this.Label15.Size = new Size(320, 0x18);
            this.Label15.TabIndex = 0;
            this.Label15.Text = "Completing the Return Sales Wizard";
            this.PictureBox2.Dock = DockStyle.Left;
            this.PictureBox2.Image = (Image) manager.GetObject("PictureBox2.Image");
            this.PictureBox2.Location = new Point(0, 0);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new Size(0xa4, 0x138);
            this.PictureBox2.TabIndex = 6;
            this.PictureBox2.TabStop = false;
            this.txtSummary.Location = new Point(0xa8, 0x30);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = ScrollBars.Vertical;
            this.txtSummary.Size = new Size(320, 0x108);
            this.txtSummary.TabIndex = 1;
            this.pnlFinish.BackColor = SystemColors.Control;
            this.pnlFinish.Controls.Add(this.txtResults);
            this.pnlFinish.Controls.Add(this.Label18);
            this.pnlFinish.Controls.Add(this.Label17);
            this.pnlFinish.Controls.Add(this.PictureBox3);
            this.pnlFinish.Location = new Point(0x1f0, 0x1d8);
            this.pnlFinish.Name = "pnlFinish";
            this.pnlFinish.Size = new Size(0x1ec, 0x13b);
            this.pnlFinish.TabIndex = 5;
            this.pnlFinish.Visible = false;
            this.txtResults.Location = new Point(0xa8, 0x38);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = ScrollBars.Vertical;
            this.txtResults.Size = new Size(320, 0x100);
            this.txtResults.TabIndex = 8;
            this.Label18.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label18.Location = new Point(0xb0, 0x20);
            this.Label18.Name = "Label18";
            this.Label18.Size = new Size(0x108, 0x18);
            this.Label18.TabIndex = 1;
            this.Label18.Text = "All operations have been successfully completed";
            this.Label17.Font = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label17.Location = new Point(0xa8, 8);
            this.Label17.Name = "Label17";
            this.Label17.Size = new Size(320, 0x18);
            this.Label17.TabIndex = 0;
            this.Label17.Text = "Close the Return Sales Wizard";
            this.PictureBox3.Dock = DockStyle.Left;
            this.PictureBox3.Image = (Image) manager.GetObject("PictureBox3.Image");
            this.PictureBox3.Location = new Point(0, 0);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new Size(0xa4, 0x13b);
            this.PictureBox3.TabIndex = 7;
            this.PictureBox3.TabStop = false;
            this.pnlButtons.Controls.Add(this.btnNext);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnBack);
            this.pnlButtons.Dock = DockStyle.Bottom;
            this.pnlButtons.Location = new Point(0, 710);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new Size(0x3e2, 0x2d);
            this.pnlButtons.TabIndex = 6;
            this.pnlOrderInfo.BackColor = SystemColors.Control;
            this.pnlOrderInfo.Controls.Add(this.GroupBox1);
            this.pnlOrderInfo.Controls.Add(this.gbDiagnosis);
            this.pnlOrderInfo.Controls.Add(this.PictureBox6);
            this.pnlOrderInfo.Location = new Point(0, 0xf8);
            this.pnlOrderInfo.Name = "pnlOrderInfo";
            this.pnlOrderInfo.Size = new Size(0x1ec, 0x138);
            this.pnlOrderInfo.TabIndex = 2;
            this.pnlOrderInfo.Visible = false;
            this.GroupBox1.Controls.Add(this.nmbDiscount);
            this.GroupBox1.Controls.Add(this.lblSalesDate);
            this.GroupBox1.Controls.Add(this.dtbSalesDate);
            this.GroupBox1.Controls.Add(this.lblTakenBy);
            this.GroupBox1.Controls.Add(this.txtTakenBy);
            this.GroupBox1.Controls.Add(this.txtInvoiceNumber);
            this.GroupBox1.Controls.Add(this.lblInvoiceNumber);
            this.GroupBox1.Controls.Add(this.lblDiscount);
            this.GroupBox1.Controls.Add(this.lblOrderNumber);
            this.GroupBox1.Controls.Add(this.txtOrderNumber);
            this.GroupBox1.Location = new Point(8, 0x40);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new Size(0xb0, 160);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.nmbDiscount.BorderStyle = BorderStyle.Fixed3D;
            this.nmbDiscount.Location = new Point(80, 0x70);
            this.nmbDiscount.Name = "nmbDiscount";
            this.nmbDiscount.Size = new Size(0x58, 0x15);
            this.nmbDiscount.TabIndex = 9;
            this.nmbDiscount.TextAlign = HorizontalAlignment.Left;
            this.lblSalesDate.BackColor = Color.Transparent;
            this.lblSalesDate.Location = new Point(0, 0x10);
            this.lblSalesDate.Name = "lblSalesDate";
            this.lblSalesDate.Size = new Size(0x48, 0x15);
            this.lblSalesDate.TabIndex = 0;
            this.lblSalesDate.Text = "Sales date";
            this.lblSalesDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbSalesDate.Location = new Point(80, 0x10);
            this.dtbSalesDate.Name = "dtbSalesDate";
            this.dtbSalesDate.Size = new Size(0x58, 0x15);
            this.dtbSalesDate.TabIndex = 1;
            this.lblTakenBy.BackColor = Color.Transparent;
            this.lblTakenBy.Location = new Point(0, 40);
            this.lblTakenBy.Name = "lblTakenBy";
            this.lblTakenBy.Size = new Size(0x48, 0x15);
            this.lblTakenBy.TabIndex = 2;
            this.lblTakenBy.Text = "Sold By";
            this.lblTakenBy.TextAlign = ContentAlignment.MiddleRight;
            this.txtTakenBy.BackColor = SystemColors.Window;
            this.txtTakenBy.Location = new Point(80, 40);
            this.txtTakenBy.Name = "txtTakenBy";
            this.txtTakenBy.ReadOnly = true;
            this.txtTakenBy.Size = new Size(0x58, 0x15);
            this.txtTakenBy.TabIndex = 3;
            this.txtInvoiceNumber.BackColor = SystemColors.Window;
            this.txtInvoiceNumber.Location = new Point(80, 0x40);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.ReadOnly = true;
            this.txtInvoiceNumber.Size = new Size(0x58, 0x15);
            this.txtInvoiceNumber.TabIndex = 5;
            this.lblInvoiceNumber.BackColor = Color.Transparent;
            this.lblInvoiceNumber.Location = new Point(0, 0x40);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new Size(0x48, 0x15);
            this.lblInvoiceNumber.TabIndex = 4;
            this.lblInvoiceNumber.Text = "Invoice #";
            this.lblInvoiceNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblDiscount.BackColor = Color.Transparent;
            this.lblDiscount.Location = new Point(0, 0x70);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new Size(0x48, 0x15);
            this.lblDiscount.TabIndex = 8;
            this.lblDiscount.Text = "Discount (%)";
            this.lblDiscount.TextAlign = ContentAlignment.MiddleRight;
            this.gbDiagnosis.Controls.Add(this.txtCustomerName);
            this.gbDiagnosis.Controls.Add(this.txtTaxRatePercent);
            this.gbDiagnosis.Controls.Add(this.txtTaxRateName);
            this.gbDiagnosis.Controls.Add(this.lblTaxPercent);
            this.gbDiagnosis.Controls.Add(this.lblTaxRate);
            this.gbDiagnosis.Controls.Add(this.txtCustomerPhone);
            this.gbDiagnosis.Controls.Add(this.Label16);
            this.gbDiagnosis.Controls.Add(this.CustomerAddress);
            this.gbDiagnosis.Controls.Add(this.Label6);
            this.gbDiagnosis.Location = new Point(0xc0, 0x40);
            this.gbDiagnosis.Name = "gbDiagnosis";
            this.gbDiagnosis.Size = new Size(0x128, 160);
            this.gbDiagnosis.TabIndex = 1;
            this.gbDiagnosis.TabStop = false;
            this.gbDiagnosis.Text = "Customer";
            this.txtCustomerName.BackColor = SystemColors.Window;
            this.txtCustomerName.Location = new Point(80, 0x10);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new Size(0xd0, 0x15);
            this.txtCustomerName.TabIndex = 1;
            this.txtTaxRatePercent.BackColor = SystemColors.Window;
            this.txtTaxRatePercent.Location = new Point(0xf8, 0x88);
            this.txtTaxRatePercent.Name = "txtTaxRatePercent";
            this.txtTaxRatePercent.ReadOnly = true;
            this.txtTaxRatePercent.Size = new Size(40, 0x15);
            this.txtTaxRatePercent.TabIndex = 8;
            this.txtTaxRatePercent.TextAlign = HorizontalAlignment.Right;
            this.txtTaxRateName.BackColor = SystemColors.Window;
            this.txtTaxRateName.Location = new Point(80, 0x88);
            this.txtTaxRateName.Name = "txtTaxRateName";
            this.txtTaxRateName.ReadOnly = true;
            this.txtTaxRateName.Size = new Size(0x88, 0x15);
            this.txtTaxRateName.TabIndex = 6;
            this.lblTaxPercent.Location = new Point(0xe0, 0x88);
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
            this.txtCustomerPhone.BackColor = SystemColors.Window;
            this.txtCustomerPhone.Location = new Point(80, 0x70);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.ReadOnly = true;
            this.txtCustomerPhone.Size = new Size(0xd0, 0x15);
            this.txtCustomerPhone.TabIndex = 4;
            this.Label16.BackColor = Color.Transparent;
            this.Label16.Location = new Point(0x10, 0x70);
            this.Label16.Name = "Label16";
            this.Label16.Size = new Size(0x38, 0x15);
            this.Label16.TabIndex = 3;
            this.Label16.Text = "Phone";
            this.Label16.TextAlign = ContentAlignment.MiddleRight;
            this.CustomerAddress.BackColor = SystemColors.Control;
            this.CustomerAddress.Location = new Point(8, 40);
            this.CustomerAddress.Name = "CustomerAddress";
            this.CustomerAddress.Size = new Size(280, 0x48);
            this.CustomerAddress.TabIndex = 2;
            this.Label6.BackColor = Color.Transparent;
            this.Label6.Location = new Point(0x10, 0x10);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x38, 0x15);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "Customer";
            this.Label6.TextAlign = ContentAlignment.MiddleRight;
            this.PictureBox6.Dock = DockStyle.Top;
            this.PictureBox6.Image = (Image) manager.GetObject("PictureBox6.Image");
            this.PictureBox6.Location = new Point(0, 0);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new Size(0x1ec, 60);
            this.PictureBox6.TabIndex = 9;
            this.PictureBox6.TabStop = false;
            base.AcceptButton = this.btnNext;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x3e2, 0x2f3);
            base.ControlBox = false;
            base.Controls.Add(this.pnlButtons);
            base.Controls.Add(this.pnlOrder);
            base.Controls.Add(this.pnlFinish);
            base.Controls.Add(this.pnlSummary);
            base.Controls.Add(this.pnlOrderItems);
            base.Controls.Add(this.pnlIntro);
            base.Controls.Add(this.pnlOrderInfo);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WizardReturnSales";
            this.Text = "Wizard Return Sales";
            this.pnlIntro.ResumeLayout(false);
            ((ISupportInitialize) this.PictureBox1).EndInit();
            this.pnlOrder.ResumeLayout(false);
            ((ISupportInitialize) this.PictureBox4).EndInit();
            this.pnlOrderItems.ResumeLayout(false);
            ((ISupportInitialize) this.PictureBox5).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            ((ISupportInitialize) this.PictureBox2).EndInit();
            this.pnlFinish.ResumeLayout(false);
            this.pnlFinish.PerformLayout();
            ((ISupportInitialize) this.PictureBox3).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlOrderInfo.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.gbDiagnosis.ResumeLayout(false);
            ((ISupportInitialize) this.PictureBox6).EndInit();
            base.ResumeLayout(false);
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AllowEdit = true;
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("InventoryItemName", "Inventory Item", 160);
            Appearance.AddTextColumn("OrderedQuantity", "Quantity", 60, Appearance.IntegerStyle());
            Appearance.AddTextColumn("BillablePrice", "Price", 60, Appearance.PriceStyle());
            Appearance.AddTextColumn("Amount", "Amount", 60, Appearance.PriceStyle());
            Appearance.AddBoolColumn("Selected", "Return", 60).ReadOnly = false;
            Appearance.AddBoolColumn("Return2Inventory", "Ret 2 Inv", 60).ReadOnly = false;
        }

        private void MoveNext()
        {
            Panel nextStage = this.GetNextStage();
            Panel currentStage = this.GetCurrentStage();
            if ((nextStage == null) || (currentStage == null))
            {
                return;
            }
            else if (!ReferenceEquals(currentStage, this.pnlIntro))
            {
                if (!ReferenceEquals(currentStage, this.pnlOrder))
                {
                    if (!ReferenceEquals(currentStage, this.pnlOrderInfo))
                    {
                        if (!ReferenceEquals(currentStage, this.pnlOrderItems))
                        {
                            if (!ReferenceEquals(currentStage, this.pnlSummary))
                            {
                                if (ReferenceEquals(currentStage, this.pnlFinish))
                                {
                                    base.Close();
                                }
                            }
                            else
                            {
                                try
                                {
                                    DataRow[] rowArray = this.GridOrderItems.GetTableSource<TableReturnSales>().Select("(Selected = True)");
                                    if (rowArray.Length != 0)
                                    {
                                        int[] orderItemsID = new int[(rowArray.Length - 1) + 1];
                                        int num5 = rowArray.Length - 1;
                                        int index = 0;
                                        while (true)
                                        {
                                            if (index > num5)
                                            {
                                                PrintRetailInvoice(SaveObject(Conversions.ToInteger(this.txtOrderNumber.Text), orderItemsID, new StatusDelegate(this.AddStatusString)).InvoiceID);
                                                break;
                                            }
                                            orderItemsID[index] = Convert.ToInt32(rowArray[index]["OrderDetailsID"]);
                                            index++;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                catch (Exception exception5)
                                {
                                    Exception ex = exception5;
                                    ProjectData.SetProjectError(ex);
                                    Exception exception = ex;
                                    this.ShowException(exception, "Order Info Stage");
                                    ProjectData.ClearProjectError();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            int num3;
                            if (int.TryParse(this.txtOrderNumber.Text, out num3))
                            {
                                TableReturnSales dataTable = new TableReturnSales();
                                using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT tbl_orderdetails.ID as OrderDetailsID,
       tbl_inventoryitem.ID as InventoryItemID,
       tbl_pricecode.ID as PriceCodeID,
       tbl_inventoryitem.Name as InventoryItemName,
       tbl_pricecode.Name as PriceCodeName,
       tbl_orderdetails.SaleRentType,
       tbl_orderdetails.BillablePrice,
       tbl_orderdetails.OrderedQuantity
FROM tbl_orderdetails
     INNER JOIN tbl_inventoryitem ON tbl_inventoryitem.ID = tbl_orderdetails.InventoryItemID
     INNER JOIN tbl_pricecode ON tbl_pricecode.ID = tbl_orderdetails.PriceCodeID
WHERE (tbl_orderdetails.OrderID = {num3})
  AND (tbl_orderdetails.SaleRentType = 'One Time Sale')
  AND (tbl_orderdetails.NextOrderID IS NULL)", ClassGlobalObjects.ConnectionString_MySql))
                                {
                                    adapter.AcceptChangesDuringFill = true;
                                    adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                                    adapter.Fill(dataTable);
                                }
                                this.GridOrderItems.GridSource = dataTable.ToGridSource();
                            }
                            else
                            {
                                return;
                            }
                        }
                        catch (Exception exception4)
                        {
                            Exception ex = exception4;
                            ProjectData.SetProjectError(ex);
                            Exception exception = ex;
                            this.ShowException(exception, "Order Info Stage");
                            ProjectData.ClearProjectError();
                            return;
                        }
                    }
                }
                else
                {
                    try
                    {
                        string commandText = "SELECT tbl_customer.ID as CustomerID,\r\n       CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName,\r\n       tbl_customer.Address1 as CustomerAddress1,\r\n       tbl_customer.Address2 as CustomerAddress2,\r\n       tbl_customer.City     as CustomerCity,\r\n       tbl_customer.State    as CustomerState,\r\n       tbl_customer.Zip      as CustomerZip,\r\n       tbl_customer.Phone    as CustomerPhone,\r\n       IFNULL(tbl_taxrate.Name, '<NO TAX>') as TaxRateName,\r\n       IFNULL(tbl_invoice.TaxRatePercent, 0) as TaxRatePercent,\r\n       tbl_order.Discount,\r\n       tbl_order.TakenBy,\r\n       tbl_order.OrderDate as SalesDate,\r\n       tbl_order.ID as OrderID,\r\n       tbl_invoice.ID as InvoiceID\r\nFROM tbl_order\r\n     INNER JOIN tbl_customer ON tbl_customer.ID = tbl_order.CustomerID\r\n     LEFT JOIN tbl_invoice ON tbl_invoice.OrderID = tbl_order.ID\r\n     LEFT JOIN tbl_taxrate ON tbl_taxrate.ID = tbl_invoice.TaxRateID\r\nWHERE (1 = 1)";
                        if (!this.rbInvoiceID.Checked)
                        {
                            if (this.rbOrderID.Checked)
                            {
                                int num2;
                                if (int.TryParse(this.txtOrderID.Text, out num2))
                                {
                                    commandText = commandText + "\r\n" + $"  AND (tbl_order.ID = {num2})";
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            int num;
                            if (int.TryParse(this.txtInvoiceID.Text, out num))
                            {
                                commandText = commandText + "\r\n" + $"  AND (tbl_invoice.ID = {num})";
                            }
                            else
                            {
                                return;
                            }
                        }
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand(commandText, connection))
                            {
                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (!reader.Read())
                                    {
                                        Functions.SetTextBoxText(this.CustomerAddress.txtAddress1, DBNull.Value);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtAddress2, DBNull.Value);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtCity, DBNull.Value);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtState, DBNull.Value);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtZip, DBNull.Value);
                                        Functions.SetTextBoxText(this.txtCustomerPhone, DBNull.Value);
                                        Functions.SetTextBoxText(this.txtTaxRateName, DBNull.Value);
                                        Functions.SetTextBoxText(this.txtTaxRatePercent, DBNull.Value);
                                        Functions.SetNumericBoxValue(this.nmbDiscount, DBNull.Value);
                                        Functions.SetDateBoxValue(this.dtbSalesDate, DBNull.Value);
                                        Functions.SetTextBoxText(this.txtOrderNumber, DBNull.Value);
                                        Functions.SetTextBoxText(this.txtInvoiceNumber, DBNull.Value);
                                        return;
                                    }
                                    else
                                    {
                                        Functions.SetTextBoxText(this.txtCustomerName, reader["CustomerName"]);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtAddress1, reader["CustomerAddress1"]);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtAddress2, reader["CustomerAddress2"]);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtCity, reader["CustomerCity"]);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtState, reader["CustomerState"]);
                                        Functions.SetTextBoxText(this.CustomerAddress.txtZip, reader["CustomerZip"]);
                                        Functions.SetTextBoxText(this.txtCustomerPhone, reader["CustomerPhone"]);
                                        Functions.SetTextBoxText(this.txtTaxRateName, reader["TaxRateName"]);
                                        Functions.SetTextBoxText(this.txtTaxRatePercent, reader["TaxRatePercent"]);
                                        Functions.SetNumericBoxValue(this.nmbDiscount, reader["Discount"]);
                                        Functions.SetTextBoxText(this.txtTakenBy, reader["TakenBy"]);
                                        Functions.SetDateBoxValue(this.dtbSalesDate, reader["SalesDate"]);
                                        Functions.SetTextBoxText(this.txtOrderNumber, reader["OrderID"]);
                                        Functions.SetTextBoxText(this.txtInvoiceNumber, reader["InvoiceID"]);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception exception = ex;
                        this.ShowException(exception, "Order Stage");
                        ProjectData.ClearProjectError();
                        return;
                    }
                }
            }
            this.SetActivePanel(nextStage);
            this.SetButtonState();
        }

        private void MovePrev()
        {
            Panel previousStage = this.GetPreviousStage();
            if (previousStage != null)
            {
                this.SetActivePanel(previousStage);
                this.SetButtonState();
            }
        }

        private static void PrintRetailInvoice(int InvoiceID)
        {
            ReportParameters @params = new ReportParameters {
                ["{?tbl_invoice.ID}"] = InvoiceID,
                ["{?Amount Tendered}"] = 0,
                ["{?Payment Type}"] = "Cash",
                ["{?Check Number}"] = "",
                ["{?Credit Card Number}"] = ""
            };
            ClassGlobalObjects.ShowReport("RetailInvoice", @params, true);
        }

        private void rbInvoiceID_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateControls();
        }

        private void rbOrderID_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateControls();
        }

        private static SaveObjectResult SaveObject(int OrderID, int[] OrderItemsID, StatusDelegate StatusProc)
        {
            SaveObjectResult result;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                MySqlTransaction tran = connection.BeginTransaction();
                try
                {
                    int num2;
                    int orderID = SaveOrder(connection, tran, OrderID, OrderItemsID, StatusProc);
                    DateTime today = DateTime.Today;
                    using (MySqlCommand command = new MySqlCommand("", connection, tran))
                    {
                        command.CommandText = "CALL `order_process`(:P_OrderID, :P_BillingMonth, :P_BillingFlags, :P_InvoiceDate, @P_InvoiceID)";
                        command.Parameters.Add("P_OrderID", MySqlType.Int).Value = orderID;
                        command.Parameters.Add("P_BillingMonth", MySqlType.Int).Value = 1;
                        command.Parameters.Add("P_BillingFlags", MySqlType.Int).Value = 0;
                        command.Parameters.Add("P_InvoiceDate", MySqlType.Date).Value = today;
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
                        num2 = nullable.Value;
                    }
                    WriteStatusString(StatusProc, "Created new Invoice # {0}", num2);
                    using (MySqlCommand command3 = new MySqlCommand("", connection, tran))
                    {
                        command3.CommandText = "CALL Invoice_AddSubmitted(:InvoiceID, 'Patient', 'Paper', Null, :LastUpdateUserID)";
                        command3.Parameters.Add("InvoiceID", MySqlType.Int).Value = num2;
                        command3.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                        command3.ExecuteNonQuery();
                    }
                    PaymentExtraData data1 = new PaymentExtraData();
                    data1.Paid = 0.0M;
                    string str = data1.ToString();
                    using (MySqlCommand command4 = new MySqlCommand("", connection, tran))
                    {
                        command4.CommandText = "CALL retailinvoice_addpayments(:P_InvoiceID, :P_TransactionDate, :P_Extra, :P_LastUpdateUserID)";
                        command4.Parameters.Add("P_InvoiceID", MySqlType.Int).Value = num2;
                        command4.Parameters.Add("P_TransactionDate", MySqlType.Date).Value = today;
                        command4.Parameters.Add("P_Extra", MySqlType.Text, 0x10000).Value = str;
                        command4.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                        command4.ExecuteNonQuery();
                    }
                    using (MySqlCommand command5 = new MySqlCommand("", connection, tran))
                    {
                        command5.CommandText = "CALL Invoice_UpdateBalance(:P_InvoiceID, true)";
                        command5.Parameters.Add("P_InvoiceID", MySqlType.Int).Value = num2;
                        command5.ExecuteNonQuery();
                    }
                    tran.Commit();
                    WriteStatusString(StatusProc, "Commit transaction");
                    result = new SaveObjectResult(orderID, num2);
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception innerException = ex;
                    tran.Rollback();
                    WriteStatusString(StatusProc, "Error:");
                    WriteStatusString(StatusProc, innerException.ToString());
                    WriteStatusString(StatusProc, "Rollback transaction");
                    throw new Exception("SaveOrder", innerException);
                }
            }
            return result;
        }

        private static int SaveOrder(MySqlConnection cnn, MySqlTransaction tran, int OrderID, int[] OrderDetailIDs, StatusDelegate StatusProc)
        {
            int num;
            if (OrderDetailIDs.Length == 0)
            {
                throw new UserNotifyException("There are no items to add to return sales order");
            }
            using (MySqlCommand command = new MySqlCommand("", cnn, tran))
            {
                string[] textArray1 = new string[] { "SELECT ID\r\nFROM tbl_orderdetails\r\nWHERE (OrderID = ", Conversions.ToString(OrderID), ")\r\n  AND (ID IN (", string.Join<int>(",", OrderDetailIDs), "))\r\n  AND (SaleRentType = 'One Time Sale')" };
                command.CommandText = string.Concat(textArray1);
                OrderDetailIDs = command.ExecuteReader().ToEnumerable().Select<IDataRecord, int>(((_Closure$__.$I241-0 == null) ? (_Closure$__.$I241-0 = new Func<IDataRecord, int>(_Closure$__.$I._Lambda$__241-0)) : _Closure$__.$I241-0)).ToArray<int>();
            }
            if (OrderDetailIDs.Length == 0)
            {
                throw new UserNotifyException("There are no items to add to return sales order");
            }
            List<Tuple<string, string, string>> source = new List<Tuple<string, string, string>>(0x40) {
                Tuple.Create<string, string, string>("| int(11)     |", "CustomerID            ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)  |", "Approved              ", "1"),
                Tuple.Create<string, string, string>("| tinyint(1)  |", "RetailSales           ", ""),
                Tuple.Create<string, string, string>("| date        |", "OrderDate             ", "CURRENT_DATE()"),
                Tuple.Create<string, string, string>("| date        |", "DeliveryDate          ", "CURRENT_DATE()"),
                Tuple.Create<string, string, string>("| date        |", "BillDate              ", "CURRENT_DATE()"),
                Tuple.Create<string, string, string>("| date        |", "EndDate               ", "CURRENT_DATE()"),
                Tuple.Create<string, string, string>("| int(11)     |", "ShippingMethodID      ", "null"),
                Tuple.Create<string, string, string>("| text        |", "SpecialInstructions   ", "null"),
                Tuple.Create<string, string, string>("| varchar(50) |", "TicketMesage          ", "null"),
                Tuple.Create<string, string, string>("| int(11)     |", "CustomerInsurance1_ID ", "null"),
                Tuple.Create<string, string, string>("| int(11)     |", "CustomerInsurance2_ID ", "null"),
                Tuple.Create<string, string, string>("| int(11)     |", "CustomerInsurance3_ID ", "null"),
                Tuple.Create<string, string, string>("| int(11)     |", "CustomerInsurance4_ID ", "null"),
                Tuple.Create<string, string, string>("| varchar(6)  |", "ICD9_1                ", ""),
                Tuple.Create<string, string, string>("| varchar(6)  |", "ICD9_2                ", ""),
                Tuple.Create<string, string, string>("| varchar(6)  |", "ICD9_3                ", ""),
                Tuple.Create<string, string, string>("| varchar(6)  |", "ICD9_4                ", ""),
                Tuple.Create<string, string, string>("| int(11)     |", "DoctorID              ", ""),
                Tuple.Create<string, string, string>("| int(11)     |", "POSTypeID             ", ""),
                Tuple.Create<string, string, string>("| varchar(50) |", "TakenBy               ", "'" + Globals.CompanyUserName.Replace("'", "''") + "'"),
                Tuple.Create<string, string, string>("| double      |", "Discount              ", ""),
                Tuple.Create<string, string, string>("| smallint(6) |", "LastUpdateUserID      ", Globals.CompanyUserID.ToString()),
                Tuple.Create<string, string, string>("| timestamp   |", "LastUpdateDatetime    ", "NOW()"),
                Tuple.Create<string, string, string>("| enum()      |", "SaleType              ", ""),
                Tuple.Create<string, string, string>("| enum()      |", "State                 ", "'New'"),
                Tuple.Create<string, string, string>("| set()       |", "MIR                   ", "''"),
                Tuple.Create<string, string, string>("| tinyint(1)  |", "AcceptAssignment      ", "0"),
                Tuple.Create<string, string, string>("| varchar(80) |", "ClaimNote             ", ""),
                Tuple.Create<string, string, string>("| int(11)     |", "FacilityID            ", ""),
                Tuple.Create<string, string, string>("| int(11)     |", "ReferralID            ", ""),
                Tuple.Create<string, string, string>("| int(11)     |", "SalesrepID            ", ""),
                Tuple.Create<string, string, string>("| int(11)     |", "LocationID            ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)  |", "Archived              ", "0"),
                Tuple.Create<string, string, string>("| datetime    |", "TakenAt               ", "NOW()"),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_01              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_02              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_03              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_04              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_05              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_06              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_07              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_08              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_09              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_10              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_11              ", ""),
                Tuple.Create<string, string, string>("| varchar(8)  |", "ICD10_12              ", "")
            };
            using (MySqlCommand command2 = new MySqlCommand("", cnn, tran))
            {
                string[] textArray2 = new string[7];
                textArray2[0] = "INSERT INTO tbl_order\r\n(";
                textArray2[1] = string.Join("\r\n,", source.Select<Tuple<string, string, string>, string>((_Closure$__.$I241-1 == null) ? (_Closure$__.$I241-1 = new Func<Tuple<string, string, string>, string>(_Closure$__.$I._Lambda$__241-1)) : _Closure$__.$I241-1));
                string[] local1 = textArray2;
                local1[2] = ")\r\nSELECT\r\n ";
                local1[3] = string.Join("\r\n,", source.Select<Tuple<string, string, string>, string>((_Closure$__.$I241-2 == null) ? (_Closure$__.$I241-2 = new Func<Tuple<string, string, string>, string>(_Closure$__.$I._Lambda$__241-2)) : _Closure$__.$I241-2));
                string[] local2 = local1;
                local2[4] = "\r\nFROM tbl_order\r\nWHERE (ID = ";
                local2[5] = Conversions.ToString(OrderID);
                local2[6] = ")";
                command2.CommandText = string.Concat(local2);
                command2.ExecuteNonQuery();
            }
            using (MySqlCommand command3 = new MySqlCommand("", cnn, tran))
            {
                command3.CommandText = "SELECT LAST_INSERT_ID()";
                num = Convert.ToInt32(command3.ExecuteScalar());
            }
            WriteStatusString(StatusProc, "Added New Order # {0}", num);
            using (MySqlCommand command4 = new MySqlCommand("", cnn, tran))
            {
                string[] textArray3 = new string[] { "UPDATE tbl_orderdetails\r\nSET NextOrderID = ", Conversions.ToString(num), "\r\nWHERE (OrderID = ", Conversions.ToString(OrderID), ")\r\n  AND (ID IN (", string.Join<int>(",", OrderDetailIDs), "))\r\n  AND (SaleRentType = 'One Time Sale')" };
                command4.CommandText = string.Concat(textArray3);
                command4.ExecuteNonQuery();
            }
            WriteStatusString(StatusProc, "Set status to 'return' for selected order details");
            List<Tuple<string, string, string>> list2 = new List<Tuple<string, string, string>>(0x40) {
                Tuple.Create<string, string, string>("| int(11)       |", "OrderID              ", num.ToString()),
                Tuple.Create<string, string, string>("| int(11)       |", "CustomerID           ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "SerialNumber         ", ""),
                Tuple.Create<string, string, string>("| int(11)       |", "InventoryItemID      ", ""),
                Tuple.Create<string, string, string>("| int(11)       |", "PriceCodeID          ", ""),
                Tuple.Create<string, string, string>("| enum()        |", "SaleRentType         ", ""),
                Tuple.Create<string, string, string>("| int(11)       |", "SerialID             ", ""),
                Tuple.Create<string, string, string>("| decimal(18,2) |", "BillablePrice        ", ""),
                Tuple.Create<string, string, string>("| decimal(18,2) |", "AllowablePrice       ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "Taxable              ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "FlatRate             ", ""),
                Tuple.Create<string, string, string>("| date          |", "DOSFrom              ", ""),
                Tuple.Create<string, string, string>("| date          |", "DOSTo                ", ""),
                Tuple.Create<string, string, string>("| date          |", "PickupDate           ", "CURRENT_DATE()"),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "ShowSpanDates        ", ""),
                Tuple.Create<string, string, string>("| double        |", "OrderedQuantity      ", "0 - OrderedQuantity"),
                Tuple.Create<string, string, string>("| varchar(50)   |", "OrderedUnits         ", ""),
                Tuple.Create<string, string, string>("| enum()        |", "OrderedWhen          ", ""),
                Tuple.Create<string, string, string>("| double        |", "OrderedConverter     ", ""),
                Tuple.Create<string, string, string>("| double        |", "BilledQuantity       ", "0 - BilledQuantity"),
                Tuple.Create<string, string, string>("| varchar(50)   |", "BilledUnits          ", ""),
                Tuple.Create<string, string, string>("| enum()        |", "BilledWhen           ", ""),
                Tuple.Create<string, string, string>("| double        |", "BilledConverter      ", ""),
                Tuple.Create<string, string, string>("| double        |", "DeliveryQuantity     ", "0 - DeliveryQuantity"),
                Tuple.Create<string, string, string>("| varchar(50)   |", "DeliveryUnits        ", ""),
                Tuple.Create<string, string, string>("| double        |", "DeliveryConverter    ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "BillingCode          ", ""),
                Tuple.Create<string, string, string>("| varchar(8)    |", "Modifier1            ", ""),
                Tuple.Create<string, string, string>("| varchar(8)    |", "Modifier2            ", ""),
                Tuple.Create<string, string, string>("| varchar(8)    |", "Modifier3            ", ""),
                Tuple.Create<string, string, string>("| varchar(8)    |", "Modifier4            ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "DXPointer            ", ""),
                Tuple.Create<string, string, string>("| int(11)       |", "BillingMonth         ", "0"),
                Tuple.Create<string, string, string>("| varchar(50)   |", "BillItemOn           ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "AuthorizationNumber  ", ""),
                Tuple.Create<string, string, string>("| int(11)       |", "AuthorizationTypeID  ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "ReasonForPickup      ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "SendCMN_RX_w_invoice ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "MedicallyUnnecessary ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "Sale                 ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "SpecialCode          ", "null"),
                Tuple.Create<string, string, string>("| varchar(50)   |", "ReviewCode           ", "null"),
                Tuple.Create<string, string, string>("| int(11)       |", "NextOrderID          ", "null"),
                Tuple.Create<string, string, string>("| int(11)       |", "ReoccuringID         ", "null"),
                Tuple.Create<string, string, string>("| int(11)       |", "CMNFormID            ", ""),
                Tuple.Create<string, string, string>("| varchar(100)  |", "HAODescription       ", ""),
                Tuple.Create<string, string, string>("| enum()        |", "State                ", "'New'"),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "BillIns1             ", "0"),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "BillIns2             ", "0"),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "BillIns3             ", "0"),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "BillIns4             ", "0"),
                Tuple.Create<string, string, string>("| date          |", "EndDate              ", "null"),
                Tuple.Create<string, string, string>("| set()         |", "MIR                  ", "''"),
                Tuple.Create<string, string, string>("| date          |", "NextBillingDate      ", "null"),
                Tuple.Create<string, string, string>("| int(11)       |", "WarehouseID          ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "AcceptAssignment     ", "0"),
                Tuple.Create<string, string, string>("| varchar(20)   |", "DrugNoteField        ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "DrugControlNumber    ", ""),
                Tuple.Create<string, string, string>("| tinyint(1)    |", "NopayIns1            ", ""),
                Tuple.Create<string, string, string>("| smallint(6)   |", "PointerICD10         ", ""),
                Tuple.Create<string, string, string>("| varchar(50)   |", "DXPointer10          ", ""),
                Tuple.Create<string, string, string>("| set()         |", "`MIR.ORDER`          ", "''")
            };
            using (MySqlCommand command5 = new MySqlCommand("", cnn, tran))
            {
                string[] textArray4 = new string[9];
                textArray4[0] = "INSERT INTO tbl_orderdetails\r\n(";
                textArray4[1] = string.Join("\r\n,", list2.Select<Tuple<string, string, string>, string>((_Closure$__.$I241-3 == null) ? (_Closure$__.$I241-3 = new Func<Tuple<string, string, string>, string>(_Closure$__.$I._Lambda$__241-3)) : _Closure$__.$I241-3));
                string[] local3 = textArray4;
                local3[2] = ")\r\nSELECT\r\n ";
                local3[3] = string.Join("\r\n,", list2.Select<Tuple<string, string, string>, string>((_Closure$__.$I241-4 == null) ? (_Closure$__.$I241-4 = new Func<Tuple<string, string, string>, string>(_Closure$__.$I._Lambda$__241-4)) : _Closure$__.$I241-4));
                string[] local4 = local3;
                local4[4] = "\r\nFROM tbl_orderdetails\r\nWHERE (OrderID = ";
                local4[5] = Conversions.ToString(OrderID);
                local4[6] = ")\r\n  AND (ID IN (";
                local4[7] = string.Join<int>(",", OrderDetailIDs);
                local4[8] = "))\r\n  AND (SaleRentType = 'One Time Sale')";
                command5.CommandText = string.Concat(local4);
                command5.ExecuteNonQuery();
            }
            WriteStatusString(StatusProc, "Added line items to created order");
            using (MySqlCommand command6 = new MySqlCommand("", cnn, tran))
            {
                command6.CommandText = "CALL mir_update_orderdetails(" + Conversions.ToString(num) + ")";
                command6.ExecuteNonQuery();
            }
            using (MySqlCommand command7 = new MySqlCommand("", cnn, tran))
            {
                command7.CommandText = "CALL inventory_transaction_order_refresh(" + Conversions.ToString(num) + ")";
                command7.ExecuteNonQuery();
            }
            using (MySqlCommand command8 = new MySqlCommand("", cnn, tran))
            {
                command8.CommandText = "CALL inventory_order_refresh(" + Conversions.ToString(num) + ")";
                command8.ExecuteNonQuery();
            }
            using (MySqlCommand command9 = new MySqlCommand("", cnn, tran))
            {
                command9.CommandText = "CALL serial_order_refresh(" + Conversions.ToString(num) + ")";
                command9.ExecuteNonQuery();
            }
            WriteStatusString(StatusProc, "Executed stored procedures for created order");
            return num;
        }

        private void SetActivePanel(Panel panel)
        {
            panel.Location = new Point(0, 0);
            panel.Size = new Size(0x1ec, 0x138);
            this.pnlIntro.Visible = ReferenceEquals(panel, this.pnlIntro);
            this.pnlOrder.Visible = ReferenceEquals(panel, this.pnlOrder);
            if (this.pnlOrder.Visible)
            {
                this.txtOrderNumber.Focus();
            }
            this.pnlOrderInfo.Visible = ReferenceEquals(panel, this.pnlOrderInfo);
            this.pnlOrderItems.Visible = ReferenceEquals(panel, this.pnlOrderItems);
            if (this.pnlOrderItems.Visible)
            {
                this.GridOrderItems.Focus();
            }
            this.pnlSummary.Visible = ReferenceEquals(panel, this.pnlSummary);
            this.pnlFinish.Visible = ReferenceEquals(panel, this.pnlFinish);
            this.SetButtonState();
        }

        private void SetButtonState()
        {
            Panel currentStage = this.GetCurrentStage();
            Panel nextStage = this.GetNextStage();
            if (this.GetPreviousStage() == null)
            {
                this.btnBack.Text = "<< &Back";
                this.btnBack.Enabled = false;
            }
            else
            {
                this.btnBack.Text = "<< &Back";
                this.btnBack.Enabled = true;
            }
            if (nextStage == null)
            {
                if (ReferenceEquals(currentStage, this.GetLastStage()))
                {
                    this.btnNext.Text = "Close";
                    this.btnNext.Enabled = true;
                }
                else
                {
                    this.btnNext.Text = "&Next >>";
                    this.btnNext.Enabled = false;
                }
            }
            else if (ReferenceEquals(nextStage, this.GetLastStage()))
            {
                this.btnNext.Text = "&Finish";
                this.btnNext.Enabled = true;
            }
            else
            {
                this.btnNext.Text = "&Next >>";
                this.btnNext.Enabled = true;
            }
        }

        private void UpdateControls()
        {
            this.txtOrderID.Enabled = this.rbOrderID.Checked;
            this.txtInvoiceID.Enabled = this.rbInvoiceID.Checked;
        }

        private static void WriteStatusString(StatusDelegate StatusProc, string Status)
        {
            if (StatusProc != null)
            {
                StatusProc(Status);
            }
        }

        private static void WriteStatusString(StatusDelegate StatusProc, string Format, object arg0)
        {
            if (StatusProc != null)
            {
                StatusProc(string.Format(Format, arg0));
            }
        }

        private static void WriteStatusString(StatusDelegate StatusProc, string Format, object arg0, object arg1)
        {
            if (StatusProc != null)
            {
                StatusProc(string.Format(Format, arg0, arg1));
            }
        }

        [field: AccessedThroughProperty("pnlButtons")]
        private Panel pnlButtons { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnNext")]
        private Button btnNext { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnBack")]
        private Button btnBack { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlIntro")]
        private Panel pnlIntro { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlSummary")]
        private Panel pnlSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlFinish")]
        private Panel pnlFinish { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PictureBox1")]
        private PictureBox PictureBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label9")]
        private Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label10")]
        private Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PictureBox2")]
        private PictureBox PictureBox2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label15")]
        private Label Label15 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PictureBox3")]
        private PictureBox PictureBox3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label17")]
        private Label Label17 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label18")]
        private Label Label18 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlOrder")]
        private Panel pnlOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrderNumber")]
        private Label lblOrderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOrderNumber")]
        private TextBox txtOrderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PictureBox4")]
        private PictureBox PictureBox4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label21")]
        private Label Label21 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PictureBox5")]
        private PictureBox PictureBox5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("GridOrderItems")]
        private FilteredGrid GridOrderItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PictureBox6")]
        private PictureBox PictureBox6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("GroupBox1")]
        private GroupBox GroupBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDiscount")]
        private NumericBox nmbDiscount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSalesDate")]
        private Label lblSalesDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbSalesDate")]
        private UltraDateTimeEditor dtbSalesDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInvoiceNumber")]
        private TextBox txtInvoiceNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoiceNumber")]
        private Label lblInvoiceNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDiscount")]
        private Label lblDiscount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbDiagnosis")]
        private GroupBox gbDiagnosis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxPercent")]
        private Label lblTaxPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxRate")]
        private Label lblTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label16")]
        private Label Label16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label6")]
        private Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlOrderItems")]
        private Panel pnlOrderItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlOrderInfo")]
        private Panel pnlOrderInfo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerName")]
        private TextBox txtCustomerName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomerPhone")]
        private TextBox txtCustomerPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CustomerAddress")]
        private ControlAddress CustomerAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxRatePercent")]
        private TextBox txtTaxRatePercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxRateName")]
        private TextBox txtTaxRateName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTakenBy")]
        private Label lblTakenBy { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTakenBy")]
        private TextBox txtTakenBy { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSummary")]
        private TextBox txtSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtResults")]
        private TextBox txtResults { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInvoiceID")]
        private TextBox txtInvoiceID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbOrderID")]
        private RadioButton rbOrderID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbInvoiceID")]
        private RadioButton rbInvoiceID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOrderID")]
        private TextBox txtOrderID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly WizardReturnSales._Closure$__ $I = new WizardReturnSales._Closure$__();
            public static Func<IDataRecord, int> $I241-0;
            public static Func<Tuple<string, string, string>, string> $I241-1;
            public static Func<Tuple<string, string, string>, string> $I241-2;
            public static Func<Tuple<string, string, string>, string> $I241-3;
            public static Func<Tuple<string, string, string>, string> $I241-4;

            internal int _Lambda$__241-0(IDataRecord r) => 
                Convert.ToInt32(r[0]);

            internal string _Lambda$__241-1(Tuple<string, string, string> t) => 
                t.Item2;

            internal string _Lambda$__241-2(Tuple<string, string, string> t) => 
                string.IsNullOrEmpty(t.Item3) ? t.Item2 : t.Item3;

            internal string _Lambda$__241-3(Tuple<string, string, string> t) => 
                t.Item2;

            internal string _Lambda$__241-4(Tuple<string, string, string> t) => 
                string.IsNullOrEmpty(t.Item3) ? t.Item2 : t.Item3;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SaveObjectResult
        {
            public readonly int OrderID;
            public readonly int InvoiceID;
            public SaveObjectResult(int orderID, int invoiceID)
            {
                this = new WizardReturnSales.SaveObjectResult();
                this.OrderID = orderID;
                this.InvoiceID = invoiceID;
            }
        }

        private delegate void StatusDelegate(string Status);

        public class TableReturnSales : DataTable
        {
            public readonly DataColumn Col_RowID;
            public readonly DataColumn Col_OrderDetailsID;
            public readonly DataColumn Col_InventoryItemID;
            public readonly DataColumn Col_PriceCodeID;
            public readonly DataColumn Col_InventoryItemName;
            public readonly DataColumn Col_PriceCodeName;
            public readonly DataColumn Col_Selected;
            public readonly DataColumn Col_Return2Inventory;
            public readonly DataColumn Col_SaleRentType;
            public readonly DataColumn Col_BillablePrice;
            public readonly DataColumn Col_OrderedQuantity;
            public readonly DataColumn Col_Amount;

            public TableReturnSales() : this("tbl_orderdetails")
            {
            }

            public TableReturnSales(string TableName) : base(TableName)
            {
                this.Col_RowID = this.AddColumn("RowID", typeof(int), false);
                this.Col_RowID.AutoIncrement = true;
                this.Col_RowID.AutoIncrementSeed = 0L;
                this.Col_RowID.AutoIncrementStep = 1L;
                this.Col_RowID.Unique = true;
                this.Col_RowID.AllowDBNull = false;
                this.Col_RowID.ReadOnly = true;
                this.Col_OrderDetailsID = this.AddColumn("OrderDetailsID", typeof(int), true);
                this.Col_InventoryItemID = this.AddColumn("InventoryItemID", typeof(int), false);
                this.Col_PriceCodeID = this.AddColumn("PriceCodeID", typeof(int), false);
                this.Col_InventoryItemName = this.AddColumn("InventoryItemName", typeof(string), true);
                this.Col_PriceCodeName = this.AddColumn("PriceCodeName", typeof(string), true);
                this.Col_Selected = base.Columns.Add("Selected", typeof(bool));
                this.Col_Selected.AllowDBNull = false;
                this.Col_Selected.DefaultValue = false;
                this.Col_Return2Inventory = base.Columns.Add("Return2Inventory", typeof(bool));
                this.Col_Return2Inventory.AllowDBNull = false;
                this.Col_Return2Inventory.DefaultValue = false;
                this.Col_SaleRentType = this.AddColumn("SaleRentType", typeof(string), false);
                this.Col_BillablePrice = this.AddColumn("BillablePrice", typeof(double), false);
                this.Col_OrderedQuantity = this.AddColumn("OrderedQuantity", typeof(double), false);
                this.Col_Amount = base.Columns.Add("Amount", typeof(double), "OrderedQuantity * BillablePrice");
            }

            protected DataColumn AddColumn(string name, System.Type type, bool AllowDBNull = true)
            {
                DataColumn column1 = base.Columns.Add(name, type);
                column1.AllowDBNull = AllowDBNull;
                return column1;
            }
        }
    }
}

