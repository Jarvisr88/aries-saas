namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class ControlCustomerInsurance : ControlDetails
    {
        private IContainer components;

        public ControlCustomerInsurance()
        {
            this.InitializeComponent();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DataTable tableSource = this.Grid.GetTableSource<DataTable>();
            if (tableSource != null)
            {
                DataRow dataRow = this.Grid.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    DataRow[] rowArray = tableSource.Select($"[Rank] > {dataRow["Rank"]}", "[Rank] ASC");
                    if ((rowArray != null) && (rowArray.Length != 0))
                    {
                        object obj2 = dataRow["Rank"];
                        dataRow["Rank"] = rowArray[0]["Rank"];
                        rowArray[0]["Rank"] = obj2;
                    }
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            DataTable tableSource = this.Grid.GetTableSource<DataTable>();
            if (tableSource != null)
            {
                DataRow dataRow = this.Grid.CurrentRow.GetDataRow();
                if (dataRow != null)
                {
                    DataRow[] rowArray = tableSource.Select($"[Rank] < {dataRow["Rank"]}", "[Rank] DESC");
                    if ((rowArray != null) && (rowArray.Length != 0))
                    {
                        object obj2 = dataRow["Rank"];
                        dataRow["Rank"] = rowArray[0]["Rank"];
                        rowArray[0]["Rank"] = obj2;
                    }
                }
            }
        }

        public void ClearGrid()
        {
            base.CloseDialogs();
            TableCustomerInsurance table = this.GetTable();
            table.Clear();
            table.AcceptChanges();
        }

        protected override FormDetails CreateDialog(object param) => 
            base.AddDialog(new FormCustomerInsurance(this));

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableCustomerInsurance();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected static void GenerateDeleteCommand_CustomerInsurance(MySqlCommand DeleteCommand, int CustomerID)
        {
            DeleteCommand.Parameters.Clear();
            DeleteCommand.Parameters.Add("CustomerID", MySqlType.Int, 4).Value = CustomerID;
            DeleteCommand.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            DeleteCommand.GenerateDeleteCommand("tbl_customer_insurance");
        }

        protected static void GenerateInsertCommand_CustomerInsurance(MySqlCommand InsertCommand, int CustomerID)
        {
            InsertCommand.Parameters.Clear();
            InsertCommand.Parameters.Add("Address1", MySqlType.VarChar, 40, "Address1");
            InsertCommand.Parameters.Add("Address2", MySqlType.VarChar, 40, "Address2");
            InsertCommand.Parameters.Add("City", MySqlType.VarChar, 0x19, "City");
            InsertCommand.Parameters.Add("State", MySqlType.VarChar, 2, "State");
            InsertCommand.Parameters.Add("Zip", MySqlType.VarChar, 10, "Zip");
            InsertCommand.Parameters.Add("Basis", MySqlType.VarChar, 7, "Basis");
            InsertCommand.Parameters.Add("DateofBirth", MySqlType.Date, 0, "DateofBirth");
            InsertCommand.Parameters.Add("Gender", MySqlType.VarChar, 6, "Gender");
            InsertCommand.Parameters.Add("GroupNumber", MySqlType.VarChar, 50, "GroupNumber");
            InsertCommand.Parameters.Add("InactiveDate", MySqlType.Date, 0, "InactiveDate");
            InsertCommand.Parameters.Add("InsuranceCompanyID", MySqlType.Int, 4, "InsuranceCompanyID");
            InsertCommand.Parameters.Add("InsuranceType", MySqlType.VarChar, 2, "InsuranceType");
            InsertCommand.Parameters.Add("FirstName", MySqlType.VarChar, 0x19, "FirstName");
            InsertCommand.Parameters.Add("LastName", MySqlType.VarChar, 30, "LastName");
            InsertCommand.Parameters.Add("MiddleName", MySqlType.VarChar, 1, "MiddleName");
            InsertCommand.Parameters.Add("Suffix", MySqlType.VarChar, 4, "Suffix");
            InsertCommand.Parameters.Add("Employer", MySqlType.VarChar, 50, "Employer");
            InsertCommand.Parameters.Add("Mobile", MySqlType.VarChar, 50, "Mobile");
            InsertCommand.Parameters.Add("PaymentPercent", MySqlType.Int, 4, "PaymentPercent");
            InsertCommand.Parameters.Add("Phone", MySqlType.VarChar, 50, "Phone");
            InsertCommand.Parameters.Add("PolicyNumber", MySqlType.VarChar, 50, "PolicyNumber");
            InsertCommand.Parameters.Add("Rank", MySqlType.Int, 4, "Rank");
            InsertCommand.Parameters.Add("RelationshipCode", MySqlType.Char, 2, "RelationshipCode");
            InsertCommand.Parameters.Add("RequestEligibility", MySqlType.Bit, 1, "RequestEligibility");
            InsertCommand.Parameters.Add("RequestEligibilityOn", MySqlType.Date, 0, "RequestEligibilityOn");
            InsertCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            InsertCommand.Parameters.Add("CustomerID", MySqlType.Int, 4).Value = CustomerID;
            InsertCommand.GenerateInsertCommand("tbl_customer_insurance");
        }

        protected static void GenerateUpdateCommand_CustomerInsurance(MySqlCommand UpdateCommand, int CustomerID)
        {
            UpdateCommand.Parameters.Clear();
            UpdateCommand.Parameters.Add("Address1", MySqlType.VarChar, 40, "Address1");
            UpdateCommand.Parameters.Add("Address2", MySqlType.VarChar, 40, "Address2");
            UpdateCommand.Parameters.Add("City", MySqlType.VarChar, 0x19, "City");
            UpdateCommand.Parameters.Add("State", MySqlType.VarChar, 2, "State");
            UpdateCommand.Parameters.Add("Zip", MySqlType.VarChar, 10, "Zip");
            UpdateCommand.Parameters.Add("Basis", MySqlType.VarChar, 7, "Basis");
            UpdateCommand.Parameters.Add("DateofBirth", MySqlType.Date, 0, "DateofBirth");
            UpdateCommand.Parameters.Add("Gender", MySqlType.VarChar, 6, "Gender");
            UpdateCommand.Parameters.Add("GroupNumber", MySqlType.VarChar, 50, "GroupNumber");
            UpdateCommand.Parameters.Add("InactiveDate", MySqlType.Date, 0, "InactiveDate");
            UpdateCommand.Parameters.Add("InsuranceCompanyID", MySqlType.Int, 4, "InsuranceCompanyID");
            UpdateCommand.Parameters.Add("InsuranceType", MySqlType.VarChar, 2, "InsuranceType");
            UpdateCommand.Parameters.Add("FirstName", MySqlType.VarChar, 0x19, "FirstName");
            UpdateCommand.Parameters.Add("LastName", MySqlType.VarChar, 30, "LastName");
            UpdateCommand.Parameters.Add("MiddleName", MySqlType.VarChar, 1, "MiddleName");
            UpdateCommand.Parameters.Add("Suffix", MySqlType.VarChar, 4, "Suffix");
            UpdateCommand.Parameters.Add("Employer", MySqlType.VarChar, 50, "Employer");
            UpdateCommand.Parameters.Add("Mobile", MySqlType.VarChar, 50, "Mobile");
            UpdateCommand.Parameters.Add("PaymentPercent", MySqlType.Int, 4, "PaymentPercent");
            UpdateCommand.Parameters.Add("Phone", MySqlType.VarChar, 50, "Phone");
            UpdateCommand.Parameters.Add("PolicyNumber", MySqlType.VarChar, 50, "PolicyNumber");
            UpdateCommand.Parameters.Add("Rank", MySqlType.Int, 4, "Rank");
            UpdateCommand.Parameters.Add("RelationshipCode", MySqlType.Char, 2, "RelationshipCode");
            UpdateCommand.Parameters.Add("RequestEligibility", MySqlType.Bit, 1, "RequestEligibility");
            UpdateCommand.Parameters.Add("RequestEligibilityOn", MySqlType.Date, 0, "RequestEligibilityOn");
            UpdateCommand.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            UpdateCommand.Parameters.Add("CustomerID", MySqlType.Int, 4).Value = CustomerID;
            UpdateCommand.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            string[] whereParameters = new string[] { "CustomerID", "ID" };
            UpdateCommand.GenerateUpdateCommand("tbl_customer_insurance", whereParameters);
        }

        protected TableCustomerInsurance GetTable() => 
            (TableCustomerInsurance) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ResourceManager manager = new ResourceManager(typeof(ControlCustomerInsurance));
            this.btnDown = new Button();
            this.btnUp = new Button();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[] { this.btnDown, this.btnUp };
            this.Panel1.Controls.AddRange(controls);
            this.btnDown.FlatStyle = FlatStyle.Flat;
            this.btnDown.Image = (Bitmap) manager.GetObject("btnDown.Image");
            this.btnDown.Location = new Point(4, 0x70);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new Size(0x18, 0x18);
            this.btnDown.TabIndex = 3;
            this.btnUp.FlatStyle = FlatStyle.Flat;
            this.btnUp.Image = (Bitmap) manager.GetObject("btnUp.Image");
            this.btnUp.Location = new Point(4, 80);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new Size(0x18, 0x18);
            this.btnUp.TabIndex = 2;
            base.Name = "ControlCustomerInsurance2";
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("Rank", "Rank", 40);
            Appearance.AddTextColumn("InsuranceCompanyName", "Insurance Company", 160);
            Appearance.AddTextColumn("PolicyNumber", "Policy #", 80);
            Appearance.AddTextColumn("GroupNumber", "Group #", 80);
        }

        public void LoadGrid(MySqlConnection cnn, int CustomerID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT tbl_customer_insurance.*,
       tbl_insurancecompany.Name as InsuranceCompanyName,
       tbl_insurancetype.Description as InsuranceTypeName
FROM ((tbl_customer_insurance
       LEFT JOIN tbl_insurancecompany ON tbl_insurancecompany.ID = tbl_customer_insurance.InsuranceCompanyID)
      LEFT JOIN tbl_insurancetype ON tbl_insurancetype.Code = tbl_customer_insurance.InsuranceType)
WHERE (tbl_customer_insurance.CustomerID = {CustomerID})
ORDER BY tbl_customer_insurance.`Rank`", cnn))
            {
                TableCustomerInsurance table = this.GetTable();
                base.CloseDialogs();
                table.Clear();
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.Fill(table);
                table.AcceptChanges();
            }
        }

        public void SaveGrid(MySqlConnection cnn, int CustomerID)
        {
            TableCustomerInsurance table = this.GetTable();
            TableCustomerInsurance changes = (TableCustomerInsurance) table.GetChanges();
            if (changes != null)
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                try
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                    {
                        adapter.InsertCommand = new MySqlCommand();
                        GenerateInsertCommand_CustomerInsurance(adapter.InsertCommand, CustomerID);
                        adapter.UpdateCommand = new MySqlCommand();
                        GenerateUpdateCommand_CustomerInsurance(adapter.UpdateCommand, CustomerID);
                        adapter.DeleteCommand = new MySqlCommand();
                        GenerateDeleteCommand_CustomerInsurance(adapter.DeleteCommand, CustomerID);
                        using (new DataAdapterEvents(adapter))
                        {
                            adapter.InsertCommand.Connection = cnn;
                            adapter.UpdateCommand.Connection = cnn;
                            adapter.DeleteCommand.Connection = cnn;
                            adapter.ContinueUpdateOnError = false;
                            adapter.Update(changes);
                            table.MergeKeys(changes);
                            table.AcceptChanges();
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand("", cnn))
                    {
                        command.CommandText = $"CALL mir_update_customer_insurance({CustomerID})";
                        command.ExecuteNonQuery();
                        command.CommandText = $"CALL `customer_insurance_fixrank`({CustomerID})";
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    bool flag;
                    if (flag)
                    {
                        cnn.Close();
                    }
                }
            }
        }

        public void ShowDetails(object CustomerInsuranceID)
        {
            try
            {
                DataTable tableSource = this.Grid.GetTableSource<DataTable>();
                if (tableSource != null)
                {
                    foreach (DataRow row in tableSource.Select($"[ID] = {CustomerInsuranceID}", "", DataViewRowState.CurrentRows))
                    {
                        base.EditRow(Conversions.ToInteger(row["RowID"]));
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

        public void ShowMissingInformation(ErrorProvider MissingProvider, bool Show)
        {
            _Closure$__25-0 e$__- = new _Closure$__25-0 {
                $VB$Local_Show = Show
            };
            TableCustomerInsurance table = this.GetTable();
            table.ShowMissingInformation(e$__-.$VB$Local_Show);
            if (!e$__-.$VB$Local_Show || !table.HasErrors)
            {
                MissingProvider.SetError(this, "");
            }
            else
            {
                MissingProvider.SetIconAlignment(this, ErrorIconAlignment.TopRight);
                MissingProvider.SetIconPadding(this, -16);
                MissingProvider.SetError(this, "Some information is missing in customer insurances");
            }
            base.DoForAllDialogs<FormCustomerInsurance>(new Action<FormCustomerInsurance>(e$__-._Lambda$__0));
        }

        [field: AccessedThroughProperty("btnDown")]
        private Button btnDown { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnUp")]
        private Button btnUp { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__25-0
        {
            public bool $VB$Local_Show;

            internal void _Lambda$__0(FormCustomerInsurance d)
            {
                d.ShowMissingInformation(this.$VB$Local_Show);
            }
        }

        private class DataAdapterEvents : MySqlDataAdapterEventsBase
        {
            public DataAdapterEvents(MySqlDataAdapter da) : base(da)
            {
            }

            protected override void ProcessRowUpdated(MySqlRowUpdatedEventArgs e)
            {
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && ((e.RecordsAffected == 1) && (e.Row.Table is ControlCustomerInsurance.TableCustomerInsurance))))
                {
                    ControlCustomerInsurance.TableCustomerInsurance table = (ControlCustomerInsurance.TableCustomerInsurance) e.Row.Table;
                    base.cmdSelectIdentity.Connection = e.Command.Connection;
                    try
                    {
                        base.cmdSelectIdentity.Transaction = e.Command.Transaction;
                        try
                        {
                            e.Row[table.Col_ID] = Conversions.ToInteger(base.cmdSelectIdentity.ExecuteScalar());
                        }
                        finally
                        {
                            base.cmdSelectIdentity.Transaction = null;
                        }
                    }
                    finally
                    {
                        base.cmdSelectIdentity.Connection = null;
                    }
                }
            }
        }

        public class TableCustomerInsurance : ControlDetails.TableDetails
        {
            public DataColumn Col_Address1;
            public DataColumn Col_Address2;
            public DataColumn Col_Basis;
            public DataColumn Col_City;
            public DataColumn Col_State;
            public DataColumn Col_Zip;
            public DataColumn Col_DateofBirth;
            public DataColumn Col_Gender;
            public DataColumn Col_GroupNumber;
            public DataColumn Col_ID;
            public DataColumn Col_InactiveDate;
            public DataColumn Col_InsuranceCompanyID;
            public DataColumn Col_InsuranceCompanyName;
            public DataColumn Col_InsuranceType;
            public DataColumn Col_InsuranceTypeName;
            public DataColumn Col_FirstName;
            public DataColumn Col_LastName;
            public DataColumn Col_MiddleName;
            public DataColumn Col_Suffix;
            public DataColumn Col_Employer;
            public DataColumn Col_Mobile;
            public DataColumn Col_PaymentPercent;
            public DataColumn Col_Phone;
            public DataColumn Col_PolicyNumber;
            public DataColumn Col_Rank;
            public DataColumn Col_RelationshipCode;
            public DataColumn Col_RequestEligibility;
            public DataColumn Col_RequestEligibilityOn;
            public DataColumn Col_MIR;
            private static DataTableMirHelper F_MirHelper;

            public TableCustomerInsurance() : this("tbl_customer_insurance")
            {
            }

            public TableCustomerInsurance(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_Address1 = base.Columns["Address1"];
                this.Col_Address2 = base.Columns["Address2"];
                this.Col_City = base.Columns["City"];
                this.Col_State = base.Columns["State"];
                this.Col_Zip = base.Columns["Zip"];
                this.Col_Basis = base.Columns["Basis"];
                this.Col_DateofBirth = base.Columns["DateofBirth"];
                this.Col_Gender = base.Columns["Gender"];
                this.Col_GroupNumber = base.Columns["GroupNumber"];
                this.Col_ID = base.Columns["ID"];
                this.Col_InactiveDate = base.Columns["InactiveDate"];
                this.Col_InsuranceCompanyID = base.Columns["InsuranceCompanyID"];
                this.Col_InsuranceCompanyName = base.Columns["InsuranceCompanyName"];
                this.Col_InsuranceType = base.Columns["InsuranceType"];
                this.Col_InsuranceTypeName = base.Columns["InsuranceTypeName"];
                this.Col_FirstName = base.Columns["FirstName"];
                this.Col_LastName = base.Columns["LastName"];
                this.Col_MiddleName = base.Columns["MiddleName"];
                this.Col_Suffix = base.Columns["Suffix"];
                this.Col_Employer = base.Columns["Employer"];
                this.Col_Mobile = base.Columns["Mobile"];
                this.Col_PaymentPercent = base.Columns["PaymentPercent"];
                this.Col_Phone = base.Columns["Phone"];
                this.Col_PolicyNumber = base.Columns["PolicyNumber"];
                this.Col_Rank = base.Columns["Rank"];
                this.Col_RelationshipCode = base.Columns["RelationshipCode"];
                this.Col_RequestEligibility = base.Columns["RequestEligibility"];
                this.Col_RequestEligibilityOn = base.Columns["RequestEligibilityOn"];
                this.Col_MIR = base.Columns["MIR"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                base.Columns.Add("Address1", typeof(string));
                base.Columns.Add("Address2", typeof(string));
                base.Columns.Add("City", typeof(string));
                base.Columns.Add("State", typeof(string));
                base.Columns.Add("Zip", typeof(string));
                base.Columns.Add("Basis", typeof(string));
                base.Columns.Add("DateofBirth", typeof(DateTime));
                base.Columns.Add("Gender", typeof(string));
                base.Columns.Add("GroupNumber", typeof(string));
                base.Columns.Add("ID", typeof(int));
                base.Columns.Add("InactiveDate", typeof(DateTime));
                base.Columns.Add("InsuranceCompanyID", typeof(int));
                base.Columns.Add("InsuranceCompanyName", typeof(string));
                base.Columns.Add("InsuranceType", typeof(string));
                base.Columns.Add("InsuranceTypeName", typeof(string));
                base.Columns.Add("FirstName", typeof(string));
                base.Columns.Add("LastName", typeof(string));
                base.Columns.Add("MiddleName", typeof(string));
                base.Columns.Add("Suffix", typeof(string));
                base.Columns.Add("Employer", typeof(string));
                base.Columns.Add("Mobile", typeof(string));
                base.Columns.Add("PaymentPercent", typeof(int));
                base.Columns.Add("Phone", typeof(string));
                base.Columns.Add("PolicyNumber", typeof(string));
                DataColumn column1 = base.Columns.Add("Rank", typeof(int));
                column1.AutoIncrement = true;
                column1.AutoIncrementSeed = 1L;
                column1.AutoIncrementStep = 1L;
                column1.AllowDBNull = false;
                base.Columns.Add("RelationshipCode", typeof(string));
                base.Columns.Add("RequestEligibility", typeof(bool));
                base.Columns.Add("RequestEligibilityOn", typeof(DateTime));
                base.Columns.Add("MIR", typeof(string));
            }

            public void MergeKeys(ControlCustomerInsurance.TableCustomerInsurance table)
            {
                int num2 = table.Rows.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    DataRow row = table.Rows[i];
                    DataRow row2 = base.Rows.Find(row["RowID"]);
                    if (row2 != null)
                    {
                        row2[this.Col_ID] = row["ID"];
                    }
                }
            }

            public void ShowMissingInformation(bool Show)
            {
                StringBuilder builder = new StringBuilder();
                int num2 = base.Rows.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    DataRow row = base.Rows[i];
                    if (row.RowState != DataRowState.Deleted)
                    {
                        builder.Length = 0;
                        row.RowError = !Show ? "" : MirHelper.GetErrorMessage(NullableConvert.ToString(row[this.Col_MIR]));
                    }
                }
            }

            public static DataTableMirHelper MirHelper
            {
                get
                {
                    if (F_MirHelper == null)
                    {
                        F_MirHelper = new DataTableMirHelper();
                        F_MirHelper.Add("FirstName", "First Name is required for invoice");
                        F_MirHelper.Add("LastName", "Last Name is required for invoice");
                        F_MirHelper.Add("Address1", "Address-line-1 is required for invoice");
                        F_MirHelper.Add("City", "City is required for invoice");
                        F_MirHelper.Add("State", "State is required for invoice");
                        F_MirHelper.Add("Zip", "Zip is required for invoice");
                        F_MirHelper.Add("Gender", "Gender is required for invoice");
                        F_MirHelper.Add("DateofBirth", "Date of Birth is required for invoice");
                        F_MirHelper.Add("InsuranceCompanyID", "Insurance Company is required for invoice");
                        F_MirHelper.Add("InsuranceType", "Insurance Type is required for invoice");
                        F_MirHelper.Add("PolicyNumber", "Policy Number is required for invoice");
                        F_MirHelper.Add("RelationshipCode", "Relationship To Insured is required for invoice");
                    }
                    return F_MirHelper;
                }
            }
        }
    }
}

