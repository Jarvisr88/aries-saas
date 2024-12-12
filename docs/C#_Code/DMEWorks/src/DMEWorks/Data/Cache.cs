namespace DMEWorks.Data
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using DMEWorks.Reports;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class Cache
    {
        private const string CrLf = "\r\n";
        private static readonly Dictionary<object, DropdownHelper> hashHelpers = new Dictionary<object, DropdownHelper>(EqualityComparer<object>.Default);
        private static readonly List<IReference> references;
        private static TableInvoiceTransactionType F_Table_Invoice_TransactionType;
        private static DataSourceReports FReportsDataSource;

        static Cache()
        {
            hashHelpers.Add("tbl_ability_eligibility_payer", new DropdownAbilityEligibilityPayerEvents());
            hashHelpers.Add("tbl_billingtype", new DropdownBillingTypeEvents());
            hashHelpers.Add("tbl_customer", new DropdownCustomerEvents());
            hashHelpers.Add("tbl_customerclass", new DropdownCustomerClassEvents());
            hashHelpers.Add("tbl_customertype", new DropdownCustomerTypeEvents());
            hashHelpers.Add("tbl_doctor", new DropdownDoctorEvents());
            hashHelpers.Add("tbl_doctortype", new DropdownDoctorTypeEvents());
            hashHelpers.Add("tbl_facility", new DropdownFacilityEvents());
            hashHelpers.Add("tbl_hao", new DropdownHAO());
            hashHelpers.Add("tbl_icd9", new DropdownICD9());
            hashHelpers.Add("tbl_icd10", new DropdownICD10());
            hashHelpers.Add("tbl_insurancecompany", new DropdownInsuranceCompanyEvents());
            hashHelpers.Add("tbl_insurancecompanygroup", new DropdownInsuranceCompanyGroupEvents());
            hashHelpers.Add("tbl_insurancecompanytype", new DropdownInsuranceCompanyTYpeEvents());
            hashHelpers.Add("tbl_insurancetype", new DropdownInsuranceTypeEvents());
            hashHelpers.Add("tbl_inventoryitem", new DropdownInventoryItemEvents());
            hashHelpers.Add("tbl_inventorycode", new DropdownInventoryCodeEvents());
            hashHelpers.Add("tbl_invoiceform", new DropdownInvoiceFormEvents());
            hashHelpers.Add("tbl_kit", new DropdownKitEvents());
            hashHelpers.Add("tbl_legalrep", new DropdownLegalrepEvents());
            hashHelpers.Add("tbl_location", new DropdownLocationEvents());
            hashHelpers.Add("tbl_manufacturer", new DropdownManufacturerEvents());
            hashHelpers.Add("tbl_postype", new DropdownPosTypeEvents());
            hashHelpers.Add("tbl_predefinedtext", new DropdownPredefinedTextEvents());
            hashHelpers.Add("tbl_predefinedtext_compliancenotes", new DropdownPredefinedTextComplianceNotesEvents());
            hashHelpers.Add("tbl_predefinedtext_customernotes", new DropdownPredefinedTextCustomerNotesEvents());
            hashHelpers.Add("tbl_predefinedtext_invoicenotes", new DropdownPredefinedTextInvoiceNotesEvents());
            hashHelpers.Add("tbl_pricecode", new DropdownPriceCodeEvents());
            hashHelpers.Add("tbl_authorizationtype", new DropdownAuthorizationTypeEvents());
            hashHelpers.Add("tbl_producttype", new DropdownProductTypeEvents());
            hashHelpers.Add("tbl_providernumbertype", new DropdownProviderNumberTypeEvents());
            hashHelpers.Add("tbl_referral", new DropdownReferralEvents());
            hashHelpers.Add("tbl_referraltype", new DropdownReferralTypeEvents());
            hashHelpers.Add("tbl_relationship", new DropdownRelationshipEvents());
            hashHelpers.Add("tbl_salesrep", new DropdownSalesrepEvents());
            hashHelpers.Add("tbl_shippingmethod", new DropdownShippingMethodEvents());
            hashHelpers.Add("tbl_signaturetype", new DropdownSignatureTypeEvents());
            hashHelpers.Add("tbl_survey", new DropdownSurveyEvents());
            hashHelpers.Add("tbl_taxrate", new DropdownTaxRateEvents());
            hashHelpers.Add("tbl_upsshipping_billingoption", new DropdownUpsShipping_BillingOptionEvents());
            hashHelpers.Add("tbl_upsshipping_freightclass", new DropdownUpsShipping_FreightClassEvents());
            hashHelpers.Add("tbl_upsshipping_packagingtype", new DropdownUpsShipping_PackagingTypeEvents());
            hashHelpers.Add("tbl_user", new DropdownUserEvents());
            hashHelpers.Add("tbl_vendor", new DropdownVendorEvents());
            hashHelpers.Add("tbl_warehouse", new DropdownWarehouseEvents());
            references = new List<IReference>(0x40);
        }

        public static void AddCategory(Menu Menu, string Category, EventHandler onClick)
        {
            _Closure$__24-0 e$__-;
            e$__- = new _Closure$__24-0(e$__-) {
                $VB$Local_Category = Category
            };
            try
            {
                Report[] reportArray = CrystalReports.Select().Where<Report>(new Func<Report, bool>(e$__-._Lambda$__0)).OrderBy<Report, string>(((_Closure$__.$I24-1 == null) ? (_Closure$__.$I24-1 = new Func<Report, string>(_Closure$__.$I._Lambda$__24-1)) : _Closure$__.$I24-1), SqlStringComparer.Default).ToArray<Report>();
                Menu.MenuItemCollection menuItems = Menu.MenuItems;
                if (0 < reportArray.Length)
                {
                    if (0 < menuItems.Count)
                    {
                        menuItems.Add("-");
                    }
                    foreach (Report report in reportArray)
                    {
                        ReportMenuItem item1 = new ReportMenuItem();
                        item1.Text = report.Name;
                        item1.ReportFileName = report.FileName;
                        ReportMenuItem item = item1;
                        item.Click += onClick;
                        menuItems.Add(item);
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        public static void AddCategory(ToolStripItemCollection Items, string Category, EventHandler onClick)
        {
            _Closure$__23-0 e$__-;
            e$__- = new _Closure$__23-0(e$__-) {
                $VB$Local_Category = Category
            };
            try
            {
                Report[] reportArray = CrystalReports.Select().Where<Report>(new Func<Report, bool>(e$__-._Lambda$__0)).OrderBy<Report, string>(((_Closure$__.$I23-1 == null) ? (_Closure$__.$I23-1 = new Func<Report, string>(_Closure$__.$I._Lambda$__23-1)) : _Closure$__.$I23-1), SqlStringComparer.Default).ToArray<Report>();
                if (0 < reportArray.Length)
                {
                    if (0 < Items.Count)
                    {
                        Items.Add(new ToolStripSeparator());
                    }
                    foreach (Report report in reportArray)
                    {
                        ReportToolStripMenuItem item1 = new ReportToolStripMenuItem();
                        item1.Text = report.Name;
                        item1.ReportFileName = report.FileName;
                        ReportToolStripMenuItem item = item1;
                        item.Click += onClick;
                        Items.Add(item);
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private static void AddReference(IReference reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException("reference");
            }
            object syncRoot = ((ICollection) references).SyncRoot;
            ObjectFlowControl.CheckForSyncLockOnValueType(syncRoot);
            lock (syncRoot)
            {
                _Closure$__25-0 e$__- = new _Closure$__25-0 {
                    $VB$Local_component = reference.Component
                };
                references.RemoveAll(new Predicate<IReference>(e$__-._Lambda$__0));
                references.Add(reference);
            }
        }

        public static Batch BeginBatch() => 
            new Batch();

        public static void Clear()
        {
            object syncRoot = ((ICollection) references).SyncRoot;
            ObjectFlowControl.CheckForSyncLockOnValueType(syncRoot);
            lock (syncRoot)
            {
                references.Clear();
            }
            using (Dictionary<object, DropdownHelper>.ValueCollection.Enumerator enumerator = hashHelpers.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.ClearCachedData();
                }
            }
            Clear_Table_Invoice_TransactionType();
        }

        public static void Clear_Table_Invoice_TransactionType()
        {
            F_Table_Invoice_TransactionType = null;
        }

        public static bool ConvertToBoolean(object value) => 
            !(value as bool) ? (!(value is byte) ? (!(value is short) ? (!(value is int) ? (!(value is long) ? (!(value is sbyte) ? (!(value is ushort) ? (!(value is uint) ? ((value is ulong) && (decimal.Compare(new decimal(Conversions.ToULong(value)), 0M) != 0)) : (Conversions.ToUInteger(value) != 0L)) : (Conversions.ToUShort(value) != 0)) : (Conversions.ToSByte(value) != 0)) : (Conversions.ToLong(value) != 0L)) : (Conversions.ToInteger(value) != 0)) : (Conversions.ToShort(value) != 0)) : (Conversions.ToByte(value) != 0)) : Conversions.ToBoolean(value);

        public static DropdownHelper GetDropdownHelper(string tablename)
        {
            if (tablename == null)
            {
                throw new ArgumentNullException("tablename");
            }
            DropdownHelper helper2 = null;
            if (!hashHelpers.TryGetValue(tablename, out helper2) || (helper2 == null))
            {
                throw new Exception("GetDropdownTable method does not support specified tablename (" + tablename + ")");
            }
            return helper2;
        }

        public static void HandleDatabaseChanged(string[] TableNames)
        {
            _Closure$__26-0 e$__-;
            e$__- = new _Closure$__26-0(e$__-);
            if (TableNames == null)
            {
                throw new ArgumentNullException("TableNames");
            }
            e$__-.$VB$Local_set = new HashSet<string>(TableNames, StringComparer.InvariantCultureIgnoreCase);
            if (e$__-.$VB$Local_set.Count != 0)
            {
                IReference[] referenceArray;
                foreach (string str in e$__-.$VB$Local_set)
                {
                    DropdownHelper helper = null;
                    if (hashHelpers.TryGetValue(str, out helper))
                    {
                        helper.ClearCachedData();
                    }
                }
                if (e$__-.$VB$Local_set.Contains("tbl_invoice_transactiontype"))
                {
                    Clear_Table_Invoice_TransactionType();
                }
                object syncRoot = ((ICollection) references).SyncRoot;
                ObjectFlowControl.CheckForSyncLockOnValueType(syncRoot);
                lock (syncRoot)
                {
                    referenceArray = references.Where<IReference>(new Func<IReference, bool>(e$__-._Lambda$__0)).ToArray<IReference>();
                    references.RemoveAll(new Predicate<IReference>(e$__-._Lambda$__1));
                }
                IReference[] referenceArray2 = referenceArray;
                for (int i = 0; i < referenceArray2.Length; i++)
                {
                    referenceArray2[i].Invoke();
                }
            }
        }

        public static void InitDialog(FindDialog dialog, string tablename, DMEWorks.Data.IFilter filter = null)
        {
            new FindDialogReference(tablename, dialog, filter).Invoke();
        }

        public static void InitDropdown(Combobox dropdown, string tablename, DMEWorks.Data.IFilter filter = null)
        {
            new DMEWorksFormsComboBoxReference(tablename, dropdown, filter).Invoke();
        }

        public static void InitDropdown(ExtendedDropdown dropdown, string tablename, DMEWorks.Data.IFilter filter = null)
        {
            new ExtendedDropdownReference(tablename, dropdown, filter).Invoke();
        }

        public static void InitDropdown(ComboBox dropdown, string tablename, DMEWorks.Data.IFilter filter = null)
        {
            new WindowsFormsComboBoxReference(tablename, dropdown, filter).Invoke();
        }

        public static void InventoryItem_CellFormatting(object sender, GridCellFormattingEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    DataColumn column = dataRow.Table.Columns["Inactive"];
                    if ((column != null) && NullableConvert.ToBoolean(dataRow[column], false))
                    {
                        e.CellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        internal static void LoadTable(DataTable table, string sql, bool WithNullRow = true)
        {
            if (WithNullRow)
            {
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                row.AcceptChanges();
            }
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(table);
            }
        }

        public static TableInvoiceTransactionType Table_Invoice_TransactionType
        {
            get
            {
                if (F_Table_Invoice_TransactionType == null)
                {
                    F_Table_Invoice_TransactionType = new TableInvoiceTransactionType("tbl_invoice_transactiontype");
                    string sql = "SELECT ID,\r\n       Name,\r\n       Balance,\r\n       Allowable,\r\n       Amount,\r\n       Taxes\r\nFROM tbl_invoice_transactiontype\r\nORDER BY Name";
                    LoadTable(F_Table_Invoice_TransactionType, sql, false);
                }
                return F_Table_Invoice_TransactionType;
            }
        }

        private static bool IsDemoVersion =>
            DMEWorks.Globals.SerialNumber.IsDemoSerial();

        public static DataSourceReports CrystalReports
        {
            get
            {
                FReportsDataSource ??= new DataSourceReports(Path.Combine(ClassGlobalObjects.CustomReportsFolder, "Description.xml"), Path.Combine(ClassGlobalObjects.DefaultReportsFolder, "Description.xml"));
                return FReportsDataSource;
            }
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly Cache._Closure$__ $I = new Cache._Closure$__();
            public static Func<Report, string> $I23-1;
            public static Func<Report, string> $I24-1;

            internal string _Lambda$__23-1(Report r) => 
                r.Name;

            internal string _Lambda$__24-1(Report r) => 
                r.Name;
        }

        [CompilerGenerated]
        internal sealed class _Closure$__23-0
        {
            public string $VB$Local_Category;

            public _Closure$__23-0(Cache._Closure$__23-0 arg0)
            {
                if (arg0 != null)
                {
                    this.$VB$Local_Category = arg0.$VB$Local_Category;
                }
            }

            internal bool _Lambda$__0(Report r) => 
                SqlString.Equals(r.Category, this.$VB$Local_Category);
        }

        [CompilerGenerated]
        internal sealed class _Closure$__24-0
        {
            public string $VB$Local_Category;

            public _Closure$__24-0(Cache._Closure$__24-0 arg0)
            {
                if (arg0 != null)
                {
                    this.$VB$Local_Category = arg0.$VB$Local_Category;
                }
            }

            internal bool _Lambda$__0(Report r) => 
                SqlString.Equals(r.Category, this.$VB$Local_Category);
        }

        [CompilerGenerated]
        internal sealed class _Closure$__25-0
        {
            public Component $VB$Local_component;

            internal bool _Lambda$__0(Cache.IReference r) => 
                ReferenceEquals(r.Component, this.$VB$Local_component);
        }

        [CompilerGenerated]
        internal sealed class _Closure$__26-0
        {
            public HashSet<string> $VB$Local_set;

            public _Closure$__26-0(Cache._Closure$__26-0 arg0)
            {
                if (arg0 != null)
                {
                    this.$VB$Local_set = arg0.$VB$Local_set;
                }
            }

            internal bool _Lambda$__0(Cache.IReference r) => 
                this.$VB$Local_set.Contains(r.TableName);

            internal bool _Lambda$__1(Cache.IReference r) => 
                this.$VB$Local_set.Contains(r.TableName);
        }

        public class Batch : IDisposable
        {
            private readonly List<Cache.IReference> m_references = new List<Cache.IReference>();

            public void Dispose()
            {
                using (IEnumerator<IGrouping<string, Cache.IReference>> enumerator = this.m_references.GroupBy<Cache.IReference, string>(((_Closure$__.$I6-0 == null) ? (_Closure$__.$I6-0 = new Func<Cache.IReference, string>(_Closure$__.$I._Lambda$__6-0)) : _Closure$__.$I6-0)).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Cache.GetDropdownHelper(enumerator.Current.Key).PreloadData();
                    }
                }
                using (List<Cache.IReference>.Enumerator enumerator2 = this.m_references.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        enumerator2.Current.Invoke();
                    }
                }
            }

            public void InitDialog(FindDialog dialog, string tablename, DMEWorks.Data.IFilter filter = null)
            {
                this.m_references.Add(new Cache.FindDialogReference(tablename, dialog, filter));
            }

            public void InitDropdown(Combobox dropdown, string tablename, DMEWorks.Data.IFilter filter = null)
            {
                this.m_references.Add(new Cache.DMEWorksFormsComboBoxReference(tablename, dropdown, filter));
            }

            public void InitDropdown(ExtendedDropdown dropdown, string tablename, DMEWorks.Data.IFilter filter = null)
            {
                this.m_references.Add(new Cache.ExtendedDropdownReference(tablename, dropdown, filter));
            }

            public void InitDropdown(ComboBox dropdown, string tablename, DMEWorks.Data.IFilter filter = null)
            {
                this.m_references.Add(new Cache.WindowsFormsComboBoxReference(tablename, dropdown, filter));
            }

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly Cache.Batch._Closure$__ $I = new Cache.Batch._Closure$__();
                public static Func<Cache.IReference, string> $I6-0;

                internal string _Lambda$__6-0(Cache.IReference r) => 
                    r.TableName;
            }
        }

        public abstract class DatabaseDropdownHelper : Cache.DropdownHelper
        {
            public override DataTable GetTable()
            {
                DataTable table = new DataTable(this.TableName());
                Cache.LoadTable(table, this.Query(), true);
                return table;
            }

            public abstract string Query();
        }

        private class DMEWorksFormsComboBoxReference : Cache.ReferenceBase<Combobox>
        {
            public DMEWorksFormsComboBoxReference(string tableName, Combobox dropdown, DMEWorks.Data.IFilter filter) : base(tableName, dropdown, filter)
            {
            }

            protected override void Invoke(Combobox dropdown, DMEWorks.Data.IFilter filter)
            {
                if (!dropdown.Disposing)
                {
                    Form form = dropdown.FindForm();
                    if ((form != null) && (!form.Disposing && !form.IsDisposed))
                    {
                        Cache.GetDropdownHelper(base.TableName).InitDropdown(dropdown, filter);
                        Cache.AddReference(this);
                    }
                }
            }
        }

        private class DropdownAbilityEligibilityPayerEvents : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox comboBox, DataTable table)
            {
                Functions.AssignDatasource(comboBox, table, "Code", "Id");
            }

            public override void AssignDatasource(ComboBox comboBox, DataTable table)
            {
                Functions.AssignDatasource(comboBox, table, "Code", "Id");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
            }

            public override void ClickNew(object source, EventArgs args)
            {
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Ability eligibility payer";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("Id", "Id", 40);
                e.Appearance.AddTextColumn("Code", "Code", 60);
                e.Appearance.AddTextColumn("Name", "Name", 150);
                e.Appearance.AddTextColumn("Comments", "Comments", 80);
            }

            public override string Query() => 
                "SELECT Id, Code, Name, Comments FROM tbl_ability_eligibility_payer ORDER BY Code";

            public override string TableName() => 
                "tbl_ability_eligibility_payer";
        }

        public class DropdownAuthorizationTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormAuthorizationType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormAuthorizationType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Authorization Type";
            }

            public override string Query() => 
                "SELECT\r\n  ID\r\n, Name\r\nFROM tbl_authorizationtype\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_authorizationtype";
        }

        public class DropdownBillingTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormBillingType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormBillingType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Billing Type";
            }

            public override string Query() => 
                "SELECT ID, Name FROM tbl_billingtype ORDER BY Name";

            public override string TableName() => 
                "tbl_billingtype";
        }

        public class DropdownCustomerClassEvents : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormCustomerClass(), CreateHash_Edit(source, "Code"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormCustomerClass(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Customer class";
            }

            public override string Query() => 
                "SELECT Code, Description FROM tbl_customerclass ORDER BY Description";

            public override string TableName() => 
                "tbl_customerclass";
        }

        public class DropdownCustomerEvents : Cache.DatabaseDropdownHelper
        {
            private readonly ComboboxDrawItemEventHandler FEventDrawItem = new ComboboxDrawItemEventHandler(Cache.DropdownCustomerEvents.DrawItem);

            private static void Appearance_CellFormatting(object sender, GridCellFormattingEventArgs e)
            {
                try
                {
                    DataRow dataRow = e.Row.GetDataRow();
                    if (((dataRow != null) && dataRow.Table.Columns.Contains("Collections")) && Cache.ConvertToBoolean(dataRow["Collections"]))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormCustomer(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormCustomer(), CreateHash_New(source));
            }

            private static void DrawItem(object source, ComboboxDrawItemEventArgs e)
            {
                DataRowView row = e.Row;
                Color backColor = e.BackColor;
                if ((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)
                {
                    backColor = SystemColors.Control;
                }
                using (Brush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                bool flag = false;
                if (row != null)
                {
                    try
                    {
                        flag = Cache.ConvertToBoolean(row["Collections"]);
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                }
                Color foreColor = e.ForeColor;
                if (flag && ((e.State & DrawItemState.Selected) != DrawItemState.Selected))
                {
                    foreColor = Color.Red;
                }
                using (Brush brush2 = new SolidBrush(foreColor))
                {
                    e.Graphics.DrawString(e.Text, e.Font, brush2, e.Bounds);
                }
                if (((e.State & DrawItemState.Focus) == DrawItemState.Focus) && ((e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect))
                {
                    ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, e.ForeColor, e.BackColor);
                }
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Customer";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("ID", "ID", 40);
                e.Appearance.AddTextColumn("CustomerType", "Type", 60);
                e.Appearance.AddTextColumn("FirstName", "First Name", 80);
                e.Appearance.AddTextColumn("LastName", "Last Name", 80);
                e.Appearance.AddTextColumn("DateofBirth", "Birthday", 80, e.Appearance.DateStyle());
                e.Appearance.AddTextColumn("AccountNumber", "Account#", 60);
                e.Appearance.AddTextColumn("Phone", "Phone", 80);
                e.Appearance.AddTextColumn("City", "City", 80);
                e.Appearance.AddTextColumn("State", "State", 40);
                e.Appearance.AddTextColumn("Zip", "Zip", 60);
                e.Appearance.AddTextColumn("SSNumber", "SSN", 80);
                e.Appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(Cache.DropdownCustomerEvents.Appearance_CellFormatting);
            }

            public override string Query() => 
                $"SELECT
  tbl_customer.ID
, tbl_customertype.Name as CustomerType
, CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as Name
, tbl_customer.LastName
, tbl_customer.FirstName
, tbl_customer.DateofBirth
, tbl_customer.AccountNumber
, tbl_customer.Phone
, tbl_customer.Address1
, tbl_customer.City
, tbl_customer.State
, tbl_customer.Zip
, tbl_customer.SSNumber
, tbl_customer.Collections
FROM tbl_customer
     LEFT JOIN tbl_customertype ON tbl_customertype.ID = tbl_customer.CustomerTypeID
     LEFT JOIN tbl_company ON tbl_company.ID = 1
WHERE ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))
  AND ({Interaction.IIf(Cache.IsDemoVersion, "tbl_customer.ID BETWEEN 1 and 50", "1 = 1")})";

            public override string TableName() => 
                "tbl_customer";

            public override ComboboxDrawItemEventHandler EventDrawItem =>
                this.FEventDrawItem;
        }

        public class DropdownCustomerTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormCustomerType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormCustomerType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Customer Type";
            }

            public override string Query() => 
                "SELECT ID, Name FROM tbl_customertype ORDER BY Name";

            public override string TableName() => 
                "tbl_customertype";
        }

        public class DropdownDoctorEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormDoctor(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormDoctor(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Doctor";
            }

            public override string Query() => 
                "SELECT\r\n  ID,\r\n  CONCAT(LastName, ', ', FirstName) as Name,\r\n  LastName,\r\n  FirstName,\r\n  Address1,\r\n  City,\r\n  State,\r\n  UPINNumber\r\nFROM tbl_doctor\r\nORDER BY LastName, FirstName";

            public override string TableName() => 
                "tbl_doctor";
        }

        public class DropdownDoctorTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormDoctorType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormDoctorType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Doctor Type";
            }

            public override string Query() => 
                "SELECT ID, Name FROM tbl_doctortype ORDER BY Name";

            public override string TableName() => 
                "tbl_doctortype";
        }

        public class DropdownFacilityEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormFacility(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormFacility(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Facility";
            }

            public override string Query() => 
                "SELECT\r\n   ID,\r\n   Name,\r\n   City,\r\n   State\r\nFROM tbl_facility\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_facility";
        }

        public class DropdownHAO : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormHAO(), CreateHash_Edit(source, "Code"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormHAO(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select HAO";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("Code", "Code", 40);
                e.Appearance.AddTextColumn("Description", "Description", 200);
            }

            public override string Query() => 
                "SELECT Code, Description FROM tbl_hao ORDER BY Description";

            public override string TableName() => 
                "tbl_hao";
        }

        public abstract class DropdownHelper : DropdownHelperBase
        {
            protected DropdownHelper()
            {
            }

            public abstract string TableName();
        }

        public class DropdownICD10 : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormICD10(), CreateHash_Edit(source, "Code"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormICD10(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select ICD 10 code";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("Code", "Code", 60);
                e.Appearance.AddTextColumn("Description", "Description", 240);
            }

            public override string Query() => 
                "SELECT Code, Description, ActiveDate, InactiveDate FROM tbl_icd10 ORDER BY Code";

            public override string TableName() => 
                "tbl_icd10";
        }

        public class DropdownICD9 : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormICD9(), CreateHash_Edit(source, "Code"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormICD9(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select ICD 9 code";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("Code", "Code", 50);
                e.Appearance.AddTextColumn("Description", "Description", 240);
            }

            public override string Query() => 
                "SELECT Code, Description, ActiveDate, InactiveDate FROM tbl_icd9 ORDER BY Code";

            public override string TableName() => 
                "tbl_icd9";
        }

        public class DropdownInsuranceCompanyEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceCompany(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceCompany(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Insurance Company";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.AddTextColumn("ID", "ID", 60, e.Appearance.IntegerStyle());
                e.Appearance.AddTextColumn("Name", "Name", 160);
                e.Appearance.AddTextColumn("State", "State", 60);
                e.Appearance.AddTextColumn("City", "City", 80);
                e.Appearance.AddTextColumn("Address1", "Address1", 80);
                e.Appearance.AddTextColumn("Group", "Group", 80);
            }

            public override string Query() => 
                "SELECT\r\n  tbl_insurancecompany.ID\r\n, tbl_insurancecompany.Name\r\n, tbl_insurancecompany.Address1\r\n, tbl_insurancecompany.City\r\n, tbl_insurancecompany.State\r\n, tbl_insurancecompany.Zip\r\n, tbl_insurancecompany.Phone\r\n, tbl_insurancecompany.Basis\r\n, tbl_insurancecompany.ExpectedPercent as Percent\r\n, IFNULL(tbl_insurancecompanygroup.Name, '') as `Group`\r\nFROM tbl_insurancecompany\r\n     LEFT JOIN tbl_insurancecompanygroup ON tbl_insurancecompany.GroupID = tbl_insurancecompanygroup.ID\r\nORDER BY IFNULL(tbl_insurancecompanygroup.Name, ''), tbl_insurancecompany.Name";

            public override string TableName() => 
                "tbl_insurancecompany";
        }

        public class DropdownInsuranceCompanyGroupEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceCompanyGroup(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceCompanyGroup(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Insurance Company Group";
            }

            public override string Query() => 
                "SELECT\r\n  ID\r\n, Name\r\nFROM tbl_insurancecompanygroup\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_insurancecompanygroup";
        }

        public class DropdownInsuranceCompanyTYpeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
            }

            public override void ClickNew(object source, EventArgs e)
            {
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Insurance Company Type";
            }

            public override string Query() => 
                "SELECT\r\n  ID\r\n, Name\r\nFROM tbl_insurancecompanytype\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_insurancecompanytype";
        }

        public class DropdownInsuranceTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInsuranceType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Insurance Type";
            }

            public override string Query() => 
                "SELECT\r\n  Code as ID\r\n, Description as Name\r\nFROM tbl_insurancetype\r\nORDER BY Description";

            public override string TableName() => 
                "tbl_insurancetype";
        }

        public class DropdownInventoryCodeEvents : Cache.DropdownInventoryItemEvents
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "InventoryCode", "ID");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "InventoryCode", "ID");
            }
        }

        public class DropdownInventoryItemEvents : Cache.DatabaseDropdownHelper
        {
            private readonly ComboboxDrawItemEventHandler FEventDrawItem = new ComboboxDrawItemEventHandler(Cache.DropdownInventoryItemEvents.DrawItem);

            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInventoryItem(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInventoryItem(), CreateHash_New(source));
            }

            public static void DrawItem(object source, ComboboxDrawItemEventArgs e)
            {
                DataRowView row = e.Row;
                bool flag = false;
                if (row != null)
                {
                    try
                    {
                        flag = Cache.ConvertToBoolean(row["Inactive"]);
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                }
                Color backColor = e.BackColor;
                if (flag && ((e.State & DrawItemState.Selected) != DrawItemState.Selected))
                {
                    backColor = Color.LightCoral;
                }
                using (Brush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                using (Brush brush2 = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(e.Text, e.Font, brush2, e.Bounds);
                }
                if (((e.State & DrawItemState.Focus) == DrawItemState.Focus) && ((e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect))
                {
                    ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, e.ForeColor, backColor);
                }
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Inventory Item";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.AddTextColumn("ID", "ID", 40, e.Appearance.IntegerStyle());
                e.Appearance.AddTextColumn("Name", "Inventory Item", 160);
                e.Appearance.AddTextColumn("BarCode", "Bar Code", 60);
                e.Appearance.AddTextColumn("ModelNumber", "Model #", 60);
                e.Appearance.AddTextColumn("InventoryCode", "Inv Code", 60);
                if (DMEWorks.Globals.ShowQuantityOnHand)
                {
                    e.Appearance.AddTextColumn("QuantityOnHand", "QOH", 40, e.Appearance.IntegerStyle());
                }
                e.Appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(Cache.InventoryItem_CellFormatting);
            }

            public override string Query()
            {
                if (DMEWorks.Globals.ShowQuantityOnHand)
                {
                    int? defaultWarehouseID = ClassGlobalObjects.DefaultWarehouseID;
                    if (defaultWarehouseID != null)
                    {
                        return ("SELECT\r\n  ii.ID\r\n, ii.Name\r\n, ii.Barcode\r\n, ii.InventoryCode\r\n, ii.ModelNumber\r\n, ii.Inactive\r\n, CASE ii.Serialized WHEN 0 THEN 'N' WHEN 1 THEN 'Y' ELSE '' END as Serialized\r\n, ii.PurchasePrice\r\n, i.OnHand as QuantityOnHand\r\nFROM tbl_inventoryitem as ii\r\n     LEFT JOIN tbl_inventory as i ON i.InventoryItemID = ii.ID\r\n                                 AND i.WarehouseID = " + defaultWarehouseID.Value.ToString() + "\r\nORDER BY ii.Name");
                    }
                }
                return "SELECT\r\n  ID\r\n, Name\r\n, Barcode\r\n, InventoryCode\r\n, ModelNumber\r\n, Inactive\r\n, CASE Serialized WHEN 0 THEN 'N' WHEN 1 THEN 'Y' ELSE '' END as Serialized\r\n, PurchasePrice\r\nFROM tbl_inventoryitem\r\nORDER BY Name";
            }

            public override string TableName() => 
                "tbl_inventoryitem";

            public override ComboboxDrawItemEventHandler EventDrawItem =>
                this.FEventDrawItem;
        }

        public class DropdownInvoiceFormEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInvoiceForm(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInvoiceForm(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Invoice Form";
            }

            public override string Query() => 
                "SELECT ID, Name FROM tbl_invoiceform ORDER BY Name";

            public override string TableName() => 
                "tbl_invoiceform";
        }

        public class DropdownKitEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormKit(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormKit(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Kit";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("ID", "#", 50);
                e.Appearance.AddTextColumn("Name", "Name", 240);
            }

            public override string Query() => 
                "SELECT ID, Name FROM tbl_kit ORDER BY Name";

            public override string TableName() => 
                "tbl_kit";
        }

        public class DropdownLegalrepEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormLegalRep(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormLegalRep(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Legal Representative";
            }

            public override string Query() => 
                "SELECT\r\n  ID,\r\n  CONCAT(LastName, ', ', FirstName) as Name,\r\n  Address1,\r\n  City,\r\n  State\r\nFROM tbl_legalrep\r\nORDER BY LastName, FirstName";

            public override string TableName() => 
                "tbl_legalrep";
        }

        public class DropdownLocationEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormLocation(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormLocation(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Location";
            }

            public override string Query() => 
                "SELECT\r\n  ID,\r\n  Name,\r\n  Address1,\r\n  City,\r\n  State\r\nFROM tbl_location\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_location";
        }

        public class DropdownManufacturerEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormManufacturer(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormManufacturer(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Manufacturer";
            }

            public override string Query() => 
                "SELECT\r\n  ID,\r\n  Name,\r\n  Address1,\r\n  City,\r\n  State\r\nFROM tbl_manufacturer\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_manufacturer";
        }

        public class DropdownPosTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPOSType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPOSType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select POS Type";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_postype\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_postype";
        }

        public class DropdownPredefinedTextComplianceNotesEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Compliance Notes";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_predefinedtext\r\nWHERE (`Type` = 'Compliance Notes')\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_predefinedtext_compliancenotes";
        }

        public class DropdownPredefinedTextCustomerNotesEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Customer Notes";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_predefinedtext\r\nWHERE (`Type` = 'Customer Notes')\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_predefinedtext_customernotes";
        }

        public class DropdownPredefinedTextEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Predefined Text";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_predefinedtext\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_predefinedtext";
        }

        public class DropdownPredefinedTextInvoiceNotesEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPredefinedText(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Invoice Notes";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_predefinedtext\r\nWHERE (`Type` = 'Invoice Notes')\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_predefinedtext_invoicenotes";
        }

        public class DropdownPriceCodeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPriceCode(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPriceCode(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Price Code";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_pricecode\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_pricecode";
        }

        public class DropdownProductTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormProductType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormProductType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Product Type";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_producttype\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_producttype";
        }

        public class DropdownProviderNumberTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormProviderNumberType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormProviderNumberType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Provider Number Type";
            }

            public override string Query() => 
                "SELECT\r\n  Code as ID\r\n, Description as Name\r\nFROM tbl_providernumbertype\r\nORDER BY Description";

            public override string TableName() => 
                "tbl_providernumbertype";
        }

        public class DropdownReferralEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormReferral(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormReferral(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Referral";
            }

            public override string Query() => 
                "SELECT\r\n  ID,\r\n  CONCAT(LastName, ', ', FirstName) as Name,\r\n  LastName,\r\n  FirstName,\r\n  Employer,\r\n  Address1,\r\n  City,\r\n  State\r\nFROM tbl_referral\r\nORDER BY LastName, FirstName";

            public override string TableName() => 
                "tbl_referral";
        }

        public class DropdownReferralTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormReferralType(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormReferralType(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Referral Type";
            }

            public override string Query() => 
                "SELECT ID, Name FROM tbl_referraltype ORDER BY Name";

            public override string TableName() => 
                "tbl_referraltype";
        }

        public class DropdownRelationshipEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
            }

            public override void ClickNew(object source, EventArgs e)
            {
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Relationship";
            }

            public override string Query() => 
                "SELECT\r\n   Code as ID\r\n  ,Description as Name\r\nFROM tbl_relationship\r\nORDER BY Description";

            public override string TableName() => 
                "tbl_relationship";
        }

        public class DropdownSalesrepEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormSalesRep(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormSalesRep(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Salesrep";
            }

            public override string Query() => 
                "SELECT ID,\r\n       CONCAT(LastName, ', ', FirstName) as Name,\r\n       LastName,\r\n       FirstName,\r\n       Address1,\r\n       City,\r\n       State\r\nFROM tbl_salesrep\r\nORDER BY LastName, FirstName";

            public override string TableName() => 
                "tbl_salesrep";
        }

        public class DropdownShippingMethodEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormShippingMethod(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormShippingMethod(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Shipping Method";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("ID", "ID", 40);
                e.Appearance.AddTextColumn("Name", "Name", 200);
            }

            public override string Query() => 
                "SELECT\r\n  ID\r\n, Name\r\n, Type\r\nFROM tbl_shippingmethod\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_shippingmethod";
        }

        public class DropdownSignatureTypeEvents : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void ClickEdit(object source, EventArgs e)
            {
            }

            public override void ClickNew(object source, EventArgs e)
            {
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Signature Type";
            }

            public override string Query() => 
                "SELECT Code, Description FROM tbl_signaturetype ORDER BY Description";

            public override string TableName() => 
                "tbl_signaturetype";
        }

        public class DropdownSurveyEvents : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Name", "ID");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Name", "ID");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormSurvey(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs e)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormSurvey(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Survey";
            }

            public override string Query() => 
                "SELECT ID, Name, Description FROM tbl_survey ORDER BY Name";

            public override string TableName() => 
                "tbl_survey";
        }

        public class DropdownTaxRateEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormTaxRate(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormTaxRate(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Tax Rate";
            }

            public override string Query() => 
                "SELECT\r\n  ID\r\n, Name\r\n, IFNULL(CityTax, 0) + IFNULL(CountyTax, 0) + IFNULL(OtherTax, 0) + IFNULL(StateTax, 0) as Percent\r\nFROM tbl_taxrate\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_taxrate";
        }

        public class DropdownUpsShipping_BillingOptionEvents : Cache.DropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
            }

            public override void ClickNew(object source, EventArgs args)
            {
            }

            public override DataTable GetTable()
            {
                Cache.TableCodeDescription description1 = new Cache.TableCodeDescription();
                description1.Add("", "");
                description1.Add("10", "Prepaid");
                description1.Add("20", "Bill to Consignee");
                description1.Add("30", "Bill to Third Party");
                description1.Add("40", "Freight Collect");
                description1.AcceptChanges();
                return description1;
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Billing Option";
            }

            public override string TableName() => 
                "tbl_upsshipping_billingoption";
        }

        public class DropdownUpsShipping_FreightClassEvents : Cache.DropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Code", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Code", "Code");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
            }

            public override void ClickNew(object source, EventArgs args)
            {
            }

            public override DataTable GetTable()
            {
                Cache.TableCodeDescription description1 = new Cache.TableCodeDescription();
                description1.Add("", "");
                description1.Add("50", "");
                description1.Add("55", "");
                description1.Add("60", "");
                description1.Add("65", "");
                description1.Add("70", "");
                description1.Add("77.5", "");
                description1.Add("85", "");
                description1.Add("92.5", "");
                description1.Add("100", "");
                description1.Add("110", "");
                description1.Add("125", "");
                description1.Add("150", "");
                description1.Add("175", "");
                description1.Add("200", "");
                description1.Add("250", "");
                description1.Add("300", "");
                description1.Add("400", "");
                description1.Add("500", "");
                description1.AcceptChanges();
                return description1;
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Freight Class";
            }

            public override string TableName() => 
                "tbl_upsshipping_freightclass";
        }

        public class DropdownUpsShipping_PackagingTypeEvents : Cache.DropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Description", "Code");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
            }

            public override void ClickNew(object source, EventArgs args)
            {
            }

            public override DataTable GetTable()
            {
                Cache.TableCodeDescription description1 = new Cache.TableCodeDescription();
                description1.Add("", "");
                description1.Add("BAG", "Bag");
                description1.Add("BAL", "Bale");
                description1.Add("BAR", "Barrel");
                description1.Add("BDL", "Bundle");
                description1.Add("BIN", "Bin");
                description1.Add("BOX", "Box");
                description1.Add("BSK", "Basket");
                description1.Add("BUN", "Bunch");
                description1.Add("CAB", "Cabinet");
                description1.Add("CAN", "Can");
                description1.Add("CAR", "Carrier");
                description1.Add("CAS", "Case");
                description1.Add("CBY", "Carboy");
                description1.Add("CON", "Container");
                description1.Add("CRT", "Crate");
                description1.Add("CSK", "Cask");
                description1.Add("CTN", "Carton");
                description1.Add("CYL", "Cylinder");
                description1.Add("DRM", "Drum");
                description1.Add("LOO", "Loose");
                description1.Add("OTH", "Other");
                description1.Add("PAL", "Pail");
                description1.Add("PCS", "Pieces");
                description1.Add("PKG", "Package");
                description1.Add("PLN", "Pipe Line");
                description1.Add("PLT", "Pallet");
                description1.Add("RCK", "Rack");
                description1.Add("REL", "Reel");
                description1.Add("ROL", "Roll");
                description1.Add("SKD", "Skid");
                description1.Add("SPL", "Spool");
                description1.Add("TBE", "Tube");
                description1.Add("TNK", "Tank");
                description1.Add("UNT", "Unit");
                description1.Add("VPK", "Van Pack");
                description1.Add("WRP", "Wrapped");
                description1.AcceptChanges();
                return description1;
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Packaging Type";
            }

            public override string TableName() => 
                "tbl_upsshipping_packagingtype";
        }

        public class DropdownUserEvents : Cache.DatabaseDropdownHelper
        {
            public override void AssignDatasource(Combobox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Login", "ID");
            }

            public override void AssignDatasource(ComboBox ComboBox, DataTable table)
            {
                Functions.AssignDatasource(ComboBox, table, "Login", "ID");
            }

            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormUser(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormUser(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select User";
                e.Appearance.AutoGenerateColumns = false;
                e.Appearance.Columns.Clear();
                e.Appearance.RowHeadersWidth = 0x10;
                e.Appearance.AddTextColumn("ID", "ID", 50);
                e.Appearance.AddTextColumn("Login", "Login", 240);
            }

            public override string Query() => 
                "SELECT ID, Login\r\nFROM tbl_user\r\nORDER BY Login";

            public override string TableName() => 
                "tbl_user";
        }

        public class DropdownVendorEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormVendor(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormVendor(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Vendor";
            }

            public override string Query() => 
                "SELECT\r\n  ID,\r\n  Name,\r\n  AccountNumber\r\nFROM tbl_vendor\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_vendor";
        }

        public class DropdownWarehouseEvents : Cache.DatabaseDropdownHelper
        {
            public override void ClickEdit(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormWarehouse(), CreateHash_Edit(source, "ID"));
            }

            public override void ClickNew(object source, EventArgs args)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormWarehouse(), CreateHash_New(source));
            }

            public override void InitDialog(object source, InitDialogEventArgs e)
            {
                e.Caption = "Select Warehouse";
            }

            public override string Query() => 
                "SELECT ID, Name\r\nFROM tbl_warehouse\r\nORDER BY Name";

            public override string TableName() => 
                "tbl_warehouse";
        }

        private class ExtendedDropdownReference : Cache.ReferenceBase<ExtendedDropdown>
        {
            public ExtendedDropdownReference(string tableName, ExtendedDropdown dropdown, DMEWorks.Data.IFilter filter) : base(tableName, dropdown, filter)
            {
            }

            protected override void Invoke(ExtendedDropdown dropdown, DMEWorks.Data.IFilter filter)
            {
                if (!dropdown.Disposing)
                {
                    Form form = dropdown.FindForm();
                    if ((form != null) && (!form.Disposing && !form.IsDisposed))
                    {
                        Cache.GetDropdownHelper(base.TableName).InitDropdown(dropdown, filter);
                        Cache.AddReference(this);
                    }
                }
            }
        }

        private class FindDialogReference : Cache.ReferenceBase<FindDialog>
        {
            public FindDialogReference(string tableName, FindDialog dialog, DMEWorks.Data.IFilter filter) : base(tableName, dialog, filter)
            {
            }

            protected override void Invoke(FindDialog dialog, DMEWorks.Data.IFilter filter)
            {
                Cache.GetDropdownHelper(base.TableName).InitDialog(dialog, filter);
                Cache.AddReference(this);
            }
        }

        private interface IReference
        {
            void Invoke();

            System.ComponentModel.Component Component { get; }

            string TableName { get; }
        }

        private abstract class ReferenceBase<TComponent> : Cache.IReference where TComponent: System.ComponentModel.Component
        {
            private readonly string m_TableName;
            private readonly WeakReference<TComponent> m_Component;
            private readonly WeakReference<DMEWorks.Data.IFilter> m_Filter;

            public ReferenceBase(string tableName, TComponent component, DMEWorks.Data.IFilter filter)
            {
                if (tableName == null)
                {
                    throw new ArgumentNullException("tableName");
                }
                if (component == null)
                {
                    throw new ArgumentNullException("component");
                }
                this.m_TableName = tableName;
                this.m_Component = new WeakReference<TComponent>(component);
                this.m_Filter = (filter != null) ? new WeakReference<DMEWorks.Data.IFilter>(filter) : null;
            }

            public void Invoke()
            {
                TComponent target = default(TComponent);
                if (this.m_Component.TryGetTarget(out target))
                {
                    if (this.m_Filter == null)
                    {
                        this.Invoke(target, null);
                    }
                    else
                    {
                        DMEWorks.Data.IFilter filter = null;
                        if (this.m_Filter.TryGetTarget(out filter))
                        {
                            this.Invoke(target, filter);
                        }
                    }
                }
            }

            protected abstract void Invoke(TComponent component, DMEWorks.Data.IFilter filter);

            public string TableName =>
                this.m_TableName;

            public System.ComponentModel.Component Component
            {
                get
                {
                    TComponent target = default(TComponent);
                    return (!this.m_Component.TryGetTarget(out target) ? null : ((System.ComponentModel.Component) target));
                }
            }

            public string DMEWorks.Data.Cache.IReference.TableName =>
                this.m_TableName;

            public System.ComponentModel.Component DMEWorks.Data.Cache.IReference.Component
            {
                get
                {
                    TComponent target = default(TComponent);
                    return (!this.m_Component.TryGetTarget(out target) ? null : ((System.ComponentModel.Component) target));
                }
            }
        }

        public class TableCodeDescription : TableBase
        {
            private DataColumn _col_Code;
            private DataColumn _col_Description;

            public void Add(string Code, string Description)
            {
                DataRow row = base.NewRow();
                row[this.Col_Code] = Code;
                row[this.Col_Description] = Description;
                base.Rows.Add(row);
            }

            protected override void Initialize()
            {
                this._col_Code = base.Columns["Code"];
                this._col_Description = base.Columns["Description"];
            }

            protected override void InitializeClass()
            {
                base.Columns.Add("Code", typeof(string));
                base.Columns.Add("Description", typeof(string));
                base.PrimaryKey = new DataColumn[] { base.Columns["Code"] };
            }

            public DataColumn Col_Code =>
                this._col_Code;

            public DataColumn Col_Description =>
                this._col_Description;
        }

        private class WindowsFormsComboBoxReference : Cache.ReferenceBase<ComboBox>
        {
            public WindowsFormsComboBoxReference(string tableName, ComboBox dropdown, DMEWorks.Data.IFilter filter) : base(tableName, dropdown, filter)
            {
            }

            protected override void Invoke(ComboBox dropdown, DMEWorks.Data.IFilter filter)
            {
                if (!dropdown.Disposing)
                {
                    Form form = dropdown.FindForm();
                    if ((form != null) && (!form.Disposing && !form.IsDisposed))
                    {
                        Cache.GetDropdownHelper(base.TableName).InitDropdown(dropdown, filter);
                        Cache.AddReference(this);
                    }
                }
            }
        }
    }
}

