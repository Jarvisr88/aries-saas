namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormCompliancePopup : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private const string Key_ShowAtStartup = "FormCompliancePopup.ShowAtStartup";

        public FormCompliancePopup()
        {
            this.InitializeComponent();
            this.InitializeGridStyle(this.Grid.Appearance);
            this.chbShowAtStartup.Checked = ShowAtStartup;
        }

        private void chbShowAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            ShowAtStartup = this.chbShowAtStartup.Checked;
        }

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
                    DataColumn column = dataRow.Table.Columns["ID"];
                    if (column != null)
                    {
                        int? nullable = NullableConvert.ToInt32(dataRow[column]);
                        if (nullable != null)
                        {
                            FormParameters @params = new FormParameters("ID", nullable);
                            ClassGlobalObjects.ShowForm(FormFactories.FormCompliance(), @params);
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
            this.chbShowAtStartup = new CheckBox();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x304, 0x10d);
            this.Grid.TabIndex = 0;
            this.chbShowAtStartup.Dock = DockStyle.Bottom;
            this.chbShowAtStartup.Location = new Point(0, 0x10d);
            this.chbShowAtStartup.Name = "chbShowAtStartup";
            this.chbShowAtStartup.Size = new Size(0x304, 0x18);
            this.chbShowAtStartup.TabIndex = 1;
            this.chbShowAtStartup.Text = "Show At Startup";
            this.chbShowAtStartup.UseVisualStyleBackColor = true;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x304, 0x125);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.chbShowAtStartup);
            base.Name = "FormCompliancePopup";
            this.Text = "Compliance Popup";
            base.ResumeLayout(false);
        }

        private void InitializeGridStyle(SearchableGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "#", 40, Appearance.IntegerStyle());
            Appearance.AddTextColumn("CustomerName", "Customer Name", 120);
            Appearance.AddTextColumn("InventoryItemName", "Inventory Item", 180);
            Appearance.AddTextColumn("Date", "Comp. Date", 0x4b, Appearance.DateStyle());
            Appearance.AddTextColumn("AssignedTo", "Assigned To", 0x4b);
            Appearance.AddTextColumn("DeliveryDate", "Deliv. Date", 0x4b, Appearance.DateStyle());
            Appearance.AddTextColumn("Notes", "Notes", 150);
        }

        [HandleDatabaseChanged(new string[] { "tbl_compliance", "tbl_compliance_items", "tbl_compliance_notes", "tbl_inventoryitem", "tbl_customer", "tbl_company", "tbl_user" })]
        private void LoadGrid()
        {
            string selectCommandText = "SELECT\r\n  c.ID\r\n, c.DeliveryDate\r\n, CONCAT(cr.LastName, ', ', cr.FirstName) as CustomerName\r\n, ii.Name as InventoryItemName\r\n, cn.Date\r\n, tbl_user.Login as AssignedTo\r\n, cn.Notes\r\nFROM tbl_compliance as c\r\n     INNER JOIN tbl_compliance_notes as cn ON cn.ComplianceID = c.ID\r\n     LEFT  JOIN tbl_compliance_items as ci ON ci.ComplianceID = c.ID\r\n     LEFT  JOIN tbl_inventoryitem as ii ON ii.ID = ci.InventoryItemID\r\n     INNER JOIN tbl_customer as cr ON cr.ID = c.CustomerID\r\n     INNER JOIN tbl_company ON tbl_company.ID = 1\r\n     LEFT  JOIN tbl_user ON tbl_user.ID = cn.AssignedToUserID\r\nWHERE (cn.Done = 0)\r\n  AND (cn.Date < DATE_ADD(CURRENT_DATE(), INTERVAL 86400 SECOND))\r\n  AND ((tbl_company.Show_InactiveCustomers = 1) OR (cr.InactiveDate IS NULL) OR (Now() < cr.InactiveDate))";
            if (this.IsDemoVersion)
            {
                selectCommandText = selectCommandText + "\r\n  AND (cr.ID BETWEEN 1 and 50)";
            }
            if (!Permissions.FormCompliancePopup_Unrestricted.Allow_VIEW)
            {
                selectCommandText = selectCommandText + "\r\n" + string.Format("  AND (cn.AssignedToUserID IS NULL OR cn.AssignedToUserID = {0} OR cn.CreatedByUserID = {0})", Globals.CompanyUserID);
            }
            DataTable dataTable = new DataTable("Compliance");
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommandText, ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SafeInvoke(new Action(this.LoadGrid));
        }

        [field: AccessedThroughProperty("chbShowAtStartup")]
        private CheckBox chbShowAtStartup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Grid")]
        private SearchableGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        public static bool ShowAtStartup
        {
            get => 
                UserSettings.GetBool("FormCompliancePopup.ShowAtStartup", true);
            set => 
                UserSettings.SetBool("FormCompliancePopup.ShowAtStartup", value);
        }
    }
}

