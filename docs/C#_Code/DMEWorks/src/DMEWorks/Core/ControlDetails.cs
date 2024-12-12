namespace DMEWorks.Core
{
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class ControlDetails : DmeUserControl
    {
        private IContainer components;
        public const string CrLf = "\r\n";
        protected internal readonly TableDetails F_TableDetails;
        private readonly List<FormDetails> FDialogs = new List<FormDetails>();
        private AllowStateEnum FAllowState = AllowStateEnum.AllowAll;

        public event EventHandler Changed;

        public ControlDetails()
        {
            this.InitializeComponent();
            this.F_TableDetails = this.CreateTable();
            this.F_TableDetails.Changed += new EventHandler(this.F_TableDetails_Changed);
            this.Grid.GridSource = this.F_TableDetails.ToGridSource();
            this.InitializeGrid(this.Grid.Appearance);
        }

        [CompilerGenerated]
        private void _Lambda$__38-0(FormDetails d)
        {
            d.AllowState = this.FAllowState;
        }

        protected FormDetails AddDialog(FormDetails Dialog)
        {
            Dialog.MdiParent = base.FindForm().MdiParent;
            this.FDialogs.Add(Dialog);
            this.SafeInvoke(new Action(Dialog.LoadComboBoxes));
            Dialog.AllowState = this.AllowState;
            return Dialog;
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.AllowState & AllowStateEnum.AllowNew) == AllowStateEnum.AllowNew)
                {
                    this.DoAdd(null);
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

        public void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.AllowState & AllowStateEnum.AllowDelete) == AllowStateEnum.AllowDelete)
                {
                    this.DoDelete();
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

        protected void CloseDialogs()
        {
            int num = this.FDialogs.Count - 1;
            for (int i = 0; i <= num; i++)
            {
                FormDetails details = this.FDialogs[i];
                if (!details.Disposing)
                {
                    details.Close();
                    details.Dispose();
                }
            }
            this.FDialogs.Clear();
        }

        public void Commit()
        {
            this.DoForAllDialogs<FormDetails>((_Closure$__.$I34-0 == null) ? (_Closure$__.$I34-0 = new Action<FormDetails>(_Closure$__.$I._Lambda$__34-0)) : _Closure$__.$I34-0);
        }

        protected virtual FormDetails CreateDialog(object param) => 
            null;

        protected virtual TableDetails CreateTable() => 
            new TableDetails();

        protected internal virtual void DeletingCompleted(DataRow row)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
                this.CloseDialogs();
            }
            base.Dispose(disposing);
        }

        protected virtual void DoAdd(object param)
        {
            FormDetails details1 = this.CreateDialog(param);
            details1.Edit(DBNull.Value);
            details1.Show();
        }

        protected virtual void DoDelete()
        {
            DataRow dataRow = this.Grid.CurrentRow.GetDataRow();
            if (dataRow != null)
            {
                dataRow.Delete();
                this.DeletingCompleted(dataRow);
            }
        }

        protected void DoForAllDialogs<TDialog>(Action<TDialog> action) where TDialog: FormDetails
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            int num = this.FDialogs.Count - 1;
            for (int i = 0; i <= num; i++)
            {
                TDialog local = this.FDialogs[i] as TDialog;
                if ((local != null) && (!local.IsDisposed && !local.Disposing))
                {
                    action(local);
                }
            }
        }

        protected internal virtual void EditingCompleted(DataRow row)
        {
        }

        protected void EditRow(int RowID)
        {
            FormDetails details = this.CreateDialog(null);
            if (!details.Edit(RowID))
            {
                details.Dispose();
            }
            else
            {
                details.AllowState = this.AllowState;
                details.Show();
            }
        }

        private void F_TableDetails_Changed(object sender, EventArgs e)
        {
            this.OnChanged(e);
        }

        protected TableDetails GetTable() => 
            this.F_TableDetails;

        private void Grid_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    this.EditRow(Conversions.ToInteger(dataRow[dataRow.Table.Columns["RowID"]]));
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ResourceManager manager = new ResourceManager(typeof(ControlDetails));
            this.Grid = new FilteredGrid();
            this.Panel1 = new Panel();
            this.btnDelete = new Button();
            this.btnAdd = new Button();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(320, 240);
            this.Grid.TabIndex = 2;
            Control[] controls = new Control[] { this.btnDelete, this.btnAdd };
            this.Panel1.Controls.AddRange(controls);
            this.Panel1.Dock = DockStyle.Right;
            this.Panel1.Location = new Point(320, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x20, 240);
            this.Panel1.TabIndex = 3;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Image = (Bitmap) manager.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(4, 40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 1;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Image = (Bitmap) manager.GetObject("btnAdd.Image");
            this.btnAdd.Location = new Point(4, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x18, 0x18);
            this.btnAdd.TabIndex = 0;
            Control[] controlArray2 = new Control[] { this.Grid, this.Panel1 };
            base.Controls.AddRange(controlArray2);
            base.Name = "ControlDetails";
            base.Size = new Size(0x160, 240);
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected virtual void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = true;
            Appearance.Columns.Clear();
        }

        protected virtual void OnChanged(EventArgs e)
        {
            EventHandler changedEvent = this.ChangedEvent;
            if (changedEvent != null)
            {
                changedEvent(this, e);
            }
        }

        [field: AccessedThroughProperty("Panel1")]
        protected virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnDelete")]
        protected virtual Button btnDelete { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnAdd")]
        protected virtual Button btnAdd { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Grid")]
        protected virtual FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Browsable(false), DefaultValue(0x3ff), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual AllowStateEnum AllowState
        {
            get => 
                this.FAllowState;
            set
            {
                this.FAllowState = value;
                this.btnAdd.Enabled = (this.FAllowState & AllowStateEnum.AllowNew) == AllowStateEnum.AllowNew;
                this.btnDelete.Enabled = (this.FAllowState & AllowStateEnum.AllowDelete) == AllowStateEnum.AllowDelete;
                this.DoForAllDialogs<FormDetails>(new Action<FormDetails>(this._Lambda$__38-0));
            }
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly ControlDetails._Closure$__ $I = new ControlDetails._Closure$__();
            public static Action<FormDetails> $I34-0;

            internal void _Lambda$__34-0(FormDetails d)
            {
                d.Commit();
            }
        }

        public class TableDetails : EventTableBase
        {
            private DataColumn F_Col_RowID;

            public TableDetails() : this("tbl_details")
            {
            }

            public TableDetails(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                this.F_Col_RowID = base.Columns["RowID"];
            }

            protected override void InitializeClass()
            {
                DataColumn column1 = base.Columns.Add("RowID", typeof(int));
                column1.AutoIncrement = true;
                column1.AutoIncrementSeed = 0L;
                column1.AutoIncrementStep = 1L;
                column1.Unique = true;
                column1.AllowDBNull = false;
                column1.ReadOnly = true;
                base.PrimaryKey = new DataColumn[] { base.Columns["RowID"] };
            }

            public DataColumn Col_RowID =>
                this.F_Col_RowID;
        }
    }
}

