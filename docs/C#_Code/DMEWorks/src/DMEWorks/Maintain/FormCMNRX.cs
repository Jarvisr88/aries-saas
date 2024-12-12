namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.CMN;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using My;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonDelete=false, ButtonMissing=true)]
    public class FormCMNRX : FormAutoIncrementMaintain
    {
        private IContainer components;
        private bool Customer_UsingICD10;
        private Control_CMNBase FControl;
        private readonly CMNDetailsTable FDetails;
        private string F_MIR;

        public FormCMNRX()
        {
            base.Load += new EventHandler(this.FormCMNRX_Load);
            this.Customer_UsingICD10 = true;
            this.FDetails = new CMNDetailsTable();
            this.InitializeComponent();
            this.Control_Header1.cmbCustomer.SelectedIndexChanged += new EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.Control_Header1.cmbDoctor.SelectedIndexChanged += new EventHandler(this.cmbDoctor_SelectedIndexChanged);
            this.Control_Header1.cmbFacility.SelectedIndexChanged += new EventHandler(this.cmbFacility_SelectedIndexChanged);
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_cmnform", "tbl_customer", "tbl_doctor", "tbl_company" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
        }

        private void chbApproved_CheckedChanged(object sender, EventArgs e)
        {
            base.OnObjectChanged(sender);
        }

        protected void Clear_cmnform_details()
        {
            this.Control_Footer1.GridDetails.DataSource = null;
            try
            {
                this.FDetails.Clear();
                this.FDetails.AcceptChanges();
            }
            finally
            {
                this.Control_Footer1.GridDetails.DataSource = this.FDetails;
            }
        }

        private void ClearCustomer(bool Recursive)
        {
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerHICN, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerCity, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerState, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerZip, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerPhone, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCustomerGender, DBNull.Value);
            Functions.SetDateBoxValue(this.Control_Header1.dtbCustomerDateofBirth, DBNull.Value);
            Functions.SetNumericBoxValue(this.Control_Header1.nmbCustomer_Height, DBNull.Value);
            Functions.SetNumericBoxValue(this.Control_Header1.nmbCustomer_Weight, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyName, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyCity, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyState, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyZip, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyPhone, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtCompanyAccount, DBNull.Value);
            if (Recursive)
            {
                this.Customer_UsingICD10 = true;
                this.Load_Table_ICD9or10();
                this.Control_Header1.eddICD9_1.Text = "";
                this.Control_Header1.eddICD9_2.Text = "";
                this.Control_Header1.eddICD9_3.Text = "";
                this.Control_Header1.eddICD9_4.Text = "";
                Functions.SetComboBoxValue(this.Control_Header1.cmbPOSType, DBNull.Value);
                Functions.SetComboBoxValue(this.Control_Header1.cmbDoctor, DBNull.Value);
                this.ClearDoctor();
                Functions.SetComboBoxValue(this.Control_Header1.cmbFacility, DBNull.Value);
                this.ClearFacility();
            }
        }

        private void ClearDoctor()
        {
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorAccount, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorAccount, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorCity, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorState, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorZip, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtDoctorPhone, DBNull.Value);
        }

        private void ClearFacility()
        {
            Functions.SetTextBoxText(this.Control_Header1.txtFacilityAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtFacilityAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtFacilityCity, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtFacilityState, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Header1.txtFacilityZip, DBNull.Value);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            this.F_MIR = "";
            Functions.SetDateBoxValue(this.Control_Header1.dtbInitialDate, DBNull.Value);
            Functions.SetDateBoxValue(this.Control_Header1.dtbRevisedDate, DBNull.Value);
            Functions.SetDateBoxValue(this.Control_Header1.dtbRecertification, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbCMNType, DBNull.Value);
            Functions.SetComboBoxValue(this.Control_Header1.cmbCustomer, DBNull.Value);
            this.ClearCustomer(false);
            this.Control_Header1.eddICD9_1.Text = "";
            this.Control_Header1.eddICD9_2.Text = "";
            this.Control_Header1.eddICD9_3.Text = "";
            this.Control_Header1.eddICD9_4.Text = "";
            Functions.SetComboBoxValue(this.Control_Header1.cmbDoctor, DBNull.Value);
            this.ClearDoctor();
            Functions.SetComboBoxValue(this.Control_Header1.cmbPOSType, DBNull.Value);
            Functions.SetComboBoxValue(this.Control_Header1.cmbFacility, DBNull.Value);
            this.ClearFacility();
            Functions.SetTextBoxText(this.Control_Footer1.txtAnsweringName, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Footer1.txtAnsweringTitle, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Footer1.txtAnsweringEmployer, DBNull.Value);
            Functions.SetNumericBoxValue(this.Control_Header1.nmbEstimatedLength, DBNull.Value);
            Functions.SetTextBoxText(this.Control_Footer1.txtSignatureName, DBNull.Value);
            Functions.SetDateBoxValue(this.Control_Footer1.dtbSignatureDate, DBNull.Value);
            Functions.SetCheckBoxChecked(this.Control_Footer1.chbOnFile, DBNull.Value);
            this.Clear_cmnform_details();
            this.Control_Clear();
            this.Control_Initialize(this.cmbCMNType.SelectedValue as string);
        }

        private void cmbCMNType_DrawItem(object sender, DrawItemEventArgs e)
        {
            object item = null;
            if (0 <= e.Index)
            {
                item = this.cmbCMNType.Items[e.Index];
            }
            DmercType type = (DmercType) 0;
            if (item is CmnDescription)
            {
                type = ((CmnDescription) item).Type;
            }
            using (Brush brush = new SolidBrush(e.BackColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            Color foreColor = e.ForeColor;
            if (!string.IsNullOrEmpty(DmercHelper.GetStatus(type)) && ((e.State & DrawItemState.Selected) != DrawItemState.Selected))
            {
                foreColor = Color.DarkRed;
            }
            using (Brush brush2 = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(this.cmbCMNType.GetItemText(item), e.Font, brush2, e.Bounds);
            }
            if (((e.State & DrawItemState.Focus) == DrawItemState.Focus) && ((e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect))
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, e.ForeColor, e.BackColor);
            }
        }

        private void cmbCMNType_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Control_Initialize(this.cmbCMNType.SelectedValue as string);
            base.OnObjectChanged(sender);
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.LoadCustomer(this.Control_Header1.cmbCustomer.SelectedValue, true);
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

        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.LoadDoctor(this.Control_Header1.cmbDoctor.SelectedValue);
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

        private void cmbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.LoadFacility(this.Control_Header1.cmbFacility.SelectedValue);
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

        private void Control_Clear()
        {
            if (this.FControl != null)
            {
                this.FControl.Clear();
            }
        }

        private void Control_Footer1_ValueChanged(object sender, EventArgs e)
        {
            base.OnObjectChanged(sender);
        }

        private void Control_Header1_ValueChanged(object sender, EventArgs e)
        {
            base.OnObjectChanged(sender);
        }

        private void Control_Initialize(string FormName)
        {
            this.PanelScroller.SuspendLayout();
            try
            {
                if (this.FControl != null)
                {
                    if (string.Compare(this.FControl.FormName, FormName, true) != 0)
                    {
                        this.FControl.ValueChanged -= new Control_CMNBase.ValueChangedEventHandler(this.FControl_ValueChanged);
                        this.FControl.Dispose();
                    }
                    else
                    {
                        return;
                    }
                }
                this.FControl = null;
                switch (DmercHelper.String2Dmerc(FormName))
                {
                    case DmercType.DMERC_0102A:
                        this.FControl = new Control_CMN0102A();
                        break;

                    case DmercType.DMERC_0102B:
                        this.FControl = new Control_CMN0102B();
                        break;

                    case DmercType.DMERC_0203A:
                        this.FControl = new Control_CMN0203A();
                        break;

                    case DmercType.DMERC_0203B:
                        this.FControl = new Control_CMN0203B();
                        break;

                    case DmercType.DMERC_0302:
                        this.FControl = new Control_CMN0302();
                        break;

                    case DmercType.DMERC_0403B:
                        this.FControl = new Control_CMN0403B();
                        break;

                    case DmercType.DMERC_0403C:
                        this.FControl = new Control_CMN0403C();
                        break;

                    case DmercType.DMERC_0602B:
                        this.FControl = new Control_CMN0602B();
                        break;

                    case DmercType.DMERC_0702A:
                        this.FControl = new Control_CMN0702A();
                        break;

                    case DmercType.DMERC_0702B:
                        this.FControl = new Control_CMN0702B();
                        break;

                    case DmercType.DMERC_0802:
                        this.FControl = new Control_CMN0802();
                        break;

                    case DmercType.DMERC_0902:
                        this.FControl = new Control_CMN0902();
                        break;

                    case DmercType.DMERC_1002A:
                        this.FControl = new Control_CMN1002A();
                        break;

                    case DmercType.DMERC_1002B:
                        this.FControl = new Control_CMN1002B();
                        break;

                    case DmercType.DMERC_4842:
                        this.FControl = new Control_CMN4842();
                        break;

                    case DmercType.DMERC_DRORDER:
                        this.FControl = new Control_CMNDRORDER();
                        break;

                    case DmercType.DMERC_URO:
                        this.FControl = new Control_CMNURO();
                        break;

                    case DmercType.DME_0404B:
                        this.FControl = new Control_DME0404B();
                        break;

                    case DmercType.DME_0404C:
                        this.FControl = new Control_DME0404C();
                        break;

                    case DmercType.DME_0603B:
                        this.FControl = new Control_DME0603B();
                        break;

                    case DmercType.DME_0703A:
                        this.FControl = new Control_DME0703A();
                        break;

                    case DmercType.DME_0903:
                        this.FControl = new Control_DME0903();
                        break;

                    case DmercType.DME_1003:
                        this.FControl = new Control_DME1003();
                        break;

                    case DmercType.DME_48403:
                        this.FControl = new Control_DME48403();
                        break;

                    default:
                        break;
                }
                if (this.FControl != null)
                {
                    this.FControl.Clear();
                    this.FControl.ValueChanged += new Control_CMNBase.ValueChangedEventHandler(this.FControl_ValueChanged);
                    this.PanelScroller.Controls.Add(this.FControl);
                    this.FControl.Dock = DockStyle.Top;
                    this.FControl.Name = "FControl";
                    this.FControl.Location = new Point(0, this.Control_Header1.Height);
                    this.FControl.TabIndex = 1;
                    this.PanelScroller.Controls.SetChildIndex(this.FControl, Math.Max(this.PanelScroller.Controls.GetChildIndex(this.Control_Header1), this.PanelScroller.Controls.GetChildIndex(this.Control_Footer1)));
                }
            }
            finally
            {
                this.PanelScroller.ResumeLayout();
            }
        }

        private void Control_LoadFromDB(MySqlConnection cnn, int CMNFormID)
        {
            if (this.FControl != null)
            {
                this.FControl.LoadFromDB(cnn, CMNFormID);
            }
        }

        private void Control_SaveToDB(MySqlConnection cnn, int CMNFormID)
        {
            if (this.FControl != null)
            {
                this.FControl.SaveToDB(cnn, CMNFormID);
            }
        }

        private void Control_ShowMissingInformation(bool Show)
        {
            if (this.FControl != null)
            {
                this.FControl.ShowMissingInformation(base.MissingData, Show, this.F_MIR);
            }
        }

        public static int CreateCMN(DmercType Type, int OrderID)
        {
            int lastIdentity;
            if (!string.IsNullOrEmpty(DmercHelper.GetStatus(Type)))
            {
                throw new UserNotifyException("Form " + DmercHelper.Dmerc2String(Type) + " cannot be created");
            }
            if (DmercHelper.GetTableName(Type) == "")
            {
                throw new Exception("DMEWorks cannot find appropriate CMN form.");
            }
            string commandText = $"SELECT
  tbl_order.CustomerID
, tbl_order.ICD10_01 as Customer_ICD9_1
, tbl_order.ICD10_02 as Customer_ICD9_2
, tbl_order.ICD10_03 as Customer_ICD9_3
, tbl_order.ICD10_04 as Customer_ICD9_4
, tbl_doctor.ID as DoctorID
, tbl_postype.ID as POSTypeID
, tbl_facility.ID as FacilityID
FROM tbl_order
     LEFT JOIN tbl_doctor ON tbl_doctor.ID = tbl_order.DoctorID
     LEFT JOIN tbl_postype ON tbl_postype.ID = tbl_order.POSTypeID
     LEFT JOIN tbl_facility ON tbl_facility.ID = tbl_order.FacilityID
     LEFT JOIN tbl_company ON tbl_company.ID = 1
WHERE (tbl_order.ID = {OrderID})";
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    using (MySqlCommand command2 = new MySqlCommand(commandText, connection))
                    {
                        using (MySqlDataReader reader = command2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                command.Parameters.Add("CustomerID", MySqlType.Int).Value = reader["CustomerID"];
                                command.Parameters.Add("Customer_UsingICD10", MySqlType.Bit).Value = true;
                                command.Parameters.Add("Customer_ICD9_1", MySqlType.VarChar, 8).Value = reader["Customer_ICD9_1"];
                                command.Parameters.Add("Customer_ICD9_2", MySqlType.VarChar, 8).Value = reader["Customer_ICD9_2"];
                                command.Parameters.Add("Customer_ICD9_3", MySqlType.VarChar, 8).Value = reader["Customer_ICD9_3"];
                                command.Parameters.Add("Customer_ICD9_4", MySqlType.VarChar, 8).Value = reader["Customer_ICD9_4"];
                                command.Parameters.Add("DoctorID", MySqlType.Int).Value = reader["DoctorID"];
                                command.Parameters.Add("FacilityID", MySqlType.Int).Value = reader["FacilityID"];
                                command.Parameters.Add("POSTypeID", MySqlType.Int).Value = reader["POSTypeID"];
                                command.Parameters.Add("OrderID", MySqlType.Int).Value = OrderID;
                            }
                        }
                    }
                    command.Parameters.Add("CMNType", MySqlType.VarChar, 20).Value = DmercHelper.Dmerc2String(Type);
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    command.GenerateInsertCommand("tbl_cmnform");
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        command.Transaction = transaction;
                        if (command.ExecuteNonQuery() < 1)
                        {
                            throw new Exception("Can''not insert record into tbl_cmnform");
                        }
                        command.Parameters.Clear();
                        lastIdentity = command.GetLastIdentity();
                        command.Parameters.Clear();
                        command.CommandText = $"INSERT INTO tbl_cmnform_details
( CMNFormID
, BillingCode
, InventoryItemID
, OrderedQuantity
, OrderedUnits
, BillablePrice
, AllowablePrice
, Period
, Modifier1
, Modifier2
, Modifier3
, Modifier4
, PredefinedTextID)
SELECT
  {lastIdentity} as CMNFormID
, tbl_orderdetails.BillingCode
, tbl_orderdetails.InventoryItemID
, tbl_orderdetails.OrderedQuantity
, tbl_orderdetails.OrderedUnits
, tbl_orderdetails.BillablePrice
, tbl_orderdetails.AllowablePrice
, tbl_orderdetails.BilledWhen
, tbl_orderdetails.Modifier1
, tbl_orderdetails.Modifier2
, tbl_orderdetails.Modifier3
, tbl_orderdetails.Modifier4
, tbl_pricecode_item.PredefinedTextID
FROM tbl_orderdetails
     LEFT JOIN tbl_pricecode_item ON tbl_orderdetails.InventoryItemID = tbl_pricecode_item.InventoryItemID
                                 AND tbl_orderdetails.PriceCodeID = tbl_pricecode_item.PriceCodeID
WHERE (tbl_orderdetails.OrderID = {OrderID})
  AND (tbl_pricecode_item.DefaultCMNType = '{DmercHelper.Dmerc2String(Type)}')";
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        command.CommandText = $"CALL mir_update_cmnform({lastIdentity})";
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception ex = exception1;
                        ProjectData.SetProjectError(ex);
                        Exception exception = ex;
                        transaction.Rollback();
                        throw exception;
                    }
                }
            }
            return lastIdentity;
        }

        public static int CreateCMN(string CMNType, int OrderID) => 
            CreateCMN(DmercHelper.String2Dmerc(CMNType), OrderID);

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

        private void FControl_ValueChanged(object sender, EventArgs e)
        {
            base.OnObjectChanged(sender);
        }

        private void FormCMNRX_Load(object sender, EventArgs e)
        {
            this.Control_Initialize("");
        }

        public static string GetQuery() => 
            $"SELECT
  tbl_cmnform.ID,
  tbl_cmnform.CMNType,
  CASE tbl_cmnform.CMNType
       WHEN 'DMERC 01.02A'  THEN 'HOSPITAL BEDS - eliminated'
       WHEN 'DMERC 01.02B'  THEN 'SUPPORT SURFACES - eliminated'
       WHEN 'DMERC 02.03A'  THEN 'MOTORIZED WHEELCHAIRS'
       WHEN 'DMERC 02.03B'  THEN 'MANUAL WHEELCHAIRS'
       WHEN 'DMERC 03.02'   THEN 'CONTINUOUS POSITIVE AIRWAY PRESSURE (CPAP)'
       WHEN 'DMERC 04.03B'  THEN 'LYMPHEDEMA PUMPS - obsolete'
       WHEN 'DMERC 04.03C'  THEN 'OSTEOGENESIS STIMULATORS - obsolete'
       WHEN 'DMERC 06.02B'  THEN 'TRANSCUTANEOUS ELECTRICAL NERVE STIMULATOR (TENS) - obsolete'
       WHEN 'DMERC 07.02A'  THEN 'SEAT LIFT MECHANISM - obsolete'
       WHEN 'DMERC 07.02B'  THEN 'POWER OPERATED VEHICLE (POV)'
       WHEN 'DMERC 08.02'   THEN 'SUPPORT SURFACES'
       WHEN 'DMERC 09.02'   THEN 'EXTERNAL INFUSION PUMP - obsolete'
       WHEN 'DMERC 10.02A'  THEN 'PARENTERAL NUTRITION - obsolete'
       WHEN 'DMERC 10.02B'  THEN 'ENTERAL NUTRITION - obsolete'
       WHEN 'DMERC 484.2'   THEN 'OXYGEN - obsolete'
       WHEN 'DMERC DRORDER' THEN 'PHYSICIAN''S ORDER'
       WHEN 'DMERC URO'     THEN 'UROLOGICAL CERTIFICATION'
       WHEN 'DME 04.04B'    THEN 'PNEUMATIC COMPRESSION DEVICES'
       WHEN 'DME 04.04C'    THEN 'OSTEOGENESIS STIMULATORS'
       WHEN 'DME 06.03B'    THEN 'TRANSCUTANEOUS ELECTRICAL NERVE STIMULATOR (TENS)'
       WHEN 'DME 07.03A'    THEN 'SEAT LIFT MECHANISMS'
       WHEN 'DME 09.03'     THEN 'EXTERNAL INFUSION PUMPS'
       WHEN 'DME 10.03'     THEN 'ENTERAL AND PARENTERAL NUTRITION'
       WHEN 'DME 484.03'    THEN 'OXYGEN'
       ELSE '' END as Description,
  tbl_cmnform.InitialDate,
  tbl_customer.ID as CustomerID,
  CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as Customer_Name,
  tbl_doctor.ID as DoctorID,
  CONCAT(tbl_doctor.LastName, ', ', tbl_doctor.FirstName) as Doctor_Name
FROM tbl_cmnform
     INNER JOIN tbl_customer ON tbl_cmnform.CustomerID = tbl_customer.ID
     INNER JOIN tbl_doctor ON tbl_cmnform.DoctorID = tbl_doctor.ID
     LEFT JOIN tbl_company ON tbl_company.ID = 1
WHERE ((tbl_company.Show_InactiveCustomers = 1) OR (tbl_customer.InactiveDate IS NULL) OR (Now() < tbl_customer.InactiveDate))
  AND ({IsDemoVersion ? "tbl_customer.ID BETWEEN 1 and 50" : "1 = 1"})";

        public static string GetReportName(DmercType value)
        {
            string str;
            switch (value)
            {
                case DmercType.DMERC_0102A:
                    str = MySettingsProperty.Settings.REPORT_0102A;
                    break;

                case DmercType.DMERC_0102B:
                    str = MySettingsProperty.Settings.REPORT_0102B;
                    break;

                case DmercType.DMERC_0203A:
                    str = MySettingsProperty.Settings.REPORT_0203A;
                    break;

                case DmercType.DMERC_0203B:
                    str = MySettingsProperty.Settings.REPORT_0203B;
                    break;

                case DmercType.DMERC_0302:
                    str = MySettingsProperty.Settings.REPORT_0302;
                    break;

                case DmercType.DMERC_0403B:
                    str = MySettingsProperty.Settings.REPORT_0403B;
                    break;

                case DmercType.DMERC_0403C:
                    str = MySettingsProperty.Settings.REPORT_0403C;
                    break;

                case DmercType.DMERC_0602B:
                    str = MySettingsProperty.Settings.REPORT_0602B;
                    break;

                case DmercType.DMERC_0702A:
                    str = MySettingsProperty.Settings.REPORT_0702A;
                    break;

                case DmercType.DMERC_0702B:
                    str = MySettingsProperty.Settings.REPORT_0702B;
                    break;

                case DmercType.DMERC_0802:
                    str = MySettingsProperty.Settings.REPORT_0802;
                    break;

                case DmercType.DMERC_0902:
                    str = MySettingsProperty.Settings.REPORT_0902;
                    break;

                case DmercType.DMERC_1002A:
                    str = MySettingsProperty.Settings.REPORT_1002A;
                    break;

                case DmercType.DMERC_1002B:
                    str = MySettingsProperty.Settings.REPORT_1002B;
                    break;

                case DmercType.DMERC_4842:
                    str = MySettingsProperty.Settings.REPORT_4842;
                    break;

                case DmercType.DMERC_DRORDER:
                    str = MySettingsProperty.Settings.REPORT_DRORDER;
                    break;

                case DmercType.DMERC_URO:
                    str = MySettingsProperty.Settings.REPORT_URO;
                    break;

                case DmercType.DME_0404B:
                    str = MySettingsProperty.Settings.REPORT_0404B;
                    break;

                case DmercType.DME_0404C:
                    str = MySettingsProperty.Settings.REPORT_0404C;
                    break;

                case DmercType.DME_0603B:
                    str = MySettingsProperty.Settings.REPORT_0603B;
                    break;

                case DmercType.DME_0703A:
                    str = MySettingsProperty.Settings.REPORT_0703A;
                    break;

                case DmercType.DME_0903:
                    str = MySettingsProperty.Settings.REPORT_0903;
                    break;

                case DmercType.DME_1003:
                    str = MySettingsProperty.Settings.REPORT_1003;
                    break;

                case DmercType.DME_48403:
                    str = MySettingsProperty.Settings.REPORT_48403;
                    break;

                default:
                    str = "";
                    break;
            }
            return str;
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.Control_Header1.cmbCustomer, "tbl_customer", null);
            Cache.InitDropdown(this.Control_Header1.cmbDoctor, "tbl_doctor", null);
            Cache.InitDropdown(this.Control_Header1.cmbPOSType, "tbl_postype", null);
            Cache.InitDropdown(this.Control_Header1.cmbFacility, "tbl_facility", null);
            this.Load_Table_ICD9or10();
            this.Load_Combobox_CMNType();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager1 = new ComponentResourceManager(typeof(FormCMNRX));
            this.Panel1 = new Panel();
            this.lblCmnID = new Label();
            this.cmbCMNType = new ComboBox();
            this.lblCMNType = new Label();
            this.PanelScroller = new Panel();
            this.Control_Footer1 = new Control_Footer();
            this.Control_Header1 = new Control_Header();
            this.mnuGotoImages = new MenuItem();
            this.mnuGotoNewImage = new MenuItem();
            base.tpWorkArea.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.PanelScroller.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.AutoScroll = true;
            base.tpWorkArea.Controls.Add(this.PanelScroller);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x330, 0x2a6);
            MenuItem[] items = new MenuItem[] { this.mnuGotoImages, this.mnuGotoNewImage };
            base.cmnuGoto.MenuItems.AddRange(items);
            this.Panel1.Controls.Add(this.lblCmnID);
            this.Panel1.Controls.Add(this.cmbCMNType);
            this.Panel1.Controls.Add(this.lblCMNType);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x330, 0x1c);
            this.Panel1.TabIndex = 0;
            this.lblCmnID.BorderStyle = BorderStyle.FixedSingle;
            this.lblCmnID.Location = new Point(4, 4);
            this.lblCmnID.Name = "lblCmnID";
            this.lblCmnID.Size = new Size(100, 0x15);
            this.lblCmnID.TabIndex = 0;
            this.lblCmnID.Text = "CMN # 00000";
            this.lblCmnID.TextAlign = ContentAlignment.MiddleCenter;
            this.cmbCMNType.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmbCMNType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCMNType.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.cmbCMNType.Location = new Point(0xcc, 4);
            this.cmbCMNType.MaxDropDownItems = 0x10;
            this.cmbCMNType.Name = "cmbCMNType";
            this.cmbCMNType.Size = new Size(0x1be, 0x15);
            this.cmbCMNType.TabIndex = 2;
            this.lblCMNType.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblCMNType.Location = new Point(0x7a, 4);
            this.lblCMNType.Name = "lblCMNType";
            this.lblCMNType.Size = new Size(0x4c, 0x15);
            this.lblCMNType.TabIndex = 1;
            this.lblCMNType.Text = "CMN Type";
            this.lblCMNType.TextAlign = ContentAlignment.MiddleLeft;
            this.PanelScroller.AutoScroll = true;
            this.PanelScroller.Controls.Add(this.Control_Footer1);
            this.PanelScroller.Controls.Add(this.Control_Header1);
            this.PanelScroller.Dock = DockStyle.Fill;
            this.PanelScroller.Location = new Point(0, 0x1c);
            this.PanelScroller.Name = "PanelScroller";
            this.PanelScroller.Size = new Size(0x330, 650);
            this.PanelScroller.TabIndex = 1;
            this.Control_Footer1.Dock = DockStyle.Top;
            this.Control_Footer1.Location = new Point(0, 0x184);
            this.Control_Footer1.Name = "Control_Footer1";
            this.Control_Footer1.Size = new Size(0x31f, 0x150);
            this.Control_Footer1.TabIndex = 13;
            this.Control_Header1.Dock = DockStyle.Top;
            this.Control_Header1.Location = new Point(0, 0);
            this.Control_Header1.Name = "Control_Header1";
            this.Control_Header1.Size = new Size(0x31f, 0x184);
            this.Control_Header1.TabIndex = 0;
            this.mnuGotoImages.Index = 0;
            this.mnuGotoImages.Text = "Images";
            this.mnuGotoNewImage.Index = 1;
            this.mnuGotoNewImage.Text = "New Image";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x338, 0x2f1);
            base.Name = "FormCMNRX";
            this.Text = "CMN Form";
            base.tpWorkArea.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.PanelScroller.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu {
                MenuItems = { new MenuItem("Form", new EventHandler(this.mnuPrintForm_Click)) }
            };
            Cache.AddCategory(menu, "CMN/RX", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        protected void Load_cmnform_details(MySqlConnection cnn, int CMNFormID)
        {
            string selectCommandText = $"SELECT tbl_inventoryitem.Name as InventoryItemName,
       tbl_inventoryitem.ID as InventoryItemID,
       tbl_cmnform_details.BillingCode as BillingCode,
       tbl_cmnform_details.OrderedQuantity, 
       tbl_cmnform_details.BillablePrice, 
       tbl_cmnform_details.AllowablePrice,
       tbl_cmnform_details.Period,
       tbl_cmnform_details.Modifier1,
       tbl_cmnform_details.Modifier2,
       tbl_cmnform_details.Modifier3,
       tbl_cmnform_details.Modifier4
FROM (tbl_cmnform_details
      INNER JOIN tbl_inventoryitem ON tbl_cmnform_details.InventoryItemID = tbl_inventoryitem.ID)
WHERE (tbl_cmnform_details.CMNFormID = {CMNFormID})";
            this.Control_Footer1.GridDetails.DataSource = null;
            try
            {
                this.FDetails.Clear();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommandText, cnn))
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.Ignore;
                    adapter.Fill(this.FDetails);
                }
                this.FDetails.AcceptChanges();
            }
            finally
            {
                this.Control_Footer1.GridDetails.DataSource = this.FDetails;
            }
        }

        private void Load_Combobox_CMNType()
        {
            IEnumerator enumerator;
            List<CmnDescription> list = new List<CmnDescription>();
            try
            {
                enumerator = Enum.GetValues(typeof(DmercType)).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DmercType type = (DmercType) Conversions.ToInteger(enumerator.Current);
                    list.Add(new CmnDescription(type));
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            list.Sort(new Comparison<CmnDescription>(CmnDescription.Compare));
            this.cmbCMNType.DataSource = list;
            this.cmbCMNType.DisplayMember = "Description";
            this.cmbCMNType.ValueMember = "DbType";
        }

        private void Load_Table_ICD9or10()
        {
            string tablename = this.Customer_UsingICD10 ? "tbl_icd10" : "tbl_icd9";
            Cache.InitDropdown(this.Control_Header1.eddICD9_1, tablename, null);
            Cache.InitDropdown(this.Control_Header1.eddICD9_2, tablename, null);
            Cache.InitDropdown(this.Control_Header1.eddICD9_3, tablename, null);
            Cache.InitDropdown(this.Control_Header1.eddICD9_4, tablename, null);
        }

        private void LoadCustomer(object CustomerID, bool Recursive)
        {
            string commandText = "SELECT\r\n  cust.Address1 as CustomerAddress1\r\n, cust.Address2 as CustomerAddress2\r\n, cust.City     as CustomerCity\r\n, cust.State    as CustomerState\r\n, cust.Zip      as CustomerZip\r\n, cust.Phone    as CustomerPhone\r\n, pol.PolicyNumber as CustomerHICN\r\n, cust.DateofBirth as CustomerDateofBirth\r\n, cust.Doctor1_ID  as CustomerDoctor1_ID\r\n, cust.FacilityID  as CustomerFacilityID\r\n, cust.POSTypeID   as CustomerPOSTypeID\r\n, cust.ICD10_01 as CustomerICD9_1\r\n, cust.ICD10_02 as CustomerICD9_2\r\n, cust.ICD10_03 as CustomerICD9_3\r\n, cust.ICD10_04 as CustomerICD9_4\r\n, cust.Gender as CustomerGender\r\n, cust.Height as CustomerHeight\r\n, cust.Weight as CustomerWeight\r\n, IF(comp.IncludeLocationInfo = 1 and loc.ID is not null, loc.Name    , comp.Name    ) as CompanyName    \r\n, IF(comp.IncludeLocationInfo = 1 and loc.ID is not null, loc.Address1, comp.Address1) as CompanyAddress1\r\n, IF(comp.IncludeLocationInfo = 1 and loc.ID is not null, loc.Address2, comp.Address2) as CompanyAddress2\r\n, IF(comp.IncludeLocationInfo = 1 and loc.ID is not null, loc.City    , comp.City    ) as CompanyCity    \r\n, IF(comp.IncludeLocationInfo = 1 and loc.ID is not null, loc.State   , comp.State   ) as CompanyState   \r\n, IF(comp.IncludeLocationInfo = 1 and loc.ID is not null, loc.Zip     , comp.Zip     ) as CompanyZip     \r\n, IF(comp.IncludeLocationInfo = 1 and loc.ID is not null, loc.Phone   , comp.Phone   ) as CompanyPhone   \r\n, prov.ProviderNumber\r\nFROM tbl_customer as cust\r\n     LEFT JOIN tbl_location as loc ON cust.LocationID = loc.ID\r\n     LEFT JOIN tbl_customer_insurance as pol ON cust.ID = pol.CustomerID\r\n                                            AND ((pol.`Rank` IS NULL) OR (pol.`Rank` = 1))\r\n     LEFT JOIN tbl_provider as prov ON prov.LocationID = loc.ID\r\n                                   AND prov.InsuranceCompanyID = pol.InsuranceCompanyID\r\n     LEFT JOIN tbl_company as comp ON comp.ID = 1\r\nWHERE (1 = 1)";
            commandText = !Versioned.IsNumeric(CustomerID) ? (commandText + "\r\n  AND (1 <> 1)") : (commandText + "\r\n" + $"  AND (cust.ID = {CustomerID})");
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(commandText, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            this.ClearCustomer(Recursive);
                        }
                        else
                        {
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerHICN, reader["CustomerHICN"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerAddress1, reader["CustomerAddress1"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerAddress2, reader["CustomerAddress2"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerCity, reader["CustomerCity"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerState, reader["CustomerState"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerZip, reader["CustomerZip"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerPhone, reader["CustomerPhone"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCustomerGender, reader["CustomerGender"]);
                            Functions.SetDateBoxValue(this.Control_Header1.dtbCustomerDateofBirth, reader["CustomerDateofBirth"]);
                            Functions.SetNumericBoxValue(this.Control_Header1.nmbCustomer_Height, reader["CustomerHeight"]);
                            Functions.SetNumericBoxValue(this.Control_Header1.nmbCustomer_Weight, reader["CustomerWeight"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyName, reader["CompanyName"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyAddress1, reader["CompanyAddress1"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyAddress2, reader["CompanyAddress2"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyCity, reader["CompanyCity"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyState, reader["CompanyState"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyZip, reader["CompanyZip"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyPhone, reader["CompanyPhone"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtCompanyAccount, reader["ProviderNumber"]);
                            if (Recursive)
                            {
                                this.Customer_UsingICD10 = true;
                                this.Load_Table_ICD9or10();
                                this.Control_Header1.eddICD9_1.Text = NullableConvert.ToString(reader["CustomerICD9_1"]);
                                this.Control_Header1.eddICD9_2.Text = NullableConvert.ToString(reader["CustomerICD9_2"]);
                                this.Control_Header1.eddICD9_3.Text = NullableConvert.ToString(reader["CustomerICD9_3"]);
                                this.Control_Header1.eddICD9_4.Text = NullableConvert.ToString(reader["CustomerICD9_4"]);
                                Functions.SetComboBoxValue(this.Control_Header1.cmbPOSType, reader["CustomerPOSTypeID"]);
                                Functions.SetComboBoxValue(this.Control_Header1.cmbDoctor, reader["CustomerDoctor1_ID"]);
                                this.LoadDoctor(reader["CustomerDoctor1_ID"]);
                                Functions.SetComboBoxValue(this.Control_Header1.cmbFacility, reader["CustomerFacilityID"]);
                                this.LoadFacility(reader["CustomerFacilityID"]);
                            }
                        }
                    }
                }
            }
        }

        private void LoadDoctor(object DoctorID)
        {
            string commandText = !Versioned.IsNumeric(DoctorID) ? "SELECT * FROM tbl_doctor WHERE (1 <> 1)" : $"SELECT * FROM tbl_doctor WHERE (ID = {Conversions.ToInteger(DoctorID)})";
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(commandText, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            this.ClearDoctor();
                        }
                        else
                        {
                            Functions.SetTextBoxText(this.Control_Header1.txtDoctorAccount, reader["UPINNumber"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtDoctorAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtDoctorAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtDoctorCity, reader["City"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtDoctorState, reader["State"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtDoctorZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtDoctorPhone, reader["Phone"]);
                        }
                    }
                }
            }
        }

        private void LoadFacility(object FacilityID)
        {
            string commandText = !Versioned.IsNumeric(FacilityID) ? "SELECT * FROM tbl_facility WHERE (1 <> 1)" : $"SELECT * FROM tbl_facility WHERE (ID = {Conversions.ToInteger(FacilityID)})";
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(commandText, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            this.ClearFacility();
                        }
                        else
                        {
                            Functions.SetTextBoxText(this.Control_Header1.txtFacilityAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtFacilityAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtFacilityCity, reader["City"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtFacilityState, reader["State"]);
                            Functions.SetTextBoxText(this.Control_Header1.txtFacilityZip, reader["Zip"]);
                        }
                    }
                }
            }
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                bool flag;
                object obj2;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_cmnform WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            this.F_MIR = NullableConvert.ToString(reader["MIR"]);
                            Functions.SetDateBoxValue(this.Control_Header1.dtbInitialDate, reader["InitialDate"]);
                            Functions.SetDateBoxValue(this.Control_Header1.dtbRevisedDate, reader["RevisedDate"]);
                            Functions.SetDateBoxValue(this.Control_Header1.dtbRecertification, reader["RecertificationDate"]);
                            Functions.SetComboBoxValue(this.cmbCMNType, reader["CMNType"]);
                            obj2 = reader["CustomerID"];
                            Functions.SetComboBoxValue(this.Control_Header1.cmbCustomer, obj2);
                            this.Customer_UsingICD10 = NullableConvert.ToBoolean(reader["Customer_UsingICD10"]).GetValueOrDefault(false);
                            this.Control_Header1.eddICD9_1.Text = NullableConvert.ToString(reader["Customer_ICD9_1"]);
                            this.Control_Header1.eddICD9_2.Text = NullableConvert.ToString(reader["Customer_ICD9_2"]);
                            this.Control_Header1.eddICD9_3.Text = NullableConvert.ToString(reader["Customer_ICD9_3"]);
                            this.Control_Header1.eddICD9_4.Text = NullableConvert.ToString(reader["Customer_ICD9_4"]);
                            Functions.SetComboBoxValue(this.Control_Header1.cmbDoctor, reader["DoctorID"]);
                            this.LoadDoctor(reader["DoctorID"]);
                            Functions.SetComboBoxValue(this.Control_Header1.cmbPOSType, reader["POSTypeID"]);
                            Functions.SetComboBoxValue(this.Control_Header1.cmbFacility, reader["FacilityID"]);
                            this.LoadFacility(reader["FacilityID"]);
                            Functions.SetTextBoxText(this.Control_Footer1.txtAnsweringName, reader["AnsweringName"]);
                            Functions.SetTextBoxText(this.Control_Footer1.txtAnsweringTitle, reader["AnsweringTitle"]);
                            Functions.SetTextBoxText(this.Control_Footer1.txtAnsweringEmployer, reader["AnsweringEmployer"]);
                            Functions.SetNumericBoxValue(this.Control_Header1.nmbEstimatedLength, reader["EstimatedLengthOfNeed"]);
                            Functions.SetTextBoxText(this.Control_Footer1.txtSignatureName, reader["Signature_Name"]);
                            Functions.SetDateBoxValue(this.Control_Footer1.dtbSignatureDate, reader["Signature_Date"]);
                            Functions.SetCheckBoxChecked(this.Control_Footer1.chbOnFile, reader["OnFile"]);
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
                this.LoadCustomer(obj2, false);
                this.Load_cmnform_details(connection, ID);
                this.Control_Initialize(this.cmbCMNType.SelectedValue as string);
                this.Control_LoadFromDB(connection, ID);
                this.Load_Table_ICD9or10();
                return true;
            }
        }

        private void mnuGotoImages_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["DoctorID"] = this.Control_Header1.cmbDoctor.SelectedValue,
                ["CustomerID"] = this.Control_Header1.cmbCustomer.SelectedValue,
                ["CMNFormID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImageSearch(), @params);
        }

        private void mnuGotoNewImage_Click(object sender, EventArgs e)
        {
            FormParameters @params = new FormParameters {
                ["DoctorID"] = this.Control_Header1.cmbDoctor.SelectedValue,
                ["CustomerID"] = this.Control_Header1.cmbCustomer.SelectedValue,
                ["CMNFormID"] = this.ObjectID
            };
            ClassGlobalObjects.ShowForm(FormFactories.FormImage(), @params);
        }

        private void mnuPrintForm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Versioned.IsNumeric(this.ObjectID))
                {
                    if (MessageBox.Show("Current CMN form was not saved. In order to print CMN form it should be saved. Whould you like to save form and print report?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        base.DoSaveClick();
                    }
                    else
                    {
                        return;
                    }
                }
                string reportName = GetReportName(DmercHelper.String2Dmerc(this.cmbCMNType.SelectedValue as string));
                if (reportName == "")
                {
                    throw new UserNotifyException($"You should select correct CMNRX form type. CMNType = {this.cmbCMNType.SelectedValue}");
                }
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_cmnform.ID}"] = this.ObjectID
                };
                ClassGlobalObjects.ShowReport(reportName, @params);
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

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_cmnform.ID}"] = this.ObjectID
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
            string[] tableNames = new string[] { "tbl_cmnform" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("CMNType", MySqlType.VarChar, 20).Value = this.cmbCMNType.SelectedValue as string;
                    command.Parameters.Add("InitialDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.Control_Header1.dtbInitialDate);
                    command.Parameters.Add("RevisedDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.Control_Header1.dtbRevisedDate);
                    command.Parameters.Add("RecertificationDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.Control_Header1.dtbRecertification);
                    command.Parameters.Add("CustomerID", MySqlType.Int).Value = this.Control_Header1.cmbCustomer.SelectedValue;
                    command.Parameters.Add("Customer_UsingICD10", MySqlType.Bit).Value = this.Customer_UsingICD10;
                    command.Parameters.Add("Customer_ICD9_1", MySqlType.VarChar, 8).Value = this.Control_Header1.eddICD9_1.Text;
                    command.Parameters.Add("Customer_ICD9_2", MySqlType.VarChar, 8).Value = this.Control_Header1.eddICD9_2.Text;
                    command.Parameters.Add("Customer_ICD9_3", MySqlType.VarChar, 8).Value = this.Control_Header1.eddICD9_3.Text;
                    command.Parameters.Add("Customer_ICD9_4", MySqlType.VarChar, 8).Value = this.Control_Header1.eddICD9_4.Text;
                    command.Parameters.Add("DoctorID", MySqlType.Int).Value = this.Control_Header1.cmbDoctor.SelectedValue;
                    command.Parameters.Add("POSTypeID", MySqlType.Int).Value = this.Control_Header1.cmbPOSType.SelectedValue;
                    command.Parameters.Add("FacilityID", MySqlType.Int).Value = this.Control_Header1.cmbFacility.SelectedValue;
                    command.Parameters.Add("AnsweringName", MySqlType.VarChar, 50).Value = this.Control_Footer1.txtAnsweringName.Text;
                    command.Parameters.Add("AnsweringTitle", MySqlType.VarChar, 50).Value = this.Control_Footer1.txtAnsweringTitle.Text;
                    command.Parameters.Add("AnsweringEmployer", MySqlType.VarChar, 50).Value = this.Control_Footer1.txtAnsweringEmployer.Text;
                    command.Parameters.Add("EstimatedLengthOfNeed", MySqlType.Int).Value = this.Control_Header1.nmbEstimatedLength.AsInt32.GetValueOrDefault(0);
                    command.Parameters.Add("Signature_Name", MySqlType.VarChar, 50).Value = this.Control_Footer1.txtSignatureName.Text;
                    command.Parameters.Add("Signature_Date", MySqlType.Date).Value = Functions.GetDateBoxValue(this.Control_Footer1.dtbSignatureDate);
                    command.Parameters.Add("OnFile", MySqlType.Bit).Value = this.Control_Footer1.chbOnFile.Checked;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.Int).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_cmnform", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_cmnform"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_cmnform");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                    this.Control_SaveToDB(connection, ID);
                    command.Parameters.Clear();
                    command.CommandText = $"CALL mir_update_cmnform({ID})";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT MIR FROM tbl_cmnform WHERE ID = " + Conversions.ToString(ID);
                    this.F_MIR = NullableConvert.ToString(command.ExecuteScalar());
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(GetQuery(), ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("CMNType", "CMN Type", 80);
            appearance.AddTextColumn("Description", "Description", 120);
            appearance.AddTextColumn("InitialDate", "Initial Date", 80);
            appearance.AddTextColumn("Customer_Name", "Customer Name", 160);
            appearance.AddTextColumn("Doctor_Name", "Doctor Name", 160);
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
            base.ProcessParameter_ID(Params);
            base.ProcessParameter_ShowMissing(Params);
        }

        protected override void ShowMissingInformation(bool Show)
        {
            this.Control_Header1.ShowMissingInformation(base.MissingData, Show, this.F_MIR);
            this.Control_Footer1.ShowMissingInformation(base.MissingData, Show, this.F_MIR);
            this.FDetails.ShowMissingInformation(Show);
            this.Control_ShowMissingInformation(Show);
        }

        private void tlbMain_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            try
            {
                if (e.Button == null)
                {
                    if (!Versioned.IsNumeric(this.ObjectID))
                    {
                        if (MessageBox.Show("Current CMN form was not saved. In order to print CMN form it should be saved. Whould you like to save form and print report?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            base.DoSaveClick();
                        }
                        else
                        {
                            return;
                        }
                    }
                    string reportName = GetReportName(DmercHelper.String2Dmerc(this.cmbCMNType.SelectedValue as string));
                    if (reportName == "")
                    {
                        throw new UserNotifyException($"You should select correct CMNRX form type. CMNType = {this.cmbCMNType.SelectedValue}");
                    }
                    ReportParameters @params = new ReportParameters {
                        ["{?tbl_cmnform.ID}"] = this.ObjectID
                    };
                    ClassGlobalObjects.ShowReport(reportName, @params);
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

        protected override void ValidateObject(int ID, bool IsNew)
        {
            DmercType type = DmercHelper.String2Dmerc(this.cmbCMNType.SelectedValue as string);
            if (type == ((DmercType) 0))
            {
                base.ValidationErrors.SetError(this.cmbCMNType, "You Must Select CMN Form Type");
            }
            else if (IsNew)
            {
                string status = DmercHelper.GetStatus(type);
                if (!string.IsNullOrEmpty(status))
                {
                    base.ValidationErrors.SetError(this.cmbCMNType, "CMN Form Type is " + status.ToLower());
                }
                else
                {
                    base.ValidationErrors.SetError(this.cmbCMNType, "");
                }
            }
            if (!Versioned.IsNumeric(this.Control_Header1.cmbCustomer.SelectedValue))
            {
                base.ValidationErrors.SetError(this.Control_Header1.cmbCustomer, "You Must Select Customer");
            }
            if (!Versioned.IsNumeric(this.Control_Header1.cmbDoctor.SelectedValue))
            {
                base.ValidationErrors.SetError(this.Control_Header1.cmbDoctor, "You Must Select Doctor");
            }
        }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCMNType")]
        private Label lblCMNType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCMNType")]
        private ComboBox cmbCMNType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbApproved")]
        private CheckBox chbApproved { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PanelScroller")]
        private Panel PanelScroller { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Control_Header1")]
        private Control_Header Control_Header1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Control_Footer1")]
        private Control_Footer Control_Footer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCmnID")]
        private Label lblCmnID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoImages")]
        private MenuItem mnuGotoImages { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoNewImage")]
        private MenuItem mnuGotoNewImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        private static bool IsDemoVersion =>
            Globals.SerialNumber.IsDemoSerial();

        private bool ApprovedState
        {
            get => 
                !this.chbApproved.Enabled;
            set
            {
                this.chbApproved.Enabled = !value;
                this.cmbCMNType.Enabled = !value;
                this.Control_Header1.cmbCustomer.Enabled = !value;
                this.Control_Header1.cmbDoctor.Enabled = !value;
                this.Control_Header1.cmbFacility.Enabled = !value;
                this.Control_Header1.cmbPOSType.Enabled = !value;
            }
        }

        protected override object ObjectID
        {
            get => 
                base.ObjectID;
            set
            {
                base.ObjectID = value;
                if (Versioned.IsNumeric(value))
                {
                    this.lblCmnID.Text = "CMN # " + Conversions.ToString(Conversions.ToLong(value));
                }
                else
                {
                    this.lblCmnID.Text = "CMN #";
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
                this.Control_Header1.cmbCustomer.Enabled = value;
                this.cmbCMNType.Enabled = value;
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__50-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }

        public class CMNDetailsTable : DataTable
        {
            public readonly DataColumn Col_InventoryItemID;
            public readonly DataColumn Col_InventoryItemName;
            public readonly DataColumn Col_BillingCode;
            public readonly DataColumn Col_OrderedQuantity;
            public readonly DataColumn Col_BillablePrice;
            public readonly DataColumn Col_AllowablePrice;
            public readonly DataColumn Col_Period;
            public readonly DataColumn Col_Modifier1;
            public readonly DataColumn Col_Modifier2;
            public readonly DataColumn Col_Modifier3;
            public readonly DataColumn Col_Modifier4;

            public CMNDetailsTable()
            {
                this.Col_InventoryItemID = this.AddColumn("InventoryItemID", typeof(int), true);
                this.Col_BillingCode = this.AddColumn("BillingCode", typeof(string), true);
                this.Col_InventoryItemName = this.AddColumn("InventoryItemName", typeof(string), true);
                this.Col_OrderedQuantity = this.AddColumn("OrderedQuantity", typeof(double), true);
                this.Col_BillablePrice = this.AddColumn("BillablePrice", typeof(double), true);
                this.Col_AllowablePrice = this.AddColumn("AllowablePrice", typeof(double), true);
                this.Col_Period = this.AddColumn("Period", typeof(string), true);
                this.Col_Modifier1 = this.AddColumn("Modifier1", typeof(string), true);
                this.Col_Modifier2 = this.AddColumn("Modifier2", typeof(string), true);
                this.Col_Modifier3 = this.AddColumn("Modifier3", typeof(string), true);
                this.Col_Modifier4 = this.AddColumn("Modifier4", typeof(string), true);
            }

            protected DataColumn AddColumn(string name, System.Type type, bool AllowDBNull = true)
            {
                DataColumn column1 = base.Columns.Add(name, type);
                column1.AllowDBNull = AllowDBNull;
                return column1;
            }

            public void ShowMissingInformation(bool Show)
            {
                int num2 = base.Rows.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    DataRow row = base.Rows[i];
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (Show && ((row[this.Col_BillingCode] == DBNull.Value) || (Conversions.ToString(row[this.Col_BillingCode]) == "")))
                        {
                            row.SetColumnError(this.Col_BillingCode, "Billing Code is required");
                        }
                        else
                        {
                            row.SetColumnError(this.Col_BillingCode, "");
                        }
                        if (Show && (row[this.Col_InventoryItemID] == DBNull.Value))
                        {
                            row.SetColumnError(this.Col_InventoryItemID, "Inventory Item is required");
                            row.SetColumnError(this.Col_InventoryItemName, "Inventory Item is required");
                        }
                        else
                        {
                            row.SetColumnError(this.Col_InventoryItemID, "");
                            row.SetColumnError(this.Col_InventoryItemName, "");
                        }
                        if (Show && ((row[this.Col_OrderedQuantity] == DBNull.Value) || (Conversions.ToDouble(row[this.Col_OrderedQuantity]) == 0.0)))
                        {
                            row.SetColumnError(this.Col_OrderedQuantity, "Ordered Quantity is required");
                        }
                        else
                        {
                            row.SetColumnError(this.Col_OrderedQuantity, "");
                        }
                        if (Show && ((row[this.Col_BillablePrice] == DBNull.Value) || (Conversions.ToDouble(row[this.Col_BillablePrice]) == 0.0)))
                        {
                            row.SetColumnError(this.Col_BillablePrice, "Billable Price is required");
                        }
                        else
                        {
                            row.SetColumnError(this.Col_BillablePrice, "");
                        }
                        if (Show && ((row[this.Col_AllowablePrice] == DBNull.Value) || (Conversions.ToDouble(row[this.Col_AllowablePrice]) == 0.0)))
                        {
                            row.SetColumnError(this.Col_AllowablePrice, "Allowable Price is required");
                        }
                        else
                        {
                            row.SetColumnError(this.Col_AllowablePrice, "");
                        }
                    }
                }
            }
        }
    }
}

