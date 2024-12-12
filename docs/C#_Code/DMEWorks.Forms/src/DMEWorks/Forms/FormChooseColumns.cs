namespace DMEWorks.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormChooseColumns : Form
    {
        private IContainer components;
        private Label label1;
        private Label label2;
        private Button btnMoveUp;
        private CheckedListBox chlbColumns;
        private Button btnMoveDown;
        private Button btnShow;
        private Button btnHide;
        private Button btnOK;
        private Button btnCancel;

        public FormChooseColumns(DataGridView grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }
            this.InitializeComponent();
            DataGridViewColumnWrapper[] array = new DataGridViewColumnWrapper[grid.Columns.Count];
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                array[i] = new DataGridViewColumnWrapper(grid.Columns[i]);
            }
            Array.Sort<DataGridViewColumnWrapper>(array, new Comparison<DataGridViewColumnWrapper>(DataGridViewColumnWrapper.Compare));
            this.chlbColumns.BeginUpdate();
            try
            {
                this.chlbColumns.Items.Clear();
                foreach (DataGridViewColumnWrapper wrapper in array)
                {
                    this.chlbColumns.Items.Add(wrapper, wrapper.visible);
                }
            }
            finally
            {
                this.chlbColumns.EndUpdate();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.chlbColumns.SelectedIndex;
            if ((selectedIndex >= 0) && (this.chlbColumns.Items.Count > selectedIndex))
            {
                this.chlbColumns.SetItemChecked(selectedIndex, false);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.chlbColumns.SelectedIndex;
            if ((selectedIndex >= 0) && ((this.chlbColumns.Items.Count - 1) > selectedIndex))
            {
                this.chlbColumns.BeginUpdate();
                try
                {
                    this.chlbColumns.Items.RemoveAt(selectedIndex + 1);
                    this.chlbColumns.Items.Insert(selectedIndex, this.chlbColumns.Items[selectedIndex + 1]);
                    this.chlbColumns.SetItemChecked(selectedIndex, this.chlbColumns.GetItemChecked(selectedIndex + 1));
                }
                finally
                {
                    this.chlbColumns.EndUpdate();
                }
                this.chlbColumns_SelectedIndexChanged(sender, e);
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.chlbColumns.SelectedIndex;
            if ((selectedIndex >= 1) && (this.chlbColumns.Items.Count > selectedIndex))
            {
                this.chlbColumns.BeginUpdate();
                try
                {
                    this.chlbColumns.Items.RemoveAt(selectedIndex - 1);
                    this.chlbColumns.Items.Insert(selectedIndex, this.chlbColumns.Items[selectedIndex - 1]);
                    this.chlbColumns.SetItemChecked(selectedIndex, this.chlbColumns.GetItemChecked(selectedIndex - 1));
                }
                finally
                {
                    this.chlbColumns.EndUpdate();
                }
                this.chlbColumns_SelectedIndexChanged(sender, e);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataGridViewColumnWrapper[] destination = new DataGridViewColumnWrapper[this.chlbColumns.Items.Count];
            this.chlbColumns.Items.CopyTo(destination, 0);
            if (destination.Length != 0)
            {
                for (int i = 0; i < this.chlbColumns.Items.Count; i++)
                {
                    destination[i].column.Visible = this.chlbColumns.GetItemChecked(i);
                    destination[i].column.DisplayIndex = i;
                }
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.chlbColumns.SelectedIndex;
            if ((selectedIndex >= 0) && (this.chlbColumns.Items.Count > selectedIndex))
            {
                this.chlbColumns.SetItemChecked(selectedIndex, true);
            }
        }

        private void chlbColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.chlbColumns.SelectedIndex;
            if ((selectedIndex >= 0) && (this.chlbColumns.Items.Count > selectedIndex))
            {
                this.btnMoveUp.Enabled = 0 < selectedIndex;
                this.btnMoveDown.Enabled = selectedIndex < (this.chlbColumns.Items.Count - 1);
                this.btnHide.Enabled = this.chlbColumns.GetSelected(selectedIndex);
                this.btnShow.Enabled = !this.btnHide.Enabled;
            }
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnMoveUp = new Button();
            this.chlbColumns = new CheckedListBox();
            this.btnMoveDown = new Button();
            this.btnShow = new Button();
            this.btnHide = new Button();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(320, 0x17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the columns that you want to display in the grid";
            this.label2.Location = new Point(8, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 0x15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Columns:";
            this.label2.TextAlign = ContentAlignment.BottomLeft;
            this.btnMoveUp.FlatAppearance.BorderColor = Color.Gray;
            this.btnMoveUp.FlatStyle = FlatStyle.Flat;
            this.btnMoveUp.Location = new Point(0xf8, 0x38);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(80, 0x17);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = "Move &Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.chlbColumns.BorderStyle = BorderStyle.FixedSingle;
            object[] items = new object[] { "First", "Second", "Third", "Fourth", "Fifth", "Sixth" };
            this.chlbColumns.Items.AddRange(items);
            this.chlbColumns.Location = new Point(8, 0x38);
            this.chlbColumns.Name = "chlbColumns";
            this.chlbColumns.Size = new Size(0xe8, 0x11f);
            this.chlbColumns.TabIndex = 2;
            this.chlbColumns.SelectedIndexChanged += new EventHandler(this.chlbColumns_SelectedIndexChanged);
            this.btnMoveDown.FlatAppearance.BorderColor = Color.Gray;
            this.btnMoveDown.FlatStyle = FlatStyle.Flat;
            this.btnMoveDown.Location = new Point(0xf8, 80);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(80, 0x17);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.Text = "Move &Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnShow.FlatAppearance.BorderColor = Color.Gray;
            this.btnShow.FlatStyle = FlatStyle.Flat;
            this.btnShow.Location = new Point(0xf8, 0x68);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new Size(80, 0x17);
            this.btnShow.TabIndex = 5;
            this.btnShow.Text = "&Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new EventHandler(this.btnShow_Click);
            this.btnHide.FlatAppearance.BorderColor = Color.Gray;
            this.btnHide.FlatStyle = FlatStyle.Flat;
            this.btnHide.Location = new Point(0xf8, 0x80);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new Size(80, 0x17);
            this.btnHide.TabIndex = 6;
            this.btnHide.Text = "&Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new EventHandler(this.btnHide_Click);
            this.btnOK.FlatAppearance.BorderColor = Color.Gray;
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.Location = new Point(160, 0x160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(80, 0x17);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.FlatAppearance.BorderColor = Color.Gray;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Location = new Point(0xf8, 0x160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(80, 0x17);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x151, 0x17e);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnHide);
            base.Controls.Add(this.btnShow);
            base.Controls.Add(this.btnMoveDown);
            base.Controls.Add(this.chlbColumns);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormChooseColumns";
            this.Text = "Choose Columns";
            base.ResumeLayout(false);
        }

        private sealed class DataGridViewColumnWrapper
        {
            public readonly DataGridViewColumn column;
            public readonly string headerText;

            public DataGridViewColumnWrapper(DataGridViewColumn column)
            {
                if (column == null)
                {
                    throw new ArgumentNullException("column");
                }
                this.column = column;
                this.displayIndex = column.DisplayIndex;
                this.headerText = column.HeaderText;
                this.visible = column.Visible;
            }

            public static int Compare(FormChooseColumns.DataGridViewColumnWrapper x, FormChooseColumns.DataGridViewColumnWrapper y)
            {
                int num;
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.visible)
                {
                    if (!y.visible)
                    {
                        num = -1;
                    }
                    else
                    {
                        num = Comparer<int>.Default.Compare(x.displayIndex, y.displayIndex);
                        num ??= StringComparer.InvariantCultureIgnoreCase.Compare(x.headerText, y.headerText);
                    }
                }
                else if (y.visible)
                {
                    num = 1;
                }
                else
                {
                    num = StringComparer.InvariantCultureIgnoreCase.Compare(x.headerText, y.headerText);
                    num ??= Comparer<int>.Default.Compare(x.displayIndex, y.displayIndex);
                }
                return num;
            }

            public override string ToString() => 
                this.headerText;

            public int displayIndex { get; set; }

            public bool visible { get; set; }
        }
    }
}

