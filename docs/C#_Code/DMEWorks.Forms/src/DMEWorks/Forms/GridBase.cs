namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class GridBase : UserControl
    {
        private string m_filterTextCore;
        private static readonly object EVENT_ROWCLICK = new object();
        private static readonly object EVENT_ROWDOUBLECLICK = new object();
        private static readonly object EVENT_ROWCONTEXTMENUNEEDED = new object();
        private static readonly object EVENT_CELLBEGINEDIT = new object();
        private static readonly object EVENT_CELLENDEDIT = new object();
        private static readonly object EVENT_CELLFORMATTING = new object();
        private static readonly object EVENT_CELLPARSING = new object();
        private static readonly object EVENT_CELLTOOLTIPTEXTNEEDED = new object();
        private static readonly object EVENT_CURRENTCELLDIRTYSTATECHANGED = new object();
        private static object EVENT_SEARCHABLEGRIDDATAERROR = new object();
        private IContainer components;
        private DataGridViewEx Grid;
        protected Panel pnlGrid;

        public event EventHandler<GridCellCancelEventArgs> CellBeginEdit
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLBEGINEDIT, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_CELLBEGINEDIT, value);
            }
        }

        public event EventHandler<GridCellEventArgs> CellEndEdit
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLENDEDIT, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_CELLENDEDIT, value);
            }
        }

        public event EventHandler<GridCellFormattingEventArgs> CellFormatting
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLFORMATTING, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_CELLFORMATTING, value);
            }
        }

        public event EventHandler<GridCellParsingEventArgs> CellParsing
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLPARSING, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_CELLPARSING, value);
            }
        }

        public event EventHandler<GridCellToolTipTextNeededEventArgs> CellToolTipTextNeeded
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLTOOLTIPTEXTNEEDED, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_CELLTOOLTIPTEXTNEEDED, value);
            }
        }

        public event EventHandler<EventArgs> CurrentCellDirtyStateChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_CURRENTCELLDIRTYSTATECHANGED, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_CURRENTCELLDIRTYSTATECHANGED, value);
            }
        }

        public event EventHandler<GridDataErrorEventArgs> DataError
        {
            add
            {
                base.Events.AddHandler(EVENT_SEARCHABLEGRIDDATAERROR, value);
                if (base.Events[EVENT_SEARCHABLEGRIDDATAERROR] is EventHandler<GridDataErrorEventArgs>)
                {
                    this.Grid.DataError += new DataGridViewDataErrorEventHandler(this.Grid_DataError);
                }
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_SEARCHABLEGRIDDATAERROR, value);
                if (!(base.Events[EVENT_SEARCHABLEGRIDDATAERROR] is EventHandler<GridDataErrorEventArgs>))
                {
                    this.Grid.DataError -= new DataGridViewDataErrorEventHandler(this.Grid_DataError);
                }
            }
        }

        public event KeyEventHandler GridKeyDown
        {
            add
            {
                this.Grid.KeyDown += value;
            }
            remove
            {
                this.Grid.KeyDown -= value;
            }
        }

        public event KeyPressEventHandler GridKeyPress
        {
            add
            {
                this.Grid.KeyPress += value;
            }
            remove
            {
                this.Grid.KeyPress -= value;
            }
        }

        public event KeyEventHandler GridKeyUp
        {
            add
            {
                this.Grid.KeyUp += value;
            }
            remove
            {
                this.Grid.KeyUp -= value;
            }
        }

        public event EventHandler<GridMouseEventArgs> RowClick
        {
            add
            {
                base.Events.AddHandler(EVENT_ROWCLICK, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_ROWCLICK, value);
            }
        }

        public event EventHandler<GridContextMenuNeededEventArgs> RowContextMenuNeeded
        {
            add
            {
                base.Events.AddHandler(EVENT_ROWCONTEXTMENUNEEDED, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_ROWCONTEXTMENUNEEDED, value);
            }
        }

        public event EventHandler<GridMouseEventArgs> RowDoubleClick
        {
            add
            {
                base.Events.AddHandler(EVENT_ROWDOUBLECLICK, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_ROWDOUBLECLICK, value);
            }
        }

        public GridBase()
        {
            this.InitializeComponent();
        }

        public bool BegiEdit(bool selectAll) => 
            this.Grid.BeginEdit(selectAll);

        public bool CancelEdit() => 
            this.Grid.CancelEdit();

        public bool CommitEdit(DataGridViewDataErrorContexts context) => 
            this.Grid.CommitEdit(context);

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool EndEdit() => 
            this.Grid.EndEdit();

        public bool EndEdit(DataGridViewDataErrorContexts context) => 
            this.Grid.EndEdit(context);

        private static DataTable GetDataTable(DataView view) => 
            view?.Table;

        public DataGridView GetGrid() => 
            this.Grid;

        public DataGridViewRow[] GetRows()
        {
            DataGridViewRowCollection rows = this.Grid.Rows;
            DataGridViewRow[] rowArray = new DataGridViewRow[rows.Count];
            int index = 0;
            int count = rows.Count;
            while (index < count)
            {
                rowArray[index] = rows[index];
                index++;
            }
            return rowArray;
        }

        public DataGridViewRow[] GetSelectedRows()
        {
            DataGridViewSelectedRowCollection selectedRows = this.Grid.SelectedRows;
            DataGridViewRow[] array = new DataGridViewRow[selectedRows.Count];
            selectedRows.CopyTo(array, 0);
            return array;
        }

        private void Grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EventHandler<GridCellCancelEventArgs> handler = base.Events[EVENT_CELLBEGINEDIT] as EventHandler<GridCellCancelEventArgs>;
            if (((handler != null) && ((e.RowIndex >= 0) && (this.Grid.RowCount > e.RowIndex))) && ((e.ColumnIndex >= 0) && (this.Grid.ColumnCount > e.ColumnIndex)))
            {
                GridCellCancelEventArgs args = new GridCellCancelEventArgs(this.Grid.Rows[e.RowIndex], this.Grid.Columns[e.ColumnIndex]);
                handler(this, args);
                e.Cancel = args.Cancel;
            }
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            EventHandler<GridCellEventArgs> handler = base.Events[EVENT_CELLENDEDIT] as EventHandler<GridCellEventArgs>;
            if (((handler != null) && ((e.RowIndex >= 0) && (this.Grid.RowCount > e.RowIndex))) && ((e.ColumnIndex >= 0) && (this.Grid.ColumnCount > e.ColumnIndex)))
            {
                GridCellEventArgs args = new GridCellEventArgs(this.Grid.Rows[e.RowIndex], this.Grid.Columns[e.ColumnIndex]);
                handler(this, args);
            }
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            EventHandler<GridCellFormattingEventArgs> handler = base.Events[EVENT_CELLFORMATTING] as EventHandler<GridCellFormattingEventArgs>;
            if (((handler != null) && ((e.RowIndex >= 0) && (this.Grid.RowCount > e.RowIndex))) && ((e.ColumnIndex >= 0) && (this.Grid.ColumnCount > e.ColumnIndex)))
            {
                GridCellFormattingEventArgs args = new GridCellFormattingEventArgs(this.Grid.Rows[e.RowIndex], this.Grid.Columns[e.ColumnIndex], e.CellStyle, e.Value, e.DesiredType);
                handler(this, args);
                e.CellStyle = args.CellStyle;
                e.FormattingApplied = args.FormattingApplied;
                e.Value = args.Value;
            }
        }

        private void Grid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            EventHandler<GridCellParsingEventArgs> handler = base.Events[EVENT_CELLPARSING] as EventHandler<GridCellParsingEventArgs>;
            if (((handler != null) && ((e.RowIndex >= 0) && (this.Grid.RowCount > e.RowIndex))) && ((e.ColumnIndex >= 0) && (this.Grid.ColumnCount > e.ColumnIndex)))
            {
                GridCellParsingEventArgs args = new GridCellParsingEventArgs(this.Grid.Rows[e.RowIndex], this.Grid.Columns[e.ColumnIndex], e.InheritedCellStyle, e.Value, e.DesiredType);
                handler(this, args);
                e.InheritedCellStyle = args.CellStyle;
                e.ParsingApplied = args.ParsingApplied;
                e.Value = args.Value;
            }
        }

        private void Grid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            EventHandler<GridCellToolTipTextNeededEventArgs> handler = base.Events[EVENT_CELLTOOLTIPTEXTNEEDED] as EventHandler<GridCellToolTipTextNeededEventArgs>;
            if (((handler != null) && ((e.RowIndex >= 0) && (this.Grid.RowCount > e.RowIndex))) && ((e.ColumnIndex >= 0) && (this.Grid.ColumnCount > e.ColumnIndex)))
            {
                GridCellToolTipTextNeededEventArgs args = new GridCellToolTipTextNeededEventArgs(this.Grid.Rows[e.RowIndex], this.Grid.Columns[e.ColumnIndex], e.ToolTipText);
                handler(this, args);
                e.ToolTipText = args.ToolTipText;
            }
        }

        private void Grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.Grid.IsCurrentCellDirty && (this.Grid.CurrentCell.OwningColumn is DataGridViewCheckBoxColumn))
            {
                this.Grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            EventHandler<EventArgs> handler = base.Events[EVENT_CURRENTCELLDIRTYSTATECHANGED] as EventHandler<EventArgs>;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            EventHandler<GridDataErrorEventArgs> handler = base.Events[EVENT_SEARCHABLEGRIDDATAERROR] as EventHandler<GridDataErrorEventArgs>;
            if (((handler != null) && ((e.RowIndex >= 0) && (this.Grid.RowCount > e.RowIndex))) && ((e.ColumnIndex >= 0) && (this.Grid.ColumnCount > e.ColumnIndex)))
            {
                GridDataErrorEventArgs args = new GridDataErrorEventArgs(this.Grid.Rows[e.RowIndex], this.Grid.Columns[e.ColumnIndex], e.Exception, e.Context) {
                    Cancel = e.Cancel,
                    ThrowException = e.ThrowException
                };
                handler(this, args);
                e.Cancel = args.Cancel;
                e.ThrowException = args.ThrowException;
            }
        }

        private void Grid_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = this.Grid.HitTest(e.X, e.Y);
            if (((info.Type == DataGridViewHitTestType.Cell) || (info.Type == DataGridViewHitTestType.RowHeader)) && ((info.RowIndex >= 0) && (this.Grid.RowCount > info.RowIndex)))
            {
                DataGridViewRow row = this.Grid.Rows[info.RowIndex];
                this.OnRowClick(new GridMouseEventArgs(row, e));
            }
        }

        private void Grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = this.Grid.HitTest(e.X, e.Y);
            if (((info.Type == DataGridViewHitTestType.Cell) || (info.Type == DataGridViewHitTestType.RowHeader)) && ((info.RowIndex >= 0) && (this.Grid.RowCount > info.RowIndex)))
            {
                DataGridViewRow row = this.Grid.Rows[info.RowIndex];
                this.OnRowDoubleClick(new GridMouseEventArgs(row, e));
            }
        }

        private void Grid_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            EventHandler<GridContextMenuNeededEventArgs> handler = base.Events[EVENT_ROWCONTEXTMENUNEEDED] as EventHandler<GridContextMenuNeededEventArgs>;
            if ((handler != null) && ((e.RowIndex >= 0) && (this.Grid.RowCount > e.RowIndex)))
            {
                GridContextMenuNeededEventArgs args = new GridContextMenuNeededEventArgs(this.Grid.Rows[e.RowIndex], e.ContextMenuStrip);
                handler(this, args);
                e.ContextMenuStrip = args.ContextMenu;
            }
        }

        private void InitializeComponent()
        {
            this.Grid = new DataGridViewEx();
            this.pnlGrid = new Panel();
            ((ISupportInitialize) this.Grid).BeginInit();
            this.pnlGrid.SuspendLayout();
            base.SuspendLayout();
            this.Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.Grid.ColumnHeadersHeight = 20;
            this.Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0);
            this.Grid.MultiSelect = false;
            this.Grid.Name = "Grid";
            this.Grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.Grid.RowHeadersWidth = 0x15;
            this.Grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Grid.RowTemplate.Height = 0x12;
            this.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Grid.Size = new Size(0x220, 0x130);
            this.Grid.TabIndex = 0;
            this.Grid.MouseClick += new MouseEventHandler(this.Grid_MouseClick);
            this.Grid.MouseDoubleClick += new MouseEventHandler(this.Grid_MouseDoubleClick);
            this.Grid.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.Grid_CellBeginEdit);
            this.Grid.CellEndEdit += new DataGridViewCellEventHandler(this.Grid_CellEndEdit);
            this.Grid.CellParsing += new DataGridViewCellParsingEventHandler(this.Grid_CellParsing);
            this.Grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(this.Grid_CellToolTipTextNeeded);
            this.Grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            this.Grid.CurrentCellDirtyStateChanged += new EventHandler(this.Grid_CurrentCellDirtyStateChanged);
            this.Grid.RowContextMenuStripNeeded += new DataGridViewRowContextMenuStripNeededEventHandler(this.Grid_RowContextMenuStripNeeded);
            this.pnlGrid.Controls.Add(this.Grid);
            this.pnlGrid.Dock = DockStyle.Fill;
            this.pnlGrid.Location = new Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new Size(0x220, 0x130);
            this.pnlGrid.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.pnlGrid);
            base.Name = "GridBase";
            base.Size = new Size(0x220, 0x130);
            ((ISupportInitialize) this.Grid).EndInit();
            this.pnlGrid.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected virtual void OnRowClick(GridMouseEventArgs e)
        {
            EventHandler<GridMouseEventArgs> handler = base.Events[EVENT_ROWCLICK] as EventHandler<GridMouseEventArgs>;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRowContextMenuNeeded(GridContextMenuNeededEventArgs e)
        {
            EventHandler<GridContextMenuNeededEventArgs> handler = base.Events[EVENT_ROWCONTEXTMENUNEEDED] as EventHandler<GridContextMenuNeededEventArgs>;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRowDoubleClick(GridMouseEventArgs e)
        {
            EventHandler<GridMouseEventArgs> handler = base.Events[EVENT_ROWDOUBLECLICK] as EventHandler<GridMouseEventArgs>;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void SetCurrentRow(Func<DataGridViewRow, bool> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            this.Grid.FirstDisplayedCell = null;
            this.Grid.CurrentCell = null;
            DataGridViewRowCollection rows = this.Grid.Rows;
            int num = 0;
            int count = rows.Count;
            while (true)
            {
                if (num < count)
                {
                    DataGridViewRow arg = rows[num];
                    if (!selector(arg))
                    {
                        num++;
                        continue;
                    }
                    try
                    {
                        arg.Selected = true;
                    }
                    catch (Exception exception1)
                    {
                        Trace.TraceError(exception1.ToString());
                    }
                    if (arg.DataGridView == null)
                    {
                        arg = rows[num];
                    }
                    if (0 < arg.Cells.Count)
                    {
                        this.Grid.FirstDisplayedCell = arg.Cells[0];
                    }
                    if (arg.DataGridView == null)
                    {
                        arg = rows[num];
                    }
                    if (0 < arg.Cells.Count)
                    {
                        this.Grid.CurrentCell = arg.Cells[0];
                        return;
                    }
                }
                return;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewRow CurrentRow =>
            this.Grid.CurrentRow;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IGridSource GridSource
        {
            get => 
                this.Grid.DataSource as IGridSource;
            set
            {
                if (!ReferenceEquals(this.GridSource, value))
                {
                    if (value != null)
                    {
                        value.ApplyFilterText(this.FilterTextCore);
                    }
                    this.Grid.DataSource = value;
                }
            }
        }

        protected string FilterTextCore
        {
            get => 
                this.m_filterTextCore ?? string.Empty;
            set
            {
                value ??= string.Empty;
                if (this.FilterTextCore != value)
                {
                    this.m_filterTextCore = value;
                    IGridSource gridSource = this.GridSource;
                    if (gridSource != null)
                    {
                        gridSource.ApplyFilterText(value);
                    }
                }
            }
        }
    }
}

