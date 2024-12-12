namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Calendar;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonMissing=true)]
    public class FormCustomer : FormAutoIncrementMaintain
    {
        private IContainer components;
        private string F_MIR;
        private FormMirHelper F_MirHelper;

        public FormCustomer()
        {
            base.Load += new EventHandler(this.FormCustomer_Load);
            this.InitializeComponent();
            this.InitializeGrid(this.dgEquipment.Appearance);
            this.cmbDoctor1.EditButton = true;
            this.cmbDoctor1.FindButton = true;
            this.cmbDoctor1.NewButton = true;
            this.cmbDoctor2.EditButton = true;
            this.cmbDoctor2.FindButton = true;
            this.cmbDoctor2.NewButton = true;
            this.cmbReferral.EditButton = true;
            this.cmbReferral.FindButton = true;
            this.cmbReferral.NewButton = true;
            this.cmbSalesRep.EditButton = true;
            this.cmbSalesRep.FindButton = true;
            this.cmbSalesRep.NewButton = true;
            this.cmbLegalRep.EditButton = true;
            this.cmbLegalRep.FindButton = true;
            this.cmbLegalRep.NewButton = true;
            this.cmbFacility.EditButton = true;
            this.cmbFacility.FindButton = true;
            this.cmbFacility.NewButton = true;
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_customer", "tbl_customer_notes", "tbl_customertype", "tbl_company" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            options = new NavigatorOptions {
                Caption = "Insurances",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Insurances_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Insurances_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Insurances_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Insurances_NavigatorRowClick),
                Switchable = false
            };
            string[] textArray2 = new string[] { "tbl_customer", "tbl_customer_insurance", "tbl_insurancecompany", "tbl_relationship", "tbl_company" };
            options.TableNames = textArray2;
            base.AddNavigator(options);
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtPhone2);
            this.chbShipActive.CheckedChanged += new EventHandler(this._Lambda$__755-0);
            this.chbBillActive.CheckedChanged += new EventHandler(this._Lambda$__755-1);
            this.StartTrackingChanges();
        }

        [CompilerGenerated]
        private void _Lambda$__755-0(object sender, EventArgs args)
        {
            this.SetShipActiveState();
        }

        [CompilerGenerated]
        private void _Lambda$__755-1(object sender, EventArgs args)
        {
            this.SetBillActiveState();
        }

        private void Appearance_CellFormatting(object sender, GridCellFormattingEventArgs e)
        {
            DataRow dataRow = e.Row.GetDataRow();
            if (dataRow == null)
            {
                return;
            }
            else
            {
                try
                {
                    if (dataRow.Table.Columns.Contains("ActiveCount"))
                    {
                        object obj2 = dataRow["ActiveCount"];
                        if ((((obj2 is long) && (Conversions.ToLong(obj2) != 0)) || ((obj2 is int) && (Conversions.ToInteger(obj2) != 0))) || ((obj2 is short) && (Conversions.ToShort(obj2) != 0)))
                        {
                            e.CellStyle.BackColor = Color.LightSteelBlue;
                        }
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
            }
            try
            {
                if (dataRow.Table.Columns.Contains("Collections") && Cache.ConvertToBoolean(dataRow["Collections"]))
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                ProjectData.ClearProjectError();
            }
            try
            {
                if (dataRow.Table.Columns.Contains("IsInactive") && Cache.ConvertToBoolean(dataRow["IsInactive"]))
                {
                    e.CellStyle.BackColor = Color.LightCoral;
                }
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                ProjectData.ClearProjectError();
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            string text = this.txtEmail.Text;
            if (text != null)
            {
                text = text.Trim();
                if (text.Length >= 3)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("mailto:");
                    if ((0 < text.IndexOf('<')) && (0 < text.IndexOf('>')))
                    {
                        builder.Append(text);
                    }
                    else
                    {
                        string str2 = this.CName.txtLastName.Text;
                        str2 = (str2 != null) ? str2.Trim() : "";
                        string str3 = this.CName.txtFirstName.Text;
                        str3 = (str3 != null) ? str3.Trim() : "";
                        if (0 >= str2.Length)
                        {
                            builder.Append(text);
                        }
                        else
                        {
                            if (0 < str3.Length)
                            {
                                builder.Append(str3).Append(" ");
                            }
                            builder.Append(str2).Append(" <").Append(text).Append(">");
                        }
                    }
                    builder.Replace(" ", "%20");
                    try
                    {
                        ClassGlobalObjects.OpenUrl(builder.ToString());
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
        }

        private void btnRefreshBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (Versioned.IsNumeric(this.ObjectID))
                {
                    this.LoadBalance(new int?(Conversions.ToInteger(this.ObjectID)));
                }
                else
                {
                    int? customerID = null;
                    this.LoadBalance(customerID);
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

        private void Clear_Customer_Equipment()
        {
            this.dgEquipment.GridSource = CreateEquipmentTable().ToGridSource();
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.F_MIR = "";
            Functions.SetComboBoxText(this.CName.cmbCourtesy, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtFirstName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtMiddleName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtLastName, DBNull.Value);
            Functions.SetTextBoxText(this.CName.txtSuffix, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone2, DBNull.Value);
            Functions.SetTextBoxText(this.txtEmail, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDateofBirth, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDeceasedDate, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbCustomerClass, DBNull.Value);
            Functions.SetTextBoxText(this.txtAccountNumber, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbBillingType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbCustomerType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbLocation, NullableConvert.ToDb(Globals.LocationID));
            Functions.SetNumericBoxValue(this.nmbTotalBalance, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbCustomerBalance, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbCollections, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillActive, DBNull.Value);
            this.SetBillActiveState();
            Functions.SetTextBoxText(this.CBillToAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CBillToAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CBillToAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.txtBillName, DBNull.Value);
            Functions.SetTextBoxText(this.CBillToAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CBillToAddress.txtZip, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbCommercialAccount, DBNull.Value);
            Functions.SetTextBoxText(this.txtDeliveryDirections, DBNull.Value);
            Functions.SetComboBoxText(this.cmbEmploymentStatus, "Unknown");
            Functions.SetComboBoxText(this.cmbGender, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbHeight, DBNull.Value);
            Functions.SetTextBoxText(this.txtLicense, DBNull.Value);
            Functions.SetComboBoxText(this.cmbMaritalStatus, "Unknown");
            Functions.SetComboBoxText(this.cmbMilitaryBranch, "N/A");
            Functions.SetComboBoxText(this.cmbMilitaryStatus, "N/A");
            Functions.SetCheckBoxChecked(this.chbShipActive, DBNull.Value);
            this.SetShipActiveState();
            Functions.SetTextBoxText(this.CShipToAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CShipToAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CShipToAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.txtShipName, DBNull.Value);
            Functions.SetTextBoxText(this.CShipToAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CShipToAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtSSNumber, DBNull.Value);
            Functions.SetComboBoxText(this.cmbStudentStatus, "N/A");
            Functions.SetNumericBoxValue(this.nmbWeight, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbSignatureOnFile, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbSignatureType, "B");
            Functions.SetNumericBoxValue(this.nmbMonthsValid, (int) 0x63);
            Functions.SetCheckBoxChecked(this.chbBlock12HCFA, true);
            Functions.SetCheckBoxChecked(this.chbBlock13HCFA, true);
            Functions.SetNumericBoxValue(this.nmbCopayPercent, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbCopayDollar, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbDeductible, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbTaxRate, NullableConvert.ToDb(ClassGlobalObjects.DefaultTaxRateID));
            Functions.SetComboBoxValue(this.cmbInvoiceForm, 4);
            Functions.SetComboBoxText(this.cmbBasis, DBNull.Value);
            Functions.SetComboBoxText(this.cmbFrequency, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbOutOfPocket, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbHardship, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbCommercialAcctCreditLimit, DBNull.Value);
            Functions.SetTextBoxText(this.txtCommercialAcctTerms, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbDoctor1, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbDoctor2, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbLegalRep, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbReferral, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbSalesRep, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbFacility, DBNull.Value);
            Functions.SetTextBoxText(this.txtEmergencyContact, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbDateOfInjury, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbReturnToWorkDate, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbFirstConsultDate, DBNull.Value);
            Functions.SetComboBoxText(this.cmbAccidentType, "No");
            Functions.SetCheckBoxChecked(this.chbEmergency, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbEmploymentRelated, DBNull.Value);
            Functions.SetTextBoxText(this.txtStateOfAccident, DBNull.Value);
            this.eddICD9_1.Text = "";
            this.eddICD9_2.Text = "";
            this.eddICD9_3.Text = "";
            this.eddICD9_4.Text = "";
            this.eddICD10_01.Text = "";
            this.eddICD10_02.Text = "";
            this.eddICD10_03.Text = "";
            this.eddICD10_04.Text = "";
            this.eddICD10_05.Text = "";
            this.eddICD10_06.Text = "";
            this.eddICD10_07.Text = "";
            this.eddICD10_08.Text = "";
            this.eddICD10_09.Text = "";
            this.eddICD10_10.Text = "";
            this.eddICD10_11.Text = "";
            this.eddICD10_12.Text = "";
            Functions.SetComboBoxValue(this.cmbPOSType, NullableConvert.ToDb(ClassGlobalObjects.DefaultPOSTypeID));
            Functions.SetCheckBoxChecked(this.chbHIPPANote, true);
            Functions.SetCheckBoxChecked(this.chbSupplierStandards, true);
            Functions.SetDateBoxValue(this.dtbInactiveDate, DBNull.Value);
            this.ControlCustomerInsurance1.ClearGrid();
            this.ControlCustomerNotes1.ClearGrid();
            this.Clear_Customer_Equipment();
        }

        private static DataTable CreateEquipmentTable()
        {
            DataTable table1 = new DataTable("Equipment");
            table1.Columns.Add("ID", typeof(int));
            table1.Columns.Add("InventoryItemID", typeof(int));
            table1.Columns.Add("InventoryItemName", typeof(string));
            table1.Columns.Add("SaleRentType", typeof(string));
            table1.Columns.Add("DOSFrom", typeof(DateTime));
            table1.Columns.Add("DOSTo", typeof(DateTime));
            return table1;
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT Count(*) FROM tbl_order WHERE CustomerID = :CustomerID";
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = ID;
                    if (0 < Convert.ToInt32(command.ExecuteScalar()))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.CommandText = "DELETE FROM tbl_customer_insurance WHERE CustomerID = :CustomerID";
                    command2.Parameters.Add("CustomerID", MySqlType.Int).Value = ID;
                    command2.ExecuteNonQuery();
                }
                using (MySqlCommand command3 = new MySqlCommand("", connection))
                {
                    command3.CommandText = "DELETE FROM tbl_customer_notes WHERE CustomerID = :CustomerID";
                    command3.Parameters.Add("CustomerID", MySqlType.Int).Value = ID;
                    command3.ExecuteNonQuery();
                }
                using (MySqlCommand command4 = new MySqlCommand("", connection))
                {
                    command4.CommandText = "DELETE FROM tbl_customer WHERE ID = :ID";
                    command4.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command4.ExecuteNonQuery())
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
            }
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

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                this.ControlCustomerNotes1.AllowState = ToAllowState(Permissions.FormCustomerNotes);
                this.ControlCustomerInsurance1.AllowState = ToAllowState(Permissions.FormCustomerInsurance);
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

        private static string GetInsurancesQuery()
        {
            string str = IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1";
            return ("SELECT\r\n  tbl_customer_insurance.CustomerID\r\n, tbl_customer_insurance.ID as CustomerInsuranceID\r\n, CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName\r\n, tbl_customer.SSNumber\r\n, tbl_customer.AccountNumber\r\n, tbl_customer_insurance.`Rank`\r\n, tbl_customer_insurance.RelationshipCode\r\n, tbl_relationship.Description as Relationship\r\n, CONCAT(tbl_customer_insurance.LastName, ', ', tbl_customer_insurance.FirstName) as InsuredName\r\n, IF(tbl_customer_insurance.RequestEligibility = 1, tbl_customer_insurance.RequestEligibilityOn, NULL) as RequestEligibilityOn\r\n, tbl_customer_insurance.PolicyNumber\r\n, tbl_customer_insurance.GroupNumber\r\n, tbl_insurancecompany.ID   as InsuranceCompanyID\r\n, tbl_insurancecompany.Name as InsuranceCompanyName\r\nFROM tbl_customer\r\n     INNER JOIN tbl_customer_insurance ON tbl_customer_insurance.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_insurancecompany ON tbl_customer_insurance.InsuranceCompanyID = tbl_insurancecompany.ID\r\n     LEFT JOIN tbl_relationship ON tbl_customer_insurance.RelationshipCode = tbl_relationship.Code\r\n     LEFT JOIN tbl_company ON tbl_company.ID = 1\r\nWHERE (" + str + ")\r\n  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))\r\nORDER BY tbl_insurancecompany.Name, tbl_customer.LastName, tbl_customer.FirstName");
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete customer '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}'?";
            messages.DeletedSuccessfully = $"Customer '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}' was successfully deleted.";
            messages.ObjectToBeDeletedIsNotFound = $"Customer '{this.CName.txtFirstName.Text} {this.CName.txtLastName.Text}' has orders and cannot be deleted therefore.";
            return messages;
        }

        private long GetNewAccountNumber()
        {
            long num;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT Max(AccountNumber + 0) as NewAccount FROM tbl_customer";
                    try
                    {
                        num = Conversions.ToLong(command.ExecuteScalar()) + 1L;
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception exception = ex;
                        throw new UserNotifyException("Cannot Generate automatic AccountNumber. Please input it manually");
                    }
                }
            }
            return num;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_customer", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.Add;
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.CName.cmbCourtesy, table, "Courtesy");
                Functions.LoadComboBoxItems(this.cmbGender, table, "Gender");
                Functions.LoadComboBoxItems(this.cmbEmploymentStatus, table, "EmploymentStatus");
                Functions.LoadComboBoxItems(this.cmbMaritalStatus, table, "MaritalStatus");
                Functions.LoadComboBoxItems(this.cmbMilitaryBranch, table, "MilitaryBranch");
                Functions.LoadComboBoxItems(this.cmbMilitaryStatus, table, "MilitaryStatus");
                Functions.LoadComboBoxItems(this.cmbStudentStatus, table, "StudentStatus");
                Functions.LoadComboBoxItems(this.cmbBasis, table, "Basis");
                Functions.LoadComboBoxItems(this.cmbFrequency, table, "Frequency");
                Functions.LoadComboBoxItems(this.cmbAccidentType, table, "AccidentType");
            }
            Cache.InitDropdown(this.cmbLocation, "tbl_location", null);
            Cache.InitDropdown(this.cmbSignatureType, "tbl_signaturetype", null);
            Cache.InitDropdown(this.eddICD9_1, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD9_2, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD9_3, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD9_4, "tbl_icd9", null);
            Cache.InitDropdown(this.eddICD10_01, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_02, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_03, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_04, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_05, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_06, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_07, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_08, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_09, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_10, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_11, "tbl_icd10", null);
            Cache.InitDropdown(this.eddICD10_12, "tbl_icd10", null);
            Cache.InitDropdown(this.cmbBillingType, "tbl_billingtype", null);
            Cache.InitDropdown(this.cmbCustomerType, "tbl_customertype", null);
            Cache.InitDropdown(this.cmbCustomerClass, "tbl_customerclass", null);
            Cache.InitDropdown(this.cmbTaxRate, "tbl_taxrate", null);
            Cache.InitDropdown(this.cmbInvoiceForm, "tbl_invoiceform", null);
            Cache.InitDropdown(this.cmbDoctor1, "tbl_doctor", null);
            Cache.InitDropdown(this.cmbDoctor2, "tbl_doctor", null);
            Cache.InitDropdown(this.cmbReferral, "tbl_referral", null);
            Cache.InitDropdown(this.cmbLegalRep, "tbl_legalrep", null);
            Cache.InitDropdown(this.cmbSalesRep, "tbl_salesrep", null);
            Cache.InitDropdown(this.cmbFacility, "tbl_facility", null);
            Cache.InitDropdown(this.cmbPOSType, "tbl_postype", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormCustomer));
            this.TabControl1 = new TabControl();
            this.tpGeneral = new TabPage();
            this.btnEmail = new Button();
            this.txtEmail = new TextBox();
            this.lblEmail = new Label();
            this.dtbInactiveDate = new UltraDateTimeEditor();
            this.lblInactiveDate = new Label();
            this.gbBalance = new GroupBox();
            this.chbCollections = new CheckBox();
            this.lblCollections = new Label();
            this.lblTotalBalance = new Label();
            this.btnRefreshBalance = new Button();
            this.nmbTotalBalance = new NumericBox();
            this.lblCustomerBalance = new Label();
            this.nmbCustomerBalance = new NumericBox();
            this.cmbPOSType = new Combobox();
            this.lblPOSType = new Label();
            this.cmbCustomerClass = new Combobox();
            this.lblCustomerClass = new Label();
            this.txtPhone = new TextBox();
            this.txtPhone2 = new TextBox();
            this.cmbBillingType = new Combobox();
            this.cmbCustomerType = new Combobox();
            this.CAddress = new ControlAddress();
            this.cmbLocation = new Combobox();
            this.txtAccountNumber = new TextBox();
            this.lblCustomerType = new Label();
            this.lblBillingType = new Label();
            this.lblLocationCode = new Label();
            this.lblAcctNum = new Label();
            this.dtbDeceasedDate = new UltraDateTimeEditor();
            this.dtbDateofBirth = new UltraDateTimeEditor();
            this.lblDeceasedDate = new Label();
            this.lblDateofBirth = new Label();
            this.lblPhone2 = new Label();
            this.lblPhone = new Label();
            this.tpNotes = new TabPage();
            this.ControlCustomerNotes1 = new ControlCustomerNotes();
            this.tpOtherAddresses = new TabPage();
            this.gbShipTo = new GroupBox();
            this.CShipToAddress = new ControlAddress();
            this.lblShipToName = new Label();
            this.txtShipName = new TextBox();
            this.chbShipActive = new CheckBox();
            this.gbBillTo = new GroupBox();
            this.CBillToAddress = new ControlAddress();
            this.lblBillToName = new Label();
            this.txtBillName = new TextBox();
            this.chbBillActive = new CheckBox();
            this.tpContacts = new TabPage();
            this.lblFacility = new Label();
            this.cmbFacility = new Combobox();
            this.txtEmergencyContact = new TextBox();
            this.lblEmergencyContact = new Label();
            this.lblLegalRep = new Label();
            this.cmbLegalRep = new Combobox();
            this.lblSalesRep = new Label();
            this.lblReferral = new Label();
            this.lblDoctor2 = new Label();
            this.lblDoctor1 = new Label();
            this.cmbSalesRep = new Combobox();
            this.cmbReferral = new Combobox();
            this.cmbDoctor2 = new Combobox();
            this.cmbDoctor1 = new Combobox();
            this.tpDiagnosis = new TabPage();
            this.TabControl2 = new TabControl();
            this.TabPage1 = new TabPage();
            this.cmbAccidentType = new ComboBox();
            this.lblAccidentType = new Label();
            this.lblDateInq = new Label();
            this.dtbDateOfInjury = new UltraDateTimeEditor();
            this.chbEmergency = new CheckBox();
            this.lblStateOfAccident = new Label();
            this.chbEmploymentRelated = new CheckBox();
            this.dtbFirstConsultDate = new UltraDateTimeEditor();
            this.txtStateOfAccident = new TextBox();
            this.dtbReturnToWorkDate = new UltraDateTimeEditor();
            this.lblRetToWorkDat = new Label();
            this.lblFirConDat = new Label();
            this.TabPage2 = new TabPage();
            this.eddICD9_2 = new ExtendedDropdown();
            this.eddICD9_4 = new ExtendedDropdown();
            this.lblICD9_1 = new Label();
            this.eddICD9_3 = new ExtendedDropdown();
            this.lblICD9_2 = new Label();
            this.eddICD9_1 = new ExtendedDropdown();
            this.lblICD9_3 = new Label();
            this.lblICD9_4 = new Label();
            this.TabPage3 = new TabPage();
            this.eddICD10_10 = new ExtendedDropdown();
            this.eddICD10_06 = new ExtendedDropdown();
            this.eddICD10_08 = new ExtendedDropdown();
            this.eddICD10_12 = new ExtendedDropdown();
            this.Label5 = new Label();
            this.eddICD10_07 = new ExtendedDropdown();
            this.Label9 = new Label();
            this.Label6 = new Label();
            this.eddICD10_05 = new ExtendedDropdown();
            this.eddICD10_11 = new ExtendedDropdown();
            this.Label7 = new Label();
            this.Label8 = new Label();
            this.Label10 = new Label();
            this.eddICD10_02 = new ExtendedDropdown();
            this.eddICD10_09 = new ExtendedDropdown();
            this.Label12 = new Label();
            this.eddICD10_04 = new ExtendedDropdown();
            this.Label11 = new Label();
            this.Label1 = new Label();
            this.eddICD10_03 = new ExtendedDropdown();
            this.Label2 = new Label();
            this.eddICD10_01 = new ExtendedDropdown();
            this.Label3 = new Label();
            this.Label4 = new Label();
            this.tpInsurance = new TabPage();
            this.ControlCustomerInsurance1 = new ControlCustomerInsurance();
            this.tpEquipment = new TabPage();
            this.dgEquipment = new FilteredGrid();
            this.tpAssignment = new TabPage();
            this.cmbInvoiceForm = new Combobox();
            this.lblInvoiceForm = new Label();
            this.chbHIPPANote = new CheckBox();
            this.chbSupplierStandards = new CheckBox();
            this.cmbSignatureType = new ComboBox();
            this.lblSignatureType = new Label();
            this.dtbSignatureOnFile = new UltraDateTimeEditor();
            this.cmbTaxRate = new Combobox();
            this.nmbOutOfPocket = new NumericBox();
            this.nmbDeductible = new NumericBox();
            this.nmbMonthsValid = new NumericBox();
            this.nmbCopayDollar = new NumericBox();
            this.nmbCopayPercent = new NumericBox();
            this.gbCommercialAccounts = new GroupBox();
            this.lblTerms = new Label();
            this.lblCreditLimit = new Label();
            this.nmbCommercialAcctCreditLimit = new NumericBox();
            this.txtCommercialAcctTerms = new TextBox();
            this.cmbFrequency = new ComboBox();
            this.cmbBasis = new ComboBox();
            this.chbHardship = new CheckBox();
            this.chbBlock13HCFA = new CheckBox();
            this.chbBlock12HCFA = new CheckBox();
            this.lblOutOfPocket = new Label();
            this.lblTaxRate = new Label();
            this.lblFrequency = new Label();
            this.lblCopayDollar = new Label();
            this.lblDeductible = new Label();
            this.lblBasis = new Label();
            this.lblCopayPercent = new Label();
            this.lblMonthsValid = new Label();
            this.lblSignatureOnFile = new Label();
            this.tpPersonal = new TabPage();
            this.txtSSNumber = new TextBox();
            this.txtDeliveryDirections = new TextBox();
            this.lblDeliveryDirections = new Label();
            this.nmbWeight = new NumericBox();
            this.nmbHeight = new NumericBox();
            this.gbMilitary = new GroupBox();
            this.cmbMilitaryBranch = new ComboBox();
            this.lblMilitaryBranch = new Label();
            this.cmbMilitaryStatus = new ComboBox();
            this.lblMilitaryStatus = new Label();
            this.gbStatus = new GroupBox();
            this.cmbMaritalStatus = new ComboBox();
            this.lblEmploymentStatus = new Label();
            this.lblStudentStatus = new Label();
            this.cmbEmploymentStatus = new ComboBox();
            this.lblMaritalStatus = new Label();
            this.cmbStudentStatus = new ComboBox();
            this.txtLicense = new TextBox();
            this.cmbGender = new ComboBox();
            this.chbCommercialAccount = new CheckBox();
            this.lblLicense = new Label();
            this.lblSSNumber = new Label();
            this.lblGender = new Label();
            this.lblWeight = new Label();
            this.lblHeight = new Label();
            this.ImageList1 = new ImageList(this.components);
            this.Panel1 = new Panel();
            this.CName = new ControlName();
            this.mnuGotoImages = new MenuItem();
            this.mnuGotoNewImage = new MenuItem();
            this.mnuGotoEligibility = new MenuItem();
            this.mnuGotoPaymentPlan = new MenuItem();
            this.mnuActionsScheduleMeeting = new MenuItem();
            this.cmsCustomerInsurancesGrid = new ContextMenuStrip(this.components);
            this.tsmiCustomerInsurancesGridRequest = new ToolStripMenuItem();
            this.cmsGridSearch = new ContextMenuStrip(this.components);
            this.tsmiGridSearchMakeInactive = new ToolStripMenuItem();
            base.tpWorkArea.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.gbBalance.SuspendLayout();
            this.tpNotes.SuspendLayout();
            this.tpOtherAddresses.SuspendLayout();
            this.gbShipTo.SuspendLayout();
            this.gbBillTo.SuspendLayout();
            this.tpContacts.SuspendLayout();
            this.tpDiagnosis.SuspendLayout();
            this.TabControl2.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.tpInsurance.SuspendLayout();
            this.tpEquipment.SuspendLayout();
            this.tpAssignment.SuspendLayout();
            this.gbCommercialAccounts.SuspendLayout();
            this.tpPersonal.SuspendLayout();
            this.gbMilitary.SuspendLayout();
            this.gbStatus.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.cmsCustomerInsurancesGrid.SuspendLayout();
            this.cmsGridSearch.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x210, 0x1a2);
            MenuItem[] items = new MenuItem[] { this.mnuGotoImages, this.mnuGotoNewImage, this.mnuGotoEligibility, this.mnuGotoPaymentPlan };
            base.cmnuGoto.MenuItems.AddRange(items);
            MenuItem[] itemArray2 = new MenuItem[] { this.mnuActionsScheduleMeeting };
            base.cmnuActions.MenuItems.AddRange(itemArray2);
            this.TabControl1.Controls.Add(this.tpGeneral);
            this.TabControl1.Controls.Add(this.tpNotes);
            this.TabControl1.Controls.Add(this.tpOtherAddresses);
            this.TabControl1.Controls.Add(this.tpContacts);
            this.TabControl1.Controls.Add(this.tpDiagnosis);
            this.TabControl1.Controls.Add(this.tpInsurance);
            this.TabControl1.Controls.Add(this.tpEquipment);
            this.TabControl1.Controls.Add(this.tpAssignment);
            this.TabControl1.Controls.Add(this.tpPersonal);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.ImageList = this.ImageList1;
            this.TabControl1.Location = new Point(0, 0x38);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x210, 0x16a);
            this.TabControl1.TabIndex = 0;
            this.tpGeneral.Controls.Add(this.btnEmail);
            this.tpGeneral.Controls.Add(this.txtEmail);
            this.tpGeneral.Controls.Add(this.lblEmail);
            this.tpGeneral.Controls.Add(this.dtbInactiveDate);
            this.tpGeneral.Controls.Add(this.lblInactiveDate);
            this.tpGeneral.Controls.Add(this.gbBalance);
            this.tpGeneral.Controls.Add(this.cmbPOSType);
            this.tpGeneral.Controls.Add(this.lblPOSType);
            this.tpGeneral.Controls.Add(this.cmbCustomerClass);
            this.tpGeneral.Controls.Add(this.lblCustomerClass);
            this.tpGeneral.Controls.Add(this.txtPhone);
            this.tpGeneral.Controls.Add(this.txtPhone2);
            this.tpGeneral.Controls.Add(this.cmbBillingType);
            this.tpGeneral.Controls.Add(this.cmbCustomerType);
            this.tpGeneral.Controls.Add(this.CAddress);
            this.tpGeneral.Controls.Add(this.cmbLocation);
            this.tpGeneral.Controls.Add(this.txtAccountNumber);
            this.tpGeneral.Controls.Add(this.lblCustomerType);
            this.tpGeneral.Controls.Add(this.lblBillingType);
            this.tpGeneral.Controls.Add(this.lblLocationCode);
            this.tpGeneral.Controls.Add(this.lblAcctNum);
            this.tpGeneral.Controls.Add(this.dtbDeceasedDate);
            this.tpGeneral.Controls.Add(this.dtbDateofBirth);
            this.tpGeneral.Controls.Add(this.lblDeceasedDate);
            this.tpGeneral.Controls.Add(this.lblDateofBirth);
            this.tpGeneral.Controls.Add(this.lblPhone2);
            this.tpGeneral.Controls.Add(this.lblPhone);
            this.tpGeneral.Location = new Point(4, 0x17);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new Size(520, 0x14f);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.btnEmail.BackgroundImage = My.Resources.Resources.ImageEmail;
            this.btnEmail.BackgroundImageLayout = ImageLayout.Center;
            this.btnEmail.FlatAppearance.BorderColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.btnEmail.FlatStyle = FlatStyle.Flat;
            this.btnEmail.Location = new Point(0x178, 0x88);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new Size(0x15, 0x15);
            this.btnEmail.TabIndex = 0x1a;
            this.txtEmail.Location = new Point(0x68, 0x88);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(0x110, 20);
            this.txtEmail.TabIndex = 6;
            this.lblEmail.Location = new Point(8, 0x88);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(0x58, 0x16);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = ContentAlignment.MiddleRight;
            this.dtbInactiveDate.Location = new Point(0x178, 0xd8);
            this.dtbInactiveDate.Name = "dtbInactiveDate";
            this.dtbInactiveDate.Size = new Size(0x70, 0x15);
            this.dtbInactiveDate.TabIndex = 0x18;
            this.lblInactiveDate.Location = new Point(0x128, 0xd8);
            this.lblInactiveDate.Name = "lblInactiveDate";
            this.lblInactiveDate.Size = new Size(0x48, 0x16);
            this.lblInactiveDate.TabIndex = 0x17;
            this.lblInactiveDate.Text = "Inactive Date";
            this.lblInactiveDate.TextAlign = ContentAlignment.MiddleRight;
            this.gbBalance.Controls.Add(this.chbCollections);
            this.gbBalance.Controls.Add(this.lblCollections);
            this.gbBalance.Controls.Add(this.lblTotalBalance);
            this.gbBalance.Controls.Add(this.btnRefreshBalance);
            this.gbBalance.Controls.Add(this.nmbTotalBalance);
            this.gbBalance.Controls.Add(this.lblCustomerBalance);
            this.gbBalance.Controls.Add(this.nmbCustomerBalance);
            this.gbBalance.Location = new Point(0x128, 240);
            this.gbBalance.Name = "gbBalance";
            this.gbBalance.Size = new Size(200, 0x60);
            this.gbBalance.TabIndex = 0x19;
            this.gbBalance.TabStop = false;
            this.gbBalance.Text = "Balance";
            this.chbCollections.Location = new Point(80, 0x40);
            this.chbCollections.Name = "chbCollections";
            this.chbCollections.Size = new Size(0x58, 0x16);
            this.chbCollections.TabIndex = 6;
            this.chbCollections.TextAlign = ContentAlignment.MiddleRight;
            this.lblCollections.Location = new Point(8, 0x40);
            this.lblCollections.Name = "lblCollections";
            this.lblCollections.Size = new Size(0x40, 0x16);
            this.lblCollections.TabIndex = 5;
            this.lblCollections.Text = "Collections";
            this.lblCollections.TextAlign = ContentAlignment.MiddleRight;
            this.lblTotalBalance.Location = new Point(8, 0x10);
            this.lblTotalBalance.Name = "lblTotalBalance";
            this.lblTotalBalance.Size = new Size(0x40, 0x16);
            this.lblTotalBalance.TabIndex = 0;
            this.lblTotalBalance.Text = "Total";
            this.lblTotalBalance.TextAlign = ContentAlignment.MiddleRight;
            this.btnRefreshBalance.FlatStyle = FlatStyle.Flat;
            this.btnRefreshBalance.Image = My.Resources.Resources.ImageRefresh3;
            this.btnRefreshBalance.Location = new Point(0xa8, 40);
            this.btnRefreshBalance.Name = "btnRefreshBalance";
            this.btnRefreshBalance.Size = new Size(0x15, 0x15);
            this.btnRefreshBalance.TabIndex = 4;
            base.ToolTip1.SetToolTip(this.btnRefreshBalance, "Refresh balance");
            this.nmbTotalBalance.Location = new Point(80, 0x10);
            this.nmbTotalBalance.Name = "nmbTotalBalance";
            this.nmbTotalBalance.Size = new Size(0x58, 20);
            this.nmbTotalBalance.TabIndex = 1;
            this.lblCustomerBalance.Location = new Point(8, 40);
            this.lblCustomerBalance.Name = "lblCustomerBalance";
            this.lblCustomerBalance.Size = new Size(0x40, 0x16);
            this.lblCustomerBalance.TabIndex = 2;
            this.lblCustomerBalance.Text = "Customer";
            this.lblCustomerBalance.TextAlign = ContentAlignment.MiddleRight;
            this.nmbCustomerBalance.Location = new Point(80, 40);
            this.nmbCustomerBalance.Name = "nmbCustomerBalance";
            this.nmbCustomerBalance.Size = new Size(0x58, 20);
            this.nmbCustomerBalance.TabIndex = 3;
            this.cmbPOSType.Location = new Point(0x68, 0x120);
            this.cmbPOSType.Name = "cmbPOSType";
            this.cmbPOSType.Size = new Size(0xa8, 0x15);
            this.cmbPOSType.TabIndex = 0x12;
            this.lblPOSType.Location = new Point(8, 0x120);
            this.lblPOSType.Name = "lblPOSType";
            this.lblPOSType.Size = new Size(0x58, 0x16);
            this.lblPOSType.TabIndex = 0x11;
            this.lblPOSType.Text = "POS";
            this.lblPOSType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomerClass.Location = new Point(0x68, 0xa8);
            this.cmbCustomerClass.Name = "cmbCustomerClass";
            this.cmbCustomerClass.Size = new Size(0xa8, 0x15);
            this.cmbCustomerClass.TabIndex = 8;
            this.lblCustomerClass.Location = new Point(8, 0xa8);
            this.lblCustomerClass.Name = "lblCustomerClass";
            this.lblCustomerClass.Size = new Size(0x58, 0x16);
            this.lblCustomerClass.TabIndex = 7;
            this.lblCustomerClass.Text = "Customer Class";
            this.lblCustomerClass.TextAlign = ContentAlignment.MiddleRight;
            this.txtPhone.Location = new Point(0x68, 0x58);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(0x128, 20);
            this.txtPhone.TabIndex = 2;
            this.txtPhone2.Location = new Point(0x68, 0x70);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new Size(0x128, 20);
            this.txtPhone2.TabIndex = 4;
            this.cmbBillingType.Location = new Point(0x68, 240);
            this.cmbBillingType.Name = "cmbBillingType";
            this.cmbBillingType.Size = new Size(0xa8, 0x15);
            this.cmbBillingType.TabIndex = 14;
            this.cmbCustomerType.Location = new Point(0x68, 0x108);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.Size = new Size(0xa8, 0x15);
            this.cmbCustomerType.TabIndex = 0x10;
            this.CAddress.Location = new Point(0x20, 8);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x170, 0x48);
            this.CAddress.TabIndex = 0;
            this.cmbLocation.Location = new Point(0x68, 0xd8);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new Size(0xa8, 0x15);
            this.cmbLocation.TabIndex = 12;
            this.txtAccountNumber.Location = new Point(0x68, 0xc0);
            this.txtAccountNumber.MaxLength = 0;
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new Size(0x90, 20);
            this.txtAccountNumber.TabIndex = 10;
            this.lblCustomerType.Location = new Point(8, 0x108);
            this.lblCustomerType.Name = "lblCustomerType";
            this.lblCustomerType.Size = new Size(0x58, 0x16);
            this.lblCustomerType.TabIndex = 15;
            this.lblCustomerType.Text = "Customer Type";
            this.lblCustomerType.TextAlign = ContentAlignment.MiddleRight;
            this.lblBillingType.Location = new Point(8, 240);
            this.lblBillingType.Name = "lblBillingType";
            this.lblBillingType.Size = new Size(0x58, 0x16);
            this.lblBillingType.TabIndex = 13;
            this.lblBillingType.Text = "Billing Type";
            this.lblBillingType.TextAlign = ContentAlignment.MiddleRight;
            this.lblLocationCode.Location = new Point(8, 0xd8);
            this.lblLocationCode.Name = "lblLocationCode";
            this.lblLocationCode.Size = new Size(0x58, 0x16);
            this.lblLocationCode.TabIndex = 11;
            this.lblLocationCode.Text = "Location";
            this.lblLocationCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblAcctNum.Location = new Point(8, 0xc0);
            this.lblAcctNum.Name = "lblAcctNum";
            this.lblAcctNum.Size = new Size(0x58, 0x16);
            this.lblAcctNum.TabIndex = 9;
            this.lblAcctNum.Text = "&ACCT#";
            this.lblAcctNum.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDeceasedDate.Location = new Point(0x178, 0xc0);
            this.dtbDeceasedDate.Name = "dtbDeceasedDate";
            this.dtbDeceasedDate.Size = new Size(0x70, 0x15);
            this.dtbDeceasedDate.TabIndex = 0x16;
            this.dtbDateofBirth.Location = new Point(0x178, 0xa8);
            this.dtbDateofBirth.Name = "dtbDateofBirth";
            this.dtbDateofBirth.Size = new Size(0x70, 0x15);
            this.dtbDateofBirth.TabIndex = 20;
            this.lblDeceasedDate.Location = new Point(0x128, 0xc0);
            this.lblDeceasedDate.Name = "lblDeceasedDate";
            this.lblDeceasedDate.Size = new Size(0x48, 0x16);
            this.lblDeceasedDate.TabIndex = 0x15;
            this.lblDeceasedDate.Text = "Deceased";
            this.lblDeceasedDate.TextAlign = ContentAlignment.MiddleRight;
            this.lblDateofBirth.Location = new Point(0x128, 0xa8);
            this.lblDateofBirth.Name = "lblDateofBirth";
            this.lblDateofBirth.Size = new Size(0x48, 0x16);
            this.lblDateofBirth.TabIndex = 0x13;
            this.lblDateofBirth.Text = "DOB";
            this.lblDateofBirth.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone2.Location = new Point(8, 0x70);
            this.lblPhone2.Name = "lblPhone2";
            this.lblPhone2.Size = new Size(0x58, 0x16);
            this.lblPhone2.TabIndex = 3;
            this.lblPhone2.Text = "Phone 2";
            this.lblPhone2.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone.Location = new Point(8, 0x58);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new Size(0x58, 0x16);
            this.lblPhone.TabIndex = 1;
            this.lblPhone.Text = "Phone";
            this.lblPhone.TextAlign = ContentAlignment.MiddleRight;
            this.tpNotes.Controls.Add(this.ControlCustomerNotes1);
            this.tpNotes.Location = new Point(4, 0x17);
            this.tpNotes.Name = "tpNotes";
            this.tpNotes.Size = new Size(520, 0x157);
            this.tpNotes.TabIndex = 6;
            this.tpNotes.Text = "Notes";
            this.tpNotes.Visible = false;
            this.ControlCustomerNotes1.Dock = DockStyle.Fill;
            this.ControlCustomerNotes1.Location = new Point(0, 0);
            this.ControlCustomerNotes1.Name = "ControlCustomerNotes1";
            this.ControlCustomerNotes1.Size = new Size(520, 0x157);
            this.ControlCustomerNotes1.TabIndex = 0;
            this.tpOtherAddresses.Controls.Add(this.gbShipTo);
            this.tpOtherAddresses.Controls.Add(this.gbBillTo);
            this.tpOtherAddresses.Location = new Point(4, 0x17);
            this.tpOtherAddresses.Name = "tpOtherAddresses";
            this.tpOtherAddresses.Size = new Size(520, 0x157);
            this.tpOtherAddresses.TabIndex = 4;
            this.tpOtherAddresses.Text = "Other Addresses";
            this.tpOtherAddresses.Visible = false;
            this.gbShipTo.Controls.Add(this.CShipToAddress);
            this.gbShipTo.Controls.Add(this.lblShipToName);
            this.gbShipTo.Controls.Add(this.txtShipName);
            this.gbShipTo.Controls.Add(this.chbShipActive);
            this.gbShipTo.Location = new Point(8, 8);
            this.gbShipTo.Name = "gbShipTo";
            this.gbShipTo.Size = new Size(0x1b0, 0x90);
            this.gbShipTo.TabIndex = 0;
            this.gbShipTo.TabStop = false;
            this.gbShipTo.Text = "Ship To Address";
            this.CShipToAddress.Location = new Point(8, 0x40);
            this.CShipToAddress.Name = "CShipToAddress";
            this.CShipToAddress.Size = new Size(0x198, 0x48);
            this.CShipToAddress.TabIndex = 3;
            this.lblShipToName.Location = new Point(8, 40);
            this.lblShipToName.Name = "lblShipToName";
            this.lblShipToName.Size = new Size(0x40, 0x16);
            this.lblShipToName.TabIndex = 1;
            this.lblShipToName.Text = "Name";
            this.lblShipToName.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipName.AcceptsReturn = true;
            this.txtShipName.Location = new Point(80, 40);
            this.txtShipName.MaxLength = 0;
            this.txtShipName.Name = "txtShipName";
            this.txtShipName.Size = new Size(0x150, 20);
            this.txtShipName.TabIndex = 2;
            this.chbShipActive.CheckAlign = ContentAlignment.MiddleRight;
            this.chbShipActive.Location = new Point(8, 0x10);
            this.chbShipActive.Name = "chbShipActive";
            this.chbShipActive.Size = new Size(0x40, 0x16);
            this.chbShipActive.TabIndex = 0;
            this.chbShipActive.Text = "Activate";
            this.chbShipActive.TextAlign = ContentAlignment.MiddleRight;
            this.gbBillTo.Controls.Add(this.CBillToAddress);
            this.gbBillTo.Controls.Add(this.lblBillToName);
            this.gbBillTo.Controls.Add(this.txtBillName);
            this.gbBillTo.Controls.Add(this.chbBillActive);
            this.gbBillTo.Location = new Point(8, 160);
            this.gbBillTo.Name = "gbBillTo";
            this.gbBillTo.Size = new Size(0x1b0, 0x90);
            this.gbBillTo.TabIndex = 1;
            this.gbBillTo.TabStop = false;
            this.gbBillTo.Text = "Bill To Address";
            this.CBillToAddress.Location = new Point(8, 0x40);
            this.CBillToAddress.Name = "CBillToAddress";
            this.CBillToAddress.Size = new Size(0x198, 0x48);
            this.CBillToAddress.TabIndex = 3;
            this.lblBillToName.Location = new Point(8, 40);
            this.lblBillToName.Name = "lblBillToName";
            this.lblBillToName.Size = new Size(0x40, 0x16);
            this.lblBillToName.TabIndex = 1;
            this.lblBillToName.Text = "Name";
            this.lblBillToName.TextAlign = ContentAlignment.MiddleRight;
            this.txtBillName.AcceptsReturn = true;
            this.txtBillName.Location = new Point(80, 40);
            this.txtBillName.MaxLength = 0;
            this.txtBillName.Name = "txtBillName";
            this.txtBillName.Size = new Size(0x150, 20);
            this.txtBillName.TabIndex = 2;
            this.chbBillActive.CheckAlign = ContentAlignment.MiddleRight;
            this.chbBillActive.Location = new Point(8, 0x10);
            this.chbBillActive.Name = "chbBillActive";
            this.chbBillActive.Size = new Size(0x40, 0x16);
            this.chbBillActive.TabIndex = 0;
            this.chbBillActive.Text = "Activate";
            this.chbBillActive.TextAlign = ContentAlignment.MiddleRight;
            this.tpContacts.Controls.Add(this.lblFacility);
            this.tpContacts.Controls.Add(this.cmbFacility);
            this.tpContacts.Controls.Add(this.txtEmergencyContact);
            this.tpContacts.Controls.Add(this.lblEmergencyContact);
            this.tpContacts.Controls.Add(this.lblLegalRep);
            this.tpContacts.Controls.Add(this.cmbLegalRep);
            this.tpContacts.Controls.Add(this.lblSalesRep);
            this.tpContacts.Controls.Add(this.lblReferral);
            this.tpContacts.Controls.Add(this.lblDoctor2);
            this.tpContacts.Controls.Add(this.lblDoctor1);
            this.tpContacts.Controls.Add(this.cmbSalesRep);
            this.tpContacts.Controls.Add(this.cmbReferral);
            this.tpContacts.Controls.Add(this.cmbDoctor2);
            this.tpContacts.Controls.Add(this.cmbDoctor1);
            this.tpContacts.Location = new Point(4, 0x17);
            this.tpContacts.Name = "tpContacts";
            this.tpContacts.Size = new Size(520, 0x157);
            this.tpContacts.TabIndex = 2;
            this.tpContacts.Text = "Contacts";
            this.tpContacts.Visible = false;
            this.lblFacility.BackColor = Color.Transparent;
            this.lblFacility.Location = new Point(8, 0x80);
            this.lblFacility.Name = "lblFacility";
            this.lblFacility.Size = new Size(80, 0x16);
            this.lblFacility.TabIndex = 10;
            this.lblFacility.Text = "Facility";
            this.lblFacility.TextAlign = ContentAlignment.MiddleRight;
            this.cmbFacility.Location = new Point(0x60, 0x80);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new Size(320, 0x15);
            this.cmbFacility.TabIndex = 11;
            this.txtEmergencyContact.AcceptsReturn = true;
            this.txtEmergencyContact.Location = new Point(8, 0xb0);
            this.txtEmergencyContact.MaxLength = 0;
            this.txtEmergencyContact.Multiline = true;
            this.txtEmergencyContact.Name = "txtEmergencyContact";
            this.txtEmergencyContact.ScrollBars = ScrollBars.Vertical;
            this.txtEmergencyContact.Size = new Size(0x198, 0x81);
            this.txtEmergencyContact.TabIndex = 13;
            this.lblEmergencyContact.BackColor = Color.FromArgb(0xc0, 0xff, 0xc0);
            this.lblEmergencyContact.Location = new Point(8, 0x98);
            this.lblEmergencyContact.Name = "lblEmergencyContact";
            this.lblEmergencyContact.Size = new Size(0x198, 0x19);
            this.lblEmergencyContact.TabIndex = 12;
            this.lblEmergencyContact.Text = "Emergency Contact:";
            this.lblEmergencyContact.TextAlign = ContentAlignment.MiddleLeft;
            this.lblLegalRep.BackColor = Color.Transparent;
            this.lblLegalRep.Location = new Point(8, 0x68);
            this.lblLegalRep.Name = "lblLegalRep";
            this.lblLegalRep.Size = new Size(80, 0x16);
            this.lblLegalRep.TabIndex = 8;
            this.lblLegalRep.Text = "Legal Rep";
            this.lblLegalRep.TextAlign = ContentAlignment.MiddleRight;
            this.cmbLegalRep.Location = new Point(0x60, 0x68);
            this.cmbLegalRep.Name = "cmbLegalRep";
            this.cmbLegalRep.Size = new Size(320, 0x15);
            this.cmbLegalRep.TabIndex = 9;
            this.lblSalesRep.BackColor = Color.Transparent;
            this.lblSalesRep.Location = new Point(8, 80);
            this.lblSalesRep.Name = "lblSalesRep";
            this.lblSalesRep.Size = new Size(80, 0x16);
            this.lblSalesRep.TabIndex = 6;
            this.lblSalesRep.Text = "Sales Person";
            this.lblSalesRep.TextAlign = ContentAlignment.MiddleRight;
            this.lblReferral.BackColor = Color.Transparent;
            this.lblReferral.Location = new Point(8, 0x38);
            this.lblReferral.Name = "lblReferral";
            this.lblReferral.Size = new Size(80, 0x16);
            this.lblReferral.TabIndex = 4;
            this.lblReferral.Text = "Referral";
            this.lblReferral.TextAlign = ContentAlignment.MiddleRight;
            this.lblDoctor2.BackColor = Color.Transparent;
            this.lblDoctor2.Location = new Point(8, 0x20);
            this.lblDoctor2.Name = "lblDoctor2";
            this.lblDoctor2.Size = new Size(80, 0x16);
            this.lblDoctor2.TabIndex = 2;
            this.lblDoctor2.Text = "Doctor 2";
            this.lblDoctor2.TextAlign = ContentAlignment.MiddleRight;
            this.lblDoctor1.BackColor = Color.Transparent;
            this.lblDoctor1.Location = new Point(8, 8);
            this.lblDoctor1.Name = "lblDoctor1";
            this.lblDoctor1.Size = new Size(80, 0x16);
            this.lblDoctor1.TabIndex = 0;
            this.lblDoctor1.Text = "Doctor1";
            this.lblDoctor1.TextAlign = ContentAlignment.MiddleRight;
            this.cmbSalesRep.Location = new Point(0x60, 80);
            this.cmbSalesRep.Name = "cmbSalesRep";
            this.cmbSalesRep.Size = new Size(320, 0x15);
            this.cmbSalesRep.TabIndex = 7;
            this.cmbReferral.Location = new Point(0x60, 0x38);
            this.cmbReferral.Name = "cmbReferral";
            this.cmbReferral.Size = new Size(320, 0x15);
            this.cmbReferral.TabIndex = 5;
            this.cmbDoctor2.Location = new Point(0x60, 0x20);
            this.cmbDoctor2.Name = "cmbDoctor2";
            this.cmbDoctor2.Size = new Size(320, 0x15);
            this.cmbDoctor2.TabIndex = 3;
            this.cmbDoctor1.Location = new Point(0x60, 8);
            this.cmbDoctor1.Name = "cmbDoctor1";
            this.cmbDoctor1.Size = new Size(320, 0x15);
            this.cmbDoctor1.TabIndex = 1;
            this.tpDiagnosis.Controls.Add(this.TabControl2);
            this.tpDiagnosis.Location = new Point(4, 0x17);
            this.tpDiagnosis.Name = "tpDiagnosis";
            this.tpDiagnosis.Size = new Size(520, 0x157);
            this.tpDiagnosis.TabIndex = 7;
            this.tpDiagnosis.Text = "Diagnosis";
            this.tpDiagnosis.Visible = false;
            this.TabControl2.Controls.Add(this.TabPage1);
            this.TabControl2.Controls.Add(this.TabPage2);
            this.TabControl2.Controls.Add(this.TabPage3);
            this.TabControl2.Dock = DockStyle.Fill;
            this.TabControl2.Location = new Point(0, 0);
            this.TabControl2.Name = "TabControl2";
            this.TabControl2.SelectedIndex = 0;
            this.TabControl2.Size = new Size(520, 0x157);
            this.TabControl2.TabIndex = 2;
            this.TabPage1.Controls.Add(this.cmbAccidentType);
            this.TabPage1.Controls.Add(this.lblAccidentType);
            this.TabPage1.Controls.Add(this.lblDateInq);
            this.TabPage1.Controls.Add(this.dtbDateOfInjury);
            this.TabPage1.Controls.Add(this.chbEmergency);
            this.TabPage1.Controls.Add(this.lblStateOfAccident);
            this.TabPage1.Controls.Add(this.chbEmploymentRelated);
            this.TabPage1.Controls.Add(this.dtbFirstConsultDate);
            this.TabPage1.Controls.Add(this.txtStateOfAccident);
            this.TabPage1.Controls.Add(this.dtbReturnToWorkDate);
            this.TabPage1.Controls.Add(this.lblRetToWorkDat);
            this.TabPage1.Controls.Add(this.lblFirConDat);
            this.TabPage1.Location = new Point(4, 0x16);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new Padding(3);
            this.TabPage1.Size = new Size(0x200, 0x13d);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Workers Comp/ Auto Accident";
            this.TabPage1.UseVisualStyleBackColor = true;
            this.cmbAccidentType.Location = new Point(0x80, 80);
            this.cmbAccidentType.Name = "cmbAccidentType";
            this.cmbAccidentType.Size = new Size(0x68, 0x15);
            this.cmbAccidentType.TabIndex = 7;
            this.lblAccidentType.Location = new Point(8, 80);
            this.lblAccidentType.Name = "lblAccidentType";
            this.lblAccidentType.Size = new Size(0x70, 0x16);
            this.lblAccidentType.TabIndex = 6;
            this.lblAccidentType.Text = "Accident";
            this.lblAccidentType.TextAlign = ContentAlignment.MiddleRight;
            this.lblDateInq.Location = new Point(8, 8);
            this.lblDateInq.Name = "lblDateInq";
            this.lblDateInq.Size = new Size(0x70, 0x16);
            this.lblDateInq.TabIndex = 0;
            this.lblDateInq.Text = "Date of Injury";
            this.lblDateInq.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDateOfInjury.Location = new Point(0x80, 8);
            this.dtbDateOfInjury.Name = "dtbDateOfInjury";
            this.dtbDateOfInjury.Size = new Size(0x68, 0x15);
            this.dtbDateOfInjury.TabIndex = 1;
            this.chbEmergency.CheckAlign = ContentAlignment.MiddleRight;
            this.chbEmergency.Location = new Point(8, 0x80);
            this.chbEmergency.Name = "chbEmergency";
            this.chbEmergency.Size = new Size(0x88, 0x16);
            this.chbEmergency.TabIndex = 10;
            this.chbEmergency.Text = "Emergency";
            this.chbEmergency.TextAlign = ContentAlignment.MiddleRight;
            this.lblStateOfAccident.Location = new Point(8, 0x68);
            this.lblStateOfAccident.Name = "lblStateOfAccident";
            this.lblStateOfAccident.Size = new Size(0x70, 0x16);
            this.lblStateOfAccident.TabIndex = 8;
            this.lblStateOfAccident.Text = "State of Injury";
            this.lblStateOfAccident.TextAlign = ContentAlignment.MiddleRight;
            this.chbEmploymentRelated.CheckAlign = ContentAlignment.MiddleRight;
            this.chbEmploymentRelated.Location = new Point(8, 0x98);
            this.chbEmploymentRelated.Name = "chbEmploymentRelated";
            this.chbEmploymentRelated.Size = new Size(0x88, 0x16);
            this.chbEmploymentRelated.TabIndex = 11;
            this.chbEmploymentRelated.Text = "Employment Related";
            this.chbEmploymentRelated.TextAlign = ContentAlignment.MiddleRight;
            this.dtbFirstConsultDate.Location = new Point(0x80, 0x38);
            this.dtbFirstConsultDate.Name = "dtbFirstConsultDate";
            this.dtbFirstConsultDate.Size = new Size(0x68, 0x15);
            this.dtbFirstConsultDate.TabIndex = 5;
            this.txtStateOfAccident.Location = new Point(0x80, 0x68);
            this.txtStateOfAccident.MaxLength = 2;
            this.txtStateOfAccident.Name = "txtStateOfAccident";
            this.txtStateOfAccident.Size = new Size(0x68, 20);
            this.txtStateOfAccident.TabIndex = 9;
            this.dtbReturnToWorkDate.Location = new Point(0x80, 0x20);
            this.dtbReturnToWorkDate.Name = "dtbReturnToWorkDate";
            this.dtbReturnToWorkDate.Size = new Size(0x68, 0x15);
            this.dtbReturnToWorkDate.TabIndex = 3;
            this.lblRetToWorkDat.Location = new Point(8, 0x20);
            this.lblRetToWorkDat.Name = "lblRetToWorkDat";
            this.lblRetToWorkDat.Size = new Size(0x70, 0x16);
            this.lblRetToWorkDat.TabIndex = 2;
            this.lblRetToWorkDat.Text = "Return to Work Date";
            this.lblRetToWorkDat.TextAlign = ContentAlignment.MiddleRight;
            this.lblFirConDat.Location = new Point(8, 0x38);
            this.lblFirConDat.Name = "lblFirConDat";
            this.lblFirConDat.Size = new Size(0x70, 0x16);
            this.lblFirConDat.TabIndex = 4;
            this.lblFirConDat.Text = "First Consult Date";
            this.lblFirConDat.TextAlign = ContentAlignment.MiddleRight;
            this.TabPage2.Controls.Add(this.eddICD9_2);
            this.TabPage2.Controls.Add(this.eddICD9_4);
            this.TabPage2.Controls.Add(this.lblICD9_1);
            this.TabPage2.Controls.Add(this.eddICD9_3);
            this.TabPage2.Controls.Add(this.lblICD9_2);
            this.TabPage2.Controls.Add(this.eddICD9_1);
            this.TabPage2.Controls.Add(this.lblICD9_3);
            this.TabPage2.Controls.Add(this.lblICD9_4);
            this.TabPage2.Location = new Point(4, 0x16);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new Padding(3);
            this.TabPage2.Size = new Size(0x200, 0x13d);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "ICD 9";
            this.TabPage2.UseVisualStyleBackColor = true;
            this.eddICD9_2.Location = new Point(0x40, 0x20);
            this.eddICD9_2.Name = "eddICD9_2";
            this.eddICD9_2.Size = new Size(280, 0x15);
            this.eddICD9_2.TabIndex = 3;
            this.eddICD9_2.TextMember = "";
            this.eddICD9_4.Location = new Point(0x40, 80);
            this.eddICD9_4.Name = "eddICD9_4";
            this.eddICD9_4.Size = new Size(280, 0x15);
            this.eddICD9_4.TabIndex = 7;
            this.eddICD9_4.TextMember = "";
            this.lblICD9_1.Location = new Point(8, 8);
            this.lblICD9_1.Name = "lblICD9_1";
            this.lblICD9_1.Size = new Size(0x30, 0x16);
            this.lblICD9_1.TabIndex = 0;
            this.lblICD9_1.Text = "ICD9 1";
            this.lblICD9_1.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD9_3.Location = new Point(0x40, 0x38);
            this.eddICD9_3.Name = "eddICD9_3";
            this.eddICD9_3.Size = new Size(280, 0x15);
            this.eddICD9_3.TabIndex = 5;
            this.eddICD9_3.TextMember = "";
            this.lblICD9_2.Location = new Point(8, 0x20);
            this.lblICD9_2.Name = "lblICD9_2";
            this.lblICD9_2.Size = new Size(0x30, 0x16);
            this.lblICD9_2.TabIndex = 2;
            this.lblICD9_2.Text = "ICD9 2";
            this.lblICD9_2.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD9_1.Location = new Point(0x40, 8);
            this.eddICD9_1.Name = "eddICD9_1";
            this.eddICD9_1.Size = new Size(280, 0x15);
            this.eddICD9_1.TabIndex = 1;
            this.eddICD9_1.TextMember = "";
            this.lblICD9_3.Location = new Point(8, 0x38);
            this.lblICD9_3.Name = "lblICD9_3";
            this.lblICD9_3.Size = new Size(0x30, 0x16);
            this.lblICD9_3.TabIndex = 4;
            this.lblICD9_3.Text = "ICD9 3";
            this.lblICD9_3.TextAlign = ContentAlignment.MiddleRight;
            this.lblICD9_4.Location = new Point(8, 80);
            this.lblICD9_4.Name = "lblICD9_4";
            this.lblICD9_4.Size = new Size(0x30, 0x16);
            this.lblICD9_4.TabIndex = 6;
            this.lblICD9_4.Text = "ICD9 4";
            this.lblICD9_4.TextAlign = ContentAlignment.MiddleRight;
            this.TabPage3.Controls.Add(this.eddICD10_10);
            this.TabPage3.Controls.Add(this.eddICD10_06);
            this.TabPage3.Controls.Add(this.eddICD10_08);
            this.TabPage3.Controls.Add(this.eddICD10_12);
            this.TabPage3.Controls.Add(this.Label5);
            this.TabPage3.Controls.Add(this.eddICD10_07);
            this.TabPage3.Controls.Add(this.Label9);
            this.TabPage3.Controls.Add(this.Label6);
            this.TabPage3.Controls.Add(this.eddICD10_05);
            this.TabPage3.Controls.Add(this.eddICD10_11);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.Label10);
            this.TabPage3.Controls.Add(this.eddICD10_02);
            this.TabPage3.Controls.Add(this.eddICD10_09);
            this.TabPage3.Controls.Add(this.Label12);
            this.TabPage3.Controls.Add(this.eddICD10_04);
            this.TabPage3.Controls.Add(this.Label11);
            this.TabPage3.Controls.Add(this.Label1);
            this.TabPage3.Controls.Add(this.eddICD10_03);
            this.TabPage3.Controls.Add(this.Label2);
            this.TabPage3.Controls.Add(this.eddICD10_01);
            this.TabPage3.Controls.Add(this.Label3);
            this.TabPage3.Controls.Add(this.Label4);
            this.TabPage3.Location = new Point(4, 0x16);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new Padding(3);
            this.TabPage3.Size = new Size(0x200, 0x13d);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "ICD 10";
            this.TabPage3.UseVisualStyleBackColor = true;
            this.eddICD10_10.Location = new Point(0x40, 0xe0);
            this.eddICD10_10.Name = "eddICD10_10";
            this.eddICD10_10.Size = new Size(280, 0x15);
            this.eddICD10_10.TabIndex = 0x13;
            this.eddICD10_10.TextMember = "";
            this.eddICD10_06.Location = new Point(0x40, 0x80);
            this.eddICD10_06.Name = "eddICD10_06";
            this.eddICD10_06.Size = new Size(280, 0x15);
            this.eddICD10_06.TabIndex = 11;
            this.eddICD10_06.TextMember = "";
            this.eddICD10_08.Location = new Point(0x40, 0xb0);
            this.eddICD10_08.Name = "eddICD10_08";
            this.eddICD10_08.Size = new Size(280, 0x15);
            this.eddICD10_08.TabIndex = 15;
            this.eddICD10_08.TextMember = "";
            this.eddICD10_12.Location = new Point(0x40, 0x110);
            this.eddICD10_12.Name = "eddICD10_12";
            this.eddICD10_12.Size = new Size(280, 0x15);
            this.eddICD10_12.TabIndex = 0x17;
            this.eddICD10_12.TextMember = "";
            this.Label5.Location = new Point(0x10, 0x68);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x20, 20);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "# 5";
            this.Label5.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_07.Location = new Point(0x40, 0x98);
            this.eddICD10_07.Name = "eddICD10_07";
            this.eddICD10_07.Size = new Size(280, 0x15);
            this.eddICD10_07.TabIndex = 13;
            this.eddICD10_07.TextMember = "";
            this.Label9.Location = new Point(0x10, 200);
            this.Label9.Name = "Label9";
            this.Label9.Size = new Size(0x20, 20);
            this.Label9.TabIndex = 0x10;
            this.Label9.Text = "# 9";
            this.Label9.TextAlign = ContentAlignment.MiddleRight;
            this.Label6.Location = new Point(0x10, 0x80);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x20, 20);
            this.Label6.TabIndex = 10;
            this.Label6.Text = "# 6";
            this.Label6.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_05.Location = new Point(0x40, 0x68);
            this.eddICD10_05.Name = "eddICD10_05";
            this.eddICD10_05.Size = new Size(280, 0x15);
            this.eddICD10_05.TabIndex = 9;
            this.eddICD10_05.TextMember = "";
            this.eddICD10_11.Location = new Point(0x40, 0xf8);
            this.eddICD10_11.Name = "eddICD10_11";
            this.eddICD10_11.Size = new Size(280, 0x15);
            this.eddICD10_11.TabIndex = 0x15;
            this.eddICD10_11.TextMember = "";
            this.Label7.Location = new Point(0x10, 0x98);
            this.Label7.Name = "Label7";
            this.Label7.Size = new Size(0x20, 20);
            this.Label7.TabIndex = 12;
            this.Label7.Text = "# 7";
            this.Label7.TextAlign = ContentAlignment.MiddleRight;
            this.Label8.Location = new Point(0x10, 0xb0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new Size(0x20, 20);
            this.Label8.TabIndex = 14;
            this.Label8.Text = "# 8";
            this.Label8.TextAlign = ContentAlignment.MiddleRight;
            this.Label10.Location = new Point(0x10, 0xe0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new Size(0x20, 20);
            this.Label10.TabIndex = 0x12;
            this.Label10.Text = "# 10";
            this.Label10.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_02.Location = new Point(0x40, 0x20);
            this.eddICD10_02.Name = "eddICD10_02";
            this.eddICD10_02.Size = new Size(280, 0x15);
            this.eddICD10_02.TabIndex = 3;
            this.eddICD10_02.TextMember = "";
            this.eddICD10_09.Location = new Point(0x40, 200);
            this.eddICD10_09.Name = "eddICD10_09";
            this.eddICD10_09.Size = new Size(280, 0x15);
            this.eddICD10_09.TabIndex = 0x11;
            this.eddICD10_09.TextMember = "";
            this.Label12.Location = new Point(0x10, 0x110);
            this.Label12.Name = "Label12";
            this.Label12.Size = new Size(0x20, 20);
            this.Label12.TabIndex = 0x16;
            this.Label12.Text = "# 12";
            this.Label12.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_04.Location = new Point(0x40, 80);
            this.eddICD10_04.Name = "eddICD10_04";
            this.eddICD10_04.Size = new Size(280, 0x15);
            this.eddICD10_04.TabIndex = 7;
            this.eddICD10_04.TextMember = "";
            this.Label11.Location = new Point(0x10, 0xf8);
            this.Label11.Name = "Label11";
            this.Label11.Size = new Size(0x20, 20);
            this.Label11.TabIndex = 20;
            this.Label11.Text = "# 11";
            this.Label11.TextAlign = ContentAlignment.MiddleRight;
            this.Label1.Location = new Point(0x10, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x20, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "# 1";
            this.Label1.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_03.Location = new Point(0x40, 0x38);
            this.eddICD10_03.Name = "eddICD10_03";
            this.eddICD10_03.Size = new Size(280, 0x15);
            this.eddICD10_03.TabIndex = 5;
            this.eddICD10_03.TextMember = "";
            this.Label2.Location = new Point(0x10, 0x20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x20, 20);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "# 2";
            this.Label2.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_01.Location = new Point(0x40, 8);
            this.eddICD10_01.Name = "eddICD10_01";
            this.eddICD10_01.Size = new Size(280, 0x15);
            this.eddICD10_01.TabIndex = 1;
            this.eddICD10_01.TextMember = "";
            this.Label3.Location = new Point(0x10, 0x38);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x20, 20);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "# 3";
            this.Label3.TextAlign = ContentAlignment.MiddleRight;
            this.Label4.Location = new Point(0x10, 80);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x20, 20);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "# 4";
            this.Label4.TextAlign = ContentAlignment.MiddleRight;
            this.tpInsurance.Controls.Add(this.ControlCustomerInsurance1);
            this.tpInsurance.Location = new Point(4, 0x17);
            this.tpInsurance.Name = "tpInsurance";
            this.tpInsurance.Size = new Size(520, 0x157);
            this.tpInsurance.TabIndex = 8;
            this.tpInsurance.Text = "Insurance";
            this.tpInsurance.Visible = false;
            this.ControlCustomerInsurance1.Dock = DockStyle.Fill;
            this.ControlCustomerInsurance1.Location = new Point(0, 0);
            this.ControlCustomerInsurance1.Name = "ControlCustomerInsurance1";
            this.ControlCustomerInsurance1.Size = new Size(520, 0x157);
            this.ControlCustomerInsurance1.TabIndex = 1;
            this.tpEquipment.Controls.Add(this.dgEquipment);
            this.tpEquipment.Location = new Point(4, 0x17);
            this.tpEquipment.Name = "tpEquipment";
            this.tpEquipment.Size = new Size(520, 0x157);
            this.tpEquipment.TabIndex = 1;
            this.tpEquipment.Text = "Equipment";
            this.tpEquipment.Visible = false;
            this.dgEquipment.Dock = DockStyle.Fill;
            this.dgEquipment.Location = new Point(0, 0);
            this.dgEquipment.Name = "dgEquipment";
            this.dgEquipment.Size = new Size(520, 0x157);
            this.dgEquipment.TabIndex = 0;
            this.tpAssignment.Controls.Add(this.cmbInvoiceForm);
            this.tpAssignment.Controls.Add(this.lblInvoiceForm);
            this.tpAssignment.Controls.Add(this.chbHIPPANote);
            this.tpAssignment.Controls.Add(this.chbSupplierStandards);
            this.tpAssignment.Controls.Add(this.cmbSignatureType);
            this.tpAssignment.Controls.Add(this.lblSignatureType);
            this.tpAssignment.Controls.Add(this.dtbSignatureOnFile);
            this.tpAssignment.Controls.Add(this.cmbTaxRate);
            this.tpAssignment.Controls.Add(this.nmbOutOfPocket);
            this.tpAssignment.Controls.Add(this.nmbDeductible);
            this.tpAssignment.Controls.Add(this.nmbMonthsValid);
            this.tpAssignment.Controls.Add(this.nmbCopayDollar);
            this.tpAssignment.Controls.Add(this.nmbCopayPercent);
            this.tpAssignment.Controls.Add(this.gbCommercialAccounts);
            this.tpAssignment.Controls.Add(this.cmbFrequency);
            this.tpAssignment.Controls.Add(this.cmbBasis);
            this.tpAssignment.Controls.Add(this.chbHardship);
            this.tpAssignment.Controls.Add(this.chbBlock13HCFA);
            this.tpAssignment.Controls.Add(this.chbBlock12HCFA);
            this.tpAssignment.Controls.Add(this.lblOutOfPocket);
            this.tpAssignment.Controls.Add(this.lblTaxRate);
            this.tpAssignment.Controls.Add(this.lblFrequency);
            this.tpAssignment.Controls.Add(this.lblCopayDollar);
            this.tpAssignment.Controls.Add(this.lblDeductible);
            this.tpAssignment.Controls.Add(this.lblBasis);
            this.tpAssignment.Controls.Add(this.lblCopayPercent);
            this.tpAssignment.Controls.Add(this.lblMonthsValid);
            this.tpAssignment.Controls.Add(this.lblSignatureOnFile);
            this.tpAssignment.Location = new Point(4, 0x17);
            this.tpAssignment.Name = "tpAssignment";
            this.tpAssignment.Size = new Size(520, 0x157);
            this.tpAssignment.TabIndex = 3;
            this.tpAssignment.Text = "Billing";
            this.tpAssignment.Visible = false;
            this.cmbInvoiceForm.Location = new Point(0xe8, 0x128);
            this.cmbInvoiceForm.Name = "cmbInvoiceForm";
            this.cmbInvoiceForm.Size = new Size(0xe8, 0x15);
            this.cmbInvoiceForm.TabIndex = 0x1d;
            this.lblInvoiceForm.Location = new Point(0x90, 0x128);
            this.lblInvoiceForm.Name = "lblInvoiceForm";
            this.lblInvoiceForm.RightToLeft = RightToLeft.No;
            this.lblInvoiceForm.Size = new Size(80, 0x16);
            this.lblInvoiceForm.TabIndex = 0x1c;
            this.lblInvoiceForm.Text = "Invoice Form:";
            this.lblInvoiceForm.TextAlign = ContentAlignment.MiddleRight;
            this.chbHIPPANote.CheckAlign = ContentAlignment.MiddleRight;
            this.chbHIPPANote.Location = new Point(0x10, 0x128);
            this.chbHIPPANote.Name = "chbHIPPANote";
            this.chbHIPPANote.Size = new Size(120, 0x18);
            this.chbHIPPANote.TabIndex = 0x19;
            this.chbHIPPANote.Text = "HIPPA Note";
            this.chbHIPPANote.TextAlign = ContentAlignment.MiddleRight;
            this.chbSupplierStandards.CheckAlign = ContentAlignment.MiddleRight;
            this.chbSupplierStandards.Location = new Point(0x10, 0x110);
            this.chbSupplierStandards.Name = "chbSupplierStandards";
            this.chbSupplierStandards.Size = new Size(120, 0x18);
            this.chbSupplierStandards.TabIndex = 0x18;
            this.chbSupplierStandards.Text = "Supplier Standards";
            this.chbSupplierStandards.TextAlign = ContentAlignment.MiddleRight;
            this.cmbSignatureType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSignatureType.Location = new Point(0x70, 0x20);
            this.cmbSignatureType.Name = "cmbSignatureType";
            this.cmbSignatureType.Size = new Size(0x128, 0x15);
            this.cmbSignatureType.TabIndex = 5;
            this.lblSignatureType.Location = new Point(8, 0x20);
            this.lblSignatureType.Name = "lblSignatureType";
            this.lblSignatureType.Size = new Size(0x60, 0x16);
            this.lblSignatureType.TabIndex = 4;
            this.lblSignatureType.Text = "Signature Type";
            this.lblSignatureType.TextAlign = ContentAlignment.MiddleRight;
            this.dtbSignatureOnFile.Location = new Point(0x70, 8);
            this.dtbSignatureOnFile.Name = "dtbSignatureOnFile";
            this.dtbSignatureOnFile.Size = new Size(0x60, 0x15);
            this.dtbSignatureOnFile.TabIndex = 1;
            this.cmbTaxRate.Location = new Point(0x70, 160);
            this.cmbTaxRate.Name = "cmbTaxRate";
            this.cmbTaxRate.Size = new Size(0xd0, 0x15);
            this.cmbTaxRate.TabIndex = 0x15;
            this.nmbOutOfPocket.Location = new Point(0x130, 0x88);
            this.nmbOutOfPocket.Name = "nmbOutOfPocket";
            this.nmbOutOfPocket.Size = new Size(0x68, 20);
            this.nmbOutOfPocket.TabIndex = 0x13;
            this.nmbDeductible.Location = new Point(0x70, 0x88);
            this.nmbDeductible.Name = "nmbDeductible";
            this.nmbDeductible.Size = new Size(80, 20);
            this.nmbDeductible.TabIndex = 0x11;
            this.nmbMonthsValid.Location = new Point(0x130, 8);
            this.nmbMonthsValid.Name = "nmbMonthsValid";
            this.nmbMonthsValid.Size = new Size(0x38, 20);
            this.nmbMonthsValid.TabIndex = 3;
            this.nmbCopayDollar.Location = new Point(0x70, 0x70);
            this.nmbCopayDollar.Name = "nmbCopayDollar";
            this.nmbCopayDollar.Size = new Size(80, 20);
            this.nmbCopayDollar.TabIndex = 13;
            this.nmbCopayPercent.Location = new Point(0x70, 0x58);
            this.nmbCopayPercent.Name = "nmbCopayPercent";
            this.nmbCopayPercent.Size = new Size(80, 20);
            this.nmbCopayPercent.TabIndex = 9;
            this.gbCommercialAccounts.Controls.Add(this.lblTerms);
            this.gbCommercialAccounts.Controls.Add(this.lblCreditLimit);
            this.gbCommercialAccounts.Controls.Add(this.nmbCommercialAcctCreditLimit);
            this.gbCommercialAccounts.Controls.Add(this.txtCommercialAcctTerms);
            this.gbCommercialAccounts.Location = new Point(8, 0xc0);
            this.gbCommercialAccounts.Name = "gbCommercialAccounts";
            this.gbCommercialAccounts.Size = new Size(0x1a0, 0x48);
            this.gbCommercialAccounts.TabIndex = 0x17;
            this.gbCommercialAccounts.TabStop = false;
            this.gbCommercialAccounts.Text = "Commercial Accounts";
            this.lblTerms.Location = new Point(8, 40);
            this.lblTerms.Name = "lblTerms";
            this.lblTerms.Size = new Size(0x40, 0x16);
            this.lblTerms.TabIndex = 2;
            this.lblTerms.Text = "Terms";
            this.lblTerms.TextAlign = ContentAlignment.MiddleRight;
            this.lblCreditLimit.Location = new Point(8, 0x10);
            this.lblCreditLimit.Name = "lblCreditLimit";
            this.lblCreditLimit.Size = new Size(0x40, 0x16);
            this.lblCreditLimit.TabIndex = 0;
            this.lblCreditLimit.Text = "Credit Limit";
            this.lblCreditLimit.TextAlign = ContentAlignment.MiddleRight;
            this.nmbCommercialAcctCreditLimit.Location = new Point(80, 0x10);
            this.nmbCommercialAcctCreditLimit.Name = "nmbCommercialAcctCreditLimit";
            this.nmbCommercialAcctCreditLimit.Size = new Size(80, 20);
            this.nmbCommercialAcctCreditLimit.TabIndex = 1;
            this.txtCommercialAcctTerms.AcceptsReturn = true;
            this.txtCommercialAcctTerms.Location = new Point(80, 40);
            this.txtCommercialAcctTerms.MaxLength = 0;
            this.txtCommercialAcctTerms.Name = "txtCommercialAcctTerms";
            this.txtCommercialAcctTerms.Size = new Size(0x148, 20);
            this.txtCommercialAcctTerms.TabIndex = 3;
            this.cmbFrequency.Location = new Point(0x130, 0x70);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.Size = new Size(0x68, 0x15);
            this.cmbFrequency.TabIndex = 15;
            this.cmbBasis.Location = new Point(0x130, 0x58);
            this.cmbBasis.Name = "cmbBasis";
            this.cmbBasis.Size = new Size(0x68, 0x15);
            this.cmbBasis.TabIndex = 11;
            this.chbHardship.CheckAlign = ContentAlignment.MiddleRight;
            this.chbHardship.Location = new Point(0x150, 160);
            this.chbHardship.Name = "chbHardship";
            this.chbHardship.Size = new Size(0x48, 0x16);
            this.chbHardship.TabIndex = 0x16;
            this.chbHardship.Text = "Hardship";
            this.chbHardship.TextAlign = ContentAlignment.MiddleRight;
            this.chbBlock13HCFA.CheckAlign = ContentAlignment.MiddleRight;
            this.chbBlock13HCFA.Location = new Point(200, 0x38);
            this.chbBlock13HCFA.Name = "chbBlock13HCFA";
            this.chbBlock13HCFA.Size = new Size(120, 0x16);
            this.chbBlock13HCFA.TabIndex = 7;
            this.chbBlock13HCFA.Text = "Block 13 on HCFA";
            this.chbBlock13HCFA.TextAlign = ContentAlignment.MiddleRight;
            this.chbBlock12HCFA.CheckAlign = ContentAlignment.MiddleRight;
            this.chbBlock12HCFA.Location = new Point(8, 0x38);
            this.chbBlock12HCFA.Name = "chbBlock12HCFA";
            this.chbBlock12HCFA.Size = new Size(120, 0x16);
            this.chbBlock12HCFA.TabIndex = 6;
            this.chbBlock12HCFA.Text = "Block 12 on HCFA";
            this.chbBlock12HCFA.TextAlign = ContentAlignment.MiddleRight;
            this.lblOutOfPocket.Location = new Point(0xd8, 0x88);
            this.lblOutOfPocket.Name = "lblOutOfPocket";
            this.lblOutOfPocket.Size = new Size(80, 0x16);
            this.lblOutOfPocket.TabIndex = 0x12;
            this.lblOutOfPocket.Text = "Out of Pocket";
            this.lblOutOfPocket.TextAlign = ContentAlignment.MiddleRight;
            this.lblTaxRate.Location = new Point(8, 160);
            this.lblTaxRate.Name = "lblTaxRate";
            this.lblTaxRate.Size = new Size(0x60, 0x16);
            this.lblTaxRate.TabIndex = 20;
            this.lblTaxRate.Text = "Tax Rate";
            this.lblTaxRate.TextAlign = ContentAlignment.MiddleRight;
            this.lblFrequency.Location = new Point(0xd8, 0x70);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new Size(80, 0x16);
            this.lblFrequency.TabIndex = 14;
            this.lblFrequency.Text = "Frequency";
            this.lblFrequency.TextAlign = ContentAlignment.MiddleRight;
            this.lblCopayDollar.Location = new Point(8, 0x70);
            this.lblCopayDollar.Name = "lblCopayDollar";
            this.lblCopayDollar.Size = new Size(0x60, 0x16);
            this.lblCopayDollar.TabIndex = 12;
            this.lblCopayDollar.Text = "Copay $";
            this.lblCopayDollar.TextAlign = ContentAlignment.MiddleRight;
            this.lblDeductible.Location = new Point(8, 0x88);
            this.lblDeductible.Name = "lblDeductible";
            this.lblDeductible.Size = new Size(0x60, 0x16);
            this.lblDeductible.TabIndex = 0x10;
            this.lblDeductible.Text = "Deductible";
            this.lblDeductible.TextAlign = ContentAlignment.MiddleRight;
            this.lblBasis.Location = new Point(0xd8, 0x58);
            this.lblBasis.Name = "lblBasis";
            this.lblBasis.Size = new Size(80, 0x16);
            this.lblBasis.TabIndex = 10;
            this.lblBasis.Text = "Basis";
            this.lblBasis.TextAlign = ContentAlignment.MiddleRight;
            this.lblCopayPercent.Location = new Point(8, 0x58);
            this.lblCopayPercent.Name = "lblCopayPercent";
            this.lblCopayPercent.Size = new Size(0x60, 0x16);
            this.lblCopayPercent.TabIndex = 8;
            this.lblCopayPercent.Text = "CoPay %";
            this.lblCopayPercent.TextAlign = ContentAlignment.MiddleRight;
            this.lblMonthsValid.Location = new Point(0xd8, 8);
            this.lblMonthsValid.Name = "lblMonthsValid";
            this.lblMonthsValid.Size = new Size(80, 0x16);
            this.lblMonthsValid.TabIndex = 2;
            this.lblMonthsValid.Text = "Months Valid";
            this.lblMonthsValid.TextAlign = ContentAlignment.MiddleRight;
            this.lblSignatureOnFile.Location = new Point(8, 8);
            this.lblSignatureOnFile.Name = "lblSignatureOnFile";
            this.lblSignatureOnFile.Size = new Size(0x60, 0x16);
            this.lblSignatureOnFile.TabIndex = 0;
            this.lblSignatureOnFile.Text = "Signature on File ";
            this.lblSignatureOnFile.TextAlign = ContentAlignment.MiddleRight;
            this.tpPersonal.Controls.Add(this.txtSSNumber);
            this.tpPersonal.Controls.Add(this.txtDeliveryDirections);
            this.tpPersonal.Controls.Add(this.lblDeliveryDirections);
            this.tpPersonal.Controls.Add(this.nmbWeight);
            this.tpPersonal.Controls.Add(this.nmbHeight);
            this.tpPersonal.Controls.Add(this.gbMilitary);
            this.tpPersonal.Controls.Add(this.gbStatus);
            this.tpPersonal.Controls.Add(this.txtLicense);
            this.tpPersonal.Controls.Add(this.cmbGender);
            this.tpPersonal.Controls.Add(this.chbCommercialAccount);
            this.tpPersonal.Controls.Add(this.lblLicense);
            this.tpPersonal.Controls.Add(this.lblSSNumber);
            this.tpPersonal.Controls.Add(this.lblGender);
            this.tpPersonal.Controls.Add(this.lblWeight);
            this.tpPersonal.Controls.Add(this.lblHeight);
            this.tpPersonal.Location = new Point(4, 0x17);
            this.tpPersonal.Name = "tpPersonal";
            this.tpPersonal.Size = new Size(520, 0x157);
            this.tpPersonal.TabIndex = 5;
            this.tpPersonal.Text = "Personal";
            this.tpPersonal.Visible = false;
            this.txtSSNumber.Location = new Point(0x130, 0x20);
            this.txtSSNumber.Name = "txtSSNumber";
            this.txtSSNumber.Size = new Size(0x68, 20);
            this.txtSSNumber.TabIndex = 8;
            this.txtDeliveryDirections.AcceptsReturn = true;
            this.txtDeliveryDirections.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtDeliveryDirections.Location = new Point(8, 0xd8);
            this.txtDeliveryDirections.MaxLength = 0;
            this.txtDeliveryDirections.Multiline = true;
            this.txtDeliveryDirections.Name = "txtDeliveryDirections";
            this.txtDeliveryDirections.ScrollBars = ScrollBars.Vertical;
            this.txtDeliveryDirections.Size = new Size(0x1a8, 120);
            this.txtDeliveryDirections.TabIndex = 14;
            this.lblDeliveryDirections.BackColor = Color.FromArgb(0xc0, 0xff, 0xc0);
            this.lblDeliveryDirections.Location = new Point(8, 0xc0);
            this.lblDeliveryDirections.Name = "lblDeliveryDirections";
            this.lblDeliveryDirections.Size = new Size(0x1a8, 0x15);
            this.lblDeliveryDirections.TabIndex = 13;
            this.lblDeliveryDirections.Text = "Delivery Directions";
            this.lblDeliveryDirections.TextAlign = ContentAlignment.MiddleLeft;
            this.nmbWeight.Location = new Point(0x40, 0x38);
            this.nmbWeight.Name = "nmbWeight";
            this.nmbWeight.Size = new Size(0x68, 20);
            this.nmbWeight.TabIndex = 5;
            this.nmbHeight.Location = new Point(0x40, 0x20);
            this.nmbHeight.Name = "nmbHeight";
            this.nmbHeight.Size = new Size(0x68, 20);
            this.nmbHeight.TabIndex = 3;
            this.gbMilitary.Controls.Add(this.cmbMilitaryBranch);
            this.gbMilitary.Controls.Add(this.lblMilitaryBranch);
            this.gbMilitary.Controls.Add(this.cmbMilitaryStatus);
            this.gbMilitary.Controls.Add(this.lblMilitaryStatus);
            this.gbMilitary.Location = new Point(0xf8, 0x58);
            this.gbMilitary.Name = "gbMilitary";
            this.gbMilitary.Size = new Size(200, 0x60);
            this.gbMilitary.TabIndex = 12;
            this.gbMilitary.TabStop = false;
            this.gbMilitary.Text = "Military";
            this.cmbMilitaryBranch.Location = new Point(0x38, 40);
            this.cmbMilitaryBranch.Name = "cmbMilitaryBranch";
            this.cmbMilitaryBranch.Size = new Size(0x88, 0x15);
            this.cmbMilitaryBranch.TabIndex = 3;
            this.lblMilitaryBranch.Location = new Point(8, 40);
            this.lblMilitaryBranch.Name = "lblMilitaryBranch";
            this.lblMilitaryBranch.Size = new Size(40, 0x15);
            this.lblMilitaryBranch.TabIndex = 2;
            this.lblMilitaryBranch.Text = "Branch";
            this.lblMilitaryBranch.TextAlign = ContentAlignment.MiddleRight;
            this.cmbMilitaryStatus.Location = new Point(0x38, 0x10);
            this.cmbMilitaryStatus.Name = "cmbMilitaryStatus";
            this.cmbMilitaryStatus.Size = new Size(0x88, 0x15);
            this.cmbMilitaryStatus.TabIndex = 1;
            this.lblMilitaryStatus.Location = new Point(8, 0x10);
            this.lblMilitaryStatus.Name = "lblMilitaryStatus";
            this.lblMilitaryStatus.Size = new Size(40, 0x15);
            this.lblMilitaryStatus.TabIndex = 0;
            this.lblMilitaryStatus.Text = "Status";
            this.lblMilitaryStatus.TextAlign = ContentAlignment.MiddleRight;
            this.gbStatus.Controls.Add(this.cmbMaritalStatus);
            this.gbStatus.Controls.Add(this.lblEmploymentStatus);
            this.gbStatus.Controls.Add(this.lblStudentStatus);
            this.gbStatus.Controls.Add(this.cmbEmploymentStatus);
            this.gbStatus.Controls.Add(this.lblMaritalStatus);
            this.gbStatus.Controls.Add(this.cmbStudentStatus);
            this.gbStatus.Location = new Point(8, 0x58);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new Size(0xe8, 0x60);
            this.gbStatus.TabIndex = 11;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "Status";
            this.cmbMaritalStatus.Location = new Point(0x58, 40);
            this.cmbMaritalStatus.Name = "cmbMaritalStatus";
            this.cmbMaritalStatus.Size = new Size(0x88, 0x15);
            this.cmbMaritalStatus.TabIndex = 3;
            this.lblEmploymentStatus.Location = new Point(8, 0x10);
            this.lblEmploymentStatus.Name = "lblEmploymentStatus";
            this.lblEmploymentStatus.Size = new Size(0x48, 0x16);
            this.lblEmploymentStatus.TabIndex = 0;
            this.lblEmploymentStatus.Text = "Employment";
            this.lblEmploymentStatus.TextAlign = ContentAlignment.MiddleRight;
            this.lblStudentStatus.Location = new Point(8, 0x40);
            this.lblStudentStatus.Name = "lblStudentStatus";
            this.lblStudentStatus.Size = new Size(0x48, 0x16);
            this.lblStudentStatus.TabIndex = 4;
            this.lblStudentStatus.Text = "Student";
            this.lblStudentStatus.TextAlign = ContentAlignment.MiddleRight;
            this.cmbEmploymentStatus.Location = new Point(0x58, 0x10);
            this.cmbEmploymentStatus.Name = "cmbEmploymentStatus";
            this.cmbEmploymentStatus.Size = new Size(0x88, 0x15);
            this.cmbEmploymentStatus.TabIndex = 1;
            this.lblMaritalStatus.Location = new Point(8, 40);
            this.lblMaritalStatus.Name = "lblMaritalStatus";
            this.lblMaritalStatus.Size = new Size(0x48, 0x16);
            this.lblMaritalStatus.TabIndex = 2;
            this.lblMaritalStatus.Text = "Marital";
            this.lblMaritalStatus.TextAlign = ContentAlignment.MiddleRight;
            this.cmbStudentStatus.Location = new Point(0x58, 0x40);
            this.cmbStudentStatus.Name = "cmbStudentStatus";
            this.cmbStudentStatus.Size = new Size(0x88, 0x15);
            this.cmbStudentStatus.TabIndex = 5;
            this.txtLicense.AcceptsReturn = true;
            this.txtLicense.Location = new Point(0x130, 0x38);
            this.txtLicense.MaxLength = 0;
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.Size = new Size(0x68, 20);
            this.txtLicense.TabIndex = 10;
            this.cmbGender.Location = new Point(0x40, 8);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new Size(0x68, 0x15);
            this.cmbGender.TabIndex = 1;
            this.chbCommercialAccount.CheckAlign = ContentAlignment.MiddleRight;
            this.chbCommercialAccount.Location = new Point(0xf8, 8);
            this.chbCommercialAccount.Name = "chbCommercialAccount";
            this.chbCommercialAccount.Size = new Size(0x80, 0x16);
            this.chbCommercialAccount.TabIndex = 6;
            this.chbCommercialAccount.Text = "Commercial Account";
            this.chbCommercialAccount.TextAlign = ContentAlignment.MiddleRight;
            this.lblLicense.Location = new Point(0xf8, 0x38);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new Size(0x30, 0x16);
            this.lblLicense.TabIndex = 9;
            this.lblLicense.Text = "License";
            this.lblLicense.TextAlign = ContentAlignment.MiddleRight;
            this.lblSSNumber.Location = new Point(0xf8, 0x20);
            this.lblSSNumber.Name = "lblSSNumber";
            this.lblSSNumber.Size = new Size(0x30, 0x16);
            this.lblSSNumber.TabIndex = 7;
            this.lblSSNumber.Text = "SSN";
            this.lblSSNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblGender.Location = new Point(8, 8);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new Size(0x30, 0x16);
            this.lblGender.TabIndex = 0;
            this.lblGender.Text = "Gender";
            this.lblGender.TextAlign = ContentAlignment.MiddleRight;
            this.lblWeight.Location = new Point(8, 0x38);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new Size(0x30, 0x16);
            this.lblWeight.TabIndex = 4;
            this.lblWeight.Text = "Weight";
            this.lblWeight.TextAlign = ContentAlignment.MiddleRight;
            this.lblHeight.Location = new Point(8, 0x20);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new Size(0x30, 0x16);
            this.lblHeight.TabIndex = 2;
            this.lblHeight.Text = "Height";
            this.lblHeight.TextAlign = ContentAlignment.MiddleRight;
            this.ImageList1.ImageStream = (ImageListStreamer) manager.GetObject("ImageList1.ImageStream");
            this.ImageList1.TransparentColor = Color.Magenta;
            this.ImageList1.Images.SetKeyName(0, "");
            this.Panel1.Controls.Add(this.CName);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x210, 0x38);
            this.Panel1.TabIndex = 0;
            this.CName.Location = new Point(0x10, 0);
            this.CName.Name = "CName";
            this.CName.Size = new Size(440, 0x30);
            this.CName.TabIndex = 0;
            this.mnuGotoImages.Index = 0;
            this.mnuGotoImages.Text = "Images";
            this.mnuGotoNewImage.Index = 1;
            this.mnuGotoNewImage.Text = "New Image";
            this.mnuGotoEligibility.Index = 2;
            this.mnuGotoEligibility.Text = "Eligibility";
            this.mnuGotoPaymentPlan.Index = 3;
            this.mnuGotoPaymentPlan.Text = "Payment Plan";
            this.mnuActionsScheduleMeeting.Index = 0;
            this.mnuActionsScheduleMeeting.Text = "Schedule Meeting";
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiCustomerInsurancesGridRequest };
            this.cmsCustomerInsurancesGrid.Items.AddRange(toolStripItems);
            this.cmsCustomerInsurancesGrid.Name = "cmsCustomerInsurancesGrid";
            this.cmsCustomerInsurancesGrid.Size = new Size(0x75, 0x1a);
            this.tsmiCustomerInsurancesGridRequest.Name = "tsmiCustomerInsurancesGridRequest";
            this.tsmiCustomerInsurancesGridRequest.Size = new Size(0x74, 0x16);
            this.tsmiCustomerInsurancesGridRequest.Text = "Request";
            ToolStripItem[] itemArray4 = new ToolStripItem[] { this.tsmiGridSearchMakeInactive };
            this.cmsGridSearch.Items.AddRange(itemArray4);
            this.cmsGridSearch.Name = "cmsGridSearch";
            this.cmsGridSearch.Size = new Size(160, 0x1a);
            this.tsmiGridSearchMakeInactive.Name = "tsmiGridSearchMakeInactive";
            this.tsmiGridSearchMakeInactive.Size = new Size(0x9f, 0x16);
            this.tsmiGridSearchMakeInactive.Text = "Make Inactive ...";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x218, 0x1ed);
            base.Name = "FormCustomer";
            this.Text = "Maintain Customer";
            base.tpWorkArea.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.gbBalance.ResumeLayout(false);
            this.tpNotes.ResumeLayout(false);
            this.tpOtherAddresses.ResumeLayout(false);
            this.gbShipTo.ResumeLayout(false);
            this.gbShipTo.PerformLayout();
            this.gbBillTo.ResumeLayout(false);
            this.gbBillTo.PerformLayout();
            this.tpContacts.ResumeLayout(false);
            this.tpContacts.PerformLayout();
            this.tpDiagnosis.ResumeLayout(false);
            this.TabControl2.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.tpInsurance.ResumeLayout(false);
            this.tpEquipment.ResumeLayout(false);
            this.tpAssignment.ResumeLayout(false);
            this.gbCommercialAccounts.ResumeLayout(false);
            this.gbCommercialAccounts.PerformLayout();
            this.tpPersonal.ResumeLayout(false);
            this.tpPersonal.PerformLayout();
            this.gbMilitary.ResumeLayout(false);
            this.gbStatus.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.cmsCustomerInsurancesGrid.ResumeLayout(false);
            this.cmsGridSearch.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("ID", "ID", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("InventoryItemID", "Inv ID", 50, Appearance.IntegerStyle());
            Appearance.AddTextColumn("InventoryItemName", "Item Name", 140);
            Appearance.AddTextColumn("SaleRentType", "Sale/Rent Type", 90);
            Appearance.AddTextColumn("DOSFrom", "DOS FROM", 70, Appearance.DateStyle());
            Appearance.AddTextColumn("DOSTo", "DOS TO", 70, Appearance.DateStyle());
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu();
            Cache.AddCategory(menu, "Customer", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        private static void Insurances_CellFormatting(object sender, GridCellFormattingEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    DataColumn column = dataRow.Table.Columns["RequestEligibilityOn"];
                    if ((column != null) && (NullableConvert.ToDateTime(dataRow[column]) != null))
                    {
                        e.CellStyle.ForeColor = Color.Blue;
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private void Insurances_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Insurances_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(GetInsurancesQuery(), ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        protected void Insurances_InitializeAppearance(GridAppearanceBase Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.MultiSelect = true;
            Appearance.AddTextColumn("AccountNumber", "Account #", 80);
            Appearance.AddTextColumn("CustomerName", "Customer", 100);
            Appearance.AddTextColumn("SSNumber", "SSN", 80);
            Appearance.AddTextColumn("Rank", "Rank", 40);
            Appearance.AddTextColumn("InsuranceCompanyName", "Ins. Co.", 160);
            Appearance.AddTextColumn("Relationship", "Relation", 60);
            Appearance.AddTextColumn("InsuredName", "Insured", 100);
            Appearance.AddTextColumn("PolicyNumber", "Policy #", 80);
            Appearance.AddTextColumn("GroupNumber", "Group #", 80);
            Appearance.AddTextColumn("RequestEligibilityOn", "Request On", 80, Appearance.DateStyle());
            Appearance.ContextMenuStrip = this.cmsCustomerInsurancesGrid;
            Appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(FormCustomer.Insurances_CellFormatting);
        }

        private void Insurances_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            try
            {
                DataRow dataRow = args.GridRow.GetDataRow();
                base.OpenObject(dataRow["CustomerID"]);
                this.ControlCustomerInsurance1.ShowDetails(dataRow["CustomerInsuranceID"]);
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

        private void Load_Customer_Equipment(int CustomerID)
        {
            DataTable dataTable = CreateEquipmentTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT view_orderdetails.ID,
       view_orderdetails.InventoryItemID,
       tbl_inventoryitem.Name as InventoryItemName,
       view_orderdetails.SaleRentType,
       view_orderdetails.DOSFrom,
       view_orderdetails.DOSTo
FROM view_orderdetails
     INNER JOIN tbl_order ON view_orderdetails.OrderID    = tbl_order.ID
                         AND view_orderdetails.CustomerID = tbl_order.CustomerID
     LEFT JOIN tbl_inventoryitem ON view_orderdetails.InventoryItemID = tbl_inventoryitem.ID
WHERE (tbl_order.Approved = 1)
  AND (view_orderdetails.IsRented = 1)
  AND (view_orderdetails.IsActive = 1)
  AND (tbl_order.CustomerID = {CustomerID})", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                adapter.Fill(dataTable);
            }
            this.dgEquipment.GridSource = dataTable.ToGridSource();
        }

        private void LoadBalance(int? CustomerID)
        {
            if (CustomerID != null)
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.CommandText = "SELECT\r\n  Sum(IF(1 = 1                     , d.BillableAmount - d.PaymentAmount - d.WriteoffAmount, 0)) as TotalBalance\r\n, Sum(IF(d.CurrentPayer = 'Patient', d.BillableAmount - d.PaymentAmount - d.WriteoffAmount, 0)) as CustomerBalance\r\nFROM tbl_invoicedetails as d\r\n     INNER JOIN tbl_invoice as i ON i.CustomerID = d.CustomerID\r\n                                AND i.ID         = d.InvoiceID\r\nWHERE i.CustomerID = :CustomerID";
                        command.Parameters.Add("CustomerID", MySqlType.Int).Value = CustomerID.Value;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Functions.SetNumericBoxValue(this.nmbCustomerBalance, reader["CustomerBalance"]);
                                Functions.SetNumericBoxValue(this.nmbTotalBalance, reader["TotalBalance"]);
                                return;
                            }
                        }
                    }
                }
            }
            Functions.SetNumericBoxValue(this.nmbCustomerBalance, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbTotalBalance, DBNull.Value);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                bool flag;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_customer WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.F_MIR = Convert.ToString(reader["MIR"], CultureInfo.InvariantCulture);
                            Functions.SetComboBoxText(this.CName.cmbCourtesy, reader["Courtesy"]);
                            Functions.SetTextBoxText(this.CName.txtFirstName, reader["FirstName"]);
                            Functions.SetTextBoxText(this.CName.txtMiddleName, reader["MiddleName"]);
                            Functions.SetTextBoxText(this.CName.txtLastName, reader["LastName"]);
                            Functions.SetTextBoxText(this.CName.txtSuffix, reader["Suffix"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtPhone2, reader["Phone2"]);
                            Functions.SetTextBoxText(this.txtEmail, reader["Email"]);
                            Functions.SetDateBoxValue(this.dtbDateofBirth, reader["DateofBirth"]);
                            Functions.SetDateBoxValue(this.dtbDeceasedDate, reader["DeceasedDate"]);
                            Functions.SetComboBoxValue(this.cmbCustomerClass, reader["CustomerClassCode"]);
                            Functions.SetTextBoxText(this.txtAccountNumber, reader["AccountNumber"]);
                            Functions.SetComboBoxValue(this.cmbBillingType, reader["BillingTypeID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerType, reader["CustomerTypeID"]);
                            Functions.SetComboBoxValue(this.cmbLocation, reader["LocationID"]);
                            Functions.SetNumericBoxValue(this.nmbTotalBalance, reader["TotalBalance"]);
                            Functions.SetNumericBoxValue(this.nmbCustomerBalance, reader["CustomerBalance"]);
                            Functions.SetCheckBoxChecked(this.chbCollections, reader["Collections"]);
                            Functions.SetCheckBoxChecked(this.chbBillActive, reader["BillActive"]);
                            this.SetBillActiveState();
                            Functions.SetTextBoxText(this.CBillToAddress.txtAddress1, reader["BillAddress1"]);
                            Functions.SetTextBoxText(this.CBillToAddress.txtAddress2, reader["BillAddress2"]);
                            Functions.SetTextBoxText(this.CBillToAddress.txtCity, reader["BillCity"]);
                            Functions.SetTextBoxText(this.txtBillName, reader["BillName"]);
                            Functions.SetTextBoxText(this.CBillToAddress.txtState, reader["BillState"]);
                            Functions.SetTextBoxText(this.CBillToAddress.txtZip, reader["BillZip"]);
                            Functions.SetCheckBoxChecked(this.chbCommercialAccount, reader["CommercialAccount"]);
                            Functions.SetTextBoxText(this.txtDeliveryDirections, reader["DeliveryDirections"]);
                            Functions.SetComboBoxText(this.cmbEmploymentStatus, reader["EmploymentStatus"]);
                            Functions.SetComboBoxText(this.cmbGender, reader["Gender"]);
                            Functions.SetNumericBoxValue(this.nmbHeight, reader["Height"]);
                            Functions.SetTextBoxText(this.txtLicense, reader["License"]);
                            Functions.SetComboBoxText(this.cmbMaritalStatus, reader["MaritalStatus"]);
                            Functions.SetComboBoxText(this.cmbMilitaryBranch, reader["MilitaryBranch"]);
                            Functions.SetComboBoxText(this.cmbMilitaryStatus, reader["MilitaryStatus"]);
                            Functions.SetCheckBoxChecked(this.chbShipActive, reader["ShipActive"]);
                            this.SetShipActiveState();
                            Functions.SetTextBoxText(this.CShipToAddress.txtAddress1, reader["ShipAddress1"]);
                            Functions.SetTextBoxText(this.CShipToAddress.txtAddress2, reader["ShipAddress2"]);
                            Functions.SetTextBoxText(this.CShipToAddress.txtCity, reader["ShipCity"]);
                            Functions.SetTextBoxText(this.txtShipName, reader["ShipName"]);
                            Functions.SetTextBoxText(this.CShipToAddress.txtState, reader["ShipState"]);
                            Functions.SetTextBoxText(this.CShipToAddress.txtZip, reader["ShipZip"]);
                            Functions.SetTextBoxText(this.txtSSNumber, reader["SSNumber"]);
                            Functions.SetComboBoxText(this.cmbStudentStatus, reader["StudentStatus"]);
                            Functions.SetNumericBoxValue(this.nmbWeight, reader["Weight"]);
                            Functions.SetDateBoxValue(this.dtbSignatureOnFile, reader["SignatureOnFile"]);
                            Functions.SetComboBoxValue(this.cmbSignatureType, reader["SignatureType"]);
                            Functions.SetNumericBoxValue(this.nmbMonthsValid, reader["MonthsValid"]);
                            Functions.SetCheckBoxChecked(this.chbBlock12HCFA, reader["Block12HCFA"]);
                            Functions.SetCheckBoxChecked(this.chbBlock13HCFA, reader["Block13HCFA"]);
                            Functions.SetNumericBoxValue(this.nmbCopayPercent, reader["CopayPercent"]);
                            Functions.SetNumericBoxValue(this.nmbCopayDollar, reader["CopayDollar"]);
                            Functions.SetNumericBoxValue(this.nmbDeductible, reader["Deductible"]);
                            Functions.SetComboBoxValue(this.cmbTaxRate, reader["TaxRateID"]);
                            Functions.SetComboBoxValue(this.cmbInvoiceForm, reader["InvoiceFormID"]);
                            Functions.SetComboBoxText(this.cmbBasis, reader["Basis"]);
                            Functions.SetComboBoxText(this.cmbFrequency, reader["Frequency"]);
                            Functions.SetNumericBoxValue(this.nmbOutOfPocket, reader["OutOfPocket"]);
                            Functions.SetCheckBoxChecked(this.chbHardship, reader["Hardship"]);
                            Functions.SetNumericBoxValue(this.nmbCommercialAcctCreditLimit, reader["CommercialAcctCreditLimit"]);
                            Functions.SetTextBoxText(this.txtCommercialAcctTerms, reader["CommercialAcctTerms"]);
                            Functions.SetComboBoxValue(this.cmbDoctor1, reader["Doctor1_ID"]);
                            Functions.SetComboBoxValue(this.cmbDoctor2, reader["Doctor2_ID"]);
                            Functions.SetComboBoxValue(this.cmbLegalRep, reader["LegalRepID"]);
                            Functions.SetComboBoxValue(this.cmbReferral, reader["ReferralID"]);
                            Functions.SetComboBoxValue(this.cmbSalesRep, reader["SalesRepID"]);
                            Functions.SetComboBoxValue(this.cmbFacility, reader["FacilityID"]);
                            Functions.SetTextBoxText(this.txtEmergencyContact, reader["EmergencyContact"]);
                            Functions.SetDateBoxValue(this.dtbDateOfInjury, reader["DateOfInjury"]);
                            Functions.SetDateBoxValue(this.dtbReturnToWorkDate, reader["ReturnToWorkDate"]);
                            Functions.SetDateBoxValue(this.dtbFirstConsultDate, reader["FirstConsultDate"]);
                            Functions.SetComboBoxText(this.cmbAccidentType, reader["AccidentType"]);
                            Functions.SetCheckBoxChecked(this.chbEmergency, reader["Emergency"]);
                            Functions.SetCheckBoxChecked(this.chbEmploymentRelated, reader["EmploymentRelated"]);
                            Functions.SetTextBoxText(this.txtStateOfAccident, reader["StateOfAccident"]);
                            this.eddICD9_1.Text = NullableConvert.ToString(reader["ICD9_1"]);
                            this.eddICD9_2.Text = NullableConvert.ToString(reader["ICD9_2"]);
                            this.eddICD9_3.Text = NullableConvert.ToString(reader["ICD9_3"]);
                            this.eddICD9_4.Text = NullableConvert.ToString(reader["ICD9_4"]);
                            this.eddICD10_01.Text = NullableConvert.ToString(reader["ICD10_01"]);
                            this.eddICD10_02.Text = NullableConvert.ToString(reader["ICD10_02"]);
                            this.eddICD10_03.Text = NullableConvert.ToString(reader["ICD10_03"]);
                            this.eddICD10_04.Text = NullableConvert.ToString(reader["ICD10_04"]);
                            this.eddICD10_05.Text = NullableConvert.ToString(reader["ICD10_05"]);
                            this.eddICD10_06.Text = NullableConvert.ToString(reader["ICD10_06"]);
                            this.eddICD10_07.Text = NullableConvert.ToString(reader["ICD10_07"]);
                            this.eddICD10_08.Text = NullableConvert.ToString(reader["ICD10_08"]);
                            this.eddICD10_09.Text = NullableConvert.ToString(reader["ICD10_09"]);
                            this.eddICD10_10.Text = NullableConvert.ToString(reader["ICD10_10"]);
                            this.eddICD10_11.Text = NullableConvert.ToString(reader["ICD10_11"]);
                            this.eddICD10_12.Text = NullableConvert.ToString(reader["ICD10_12"]);
                            Functions.SetComboBoxValue(this.cmbPOSType, reader["POSTypeID"]);
                            Functions.SetCheckBoxChecked(this.chbHIPPANote, reader["HIPPANote"]);
                            Functions.SetCheckBoxChecked(this.chbSupplierStandards, reader["SupplierStandards"]);
                            Functions.SetDateBoxValue(this.dtbInactiveDate, reader["InactiveDate"]);
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
                this.ControlCustomerInsurance1.LoadGrid(connection, ID);
                this.ControlCustomerNotes1.LoadGrid(connection, ID);
                this.Load_Customer_Equipment(ID);
                this.LoadBalance(new int?(ID));
                return true;
            }
        }

        private void mnuActionsScheduleMeeting_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                builder.Append("Meeting with \"").Append(this.CName.txtLastName.Text).Append(", ").Append(this.CName.txtFirstName.Text).Append("\"");
                builder2.Append("Customer : ").Append(this.CName.txtLastName.Text).Append(", ").Append(this.CName.txtFirstName.Text).Append(" ").Append(this.CName.txtMiddleName.Text).AppendLine().Append("Address : ").Append(this.CAddress.txtAddress1.Text).AppendLine().Append("City State Zip : ").Append(this.CAddress.txtCity.Text).Append(" ").Append(this.CAddress.txtState.Text).Append(" ").Append(this.CAddress.txtZip.Text).AppendLine().Append("Phone : ").Append(this.txtPhone.Text).AppendLine().Append("Phone : ").Append(this.txtPhone2.Text).AppendLine();
                using (DialogCreateCalendarEvent event2 = new DialogCreateCalendarEvent(builder.ToString(), builder2.ToString()))
                {
                    event2.ShowDialog();
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

        private void mnuGotoEligibility_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["CustomerID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormEligibility(), @params);
        }

        private void mnuGotoImages_Click(object sender, EventArgs e)
        {
            ClassGlobalObjects.ShowForm(FormFactories.FormImageSearch(), new FormParameters("CustomerID", this.ObjectID));
        }

        private void mnuGotoNewImage_Click(object sender, EventArgs e)
        {
            ClassGlobalObjects.ShowForm(FormFactories.FormImage(), new FormParameters("CustomerID", this.ObjectID));
        }

        private void mnuGotoPaymentPlan_Click(object sender, EventArgs e)
        {
            int? nullable = NullableConvert.ToInt32(this.ObjectID);
            if (nullable != null)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPaymentPlan(nullable.Value));
            }
        }

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_customer.ID}"] = this.ObjectID
                };
                try
                {
                    ClassGlobalObjects.ShowReport(item.ReportFileName, @params);
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

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_customer" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected void ProcessParameter_Other(FormParameters Params)
        {
            if ((Params != null) && Params.ContainsKey("ID"))
            {
                base.OpenObject(Params["ID"]);
                if (Params.ContainsKey("CustomerInsuranceID"))
                {
                    this.ControlCustomerInsurance1.ShowDetails(Params["CustomerInsuranceID"]);
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
                    command.Parameters.Add("Courtesy", MySqlType.Char, 4).Value = this.CName.cmbCourtesy.Text;
                    command.Parameters.Add("FirstName", MySqlType.VarChar, 0x19).Value = this.CName.txtFirstName.Text;
                    command.Parameters.Add("LastName", MySqlType.VarChar, 30).Value = this.CName.txtLastName.Text;
                    command.Parameters.Add("MiddleName", MySqlType.Char, 1).Value = this.CName.txtMiddleName.Text;
                    command.Parameters.Add("Suffix", MySqlType.VarChar, 4).Value = this.CName.txtSuffix.Text;
                    command.Parameters.Add("Address1", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress1.Text;
                    command.Parameters.Add("Address2", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress2.Text;
                    command.Parameters.Add("City", MySqlType.VarChar, 0x19).Value = this.CAddress.txtCity.Text;
                    command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                    command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                    command.Parameters.Add("Phone", MySqlType.VarChar, 50).Value = this.txtPhone.Text;
                    command.Parameters.Add("Phone2", MySqlType.VarChar, 50).Value = this.txtPhone2.Text;
                    command.Parameters.Add("Email", MySqlType.VarChar, 150).Value = this.txtEmail.Text;
                    if (this.txtAccountNumber.Text.Trim() == "")
                    {
                        this.txtAccountNumber.Text = this.GetNewAccountNumber().ToString();
                    }
                    command.Parameters.Add("CustomerClassCode", MySqlType.VarChar, 2).Value = this.cmbCustomerClass.SelectedValue;
                    command.Parameters.Add("AccountNumber", MySqlType.VarChar, 40).Value = this.txtAccountNumber.Text;
                    command.Parameters.Add("BillingTypeID", MySqlType.Int).Value = this.cmbBillingType.SelectedValue;
                    command.Parameters.Add("CustomerBalance", MySqlType.Double).Value = this.nmbCustomerBalance.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("CustomerTypeID", MySqlType.Int).Value = this.cmbCustomerType.SelectedValue;
                    command.Parameters.Add("DeceasedDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbDeceasedDate);
                    command.Parameters.Add("DateofBirth", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbDateofBirth);
                    command.Parameters.Add("LocationID", MySqlType.Int).Value = this.cmbLocation.SelectedValue;
                    command.Parameters.Add("TotalBalance", MySqlType.Double).Value = this.nmbTotalBalance.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Collections", MySqlType.Bit).Value = this.chbCollections.Checked;
                    command.Parameters.Add("BillActive", MySqlType.Bit).Value = this.chbBillActive.Checked;
                    command.Parameters.Add("BillAddress1", MySqlType.VarChar, 40).Value = this.CBillToAddress.txtAddress1.Text;
                    command.Parameters.Add("BillAddress2", MySqlType.VarChar, 40).Value = this.CBillToAddress.txtAddress2.Text;
                    command.Parameters.Add("BillCity", MySqlType.VarChar, 0x19).Value = this.CBillToAddress.txtCity.Text;
                    command.Parameters.Add("BillName", MySqlType.VarChar, 50).Value = this.txtBillName.Text;
                    command.Parameters.Add("BillState", MySqlType.Char, 2).Value = this.CBillToAddress.txtState.Text;
                    command.Parameters.Add("BillZip", MySqlType.VarChar, 10).Value = this.CBillToAddress.txtZip.Text;
                    command.Parameters.Add("CommercialAccount", MySqlType.Bit).Value = this.chbCommercialAccount.Checked;
                    command.Parameters.Add("DeliveryDirections", MySqlType.Text, 0x7fffffff).Value = this.txtDeliveryDirections.Text;
                    command.Parameters.Add("EmploymentStatus", MySqlType.Char, 10).Value = this.cmbEmploymentStatus.Text;
                    command.Parameters.Add("Gender", MySqlType.Char, 6).Value = this.cmbGender.Text;
                    command.Parameters.Add("Height", MySqlType.Double).Value = NullableConvert.ToDb(this.nmbHeight.AsDouble);
                    command.Parameters.Add("License", MySqlType.VarChar, 50).Value = this.txtLicense.Text;
                    command.Parameters.Add("MaritalStatus", MySqlType.Char, 0x10).Value = this.cmbMaritalStatus.Text;
                    command.Parameters.Add("MilitaryBranch", MySqlType.Char, 14).Value = this.cmbMilitaryBranch.Text;
                    command.Parameters.Add("MilitaryStatus", MySqlType.Char, 7).Value = this.cmbMilitaryStatus.Text;
                    command.Parameters.Add("ShipActive", MySqlType.Bit).Value = this.chbShipActive.Checked;
                    command.Parameters.Add("ShipAddress1", MySqlType.VarChar, 40).Value = this.CShipToAddress.txtAddress1.Text;
                    command.Parameters.Add("ShipAddress2", MySqlType.VarChar, 40).Value = this.CShipToAddress.txtAddress2.Text;
                    command.Parameters.Add("ShipCity", MySqlType.VarChar, 0x19).Value = this.CShipToAddress.txtCity.Text;
                    command.Parameters.Add("ShipName", MySqlType.VarChar, 50).Value = this.txtShipName.Text;
                    command.Parameters.Add("ShipState", MySqlType.Char, 2).Value = this.CShipToAddress.txtState.Text;
                    command.Parameters.Add("ShipZip", MySqlType.VarChar, 10).Value = this.CShipToAddress.txtZip.Text;
                    command.Parameters.Add("SSNumber", MySqlType.VarChar, 50).Value = this.txtSSNumber.Text;
                    command.Parameters.Add("StudentStatus", MySqlType.Char, 9).Value = this.cmbStudentStatus.Text;
                    command.Parameters.Add("Weight", MySqlType.Double).Value = NullableConvert.ToDb(this.nmbWeight.AsDouble);
                    command.Parameters.Add("Basis", MySqlType.Char, 7).Value = this.cmbBasis.Text;
                    command.Parameters.Add("Block12HCFA", MySqlType.Bit).Value = this.chbBlock12HCFA.Checked;
                    command.Parameters.Add("Block13HCFA", MySqlType.Bit).Value = this.chbBlock13HCFA.Checked;
                    command.Parameters.Add("CommercialAcctCreditLimit", MySqlType.Double).Value = this.nmbCommercialAcctCreditLimit.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("CommercialAcctTerms", MySqlType.VarChar, 50).Value = this.txtCommercialAcctTerms.Text;
                    command.Parameters.Add("CopayDollar", MySqlType.Double, 8).Value = this.nmbCopayDollar.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("CopayPercent", MySqlType.Double, 8).Value = this.nmbCopayPercent.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Deductible", MySqlType.Double, 8).Value = this.nmbDeductible.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Frequency", MySqlType.Char, 9).Value = this.cmbFrequency.Text;
                    command.Parameters.Add("Hardship", MySqlType.Bit).Value = this.chbHardship.Checked;
                    command.Parameters.Add("MonthsValid", MySqlType.Int).Value = this.nmbMonthsValid.AsInt32.GetValueOrDefault(0);
                    command.Parameters.Add("OutOfPocket", MySqlType.Double).Value = this.nmbOutOfPocket.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("SignatureOnFile", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbSignatureOnFile);
                    command.Parameters.Add("SignatureType", MySqlType.Char, 1).Value = this.cmbSignatureType.SelectedValue;
                    command.Parameters.Add("TaxRateID", MySqlType.Int).Value = this.cmbTaxRate.SelectedValue;
                    command.Parameters.Add("InvoiceFormID", MySqlType.Int).Value = this.cmbInvoiceForm.SelectedValue;
                    command.Parameters.Add("Doctor1_ID", MySqlType.Int, 4).Value = this.cmbDoctor1.SelectedValue;
                    command.Parameters.Add("Doctor2_ID", MySqlType.Int, 4).Value = this.cmbDoctor2.SelectedValue;
                    command.Parameters.Add("EmergencyContact", MySqlType.Text, 0x7fffffff).Value = this.txtEmergencyContact.Text;
                    command.Parameters.Add("LegalRepID", MySqlType.Int, 4).Value = this.cmbLegalRep.SelectedValue;
                    command.Parameters.Add("ReferralID", MySqlType.Int, 4).Value = this.cmbReferral.SelectedValue;
                    command.Parameters.Add("SalesRepID", MySqlType.Int, 4).Value = this.cmbSalesRep.SelectedValue;
                    command.Parameters.Add("FacilityID", MySqlType.Int, 4).Value = this.cmbFacility.SelectedValue;
                    command.Parameters.Add("AccidentType", MySqlType.VarChar, 10).Value = this.cmbAccidentType.Text;
                    command.Parameters.Add("DateOfInjury", MySqlType.Date, 8).Value = Functions.GetDateBoxValue(this.dtbDateOfInjury);
                    command.Parameters.Add("Emergency", MySqlType.Bit, 1).Value = this.chbEmergency.Checked;
                    command.Parameters.Add("EmploymentRelated", MySqlType.Bit, 1).Value = this.chbEmploymentRelated.Checked;
                    command.Parameters.Add("FirstConsultDate", MySqlType.Date, 8).Value = Functions.GetDateBoxValue(this.dtbFirstConsultDate);
                    command.Parameters.Add("ICD9_1", MySqlType.VarChar, 6).Value = this.eddICD9_1.Text;
                    command.Parameters.Add("ICD9_2", MySqlType.VarChar, 6).Value = this.eddICD9_2.Text;
                    command.Parameters.Add("ICD9_3", MySqlType.VarChar, 6).Value = this.eddICD9_3.Text;
                    command.Parameters.Add("ICD9_4", MySqlType.VarChar, 6).Value = this.eddICD9_4.Text;
                    command.Parameters.Add("ICD10_01", MySqlType.VarChar, 10).Value = this.eddICD10_01.Text;
                    command.Parameters.Add("ICD10_02", MySqlType.VarChar, 10).Value = this.eddICD10_02.Text;
                    command.Parameters.Add("ICD10_03", MySqlType.VarChar, 10).Value = this.eddICD10_03.Text;
                    command.Parameters.Add("ICD10_04", MySqlType.VarChar, 10).Value = this.eddICD10_04.Text;
                    command.Parameters.Add("ICD10_05", MySqlType.VarChar, 10).Value = this.eddICD10_05.Text;
                    command.Parameters.Add("ICD10_06", MySqlType.VarChar, 10).Value = this.eddICD10_06.Text;
                    command.Parameters.Add("ICD10_07", MySqlType.VarChar, 10).Value = this.eddICD10_07.Text;
                    command.Parameters.Add("ICD10_08", MySqlType.VarChar, 10).Value = this.eddICD10_08.Text;
                    command.Parameters.Add("ICD10_09", MySqlType.VarChar, 10).Value = this.eddICD10_09.Text;
                    command.Parameters.Add("ICD10_10", MySqlType.VarChar, 10).Value = this.eddICD10_10.Text;
                    command.Parameters.Add("ICD10_11", MySqlType.VarChar, 10).Value = this.eddICD10_11.Text;
                    command.Parameters.Add("ICD10_12", MySqlType.VarChar, 10).Value = this.eddICD10_12.Text;
                    command.Parameters.Add("ReturnToWorkDate", MySqlType.Date, 8).Value = Functions.GetDateBoxValue(this.dtbReturnToWorkDate);
                    command.Parameters.Add("StateOfAccident", MySqlType.Char, 2).Value = this.txtStateOfAccident.Text;
                    command.Parameters.Add("POSTypeID", MySqlType.Int, 4).Value = this.cmbPOSType.SelectedValue;
                    command.Parameters.Add("HIPPANote", MySqlType.Int, 4).Value = this.chbHIPPANote.Checked;
                    command.Parameters.Add("SupplierStandards", MySqlType.Int, 4).Value = this.chbSupplierStandards.Checked;
                    command.Parameters.Add("InactiveDate", MySqlType.Date, 8).Value = Functions.GetDateBoxValue(this.dtbInactiveDate);
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (IsNew)
                    {
                        command.Parameters.Add("SetupDate", MySqlType.Date).Value = DateTime.Today;
                        flag = 0 < command.ExecuteInsert("tbl_customer");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                    else
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        if (0 >= command.ExecuteUpdate("tbl_customer", whereParameters))
                        {
                            command.Parameters.Add("SetupDate", MySqlType.Date).Value = DateTime.Today;
                            flag = 0 < command.ExecuteInsert("tbl_customer");
                        }
                    }
                }
                this.ControlCustomerInsurance1.SaveGrid(connection, ID);
                this.ControlCustomerNotes1.SaveGrid(connection, ID);
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.Parameters.Add("P_CustomerID", MySqlType.Int).Value = ID;
                    command2.ExecuteProcedure("mir_update_customer");
                }
                using (MySqlCommand command3 = new MySqlCommand("", connection))
                {
                    command3.CommandText = "SELECT MIR FROM tbl_customer WHERE ID = :ID";
                    command3.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    this.F_MIR = Convert.ToString(command3.ExecuteScalar());
                }
                this.ControlCustomerInsurance1.LoadGrid(connection, ID);
                this.ControlCustomerNotes1.LoadGrid(connection, ID);
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    string str = Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TEMPORARY TABLE `{str}` (
  CustomerID int(11) not null default '0',
  Count int(11) not null default '0',
  PRIMARY KEY (CustomerID))";
                    command.ExecuteNonQuery();
                    command.CommandText = $"INSERT INTO `{str}` (CustomerID, Count)
SELECT tbl_customer_notes.CustomerID, SUM(IF(tbl_customer_notes.Active <> 0, 1, 0))
FROM tbl_customer_notes
WHERE ({IsDemoVersion ? "tbl_customer_notes.CustomerID BETWEEN 1 and 50" : "1 = 1"})
GROUP BY tbl_customer_notes.CustomerID";
                    command.ExecuteNonQuery();
                    command.CommandText = $"SELECT tbl_customer.ID,
       tbl_customertype.Name as CustomerType,
       tbl_customer.FirstName,
       tbl_customer.LastName,
       tbl_customer.DateofBirth,
       tbl_customer.Phone,
       tbl_customer.Address1,
       tbl_customer.City,
       tbl_customer.State,
       tbl_customer.Zip,
       tbl_customer.SSNumber,
       tbl_customer.AccountNumber,
       tbl_customer.Collections,
       IFNULL(tmp.Count, 0) as ActiveCount,
       IF(tbl_customer.InactiveDate <= Now(), 1, 0) as IsInactive
FROM tbl_customer
     LEFT JOIN `{str}` as tmp ON tmp.CustomerID = tbl_customer.ID
     LEFT JOIN tbl_customertype ON tbl_customertype.ID = tbl_customer.CustomerTypeID
     LEFT JOIN tbl_company ON tbl_company.ID = 1
WHERE ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))
  AND ({IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1"})";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill((args.Source as DataTableGridSource).Table);
                    }
                }
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.ContextMenuStrip = this.cmsGridSearch;
            appearance.MultiSelect = true;
            appearance.RowHeadersWidth = 0x10;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 40);
            appearance.AddTextColumn("CustomerType", "Type", 60);
            appearance.AddTextColumn("FirstName", "First Name", 80);
            appearance.AddTextColumn("LastName", "Last Name", 80);
            appearance.AddTextColumn("DateofBirth", "Birthday", 80, appearance.DateStyle());
            appearance.AddTextColumn("Phone", "Phone", 80);
            appearance.AddTextColumn("SSNumber", "SSN", 80);
            appearance.AddTextColumn("Address1", "Address", 80);
            appearance.AddTextColumn("City", "City", 80);
            appearance.AddTextColumn("State", "State", 40);
            appearance.AddTextColumn("Zip", "Zip", 60);
            appearance.AddTextColumn("AccountNumber", "Account", 80);
            appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(this.Appearance_CellFormatting);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__762-0 e$__- = new _Closure$__762-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        private void SetBillActiveState()
        {
            this.lblBillToName.Enabled = this.chbBillActive.Checked;
            this.txtBillName.Enabled = this.chbBillActive.Checked;
            this.CBillToAddress.Enabled = this.chbBillActive.Checked;
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            this.ProcessParameter_Other(Params);
            base.ProcessParameter_ShowMissing(Params);
        }

        private void SetShipActiveState()
        {
            this.lblShipToName.Enabled = this.chbShipActive.Checked;
            this.txtShipName.Enabled = this.chbShipActive.Checked;
            this.CShipToAddress.Enabled = this.chbShipActive.Checked;
        }

        protected override void ShowMissingInformation(bool Show)
        {
            if (Show)
            {
                this.MirHelper.ShowMissingInformation(base.MissingData, this.F_MIR);
            }
            else
            {
                this.MirHelper.ShowMissingInformation(base.MissingData, "");
            }
            this.ControlCustomerInsurance1.ShowMissingInformation(base.MissingData, Show);
            this.ShowMissingInformation(Show, this.tpAssignment);
            this.ShowMissingInformation(Show, this.tpContacts);
            this.ShowMissingInformation(Show, this.tpDiagnosis);
            this.ShowMissingInformation(Show, this.tpGeneral);
            this.ShowMissingInformation(Show, this.tpInsurance);
            this.ShowMissingInformation(Show, this.tpOtherAddresses);
            this.ShowMissingInformation(Show, this.tpPersonal);
        }

        private void ShowMissingInformation(bool Show, TabPage tabPage)
        {
            if (this.TabControl1.TabPages.IndexOf(tabPage) >= 0)
            {
                if (Show && (0 < Functions.EnumerateErrors(tabPage, base.MissingData)))
                {
                    tabPage.ImageIndex = 0;
                }
                else
                {
                    tabPage.ImageIndex = -1;
                }
            }
        }

        private void StartTrackingChanges()
        {
            base.ChangesTracker.Subscribe(this.CName.cmbCourtesy);
            base.ChangesTracker.Subscribe(this.CName.txtFirstName);
            base.ChangesTracker.Subscribe(this.CName.txtLastName);
            base.ChangesTracker.Subscribe(this.CName.txtMiddleName);
            base.ChangesTracker.Subscribe(this.CName.txtSuffix);
            base.ChangesTracker.Subscribe(this.CAddress.txtAddress1);
            base.ChangesTracker.Subscribe(this.CAddress.txtAddress2);
            base.ChangesTracker.Subscribe(this.CAddress.txtCity);
            base.ChangesTracker.Subscribe(this.CAddress.txtState);
            base.ChangesTracker.Subscribe(this.CAddress.txtZip);
            base.ChangesTracker.Subscribe(this.txtPhone);
            base.ChangesTracker.Subscribe(this.txtPhone2);
            base.ChangesTracker.Subscribe(this.txtEmail);
            base.ChangesTracker.Subscribe(this.cmbCustomerClass);
            base.ChangesTracker.Subscribe(this.txtAccountNumber);
            base.ChangesTracker.Subscribe(this.cmbBillingType);
            base.ChangesTracker.Subscribe(this.nmbCustomerBalance);
            base.ChangesTracker.Subscribe(this.cmbCustomerType);
            base.ChangesTracker.Subscribe(this.dtbDeceasedDate);
            base.ChangesTracker.Subscribe(this.dtbDateofBirth);
            base.ChangesTracker.Subscribe(this.cmbLocation);
            base.ChangesTracker.Subscribe(this.nmbTotalBalance);
            base.ChangesTracker.Subscribe(this.chbCollections);
            base.ChangesTracker.Subscribe(this.chbBillActive);
            base.ChangesTracker.Subscribe(this.CBillToAddress.txtAddress1);
            base.ChangesTracker.Subscribe(this.CBillToAddress.txtAddress2);
            base.ChangesTracker.Subscribe(this.CBillToAddress.txtCity);
            base.ChangesTracker.Subscribe(this.txtBillName);
            base.ChangesTracker.Subscribe(this.CBillToAddress.txtState);
            base.ChangesTracker.Subscribe(this.CBillToAddress.txtZip);
            base.ChangesTracker.Subscribe(this.chbCommercialAccount);
            base.ChangesTracker.Subscribe(this.txtDeliveryDirections);
            base.ChangesTracker.Subscribe(this.cmbEmploymentStatus);
            base.ChangesTracker.Subscribe(this.cmbGender);
            base.ChangesTracker.Subscribe(this.nmbHeight);
            base.ChangesTracker.Subscribe(this.txtLicense);
            base.ChangesTracker.Subscribe(this.cmbMaritalStatus);
            base.ChangesTracker.Subscribe(this.cmbMilitaryBranch);
            base.ChangesTracker.Subscribe(this.cmbMilitaryStatus);
            base.ChangesTracker.Subscribe(this.chbShipActive);
            base.ChangesTracker.Subscribe(this.CShipToAddress.txtAddress1);
            base.ChangesTracker.Subscribe(this.CShipToAddress.txtAddress2);
            base.ChangesTracker.Subscribe(this.CShipToAddress.txtCity);
            base.ChangesTracker.Subscribe(this.txtShipName);
            base.ChangesTracker.Subscribe(this.CShipToAddress.txtState);
            base.ChangesTracker.Subscribe(this.CShipToAddress.txtZip);
            base.ChangesTracker.Subscribe(this.txtSSNumber);
            base.ChangesTracker.Subscribe(this.cmbStudentStatus);
            base.ChangesTracker.Subscribe(this.nmbWeight);
            base.ChangesTracker.Subscribe(this.cmbBasis);
            base.ChangesTracker.Subscribe(this.chbBlock12HCFA);
            base.ChangesTracker.Subscribe(this.chbBlock13HCFA);
            base.ChangesTracker.Subscribe(this.nmbCommercialAcctCreditLimit);
            base.ChangesTracker.Subscribe(this.txtCommercialAcctTerms);
            base.ChangesTracker.Subscribe(this.nmbCopayDollar);
            base.ChangesTracker.Subscribe(this.nmbCopayPercent);
            base.ChangesTracker.Subscribe(this.nmbDeductible);
            base.ChangesTracker.Subscribe(this.cmbFrequency);
            base.ChangesTracker.Subscribe(this.chbHardship);
            base.ChangesTracker.Subscribe(this.nmbMonthsValid);
            base.ChangesTracker.Subscribe(this.nmbOutOfPocket);
            base.ChangesTracker.Subscribe(this.dtbSignatureOnFile);
            base.ChangesTracker.Subscribe(this.cmbSignatureType);
            base.ChangesTracker.Subscribe(this.cmbTaxRate);
            base.ChangesTracker.Subscribe(this.cmbInvoiceForm);
            base.ChangesTracker.Subscribe(this.cmbDoctor1);
            base.ChangesTracker.Subscribe(this.cmbDoctor2);
            base.ChangesTracker.Subscribe(this.txtEmergencyContact);
            base.ChangesTracker.Subscribe(this.cmbLegalRep);
            base.ChangesTracker.Subscribe(this.cmbReferral);
            base.ChangesTracker.Subscribe(this.cmbSalesRep);
            base.ChangesTracker.Subscribe(this.cmbFacility);
            base.ChangesTracker.Subscribe(this.cmbAccidentType);
            base.ChangesTracker.Subscribe(this.dtbDateOfInjury);
            base.ChangesTracker.Subscribe(this.chbEmergency);
            base.ChangesTracker.Subscribe(this.chbEmploymentRelated);
            base.ChangesTracker.Subscribe(this.dtbFirstConsultDate);
            base.ChangesTracker.Subscribe(this.eddICD9_1);
            base.ChangesTracker.Subscribe(this.eddICD9_2);
            base.ChangesTracker.Subscribe(this.eddICD9_3);
            base.ChangesTracker.Subscribe(this.eddICD9_4);
            base.ChangesTracker.Subscribe(this.eddICD10_01);
            base.ChangesTracker.Subscribe(this.eddICD10_02);
            base.ChangesTracker.Subscribe(this.eddICD10_03);
            base.ChangesTracker.Subscribe(this.eddICD10_04);
            base.ChangesTracker.Subscribe(this.eddICD10_05);
            base.ChangesTracker.Subscribe(this.eddICD10_06);
            base.ChangesTracker.Subscribe(this.eddICD10_07);
            base.ChangesTracker.Subscribe(this.eddICD10_08);
            base.ChangesTracker.Subscribe(this.eddICD10_09);
            base.ChangesTracker.Subscribe(this.eddICD10_10);
            base.ChangesTracker.Subscribe(this.eddICD10_11);
            base.ChangesTracker.Subscribe(this.eddICD10_12);
            base.ChangesTracker.Subscribe(this.dtbReturnToWorkDate);
            base.ChangesTracker.Subscribe(this.txtStateOfAccident);
            base.ChangesTracker.Subscribe(this.cmbPOSType);
            base.ChangesTracker.Subscribe(this.chbHIPPANote);
            base.ChangesTracker.Subscribe(this.chbSupplierStandards);
            base.ChangesTracker.Subscribe(this.dtbInactiveDate);
            this.ControlCustomerNotes1.Changed += new EventHandler(this.HandleControlChanged);
            this.ControlCustomerInsurance1.Changed += new EventHandler(this.HandleControlChanged);
        }

        private static AllowStateEnum ToAllowState(PermissionsStruct permissions)
        {
            AllowStateEnum allowNone = AllowStateEnum.AllowNone;
            if (permissions.Allow_ADD_EDIT)
            {
                allowNone |= AllowStateEnum.AllowNew | AllowStateEnum.AllowEdit;
            }
            if (permissions.Allow_DELETE)
            {
                allowNone |= AllowStateEnum.AllowDelete;
            }
            return allowNone;
        }

        private void tsmiCustomerInsurancesGridRequest_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rowArray;
                DateTime time;
                GridBase base2 = this.cmsCustomerInsurancesGrid.SourceControl<GridBase>();
                if (base2 != null)
                {
                    rowArray = base2.GetSelectedRows().GetDataRows().ToArray<DataRow>();
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        using (VBDateBox box = new VBDateBox())
                        {
                            box.Text = "Request Eligibility";
                            box.Prompt = "Enter date for requesting eligibility";
                            if (box.ShowDialog() == DialogResult.OK)
                            {
                                if (box.Value == null)
                                {
                                    throw new UserNotifyException("You should select request date");
                                }
                                time = box.Value.Value;
                                goto TR_0013;
                            }
                        }
                    }
                }
                return;
            TR_000B:
                this.InvalidateObjectList();
                return;
            TR_0013:
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE tbl_customer_insurance\r\nSET RequestEligibility = 1\r\n  , RequestEligibilityOn = :P_RequestDate\r\nWHERE ID = :P_CustomerInsuranceID\r\n  AND CustomerID = :P_CustomerID";
                        command.Parameters.Add("P_RequestDate", MySqlType.Date).Value = time;
                        command.Parameters.Add("P_CustomerInsuranceID", MySqlType.Int);
                        command.Parameters.Add("P_CustomerID", MySqlType.SmallInt);
                        foreach (DataRow row in rowArray)
                        {
                            command.Parameters["P_CustomerInsuranceID"].Value = row["CustomerInsuranceID"];
                            command.Parameters["P_CustomerID"].Value = row["CustomerID"];
                            command.ExecuteNonQuery();
                        }
                    }
                }
                goto TR_000B;
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

        private void tsmiGridSearchMakeInactive_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                if (grid != null)
                {
                    new SearchGridProcessor.MakeInactive(grid).Run();
                    this.InvalidateObjectList();
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

        private void txtSSNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar != '\b') && (e.KeyChar != '\x0018')) && (((this.txtSSNumber.SelectionStart == 3) && (this.txtSSNumber.Text.Length == 3)) || ((this.txtSSNumber.SelectionStart == 6) && (this.txtSSNumber.Text.Length == 6))))
                {
                    char[] chArray1 = new char[] { '-', e.KeyChar };
                    this.txtSSNumber.SelectedText = new string(chArray1);
                    e.Handled = true;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private void ValidateAccountNumber(int ID, bool IsNew)
        {
            if (!Globals.AutoGenerateAccountNumbers)
            {
                string str = this.txtAccountNumber.Text.Trim();
                if (str == "")
                {
                    base.ValidationErrors.SetError(this.txtAccountNumber, "You must input account Number");
                }
                else
                {
                    int? nullable;
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            if (IsNew)
                            {
                                command.CommandText = "SELECT Count(*) FROM tbl_customer WHERE (`AccountNumber` = :AccountNumber)";
                                command.Parameters.Add("AccountNumber", MySqlType.VarChar, 50).Value = str;
                            }
                            else
                            {
                                command.CommandText = "SELECT Count(*) FROM tbl_customer WHERE (`AccountNumber` = :AccountNumber) AND (`ID` <> :ID)";
                                command.Parameters.Add("AccountNumber", MySqlType.VarChar, 50).Value = str;
                                command.Parameters.Add("ID", MySqlType.Int, 4).Value = ID;
                            }
                            nullable = NullableConvert.ToInt32(command.ExecuteScalar());
                        }
                    }
                    if ((nullable != null) && (0 < nullable.Value))
                    {
                        base.ValidationErrors.SetError(this.txtAccountNumber, $"Customer with account# {str} already exists in the database");
                    }
                }
            }
        }

        private void ValidateNameDOB(int ID, bool IsNew)
        {
            int? nullable;
            int? nullable2;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  Sum(1) as CountSimilar\r\n, Sum(CASE WHEN MiddleName = :MiddleName THEN 1 ELSE 0 END) as CountSame\r\nFROM tbl_customer\r\nWHERE (FirstName = :FirstName)\r\n  AND (LastName = :LastName)\r\n  AND (DateOfBirth = :DateOfBirth)\r\n  AND ((:ID IS NULL) OR (ID != :ID))";
                    command.Parameters.Add("FirstName", MySqlType.VarChar, 0x19).Value = this.CName.txtFirstName.Text;
                    command.Parameters.Add("LastName", MySqlType.VarChar, 30).Value = this.CName.txtLastName.Text;
                    command.Parameters.Add("MiddleName", MySqlType.Char, 1).Value = this.CName.txtMiddleName.Text;
                    command.Parameters.Add("DateofBirth", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbDateofBirth);
                    command.Parameters.Add("ID", MySqlType.Int).Value = !IsNew ? ((object) ID) : ((object) DBNull.Value);
                    using (MySqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.SingleResult))
                    {
                        if (reader.Read())
                        {
                            nullable = NullableConvert.ToInt32(reader["CountSimilar"]);
                            nullable2 = NullableConvert.ToInt32(reader["CountSame"]);
                        }
                        else
                        {
                            nullable = null;
                            nullable2 = null;
                        }
                    }
                }
            }
            if ((nullable2 != null) && (0 < nullable2.Value))
            {
                base.ValidationErrors.SetError(this.CName.txtFirstName, "Customer with same First name, Last name, Middle name and DOD already exists in the database");
                base.ValidationErrors.SetError(this.CName.txtLastName, "Customer with same First name, Last name, Middle name and DOD already exists in the database");
                base.ValidationErrors.SetError(this.CName.txtMiddleName, "Customer with same First name, Last name, Middle name and DOD already exists in the database");
                base.ValidationErrors.SetError(this.dtbDateofBirth, "Customer with same First name, Last name, Middle name and DOD already exists in the database");
            }
            else if ((nullable != null) && (0 < nullable.Value))
            {
                base.ValidationWarnings.SetError(this.CName.txtFirstName, "Customer with same First name, Last name and DOD already exists in the database");
                base.ValidationWarnings.SetError(this.CName.txtLastName, "Customer with same First name, Last name and DOD already exists in the database");
                base.ValidationWarnings.SetError(this.dtbDateofBirth, "Customer with same First name, Last name and DOD already exists in the database");
            }
            else
            {
                if (this.CName.txtFirstName.Text.TrimEnd(new char[0]).Length == 0)
                {
                    base.ValidationWarnings.SetError(this.CName.txtFirstName, "Did you forget to enter First name?");
                }
                if (this.CName.txtLastName.Text.TrimEnd(new char[0]).Length == 0)
                {
                    base.ValidationWarnings.SetError(this.CName.txtLastName, "Did you forget to enter Last name?");
                }
                if (!(this.dtbDateofBirth.Value is DateTime))
                {
                    base.ValidationWarnings.SetError(this.dtbDateofBirth, "Did you forget to enter Date of birth?");
                }
            }
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            this.ValidateAccountNumber(ID, IsNew);
            this.ValidateNameDOB(ID, IsNew);
            base.ValidationErrors.SetError(this.txtPhone, Functions.PhoneValidate(this.txtPhone.Text));
            base.ValidationErrors.SetError(this.txtPhone2, Functions.PhoneValidate(this.txtPhone2.Text));
        }

        [field: AccessedThroughProperty("CName")]
        private ControlName CName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpGeneral")]
        private TabPage tpGeneral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBillingType")]
        private Combobox cmbBillingType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerType")]
        private Combobox cmbCustomerType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCustomerBalance")]
        private NumericBox nmbCustomerBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbTotalBalance")]
        private NumericBox nmbTotalBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtAccountNumber")]
        private TextBox txtAccountNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerBalance")]
        private Label lblCustomerBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerType")]
        private Label lblCustomerType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillingType")]
        private Label lblBillingType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLocationCode")]
        private Label lblLocationCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAcctNum")]
        private Label lblAcctNum { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDeceasedDate")]
        private UltraDateTimeEditor dtbDeceasedDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDeceasedDate")]
        private Label lblDeceasedDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone2")]
        private Label lblPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone")]
        private Label lblPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpPersonal")]
        private TabPage tpPersonal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDeliveryDirections")]
        private TextBox txtDeliveryDirections { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDeliveryDirections")]
        private Label lblDeliveryDirections { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbWeight")]
        private NumericBox nmbWeight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbHeight")]
        private NumericBox nmbHeight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbMilitary")]
        private GroupBox gbMilitary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbMilitaryBranch")]
        private ComboBox cmbMilitaryBranch { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMilitaryBranch")]
        private Label lblMilitaryBranch { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbMilitaryStatus")]
        private ComboBox cmbMilitaryStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMilitaryStatus")]
        private Label lblMilitaryStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbStatus")]
        private GroupBox gbStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbMaritalStatus")]
        private ComboBox cmbMaritalStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEmploymentStatus")]
        private Label lblEmploymentStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblStudentStatus")]
        private Label lblStudentStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbEmploymentStatus")]
        private ComboBox cmbEmploymentStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMaritalStatus")]
        private Label lblMaritalStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbStudentStatus")]
        private ComboBox cmbStudentStatus { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLicense")]
        private TextBox txtLicense { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbGender")]
        private ComboBox cmbGender { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbCommercialAccount")]
        private CheckBox chbCommercialAccount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLicense")]
        private Label lblLicense { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSSNumber")]
        private Label lblSSNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblGender")]
        private Label lblGender { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWeight")]
        private Label lblWeight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblHeight")]
        private Label lblHeight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpInsurance")]
        private TabPage tpInsurance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpDiagnosis")]
        private TabPage tpDiagnosis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDateOfInjury")]
        private UltraDateTimeEditor dtbDateOfInjury { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblStateOfAccident")]
        private Label lblStateOfAccident { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDateInq")]
        private Label lblDateInq { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtStateOfAccident")]
        private TextBox txtStateOfAccident { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblRetToWorkDat")]
        private Label lblRetToWorkDat { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFirConDat")]
        private Label lblFirConDat { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbReturnToWorkDate")]
        private UltraDateTimeEditor dtbReturnToWorkDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbFirstConsultDate")]
        private UltraDateTimeEditor dtbFirstConsultDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbEmploymentRelated")]
        private CheckBox chbEmploymentRelated { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbEmergency")]
        private CheckBox chbEmergency { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_2")]
        private ExtendedDropdown eddICD9_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_4")]
        private ExtendedDropdown eddICD9_4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_3")]
        private ExtendedDropdown eddICD9_3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD9_1")]
        private ExtendedDropdown eddICD9_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_4")]
        private Label lblICD9_4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_3")]
        private Label lblICD9_3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_2")]
        private Label lblICD9_2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblICD9_1")]
        private Label lblICD9_1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpContacts")]
        private TabPage tpContacts { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEmergencyContact")]
        private TextBox txtEmergencyContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEmergencyContact")]
        private Label lblEmergencyContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLegalRep")]
        private Label lblLegalRep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbLegalRep")]
        private Combobox cmbLegalRep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSalesRep")]
        private Label lblSalesRep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReferral")]
        private Label lblReferral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDoctor2")]
        private Label lblDoctor2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDoctor1")]
        private Label lblDoctor1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbSalesRep")]
        private Combobox cmbSalesRep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbReferral")]
        private Combobox cmbReferral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctor2")]
        private Combobox cmbDoctor2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctor1")]
        private Combobox cmbDoctor1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpOtherAddresses")]
        private TabPage tpOtherAddresses { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbShipTo")]
        private GroupBox gbShipTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CShipToAddress")]
        private ControlAddress CShipToAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipToName")]
        private Label lblShipToName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipName")]
        private TextBox txtShipName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbShipActive")]
        private CheckBox chbShipActive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbBillTo")]
        private GroupBox gbBillTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CBillToAddress")]
        private ControlAddress CBillToAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillToName")]
        private Label lblBillToName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBillName")]
        private TextBox txtBillName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBillActive")]
        private CheckBox chbBillActive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpEquipment")]
        private TabPage tpEquipment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dgEquipment")]
        private FilteredGrid dgEquipment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpAssignment")]
        private TabPage tpAssignment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbSignatureOnFile")]
        private UltraDateTimeEditor dtbSignatureOnFile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbTaxRate")]
        private Combobox cmbTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbOutOfPocket")]
        private NumericBox nmbOutOfPocket { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDeductible")]
        private NumericBox nmbDeductible { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbMonthsValid")]
        private NumericBox nmbMonthsValid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCopayDollar")]
        private NumericBox nmbCopayDollar { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCopayPercent")]
        private NumericBox nmbCopayPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbCommercialAccounts")]
        private GroupBox gbCommercialAccounts { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTerms")]
        private Label lblTerms { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCreditLimit")]
        private Label lblCreditLimit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCommercialAcctCreditLimit")]
        private NumericBox nmbCommercialAcctCreditLimit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCommercialAcctTerms")]
        private TextBox txtCommercialAcctTerms { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbFrequency")]
        private ComboBox cmbFrequency { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBasis")]
        private ComboBox cmbBasis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbHardship")]
        private CheckBox chbHardship { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBlock13HCFA")]
        private CheckBox chbBlock13HCFA { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBlock12HCFA")]
        private CheckBox chbBlock12HCFA { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOutOfPocket")]
        private Label lblOutOfPocket { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxRate")]
        private Label lblTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFrequency")]
        private Label lblFrequency { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCopayDollar")]
        private Label lblCopayDollar { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDeductible")]
        private Label lblDeductible { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBasis")]
        private Label lblBasis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCopayPercent")]
        private Label lblCopayPercent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMonthsValid")]
        private Label lblMonthsValid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSignatureOnFile")]
        private Label lblSignatureOnFile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpNotes")]
        private TabPage tpNotes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbLocation")]
        private Combobox cmbLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone2")]
        private TextBox txtPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSSNumber")]
        private TextBox txtSSNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFacility")]
        private Label lblFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbFacility")]
        private Combobox cmbFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDateofBirth")]
        private UltraDateTimeEditor dtbDateofBirth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDateofBirth")]
        private Label lblDateofBirth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerClass")]
        private Combobox cmbCustomerClass { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerClass")]
        private Label lblCustomerClass { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlCustomerInsurance1")]
        private ControlCustomerInsurance ControlCustomerInsurance1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlCustomerNotes1")]
        private ControlCustomerNotes ControlCustomerNotes1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ImageList1")]
        private ImageList ImageList1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAccidentType")]
        private Label lblAccidentType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbAccidentType")]
        private ComboBox cmbAccidentType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSignatureType")]
        private Label lblSignatureType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbSignatureType")]
        private ComboBox cmbSignatureType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPOSType")]
        private Combobox cmbPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPOSType")]
        private Label lblPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbBalance")]
        private GroupBox gbBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbInactiveDate")]
        private UltraDateTimeEditor dtbInactiveDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInactiveDate")]
        private Label lblInactiveDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSupplierStandards")]
        private CheckBox chbSupplierStandards { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbHIPPANote")]
        private CheckBox chbHIPPANote { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInvoiceForm")]
        private Combobox cmbInvoiceForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoImages")]
        private MenuItem mnuGotoImages { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoNewImage")]
        private MenuItem mnuGotoNewImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoEligibility")]
        private MenuItem mnuGotoEligibility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoPaymentPlan")]
        private MenuItem mnuGotoPaymentPlan { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEmail")]
        private TextBox txtEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEmail")]
        private Label lblEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnEmail")]
        private Button btnEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnRefreshBalance")]
        private Button btnRefreshBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTotalBalance")]
        private Label lblTotalBalance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbCollections")]
        private CheckBox chbCollections { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCollections")]
        private Label lblCollections { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInvoiceForm")]
        private Label lblInvoiceForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsScheduleMeeting")]
        private MenuItem mnuActionsScheduleMeeting { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsCustomerInsurancesGrid")]
        private ContextMenuStrip cmsCustomerInsurancesGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiCustomerInsurancesGridRequest")]
        private ToolStripMenuItem tsmiCustomerInsurancesGridRequest { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl2")]
        private TabControl TabControl2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabPage1")]
        private TabPage TabPage1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabPage2")]
        private TabPage TabPage2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabPage3")]
        private TabPage TabPage3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_02")]
        private ExtendedDropdown eddICD10_02 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_04")]
        private ExtendedDropdown eddICD10_04 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_03")]
        private ExtendedDropdown eddICD10_03 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_01")]
        private ExtendedDropdown eddICD10_01 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label4")]
        private Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_10")]
        private ExtendedDropdown eddICD10_10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_06")]
        private ExtendedDropdown eddICD10_06 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_08")]
        private ExtendedDropdown eddICD10_08 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_12")]
        private ExtendedDropdown eddICD10_12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_07")]
        private ExtendedDropdown eddICD10_07 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label9")]
        private Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label6")]
        private Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_05")]
        private ExtendedDropdown eddICD10_05 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_11")]
        private ExtendedDropdown eddICD10_11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label7")]
        private Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label8")]
        private Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label10")]
        private Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_09")]
        private ExtendedDropdown eddICD10_09 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label12")]
        private Label Label12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label11")]
        private Label Label11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridSearch")]
        private ContextMenuStrip cmsGridSearch { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchMakeInactive")]
        private ToolStripMenuItem tsmiGridSearchMakeInactive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private static bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        private FormMirHelper MirHelper
        {
            get
            {
                if (this.F_MirHelper == null)
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.F_MirHelper.Add("AccountNumber", this.txtAccountNumber, "Account Number is required for invoice");
                    this.F_MirHelper.Add("FirstName", this.CName.txtFirstName, "First Name is required for invoice");
                    this.F_MirHelper.Add("LastName", this.CName.txtLastName, "Last Name is required for invoice");
                    this.F_MirHelper.Add("Address1", this.CAddress.txtAddress1, "Address-line-1 is required for invoice");
                    this.F_MirHelper.Add("City", this.CAddress.txtCity, "City is required for invoice");
                    this.F_MirHelper.Add("State", this.CAddress.txtState, "State is required for invoice");
                    this.F_MirHelper.Add("Zip", this.CAddress.txtZip, "Zip is required for invoice");
                    this.F_MirHelper.Add("EmploymentStatus", this.cmbEmploymentStatus, "Employment Status is required for invoice");
                    this.F_MirHelper.Add("Gender", this.cmbGender, "Gender is required for invoice");
                    this.F_MirHelper.Add("MaritalStatus", this.cmbMaritalStatus, "Marital Status is required for invoice");
                    this.F_MirHelper.Add("MilitaryBranch", this.cmbMilitaryBranch, "Military Branch is required for invoice");
                    this.F_MirHelper.Add("MilitaryStatus", this.cmbMilitaryStatus, "Military Status is required for invoice");
                    this.F_MirHelper.Add("StudentStatus", this.cmbStudentStatus, "Student Status is required for invoice");
                    this.F_MirHelper.Add("MonthsValid", this.nmbMonthsValid, "Number of Months valid is required for invoice");
                    this.F_MirHelper.Add("DateofBirth", this.dtbDateofBirth, "Date of Birth is required for invoice");
                    this.F_MirHelper.Add("SignatureOnFile", this.dtbSignatureOnFile, "Date of Signature is required for invoice");
                    this.F_MirHelper.Add("Doctor1_ID", this.cmbDoctor1, "Doctor is required for invoice");
                    this.F_MirHelper.Add("Doctor1", this.cmbDoctor1, "Doctor contains missing information");
                }
                return this.F_MirHelper;
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__762-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }

        private abstract class SearchGridProcessor
        {
            protected readonly GridBase Grid;
            protected readonly string DialogCaption;

            protected SearchGridProcessor(GridBase grid, string dialogCaption)
            {
                if (grid == null)
                {
                    throw new ArgumentNullException("grid");
                }
                this.Grid = grid;
                this.DialogCaption = dialogCaption;
            }

            protected abstract MySqlCommand[] CreateCommands();
            private void DoWork(object sender, DoWorkEventArgs e)
            {
                Tuple<int[], MySqlCommand[]> argument = e.Argument as Tuple<int[], MySqlCommand[]>;
                if (argument != null)
                {
                    int[] numArray = argument.Item1;
                    if ((numArray != null) && (numArray.Length != 0))
                    {
                        MySqlCommand[] commandArray = argument.Item2;
                        if ((commandArray != null) && (commandArray.Length != 0))
                        {
                            BackgroundWorker worker = (BackgroundWorker) sender;
                            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                            {
                                connection.Open();
                                int[] affected = new int[(commandArray.Length - 1) + 1];
                                int num2 = commandArray.Length - 1;
                                int index = 0;
                                while (true)
                                {
                                    if (index > num2)
                                    {
                                        int num = Math.Max(1, numArray.Length / 50);
                                        int num4 = numArray.Length - 1;
                                        for (int i = 0; (i <= num4) && !worker.CancellationPending; i++)
                                        {
                                            MySqlTransaction transaction = connection.BeginTransaction();
                                            try
                                            {
                                                int num6 = commandArray.Length - 1;
                                                int num7 = 0;
                                                while (true)
                                                {
                                                    if (num7 > num6)
                                                    {
                                                        transaction.Commit();
                                                        break;
                                                    }
                                                    MySqlCommand command = commandArray[num7];
                                                    if (command != null)
                                                    {
                                                        ref int numRef;
                                                        command.Parameters["ID"].Value = numArray[i];
                                                        *(numRef = ref affected[num7]) = numRef + command.ExecuteNonQuery();
                                                    }
                                                    num7++;
                                                }
                                            }
                                            catch (Exception exception1)
                                            {
                                                Exception ex = exception1;
                                                ProjectData.SetProjectError(ex);
                                                Exception exception = ex;
                                                transaction.Rollback();
                                                throw;
                                            }
                                            if ((i % num) == 0)
                                            {
                                                worker.ReportProgress((int) Math.Round((double) ((100.0 * (i + 1)) / ((double) numArray.Length))), this.GetUserState(affected));
                                            }
                                        }
                                        break;
                                    }
                                    if (commandArray[index] != null)
                                    {
                                        commandArray[index].Connection = connection;
                                    }
                                    affected[index] = 0;
                                    index++;
                                }
                            }
                        }
                    }
                }
            }

            protected int[] GetSelectedIDs() => 
                this.Grid.GetSelectedRows().GetDataRows().Select<DataRow, int>(((_Closure$__.$I3-0 == null) ? (_Closure$__.$I3-0 = new Func<DataRow, int>(_Closure$__.$I._Lambda$__3-0)) : _Closure$__.$I3-0)).ToArray<int>();

            protected abstract string GetUserState(int[] affected);
            public void Run()
            {
                Tuple<int[], MySqlCommand[]> argument = Tuple.Create<int[], MySqlCommand[]>(this.GetSelectedIDs(), this.CreateCommands());
                using (DialogBackgroundWorker worker = new DialogBackgroundWorker(this.DialogCaption, new DoWorkEventHandler(this.DoWork), argument))
                {
                    worker.ShowDialog();
                }
            }

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly FormCustomer.SearchGridProcessor._Closure$__ $I = new FormCustomer.SearchGridProcessor._Closure$__();
                public static Func<DataRow, int> $I3-0;

                internal int _Lambda$__3-0(DataRow r) => 
                    Convert.ToInt32(r["ID"]);
            }

            public class MakeInactive : FormCustomer.SearchGridProcessor
            {
                public MakeInactive(GridBase grid) : base(grid, "Make Inactive...")
                {
                }

                protected override MySqlCommand[] CreateCommands()
                {
                    MySqlCommand[] commandArray;
                    using (VBDateBox box = new VBDateBox())
                    {
                        box.Text = "Make Inactive...";
                        box.Prompt = "Select \"Inactive Date\"";
                        if (box.ShowDialog() != DialogResult.OK)
                        {
                            commandArray = new MySqlCommand[0];
                        }
                        else if (box.Value == null)
                        {
                            commandArray = new MySqlCommand[0];
                        }
                        else
                        {
                            MySqlCommand[] commandArray1 = new MySqlCommand[] { new MySqlCommand() };
                            commandArray1[0].CommandType = CommandType.Text;
                            commandArray1[0].CommandText = "UPDATE tbl_customer SET InactiveDate = :InactiveDate WHERE ID = :ID AND InactiveDate IS NULL";
                            commandArray1[0].Parameters.Add(new MySqlParameter(":ID", MySqlType.Int));
                            MySqlParameter parameter1 = new MySqlParameter(":InactiveDate", MySqlType.Date);
                            parameter1.Value = box.Value.Value;
                            commandArray1[0].Parameters.Add(parameter1);
                            commandArray = commandArray1;
                        }
                    }
                    return commandArray;
                }

                protected override string GetUserState(int[] affected) => 
                    $"{affected[0]} customers made inactive";
            }
        }
    }
}

