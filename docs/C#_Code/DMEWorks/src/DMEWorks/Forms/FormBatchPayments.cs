namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormBatchPayments : DmeForm
    {
        private IContainer components;
        private readonly TablePayments FTable_Payments = new TablePayments("tbl_payments");
        private readonly TableLineItems FTable_LineItems = new TableLineItems("tbl_lineitems");
        private const string CrLf = "\r\n";
        private decimal FAmountUsed = 0M;
        private decimal FAmountLeft = 0M;
        private int? FBatchPaymentID = null;

        public FormBatchPayments()
        {
            this.InitializeComponent();
            InitializeGridStyle_LineItems(this.sgLineItems.Appearance);
            InitializeGridStyle_Payments(this.sgPayments.Appearance);
            this.dtbCheckDate.Value = DateTime.Today;
            this.dtbPostingDate.Value = DateTime.Today;
            this.FTable_Payments.RowChanged += new DataRowChangeEventHandler(this.FTable_Payments_RowChanged);
        }

        private void btnReminder_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadReminder();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.sgPayments.EndEdit();
                this.ValidatePayments();
                if ((NullableConvert.ToInt32(this.cmbInsuranceCompany.SelectedValue) == null) || ((decimal.Compare(0M, Math.Round(this.AmountLeft, 2)) >= 0) || (MessageBox.Show("The amount left is greater than zero. Would you like to proceed and receive notification next time you use this module?", "Batch payments", MessageBoxButtons.YesNo) != DialogResult.No)))
                {
                    this.SavePayments();
                    this.UpdateAmountLeft();
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

        private void ClearLineItems()
        {
            this.sgLineItems.GridSource = null;
            try
            {
                this.FTable_LineItems.Clear();
                this.FTable_LineItems.AcceptChanges();
            }
            finally
            {
                this.sgLineItems.GridSource = this.FTable_LineItems.ToGridSource();
            }
        }

        private void ClearPaymentTable()
        {
            this.sgPayments.GridSource = null;
            try
            {
                this.FTable_Payments.Clear();
                this.FTable_Payments.AcceptChanges();
            }
            finally
            {
                this.sgPayments.GridSource = this.FTable_Payments.ToGridSource();
            }
        }

        private void cmbInsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.FBatchPaymentID != null)
                {
                    this.FBatchPaymentID = null;
                    Functions.SetUpDownValue(this.nudCheckAmount, 0.0);
                    Functions.SetDateBoxValue(this.dtbCheckDate, DateTime.Today);
                    Functions.SetTextBoxText(this.txtCheckNumber, DBNull.Value);
                    this.AmountUsed = 0M;
                }
                this.ClearPaymentTable();
                this.LoadLineItems();
                this.UpdateAmountLeft();
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

        private bool DeleteBatchPayment()
        {
            bool flag;
            if (this.FBatchPaymentID == null)
            {
                flag = false;
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = this.FBatchPaymentID.Value;
                        flag = 0 < command.ExecuteDelete("tbl_batchpayment");
                    }
                }
            }
            return flag;
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

        private void dtbCheckDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.FBatchPaymentID != null)
                {
                    this.FBatchPaymentID = null;
                    Functions.SetUpDownValue(this.nudCheckAmount, 0.0);
                    Functions.SetTextBoxText(this.txtCheckNumber, DBNull.Value);
                    this.AmountUsed = 0M;
                    this.UpdateAmountLeft();
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

        private void FTable_Payments_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            this.UpdateAmountLeft();
        }

        private int GetReminderCount()
        {
            int num;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "DELETE FROM tbl_batchpayment WHERE CheckAmount <= AmountUsed";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT Count(*) FROM tbl_batchpayment";
                    num = Conversions.ToInteger(command.ExecuteScalar());
                }
            }
            return num;
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbInsuranceCompany, "tbl_insurancecompany", Filter.Instance);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormBatchPayments));
            this.TableLayoutPanel1 = new TableLayoutPanel();
            this.lblCheckDate = new Label();
            this.dtbCheckDate = new UltraDateTimeEditor();
            this.cmbInsuranceCompany = new Combobox();
            this.lblInsuranceCompany = new Label();
            this.lblPostingDate = new Label();
            this.dtbPostingDate = new UltraDateTimeEditor();
            this.lblCheckNumber = new Label();
            this.txtCheckNumber = new TextBox();
            this.btnReminder = new Button();
            this.lblAmount = new Label();
            this.nudCheckAmount = new NumericUpDown();
            this.btnSave = new Button();
            this.lblAmountLeft = new Label();
            this.lblAmountLeftValue = new Label();
            this.lblAmountUsed = new Label();
            this.lblAmountUsedValue = new Label();
            this.ToolStrip1 = new ToolStrip();
            this.tslblPayments = new ToolStripLabel();
            this.ToolStripSeparator1 = new ToolStripSeparator();
            this.tsbAdd = new ToolStripButton();
            this.tsbRemove = new ToolStripButton();
            this.tsbRemoveAll = new ToolStripButton();
            this.Splitter1 = new Splitter();
            this.sgPayments = new SearchableGrid();
            this.sgLineItems = new SearchableGrid();
            this.ErrorProvider1 = new ErrorProvider(this.components);
            this.ToolTip1 = new ToolTip(this.components);
            this.tableZeroPayment = new TableLayoutPanel();
            this.rbZeroPayment_Payment = new RadioButton();
            this.lblZeroPayment = new Label();
            this.rbZeroPayment_Denied = new RadioButton();
            this.TableLayoutPanel1.SuspendLayout();
            this.nudCheckAmount.BeginInit();
            this.ToolStrip1.SuspendLayout();
            ((ISupportInitialize) this.ErrorProvider1).BeginInit();
            this.tableZeroPayment.SuspendLayout();
            base.SuspendLayout();
            this.TableLayoutPanel1.ColumnCount = 8;
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320f));
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88f));
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108f));
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80f));
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108f));
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80f));
            this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 295f));
            this.TableLayoutPanel1.Controls.Add(this.lblCheckDate, 2, 0);
            this.TableLayoutPanel1.Controls.Add(this.dtbCheckDate, 3, 0);
            this.TableLayoutPanel1.Controls.Add(this.cmbInsuranceCompany, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.lblInsuranceCompany, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.lblPostingDate, 4, 0);
            this.TableLayoutPanel1.Controls.Add(this.dtbPostingDate, 5, 0);
            this.TableLayoutPanel1.Controls.Add(this.lblCheckNumber, 0, 1);
            this.TableLayoutPanel1.Controls.Add(this.txtCheckNumber, 1, 1);
            this.TableLayoutPanel1.Controls.Add(this.btnReminder, 6, 0);
            this.TableLayoutPanel1.Controls.Add(this.lblAmount, 2, 1);
            this.TableLayoutPanel1.Controls.Add(this.nudCheckAmount, 3, 1);
            this.TableLayoutPanel1.Controls.Add(this.btnSave, 6, 1);
            this.TableLayoutPanel1.Controls.Add(this.lblAmountLeft, 4, 2);
            this.TableLayoutPanel1.Controls.Add(this.lblAmountLeftValue, 5, 2);
            this.TableLayoutPanel1.Controls.Add(this.lblAmountUsed, 2, 2);
            this.TableLayoutPanel1.Controls.Add(this.lblAmountUsedValue, 3, 2);
            this.TableLayoutPanel1.Controls.Add(this.tableZeroPayment, 1, 2);
            this.TableLayoutPanel1.Dock = DockStyle.Top;
            this.TableLayoutPanel1.Location = new Point(0, 0);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 4;
            this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 8f));
            this.TableLayoutPanel1.Size = new Size(0x400, 80);
            this.TableLayoutPanel1.TabIndex = 0;
            this.lblCheckDate.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblCheckDate.BorderStyle = BorderStyle.FixedSingle;
            this.lblCheckDate.Location = new Point(0x1a8, 1);
            this.lblCheckDate.Margin = new Padding(4, 1, 4, 1);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new Size(80, 0x15);
            this.lblCheckDate.TabIndex = 2;
            this.lblCheckDate.Text = "Check Date";
            this.lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
            this.ToolTip1.SetToolTip(this.lblCheckDate, "Check Date");
            this.dtbCheckDate.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.dtbCheckDate.DateTime = new DateTime(0x7d6, 11, 13, 0, 0, 0, 0);
            this.dtbCheckDate.Location = new Point(0x200, 1);
            this.dtbCheckDate.Margin = new Padding(4, 1, 4, 1);
            this.dtbCheckDate.Name = "dtbCheckDate";
            this.dtbCheckDate.Size = new Size(100, 0x15);
            this.dtbCheckDate.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.dtbCheckDate, "Check Date");
            this.dtbCheckDate.Value = new DateTime(0x7d6, 11, 13, 0, 0, 0, 0);
            this.cmbInsuranceCompany.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbInsuranceCompany.Location = new Point(0x68, 1);
            this.cmbInsuranceCompany.Margin = new Padding(4, 1, 4, 1);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new Size(0x138, 0x15);
            this.cmbInsuranceCompany.TabIndex = 1;
            this.lblInsuranceCompany.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblInsuranceCompany.BorderStyle = BorderStyle.FixedSingle;
            this.lblInsuranceCompany.Location = new Point(4, 1);
            this.lblInsuranceCompany.Margin = new Padding(4, 1, 4, 1);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Size = new Size(0x5c, 0x15);
            this.lblInsuranceCompany.TabIndex = 0;
            this.lblInsuranceCompany.Text = "Payer";
            this.lblInsuranceCompany.TextAlign = ContentAlignment.MiddleRight;
            this.lblPostingDate.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblPostingDate.BorderStyle = BorderStyle.FixedSingle;
            this.lblPostingDate.Location = new Point(620, 1);
            this.lblPostingDate.Margin = new Padding(4, 1, 4, 1);
            this.lblPostingDate.Name = "lblPostingDate";
            this.lblPostingDate.Size = new Size(0x48, 0x15);
            this.lblPostingDate.TabIndex = 4;
            this.lblPostingDate.Text = "Posting Date";
            this.lblPostingDate.TextAlign = ContentAlignment.MiddleRight;
            this.ToolTip1.SetToolTip(this.lblPostingDate, "Posting Date");
            this.dtbPostingDate.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.dtbPostingDate.DateTime = new DateTime(0x7d6, 11, 13, 0, 0, 0, 0);
            this.dtbPostingDate.Location = new Point(700, 1);
            this.dtbPostingDate.Margin = new Padding(4, 1, 4, 1);
            this.dtbPostingDate.Name = "dtbPostingDate";
            this.dtbPostingDate.Size = new Size(100, 0x15);
            this.dtbPostingDate.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.dtbPostingDate, "Posting Date");
            this.dtbPostingDate.Value = new DateTime(0x7d6, 11, 13, 0, 0, 0, 0);
            this.lblCheckNumber.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblCheckNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblCheckNumber.Location = new Point(4, 0x19);
            this.lblCheckNumber.Margin = new Padding(4, 1, 4, 1);
            this.lblCheckNumber.Name = "lblCheckNumber";
            this.lblCheckNumber.Size = new Size(0x5c, 0x15);
            this.lblCheckNumber.TabIndex = 6;
            this.lblCheckNumber.Text = "Check #";
            this.lblCheckNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtCheckNumber.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtCheckNumber.BorderStyle = BorderStyle.FixedSingle;
            this.txtCheckNumber.Location = new Point(0x68, 0x19);
            this.txtCheckNumber.Margin = new Padding(4, 1, 4, 1);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new Size(0x138, 20);
            this.txtCheckNumber.TabIndex = 7;
            this.txtCheckNumber.WordWrap = false;
            this.btnReminder.Location = new Point(0x328, 0);
            this.btnReminder.Margin = new Padding(4, 0, 4, 2);
            this.btnReminder.Name = "btnReminder";
            this.btnReminder.Size = new Size(0x48, 0x16);
            this.btnReminder.TabIndex = 15;
            this.btnReminder.Text = "Reminder";
            this.lblAmount.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblAmount.BorderStyle = BorderStyle.FixedSingle;
            this.lblAmount.Location = new Point(0x1a8, 0x19);
            this.lblAmount.Margin = new Padding(4, 1, 4, 1);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new Size(80, 0x15);
            this.lblAmount.TabIndex = 8;
            this.lblAmount.Text = "Amount";
            this.lblAmount.TextAlign = ContentAlignment.MiddleRight;
            this.nudCheckAmount.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.nudCheckAmount.BorderStyle = BorderStyle.FixedSingle;
            this.nudCheckAmount.DecimalPlaces = 2;
            this.nudCheckAmount.Location = new Point(0x200, 0x19);
            this.nudCheckAmount.Margin = new Padding(4, 1, 4, 1);
            int[] bits = new int[4];
            bits[0] = 0x3b9aca00;
            this.nudCheckAmount.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 0x3b9aca00;
            numArray2[3] = -2147483648;
            this.nudCheckAmount.Minimum = new decimal(numArray2);
            this.nudCheckAmount.Name = "nudCheckAmount";
            this.nudCheckAmount.Size = new Size(100, 20);
            this.nudCheckAmount.TabIndex = 9;
            this.nudCheckAmount.TextAlign = HorizontalAlignment.Right;
            this.nudCheckAmount.UpDownAlign = LeftRightAlignment.Left;
            this.btnSave.Location = new Point(0x328, 0x18);
            this.btnSave.Margin = new Padding(4, 0, 4, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x48, 0x16);
            this.btnSave.TabIndex = 0x10;
            this.btnSave.Text = "Save";
            this.lblAmountLeft.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblAmountLeft.BorderStyle = BorderStyle.FixedSingle;
            this.lblAmountLeft.Location = new Point(620, 0x31);
            this.lblAmountLeft.Margin = new Padding(4, 1, 4, 1);
            this.lblAmountLeft.Name = "lblAmountLeft";
            this.lblAmountLeft.Size = new Size(0x48, 0x15);
            this.lblAmountLeft.TabIndex = 13;
            this.lblAmountLeft.Text = "Amount Left";
            this.lblAmountLeft.TextAlign = ContentAlignment.MiddleRight;
            this.ToolTip1.SetToolTip(this.lblAmountLeft, "Amount Left");
            this.lblAmountLeftValue.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblAmountLeftValue.BorderStyle = BorderStyle.Fixed3D;
            this.lblAmountLeftValue.Location = new Point(700, 0x31);
            this.lblAmountLeftValue.Margin = new Padding(4, 1, 4, 1);
            this.lblAmountLeftValue.Name = "lblAmountLeftValue";
            this.lblAmountLeftValue.Size = new Size(100, 20);
            this.lblAmountLeftValue.TabIndex = 14;
            this.lblAmountLeftValue.Text = "0.00";
            this.lblAmountLeftValue.TextAlign = ContentAlignment.MiddleRight;
            this.ToolTip1.SetToolTip(this.lblAmountLeftValue, "Amount Left");
            this.lblAmountUsed.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblAmountUsed.BorderStyle = BorderStyle.FixedSingle;
            this.lblAmountUsed.Location = new Point(0x1a8, 0x31);
            this.lblAmountUsed.Margin = new Padding(4, 1, 4, 1);
            this.lblAmountUsed.Name = "lblAmountUsed";
            this.lblAmountUsed.Size = new Size(80, 0x15);
            this.lblAmountUsed.TabIndex = 11;
            this.lblAmountUsed.Text = "Amount Used";
            this.lblAmountUsed.TextAlign = ContentAlignment.MiddleRight;
            this.ToolTip1.SetToolTip(this.lblAmountUsed, "Amount Used");
            this.lblAmountUsedValue.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblAmountUsedValue.BorderStyle = BorderStyle.Fixed3D;
            this.lblAmountUsedValue.Location = new Point(0x200, 0x31);
            this.lblAmountUsedValue.Margin = new Padding(4, 1, 4, 1);
            this.lblAmountUsedValue.Name = "lblAmountUsedValue";
            this.lblAmountUsedValue.Size = new Size(100, 20);
            this.lblAmountUsedValue.TabIndex = 12;
            this.lblAmountUsedValue.Text = "0.00";
            this.lblAmountUsedValue.TextAlign = ContentAlignment.MiddleRight;
            this.ToolTip1.SetToolTip(this.lblAmountUsedValue, "Amount Left");
            this.ToolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tslblPayments, this.ToolStripSeparator1, this.tsbAdd, this.tsbRemove, this.tsbRemoveAll };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0x146);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x400, 0x19);
            this.ToolStrip1.TabIndex = 3;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tslblPayments.Name = "tslblPayments";
            this.tslblPayments.Size = new Size(0x3b, 0x16);
            this.tslblPayments.Text = "Payments";
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new Size(6, 0x19);
            this.tsbAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = (Image) manager.GetObject("tsbAdd.Image");
            this.tsbAdd.ImageTransparentColor = Color.White;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new Size(0x17, 0x16);
            this.tsbAdd.Text = "Add";
            this.tsbAdd.TextImageRelation = TextImageRelation.TextBeforeImage;
            this.tsbRemove.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbRemove.Image = (Image) manager.GetObject("tsbRemove.Image");
            this.tsbRemove.ImageTransparentColor = Color.White;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new Size(0x17, 0x16);
            this.tsbRemove.Text = "Remove";
            this.tsbRemoveAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbRemoveAll.Image = (Image) manager.GetObject("tsbRemoveAll.Image");
            this.tsbRemoveAll.ImageTransparentColor = Color.White;
            this.tsbRemoveAll.Name = "tsbRemoveAll";
            this.tsbRemoveAll.Size = new Size(0x17, 0x16);
            this.tsbRemoveAll.Text = "Remove All";
            this.Splitter1.Dock = DockStyle.Top;
            this.Splitter1.Location = new Point(0, 320);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new Size(0x400, 6);
            this.Splitter1.TabIndex = 2;
            this.Splitter1.TabStop = false;
            this.sgPayments.Dock = DockStyle.Fill;
            this.sgPayments.Location = new Point(0, 0x15f);
            this.sgPayments.Name = "sgPayments";
            this.sgPayments.Size = new Size(0x400, 0xf9);
            this.sgPayments.TabIndex = 4;
            this.sgLineItems.Dock = DockStyle.Top;
            this.sgLineItems.Location = new Point(0, 80);
            this.sgLineItems.Name = "sgLineItems";
            this.sgLineItems.Size = new Size(0x400, 240);
            this.sgLineItems.TabIndex = 1;
            this.ErrorProvider1.ContainerControl = this;
            this.tableZeroPayment.ColumnCount = 3;
            this.tableZeroPayment.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34f));
            this.tableZeroPayment.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
            this.tableZeroPayment.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
            this.tableZeroPayment.Controls.Add(this.rbZeroPayment_Payment, 2, 0);
            this.tableZeroPayment.Controls.Add(this.lblZeroPayment, 0, 0);
            this.tableZeroPayment.Controls.Add(this.rbZeroPayment_Denied, 1, 0);
            this.tableZeroPayment.Location = new Point(0x68, 0x31);
            this.tableZeroPayment.Margin = new Padding(4, 1, 4, 1);
            this.tableZeroPayment.Name = "tableZeroPayment";
            this.tableZeroPayment.RowCount = 1;
            this.tableZeroPayment.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableZeroPayment.Size = new Size(0x138, 0x16);
            this.tableZeroPayment.TabIndex = 10;
            this.rbZeroPayment_Payment.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.rbZeroPayment_Payment.Checked = true;
            this.rbZeroPayment_Payment.Location = new Point(0xd3, 0);
            this.rbZeroPayment_Payment.Margin = new Padding(3, 0, 3, 0);
            this.rbZeroPayment_Payment.Name = "rbZeroPayment_Payment";
            this.rbZeroPayment_Payment.Size = new Size(0x62, 0x16);
            this.rbZeroPayment_Payment.TabIndex = 2;
            this.rbZeroPayment_Payment.TabStop = true;
            this.rbZeroPayment_Payment.Text = "Post Payment";
            this.rbZeroPayment_Payment.UseVisualStyleBackColor = true;
            this.lblZeroPayment.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblZeroPayment.Location = new Point(3, 0);
            this.lblZeroPayment.Name = "lblZeroPayment";
            this.lblZeroPayment.Size = new Size(100, 0x16);
            this.lblZeroPayment.TabIndex = 0;
            this.lblZeroPayment.Text = "Zero Payment?";
            this.lblZeroPayment.TextAlign = ContentAlignment.MiddleLeft;
            this.rbZeroPayment_Denied.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.rbZeroPayment_Denied.Location = new Point(0x6d, 0);
            this.rbZeroPayment_Denied.Margin = new Padding(3, 0, 3, 0);
            this.rbZeroPayment_Denied.Name = "rbZeroPayment_Denied";
            this.rbZeroPayment_Denied.Size = new Size(0x60, 0x16);
            this.rbZeroPayment_Denied.TabIndex = 1;
            this.rbZeroPayment_Denied.Text = "Post Denied";
            this.rbZeroPayment_Denied.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x400, 600);
            base.Controls.Add(this.sgPayments);
            base.Controls.Add(this.ToolStrip1);
            base.Controls.Add(this.Splitter1);
            base.Controls.Add(this.sgLineItems);
            base.Controls.Add(this.TableLayoutPanel1);
            base.Name = "FormBatchPayments";
            this.Text = "Batch Posting";
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel1.PerformLayout();
            this.nudCheckAmount.EndInit();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            ((ISupportInitialize) this.ErrorProvider1).EndInit();
            this.tableZeroPayment.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private static void InitializeGridStyle_LineItems(SearchableGridAppearance Appearance)
        {
            Appearance.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Appearance.RowTemplate.Height = 0x13;
            Appearance.AutoGenerateColumns = false;
            Appearance.MultiSelect = true;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("BillingCode", "B. Code", 60);
            Appearance.AddTextColumn("InvoiceID", "Invoice #", 60);
            Appearance.AddTextColumn("FirstName", "First Name", 0x55);
            Appearance.AddTextColumn("LastName", "Last Name", 0x55);
            Appearance.AddTextColumn("DOSFrom", "DOSFrom", 80);
            Appearance.AddTextColumn("BillableAmount", "Billable", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("AllowableAmount", "Allowable", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("Balance", "Balance", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("Responsibility", "Responsib.", 60);
            Appearance.AddTextColumn("CurrentPayer", "Curr payer", 60);
        }

        private static void InitializeGridStyle_Payments(SearchableGridAppearance Appearance)
        {
            Appearance.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Appearance.RowTemplate.Height = 20;
            Appearance.AutoGenerateColumns = false;
            Appearance.AllowEdit = true;
            Appearance.AllowNew = false;
            Appearance.MultiSelect = true;
            Appearance.Columns.Clear();
            Appearance.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            Appearance.EditMode = DataGridViewEditMode.EditOnEnter;
            Appearance.AddTextColumn("BillingCode", "B. Code", 60);
            Appearance.AddTextColumn("InvoiceID", "Invoice #", 60);
            Appearance.AddTextColumn("FirstName", "First Name", 0x55);
            Appearance.AddTextColumn("LastName", "Last Name", 0x55);
            Appearance.AddTextColumn("DOSFrom", "DOSFrom", 80);
            Appearance.AddTextColumn("BillableAmount", "Billable", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("AllowableAmount", "Allowable", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("Balance", "Balance", 0x37, Appearance.PriceStyle());
            Appearance.AddTextColumn("ExpectedPaidAmount", "Expected", 0x37, Appearance.PriceStyle());
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle(Appearance.PriceStyle()) {
                BackColor = Color.LightSteelBlue
            };
            DataGridViewCellStyle style2 = new DataGridViewCellStyle(Appearance.BoolStyle()) {
                BackColor = Color.LightSteelBlue
            };
            Appearance.AddTextColumn("EnteredAllowableAmount", "Allowed", 60, cellStyle).ReadOnly = false;
            Appearance.AddTextColumn("EnteredDeductibleAmount", "Deduct", 60, cellStyle).ReadOnly = false;
            Appearance.AddTextColumn("EnteredCoinsAmount", "Co-ins", 60, cellStyle).ReadOnly = false;
            Appearance.AddTextColumn("EnteredPaidAmount", "Paid", 60, cellStyle).ReadOnly = false;
            Appearance.AddBoolColumn("WriteoffBalance", "Writeoff", 60, style2).ReadOnly = false;
            Appearance.AddTextColumn("PostingPaidAmount", "Actual", 60, Appearance.PriceStyle());
        }

        private void LoadBatchPayment(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_batchpayment WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        this.FBatchPaymentID = null;
                        if (!reader.Read())
                        {
                            Functions.SetUpDownValue(this.nudCheckAmount, 0.0);
                            Functions.SetDateBoxValue(this.dtbCheckDate, DateTime.Today);
                            Functions.SetTextBoxText(this.txtCheckNumber, DBNull.Value);
                            Functions.SetComboBoxValue(this.cmbInsuranceCompany, DBNull.Value);
                            this.AmountUsed = 0M;
                        }
                        else
                        {
                            Functions.SetUpDownValue(this.nudCheckAmount, reader["CheckAmount"]);
                            Functions.SetDateBoxValue(this.dtbCheckDate, reader["CheckDate"]);
                            Functions.SetTextBoxText(this.txtCheckNumber, reader["CheckNumber"]);
                            Functions.SetComboBoxValue(this.cmbInsuranceCompany, reader["InsuranceCompanyID"]);
                            this.AmountUsed = Convert.ToDecimal(reader["AmountUsed"]);
                            this.FBatchPaymentID = NullableConvert.ToInt32(reader["ID"]);
                        }
                    }
                }
            }
            this.UpdateAmountLeft();
        }

        [HandleDatabaseChanged(new string[] { "tbl_customer_insurance", "tbl_customer", "tbl_insurancecompany", "tbl_invoice", "tbl_invoicedetails", "tbl_invoice_transaction" })]
        private void LoadLineItems()
        {
            int? nullable = NullableConvert.ToInt32(this.cmbInsuranceCompany.SelectedValue);
            this.sgLineItems.GridSource = null;
            try
            {
                this.FTable_LineItems.Clear();
                this.FTable_LineItems.AcceptChanges();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.CommandText = (nullable == null) ? ("SELECT\r\n  stats.CustomerID,\r\n  stats.InvoiceID,\r\n  stats.InvoiceDetailsID,\r\n  NULL as CustomerInsuranceID,\r\n  tbl_customer.LastName,\r\n  tbl_customer.FirstName,\r\n  stats.BillingCode,\r\n  IFNULL(tbl_inventoryitem.Name, '') as InventoryItemName,\r\n  stats.BillableAmount,\r\n  stats.AllowableAmount,\r\n  stats.WriteoffAmount as TotalWriteoffAmount,\r\n  stats.PaymentAmount  as TotalPaymentAmount,\r\n  stats.Quantity,\r\n  stats.Percent,\r\n  stats.CurrentPayer,\r\n  'Patient' as Responsibility,\r\n  stats.Basis,\r\n  stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount as Balance,\r\n  stats.DOSFrom,\r\n  stats.DOSTo\r\nFROM view_invoicetransaction_statistics as stats\r\n     INNER JOIN tbl_customer ON stats.CustomerID = tbl_customer.ID\r\n     LEFT  JOIN tbl_inventoryitem ON stats.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT  JOIN tbl_company ON tbl_company.ID = 1\r\nWHERE (" + (this.IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1") + ")\r\n  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate Is NULL) OR (Now() < tbl_customer.InactiveDate))\r\n  AND (0.01 <= stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount)") : string.Format("SELECT\r\n  stats.CustomerID,\r\n  stats.InvoiceID,\r\n  stats.InvoiceDetailsID,\r\n  CASE WHEN (stats.InsuranceCompany1_ID = {0}) AND (stats.Payments & 01 = 0) THEN stats.Insurance1_ID\r\n       WHEN (stats.InsuranceCompany2_ID = {0}) AND (stats.Payments & 02 = 0) THEN stats.Insurance2_ID\r\n       WHEN (stats.InsuranceCompany3_ID = {0}) AND (stats.Payments & 04 = 0) THEN stats.Insurance3_ID\r\n       WHEN (stats.InsuranceCompany4_ID = {0}) AND (stats.Payments & 08 = 0) THEN stats.Insurance4_ID\r\n       ELSE NULL END as CustomerInsuranceID,\r\n  tbl_customer.LastName,\r\n  tbl_customer.FirstName,\r\n  stats.BillingCode,\r\n  IFNULL(tbl_inventoryitem.Name, '') as InventoryItemName,\r\n  stats.BillableAmount,\r\n  stats.AllowableAmount,\r\n  stats.WriteoffAmount as TotalWriteoffAmount,\r\n  stats.PaymentAmount  as TotalPaymentAmount,\r\n  stats.Quantity,\r\n  stats.Percent,\r\n  stats.CurrentPayer,\r\n  CASE WHEN (stats.InsuranceCompany1_ID = {0}) AND (stats.Payments & 01 = 0) THEN 'Ins1'\r\n       WHEN (stats.InsuranceCompany2_ID = {0}) AND (stats.Payments & 02 = 0) THEN 'Ins2'\r\n       WHEN (stats.InsuranceCompany3_ID = {0}) AND (stats.Payments & 04 = 0) THEN 'Ins3'\r\n       WHEN (stats.InsuranceCompany4_ID = {0}) AND (stats.Payments & 08 = 0) THEN 'Ins4'\r\n       ELSE 'Patient' END as Responsibility,\r\n  stats.Basis,\r\n  stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount as Balance,\r\n  stats.DOSFrom,\r\n  stats.DOSTo\r\nFROM view_invoicetransaction_statistics as stats\r\n     INNER JOIN tbl_customer ON stats.CustomerID = tbl_customer.ID\r\n     LEFT  JOIN tbl_inventoryitem ON stats.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT  JOIN tbl_company ON tbl_company.ID = 1\r\nWHERE (" + (this.IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1") + ")\r\n  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate Is NULL) OR (Now() < tbl_customer.InactiveDate))\r\n  AND (0.01 <= stats.BillableAmount - stats.PaymentAmount - stats.WriteoffAmount)\r\n  AND (((stats.InsuranceCompany1_ID = {0}) and (stats.Payments & 01 = 0))\r\n    OR ((stats.InsuranceCompany2_ID = {0}) and (stats.Payments & 02 = 0))\r\n    OR ((stats.InsuranceCompany3_ID = {0}) and (stats.Payments & 04 = 0))\r\n    OR ((stats.InsuranceCompany4_ID = {0}) and (stats.Payments & 08 = 0)))", nullable.Value);
                    adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(this.FTable_LineItems);
                }
            }
            finally
            {
                this.sgLineItems.GridSource = this.FTable_LineItems.ToGridSource();
            }
        }

        private bool LoadReminder()
        {
            bool flag;
            using (DialogBatchPaymentsReminder reminder = new DialogBatchPaymentsReminder())
            {
                if ((reminder.ShowDialog() != DialogResult.OK) || (reminder.SelectedID == null))
                {
                    flag = false;
                }
                else
                {
                    this.LoadBatchPayment(reminder.SelectedID.Value);
                    this.ClearPaymentTable();
                    this.LoadLineItems();
                    this.UpdateAmountLeft();
                    flag = true;
                }
            }
            return flag;
        }

        private void nudAmount_Validating(object sender, CancelEventArgs e)
        {
            this.UpdateAmountLeft();
        }

        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateAmountLeft();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.FBatchPaymentID = null;
                Functions.SetUpDownValue(this.nudCheckAmount, 0.0);
                Functions.SetDateBoxValue(this.dtbCheckDate, DateTime.Today);
                Functions.SetTextBoxText(this.txtCheckNumber, DBNull.Value);
                this.AmountUsed = 0M;
                if ((0 >= this.GetReminderCount()) || !this.LoadReminder())
                {
                    this.ClearPaymentTable();
                    this.ClearLineItems();
                }
                this.UpdateAmountLeft();
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
            this.SafeInvoke(new Action(this.LoadLineItems));
        }

        private bool SaveBatchPayment(double amountUsed)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("CheckAmount", MySqlType.Double).Value = Math.Round(this.nudCheckAmount.Value, 2);
                    command.Parameters.Add("CheckDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbCheckDate);
                    command.Parameters.Add("CheckNumber", MySqlType.VarChar, 0x19).Value = this.txtCheckNumber.Text;
                    command.Parameters.Add("AmountUsed", MySqlType.Double).Value = Math.Round(amountUsed, 2);
                    command.Parameters.Add("InsuranceCompanyID", MySqlType.Int).Value = this.cmbInsuranceCompany.SelectedValue;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (this.FBatchPaymentID != null)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = this.FBatchPaymentID.Value;
                        string[] whereParameters = new string[] { "ID" };
                        flag = (0 >= command.ExecuteUpdate("tbl_batchpayment", whereParameters)) ? (0 < command.ExecuteInsert("tbl_batchpayment")) : true;
                    }
                    else if (0 >= command.ExecuteInsert("tbl_batchpayment"))
                    {
                        flag = false;
                    }
                    else
                    {
                        this.FBatchPaymentID = new int?(command.GetLastIdentity());
                        flag = true;
                    }
                }
            }
            return flag;
        }

        private void SavePayments()
        {
            decimal num2;
            int? nullable = NullableConvert.ToInt32(this.cmbInsuranceCompany.SelectedValue);
            DateTime? nullable2 = NullableConvert.ToDateTime(this.dtbCheckDate.Value);
            DateTime? nullable3 = NullableConvert.ToDateTime(this.dtbPostingDate.Value);
            if (nullable2 == null)
            {
                throw new UserNotifyException("You must select check date.");
            }
            if (nullable3 == null)
            {
                throw new UserNotifyException("You must select posting date.");
            }
            DataRow[] rowArray = this.FTable_Payments.Select();
            if (rowArray.Length == 0)
            {
                throw new UserNotifyException("You must select line items.");
            }
            double num = NullableConvert.ToDouble(this.FTable_Payments.Compute("SUM([PostingPaidAmount])", ""), 0.0);
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                DataRow[] rowArray2 = rowArray;
                int index = 0;
                while (true)
                {
                    while (true)
                    {
                        if (index < rowArray2.Length)
                        {
                            DataRow row = rowArray2[index];
                            PaymentExtraData data = new PaymentExtraData {
                                PaymentMethod = 1,
                                CheckNumber = this.txtCheckNumber.Text,
                                CheckDate = nullable2,
                                Billable = NullableConvert.ToDecimal(row[this.FTable_Payments.Col_BillableAmount]),
                                Paid = NullableConvert.ToDecimal(row[this.FTable_Payments.Col_PostingPaidAmount]),
                                Allowable = NullableConvert.ToDecimal(row[this.FTable_Payments.Col_EnteredAllowableAmount]),
                                Deductible = NullableConvert.ToDecimal(row[this.FTable_Payments.Col_EnteredDeductibleAmount]),
                                Coins = NullableConvert.ToDecimal(row[this.FTable_Payments.Col_EnteredCoinsAmount])
                            };
                            bool? nullable4 = NullableConvert.ToBoolean(row[this.FTable_Payments.Col_WriteoffBalance]);
                            string str = !nullable4.GetValueOrDefault(false) ? (!this.rbZeroPayment_Denied.Checked ? "Adjust Allowable" : "Adjust Allowable,Post Denied") : (!this.rbZeroPayment_Denied.Checked ? "Adjust Allowable,Writeoff Balance" : "Adjust Allowable,Post Denied,Writeoff Balance");
                            MySqlTransaction transaction = connection.BeginTransaction();
                            try
                            {
                                using (MySqlCommand command = new MySqlCommand("", connection, transaction))
                                {
                                    command.Parameters.Add("P_InvoiceDetailsID", MySqlType.Int).Value = row[this.FTable_Payments.Col_InvoiceDetailsID];
                                    command.Parameters.Add("P_InsuranceCompanyID", MySqlType.Int).Value = NullableConvert.ToDb(nullable);
                                    command.Parameters.Add("P_TransactionDate", MySqlType.Date).Value = nullable3.Value;
                                    command.Parameters.Add("P_Extra", MySqlType.Text).Value = data.ToString();
                                    command.Parameters.Add("P_Comments", MySqlType.Text).Value = "Batch Posting";
                                    command.Parameters.Add("P_Options", MySqlType.Text).Value = str;
                                    command.Parameters.Add("P_LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                                    command.Parameters.Add("P_Result", MySqlType.VarChar, 0xff).Direction = ParameterDirection.Output;
                                    command.ExecuteProcedure("InvoiceDetails_AddPayment");
                                }
                                using (MySqlCommand command2 = new MySqlCommand("", connection, transaction))
                                {
                                    command2.Parameters.Add("P_InvoiceID", MySqlType.Int).Value = row[this.FTable_Payments.Col_InvoiceID];
                                    command2.Parameters.Add("P_Recursive", MySqlType.Int).Value = 1;
                                    command2.ExecuteProcedure("Invoice_UpdateBalance");
                                }
                                transaction.Commit();
                            }
                            catch (Exception exception1)
                            {
                                Exception ex = exception1;
                                ProjectData.SetProjectError(ex);
                                Exception exception = ex;
                                transaction.Rollback();
                                Trace.WriteLine(exception);
                                ProjectData.ClearProjectError();
                                break;
                            }
                            row.Delete();
                            row.AcceptChanges();
                        }
                        else
                        {
                            goto TR_000B;
                        }
                        break;
                    }
                    index++;
                }
            }
        TR_000B:
            num2 = Math.Round(decimal.Add(this.AmountUsed, new decimal(num)), 2);
            if (nullable != null)
            {
                if (decimal.Compare(num2, Math.Round(this.nudCheckAmount.Value, 2)) < 0)
                {
                    this.SaveBatchPayment(Convert.ToDouble(num2));
                }
                else if (this.FBatchPaymentID != null)
                {
                    this.DeleteBatchPayment();
                }
            }
            this.AmountUsed = num2;
            this.UpdateAmountLeft();
            string[] tableNames = new string[] { "tbl_invoice_transaction", "tbl_invoicedetails", "tbl_invoice" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private static void SetError(ErrorProvider provider, Control control, string value)
        {
            provider.SetError(control, value);
            provider.SetIconAlignment(control, ErrorIconAlignment.TopLeft);
            provider.SetIconPadding(control, -17);
        }

        private void sgLineItems_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if ((dataRow != null) && ReferenceEquals(dataRow.Table, this.FTable_LineItems))
                {
                    if (this.FTable_Payments.Rows.Find(dataRow[this.FTable_LineItems.Col_InvoiceDetailsID]) == null)
                    {
                        this.FTable_Payments.ImportRow(dataRow);
                    }
                    this.FTable_Payments.AcceptChanges();
                    this.UpdateAmountLeft();
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

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in this.sgLineItems.GetSelectedRows().GetDataRows())
                {
                    if (ReferenceEquals(row.Table, this.FTable_LineItems) && (this.FTable_Payments.Rows.Find(row[this.FTable_LineItems.Col_InvoiceDetailsID]) == null))
                    {
                        this.FTable_Payments.ImportRow(row);
                    }
                }
                this.FTable_Payments.AcceptChanges();
                this.UpdateAmountLeft();
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

        private void tsbRemove_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in this.sgPayments.GetSelectedRows().GetDataRows())
                {
                    if (ReferenceEquals(row.Table, this.FTable_Payments))
                    {
                        row.Delete();
                    }
                }
                this.FTable_Payments.AcceptChanges();
                this.UpdateAmountLeft();
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

        private void tsbRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.FTable_Payments.Clear();
                this.FTable_Payments.AcceptChanges();
                this.UpdateAmountLeft();
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

        private void txtCheckNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.FBatchPaymentID != null)
                {
                    this.FBatchPaymentID = null;
                    Functions.SetUpDownValue(this.nudCheckAmount, 0.0);
                    Functions.SetDateBoxValue(this.dtbCheckDate, DateTime.Today);
                    this.AmountUsed = 0M;
                    this.UpdateAmountLeft();
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

        private void UpdateAmountLeft()
        {
            this.AmountLeft = decimal.Subtract(decimal.Subtract(this.nudCheckAmount.Value, this.AmountUsed), NullableConvert.ToDecimal(this.FTable_Payments.Compute("SUM([PostingPaidAmount])", ""), 0M));
            if (Convert.ToDouble(Math.Round(this.AmountLeft, 2)) < 0.0)
            {
                SetError(this.ErrorProvider1, this.nudCheckAmount, "The amount left is lesser than zero.");
            }
            else
            {
                SetError(this.ErrorProvider1, this.nudCheckAmount, "");
            }
        }

        private void ValidatePayments()
        {
            if (NullableConvert.ToDateTime(this.dtbCheckDate.Value) == null)
            {
                SetError(this.ErrorProvider1, this.dtbCheckDate, "You must select check date.");
            }
            else
            {
                SetError(this.ErrorProvider1, this.dtbCheckDate, "");
            }
            if (NullableConvert.ToDateTime(this.dtbPostingDate.Value) == null)
            {
                SetError(this.ErrorProvider1, this.dtbPostingDate, "You must select posting date.");
            }
            else
            {
                SetError(this.ErrorProvider1, this.dtbPostingDate, "");
            }
            if (this.txtCheckNumber.Text.TrimEnd(new char[0]).Length == 0)
            {
                SetError(this.ErrorProvider1, this.txtCheckNumber, "You must enter check number.");
            }
            else
            {
                SetError(this.ErrorProvider1, this.txtCheckNumber, "");
            }
            if (this.FTable_Payments.Select().Length == 0)
            {
                SetError(this.ErrorProvider1, this.sgPayments, "You must add line items.");
            }
            else
            {
                SetError(this.ErrorProvider1, this.sgPayments, "");
            }
            StringBuilder builder = new StringBuilder("There are some errors in input data");
            if (0 < Functions.EnumerateErrors(this, this.ErrorProvider1, builder))
            {
                throw new UserNotifyException(builder.ToString());
            }
        }

        [field: AccessedThroughProperty("TableLayoutPanel1")]
        private TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nudCheckAmount")]
        private NumericUpDown nudCheckAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCheckNumber")]
        private Label lblCheckNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmountLeftValue")]
        private Label lblAmountLeftValue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbCheckDate")]
        private UltraDateTimeEditor dtbCheckDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbPostingDate")]
        private UltraDateTimeEditor dtbPostingDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmountLeft")]
        private Label lblAmountLeft { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmount")]
        private Label lblAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInsuranceCompany")]
        private Combobox cmbInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInsuranceCompany")]
        private Label lblInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnSave")]
        private Button btnSave { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnReminder")]
        private Button btnReminder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("sgLineItems")]
        private SearchableGrid sgLineItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("sgPayments")]
        private SearchableGrid sgPayments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tslblPayments")]
        private ToolStripLabel tslblPayments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStripSeparator1")]
        private ToolStripSeparator ToolStripSeparator1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAdd")]
        private ToolStripButton tsbAdd { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbRemove")]
        private ToolStripButton tsbRemove { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbRemoveAll")]
        private ToolStripButton tsbRemoveAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Splitter1")]
        private Splitter Splitter1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCheckNumber")]
        private TextBox txtCheckNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCheckDate")]
        private Label lblCheckDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPostingDate")]
        private Label lblPostingDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider1")]
        private ErrorProvider ErrorProvider1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolTip1")]
        private ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmountUsed")]
        private Label lblAmountUsed { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmountUsedValue")]
        private Label lblAmountUsedValue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tableZeroPayment")]
        private TableLayoutPanel tableZeroPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbZeroPayment_Payment")]
        private RadioButton rbZeroPayment_Payment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblZeroPayment")]
        private Label lblZeroPayment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbZeroPayment_Denied")]
        private RadioButton rbZeroPayment_Denied { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        private decimal AmountUsed
        {
            get => 
                this.FAmountUsed;
            set
            {
                this.FAmountUsed = value;
                this.lblAmountUsedValue.Text = this.FAmountUsed.ToString("0.00");
            }
        }

        private decimal AmountLeft
        {
            get => 
                this.FAmountLeft;
            set
            {
                this.FAmountLeft = value;
                this.lblAmountLeftValue.Text = this.FAmountLeft.ToString("0.00");
            }
        }

        private class Filter : DMEWorks.Data.IFilter
        {
            public static readonly FormBatchPayments.Filter Instance = new FormBatchPayments.Filter();

            public string GetKey() => 
                base.GetType().Name;

            public DataTable Process(DataTable Source)
            {
                DataTable table = Source.Clone();
                foreach (DataRow row in Source.Select())
                {
                    table.ImportRow(row);
                }
                DataRow[] rowArray2 = table.Select("ID IS NULL");
                for (int i = 0; i < rowArray2.Length; i++)
                {
                    rowArray2[i]["Name"] = "Customer";
                }
                return table;
            }
        }

        public class TableLineItems : EventTableBase
        {
            public DataColumn Col_CustomerID;
            public DataColumn Col_InvoiceID;
            public DataColumn Col_InvoiceDetailsID;
            public DataColumn Col_CustomerInsuranceID;
            public DataColumn Col_FirstName;
            public DataColumn Col_LastName;
            public DataColumn Col_BillingCode;
            public DataColumn Col_BillableAmount;
            public DataColumn Col_AllowableAmount;
            public DataColumn Col_TotalWriteoffAmount;
            public DataColumn Col_TotalPaymentAmount;
            public DataColumn Col_Quantity;
            public DataColumn Col_Percent;
            public DataColumn Col_Basis;
            public DataColumn Col_DOSFrom;
            public DataColumn Col_CurrentPayer;
            public DataColumn Col_Responsibility;
            public DataColumn Col_Balance;

            public TableLineItems()
            {
            }

            public TableLineItems(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                this.Col_CustomerID = base.Columns["CustomerID"];
                this.Col_InvoiceID = base.Columns["InvoiceID"];
                this.Col_InvoiceDetailsID = base.Columns["InvoiceDetailsID"];
                this.Col_CustomerInsuranceID = base.Columns["CustomerInsuranceID"];
                this.Col_FirstName = base.Columns["FirstName"];
                this.Col_LastName = base.Columns["LastName"];
                this.Col_BillingCode = base.Columns["BillingCode"];
                this.Col_BillableAmount = base.Columns["BillableAmount"];
                this.Col_AllowableAmount = base.Columns["AllowableAmount"];
                this.Col_TotalWriteoffAmount = base.Columns["TotalWriteoffAmount"];
                this.Col_TotalPaymentAmount = base.Columns["TotalPaymentAmount"];
                this.Col_Quantity = base.Columns["Quantity"];
                this.Col_Percent = base.Columns["Percent"];
                this.Col_Basis = base.Columns["Basis"];
                this.Col_DOSFrom = base.Columns["DOSFrom"];
                this.Col_CurrentPayer = base.Columns["CurrentPayer"];
                this.Col_Responsibility = base.Columns["Responsibility"];
                this.Col_Balance = base.Columns["Balance"];
            }

            protected override void InitializeClass()
            {
                this.Col_CustomerID = base.Columns.Add("CustomerID", typeof(int));
                this.Col_CustomerID.AllowDBNull = false;
                this.Col_InvoiceID = base.Columns.Add("InvoiceID", typeof(int));
                this.Col_InvoiceID.AllowDBNull = false;
                this.Col_InvoiceDetailsID = base.Columns.Add("InvoiceDetailsID", typeof(int));
                this.Col_InvoiceDetailsID.AllowDBNull = false;
                this.Col_CustomerInsuranceID = base.Columns.Add("CustomerInsuranceID", typeof(int));
                this.Col_FirstName = base.Columns.Add("FirstName", typeof(string));
                this.Col_FirstName.AllowDBNull = false;
                this.Col_LastName = base.Columns.Add("LastName", typeof(string));
                this.Col_LastName.AllowDBNull = false;
                this.Col_BillingCode = base.Columns.Add("BillingCode", typeof(string));
                this.Col_BillingCode.AllowDBNull = false;
                this.Col_BillableAmount = base.Columns.Add("BillableAmount", typeof(double));
                this.Col_BillableAmount.AllowDBNull = false;
                this.Col_AllowableAmount = base.Columns.Add("AllowableAmount", typeof(double));
                this.Col_AllowableAmount.AllowDBNull = false;
                this.Col_TotalWriteoffAmount = base.Columns.Add("TotalWriteoffAmount", typeof(double));
                this.Col_TotalWriteoffAmount.AllowDBNull = false;
                this.Col_TotalPaymentAmount = base.Columns.Add("TotalPaymentAmount", typeof(double));
                this.Col_TotalPaymentAmount.AllowDBNull = false;
                this.Col_Quantity = base.Columns.Add("Quantity", typeof(double));
                this.Col_Quantity.AllowDBNull = false;
                this.Col_Percent = base.Columns.Add("Percent", typeof(double));
                this.Col_Percent.AllowDBNull = false;
                this.Col_Basis = base.Columns.Add("Basis", typeof(string));
                this.Col_Basis.AllowDBNull = false;
                this.Col_DOSFrom = base.Columns.Add("DOSFrom", typeof(DateTime));
                this.Col_CurrentPayer = base.Columns.Add("CurrentPayer", typeof(string));
                this.Col_CurrentPayer.AllowDBNull = false;
                this.Col_Responsibility = base.Columns.Add("Responsibility", typeof(string));
                this.Col_Responsibility.AllowDBNull = false;
                this.Col_Balance = base.Columns.Add("Balance", typeof(double));
                this.Col_Balance.AllowDBNull = true;
                base.PrimaryKey = new DataColumn[] { this.Col_InvoiceDetailsID };
            }
        }

        public class TablePayments : FormBatchPayments.TableLineItems
        {
            public DataColumn Col_EnteredAllowableAmount;
            public DataColumn Col_EnteredPaidAmount;
            public DataColumn Col_EnteredCoinsAmount;
            public DataColumn Col_EnteredDeductibleAmount;
            public DataColumn Col_ExpectedPaidAmount;
            public DataColumn Col_PostingPaidAmount;
            public DataColumn Col_WriteoffBalance;

            public TablePayments()
            {
            }

            public TablePayments(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_EnteredAllowableAmount = base.Columns["EnteredAllowableAmount"];
                this.Col_EnteredPaidAmount = base.Columns["EnteredPaidAmount"];
                this.Col_EnteredCoinsAmount = base.Columns["EnteredCoinsAmount"];
                this.Col_EnteredDeductibleAmount = base.Columns["EnteredDeductibleAmount"];
                this.Col_ExpectedPaidAmount = base.Columns["ExpectedPaidAmount"];
                this.Col_PostingPaidAmount = base.Columns["PostingPaidAmount"];
                this.Col_WriteoffBalance = base.Columns["WriteoffBalance"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                this.Col_EnteredAllowableAmount = base.Columns.Add("EnteredAllowableAmount", typeof(double));
                this.Col_EnteredPaidAmount = base.Columns.Add("EnteredPaidAmount", typeof(double));
                this.Col_EnteredDeductibleAmount = base.Columns.Add("EnteredDeductibleAmount", typeof(double));
                this.Col_EnteredCoinsAmount = base.Columns.Add("EnteredCoinsAmount", typeof(double));
                this.Col_ExpectedPaidAmount = base.Columns.Add("ExpectedPaidAmount", typeof(double));
                this.Col_ExpectedPaidAmount.Expression = "IIF([Responsibility] = 'Ins1',0.01 * [Percent] * IIF([Basis] = 'Bill', [BillableAmount], ISNULL([EnteredAllowableAmount], [AllowableAmount])) - ISNULL([EnteredDeductibleAmount], 0),IIF([Responsibility] = 'Patient',[BillableAmount] - [TotalWriteoffAmount] - [TotalPaymentAmount],IIF(0.01 <= [TotalPaymentAmount],[BillableAmount] - [TotalWriteoffAmount] - [TotalPaymentAmount],(1 - 0.01 * [Percent]) * IIF([Basis] = 'Bill', [BillableAmount], [AllowableAmount]))))";
                this.Col_PostingPaidAmount = base.Columns.Add("PostingPaidAmount", typeof(double));
                this.Col_PostingPaidAmount.Expression = "ISNULL([EnteredPaidAmount], [ExpectedPaidAmount])";
                this.Col_WriteoffBalance = base.Columns.Add("WriteoffBalance", typeof(bool));
                this.Col_WriteoffBalance.AllowDBNull = false;
                this.Col_WriteoffBalance.DefaultValue = false;
            }

            protected override void OnColumnChanged(DataColumnChangeEventArgs e)
            {
                if (ReferenceEquals(e.Column, this.Col_EnteredAllowableAmount) || (ReferenceEquals(e.Column, this.Col_EnteredPaidAmount) || (ReferenceEquals(e.Column, this.Col_EnteredCoinsAmount) || (ReferenceEquals(e.Column, this.Col_EnteredDeductibleAmount) || ReferenceEquals(e.Column, this.Col_WriteoffBalance)))))
                {
                    e.Row.BeginEdit();
                    e.Row.EndEdit();
                }
                base.OnColumnChanged(e);
            }
        }
    }
}

