namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [CLSCompliant(true)]
    public class FormSelector : Form
    {
        private Container components;
        private Panel Panel1;
        private Button btnCancel;
        private Button btnOK;
        private SearchableGrid searchableGrid1;

        public FormSelector()
        {
            this.InitializeComponent();
        }

        public void ClearFilter()
        {
            this.searchableGrid1.ClearFilter();
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
            this.Panel1 = new Panel();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.searchableGrid1 = new SearchableGrid();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.Panel1.Controls.Add(this.btnCancel);
            this.Panel1.Controls.Add(this.btnOK);
            this.Panel1.Dock = DockStyle.Bottom;
            this.Panel1.Location = new Point(0, 0xf5);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x1a7, 0x1c);
            this.Panel1.TabIndex = 1;
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x157, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x108, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.searchableGrid1.Dock = DockStyle.Fill;
            this.searchableGrid1.Location = new Point(0, 0);
            this.searchableGrid1.Name = "searchableGrid1";
            this.searchableGrid1.Size = new Size(0x1a7, 0xf5);
            this.searchableGrid1.TabIndex = 0;
            this.searchableGrid1.RowDoubleClick += new EventHandler<GridMouseEventArgs>(this.searchableGrid1_RowDoubleClick);
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x1a7, 0x111);
            base.Controls.Add(this.searchableGrid1);
            base.Controls.Add(this.Panel1);
            base.Name = "FormRowset3";
            this.Text = "FormRowset";
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void searchableGrid1_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        public void SelectFilter()
        {
            this.searchableGrid1.SelectSearch();
        }

        public void SetSelectedRow(Func<DataGridViewRow, bool> selector)
        {
            this.searchableGrid1.SetCurrentRow(selector);
        }

        public SearchableGridAppearance GridAppearance =>
            this.searchableGrid1.Appearance;

        public DataGridViewRow SelectedRow =>
            this.searchableGrid1.CurrentRow;

        public IGridSource DataSource
        {
            get => 
                this.searchableGrid1.GridSource;
            set => 
                this.searchableGrid1.GridSource = value;
        }
    }
}

