namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormRentalPickup : DmeForm
    {
        private IContainer components;
        private Mode F_RunningMode = ((Mode) 0);
        private const string CrLf = "\r\n";
        private bool FLoadGridEnabled = true;

        public FormRentalPickup()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
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

        public string GetQuery()
        {
            string str = IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1";
            int? nullable = NullableConvert.ToInt32(this.cmbCustomer.SelectedValue);
            string str2 = (nullable == null) ? "1 = 1" : ("tbl_customer.ID = " + nullable.Value.ToString());
            string str3 = (this.RunningMode != Mode.Pickup) ? ((this.RunningMode != Mode.Confirm) ? ((this.RunningMode != Mode.PickupConfirm) ? "1 != 1" : "tbl_orderdetails.State IN ('New', 'Approved','Pickup')") : "tbl_orderdetails.State IN ('New', 'Pickup')") : "tbl_orderdetails.State IN ('New', 'Approved')";
            string[] textArray1 = new string[] { "SELECT\r\n  tbl_orderdetails.ID\r\n, tbl_orderdetails.OrderID\r\n, CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName\r\n, tbl_customer.DateOfBirth\r\n, tbl_customer.AccountNumber\r\n, tbl_orderdetails.BillingCode\r\n, tbl_inventoryitem.Name as InventoryItem\r\n, tbl_orderdetails.State\r\n, tbl_orderdetails.EndDate\r\n, tbl_orderdetails.BillingMonth\r\n, tbl_orderdetails.DOSFrom\r\n, IFNULL(tbl_orderdetails.BilledQuantity, 0) as Quantity\r\n, tbl_serial.SerialNumber\r\nFROM tbl_orderdetails\r\n     LEFT JOIN tbl_serial ON tbl_serial.ID = tbl_orderdetails.SerialID\r\n     LEFT JOIN tbl_inventoryitem ON tbl_orderdetails.InventoryItemID = tbl_inventoryitem.ID\r\n     INNER JOIN tbl_order ON tbl_orderdetails.OrderID = tbl_order.ID\r\n                         AND tbl_orderdetails.CustomerID = tbl_order.CustomerID\r\n     LEFT JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_company ON tbl_company.ID = 1\r\n     LEFT JOIN tbl_shippingmethod ON tbl_order.ShippingMethodID = tbl_shippingmethod.ID\r\nWHERE (tbl_order.Approved = 1)\r\n  AND (tbl_order.OrderDate IS NOT NULL)\r\n  AND (tbl_order.BillDate IS NOT NULL)\r\n  AND (tbl_orderdetails.SaleRentType IN ('Capped Rental', 'Medicare Oxygen Rental', 'Parental Capped Rental', 'Rent to Purchase', 'Monthly Rental', 'One Time Rental'))\r\n  AND (tbl_orderdetails.EndDate IS NULL)\r\n  AND (", str3, ")\r\n  AND (", str, ")\r\n  AND (", str2, ")\r\nORDER BY tbl_orderdetails.OrderID DESC, tbl_orderdetails.ID DESC\r\n" };
            return string.Concat(textArray1);
        }

        private void Grid_GridKeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Space))
            {
                DataRow[] source = this.Grid.GetSelectedRows().GetDataRows().ToArray<DataRow>();
                if (source.Length != 0)
                {
                    source.Count<DataRow>();
                    bool flag = true;
                    if (source.All<DataRow>((_Closure$__.$I76-0 == null) ? (_Closure$__.$I76-0 = new Func<DataRow, bool>(_Closure$__.$I._Lambda$__76-0)) : _Closure$__.$I76-0))
                    {
                        flag = false;
                    }
                    DataRow[] rowArray2 = source;
                    for (int i = 0; i < rowArray2.Length; i++)
                    {
                        rowArray2[i]["Selected"] = flag;
                    }
                }
            }
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormRentalPickup));
            this.Panel1 = new Panel();
            this.lblCustomer = new Label();
            this.cmbCustomer = new Combobox();
            this.Grid = new FilteredGrid();
            this.pnlDate = new Panel();
            this.dtbPickupDate = new UltraDateTimeEditor();
            this.lblPickupDate = new Label();
            this.ToolStrip1 = new ToolStrip();
            this.tsbPickup = new ToolStripButton();
            this.tsbConfirm = new ToolStripButton();
            this.tsbPickupConfirm = new ToolStripButton();
            this.ToolStripSeparator1 = new ToolStripSeparator();
            this.tsbProcess = new ToolStripButton();
            this.Panel1.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Panel1.Controls.Add(this.lblCustomer);
            this.Panel1.Controls.Add(this.cmbCustomer);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0x19);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x3b0, 0x20);
            this.Panel1.TabIndex = 0;
            this.lblCustomer.Location = new Point(8, 8);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(0x48, 0x15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbCustomer.Location = new Point(0x58, 8);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0x350, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x39);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x3b0, 0x174);
            this.Grid.TabIndex = 3;
            this.pnlDate.Controls.Add(this.dtbPickupDate);
            this.pnlDate.Controls.Add(this.lblPickupDate);
            this.pnlDate.Dock = DockStyle.Bottom;
            this.pnlDate.Location = new Point(0, 0x1ad);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new Size(0x3b0, 0x20);
            this.pnlDate.TabIndex = 1;
            this.dtbPickupDate.Location = new Point(0x58, 8);
            this.dtbPickupDate.Name = "dtbPickupDate";
            this.dtbPickupDate.Size = new Size(0x5c, 0x15);
            this.dtbPickupDate.TabIndex = 1;
            this.lblPickupDate.Location = new Point(8, 8);
            this.lblPickupDate.Name = "lblPickupDate";
            this.lblPickupDate.Size = new Size(0x48, 0x15);
            this.lblPickupDate.TabIndex = 0;
            this.lblPickupDate.Text = "Pickup Date";
            this.lblPickupDate.TextAlign = ContentAlignment.MiddleRight;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbPickup, this.tsbConfirm, this.tsbPickupConfirm, this.ToolStripSeparator1, this.tsbProcess };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x3b0, 0x19);
            this.ToolStrip1.TabIndex = 5;
            this.tsbPickup.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbPickup.Image = (Image) manager.GetObject("tsbPickup.Image");
            this.tsbPickup.ImageTransparentColor = Color.Magenta;
            this.tsbPickup.Name = "tsbPickup";
            this.tsbPickup.Size = new Size(0x2f, 0x16);
            this.tsbPickup.Text = "Pickup";
            this.tsbConfirm.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbConfirm.Image = (Image) manager.GetObject("tsbConfirm.Image");
            this.tsbConfirm.ImageTransparentColor = Color.Magenta;
            this.tsbConfirm.Name = "tsbConfirm";
            this.tsbConfirm.Size = new Size(0x37, 0x16);
            this.tsbConfirm.Text = "Confirm";
            this.tsbPickupConfirm.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbPickupConfirm.Image = (Image) manager.GetObject("tsbPickupConfirm.Image");
            this.tsbPickupConfirm.ImageTransparentColor = Color.Magenta;
            this.tsbPickupConfirm.Name = "tsbPickupConfirm";
            this.tsbPickupConfirm.Size = new Size(0x6b, 0x16);
            this.tsbPickupConfirm.Text = "Pickup && Confirm";
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new Size(6, 0x19);
            this.tsbProcess.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbProcess.Image = (Image) manager.GetObject("tsbProcess.Image");
            this.tsbProcess.ImageTransparentColor = Color.Magenta;
            this.tsbProcess.Name = "tsbProcess";
            this.tsbProcess.Size = new Size(0x33, 0x16);
            this.tsbProcess.Text = "Process";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3b0, 0x1cd);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.pnlDate);
            base.Controls.Add(this.Panel1);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormRentalPickup";
            this.Text = "Pickup, Confirm";
            this.Panel1.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.AllowEdit = true;
            Appearance.Columns.Clear();
            Appearance.MultiSelect = true;
            Appearance.AddBoolColumn("Selected", "...", 30, Appearance.BoolStyle()).ReadOnly = false;
            Appearance.AddTextColumn("ID", "#", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("OrderID", "Order#", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("CustomerName", "Customer", 100);
            Appearance.AddTextColumn("DateOfBirth", "Birthday", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("AccountNumber", "Account#", 60);
            Appearance.AddTextColumn("BillingCode", "B. Code", 60);
            Appearance.AddTextColumn("InventoryItem", "Inv. Item", 80);
            Appearance.AddTextColumn("SerialNumber", "Serial#", 60);
            Appearance.AddTextColumn("State", "State", 60);
            Appearance.AddTextColumn("EndDate", "End Date", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("BillingMonth", "Month", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("DOSFrom", "DOSFrom", 80, Appearance.DateStyle());
            Appearance.AddTextColumn("Quantity", "Qty", 50, Appearance.IntegerStyle());
        }

        public static bool IsSelected(DataRow row) => 
            Convert.ToBoolean(row["Selected"]);

        [HandleDatabaseChanged(new string[] { "tbl_order", "tbl_orderdetails" })]
        private void LoadGrid()
        {
            if (this.FLoadGridEnabled)
            {
                DataTable dataTable = new DataTable("tbl_items");
                DataColumn column = new DataColumn("Selected", typeof(bool));
                column.AllowDBNull = false;
                column.DefaultValue = false;
                dataTable.Columns.Add(column);
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(this.GetQuery(), connection))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.MissingSchemaAction = MissingSchemaAction.Add;
                        adapter.Fill(dataTable);
                    }
                }
                this.Grid.GridSource = dataTable.ToGridSource();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.RunningMode = Mode.Pickup;
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

        private void tsbConfirm_Click(object sender, EventArgs e)
        {
            this.RunningMode = Mode.Confirm;
        }

        private void tsbPickup_Click(object sender, EventArgs e)
        {
            this.RunningMode = Mode.Pickup;
        }

        private void tsbPickupConfirm_Click(object sender, EventArgs e)
        {
            this.RunningMode = Mode.PickupConfirm;
        }

        private void tsbProcess_Click(object sender, EventArgs e)
        {
            try
            {
                int[] numArray = this.Grid.GetRows().GetDataRows().Where<DataRow>(((_Closure$__.$I75-0 == null) ? (_Closure$__.$I75-0 = new Func<DataRow, bool>(_Closure$__.$I._Lambda$__75-0)) : _Closure$__.$I75-0)).Select<DataRow, int>(((_Closure$__.$I75-1 == null) ? (_Closure$__.$I75-1 = new Func<DataRow, int>(_Closure$__.$I._Lambda$__75-1)) : _Closure$__.$I75-1)).ToArray<int>();
                if (numArray.Length != 0)
                {
                    Mode runningMode = this.RunningMode;
                    DateTime? nullable = NullableConvert.ToDate(this.dtbPickupDate.Value);
                    if (((runningMode == Mode.Confirm) || (runningMode == Mode.PickupConfirm)) && (nullable == null))
                    {
                        throw new UserNotifyException("Please enter pickup date");
                    }
                    using (MySqlConnection connection = Globals.CreateConnection())
                    {
                        using (MySqlCommand command = connection.CreateCommand())
                        {
                            command.Parameters.Add(new MySqlParameter("ID", MySqlType.Int));
                            MySqlParameter parameter1 = new MySqlParameter("EndDate", MySqlType.Date);
                            parameter1.Value = NullableConvert.ToDb(nullable);
                            command.Parameters.Add(parameter1);
                            switch (this.RunningMode)
                            {
                                case Mode.Pickup:
                                    command.CommandText = "UPDATE tbl_orderdetails as od\r\n       INNER JOIN tbl_order as o ON o.CustomerID = od.CustomerID\r\n                                AND o.ID = od.OrderID\r\nSET od.State = 'Pickup'\r\nWHERE (od.ID = :ID)\r\n  AND (od.SaleRentType IN ('Capped Rental', 'Medicare Oxygen Rental', 'Parental Capped Rental', 'Rent to Purchase', 'Monthly Rental', 'One Time Rental'))\r\n  AND (od.State IN ('New', 'Approved'))\r\n  AND (od.EndDate IS NULL)\r\n  AND (o.Approved = 1)\r\n  AND (o.OrderDate IS NOT NULL)\r\n  AND (o.BillDate IS NOT NULL)\r\n";
                                    goto TR_000D;

                                case Mode.Confirm:
                                    command.CommandText = "UPDATE tbl_orderdetails as od\r\n       INNER JOIN tbl_order as o ON o.CustomerID = od.CustomerID\r\n                                AND o.ID = od.OrderID\r\nSET od.State = 'Pickup'\r\n  , od.EndDate = :EndDate\r\nWHERE (od.ID = :ID)\r\n  AND (od.SaleRentType IN ('Capped Rental', 'Medicare Oxygen Rental', 'Parental Capped Rental', 'Rent to Purchase', 'Monthly Rental', 'One Time Rental'))\r\n  AND (od.State IN ('New', 'Pickup'))\r\n  AND (od.EndDate IS NULL)\r\n  AND (o.Approved = 1)\r\n  AND (o.OrderDate IS NOT NULL)\r\n  AND (o.BillDate IS NOT NULL)\r\n";
                                    goto TR_000D;

                                case Mode.PickupConfirm:
                                    command.CommandText = "UPDATE tbl_orderdetails as od\r\n       INNER JOIN tbl_order as o ON o.CustomerID = od.CustomerID\r\n                                AND o.ID = od.OrderID\r\nSET od.State = 'Pickup'\r\n  , od.EndDate = :EndDate\r\nWHERE (od.ID = :ID)\r\n  AND (od.SaleRentType IN ('Capped Rental', 'Medicare Oxygen Rental', 'Parental Capped Rental', 'Rent to Purchase', 'Monthly Rental', 'One Time Rental'))\r\n  AND (od.State IN ('New', 'Approved', 'Pickup'))\r\n  AND (od.EndDate IS NULL)\r\n  AND (o.Approved = 1)\r\n  AND (o.OrderDate IS NOT NULL)\r\n  AND (o.BillDate IS NOT NULL)\r\n";
                                    goto TR_000D;

                                default:
                                    break;
                            }
                            return;
                        TR_000D:
                            connection.Open();
                            foreach (int num2 in numArray)
                            {
                                command.Parameters["ID"].Value = num2;
                                command.ExecuteNonQuery();
                            }
                            goto TR_0009;
                        }
                    }
                }
                return;
            TR_0009:
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

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlDate")]
        private Panel pnlDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPickupDate")]
        private Label lblPickupDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbPickupDate")]
        private UltraDateTimeEditor dtbPickupDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbPickup")]
        private ToolStripButton tsbPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbConfirm")]
        private ToolStripButton tsbConfirm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbPickupConfirm")]
        private ToolStripButton tsbPickupConfirm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStripSeparator1")]
        private ToolStripSeparator ToolStripSeparator1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbProcess")]
        private ToolStripButton tsbProcess { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public Mode RunningMode
        {
            get => 
                this.F_RunningMode;
            set
            {
                if (this.F_RunningMode != value)
                {
                    this.F_RunningMode = value;
                    switch (this.F_RunningMode)
                    {
                        case Mode.Pickup:
                            this.tsbPickup.Checked = true;
                            this.tsbConfirm.Checked = false;
                            this.tsbPickupConfirm.Checked = false;
                            this.pnlDate.Visible = false;
                            break;

                        case Mode.Confirm:
                            this.tsbPickup.Checked = false;
                            this.tsbConfirm.Checked = true;
                            this.tsbPickupConfirm.Checked = false;
                            this.pnlDate.Visible = true;
                            break;

                        case Mode.PickupConfirm:
                            this.tsbPickup.Checked = false;
                            this.tsbConfirm.Checked = false;
                            this.tsbPickupConfirm.Checked = true;
                            this.pnlDate.Visible = true;
                            break;

                        default:
                            this.tsbPickup.Checked = false;
                            this.tsbConfirm.Checked = false;
                            this.tsbPickupConfirm.Checked = false;
                            this.pnlDate.Visible = false;
                            break;
                    }
                    this.LoadGrid();
                }
            }
        }

        private static bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormRentalPickup._Closure$__ $I = new FormRentalPickup._Closure$__();
            public static Func<DataRow, bool> $I75-0;
            public static Func<DataRow, int> $I75-1;
            public static Func<DataRow, bool> $I76-0;

            internal bool _Lambda$__75-0(DataRow r) => 
                FormRentalPickup.IsSelected(r);

            internal int _Lambda$__75-1(DataRow r) => 
                Convert.ToInt32(r["ID"]);

            internal bool _Lambda$__76-0(DataRow r) => 
                FormRentalPickup.IsSelected(r);
        }

        public enum Mode
        {
            Pickup = 1,
            Confirm = 2,
            PickupConfirm = 3
        }
    }
}

