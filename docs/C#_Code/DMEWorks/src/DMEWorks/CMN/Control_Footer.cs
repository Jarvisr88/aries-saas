namespace DMEWorks.CMN
{
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Control_Footer : Control_CMNBase
    {
        private IContainer components;

        public Control_Footer()
        {
            this.InitializeComponent();
            this.GridDetails.AllowUserToAddRows = false;
            this.GridDetails.AllowUserToDeleteRows = false;
            this.GridDetails.AllowUserToResizeColumns = true;
            this.GridDetails.AllowUserToResizeRows = false;
            this.GridDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.GridDetails.ColumnHeadersHeight = 0x16;
            this.GridDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridDetails.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.GridDetails.RowHeadersWidth = 0x18;
            this.GridDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridDetails.RowTemplate.Height = 0x12;
            this.GridDetails.ReadOnly = true;
            this.GridDetails.MultiSelect = true;
            this.GridDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.Alignment = DataGridViewContentAlignment.MiddleRight;
            style1.NullValue = "";
            style1.Format = "0.00";
            DataGridViewCellStyle style = style1;
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            style4.Alignment = DataGridViewContentAlignment.MiddleRight;
            style4.NullValue = "";
            style4.Format = "0";
            DataGridViewCellStyle style2 = style4;
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style5.NullValue = "";
            style5.Format = "";
            DataGridViewCellStyle style3 = style5;
            Func<string, string, int, DataGridViewCellStyle, DataGridViewTextBoxColumn> func = (_Closure$__.$I107-0 == null) ? (_Closure$__.$I107-0 = new Func<string, string, int, DataGridViewCellStyle, DataGridViewTextBoxColumn>(_Closure$__.$I._Lambda$__107-0)) : _Closure$__.$I107-0;
            this.GridDetails.AutoGenerateColumns = false;
            this.GridDetails.Columns.Clear();
            this.GridDetails.Columns.Add(func("B. Code", "BillingCode", 60, style3));
            this.GridDetails.Columns.Add(func("Inventory Item", "InventoryItemName", 240, style3));
            this.GridDetails.Columns.Add(func("Quantity", "OrderedQuantity", 60, style2));
            this.GridDetails.Columns.Add(func("Billable", "BillablePrice", 60, style));
            this.GridDetails.Columns.Add(func("Allowable", "AllowablePrice", 60, style));
            this.GridDetails.Columns.Add(func("Period", "Period", 60, style3));
            this.GridDetails.Columns.Add(func("Mod1", "Modifier1", 40, style3));
            this.GridDetails.Columns.Add(func("Mod2", "Modifier2", 40, style3));
            this.GridDetails.Columns.Add(func("Mod3", "Modifier3", 40, style3));
            this.GridDetails.Columns.Add(func("Mod4", "Modifier4", 40, style3));
        }

        private void chbOnFile_CheckedChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dtbSignatureDate_ValueChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_Footer));
            this.Panel17 = new Panel();
            this.Label49 = new Label();
            this.dtbSignatureDate = new UltraDateTimeEditor();
            this.Label39 = new Label();
            this.txtSignatureName = new TextBox();
            this.Label38 = new Label();
            this.Label37 = new Label();
            this.Panel16 = new Panel();
            this.chbOnFile = new CheckBox();
            this.Label44 = new Label();
            this.Label48 = new Label();
            this.Panel18 = new Panel();
            this.GridDetails = new DataGridView();
            this.Label53 = new Label();
            this.Label52 = new Label();
            this.Panel19 = new Panel();
            this.Label50 = new Label();
            this.Label51 = new Label();
            this.Panel3 = new Panel();
            this.txtAnsweringEmployer = new TextBox();
            this.Label34 = new Label();
            this.txtAnsweringTitle = new TextBox();
            this.Label35 = new Label();
            this.txtAnsweringName = new TextBox();
            this.Label36 = new Label();
            this.lplCaption = new Label();
            this.Panel17.SuspendLayout();
            this.Panel16.SuspendLayout();
            this.Panel18.SuspendLayout();
            ((ISupportInitialize) this.GridDetails).BeginInit();
            this.Panel19.SuspendLayout();
            this.Panel3.SuspendLayout();
            base.SuspendLayout();
            this.Panel17.BorderStyle = BorderStyle.FixedSingle;
            this.Panel17.Controls.Add(this.Label49);
            this.Panel17.Controls.Add(this.dtbSignatureDate);
            this.Panel17.Controls.Add(this.Label39);
            this.Panel17.Controls.Add(this.txtSignatureName);
            this.Panel17.Controls.Add(this.Label38);
            this.Panel17.Controls.Add(this.Label37);
            this.Panel17.Dock = DockStyle.Bottom;
            this.Panel17.Location = new Point(0, 0x11c);
            this.Panel17.Name = "Panel17";
            this.Panel17.Size = new Size(760, 0x58);
            this.Panel17.TabIndex = 4;
            this.Label49.BackColor = Color.Transparent;
            this.Label49.Location = new Point(400, 0x40);
            this.Label49.Name = "Label49";
            this.Label49.Size = new Size(320, 0x13);
            this.Label49.TabIndex = 5;
            this.Label49.Text = "(SIGNATURE AND DATE STAMPS ARE NOT ACCEPTABLE)";
            this.Label49.TextAlign = ContentAlignment.MiddleLeft;
            this.dtbSignatureDate.BorderStyle = UIElementBorderStyle.Solid;
            this.dtbSignatureDate.Location = new Point(0x128, 0x40);
            this.dtbSignatureDate.Name = "dtbSignatureDate";
            this.dtbSignatureDate.Size = new Size(0x60, 0x13);
            this.dtbSignatureDate.TabIndex = 4;
            this.Label39.BackColor = Color.Transparent;
            this.Label39.Location = new Point(0x100, 0x40);
            this.Label39.Name = "Label39";
            this.Label39.Size = new Size(0x24, 0x13);
            this.Label39.TabIndex = 3;
            this.Label39.Text = "DATE";
            this.Label39.TextAlign = ContentAlignment.MiddleRight;
            this.txtSignatureName.BorderStyle = BorderStyle.FixedSingle;
            this.txtSignatureName.Location = new Point(0x98, 0x40);
            this.txtSignatureName.Name = "txtSignatureName";
            this.txtSignatureName.Size = new Size(100, 0x13);
            this.txtSignatureName.TabIndex = 2;
            this.Label38.BackColor = Color.Transparent;
            this.Label38.Location = new Point(4, 0x40);
            this.Label38.Name = "Label38";
            this.Label38.Size = new Size(0x90, 0x13);
            this.Label38.TabIndex = 1;
            this.Label38.Text = "PHYSICIAN'S SIGNATURE";
            this.Label38.TextAlign = ContentAlignment.MiddleRight;
            this.Label37.BackColor = Color.Transparent;
            this.Label37.Dock = DockStyle.Top;
            this.Label37.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label37.Location = new Point(0, 0);
            this.Label37.Name = "Label37";
            this.Label37.Size = new Size(0x2f6, 60);
            this.Label37.TabIndex = 0;
            this.Label37.Text = manager.GetString("Label37.Text");
            this.Label37.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel16.BorderStyle = BorderStyle.FixedSingle;
            this.Panel16.Controls.Add(this.chbOnFile);
            this.Panel16.Controls.Add(this.Label44);
            this.Panel16.Controls.Add(this.Label48);
            this.Panel16.Dock = DockStyle.Bottom;
            this.Panel16.Location = new Point(0, 260);
            this.Panel16.Name = "Panel16";
            this.Panel16.Size = new Size(760, 0x18);
            this.Panel16.TabIndex = 3;
            this.chbOnFile.CheckAlign = ContentAlignment.MiddleRight;
            this.chbOnFile.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.chbOnFile.Location = new Point(0x1ec, 0);
            this.chbOnFile.Name = "chbOnFile";
            this.chbOnFile.Size = new Size(80, 0x12);
            this.chbOnFile.TabIndex = 2;
            this.chbOnFile.Text = "On File";
            this.Label44.BackColor = Color.Transparent;
            this.Label44.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label44.Location = new Point(0xb8, 0);
            this.Label44.Name = "Label44";
            this.Label44.Size = new Size(0xec, 0x12);
            this.Label44.TabIndex = 1;
            this.Label44.Text = "Physician Attestation and Signature/Date";
            this.Label48.BackColor = Color.Transparent;
            this.Label48.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label48.Location = new Point(0, 0);
            this.Label48.Name = "Label48";
            this.Label48.Size = new Size(0x5c, 0x12);
            this.Label48.TabIndex = 0;
            this.Label48.Text = "SECTION D";
            this.Label48.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel18.BorderStyle = BorderStyle.FixedSingle;
            this.Panel18.Controls.Add(this.GridDetails);
            this.Panel18.Controls.Add(this.Label53);
            this.Panel18.Controls.Add(this.Label52);
            this.Panel18.Dock = DockStyle.Fill;
            this.Panel18.Location = new Point(0, 0x44);
            this.Panel18.Name = "Panel18";
            this.Panel18.Size = new Size(760, 0xc0);
            this.Panel18.TabIndex = 2;
            this.GridDetails.Dock = DockStyle.Fill;
            this.GridDetails.Location = new Point(0, 40);
            this.GridDetails.Name = "GridDetails";
            this.GridDetails.Size = new Size(0x2f6, 150);
            this.GridDetails.TabIndex = 2;
            this.Label53.BackColor = Color.Transparent;
            this.Label53.Dock = DockStyle.Top;
            this.Label53.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label53.Location = new Point(0, 20);
            this.Label53.Name = "Label53";
            this.Label53.Size = new Size(0x2f6, 20);
            this.Label53.TabIndex = 1;
            this.Label53.Text = "Allowance for each item, accessory, and option. (See Instructions On Back)";
            this.Label53.TextAlign = ContentAlignment.MiddleLeft;
            this.Label52.BackColor = Color.Transparent;
            this.Label52.Dock = DockStyle.Top;
            this.Label52.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label52.Location = new Point(0, 0);
            this.Label52.Name = "Label52";
            this.Label52.Size = new Size(0x2f6, 20);
            this.Label52.TabIndex = 0;
            this.Label52.Text = "(1)  Narrative description of all items, accessories and options ordered; (2)  Supplier's charge; and (3)  Medicare Fee Schedule";
            this.Label52.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel19.BorderStyle = BorderStyle.FixedSingle;
            this.Panel19.Controls.Add(this.Label50);
            this.Panel19.Controls.Add(this.Label51);
            this.Panel19.Dock = DockStyle.Top;
            this.Panel19.Location = new Point(0, 0x2c);
            this.Panel19.Name = "Panel19";
            this.Panel19.Size = new Size(760, 0x18);
            this.Panel19.TabIndex = 1;
            this.Label50.BackColor = Color.Transparent;
            this.Label50.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label50.Location = new Point(0xb8, 0);
            this.Label50.Name = "Label50";
            this.Label50.Size = new Size(0x138, 0x12);
            this.Label50.TabIndex = 1;
            this.Label50.Text = "Narrative Description of Equipment and Cost";
            this.Label51.BackColor = Color.Transparent;
            this.Label51.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.Label51.Location = new Point(0, 0);
            this.Label51.Name = "Label51";
            this.Label51.Size = new Size(0x5c, 0x12);
            this.Label51.TabIndex = 0;
            this.Label51.Text = "SECTION C";
            this.Label51.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel3.BorderStyle = BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.txtAnsweringEmployer);
            this.Panel3.Controls.Add(this.Label34);
            this.Panel3.Controls.Add(this.txtAnsweringTitle);
            this.Panel3.Controls.Add(this.Label35);
            this.Panel3.Controls.Add(this.txtAnsweringName);
            this.Panel3.Controls.Add(this.Label36);
            this.Panel3.Controls.Add(this.lplCaption);
            this.Panel3.Dock = DockStyle.Top;
            this.Panel3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Panel3.Location = new Point(0, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new Size(760, 0x2c);
            this.Panel3.TabIndex = 0;
            this.txtAnsweringEmployer.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnsweringEmployer.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnsweringEmployer.Location = new Point(0x1a0, 20);
            this.txtAnsweringEmployer.Name = "txtAnsweringEmployer";
            this.txtAnsweringEmployer.Size = new Size(0xa8, 20);
            this.txtAnsweringEmployer.TabIndex = 6;
            this.Label34.BackColor = Color.Transparent;
            this.Label34.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label34.Location = new Point(0x164, 20);
            this.Label34.Name = "Label34";
            this.Label34.Size = new Size(0x34, 20);
            this.Label34.TabIndex = 5;
            this.Label34.Text = "Employer";
            this.Label34.TextAlign = ContentAlignment.MiddleLeft;
            this.txtAnsweringTitle.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnsweringTitle.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnsweringTitle.Location = new Point(220, 20);
            this.txtAnsweringTitle.Name = "txtAnsweringTitle";
            this.txtAnsweringTitle.Size = new Size(0x84, 20);
            this.txtAnsweringTitle.TabIndex = 4;
            this.Label35.BackColor = Color.Transparent;
            this.Label35.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label35.Location = new Point(0xbc, 20);
            this.Label35.Name = "Label35";
            this.Label35.Size = new Size(0x1c, 20);
            this.Label35.TabIndex = 3;
            this.Label35.Text = "Title";
            this.Label35.TextAlign = ContentAlignment.MiddleLeft;
            this.txtAnsweringName.BorderStyle = BorderStyle.FixedSingle;
            this.txtAnsweringName.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.txtAnsweringName.Location = new Point(0x2c, 20);
            this.txtAnsweringName.Name = "txtAnsweringName";
            this.txtAnsweringName.Size = new Size(140, 20);
            this.txtAnsweringName.TabIndex = 2;
            this.Label36.BackColor = Color.Transparent;
            this.Label36.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.Label36.Location = new Point(4, 20);
            this.Label36.Name = "Label36";
            this.Label36.Size = new Size(0x24, 20);
            this.Label36.TabIndex = 1;
            this.Label36.Text = "Name";
            this.Label36.TextAlign = ContentAlignment.MiddleLeft;
            this.lplCaption.BackColor = Color.Transparent;
            this.lplCaption.Dock = DockStyle.Top;
            this.lplCaption.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lplCaption.Location = new Point(0, 0);
            this.lplCaption.Name = "lplCaption";
            this.lplCaption.Size = new Size(0x2f6, 0x12);
            this.lplCaption.TabIndex = 0;
            this.lplCaption.Text = "NAME OF PERSON ANSWERING SECTION B QUESTIONS, IF OTHER THAN PHYSICIAN (Please Print):";
            this.lplCaption.TextAlign = ContentAlignment.MiddleLeft;
            base.Controls.Add(this.Panel18);
            base.Controls.Add(this.Panel16);
            base.Controls.Add(this.Panel17);
            base.Controls.Add(this.Panel19);
            base.Controls.Add(this.Panel3);
            base.Name = "Control_Footer";
            base.Size = new Size(760, 0x174);
            this.Panel17.ResumeLayout(false);
            this.Panel16.ResumeLayout(false);
            this.Panel18.ResumeLayout(false);
            ((ISupportInitialize) this.GridDetails).EndInit();
            this.Panel19.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void txtAnsweringEmployer_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnsweringName_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtAnsweringTitle_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        private void txtSignatureName_TextChanged(object sender, EventArgs e)
        {
            base.OnChanged();
        }

        [field: AccessedThroughProperty("Panel17")]
        public virtual Panel Panel17 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label49")]
        public virtual Label Label49 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbSignatureDate")]
        public virtual UltraDateTimeEditor dtbSignatureDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label39")]
        public virtual Label Label39 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSignatureName")]
        public virtual TextBox txtSignatureName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label38")]
        public virtual Label Label38 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label37")]
        public virtual Label Label37 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel16")]
        public virtual Panel Panel16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label44")]
        public virtual Label Label44 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label48")]
        public virtual Label Label48 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel18")]
        public virtual Panel Panel18 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("GridDetails")]
        public virtual DataGridView GridDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label53")]
        public virtual Label Label53 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label52")]
        public virtual Label Label52 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel19")]
        public virtual Panel Panel19 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label50")]
        public virtual Label Label50 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label51")]
        public virtual Label Label51 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel3")]
        public virtual Panel Panel3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnsweringEmployer")]
        public virtual TextBox txtAnsweringEmployer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label34")]
        public virtual Label Label34 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnsweringTitle")]
        public virtual TextBox txtAnsweringTitle { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label35")]
        public virtual Label Label35 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAnsweringName")]
        public virtual TextBox txtAnsweringName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label36")]
        public virtual Label Label36 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lplCaption")]
        public virtual Label lplCaption { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbOnFile")]
        public virtual CheckBox chbOnFile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public override FormMirHelper MirHelper
        {
            get
            {
                if (base.F_MirHelper == null)
                {
                    base.F_MirHelper = new FormMirHelper();
                    base.F_MirHelper.Add("Signature_Date", this.dtbSignatureDate, "Signature Date is required field");
                    base.F_MirHelper.Add("Signature_Name", this.txtSignatureName, "Signature Name is required field");
                }
                return base.F_MirHelper;
            }
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly Control_Footer._Closure$__ $I = new Control_Footer._Closure$__();
            public static Func<string, string, int, DataGridViewCellStyle, DataGridViewTextBoxColumn> $I107-0;

            internal DataGridViewTextBoxColumn _Lambda$__107-0(string HeaderText, string DataPropertyName, int Width, DataGridViewCellStyle DefaultCellStyle)
            {
                DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                column1.DataPropertyName = DataPropertyName;
                column1.DefaultCellStyle = DefaultCellStyle;
                column1.HeaderText = HeaderText;
                column1.ReadOnly = true;
                column1.Width = Width;
                return column1;
            }
        }
    }
}

