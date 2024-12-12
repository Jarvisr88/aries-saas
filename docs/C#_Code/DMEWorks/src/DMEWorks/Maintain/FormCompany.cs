namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Ability;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormCompany : DmeForm, IParameters
    {
        private IContainer components;
        private const string CrLf = "\r\n";
        private bool FLoaded;
        private ChangesTracker m_changesTracker;
        protected bool FChanged;
        protected bool FPictureChanged;
        protected int FChangeCount;

        public FormCompany()
        {
            base.Load += new EventHandler(this.FormCompany_Load);
            base.Closing += new CancelEventHandler(this.FormCompany_Closing);
            this.FLoaded = false;
            this.FChangeCount = 0;
            this.InitializeComponent();
            this.m_changesTracker = new ChangesTracker(new EventHandler(this.HandleControlChanged));
            this.StartTrackingChanges(this.m_changesTracker);
            Functions.AttachPhoneAutoInput(this.txtPhone);
            Functions.AttachPhoneAutoInput(this.txtPhone2);
            Functions.AttachPhoneAutoInput(this.txtFax);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveObject();
                this.ShowMissingInformation(this.chbMissinfInformation.Checked);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.pbLogo.Image != null)
            {
                this.pbLogo.Image = null;
                this.OnPictureChanged(sender);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveObject();
                base.Close();
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif|All files (*.*)|*.*";
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        this.pbLogo.Image = Image.FromFile(dialog.FileName);
                        this.OnPictureChanged(sender);
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

        private void chbMissinfInformation_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowMissingInformation(this.chbMissinfInformation.Checked);
        }

        private static void ClearErrors(Control parent, ErrorProvider provider)
        {
            int num = parent.Controls.Count - 1;
            for (int i = 0; i <= num; i++)
            {
                Control control = parent.Controls[i];
                provider.SetError(control, "");
                ClearErrors(control, provider);
            }
        }

        protected void ClearObject()
        {
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.txtContact, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddress.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone, DBNull.Value);
            Functions.SetTextBoxText(this.txtFax, DBNull.Value);
            Functions.SetTextBoxText(this.txtPhone2, DBNull.Value);
            Functions.SetTextBoxText(this.txtNPI, DBNull.Value);
            Functions.SetTextBoxText(this.txtEIN, DBNull.Value);
            Functions.SetTextBoxText(this.txtSSN, DBNull.Value);
            Functions.SetTextBoxText(this.txtTaxonomyCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtZirmedNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtAvailityNumber, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPOSType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbWarehouse, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbTaxRate, DBNull.Value);
            Functions.SetTextBoxText(this.txtImagingServer, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSystemGenerate_CustomerAccountNumbers, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSystemGenerate_PurchaseOrderNumber, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSystemGenerate_DeliveryPickupTickets, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSystemGenerate_BlanketAssignments, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSystemGenerate_PatientBillOfRights, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSystemGenerate_DroctorsOrder, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSystemGenerate_HIPAAForms, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbIncludeLocationInfo, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbWriteoffDifference, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPOAuthorizationCodeReqiered, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillCustomerCopayUpfront, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbShow_InactiveCustomers, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbShow_QuantityOnHand, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbAutomaticallyReorderInventory, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrint_PricesOnOrders, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrint_CompanyInfoOnDelivery, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrint_CompanyInfoOnInvoice, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbPrint_CompanyInfoOnPickup, DBNull.Value);
            this.ctlSettingsCredentials.LoadFrom(null);
            this.ctlSettingsClerkCredentials.LoadFrom(null);
            this.ctlSettingsEligibility.LoadFrom(null);
            this.ctlSettingsEnvelope.LoadFrom(null);
            this.pbLogo.Image = null;
            this.FChanged = false;
            this.FPictureChanged = false;
            this.FChangeCount = 0;
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

        private void FormCompany_Closing(object sender, CancelEventArgs e)
        {
            if (this.FChanged)
            {
                MsgBoxResult result = Interaction.MsgBox("Save changes made to company data?", MsgBoxStyle.YesNoCancel, "Maintain Company");
                if (result != MsgBoxResult.Cancel)
                {
                    if (result == MsgBoxResult.Yes)
                    {
                        this.SaveObject();
                    }
                    else
                    {
                        MsgBoxResult result1 = result;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void FormCompany_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearObject();
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

        private object GetPictureValue()
        {
            object obj2;
            if (this.pbLogo.Image == null)
            {
                obj2 = DBNull.Value;
            }
            else
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    this.pbLogo.Image.Save(stream, this.pbLogo.Image.RawFormat);
                    stream.Close();
                    obj2 = stream.ToArray();
                }
            }
            return obj2;
        }

        protected void HandleControlChanged(object sender, EventArgs args)
        {
            this.OnObjectChanged(sender);
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbPOSType, "tbl_postype", null);
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
            Cache.InitDropdown(this.cmbOrderSurvey, "tbl_survey", null);
            Cache.InitDropdown(this.cmbTaxRate, "tbl_taxrate", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormCompany));
            this.btnApply = new Button();
            this.btnCancel = new Button();
            this.btnClear = new Button();
            this.btnOK = new Button();
            this.btnOpen = new Button();
            this.chbBillCustomerCopayUpfront = new CheckBox();
            this.chbSystemGenerate_BlanketAssignments = new CheckBox();
            this.chbSystemGenerate_CustomerAccountNumbers = new CheckBox();
            this.chbSystemGenerate_DeliveryPickupTickets = new CheckBox();
            this.chbSystemGenerate_DroctorsOrder = new CheckBox();
            this.chbSystemGenerate_HIPAAForms = new CheckBox();
            this.chbSystemGenerate_PatientBillOfRights = new CheckBox();
            this.chbSystemGenerate_PurchaseOrderNumber = new CheckBox();
            this.chbPOAuthorizationCodeReqiered = new CheckBox();
            this.chbPrint_PricesOnOrders = new CheckBox();
            this.chbWriteoffDifference = new CheckBox();
            this.cmbPOSType = new ComboBox();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.chbShow_QuantityOnHand = new CheckBox();
            this.chbAutomaticallyReorderInventory = new CheckBox();
            this.chbShow_InactiveCustomers = new CheckBox();
            this.chbIncludeLocationInfo = new CheckBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCompanyName = new Label();
            this.lblFax = new Label();
            this.lblTaxonomyCode = new Label();
            this.lblPhone = new Label();
            this.lblPhone2 = new Label();
            this.lblPOSType = new Label();
            this.pbLogo = new PictureBox();
            this.txtName = new TextBox();
            this.txtPhone2 = new TextBox();
            this.txtPhone = new TextBox();
            this.txtFax = new TextBox();
            this.txtTaxonomyCode = new TextBox();
            this.lblContact = new Label();
            this.txtContact = new TextBox();
            this.MissingProvider = new ErrorProvider(this.components);
            this.chbMissinfInformation = new CheckBox();
            this.gbPrint = new System.Windows.Forms.GroupBox();
            this.chbPrint_CompanyInfoOnPickup = new CheckBox();
            this.chbPrint_CompanyInfoOnDelivery = new CheckBox();
            this.chbPrint_CompanyInfoOnInvoice = new CheckBox();
            this.lblWarehouse = new Label();
            this.cmbWarehouse = new Combobox();
            this.lblNPI = new Label();
            this.txtNPI = new TextBox();
            this.txtEIN = new TextBox();
            this.lblEIN = new Label();
            this.txtSSN = new TextBox();
            this.lblSSN = new Label();
            this.txtImagingServer = new TextBox();
            this.ImagingServer = new Label();
            this.txtZirmedNumber = new TextBox();
            this.lblZirmedNumber = new Label();
            this.ToolTip1 = new ToolTip(this.components);
            this.ValidationWarnings = new ErrorProvider(this.components);
            this.ValidationErrors = new ErrorProvider(this.components);
            this.txtAvailityNumber = new TextBox();
            this.lblAvailityNumber = new Label();
            this.cmbTaxRate = new Combobox();
            this.lblTaxRate = new Label();
            this.TabControl1 = new TabControl();
            this.tpMain = new TabPage();
            this.cmbOrderSurvey = new Combobox();
            this.lblOrderSurvey = new Label();
            this.CAddress = new ControlAddress();
            this.tpOptions = new TabPage();
            this.tpLogo = new TabPage();
            this.tpAbilityIntegration = new TabPage();
            this.pnlAbilityIntegration = new Panel();
            this.ctlSettingsEnvelope = new ControlRegionSettings();
            this.expSettingsEnvelope = new Expander();
            this.ctlSettingsEligibility = new ControlEligibilitySettings();
            this.expEligibilitySettings = new Expander();
            this.ctlSettingsClerkCredentials = new ControlCredentials();
            this.expClerkCredentials = new Expander();
            this.ctlSettingsCredentials = new ControlCredentials();
            this.expCredentials = new Expander();
            this.GroupBox.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            ((ISupportInitialize) this.pbLogo).BeginInit();
            ((ISupportInitialize) this.MissingProvider).BeginInit();
            this.gbPrint.SuspendLayout();
            ((ISupportInitialize) this.ValidationWarnings).BeginInit();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            this.TabControl1.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.tpLogo.SuspendLayout();
            this.tpAbilityIntegration.SuspendLayout();
            this.pnlAbilityIntegration.SuspendLayout();
            base.SuspendLayout();
            this.btnApply.Location = new Point(0x148, 0x1d0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x4b, 0x19);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x198, 0x1d0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnClear.Location = new Point(0x1a0, 40);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x30, 0x17);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnOK.Location = new Point(0xf8, 0x1d0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x19);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOpen.Location = new Point(0x1a0, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(0x30, 0x17);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.chbBillCustomerCopayUpfront.Location = new Point(8, 0x38);
            this.chbBillCustomerCopayUpfront.Name = "chbBillCustomerCopayUpfront";
            this.chbBillCustomerCopayUpfront.Size = new Size(240, 20);
            this.chbBillCustomerCopayUpfront.TabIndex = 3;
            this.chbBillCustomerCopayUpfront.Text = "Bill Customer Up Front";
            this.chbSystemGenerate_BlanketAssignments.Location = new Point(8, 0x24);
            this.chbSystemGenerate_BlanketAssignments.Name = "chbSystemGenerate_BlanketAssignments";
            this.chbSystemGenerate_BlanketAssignments.Size = new Size(0xb0, 20);
            this.chbSystemGenerate_BlanketAssignments.TabIndex = 1;
            this.chbSystemGenerate_BlanketAssignments.Text = "Blanket Assignments";
            this.chbSystemGenerate_CustomerAccountNumbers.Location = new Point(8, 0x74);
            this.chbSystemGenerate_CustomerAccountNumbers.Name = "chbSystemGenerate_CustomerAccountNumbers";
            this.chbSystemGenerate_CustomerAccountNumbers.Size = new Size(0xb0, 20);
            this.chbSystemGenerate_CustomerAccountNumbers.TabIndex = 6;
            this.chbSystemGenerate_CustomerAccountNumbers.Text = "Customer Account Numbers";
            this.chbSystemGenerate_DeliveryPickupTickets.Location = new Point(8, 0x10);
            this.chbSystemGenerate_DeliveryPickupTickets.Name = "chbSystemGenerate_DeliveryPickupTickets";
            this.chbSystemGenerate_DeliveryPickupTickets.Size = new Size(0xb0, 20);
            this.chbSystemGenerate_DeliveryPickupTickets.TabIndex = 0;
            this.chbSystemGenerate_DeliveryPickupTickets.Text = "Delivery/ Pickup Tickets";
            this.chbSystemGenerate_DroctorsOrder.Location = new Point(8, 0x4c);
            this.chbSystemGenerate_DroctorsOrder.Name = "chbSystemGenerate_DroctorsOrder";
            this.chbSystemGenerate_DroctorsOrder.Size = new Size(0xb0, 20);
            this.chbSystemGenerate_DroctorsOrder.TabIndex = 3;
            this.chbSystemGenerate_DroctorsOrder.Text = "Doctor's Order";
            this.chbSystemGenerate_HIPAAForms.Location = new Point(8, 0x60);
            this.chbSystemGenerate_HIPAAForms.Name = "chbSystemGenerate_HIPAAForms";
            this.chbSystemGenerate_HIPAAForms.Size = new Size(0xb0, 20);
            this.chbSystemGenerate_HIPAAForms.TabIndex = 5;
            this.chbSystemGenerate_HIPAAForms.Text = "HIPAA Forms";
            this.chbSystemGenerate_PatientBillOfRights.Location = new Point(8, 0x38);
            this.chbSystemGenerate_PatientBillOfRights.Name = "chbSystemGenerate_PatientBillOfRights";
            this.chbSystemGenerate_PatientBillOfRights.Size = new Size(0xb0, 20);
            this.chbSystemGenerate_PatientBillOfRights.TabIndex = 2;
            this.chbSystemGenerate_PatientBillOfRights.Text = "Patient Bill of Rights";
            this.chbSystemGenerate_PurchaseOrderNumber.Location = new Point(8, 0x88);
            this.chbSystemGenerate_PurchaseOrderNumber.Name = "chbSystemGenerate_PurchaseOrderNumber";
            this.chbSystemGenerate_PurchaseOrderNumber.Size = new Size(0xb0, 20);
            this.chbSystemGenerate_PurchaseOrderNumber.TabIndex = 7;
            this.chbSystemGenerate_PurchaseOrderNumber.Text = "Purchase Order Numbers";
            this.chbPOAuthorizationCodeReqiered.Location = new Point(8, 0x24);
            this.chbPOAuthorizationCodeReqiered.Name = "chbPOAuthorizationCodeReqiered";
            this.chbPOAuthorizationCodeReqiered.Size = new Size(240, 20);
            this.chbPOAuthorizationCodeReqiered.TabIndex = 2;
            this.chbPOAuthorizationCodeReqiered.Text = "PO Autharization Required";
            this.chbPrint_PricesOnOrders.Location = new Point(8, 0x10);
            this.chbPrint_PricesOnOrders.Name = "chbPrint_PricesOnOrders";
            this.chbPrint_PricesOnOrders.Size = new Size(0xb0, 20);
            this.chbPrint_PricesOnOrders.TabIndex = 4;
            this.chbPrint_PricesOnOrders.Text = "Prices on Orders";
            this.chbWriteoffDifference.Location = new Point(8, 0x10);
            this.chbWriteoffDifference.Name = "chbWriteoffDifference";
            this.chbWriteoffDifference.Size = new Size(240, 20);
            this.chbWriteoffDifference.TabIndex = 1;
            this.chbWriteoffDifference.Text = "Writeoff Difference b/w Billed and Allowed";
            this.cmbPOSType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPOSType.Location = new Point(0x68, 0x128);
            this.cmbPOSType.Name = "cmbPOSType";
            this.cmbPOSType.Size = new Size(0x138, 0x15);
            this.cmbPOSType.TabIndex = 0x18;
            this.GroupBox.BackColor = SystemColors.Control;
            this.GroupBox.Controls.Add(this.chbShow_QuantityOnHand);
            this.GroupBox.Controls.Add(this.chbAutomaticallyReorderInventory);
            this.GroupBox.Controls.Add(this.chbShow_InactiveCustomers);
            this.GroupBox.Controls.Add(this.chbIncludeLocationInfo);
            this.GroupBox.Controls.Add(this.chbBillCustomerCopayUpfront);
            this.GroupBox.Controls.Add(this.chbWriteoffDifference);
            this.GroupBox.Controls.Add(this.chbPOAuthorizationCodeReqiered);
            this.GroupBox.Location = new Point(0xd0, 8);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new Size(0x100, 160);
            this.GroupBox.TabIndex = 2;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Miscellaneous";
            this.chbShow_QuantityOnHand.Location = new Point(8, 0x88);
            this.chbShow_QuantityOnHand.Name = "chbShow_QuantityOnHand";
            this.chbShow_QuantityOnHand.Size = new Size(240, 20);
            this.chbShow_QuantityOnHand.TabIndex = 7;
            this.chbShow_QuantityOnHand.Text = "Show Quantity On Hand";
            this.chbAutomaticallyReorderInventory.Location = new Point(8, 0x74);
            this.chbAutomaticallyReorderInventory.Name = "chbAutomaticallyReorderInventory";
            this.chbAutomaticallyReorderInventory.Size = new Size(240, 20);
            this.chbAutomaticallyReorderInventory.TabIndex = 6;
            this.chbAutomaticallyReorderInventory.Text = "Automatically Reorder Inventory";
            this.ToolTip1.SetToolTip(this.chbAutomaticallyReorderInventory, "Turn this off carefully.\r\nThis option turns on/off inventory notifications\r\n");
            this.chbShow_InactiveCustomers.Location = new Point(8, 0x60);
            this.chbShow_InactiveCustomers.Name = "chbShow_InactiveCustomers";
            this.chbShow_InactiveCustomers.Size = new Size(240, 20);
            this.chbShow_InactiveCustomers.TabIndex = 5;
            this.chbShow_InactiveCustomers.Text = "Show Inactive Customers";
            this.chbIncludeLocationInfo.Location = new Point(8, 0x4c);
            this.chbIncludeLocationInfo.Name = "chbIncludeLocationInfo";
            this.chbIncludeLocationInfo.Size = new Size(240, 20);
            this.chbIncludeLocationInfo.TabIndex = 4;
            this.chbIncludeLocationInfo.Text = "Include Location Info";
            this.GroupBox1.BackColor = SystemColors.Control;
            this.GroupBox1.Controls.Add(this.chbSystemGenerate_BlanketAssignments);
            this.GroupBox1.Controls.Add(this.chbSystemGenerate_HIPAAForms);
            this.GroupBox1.Controls.Add(this.chbSystemGenerate_DeliveryPickupTickets);
            this.GroupBox1.Controls.Add(this.chbSystemGenerate_PatientBillOfRights);
            this.GroupBox1.Controls.Add(this.chbSystemGenerate_DroctorsOrder);
            this.GroupBox1.Controls.Add(this.chbSystemGenerate_PurchaseOrderNumber);
            this.GroupBox1.Controls.Add(this.chbSystemGenerate_CustomerAccountNumbers);
            this.GroupBox1.Location = new Point(8, 8);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new Size(0xc0, 0xa2);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Use System Generated";
            this.lblCompanyName.Location = new Point(8, 8);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new Size(0x58, 0x15);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "Company Name";
            this.lblCompanyName.TextAlign = ContentAlignment.MiddleRight;
            this.lblFax.Location = new Point(8, 0xa8);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new Size(0x58, 0x15);
            this.lblFax.TabIndex = 7;
            this.lblFax.Text = "Fax";
            this.lblFax.TextAlign = ContentAlignment.MiddleRight;
            this.lblTaxonomyCode.Location = new Point(8, 0xe0);
            this.lblTaxonomyCode.Name = "lblTaxonomyCode";
            this.lblTaxonomyCode.Size = new Size(0x58, 0x15);
            this.lblTaxonomyCode.TabIndex = 11;
            this.lblTaxonomyCode.Text = "Taxonomy Code";
            this.lblTaxonomyCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone.Location = new Point(8, 0x90);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new Size(0x58, 0x15);
            this.lblPhone.TabIndex = 5;
            this.lblPhone.Text = "Phone";
            this.lblPhone.TextAlign = ContentAlignment.MiddleRight;
            this.lblPhone2.Location = new Point(8, 0xc0);
            this.lblPhone2.Name = "lblPhone2";
            this.lblPhone2.Size = new Size(0x58, 0x15);
            this.lblPhone2.TabIndex = 9;
            this.lblPhone2.Text = "Phone 2";
            this.lblPhone2.TextAlign = ContentAlignment.MiddleRight;
            this.lblPOSType.Location = new Point(8, 0x128);
            this.lblPOSType.Name = "lblPOSType";
            this.lblPOSType.Size = new Size(0x58, 0x15);
            this.lblPOSType.TabIndex = 0x17;
            this.lblPOSType.Text = "POS Type";
            this.lblPOSType.TextAlign = ContentAlignment.MiddleRight;
            this.pbLogo.BackColor = Color.FromArgb(0xff, 240, 0xff);
            this.pbLogo.BorderStyle = BorderStyle.FixedSingle;
            this.pbLogo.Location = new Point(8, 8);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new Size(400, 300);
            this.pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0x4a;
            this.pbLogo.TabStop = false;
            this.txtName.Location = new Point(0x68, 8);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x138, 20);
            this.txtName.TabIndex = 1;
            this.txtPhone2.Location = new Point(0x68, 0xc0);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new Size(0x138, 20);
            this.txtPhone2.TabIndex = 10;
            this.txtPhone.Location = new Point(0x68, 0x90);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(0x138, 20);
            this.txtPhone.TabIndex = 6;
            this.txtFax.Location = new Point(0x68, 0xa8);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(0x138, 20);
            this.txtFax.TabIndex = 8;
            this.txtTaxonomyCode.Location = new Point(0x68, 0xe0);
            this.txtTaxonomyCode.MaxLength = 20;
            this.txtTaxonomyCode.Name = "txtTaxonomyCode";
            this.txtTaxonomyCode.Size = new Size(120, 20);
            this.txtTaxonomyCode.TabIndex = 12;
            this.lblContact.Location = new Point(8, 0x70);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new Size(0x58, 0x15);
            this.lblContact.TabIndex = 3;
            this.lblContact.Text = "Contact";
            this.lblContact.TextAlign = ContentAlignment.MiddleRight;
            this.txtContact.Location = new Point(0x68, 0x70);
            this.txtContact.MaxLength = 50;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new Size(0x138, 20);
            this.txtContact.TabIndex = 4;
            this.MissingProvider.ContainerControl = this;
            this.MissingProvider.DataMember = "";
            this.MissingProvider.Icon = (Icon) manager.GetObject("MissingProvider.Icon");
            this.chbMissinfInformation.Location = new Point(0xb0, 0x1d0);
            this.chbMissinfInformation.Name = "chbMissinfInformation";
            this.chbMissinfInformation.Size = new Size(0x40, 0x18);
            this.chbMissinfInformation.TabIndex = 1;
            this.chbMissinfInformation.Text = "Missing";
            this.gbPrint.Controls.Add(this.chbPrint_CompanyInfoOnPickup);
            this.gbPrint.Controls.Add(this.chbPrint_CompanyInfoOnDelivery);
            this.gbPrint.Controls.Add(this.chbPrint_CompanyInfoOnInvoice);
            this.gbPrint.Controls.Add(this.chbPrint_PricesOnOrders);
            this.gbPrint.Location = new Point(8, 0xb0);
            this.gbPrint.Name = "gbPrint";
            this.gbPrint.Size = new Size(0xc0, 0x68);
            this.gbPrint.TabIndex = 1;
            this.gbPrint.TabStop = false;
            this.gbPrint.Text = "Print";
            this.chbPrint_CompanyInfoOnPickup.Location = new Point(8, 0x4c);
            this.chbPrint_CompanyInfoOnPickup.Name = "chbPrint_CompanyInfoOnPickup";
            this.chbPrint_CompanyInfoOnPickup.Size = new Size(0xb0, 20);
            this.chbPrint_CompanyInfoOnPickup.TabIndex = 7;
            this.chbPrint_CompanyInfoOnPickup.Text = "Company Info On Pickup";
            this.chbPrint_CompanyInfoOnDelivery.Location = new Point(8, 0x38);
            this.chbPrint_CompanyInfoOnDelivery.Name = "chbPrint_CompanyInfoOnDelivery";
            this.chbPrint_CompanyInfoOnDelivery.Size = new Size(0xb0, 20);
            this.chbPrint_CompanyInfoOnDelivery.TabIndex = 6;
            this.chbPrint_CompanyInfoOnDelivery.Text = "Company Info On Delivery";
            this.chbPrint_CompanyInfoOnInvoice.Location = new Point(8, 0x24);
            this.chbPrint_CompanyInfoOnInvoice.Name = "chbPrint_CompanyInfoOnInvoice";
            this.chbPrint_CompanyInfoOnInvoice.Size = new Size(0xb0, 20);
            this.chbPrint_CompanyInfoOnInvoice.TabIndex = 5;
            this.chbPrint_CompanyInfoOnInvoice.Text = "Company Info On Invoice";
            this.lblWarehouse.Location = new Point(8, 320);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x58, 0x15);
            this.lblWarehouse.TabIndex = 0x19;
            this.lblWarehouse.Text = "Warehouse";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.cmbWarehouse.Location = new Point(0x68, 320);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(0x138, 0x15);
            this.cmbWarehouse.TabIndex = 0x1a;
            this.lblNPI.Location = new Point(8, 0xf8);
            this.lblNPI.Name = "lblNPI";
            this.lblNPI.Size = new Size(0x58, 0x15);
            this.lblNPI.TabIndex = 13;
            this.lblNPI.Text = "NPI";
            this.lblNPI.TextAlign = ContentAlignment.MiddleRight;
            this.txtNPI.Location = new Point(0x68, 0xf8);
            this.txtNPI.MaxLength = 20;
            this.txtNPI.Name = "txtNPI";
            this.txtNPI.Size = new Size(120, 20);
            this.txtNPI.TabIndex = 14;
            this.txtEIN.Location = new Point(0x128, 0xe0);
            this.txtEIN.MaxLength = 20;
            this.txtEIN.Name = "txtEIN";
            this.txtEIN.Size = new Size(120, 20);
            this.txtEIN.TabIndex = 0x12;
            this.lblEIN.Location = new Point(0xe8, 0xe0);
            this.lblEIN.Name = "lblEIN";
            this.lblEIN.Size = new Size(0x38, 0x15);
            this.lblEIN.TabIndex = 0x11;
            this.lblEIN.Text = "EIN";
            this.lblEIN.TextAlign = ContentAlignment.MiddleRight;
            this.txtSSN.Location = new Point(0x128, 0xf8);
            this.txtSSN.MaxLength = 20;
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.Size = new Size(120, 20);
            this.txtSSN.TabIndex = 20;
            this.lblSSN.Location = new Point(0xe8, 0xf8);
            this.lblSSN.Name = "lblSSN";
            this.lblSSN.Size = new Size(0x38, 0x15);
            this.lblSSN.TabIndex = 0x13;
            this.lblSSN.Text = "SSN";
            this.lblSSN.TextAlign = ContentAlignment.MiddleRight;
            this.txtImagingServer.Location = new Point(0x68, 0x170);
            this.txtImagingServer.Name = "txtImagingServer";
            this.txtImagingServer.Size = new Size(0x138, 20);
            this.txtImagingServer.TabIndex = 30;
            this.ImagingServer.Location = new Point(8, 0x170);
            this.ImagingServer.Name = "ImagingServer";
            this.ImagingServer.Size = new Size(0x58, 0x15);
            this.ImagingServer.TabIndex = 0x1d;
            this.ImagingServer.Text = "Imaging Server";
            this.ImagingServer.TextAlign = ContentAlignment.MiddleRight;
            this.txtZirmedNumber.Location = new Point(0x68, 0x110);
            this.txtZirmedNumber.MaxLength = 20;
            this.txtZirmedNumber.Name = "txtZirmedNumber";
            this.txtZirmedNumber.Size = new Size(120, 20);
            this.txtZirmedNumber.TabIndex = 0x10;
            this.lblZirmedNumber.Location = new Point(8, 0x110);
            this.lblZirmedNumber.Name = "lblZirmedNumber";
            this.lblZirmedNumber.Size = new Size(0x58, 0x15);
            this.lblZirmedNumber.TabIndex = 15;
            this.lblZirmedNumber.Text = "PTAN";
            this.lblZirmedNumber.TextAlign = ContentAlignment.MiddleRight;
            this.ValidationWarnings.ContainerControl = this;
            this.ValidationWarnings.DataMember = "";
            this.ValidationWarnings.Icon = (Icon) manager.GetObject("ValidationWarnings.Icon");
            this.ValidationErrors.ContainerControl = this;
            this.ValidationErrors.DataMember = "";
            this.txtAvailityNumber.Location = new Point(0x128, 0x110);
            this.txtAvailityNumber.MaxLength = 20;
            this.txtAvailityNumber.Name = "txtAvailityNumber";
            this.txtAvailityNumber.Size = new Size(120, 20);
            this.txtAvailityNumber.TabIndex = 0x16;
            this.lblAvailityNumber.Location = new Point(0xe8, 0x110);
            this.lblAvailityNumber.Name = "lblAvailityNumber";
            this.lblAvailityNumber.Size = new Size(0x38, 0x15);
            this.lblAvailityNumber.TabIndex = 0x15;
            this.lblAvailityNumber.TextAlign = ContentAlignment.MiddleRight;
            this.cmbTaxRate.Location = new Point(0x68, 0x158);
            this.cmbTaxRate.Name = "cmbTaxRate";
            this.cmbTaxRate.Size = new Size(0x138, 0x15);
            this.cmbTaxRate.TabIndex = 0x1c;
            this.lblTaxRate.Location = new Point(8, 0x158);
            this.lblTaxRate.Name = "lblTaxRate";
            this.lblTaxRate.Size = new Size(0x58, 0x15);
            this.lblTaxRate.TabIndex = 0x1b;
            this.lblTaxRate.Text = "Tax Rate";
            this.lblTaxRate.TextAlign = ContentAlignment.MiddleRight;
            this.TabControl1.Controls.Add(this.tpMain);
            this.TabControl1.Controls.Add(this.tpOptions);
            this.TabControl1.Controls.Add(this.tpLogo);
            this.TabControl1.Controls.Add(this.tpAbilityIntegration);
            this.TabControl1.Location = new Point(8, 8);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(480, 0x1c0);
            this.TabControl1.TabIndex = 0;
            this.tpMain.BackColor = SystemColors.Control;
            this.tpMain.Controls.Add(this.cmbOrderSurvey);
            this.tpMain.Controls.Add(this.lblOrderSurvey);
            this.tpMain.Controls.Add(this.lblCompanyName);
            this.tpMain.Controls.Add(this.cmbTaxRate);
            this.tpMain.Controls.Add(this.lblPOSType);
            this.tpMain.Controls.Add(this.lblTaxRate);
            this.tpMain.Controls.Add(this.cmbPOSType);
            this.tpMain.Controls.Add(this.txtAvailityNumber);
            this.tpMain.Controls.Add(this.lblTaxonomyCode);
            this.tpMain.Controls.Add(this.lblAvailityNumber);
            this.tpMain.Controls.Add(this.CAddress);
            this.tpMain.Controls.Add(this.txtZirmedNumber);
            this.tpMain.Controls.Add(this.lblFax);
            this.tpMain.Controls.Add(this.lblZirmedNumber);
            this.tpMain.Controls.Add(this.lblPhone);
            this.tpMain.Controls.Add(this.txtImagingServer);
            this.tpMain.Controls.Add(this.lblPhone2);
            this.tpMain.Controls.Add(this.ImagingServer);
            this.tpMain.Controls.Add(this.txtTaxonomyCode);
            this.tpMain.Controls.Add(this.txtSSN);
            this.tpMain.Controls.Add(this.txtFax);
            this.tpMain.Controls.Add(this.lblSSN);
            this.tpMain.Controls.Add(this.txtName);
            this.tpMain.Controls.Add(this.txtEIN);
            this.tpMain.Controls.Add(this.txtPhone);
            this.tpMain.Controls.Add(this.lblEIN);
            this.tpMain.Controls.Add(this.txtPhone2);
            this.tpMain.Controls.Add(this.txtNPI);
            this.tpMain.Controls.Add(this.txtContact);
            this.tpMain.Controls.Add(this.lblNPI);
            this.tpMain.Controls.Add(this.lblContact);
            this.tpMain.Controls.Add(this.cmbWarehouse);
            this.tpMain.Controls.Add(this.lblWarehouse);
            this.tpMain.Location = new Point(4, 0x16);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new Padding(3);
            this.tpMain.Size = new Size(0x1d8, 0x1a6);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.cmbOrderSurvey.Location = new Point(0x68, 0x188);
            this.cmbOrderSurvey.Name = "cmbOrderSurvey";
            this.cmbOrderSurvey.Size = new Size(0x138, 0x15);
            this.cmbOrderSurvey.TabIndex = 0x20;
            this.lblOrderSurvey.Location = new Point(8, 0x188);
            this.lblOrderSurvey.Name = "lblOrderSurvey";
            this.lblOrderSurvey.Size = new Size(0x58, 0x15);
            this.lblOrderSurvey.TabIndex = 0x1f;
            this.lblOrderSurvey.Text = "Order Survey";
            this.lblOrderSurvey.TextAlign = ContentAlignment.MiddleRight;
            this.CAddress.BackColor = SystemColors.Control;
            this.CAddress.Location = new Point(0x20, 0x20);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new Size(0x180, 0x48);
            this.CAddress.TabIndex = 2;
            this.tpOptions.Controls.Add(this.GroupBox1);
            this.tpOptions.Controls.Add(this.gbPrint);
            this.tpOptions.Controls.Add(this.GroupBox);
            this.tpOptions.Location = new Point(4, 0x16);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new Padding(3);
            this.tpOptions.Size = new Size(0x1d8, 0x1a6);
            this.tpOptions.TabIndex = 1;
            this.tpOptions.Text = "Options";
            this.tpLogo.Controls.Add(this.pbLogo);
            this.tpLogo.Controls.Add(this.btnOpen);
            this.tpLogo.Controls.Add(this.btnClear);
            this.tpLogo.Location = new Point(4, 0x16);
            this.tpLogo.Name = "tpLogo";
            this.tpLogo.Padding = new Padding(3);
            this.tpLogo.Size = new Size(0x1d8, 0x1a6);
            this.tpLogo.TabIndex = 2;
            this.tpLogo.Text = "Logo";
            this.tpAbilityIntegration.Controls.Add(this.pnlAbilityIntegration);
            this.tpAbilityIntegration.Location = new Point(4, 0x16);
            this.tpAbilityIntegration.Name = "tpAbilityIntegration";
            this.tpAbilityIntegration.Padding = new Padding(3);
            this.tpAbilityIntegration.Size = new Size(0x1d8, 0x1a6);
            this.tpAbilityIntegration.TabIndex = 3;
            this.tpAbilityIntegration.Text = "Ability Integration";
            this.pnlAbilityIntegration.AutoScroll = true;
            this.pnlAbilityIntegration.Controls.Add(this.ctlSettingsEnvelope);
            this.pnlAbilityIntegration.Controls.Add(this.expSettingsEnvelope);
            this.pnlAbilityIntegration.Controls.Add(this.ctlSettingsEligibility);
            this.pnlAbilityIntegration.Controls.Add(this.expEligibilitySettings);
            this.pnlAbilityIntegration.Controls.Add(this.ctlSettingsClerkCredentials);
            this.pnlAbilityIntegration.Controls.Add(this.expClerkCredentials);
            this.pnlAbilityIntegration.Controls.Add(this.ctlSettingsCredentials);
            this.pnlAbilityIntegration.Controls.Add(this.expCredentials);
            this.pnlAbilityIntegration.Dock = DockStyle.Fill;
            this.pnlAbilityIntegration.Location = new Point(3, 3);
            this.pnlAbilityIntegration.Name = "pnlAbilityIntegration";
            this.pnlAbilityIntegration.Size = new Size(0x1d2, 0x1a0);
            this.pnlAbilityIntegration.TabIndex = 2;
            this.ctlSettingsEnvelope.Dock = DockStyle.Top;
            this.ctlSettingsEnvelope.Location = new Point(0, 0x138);
            this.ctlSettingsEnvelope.Name = "ctlSettingsEnvelope";
            this.ctlSettingsEnvelope.Size = new Size(0x1d2, 0x58);
            this.ctlSettingsEnvelope.TabIndex = 13;
            this.expSettingsEnvelope.BackColor = SystemColors.ControlDark;
            this.expSettingsEnvelope.Content = this.ctlSettingsEnvelope;
            this.expSettingsEnvelope.Dock = DockStyle.Top;
            this.expSettingsEnvelope.Header = "Envelope Credentials";
            this.expSettingsEnvelope.Location = new Point(0, 0x120);
            this.expSettingsEnvelope.Name = "expSettingsEnvelope";
            this.expSettingsEnvelope.Size = new Size(0x1d2, 0x18);
            this.expSettingsEnvelope.TabIndex = 12;
            this.ctlSettingsEligibility.Dock = DockStyle.Top;
            this.ctlSettingsEligibility.Location = new Point(0, 200);
            this.ctlSettingsEligibility.Name = "ctlSettingsEligibility";
            this.ctlSettingsEligibility.Size = new Size(0x1d2, 0x58);
            this.ctlSettingsEligibility.TabIndex = 15;
            this.expEligibilitySettings.BackColor = SystemColors.ControlDark;
            this.expEligibilitySettings.Content = this.ctlSettingsEligibility;
            this.expEligibilitySettings.Dock = DockStyle.Top;
            this.expEligibilitySettings.Header = "Eligibility Credentials";
            this.expEligibilitySettings.Location = new Point(0, 0xb0);
            this.expEligibilitySettings.Name = "expEligibilitySettings";
            this.expEligibilitySettings.Size = new Size(0x1d2, 0x18);
            this.expEligibilitySettings.TabIndex = 14;
            this.ctlSettingsClerkCredentials.Dock = DockStyle.Top;
            this.ctlSettingsClerkCredentials.Location = new Point(0, 0x70);
            this.ctlSettingsClerkCredentials.Name = "ctlSettingsClerkCredentials";
            this.ctlSettingsClerkCredentials.Size = new Size(0x1d2, 0x40);
            this.ctlSettingsClerkCredentials.TabIndex = 3;
            this.expClerkCredentials.BackColor = SystemColors.ControlDark;
            this.expClerkCredentials.Content = this.ctlSettingsClerkCredentials;
            this.expClerkCredentials.Dock = DockStyle.Top;
            this.expClerkCredentials.Header = "Clerk Credentials";
            this.expClerkCredentials.Location = new Point(0, 0x58);
            this.expClerkCredentials.Name = "expClerkCredentials";
            this.expClerkCredentials.Size = new Size(0x1d2, 0x18);
            this.expClerkCredentials.TabIndex = 2;
            this.ctlSettingsCredentials.Dock = DockStyle.Top;
            this.ctlSettingsCredentials.Location = new Point(0, 0x18);
            this.ctlSettingsCredentials.Name = "ctlSettingsCredentials";
            this.ctlSettingsCredentials.Size = new Size(0x1d2, 0x40);
            this.ctlSettingsCredentials.TabIndex = 1;
            this.expCredentials.BackColor = SystemColors.ControlDark;
            this.expCredentials.Content = this.ctlSettingsCredentials;
            this.expCredentials.Dock = DockStyle.Top;
            this.expCredentials.Header = "Credentials";
            this.expCredentials.Location = new Point(0, 0);
            this.expCredentials.Name = "expCredentials";
            this.expCredentials.Size = new Size(0x1d2, 0x18);
            this.expCredentials.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x1f2, 0x1ed);
            base.Controls.Add(this.TabControl1);
            base.Controls.Add(this.chbMissinfInformation);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.Name = "FormCompany";
            base.ShowInTaskbar = false;
            this.Text = "Company Data";
            this.GroupBox.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.pbLogo).EndInit();
            ((ISupportInitialize) this.MissingProvider).EndInit();
            this.gbPrint.ResumeLayout(false);
            ((ISupportInitialize) this.ValidationWarnings).EndInit();
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.tpOptions.ResumeLayout(false);
            this.tpLogo.ResumeLayout(false);
            this.tpAbilityIntegration.ResumeLayout(false);
            this.pnlAbilityIntegration.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected void LoadObject()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM tbl_company WHERE ID = 1", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            this.ClearObject();
                        }
                        else
                        {
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.txtContact, reader["Contact"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddress.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddress.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddress.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddress.txtZip, reader["Zip"]);
                            Functions.SetTextBoxText(this.txtPhone, reader["Phone"]);
                            Functions.SetTextBoxText(this.txtFax, reader["Fax"]);
                            Functions.SetTextBoxText(this.txtPhone2, reader["Phone2"]);
                            Functions.SetTextBoxText(this.txtNPI, reader["NPI"]);
                            Functions.SetTextBoxText(this.txtEIN, reader["EIN"]);
                            Functions.SetTextBoxText(this.txtSSN, reader["SSN"]);
                            Functions.SetTextBoxText(this.txtTaxonomyCode, reader["TaxonomyCode"]);
                            Functions.SetTextBoxText(this.txtZirmedNumber, reader["ZirmedNumber"]);
                            Functions.SetTextBoxText(this.txtAvailityNumber, reader["AvailityNumber"]);
                            Functions.SetComboBoxValue(this.cmbPOSType, reader["POSTypeID"]);
                            Functions.SetComboBoxValue(this.cmbWarehouse, reader["WarehouseID"]);
                            Functions.SetComboBoxValue(this.cmbTaxRate, reader["TaxRateID"]);
                            Functions.SetComboBoxValue(this.cmbOrderSurvey, reader["OrderSurveyID"]);
                            Functions.SetTextBoxText(this.txtImagingServer, reader["ImagingServer"]);
                            Functions.SetCheckBoxChecked(this.chbIncludeLocationInfo, reader["IncludeLocationInfo"]);
                            Functions.SetCheckBoxChecked(this.chbWriteoffDifference, reader["WriteoffDifference"]);
                            Functions.SetCheckBoxChecked(this.chbPOAuthorizationCodeReqiered, reader["POAuthorizationCodeReqiered"]);
                            Functions.SetCheckBoxChecked(this.chbBillCustomerCopayUpfront, reader["BillCustomerCopayUpfront"]);
                            Functions.SetCheckBoxChecked(this.chbShow_InactiveCustomers, reader["Show_InactiveCustomers"]);
                            Functions.SetCheckBoxChecked(this.chbShow_QuantityOnHand, reader["Show_QuantityOnHand"]);
                            Functions.SetCheckBoxChecked(this.chbAutomaticallyReorderInventory, reader["AutomaticallyReorderInventory"]);
                            Functions.SetCheckBoxChecked(this.chbPrint_PricesOnOrders, reader["Print_PricesOnOrders"]);
                            Functions.SetCheckBoxChecked(this.chbPrint_CompanyInfoOnDelivery, reader["Print_CompanyInfoOnDelivery"]);
                            Functions.SetCheckBoxChecked(this.chbPrint_CompanyInfoOnInvoice, reader["Print_CompanyInfoOnInvoice"]);
                            Functions.SetCheckBoxChecked(this.chbPrint_CompanyInfoOnPickup, reader["Print_CompanyInfoOnPickup"]);
                            Functions.SetCheckBoxChecked(this.chbSystemGenerate_CustomerAccountNumbers, reader["SystemGenerate_CustomerAccountNumbers"]);
                            Functions.SetCheckBoxChecked(this.chbSystemGenerate_PurchaseOrderNumber, reader["SystemGenerate_PurchaseOrderNumber"]);
                            Functions.SetCheckBoxChecked(this.chbSystemGenerate_DeliveryPickupTickets, reader["SystemGenerate_DeliveryPickupTickets"]);
                            Functions.SetCheckBoxChecked(this.chbSystemGenerate_BlanketAssignments, reader["SystemGenerate_BlanketAssignments"]);
                            Functions.SetCheckBoxChecked(this.chbSystemGenerate_PatientBillOfRights, reader["SystemGenerate_PatientBillOfRights"]);
                            Functions.SetCheckBoxChecked(this.chbSystemGenerate_DroctorsOrder, reader["SystemGenerate_DroctorsOrder"]);
                            Functions.SetCheckBoxChecked(this.chbSystemGenerate_HIPAAForms, reader["SystemGenerate_HIPPAForms"]);
                            IntegrationSettings settings = IntegrationSettings.XmlDeserialize(Convert.ToString(reader["AbilityIntegrationSettings"]));
                            settings = new IntegrationSettings();
                            this.ctlSettingsCredentials.LoadFrom(settings.Credentials);
                            this.ctlSettingsClerkCredentials.LoadFrom(settings.ClerkCredentials);
                            this.ctlSettingsEligibility.LoadFrom(settings.EligibilityCredentials);
                            this.ctlSettingsEnvelope.LoadFrom(settings.EnvelopeCredentials);
                            try
                            {
                                MemoryStream stream = new MemoryStream((byte[]) reader["Picture"]);
                                this.pbLogo.Image = Image.FromStream(stream);
                            }
                            catch (Exception exception1)
                            {
                                ProjectData.SetProjectError(exception1);
                                ProjectData.ClearProjectError();
                            }
                            this.FChanged = false;
                            this.FPictureChanged = false;
                            this.FChangeCount = 0;
                        }
                    }
                }
            }
        }

        protected void OnObjectChanged(object sender)
        {
            this.FChanged = true;
            this.FChangeCount++;
        }

        protected void OnPictureChanged(object sender)
        {
            this.FPictureChanged = true;
            this.OnObjectChanged(sender);
        }

        private void PrivateClearValidationErrors()
        {
            ClearErrors(this, this.ValidationErrors);
            ClearErrors(this, this.ValidationWarnings);
        }

        private bool PrivateValidateObject()
        {
            bool flag;
            this.PrivateClearValidationErrors();
            this.ValidateObject();
            StringBuilder builder = new StringBuilder("There are some errors in the input data");
            if (0 < Functions.EnumerateErrors(this, this.ValidationErrors, builder))
            {
                builder.Append("\r\n");
                builder.Append("\r\n");
                builder.Append("Cannot proceed.");
                MessageBox.Show(builder.ToString(), this.Text + " - validation errors", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag = false;
            }
            else
            {
                StringBuilder builder2 = new StringBuilder("There are some warnings in the input data");
                if (0 >= Functions.EnumerateErrors(this, this.ValidationWarnings, builder2))
                {
                    flag = true;
                }
                else
                {
                    builder2.Append("\r\n");
                    builder2.Append("\r\n");
                    builder2.Append("Do you want to proceed?");
                    flag = MessageBox.Show(builder2.ToString(), this.Text + " - validation warnings", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
                }
            }
            return flag;
        }

        protected void SaveObject()
        {
            if (this.PrivateValidateObject())
            {
                if (!this.Permissions.Allow_ADD_EDIT)
                {
                    throw new UserNotifyException("You do not have permissions to Add or Edit Objects");
                }
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.Parameters.Add("Address1", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress1.Text;
                        command.Parameters.Add("Address2", MySqlType.VarChar, 40).Value = this.CAddress.txtAddress2.Text;
                        command.Parameters.Add("BillCustomerCopayUpfront", MySqlType.Bit).Value = this.chbBillCustomerCopayUpfront.Checked;
                        command.Parameters.Add("City", MySqlType.VarChar, 0x19).Value = this.CAddress.txtCity.Text;
                        command.Parameters.Add("Fax", MySqlType.VarChar, 50).Value = this.txtFax.Text;
                        command.Parameters.Add("NPI", MySqlType.VarChar, 10).Value = this.txtNPI.Text;
                        command.Parameters.Add("EIN", MySqlType.VarChar, 20).Value = this.txtEIN.Text;
                        command.Parameters.Add("SSN", MySqlType.VarChar, 20).Value = this.txtSSN.Text;
                        command.Parameters.Add("TaxonomyCode", MySqlType.VarChar, 9).Value = this.txtTaxonomyCode.Text;
                        command.Parameters.Add("ZirmedNumber", MySqlType.VarChar, 20).Value = this.txtZirmedNumber.Text;
                        command.Parameters.Add("AvailityNumber", MySqlType.VarChar, 20).Value = this.txtAvailityNumber.Text;
                        command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                        command.Parameters.Add("Contact", MySqlType.VarChar, 50).Value = this.txtContact.Text;
                        command.Parameters.Add("Phone", MySqlType.VarChar, 50).Value = this.txtPhone.Text;
                        command.Parameters.Add("Phone2", MySqlType.VarChar, 50).Value = this.txtPhone2.Text;
                        command.Parameters.Add("ImagingServer", MySqlType.VarChar, 250).Value = this.txtImagingServer.Text;
                        command.Parameters.Add("POAuthorizationCodeReqiered", MySqlType.Bit).Value = this.chbPOAuthorizationCodeReqiered.Checked;
                        command.Parameters.Add("Print_PricesOnOrders", MySqlType.Bit).Value = this.chbPrint_PricesOnOrders.Checked;
                        command.Parameters.Add("Print_CompanyInfoOnDelivery", MySqlType.Bit).Value = this.chbPrint_CompanyInfoOnDelivery.Checked;
                        command.Parameters.Add("Print_CompanyInfoOnInvoice", MySqlType.Bit).Value = this.chbPrint_CompanyInfoOnInvoice.Checked;
                        command.Parameters.Add("Print_CompanyInfoOnPickup", MySqlType.Bit).Value = this.chbPrint_CompanyInfoOnPickup.Checked;
                        command.Parameters.Add("Show_InactiveCustomers", MySqlType.Bit).Value = this.chbShow_InactiveCustomers.Checked;
                        command.Parameters.Add("Show_QuantityOnHand", MySqlType.Bit).Value = this.chbShow_QuantityOnHand.Checked;
                        command.Parameters.Add("AutomaticallyReorderInventory", MySqlType.Bit).Value = this.chbAutomaticallyReorderInventory.Checked;
                        command.Parameters.Add("POSTypeID", MySqlType.Int).Value = this.cmbPOSType.SelectedValue;
                        command.Parameters.Add("WarehouseID", MySqlType.Int).Value = this.cmbWarehouse.SelectedValue;
                        command.Parameters.Add("TaxRateID", MySqlType.Int).Value = this.cmbTaxRate.SelectedValue;
                        command.Parameters.Add("OrderSurveyID", MySqlType.Int).Value = this.cmbOrderSurvey.SelectedValue;
                        command.Parameters.Add("State", MySqlType.Char, 2).Value = this.CAddress.txtState.Text;
                        command.Parameters.Add("SystemGenerate_BlanketAssignments", MySqlType.Bit).Value = this.chbSystemGenerate_BlanketAssignments.Checked;
                        command.Parameters.Add("SystemGenerate_CustomerAccountNumbers", MySqlType.Bit).Value = this.chbSystemGenerate_CustomerAccountNumbers.Checked;
                        command.Parameters.Add("SystemGenerate_DeliveryPickupTickets", MySqlType.Bit).Value = this.chbSystemGenerate_DeliveryPickupTickets.Checked;
                        command.Parameters.Add("SystemGenerate_DroctorsOrder", MySqlType.Bit).Value = this.chbSystemGenerate_DroctorsOrder.Checked;
                        command.Parameters.Add("SystemGenerate_HIPPAForms", MySqlType.Bit).Value = this.chbSystemGenerate_HIPAAForms.Checked;
                        command.Parameters.Add("SystemGenerate_PatientBillOfRights", MySqlType.Bit).Value = this.chbSystemGenerate_PatientBillOfRights.Checked;
                        command.Parameters.Add("SystemGenerate_PurchaseOrderNumber", MySqlType.Bit).Value = this.chbSystemGenerate_PurchaseOrderNumber.Checked;
                        IntegrationSettings settings1 = new IntegrationSettings();
                        settings1.Credentials = this.ctlSettingsCredentials.Save();
                        settings1.ClerkCredentials = this.ctlSettingsClerkCredentials.Save();
                        settings1.EligibilityCredentials = this.ctlSettingsEligibility.Save();
                        settings1.EnvelopeCredentials = this.ctlSettingsEnvelope.Save();
                        IntegrationSettings settings = settings1;
                        command.Parameters.Add("AbilityIntegrationSettings", MySqlType.Text, 0xffff).Value = IntegrationSettings.XmlSerialize(settings);
                        command.Parameters.Add("IncludeLocationInfo", MySqlType.Bit).Value = this.chbIncludeLocationInfo.Checked;
                        command.Parameters.Add("WriteoffDifference", MySqlType.Bit).Value = this.chbWriteoffDifference.Checked;
                        command.Parameters.Add("Zip", MySqlType.VarChar, 10).Value = this.CAddress.txtZip.Text;
                        command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = DMEWorks.Globals.CompanyUserID;
                        if (this.FPictureChanged)
                        {
                            command.Parameters.Add("Picture", MySqlType.Blob, 0x7fffffff).Value = this.GetPictureValue();
                        }
                        command.Parameters.Add("ID", MySqlType.Int).Value = 1;
                        string[] whereParameters = new string[] { "ID" };
                        if (command.ExecuteUpdate("tbl_company", whereParameters) == 0)
                        {
                            if (!command.Parameters.Contains("Picture"))
                            {
                                command.Parameters.Add("Picture", MySqlType.Blob, 0x7fffffff).Value = this.GetPictureValue();
                            }
                            command.ExecuteInsert("tbl_company");
                        }
                        this.FPictureChanged = false;
                        this.FChanged = false;
                        this.FChangeCount = 0;
                    }
                }
                ClassGlobalObjects.ReloadCompany();
            }
        }

        protected void SetParameters(FormParameters Params)
        {
            if (!this.FLoaded)
            {
                this.LoadObject();
                this.FLoaded = true;
            }
        }

        public void ShowMissingInformation(bool Show)
        {
            this.MissingProvider.SetIconPadding(this.CAddress.txtAddress1, -16);
            this.MissingProvider.SetIconPadding(this.CAddress.txtAddress1, -16);
            this.MissingProvider.SetIconPadding(this.CAddress.txtCity, -16);
            this.MissingProvider.SetIconPadding(this.CAddress.txtState, -16);
            this.MissingProvider.SetIconPadding(this.CAddress.txtZip, -16);
            this.MissingProvider.SetIconPadding(this.txtName, -16);
            this.MissingProvider.SetIconPadding(this.txtPhone, -16);
            this.MissingProvider.SetIconPadding(this.txtTaxonomyCode, -16);
            this.MissingProvider.SetIconPadding(this.cmbPOSType, -16);
            if (Show & (this.CAddress.txtAddress1.Text.Trim() == ""))
            {
                this.MissingProvider.SetError(this.CAddress.txtAddress1, "Address-line-1 is required for invoice");
            }
            else
            {
                this.MissingProvider.SetError(this.CAddress.txtAddress1, "");
            }
            if (Show & (this.CAddress.txtCity.Text.Trim() == ""))
            {
                this.MissingProvider.SetError(this.CAddress.txtCity, "City is required for invoice");
            }
            else
            {
                this.MissingProvider.SetError(this.CAddress.txtCity, "");
            }
            if (Show & (this.CAddress.txtState.Text.Trim() == ""))
            {
                this.MissingProvider.SetError(this.CAddress.txtState, "State is required for invoice");
            }
            else
            {
                this.MissingProvider.SetError(this.CAddress.txtState, "");
            }
            if (Show & (this.CAddress.txtZip.Text.Trim() == ""))
            {
                this.MissingProvider.SetError(this.CAddress.txtZip, "ZIP is required for invoice");
            }
            else
            {
                this.MissingProvider.SetError(this.CAddress.txtZip, "");
            }
            if (Show & (this.txtName.Text.Trim() == ""))
            {
                this.MissingProvider.SetError(this.txtName, "Name is required for invoice");
            }
            else
            {
                this.MissingProvider.SetError(this.txtName, "");
            }
            if (Show & (this.txtPhone.Text.Trim() == ""))
            {
                this.MissingProvider.SetError(this.txtPhone, "Phone is required for invoice");
            }
            else
            {
                this.MissingProvider.SetError(this.txtPhone, "");
            }
            if (Show & !Versioned.IsNumeric(this.cmbPOSType.SelectedValue))
            {
                this.MissingProvider.SetError(this.cmbPOSType, "POSType is required for invoice");
            }
            else
            {
                this.MissingProvider.SetError(this.cmbPOSType, "");
            }
        }

        private void StartTrackingChanges(ChangesTracker tracker)
        {
            if (tracker == null)
            {
                throw new ArgumentNullException("tracker");
            }
            tracker.Subscribe(this.txtName);
            tracker.Subscribe(this.txtContact);
            tracker.Subscribe(this.CAddress.txtAddress1);
            tracker.Subscribe(this.CAddress.txtAddress2);
            tracker.Subscribe(this.CAddress.txtCity);
            tracker.Subscribe(this.CAddress.txtState);
            tracker.Subscribe(this.CAddress.txtZip);
            tracker.Subscribe(this.txtPhone);
            tracker.Subscribe(this.txtFax);
            tracker.Subscribe(this.txtPhone2);
            tracker.Subscribe(this.txtNPI);
            tracker.Subscribe(this.txtEIN);
            tracker.Subscribe(this.txtSSN);
            tracker.Subscribe(this.txtTaxonomyCode);
            tracker.Subscribe(this.txtZirmedNumber);
            tracker.Subscribe(this.txtAvailityNumber);
            tracker.Subscribe(this.cmbPOSType);
            tracker.Subscribe(this.cmbWarehouse);
            tracker.Subscribe(this.cmbTaxRate);
            tracker.Subscribe(this.txtImagingServer);
            tracker.Subscribe(this.chbIncludeLocationInfo);
            tracker.Subscribe(this.chbWriteoffDifference);
            tracker.Subscribe(this.chbPOAuthorizationCodeReqiered);
            tracker.Subscribe(this.chbBillCustomerCopayUpfront);
            tracker.Subscribe(this.chbShow_InactiveCustomers);
            tracker.Subscribe(this.chbShow_QuantityOnHand);
            tracker.Subscribe(this.chbAutomaticallyReorderInventory);
            tracker.Subscribe(this.chbPrint_PricesOnOrders);
            tracker.Subscribe(this.chbPrint_CompanyInfoOnDelivery);
            tracker.Subscribe(this.chbPrint_CompanyInfoOnInvoice);
            tracker.Subscribe(this.chbPrint_CompanyInfoOnPickup);
            tracker.Subscribe(this.chbSystemGenerate_CustomerAccountNumbers);
            tracker.Subscribe(this.chbSystemGenerate_PurchaseOrderNumber);
            tracker.Subscribe(this.chbSystemGenerate_DeliveryPickupTickets);
            tracker.Subscribe(this.chbSystemGenerate_BlanketAssignments);
            tracker.Subscribe(this.chbSystemGenerate_PatientBillOfRights);
            tracker.Subscribe(this.chbSystemGenerate_DroctorsOrder);
            tracker.Subscribe(this.chbSystemGenerate_HIPAAForms);
            this.ctlSettingsCredentials.StartTrackingChanges(tracker);
            this.ctlSettingsClerkCredentials.StartTrackingChanges(tracker);
            this.ctlSettingsEnvelope.StartTrackingChanges(tracker);
        }

        protected void ValidateObject()
        {
            this.ValidationErrors.SetError(this.txtPhone, Functions.PhoneValidate(this.txtPhone.Text));
            this.ValidationErrors.SetError(this.txtPhone2, Functions.PhoneValidate(this.txtPhone2.Text));
            this.ValidationErrors.SetError(this.txtFax, Functions.PhoneValidate(this.txtFax.Text));
            if (!this.chbAutomaticallyReorderInventory.Checked)
            {
                this.ValidationWarnings.SetError(this.chbAutomaticallyReorderInventory, "Are you sure you want to turn off this option?");
                this.ValidationWarnings.SetIconPadding(this.chbAutomaticallyReorderInventory, -18);
            }
            this.ctlSettingsCredentials.ValidateObject(this.ValidationErrors, this.ValidationWarnings);
        }

        [field: AccessedThroughProperty("txtAvailityNumber")]
        private TextBox txtAvailityNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblAvailityNumber")]
        private Label lblAvailityNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnApply")]
        private Button btnApply { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnCancel")]
        private Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnClear")]
        private Button btnClear { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOK")]
        private Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOpen")]
        private Button btnOpen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBillCustomerCopayUpfront")]
        private CheckBox chbBillCustomerCopayUpfront { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSystemGenerate_BlanketAssignments")]
        private CheckBox chbSystemGenerate_BlanketAssignments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSystemGenerate_CustomerAccountNumbers")]
        private CheckBox chbSystemGenerate_CustomerAccountNumbers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSystemGenerate_DeliveryPickupTickets")]
        private CheckBox chbSystemGenerate_DeliveryPickupTickets { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSystemGenerate_DroctorsOrder")]
        private CheckBox chbSystemGenerate_DroctorsOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSystemGenerate_PatientBillOfRights")]
        private CheckBox chbSystemGenerate_PatientBillOfRights { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSystemGenerate_PurchaseOrderNumber")]
        private CheckBox chbSystemGenerate_PurchaseOrderNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPOAuthorizationCodeReqiered")]
        private CheckBox chbPOAuthorizationCodeReqiered { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbWriteoffDifference")]
        private CheckBox chbWriteoffDifference { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("GroupBox")]
        private System.Windows.Forms.GroupBox GroupBox { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("GroupBox1")]
        private System.Windows.Forms.GroupBox GroupBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pbLogo")]
        private PictureBox pbLogo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddress")]
        private ControlAddress CAddress { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPOSType")]
        private ComboBox cmbPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCompanyName")]
        private Label lblCompanyName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFax")]
        private Label lblFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxonomyCode")]
        private Label lblTaxonomyCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone")]
        private Label lblPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPhone2")]
        private Label lblPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPOSType")]
        private Label lblPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone2")]
        private TextBox txtPhone2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPhone")]
        private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTaxonomyCode")]
        private TextBox txtTaxonomyCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtFax")]
        private TextBox txtFax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbIncludeLocationInfo")]
        private CheckBox chbIncludeLocationInfo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblContact")]
        private Label lblContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtContact")]
        private TextBox txtContact { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("MissingProvider")]
        private ErrorProvider MissingProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbMissinfInformation")]
        private CheckBox chbMissinfInformation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrint_PricesOnOrders")]
        private CheckBox chbPrint_PricesOnOrders { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbPrint")]
        private System.Windows.Forms.GroupBox gbPrint { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrint_CompanyInfoOnInvoice")]
        private CheckBox chbPrint_CompanyInfoOnInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrint_CompanyInfoOnDelivery")]
        private CheckBox chbPrint_CompanyInfoOnDelivery { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbPrint_CompanyInfoOnPickup")]
        private CheckBox chbPrint_CompanyInfoOnPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSystemGenerate_HIPAAForms")]
        private CheckBox chbSystemGenerate_HIPAAForms { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbWarehouse")]
        private Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNPI")]
        private TextBox txtNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNPI")]
        private Label lblNPI { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSSN")]
        private TextBox txtSSN { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSSN")]
        private Label lblSSN { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEIN")]
        private TextBox txtEIN { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEIN")]
        private Label lblEIN { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtImagingServer")]
        private TextBox txtImagingServer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ImagingServer")]
        private Label ImagingServer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtZirmedNumber")]
        private TextBox txtZirmedNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblZirmedNumber")]
        private Label lblZirmedNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbAutomaticallyReorderInventory")]
        private CheckBox chbAutomaticallyReorderInventory { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolTip1")]
        private ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ValidationWarnings")]
        private ErrorProvider ValidationWarnings { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ValidationErrors")]
        private ErrorProvider ValidationErrors { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbShow_InactiveCustomers")]
        private CheckBox chbShow_InactiveCustomers { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbTaxRate")]
        private Combobox cmbTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTaxRate")]
        private Label lblTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbShow_QuantityOnHand")]
        private CheckBox chbShow_QuantityOnHand { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpMain")]
        private TabPage tpMain { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpOptions")]
        private TabPage tpOptions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpLogo")]
        private TabPage tpLogo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpAbilityIntegration")]
        private TabPage tpAbilityIntegration { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbOrderSurvey")]
        private Combobox cmbOrderSurvey { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrderSurvey")]
        private Label lblOrderSurvey { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("pnlAbilityIntegration")]
        private Panel pnlAbilityIntegration { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("expCredentials")]
        private Expander expCredentials { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ctlSettingsCredentials")]
        private ControlCredentials ctlSettingsCredentials { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ctlSettingsClerkCredentials")]
        private ControlCredentials ctlSettingsClerkCredentials { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("expClerkCredentials")]
        private Expander expClerkCredentials { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ctlSettingsEnvelope")]
        private ControlRegionSettings ctlSettingsEnvelope { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("expSettingsEnvelope")]
        private Expander expSettingsEnvelope { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ctlSettingsEligibility")]
        private ControlEligibilitySettings ctlSettingsEligibility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("expEligibilitySettings")]
        private Expander expEligibilitySettings { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        protected PermissionsStruct Permissions =>
            DMEWorks.Core.Permissions.FormCompany;
    }
}

