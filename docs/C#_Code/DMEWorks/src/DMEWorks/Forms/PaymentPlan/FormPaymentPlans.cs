namespace DMEWorks.Forms.PaymentPlan
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
    public class FormPaymentPlans : DmeForm
    {
        private IContainer components;

        public FormPaymentPlans()
        {
            base.KeyDown += new KeyEventHandler(this.Form_KeyDown);
            this.InitializeComponent();
            this.InitializeGridStyle(this.SearchableGrid1.Appearance);
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.R) & e.Control)
            {
                this.LoadGrid();
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.SearchableGrid1 = new SearchableGrid();
            this.ToolStrip1 = new ToolStrip();
            this.tsbRefresh = new ToolStripButton();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.SearchableGrid1.Dock = DockStyle.Fill;
            this.SearchableGrid1.Location = new Point(0, 0x19);
            this.SearchableGrid1.Name = "SearchableGrid1";
            this.SearchableGrid1.Size = new Size(0x278, 0x1ac);
            this.SearchableGrid1.TabIndex = 0;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbRefresh };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x278, 0x19);
            this.ToolStrip1.TabIndex = 1;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbRefresh.Image = My.Resources.Resources.ImageRefresh;
            this.tsbRefresh.ImageTransparentColor = Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new Size(0x41, 0x16);
            this.tsbRefresh.Text = "Refresh";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.SearchableGrid1);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormPaymentPlans";
            this.Text = "Payment Plans";
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGridStyle(SearchableGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("CustomerName", "Customer", 100);
            Appearance.AddTextColumn("OrderID", "Order#", 70, Appearance.IntegerStyle());
            Appearance.AddTextColumn("OrderDetailsID", "Line Item#", 70, Appearance.IntegerStyle());
            Appearance.AddTextColumn("SaleRentType", "Sale/Rent", 80);
            Appearance.AddTextColumn("BillingCode", "Billing Code", 70);
            Appearance.AddTextColumn("InventoryItem", "Inventory Item", 100);
            Appearance.AddTextColumn("PriceCode", "Price Code", 80);
            Appearance.AddTextColumn("Payers", "Payers", 80);
            Appearance.AddTextColumn("MIR", "Summary", 120);
            Appearance.AddTextColumn("Details", "Details", 120).DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        [HandleDatabaseChanged("tbl_paymentplan")]
        private void LoadGrid()
        {
            try
            {
                DataTable dataTable = new DataTable("tbl_paymentplan");
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM tbl_paymentplan", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(dataTable);
                }
                this.SearchableGrid1.GridSource = dataTable.ToGridSource();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SafeInvoke(new Action(this.LoadGrid));
        }

        private void SearchableGrid1_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            DataRow dataRow = e.Row.GetDataRow();
            if (dataRow != null)
            {
                int? nullable = NullableConvert.ToInt32(dataRow["CustomerID"]);
                if (nullable != null)
                {
                    ClassGlobalObjects.ShowForm(FormFactories.FormPaymentPlan(nullable.Value));
                }
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        [field: AccessedThroughProperty("SearchableGrid1")]
        internal virtual SearchableGrid SearchableGrid1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        internal virtual ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbRefresh")]
        internal virtual ToolStripButton tsbRefresh { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

