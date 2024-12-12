using Devart.Data.MySql;
using DMEWorks;
using DMEWorks.Core;
using DMEWorks.Data.MySql;
using DMEWorks.Forms;
using DMEWorks.Maintain;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

[DesignerGenerated]
public class FormCmnList2 : DmeForm
{
    private IContainer components;
    private DmercType _DefaultCmnType = ((DmercType) 0);
    private int _CustomerID;
    private int? _OrderID;
    private int? _CmnID;
    private const string CmnType_None = "<None>";

    public FormCmnList2()
    {
        this.InitializeComponent();
        this.Grid.AutoGenerateColumns = false;
        this.FillCmnList();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        this.cmsCreateCmn.Show(this.btnCreate, new Point(0, this.btnCreate.Height));
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        this._CmnID = GetCmnID(this.Grid.CurrentRow);
        base.DialogResult = DialogResult.OK;
    }

    private void chbDefaultCmnType_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.LoadForms(false);
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

    private void cmsCreateCmn_Click(object sender, EventArgs args)
    {
        CreateCmnToolStripMenuItem item = sender as CreateCmnToolStripMenuItem;
        if ((item != null) && (this._OrderID != null))
        {
            try
            {
                this._CmnID = new int?(FormCMNRX.CreateCMN(item.Type, this._OrderID.Value));
                this.LoadForms(true);
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
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void FillCmnList()
    {
        IEnumerator enumerator;
        try
        {
            enumerator = Enum.GetValues(typeof(DmercType)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                DmercType type = (DmercType) Conversions.ToInteger(enumerator.Current);
                if (string.IsNullOrEmpty(DmercHelper.GetStatus(type)))
                {
                    this.cmsCreateCmn.Items.Add(new CreateCmnToolStripMenuItem(type, new EventHandler(this.cmsCreateCmn_Click)));
                }
            }
        }
        finally
        {
            if (enumerator is IDisposable)
            {
                (enumerator as IDisposable).Dispose();
            }
        }
    }

    private static int? GetCmnID(DataGridViewRow gridRow)
    {
        int? nullable;
        if (gridRow == null)
        {
            nullable = null;
        }
        else
        {
            DataRowView dataBoundItem = gridRow.DataBoundItem as DataRowView;
            if (dataBoundItem == null)
            {
                nullable = null;
            }
            else
            {
                TableCmnrx table = dataBoundItem.Row.Table as TableCmnrx;
                if (table == null)
                {
                    nullable = null;
                }
                else
                {
                    try
                    {
                        nullable = new int?(Convert.ToInt32(dataBoundItem.Row[table.Col_CmnID]));
                    }
                    catch (InvalidCastException exception1)
                    {
                        InvalidCastException ex = exception1;
                        ProjectData.SetProjectError(ex);
                        InvalidCastException exception = ex;
                        nullable = null;
                        ProjectData.ClearProjectError();
                    }
                }
            }
        }
        return nullable;
    }

    private void Grid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
    {
        if ((e.RowIndex >= 0) && (this.Grid.Rows.Count > e.RowIndex))
        {
            DataRowView dataBoundItem = this.Grid.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (dataBoundItem != null)
            {
                TableCmnrx table = dataBoundItem.Row.Table as TableCmnrx;
                if (table != null)
                {
                    e.ToolTipText = Convert.ToString(dataBoundItem.Row[table.Col_ToolTipText]);
                }
            }
        }
    }

    private void Grid_DoubleClick(object sender, EventArgs e)
    {
        this.btnOK_Click(sender, e);
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.btnOK = new Button();
        this.View = new DataView();
        this.chbDefaultCmnType = new CheckBox();
        this.Panel1 = new Panel();
        this.btnCreate = new Button();
        this.btnCancel = new Button();
        this.Grid = new DataGridView();
        this.dgvcID = new DataGridViewTextBoxColumn();
        this.dgvcCmnType = new DataGridViewTextBoxColumn();
        this.dgvcDoctorName = new DataGridViewTextBoxColumn();
        this.dgvcInitialDate = new DataGridViewTextBoxColumn();
        this.cmsCreateCmn = new ContextMenuStrip(this.components);
        this.View.BeginInit();
        this.Panel1.SuspendLayout();
        ((ISupportInitialize) this.Grid).BeginInit();
        base.SuspendLayout();
        this.btnOK.DialogResult = DialogResult.OK;
        this.btnOK.Location = new Point(0xe8, 8);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(0x4b, 0x18);
        this.btnOK.TabIndex = 2;
        this.btnOK.Text = "OK";
        this.chbDefaultCmnType.Checked = true;
        this.chbDefaultCmnType.CheckState = CheckState.Checked;
        this.chbDefaultCmnType.Location = new Point(8, 8);
        this.chbDefaultCmnType.Name = "chbDefaultCmnType";
        this.chbDefaultCmnType.Size = new Size(0x6f, 0x18);
        this.chbDefaultCmnType.TabIndex = 0;
        this.chbDefaultCmnType.Text = "Default Cmn only";
        this.Panel1.Controls.Add(this.btnCreate);
        this.Panel1.Controls.Add(this.chbDefaultCmnType);
        this.Panel1.Controls.Add(this.btnCancel);
        this.Panel1.Controls.Add(this.btnOK);
        this.Panel1.Dock = DockStyle.Bottom;
        this.Panel1.Location = new Point(0, 0xe9);
        this.Panel1.Name = "Panel1";
        this.Panel1.Size = new Size(0x188, 40);
        this.Panel1.TabIndex = 1;
        this.btnCreate.Location = new Point(0x98, 8);
        this.btnCreate.Name = "btnCreate";
        this.btnCreate.Size = new Size(0x4b, 0x18);
        this.btnCreate.TabIndex = 1;
        this.btnCreate.Text = "Create";
        this.btnCancel.DialogResult = DialogResult.Cancel;
        this.btnCancel.Location = new Point(0x138, 8);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(0x4b, 0x18);
        this.btnCancel.TabIndex = 3;
        this.btnCancel.Text = "Cancel";
        this.Grid.AllowUserToAddRows = false;
        this.Grid.AllowUserToDeleteRows = false;
        this.Grid.AllowUserToOrderColumns = true;
        this.Grid.BorderStyle = BorderStyle.None;
        this.Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        this.Grid.ColumnHeadersHeight = 0x15;
        this.Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.dgvcID, this.dgvcCmnType, this.dgvcDoctorName, this.dgvcInitialDate };
        this.Grid.Columns.AddRange(dataGridViewColumns);
        this.Grid.Dock = DockStyle.Fill;
        this.Grid.EditMode = DataGridViewEditMode.EditProgrammatically;
        this.Grid.Location = new Point(0, 0);
        this.Grid.MultiSelect = false;
        this.Grid.Name = "Grid";
        this.Grid.ReadOnly = true;
        this.Grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        this.Grid.RowHeadersWidth = 0x15;
        this.Grid.RowTemplate.ReadOnly = true;
        this.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.Grid.Size = new Size(0x188, 0xe9);
        this.Grid.TabIndex = 0;
        this.dgvcID.DataPropertyName = "CmnID";
        this.dgvcID.FillWeight = 80f;
        this.dgvcID.HeaderText = "ID";
        this.dgvcID.Name = "dgvcID";
        this.dgvcID.ReadOnly = true;
        this.dgvcID.Width = 40;
        this.dgvcCmnType.DataPropertyName = "CmnType";
        this.dgvcCmnType.FillWeight = 120f;
        this.dgvcCmnType.HeaderText = "Type";
        this.dgvcCmnType.Name = "dgvcCmnType";
        this.dgvcCmnType.ReadOnly = true;
        this.dgvcCmnType.Width = 120;
        this.dgvcDoctorName.DataPropertyName = "DoctorName";
        this.dgvcDoctorName.FillWeight = 200f;
        this.dgvcDoctorName.HeaderText = "Doctor Name";
        this.dgvcDoctorName.Name = "dgvcDoctorName";
        this.dgvcDoctorName.ReadOnly = true;
        this.dgvcDoctorName.Width = 120;
        this.dgvcInitialDate.DataPropertyName = "InitialDate";
        this.dgvcInitialDate.FillWeight = 80f;
        this.dgvcInitialDate.HeaderText = "Initial Date";
        this.dgvcInitialDate.Name = "dgvcInitialDate";
        this.dgvcInitialDate.ReadOnly = true;
        this.dgvcInitialDate.Width = 70;
        this.cmsCreateCmn.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.cmsCreateCmn.Name = "cmsCreateCmn";
        this.cmsCreateCmn.Size = new Size(0x3d, 4);
        base.AcceptButton = this.btnOK;
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.btnCancel;
        base.ClientSize = new Size(0x188, 0x111);
        base.Controls.Add(this.Grid);
        base.Controls.Add(this.Panel1);
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        this.MinimumSize = new Size(300, 0xe1);
        base.Name = "FormCmnList2";
        base.ShowInTaskbar = false;
        this.Text = "CMN / RX";
        this.View.EndInit();
        this.Panel1.ResumeLayout(false);
        ((ISupportInitialize) this.Grid).EndInit();
        base.ResumeLayout(false);
    }

    private void LoadForms(bool LoadData)
    {
        this.Grid.DataSource = null;
        try
        {
            if (LoadData)
            {
                TableCmnrx dest = new TableCmnrx();
                LoadForms(this._CustomerID, dest);
                this.View.Table = dest;
            }
            this.UpdateRowFilter();
        }
        finally
        {
            this.Grid.DataSource = this.View;
        }
        this.UpdateSelectedRow();
    }

    private static void LoadForms(int CustomerID, TableCmnrx Dest)
    {
        IEnumerator enumerator;
        DataRow row = Dest.NewRow();
        row[Dest.Col_CmnID] = DBNull.Value;
        row[Dest.Col_CmnType] = "<None>";
        row[Dest.Col_DoctorName] = DBNull.Value;
        row[Dest.Col_InitialDate] = DBNull.Value;
        row[Dest.Col_ToolTipText] = "Remove CMN/RX Form from line item";
        Dest.Rows.Add(row);
        row.AcceptChanges();
        StringBuilder builder = new StringBuilder();
        try
        {
            enumerator = Enum.GetValues(typeof(DmercType)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                DmercType type = (DmercType) Conversions.ToInteger(enumerator.Current);
                if (string.IsNullOrEmpty(DmercHelper.GetStatus(type)))
                {
                    if (0 < builder.Length)
                    {
                        builder.Append(", ");
                    }
                    builder.Append('\'').Append(DmercHelper.Dmerc2String(type)).Append('\'');
                }
            }
        }
        finally
        {
            if (enumerator is IDisposable)
            {
                (enumerator as IDisposable).Dispose();
            }
        }
        using (DataSet set = new DataSet())
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(string.Format("SELECT\r\n  tbl_cmnform.ID as CmnID\r\n, tbl_cmnform.CMNType as CmnType\r\n, Concat(tbl_doctor.LastName, ', ', tbl_doctor.FirstName) as DoctorName\r\n, tbl_cmnform.InitialDate\r\nFROM tbl_cmnform\r\n     LEFT JOIN tbl_doctor as tbl_doctor ON tbl_cmnform.DoctorID = tbl_doctor.ID\r\nWHERE (tbl_cmnform.CustomerID = {0})\r\n  AND (tbl_cmnform.CMNType IN ({1}))\r\n;\r\nSELECT\r\n  details.CMNFormID as CmnID\r\n, details.BillingCode\r\n, item.Name as InventoryItem\r\nFROM tbl_cmnform\r\n     INNER JOIN  tbl_cmnform_details as details ON tbl_cmnform.ID = details.CMNFormID\r\n     LEFT JOIN tbl_inventoryitem as item ON details.InventoryItemID = item.ID\r\nWHERE (tbl_cmnform.CustomerID = {0})\r\n  AND (tbl_cmnform.CMNType IN ({1}))", CustomerID, builder.ToString()), ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.TableMappings.Add("Table", "Forms");
                adapter.TableMappings.Add("Table1", "Items");
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(set);
            }
            DataTable table = set.Tables["Items"];
            DataRow[] rowArray = set.Tables["Forms"].Select();
            for (int i = 0; i < rowArray.Length; i++)
            {
                DataRow row1 = rowArray[i];
                int? nullable2 = NullableConvert.ToInt32(row1["CmnID"]);
                int num2 = nullable2.Value;
                string str2 = NullableConvert.ToString(row1["CmnType"]);
                string str3 = NullableConvert.ToString(row1["DoctorName"]);
                DateTime? nullable = NullableConvert.ToDateTime(row1["InitialDate"]);
                StringBuilder builder2 = new StringBuilder(0x100);
                builder2.Append("Form #").Append(num2).Append(", ").Append(str2).AppendLine();
                if (nullable != null)
                {
                    builder2.AppendFormat("Initial Date: {0:d/M/yyyy}", nullable.Value).AppendLine();
                }
                if (!string.IsNullOrEmpty(str3))
                {
                    builder2.Append("Doctor: ").Append(str3).AppendLine();
                }
                DataRow[] rowArray2 = table.Select("CmnID = " + Conversions.ToString(num2), "BillingCode");
                if (0 < rowArray2.Length)
                {
                    builder2.Append("----").AppendLine();
                    DataRow[] rowArray3 = rowArray2;
                    for (int j = 0; j < rowArray3.Length; j++)
                    {
                        DataRow row3 = rowArray3[j];
                        string str4 = NullableConvert.ToString(row3["BillingCode"]);
                        string str5 = NullableConvert.ToString(row3["InventoryItem"]);
                        if (!string.IsNullOrEmpty(str4))
                        {
                            builder2.Append("(").Append(str4).Append(") ").Append(str5);
                        }
                        else
                        {
                            builder2.Append(str5);
                        }
                        builder2.AppendLine();
                    }
                }
                DataRow row2 = Dest.NewRow();
                row2[Dest.Col_CmnID] = num2;
                row2[Dest.Col_CmnType] = str2;
                row2[Dest.Col_DoctorName] = str3;
                row2[Dest.Col_InitialDate] = (nullable != null) ? ((object) nullable) : ((object) DBNull.Value);
                row2[Dest.Col_ToolTipText] = builder2.ToString();
                Dest.Rows.Add(row2);
                row2.AcceptChanges();
            }
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        try
        {
            this.LoadForms(true);
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

    private void SelectDefaultType(DmercType @default)
    {
        IEnumerator enumerator;
        try
        {
            enumerator = this.cmsCreateCmn.Items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CreateCmnToolStripMenuItem current = (CreateCmnToolStripMenuItem) enumerator.Current;
                bool flag = current.Type == @default;
                if (current.Font.Bold != flag)
                {
                    current.Font = new Font(current.Font, flag ? FontStyle.Bold : FontStyle.Regular);
                }
            }
        }
        finally
        {
            if (enumerator is IDisposable)
            {
                (enumerator as IDisposable).Dispose();
            }
        }
    }

    private void UpdateRowFilter()
    {
        StringBuilder builder = new StringBuilder();
        if (this.chbDefaultCmnType.Visible && this.chbDefaultCmnType.Checked)
        {
            builder.AppendFormat("([CmnType] = '{0}')", MySqlUtilities.EscapeString(DmercHelper.Dmerc2String(this._DefaultCmnType)));
            builder.Append(" OR ");
            builder.AppendFormat("([CmnType] = '{0}')", MySqlUtilities.EscapeString("<None>"));
            if (this._CmnID != null)
            {
                builder.Append(" OR ");
                builder.AppendFormat("([CmnID] = {0})", this._CmnID.Value);
            }
        }
        this.View.RowFilter = builder.ToString();
    }

    private void UpdateSelectedRow()
    {
        IEnumerator enumerator;
        int? nullable = this._CmnID;
        DataGridViewRow row = null;
        try
        {
            enumerator = ((IEnumerable) this.Grid.Rows).GetEnumerator();
            while (enumerator.MoveNext())
            {
                DataGridViewRow current = (DataGridViewRow) enumerator.Current;
                current.Selected = false;
                if (nullable.Equals(GetCmnID(current)))
                {
                    row = current;
                }
            }
        }
        finally
        {
            if (enumerator is IDisposable)
            {
                (enumerator as IDisposable).Dispose();
            }
        }
        if (row != null)
        {
            this.Grid.CurrentCell = row.Cells[0];
            row.Selected = true;
        }
    }

    [field: AccessedThroughProperty("btnOK")]
    private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("View")]
    private DataView View { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("chbDefaultCmnType")]
    private CheckBox chbDefaultCmnType { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Panel1")]
    private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnCancel")]
    private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Grid")]
    private DataGridView Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnCreate")]
    private Button btnCreate { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("cmsCreateCmn")]
    private ContextMenuStrip cmsCreateCmn { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dgvcID")]
    internal virtual DataGridViewTextBoxColumn dgvcID { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dgvcCmnType")]
    internal virtual DataGridViewTextBoxColumn dgvcCmnType { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dgvcDoctorName")]
    internal virtual DataGridViewTextBoxColumn dgvcDoctorName { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("dgvcInitialDate")]
    internal virtual DataGridViewTextBoxColumn dgvcInitialDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public DmercType DefaultCmnType
    {
        get => 
            this._DefaultCmnType;
        set
        {
            this._DefaultCmnType = value;
            this.chbDefaultCmnType.Visible = value != ((DmercType) 0);
            this.SelectDefaultType(value);
        }
    }

    public int CustomerID
    {
        get => 
            this._CustomerID;
        set => 
            this._CustomerID = value;
    }

    public int? OrderID
    {
        get => 
            this._OrderID;
        set
        {
            this._OrderID = value;
            this.btnCreate.Enabled = this._OrderID != null;
        }
    }

    public int? CmnID
    {
        get => 
            this._CmnID;
        set => 
            this._CmnID = value;
    }

    public class CreateCmnToolStripMenuItem : ToolStripMenuItem
    {
        public readonly DmercType Type;

        public CreateCmnToolStripMenuItem(DmercType type, EventHandler clickHandler)
        {
            this.Type = type;
            this.Text = "Create " + DmercHelper.Dmerc2String(type);
            base.Click += clickHandler;
        }
    }

    private class TableCmnrx : TableBase
    {
        private DataColumn _col_CmnID;
        private DataColumn _col_CmnType;
        private DataColumn _col_DoctorName;
        private DataColumn _col_InitialDate;
        private DataColumn _col_ToolTipText;
        private const string Name_CmnID = "CmnID";
        private const string Name_CmnType = "CmnType";
        private const string Name_DoctorName = "DoctorName";
        private const string Name_InitialDate = "InitialDate";
        private const string Name_ToolTipText = "ToolTipText";

        public TableCmnrx() : this("tbl_cmnrx")
        {
        }

        public TableCmnrx(string TableName) : base(TableName)
        {
        }

        protected override void Initialize()
        {
            this._col_CmnID = base.Columns["CmnID"];
            this._col_CmnType = base.Columns["CmnType"];
            this._col_DoctorName = base.Columns["DoctorName"];
            this._col_InitialDate = base.Columns["InitialDate"];
            this._col_ToolTipText = base.Columns["ToolTipText"];
        }

        protected override void InitializeClass()
        {
            this._col_CmnID = base.Columns.Add("CmnID", typeof(int));
            this._col_CmnType = base.Columns.Add("CmnType", typeof(string));
            this._col_DoctorName = base.Columns.Add("DoctorName", typeof(string));
            this._col_InitialDate = base.Columns.Add("InitialDate", typeof(DateTime));
            this._col_ToolTipText = base.Columns.Add("ToolTipText", typeof(string));
        }

        public DataColumn Col_CmnID =>
            this._col_CmnID;

        public DataColumn Col_CmnType =>
            this._col_CmnType;

        public DataColumn Col_DoctorName =>
            this._col_DoctorName;

        public DataColumn Col_InitialDate =>
            this._col_InitialDate;

        public DataColumn Col_ToolTipText =>
            this._col_ToolTipText;
    }
}

