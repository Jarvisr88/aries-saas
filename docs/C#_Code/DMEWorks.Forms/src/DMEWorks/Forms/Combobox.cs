namespace DMEWorks.Forms
{
    using DMEWorks.Forms.Properties;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    [DefaultEvent("SelectedIndexChanged")]
    public class Combobox : ContainerControl
    {
        private static readonly object EVENT_CLICKEDIT = new object();
        private static readonly object EVENT_CLICKNEW = new object();
        private static readonly object EVENT_CHANGED = new object();
        private static readonly object EVENT_DATASOURCECHANGED = new object();
        private static readonly object EVENT_SELECTEDINDEXCHANGED = new object();
        private static readonly object EVENT_INITDIALOG = new object();
        private static readonly object EVENT_DRAWITEM = new object();
        private bool _allowSelectedIndexChangedEvent;
        private FormSelector _dialog;
        private bool _editButton = true;
        private bool _findButton = true;
        private bool _newButton = true;
        private bool _readonly;
        private Button btnFind;
        private ComboBox cmbInternal;
        private Button btnNew;
        private Button btnEdit;
        private Color _editorBackColor = Color.Empty;

        public event EventHandler Changed
        {
            add
            {
                base.Events.AddHandler(EVENT_CHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_CHANGED, value);
            }
        }

        public event EventHandler ClickEdit
        {
            add
            {
                base.Events.AddHandler(EVENT_CLICKEDIT, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_CLICKEDIT, value);
            }
        }

        public event EventHandler ClickNew
        {
            add
            {
                base.Events.AddHandler(EVENT_CLICKNEW, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_CLICKNEW, value);
            }
        }

        public event EventHandler DataSourceChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_DATASOURCECHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_DATASOURCECHANGED, value);
            }
        }

        public event ComboboxDrawItemEventHandler DrawItem
        {
            add
            {
                base.Events.AddHandler(EVENT_DRAWITEM, value);
                this.ResetDrawing();
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_DRAWITEM, value);
                this.ResetDrawing();
            }
        }

        public event InitDialogEventHandler InitDialog
        {
            add
            {
                base.Events.AddHandler(EVENT_INITDIALOG, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_INITDIALOG, value);
            }
        }

        public event EventHandler SelectedIndexChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_SELECTEDINDEXCHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_SELECTEDINDEXCHANGED, value);
            }
        }

        public Combobox()
        {
            this.InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.OnClickEdit(e);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DataTable dataSource = this.DataSource;
            if (dataSource != null)
            {
                if (this._dialog == null)
                {
                    this._dialog = new FormSelector();
                    this._dialog.StartPosition = FormStartPosition.CenterScreen;
                }
                DataTableGridSource source = this._dialog.DataSource as DataTableGridSource;
                if ((source == null) || !ReferenceEquals(source.Table, dataSource))
                {
                    this._dialog.DataSource = dataSource.ToGridSource();
                    InitDialogEventArgs args = new InitDialogEventArgs(this._dialog.GridAppearance) {
                        Caption = this._dialog.Text
                    };
                    this.OnInitDialog(args);
                    this._dialog.Text = args.Caption;
                }
                this._dialog.ClearFilter();
                DataRow selectedRow = this.SelectedRow;
                this._dialog.SetSelectedRow(r => ReferenceEquals(r.GetDataRow(), selectedRow));
                this._dialog.SelectFilter();
                if (this._dialog.ShowDialog() == DialogResult.OK)
                {
                    this._allowSelectedIndexChangedEvent = true;
                    this.SelectedRow = this._dialog.SelectedRow.GetDataRow();
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.OnClickNew(e);
        }

        private void cmbInternal_Click(object sender, EventArgs e)
        {
            this.OnChanged(e);
            this.OnClick(e);
        }

        private void cmbInternal_DataSourceChanged(object sender, EventArgs e)
        {
            this.OnDataSourceChanged(e);
        }

        private void cmbInternal_DrawItem(object sender, DrawItemEventArgs e)
        {
            object item = (0 <= e.Index) ? this.cmbInternal.Items[e.Index] : null;
            ComboboxDrawItemEventArgs args = new ComboboxDrawItemEventArgs(e, item as DataRowView, this.cmbInternal.GetItemText(item));
            this.OnDrawItem(args);
        }

        private void cmbInternal_DropDown(object sender, EventArgs e)
        {
            this._allowSelectedIndexChangedEvent = true;
        }

        private void cmbInternal_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down)))
            {
                this._allowSelectedIndexChangedEvent = true;
            }
        }

        private void cmbInternal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._allowSelectedIndexChangedEvent)
            {
                this._allowSelectedIndexChangedEvent = false;
                this.OnSelectedIndexChanged(e);
            }
        }

        private void InitializeComponent()
        {
            this.cmbInternal = new ComboBox();
            this.btnFind = new Button();
            this.btnNew = new Button();
            this.btnEdit = new Button();
            base.SuspendLayout();
            this.cmbInternal.DisplayMember = "Name";
            this.cmbInternal.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbInternal.DropDownWidth = 150;
            this.cmbInternal.Location = new Point(0, 0);
            this.cmbInternal.Name = "cmbInternal";
            this.cmbInternal.Size = new Size(0xe8, 0x15);
            this.cmbInternal.TabIndex = 0;
            this.cmbInternal.ValueMember = "ID";
            this.cmbInternal.DrawItem += new DrawItemEventHandler(this.cmbInternal_DrawItem);
            this.cmbInternal.SelectedIndexChanged += new EventHandler(this.cmbInternal_SelectedIndexChanged);
            this.cmbInternal.KeyDown += new KeyEventHandler(this.cmbInternal_KeyDown);
            this.cmbInternal.DropDown += new EventHandler(this.cmbInternal_DropDown);
            this.cmbInternal.Click += new EventHandler(this.cmbInternal_Click);
            this.cmbInternal.DataSourceChanged += new EventHandler(this.cmbInternal_DataSourceChanged);
            this.btnFind.BackColor = SystemColors.Control;
            this.btnFind.FlatAppearance.BorderColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.btnFind.FlatAppearance.MouseOverBackColor = Color.Silver;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Image = Resources.ImageSpyglass;
            this.btnFind.Location = new Point(0xe8, 0);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(0x15, 0x15);
            this.btnFind.TabIndex = 1;
            this.btnFind.TabStop = false;
            this.btnFind.Click += new EventHandler(this.btnFind_Click);
            this.btnNew.BackColor = SystemColors.Control;
            this.btnNew.FlatAppearance.MouseOverBackColor = Color.Silver;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = Resources.ImageNew;
            this.btnNew.Location = new Point(0xfd, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x15, 0x15);
            this.btnNew.TabIndex = 2;
            this.btnNew.TabStop = false;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnEdit.BackColor = SystemColors.Control;
            this.btnEdit.FlatAppearance.MouseOverBackColor = Color.Silver;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Image = Resources.ImageEdit;
            this.btnEdit.Location = new Point(0x112, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(0x15, 0x15);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.TabStop = false;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnFind);
            base.Controls.Add(this.cmbInternal);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.btnEdit);
            base.Name = "Combobox";
            base.Size = new Size(0x127, 0x15);
            base.ResumeLayout(false);
        }

        protected virtual void OnChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_CHANGED];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClickEdit(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_CLICKEDIT];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClickNew(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_CLICKNEW];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_DATASOURCECHANGED];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDrawItem(ComboboxDrawItemEventArgs e)
        {
            ComboboxDrawItemEventHandler handler = (ComboboxDrawItemEventHandler) base.Events[EVENT_DRAWITEM];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnInitDialog(InitDialogEventArgs e)
        {
            InitDialogEventHandler handler = (InitDialogEventHandler) base.Events[EVENT_INITDIALOG];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            base.Height = Math.Max(0x15, this.cmbInternal.Height);
            int width = base.Width;
            if (this.btnEdit.Visible)
            {
                width -= base.Height + 1;
            }
            if (this.btnNew.Visible)
            {
                width -= base.Height + 1;
            }
            if (this.btnFind.Visible)
            {
                width -= base.Height + 1;
            }
            int x = 0;
            this.cmbInternal.Location = new Point(0, 0);
            this.cmbInternal.Width = width;
            this.cmbInternal.DropDownWidth = base.Width;
            x += this.cmbInternal.Width + 1;
            if (this.btnFind.Visible)
            {
                this.btnFind.Location = new Point(x, 0);
                this.btnFind.Size = new Size(base.Height, base.Height);
                x += base.Height + 1;
            }
            if (this.btnNew.Visible)
            {
                this.btnNew.Location = new Point(x, 0);
                this.btnNew.Size = new Size(base.Height, base.Height);
                x += base.Height + 1;
            }
            if (this.btnEdit.Visible)
            {
                this.btnEdit.Location = new Point(x, 0);
                this.btnEdit.Size = new Size(base.Height, base.Height);
            }
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_SELECTEDINDEXCHANGED];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void ResetDrawing()
        {
            ComboboxDrawItemEventHandler handler = (ComboboxDrawItemEventHandler) base.Events[EVENT_DRAWITEM];
            this.cmbInternal.DrawMode = (handler != null) ? DrawMode.OwnerDrawFixed : DrawMode.Normal;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetEditorBackColor()
        {
            this.EditorBackColor = Color.Empty;
        }

        private void ResetState()
        {
            this.btnEdit.Visible = this._editButton;
            this.btnFind.Visible = this._findButton && !this._readonly;
            this.btnNew.Visible = this._newButton && !this._readonly;
            this.cmbInternal.Enabled = !this._readonly;
        }

        public void SelectItem(string criteria)
        {
            this.SelectItem(criteria, "");
        }

        public void SelectItem(string criteria, string sort)
        {
            if (criteria != null)
            {
                DataTable dataSource = this.DataSource;
                if (dataSource != null)
                {
                    try
                    {
                        DataRow[] rowArray = dataSource.Select(criteria, sort);
                        if (rowArray.Length != 0)
                        {
                            this.SelectedRow = rowArray[0];
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool ShouldSerializeEditorBackColor() => 
            !this._editorBackColor.IsEmpty;

        [Category("Appearance"), DefaultValue(true)]
        public bool EditButton
        {
            get => 
                this._editButton;
            set
            {
                this._editButton = value;
                this.ResetState();
            }
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool FindButton
        {
            get => 
                this._findButton;
            set
            {
                this._findButton = value;
                this.ResetState();
            }
        }

        [Category("Appearance"), DefaultValue(true)]
        public bool NewButton
        {
            get => 
                this._newButton;
            set
            {
                this._newButton = value;
                this.ResetState();
            }
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool ReadOnly
        {
            get => 
                this._readonly;
            set
            {
                this._readonly = value;
                this.ResetState();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DataRowView SelectedRowView
        {
            get => 
                this.cmbInternal.SelectedItem as DataRowView;
            set => 
                this.cmbInternal.SelectedItem = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DataRow SelectedRow
        {
            get
            {
                DataRowView selectedRowView = this.SelectedRowView;
                return selectedRowView?.Row;
            }
            set
            {
                if (value != null)
                {
                    ComboBox.ObjectCollection items = this.cmbInternal.Items;
                    int num = 0;
                    int count = items.Count;
                    while (num < count)
                    {
                        DataRowView view = items[num] as DataRowView;
                        if ((view != null) && ReferenceEquals(view.Row, value))
                        {
                            this.cmbInternal.SelectedIndex = num;
                            return;
                        }
                        num++;
                    }
                }
                this.cmbInternal.SelectedIndex = -1;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectedIndex
        {
            get => 
                this.cmbInternal.SelectedIndex;
            set => 
                this.cmbInternal.SelectedIndex = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DataTable DataSource
        {
            get
            {
                DataView dataSource = this.cmbInternal.DataSource as DataView;
                return dataSource?.Table;
            }
            set
            {
                DataTable dataSource = this.DataSource;
                if (!ReferenceEquals(value, dataSource))
                {
                    this.cmbInternal.DataSource = (value != null) ? new DataView(value) : null;
                }
            }
        }

        [Category("Appearance"), DefaultValue("Name")]
        public string DisplayMember
        {
            get => 
                this.cmbInternal.DisplayMember;
            set => 
                this.cmbInternal.DisplayMember = value;
        }

        [Category("Appearance"), DefaultValue("ID")]
        public string ValueMember
        {
            get => 
                this.cmbInternal.ValueMember;
            set => 
                this.cmbInternal.ValueMember = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object SelectedValue
        {
            get => 
                this.cmbInternal.SelectedValue;
            set => 
                this.cmbInternal.SelectedValue = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoScroll
        {
            get => 
                false;
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Size AutoScrollMargin
        {
            get => 
                base.AutoScrollMargin;
            set => 
                base.AutoScrollMargin = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Size AutoScrollMinSize
        {
            get => 
                base.AutoScrollMinSize;
            set => 
                base.AutoScrollMinSize = value;
        }

        [Category("Appearance")]
        public Color EditorBackColor
        {
            get => 
                this.cmbInternal.BackColor;
            set
            {
                this._editorBackColor = value;
                this.cmbInternal.BackColor = value;
            }
        }

        [Category("Appearance"), DefaultValue(2)]
        public System.Windows.Forms.FlatStyle FlatStyle
        {
            get => 
                this.cmbInternal.FlatStyle;
            set => 
                this.cmbInternal.FlatStyle = value;
        }

        public override System.Windows.Forms.ContextMenu ContextMenu
        {
            get => 
                base.ContextMenu;
            set
            {
                base.ContextMenu = value;
                this.cmbInternal.ContextMenu = value;
            }
        }

        public override System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get => 
                base.ContextMenuStrip;
            set
            {
                base.ContextMenuStrip = value;
                this.cmbInternal.ContextMenuStrip = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Focused =>
            this.cmbInternal.Focused;

        public override Color ForeColor
        {
            get => 
                this.cmbInternal.ForeColor;
            set
            {
                base.ForeColor = value;
                this.cmbInternal.ForeColor = value;
            }
        }

        protected override Size DefaultSize =>
            new Size(0x127, 0x15);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override string Text
        {
            get => 
                this.cmbInternal.Text;
            set => 
                this.cmbInternal.Text = value;
        }
    }
}

