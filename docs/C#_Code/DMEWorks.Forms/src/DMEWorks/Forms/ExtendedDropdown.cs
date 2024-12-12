namespace DMEWorks.Forms
{
    using DMEWorks.Forms.Properties;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    [DefaultEvent("SelectedIndexChanged")]
    public class ExtendedDropdown : ContainerControl
    {
        private static readonly object EVENT_CLICKEDIT = new object();
        private static readonly object EVENT_CLICKNEW = new object();
        private static readonly object EVENT_DATASOURCECHANGED = new object();
        private static readonly object EVENT_INITDIALOG = new object();
        private static readonly object EVENT_TEXTMEMBERCHANGED = new object();
        private FormSelector _dialog;
        private bool _editButton = true;
        private bool _findButton = true;
        private bool _newButton = true;
        private bool _readonly;
        private DataTable _dataSource;
        private string _textMember;
        private Button btnFind;
        private TextBox txtInternal;
        private Button btnNew;
        private Button btnEdit;
        private Color _editorBackColor = Color.Empty;

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

        public event EventHandler TextMemberChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_TEXTMEMBERCHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_TEXTMEMBERCHANGED, value);
            }
        }

        public ExtendedDropdown()
        {
            this.InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.OnClickEdit(e);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            TableWrapper wrapper;
            try
            {
                wrapper = new TableWrapper(this.DataSource, this.TextMember);
            }
            catch
            {
                return;
            }
            if (this._dialog == null)
            {
                this._dialog = new FormSelector();
                this._dialog.StartPosition = FormStartPosition.CenterScreen;
            }
            DataTableGridSource dataSource = this._dialog.DataSource as DataTableGridSource;
            if ((dataSource == null) || !ReferenceEquals(dataSource.Table, wrapper.DataSource))
            {
                this._dialog.DataSource = new DataTableGridSource(wrapper.DataSource);
                InitDialogEventArgs args = new InitDialogEventArgs(this._dialog.GridAppearance) {
                    Caption = this._dialog.Text
                };
                this.OnInitDialog(args);
                this._dialog.Text = args.Caption;
            }
            this._dialog.ClearFilter();
            DataRow selectedRow = wrapper.FindRow(this.txtInternal.Text);
            this._dialog.SetSelectedRow(r => ReferenceEquals(r.GetDataRow(), selectedRow));
            this._dialog.SelectFilter();
            if (this._dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtInternal.Text = wrapper.GetText(this._dialog.SelectedRow.GetDataRow());
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.OnClickNew(e);
        }

        private void InitializeComponent()
        {
            this.txtInternal = new TextBox();
            this.btnFind = new Button();
            this.btnNew = new Button();
            this.btnEdit = new Button();
            base.SuspendLayout();
            this.txtInternal.Location = new Point(0, 0);
            this.txtInternal.Name = "txtInternal";
            this.txtInternal.Size = new Size(0xe8, 0x15);
            this.txtInternal.TabIndex = 0;
            this.txtInternal.TextChanged += new EventHandler(this.txtInternal_TextChanged);
            this.btnFind.BackColor = SystemColors.Control;
            this.btnFind.FlatAppearance.BorderColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.btnFind.FlatAppearance.MouseOverBackColor = Color.Silver;
            this.btnFind.FlatStyle = FlatStyle.Flat;
            this.btnFind.Image = Resources.ImageSpyglass;
            this.btnFind.Location = new Point(0xe8, 0);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(0x15, 0x15);
            this.btnFind.TabIndex = 1;
            this.btnFind.TabStop = false;
            this.btnFind.Click += new EventHandler(this.btnFind_Click);
            this.btnNew.BackColor = SystemColors.Control;
            this.btnNew.FlatAppearance.MouseOverBackColor = Color.Silver;
            this.btnNew.FlatStyle = FlatStyle.Flat;
            this.btnNew.Image = Resources.ImageNew;
            this.btnNew.Location = new Point(0xfd, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x15, 0x15);
            this.btnNew.TabIndex = 2;
            this.btnNew.TabStop = false;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnEdit.BackColor = SystemColors.Control;
            this.btnEdit.FlatAppearance.MouseOverBackColor = Color.Silver;
            this.btnEdit.FlatStyle = FlatStyle.Flat;
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
            base.Controls.Add(this.txtInternal);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.btnEdit);
            base.Name = "Combobox";
            base.Size = new Size(0x127, 0x15);
            base.ResumeLayout(false);
        }

        private static bool IsTextMemberValid(DataTable dataSource, string textMember) => 
            (dataSource != null) ? (!string.IsNullOrEmpty(textMember) ? (dataSource.Columns[textMember] != null) : true) : true;

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
            base.Height = Math.Max(0x15, this.txtInternal.Height);
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
            this.txtInternal.Location = new Point(0, 0);
            this.txtInternal.Width = width;
            x += this.txtInternal.Width + 1;
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

        protected virtual void OnTextMemberChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_TEXTMEMBERCHANGED];
            if (handler != null)
            {
                handler(this, e);
            }
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
            this.txtInternal.Enabled = !this._readonly;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool ShouldSerializeEditorBackColor() => 
            !this._editorBackColor.IsEmpty;

        private void txtInternal_TextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);
        }

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
        public DataTable DataSource
        {
            get => 
                this._dataSource;
            set
            {
                if (!ReferenceEquals(this._dataSource, value))
                {
                    this._dataSource = value;
                    if (!IsTextMemberValid(value, this._textMember))
                    {
                        this.TextMember = null;
                    }
                    this.OnDataSourceChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), DefaultValue("Text")]
        public string TextMember
        {
            get => 
                (this._textMember != null) ? this._textMember : "";
            set
            {
                if (!string.Equals(this._textMember, value))
                {
                    if (!IsTextMemberValid(this.DataSource, value))
                    {
                        throw new ArgumentOutOfRangeException("value", "column does not belong to table");
                    }
                    this._textMember = value;
                    this.OnTextMemberChanged(EventArgs.Empty);
                }
            }
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
                this.txtInternal.BackColor;
            set
            {
                this._editorBackColor = value;
                this.txtInternal.BackColor = value;
            }
        }

        [Category("Appearance"), DefaultValue(2)]
        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get => 
                this.txtInternal.BorderStyle;
            set => 
                this.txtInternal.BorderStyle = value;
        }

        public override System.Windows.Forms.ContextMenu ContextMenu
        {
            get => 
                base.ContextMenu;
            set
            {
                base.ContextMenu = value;
                this.txtInternal.ContextMenu = value;
            }
        }

        public override System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get => 
                base.ContextMenuStrip;
            set
            {
                base.ContextMenuStrip = value;
                this.txtInternal.ContextMenuStrip = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Focused =>
            this.txtInternal.Focused;

        public override Color ForeColor
        {
            get => 
                this.txtInternal.ForeColor;
            set
            {
                base.ForeColor = value;
                this.txtInternal.ForeColor = value;
            }
        }

        protected override Size DefaultSize =>
            new Size(0x127, 0x15);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override string Text
        {
            get => 
                this.txtInternal.Text;
            set => 
                this.txtInternal.Text = value;
        }

        private class TableWrapper
        {
            private readonly DataTable dataSource;
            private readonly DataColumn textColumn;

            public TableWrapper(DataTable dataSource, string textMember)
            {
                if (dataSource == null)
                {
                    throw new ArgumentNullException("dataSource");
                }
                if (textMember == null)
                {
                    throw new ArgumentNullException("textMember");
                }
                this.dataSource = dataSource;
                this.textColumn = dataSource.Columns[textMember];
                if (this.textColumn == null)
                {
                    throw new ArgumentOutOfRangeException("textMember", textMember, "does not belong to table");
                }
            }

            public DataRow FindRow(string text)
            {
                string filterExpression = DropDownSupport.QuoteName(this.textColumn.ColumnName) + " = " + DropDownSupport.QuoteString(text);
                DataRow[] rowArray = this.dataSource.Select(filterExpression);
                return ((rowArray.Length != 0) ? rowArray[0] : null);
            }

            public string GetText(DataRow row)
            {
                if (row == null)
                {
                    return "";
                }
                if (!ReferenceEquals(row.Table, this.dataSource))
                {
                    throw new ArgumentException("row does not belong to table");
                }
                return Convert.ToString(row[this.textColumn]);
            }

            public DataTable DataSource =>
                this.dataSource;

            public DataColumn TextColumn =>
                this.textColumn;
        }
    }
}

