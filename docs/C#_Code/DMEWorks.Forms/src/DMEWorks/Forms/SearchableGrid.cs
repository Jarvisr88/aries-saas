namespace DMEWorks.Forms
{
    using DMEWorks.Forms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SearchableGrid : GridBase
    {
        private readonly SearchableGridAppearance m_appearance;
        private readonly Queue<DateTime> m_queue;
        private IContainer components;
        private Label lblFilter;
        private Panel pnlFilter;
        private TextBox txtFilter;
        private Timer timer;
        private Button btnColumns;

        public SearchableGrid()
        {
            this.InitializeComponent();
            this.m_appearance = new SearchableGridAppearance(this);
            this.m_queue = new Queue<DateTime>();
        }

        private void btnColumns_Click(object sender, EventArgs e)
        {
            using (FormChooseColumns columns = new FormChooseColumns(base.GetGrid()))
            {
                columns.StartPosition = FormStartPosition.CenterScreen;
                columns.ShowDialog();
            }
        }

        public void ClearFilter()
        {
            this.txtFilter.Text = "";
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
            this.components = new Container();
            this.lblFilter = new Label();
            this.pnlFilter = new Panel();
            this.txtFilter = new TextBox();
            this.timer = new Timer(this.components);
            this.btnColumns = new Button();
            this.pnlFilter.SuspendLayout();
            base.SuspendLayout();
            base.pnlGrid.Location = new Point(0, 0x16);
            base.pnlGrid.Size = new Size(0x220, 0x11a);
            this.lblFilter.BorderStyle = BorderStyle.FixedSingle;
            this.lblFilter.Location = new Point(0, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new Size(40, 20);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter :";
            this.lblFilter.TextAlign = ContentAlignment.MiddleLeft;
            this.pnlFilter.Controls.Add(this.btnColumns);
            this.pnlFilter.Controls.Add(this.lblFilter);
            this.pnlFilter.Controls.Add(this.txtFilter);
            this.pnlFilter.Dock = DockStyle.Top;
            this.pnlFilter.Location = new Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new Size(0x220, 0x16);
            this.pnlFilter.TabIndex = 0;
            this.pnlFilter.Layout += new LayoutEventHandler(this.pnlFilter_Layout);
            this.txtFilter.BorderStyle = BorderStyle.FixedSingle;
            this.txtFilter.Location = new Point(0x2a, 0);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new Size(0x1dc, 20);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.TextChanged += new EventHandler(this.txtFilter_TextChanged);
            this.timer.Interval = 50;
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.btnColumns.FlatStyle = FlatStyle.Flat;
            this.btnColumns.Image = Resources.ImageColumns;
            this.btnColumns.Location = new Point(520, 0);
            this.btnColumns.Margin = new Padding(0);
            this.btnColumns.Name = "btnColumns";
            this.btnColumns.Size = new Size(0x18, 20);
            this.btnColumns.TabIndex = 2;
            this.btnColumns.TabStop = false;
            this.btnColumns.UseVisualStyleBackColor = true;
            this.btnColumns.Click += new EventHandler(this.btnColumns_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.pnlFilter);
            base.Name = "SearchableGrid";
            base.Controls.SetChildIndex(this.pnlFilter, 0);
            base.Controls.SetChildIndex(base.pnlGrid, 0);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            base.ResumeLayout(false);
        }

        private void pnlFilter_Layout(object sender, LayoutEventArgs e)
        {
            this.lblFilter.Top = 0;
            this.txtFilter.Top = 0;
            this.btnColumns.Top = 0;
            this.lblFilter.Left = 0;
            this.btnColumns.Left = this.pnlFilter.Width - this.btnColumns.Width;
            this.txtFilter.Left = (this.lblFilter.Left + this.lblFilter.Width) + 2;
            if (this.btnColumns.Visible)
            {
                this.txtFilter.Width = Math.Max(20, (this.btnColumns.Left - this.txtFilter.Left) - 2);
            }
            else
            {
                this.txtFilter.Width = Math.Max(20, this.pnlFilter.Width - this.txtFilter.Left);
            }
        }

        public void SelectSearch()
        {
            this.txtFilter.Select();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.m_queue.Count != 0)
            {
                while ((0 < this.m_queue.Count) && (this.m_queue.Peek() < DateTime.Now))
                {
                    this.m_queue.Dequeue();
                }
                if (this.m_queue.Count == 0)
                {
                    base.FilterTextCore = this.txtFilter.Text;
                    this.timer.Enabled = false;
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.m_queue.Enqueue(DateTime.Now.AddMilliseconds(400.0));
            this.timer.Enabled = true;
        }

        internal bool FilterVisible
        {
            get => 
                this.pnlFilter.Visible;
            set => 
                this.pnlFilter.Visible = value;
        }

        internal bool ManageColumnsVisible
        {
            get => 
                this.btnColumns.Visible;
            set => 
                this.btnColumns.Visible = value;
        }

        [Category("Appearance")]
        public SearchableGridAppearance Appearance =>
            this.m_appearance;
    }
}

