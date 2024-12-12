namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlSerialMaintenance : UserControl
    {
        private IContainer components;
        private int? F_SerialID;

        public ControlSerialMaintenance()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
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

        private void Grid_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            DataRow dataRow = e.Row.GetDataRow();
            if (dataRow != null)
            {
                int? nullable = NullableConvert.ToInt32(dataRow["ID"]);
                if (nullable != null)
                {
                    FormSerialMaintenance maintenance1 = FormSerialMaintenance.Load(nullable.Value);
                    maintenance1.MdiParent = base.FindForm().MdiParent;
                    maintenance1.Show();
                }
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Grid = new FilteredGrid();
            this.ToolStrip1 = new ToolStrip();
            this.tsbAdd = new ToolStripButton();
            this.tsbPrint = new ToolStripButton();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x19);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(320, 0xd7);
            this.Grid.TabIndex = 6;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbAdd, this.tsbPrint };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(320, 0x19);
            this.ToolStrip1.TabIndex = 7;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbAdd.Image = My.Resources.Resources.ImageNew;
            this.tsbAdd.ImageTransparentColor = Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new Size(0x30, 0x16);
            this.tsbAdd.Text = "New";
            this.tsbPrint.Image = My.Resources.Resources.ImagePrint;
            this.tsbPrint.ImageTransparentColor = Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new Size(0x31, 0x16);
            this.tsbPrint.Text = "Print";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "ControlSerialMaintenance";
            base.Size = new Size(320, 240);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Appearance.MultiSelect = true;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "ID", 40, Appearance.IntegerStyle());
            Appearance.AddTextColumn("LaborHours", "Labor Hours", 100);
            Appearance.AddTextColumn("Technician", "Technician", 100);
            Appearance.AddTextColumn("MaintenanceDue", "Maintenance Due", 100);
            Appearance.AddTextColumn("MaintenanceCost", "Cost", 70, Appearance.PriceStyle());
        }

        public void LoadList()
        {
            DataTable dataTable = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandText = "SELECT\r\n  ID\r\n, LaborHours\r\n, Technician\r\n, MaintenanceDue\r\n, MaintenanceCost\r\nFROM tbl_serial_maintenance\r\nWHERE (SerialID = :ID)";
                adapter.SelectCommand.Parameters.Add("ID", MySqlType.Int).Value = ToDatabaseInt(this.F_SerialID);
                adapter.Fill(dataTable);
            }
            dataTable.AcceptChanges();
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        private static object ToDatabaseInt(int? value) => 
            (value == null) ? ((object) DBNull.Value) : ((object) value.Value);

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (this.F_SerialID != null)
            {
                FormSerialMaintenance maintenance1 = FormSerialMaintenance.New(this.F_SerialID.Value);
                maintenance1.MdiParent = base.FindForm().MdiParent;
                maintenance1.Show();
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            int[] numArray = this.Grid.GetSelectedRows().GetDataRows().Select<DataRow, int?>(((_Closure$__.$I29-0 == null) ? (_Closure$__.$I29-0 = new Func<DataRow, int?>(_Closure$__.$I._Lambda$__29-0)) : _Closure$__.$I29-0)).Where<int?>(((_Closure$__.$I29-1 == null) ? (_Closure$__.$I29-1 = new Func<int?, bool>(_Closure$__.$I._Lambda$__29-1)) : _Closure$__.$I29-1)).Select<int?, int>(((_Closure$__.$I29-2 == null) ? (_Closure$__.$I29-2 = new Func<int?, int>(_Closure$__.$I._Lambda$__29-2)) : _Closure$__.$I29-2)).ToArray<int>();
            if (0 < numArray.Length)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_serial_maintenance.ID}"] = numArray
                };
                ClassGlobalObjects.ShowReport("Maintenance", @params);
            }
        }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAdd")]
        private ToolStripButton tsbAdd { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbPrint")]
        private ToolStripButton tsbPrint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public int? SerialID
        {
            get => 
                this.F_SerialID;
            set => 
                this.F_SerialID = value;
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly ControlSerialMaintenance._Closure$__ $I = new ControlSerialMaintenance._Closure$__();
            public static Func<DataRow, int?> $I29-0;
            public static Func<int?, bool> $I29-1;
            public static Func<int?, int> $I29-2;

            internal int? _Lambda$__29-0(DataRow r) => 
                NullableConvert.ToInt32(r["ID"]);

            internal bool _Lambda$__29-1(int? v) => 
                v != null;

            internal int _Lambda$__29-2(int? v) => 
                v.Value;
        }
    }
}

