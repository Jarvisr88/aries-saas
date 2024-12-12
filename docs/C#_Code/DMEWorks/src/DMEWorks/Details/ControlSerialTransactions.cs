namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlSerialTransactions : UserControl
    {
        private IContainer components;
        public const string CrLf = "\r\n";

        public ControlSerialTransactions()
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
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    int? nullable = NullableConvert.ToInt32(dataRow["ID"]);
                    if (nullable != null)
                    {
                        FormSerialTransaction transaction1 = FormSerialTransaction.Load(nullable.Value);
                        transaction1.MdiParent = base.FindForm().MdiParent;
                        transaction1.Show();
                    }
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
            this.Grid = new FilteredGrid();
            this.ToolStrip1 = new ToolStrip();
            this.tsbAdd = new ToolStripButton();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x19);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(320, 0xd7);
            this.Grid.TabIndex = 6;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbAdd };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(320, 0x19);
            this.ToolStrip1.TabIndex = 8;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbAdd.Image = My.Resources.Resources.ImageNew;
            this.tsbAdd.ImageTransparentColor = Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new Size(0x33, 0x16);
            this.tsbAdd.Text = "New";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "ControlSerialTransactions";
            base.Size = new Size(320, 240);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.NullValue = "";
            cellStyle.Format = "g";
            Appearance.AddTextColumn("TranTime", "Date & Time", 110, cellStyle);
            Appearance.AddTextColumn("ID", "ID", 40, Appearance.IntegerStyle());
            Appearance.AddTextColumn("TypeName", "Type", 80);
            Appearance.AddTextColumn("CustomerName", "Customer", 120);
            Appearance.AddTextColumn("WarehouseName", "Warehouse", 120);
            Appearance.AddTextColumn("VendorName", "Vendor", 120);
        }

        public void LoadList()
        {
            DataTable dataTable = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandText = "SELECT\r\n  st.ID\r\n, st.TransactionDatetime as TranTime\r\n, stt.Name as TypeName\r\n, CONCAT(c.LastName, ', ', c.FirstName) as CustomerName\r\n, IFNULL(w.Name, '') as WarehouseName\r\n, IFNULL(v.Name, '') as VendorName\r\nFROM tbl_serial_transaction as st\r\n     LEFT JOIN tbl_serial_transaction_type as stt ON st.TypeID = stt.ID\r\n     LEFT JOIN tbl_customer as c ON st.CustomerID = c.ID\r\n     LEFT JOIN tbl_vendor as v ON st.VendorID = v.ID\r\n     LEFT JOIN tbl_warehouse as w ON st.WarehouseID = w.ID\r\nWHERE (st.SerialID = :ID)\r\nORDER BY st.TransactionDatetime";
                adapter.SelectCommand.Parameters.Add("ID", MySqlType.Int).Value = NullableConvert.ToDb(this.SerialID);
                adapter.Fill(dataTable);
            }
            dataTable.AcceptChanges();
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SerialID != null)
                {
                    FormSerialTransaction transaction1 = FormSerialTransaction.New(this.SerialID.Value);
                    transaction1.MdiParent = base.FindForm().MdiParent;
                    transaction1.Show();
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

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbAdd")]
        private ToolStripButton tsbAdd { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public int? SerialID { get; set; }
    }
}

