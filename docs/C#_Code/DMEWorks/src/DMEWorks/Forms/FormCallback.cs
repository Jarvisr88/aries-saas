namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormCallback : DmeForm
    {
        private IContainer components;

        public FormCallback()
        {
            base.Load += new EventHandler(this.FormCallback_Load);
            this.InitializeComponent();
            this.CreateTableStyle(this.Grid.Appearance);
        }

        private void CreateTableStyle(SearchableGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("CustomerName", "Customer", 100);
            Appearance.AddTextColumn("CustomerPhone", "Phone", 80);
            Appearance.AddTextColumn("InventoryItem", "Inventory Item", 100);
            Appearance.AddTextColumn("SaleRentType", "Sale/Rent Type", 100);
            Appearance.AddTextColumn("OrderDate", "Order Date", 100);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormCallback_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadGrid();
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

        private void Grid_RowDoubleClick(object sender, GridMouseEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    DataColumn column = dataRow.Table.Columns["OrderID"];
                    if (column != null)
                    {
                        object obj2 = dataRow[column];
                        if (obj2 is int)
                        {
                            FormParameters @params = new FormParameters("ID", obj2);
                            ClassGlobalObjects.ShowForm(FormFactories.FormOrder(), @params);
                        }
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
            this.Grid = new SearchableGrid();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(560, 0x17d);
            this.Grid.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(560, 0x17d);
            base.Controls.Add(this.Grid);
            base.Name = "FormCallback";
            this.Text = "Callback";
            base.ResumeLayout(false);
        }

        private void LoadGrid()
        {
            DataTable dataTable = new DataTable("tbl_callback");
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT
  Concat(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName,
  tbl_customer.Phone as CustomerPhone,
  tbl_inventoryitem.Name as InventoryItem,
  tbl_orderdetails.ID,
  tbl_orderdetails.OrderID,
  tbl_orderdetails.CustomerID,
  tbl_orderdetails.InventoryItemID,
  tbl_orderdetails.SaleRentType,
  tbl_order.OrderDate
FROM tbl_orderdetails
     INNER JOIN tbl_order ON tbl_orderdetails.OrderID    = tbl_order.ID
                         AND tbl_orderdetails.CustomerID = tbl_order.CustomerID
     INNER JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID
     INNER JOIN tbl_inventoryitem ON tbl_orderdetails.InventoryItemID = tbl_inventoryitem.ID
     LEFT JOIN tbl_company ON tbl_company.ID = 1
     LEFT JOIN tbl_invoicedetails ON tbl_orderdetails.ID = tbl_invoicedetails.OrderDetailsID
WHERE ({Interaction.IIf(this.IsDemoVersion, "tbl_customer.ID BETWEEN 1 and 50", "1 = 1")})
  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))
  AND (tbl_invoicedetails.OrderDetailsID IS NULL)
  AND (tbl_orderdetails.SaleRentType = 'Re-occurring Sale')", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.MissingSchemaAction = MissingSchemaAction.Add;
                adapter.Fill(dataTable);
            }
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        [field: AccessedThroughProperty("Grid")]
        private SearchableGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private bool IsDemoVersion =>
            DMEWorks.Globals.SerialNumber.IsDemoSerial();
    }
}

