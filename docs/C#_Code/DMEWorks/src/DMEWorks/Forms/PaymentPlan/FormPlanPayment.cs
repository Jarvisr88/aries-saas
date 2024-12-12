namespace DMEWorks.Forms.PaymentPlan
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Core.Extensions;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
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
    public class FormPlanPayment : DmeForm
    {
        private IContainer components;
        private readonly int CustomerID;
        private int? PaymentPlanID;
        private double? PaymentAmount;
        private const string CrLf = "\r\n";

        public FormPlanPayment(int CustomerID)
        {
            base.Load += new EventHandler(this.FormPlanPayment_Load);
            this.PaymentPlanID = null;
            this.PaymentAmount = null;
            this.CustomerID = CustomerID;
            this.InitializeComponent();
            this.InitializeGrid(this.sgInvoices.Appearance);
        }

        private void AttachTable(TableInvoiceDetails table)
        {
            TableInvoiceDetails tableSource = this.sgInvoices.GetTableSource<TableInvoiceDetails>();
            if (!ReferenceEquals(tableSource, table))
            {
                if (tableSource != null)
                {
                    tableSource.RowChanged -= new DataRowChangeEventHandler(this.Table_RowChanged);
                }
                this.sgInvoices.GridSource = table.ToGridSource();
                if (table != null)
                {
                    table.RowChanged += new DataRowChangeEventHandler(this.Table_RowChanged);
                }
            }
        }

        private void CalculateAmounts()
        {
            double valueOrDefault = 0.0;
            TableInvoiceDetails tableSource = this.sgInvoices.GetTableSource<TableInvoiceDetails>();
            if (tableSource != null)
            {
                valueOrDefault = NullableConvert.ToDouble(tableSource.Compute("Sum([PaidAmount])", "(0.01 <= ISNULL([PaidAmount], 0))")).GetValueOrDefault(0.0);
            }
            this.tsslTotalAmount.Text = $"Total : {valueOrDefault:0.00}";
            double? asDouble = this.nmbPaymentAmount.AsDouble;
            if (asDouble != null)
            {
                asDouble = new double?(asDouble.Value - valueOrDefault);
            }
            this.tsslRemainingAmount.Text = $"Remaining : {asDouble:0.00}";
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

        private void FormPlanPayment_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadPlan();
                this.CalculateAmounts();
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

        private DataRow GetDateRow()
        {
            DataRow row;
            object selectedItem = this.cmbDueDate.SelectedItem;
            if (selectedItem is DataRowView)
            {
                row = ((DataRowView) selectedItem).Row;
            }
            else
            {
                if (!(selectedItem is DataRow))
                {
                    throw new UserNotifyException("You should select scheduled date");
                }
                row = (DataRow) selectedItem;
            }
            if (!(row.Table is TableDates))
            {
                throw new UserNotifyException("You should select scheduled date");
            }
            return row;
        }

        private double GetDueAmount()
        {
            if (this.PaymentAmount == null)
            {
                throw new UserNotifyException("Something is terribly wrong");
            }
            return this.PaymentAmount.Value;
        }

        private DateTime GetDueDate()
        {
            DataRow dateRow = this.GetDateRow();
            DateTime? nullable = NullableConvert.ToDateTime(dateRow[((TableDates) dateRow.Table).Col_Date]);
            if (nullable == null)
            {
                throw new UserNotifyException("You should select scheduled date");
            }
            return nullable.Value;
        }

        private int GetIndex()
        {
            DataRow dateRow = this.GetDateRow();
            int? nullable = NullableConvert.ToInt32(dateRow[((TableDates) dateRow.Table).Col_Index]);
            if (nullable == null)
            {
                throw new UserNotifyException("You should select scheduled date");
            }
            return nullable.Value;
        }

        private DateTime GetPaymentDate()
        {
            DateTime? nullable = NullableConvert.ToDateTime(this.dtbPaymentDate.Value);
            if (nullable == null)
            {
                throw new UserNotifyException("You should enter Payment Date");
            }
            return nullable.Value;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.ToolStrip1 = new ToolStrip();
            this.tsbSave = new ToolStripButton();
            this.tsbClose = new ToolStripButton();
            this.sgInvoices = new FilteredGrid();
            this.LblInvoices = new Label();
            this.lblDueDate = new Label();
            this.lblPaymentAmount = new Label();
            this.LblPaymentDate = new Label();
            this.pnlCustomer = new Panel();
            this.txtDueAmount = new Label();
            this.lblDueAmount = new Label();
            this.nmbPaymentAmount = new NumericBox();
            this.dtbPaymentDate = new UltraDateTimeEditor();
            this.cmbDueDate = new ComboBox();
            this.LblCustomer = new Label();
            this.txtCustomer = new Label();
            this.StatusStrip1 = new StatusStrip();
            this.tsslTotalAmount = new ToolStripStatusLabel();
            this.tsslRemainingAmount = new ToolStripStatusLabel();
            this.ToolStrip1.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            base.SuspendLayout();
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbSave, this.tsbClose };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x278, 0x19);
            this.ToolStrip1.TabIndex = 0;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbSave.Image = My.Resources.Resources.ImageEdit;
            this.tsbSave.ImageTransparentColor = Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new Size(0x33, 0x16);
            this.tsbSave.Text = "Save";
            this.tsbClose.Image = My.Resources.Resources.ImageClose;
            this.tsbClose.ImageTransparentColor = Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new Size(0x35, 0x16);
            this.tsbClose.Text = "Close";
            this.sgInvoices.Dock = DockStyle.Fill;
            this.sgInvoices.Location = new Point(0, 0x7b);
            this.sgInvoices.Name = "sgInvoices";
            this.sgInvoices.Size = new Size(0x278, 0x133);
            this.sgInvoices.TabIndex = 3;
            this.LblInvoices.BackColor = SystemColors.ActiveCaption;
            this.LblInvoices.Dock = DockStyle.Top;
            this.LblInvoices.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.LblInvoices.ForeColor = SystemColors.ActiveCaptionText;
            this.LblInvoices.Location = new Point(0, 0x69);
            this.LblInvoices.Name = "LblInvoices";
            this.LblInvoices.Size = new Size(0x278, 0x12);
            this.LblInvoices.TabIndex = 2;
            this.LblInvoices.Text = "Invoices";
            this.lblDueDate.BackColor = SystemColors.GradientActiveCaption;
            this.lblDueDate.Location = new Point(8, 0x20);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new Size(0x60, 0x15);
            this.lblDueDate.TabIndex = 2;
            this.lblDueDate.Text = "Due Date";
            this.lblPaymentAmount.BackColor = SystemColors.GradientActiveCaption;
            this.lblPaymentAmount.Location = new Point(0xe8, 0x38);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new Size(0x68, 0x15);
            this.lblPaymentAmount.TabIndex = 8;
            this.lblPaymentAmount.Text = "Payment Amount";
            this.LblPaymentDate.BackColor = SystemColors.GradientActiveCaption;
            this.LblPaymentDate.Location = new Point(8, 0x38);
            this.LblPaymentDate.Name = "LblPaymentDate";
            this.LblPaymentDate.Size = new Size(0x60, 0x15);
            this.LblPaymentDate.TabIndex = 4;
            this.LblPaymentDate.Text = "Payment Date";
            this.pnlCustomer.Controls.Add(this.txtDueAmount);
            this.pnlCustomer.Controls.Add(this.lblDueAmount);
            this.pnlCustomer.Controls.Add(this.nmbPaymentAmount);
            this.pnlCustomer.Controls.Add(this.dtbPaymentDate);
            this.pnlCustomer.Controls.Add(this.cmbDueDate);
            this.pnlCustomer.Controls.Add(this.lblDueDate);
            this.pnlCustomer.Controls.Add(this.LblCustomer);
            this.pnlCustomer.Controls.Add(this.txtCustomer);
            this.pnlCustomer.Controls.Add(this.lblPaymentAmount);
            this.pnlCustomer.Controls.Add(this.LblPaymentDate);
            this.pnlCustomer.Dock = DockStyle.Top;
            this.pnlCustomer.Location = new Point(0, 0x19);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Padding = new Padding(2);
            this.pnlCustomer.Size = new Size(0x278, 80);
            this.pnlCustomer.TabIndex = 1;
            this.txtDueAmount.BorderStyle = BorderStyle.Fixed3D;
            this.txtDueAmount.Location = new Point(0x158, 0x20);
            this.txtDueAmount.Name = "txtDueAmount";
            this.txtDueAmount.Size = new Size(0x60, 20);
            this.txtDueAmount.TabIndex = 7;
            this.txtDueAmount.Text = "495.00";
            this.txtDueAmount.TextAlign = ContentAlignment.MiddleRight;
            this.lblDueAmount.BackColor = SystemColors.GradientActiveCaption;
            this.lblDueAmount.Location = new Point(0xe8, 0x20);
            this.lblDueAmount.Name = "lblDueAmount";
            this.lblDueAmount.Size = new Size(0x68, 0x15);
            this.lblDueAmount.TabIndex = 6;
            this.lblDueAmount.Text = "Scheduled Amount";
            this.nmbPaymentAmount.BorderStyle = BorderStyle.Fixed3D;
            this.nmbPaymentAmount.Location = new Point(0x158, 0x38);
            this.nmbPaymentAmount.Name = "nmbPaymentAmount";
            this.nmbPaymentAmount.Size = new Size(0x60, 20);
            this.nmbPaymentAmount.TabIndex = 9;
            this.nmbPaymentAmount.TextAlign = HorizontalAlignment.Left;
            this.dtbPaymentDate.DateTime = new DateTime(0x7da, 5, 0x15, 0, 0, 0, 0);
            this.dtbPaymentDate.Location = new Point(0x70, 0x38);
            this.dtbPaymentDate.Name = "dtbPaymentDate";
            this.dtbPaymentDate.Size = new Size(0x70, 0x15);
            this.dtbPaymentDate.TabIndex = 5;
            this.dtbPaymentDate.Value = new DateTime(0x7da, 5, 0x15, 0, 0, 0, 0);
            this.cmbDueDate.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDueDate.FormatString = "d";
            this.cmbDueDate.FormattingEnabled = true;
            this.cmbDueDate.Location = new Point(0x70, 0x20);
            this.cmbDueDate.Name = "cmbDueDate";
            this.cmbDueDate.Size = new Size(0x70, 0x15);
            this.cmbDueDate.TabIndex = 3;
            this.LblCustomer.BackColor = SystemColors.GradientActiveCaption;
            this.LblCustomer.Location = new Point(8, 8);
            this.LblCustomer.Name = "LblCustomer";
            this.LblCustomer.Size = new Size(0x60, 0x15);
            this.LblCustomer.TabIndex = 0;
            this.LblCustomer.Text = "Customer";
            this.txtCustomer.BorderStyle = BorderStyle.Fixed3D;
            this.txtCustomer.Location = new Point(0x70, 8);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new Size(0x180, 0x15);
            this.txtCustomer.TabIndex = 1;
            this.txtCustomer.Text = "TxtCustomer";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.tsslTotalAmount, this.tsslRemainingAmount };
            this.StatusStrip1.Items.AddRange(itemArray2);
            this.StatusStrip1.Location = new Point(0, 430);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new Size(0x278, 0x17);
            this.StatusStrip1.TabIndex = 4;
            this.StatusStrip1.Text = "StatusStrip1";
            this.tsslTotalAmount.AutoSize = false;
            this.tsslTotalAmount.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslTotalAmount.Margin = new Padding(1, 2, 1, 1);
            this.tsslTotalAmount.Name = "tsslTotalAmount";
            this.tsslTotalAmount.Size = new Size(100, 20);
            this.tsslTotalAmount.Text = "Total : 495.00";
            this.tsslRemainingAmount.AutoSize = false;
            this.tsslRemainingAmount.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslRemainingAmount.Margin = new Padding(1, 2, 1, 1);
            this.tsslRemainingAmount.Name = "tsslRemainingAmount";
            this.tsslRemainingAmount.Size = new Size(100, 20);
            this.tsslRemainingAmount.Text = "Remains : 495.00";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.sgInvoices);
            base.Controls.Add(this.StatusStrip1);
            base.Controls.Add(this.LblInvoices);
            base.Controls.Add(this.pnlCustomer);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormPlanPayment";
            this.Text = "Payment";
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.pnlCustomer.ResumeLayout(false);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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
            Appearance.AddTextColumn("ID", "#", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("PlanAmount", "Plan Amt", 60, Appearance.PriceStyle());
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle(Appearance.PriceStyle()) {
                BackColor = Color.LightSteelBlue
            };
            Appearance.AddTextColumn("PaidAmount", "Paid Amt", 60, cellStyle).ReadOnly = false;
            Appearance.AddTextColumn("DOSFrom", "DOS", 70, Appearance.DateStyle());
            Appearance.AddTextColumn("BillableAmount", "Billable", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("BillingCode", "B.Code", 0x37);
            Appearance.AddTextColumn("Balance", "Balance", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("CurrentPayer", "Payer", 0x37);
        }

        private void LoadPlan()
        {
            this.AttachTable(null);
            TableInvoiceDetails dataTable = new TableInvoiceDetails("tbl_details");
            TableDates dates = new TableDates("tbl_dates");
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
                    adapter.SelectCommand.CommandText = "SELECT\r\n  ID\r\n, InvoiceID\r\n, DOSFrom\r\n, BillableAmount\r\n, BillingCode\r\n, CurrentPayer\r\n, BillableAmount - PaymentAmount - WriteoffAmount as Balance\r\nFROM tbl_invoicedetails\r\nWHERE CustomerID = :CustomerID\r\nORDER BY ID";
                    adapter.SelectCommand.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                    adapter.AcceptChangesDuringFill = true;
                    adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
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
                            throw new UserNotifyException("Cannot find payment plan");
                        }
                        this.PaymentPlanID = NullableConvert.ToInt32(reader2["ID"]);
                        this.PaymentAmount = NullableConvert.ToDouble(reader2["PaymentAmount"]);
                        string period = NullableConvert.ToString(reader2["Period"]);
                        DateTime? nullable = NullableConvert.ToDateTime(reader2["FirstPayment"]);
                        int? nullable2 = NullableConvert.ToInt32(reader2["PaymentCount"]);
                        if (this.PaymentPlanID == null)
                        {
                            throw new UserNotifyException("Cannot find payment plan");
                        }
                        if (this.PaymentAmount == null)
                        {
                            throw new UserNotifyException("PaymentAmount is not assigned");
                        }
                        if (nullable == null)
                        {
                            throw new UserNotifyException("FirstPayment is not assigned");
                        }
                        if (nullable2 == null)
                        {
                            throw new UserNotifyException("PaymentCount is not assigned");
                        }
                        this.txtDueAmount.Text = $"{this.PaymentAmount:0.00}";
                        this.nmbPaymentAmount.AsDouble = this.PaymentAmount;
                        this.dtbPaymentDate.Value = DateTime.Today;
                        dates.Fill(nullable.Value, period, nullable2.Value);
                        dataTable.ApplyDataset(DatasetPaymentPlan.FromXml(NullableConvert.ToString(reader2["Details"])));
                    }
                }
            }
            this.AttachTable(dataTable);
            this.cmbDueDate.DataSource = dates;
            this.cmbDueDate.DisplayMember = dates.Col_Date.ColumnName;
        }

        private void nmbPaidAmount_ValueChanged(object sender, EventArgs e)
        {
            this.CalculateAmounts();
        }

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            this.CalculateAmounts();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.sgInvoices.EndEdit();
                int index = this.GetIndex();
                DateTime dueDate = this.GetDueDate();
                double dueAmount = this.GetDueAmount();
                DateTime paymentDate = this.GetPaymentDate();
                if (this.PaymentPlanID == null)
                {
                    throw new UserNotifyException("Something terribly wrong");
                }
                TableInvoiceDetails tableSource = this.sgInvoices.GetTableSource<TableInvoiceDetails>();
                if (tableSource == null)
                {
                    throw new UserNotifyException("Something terribly wrong");
                }
                DataRow[] rowArray = tableSource.Select("(0.01 <= ISNULL([PaidAmount], 0))", "ID", DataViewRowState.CurrentRows);
                if (rowArray.Length == 0)
                {
                    throw new UserNotifyException("There is nothing to post");
                }
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        double num3 = 0.0;
                        DataRow[] rowArray2 = rowArray;
                        int num4 = 0;
                        while (true)
                        {
                            if (num4 >= rowArray2.Length)
                            {
                                using (MySqlCommand command3 = new MySqlCommand("", connection, transaction))
                                {
                                    command3.Parameters.Add("PaymentPlanID", MySqlType.Int).Value = this.PaymentPlanID.Value;
                                    command3.Parameters.Add("CustomerID", MySqlType.Int).Value = this.CustomerID;
                                    command3.Parameters.Add("Index", MySqlType.Int).Value = index;
                                    command3.Parameters.Add("DueDate", MySqlType.Date).Value = dueDate;
                                    command3.Parameters.Add("DueAmount", MySqlType.Double).Value = dueAmount;
                                    command3.Parameters.Add("PaymentDate", MySqlType.Date).Value = paymentDate;
                                    command3.Parameters.Add("PaymentAmount", MySqlType.Double).Value = num3;
                                    command3.Parameters.Add("Details", MySqlType.Text, 0xffff).Value = DatasetPlanPayment.ToXml(tableSource.ToDataset());
                                    command3.Parameters.Add("LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                                    if (command3.ExecuteInsert("tbl_paymentplan_payments") == 0)
                                    {
                                        throw new UserNotifyException("Cannot save payment");
                                    }
                                }
                                transaction.Commit();
                                break;
                            }
                            DataRow row = rowArray2[num4];
                            int? nullable = NullableConvert.ToInt32(row[tableSource.Col_ID]);
                            if (nullable != null)
                            {
                                int? nullable2 = NullableConvert.ToInt32(row[tableSource.Col_InvoiceID]);
                                if (nullable2 != null)
                                {
                                    decimal? @decimal = row.GetDecimal(tableSource.Col_PaidAmount);
                                    if ((@decimal != null) && (decimal.Compare(@decimal.Value, 0M) > 0))
                                    {
                                        PaymentExtraData data = new PaymentExtraData {
                                            Paid = @decimal,
                                            Billable = @decimal,
                                            PaymentMethod = 0
                                        };
                                        using (MySqlCommand command = new MySqlCommand("", connection, transaction))
                                        {
                                            command.Parameters.Add("P_InvoiceDetailsID", MySqlType.Int).Value = nullable.Value;
                                            command.Parameters.Add("P_InsuranceCompanyID", MySqlType.Int).Value = DBNull.Value;
                                            command.Parameters.Add("P_TransactionDate", MySqlType.Date).Value = paymentDate;
                                            command.Parameters.Add("P_Extra", MySqlType.Text).Value = data.ToString();
                                            command.Parameters.Add("P_Comments", MySqlType.Text).Value = "Payment plan payment";
                                            command.Parameters.Add("P_Options", MySqlType.Text).Value = "";
                                            command.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                            command.Parameters.Add("P_Result", MySqlType.VarChar, 0xff).Direction = ParameterDirection.Output;
                                            command.ExecuteProcedure("InvoiceDetails_AddPayment");
                                        }
                                        num3 += Convert.ToDouble(data.Paid.Value);
                                        using (MySqlCommand command2 = new MySqlCommand("", connection, transaction))
                                        {
                                            command2.Parameters.Add("P_InvoiceID", MySqlType.Int).Value = nullable2.Value;
                                            command2.Parameters.Add("P_Recursive", MySqlType.Int).Value = 1;
                                            command2.ExecuteProcedure("Invoice_UpdateBalance");
                                        }
                                    }
                                }
                            }
                            num4++;
                        }
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception exception2)
            {
                Exception ex = exception2;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Add payment");
                ProjectData.ClearProjectError();
                return;
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbSave")]
        private ToolStripButton tsbSave { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbClose")]
        private ToolStripButton tsbClose { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("sgInvoices")]
        private FilteredGrid sgInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDueDate")]
        private Label lblDueDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblPaymentDate")]
        private Label LblPaymentDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPaymentAmount")]
        private Label lblPaymentAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblInvoices")]
        private Label LblInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("LblCustomer")]
        private Label LblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCustomer")]
        private Label txtCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlCustomer")]
        private Panel pnlCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbPaymentDate")]
        private UltraDateTimeEditor dtbPaymentDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDueAmount")]
        private Label lblDueAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPaymentAmount")]
        private NumericBox nmbPaymentAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDueAmount")]
        private Label txtDueAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("StatusStrip1")]
        private StatusStrip StatusStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslTotalAmount")]
        private ToolStripStatusLabel tsslTotalAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslRemainingAmount")]
        private ToolStripStatusLabel tsslRemainingAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDueDate")]
        private ComboBox cmbDueDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private class TableDates : TableBase
        {
            private DataColumn _col_Index;
            private DataColumn _col_Date;

            public TableDates()
            {
            }

            public TableDates(string TableName) : base(TableName)
            {
            }

            public void Fill(DateTime FirstDate, string Period, int Count)
            {
                int num = Count;
                for (int i = 1; i <= num; i++)
                {
                    DataRow row = base.NewRow();
                    row[this.Col_Index] = i;
                    row[this.Col_Date] = GetDate(FirstDate, Period, i - 1);
                    base.Rows.Add(row);
                    row.AcceptChanges();
                }
            }

            private static DateTime GetDate(DateTime FirstDate, string Period, int Index)
            {
                DateTime time;
                if (string.Compare(Period, "Weekly", true) == 0)
                {
                    time = FirstDate.AddDays((double) (7 * Index));
                }
                else if (string.Compare(Period, "Bi-weekly", true) == 0)
                {
                    time = FirstDate.AddDays((double) (14 * Index));
                }
                else
                {
                    if (string.Compare(Period, "Monthly", true) != 0)
                    {
                        throw new ArgumentOutOfRangeException("period", "Period '" + Period + "' is not supported");
                    }
                    time = FirstDate.AddMonths(Index);
                }
                return time;
            }

            protected override void Initialize()
            {
                this._col_Index = base.Columns["Index"];
                this._col_Date = base.Columns["Date"];
            }

            protected override void InitializeClass()
            {
                base.Columns.Add("Index", typeof(int));
                base.Columns.Add("Date", typeof(DateTime));
            }

            public DataColumn Col_Index =>
                this._col_Index;

            public DataColumn Col_Date =>
                this._col_Date;
        }

        private class TableInvoiceDetails : TableBase
        {
            private DataColumn _col_ID;
            private DataColumn _col_InvoiceID;
            private DataColumn _col_PaidAmount;
            private DataColumn _col_PlanAmount;
            private DataColumn _col_BillableAmount;

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
                this._col_InvoiceID = base.Columns["InvoiceID"];
                this._col_PaidAmount = base.Columns["PaidAmount"];
                this._col_PlanAmount = base.Columns["PlanAmount"];
                this._col_BillableAmount = base.Columns["BillableAmount"];
            }

            protected override void InitializeClass()
            {
                base.Columns.Add("ID", typeof(int));
                base.Columns.Add("InvoiceID", typeof(int));
                base.Columns.Add("PaidAmount", typeof(double));
                base.Columns.Add("PlanAmount", typeof(double));
                base.Columns.Add("BillableAmount", typeof(double));
            }

            protected override void OnColumnChanged(DataColumnChangeEventArgs e)
            {
                if (ReferenceEquals(e.Column, this.Col_PaidAmount))
                {
                    e.Row.BeginEdit();
                    e.Row.EndEdit();
                }
                base.OnColumnChanged(e);
            }

            public DatasetPlanPayment ToDataset()
            {
                DatasetPlanPayment payment = new DatasetPlanPayment();
                DataRow[] rowArray = base.Select("(0.01 <= ISNULL([PaidAmount], 0))", "ID", DataViewRowState.CurrentRows);
                for (int i = 0; i < rowArray.Length; i++)
                {
                    DataRow row1 = rowArray[i];
                    int? nullable = NullableConvert.ToInt32(row1[this.Col_ID]);
                    double? nullable2 = NullableConvert.ToDouble(row1[this.Col_PaidAmount]);
                    if ((nullable != null) && ((nullable2 != null) && (nullable2.Value > 0.0)))
                    {
                        payment.TableInvoiceDetails.AddTableInvoiceDetailsRow(nullable.Value, nullable2.Value, "");
                    }
                }
                payment.AcceptChanges();
                return payment;
            }

            public DataColumn Col_ID =>
                this._col_ID;

            public DataColumn Col_InvoiceID =>
                this._col_InvoiceID;

            public DataColumn Col_PaidAmount =>
                this._col_PaidAmount;

            public DataColumn Col_PlanAmount =>
                this._col_PlanAmount;

            public DataColumn Col_BillableAmount =>
                this._col_BillableAmount;
        }
    }
}

