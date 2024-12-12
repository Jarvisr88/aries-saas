namespace DMEWorks.Maintain
{
    using Dapper;
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Core.Extensions;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using DMEWorks.Forms.Shipping;
    using DMEWorks.Misc;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonReload=true, ButtonMissing=true)]
    public class FormOrder : FormAutoIncrementMaintain
    {
        private IContainer components;
        private string F_MIR;
        private FormMirHelper F_MirHelper;

        public FormOrder()
        {
            this.InitializeComponent();
            base.cmnuFilter.Popup += new EventHandler(this.cmnuFilter_Popup);
            this.ControlOrderDetails1.DefaultDOSFrom = DateTime.Today;
            if (PagedGrids)
            {
                base.AddPagedNavigator(new SearchNavigatorEventsHandler(this));
                base.AddPagedNavigator(new DetailsNavigatorEventsHandler(this));
            }
            else
            {
                base.AddSimpleNavigator(new SearchNavigatorEventsHandler(this));
                base.AddSimpleNavigator(new DetailsNavigatorEventsHandler(this));
            }
            base.ChangesTracker.Subscribe(this.chbApproved);
            base.ChangesTracker.Subscribe(this.cmbCustomer);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance1);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance2);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance3);
            base.ChangesTracker.Subscribe(this.cmbCustomerInsurance4);
            base.ChangesTracker.Subscribe(this.cmbDoctor);
            base.ChangesTracker.Subscribe(this.cmbFacility);
            base.ChangesTracker.Subscribe(this.cmbLocation);
            base.ChangesTracker.Subscribe(this.cmbPosType);
            base.ChangesTracker.Subscribe(this.cmbReferral);
            base.ChangesTracker.Subscribe(this.cmbSalesrep);
            base.ChangesTracker.Subscribe(this.cmbShippingMethod);
            base.ChangesTracker.Subscribe(this.dtbBillDate);
            base.ChangesTracker.Subscribe(this.dtbDeliveryDate);
            base.ChangesTracker.Subscribe(this.dtbOrderDate);
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
            base.ChangesTracker.Subscribe(this.eddICD9_1);
            base.ChangesTracker.Subscribe(this.eddICD9_2);
            base.ChangesTracker.Subscribe(this.eddICD9_3);
            base.ChangesTracker.Subscribe(this.eddICD9_4);
            base.ChangesTracker.Subscribe(this.txtClaimNote);
            base.ChangesTracker.Subscribe(this.txtSpecialInstructions);
            base.ChangesTracker.Subscribe(this.txtTakenBy);
            base.ChangesTracker.Subscribe(this.txtUserField1);
            base.ChangesTracker.Subscribe(this.txtUserField2);
            this.ControlOrderDetails1.Changed += new EventHandler(this.HandleControlChanged);
        }

        private void chbApproved_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateActionsState();
        }

        private void ClearCustomer()
        {
            Functions.SetComboBoxValue(this.cmbLocation, NullableConvert.ToDb(Globals.LocationID));
            Functions.SetTextBoxText(this.caShip.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.caShip.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            this.eddICD9_1.Text = "";
            this.eddICD9_2.Text = "";
            this.eddICD9_3.Text = "";
            this.eddICD9_4.Text = "";
            this.ResetDefaultDXPointer9();
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
            this.ResetDefaultDXPointer10();
            Functions.SetComboBoxValue(this.cmbDoctor, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPosType, NullableConvert.ToDb(ClassGlobalObjects.DefaultPOSTypeID));
            Functions.SetComboBoxValue(this.cmbFacility, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbReferral, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbSalesrep, DBNull.Value);
            this.InternalLoadCustomerInsurance(new CustomerInsuranceTable());
            this.cmbCustomerInsurance1.SelectedValue = DBNull.Value;
            this.cmbCustomerInsurance2.SelectedValue = DBNull.Value;
            this.cmbCustomerInsurance3.SelectedValue = DBNull.Value;
            this.cmbCustomerInsurance4.SelectedValue = DBNull.Value;
            this.ResetDefaultBillIns1();
            this.ResetDefaultBillIns2();
            this.ResetDefaultBillIns3();
            this.ResetDefaultBillIns4();
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.F_MIR = "";
            Functions.SetComboBoxValue(this.cmbCustomer, DBNull.Value);
            this.ClearCustomer();
            Functions.SetTextBoxText(this.txtSpecialInstructions, DBNull.Value);
            Functions.SetTextBoxText(this.txtClaimNote, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbBillDate, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbOrderDate, DateTime.Today);
            Functions.SetTextBoxText(this.txtTakenBy, Globals.CompanyUserName);
            Functions.SetDateBoxValue(this.dtbDeliveryDate, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbApproved, DBNull.Value);
            this.ApprovedState = false;
            this.ResetDefaultBillIns1();
            this.ResetDefaultBillIns2();
            this.ResetDefaultBillIns3();
            this.ResetDefaultBillIns4();
            this.ResetDefaultDXPointer9();
            this.ResetDefaultDXPointer10();
            this.ResetDefaultDOSFrom();
            this.ControlOrderDetails1.ClearGrid();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((base.State != FormMaintainBase.FormState.ClearingData) && ((base.State != FormMaintainBase.FormState.LoadingData) && this.IsNew))
            {
                try
                {
                    if (Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
                    {
                        int customerID = Conversions.ToInteger(this.cmbCustomer.SelectedValue);
                        this.LoadCustomer(customerID);
                        this.ControlOrderDetails1.CustomerID = new int?(customerID);
                    }
                    else
                    {
                        this.ClearCustomer();
                        this.ControlOrderDetails1.CustomerID = null;
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
        }

        private void cmbCustomerInsurance1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ResetDefaultBillIns1();
            this.ResetSameOrSimilar();
        }

        private void cmbCustomerInsurance2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ResetDefaultBillIns2();
        }

        private void cmbCustomerInsurance3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ResetDefaultBillIns3();
        }

        private void cmbCustomerInsurance4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ResetDefaultBillIns4();
        }

        private void cmbShippingMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateShippingState();
        }

        private void cmnuFilter_Popup(object sender, EventArgs e)
        {
            this.mnuFilterPagedGrids.Checked = PagedGrids;
        }

        private void CreateInvoice()
        {
            DateTime today = DateTime.Today;
            if (base.SaveOrCancelChanges())
            {
                if (NullableConvert.ToDateTime(Functions.GetDateBoxValue(this.dtbBillDate)) == null)
                {
                    throw new UserNotifyException("Cannot proceed. 'Bill Date' must be assigned.");
                }
                if (NullableConvert.ToDateTime(Functions.GetDateBoxValue(this.dtbOrderDate)) == null)
                {
                    throw new UserNotifyException("Cannot proceed. 'Order Date' must be assigned.");
                }
                if (!this.chbApproved.Checked)
                {
                    throw new UserNotifyException("Cannot proceed. Order must be approved.");
                }
                int? nullable = NullableConvert.ToInt32(this.ObjectID);
                if (nullable != null)
                {
                    int num = 0;
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.Parameters.Add("P_OrderID", MySqlType.Int).Value = nullable;
                            command.ExecuteProcedure("mir_update_orderdetails");
                            command.ExecuteProcedure("mir_update_order");
                        }
                        using (DataTable table = new DataTable())
                        {
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                            {
                                adapter.SelectCommand.CommandText = "SELECT *\r\nFROM view_mir\r\nWHERE OrderID = :OrderID";
                                adapter.SelectCommand.Parameters.Add("OrderID", MySqlType.Int).Value = nullable;
                                adapter.AcceptChangesDuringFill = true;
                                adapter.Fill(table);
                            }
                            if (0 < table.Rows.Count)
                            {
                                StringBuilder builder = new StringBuilder(0x400);
                                builder.Append("Cannot proceed. Order has some required information missing:").AppendLine();
                                DataRow[] rowArray = table.Select();
                                for (int i = 0; i < rowArray.Length; i++)
                                {
                                    DataRow row1 = rowArray[i];
                                    string str = Convert.ToString(row1["InventoryItem"]);
                                    string str2 = Convert.ToString(row1["MIR"]);
                                    builder.Append(str.Trim()).Append(" -- ").Append(str2).AppendLine();
                                }
                                throw new UserNotifyException(builder.ToString());
                            }
                        }
                        using (DataTable table2 = new DataTable())
                        {
                            using (MySqlDataAdapter adapter2 = new MySqlDataAdapter("", connection))
                            {
                                adapter2.SelectCommand.CommandText = "SELECT DISTINCT\r\n  OrderID,\r\n  BillingMonth,\r\n  BillingFlags\r\nFROM view_billinglist\r\nWHERE OrderID = :OrderID\r\nORDER BY OrderID, BillingMonth DESC";
                                adapter2.SelectCommand.Parameters.Add("OrderID", MySqlType.Int).Value = nullable.Value;
                                adapter2.AcceptChangesDuringFill = true;
                                adapter2.Fill(table2);
                            }
                            if (table2.Rows.Count == 0)
                            {
                                throw new UserNotifyException("Cannot proceed. Cannot find any line items suitable.");
                            }
                            MySqlTransaction transaction = connection.BeginTransaction();
                            try
                            {
                                using (MySqlCommand command2 = new MySqlCommand("", connection, transaction))
                                {
                                    IEnumerator enumerator;
                                    command2.CommandText = "CALL `order_process_2`(:P_OrderID, :P_BillingMonth, :P_BillingFlags, :P_InvoiceDate)";
                                    command2.Parameters.Add("P_OrderID", MySqlType.Int);
                                    command2.Parameters.Add("P_BillingMonth", MySqlType.Int);
                                    command2.Parameters.Add("P_BillingFlags", MySqlType.Int);
                                    command2.Parameters.Add("P_InvoiceDate", MySqlType.Date).Value = today;
                                    command2.Prepare();
                                    try
                                    {
                                        enumerator = table2.Rows.GetEnumerator();
                                        while (enumerator.MoveNext())
                                        {
                                            DataRow current = (DataRow) enumerator.Current;
                                            command2.Parameters["P_OrderID"].Value = current["OrderID"];
                                            command2.Parameters["P_BillingMonth"].Value = current["BillingMonth"];
                                            command2.Parameters["P_BillingFlags"].Value = current["BillingFlags"];
                                            command2.ExecuteNonQuery();
                                            num++;
                                        }
                                    }
                                    finally
                                    {
                                        if (enumerator is IDisposable)
                                        {
                                            (enumerator as IDisposable).Dispose();
                                        }
                                    }
                                }
                                transaction.Commit();
                            }
                            catch (Exception exception1)
                            {
                                Exception ex = exception1;
                                ProjectData.SetProjectError(ex);
                                Exception innerException = ex;
                                transaction.Rollback();
                                throw new Exception("Error in transaction", innerException);
                            }
                            using (MySqlCommand command3 = new MySqlCommand("", connection))
                            {
                                IEnumerator enumerator;
                                command3.CommandText = "CALL inventory_transaction_order_refresh(:P_OrderID)";
                                command3.Parameters.Add("P_OrderID", MySqlType.Int);
                                try
                                {
                                    enumerator = table2.Rows.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        DataRow current = (DataRow) enumerator.Current;
                                        command3.Parameters["P_OrderID"].Value = current["OrderID"];
                                        command3.ExecuteNonQuery();
                                    }
                                }
                                finally
                                {
                                    if (enumerator is IDisposable)
                                    {
                                        (enumerator as IDisposable).Dispose();
                                    }
                                }
                            }
                            using (MySqlCommand command4 = new MySqlCommand("", connection))
                            {
                                IEnumerator enumerator;
                                command4.CommandText = "CALL inventory_order_refresh(:P_OrderID)";
                                command4.Parameters.Add("P_OrderID", MySqlType.Int);
                                try
                                {
                                    enumerator = table2.Rows.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        DataRow current = (DataRow) enumerator.Current;
                                        command4.Parameters["P_OrderID"].Value = current["OrderID"];
                                        command4.ExecuteNonQuery();
                                    }
                                }
                                finally
                                {
                                    if (enumerator is IDisposable)
                                    {
                                        (enumerator as IDisposable).Dispose();
                                    }
                                }
                            }
                            using (MySqlCommand command5 = new MySqlCommand("", connection))
                            {
                                IEnumerator enumerator;
                                command5.CommandText = "CALL serial_order_refresh(:P_OrderID)";
                                command5.Parameters.Add("P_OrderID", MySqlType.Int);
                                try
                                {
                                    enumerator = table2.Rows.GetEnumerator();
                                    while (enumerator.MoveNext())
                                    {
                                        DataRow current = (DataRow) enumerator.Current;
                                        command5.Parameters["P_OrderID"].Value = current["OrderID"];
                                        command5.ExecuteNonQuery();
                                    }
                                }
                                finally
                                {
                                    if (enumerator is IDisposable)
                                    {
                                        (enumerator as IDisposable).Dispose();
                                    }
                                }
                            }
                            this.NotifyUser("Processing created " + Conversions.ToString(num) + " invoice" + ((num == 1) ? "" : "s"));
                            base.OpenObject(nullable.Value);
                        }
                    }
                }
            }
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "DELETE tbl_order\r\nFROM tbl_order\r\n     LEFT JOIN tbl_orderdetails ON tbl_orderdetails.CustomerID = tbl_order.CustomerID\r\n                               AND tbl_orderdetails.OrderID    = tbl_order.ID\r\nWHERE (tbl_order.ID = :ID)\r\n  AND (tbl_orderdetails.OrderID IS NULL)";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteNonQuery())
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
            }
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

        private void dtbDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            this.ResetDefaultDOSFrom();
        }

        private void eddICD10_TextChanged(object sender, EventArgs e)
        {
            this.ResetDefaultDXPointer10();
        }

        private void eddICD9_TextChanged(object sender, EventArgs e)
        {
            this.ResetDefaultDXPointer9();
        }

        private string[] GetMedicarePolicyNumbers()
        {
            string[] strArray;
            int? nullable = NullableConvert.ToInt32(this.cmbCustomerInsurance1.SelectedValue);
            if (nullable == null)
            {
                strArray = Enumerable.Empty<string>().ToArray<string>();
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    int? commandTimeout = null;
                    CommandType? commandType = null;
                    strArray = connection.Query<string>("SELECT p.PolicyNumber\r\nFROM tbl_insurancecompany as c\r\n     INNER JOIN tbl_customer_insurance as p ON c.ID = p.InsuranceCompanyID\r\nWHERE p.ID = :ID\r\n  AND p.PolicyNumber != ''\r\n  AND c.EcsFormat IN ('Region A', 'Region B', 'Region C', 'Region D')", new VB$AnonymousType_0<int>(nullable.Value), null, true, commandTimeout, commandType).ToArray<string>();
                }
            }
            return strArray;
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete order #{this.ObjectID}?";
            messages.DeletedSuccessfully = $"Order #{this.ObjectID} was successfully deleted.";
            messages.ObjectToBeDeletedIsNotFound = $"Cannot delete order #{this.ObjectID}. It has line items.";
            return messages;
        }

        public static string GetQuery()
        {
            QueryOptions options = new QueryOptions();
            options.FilterUserID = null;
            options.FilterLocationID = null;
            options.FilterArchived = YesNoAny.Any;
            options.FilterApproved = YesNoAny.Any;
            options.FilterReoccuring = false;
            options.SearchTerms = null;
            return SearchNavigatorEventsHandler.GetQuery(options);
        }

        protected override void InitDropdowns()
        {
            using (Cache.Batch batch = Cache.BeginBatch())
            {
                batch.InitDropdown(this.cmbCustomer, "tbl_customer", null);
                batch.InitDropdown(this.cmbDoctor, "tbl_doctor", null);
                batch.InitDropdown(this.cmbReferral, "tbl_referral", null);
                batch.InitDropdown(this.cmbSalesrep, "tbl_salesrep", null);
                batch.InitDropdown(this.cmbLocation, "tbl_location", null);
                batch.InitDropdown(this.cmbPosType, "tbl_postype", null);
                batch.InitDropdown(this.cmbFacility, "tbl_facility", null);
                batch.InitDropdown(this.cmbShippingMethod, "tbl_shippingmethod", null);
                batch.InitDropdown(this.eddICD9_1, "tbl_icd9", null);
                batch.InitDropdown(this.eddICD9_2, "tbl_icd9", null);
                batch.InitDropdown(this.eddICD9_3, "tbl_icd9", null);
                batch.InitDropdown(this.eddICD9_4, "tbl_icd9", null);
                batch.InitDropdown(this.eddICD10_01, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_02, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_03, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_04, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_05, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_06, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_07, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_08, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_09, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_10, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_11, "tbl_icd10", null);
                batch.InitDropdown(this.eddICD10_12, "tbl_icd10", null);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormOrder));
            this.Panel2 = new GroupBox();
            this.lnkSurvey = new LinkLabel();
            this.lblOrderNumber = new Label();
            this.lblTakenBy = new Label();
            this.txtTakenBy = new TextBox();
            this.lblDeliveryDate = new Label();
            this.dtbDeliveryDate = new UltraDateTimeEditor();
            this.chbApproved = new CheckBox();
            this.Label1 = new Label();
            this.Label2 = new Label();
            this.dtbBillDate = new UltraDateTimeEditor();
            this.dtbOrderDate = new UltraDateTimeEditor();
            this.lblDoctor = new Label();
            this.cmbDoctor = new Combobox();
            this.lblLocation = new Label();
            this.cmbLocation = new Combobox();
            this.lblPosType = new Label();
            this.cmbPosType = new Combobox();
            this.cmbFacility = new Combobox();
            this.lblFacility = new Label();
            this.cmbCustomerInsurance4 = new ComboBox();
            this.cmbCustomerInsurance3 = new ComboBox();
            this.cmbCustomerInsurance2 = new ComboBox();
            this.cmbCustomerInsurance1 = new ComboBox();
            this.lblCustomerInsurance4 = new Label();
            this.lblCustomerInsurance3 = new Label();
            this.lblCustomerInsurance2 = new Label();
            this.lblCustomerInsurance1 = new Label();
            this.txtPhone = new TextBox();
            this.eddICD9_2 = new ExtendedDropdown();
            this.eddICD9_4 = new ExtendedDropdown();
            this.eddICD9_3 = new ExtendedDropdown();
            this.eddICD9_1 = new ExtendedDropdown();
            this.lblICD9_4 = new Label();
            this.lblICD9_3 = new Label();
            this.lblICD9_2 = new Label();
            this.lblICD9_1 = new Label();
            this.cmbCustomer = new Combobox();
            this.lblPhone = new Label();
            this.caShip = new ControlAddress();
            this.Label5 = new Label();
            this.lblReferral = new Label();
            this.lblSalesrep = new Label();
            this.cmbSalesrep = new Combobox();
            this.cmbReferral = new Combobox();
            this.lblShippingMethod = new Label();
            this.cmbShippingMethod = new Combobox();
            this.TabControl1 = new TabControl();
            this.tpDelivery = new TabPage();
            this.ControlOrderDetails1 = new ControlOrderDetails();
            this.lblOrderDetails = new Label();
            this.tpSpecial = new TabPage();
            this.txtSpecialInstructions = new TextBox();
            this.lblSpecialInstructions = new Label();
            this.tpClaim = new TabPage();
            this.lblClaimNote = new Label();
            this.txtClaimNote = new TextBox();
            this.mnuGotoImages = new MenuItem();
            this.mnuGotoNewImage = new MenuItem();
            this.mnuGotoCompliance = new MenuItem();
            this.mnuActionsCreateInvoice = new MenuItem();
            this.mnuActionsMakeDeposit = new MenuItem();
            this.mnuActionsReorder = new MenuItem();
            this.mnuActionsShipping = new MenuItem();
            this.mnuActionsReloadDropdowns = new MenuItem();
            this.mnuActionsSameOrSimilar = new MenuItem();
            this.mnuFilterArchived_Any = new MenuItem();
            this.mnuFilterArchived_Yes = new MenuItem();
            this.mnuFilterArchived_No = new MenuItem();
            this.mnuFilterSeparator1 = new MenuItem();
            this.mnuFilterApproved_Any = new MenuItem();
            this.mnuFilterApproved_Yes = new MenuItem();
            this.mnuFilterApproved_No = new MenuItem();
            this.mnuFilterSeparator2 = new MenuItem();
            this.mnuFilterReoccuring = new MenuItem();
            this.mnuFilterSeparator3 = new MenuItem();
            this.mnuFilterPagedGrids = new MenuItem();
            this.cmsGridSearch = new ContextMenuStrip(this.components);
            this.tsmiGridSearchApprove = new ToolStripMenuItem();
            this.tsmiGridSearchSeparator = new ToolStripSeparator();
            this.tsmiGridSearchArchive = new ToolStripMenuItem();
            this.tsmiGridSearchUnarchive = new ToolStripMenuItem();
            this.TabControl2 = new TabControl();
            this.tpInfo = new TabPage();
            this.tpICD9 = new TabPage();
            this.tpICD10 = new TabPage();
            this.eddICD10_10 = new ExtendedDropdown();
            this.eddICD10_06 = new ExtendedDropdown();
            this.eddICD10_08 = new ExtendedDropdown();
            this.eddICD10_12 = new ExtendedDropdown();
            this.Label16 = new Label();
            this.eddICD10_07 = new ExtendedDropdown();
            this.Label17 = new Label();
            this.Label18 = new Label();
            this.eddICD10_05 = new ExtendedDropdown();
            this.eddICD10_11 = new ExtendedDropdown();
            this.Label19 = new Label();
            this.Label20 = new Label();
            this.Label21 = new Label();
            this.eddICD10_02 = new ExtendedDropdown();
            this.eddICD10_09 = new ExtendedDropdown();
            this.Label22 = new Label();
            this.eddICD10_04 = new ExtendedDropdown();
            this.Label23 = new Label();
            this.Label24 = new Label();
            this.eddICD10_03 = new ExtendedDropdown();
            this.Label25 = new Label();
            this.eddICD10_01 = new ExtendedDropdown();
            this.Label26 = new Label();
            this.Label27 = new Label();
            this.ImageList1 = new ImageList(this.components);
            this.tpUserFields = new TabPage();
            this.txtUserField2 = new TextBox();
            this.txtUserField1 = new TextBox();
            this.lblUserField2 = new Label();
            this.lblUserField1 = new Label();
            base.tpWorkArea.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tpDelivery.SuspendLayout();
            this.tpSpecial.SuspendLayout();
            this.tpClaim.SuspendLayout();
            this.cmsGridSearch.SuspendLayout();
            this.TabControl2.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpICD9.SuspendLayout();
            this.tpICD10.SuspendLayout();
            this.tpUserFields.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.TabControl2);
            base.tpWorkArea.Controls.Add(this.Panel2);
            base.tpWorkArea.Size = new Size(0x270, 0x25a);
            MenuItem[] items = new MenuItem[] { this.mnuGotoImages, this.mnuGotoNewImage, this.mnuGotoCompliance };
            base.cmnuGoto.MenuItems.AddRange(items);
            MenuItem[] itemArray2 = new MenuItem[] { this.mnuActionsCreateInvoice, this.mnuActionsMakeDeposit, this.mnuActionsReorder, this.mnuActionsShipping, this.mnuActionsReloadDropdowns, this.mnuActionsSameOrSimilar };
            base.cmnuActions.MenuItems.AddRange(itemArray2);
            MenuItem[] itemArray3 = new MenuItem[11];
            itemArray3[0] = this.mnuFilterArchived_Any;
            itemArray3[1] = this.mnuFilterArchived_Yes;
            itemArray3[2] = this.mnuFilterArchived_No;
            itemArray3[3] = this.mnuFilterSeparator1;
            itemArray3[4] = this.mnuFilterApproved_Any;
            itemArray3[5] = this.mnuFilterApproved_Yes;
            itemArray3[6] = this.mnuFilterApproved_No;
            itemArray3[7] = this.mnuFilterSeparator2;
            itemArray3[8] = this.mnuFilterReoccuring;
            itemArray3[9] = this.mnuFilterSeparator3;
            itemArray3[10] = this.mnuFilterPagedGrids;
            base.cmnuFilter.MenuItems.AddRange(itemArray3);
            this.Panel2.Controls.Add(this.lnkSurvey);
            this.Panel2.Controls.Add(this.lblOrderNumber);
            this.Panel2.Controls.Add(this.lblTakenBy);
            this.Panel2.Controls.Add(this.txtTakenBy);
            this.Panel2.Controls.Add(this.lblDeliveryDate);
            this.Panel2.Controls.Add(this.dtbDeliveryDate);
            this.Panel2.Controls.Add(this.chbApproved);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.dtbBillDate);
            this.Panel2.Controls.Add(this.dtbOrderDate);
            this.Panel2.Dock = DockStyle.Top;
            this.Panel2.Location = new Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new Size(0x270, 0x40);
            this.Panel2.TabIndex = 0;
            this.Panel2.TabStop = false;
            this.lnkSurvey.Location = new Point(0x200, 8);
            this.lnkSurvey.Name = "lnkSurvey";
            this.lnkSurvey.Size = new Size(60, 0x15);
            this.lnkSurvey.TabIndex = 13;
            this.lnkSurvey.TabStop = true;
            this.lnkSurvey.Text = "Survey";
            this.lnkSurvey.TextAlign = ContentAlignment.MiddleCenter;
            this.lblOrderNumber.BorderStyle = BorderStyle.FixedSingle;
            this.lblOrderNumber.Location = new Point(0x180, 8);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new Size(100, 0x15);
            this.lblOrderNumber.TabIndex = 10;
            this.lblOrderNumber.Text = "Order #";
            this.lblOrderNumber.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTakenBy.Location = new Point(0xc0, 8);
            this.lblTakenBy.Name = "lblTakenBy";
            this.lblTakenBy.Size = new Size(0x48, 0x15);
            this.lblTakenBy.TabIndex = 6;
            this.lblTakenBy.Text = "Taken By";
            this.lblTakenBy.TextAlign = ContentAlignment.MiddleRight;
            this.txtTakenBy.Location = new Point(0x110, 8);
            this.txtTakenBy.Name = "txtTakenBy";
            this.txtTakenBy.Size = new Size(0x60, 20);
            this.txtTakenBy.TabIndex = 7;
            this.lblDeliveryDate.Location = new Point(0xc0, 0x20);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new Size(0x48, 0x15);
            this.lblDeliveryDate.TabIndex = 8;
            this.lblDeliveryDate.Text = "Delivery Date";
            this.lblDeliveryDate.TextAlign = ContentAlignment.MiddleRight;
            this.dtbDeliveryDate.Location = new Point(0x110, 0x20);
            this.dtbDeliveryDate.Name = "dtbDeliveryDate";
            this.dtbDeliveryDate.Size = new Size(0x60, 0x15);
            this.dtbDeliveryDate.TabIndex = 9;
            this.chbApproved.Location = new Point(0x180, 0x20);
            this.chbApproved.Name = "chbApproved";
            this.chbApproved.Size = new Size(100, 0x15);
            this.chbApproved.TabIndex = 12;
            this.chbApproved.Text = "Approved";
            this.Label1.Location = new Point(8, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x40, 0x15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Order date";
            this.Label1.TextAlign = ContentAlignment.MiddleRight;
            this.Label2.Location = new Point(8, 0x20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x40, 0x15);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Bill date";
            this.Label2.TextAlign = ContentAlignment.MiddleRight;
            this.dtbBillDate.Location = new Point(80, 0x20);
            this.dtbBillDate.Name = "dtbBillDate";
            this.dtbBillDate.Size = new Size(0x60, 0x15);
            this.dtbBillDate.TabIndex = 3;
            this.dtbOrderDate.Location = new Point(80, 8);
            this.dtbOrderDate.Name = "dtbOrderDate";
            this.dtbOrderDate.Size = new Size(0x60, 0x15);
            this.dtbOrderDate.TabIndex = 1;
            this.lblDoctor.Location = new Point(0x138, 0x98);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new Size(0x38, 0x15);
            this.lblDoctor.TabIndex = 5;
            this.lblDoctor.Text = "Doctor";
            this.lblDoctor.TextAlign = ContentAlignment.MiddleRight;
            this.cmbDoctor.Location = new Point(0x178, 0x98);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new Size(0xe0, 0x15);
            this.cmbDoctor.TabIndex = 6;
            this.lblLocation.Location = new Point(0x138, 8);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new Size(0x38, 0x15);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = ContentAlignment.MiddleRight;
            this.cmbLocation.Location = new Point(0x178, 8);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new Size(0xe0, 0x15);
            this.cmbLocation.TabIndex = 8;
            this.lblPosType.Location = new Point(0x138, 0x20);
            this.lblPosType.Name = "lblPosType";
            this.lblPosType.Size = new Size(0x38, 0x15);
            this.lblPosType.TabIndex = 9;
            this.lblPosType.Text = "POS Type";
            this.lblPosType.TextAlign = ContentAlignment.MiddleRight;
            this.cmbPosType.Location = new Point(0x178, 0x20);
            this.cmbPosType.Name = "cmbPosType";
            this.cmbPosType.Size = new Size(0xe0, 0x15);
            this.cmbPosType.TabIndex = 10;
            this.cmbFacility.Location = new Point(0x178, 0x38);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new Size(0xe0, 0x15);
            this.cmbFacility.TabIndex = 12;
            this.lblFacility.Location = new Point(0x138, 0x38);
            this.lblFacility.Name = "lblFacility";
            this.lblFacility.Size = new Size(0x38, 0x15);
            this.lblFacility.TabIndex = 11;
            this.lblFacility.Text = "Facility";
            this.lblFacility.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomerInsurance4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance4.Location = new Point(80, 0xd0);
            this.cmbCustomerInsurance4.Name = "cmbCustomerInsurance4";
            this.cmbCustomerInsurance4.Size = new Size(0xe0, 0x15);
            this.cmbCustomerInsurance4.TabIndex = 0x1a;
            this.cmbCustomerInsurance3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance3.Location = new Point(80, 0xb8);
            this.cmbCustomerInsurance3.Name = "cmbCustomerInsurance3";
            this.cmbCustomerInsurance3.Size = new Size(0xe0, 0x15);
            this.cmbCustomerInsurance3.TabIndex = 0x18;
            this.cmbCustomerInsurance2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance2.Location = new Point(80, 160);
            this.cmbCustomerInsurance2.Name = "cmbCustomerInsurance2";
            this.cmbCustomerInsurance2.Size = new Size(0xe0, 0x15);
            this.cmbCustomerInsurance2.TabIndex = 0x16;
            this.cmbCustomerInsurance1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomerInsurance1.Location = new Point(80, 0x88);
            this.cmbCustomerInsurance1.Name = "cmbCustomerInsurance1";
            this.cmbCustomerInsurance1.Size = new Size(0xe0, 0x15);
            this.cmbCustomerInsurance1.TabIndex = 20;
            this.lblCustomerInsurance4.Location = new Point(0x10, 0xd0);
            this.lblCustomerInsurance4.Name = "lblCustomerInsurance4";
            this.lblCustomerInsurance4.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance4.TabIndex = 0x19;
            this.lblCustomerInsurance4.Text = "Policy 4";
            this.lblCustomerInsurance4.TextAlign = ContentAlignment.MiddleRight;
            this.lblCustomerInsurance3.Location = new Point(0x10, 0xb8);
            this.lblCustomerInsurance3.Name = "lblCustomerInsurance3";
            this.lblCustomerInsurance3.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance3.TabIndex = 0x17;
            this.lblCustomerInsurance3.Text = "Policy 3";
            this.lblCustomerInsurance3.TextAlign = ContentAlignment.MiddleRight;
            this.lblCustomerInsurance2.Location = new Point(0x10, 160);
            this.lblCustomerInsurance2.Name = "lblCustomerInsurance2";
            this.lblCustomerInsurance2.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance2.TabIndex = 0x15;
            this.lblCustomerInsurance2.Text = "Policy 2";
            this.lblCustomerInsurance2.TextAlign = ContentAlignment.MiddleRight;
            this.lblCustomerInsurance1.Location = new Point(0x10, 0x88);
            this.lblCustomerInsurance1.Name = "lblCustomerInsurance1";
            this.lblCustomerInsurance1.Size = new Size(0x38, 0x15);
            this.lblCustomerInsurance1.TabIndex = 0x13;
            this.lblCustomerInsurance1.Text = "Policy 1";
            this.lblCustomerInsurance1.TextAlign = ContentAlignment.MiddleRight;
            this.txtPhone.Location = new Point(80, 0x68);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(0xe0, 20);
            this.txtPhone.TabIndex = 4;
            this.eddICD9_2.Location = new Point(0x48, 0x20);
            this.eddICD9_2.Name = "eddICD9_2";
            this.eddICD9_2.Size = new Size(0xb8, 0x15);
            this.eddICD9_2.TabIndex = 30;
            this.eddICD9_2.TextMember = "";
            this.eddICD9_4.Location = new Point(0x48, 80);
            this.eddICD9_4.Name = "eddICD9_4";
            this.eddICD9_4.Size = new Size(0xb8, 0x15);
            this.eddICD9_4.TabIndex = 0x22;
            this.eddICD9_4.TextMember = "";
            this.eddICD9_3.Location = new Point(0x48, 0x38);
            this.eddICD9_3.Name = "eddICD9_3";
            this.eddICD9_3.Size = new Size(0xb8, 0x15);
            this.eddICD9_3.TabIndex = 0x20;
            this.eddICD9_3.TextMember = "";
            this.eddICD9_1.Location = new Point(0x48, 8);
            this.eddICD9_1.Name = "eddICD9_1";
            this.eddICD9_1.Size = new Size(0xb8, 0x15);
            this.eddICD9_1.TabIndex = 0x1c;
            this.eddICD9_1.TextMember = "";
            this.lblICD9_4.Location = new Point(8, 80);
            this.lblICD9_4.Name = "lblICD9_4";
            this.lblICD9_4.Size = new Size(0x38, 0x15);
            this.lblICD9_4.TabIndex = 0x21;
            this.lblICD9_4.Text = "ICD9 4";
            this.lblICD9_4.TextAlign = ContentAlignment.MiddleRight;
            this.lblICD9_3.Location = new Point(8, 0x38);
            this.lblICD9_3.Name = "lblICD9_3";
            this.lblICD9_3.Size = new Size(0x38, 0x15);
            this.lblICD9_3.TabIndex = 0x1f;
            this.lblICD9_3.Text = "ICD9 3";
            this.lblICD9_3.TextAlign = ContentAlignment.MiddleRight;
            this.lblICD9_2.Location = new Point(8, 0x20);
            this.lblICD9_2.Name = "lblICD9_2";
            this.lblICD9_2.Size = new Size(0x38, 0x15);
            this.lblICD9_2.TabIndex = 0x1d;
            this.lblICD9_2.Text = "ICD9 2";
            this.lblICD9_2.TextAlign = ContentAlignment.MiddleRight;
            this.lblICD9_1.Location = new Point(8, 8);
            this.lblICD9_1.Name = "lblICD9_1";
            this.lblICD9_1.Size = new Size(0x38, 0x15);
            this.lblICD9_1.TabIndex = 0x1b;
            this.lblICD9_1.Text = "ICD9 1";
            this.lblICD9_1.TextAlign = ContentAlignment.MiddleRight;
            this.cmbCustomer.Location = new Point(80, 8);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new Size(0xe0, 0x15);
            this.cmbCustomer.TabIndex = 1;
            this.lblPhone.Location = new Point(8, 0x68);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new Size(0x40, 0x15);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Phone";
            this.lblPhone.TextAlign = ContentAlignment.MiddleRight;
            this.caShip.Location = new Point(8, 0x20);
            this.caShip.Name = "caShip";
            this.caShip.Size = new Size(0x128, 0x48);
            this.caShip.TabIndex = 2;
            this.Label5.Location = new Point(8, 8);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x40, 0x15);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Customer";
            this.Label5.TextAlign = ContentAlignment.MiddleRight;
            this.lblReferral.Location = new Point(0x138, 80);
            this.lblReferral.Name = "lblReferral";
            this.lblReferral.Size = new Size(0x38, 0x15);
            this.lblReferral.TabIndex = 13;
            this.lblReferral.Text = "Referral";
            this.lblReferral.TextAlign = ContentAlignment.MiddleRight;
            this.lblSalesrep.Location = new Point(0x138, 0x68);
            this.lblSalesrep.Name = "lblSalesrep";
            this.lblSalesrep.Size = new Size(0x38, 0x15);
            this.lblSalesrep.TabIndex = 15;
            this.lblSalesrep.Text = "Sales rep";
            this.lblSalesrep.TextAlign = ContentAlignment.MiddleRight;
            this.cmbSalesrep.Location = new Point(0x178, 0x68);
            this.cmbSalesrep.Name = "cmbSalesrep";
            this.cmbSalesrep.Size = new Size(0xe0, 0x15);
            this.cmbSalesrep.TabIndex = 0x10;
            this.cmbReferral.Location = new Point(0x178, 80);
            this.cmbReferral.Name = "cmbReferral";
            this.cmbReferral.Size = new Size(0xe0, 0x15);
            this.cmbReferral.TabIndex = 14;
            this.lblShippingMethod.Location = new Point(0x138, 0x80);
            this.lblShippingMethod.Name = "lblShippingMethod";
            this.lblShippingMethod.Size = new Size(0x38, 0x15);
            this.lblShippingMethod.TabIndex = 0x11;
            this.lblShippingMethod.Text = "Shipping";
            this.lblShippingMethod.TextAlign = ContentAlignment.MiddleRight;
            this.cmbShippingMethod.Location = new Point(0x178, 0x80);
            this.cmbShippingMethod.Name = "cmbShippingMethod";
            this.cmbShippingMethod.Size = new Size(0xe0, 0x15);
            this.cmbShippingMethod.TabIndex = 0x12;
            this.TabControl1.Controls.Add(this.tpDelivery);
            this.TabControl1.Controls.Add(this.tpSpecial);
            this.TabControl1.Controls.Add(this.tpClaim);
            this.TabControl1.Controls.Add(this.tpUserFields);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.Location = new Point(0, 0x148);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x270, 0x112);
            this.TabControl1.TabIndex = 2;
            this.tpDelivery.Controls.Add(this.ControlOrderDetails1);
            this.tpDelivery.Controls.Add(this.lblOrderDetails);
            this.tpDelivery.Location = new Point(4, 0x16);
            this.tpDelivery.Name = "tpDelivery";
            this.tpDelivery.Padding = new Padding(3);
            this.tpDelivery.Size = new Size(0x268, 0xf8);
            this.tpDelivery.TabIndex = 0;
            this.tpDelivery.Text = "Delivery";
            this.tpDelivery.UseVisualStyleBackColor = true;
            this.ControlOrderDetails1.Dock = DockStyle.Fill;
            this.ControlOrderDetails1.Location = new Point(3, 3);
            this.ControlOrderDetails1.Name = "ControlOrderDetails1";
            this.ControlOrderDetails1.Size = new Size(610, 0xf2);
            this.ControlOrderDetails1.TabIndex = 0;
            this.lblOrderDetails.Dock = DockStyle.Fill;
            this.lblOrderDetails.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.lblOrderDetails.Location = new Point(3, 3);
            this.lblOrderDetails.Name = "lblOrderDetails";
            this.lblOrderDetails.Size = new Size(610, 0xf2);
            this.lblOrderDetails.TabIndex = 0x18;
            this.lblOrderDetails.Text = "Save order prior to adding details";
            this.lblOrderDetails.TextAlign = ContentAlignment.MiddleCenter;
            this.lblOrderDetails.Visible = false;
            this.tpSpecial.Controls.Add(this.txtSpecialInstructions);
            this.tpSpecial.Controls.Add(this.lblSpecialInstructions);
            this.tpSpecial.Location = new Point(4, 0x16);
            this.tpSpecial.Name = "tpSpecial";
            this.tpSpecial.Padding = new Padding(3);
            this.tpSpecial.Size = new Size(0x268, 0xf8);
            this.tpSpecial.TabIndex = 1;
            this.tpSpecial.Text = "Special";
            this.tpSpecial.UseVisualStyleBackColor = true;
            this.txtSpecialInstructions.Dock = DockStyle.Fill;
            this.txtSpecialInstructions.Location = new Point(3, 0x1a);
            this.txtSpecialInstructions.Multiline = true;
            this.txtSpecialInstructions.Name = "txtSpecialInstructions";
            this.txtSpecialInstructions.ScrollBars = ScrollBars.Both;
            this.txtSpecialInstructions.Size = new Size(610, 0xdb);
            this.txtSpecialInstructions.TabIndex = 1;
            this.lblSpecialInstructions.Dock = DockStyle.Top;
            this.lblSpecialInstructions.Location = new Point(3, 3);
            this.lblSpecialInstructions.Name = "lblSpecialInstructions";
            this.lblSpecialInstructions.Size = new Size(610, 0x17);
            this.lblSpecialInstructions.TabIndex = 0;
            this.lblSpecialInstructions.Text = "Special Instructions";
            this.tpClaim.Controls.Add(this.lblClaimNote);
            this.tpClaim.Controls.Add(this.txtClaimNote);
            this.tpClaim.Location = new Point(4, 0x16);
            this.tpClaim.Name = "tpClaim";
            this.tpClaim.Padding = new Padding(3);
            this.tpClaim.Size = new Size(0x268, 0xf8);
            this.tpClaim.TabIndex = 2;
            this.tpClaim.Text = "Claim";
            this.tpClaim.UseVisualStyleBackColor = true;
            this.lblClaimNote.Location = new Point(8, 8);
            this.lblClaimNote.Name = "lblClaimNote";
            this.lblClaimNote.Size = new Size(0x30, 0x15);
            this.lblClaimNote.TabIndex = 0;
            this.lblClaimNote.Text = "Note";
            this.lblClaimNote.TextAlign = ContentAlignment.MiddleRight;
            this.txtClaimNote.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtClaimNote.Location = new Point(0x40, 8);
            this.txtClaimNote.MaxLength = 80;
            this.txtClaimNote.Name = "txtClaimNote";
            this.txtClaimNote.Size = new Size(0x220, 20);
            this.txtClaimNote.TabIndex = 1;
            this.mnuGotoImages.Index = 0;
            this.mnuGotoImages.Text = "Images";
            this.mnuGotoNewImage.Index = 1;
            this.mnuGotoNewImage.Text = "New Image";
            this.mnuGotoCompliance.Index = 2;
            this.mnuGotoCompliance.Text = "Compliance";
            this.mnuActionsCreateInvoice.Index = 0;
            this.mnuActionsCreateInvoice.Text = "Create Invoice";
            this.mnuActionsMakeDeposit.Index = 1;
            this.mnuActionsMakeDeposit.Text = "Make Deposit";
            this.mnuActionsReorder.Index = 2;
            this.mnuActionsReorder.Text = "Reorder";
            this.mnuActionsShipping.Index = 3;
            this.mnuActionsShipping.Text = "Shipping";
            this.mnuActionsReloadDropdowns.Index = 4;
            this.mnuActionsReloadDropdowns.Text = "Reload Dropdowns";
            this.mnuActionsSameOrSimilar.Index = 5;
            this.mnuActionsSameOrSimilar.Text = "Same Or Similar";
            this.mnuFilterArchived_Any.Index = 0;
            this.mnuFilterArchived_Any.Text = "Archived : Any";
            this.mnuFilterArchived_Yes.Index = 1;
            this.mnuFilterArchived_Yes.Text = "Archived : Yes";
            this.mnuFilterArchived_No.Checked = true;
            this.mnuFilterArchived_No.Index = 2;
            this.mnuFilterArchived_No.Text = "Archived : No";
            this.mnuFilterSeparator1.Index = 3;
            this.mnuFilterSeparator1.Text = "-";
            this.mnuFilterApproved_Any.Checked = true;
            this.mnuFilterApproved_Any.Index = 4;
            this.mnuFilterApproved_Any.Text = "Approved : Any";
            this.mnuFilterApproved_Yes.Index = 5;
            this.mnuFilterApproved_Yes.Text = "Approved : Yes";
            this.mnuFilterApproved_No.Index = 6;
            this.mnuFilterApproved_No.Text = "Approved : No";
            this.mnuFilterSeparator2.Index = 7;
            this.mnuFilterSeparator2.Text = "-";
            this.mnuFilterReoccuring.Index = 8;
            this.mnuFilterReoccuring.Text = "Reoccuring";
            this.mnuFilterSeparator3.Index = 9;
            this.mnuFilterSeparator3.Text = "-";
            this.mnuFilterPagedGrids.Index = 10;
            this.mnuFilterPagedGrids.Text = "Paged Grids";
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiGridSearchApprove, this.tsmiGridSearchSeparator, this.tsmiGridSearchArchive, this.tsmiGridSearchUnarchive };
            this.cmsGridSearch.Items.AddRange(toolStripItems);
            this.cmsGridSearch.Name = "cmsGridSearch";
            this.cmsGridSearch.Size = new Size(0x84, 0x4c);
            this.tsmiGridSearchApprove.Name = "tsmiGridSearchApprove";
            this.tsmiGridSearchApprove.Size = new Size(0x83, 0x16);
            this.tsmiGridSearchApprove.Text = "Approve ...";
            this.tsmiGridSearchSeparator.Name = "tsmiGridSearchSeparator";
            this.tsmiGridSearchSeparator.Size = new Size(0x80, 6);
            this.tsmiGridSearchArchive.Name = "tsmiGridSearchArchive";
            this.tsmiGridSearchArchive.Size = new Size(0x83, 0x16);
            this.tsmiGridSearchArchive.Text = "Archive";
            this.tsmiGridSearchUnarchive.Name = "tsmiGridSearchUnarchive";
            this.tsmiGridSearchUnarchive.Size = new Size(0x83, 0x16);
            this.tsmiGridSearchUnarchive.Text = "Unarchive";
            this.TabControl2.Controls.Add(this.tpInfo);
            this.TabControl2.Controls.Add(this.tpICD9);
            this.TabControl2.Controls.Add(this.tpICD10);
            this.TabControl2.Dock = DockStyle.Top;
            this.TabControl2.ImageList = this.ImageList1;
            this.TabControl2.Location = new Point(0, 0x40);
            this.TabControl2.Name = "TabControl2";
            this.TabControl2.SelectedIndex = 0;
            this.TabControl2.Size = new Size(0x270, 0x108);
            this.TabControl2.TabIndex = 1;
            this.tpInfo.Controls.Add(this.lblLocation);
            this.tpInfo.Controls.Add(this.cmbLocation);
            this.tpInfo.Controls.Add(this.Label5);
            this.tpInfo.Controls.Add(this.lblPosType);
            this.tpInfo.Controls.Add(this.lblDoctor);
            this.tpInfo.Controls.Add(this.cmbPosType);
            this.tpInfo.Controls.Add(this.caShip);
            this.tpInfo.Controls.Add(this.cmbFacility);
            this.tpInfo.Controls.Add(this.cmbDoctor);
            this.tpInfo.Controls.Add(this.lblFacility);
            this.tpInfo.Controls.Add(this.lblPhone);
            this.tpInfo.Controls.Add(this.lblReferral);
            this.tpInfo.Controls.Add(this.cmbCustomer);
            this.tpInfo.Controls.Add(this.lblSalesrep);
            this.tpInfo.Controls.Add(this.txtPhone);
            this.tpInfo.Controls.Add(this.cmbSalesrep);
            this.tpInfo.Controls.Add(this.lblCustomerInsurance1);
            this.tpInfo.Controls.Add(this.cmbReferral);
            this.tpInfo.Controls.Add(this.lblCustomerInsurance2);
            this.tpInfo.Controls.Add(this.lblShippingMethod);
            this.tpInfo.Controls.Add(this.lblCustomerInsurance3);
            this.tpInfo.Controls.Add(this.cmbShippingMethod);
            this.tpInfo.Controls.Add(this.lblCustomerInsurance4);
            this.tpInfo.Controls.Add(this.cmbCustomerInsurance4);
            this.tpInfo.Controls.Add(this.cmbCustomerInsurance1);
            this.tpInfo.Controls.Add(this.cmbCustomerInsurance3);
            this.tpInfo.Controls.Add(this.cmbCustomerInsurance2);
            this.tpInfo.Location = new Point(4, 0x17);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Size = new Size(0x268, 0xed);
            this.tpInfo.TabIndex = 0;
            this.tpInfo.Text = "Info";
            this.tpInfo.UseVisualStyleBackColor = true;
            this.tpICD9.Controls.Add(this.lblICD9_1);
            this.tpICD9.Controls.Add(this.lblICD9_2);
            this.tpICD9.Controls.Add(this.lblICD9_3);
            this.tpICD9.Controls.Add(this.lblICD9_4);
            this.tpICD9.Controls.Add(this.eddICD9_1);
            this.tpICD9.Controls.Add(this.eddICD9_3);
            this.tpICD9.Controls.Add(this.eddICD9_4);
            this.tpICD9.Controls.Add(this.eddICD9_2);
            this.tpICD9.Location = new Point(4, 0x17);
            this.tpICD9.Name = "tpICD9";
            this.tpICD9.Size = new Size(0x268, 0xed);
            this.tpICD9.TabIndex = 1;
            this.tpICD9.Text = "ICD 9";
            this.tpICD9.UseVisualStyleBackColor = true;
            this.tpICD10.Controls.Add(this.eddICD10_10);
            this.tpICD10.Controls.Add(this.eddICD10_06);
            this.tpICD10.Controls.Add(this.eddICD10_08);
            this.tpICD10.Controls.Add(this.eddICD10_12);
            this.tpICD10.Controls.Add(this.Label16);
            this.tpICD10.Controls.Add(this.eddICD10_07);
            this.tpICD10.Controls.Add(this.Label17);
            this.tpICD10.Controls.Add(this.Label18);
            this.tpICD10.Controls.Add(this.eddICD10_05);
            this.tpICD10.Controls.Add(this.eddICD10_11);
            this.tpICD10.Controls.Add(this.Label19);
            this.tpICD10.Controls.Add(this.Label20);
            this.tpICD10.Controls.Add(this.Label21);
            this.tpICD10.Controls.Add(this.eddICD10_02);
            this.tpICD10.Controls.Add(this.eddICD10_09);
            this.tpICD10.Controls.Add(this.Label22);
            this.tpICD10.Controls.Add(this.eddICD10_04);
            this.tpICD10.Controls.Add(this.Label23);
            this.tpICD10.Controls.Add(this.Label24);
            this.tpICD10.Controls.Add(this.eddICD10_03);
            this.tpICD10.Controls.Add(this.Label25);
            this.tpICD10.Controls.Add(this.eddICD10_01);
            this.tpICD10.Controls.Add(this.Label26);
            this.tpICD10.Controls.Add(this.Label27);
            this.tpICD10.Location = new Point(4, 0x17);
            this.tpICD10.Name = "tpICD10";
            this.tpICD10.Padding = new Padding(3);
            this.tpICD10.Size = new Size(0x268, 0xed);
            this.tpICD10.TabIndex = 2;
            this.tpICD10.Text = "ICD 10";
            this.tpICD10.UseVisualStyleBackColor = true;
            this.eddICD10_10.Location = new Point(360, 80);
            this.eddICD10_10.Name = "eddICD10_10";
            this.eddICD10_10.Size = new Size(200, 0x15);
            this.eddICD10_10.TabIndex = 0x2b;
            this.eddICD10_10.TextMember = "";
            this.eddICD10_06.Location = new Point(0x38, 0x80);
            this.eddICD10_06.Name = "eddICD10_06";
            this.eddICD10_06.Size = new Size(200, 0x15);
            this.eddICD10_06.TabIndex = 0x23;
            this.eddICD10_06.TextMember = "";
            this.eddICD10_08.Location = new Point(360, 0x20);
            this.eddICD10_08.Name = "eddICD10_08";
            this.eddICD10_08.Size = new Size(200, 0x15);
            this.eddICD10_08.TabIndex = 0x27;
            this.eddICD10_08.TextMember = "";
            this.eddICD10_12.Location = new Point(360, 0x80);
            this.eddICD10_12.Name = "eddICD10_12";
            this.eddICD10_12.Size = new Size(200, 0x15);
            this.eddICD10_12.TabIndex = 0x2f;
            this.eddICD10_12.TextMember = "";
            this.Label16.Location = new Point(8, 0x68);
            this.Label16.Name = "Label16";
            this.Label16.Size = new Size(0x20, 20);
            this.Label16.TabIndex = 0x20;
            this.Label16.Text = "# 5";
            this.Label16.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_07.Location = new Point(360, 8);
            this.eddICD10_07.Name = "eddICD10_07";
            this.eddICD10_07.Size = new Size(200, 0x15);
            this.eddICD10_07.TabIndex = 0x25;
            this.eddICD10_07.TextMember = "";
            this.Label17.Location = new Point(0x138, 0x38);
            this.Label17.Name = "Label17";
            this.Label17.Size = new Size(0x20, 20);
            this.Label17.TabIndex = 40;
            this.Label17.Text = "# 9";
            this.Label17.TextAlign = ContentAlignment.MiddleRight;
            this.Label18.Location = new Point(8, 0x80);
            this.Label18.Name = "Label18";
            this.Label18.Size = new Size(0x20, 20);
            this.Label18.TabIndex = 0x22;
            this.Label18.Text = "# 6";
            this.Label18.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_05.Location = new Point(0x38, 0x68);
            this.eddICD10_05.Name = "eddICD10_05";
            this.eddICD10_05.Size = new Size(200, 0x15);
            this.eddICD10_05.TabIndex = 0x21;
            this.eddICD10_05.TextMember = "";
            this.eddICD10_11.Location = new Point(360, 0x68);
            this.eddICD10_11.Name = "eddICD10_11";
            this.eddICD10_11.Size = new Size(200, 0x15);
            this.eddICD10_11.TabIndex = 0x2d;
            this.eddICD10_11.TextMember = "";
            this.Label19.Location = new Point(0x138, 8);
            this.Label19.Name = "Label19";
            this.Label19.Size = new Size(0x20, 20);
            this.Label19.TabIndex = 0x24;
            this.Label19.Text = "# 7";
            this.Label19.TextAlign = ContentAlignment.MiddleRight;
            this.Label20.Location = new Point(0x138, 0x20);
            this.Label20.Name = "Label20";
            this.Label20.Size = new Size(0x20, 20);
            this.Label20.TabIndex = 0x26;
            this.Label20.Text = "# 8";
            this.Label20.TextAlign = ContentAlignment.MiddleRight;
            this.Label21.Location = new Point(0x138, 80);
            this.Label21.Name = "Label21";
            this.Label21.Size = new Size(0x20, 20);
            this.Label21.TabIndex = 0x2a;
            this.Label21.Text = "# 10";
            this.Label21.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_02.Location = new Point(0x38, 0x20);
            this.eddICD10_02.Name = "eddICD10_02";
            this.eddICD10_02.Size = new Size(200, 0x15);
            this.eddICD10_02.TabIndex = 0x1b;
            this.eddICD10_02.TextMember = "";
            this.eddICD10_09.Location = new Point(360, 0x38);
            this.eddICD10_09.Name = "eddICD10_09";
            this.eddICD10_09.Size = new Size(200, 0x15);
            this.eddICD10_09.TabIndex = 0x29;
            this.eddICD10_09.TextMember = "";
            this.Label22.Location = new Point(0x138, 0x80);
            this.Label22.Name = "Label22";
            this.Label22.Size = new Size(0x20, 20);
            this.Label22.TabIndex = 0x2e;
            this.Label22.Text = "# 12";
            this.Label22.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_04.Location = new Point(0x38, 80);
            this.eddICD10_04.Name = "eddICD10_04";
            this.eddICD10_04.Size = new Size(200, 0x15);
            this.eddICD10_04.TabIndex = 0x1f;
            this.eddICD10_04.TextMember = "";
            this.Label23.Location = new Point(0x138, 0x68);
            this.Label23.Name = "Label23";
            this.Label23.Size = new Size(0x20, 20);
            this.Label23.TabIndex = 0x2c;
            this.Label23.Text = "# 11";
            this.Label23.TextAlign = ContentAlignment.MiddleRight;
            this.Label24.Location = new Point(8, 8);
            this.Label24.Name = "Label24";
            this.Label24.Size = new Size(0x20, 20);
            this.Label24.TabIndex = 0x18;
            this.Label24.Text = "# 1";
            this.Label24.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_03.Location = new Point(0x38, 0x38);
            this.eddICD10_03.Name = "eddICD10_03";
            this.eddICD10_03.Size = new Size(200, 0x15);
            this.eddICD10_03.TabIndex = 0x1d;
            this.eddICD10_03.TextMember = "";
            this.Label25.Location = new Point(8, 0x20);
            this.Label25.Name = "Label25";
            this.Label25.Size = new Size(0x20, 20);
            this.Label25.TabIndex = 0x1a;
            this.Label25.Text = "# 2";
            this.Label25.TextAlign = ContentAlignment.MiddleRight;
            this.eddICD10_01.Location = new Point(0x38, 8);
            this.eddICD10_01.Name = "eddICD10_01";
            this.eddICD10_01.Size = new Size(200, 0x15);
            this.eddICD10_01.TabIndex = 0x19;
            this.eddICD10_01.TextMember = "";
            this.Label26.Location = new Point(8, 0x38);
            this.Label26.Name = "Label26";
            this.Label26.Size = new Size(0x20, 20);
            this.Label26.TabIndex = 0x1c;
            this.Label26.Text = "# 3";
            this.Label26.TextAlign = ContentAlignment.MiddleRight;
            this.Label27.Location = new Point(8, 80);
            this.Label27.Name = "Label27";
            this.Label27.Size = new Size(0x20, 20);
            this.Label27.TabIndex = 30;
            this.Label27.Text = "# 4";
            this.Label27.TextAlign = ContentAlignment.MiddleRight;
            this.ImageList1.ImageStream = (ImageListStreamer) manager.GetObject("ImageList1.ImageStream");
            this.ImageList1.TransparentColor = Color.Magenta;
            this.ImageList1.Images.SetKeyName(0, "");
            this.tpUserFields.Controls.Add(this.txtUserField2);
            this.tpUserFields.Controls.Add(this.txtUserField1);
            this.tpUserFields.Controls.Add(this.lblUserField2);
            this.tpUserFields.Controls.Add(this.lblUserField1);
            this.tpUserFields.Location = new Point(4, 0x16);
            this.tpUserFields.Name = "tpUserFields";
            this.tpUserFields.Padding = new Padding(3);
            this.tpUserFields.Size = new Size(0x268, 0xf8);
            this.tpUserFields.TabIndex = 3;
            this.tpUserFields.Text = "User";
            this.tpUserFields.UseVisualStyleBackColor = true;
            this.txtUserField2.Location = new Point(0x40, 0x20);
            this.txtUserField2.MaxLength = 100;
            this.txtUserField2.Name = "txtUserField2";
            this.txtUserField2.Size = new Size(0x218, 20);
            this.txtUserField2.TabIndex = 7;
            this.txtUserField1.Location = new Point(0x40, 8);
            this.txtUserField1.MaxLength = 100;
            this.txtUserField1.Name = "txtUserField1";
            this.txtUserField1.Size = new Size(0x218, 20);
            this.txtUserField1.TabIndex = 5;
            this.lblUserField2.Location = new Point(8, 0x20);
            this.lblUserField2.Name = "lblUserField2";
            this.lblUserField2.Size = new Size(0x30, 0x15);
            this.lblUserField2.TabIndex = 6;
            this.lblUserField2.Text = "Field #2";
            this.lblUserField2.TextAlign = ContentAlignment.MiddleRight;
            this.lblUserField1.Location = new Point(8, 8);
            this.lblUserField1.Name = "lblUserField1";
            this.lblUserField1.Size = new Size(0x30, 0x15);
            this.lblUserField1.TabIndex = 4;
            this.lblUserField1.Text = "Field #1";
            this.lblUserField1.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x278, 0x2a5);
            base.Name = "FormOrder";
            this.Text = "Form Order";
            base.tpWorkArea.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.tpDelivery.ResumeLayout(false);
            this.tpSpecial.ResumeLayout(false);
            this.tpSpecial.PerformLayout();
            this.tpClaim.ResumeLayout(false);
            this.tpClaim.PerformLayout();
            this.cmsGridSearch.ResumeLayout(false);
            this.TabControl2.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpInfo.PerformLayout();
            this.tpICD9.ResumeLayout(false);
            this.tpICD10.ResumeLayout(false);
            this.tpUserFields.ResumeLayout(false);
            this.tpUserFields.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu {
                MenuItems = { new MenuItem("Order", new EventHandler(this.mnuPrintOrder_Click)) }
            };
            Cache.AddCategory(menu, "Order", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        private void InternalLoadCustomerInsurance(CustomerInsuranceTable table)
        {
            this.cmbCustomerInsurance1.DataSource = new DataView(table);
            this.cmbCustomerInsurance1.ValueMember = "ID";
            this.cmbCustomerInsurance1.DisplayMember = "Name";
            this.cmbCustomerInsurance2.DataSource = new DataView(table);
            this.cmbCustomerInsurance2.ValueMember = "ID";
            this.cmbCustomerInsurance2.DisplayMember = "Name";
            this.cmbCustomerInsurance3.DataSource = new DataView(table);
            this.cmbCustomerInsurance3.ValueMember = "ID";
            this.cmbCustomerInsurance3.DisplayMember = "Name";
            this.cmbCustomerInsurance4.DataSource = new DataView(table);
            this.cmbCustomerInsurance4.ValueMember = "ID";
            this.cmbCustomerInsurance4.DisplayMember = "Name";
        }

        private void lnkSurvey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Uri companyImagingUri = Globals.CompanyImagingUri;
                if (companyImagingUri != null)
                {
                    int? nullable = NullableConvert.ToInt32(this.ObjectID);
                    if (nullable != null)
                    {
                        ClassGlobalObjects.ShowForm(FormFactories.FormBrowser(new Uri(companyImagingUri, $"Survey.aspx?company={HttpUtility.UrlEncode(Globals.CompanyDatabase)}&orderid={nullable.Value}"), null));
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

        private void LoadCustomer(int CustomerID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT\r\n  IF(ShipActive, ShipAddress1, Address1) as Address1\r\n, IF(ShipActive, ShipAddress2, Address2) as Address2\r\n, IF(ShipActive, ShipCity,     City    ) as City\r\n, IF(ShipActive, ShipState,    State   ) as State\r\n, IF(ShipActive, ShipZip,      Zip     ) as Zip\r\n, Phone\r\n, ICD9_1\r\n, ICD9_2\r\n, ICD9_3\r\n, ICD9_4\r\n, ICD10_01\r\n, ICD10_02\r\n, ICD10_03\r\n, ICD10_04\r\n, ICD10_05\r\n, ICD10_06\r\n, ICD10_07\r\n, ICD10_08\r\n, ICD10_09\r\n, ICD10_10\r\n, ICD10_11\r\n, ICD10_12\r\n, LocationID\r\n, Doctor1_ID as DoctorID\r\n, POSTypeID\r\n, FacilityID\r\n, ReferralID\r\n, SalesrepID\r\nFROM tbl_customer\r\nWHERE (ID = :ID)";
                    command.Parameters.Add(":ID", MySqlType.Int).Value = CustomerID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            this.ClearCustomer();
                        }
                        else
                        {
                            int? locationID = Globals.LocationID;
                            if (locationID == null)
                            {
                                locationID = NullableConvert.ToInt32(reader["LocationID"]);
                            }
                            Functions.SetComboBoxValue(this.cmbLocation, NullableConvert.ToDb(locationID));
                            Functions.SetTextBoxText(this.caShip.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.caShip.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.caShip.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.caShip.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.caShip.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            this.eddICD9_1.Text = NullableConvert.ToString(reader["ICD9_1"]);
                            this.eddICD9_2.Text = NullableConvert.ToString(reader["ICD9_2"]);
                            this.eddICD9_3.Text = NullableConvert.ToString(reader["ICD9_3"]);
                            this.eddICD9_4.Text = NullableConvert.ToString(reader["ICD9_4"]);
                            this.ResetDefaultDXPointer9();
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
                            this.ResetDefaultDXPointer10();
                            Functions.SetComboBoxValue(this.cmbDoctor, reader["DoctorID"]);
                            Functions.SetComboBoxValue(this.cmbPosType, reader["POSTypeID"]);
                            Functions.SetComboBoxValue(this.cmbFacility, reader["FacilityID"]);
                            Functions.SetComboBoxValue(this.cmbReferral, reader["ReferralID"]);
                            Functions.SetComboBoxValue(this.cmbSalesrep, reader["SalesrepID"]);
                        }
                    }
                }
                CustomerInsuranceTable dataTable = new CustomerInsuranceTable();
                DataRow row = dataTable.NewRow();
                dataTable.Rows.Add(row);
                row.AcceptChanges();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("", connection))
                {
                    adapter.SelectCommand.CommandText = $"SELECT
  tbl_customer_insurance.ID
, tbl_customer_insurance.`Rank`
, tbl_insurancecompany.Name
FROM tbl_customer_insurance
     LEFT JOIN tbl_insurancecompany ON tbl_customer_insurance.InsuranceCompanyID = tbl_insurancecompany.ID
WHERE (tbl_customer_insurance.CustomerID = {CustomerID})";
                    adapter.AcceptChangesDuringFill = true;
                    adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                    adapter.Fill(dataTable);
                }
                this.InternalLoadCustomerInsurance(dataTable);
                DataView view = new DataView(dataTable, "(ID IS NOT NULL) AND (ISNULL(Rank, 0) < 99)", "Rank ASC", DataViewRowState.OriginalRows);
                this.cmbCustomerInsurance1.SelectedValue = (1 > view.Count) ? DBNull.Value : view[0]["ID"];
                this.cmbCustomerInsurance2.SelectedValue = (2 > view.Count) ? DBNull.Value : view[1]["ID"];
                this.cmbCustomerInsurance3.SelectedValue = (3 > view.Count) ? DBNull.Value : view[2]["ID"];
                this.cmbCustomerInsurance4.SelectedValue = (4 > view.Count) ? DBNull.Value : view[3]["ID"];
                this.ResetDefaultBillIns1();
                this.ResetDefaultBillIns2();
                this.ResetDefaultBillIns3();
                this.ResetDefaultBillIns4();
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
                    command.CommandText = "SELECT * FROM tbl_order WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.F_MIR = NullableConvert.ToString(reader["MIR"]);
                            Functions.SetComboBoxValue(this.cmbCustomer, reader["CustomerID"]);
                            if (Versioned.IsNumeric(reader["CustomerID"]))
                            {
                                this.LoadCustomer(Conversions.ToInteger(reader["CustomerID"]));
                                this.ControlOrderDetails1.CustomerID = new int?(Conversions.ToInteger(reader["CustomerID"]));
                            }
                            else
                            {
                                this.ClearCustomer();
                                this.ControlOrderDetails1.CustomerID = null;
                            }
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance1, reader["CustomerInsurance1_ID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance2, reader["CustomerInsurance2_ID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance3, reader["CustomerInsurance3_ID"]);
                            Functions.SetComboBoxValue(this.cmbCustomerInsurance4, reader["CustomerInsurance4_ID"]);
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
                            Functions.SetComboBoxValue(this.cmbLocation, reader["LocationID"]);
                            Functions.SetComboBoxValue(this.cmbDoctor, reader["DoctorID"]);
                            Functions.SetComboBoxValue(this.cmbPosType, reader["POSTypeID"]);
                            Functions.SetComboBoxValue(this.cmbFacility, reader["FacilityID"]);
                            Functions.SetComboBoxValue(this.cmbReferral, reader["ReferralID"]);
                            Functions.SetComboBoxValue(this.cmbSalesrep, reader["SalesrepID"]);
                            Functions.SetComboBoxValue(this.cmbShippingMethod, reader["ShippingMethodID"]);
                            Functions.SetTextBoxText(this.txtSpecialInstructions, reader["SpecialInstructions"]);
                            Functions.SetTextBoxText(this.txtClaimNote, reader["ClaimNote"]);
                            Functions.SetTextBoxText(this.txtUserField1, reader["UserField1"]);
                            Functions.SetTextBoxText(this.txtUserField2, reader["UserField2"]);
                            Functions.SetDateBoxValue(this.dtbBillDate, reader["BillDate"]);
                            Functions.SetDateBoxValue(this.dtbOrderDate, reader["OrderDate"]);
                            Functions.SetTextBoxText(this.txtTakenBy, reader["TakenBy"]);
                            Functions.SetDateBoxValue(this.dtbDeliveryDate, reader["DeliveryDate"]);
                            Functions.SetCheckBoxChecked(this.chbApproved, reader["Approved"]);
                            this.ApprovedState = this.chbApproved.Checked;
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
                this.ResetDefaultBillIns1();
                this.ResetDefaultBillIns2();
                this.ResetDefaultBillIns3();
                this.ResetDefaultBillIns4();
                this.ResetDefaultDXPointer9();
                this.ResetDefaultDXPointer10();
                this.ResetDefaultDOSFrom();
                this.ControlOrderDetails1.LoadGrid(connection, ID);
                return true;
            }
        }

        private void mnuActionsCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                this.CreateInvoice();
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Create Invoices");
                ProjectData.ClearProjectError();
            }
        }

        private void mnuActionsMakeDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.SaveOrCancelChanges())
                {
                    int? nullable = NullableConvert.ToInt32(this.ObjectID);
                    if (nullable != null)
                    {
                        using (DialogDeposit deposit = new DialogDeposit())
                        {
                            deposit.ShowDialog(nullable.Value);
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Make Deposit");
                ProjectData.ClearProjectError();
            }
        }

        private void mnuActionsReloadDropdowns_Click(object sender, EventArgs e)
        {
            string[] tableNames = new string[0x10];
            tableNames[0] = "tbl_customer";
            tableNames[1] = "tbl_doctor";
            tableNames[2] = "tbl_referral";
            tableNames[3] = "tbl_salesrep";
            tableNames[4] = "tbl_facility";
            tableNames[5] = "tbl_shippingmethod";
            tableNames[6] = "tbl_icd9";
            tableNames[7] = "tbl_icd10";
            tableNames[8] = "tbl_crystalreport";
            tableNames[9] = "tbl_inventoryitem";
            tableNames[10] = "tbl_location";
            tableNames[11] = "tbl_postype";
            tableNames[12] = "tbl_pricecode";
            tableNames[13] = "tbl_pricecode_item";
            tableNames[14] = "tbl_hao";
            tableNames[15] = "tbl_warehouse";
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private void mnuActionsReorder_Click(object sender, EventArgs e)
        {
            this.ControlOrderDetails1.ShowReorderDialog(true);
        }

        private void mnuActionsSameOrSimilar_Click(object sender, EventArgs e)
        {
            try
            {
                string[] medicarePolicyNumbers = this.GetMedicarePolicyNumbers();
                if (medicarePolicyNumbers.Length != 0)
                {
                    ClassGlobalObjects.ShowForm(FormFactories.FormSameOrSimilar(this.ControlOrderDetails1.GetBillingCodes(), medicarePolicyNumbers.First<string>()));
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

        private void mnuActionsShipping_Click(object sender, EventArgs e)
        {
            int? nullable = NullableConvert.ToInt32(this.CustomerID);
            if (nullable != null)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormUpsShipping(nullable.Value));
            }
        }

        private void mnuFilterApproved_Any_Click(object sender, EventArgs e)
        {
            this.mnuFilterApproved_No.Checked = false;
            this.mnuFilterApproved_Any.Checked = true;
            this.mnuFilterApproved_Yes.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuFilterApproved_No_Click(object sender, EventArgs e)
        {
            this.mnuFilterApproved_No.Checked = true;
            this.mnuFilterApproved_Any.Checked = false;
            this.mnuFilterApproved_Yes.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuFilterApproved_Yes_Click(object sender, EventArgs e)
        {
            this.mnuFilterApproved_No.Checked = false;
            this.mnuFilterApproved_Any.Checked = false;
            this.mnuFilterApproved_Yes.Checked = true;
            this.InvalidateObjectList();
        }

        private void mnuFilterArchived_Any_Click(object sender, EventArgs e)
        {
            this.mnuFilterArchived_No.Checked = false;
            this.mnuFilterArchived_Any.Checked = true;
            this.mnuFilterArchived_Yes.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuFilterArchived_No_Click(object sender, EventArgs e)
        {
            this.mnuFilterArchived_No.Checked = true;
            this.mnuFilterArchived_Any.Checked = false;
            this.mnuFilterArchived_Yes.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuFilterArchived_Yes_Click(object sender, EventArgs e)
        {
            this.mnuFilterArchived_No.Checked = false;
            this.mnuFilterArchived_Any.Checked = false;
            this.mnuFilterArchived_Yes.Checked = true;
            this.InvalidateObjectList();
        }

        private void mnuFilterPagedGrids_Click(object sender, EventArgs e)
        {
            this.mnuFilterPagedGrids.Checked = !this.mnuFilterPagedGrids.Checked;
            PagedGrids = this.mnuFilterPagedGrids.Checked;
            ClassGlobalObjects.ShowForm(FormFactories.FormOrder());
            base.Close();
        }

        private void mnuFilterReoccuring_Click(object sender, EventArgs e)
        {
            this.mnuFilterReoccuring.Checked = !this.mnuFilterReoccuring.Checked;
            this.InvalidateObjectList();
        }

        private void mnuGotoCompliance_Click(object sender, EventArgs e)
        {
            if (this.ObjectID is int)
            {
                FormParameters @params = new FormParameters("OrderID", Conversions.ToInteger(this.ObjectID));
                ClassGlobalObjects.ShowForm(FormFactories.FormCompliance(), @params);
            }
        }

        private void mnuGotoImages_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["CustomerID"] = this.cmbCustomer.SelectedValue,
                ["OrderID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImageSearch(), @params);
        }

        private void mnuGotoNewImage_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["CustomerID"] = this.cmbCustomer.SelectedValue,
                ["OrderID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImage(), @params);
        }

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_order.ID}"] = this.ObjectID
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

        private void mnuPrintOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (Versioned.IsNumeric(this.ObjectID))
                {
                    if (base.HasUnsavedChanges && (MessageBox.Show("Current order has changes that was not saved in the database. Whould you like to save order and print report?", "Print Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes))
                    {
                        base.DoSaveClick();
                    }
                }
                else if (MessageBox.Show("Current order was not saved. In order to print order it should be saved. Whould you like to save order and print report?", "Print Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    base.DoSaveClick();
                }
                else
                {
                    return;
                }
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_order.ID}"] = this.ObjectID
                };
                ClassGlobalObjects.ShowReport("Order", @params);
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

        private static bool ObjectEquals(object obj1, object obj2) => 
            (obj1 is int) && ((obj2 is int) && (Conversions.ToInteger(obj1) == Conversions.ToInteger(obj2)));

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_order", "tbl_orderdetails", "tbl_inventory" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected void ProcessParameter_Other(FormParameters Params)
        {
            if ((Params != null) && Params.ContainsKey("ID"))
            {
                base.OpenObject(Params["ID"]);
                if (Params.ContainsKey("OrderDetailsID"))
                {
                    this.ControlOrderDetails1.ShowDetails(Params["OrderDetailsID"]);
                }
            }
        }

        private void ResetDefaultBillIns1()
        {
            this.ControlOrderDetails1.DefaultBillIns1 = NullableConvert.ToInt32(this.cmbCustomerInsurance1.SelectedValue) != null;
        }

        private void ResetDefaultBillIns2()
        {
            this.ControlOrderDetails1.DefaultBillIns2 = NullableConvert.ToInt32(this.cmbCustomerInsurance2.SelectedValue) != null;
        }

        private void ResetDefaultBillIns3()
        {
            this.ControlOrderDetails1.DefaultBillIns3 = NullableConvert.ToInt32(this.cmbCustomerInsurance3.SelectedValue) != null;
        }

        private void ResetDefaultBillIns4()
        {
            this.ControlOrderDetails1.DefaultBillIns4 = NullableConvert.ToInt32(this.cmbCustomerInsurance4.SelectedValue) != null;
        }

        private void ResetDefaultDOSFrom()
        {
            this.ControlOrderDetails1.DefaultDOSFrom = Functions.GetDateBoxValue(this.dtbDeliveryDate, DateTime.Today);
        }

        private void ResetDefaultDXPointer10()
        {
            Tuple<string, string>[] tupleArray1 = new Tuple<string, string>[12];
            tupleArray1[0] = Tuple.Create<string, string>("1", this.eddICD10_01.Text);
            tupleArray1[1] = Tuple.Create<string, string>("2", this.eddICD10_02.Text);
            tupleArray1[2] = Tuple.Create<string, string>("3", this.eddICD10_03.Text);
            tupleArray1[3] = Tuple.Create<string, string>("4", this.eddICD10_04.Text);
            tupleArray1[4] = Tuple.Create<string, string>("5", this.eddICD10_05.Text);
            tupleArray1[5] = Tuple.Create<string, string>("6", this.eddICD10_06.Text);
            tupleArray1[6] = Tuple.Create<string, string>("7", this.eddICD10_07.Text);
            tupleArray1[7] = Tuple.Create<string, string>("8", this.eddICD10_08.Text);
            tupleArray1[8] = Tuple.Create<string, string>("9", this.eddICD10_09.Text);
            tupleArray1[9] = Tuple.Create<string, string>("10", this.eddICD10_10.Text);
            tupleArray1[10] = Tuple.Create<string, string>("11", this.eddICD10_11.Text);
            tupleArray1[11] = Tuple.Create<string, string>("12", this.eddICD10_12.Text);
            StringBuilder builder = new StringBuilder();
            foreach (Tuple<string, string> tuple in tupleArray1)
            {
                if (!string.IsNullOrWhiteSpace(tuple.Item2))
                {
                    if (0 < builder.Length)
                    {
                        builder.Append(",");
                    }
                    builder.Append(tuple.Item1);
                }
            }
            this.ControlOrderDetails1.DefaultDXPointer10 = builder.ToString();
        }

        private void ResetDefaultDXPointer9()
        {
            Tuple<string, string>[] tupleArray1 = new Tuple<string, string>[] { Tuple.Create<string, string>("1", this.eddICD9_1.Text), Tuple.Create<string, string>("2", this.eddICD9_2.Text), Tuple.Create<string, string>("3", this.eddICD9_3.Text), Tuple.Create<string, string>("4", this.eddICD9_4.Text) };
            StringBuilder builder = new StringBuilder();
            foreach (Tuple<string, string> tuple in tupleArray1)
            {
                if (!string.IsNullOrWhiteSpace(tuple.Item2))
                {
                    if (0 < builder.Length)
                    {
                        builder.Append(",");
                    }
                    builder.Append(tuple.Item1);
                }
            }
            this.ControlOrderDetails1.DefaultDXPointer9 = builder.ToString();
        }

        private void ResetSameOrSimilar()
        {
            try
            {
                this.mnuActionsSameOrSimilar.Enabled = false;
                string[] medicarePolicyNumbers = this.GetMedicarePolicyNumbers();
                this.mnuActionsSameOrSimilar.Enabled = 0 < medicarePolicyNumbers.Length;
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

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            if (!Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
            {
                throw new UserNotifyException("You must select customer before saving order.");
            }
            int customerID = Conversions.ToInteger(this.cmbCustomer.SelectedValue);
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = customerID;
                    command.Parameters.Add("Approved", MySqlType.Bit).Value = this.chbApproved.Checked;
                    command.Parameters.Add("OrderDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbOrderDate);
                    command.Parameters.Add("DeliveryDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbDeliveryDate);
                    command.Parameters.Add("BillDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbBillDate);
                    command.Parameters.Add("SpecialInstructions", MySqlType.Text, 0x10000).Value = this.txtSpecialInstructions.Text;
                    command.Parameters.Add("ClaimNote", MySqlType.VarChar, 80).Value = this.txtClaimNote.Text;
                    command.Parameters.Add("UserField1", MySqlType.VarChar, 100).Value = this.txtUserField1.Text;
                    command.Parameters.Add("UserField2", MySqlType.VarChar, 100).Value = this.txtUserField2.Text;
                    command.Parameters.Add("LocationID", MySqlType.Int).Value = this.cmbLocation.SelectedValue;
                    command.Parameters.Add("DoctorID", MySqlType.Int).Value = this.cmbDoctor.SelectedValue;
                    command.Parameters.Add("POSTypeID", MySqlType.Int).Value = this.cmbPosType.SelectedValue;
                    command.Parameters.Add("FacilityID", MySqlType.Int).Value = this.cmbFacility.SelectedValue;
                    command.Parameters.Add("ReferralID", MySqlType.Int).Value = this.cmbReferral.SelectedValue;
                    command.Parameters.Add("SalesrepID", MySqlType.Int).Value = this.cmbSalesrep.SelectedValue;
                    command.Parameters.Add("ShippingMethodID", MySqlType.Int).Value = this.cmbShippingMethod.SelectedValue;
                    command.Parameters.Add("CustomerInsurance1_ID", MySqlType.Int).Value = this.cmbCustomerInsurance1.SelectedValue;
                    command.Parameters.Add("CustomerInsurance2_ID", MySqlType.Int).Value = this.cmbCustomerInsurance2.SelectedValue;
                    command.Parameters.Add("CustomerInsurance3_ID", MySqlType.Int).Value = this.cmbCustomerInsurance3.SelectedValue;
                    command.Parameters.Add("CustomerInsurance4_ID", MySqlType.Int).Value = this.cmbCustomerInsurance4.SelectedValue;
                    command.Parameters.Add("ICD9_1", MySqlType.VarChar, 6).Value = this.eddICD9_1.Text;
                    command.Parameters.Add("ICD9_2", MySqlType.VarChar, 6).Value = this.eddICD9_2.Text;
                    command.Parameters.Add("ICD9_3", MySqlType.VarChar, 6).Value = this.eddICD9_3.Text;
                    command.Parameters.Add("ICD9_4", MySqlType.VarChar, 6).Value = this.eddICD9_4.Text;
                    command.Parameters.Add("ICD10_01", MySqlType.VarChar, 8).Value = this.eddICD10_01.Text;
                    command.Parameters.Add("ICD10_02", MySqlType.VarChar, 8).Value = this.eddICD10_02.Text;
                    command.Parameters.Add("ICD10_03", MySqlType.VarChar, 8).Value = this.eddICD10_03.Text;
                    command.Parameters.Add("ICD10_04", MySqlType.VarChar, 8).Value = this.eddICD10_04.Text;
                    command.Parameters.Add("ICD10_05", MySqlType.VarChar, 8).Value = this.eddICD10_05.Text;
                    command.Parameters.Add("ICD10_06", MySqlType.VarChar, 8).Value = this.eddICD10_06.Text;
                    command.Parameters.Add("ICD10_07", MySqlType.VarChar, 8).Value = this.eddICD10_07.Text;
                    command.Parameters.Add("ICD10_08", MySqlType.VarChar, 8).Value = this.eddICD10_08.Text;
                    command.Parameters.Add("ICD10_09", MySqlType.VarChar, 8).Value = this.eddICD10_09.Text;
                    command.Parameters.Add("ICD10_10", MySqlType.VarChar, 8).Value = this.eddICD10_10.Text;
                    command.Parameters.Add("ICD10_11", MySqlType.VarChar, 8).Value = this.eddICD10_11.Text;
                    command.Parameters.Add("ICD10_12", MySqlType.VarChar, 8).Value = this.eddICD10_12.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (IsNew)
                    {
                        command.Parameters.Add("SaleType", MySqlType.VarChar, 50).Value = "Back Office";
                        command.Parameters.Add("TakenBy", MySqlType.VarChar, 50).Value = this.txtTakenBy.Text;
                        command.Parameters.Add("TakenAt", MySqlType.DateTime).Value = DateTime.Now;
                        flag = 0 < command.ExecuteInsert("tbl_order");
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
                        if (0 >= command.ExecuteUpdate("tbl_order", whereParameters))
                        {
                            command.Parameters.Add("SaleType", MySqlType.VarChar, 50).Value = "Back Office";
                            command.Parameters.Add("TakenBy", MySqlType.VarChar, 50).Value = this.txtTakenBy.Text;
                            command.Parameters.Add("TakenAt", MySqlType.DateTime).Value = DateTime.Now;
                            flag = 0 < command.ExecuteInsert("tbl_order");
                        }
                    }
                }
                this.ApprovedState = this.chbApproved.Checked;
                this.ControlOrderDetails1.SaveGrid(connection, ID, customerID);
                UpdateCMNDetails(ID);
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.Parameters.Add("P_OrderID", MySqlType.Int).Value = ID;
                    command2.ExecuteProcedure("mir_update_order");
                }
                using (MySqlCommand command3 = new MySqlCommand("", connection))
                {
                    command3.CommandText = "SELECT MIR FROM tbl_order WHERE ID = :ID";
                    command3.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    this.F_MIR = NullableConvert.ToString(command3.ExecuteScalar());
                }
            }
            return flag;
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            this.ProcessParameter_Other(Params);
            base.ProcessParameter_ShowMissing(Params);
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
            this.ShowMissingInformation(Show, this.tpInfo);
            this.ShowMissingInformation(Show, this.tpICD9);
            this.ShowMissingInformation(Show, this.tpICD10);
            this.ControlOrderDetails1.ShowMissingInformation(Show);
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

        private void tsmiGridSearchApprove_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                if (grid != null)
                {
                    new SearchGridProcessor.Approve(grid).Run();
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

        private void tsmiGridSearchArchive_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                if (grid != null)
                {
                    new SearchGridProcessor.Archive(grid).Run();
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

        private void tsmiGridSearchUnarchive_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase grid = this.cmsGridSearch.SourceControl<GridBase>();
                if (grid != null)
                {
                    new SearchGridProcessor.Unarchive(grid).Run();
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

        private void UpdateActionsState()
        {
            this.mnuActionsCreateInvoice.Enabled = !this.IsNew & this.chbApproved.Checked;
            this.mnuActionsMakeDeposit.Enabled = !this.IsNew;
        }

        private static int UpdateCMNDetails(int OrderID)
        {
            int num;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (MySqlCommand command = new MySqlCommand("", connection, transaction))
                    {
                        command.CommandText = $"DELETE tbl_cmnform_details.*
FROM (tbl_cmnform_details
      INNER JOIN tbl_cmnform ON tbl_cmnform_details.CMNFormID = tbl_cmnform.ID)
WHERE (tbl_cmnform.OrderID = {OrderID})";
                        command.ExecuteNonQuery();
                        command.CommandText = $"INSERT INTO tbl_cmnform_details
(CMNFormID,
 BillingCode,
 InventoryItemID,
 OrderedQuantity,
 OrderedUnits,
 BillablePrice,
 AllowablePrice,
 Period,
 Modifier1,
 Modifier2,
 Modifier3,
 Modifier4,
 PredefinedTextID)
SELECT tbl_cmnform.ID as CMNFormID,
       tbl_orderdetails.BillingCode,
       tbl_orderdetails.InventoryItemID,
       tbl_orderdetails.OrderedQuantity,
       tbl_orderdetails.OrderedUnits,
       tbl_orderdetails.BillablePrice,
       tbl_orderdetails.AllowablePrice,
       tbl_orderdetails.BilledWhen,
       tbl_orderdetails.Modifier1,
       tbl_orderdetails.Modifier2,
       tbl_orderdetails.Modifier3,
       tbl_orderdetails.Modifier4,
       tbl_pricecode_item.PredefinedTextID
FROM ((tbl_cmnform
       INNER JOIN tbl_orderdetails ON tbl_cmnform.OrderID = tbl_orderdetails.OrderID
                                  AND tbl_cmnform.ID = tbl_orderdetails.CMNFormID)
      LEFT JOIN tbl_pricecode_item ON tbl_orderdetails.InventoryItemID = tbl_pricecode_item.InventoryItemID
                                  AND tbl_orderdetails.PriceCodeID = tbl_pricecode_item.PriceCodeID)
WHERE (tbl_cmnform.OrderID = {OrderID})";
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    transaction.Rollback();
                    throw;
                }
            }
            return num;
        }

        private void UpdateShippingState()
        {
            bool flag = false;
            if (!base.IsNew)
            {
                DataRow selectedRow = this.cmbShippingMethod.SelectedRow;
                if (selectedRow != null)
                {
                    DataColumn column = selectedRow.Table.Columns["Type"];
                    if (column != null)
                    {
                        flag = ShippingType.ToEnum(selectedRow[column] as string) == ShippingTypeEnum.Ups;
                    }
                }
            }
            this.mnuActionsShipping.Enabled = flag;
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (!Versioned.IsNumeric(this.cmbCustomer.SelectedValue))
            {
                base.ValidationErrors.SetError(this.cmbCustomer, "You Must Select Customer");
            }
            object selectedValue = this.cmbCustomerInsurance2.SelectedValue;
            object obj3 = this.cmbCustomerInsurance3.SelectedValue;
            object obj4 = this.cmbCustomerInsurance4.SelectedValue;
            object obj1 = this.cmbCustomerInsurance1.SelectedValue;
            if (ObjectEquals(obj1, selectedValue))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance1, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance2, "You must use distinct values.");
            }
            object local1 = obj1;
            if (ObjectEquals(local1, obj3))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance1, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance3, "You must use distinct values.");
            }
            if (ObjectEquals(local1, obj4))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance1, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance4, "You must use distinct values.");
            }
            if (ObjectEquals(selectedValue, obj3))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance2, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance3, "You must use distinct values.");
            }
            if (ObjectEquals(selectedValue, obj4))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance2, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance4, "You must use distinct values.");
            }
            if (ObjectEquals(obj3, obj4))
            {
                base.ValidationErrors.SetError(this.cmbCustomerInsurance3, "You must use distinct values.");
                base.ValidationErrors.SetError(this.cmbCustomerInsurance4, "You must use distinct values.");
            }
            if (Globals.AutoReorderInventory && (Functions.EnumerateErrors(this, base.ValidationErrors) == 0))
            {
                this.ControlOrderDetails1.ShowReorderDialog(false);
            }
        }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("cmbCustomer")]
        private Combobox cmbCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone")]
        private Label lblPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("caShip")]
        private ControlAddress caShip { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label5")]
        private Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReferral")]
        private Label lblReferral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSalesrep")]
        private Label lblSalesrep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbSalesrep")]
        private Combobox cmbSalesrep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbReferral")]
        private Combobox cmbReferral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbBillDate")]
        private UltraDateTimeEditor dtbBillDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbOrderDate")]
        private UltraDateTimeEditor dtbOrderDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel2")]
        private GroupBox Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbApproved")]
        private CheckBox chbApproved { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbShippingMethod")]
        private Combobox cmbShippingMethod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShippingMethod")]
        private Label lblShippingMethod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbDeliveryDate")]
        private UltraDateTimeEditor dtbDeliveryDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDeliveryDate")]
        private Label lblDeliveryDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance1")]
        private Label lblCustomerInsurance1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance2")]
        private Label lblCustomerInsurance2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance3")]
        private Label lblCustomerInsurance3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCustomerInsurance4")]
        private Label lblCustomerInsurance4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance1")]
        private ComboBox cmbCustomerInsurance1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance2")]
        private ComboBox cmbCustomerInsurance2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance3")]
        private ComboBox cmbCustomerInsurance3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCustomerInsurance4")]
        private ComboBox cmbCustomerInsurance4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTakenBy")]
        private TextBox txtTakenBy { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTakenBy")]
        private Label lblTakenBy { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbFacility")]
        private Combobox cmbFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFacility")]
        private Label lblFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrderNumber")]
        private Label lblOrderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpDelivery")]
        private TabPage tpDelivery { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpSpecial")]
        private TabPage tpSpecial { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSpecialInstructions")]
        private TextBox txtSpecialInstructions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSpecialInstructions")]
        private Label lblSpecialInstructions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ControlOrderDetails1")]
        private ControlOrderDetails ControlOrderDetails1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoImages")]
        private MenuItem mnuGotoImages { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoNewImage")]
        private MenuItem mnuGotoNewImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoCompliance")]
        private MenuItem mnuGotoCompliance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPosType")]
        private Label lblPosType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPosType")]
        private Combobox cmbPosType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrderDetails")]
        private Label lblOrderDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLocation")]
        private Label lblLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbLocation")]
        private Combobox cmbLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDoctor")]
        private Label lblDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDoctor")]
        private Combobox cmbDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpClaim")]
        private TabPage tpClaim { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblClaimNote")]
        private Label lblClaimNote { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtClaimNote")]
        private TextBox txtClaimNote { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsCreateInvoice")]
        private MenuItem mnuActionsCreateInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsMakeDeposit")]
        private MenuItem mnuActionsMakeDeposit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsReorder")]
        private MenuItem mnuActionsReorder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsShipping")]
        private MenuItem mnuActionsShipping { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsReloadDropdowns")]
        private MenuItem mnuActionsReloadDropdowns { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsSameOrSimilar")]
        private MenuItem mnuActionsSameOrSimilar { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterArchived_Any")]
        private MenuItem mnuFilterArchived_Any { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterArchived_Yes")]
        private MenuItem mnuFilterArchived_Yes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterArchived_No")]
        private MenuItem mnuFilterArchived_No { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterSeparator1")]
        private MenuItem mnuFilterSeparator1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterApproved_Any")]
        private MenuItem mnuFilterApproved_Any { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterApproved_Yes")]
        private MenuItem mnuFilterApproved_Yes { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterApproved_No")]
        private MenuItem mnuFilterApproved_No { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterSeparator2")]
        private MenuItem mnuFilterSeparator2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterReoccuring")]
        private MenuItem mnuFilterReoccuring { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterSeparator3")]
        private MenuItem mnuFilterSeparator3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterPagedGrids")]
        private MenuItem mnuFilterPagedGrids { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGridSearch")]
        private ContextMenuStrip cmsGridSearch { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchArchive")]
        private ToolStripMenuItem tsmiGridSearchArchive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchUnarchive")]
        private ToolStripMenuItem tsmiGridSearchUnarchive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchApprove")]
        private ToolStripMenuItem tsmiGridSearchApprove { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridSearchSeparator")]
        private ToolStripSeparator tsmiGridSearchSeparator { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl2")]
        private TabControl TabControl2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpInfo")]
        private TabPage tpInfo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpICD9")]
        private TabPage tpICD9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpICD10")]
        private TabPage tpICD10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("Label16")]
        private Label Label16 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_07")]
        private ExtendedDropdown eddICD10_07 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label17")]
        private Label Label17 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label18")]
        private Label Label18 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_05")]
        private ExtendedDropdown eddICD10_05 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_11")]
        private ExtendedDropdown eddICD10_11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label19")]
        private Label Label19 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label20")]
        private Label Label20 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label21")]
        private Label Label21 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_02")]
        private ExtendedDropdown eddICD10_02 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_09")]
        private ExtendedDropdown eddICD10_09 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label22")]
        private Label Label22 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_04")]
        private ExtendedDropdown eddICD10_04 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label23")]
        private Label Label23 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label24")]
        private Label Label24 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_03")]
        private ExtendedDropdown eddICD10_03 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label25")]
        private Label Label25 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("eddICD10_01")]
        private ExtendedDropdown eddICD10_01 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label26")]
        private Label Label26 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label27")]
        private Label Label27 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ImageList1")]
        private ImageList ImageList1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lnkSurvey")]
        private LinkLabel lnkSurvey { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpUserFields")]
        private TabPage tpUserFields { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUserField2")]
        private TextBox txtUserField2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtUserField1")]
        private TextBox txtUserField1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUserField2")]
        private Label lblUserField2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUserField1")]
        private Label lblUserField1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public object CustomerID =>
            this.cmbCustomer.SelectedValue;

        private static bool PagedGrids
        {
            get => 
                RegistrySettings.GetUserBool("FormOrder:PagedGrids").GetValueOrDefault(false);
            set => 
                RegistrySettings.SetUserBool("FormOrder:PagedGrids", value);
        }

        private static bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        protected YesNoAny ArchivedFilter =>
            !this.mnuFilterArchived_Yes.Checked ? (!this.mnuFilterArchived_No.Checked ? YesNoAny.Any : YesNoAny.No) : YesNoAny.Yes;

        protected YesNoAny ApprovedFilter =>
            !this.mnuFilterApproved_Yes.Checked ? (!this.mnuFilterApproved_No.Checked ? YesNoAny.Any : YesNoAny.No) : YesNoAny.Yes;

        protected bool ReoccuringFilter =>
            this.mnuFilterReoccuring.Checked;

        private bool ApprovedState
        {
            get => 
                !this.chbApproved.Enabled;
            set
            {
                this.chbApproved.Enabled = !value || Permissions.FormOrder_Approved.Allow_ADD_EDIT;
                if (value)
                {
                    this.ControlOrderDetails1.AllowState = AllowStateEnum.AllowEdit00;
                }
                else
                {
                    this.ControlOrderDetails1.AllowState = AllowStateEnum.AllowAll;
                }
            }
        }

        protected override object ObjectID
        {
            get => 
                base.ObjectID;
            set
            {
                int? companyOrderSurveyID;
                bool flag1;
                base.ObjectID = value;
                if (!Versioned.IsNumeric(value) || (Globals.CompanyImagingUri == null))
                {
                    flag1 = false;
                }
                else
                {
                    companyOrderSurveyID = Globals.CompanyOrderSurveyID;
                    flag1 = companyOrderSurveyID != null;
                }
                this.lnkSurvey.Enabled = flag1;
                this.ControlOrderDetails1.Visible = Versioned.IsNumeric(value);
                this.lblOrderDetails.Visible = !Versioned.IsNumeric(value);
                if (Versioned.IsNumeric(value))
                {
                    this.ControlOrderDetails1.OrderID = new int?(Conversions.ToInteger(value));
                    this.lblOrderNumber.Text = "Order # " + Conversions.ToString(Conversions.ToInteger(value));
                }
                else
                {
                    companyOrderSurveyID = null;
                    this.ControlOrderDetails1.OrderID = companyOrderSurveyID;
                    this.lblOrderNumber.Text = "Order #";
                }
            }
        }

        protected override bool IsNew
        {
            get => 
                base.IsNew;
            set
            {
                base.IsNew = value;
                this.cmbCustomer.ReadOnly = !value;
                this.mnuGotoCompliance.Enabled = !value;
                this.UpdateActionsState();
                this.UpdateShippingState();
            }
        }

        private FormMirHelper MirHelper
        {
            get
            {
                int num;
                int num2;
                if (this.F_MirHelper != null)
                {
                    goto TR_0000;
                }
                else
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.F_MirHelper.Add("DeliveryDate", this.dtbDeliveryDate, "Delivery Date is required for invoice");
                    this.F_MirHelper.Add("BillDate", this.dtbBillDate, "Bill Date is required for invoice");
                    this.F_MirHelper.Add("Customer.Required", this.cmbCustomer, "Customer is required for invoice");
                    this.F_MirHelper.Add("Customer.ID", this.cmbCustomer, "Customer contains missing information");
                    this.F_MirHelper.Add("Policy1.Required", this.cmbCustomerInsurance1, "Policy #1 is required for invoice");
                    this.F_MirHelper.Add("Policy1.MIR", this.cmbCustomerInsurance1, "Policy #1 contains missing information");
                    this.F_MirHelper.Add("Policy2.Required", this.cmbCustomerInsurance2, "Policy #2 is required for invoice");
                    this.F_MirHelper.Add("Policy2.MIR", this.cmbCustomerInsurance2, "Policy #2 contains missing information");
                    this.F_MirHelper.Add("Facility.MIR", this.cmbFacility, "Facility contains missing information");
                    this.F_MirHelper.Add("PosType.Required", this.cmbPosType, "Valid POS Type is required");
                    num = 1;
                }
                goto TR_001B;
            TR_0000:
                return this.F_MirHelper;
            TR_0001:
                num2++;
                if (num2 > 12)
                {
                    goto TR_0000;
                }
            TR_0011:
                while (true)
                {
                    ExtendedDropdown dropdown2;
                    switch (num2)
                    {
                        case 1:
                            dropdown2 = this.eddICD10_01;
                            break;

                        case 2:
                            dropdown2 = this.eddICD10_02;
                            break;

                        case 3:
                            dropdown2 = this.eddICD10_03;
                            break;

                        case 4:
                            dropdown2 = this.eddICD10_04;
                            break;

                        case 5:
                            dropdown2 = this.eddICD10_05;
                            break;

                        case 6:
                            dropdown2 = this.eddICD10_06;
                            break;

                        case 7:
                            dropdown2 = this.eddICD10_07;
                            break;

                        case 8:
                            dropdown2 = this.eddICD10_08;
                            break;

                        case 9:
                            dropdown2 = this.eddICD10_09;
                            break;

                        case 10:
                            dropdown2 = this.eddICD10_10;
                            break;

                        case 11:
                            dropdown2 = this.eddICD10_11;
                            break;

                        case 12:
                            dropdown2 = this.eddICD10_12;
                            break;

                        default:
                            goto TR_0001;
                    }
                    this.F_MirHelper.Add("ICD10.Required", dropdown2, "At least one diagnosis code is required");
                    this.F_MirHelper.Add($"ICD10.{num2:00}.Unknown", dropdown2, $"Diagnosis code #{num2} is unknown");
                    this.F_MirHelper.Add($"ICD10.{num2:00}.Inactive", dropdown2, $"Diagnosis code #{num2} was not active at the date of order creation");
                    break;
                }
                goto TR_0001;
            TR_0013:
                num++;
                if (num > 4)
                {
                    num2 = 1;
                    goto TR_0011;
                }
            TR_001B:
                while (true)
                {
                    ExtendedDropdown dropdown;
                    switch (num)
                    {
                        case 1:
                            dropdown = this.eddICD9_1;
                            break;

                        case 2:
                            dropdown = this.eddICD9_2;
                            break;

                        case 3:
                            dropdown = this.eddICD9_3;
                            break;

                        case 4:
                            dropdown = this.eddICD9_4;
                            break;

                        default:
                            goto TR_0013;
                    }
                    this.F_MirHelper.Add("ICD9.Required", dropdown, "At least one diagnosis code is required");
                    this.F_MirHelper.Add($"ICD9.{num}.Unknown", dropdown, $"Diagnosis code #{num} is unknown");
                    this.F_MirHelper.Add($"ICD9.{num}.Inactive", dropdown, $"Diagnosis code #{num} was not active at the date of order creation");
                    break;
                }
                goto TR_0013;
            }
        }

        public class CustomerInsuranceTable : DataTable
        {
            public readonly DataColumn Col_ID;
            public readonly DataColumn Col_Name;
            public readonly DataColumn Col_Rank;

            public CustomerInsuranceTable() : base("tbl_customer_insurance")
            {
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_Name = base.Columns.Add("Name", typeof(string));
                this.Col_Rank = base.Columns.Add("Rank", typeof(int));
            }
        }

        private class DetailInfo
        {
            public DetailInfo()
            {
            }

            public DetailInfo(IDataRecord record)
            {
                this.AccountNumber = record.GetString("AccountNumber");
                this.Approved = record.GetBoolean("Approved").Value;
                this.BillableAmount = record.GetDecimal("BillableAmount").Value;
                this.BillingCode = record.GetString("BillingCode");
                this.BillingMonth = record.GetInt32("BillingMonth").Value;
                this.DateOfBirth = record.GetDateTime("DateOfBirth");
                this.DOSFrom = record.GetDateTime("DOSFrom");
                this.EndDate = record.GetDateTime("EndDate");
                this.FirstName = record.GetString("FirstName");
                this.ID = record.GetInt32("ID").Value;
                this.InventoryItem = record.GetString("InventoryItem");
                this.LastName = record.GetString("LastName");
                this.OrderID = record.GetInt32("OrderID").Value;
                this.Quantity = record.GetInt32("Quantity").Value;
                this.State = record.GetString("State");
                this.TakenAt = record.GetDateTime("TakenAt");
                this.TakenBy = record.GetString("TakenBy");
                this.VisualState = record.GetString("VisualState");
            }

            public int ID { get; set; }

            public int OrderID { get; set; }

            public string LastName { get; set; }

            public string FirstName { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public string AccountNumber { get; set; }

            public string BillingCode { get; set; }

            public string InventoryItem { get; set; }

            public string State { get; set; }

            public DateTime? EndDate { get; set; }

            public int BillingMonth { get; set; }

            public DateTime? DOSFrom { get; set; }

            public decimal BillableAmount { get; set; }

            public int Quantity { get; set; }

            public bool Approved { get; set; }

            public string TakenBy { get; set; }

            public DateTime? TakenAt { get; set; }

            public string VisualState { get; set; }
        }

        private class DetailsNavigatorEventsHandler : NavigatorEventsHandler
        {
            private readonly FormOrder Form;

            public DetailsNavigatorEventsHandler(FormOrder form)
            {
                if (form == null)
                {
                    throw new ArgumentNullException("form");
                }
                this.Form = form;
            }

            private static void CellFormatting(object sender, GridCellFormattingEventArgs e)
            {
                try
                {
                    FormOrder.DetailInfo info = e.Row.Get<FormOrder.DetailInfo>();
                    if (info != null)
                    {
                        string visualState = info.VisualState;
                        if (string.Compare(visualState, "Not Approved", true) == 0)
                        {
                            e.CellStyle.ForeColor = Color.Blue;
                        }
                        else if (string.Compare(visualState, "Not Confirmed", true) == 0)
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        else if (string.Compare(visualState, "Confirmed", true) == 0)
                        {
                            e.CellStyle.ForeColor = Color.Green;
                        }
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
            }

            public override void CreateSource(object sender, CreateSourceEventArgs args)
            {
                args.Source = new List<FormOrder.DetailInfo>().ToGridSource<FormOrder.DetailInfo>(new string[0]);
            }

            private IEnumerable<FormOrder.DetailInfo> FetchData(PagedFilter SearchTerms)
            {
                IEnumerable<FormOrder.DetailInfo> enumerable;
                FormOrder.QueryOptions options1 = new FormOrder.QueryOptions();
                options1.FilterLocationID = Globals.LocationID;
                options1.FilterUserID = new short?(Globals.CompanyUserID);
                options1.FilterArchived = this.Form.ArchivedFilter;
                options1.FilterApproved = this.Form.ApprovedFilter;
                options1.FilterReoccuring = this.Form.ReoccuringFilter;
                options1.SearchTerms = SearchTerms;
                FormOrder.QueryOptions options = options1;
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    using (MySqlCommand command = new MySqlCommand(GetDetailsQuery(options), connection))
                    {
                        connection.Open();
                        enumerable = command.ExecuteList<FormOrder.DetailInfo>((_Closure$__.$I13-0 == null) ? (_Closure$__.$I13-0 = new Func<IDataRecord, FormOrder.DetailInfo>(_Closure$__.$I._Lambda$__13-0)) : _Closure$__.$I13-0);
                    }
                }
                return enumerable;
            }

            public override void FillSource(object sender, FillSourceEventArgs args)
            {
                ((ArrayGridSource<FormOrder.DetailInfo>) args.Source).AppendRange(this.FetchData(null));
            }

            public override void FillSource(object sender, PagedFillSourceEventArgs args)
            {
                ((ArrayGridSource<FormOrder.DetailInfo>) args.Source).AppendRange(this.FetchData(args.Filter));
            }

            private static string GetDetailsQuery(FormOrder.QueryOptions Options)
            {
                string str = FormOrder.IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1";
                string str2 = (Options.FilterLocationID == null) ? ((Options.FilterUserID == null) ? "1 = 1" : ("tbl_order.LocationID IS NULL OR tbl_order.LocationID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + Options.FilterUserID.Value.ToString() + ")")) : ("tbl_order.LocationID = " + Options.FilterLocationID.Value.ToString());
                string str3 = (Options.FilterArchived != YesNoAny.Yes) ? ((Options.FilterArchived != YesNoAny.No) ? "1 = 1" : "tbl_order.Archived = 0") : "tbl_order.Archived = 1";
                string str4 = (Options.FilterApproved != YesNoAny.Yes) ? ((Options.FilterApproved != YesNoAny.No) ? "1 = 1" : "tbl_order.Approved = 0") : "tbl_order.Approved = 1";
                string str5 = !Options.FilterReoccuring ? "1 = 1" : "tbl_orderdetails.SaleRentType = 'Re-occurring Sale'";
                StringBuilder builder = new StringBuilder(0x1000);
                string[] textArray1 = new string[11];
                textArray1[0] = "SELECT\r\n  tbl_orderdetails.ID\r\n, tbl_orderdetails.OrderID\r\n, tbl_customer.LastName\r\n, tbl_customer.FirstName\r\n, tbl_customer.DateOfBirth\r\n, tbl_customer.AccountNumber\r\n, tbl_orderdetails.BillingCode\r\n, tbl_inventoryitem.Name as InventoryItem\r\n, tbl_orderdetails.State\r\n, tbl_orderdetails.EndDate\r\n, tbl_orderdetails.BillingMonth\r\n, tbl_orderdetails.DOSFrom\r\n, IFNULL(tbl_orderdetails.BilledQuantity, 0) as Quantity\r\n, IFNULL(tbl_orderdetails.BilledQuantity * tbl_orderdetails.BillablePrice, 0.0) as BillableAmount\r\n, tbl_order.Approved\r\n, tbl_order.TakenBy\r\n, tbl_order.TakenAt\r\n, tbl_shippingmethod.Name as ShippingMethod\r\n, CASE WHEN tbl_order.Approved = 0 THEN 'Not Approved'\r\n       WHEN tbl_orderdetails.State = 'Pickup' AND tbl_orderdetails.EndDate IS NULL THEN 'Not Confirmed'\r\n       WHEN tbl_orderdetails.State = 'Pickup' AND tbl_orderdetails.EndDate IS NOT NULL THEN 'Confirmed'\r\n       ELSE 'Usual' END as VisualState\r\nFROM tbl_orderdetails\r\n     LEFT JOIN tbl_inventoryitem ON tbl_orderdetails.InventoryItemID = tbl_inventoryitem.ID\r\n     INNER JOIN tbl_order ON tbl_orderdetails.OrderID = tbl_order.ID\r\n                         AND tbl_orderdetails.CustomerID = tbl_order.CustomerID\r\n     LEFT JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_company ON tbl_company.ID = 1\r\n     LEFT JOIN tbl_shippingmethod ON tbl_order.ShippingMethodID = tbl_shippingmethod.ID\r\nWHERE (";
                textArray1[1] = str3;
                textArray1[2] = ")\r\n  AND (";
                textArray1[3] = str4;
                textArray1[4] = ")\r\n  AND (";
                textArray1[5] = str;
                textArray1[6] = ")\r\n  AND (";
                textArray1[7] = str2;
                textArray1[8] = ")\r\n  AND (";
                textArray1[9] = str5;
                textArray1[10] = ")\r\n  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))\r\n";
                builder.Append(string.Concat(textArray1));
                if ((Options.SearchTerms != null) && !string.IsNullOrWhiteSpace(Options.SearchTerms.Filter))
                {
                    QueryExpression[] expressions = new QueryExpression[0x11];
                    expressions[0] = new QueryExpression("tbl_orderdetails.ID", MySqlType.Int);
                    expressions[1] = new QueryExpression("tbl_orderdetails.OrderID", MySqlType.Int);
                    expressions[2] = new QueryExpression("tbl_customer.LastName", MySqlType.VarChar);
                    expressions[3] = new QueryExpression("tbl_customer.FirstName", MySqlType.VarChar);
                    expressions[4] = new QueryExpression("tbl_customer.DateOfBirth", MySqlType.Date);
                    expressions[5] = new QueryExpression("tbl_customer.AccountNumber", MySqlType.VarChar);
                    expressions[6] = new QueryExpression("tbl_orderdetails.BillingCode", MySqlType.VarChar);
                    expressions[7] = new QueryExpression("tbl_inventoryitem.Name", MySqlType.VarChar);
                    expressions[8] = new QueryExpression("tbl_orderdetails.State", MySqlType.VarChar);
                    expressions[9] = new QueryExpression("tbl_orderdetails.EndDate", MySqlType.Date);
                    expressions[10] = new QueryExpression("tbl_orderdetails.BillingMonth", MySqlType.Int);
                    expressions[11] = new QueryExpression("tbl_orderdetails.DOSFrom", MySqlType.Date);
                    expressions[12] = new QueryExpression("IFNULL(tbl_orderdetails.BilledQuantity * tbl_orderdetails.BillablePrice, 0.0)", MySqlType.Float);
                    expressions[13] = new QueryExpression("tbl_orderdetails.BilledQuantity", MySqlType.Int);
                    expressions[14] = new QueryExpression("tbl_order.TakenBy", MySqlType.VarChar);
                    expressions[15] = new QueryExpression("tbl_order.TakenAt", MySqlType.Date);
                    expressions[0x10] = new QueryExpression("tbl_shippingmethod.Name", MySqlType.VarChar);
                    string str6 = MySqlFilterUtilities.BuildFilter(expressions, Options.SearchTerms.Filter);
                    if (!string.IsNullOrEmpty(str6))
                    {
                        builder.Append("  AND (").Append(str6).Append(")\r\n");
                    }
                }
                builder.Append("ORDER BY tbl_orderdetails.OrderID DESC, tbl_orderdetails.ID DESC\r\n");
                if (Options.SearchTerms != null)
                {
                    builder.AppendFormat("LIMIT {0}, {1}", Options.SearchTerms.Start, Options.SearchTerms.Count);
                }
                return builder.ToString();
            }

            public override void InitializeAppearance(GridAppearanceBase appearance)
            {
                appearance.AutoGenerateColumns = false;
                appearance.Columns.Clear();
                appearance.MultiSelect = true;
                appearance.AddTextColumn("ID", "#", 50, appearance.IntegerStyle());
                appearance.AddTextColumn("OrderID", "Order#", 50, appearance.IntegerStyle());
                appearance.AddTextColumn("LastName", "Last Name", 80);
                appearance.AddTextColumn("FirstName", "First Name", 80);
                appearance.AddTextColumn("DateOfBirth", "Birthday", 80, appearance.DateStyle());
                appearance.AddTextColumn("AccountNumber", "Account#", 60);
                appearance.AddTextColumn("BillingCode", "B. Code", 60, appearance.IntegerStyle());
                appearance.AddTextColumn("InventoryItem", "Inv. Item", 80);
                appearance.AddTextColumn("State", "State", 60);
                appearance.AddTextColumn("EndDate", "End Date", 80, appearance.DateStyle());
                appearance.AddTextColumn("BillingMonth", "Month", 50, appearance.IntegerStyle());
                appearance.AddTextColumn("DOSFrom", "DOSFrom", 80, appearance.DateStyle());
                appearance.AddTextColumn("BillableAmount", "Billable", 60, appearance.PriceStyle());
                appearance.AddTextColumn("Quantity", "Qty", 50, appearance.IntegerStyle());
                appearance.AddTextColumn("TakenBy", "Ordered By", 80);
                appearance.AddTextColumn("TakenAt", "Ordered At", 100, appearance.DateTimeStyle());
                appearance.AddTextColumn("ShippingMethod", "Shipping", 80);
                appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(FormOrder.DetailsNavigatorEventsHandler.CellFormatting);
            }

            public override void NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
            {
                FormOrder.DetailInfo info = args.GridRow.Get<FormOrder.DetailInfo>();
                if (info != null)
                {
                    this.Form.OpenObject(info.OrderID);
                    this.Form.ControlOrderDetails1.ShowDetails(info.ID);
                }
            }

            public override string Caption =>
                "Details";

            public override bool Switchable =>
                false;

            public override IEnumerable<string> TableNames =>
                new string[] { "tbl_orderdetails", "tbl_inventoryitem", "tbl_order", "tbl_customer", "tbl_company", "tbl_shippingmethod", "tbl_user_location" };

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly FormOrder.DetailsNavigatorEventsHandler._Closure$__ $I = new FormOrder.DetailsNavigatorEventsHandler._Closure$__();
                public static Func<IDataRecord, FormOrder.DetailInfo> $I13-0;

                internal FormOrder.DetailInfo _Lambda$__13-0(IDataRecord r) => 
                    new FormOrder.DetailInfo(r);
            }
        }

        private class OrderInfo
        {
            public OrderInfo()
            {
            }

            public OrderInfo(IDataRecord record)
            {
                this.AccountNumber = record.GetString("AccountNumber");
                this.Approved = record.GetBoolean("Approved").Value;
                this.BillDate = record.GetDateTime("BillDate");
                this.DeliveryDate = record.GetDateTime("DeliveryDate");
                this.DateOfBirth = record.GetDateTime("DateOfBirth");
                this.FirstName = record.GetString("FirstName");
                this.ID = record.GetInt32("ID").Value;
                this.LastName = record.GetString("LastName");
                this.OrderDate = record.GetDateTime("OrderDate");
                this.ShippingMethod = record.GetString("ShippingMethod");
                this.TakenAt = record.GetDateTime("TakenAt");
                this.TakenBy = record.GetString("TakenBy");
            }

            public int ID { get; set; }

            public string LastName { get; set; }

            public string FirstName { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public string AccountNumber { get; set; }

            public DateTime? OrderDate { get; set; }

            public DateTime? BillDate { get; set; }

            public DateTime? DeliveryDate { get; set; }

            public bool Approved { get; set; }

            public string TakenBy { get; set; }

            public DateTime? TakenAt { get; set; }

            public string ShippingMethod { get; set; }
        }

        private class QueryOptions
        {
            public int? FilterLocationID;
            public short? FilterUserID;
            public YesNoAny FilterArchived;
            public YesNoAny FilterApproved;
            public bool FilterReoccuring;
            public PagedFilter SearchTerms;
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
                this.Grid.GetSelectedRows().Select<DataGridViewRow, int>(((_Closure$__.$I3-0 == null) ? (_Closure$__.$I3-0 = new Func<DataGridViewRow, int>(_Closure$__.$I._Lambda$__3-0)) : _Closure$__.$I3-0)).ToArray<int>();

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
                public static readonly FormOrder.SearchGridProcessor._Closure$__ $I = new FormOrder.SearchGridProcessor._Closure$__();
                public static Func<DataGridViewRow, int> $I3-0;

                internal int _Lambda$__3-0(DataGridViewRow r) => 
                    ((FormOrder.OrderInfo) r.DataBoundItem).ID;
            }

            public class Approve : FormOrder.SearchGridProcessor
            {
                public Approve(GridBase grid) : base(grid, "Approve orders")
                {
                }

                protected override MySqlCommand[] CreateCommands()
                {
                    MySqlCommand[] commandArray;
                    using (DialogApproveParameters parameters = new DialogApproveParameters())
                    {
                        if (parameters.ShowDialog() != DialogResult.OK)
                        {
                            commandArray = new MySqlCommand[0];
                        }
                        else
                        {
                            MySqlCommand[] commandArray2 = new MySqlCommand[2];
                            if (!parameters.SetDateOfService)
                            {
                                commandArray2[0] = null;
                            }
                            else
                            {
                                commandArray2[0] = new MySqlCommand();
                                commandArray2[0].CommandType = CommandType.Text;
                                commandArray2[0].CommandText = "CALL order_update_dos(:ID, :DOSFrom)";
                                commandArray2[0].Parameters.Add(new MySqlParameter(":ID", MySqlType.Int));
                                MySqlParameter parameter1 = new MySqlParameter(":DOSFrom", MySqlType.Date);
                                parameter1.Value = parameters.DateOfService;
                                commandArray2[0].Parameters.Add(parameter1);
                            }
                            StringBuilder builder = new StringBuilder();
                            builder.AppendLine("UPDATE tbl_order");
                            builder.AppendLine("SET Approved = 1");
                            if (parameters.SetBillDate_IfEmpty & parameters.SetBillDate_IfNotEmpty)
                            {
                                builder.AppendLine("  , BillDate = :BillDate");
                            }
                            else if (parameters.SetBillDate_IfEmpty & !parameters.SetBillDate_IfNotEmpty)
                            {
                                builder.AppendLine("  , BillDate = CASE WHEN BillDate IS NULL THEN :BillDate ELSE BillDate END");
                            }
                            else if (!parameters.SetBillDate_IfEmpty & parameters.SetBillDate_IfNotEmpty)
                            {
                                builder.AppendLine("  , BillDate = CASE WHEN BillDate IS NULL THEN BillDate ELSE :BillDate END");
                            }
                            if (parameters.SetDeliveryDate_IfEmpty & parameters.SetDeliveryDate_IfNotEmpty)
                            {
                                builder.AppendLine("  , DeliveryDate = :DeliveryDate");
                            }
                            else if (parameters.SetDeliveryDate_IfEmpty & !parameters.SetDeliveryDate_IfNotEmpty)
                            {
                                builder.AppendLine("  , DeliveryDate = CASE WHEN DeliveryDate IS NULL THEN :DeliveryDate ELSE DeliveryDate END");
                            }
                            else if (!parameters.SetDeliveryDate_IfEmpty & parameters.SetDeliveryDate_IfNotEmpty)
                            {
                                builder.AppendLine("  , DeliveryDate = CASE WHEN DeliveryDate IS NULL THEN DeliveryDate ELSE :DeliveryDate END");
                            }
                            builder.AppendLine("WHERE ID = :ID");
                            builder.AppendLine("  AND Approved = 0");
                            commandArray2[1] = new MySqlCommand();
                            commandArray2[1].CommandType = CommandType.Text;
                            commandArray2[1].CommandText = builder.ToString();
                            commandArray2[1].Parameters.Add(new MySqlParameter(":ID", MySqlType.Int));
                            MySqlParameter parameter2 = new MySqlParameter(":BillDate", MySqlType.Date);
                            parameter2.Value = parameters.BillDate;
                            commandArray2[1].Parameters.Add(parameter2);
                            MySqlParameter parameter3 = new MySqlParameter(":DeliveryDate", MySqlType.Date);
                            parameter3.Value = parameters.DeliveryDate;
                            commandArray2[1].Parameters.Add(parameter3);
                            commandArray = commandArray2;
                        }
                    }
                    return commandArray;
                }

                protected override string GetUserState(int[] affected) => 
                    $"{affected[1]} orders approved";
            }

            public class Archive : FormOrder.SearchGridProcessor
            {
                public Archive(GridBase grid) : base(grid, "Archive orders")
                {
                }

                protected override MySqlCommand[] CreateCommands()
                {
                    MySqlCommand[] commandArray1 = new MySqlCommand[] { new MySqlCommand() };
                    commandArray1[0].CommandType = CommandType.Text;
                    commandArray1[0].CommandText = "UPDATE tbl_order SET Archived = 1\r\nWHERE IFNULL(Archived, 0) != 1\r\n  AND ID = :ID\r\n  AND Approved = 1\r\n  AND NOT EXISTS (SELECT * FROM tbl_orderdetails\r\n                  WHERE OrderID = :ID\r\n                    AND State NOT IN ('Closed', 'Canceled'))";
                    commandArray1[0].Parameters.Add(new MySqlParameter(":ID", MySqlType.Int));
                    return commandArray1;
                }

                protected override string GetUserState(int[] affected) => 
                    $"{affected[0]} orders archived";
            }

            public class Unarchive : FormOrder.SearchGridProcessor
            {
                public Unarchive(GridBase grid) : base(grid, "Unarchive orders")
                {
                }

                protected override MySqlCommand[] CreateCommands()
                {
                    MySqlCommand[] commandArray1 = new MySqlCommand[] { new MySqlCommand() };
                    commandArray1[0].CommandType = CommandType.Text;
                    commandArray1[0].CommandText = "UPDATE tbl_order SET Archived = 0\r\nWHERE IFNULL(Archived, 0) != 0\r\n  AND ID = :ID";
                    commandArray1[0].Parameters.Add(new MySqlParameter(":ID", MySqlType.Int));
                    return commandArray1;
                }

                protected override string GetUserState(int[] affected) => 
                    $"{affected[0]} orders unarchived";
            }
        }

        private class SearchNavigatorEventsHandler : NavigatorEventsHandler
        {
            private readonly FormOrder Form;

            public SearchNavigatorEventsHandler(FormOrder form)
            {
                if (form == null)
                {
                    throw new ArgumentNullException("form");
                }
                this.Form = form;
            }

            private static void CellFormatting(object sender, GridCellFormattingEventArgs e)
            {
                try
                {
                    FormOrder.OrderInfo info = e.Row.Get<FormOrder.OrderInfo>();
                    if ((info != null) && !info.Approved)
                    {
                        e.CellStyle.ForeColor = Color.Blue;
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
            }

            public override void CreateSource(object sender, CreateSourceEventArgs args)
            {
                args.Source = new List<FormOrder.OrderInfo>().ToGridSource<FormOrder.OrderInfo>(new string[0]);
            }

            private IEnumerable<FormOrder.OrderInfo> FetchData(PagedFilter SearchTerms)
            {
                IEnumerable<FormOrder.OrderInfo> enumerable;
                FormOrder.QueryOptions options1 = new FormOrder.QueryOptions();
                options1.FilterLocationID = Globals.LocationID;
                options1.FilterUserID = new short?(Globals.CompanyUserID);
                options1.FilterArchived = this.Form.ArchivedFilter;
                options1.FilterApproved = this.Form.ApprovedFilter;
                options1.FilterReoccuring = this.Form.ReoccuringFilter;
                options1.SearchTerms = SearchTerms;
                FormOrder.QueryOptions options = options1;
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    using (MySqlCommand command = new MySqlCommand(GetQuery(options), connection))
                    {
                        connection.Open();
                        enumerable = command.ExecuteList<FormOrder.OrderInfo>((_Closure$__.$I9-0 == null) ? (_Closure$__.$I9-0 = new Func<IDataRecord, FormOrder.OrderInfo>(_Closure$__.$I._Lambda$__9-0)) : _Closure$__.$I9-0);
                    }
                }
                return enumerable;
            }

            public override void FillSource(object sender, FillSourceEventArgs args)
            {
                ((ArrayGridSource<FormOrder.OrderInfo>) args.Source).AppendRange(this.FetchData(null));
            }

            public override void FillSource(object sender, PagedFillSourceEventArgs args)
            {
                ((ArrayGridSource<FormOrder.OrderInfo>) args.Source).AppendRange(this.FetchData(args.Filter));
            }

            public static string GetQuery(FormOrder.QueryOptions Options)
            {
                string str = FormOrder.IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1";
                string str2 = (Options.FilterLocationID == null) ? ((Options.FilterUserID == null) ? "1 = 1" : ("tbl_order.LocationID IS NULL OR tbl_order.LocationID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + Options.FilterUserID.Value.ToString() + ")")) : ("tbl_order.LocationID = " + Options.FilterLocationID.Value.ToString());
                string str3 = (Options.FilterArchived != YesNoAny.Yes) ? ((Options.FilterArchived != YesNoAny.No) ? "1 = 1" : "tbl_order.Archived = 0") : "tbl_order.Archived = 1";
                string str4 = (Options.FilterApproved != YesNoAny.Yes) ? ((Options.FilterApproved != YesNoAny.No) ? "1 = 1" : "tbl_order.Approved = 0") : "tbl_order.Approved = 1";
                string str5 = !Options.FilterReoccuring ? "1 = 1" : "EXISTS (SELECT * FROM tbl_orderdetails WHERE CustomerID = tbl_order.CustomerID AND OrderID = tbl_order.ID AND SaleRentType = 'Re-occurring Sale')";
                StringBuilder builder = new StringBuilder();
                string[] textArray1 = new string[11];
                textArray1[0] = "SELECT\r\n  tbl_order.ID\r\n, tbl_customer.LastName\r\n, tbl_customer.FirstName\r\n, tbl_customer.DateOfBirth\r\n, tbl_customer.AccountNumber\r\n, tbl_order.OrderDate\r\n, tbl_order.BillDate\r\n, tbl_order.DeliveryDate\r\n, tbl_order.Approved\r\n, tbl_order.TakenBy\r\n, tbl_order.TakenAt\r\n, tbl_shippingmethod.Name as ShippingMethod\r\nFROM tbl_order\r\n     LEFT JOIN tbl_customer ON tbl_order.CustomerID = tbl_customer.ID\r\n     LEFT JOIN tbl_company ON tbl_company.ID = 1\r\n     LEFT JOIN tbl_shippingmethod ON tbl_order.ShippingMethodID = tbl_shippingmethod.ID\r\nWHERE (";
                textArray1[1] = str3;
                textArray1[2] = ")\r\n  AND (";
                textArray1[3] = str4;
                textArray1[4] = ")\r\n  AND (";
                textArray1[5] = str;
                textArray1[6] = ")\r\n  AND (";
                textArray1[7] = str2;
                textArray1[8] = ")\r\n  AND (";
                textArray1[9] = str5;
                textArray1[10] = ")\r\n  AND ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))\r\n";
                builder.Append(string.Concat(textArray1));
                if ((Options.SearchTerms != null) && !string.IsNullOrEmpty(Options.SearchTerms.Filter))
                {
                    QueryExpression[] expressions = new QueryExpression[11];
                    expressions[0] = new QueryExpression("tbl_order.ID", MySqlType.Int);
                    expressions[1] = new QueryExpression("tbl_customer.LastName", MySqlType.VarChar);
                    expressions[2] = new QueryExpression("tbl_customer.FirstName", MySqlType.VarChar);
                    expressions[3] = new QueryExpression("tbl_customer.DateOfBirth", MySqlType.Date);
                    expressions[4] = new QueryExpression("tbl_customer.AccountNumber", MySqlType.VarChar);
                    expressions[5] = new QueryExpression("tbl_order.OrderDate", MySqlType.Date);
                    expressions[6] = new QueryExpression("tbl_order.BillDate", MySqlType.Date);
                    expressions[7] = new QueryExpression("tbl_order.DeliveryDate", MySqlType.Date);
                    expressions[8] = new QueryExpression("tbl_order.TakenBy", MySqlType.VarChar);
                    expressions[9] = new QueryExpression("tbl_order.TakenAt", MySqlType.Date);
                    expressions[10] = new QueryExpression("tbl_shippingmethod.Name", MySqlType.VarChar);
                    string str6 = MySqlFilterUtilities.BuildFilter(expressions, Options.SearchTerms.Filter);
                    if (!string.IsNullOrEmpty(str6))
                    {
                        builder.Append("  AND (").Append(str6).Append(")\r\n");
                    }
                }
                builder.Append("ORDER BY tbl_order.ID DESC\r\n");
                if (Options.SearchTerms != null)
                {
                    builder.AppendFormat("LIMIT {0}, {1}", Options.SearchTerms.Start, Options.SearchTerms.Count);
                }
                return builder.ToString();
            }

            public override void InitializeAppearance(GridAppearanceBase Appearance)
            {
                Appearance.AutoGenerateColumns = false;
                Appearance.Columns.Clear();
                Appearance.MultiSelect = true;
                Appearance.AddTextColumn("ID", "Order#", 50, Appearance.IntegerStyle());
                Appearance.AddTextColumn("LastName", "Last name", 80);
                Appearance.AddTextColumn("FirstName", "First name", 80);
                Appearance.AddTextColumn("DateOfBirth", "Birthday", 80, Appearance.DateStyle());
                Appearance.AddTextColumn("AccountNumber", "Account#", 60);
                Appearance.AddTextColumn("OrderDate", "Order date", 80, Appearance.DateStyle());
                Appearance.AddTextColumn("BillDate", "Bill date", 80, Appearance.DateStyle());
                Appearance.AddTextColumn("DeliveryDate", "Delivery", 80);
                Appearance.AddTextColumn("TakenBy", "Ordered By", 80);
                Appearance.AddTextColumn("TakenAt", "Ordered At", 100, Appearance.DateTimeStyle());
                Appearance.AddTextColumn("ShippingMethod", "Shipping", 80);
                Appearance.ContextMenuStrip = this.Form.cmsGridSearch;
                Appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(FormOrder.SearchNavigatorEventsHandler.CellFormatting);
            }

            public override void NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
            {
                _Closure$__5-0 e$__- = new _Closure$__5-0 {
                    $VB$Local_args = args
                };
                this.Form.OpenObject(new Func<object>(e$__-._Lambda$__0));
            }

            public override IEnumerable<string> TableNames =>
                new string[] { "tbl_order", "tbl_customer", "tbl_company", "tbl_shippingmethod", "tbl_user_location", "tbl_orderdetails" };

            [Serializable, CompilerGenerated]
            internal sealed class _Closure$__
            {
                public static readonly FormOrder.SearchNavigatorEventsHandler._Closure$__ $I = new FormOrder.SearchNavigatorEventsHandler._Closure$__();
                public static Func<IDataRecord, FormOrder.OrderInfo> $I9-0;

                internal FormOrder.OrderInfo _Lambda$__9-0(IDataRecord r) => 
                    new FormOrder.OrderInfo(r);
            }

            [CompilerGenerated]
            internal sealed class _Closure$__5-0
            {
                public NavigatorRowClickEventArgs $VB$Local_args;

                internal object _Lambda$__0() => 
                    this.$VB$Local_args.GridRow.Get<FormOrder.OrderInfo>().ID;
            }
        }
    }
}

