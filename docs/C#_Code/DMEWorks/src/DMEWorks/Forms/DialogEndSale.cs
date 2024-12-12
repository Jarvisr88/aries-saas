namespace DMEWorks.Forms
{
    using DMEWorks.Controls;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class DialogEndSale : DmeForm
    {
        private IContainer components;

        public DialogEndSale()
        {
            this.InitializeComponent();
            this.cmbPaymentMethod.Text = "Cash";
            this.UpdateVisibleState();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.ValidateObject();
                StringBuilder builder = new StringBuilder();
                builder.Append("Errors in the input data.\r\nPlease fix and proceed.\r\n");
                if (Functions.EnumerateErrors(this, this.ErrorProvider, builder) > 0)
                {
                    MessageBox.Show(builder.ToString(), "Errors");
                }
                else
                {
                    base.DialogResult = DialogResult.OK;
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

        private void cmbPaymentMethod_TextChanged(object sender, EventArgs e)
        {
            this.UpdateVisibleState();
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new Button();
            this.txtPaymentTotal = new TextBox();
            this.lblDiscountTotal = new Label();
            this.lblTaxTotal = new Label();
            this.txtTaxTotal = new TextBox();
            this.lblPaymentTotal = new Label();
            this.lblSubtotal = new Label();
            this.txtSubtotal = new TextBox();
            this.txtDiscountTotal = new TextBox();
            this.btnOK = new Button();
            this.lblPaymentMethod = new Label();
            this.cmbPaymentMethod = new ComboBox();
            this.lblExpirationDate = new Label();
            this.txtMonth = new TextBox();
            this.txtYear = new TextBox();
            this.pnlCreditCard = new Panel();
            this.txtCreditCardNumber = new TextBox();
            this.lblCreditCardNumber = new Label();
            this.lblSlash = new Label();
            this.pnlCheck = new Panel();
            this.dtbCheckDate = new UltraDateTimeEditor();
            this.lblCheckDate = new Label();
            this.lblDriverLicenseNumber = new Label();
            this.dtbBirthdate = new UltraDateTimeEditor();
            this.txtCheckNumber = new TextBox();
            this.lblCheckNumber = new Label();
            this.txtDriverLicenseNumber = new TextBox();
            this.lblBirthDate = new Label();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblAmountTendered = new Label();
            this.lblChange = new Label();
            this.nmbAmountTendered = new NumericBox();
            this.nmbChange = new NumericBox();
            this.ToolTip1 = new ToolTip(this.components);
            this.pnlCreditCard.SuspendLayout();
            this.pnlCheck.SuspendLayout();
            ((ISupportInitialize) this.ErrorProvider).BeginInit();
            base.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xd8, 0xa8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 0x11;
            this.btnCancel.Text = "Cancel";
            this.txtPaymentTotal.BackColor = SystemColors.Window;
            this.txtPaymentTotal.Location = new Point(320, 0x70);
            this.txtPaymentTotal.Name = "txtPaymentTotal";
            this.txtPaymentTotal.ReadOnly = true;
            this.txtPaymentTotal.Size = new Size(0x60, 20);
            this.txtPaymentTotal.TabIndex = 13;
            this.txtPaymentTotal.TextAlign = HorizontalAlignment.Right;
            this.lblDiscountTotal.BackColor = Color.Transparent;
            this.lblDiscountTotal.Location = new Point(0x100, 0x38);
            this.lblDiscountTotal.Name = "lblDiscountTotal";
            this.lblDiscountTotal.Size = new Size(0x38, 0x15);
            this.lblDiscountTotal.TabIndex = 8;
            this.lblDiscountTotal.Text = "Discount";
            this.lblDiscountTotal.TextAlign = ContentAlignment.MiddleRight;
            this.lblTaxTotal.Location = new Point(0x100, 80);
            this.lblTaxTotal.Name = "lblTaxTotal";
            this.lblTaxTotal.Size = new Size(0x38, 0x15);
            this.lblTaxTotal.TabIndex = 10;
            this.lblTaxTotal.Text = "Tax Total";
            this.lblTaxTotal.TextAlign = ContentAlignment.MiddleRight;
            this.txtTaxTotal.BackColor = SystemColors.Window;
            this.txtTaxTotal.Location = new Point(320, 80);
            this.txtTaxTotal.Name = "txtTaxTotal";
            this.txtTaxTotal.ReadOnly = true;
            this.txtTaxTotal.Size = new Size(0x60, 20);
            this.txtTaxTotal.TabIndex = 11;
            this.txtTaxTotal.TextAlign = HorizontalAlignment.Right;
            this.lblPaymentTotal.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.lblPaymentTotal.Location = new Point(0x100, 0x70);
            this.lblPaymentTotal.Name = "lblPaymentTotal";
            this.lblPaymentTotal.Size = new Size(0x38, 0x15);
            this.lblPaymentTotal.TabIndex = 12;
            this.lblPaymentTotal.Text = "Total";
            this.lblPaymentTotal.TextAlign = ContentAlignment.MiddleRight;
            this.lblSubtotal.Location = new Point(0x100, 0x20);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new Size(0x38, 0x15);
            this.lblSubtotal.TabIndex = 6;
            this.lblSubtotal.Text = "Sub-total";
            this.lblSubtotal.TextAlign = ContentAlignment.MiddleRight;
            this.txtSubtotal.BackColor = SystemColors.Window;
            this.txtSubtotal.Location = new Point(320, 0x20);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new Size(0x60, 20);
            this.txtSubtotal.TabIndex = 7;
            this.txtSubtotal.TextAlign = HorizontalAlignment.Right;
            this.txtDiscountTotal.BackColor = SystemColors.Window;
            this.txtDiscountTotal.Location = new Point(320, 0x38);
            this.txtDiscountTotal.Name = "txtDiscountTotal";
            this.txtDiscountTotal.ReadOnly = true;
            this.txtDiscountTotal.Size = new Size(0x60, 20);
            this.txtDiscountTotal.TabIndex = 9;
            this.txtDiscountTotal.TextAlign = HorizontalAlignment.Right;
            this.btnOK.Location = new Point(0x88, 0xa8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 0x10;
            this.btnOK.Text = "OK";
            this.lblPaymentMethod.Location = new Point(0x10, 0x20);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new Size(0x60, 0x17);
            this.lblPaymentMethod.TabIndex = 0;
            this.lblPaymentMethod.Text = "Payment Method";
            this.lblPaymentMethod.TextAlign = ContentAlignment.MiddleRight;
            object[] items = new object[] { "Cash", "Check", "Credit card" };
            this.cmbPaymentMethod.Items.AddRange(items);
            this.cmbPaymentMethod.Location = new Point(120, 0x20);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new Size(0x79, 0x15);
            this.cmbPaymentMethod.TabIndex = 1;
            this.lblExpirationDate.Location = new Point(8, 0x20);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new Size(0x60, 0x17);
            this.lblExpirationDate.TabIndex = 2;
            this.lblExpirationDate.Text = "Expiration Date";
            this.lblExpirationDate.TextAlign = ContentAlignment.MiddleRight;
            this.txtMonth.Location = new Point(0x70, 0x20);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new Size(0x20, 20);
            this.txtMonth.TabIndex = 3;
            this.txtMonth.Text = "MM";
            this.ToolTip1.SetToolTip(this.txtMonth, "MM - month");
            this.txtYear.Location = new Point(160, 0x20);
            this.txtYear.MaxLength = 2;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new Size(0x20, 20);
            this.txtYear.TabIndex = 5;
            this.txtYear.Text = "YY";
            this.ToolTip1.SetToolTip(this.txtYear, "YY - year");
            this.pnlCreditCard.Controls.Add(this.txtCreditCardNumber);
            this.pnlCreditCard.Controls.Add(this.lblCreditCardNumber);
            this.pnlCreditCard.Controls.Add(this.lblSlash);
            this.pnlCreditCard.Controls.Add(this.txtMonth);
            this.pnlCreditCard.Controls.Add(this.txtYear);
            this.pnlCreditCard.Controls.Add(this.lblExpirationDate);
            this.pnlCreditCard.Location = new Point(8, 0x38);
            this.pnlCreditCard.Name = "pnlCreditCard";
            this.pnlCreditCard.Size = new Size(240, 0x68);
            this.pnlCreditCard.TabIndex = 2;
            this.txtCreditCardNumber.Location = new Point(0x70, 8);
            this.txtCreditCardNumber.MaxLength = 20;
            this.txtCreditCardNumber.Name = "txtCreditCardNumber";
            this.txtCreditCardNumber.Size = new Size(80, 20);
            this.txtCreditCardNumber.TabIndex = 1;
            this.lblCreditCardNumber.Location = new Point(8, 8);
            this.lblCreditCardNumber.Name = "lblCreditCardNumber";
            this.lblCreditCardNumber.Size = new Size(0x60, 0x17);
            this.lblCreditCardNumber.TabIndex = 0;
            this.lblCreditCardNumber.Text = "Credit Card #";
            this.lblCreditCardNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblSlash.Location = new Point(0x90, 0x20);
            this.lblSlash.Name = "lblSlash";
            this.lblSlash.Size = new Size(0x10, 0x17);
            this.lblSlash.TabIndex = 4;
            this.lblSlash.Text = "/";
            this.lblSlash.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlCheck.Controls.Add(this.dtbCheckDate);
            this.pnlCheck.Controls.Add(this.lblCheckDate);
            this.pnlCheck.Controls.Add(this.lblDriverLicenseNumber);
            this.pnlCheck.Controls.Add(this.dtbBirthdate);
            this.pnlCheck.Controls.Add(this.txtCheckNumber);
            this.pnlCheck.Controls.Add(this.lblCheckNumber);
            this.pnlCheck.Controls.Add(this.txtDriverLicenseNumber);
            this.pnlCheck.Controls.Add(this.lblBirthDate);
            this.pnlCheck.Location = new Point(8, 0x38);
            this.pnlCheck.Name = "pnlCheck";
            this.pnlCheck.Size = new Size(240, 0x68);
            this.pnlCheck.TabIndex = 3;
            this.dtbCheckDate.Location = new Point(0x70, 0x20);
            this.dtbCheckDate.Name = "dtbCheckDate";
            this.dtbCheckDate.Size = new Size(120, 0x15);
            this.dtbCheckDate.TabIndex = 3;
            this.lblCheckDate.Location = new Point(8, 0x20);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new Size(0x60, 0x17);
            this.lblCheckDate.TabIndex = 2;
            this.lblCheckDate.Text = "Check date";
            this.lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblDriverLicenseNumber.Location = new Point(8, 80);
            this.lblDriverLicenseNumber.Name = "lblDriverLicenseNumber";
            this.lblDriverLicenseNumber.Size = new Size(0x60, 0x17);
            this.lblDriverLicenseNumber.TabIndex = 6;
            this.lblDriverLicenseNumber.Text = "Driver License #";
            this.lblDriverLicenseNumber.TextAlign = ContentAlignment.MiddleRight;
            this.dtbBirthdate.Location = new Point(0x70, 0x38);
            this.dtbBirthdate.Name = "dtbBirthdate";
            this.dtbBirthdate.Size = new Size(120, 0x15);
            this.dtbBirthdate.TabIndex = 5;
            this.txtCheckNumber.Location = new Point(0x70, 8);
            this.txtCheckNumber.MaxLength = 20;
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new Size(120, 20);
            this.txtCheckNumber.TabIndex = 1;
            this.lblCheckNumber.Location = new Point(8, 8);
            this.lblCheckNumber.Name = "lblCheckNumber";
            this.lblCheckNumber.Size = new Size(0x60, 0x17);
            this.lblCheckNumber.TabIndex = 0;
            this.lblCheckNumber.Text = "Check #";
            this.lblCheckNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtDriverLicenseNumber.Location = new Point(0x70, 80);
            this.txtDriverLicenseNumber.MaxLength = 20;
            this.txtDriverLicenseNumber.Name = "txtDriverLicenseNumber";
            this.txtDriverLicenseNumber.Size = new Size(120, 20);
            this.txtDriverLicenseNumber.TabIndex = 7;
            this.lblBirthDate.Location = new Point(8, 0x38);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new Size(0x60, 0x17);
            this.lblBirthDate.TabIndex = 4;
            this.lblBirthDate.Text = "Birthdate";
            this.lblBirthDate.TextAlign = ContentAlignment.MiddleRight;
            this.ErrorProvider.ContainerControl = this;
            this.lblAmountTendered.Location = new Point(0xd8, 8);
            this.lblAmountTendered.Name = "lblAmountTendered";
            this.lblAmountTendered.Size = new Size(0x60, 0x15);
            this.lblAmountTendered.TabIndex = 4;
            this.lblAmountTendered.Text = "Amount Tendered";
            this.lblAmountTendered.TextAlign = ContentAlignment.MiddleRight;
            this.lblChange.Location = new Point(0x100, 0x88);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new Size(0x38, 0x15);
            this.lblChange.TabIndex = 14;
            this.lblChange.Text = "Change";
            this.lblChange.TextAlign = ContentAlignment.MiddleRight;
            this.nmbAmountTendered.Location = new Point(320, 8);
            this.nmbAmountTendered.Name = "nmbAmountTendered";
            this.nmbAmountTendered.Size = new Size(0x60, 20);
            this.nmbAmountTendered.TabIndex = 5;
            this.nmbAmountTendered.TextAlign = HorizontalAlignment.Right;
            this.nmbChange.Location = new Point(320, 0x88);
            this.nmbChange.Name = "nmbChange";
            this.nmbChange.Size = new Size(0x60, 20);
            this.nmbChange.TabIndex = 15;
            this.nmbChange.TextAlign = HorizontalAlignment.Right;
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x1a8, 0xc7);
            base.Controls.Add(this.nmbChange);
            base.Controls.Add(this.nmbAmountTendered);
            base.Controls.Add(this.lblChange);
            base.Controls.Add(this.lblAmountTendered);
            base.Controls.Add(this.cmbPaymentMethod);
            base.Controls.Add(this.lblPaymentMethod);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.txtPaymentTotal);
            base.Controls.Add(this.lblDiscountTotal);
            base.Controls.Add(this.lblTaxTotal);
            base.Controls.Add(this.txtTaxTotal);
            base.Controls.Add(this.txtDiscountTotal);
            base.Controls.Add(this.lblSubtotal);
            base.Controls.Add(this.lblPaymentTotal);
            base.Controls.Add(this.txtSubtotal);
            base.Controls.Add(this.pnlCheck);
            base.Controls.Add(this.pnlCreditCard);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "DialogEndSale";
            base.ShowInTaskbar = false;
            this.Text = "End Sale";
            this.pnlCreditCard.ResumeLayout(false);
            this.pnlCreditCard.PerformLayout();
            this.pnlCheck.ResumeLayout(false);
            this.pnlCheck.PerformLayout();
            ((ISupportInitialize) this.ErrorProvider).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void nmbAmountTendered_ValueChanged(object sender, EventArgs e)
        {
            double num = 0.0;
            try
            {
                num = double.Parse(this.txtPaymentTotal.Text);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
            this.nmbChange.AsDouble = new double?(Math.Round((double) (Math.Round(this.nmbAmountTendered.AsDouble.GetValueOrDefault(0.0), 2) - Math.Round(num, 2)), 2));
        }

        public void SetTotals(double SubTotal, double DiscountTotal, double TaxTotal)
        {
            this.txtSubtotal.Text = SubTotal.ToString("0.00");
            this.txtDiscountTotal.Text = DiscountTotal.ToString("0.00");
            this.txtTaxTotal.Text = TaxTotal.ToString("0.00");
            this.txtPaymentTotal.Text = ((SubTotal - DiscountTotal) + TaxTotal).ToString("0.00");
        }

        private void UpdateVisibleState()
        {
            this.pnlCheck.Visible = string.Compare(this.cmbPaymentMethod.Text, "Check", true) == 0;
            this.pnlCreditCard.Visible = string.Compare(this.cmbPaymentMethod.Text, "Credit Card", true) == 0;
            bool flag = string.Compare(this.cmbPaymentMethod.Text, "Cash", true) == 0;
            this.lblAmountTendered.Visible = flag;
            this.nmbAmountTendered.Visible = flag;
            this.lblChange.Visible = flag;
            this.nmbChange.Visible = flag;
        }

        private void ValidateObject()
        {
            this.ErrorProvider.SetIconAlignment(this.cmbPaymentMethod, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconPadding(this.cmbPaymentMethod, -16);
            this.ErrorProvider.SetIconAlignment(this.txtCheckNumber, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconAlignment(this.dtbCheckDate, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconAlignment(this.dtbBirthdate, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconAlignment(this.txtDriverLicenseNumber, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconPadding(this.txtCheckNumber, -16);
            this.ErrorProvider.SetIconPadding(this.dtbCheckDate, -16);
            this.ErrorProvider.SetIconPadding(this.dtbBirthdate, -16);
            this.ErrorProvider.SetIconPadding(this.txtDriverLicenseNumber, -16);
            this.ErrorProvider.SetIconAlignment(this.txtCreditCardNumber, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconAlignment(this.txtMonth, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconAlignment(this.txtYear, ErrorIconAlignment.TopRight);
            this.ErrorProvider.SetIconPadding(this.txtCreditCardNumber, -16);
            this.ErrorProvider.SetIconPadding(this.txtMonth, -16);
            this.ErrorProvider.SetIconPadding(this.txtYear, -16);
            if ((string.Compare(this.cmbPaymentMethod.Text, "Check", true) != 0) && ((string.Compare(this.cmbPaymentMethod.Text, "Credit Card", true) != 0) && (string.Compare(this.cmbPaymentMethod.Text, "Cash", true) != 0)))
            {
                this.ErrorProvider.SetError(this.cmbPaymentMethod, "Please select value from the dropdown list.");
            }
            else
            {
                this.ErrorProvider.SetError(this.cmbPaymentMethod, "");
            }
            if (string.Compare(this.cmbPaymentMethod.Text, "Check", true) != 0)
            {
                this.ErrorProvider.SetError(this.txtCheckNumber, "");
                this.ErrorProvider.SetError(this.dtbCheckDate, "");
                this.ErrorProvider.SetError(this.dtbBirthdate, "");
                this.ErrorProvider.SetError(this.txtDriverLicenseNumber, "");
            }
            else
            {
                string str = (this.txtCheckNumber.Text ?? "").Trim();
                if (str.Length == 0)
                {
                    this.ErrorProvider.SetError(this.txtCheckNumber, "Please input check #.");
                }
                else if (str.Length != 4)
                {
                    this.ErrorProvider.SetError(this.txtCheckNumber, "Check # must me 4 digits number. Pad it with zeros if necessary.");
                }
                else
                {
                    this.ErrorProvider.SetError(this.txtCheckNumber, "");
                }
                if (Functions.GetDateBoxValue(this.dtbCheckDate) is DateTime)
                {
                    if (DateTime.Compare(Conversions.ToDate(Functions.GetDateBoxValue(this.dtbCheckDate)), DateTime.Today.AddMonths(-3)) < 0)
                    {
                        this.ErrorProvider.SetError(this.dtbCheckDate, "It looks like check is very old.");
                    }
                    else
                    {
                        this.ErrorProvider.SetError(this.dtbCheckDate, "");
                    }
                }
                else
                {
                    this.ErrorProvider.SetError(this.dtbCheckDate, "Please input date.");
                }
                if (Functions.GetDateBoxValue(this.dtbBirthdate) is DateTime)
                {
                    if (DateTime.Compare(DateTime.Today.AddYears(-16), Conversions.ToDate(Functions.GetDateBoxValue(this.dtbBirthdate))) < 0)
                    {
                        this.ErrorProvider.SetError(this.dtbBirthdate, "It looks like check owner is very young person.");
                    }
                    else
                    {
                        this.ErrorProvider.SetError(this.dtbBirthdate, "");
                    }
                }
                else
                {
                    this.ErrorProvider.SetError(this.dtbBirthdate, "Please input date");
                }
                (this.txtDriverLicenseNumber.Text ?? "").Trim();
                if (string.IsNullOrWhiteSpace(this.txtDriverLicenseNumber.Text))
                {
                    this.ErrorProvider.SetError(this.txtDriverLicenseNumber, "Please input driver license #.");
                }
                else
                {
                    this.ErrorProvider.SetError(this.txtDriverLicenseNumber, "");
                }
            }
            if (string.Compare(this.cmbPaymentMethod.Text, "Credit Card", true) != 0)
            {
                this.ErrorProvider.SetError(this.txtCreditCardNumber, "");
                this.ErrorProvider.SetError(this.txtMonth, "");
                this.ErrorProvider.SetError(this.txtYear, "");
            }
            else
            {
                if ("" == this.txtCreditCardNumber.Text)
                {
                    this.ErrorProvider.SetError(this.txtCreditCardNumber, "Please input credit card  #.");
                }
                else
                {
                    this.ErrorProvider.SetError(this.txtCreditCardNumber, "");
                }
                try
                {
                    int month = int.Parse(this.txtMonth.Text);
                    if (DateTime.Compare(new DateTime(0x7d0 + int.Parse(this.txtYear.Text), month, 1), DateTime.Today) < 0)
                    {
                        this.ErrorProvider.SetError(this.txtYear, "Expiration month must be less than today.");
                    }
                    else
                    {
                        this.ErrorProvider.SetError(this.txtYear, "");
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    this.ErrorProvider.SetError(this.txtYear, "Month must be value in the range 1 - 12.\r\nYear must be value in the range 00 - 99.");
                    ProjectData.ClearProjectError();
                }
            }
        }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPaymentTotal")]
        private TextBox txtPaymentTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDiscountTotal")]
        private Label lblDiscountTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxTotal")]
        private Label lblTaxTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxTotal")]
        private TextBox txtTaxTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPaymentTotal")]
        private Label lblPaymentTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSubtotal")]
        private Label lblSubtotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSubtotal")]
        private TextBox txtSubtotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDiscountTotal")]
        private TextBox txtDiscountTotal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPaymentMethod")]
        private Label lblPaymentMethod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPaymentMethod")]
        private ComboBox cmbPaymentMethod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtMonth")]
        private TextBox txtMonth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtYear")]
        private TextBox txtYear { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblExpirationDate")]
        private Label lblExpirationDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSlash")]
        private Label lblSlash { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCreditCardNumber")]
        private Label lblCreditCardNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCheckNumber")]
        private Label lblCheckNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDriverLicenseNumber")]
        private TextBox txtDriverLicenseNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBirthDate")]
        private Label lblBirthDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbBirthdate")]
        private UltraDateTimeEditor dtbBirthdate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDriverLicenseNumber")]
        private Label lblDriverLicenseNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlCreditCard")]
        private Panel pnlCreditCard { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlCheck")]
        private Panel pnlCheck { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbCheckDate")]
        private UltraDateTimeEditor dtbCheckDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCheckDate")]
        private Label lblCheckDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ErrorProvider")]
        private System.Windows.Forms.ErrorProvider ErrorProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCheckNumber")]
        private TextBox txtCheckNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCreditCardNumber")]
        private TextBox txtCreditCardNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAmountTendered")]
        private Label lblAmountTendered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblChange")]
        private Label lblChange { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbAmountTendered")]
        private NumericBox nmbAmountTendered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbChange")]
        private NumericBox nmbChange { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolTip1")]
        private ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public Parameters Results
        {
            get
            {
                Parameters parameters = new Parameters();
                if (string.Compare(this.cmbPaymentMethod.Text, "Cash", true) == 0)
                {
                    parameters = Parameters.Cash(this.nmbAmountTendered.AsDouble);
                }
                else if (string.Compare(this.cmbPaymentMethod.Text, "Check", true) == 0)
                {
                    parameters = Parameters.Check(this.nmbAmountTendered.AsDouble, this.txtCheckNumber.Text, Conversions.ToDate(Functions.GetDateBoxValue(this.dtbCheckDate)), Conversions.ToDate(Functions.GetDateBoxValue(this.dtbBirthdate)), this.txtDriverLicenseNumber.Text);
                }
                else
                {
                    if (string.Compare(this.cmbPaymentMethod.Text, "Credit Card", true) != 0)
                    {
                        throw new Exception("Unpredictable case. Please contact developer.");
                    }
                    int month = int.Parse(this.txtMonth.Text);
                    int num2 = int.Parse(this.txtYear.Text);
                    parameters = Parameters.CreditCard(this.nmbAmountTendered.AsDouble, this.txtCreditCardNumber.Text, new DateTime(0x7d0 + num2, month, 1));
                }
                return parameters;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Parameters
        {
            public DMEWorks.Data.PaymentMethod? PaymentMethod;
            public string CheckNumber;
            public DateTime? CheckDate;
            public DateTime? Birthdate;
            public string DriverLicenseNumber;
            public string CreditCardNumber;
            public DateTime? ExpirationMonth;
            public double? AmountTendered;
            public static DialogEndSale.Parameters Cash(double? AmountTendered) => 
                new DialogEndSale.Parameters { 
                    AmountTendered = AmountTendered,
                    PaymentMethod = 0,
                    CheckNumber = "",
                    CheckDate = null,
                    Birthdate = null,
                    DriverLicenseNumber = "",
                    CreditCardNumber = "",
                    ExpirationMonth = null
                };

            public static DialogEndSale.Parameters Check(double? AmountTendered, string CheckNumber, DateTime CheckDate, DateTime Birtdate, string DriverLicenseNumber) => 
                new DialogEndSale.Parameters { 
                    AmountTendered = AmountTendered,
                    PaymentMethod = 1,
                    CheckNumber = CheckNumber,
                    CheckDate = new DateTime?(CheckDate),
                    Birthdate = new DateTime?(Birtdate),
                    DriverLicenseNumber = DriverLicenseNumber,
                    CreditCardNumber = "",
                    ExpirationMonth = null
                };

            public static DialogEndSale.Parameters CreditCard(double? AmountTendered, string CreditCardNumber, DateTime ExpirationMonth) => 
                new DialogEndSale.Parameters { 
                    AmountTendered = AmountTendered,
                    PaymentMethod = 2,
                    CheckNumber = "",
                    CheckDate = null,
                    Birthdate = null,
                    DriverLicenseNumber = "",
                    CreditCardNumber = CreditCardNumber,
                    ExpirationMonth = new DateTime?(ExpirationMonth)
                };
        }
    }
}

