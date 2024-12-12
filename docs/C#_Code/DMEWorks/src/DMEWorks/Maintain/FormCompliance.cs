namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonDelete=false)]
    public class FormCompliance : FormAutoIncrementMaintain
    {
        private IContainer components;
        private object FOrderID;

        public FormCompliance()
        {
            this.InitializeComponent();
            this.cmbCustomer.NewButton = false;
            this.cmbCustomer.EditButton = false;
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_compliance", "tbl_customer", "tbl_company" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.cmbCustomer);
            base.ChangesTracker.Subscribe(this.dtbDeliveryDate);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.FOrderID = DBNull.Value;
            Functions.SetComboBoxValue(this.cmbCustomer, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDeliveryDate, DateTime.Today);
            this.ComplianceNotes.ClearGrid();
            this.ComplianceItems.ClearGrid();
        }

        private void ComplianceItems_Changed(object sender, EventArgs e)
        {
            base.OnObjectChanged(sender);
        }

        private void ComplianceNotes_Changed(object sender, EventArgs e)
        {
            base.OnObjectChanged(sender);
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

        private int? GetComplianceID(int OrderID)
        {
            int? nullable;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT ID FROM tbl_compliance WHERE OrderID = :OrderID";
                    command.Parameters.Add("OrderID", MySqlType.Int).Value = OrderID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        nullable = !reader.Read() ? null : new int?(Convert.ToInt32(reader["ID"]));
                    }
                }
            }
            return nullable;
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbCustomer, "tbl_customer", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblCustomer = new Label();
            this.cmbCustomer = new Combobox();
            this.lblDeliveryDate = new Label();
            this.dtbDeliveryDate = new UltraDateTimeEditor();
            this.ComplianceNotes = new ControlComplianceNotes();
            this.ComplianceItems = new ControlComplianceItems();
            this.tcDetails = new TabControl();
            this.tpInventoryItems = new TabPage();
            this.tpDates = new TabPage();
            this.pnlHeader = new Panel();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            this.tcDetails.SuspendLayout();
            this.tpInventoryItems.SuspendLayout();
            this.tpDates.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.tcDetails);
            base.tpWorkArea.Controls.Add(this.pnlHeader);
            base.tpWorkArea.Size = new Size(600, 0x18a);
            this.lblCustomer.Location = new Point(8, 8);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(0x48, 0x15);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomer.Location = new Point(0x58, 8);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0x127, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.lblDeliveryDate.Location = new Point(8, 0x20);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new Size(0x48, 0x15);
            this.lblDeliveryDate.TabIndex = 2;
            this.lblDeliveryDate.Text = "Delivery Date";
            this.lblDeliveryDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDeliveryDate.Location = new Point(0x58, 0x20);
            this.dtbDeliveryDate.Name = "dtbDeliveryDate";
            this.dtbDeliveryDate.Size = new Size(0x68, 0x15);
            this.dtbDeliveryDate.TabIndex = 3;
            this.ComplianceNotes.AllowState = AllowStateEnum.AllowAll;
            this.ComplianceNotes.Dock = DockStyle.Fill;
            this.ComplianceNotes.Location = new Point(0, 0);
            this.ComplianceNotes.Name = "ComplianceNotes";
            this.ComplianceNotes.Size = new Size(0x250, 0x138);
            this.ComplianceNotes.TabIndex = 0;
            this.ComplianceItems.Dock = DockStyle.Fill;
            this.ComplianceItems.Location = new Point(0, 0);
            this.ComplianceItems.Name = "ComplianceItems";
            this.ComplianceItems.Size = new Size(0x250, 0x138);
            this.ComplianceItems.TabIndex = 0;
            this.tcDetails.Controls.Add(this.tpInventoryItems);
            this.tcDetails.Controls.Add(this.tpDates);
            this.tcDetails.Dock = DockStyle.Fill;
            this.tcDetails.Location = new Point(0, 0x38);
            this.tcDetails.Name = "tcDetails";
            this.tcDetails.SelectedIndex = 0;
            this.tcDetails.Size = new Size(600, 0x152);
            this.tcDetails.TabIndex = 1;
            this.tpInventoryItems.Controls.Add(this.ComplianceItems);
            this.tpInventoryItems.Location = new Point(4, 0x16);
            this.tpInventoryItems.Name = "tpInventoryItems";
            this.tpInventoryItems.Size = new Size(0x250, 0x138);
            this.tpInventoryItems.TabIndex = 0;
            this.tpInventoryItems.Text = "Inventory Items";
            this.tpDates.Controls.Add(this.ComplianceNotes);
            this.tpDates.Location = new Point(4, 0x16);
            this.tpDates.Name = "tpDates";
            this.tpDates.Size = new Size(0x250, 0x138);
            this.tpDates.TabIndex = 1;
            this.tpDates.Text = "Dates and Notes";
            this.pnlHeader.Controls.Add(this.lblDeliveryDate);
            this.pnlHeader.Controls.Add(this.cmbCustomer);
            this.pnlHeader.Controls.Add(this.lblCustomer);
            this.pnlHeader.Controls.Add(this.dtbDeliveryDate);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(600, 0x38);
            this.pnlHeader.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x260, 0x1cd);
            base.Name = "FormCompliance";
            this.Text = "Form Compliance";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            this.tcDetails.ResumeLayout(false);
            this.tpInventoryItems.ResumeLayout(false);
            this.tpDates.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected void LoadFromOrder(int OrderID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT ID as OrderID, CustomerID, DeliveryDate FROM tbl_order WHERE ID = :OrderID";
                    command.Parameters.Add("OrderID", MySqlType.Int).Value = OrderID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = DBNull.Value;
                            this.FOrderID = reader["OrderID"];
                            Functions.SetComboBoxValue(this.cmbCustomer, reader["CustomerID"]);
                            Functions.SetDateBoxValue(this.dtbDeliveryDate, reader["DeliveryDate"]);
                        }
                        else
                        {
                            this.ClearObject();
                            return;
                        }
                    }
                }
                this.ComplianceNotes.ClearGrid();
                this.ComplianceItems.LoadGridFromOrder(connection, OrderID);
            }
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                bool flag;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_compliance WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.FOrderID = reader["OrderID"];
                            Functions.SetComboBoxValue(this.cmbCustomer, reader["CustomerID"]);
                            Functions.SetDateBoxValue(this.dtbDeliveryDate, reader["DeliveryDate"]);
                            goto TR_000C;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
                return flag;
            TR_000C:
                this.ComplianceNotes.LoadGrid(connection, ID);
                this.ComplianceItems.LoadGrid(connection, ID);
                return true;
            }
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_compliance" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected void ProcessParameter_OrderID_ID(FormParameters Params)
        {
            if (Params != null)
            {
                if (!Params.ContainsKey("OrderID"))
                {
                    if (Params.ContainsKey("ID"))
                    {
                        base.OpenObject(Params["ID"]);
                    }
                }
                else
                {
                    int? nullable = NullableConvert.ToInt32(Params["OrderID"]);
                    if (nullable != null)
                    {
                        int? complianceID = this.GetComplianceID(nullable.Value);
                        if (complianceID != null)
                        {
                            base.OpenObject(complianceID.Value);
                        }
                        else
                        {
                            this.LoadFromOrder(Conversions.ToInteger(Params["OrderID"]));
                        }
                    }
                }
            }
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    object obj2 = !(this.FOrderID is int) ? ((object) DBNull.Value) : ((object) Conversions.ToInteger(this.FOrderID));
                    command.Parameters.Add("OrderID", MySqlType.Int).Value = obj2;
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = this.cmbCustomer.SelectedValue;
                    command.Parameters.Add("DeliveryDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbDeliveryDate);
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_compliance", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_compliance"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_compliance");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                    this.ComplianceNotes.SaveGrid(connection, ID);
                    this.ComplianceItems.SaveGrid(connection, ID);
                }
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT tbl_compliance.ID,
       CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName,
       tbl_compliance.DeliveryDate
FROM ((tbl_compliance
       LEFT JOIN tbl_customer ON tbl_customer.ID = tbl_compliance.CustomerID)
      LEFT JOIN tbl_company ON tbl_company.ID = 1)
WHERE ({IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1"})
  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))
ORDER BY tbl_customer.LastName, tbl_customer.FirstName", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 40);
            appearance.AddTextColumn("CustomerName", "Customer Name", 120);
            appearance.AddTextColumn("DeliveryDate", "Delivery Date", 80);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__50-0 e$__- = new _Closure$__50-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            this.ProcessParameter_OrderID_ID(Params);
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (!(this.cmbCustomer.SelectedValue is int))
            {
                base.ValidationErrors.SetError(this.cmbCustomer, "You must select customer");
            }
            if (!(Functions.GetDateBoxValue(this.dtbDeliveryDate) is DateTime))
            {
                base.ValidationErrors.SetError(this.dtbDeliveryDate, "You must input delivery date");
            }
        }

        [field: AccessedThroughProperty("lblCustomer")]
        private Label lblCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDeliveryDate")]
        private Label lblDeliveryDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDeliveryDate")]
        private UltraDateTimeEditor dtbDeliveryDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ComplianceNotes")]
        private ControlComplianceNotes ComplianceNotes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ComplianceItems")]
        private ControlComplianceItems ComplianceItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tcDetails")]
        private TabControl tcDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpInventoryItems")]
        private TabPage tpInventoryItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpDates")]
        private TabPage tpDates { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlHeader")]
        private Panel pnlHeader { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private static bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        protected override bool IsNew
        {
            get => 
                base.IsNew;
            set
            {
                base.IsNew = value;
                this.cmbCustomer.Enabled = this.IsNew;
                this.dtbDeliveryDate.Enabled = this.IsNew;
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__50-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

