namespace DMEWorks.Forms.Printing
{
    using DMEWorks.Printing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class ImageSetupDialog : Form
    {
        private ImagePrintDocument _document;
        private IContainer components;
        private GroupBox gbMargins;
        private GroupBox gbOrientation;
        private RadioButton rbOrientation_Portrait;
        private RadioButton rbOrientation_Landscape;
        private GroupBox gbPaper;
        private ComboBox cmbSource;
        private ComboBox cmbSize;
        private GroupBox gbCentering;
        private GroupBox gbPreview;
        private GroupBox gbScaling;
        private RadioButton rbFitTo;
        private RadioButton rbAdjustTo;
        private NumericUpDown nudRight;
        private Label label4;
        private NumericUpDown nudTop;
        private Label label3;
        private NumericUpDown nudBottom;
        private Label label2;
        private NumericUpDown nudLeft;
        private Label label1;
        private CheckBox chbVertical;
        private CheckBox chbHorizontal;
        private Label lblSource;
        private Label lblSize;
        private Panel pnlPaper;
        private Panel pnlPrintArea;
        private Panel pnlImage;
        private Panel pnlShadow;
        private NumericUpDown numericUpDown5;
        private Label label7;
        private Label label6;
        private NumericUpDown numericUpDown7;
        private NumericUpDown numericUpDown6;
        private Label label5;
        private Button btnOk;
        private Button btnCancel;
        private Button button3;

        public ImageSetupDialog()
        {
            this.InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
        }

        private void chbHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        private void chbVertical_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        private void cmbSource_SelectedValueChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.gbMargins = new GroupBox();
            this.nudRight = new NumericUpDown();
            this.label4 = new Label();
            this.nudTop = new NumericUpDown();
            this.label3 = new Label();
            this.nudBottom = new NumericUpDown();
            this.label2 = new Label();
            this.nudLeft = new NumericUpDown();
            this.label1 = new Label();
            this.gbOrientation = new GroupBox();
            this.rbOrientation_Portrait = new RadioButton();
            this.rbOrientation_Landscape = new RadioButton();
            this.gbPaper = new GroupBox();
            this.lblSource = new Label();
            this.lblSize = new Label();
            this.cmbSource = new ComboBox();
            this.cmbSize = new ComboBox();
            this.gbCentering = new GroupBox();
            this.chbVertical = new CheckBox();
            this.chbHorizontal = new CheckBox();
            this.gbPreview = new GroupBox();
            this.pnlPaper = new Panel();
            this.pnlPrintArea = new Panel();
            this.pnlImage = new Panel();
            this.pnlShadow = new Panel();
            this.gbScaling = new GroupBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.numericUpDown7 = new NumericUpDown();
            this.numericUpDown6 = new NumericUpDown();
            this.label5 = new Label();
            this.numericUpDown5 = new NumericUpDown();
            this.rbFitTo = new RadioButton();
            this.rbAdjustTo = new RadioButton();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.button3 = new Button();
            this.gbMargins.SuspendLayout();
            this.nudRight.BeginInit();
            this.nudTop.BeginInit();
            this.nudBottom.BeginInit();
            this.nudLeft.BeginInit();
            this.gbOrientation.SuspendLayout();
            this.gbPaper.SuspendLayout();
            this.gbCentering.SuspendLayout();
            this.gbPreview.SuspendLayout();
            this.pnlPaper.SuspendLayout();
            this.pnlPrintArea.SuspendLayout();
            this.gbScaling.SuspendLayout();
            this.numericUpDown7.BeginInit();
            this.numericUpDown6.BeginInit();
            this.numericUpDown5.BeginInit();
            base.SuspendLayout();
            this.gbMargins.Controls.Add(this.nudRight);
            this.gbMargins.Controls.Add(this.label4);
            this.gbMargins.Controls.Add(this.nudTop);
            this.gbMargins.Controls.Add(this.label3);
            this.gbMargins.Controls.Add(this.nudBottom);
            this.gbMargins.Controls.Add(this.label2);
            this.gbMargins.Controls.Add(this.nudLeft);
            this.gbMargins.Controls.Add(this.label1);
            this.gbMargins.Location = new Point(0x110, 0x68);
            this.gbMargins.Name = "gbMargins";
            this.gbMargins.Size = new Size(0xf8, 0x58);
            this.gbMargins.TabIndex = 3;
            this.gbMargins.TabStop = false;
            this.gbMargins.Text = "Margins";
            this.nudRight.Location = new Point(0xa8, 0x18);
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new Size(0x38, 20);
            this.nudRight.TabIndex = 3;
            this.nudRight.ValueChanged += new EventHandler(this.nudRight_ValueChanged);
            this.label4.Location = new Point(120, 0x18);
            this.label4.Name = "label4";
            this.label4.Size = new Size(40, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "&Right";
            this.nudTop.Location = new Point(0x38, 0x38);
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new Size(0x38, 20);
            this.nudTop.TabIndex = 5;
            this.nudTop.ValueChanged += new EventHandler(this.nudTop_ValueChanged);
            this.label3.Location = new Point(120, 0x38);
            this.label3.Name = "label3";
            this.label3.Size = new Size(40, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "&Bottom";
            this.nudBottom.Location = new Point(0xa8, 0x38);
            this.nudBottom.Name = "nudBottom";
            this.nudBottom.Size = new Size(0x38, 20);
            this.nudBottom.TabIndex = 7;
            this.nudBottom.ValueChanged += new EventHandler(this.nudBottom_ValueChanged);
            this.label2.Location = new Point(8, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x22, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "&Top";
            this.nudLeft.Location = new Point(0x38, 0x18);
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new Size(0x38, 20);
            this.nudLeft.TabIndex = 1;
            this.nudLeft.ValueChanged += new EventHandler(this.nudLeft_ValueChanged);
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Left";
            this.gbOrientation.Controls.Add(this.rbOrientation_Portrait);
            this.gbOrientation.Controls.Add(this.rbOrientation_Landscape);
            this.gbOrientation.Location = new Point(0xa8, 0x68);
            this.gbOrientation.Name = "gbOrientation";
            this.gbOrientation.Size = new Size(0x61, 0x58);
            this.gbOrientation.TabIndex = 2;
            this.gbOrientation.TabStop = false;
            this.gbOrientation.Text = "Orientation";
            this.rbOrientation_Portrait.Checked = true;
            this.rbOrientation_Portrait.Location = new Point(8, 0x18);
            this.rbOrientation_Portrait.Margin = new Padding(0);
            this.rbOrientation_Portrait.Name = "rbOrientation_Portrait";
            this.rbOrientation_Portrait.Size = new Size(80, 20);
            this.rbOrientation_Portrait.TabIndex = 0;
            this.rbOrientation_Portrait.TabStop = true;
            this.rbOrientation_Portrait.Text = "P&ortrait";
            this.rbOrientation_Portrait.UseVisualStyleBackColor = true;
            this.rbOrientation_Portrait.CheckedChanged += new EventHandler(this.rbOrientation_CheckedChanged);
            this.rbOrientation_Landscape.Location = new Point(8, 0x38);
            this.rbOrientation_Landscape.Margin = new Padding(0);
            this.rbOrientation_Landscape.Name = "rbOrientation_Landscape";
            this.rbOrientation_Landscape.Size = new Size(80, 20);
            this.rbOrientation_Landscape.TabIndex = 1;
            this.rbOrientation_Landscape.Text = "L&andscape";
            this.rbOrientation_Landscape.UseVisualStyleBackColor = true;
            this.rbOrientation_Landscape.CheckedChanged += new EventHandler(this.rbOrientation_CheckedChanged);
            this.gbPaper.Controls.Add(this.lblSource);
            this.gbPaper.Controls.Add(this.lblSize);
            this.gbPaper.Controls.Add(this.cmbSource);
            this.gbPaper.Controls.Add(this.cmbSize);
            this.gbPaper.Location = new Point(0xa8, 8);
            this.gbPaper.Name = "gbPaper";
            this.gbPaper.Size = new Size(0x160, 0x58);
            this.gbPaper.TabIndex = 1;
            this.gbPaper.TabStop = false;
            this.gbPaper.Text = "Paper";
            this.lblSource.Location = new Point(8, 0x38);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new Size(0x40, 20);
            this.lblSource.TabIndex = 2;
            this.lblSource.Text = "&Source";
            this.lblSize.Location = new Point(8, 0x18);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new Size(0x40, 20);
            this.lblSize.TabIndex = 0;
            this.lblSize.Text = "Si&ze";
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Location = new Point(80, 0x38);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new Size(0x108, 0x15);
            this.cmbSource.TabIndex = 3;
            this.cmbSource.SelectedValueChanged += new EventHandler(this.cmbSource_SelectedValueChanged);
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new Point(80, 0x18);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new Size(0x108, 0x15);
            this.cmbSize.TabIndex = 1;
            this.cmbSize.SelectedIndexChanged += new EventHandler(this.cmbSize_SelectedIndexChanged);
            this.gbCentering.Controls.Add(this.chbVertical);
            this.gbCentering.Controls.Add(this.chbHorizontal);
            this.gbCentering.Location = new Point(0xa8, 200);
            this.gbCentering.Name = "gbCentering";
            this.gbCentering.Size = new Size(0x61, 0x58);
            this.gbCentering.TabIndex = 4;
            this.gbCentering.TabStop = false;
            this.gbCentering.Text = "Centering";
            this.chbVertical.Checked = true;
            this.chbVertical.CheckState = CheckState.Checked;
            this.chbVertical.Location = new Point(8, 0x38);
            this.chbVertical.Margin = new Padding(0);
            this.chbVertical.Name = "chbVertical";
            this.chbVertical.Size = new Size(80, 20);
            this.chbVertical.TabIndex = 1;
            this.chbVertical.Text = "&Vertical";
            this.chbVertical.UseVisualStyleBackColor = true;
            this.chbVertical.CheckedChanged += new EventHandler(this.chbVertical_CheckedChanged);
            this.chbHorizontal.Checked = true;
            this.chbHorizontal.CheckState = CheckState.Checked;
            this.chbHorizontal.Location = new Point(8, 0x18);
            this.chbHorizontal.Margin = new Padding(0);
            this.chbHorizontal.Name = "chbHorizontal";
            this.chbHorizontal.Size = new Size(80, 20);
            this.chbHorizontal.TabIndex = 0;
            this.chbHorizontal.Text = "&Horizontal";
            this.chbHorizontal.UseVisualStyleBackColor = true;
            this.chbHorizontal.CheckedChanged += new EventHandler(this.chbHorizontal_CheckedChanged);
            this.gbPreview.BackColor = Color.Transparent;
            this.gbPreview.Controls.Add(this.pnlPaper);
            this.gbPreview.Controls.Add(this.pnlShadow);
            this.gbPreview.Location = new Point(8, 8);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.Size = new Size(0x98, 280);
            this.gbPreview.TabIndex = 0;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "Preview";
            this.pnlPaper.BackColor = Color.White;
            this.pnlPaper.BorderStyle = BorderStyle.FixedSingle;
            this.pnlPaper.Controls.Add(this.pnlPrintArea);
            this.pnlPaper.Location = new Point(0x10, 0x68);
            this.pnlPaper.Margin = new Padding(0);
            this.pnlPaper.Name = "pnlPaper";
            this.pnlPaper.Size = new Size(0x70, 0x60);
            this.pnlPaper.TabIndex = 0;
            this.pnlPrintArea.BackColor = Color.Transparent;
            this.pnlPrintArea.BorderStyle = BorderStyle.FixedSingle;
            this.pnlPrintArea.Controls.Add(this.pnlImage);
            this.pnlPrintArea.Location = new Point(8, 12);
            this.pnlPrintArea.Margin = new Padding(0);
            this.pnlPrintArea.Name = "pnlPrintArea";
            this.pnlPrintArea.Size = new Size(0x58, 0x44);
            this.pnlPrintArea.TabIndex = 0;
            this.pnlImage.BackColor = Color.Silver;
            this.pnlImage.Location = new Point(8, 0x10);
            this.pnlImage.Margin = new Padding(0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new Size(0x48, 0x30);
            this.pnlImage.TabIndex = 0;
            this.pnlShadow.BackColor = Color.LightGray;
            this.pnlShadow.Location = new Point(0x18, 120);
            this.pnlShadow.Margin = new Padding(0);
            this.pnlShadow.Name = "pnlShadow";
            this.pnlShadow.Size = new Size(0x6f, 0x8b);
            this.pnlShadow.TabIndex = 1;
            this.gbScaling.Controls.Add(this.label7);
            this.gbScaling.Controls.Add(this.label6);
            this.gbScaling.Controls.Add(this.numericUpDown7);
            this.gbScaling.Controls.Add(this.numericUpDown6);
            this.gbScaling.Controls.Add(this.label5);
            this.gbScaling.Controls.Add(this.numericUpDown5);
            this.gbScaling.Controls.Add(this.rbFitTo);
            this.gbScaling.Controls.Add(this.rbAdjustTo);
            this.gbScaling.Location = new Point(0x110, 200);
            this.gbScaling.Name = "gbScaling";
            this.gbScaling.Size = new Size(0xf8, 0x58);
            this.gbScaling.TabIndex = 5;
            this.gbScaling.TabStop = false;
            this.gbScaling.Text = "Scaling";
            this.label7.Location = new Point(0xd0, 0x38);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x20, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "page(s)";
            this.label7.TextAlign = ContentAlignment.MiddleCenter;
            this.label6.Location = new Point(0x88, 0x38);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x18, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "by";
            this.label6.TextAlign = ContentAlignment.MiddleCenter;
            this.numericUpDown7.Location = new Point(160, 0x38);
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new Size(0x30, 20);
            this.numericUpDown7.TabIndex = 6;
            int[] bits = new int[4];
            bits[0] = 1;
            this.numericUpDown7.Value = new decimal(bits);
            this.numericUpDown6.Location = new Point(80, 0x38);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new Size(0x38, 20);
            this.numericUpDown6.TabIndex = 4;
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.numericUpDown6.Value = new decimal(numArray2);
            this.label5.Location = new Point(0x90, 0x10);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x48, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "% normal size";
            this.label5.TextAlign = ContentAlignment.MiddleLeft;
            this.numericUpDown5.Location = new Point(80, 0x10);
            int[] numArray3 = new int[4];
            numArray3[0] = 0x2710;
            this.numericUpDown5.Maximum = new decimal(numArray3);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new Size(0x38, 20);
            this.numericUpDown5.TabIndex = 1;
            int[] numArray4 = new int[4];
            numArray4[0] = 100;
            this.numericUpDown5.Value = new decimal(numArray4);
            this.rbFitTo.Location = new Point(8, 0x38);
            this.rbFitTo.Name = "rbFitTo";
            this.rbFitTo.Size = new Size(0x48, 20);
            this.rbFitTo.TabIndex = 3;
            this.rbFitTo.TabStop = true;
            this.rbFitTo.Text = "&Fit to";
            this.rbFitTo.UseVisualStyleBackColor = true;
            this.rbAdjustTo.Location = new Point(8, 0x10);
            this.rbAdjustTo.Name = "rbAdjustTo";
            this.rbAdjustTo.Size = new Size(0x48, 20);
            this.rbAdjustTo.TabIndex = 0;
            this.rbAdjustTo.TabStop = true;
            this.rbAdjustTo.Text = "A&djust to";
            this.rbAdjustTo.UseVisualStyleBackColor = true;
            this.btnOk.Location = new Point(280, 0x130);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x4b, 0x17);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancel.Location = new Point(360, 0x130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.button3.Location = new Point(440, 0x130);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 8;
            this.button3.Text = "&Printer...";
            this.button3.UseVisualStyleBackColor = true;
            base.AcceptButton = this.btnOk;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImageLayout = ImageLayout.None;
            base.ClientSize = new Size(0x210, 0x14f);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.gbScaling);
            base.Controls.Add(this.gbPreview);
            base.Controls.Add(this.gbPaper);
            base.Controls.Add(this.gbCentering);
            base.Controls.Add(this.gbOrientation);
            base.Controls.Add(this.gbMargins);
            base.Name = "ImageSetupDialog";
            this.Text = "Image Print Setup";
            this.gbMargins.ResumeLayout(false);
            this.nudRight.EndInit();
            this.nudTop.EndInit();
            this.nudBottom.EndInit();
            this.nudLeft.EndInit();
            this.gbOrientation.ResumeLayout(false);
            this.gbPaper.ResumeLayout(false);
            this.gbCentering.ResumeLayout(false);
            this.gbPreview.ResumeLayout(false);
            this.pnlPaper.ResumeLayout(false);
            this.pnlPrintArea.ResumeLayout(false);
            this.gbScaling.ResumeLayout(false);
            this.numericUpDown7.EndInit();
            this.numericUpDown6.EndInit();
            this.numericUpDown5.EndInit();
            base.ResumeLayout(false);
        }

        private void nudBottom_ValueChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        private void nudLeft_ValueChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        private void nudRight_ValueChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        private void nudTop_ValueChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Document == null)
            {
                throw new ArgumentException("Cannot show without document");
            }
            PageSettings defaultPageSettings = this.Document.DefaultPageSettings;
            PrinterSettings printerSettings = this.Document.PrinterSettings;
            this.cmbSize.DataSource = printerSettings.PaperSizes;
            this.cmbSize.DisplayMember = "PaperName";
            this.cmbSize.ValueMember = "RawKind";
            this.cmbSize.SelectedValue = defaultPageSettings.PaperSize.RawKind;
            this.cmbSource.DataSource = printerSettings.PaperSources;
            this.cmbSource.DisplayMember = "SourceName";
            this.cmbSource.ValueMember = "RawKind";
            this.cmbSource.SelectedValue = defaultPageSettings.PaperSource.RawKind;
            this.Landscape = defaultPageSettings.Landscape;
            Margins margins = PrinterUnitConvert.Convert(defaultPageSettings.Margins, PrinterUnit.Display, PrinterUnit.ThousandthsOfAnInch);
            this.nudLeft.Value = 10 * margins.Left;
            this.nudRight.Value = 10 * margins.Right;
            this.nudTop.Value = 10 * margins.Top;
            this.nudBottom.Value = 10 * margins.Bottom;
        }

        private void rbOrientation_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdatePreview();
        }

        private void UpdatePreview()
        {
        }

        public ImagePrintDocument Document
        {
            get => 
                this._document;
            set => 
                this._document = value;
        }

        private bool Landscape
        {
            get => 
                this.rbOrientation_Landscape.Checked;
            set
            {
                if (value)
                {
                    this.rbOrientation_Landscape.Checked = true;
                }
                else
                {
                    this.rbOrientation_Portrait.Checked = true;
                }
            }
        }
    }
}

