namespace DMEWorks.Forms.PaymentPlan
{
    using Devart.Data.MySql;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormPaymentPlan : DmeForm
    {
        private IContainer components;
        private readonly int CustomerID;
        private const string CrLf = "\r\n";

        public FormPaymentPlan(int CustomerID)
        {
            base.Load += new EventHandler(this.FormPaymentPlan_Load);
            this.CustomerID = CustomerID;
            this.InitializeComponent();
            this.tsbModify.Enabled = Permissions.FormPaymentPlan.Allow_ADD_EDIT;
            this.InitializeGrid_Invoice(this.sgInvoices.Appearance);
            this.InitializeGrid_Payments(this.sgPayments.Appearance);
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

        private void FormPaymentPlan_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadPlan();
                this.LoadPayments();
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormPaymentPlan));
            this.ToolStrip1 = new ToolStrip();
            this.tsbModify = new ToolStripButton();
            this.tsbPayment = new ToolStripButton();
            this.tsbClose = new ToolStripButton();
            this.tpGeneral = new TabControl();
            this.TabPage1 = new TabPage();
            this.PnlPlan = new Panel();
            this.sgInvoices = new FilteredGrid();
            this.LblInvoices = new Label();
            this.pnlDetails = new Panel();
            this.LblFirstPayment = new Label();
            this.TxtPaymentAmount = new Label();
            this.TxtPaymentCount = new Label();
            this.TxtPeriod = new Label();
            this.LblPaymentAmount = new Label();
            this.LblPeriod = new Label();
            this.LblPaymentCount = new Label();
            this.TxtFirstPayment = new Label();
            this.LblNoPlan = new Label();
            this.pnlCustomer = new Panel();
            this.Lbl = new Label();
            this.txtCustomer = new Label();
            this.tpHistory = new TabPage();
            this.sgPayments = new FilteredGrid();
            this.ToolStrip1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.PnlPlan.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.tpHistory.SuspendLayout();
            base.SuspendLayout();
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbModify, this.tsbPayment, this.tsbClose };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x278, 0x19);
            this.ToolStrip1.TabIndex = 0;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbModify.Image = My.Resources.Resources.ImageEdit;
            this.tsbModify.ImageTransparentColor = Color.Magenta;
            this.tsbModify.Name = "tsbModify";
            this.tsbModify.Size = new Size(0x3b, 0x16);
            this.tsbModify.Text = "Modify";
            this.tsbPayment.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbPayment.Image = (Image) manager.GetObject("tsbPayment.Image");
            this.tsbPayment.ImageTransparentColor = Color.Magenta;
            this.tsbPayment.Name = "tsbPayment";
            this.tsbPayment.Size = new Size(0x35, 0x16);
            this.tsbPayment.Text = "Payment";
            this.tsbClose.Image = My.Resources.Resources.ImageClose;
            this.tsbClose.ImageTransparentColor = Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new Size(0x35, 0x16);
            this.tsbClose.Text = "Close";
            this.tpGeneral.Appearance = TabAppearance.FlatButtons;
            this.tpGeneral.Controls.Add(this.TabPage1);
            this.tpGeneral.Controls.Add(this.tpHistory);
            this.tpGeneral.Dock = DockStyle.Fill;
            this.tpGeneral.Location = new Point(0, 0x19);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.SelectedIndex = 0;
            this.tpGeneral.Size = new Size(0x278, 0x1ac);
            this.tpGeneral.TabIndex = 1;
            this.TabPage1.Controls.Add(this.PnlPlan);
            this.TabPage1.Controls.Add(this.LblNoPlan);
            this.TabPage1.Controls.Add(this.pnlCustomer);
            this.TabPage1.Location = new Point(4, 0x19);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new Size(0x270, 0x18f);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "General";
            this.TabPage1.UseVisualStyleBackColor = true;
            this.PnlPlan.Controls.Add(this.sgInvoices);
            this.PnlPlan.Controls.Add(this.LblInvoices);
            this.PnlPlan.Controls.Add(this.pnlDetails);
            this.PnlPlan.Dock = DockStyle.Fill;
            this.PnlPlan.Location = new Point(0, 0x68);
            this.PnlPlan.Name = "PnlPlan";
            this.PnlPlan.Size = new Size(0x270, 0x127);
            this.PnlPlan.TabIndex = 3;
            this.sgInvoices.Dock = DockStyle.Fill;
            this.sgInvoices.Location = new Point(0, 0x4a);
            this.sgInvoices.Name = "sgInvoices";
            this.sgInvoices.Size = new Size(0x270, 0xdd);
            this.sgInvoices.TabIndex = 2;
            this.LblInvoices.BackColor = SystemColors.ActiveCaption;
            this.LblInvoices.Dock = DockStyle.Top;
            this.LblInvoices.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.LblInvoices.ForeColor = SystemColors.ActiveCaptionText;
            this.LblInvoices.Location = new Point(0, 0x38);
            this.LblInvoices.Name = "LblInvoices";
            this.LblInvoices.Size = new Size(0x270, 0x12);
            this.LblInvoices.TabIndex = 1;
            this.LblInvoices.Text = "Invoices";
            this.pnlDetails.Controls.Add(this.LblFirstPayment);
            this.pnlDetails.Controls.Add(this.TxtPaymentAmount);
            this.pnlDetails.Controls.Add(this.TxtPaymentCount);
            this.pnlDetails.Controls.Add(this.TxtPeriod);
            this.pnlDetails.Controls.Add(this.LblPaymentAmount);
            this.pnlDetails.Controls.Add(this.LblPeriod);
            this.pnlDetails.Controls.Add(this.LblPaymentCount);
            this.pnlDetails.Controls.Add(this.TxtFirstPayment);
            this.pnlDetails.Dock = DockStyle.Top;
            this.pnlDetails.Location = new Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new Size(0x270, 0x38);
            this.pnlDetails.TabIndex = 0;
            this.LblFirstPayment.BackColor = SystemColors.GradientActiveCaption;
            this.LblFirstPayment.Location = new Point(8, 8);
            this.LblFirstPayment.Name = "LblFirstPayment";
            this.LblFirstPayment.Size = new Size(80, 0x15);
            this.LblFirstPayment.TabIndex = 0;
            this.LblFirstPayment.Text = "First Payment";
            this.LblFirstPayment.TextAlign = ContentAlignment.MiddleRight;
            this.TxtPaymentAmount.BorderStyle = BorderStyle.Fixed3D;
            this.TxtPaymentAmount.Location = new Point(0x128, 0x20);
            this.TxtPaymentAmount.Name = "TxtPaymentAmount";
            this.TxtPaymentAmount.Size = new Size(80, 0x15);
            this.TxtPaymentAmount.TabIndex = 7;
            this.TxtPaymentAmount.Text = "495.00";
            this.TxtPaymentAmount.TextAlign = ContentAlignment.MiddleRight;
            this.TxtPaymentCount.BorderStyle = BorderStyle.Fixed3D;
            this.TxtPaymentCount.Location = new Point(0x128, 8);
            this.TxtPaymentCount.Name = "TxtPaymentCount";
            this.TxtPaymentCount.Size = new Size(80, 0x15);
            this.TxtPaymentCount.TabIndex = 5;
            this.TxtPaymentCount.Text = "10";
            this.TxtPaymentCount.TextAlign = ContentAlignment.MiddleRight;
            this.TxtPeriod.BorderStyle = BorderStyle.Fixed3D;
            this.TxtPeriod.Location = new Point(0x60, 0x20);
            this.TxtPeriod.Name = "TxtPeriod";
            this.TxtPeriod.Size = new Size(80, 0x15);
            this.TxtPeriod.TabIndex = 3;
            this.TxtPeriod.Text = "Bi-Weekly";
            this.TxtPeriod.TextAlign = ContentAlignment.MiddleLeft;
            this.LblPaymentAmount.BackColor = SystemColors.GradientActiveCaption;
            this.LblPaymentAmount.Location = new Point(0xc0, 0x20);
            this.LblPaymentAmount.Name = "LblPaymentAmount";
            this.LblPaymentAmount.Size = new Size(0x60, 0x15);
            this.LblPaymentAmount.TabIndex = 6;
            this.LblPaymentAmount.Text = "Payment Amount";
            this.LblPaymentAmount.TextAlign = ContentAlignment.MiddleRight;
            this.LblPeriod.BackColor = SystemColors.GradientActiveCaption;
            this.LblPeriod.Location = new Point(8, 0x20);
            this.LblPeriod.Name = "LblPeriod";
            this.LblPeriod.Size = new Size(80, 0x15);
            this.LblPeriod.TabIndex = 2;
            this.LblPeriod.Text = "Period";
            this.LblPeriod.TextAlign = ContentAlignment.MiddleRight;
            this.LblPaymentCount.BackColor = SystemColors.GradientActiveCaption;
            this.LblPaymentCount.Location = new Point(0xc0, 8);
            this.LblPaymentCount.Name = "LblPaymentCount";
            this.LblPaymentCount.Size = new Size(0x60, 0x15);
            this.LblPaymentCount.TabIndex = 4;
            this.LblPaymentCount.Text = "Payment Count";
            this.LblPaymentCount.TextAlign = ContentAlignment.MiddleRight;
            this.TxtFirstPayment.BorderStyle = BorderStyle.Fixed3D;
            this.TxtFirstPayment.Location = new Point(0x60, 8);
            this.TxtFirstPayment.Name = "TxtFirstPayment";
            this.TxtFirstPayment.Size = new Size(80, 0x15);
            this.TxtFirstPayment.TabIndex = 1;
            this.TxtFirstPayment.Text = "2010-99-99";
            this.TxtFirstPayment.TextAlign = ContentAlignment.MiddleLeft;
            this.LblNoPlan.Dock = DockStyle.Top;
            this.LblNoPlan.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.LblNoPlan.Location = new Point(0, 0x20);
            this.LblNoPlan.Name = "LblNoPlan";
            this.LblNoPlan.Size = new Size(0x270, 0x48);
            this.LblNoPlan.TabIndex = 1;
            this.LblNoPlan.Text = "Customer does not have active payment plan";
            this.LblNoPlan.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlCustomer.Controls.Add(this.Lbl);
            this.pnlCustomer.Controls.Add(this.txtCustomer);
            this.pnlCustomer.Dock = DockStyle.Top;
            this.pnlCustomer.Location = new Point(0, 0);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Padding = new Padding(2);
            this.pnlCustomer.Size = new Size(0x270, 0x20);
            this.pnlCustomer.TabIndex = 0;
            this.Lbl.BackColor = SystemColors.GradientActiveCaption;
            this.Lbl.Location = new Point(8, 8);
            this.Lbl.Name = "Lbl";
            this.Lbl.Size = new Size(80, 0x15);
            this.Lbl.TabIndex = 0;
            this.Lbl.Text = "Customer";
            this.Lbl.TextAlign = ContentAlignment.MiddleRight;
            this.txtCustomer.BorderStyle = BorderStyle.Fixed3D;
            this.txtCustomer.Location = new Point(0x60, 8);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new Size(280, 0x15);
            this.txtCustomer.TabIndex = 1;
            this.txtCustomer.Text = "TxtCustomer";
            this.txtCustomer.TextAlign = ContentAlignment.MiddleLeft;
            this.tpHistory.Controls.Add(this.sgPayments);
            this.tpHistory.Location = new Point(4, 0x19);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Size = new Size(0x270, 0x18f);
            this.tpHistory.TabIndex = 1;
            this.tpHistory.Text = "Payments";
            this.tpHistory.UseVisualStyleBackColor = true;
            this.sgPayments.Dock = DockStyle.Fill;
            this.sgPayments.Location = new Point(0, 0);
            this.sgPayments.Name = "sgPayments";
            this.sgPayments.Size = new Size(0x270, 0x18f);
            this.sgPayments.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.tpGeneral);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormPaymentPlan";
            this.Text = "Payment Plan";
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.tpGeneral.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.PnlPlan.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlCustomer.ResumeLayout(false);
            this.tpHistory.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid_Invoice(FilteredGridAppearance Appearance)
        {
            Appearance.AllowEdit = false;
            Appearance.AllowNew = false;
            Appearance.AllowDelete = false;
            Appearance.AutoGenerateColumns = false;
            Appearance.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "#", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("PlanAmount", "Plan Amt", 60, Appearance.PriceStyle());
            Appearance.AddTextColumn("DOSFrom", "DOS", 70, Appearance.DateStyle());
            Appearance.AddTextColumn("BillableAmount", "Billable", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("BillingCode", "B.Code", 0x37);
            Appearance.AddTextColumn("Balance", "Balance", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("CurrentPayer", "Payer", 0x37);
        }

        private void InitializeGrid_Payments(FilteredGridAppearance Appearance)
        {
            Appearance.AllowEdit = false;
            Appearance.AllowNew = false;
            Appearance.AllowDelete = false;
            Appearance.AutoGenerateColumns = false;
            Appearance.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("DueDate", "Due Date", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("DueAmount", "Due Amount", 80, Appearance.PriceStyle());
            Appearance.AddTextColumn("PaymentDate", "Paid Date", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("PaymentAmount", "Paid Amount", 80, Appearance.PriceStyle());
        }

        private void LoadPayments()
        {
            DataTable dataTable = new DataTable("payments");
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandText = "SELECT * FROM tbl_paymentplan_payments WHERE CustomerID = :CustomerID";
                adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.sgPayments.GridSource = dataTable.ToGridSource();
        }

        private void LoadPlan()
        {
            this.sgInvoices.GridSource = null;
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
                            Functions.SetLabelText(this.TxtPeriod, DBNull.Value);
                            Functions.SetLabelText(this.TxtFirstPayment, DBNull.Value);
                            Functions.SetLabelText(this.TxtPaymentCount, DBNull.Value);
                            Functions.SetLabelText(this.TxtPaymentAmount, DBNull.Value);
                            this.PnlPlan.Visible = false;
                            this.LblNoPlan.Visible = true;
                            this.tsbModify.Text = "Create";
                            this.tsbPayment.Visible = false;
                        }
                        else
                        {
                            Functions.SetLabelText(this.TxtPeriod, reader2["Period"]);
                            Functions.SetLabelText(this.TxtFirstPayment, reader2["FirstPayment"]);
                            Functions.SetLabelText(this.TxtPaymentCount, reader2["PaymentCount"]);
                            Functions.SetLabelText(this.TxtPaymentAmount, $"{reader2["PaymentAmount"]:0.00}");
                            dataTable.ApplyDataset(DatasetPaymentPlan.FromXml(NullableConvert.ToString(reader2["Details"])));
                            this.PnlPlan.Visible = true;
                            this.LblNoPlan.Visible = false;
                            this.tsbModify.Text = "Modify";
                            this.tsbPayment.Visible = true;
                        }
                    }
                }
            }
            this.sgInvoices.GridSource = dataTable.ToGridSource();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tsbModify_Click(object sender, EventArgs e)
        {
            using (WizardPaymentPlan plan = new WizardPaymentPlan(this.CustomerID))
            {
                Form mdiParent = base.MdiParent;
                Form form2 = mdiParent;
                if (mdiParent == null)
                {
                    Form local1 = mdiParent;
                    form2 = this;
                }
                plan.Owner = form2;
                if (plan.ShowDialog() == DialogResult.OK)
                {
                    this.LoadPlan();
                }
            }
        }

        private void tsbPayment_Click(object sender, EventArgs e)
        {
            using (FormPlanPayment payment = new FormPlanPayment(this.CustomerID))
            {
                Form mdiParent = base.MdiParent;
                Form form2 = mdiParent;
                if (mdiParent == null)
                {
                    Form local1 = mdiParent;
                    form2 = this;
                }
                payment.Owner = form2;
                if (payment.ShowDialog() == DialogResult.OK)
                {
                    this.LoadPlan();
                    this.LoadPayments();
                }
            }
        }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpGeneral")]
        private TabControl tpGeneral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabPage1")]
        private TabPage TabPage1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpHistory")]
        private TabPage tpHistory { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("sgPayments")]
        private FilteredGrid sgPayments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbModify")]
        private ToolStripButton tsbModify { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbClose")]
        private ToolStripButton tsbClose { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("sgInvoices")]
        private FilteredGrid sgInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TxtFirstPayment")]
        private Label TxtFirstPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TxtPeriod")]
        private Label TxtPeriod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblFirstPayment")]
        private Label LblFirstPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblPeriod")]
        private Label LblPeriod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblPaymentCount")]
        private Label LblPaymentCount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TxtPaymentCount")]
        private Label TxtPaymentCount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TxtPaymentAmount")]
        private Label TxtPaymentAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblPaymentAmount")]
        private Label LblPaymentAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblInvoices")]
        private Label LblInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlDetails")]
        private Panel pnlDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbPayment")]
        private ToolStripButton tsbPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Lbl")]
        private Label Lbl { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomer")]
        private Label txtCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PnlPlan")]
        private Panel PnlPlan { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblNoPlan")]
        private Label LblNoPlan { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlCustomer")]
        private Panel pnlCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private class TableInvoiceDetails : TableBase
        {
            private DataColumn _col_ID;
            private DataColumn _col_PlanAmount;

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
                                row[this.Col_PlanAmount] = row2.PlanAmount;
                            }
                            else
                            {
                                row.Delete();
                            }
                            row.AcceptChanges();
                        }
                    }
                }
            }

            protected override void Initialize()
            {
                this._col_ID = base.Columns["ID"];
                this._col_PlanAmount = base.Columns["PlanAmount"];
            }

            protected override void InitializeClass()
            {
                base.Columns.Add("ID", typeof(int));
                base.Columns.Add("PlanAmount", typeof(double));
            }

            public DataColumn Col_ID =>
                this._col_ID;

            public DataColumn Col_PlanAmount =>
                this._col_PlanAmount;
        }
    }
}

