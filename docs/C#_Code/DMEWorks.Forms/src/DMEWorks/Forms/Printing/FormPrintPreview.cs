namespace DMEWorks.Forms.Printing
{
    using DMEWorks.Forms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class FormPrintPreview : DmeForm
    {
        private const string ZoomAuto = "Auto";
        private const string Zoom500 = "500%";
        private const string Zoom200 = "200%";
        private const string Zoom150 = "150%";
        private const string Zoom100 = "100%";
        private const string Zoom75 = "75%";
        private const string Zoom50 = "50%";
        private const string Zoom25 = "25%";
        private const string Zoom10 = "10%";
        private const string View1x1 = "1 x 1 page";
        private const string View2x1 = "2 x 1 pages";
        private const string View3x1 = "3 x 1 pages";
        private const string View1x2 = "1 x 2 pages";
        private const string View2x2 = "2 x 2 pages";
        private const string View3x2 = "3 x 2 pages";
        private IContainer components;
        private PrintPreviewControl viewer;
        private ToolStripTextBox tstStartPage;
        private ToolStrip ToolStrip2;
        private ToolStripButton tsbPrint;
        private ToolStripSeparator ToolStripSeparator4;
        private ToolStripSeparator ToolStripSeparator5;
        private ToolStripButton tsbPageSetup;
        private ToolStripComboBox tscZoom;
        private ToolStripButton tsbMoveFirst;
        private ToolStripButton tsbMovePrev;
        private ToolStripButton tsbMoveNext;
        private ToolStripButton tsbMoveLast;
        private ToolStripComboBox tscView;
        private ToolStripSeparator toolStripSeparator1;

        public FormPrintPreview()
        {
            this.InitializeComponent();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormPrintPreview));
            this.viewer = new PrintPreviewControl();
            this.tsbMoveFirst = new ToolStripButton();
            this.tsbMovePrev = new ToolStripButton();
            this.tstStartPage = new ToolStripTextBox();
            this.tsbMoveNext = new ToolStripButton();
            this.tsbMoveLast = new ToolStripButton();
            this.ToolStrip2 = new ToolStrip();
            this.tsbPrint = new ToolStripButton();
            this.tsbPageSetup = new ToolStripButton();
            this.ToolStripSeparator4 = new ToolStripSeparator();
            this.tscView = new ToolStripComboBox();
            this.ToolStripSeparator5 = new ToolStripSeparator();
            this.tscZoom = new ToolStripComboBox();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.ToolStrip2.SuspendLayout();
            base.SuspendLayout();
            this.viewer.Dock = DockStyle.Fill;
            this.viewer.Location = new Point(0, 0x19);
            this.viewer.Name = "viewer";
            this.viewer.Size = new Size(0x278, 0x1ac);
            this.viewer.TabIndex = 0;
            this.tsbMoveFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMoveFirst.Image = (Image) manager.GetObject("tsbMoveFirst.Image");
            this.tsbMoveFirst.ImageTransparentColor = Color.Magenta;
            this.tsbMoveFirst.Name = "tsbMoveFirst";
            this.tsbMoveFirst.Size = new Size(0x17, 0x16);
            this.tsbMoveFirst.ToolTipText = "First page";
            this.tsbMoveFirst.Click += new EventHandler(this.tsbMoveFirst_Click);
            this.tsbMovePrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMovePrev.Image = (Image) manager.GetObject("tsbMovePrev.Image");
            this.tsbMovePrev.ImageTransparentColor = Color.Magenta;
            this.tsbMovePrev.Name = "tsbMovePrev";
            this.tsbMovePrev.Size = new Size(0x17, 0x16);
            this.tsbMovePrev.ToolTipText = "Previous page";
            this.tsbMovePrev.Click += new EventHandler(this.tsbMovePrev_Click);
            this.tstStartPage.Name = "tstStartPage";
            this.tstStartPage.Size = new Size(40, 0x19);
            this.tstStartPage.TextChanged += new EventHandler(this.tstStartPage_TextChanged);
            this.tsbMoveNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMoveNext.Image = (Image) manager.GetObject("tsbMoveNext.Image");
            this.tsbMoveNext.ImageTransparentColor = Color.Magenta;
            this.tsbMoveNext.Name = "tsbMoveNext";
            this.tsbMoveNext.Size = new Size(0x17, 0x16);
            this.tsbMoveNext.ToolTipText = "Next page";
            this.tsbMoveNext.Click += new EventHandler(this.tsbMoveNext_Click);
            this.tsbMoveLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMoveLast.Image = (Image) manager.GetObject("tsbMoveLast.Image");
            this.tsbMoveLast.ImageTransparentColor = Color.Magenta;
            this.tsbMoveLast.Name = "tsbMoveLast";
            this.tsbMoveLast.Size = new Size(0x17, 0x16);
            this.tsbMoveLast.ToolTipText = "Last page";
            this.tsbMoveLast.Click += new EventHandler(this.tsbMoveLast_Click);
            ToolStripItem[] toolStripItems = new ToolStripItem[12];
            toolStripItems[0] = this.tsbPrint;
            toolStripItems[1] = this.tsbPageSetup;
            toolStripItems[2] = this.ToolStripSeparator4;
            toolStripItems[3] = this.tscView;
            toolStripItems[4] = this.ToolStripSeparator5;
            toolStripItems[5] = this.tscZoom;
            toolStripItems[6] = this.toolStripSeparator1;
            toolStripItems[7] = this.tsbMoveFirst;
            toolStripItems[8] = this.tsbMovePrev;
            toolStripItems[9] = this.tstStartPage;
            toolStripItems[10] = this.tsbMoveNext;
            toolStripItems[11] = this.tsbMoveLast;
            this.ToolStrip2.Items.AddRange(toolStripItems);
            this.ToolStrip2.Location = new Point(0, 0);
            this.ToolStrip2.Name = "ToolStrip2";
            this.ToolStrip2.Size = new Size(0x278, 0x19);
            this.ToolStrip2.TabIndex = 0;
            this.tsbPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = (Image) manager.GetObject("tsbPrint.Image");
            this.tsbPrint.ImageTransparentColor = Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new Size(0x17, 0x16);
            this.tsbPrint.ToolTipText = "Print";
            this.tsbPrint.Click += new EventHandler(this.tsbPrint_Click);
            this.tsbPageSetup.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbPageSetup.Image = (Image) manager.GetObject("tsbPageSetup.Image");
            this.tsbPageSetup.ImageTransparentColor = Color.Magenta;
            this.tsbPageSetup.Name = "tsbPageSetup";
            this.tsbPageSetup.Size = new Size(0x17, 0x16);
            this.tsbPageSetup.ToolTipText = "Page setup";
            this.tsbPageSetup.Click += new EventHandler(this.tsbPageSetup_Click);
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new Size(6, 0x19);
            this.tscView.DropDownStyle = ComboBoxStyle.DropDownList;
            object[] items = new object[] { "1 x 1 page", "2 x 1 pages", "3 x 1 pages", "1 x 2 pages", "2 x 2 pages", "3 x 2 pages" };
            this.tscView.Items.AddRange(items);
            this.tscView.Name = "tscView";
            this.tscView.Size = new Size(0x79, 0x19);
            this.tscView.ToolTipText = "View";
            this.tscView.SelectedIndexChanged += new EventHandler(this.tscView_SelectedIndexChanged);
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new Size(6, 0x19);
            this.tscZoom.DropDownStyle = ComboBoxStyle.DropDownList;
            object[] objArray2 = new object[9];
            objArray2[0] = "Auto";
            objArray2[1] = "500%";
            objArray2[2] = "200%";
            objArray2[3] = "150%";
            objArray2[4] = "100%";
            objArray2[5] = "75%";
            objArray2[6] = "50%";
            objArray2[7] = "25%";
            objArray2[8] = "10%";
            this.tscZoom.Items.AddRange(objArray2);
            this.tscZoom.Name = "tscZoom";
            this.tscZoom.Size = new Size(0x79, 0x19);
            this.tscZoom.ToolTipText = "Zoom";
            this.tscZoom.SelectedIndexChanged += new EventHandler(this.tscZoom_SelectedIndexChanged);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.viewer);
            base.Controls.Add(this.ToolStrip2);
            base.Name = "FormPrintPreview";
            this.Text = "Print Preview";
            this.ToolStrip2.ResumeLayout(false);
            this.ToolStrip2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.tscZoom.Items.Clear();
                string[] textArray1 = new string[9];
                textArray1[0] = "Auto";
                textArray1[1] = "500%";
                textArray1[2] = "200%";
                textArray1[3] = "150%";
                textArray1[4] = "100%";
                textArray1[5] = "75%";
                textArray1[6] = "50%";
                textArray1[7] = "25%";
                textArray1[8] = "10%";
                object[] items = textArray1;
                this.tscZoom.Items.AddRange(items);
                this.tscZoom.SelectedItem = "Auto";
            }
            catch (Exception)
            {
            }
            try
            {
                this.tscView.Items.Clear();
                string[] items = new string[] { "1 x 1 page", "2 x 1 pages", "3 x 1 pages", "1 x 2 pages", "2 x 2 pages", "3 x 2 pages" };
                this.tscView.Items.AddRange(items);
                this.tscView.SelectedItem = "1 x 1 page";
            }
            catch (Exception)
            {
            }
            this.ShowStartPage();
        }

        private void ShowStartPage()
        {
            this.tstStartPage.Text = (this.viewer.StartPage + 1).ToString();
        }

        private void tsbMoveFirst_Click(object sender, EventArgs e)
        {
            if (this.viewer.Document != null)
            {
                this.viewer.StartPage = 0;
                this.ShowStartPage();
            }
        }

        private void tsbMoveLast_Click(object sender, EventArgs e)
        {
            if (this.viewer.Document != null)
            {
                this.viewer.StartPage = 0x3e8;
                this.ShowStartPage();
            }
        }

        private void tsbMoveNext_Click(object sender, EventArgs e)
        {
            if (this.viewer.Document != null)
            {
                this.viewer.StartPage++;
                this.ShowStartPage();
            }
        }

        private void tsbMovePrev_Click(object sender, EventArgs e)
        {
            if (this.viewer.Document != null)
            {
                this.viewer.StartPage = Math.Max(0, this.viewer.StartPage - 1);
                this.ShowStartPage();
            }
        }

        private void tsbPageSetup_Click(object sender, EventArgs e)
        {
            using (PageSetupDialog dialog = new PageSetupDialog())
            {
                dialog.Document = this.Document;
                dialog.ShowDialog();
            }
            this.viewer.InvalidatePreview();
            this.ShowStartPage();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog dialog = new PrintDialog())
            {
                dialog.Document = this.Document;
                dialog.AllowCurrentPage = true;
                dialog.AllowPrintToFile = true;
                dialog.AllowSelection = false;
                dialog.AllowSomePages = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Document.Print();
                }
            }
        }

        private void tscView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.viewer.Document != null)
            {
                object selectedItem = this.tscView.SelectedItem;
                if (selectedItem == "1 x 1 page")
                {
                    this.viewer.Columns = 1;
                    this.viewer.Rows = 1;
                }
                else if (selectedItem == "2 x 1 pages")
                {
                    this.viewer.Columns = 2;
                    this.viewer.Rows = 1;
                }
                else if (selectedItem == "3 x 1 pages")
                {
                    this.viewer.Columns = 3;
                    this.viewer.Rows = 1;
                }
                else if (selectedItem == "1 x 2 pages")
                {
                    this.viewer.Columns = 1;
                    this.viewer.Rows = 2;
                }
                else if (selectedItem == "2 x 2 pages")
                {
                    this.viewer.Columns = 2;
                    this.viewer.Rows = 2;
                }
                else if (selectedItem == "3 x 2 pages")
                {
                    this.viewer.Columns = 3;
                    this.viewer.Rows = 2;
                }
            }
        }

        private void tscZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedItem = this.tscZoom.SelectedItem;
            if (selectedItem == "Auto")
            {
                this.viewer.AutoZoom = true;
            }
            else if (selectedItem == "500%")
            {
                this.viewer.Zoom = 5.0;
            }
            else if (selectedItem == "200%")
            {
                this.viewer.Zoom = 2.0;
            }
            else if (selectedItem == "150%")
            {
                this.viewer.Zoom = 1.5;
            }
            else if (selectedItem == "100%")
            {
                this.viewer.Zoom = 1.0;
            }
            else if (selectedItem == "75%")
            {
                this.viewer.Zoom = 0.75;
            }
            else if (selectedItem == "50%")
            {
                this.viewer.Zoom = 0.5;
            }
            else if (selectedItem == "25%")
            {
                this.viewer.Zoom = 0.25;
            }
            else if (selectedItem == "10%")
            {
                this.viewer.Zoom = 0.1;
            }
        }

        private void tstStartPage_TextChanged(object sender, EventArgs e)
        {
            PrintDocument document = this.viewer.Document;
        }

        public PrintDocument Document
        {
            get => 
                this.viewer.Document;
            set => 
                this.viewer.Document = value;
        }
    }
}

