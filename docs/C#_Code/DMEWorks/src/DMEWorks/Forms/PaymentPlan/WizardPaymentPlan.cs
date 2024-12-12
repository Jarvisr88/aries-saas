namespace DMEWorks.Forms.PaymentPlan
{
    using ActiproSoftware.Wizard;
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class WizardPaymentPlan : DmeForm
    {
        private IContainer components;
        private readonly int CustomerID;
        private const string CrLf = "\r\n";

        public WizardPaymentPlan(int CustomerID)
        {
            base.Load += new EventHandler(this.Form_Load);
            this.CustomerID = CustomerID;
            this.InitializeComponent();
            this.InitializeGrid(this.gridInvoices.Appearance);
        }

        private void AttachTable(TableInvoiceDetails Table)
        {
            TableInvoiceDetails tableSource = this.gridInvoices.GetTableSource<TableInvoiceDetails>();
            if (!ReferenceEquals(tableSource, Table))
            {
                if (tableSource != null)
                {
                    tableSource.RowChanged -= new DataRowChangeEventHandler(this.Table_RowChanged);
                }
                this.gridInvoices.GridSource = Table.ToGridSource();
                if (Table != null)
                {
                    Table.RowChanged += new DataRowChangeEventHandler(this.Table_RowChanged);
                }
            }
        }

        private void CalculateLastPayment()
        {
            DateTime? nullable = null;
            try
            {
                nullable = new DateTime?(this.GetLastPayment());
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            this.txtLastPaymentDate.Text = $"{nullable:d}";
        }

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidateSchedule();
            this.CalculateLastPayment();
        }

        private void cmbPeriod_TextChanged(object sender, EventArgs e)
        {
            this.ValidateSchedule();
            this.CalculateLastPayment();
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

        private void dtbStartDate_ValueChanged(object sender, EventArgs e)
        {
            this.ValidateSchedule();
            this.CalculateLastPayment();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadComboBoxes();
                this.LoadPlan();
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

        private DateTime GetFirstPayment()
        {
            DateTime? nullable = NullableConvert.ToDateTime(this.dtbFirstPayment.Value);
            if (nullable == null)
            {
                throw new UserNotifyException("You have to enter date of  the First Payment");
            }
            return nullable.Value;
        }

        private DateTime GetLastPayment()
        {
            DateTime time;
            Period period = this.GetPeriod();
            switch (period)
            {
                case Period.Weekly:
                    time = this.GetFirstPayment().AddDays((double) (7 * this.GetPaymentCount()));
                    break;

                case Period.Biweekly:
                    time = this.GetFirstPayment().AddDays((double) (14 * this.GetPaymentCount()));
                    break;

                case Period.Monthly:
                    time = this.GetFirstPayment().AddMonths(this.GetPaymentCount());
                    break;

                default:
                    throw new UserNotifyException("Period " + period.ToString() + "is not supported");
            }
            return time;
        }

        private double GetPaymentAmount()
        {
            double? asDouble = this.nmbPaymentAmount.AsDouble;
            if (asDouble == null)
            {
                throw new UserNotifyException("You have to enter Payment Amount");
            }
            if (asDouble.Value < 0.01)
            {
                throw new UserNotifyException("You have to enter positive Payment Amount");
            }
            return asDouble.Value;
        }

        private int GetPaymentCount()
        {
            int? nullable = this.nmbPaymentCount.AsInt32;
            if (nullable == null)
            {
                throw new UserNotifyException("You have to enter Payment Count");
            }
            if (nullable.Value <= 0)
            {
                throw new UserNotifyException("Payment Count must be greater than zero");
            }
            return nullable.Value;
        }

        private Period GetPeriod()
        {
            Period weekly;
            string text = this.cmbPeriod.Text;
            if (string.Compare(text, "Weekly", true) == 0)
            {
                weekly = Period.Weekly;
            }
            else if (string.Compare(text, "Bi-weekly", true) == 0)
            {
                weekly = Period.Biweekly;
            }
            else
            {
                if (string.Compare(text, "Monthly", true) != 0)
                {
                    throw new UserNotifyException("Period '" + text + "' is not supported");
                }
                weekly = Period.Monthly;
            }
            return weekly;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            WindowsClassicWizardRenderer renderer = new WindowsClassicWizardRenderer();
            this.wizard = new ActiproSoftware.Wizard.Wizard();
            this.wpWelcome = new WizardWelcomePage();
            this.Label3 = new Label();
            this.Label2 = new Label();
            this.Label1 = new Label();
            this.wpCustomer = new WizardPage();
            this.txtCustomer = new Label();
            this.lblCustomer = new Label();
            this.wpInvoices = new WizardPage();
            this.gridInvoices = new FilteredGrid();
            this.lblInvoices = new Label();
            this.wpSchedule = new WizardPage();
            this.nmbPaymentCount = new NumericBox();
            this.txtLastPaymentDate = new Label();
            this.lblNumberOfPayments = new Label();
            this.lblLastPaymentDate = new Label();
            this.txtTotalAmount = new Label();
            this.lblTotalAmount = new Label();
            this.dtbFirstPayment = new UltraDateTimeEditor();
            this.nmbPaymentAmount = new NumericBox();
            this.cmbPeriod = new ComboBox();
            this.lblPeriod = new Label();
            this.lblTo = new Label();
            this.lblFirstPayment = new Label();
            this.lblPaymentAmount = new Label();
            this.wpSummary = new WizardPage();
            this.txtSummary = new TextBox();
            this.wpReport = new WizardPage();
            this.txtReport = new TextBox();
            this.ErrorProvider1 = new ErrorProvider(this.components);
            ((ISupportInitialize) this.wizard).BeginInit();
            this.wpWelcome.SuspendLayout();
            this.wpCustomer.SuspendLayout();
            this.wpInvoices.SuspendLayout();
            this.wpSchedule.SuspendLayout();
            this.wpSummary.SuspendLayout();
            this.wpReport.SuspendLayout();
            ((ISupportInitialize) this.ErrorProvider1).BeginInit();
            base.SuspendLayout();
            this.wizard.Dock = DockStyle.Fill;
            this.wizard.Location = new Point(0, 0);
            this.wizard.Name = "wizard";
            WizardPage[] pages = new WizardPage[] { this.wpWelcome, this.wpCustomer, this.wpInvoices, this.wpSchedule, this.wpSummary, this.wpReport };
            this.wizard.Pages.AddRange(pages);
            this.wizard.Renderer = renderer;
            this.wizard.Size = new Size(0x1fd, 0x169);
            this.wizard.TabIndex = 0;
            this.wizard.Text = "Wizard1";
            this.wpWelcome.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpWelcome.BackColor = SystemColors.Window;
            this.wpWelcome.Controls.Add(this.Label3);
            this.wpWelcome.Controls.Add(this.Label2);
            this.wpWelcome.Controls.Add(this.Label1);
            this.wpWelcome.HelpButtonVisible = false;
            this.wpWelcome.IsInteriorPage = false;
            this.wpWelcome.Location = new Point(0, 0);
            this.wpWelcome.Name = "wpWelcome";
            this.wpWelcome.PageCaption = "";
            this.wpWelcome.PageDescription = "";
            this.wpWelcome.Size = new Size(0x1fd, 0x142);
            this.wpWelcome.TabIndex = 2;
            this.Label3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.Label3.AutoSize = true;
            this.Label3.Location = new Point(0xb2, 0x126);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(120, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "To continue, click Next.";
            this.Label2.Location = new Point(0xb2, 0x45);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x13c, 0xf4);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Enter a brief description of the wizard here.";
            this.Label1.Font = new Font("Verdana", 12f, FontStyle.Bold);
            this.Label1.Location = new Point(0xb0, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x135, 0x37);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Welcome to the New Payment Plan Wizard";
            this.wpCustomer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpCustomer.Controls.Add(this.txtCustomer);
            this.wpCustomer.Controls.Add(this.lblCustomer);
            this.wpCustomer.HelpButtonVisible = false;
            this.wpCustomer.Location = new Point(0x10, 0x4c);
            this.wpCustomer.Name = "wpCustomer";
            this.wpCustomer.PageCaption = "Select Customer";
            this.wpCustomer.PageDescription = "Select customer for whom you suppose to create payment plan schedule";
            this.wpCustomer.Size = new Size(0x37f, 0x160);
            this.wpCustomer.TabIndex = 0;
            this.wpCustomer.Text = "WizardPage1";
            this.txtCustomer.BorderStyle = BorderStyle.Fixed3D;
            this.txtCustomer.Location = new Point(0x10, 0x20);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new Size(0x1c0, 0x17);
            this.txtCustomer.TabIndex = 2;
            this.txtCustomer.Text = "txtCustomer";
            this.lblCustomer.Location = new Point(0, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(100, 0x17);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer :";
            this.wpInvoices.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpInvoices.Controls.Add(this.gridInvoices);
            this.wpInvoices.Controls.Add(this.lblInvoices);
            this.wpInvoices.HelpButtonVisible = false;
            this.wpInvoices.Location = new Point(0x10, 0x4c);
            this.wpInvoices.Name = "wpInvoices";
            this.wpInvoices.PageCaption = "Select Invoices";
            this.wpInvoices.PageDescription = "Select invoices attached to this payment plan";
            this.wpInvoices.Size = new Size(0x37f, 0x160);
            this.wpInvoices.TabIndex = 1;
            this.wpInvoices.Text = "WizardPage1";
            this.gridInvoices.Dock = DockStyle.Fill;
            this.gridInvoices.Location = new Point(0, 0x17);
            this.gridInvoices.Name = "gridInvoices";
            this.gridInvoices.Size = new Size(0x37f, 0x149);
            this.gridInvoices.TabIndex = 2;
            this.lblInvoices.Dock = DockStyle.Top;
            this.lblInvoices.Location = new Point(0, 0);
            this.lblInvoices.Name = "lblInvoices";
            this.lblInvoices.Size = new Size(0x37f, 0x17);
            this.lblInvoices.TabIndex = 1;
            this.lblInvoices.Text = "Invoices :";
            this.wpSchedule.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpSchedule.Controls.Add(this.nmbPaymentCount);
            this.wpSchedule.Controls.Add(this.txtLastPaymentDate);
            this.wpSchedule.Controls.Add(this.lblNumberOfPayments);
            this.wpSchedule.Controls.Add(this.lblLastPaymentDate);
            this.wpSchedule.Controls.Add(this.txtTotalAmount);
            this.wpSchedule.Controls.Add(this.lblTotalAmount);
            this.wpSchedule.Controls.Add(this.dtbFirstPayment);
            this.wpSchedule.Controls.Add(this.nmbPaymentAmount);
            this.wpSchedule.Controls.Add(this.cmbPeriod);
            this.wpSchedule.Controls.Add(this.lblPeriod);
            this.wpSchedule.Controls.Add(this.lblTo);
            this.wpSchedule.Controls.Add(this.lblFirstPayment);
            this.wpSchedule.Controls.Add(this.lblPaymentAmount);
            this.wpSchedule.HelpButtonVisible = false;
            this.wpSchedule.Location = new Point(0x10, 0x4c);
            this.wpSchedule.Name = "wpSchedule";
            this.wpSchedule.PageCaption = "Schedule";
            this.wpSchedule.PageDescription = "Select details of payments schedule";
            this.wpSchedule.Size = new Size(0x37f, 0x160);
            this.wpSchedule.TabIndex = 3;
            this.nmbPaymentCount.Location = new Point(0x10, 80);
            this.nmbPaymentCount.Name = "nmbPaymentCount";
            this.nmbPaymentCount.Size = new Size(0x60, 20);
            this.nmbPaymentCount.TabIndex = 3;
            this.txtLastPaymentDate.BorderStyle = BorderStyle.Fixed3D;
            this.txtLastPaymentDate.Location = new Point(0x108, 0x88);
            this.txtLastPaymentDate.Name = "txtLastPaymentDate";
            this.txtLastPaymentDate.Size = new Size(0x60, 0x15);
            this.txtLastPaymentDate.TabIndex = 10;
            this.txtLastPaymentDate.Text = "10";
            this.txtLastPaymentDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblNumberOfPayments.BorderStyle = BorderStyle.FixedSingle;
            this.lblNumberOfPayments.Location = new Point(0, 0x38);
            this.lblNumberOfPayments.Name = "lblNumberOfPayments";
            this.lblNumberOfPayments.Size = new Size(100, 20);
            this.lblNumberOfPayments.TabIndex = 2;
            this.lblNumberOfPayments.Text = "# payments";
            this.lblLastPaymentDate.BorderStyle = BorderStyle.FixedSingle;
            this.lblLastPaymentDate.Location = new Point(0xf8, 0x70);
            this.lblLastPaymentDate.Name = "lblLastPaymentDate";
            this.lblLastPaymentDate.Size = new Size(100, 20);
            this.lblLastPaymentDate.TabIndex = 9;
            this.lblLastPaymentDate.Text = "To Date :";
            this.txtTotalAmount.BorderStyle = BorderStyle.Fixed3D;
            this.txtTotalAmount.Location = new Point(0x10, 0x18);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new Size(0x60, 20);
            this.txtTotalAmount.TabIndex = 1;
            this.txtTotalAmount.Text = "495.00";
            this.txtTotalAmount.TextAlign = ContentAlignment.MiddleRight;
            this.lblTotalAmount.BorderStyle = BorderStyle.FixedSingle;
            this.lblTotalAmount.Location = new Point(0, 0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new Size(100, 20);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "Total Amount :";
            this.dtbFirstPayment.DateTime = new DateTime(0x7da, 5, 0x15, 0, 0, 0, 0);
            this.dtbFirstPayment.Location = new Point(0x90, 0x88);
            this.dtbFirstPayment.Name = "dtbFirstPayment";
            this.dtbFirstPayment.Size = new Size(0x60, 0x15);
            this.dtbFirstPayment.TabIndex = 7;
            this.dtbFirstPayment.Value = new DateTime(0x7da, 5, 0x15, 0, 0, 0, 0);
            this.nmbPaymentAmount.Location = new Point(0x10, 0xc0);
            this.nmbPaymentAmount.Name = "nmbPaymentAmount";
            this.nmbPaymentAmount.Size = new Size(0x60, 20);
            this.nmbPaymentAmount.TabIndex = 12;
            this.cmbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Location = new Point(0x10, 0x88);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new Size(0x60, 0x15);
            this.cmbPeriod.TabIndex = 5;
            this.lblPeriod.BorderStyle = BorderStyle.FixedSingle;
            this.lblPeriod.Location = new Point(0, 0x70);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new Size(100, 20);
            this.lblPeriod.TabIndex = 4;
            this.lblPeriod.Text = "Period :";
            this.lblTo.Location = new Point(240, 0x88);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new Size(0x18, 20);
            this.lblTo.TabIndex = 8;
            this.lblTo.Text = " - ";
            this.lblTo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblFirstPayment.BorderStyle = BorderStyle.FixedSingle;
            this.lblFirstPayment.Location = new Point(0x80, 0x70);
            this.lblFirstPayment.Name = "lblFirstPayment";
            this.lblFirstPayment.Size = new Size(100, 20);
            this.lblFirstPayment.TabIndex = 6;
            this.lblFirstPayment.Text = "First Payment :";
            this.lblPaymentAmount.BorderStyle = BorderStyle.FixedSingle;
            this.lblPaymentAmount.Location = new Point(0, 0xa8);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new Size(100, 20);
            this.lblPaymentAmount.TabIndex = 11;
            this.lblPaymentAmount.Text = "Payment Amount :";
            this.wpSummary.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpSummary.Controls.Add(this.txtSummary);
            this.wpSummary.HelpButtonVisible = false;
            this.wpSummary.Location = new Point(0x10, 0x4c);
            this.wpSummary.Name = "wpSummary";
            this.wpSummary.PageCaption = "Review";
            this.wpSummary.PageDescription = "Review schedule";
            this.wpSummary.Size = new Size(0x5f2, 0x217);
            this.wpSummary.TabIndex = 4;
            this.txtSummary.Dock = DockStyle.Fill;
            this.txtSummary.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtSummary.Location = new Point(0, 0);
            this.txtSummary.MaxLength = 0;
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ReadOnly = true;
            this.txtSummary.ScrollBars = ScrollBars.Both;
            this.txtSummary.Size = new Size(0x5f2, 0x217);
            this.txtSummary.TabIndex = 0;
            this.txtSummary.Text = "Some text goes here";
            this.wpReport.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.wpReport.CancelButtonVisible = false;
            this.wpReport.Controls.Add(this.txtReport);
            this.wpReport.HelpButtonVisible = false;
            this.wpReport.Location = new Point(0x10, 0x4c);
            this.wpReport.Name = "wpReport";
            this.wpReport.PageCaption = "Report";
            this.wpReport.PageDescription = "Payment plan creation report";
            this.wpReport.Size = new Size(0x865, 0x2ce);
            this.wpReport.TabIndex = 5;
            this.txtReport.Dock = DockStyle.Fill;
            this.txtReport.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtReport.Location = new Point(0, 0);
            this.txtReport.MaxLength = 0;
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.ReadOnly = true;
            this.txtReport.ScrollBars = ScrollBars.Both;
            this.txtReport.Size = new Size(0x865, 0x2ce);
            this.txtReport.TabIndex = 1;
            this.txtReport.Text = "Some text goes here";
            this.ErrorProvider1.ContainerControl = this;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1fd, 0x169);
            base.Controls.Add(this.wizard);
            base.Name = "WizardPaymentPlan";
            this.Text = "Create/Modify Payment Plan";
            ((ISupportInitialize) this.wizard).EndInit();
            this.wpWelcome.ResumeLayout(false);
            this.wpWelcome.PerformLayout();
            this.wpCustomer.ResumeLayout(false);
            this.wpInvoices.ResumeLayout(false);
            this.wpSchedule.ResumeLayout(false);
            this.wpSummary.ResumeLayout(false);
            this.wpSummary.PerformLayout();
            this.wpReport.ResumeLayout(false);
            this.wpReport.PerformLayout();
            ((ISupportInitialize) this.ErrorProvider1).EndInit();
            base.ResumeLayout(false);
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AllowEdit = true;
            Appearance.AllowNew = false;
            Appearance.AllowDelete = false;
            Appearance.AutoGenerateColumns = false;
            Appearance.EditMode = DataGridViewEditMode.EditOnEnter;
            Appearance.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            Appearance.Columns.Clear();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle(Appearance.BoolStyle()) {
                BackColor = Color.LightSteelBlue
            };
            DataGridViewCheckBoxColumn column1 = Appearance.AddBoolColumn("Selected", "...", 50, cellStyle);
            column1.ReadOnly = false;
            column1.ThreeState = false;
            DataGridViewCellStyle style2 = new DataGridViewCellStyle(Appearance.PriceStyle()) {
                BackColor = Color.LightSteelBlue
            };
            Appearance.AddTextColumn("PlanAmount", "Plan Amt", 60, style2).ReadOnly = false;
            Appearance.AddTextColumn("DOSFrom", "DOS", 70, Appearance.DateStyle());
            Appearance.AddTextColumn("BillingCode", "B.Code", 0x37);
            Appearance.AddTextColumn("Balance", "Balance", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("CurrentPayer", "Payer", 0x37);
        }

        private void InitStage_Customer()
        {
        }

        private void InitStage_Invoices()
        {
        }

        private void InitStage_Report()
        {
            bool flag = false;
            try
            {
                TableInvoiceDetails tableSource = this.gridInvoices.GetTableSource<TableInvoiceDetails>();
                if (tableSource == null)
                {
                    throw new UserNotifyException("You have to select invoices");
                }
                using (StringWriter writer = new StringWriter())
                {
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                            command.Parameters.Add("Period", MySqlType.VarChar, 0x10).Value = PeriodToString(this.GetPeriod());
                            command.Parameters.Add("FirstPayment", MySqlType.Date).Value = this.GetFirstPayment();
                            command.Parameters.Add("PaymentCount", MySqlType.Int).Value = this.GetPaymentCount();
                            command.Parameters.Add("PaymentAmount", MySqlType.Double).Value = this.GetPaymentAmount();
                            command.Parameters.Add("Details", MySqlType.Text, 0xffff).Value = DatasetPaymentPlan.ToXml(tableSource.ToDataset());
                            command.Parameters.Add("LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                            if (0 >= command.ExecuteInsert("tbl_paymentplan"))
                            {
                                throw new UserNotifyException("Cannot store payment plan");
                            }
                            writer.WriteLine("Payment plan was created");
                        }
                    }
                    flag = true;
                    this.txtReport.Text = writer.ToString();
                }
            }
            catch (UserNotifyException exception1)
            {
                UserNotifyException ex = exception1;
                ProjectData.SetProjectError(ex);
                UserNotifyException exception = ex;
                this.txtReport.Text = exception.Message;
                ProjectData.ClearProjectError();
            }
            catch (Exception exception3)
            {
                Exception ex = exception3;
                ProjectData.SetProjectError(ex);
                this.txtReport.Text = "Exception\r\n\r\n" + ex.ToString();
                ProjectData.ClearProjectError();
            }
            this.wpReport.FinishButtonVisible = flag;
            this.wpReport.CancelButtonVisible = !flag;
            this.wpReport.NextButtonVisible = !flag;
            this.wpReport.BackButtonVisible = !flag;
        }

        private void InitStage_Schedule()
        {
            double num = 0.0;
            TableInvoiceDetails tableSource = this.gridInvoices.GetTableSource<TableInvoiceDetails>();
            if (tableSource != null)
            {
                num = Convert.ToDouble(tableSource.TotalSelectedAmount);
            }
            this.txtTotalAmount.Text = num.ToString("0.00");
            this.ValidateSchedule();
            this.CalculateLastPayment();
        }

        private void InitStage_Summary()
        {
            try
            {
                TableInvoiceDetails tableSource = this.gridInvoices.GetTableSource<TableInvoiceDetails>();
                using (StringWriter writer = new StringWriter())
                {
                    writer.WriteLine("Customer : ({0}) {1}", this.CustomerID, this.txtCustomer.Text);
                    writer.WriteLine("Period : '{0}'", PeriodToString(this.GetPeriod()));
                    writer.WriteLine("First Payment : {0:d}", this.GetFirstPayment());
                    writer.WriteLine("Last Payment : {0:d}", this.GetLastPayment());
                    writer.WriteLine("Payment Count : '{0}'", this.GetPaymentCount());
                    writer.WriteLine("Payment Amount : {0:0.00}", this.GetPaymentAmount());
                    if (tableSource == null)
                    {
                        writer.WriteLine("Nothing selected");
                    }
                    else
                    {
                        DataRow[] rowArray = tableSource.Select("([Selected] = true) AND (0.01 <= [PlanAmount])");
                        if (rowArray.Length == 0)
                        {
                            writer.WriteLine("Nothing selected");
                        }
                        else
                        {
                            writer.WriteLine("Selected invoice items:");
                            foreach (DataRow row in rowArray)
                            {
                                writer.WriteLine("#{0} Amount {1:0.00}", row[tableSource.Col_ID], row[tableSource.Col_PlanAmount]);
                            }
                        }
                    }
                    this.txtSummary.Text = writer.ToString();
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                this.txtSummary.Text = ex.ToString();
                ProjectData.ClearProjectError();
            }
        }

        private void LoadComboBoxes()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_paymentplan", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbPeriod, table, "Period");
            }
        }

        private void LoadPlan()
        {
            this.AttachTable(null);
            TableInvoiceDetails dataTable = new TableInvoiceDetails("tbl_details");
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_customer WHERE ID = :CustomerID";
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new UserNotifyException("Specified customer does not exist");
                        }
                        this.txtCustomer.Text = reader["FirstName"] + " " + reader["LastName"];
                    }
                }
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                {
                    adapter.SelectCommand.CommandText = "SELECT\r\n  ID\r\n, DOSFrom\r\n, BillableAmount\r\n, BillingCode\r\n, CurrentPayer\r\n, BillableAmount - PaymentAmount - WriteoffAmount as Balance\r\nFROM tbl_invoicedetails\r\nWHERE CustomerID = :CustomerID\r\nORDER BY ID DESC";
                    adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                    adapter.AcceptChangesDuringFill = true;
                    adapter.MissingSchemaAction = MissingSchemaAction.Add;
                    adapter.Fill(dataTable);
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.CommandText = "SELECT *\r\nFROM tbl_paymentplan\r\nWHERE ID IN (SELECT MAX(ID) FROM tbl_paymentplan WHERE CustomerID = :CustomerID)";
                    command2.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                    using (MySqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (!reader2.Read())
                        {
                            Functions.SetComboBoxText(this.cmbPeriod, DBNull.Value);
                            Functions.SetDateBoxValue(this.dtbFirstPayment, DBNull.Value);
                            Functions.SetNumericBoxValue(this.nmbPaymentAmount, DBNull.Value);
                            Functions.SetNumericBoxValue(this.nmbPaymentCount, DBNull.Value);
                        }
                        else
                        {
                            Functions.SetComboBoxText(this.cmbPeriod, reader2["Period"]);
                            Functions.SetDateBoxValue(this.dtbFirstPayment, reader2["FirstPayment"]);
                            Functions.SetNumericBoxValue(this.nmbPaymentAmount, reader2["PaymentAmount"]);
                            Functions.SetNumericBoxValue(this.nmbPaymentCount, reader2["PaymentCount"]);
                            dataTable.ApplyDataset(DatasetPaymentPlan.FromXml(NullableConvert.ToString(reader2["Details"])));
                        }
                    }
                }
            }
            this.AttachTable(dataTable);
        }

        private void nmbAmount_ValueChanged(object sender, EventArgs e)
        {
            this.ValidateSchedule();
        }

        private void nmbPaymentCount_ValueChanged(object sender, EventArgs e)
        {
            this.ValidateSchedule();
            this.CalculateLastPayment();
        }

        private static string PeriodToString(Period Value)
        {
            string str;
            switch (Value)
            {
                case Period.Weekly:
                    str = "Weekly";
                    break;

                case Period.Biweekly:
                    str = "Bi-weekly";
                    break;

                case Period.Monthly:
                    str = "Monthly";
                    break;

                default:
                    throw new UserNotifyException("Period " + Value.ToString() + " is not supported");
            }
            return str;
        }

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            TableInvoiceDetails table = e.Row.Table as TableInvoiceDetails;
            if ((table != null) && table.IsValid)
            {
                this.wpInvoices.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
            }
            else
            {
                this.wpInvoices.NextButtonEnabled = WizardButtonEnabledDefault.False;
            }
        }

        private bool ValidateInvoices()
        {
            bool flag;
            TableInvoiceDetails tableSource = this.gridInvoices.GetTableSource<TableInvoiceDetails>();
            if (tableSource == null)
            {
                flag = false;
            }
            else if (tableSource.IsValid)
            {
                flag = true;
            }
            else
            {
                MessageBox.Show(this, "Select participating invoice lines and enter amount to be paid for each selected line");
                flag = false;
            }
            return flag;
        }

        private void ValidateSchedule()
        {
            try
            {
                this.GetPeriod();
                this.GetPaymentAmount();
                this.GetPaymentCount();
                this.GetFirstPayment();
                this.wpSchedule.NextButtonEnabled = WizardButtonEnabledDefault.Auto;
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.wpSchedule.NextButtonEnabled = WizardButtonEnabledDefault.False;
                ProjectData.ClearProjectError();
            }
        }

        private void wizard_BackButtonClick(object sender, WizardPageCancelEventArgs e)
        {
            e.Cancel = true;
            if (ReferenceEquals(this.wizard.SelectedPage, this.wpWelcome))
            {
                this.wizard.SelectedPage = this.wpWelcome;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpCustomer))
            {
                this.wizard.SelectedPage = this.wpWelcome;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpInvoices))
            {
                this.wizard.SelectedPage = this.wpCustomer;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSchedule))
            {
                this.wizard.SelectedPage = this.wpInvoices;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSummary))
            {
                this.wizard.SelectedPage = this.wpSchedule;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpReport))
            {
                this.wizard.SelectedPage = this.wpSummary;
            }
        }

        private void wizard_CancelButtonClick(object sender, EventArgs e)
        {
            base.Close();
        }

        private void wizard_FinishButtonClick(object sender, EventArgs e)
        {
            base.Close();
        }

        private void wizard_NextButtonClick(object sender, WizardPageCancelEventArgs e)
        {
            e.Cancel = true;
            if (ReferenceEquals(this.wizard.SelectedPage, this.wpWelcome))
            {
                this.wizard.SelectedPage = this.wpCustomer;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpCustomer))
            {
                this.wizard.SelectedPage = this.wpInvoices;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpInvoices))
            {
                if (this.ValidateInvoices())
                {
                    this.wizard.SelectedPage = this.wpSchedule;
                }
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSchedule))
            {
                this.wizard.SelectedPage = this.wpSummary;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSummary))
            {
                this.wizard.SelectedPage = this.wpReport;
            }
            else if (ReferenceEquals(this.wizard.SelectedPage, this.wpReport))
            {
                this.wizard.SelectedPage = this.wpReport;
            }
        }

        private void wizard_SelectionChanged(object sender, EventArgs e)
        {
            if (!ReferenceEquals(this.wizard.SelectedPage, this.wpWelcome))
            {
                if (ReferenceEquals(this.wizard.SelectedPage, this.wpCustomer))
                {
                    this.InitStage_Customer();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpInvoices))
                {
                    this.InitStage_Invoices();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSchedule))
                {
                    this.InitStage_Schedule();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpSummary))
                {
                    this.InitStage_Summary();
                }
                else if (ReferenceEquals(this.wizard.SelectedPage, this.wpReport))
                {
                    this.InitStage_Report();
                }
            }
        }

        [field: AccessedThroughProperty("wpInvoices")]
        private WizardPage wpInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpWelcome")]
        private WizardWelcomePage wpWelcome { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpCustomer")]
        private WizardPage wpCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpSchedule")]
        private WizardPage wpSchedule { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpSummary")]
        private WizardPage wpSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gridInvoices")]
        private FilteredGrid gridInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoices")]
        private Label lblInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPaymentAmount")]
        private NumericBox nmbPaymentAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPeriod")]
        private ComboBox cmbPeriod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPeriod")]
        private Label lblPeriod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTo")]
        private Label lblTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFirstPayment")]
        private Label lblFirstPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPaymentAmount")]
        private Label lblPaymentAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbFirstPayment")]
        private UltraDateTimeEditor dtbFirstPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTotalAmount")]
        private Label lblTotalAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTotalAmount")]
        private Label txtTotalAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider1")]
        private ErrorProvider ErrorProvider1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLastPaymentDate")]
        private Label lblLastPaymentDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLastPaymentDate")]
        private Label txtLastPaymentDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNumberOfPayments")]
        private Label lblNumberOfPayments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wizard")]
        private ActiproSoftware.Wizard.Wizard wizard { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSummary")]
        private TextBox txtSummary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomer")]
        private Label txtCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("wpReport")]
        private WizardPage wpReport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtReport")]
        private TextBox txtReport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPaymentCount")]
        private NumericBox nmbPaymentCount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private enum Period
        {
            Weekly,
            Biweekly,
            Monthly
        }

        private class TableInvoiceDetails : TableBase
        {
            private DataColumn _col_Selected;
            private DataColumn _col_ID;
            private DataColumn _col_PlanAmount;
            public const string SelectedRowsExpression = "([Selected] = true)";
            public const string SuitableRowsExpression = "([Selected] = true) AND (0.01 <= [PlanAmount])";

            public TableInvoiceDetails()
            {
            }

            public TableInvoiceDetails(string TableName) : base(TableName)
            {
            }

            public void ApplyDataset(DatasetPaymentPlan ds)
            {
                if (ds != null)
                {
                    foreach (DataRow row in base.Select("", "ID", DataViewRowState.CurrentRows))
                    {
                        int? nullable = NullableConvert.ToInt32(row[this.Col_ID]);
                        if (nullable != null)
                        {
                            DatasetPaymentPlan.TableInvoiceDetailsRow row2 = ds.TableInvoiceDetails.FindByID(nullable.Value);
                            if (row2 != null)
                            {
                                row[this.Col_Selected] = true;
                                row[this.Col_PlanAmount] = row2.PlanAmount;
                            }
                            else
                            {
                                row[this.Col_Selected] = false;
                                row[this.Col_PlanAmount] = DBNull.Value;
                            }
                            row.AcceptChanges();
                        }
                    }
                }
            }

            protected override void Initialize()
            {
                this._col_Selected = base.Columns["Selected"];
                this._col_ID = base.Columns["ID"];
                this._col_PlanAmount = base.Columns["PlanAmount"];
            }

            protected override void InitializeClass()
            {
                DataColumn column1 = base.Columns.Add("Selected", typeof(bool));
                column1.AllowDBNull = false;
                column1.DefaultValue = false;
                DataColumn column2 = base.Columns.Add("ID", typeof(int));
                column2.AllowDBNull = false;
                column2.DefaultValue = false;
                base.Columns.Add("PlanAmount", typeof(decimal));
            }

            protected override void OnColumnChanged(DataColumnChangeEventArgs e)
            {
                if (!ReferenceEquals(e.Column, this.Col_Selected))
                {
                    if (ReferenceEquals(e.Column, this.Col_PlanAmount))
                    {
                        e.Row.BeginEdit();
                        e.Row.EndEdit();
                    }
                }
                else
                {
                    if (true.Equals(e.Row[e.Column]))
                    {
                        e.Row[this.Col_PlanAmount] = e.Row["Balance"];
                    }
                    else if (false.Equals(e.Row[e.Column]))
                    {
                        e.Row[this.Col_PlanAmount] = DBNull.Value;
                    }
                    e.Row.BeginEdit();
                    e.Row.EndEdit();
                }
                base.OnColumnChanged(e);
            }

            public DatasetPaymentPlan ToDataset()
            {
                DatasetPaymentPlan plan = new DatasetPaymentPlan();
                DataRow[] rowArray = base.Select("([Selected] = true) AND (0.01 <= [PlanAmount])", "ID", DataViewRowState.CurrentRows);
                for (int i = 0; i < rowArray.Length; i++)
                {
                    DataRow row1 = rowArray[i];
                    int? nullable = NullableConvert.ToInt32(row1[this.Col_ID]);
                    double? nullable2 = NullableConvert.ToDouble(row1[this.Col_PlanAmount]);
                    if ((nullable != null) && ((nullable2 != null) && (nullable2.Value > 0.0)))
                    {
                        plan.TableInvoiceDetails.AddTableInvoiceDetailsRow(nullable.Value, nullable2.Value);
                    }
                }
                plan.AcceptChanges();
                return plan;
            }

            public DataColumn Col_Selected =>
                this._col_Selected;

            public DataColumn Col_ID =>
                this._col_ID;

            public DataColumn Col_PlanAmount =>
                this._col_PlanAmount;

            public bool IsValid
            {
                get
                {
                    int num = Convert.ToInt32(base.Compute("COUNT([Selected])", "([Selected] = true)"));
                    int num2 = Convert.ToInt32(base.Compute("COUNT([Selected])", "([Selected] = true) AND (0.01 <= [PlanAmount])"));
                    return ((0 < num) && (num == num2));
                }
            }

            public decimal TotalSelectedAmount =>
                NullableConvert.ToDecimal(base.Compute("SUM([PlanAmount])", "([Selected] = true) AND (0.01 <= [PlanAmount])")).GetValueOrDefault(0M);
        }
    }
}

