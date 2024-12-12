namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Forms.Native;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormMain : DmeMdiParentForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";

        public FormMain()
        {
            base.Load += new EventHandler(this.FormMain_Load);
            base.Closed += new EventHandler(this.FormMain_Closed);
            this.InitializeComponent();
            this.mnuCreateEditCustomer.Factory = FormFactories.FormCustomer();
            this.mnuCreateEditOrder.Factory = FormFactories.FormOrder();
            this.mnuCreateEditOrderNew.Factory = FormFactories.FormOrder();
            this.mnuCreateEditOrderConfirm.Factory = FormFactories.FormOrder();
            this.mnuCreateEditOrderConfirm.Parameters["TabPage"] = "Search";
            this.mnuMaintainBillingType.Factory = FormFactories.FormBillingType();
            this.mnuMaintainCallback.Factory = FormFactories.FormCallback();
            this.mnuMaintainCompany.Factory = FormFactories.FormCompany();
            this.mnuMaintainCustomer.Factory = FormFactories.FormCustomer();
            this.mnuMaintainCustomerType.Factory = FormFactories.FormCustomerType();
            this.mnuMaintainCustomerClass.Factory = FormFactories.FormCustomerClass();
            this.mnuMaintainInsuranceType.Factory = FormFactories.FormInsuranceType();
            this.mnuMaintainInvoiceForm.Factory = FormFactories.FormInvoiceForm();
            this.mnuMaintainDenial.Factory = FormFactories.FormDenial();
            this.mnuMaintainDoctor.Factory = FormFactories.FormDoctor();
            this.mnuMaintainDoctorType.Factory = FormFactories.FormDoctorType();
            this.mnuMaintainFacility.Factory = FormFactories.FormFacility();
            this.mnuMaintainHAO.Factory = FormFactories.FormHAO();
            this.mnuMaintainICD9.Factory = FormFactories.FormICD9();
            this.mnuMaintainICD10.Factory = FormFactories.FormICD10();
            this.mnuMaintainImage.Factory = FormFactories.FormImage();
            this.mnuMaintainInsuranceCompany.Factory = FormFactories.FormInsuranceCompany();
            this.mnuMaintainInsuranceCompanyGroup.Factory = FormFactories.FormInsuranceCompanyGroup();
            this.mnuMaintainInventoryItem.Factory = FormFactories.FormInventoryItem();
            this.mnuMaintainKit.Factory = FormFactories.FormKit();
            this.mnuMaintainLegalRep.Factory = FormFactories.FormLegalRep();
            this.mnuMaintainLocation.Factory = FormFactories.FormLocation();
            this.mnuMaintainPredefinedText.Factory = FormFactories.FormPredefinedText();
            this.mnuMaintainManufacturer.Factory = FormFactories.FormManufacturer();
            this.mnuMaintainMedicalConditions.Factory = FormFactories.FormMedicalConditions();
            this.mnuMaintainPOSType.Factory = FormFactories.FormPOSType();
            this.mnuMaintainPriceCode.Factory = FormFactories.FormPriceCode();
            this.mnuMaintainProductType.Factory = FormFactories.FormProductType();
            this.mnuMaintainPricing.Factory = FormFactories.FormPricing();
            this.mnuMaintainAuthorizationType.Factory = FormFactories.FormAuthorizationType();
            this.mnuMaintainProvider.Factory = FormFactories.FormProvider();
            this.mnuMaintainProviderNumberType.Factory = FormFactories.FormProviderNumberType();
            this.mnuMaintainReferral.Factory = FormFactories.FormReferral();
            this.mnuMaintainReferralType.Factory = FormFactories.FormReferralType();
            this.mnuMaintainSalesRep.Factory = FormFactories.FormSalesRep();
            this.mnuMaintainSerial.Factory = FormFactories.FormSerial();
            this.mnuMaintainSurvey.Factory = FormFactories.FormSurvey();
            this.mnuMaintainTaxRate.Factory = FormFactories.FormTaxRate();
            this.mnuMaintainUser.Factory = FormFactories.FormUser();
            this.mnuMaintainVendor.Factory = FormFactories.FormVendor();
            this.mnuMaintainWarehouse.Factory = FormFactories.FormWarehouse();
            this.mnuMaintainInventory.Factory = FormFactories.FormInventory();
            this.mnuMaintainZipCode.Factory = FormFactories.FormZipCode();
            this.mnuMaintainInvoice.Factory = FormFactories.FormInvoice();
            this.mnuMaintainOrder.Factory = FormFactories.FormOrder();
            this.mnuMaintainCrystalReport.Factory = FormFactories.FormCrystalReport();
            this.mnuMaintainCompliance.Factory = FormFactories.FormCompliance();
            this.mnuMaintainCMNRX.Factory = FormFactories.FormCMNRX();
            this.mnuMaintainShippingMethod.Factory = FormFactories.FormShippingMethod();
            this.tsmiOrderingActiveCompliances.Factory = FormFactories.FormCompliancePopup();
            this.tsmiOrderingBatchPayments.Factory = FormFactories.FormBatchPayments();
            this.tsmiOrderingCmnForm.Factory = FormFactories.FormCMNRX();
            this.tsmiOrderingCompliance.Factory = FormFactories.FormCompliance();
            this.tsmiOrderingCallbackForm.Factory = FormFactories.FormCallback();
            this.tsmiOrderingCustomer.Factory = FormFactories.FormCustomer();
            this.tsmiOrderingImage.Factory = FormFactories.FormImage();
            this.tsmiOrderingInvoice.Factory = FormFactories.FormInvoice();
            this.tsmiOrderingMissingInformation.Factory = FormFactories.FormMissingInformation();
            this.tsmiOrderingOrder.Factory = FormFactories.FormOrder();
            this.mnuProcessPurchaseOrder.Factory = FormFactories.FormPurchaseOrder();
            this.mnuProcessSecureCare.Factory = FormFactories.FormSecureCare();
            this.mnuProcessPrintInvoices.Factory = FormFactories.FormPrintInvoices();
            this.mnuProcessUpdateAllowables.Factory = FormFactories.FormPriceUpdater();
            this.mnuProcessUpdatePriceList.Factory = FormFactories.FormPriceListEditor();
            this.mnuProcessOrders.Factory = FormFactories.FormProcessOrders();
            this.mnuProcessOxygen.Factory = FormFactories.FormProcessOxygen();
            this.mnuProcessOxygenSend.Factory = FormFactories.FormProcessOxygen();
            this.mnuProcessOxygenSend.Parameters["Type"] = "Send";
            this.mnuProcessOxygenReceive.Factory = FormFactories.FormProcessOxygen();
            this.mnuProcessOxygenReceive.Parameters["Type"] = "Receive";
            this.mnuProcessOxygenRent.Factory = FormFactories.FormProcessOxygen();
            this.mnuProcessOxygenRent.Parameters["Type"] = "Rent";
            this.mnuProcessOxygenPickup.Factory = FormFactories.FormProcessOxygen();
            this.mnuProcessOxygenPickup.Parameters["Type"] = "Pickup";
            this.mnuProcessRentalPickup.Factory = FormFactories.FormRentalPickup();
            this.mnuProcessReturnSales.Factory = FormFactories.WizardReturnSales();
            this.mnuProcessReturnSales.Modal = true;
            this.mnuProcessMissingInformation.Factory = FormFactories.FormMissingInformation();
            this.mnuProcessActiveCompliances.Factory = FormFactories.FormCompliancePopup();
            this.mnuProcessReports.Factory = FormFactories.FormReportSelector();
            this.mnuProcessRetailSales.Factory = FormFactories.FormRetailSales();
            this.mnuProcessSearchForImages.Factory = FormFactories.FormImageSearch();
            this.mnuProcessSessions.Factory = FormFactories.FormSessions();
            this.mnuProcessEligibility.Factory = FormFactories.FormEligibility();
            this.nmuProcessBatchPayments.Factory = FormFactories.FormBatchPayments();
            this.AssignClickHandler(this.mnuFile);
            this.AssignClickHandler(this.mnuCreateEdit);
            this.AssignClickHandler(this.mnuOrdering);
            this.AssignClickHandler(this.mnuMaintain);
            this.AssignClickHandler(this.mnuProcess);
            this.FillToolsMenu();
            this.mnuOrdering.Visible = false;
            this.mnuAdminThrowException.Visible = false;
        }

        private void AssignClickHandler(ToolStripItem Item)
        {
            FormToolStripMenuItem item = Item as FormToolStripMenuItem;
            if ((item != null) && ((item.Factory != null) && (item.DropDownItems.Count == 0)))
            {
                Item.Click += new EventHandler(this.FormToolStripMenuItem_Click);
            }
            ToolStripDropDownItem item2 = Item as ToolStripDropDownItem;
            if (item2 != null)
            {
                IEnumerator enumerator;
                try
                {
                    enumerator = item2.DropDownItems.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ToolStripItem current = (ToolStripItem) enumerator.Current;
                        this.AssignClickHandler(current);
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
        }

        private ShellLinkToolStripMenuItem CreateMenuItem(string path)
        {
            ShellLinkToolStripMenuItem item = new ShellLinkToolStripMenuItem {
                Path = path,
                Text = Path.GetFileNameWithoutExtension(path)
            };
            item.Click += new EventHandler(this.ShellLinkToolStripMenuItem_Click);
            try
            {
                using (ShellShortcut shortcut = new ShellShortcut(path))
                {
                    using (Icon icon = shortcut.Icon)
                    {
                        item.Image = icon.ToBitmap();
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                TraceHelper.TraceException(ex);
                ProjectData.ClearProjectError();
            }
            return item;
        }

        public void DebugPrint(string st)
        {
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

        public void DoLogin()
        {
            try
            {
                using (FormLogin login = new FormLogin())
                {
                    login.LoadLastLoginInfo();
                    while (login.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            Globals.Login(login.DsnInfo, login.Database, login.UserName, login.Password);
                            this.UpdateLoginState();
                            login.SaveLastLoginInfo();
                            if (Globals.SerialNumber.IsExpired())
                            {
                                throw new UserNotifyException("Your serial number is expired.\r\nYou need to call 866-DMEWORX to obtain another serial number.");
                            }
                            if (Globals.SerialNumber.IsDemoSerial())
                            {
                                MessageBox.Show("DMEWorks! is working in demo mode.\r\nCall 866-DMEWORX to purchase your DMEWorks!", "DMEWorks!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            if (DialogLocation.ShowAtStartup)
                            {
                                this.ShowLocationDialog();
                            }
                            if (FormCompliancePopup.ShowAtStartup)
                            {
                                FormFactory factory = FormFactories.FormCompliancePopup();
                                if (factory.GetPermissions().Allow_VIEW)
                                {
                                    ClassGlobalObjects.ShowForm(factory);
                                }
                            }
                            this.tsslCompany.Text = "Company: " + Globals.CompanyName;
                            this.tsslUser.Text = "User: " + Globals.CompanyUserName;
                            this.tsslFunctions.Text = "Func Version: '" + Globals.GetCompanyVariable("Functions") + "'";
                            this.tsslVersion.Text = "DB Version: '" + Globals.GetCompanyVariable("Version") + "'";
                        }
                        catch (Exception exception3)
                        {
                            Exception ex = exception3;
                            ProjectData.SetProjectError(ex);
                            Exception exception = ex;
                            try
                            {
                                Globals.Logoff();
                            }
                            catch (Exception exception1)
                            {
                                ProjectData.SetProjectError(exception1);
                                ProjectData.ClearProjectError();
                            }
                            this.ShowException(exception);
                            ProjectData.ClearProjectError();
                            continue;
                        }
                        break;
                    }
                }
            }
            catch (Exception exception4)
            {
                Exception ex = exception4;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
        }

        public void DoLogoff()
        {
            try
            {
                Globals.Logoff();
                this.tsslCompany.Text = "";
                this.tsslUser.Text = "";
                this.tsslFunctions.Text = "";
                this.tsslVersion.Text = "";
                this.UpdateLoginState();
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

        private void FillToolsMenu()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            if (Directory.Exists(folderPath))
            {
                folderPath = Path.Combine(folderPath, "DME");
                if (Directory.Exists(folderPath))
                {
                    folderPath = Path.Combine(folderPath, "DME Works");
                    if (Directory.Exists(folderPath))
                    {
                        folderPath = Path.Combine(folderPath, "Tools");
                        if (Directory.Exists(folderPath))
                        {
                            string[] files = Directory.GetFiles(folderPath, "*.lnk");
                            int index = 0;
                            while (true)
                            {
                                if (index >= files.Length)
                                {
                                    this.mnuTools.Visible = 0 < this.mnuTools.DropDownItems.Count;
                                    break;
                                }
                                string path = files[index];
                                if (File.Exists(path))
                                {
                                    try
                                    {
                                        this.mnuTools.DropDownItems.Add(this.CreateMenuItem(path));
                                    }
                                    catch (Exception exception1)
                                    {
                                        Exception ex = exception1;
                                        ProjectData.SetProjectError(ex);
                                        TraceHelper.TraceException(ex);
                                        ProjectData.ClearProjectError();
                                    }
                                }
                                index++;
                            }
                        }
                    }
                }
            }
        }

        public void ForEachForm(FormRepeater Repeater, object Param)
        {
            foreach (Form form in base.MdiChildren)
            {
                if (!form.IsDisposed)
                {
                    Repeater(form, Param);
                }
            }
        }

        private void FormMain_Closed(object eventSender, EventArgs eventArgs)
        {
            Globals.Logoff();
        }

        private void FormMain_Load(object eventSender, EventArgs eventArgs)
        {
            this.UpdateLoginState();
            this.DoLogin();
        }

        private void FormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormToolStripMenuItem item = sender as FormToolStripMenuItem;
            if ((item != null) && (item.Factory != null))
            {
                FormParameters @params = item.Parameters;
                if (@params.Count == 0)
                {
                    @params = null;
                }
                ClassGlobalObjects.ShowForm(item.Factory, @params, item.Modal);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuFile = new ToolStripMenuItem();
            this.mnuFileLogin = new ToolStripMenuItem();
            this.mnuFileLogoff = new ToolStripMenuItem();
            this.mnuFileChangeLocation = new ToolStripMenuItem();
            this.mnuAdminSeparator3 = new ToolStripSeparator();
            this.mnuFileExit = new ToolStripMenuItem();
            this.mnuAdminThrowException = new ToolStripMenuItem();
            this.mnuCreateEdit = new ToolStripMenuItem();
            this.mnuCreateEditCustomer = new FormToolStripMenuItem();
            this.mnuCreateEditOrder = new FormToolStripMenuItem();
            this.mnuCreateEditOrderNew = new FormToolStripMenuItem();
            this.mnuCreateEditOrderConfirm = new FormToolStripMenuItem();
            this.mnuMaintain = new ToolStripMenuItem();
            this.mnuMaintainPricing = new FormToolStripMenuItem();
            this.mnuMaintainAuxilary = new ToolStripMenuItem();
            this.mnuMaintainPriceCode = new FormToolStripMenuItem();
            this.mnuMaintainAuthorizationType = new FormToolStripMenuItem();
            this.mnuMaintainBillingType = new FormToolStripMenuItem();
            this.mnuMaintainCustomerClass = new FormToolStripMenuItem();
            this.mnuMaintainCustomerType = new FormToolStripMenuItem();
            this.mnuMaintainDenial = new FormToolStripMenuItem();
            this.mnuMaintainDoctorType = new FormToolStripMenuItem();
            this.mnuMaintainICD9 = new FormToolStripMenuItem();
            this.mnuMaintainICD10 = new FormToolStripMenuItem();
            this.mnuMaintainInsuranceType = new FormToolStripMenuItem();
            this.mnuMaintainInsuranceCompanyGroup = new FormToolStripMenuItem();
            this.mnuMaintainInvoiceForm = new FormToolStripMenuItem();
            this.mnuMaintainMedicalConditions = new FormToolStripMenuItem();
            this.mnuMaintainPOSType = new FormToolStripMenuItem();
            this.mnuMaintainPredefinedText = new FormToolStripMenuItem();
            this.mnuMaintainProductType = new FormToolStripMenuItem();
            this.mnuMaintainProviderNumberType = new FormToolStripMenuItem();
            this.mnuMaintainReferralType = new FormToolStripMenuItem();
            this.mnuMaintainShippingMethod = new FormToolStripMenuItem();
            this.mnuMaintainTaxRate = new FormToolStripMenuItem();
            this.mnuMaintainZipCode = new FormToolStripMenuItem();
            this.mnuMaintainCMNRX = new FormToolStripMenuItem();
            this.mnuMaintainCompany = new FormToolStripMenuItem();
            this.mnuMaintainCompliance = new FormToolStripMenuItem();
            this.mnuMaintainCrystalReport = new FormToolStripMenuItem();
            this.mnuMaintainCallback = new FormToolStripMenuItem();
            this.mnuMaintainCustomer = new FormToolStripMenuItem();
            this.mnuMaintainDoctor = new FormToolStripMenuItem();
            this.mnuMaintainFacility = new FormToolStripMenuItem();
            this.mnuMaintainHAO = new FormToolStripMenuItem();
            this.mnuMaintainImage = new FormToolStripMenuItem();
            this.mnuMaintainInsuranceCompany = new FormToolStripMenuItem();
            this.mnuMaintainInventory = new FormToolStripMenuItem();
            this.mnuMaintainInventoryItem = new FormToolStripMenuItem();
            this.mnuMaintainInvoice = new FormToolStripMenuItem();
            this.mnuMaintainKit = new FormToolStripMenuItem();
            this.mnuMaintainLegalRep = new FormToolStripMenuItem();
            this.mnuMaintainLocation = new FormToolStripMenuItem();
            this.mnuMaintainManufacturer = new FormToolStripMenuItem();
            this.mnuMaintainOrder = new FormToolStripMenuItem();
            this.mnuMaintainProvider = new FormToolStripMenuItem();
            this.mnuMaintainReferral = new FormToolStripMenuItem();
            this.mnuMaintainSalesRep = new FormToolStripMenuItem();
            this.mnuMaintainSerial = new FormToolStripMenuItem();
            this.mnuMaintainSurvey = new FormToolStripMenuItem();
            this.mnuMaintainUser = new FormToolStripMenuItem();
            this.mnuMaintainVendor = new FormToolStripMenuItem();
            this.mnuMaintainWarehouse = new FormToolStripMenuItem();
            this.mnuProcess = new ToolStripMenuItem();
            this.nmuProcessBatchPayments = new FormToolStripMenuItem();
            this.mnuProcessOxygen = new FormToolStripMenuItem();
            this.mnuProcessOxygenSend = new FormToolStripMenuItem();
            this.mnuProcessOxygenReceive = new FormToolStripMenuItem();
            this.mnuProcessOxygenRent = new FormToolStripMenuItem();
            this.mnuProcessOxygenPickup = new FormToolStripMenuItem();
            this.mnuProcessPurchaseOrder = new FormToolStripMenuItem();
            this.MenuItem3 = new ToolStripSeparator();
            this.mnuProcessActiveCompliances = new FormToolStripMenuItem();
            this.mnuProcessSessions = new FormToolStripMenuItem();
            this.mnuProcessEligibility = new FormToolStripMenuItem();
            this.mnuProcessMissingInformation = new FormToolStripMenuItem();
            this.mnuProcessOrders = new FormToolStripMenuItem();
            this.mnuPointOfSale = new ToolStripMenuItem();
            this.mnuProcessRetailSales = new FormToolStripMenuItem();
            this.mnuProcessReturnSales = new FormToolStripMenuItem();
            this.mnuProcessPrintInvoices = new FormToolStripMenuItem();
            this.mnuProcessRentalPickup = new FormToolStripMenuItem();
            this.mnuProcessReports = new FormToolStripMenuItem();
            this.mnuProcessSearchForImages = new FormToolStripMenuItem();
            this.mnuProcessSecureCare = new FormToolStripMenuItem();
            this.mnuProcessUpdateAllowables = new FormToolStripMenuItem();
            this.mnuProcessUpdatePriceList = new FormToolStripMenuItem();
            this.mnuWindow = new ToolStripMenuItem();
            this.mnuWindowCascade = new ToolStripMenuItem();
            this.mnuWindowTileHorizontal = new ToolStripMenuItem();
            this.mnuWindowTileVertical = new ToolStripMenuItem();
            this.mnuWindowSeparator = new ToolStripSeparator();
            this.mnuHelp = new ToolStripMenuItem();
            this.mnuHelpContents = new ToolStripMenuItem();
            this.mnuHelpBar0 = new ToolStripSeparator();
            this.mnuWebMedicare = new ToolStripMenuItem();
            this.mnuWebCMS = new ToolStripMenuItem();
            this.mnuWebRegionA = new ToolStripMenuItem();
            this.mnuWebRegionB = new ToolStripMenuItem();
            this.mnuWebRegionC = new ToolStripMenuItem();
            this.mnuWebRegionD = new ToolStripMenuItem();
            this.mnuWebDMEWorks = new ToolStripMenuItem();
            this.MenuItem1 = new ToolStripSeparator();
            this.mnuHelpAbout = new ToolStripMenuItem();
            this.mnuHelpTipOfTheDay = new ToolStripMenuItem();
            this.StatusStrip1 = new StatusStrip();
            this.tsslCompany = new ToolStripStatusLabel();
            this.tsslLocation = new ToolStripStatusLabel();
            this.tsslUser = new ToolStripStatusLabel();
            this.tsslVersion = new ToolStripStatusLabel();
            this.tsslFunctions = new ToolStripStatusLabel();
            this.ToolStripStatusLabel1 = new ToolStripStatusLabel();
            this.MenuStrip1 = new MenuStrip();
            this.mnuOrdering = new ToolStripMenuItem();
            this.tsmiOrderingActiveCompliances = new FormToolStripMenuItem();
            this.tsmiOrderingBatchPayments = new FormToolStripMenuItem();
            this.tsmiOrderingCmnForm = new FormToolStripMenuItem();
            this.tsmiOrderingCompliance = new FormToolStripMenuItem();
            this.tsmiOrderingCallbackForm = new FormToolStripMenuItem();
            this.tsmiOrderingCustomer = new FormToolStripMenuItem();
            this.tsmiOrderingImage = new FormToolStripMenuItem();
            this.tsmiOrderingInvoice = new FormToolStripMenuItem();
            this.tsmiOrderingMissingInformation = new FormToolStripMenuItem();
            this.tsmiOrderingOrder = new FormToolStripMenuItem();
            this.mnuTools = new ToolStripMenuItem();
            this.Timer1 = new Timer(this.components);
            this.StatusStrip1.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            base.SuspendLayout();
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.mnuFileLogin, this.mnuFileLogoff, this.mnuFileChangeLocation, this.mnuAdminSeparator3, this.mnuFileExit, this.mnuAdminThrowException };
            this.mnuFile.DropDownItems.AddRange(toolStripItems);
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new Size(0x25, 20);
            this.mnuFile.Text = "File";
            this.mnuFileLogin.Name = "mnuFileLogin";
            this.mnuFileLogin.Padding = new Padding(0);
            this.mnuFileLogin.Size = new Size(180, 20);
            this.mnuFileLogin.Text = "Login";
            this.mnuFileLogoff.Name = "mnuFileLogoff";
            this.mnuFileLogoff.Padding = new Padding(0);
            this.mnuFileLogoff.Size = new Size(180, 20);
            this.mnuFileLogoff.Text = "Logoff";
            this.mnuFileChangeLocation.Name = "mnuFileChangeLocation";
            this.mnuFileChangeLocation.Padding = new Padding(0);
            this.mnuFileChangeLocation.Size = new Size(180, 20);
            this.mnuFileChangeLocation.Text = "Change Location";
            this.mnuAdminSeparator3.Name = "mnuAdminSeparator3";
            this.mnuAdminSeparator3.Size = new Size(0xb1, 6);
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Padding = new Padding(0);
            this.mnuFileExit.Size = new Size(180, 20);
            this.mnuFileExit.Text = "Exit";
            this.mnuAdminThrowException.Name = "mnuAdminThrowException";
            this.mnuAdminThrowException.Padding = new Padding(0);
            this.mnuAdminThrowException.Size = new Size(180, 20);
            this.mnuAdminThrowException.Text = "Throw Exception";
            ToolStripItem[] itemArray2 = new ToolStripItem[] { this.mnuCreateEditCustomer, this.mnuCreateEditOrder };
            this.mnuCreateEdit.DropDownItems.AddRange(itemArray2);
            this.mnuCreateEdit.Name = "mnuCreateEdit";
            this.mnuCreateEdit.Size = new Size(0x4e, 20);
            this.mnuCreateEdit.Text = "Create/Edit";
            this.mnuCreateEditCustomer.Name = "mnuCreateEditCustomer";
            this.mnuCreateEditCustomer.Padding = new Padding(0);
            this.mnuCreateEditCustomer.Size = new Size(180, 20);
            this.mnuCreateEditCustomer.Text = "Customer";
            ToolStripItem[] itemArray3 = new ToolStripItem[] { this.mnuCreateEditOrderNew, this.mnuCreateEditOrderConfirm };
            this.mnuCreateEditOrder.DropDownItems.AddRange(itemArray3);
            this.mnuCreateEditOrder.Name = "mnuCreateEditOrder";
            this.mnuCreateEditOrder.Padding = new Padding(0);
            this.mnuCreateEditOrder.Size = new Size(180, 20);
            this.mnuCreateEditOrder.Text = "Order";
            this.mnuCreateEditOrderNew.Name = "mnuCreateEditOrderNew";
            this.mnuCreateEditOrderNew.Size = new Size(0x76, 0x16);
            this.mnuCreateEditOrderNew.Text = "New";
            this.mnuCreateEditOrderConfirm.Name = "mnuCreateEditOrderConfirm";
            this.mnuCreateEditOrderConfirm.Size = new Size(0x76, 0x16);
            this.mnuCreateEditOrderConfirm.Text = "Confirm";
            ToolStripItem[] itemArray4 = new ToolStripItem[0x1d];
            itemArray4[0] = this.mnuMaintainAuxilary;
            itemArray4[1] = this.mnuMaintainCMNRX;
            itemArray4[2] = this.mnuMaintainCompany;
            itemArray4[3] = this.mnuMaintainCompliance;
            itemArray4[4] = this.mnuMaintainCrystalReport;
            itemArray4[5] = this.mnuMaintainCallback;
            itemArray4[6] = this.mnuMaintainCustomer;
            itemArray4[7] = this.mnuMaintainDoctor;
            itemArray4[8] = this.mnuMaintainFacility;
            itemArray4[9] = this.mnuMaintainHAO;
            itemArray4[10] = this.mnuMaintainImage;
            itemArray4[11] = this.mnuMaintainInsuranceCompany;
            itemArray4[12] = this.mnuMaintainInventory;
            itemArray4[13] = this.mnuMaintainInventoryItem;
            itemArray4[14] = this.mnuMaintainInvoice;
            itemArray4[15] = this.mnuMaintainKit;
            itemArray4[0x10] = this.mnuMaintainLegalRep;
            itemArray4[0x11] = this.mnuMaintainLocation;
            itemArray4[0x12] = this.mnuMaintainManufacturer;
            itemArray4[0x13] = this.mnuMaintainOrder;
            itemArray4[20] = this.mnuMaintainPricing;
            itemArray4[0x15] = this.mnuMaintainProvider;
            itemArray4[0x16] = this.mnuMaintainReferral;
            itemArray4[0x17] = this.mnuMaintainSalesRep;
            itemArray4[0x18] = this.mnuMaintainSerial;
            itemArray4[0x19] = this.mnuMaintainSurvey;
            itemArray4[0x1a] = this.mnuMaintainUser;
            itemArray4[0x1b] = this.mnuMaintainVendor;
            itemArray4[0x1c] = this.mnuMaintainWarehouse;
            this.mnuMaintain.DropDownItems.AddRange(itemArray4);
            this.mnuMaintain.Name = "mnuMaintain";
            this.mnuMaintain.Size = new Size(0x42, 20);
            this.mnuMaintain.Text = "Maintain";
            this.mnuMaintainPricing.Name = "mnuMaintainPricing";
            this.mnuMaintainPricing.Padding = new Padding(0);
            this.mnuMaintainPricing.Size = new Size(180, 20);
            this.mnuMaintainPricing.Text = "Pricing";
            ToolStripItem[] itemArray5 = new ToolStripItem[0x15];
            itemArray5[0] = this.mnuMaintainAuthorizationType;
            itemArray5[1] = this.mnuMaintainBillingType;
            itemArray5[2] = this.mnuMaintainCustomerClass;
            itemArray5[3] = this.mnuMaintainCustomerType;
            itemArray5[4] = this.mnuMaintainDenial;
            itemArray5[5] = this.mnuMaintainDoctorType;
            itemArray5[6] = this.mnuMaintainICD9;
            itemArray5[7] = this.mnuMaintainICD10;
            itemArray5[8] = this.mnuMaintainInsuranceType;
            itemArray5[9] = this.mnuMaintainInsuranceCompanyGroup;
            itemArray5[10] = this.mnuMaintainInvoiceForm;
            itemArray5[11] = this.mnuMaintainMedicalConditions;
            itemArray5[12] = this.mnuMaintainPOSType;
            itemArray5[13] = this.mnuMaintainPredefinedText;
            itemArray5[14] = this.mnuMaintainPriceCode;
            itemArray5[15] = this.mnuMaintainProductType;
            itemArray5[0x10] = this.mnuMaintainProviderNumberType;
            itemArray5[0x11] = this.mnuMaintainReferralType;
            itemArray5[0x12] = this.mnuMaintainShippingMethod;
            itemArray5[0x13] = this.mnuMaintainTaxRate;
            itemArray5[20] = this.mnuMaintainZipCode;
            this.mnuMaintainAuxilary.DropDownItems.AddRange(itemArray5);
            this.mnuMaintainAuxilary.Name = "mnuMaintainAuxilary";
            this.mnuMaintainAuxilary.Padding = new Padding(0);
            this.mnuMaintainAuxilary.Size = new Size(180, 20);
            this.mnuMaintainAuxilary.Text = "Auxilary";
            this.mnuMaintainPriceCode.Name = "mnuMaintainPriceCode";
            this.mnuMaintainPriceCode.Padding = new Padding(0);
            this.mnuMaintainPriceCode.Size = new Size(0xd8, 20);
            this.mnuMaintainPriceCode.Text = "Price Code";
            this.mnuMaintainAuthorizationType.Name = "mnuMaintainAuthorizationType";
            this.mnuMaintainAuthorizationType.Padding = new Padding(0);
            this.mnuMaintainAuthorizationType.Size = new Size(0xd8, 20);
            this.mnuMaintainAuthorizationType.Text = "Authorization Type";
            this.mnuMaintainBillingType.Name = "mnuMaintainBillingType";
            this.mnuMaintainBillingType.Padding = new Padding(0);
            this.mnuMaintainBillingType.Size = new Size(0xd8, 20);
            this.mnuMaintainBillingType.Text = "Billing Type";
            this.mnuMaintainCustomerClass.Name = "mnuMaintainCustomerClass";
            this.mnuMaintainCustomerClass.Padding = new Padding(0);
            this.mnuMaintainCustomerClass.Size = new Size(0xd8, 20);
            this.mnuMaintainCustomerClass.Text = "Customer Class";
            this.mnuMaintainCustomerType.Name = "mnuMaintainCustomerType";
            this.mnuMaintainCustomerType.Padding = new Padding(0);
            this.mnuMaintainCustomerType.Size = new Size(0xd8, 20);
            this.mnuMaintainCustomerType.Text = "Customer Type";
            this.mnuMaintainDenial.Name = "mnuMaintainDenial";
            this.mnuMaintainDenial.Padding = new Padding(0);
            this.mnuMaintainDenial.Size = new Size(0xd8, 20);
            this.mnuMaintainDenial.Text = "Denial";
            this.mnuMaintainDoctorType.Name = "mnuMaintainDoctorType";
            this.mnuMaintainDoctorType.Padding = new Padding(0);
            this.mnuMaintainDoctorType.Size = new Size(0xd8, 20);
            this.mnuMaintainDoctorType.Text = "Doctor Type";
            this.mnuMaintainICD9.Name = "mnuMaintainICD9";
            this.mnuMaintainICD9.Padding = new Padding(0);
            this.mnuMaintainICD9.Size = new Size(0xd8, 20);
            this.mnuMaintainICD9.Text = "ICD 9";
            this.mnuMaintainICD10.Name = "mnuMaintainICD10";
            this.mnuMaintainICD10.Padding = new Padding(0);
            this.mnuMaintainICD10.Size = new Size(0xd8, 20);
            this.mnuMaintainICD10.Text = "ICD 10";
            this.mnuMaintainInsuranceType.Name = "mnuMaintainInsuranceType";
            this.mnuMaintainInsuranceType.Padding = new Padding(0);
            this.mnuMaintainInsuranceType.Size = new Size(0xd8, 20);
            this.mnuMaintainInsuranceType.Text = "Insurance Type";
            this.mnuMaintainInsuranceCompanyGroup.Name = "mnuMaintainInsuranceCompanyGroup";
            this.mnuMaintainInsuranceCompanyGroup.Padding = new Padding(0);
            this.mnuMaintainInsuranceCompanyGroup.Size = new Size(0xd8, 20);
            this.mnuMaintainInsuranceCompanyGroup.Text = "Insurance Company Group";
            this.mnuMaintainInvoiceForm.Name = "mnuMaintainInvoiceForm";
            this.mnuMaintainInvoiceForm.Padding = new Padding(0);
            this.mnuMaintainInvoiceForm.Size = new Size(0xd8, 20);
            this.mnuMaintainInvoiceForm.Text = "Invoice Form";
            this.mnuMaintainMedicalConditions.Name = "mnuMaintainMedicalConditions";
            this.mnuMaintainMedicalConditions.Padding = new Padding(0);
            this.mnuMaintainMedicalConditions.Size = new Size(0xd8, 20);
            this.mnuMaintainMedicalConditions.Text = "Medical Conditions";
            this.mnuMaintainPOSType.Name = "mnuMaintainPOSType";
            this.mnuMaintainPOSType.Padding = new Padding(0);
            this.mnuMaintainPOSType.Size = new Size(0xd8, 20);
            this.mnuMaintainPOSType.Text = "POS Type";
            this.mnuMaintainPredefinedText.Name = "mnuMaintainPredefinedText";
            this.mnuMaintainPredefinedText.Padding = new Padding(0);
            this.mnuMaintainPredefinedText.Size = new Size(0xd8, 20);
            this.mnuMaintainPredefinedText.Text = "Predefined Text";
            this.mnuMaintainProductType.Name = "mnuMaintainProductType";
            this.mnuMaintainProductType.Padding = new Padding(0);
            this.mnuMaintainProductType.Size = new Size(0xd8, 20);
            this.mnuMaintainProductType.Text = "Product Type";
            this.mnuMaintainProviderNumberType.Name = "mnuMaintainProviderNumberType";
            this.mnuMaintainProviderNumberType.Padding = new Padding(0);
            this.mnuMaintainProviderNumberType.Size = new Size(0xd8, 20);
            this.mnuMaintainProviderNumberType.Text = "Provider Number Type";
            this.mnuMaintainReferralType.Name = "mnuMaintainReferralType";
            this.mnuMaintainReferralType.Padding = new Padding(0);
            this.mnuMaintainReferralType.Size = new Size(0xd8, 20);
            this.mnuMaintainReferralType.Text = "Referral Type";
            this.mnuMaintainShippingMethod.Name = "mnuMaintainShippingMethod";
            this.mnuMaintainShippingMethod.Padding = new Padding(0);
            this.mnuMaintainShippingMethod.Size = new Size(0xd8, 20);
            this.mnuMaintainShippingMethod.Text = "Shipping Method";
            this.mnuMaintainTaxRate.Name = "mnuMaintainTaxRate";
            this.mnuMaintainTaxRate.Padding = new Padding(0);
            this.mnuMaintainTaxRate.Size = new Size(0xd8, 20);
            this.mnuMaintainTaxRate.Text = "Tax Rate";
            this.mnuMaintainZipCode.Name = "mnuMaintainZipCode";
            this.mnuMaintainZipCode.Padding = new Padding(0);
            this.mnuMaintainZipCode.Size = new Size(0xd8, 20);
            this.mnuMaintainZipCode.Text = "Zip Codes";
            this.mnuMaintainCMNRX.Name = "mnuMaintainCMNRX";
            this.mnuMaintainCMNRX.Padding = new Padding(0);
            this.mnuMaintainCMNRX.Size = new Size(180, 20);
            this.mnuMaintainCMNRX.Text = "CMN Form";
            this.mnuMaintainCompany.Name = "mnuMaintainCompany";
            this.mnuMaintainCompany.Padding = new Padding(0);
            this.mnuMaintainCompany.Size = new Size(180, 20);
            this.mnuMaintainCompany.Text = "Company";
            this.mnuMaintainCompliance.Name = "mnuMaintainCompliance";
            this.mnuMaintainCompliance.Padding = new Padding(0);
            this.mnuMaintainCompliance.Size = new Size(180, 20);
            this.mnuMaintainCompliance.Text = "Compliance";
            this.mnuMaintainCrystalReport.Name = "mnuMaintainCrystalReport";
            this.mnuMaintainCrystalReport.Padding = new Padding(0);
            this.mnuMaintainCrystalReport.Size = new Size(180, 20);
            this.mnuMaintainCrystalReport.Text = "Crystal Report";
            this.mnuMaintainCallback.Name = "mnuMaintainCallback";
            this.mnuMaintainCallback.Padding = new Padding(0);
            this.mnuMaintainCallback.Size = new Size(180, 20);
            this.mnuMaintainCallback.Text = "Callback form";
            this.mnuMaintainCustomer.Name = "mnuMaintainCustomer";
            this.mnuMaintainCustomer.Padding = new Padding(0);
            this.mnuMaintainCustomer.Size = new Size(180, 20);
            this.mnuMaintainCustomer.Text = "Customer";
            this.mnuMaintainDoctor.Name = "mnuMaintainDoctor";
            this.mnuMaintainDoctor.Padding = new Padding(0);
            this.mnuMaintainDoctor.Size = new Size(180, 20);
            this.mnuMaintainDoctor.Text = "Doctor";
            this.mnuMaintainFacility.Name = "mnuMaintainFacility";
            this.mnuMaintainFacility.Padding = new Padding(0);
            this.mnuMaintainFacility.Size = new Size(180, 20);
            this.mnuMaintainFacility.Text = "Facility";
            this.mnuMaintainHAO.Name = "mnuMaintainHAO";
            this.mnuMaintainHAO.Padding = new Padding(0);
            this.mnuMaintainHAO.Size = new Size(180, 20);
            this.mnuMaintainHAO.Text = "HAO";
            this.mnuMaintainImage.Name = "mnuMaintainImage";
            this.mnuMaintainImage.Padding = new Padding(0);
            this.mnuMaintainImage.Size = new Size(180, 20);
            this.mnuMaintainImage.Text = "Image";
            this.mnuMaintainInsuranceCompany.Name = "mnuMaintainInsuranceCompany";
            this.mnuMaintainInsuranceCompany.Padding = new Padding(0);
            this.mnuMaintainInsuranceCompany.Size = new Size(180, 20);
            this.mnuMaintainInsuranceCompany.Text = "Insurance Company";
            this.mnuMaintainInventory.Name = "mnuMaintainInventory";
            this.mnuMaintainInventory.Padding = new Padding(0);
            this.mnuMaintainInventory.Size = new Size(180, 20);
            this.mnuMaintainInventory.Text = "Inventory";
            this.mnuMaintainInventoryItem.Name = "mnuMaintainInventoryItem";
            this.mnuMaintainInventoryItem.Padding = new Padding(0);
            this.mnuMaintainInventoryItem.Size = new Size(180, 20);
            this.mnuMaintainInventoryItem.Text = "Inventory Item";
            this.mnuMaintainInvoice.Name = "mnuMaintainInvoice";
            this.mnuMaintainInvoice.Padding = new Padding(0);
            this.mnuMaintainInvoice.Size = new Size(180, 20);
            this.mnuMaintainInvoice.Text = "Invoice";
            this.mnuMaintainKit.Name = "mnuMaintainKit";
            this.mnuMaintainKit.Padding = new Padding(0);
            this.mnuMaintainKit.Size = new Size(180, 20);
            this.mnuMaintainKit.Text = "Kits";
            this.mnuMaintainLegalRep.Name = "mnuMaintainLegalRep";
            this.mnuMaintainLegalRep.Padding = new Padding(0);
            this.mnuMaintainLegalRep.Size = new Size(180, 20);
            this.mnuMaintainLegalRep.Text = "Legal Rep";
            this.mnuMaintainLocation.Name = "mnuMaintainLocation";
            this.mnuMaintainLocation.Padding = new Padding(0);
            this.mnuMaintainLocation.Size = new Size(180, 20);
            this.mnuMaintainLocation.Text = "Location";
            this.mnuMaintainManufacturer.Name = "mnuMaintainManufacturer";
            this.mnuMaintainManufacturer.Padding = new Padding(0);
            this.mnuMaintainManufacturer.Size = new Size(180, 20);
            this.mnuMaintainManufacturer.Text = "Manufacturer";
            this.mnuMaintainOrder.Name = "mnuMaintainOrder";
            this.mnuMaintainOrder.Padding = new Padding(0);
            this.mnuMaintainOrder.Size = new Size(180, 20);
            this.mnuMaintainOrder.Text = "Order";
            this.mnuMaintainProvider.Name = "mnuMaintainProvider";
            this.mnuMaintainProvider.Padding = new Padding(0);
            this.mnuMaintainProvider.Size = new Size(180, 20);
            this.mnuMaintainProvider.Text = "Provider";
            this.mnuMaintainReferral.Name = "mnuMaintainReferral";
            this.mnuMaintainReferral.Padding = new Padding(0);
            this.mnuMaintainReferral.Size = new Size(180, 20);
            this.mnuMaintainReferral.Text = "Referral";
            this.mnuMaintainSalesRep.Name = "mnuMaintainSalesRep";
            this.mnuMaintainSalesRep.Padding = new Padding(0);
            this.mnuMaintainSalesRep.Size = new Size(180, 20);
            this.mnuMaintainSalesRep.Text = "Sales Rep";
            this.mnuMaintainSerial.Name = "mnuMaintainSerial";
            this.mnuMaintainSerial.Padding = new Padding(0);
            this.mnuMaintainSerial.Size = new Size(180, 20);
            this.mnuMaintainSerial.Text = "Serial";
            this.mnuMaintainSurvey.Name = "mnuMaintainSurvey";
            this.mnuMaintainSurvey.Padding = new Padding(0);
            this.mnuMaintainSurvey.Size = new Size(180, 20);
            this.mnuMaintainSurvey.Text = "Survey";
            this.mnuMaintainUser.Name = "mnuMaintainUser";
            this.mnuMaintainUser.Padding = new Padding(0);
            this.mnuMaintainUser.Size = new Size(180, 20);
            this.mnuMaintainUser.Text = "User";
            this.mnuMaintainVendor.Name = "mnuMaintainVendor";
            this.mnuMaintainVendor.Padding = new Padding(0);
            this.mnuMaintainVendor.Size = new Size(180, 20);
            this.mnuMaintainVendor.Text = "Vendor";
            this.mnuMaintainWarehouse.Name = "mnuMaintainWarehouse";
            this.mnuMaintainWarehouse.Padding = new Padding(0);
            this.mnuMaintainWarehouse.Size = new Size(180, 20);
            this.mnuMaintainWarehouse.Text = "Warehouse";
            ToolStripItem[] itemArray6 = new ToolStripItem[0x11];
            itemArray6[0] = this.nmuProcessBatchPayments;
            itemArray6[1] = this.mnuProcessOxygen;
            itemArray6[2] = this.mnuProcessPurchaseOrder;
            itemArray6[3] = this.MenuItem3;
            itemArray6[4] = this.mnuProcessActiveCompliances;
            itemArray6[5] = this.mnuProcessSessions;
            itemArray6[6] = this.mnuProcessEligibility;
            itemArray6[7] = this.mnuProcessMissingInformation;
            itemArray6[8] = this.mnuProcessOrders;
            itemArray6[9] = this.mnuPointOfSale;
            itemArray6[10] = this.mnuProcessPrintInvoices;
            itemArray6[11] = this.mnuProcessRentalPickup;
            itemArray6[12] = this.mnuProcessReports;
            itemArray6[13] = this.mnuProcessSearchForImages;
            itemArray6[14] = this.mnuProcessSecureCare;
            itemArray6[15] = this.mnuProcessUpdateAllowables;
            itemArray6[0x10] = this.mnuProcessUpdatePriceList;
            this.mnuProcess.DropDownItems.AddRange(itemArray6);
            this.mnuProcess.Name = "mnuProcess";
            this.mnuProcess.Size = new Size(0x3b, 20);
            this.mnuProcess.Text = "Process";
            this.nmuProcessBatchPayments.Name = "nmuProcessBatchPayments";
            this.nmuProcessBatchPayments.Padding = new Padding(0);
            this.nmuProcessBatchPayments.Size = new Size(0xb5, 20);
            this.nmuProcessBatchPayments.Text = "Batch Payments";
            ToolStripItem[] itemArray7 = new ToolStripItem[] { this.mnuProcessOxygenSend, this.mnuProcessOxygenReceive, this.mnuProcessOxygenRent, this.mnuProcessOxygenPickup };
            this.mnuProcessOxygen.DropDownItems.AddRange(itemArray7);
            this.mnuProcessOxygen.Name = "mnuProcessOxygen";
            this.mnuProcessOxygen.Padding = new Padding(0);
            this.mnuProcessOxygen.Size = new Size(0xb5, 20);
            this.mnuProcessOxygen.Text = "Oxygen";
            this.mnuProcessOxygenSend.Name = "mnuProcessOxygenSend";
            this.mnuProcessOxygenSend.Padding = new Padding(0);
            this.mnuProcessOxygenSend.Size = new Size(180, 20);
            this.mnuProcessOxygenSend.Text = "Send Tanks";
            this.mnuProcessOxygenReceive.Name = "mnuProcessOxygenReceive";
            this.mnuProcessOxygenReceive.Padding = new Padding(0);
            this.mnuProcessOxygenReceive.Size = new Size(180, 20);
            this.mnuProcessOxygenReceive.Text = "Receive Tanks";
            this.mnuProcessOxygenRent.Name = "mnuProcessOxygenRent";
            this.mnuProcessOxygenRent.Padding = new Padding(0);
            this.mnuProcessOxygenRent.Size = new Size(180, 20);
            this.mnuProcessOxygenRent.Text = "Rent Tanks";
            this.mnuProcessOxygenPickup.Name = "mnuProcessOxygenPickup";
            this.mnuProcessOxygenPickup.Padding = new Padding(0);
            this.mnuProcessOxygenPickup.Size = new Size(180, 20);
            this.mnuProcessOxygenPickup.Text = "Pick up Tanks";
            this.mnuProcessPurchaseOrder.Name = "mnuProcessPurchaseOrder";
            this.mnuProcessPurchaseOrder.Padding = new Padding(0);
            this.mnuProcessPurchaseOrder.Size = new Size(0xb5, 20);
            this.mnuProcessPurchaseOrder.Text = "Purchase Order";
            this.MenuItem3.Name = "MenuItem3";
            this.MenuItem3.Size = new Size(0xb2, 6);
            this.mnuProcessActiveCompliances.Name = "mnuProcessActiveCompliances";
            this.mnuProcessActiveCompliances.Padding = new Padding(0);
            this.mnuProcessActiveCompliances.Size = new Size(0xb5, 20);
            this.mnuProcessActiveCompliances.Text = "Active Compliances";
            this.mnuProcessSessions.Name = "mnuProcessSessions";
            this.mnuProcessSessions.Padding = new Padding(0);
            this.mnuProcessSessions.Size = new Size(0xb5, 20);
            this.mnuProcessSessions.Text = "Connections";
            this.mnuProcessEligibility.Name = "mnuProcessEligibility";
            this.mnuProcessEligibility.Padding = new Padding(0);
            this.mnuProcessEligibility.Size = new Size(0xb5, 20);
            this.mnuProcessEligibility.Text = "Eligibility";
            this.mnuProcessMissingInformation.Name = "mnuProcessMissingInformation";
            this.mnuProcessMissingInformation.Padding = new Padding(0);
            this.mnuProcessMissingInformation.Size = new Size(0xb5, 20);
            this.mnuProcessMissingInformation.Text = "Missing Information";
            this.mnuProcessOrders.Name = "mnuProcessOrders";
            this.mnuProcessOrders.Padding = new Padding(0);
            this.mnuProcessOrders.Size = new Size(0xb5, 20);
            this.mnuProcessOrders.Text = "Orders";
            ToolStripItem[] itemArray8 = new ToolStripItem[] { this.mnuProcessRetailSales, this.mnuProcessReturnSales };
            this.mnuPointOfSale.DropDownItems.AddRange(itemArray8);
            this.mnuPointOfSale.Name = "mnuPointOfSale";
            this.mnuPointOfSale.Padding = new Padding(0);
            this.mnuPointOfSale.Size = new Size(0xb5, 20);
            this.mnuPointOfSale.Text = "Point of sale";
            this.mnuProcessRetailSales.Name = "mnuProcessRetailSales";
            this.mnuProcessRetailSales.Size = new Size(180, 0x16);
            this.mnuProcessRetailSales.Text = "Retail Sales";
            this.mnuProcessReturnSales.Name = "mnuProcessReturnSales";
            this.mnuProcessReturnSales.Size = new Size(180, 0x16);
            this.mnuProcessReturnSales.Text = "Return Sales";
            this.mnuProcessPrintInvoices.Name = "mnuProcessPrintInvoices";
            this.mnuProcessPrintInvoices.Padding = new Padding(0);
            this.mnuProcessPrintInvoices.Size = new Size(0xb5, 20);
            this.mnuProcessPrintInvoices.Text = "Print Invoices";
            this.mnuProcessRentalPickup.Name = "mnuProcessRentalPickup";
            this.mnuProcessRentalPickup.Padding = new Padding(0);
            this.mnuProcessRentalPickup.Size = new Size(0xb5, 20);
            this.mnuProcessRentalPickup.Text = "Rental Pickup";
            this.mnuProcessReports.Name = "mnuProcessReports";
            this.mnuProcessReports.Padding = new Padding(0);
            this.mnuProcessReports.Size = new Size(0xb5, 20);
            this.mnuProcessReports.Text = "Reports";
            this.mnuProcessSearchForImages.Name = "mnuProcessSearchForImages";
            this.mnuProcessSearchForImages.Padding = new Padding(0);
            this.mnuProcessSearchForImages.Size = new Size(0xb5, 20);
            this.mnuProcessSearchForImages.Text = "Search for images";
            this.mnuProcessSecureCare.Name = "mnuProcessSecureCare";
            this.mnuProcessSecureCare.Padding = new Padding(0);
            this.mnuProcessSecureCare.Size = new Size(0xb5, 20);
            this.mnuProcessSecureCare.Text = "Secure Care";
            this.mnuProcessSecureCare.Visible = false;
            this.mnuProcessUpdateAllowables.Name = "mnuProcessUpdateAllowables";
            this.mnuProcessUpdateAllowables.Padding = new Padding(0);
            this.mnuProcessUpdateAllowables.Size = new Size(0xb5, 20);
            this.mnuProcessUpdateAllowables.Text = "Update Allowables";
            this.mnuProcessUpdatePriceList.Name = "mnuProcessUpdatePriceList";
            this.mnuProcessUpdatePriceList.Padding = new Padding(0);
            this.mnuProcessUpdatePriceList.Size = new Size(0xb5, 20);
            this.mnuProcessUpdatePriceList.Text = "Update Price List";
            ToolStripItem[] itemArray9 = new ToolStripItem[] { this.mnuWindowCascade, this.mnuWindowTileHorizontal, this.mnuWindowTileVertical, this.mnuWindowSeparator };
            this.mnuWindow.DropDownItems.AddRange(itemArray9);
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Size = new Size(0x3f, 20);
            this.mnuWindow.Text = "&Window";
            this.mnuWindowCascade.Name = "mnuWindowCascade";
            this.mnuWindowCascade.Size = new Size(180, 0x16);
            this.mnuWindowCascade.Text = "&Cascade";
            this.mnuWindowTileHorizontal.Name = "mnuWindowTileHorizontal";
            this.mnuWindowTileHorizontal.Size = new Size(180, 0x16);
            this.mnuWindowTileHorizontal.Text = "Tile &Horizontal";
            this.mnuWindowTileVertical.Name = "mnuWindowTileVertical";
            this.mnuWindowTileVertical.Size = new Size(180, 0x16);
            this.mnuWindowTileVertical.Text = "Tile &Vertical";
            this.mnuWindowSeparator.Name = "mnuWindowSeparator";
            this.mnuWindowSeparator.Size = new Size(0xb1, 6);
            ToolStripItem[] itemArray10 = new ToolStripItem[] { this.mnuHelpContents, this.mnuHelpBar0, this.mnuWebMedicare, this.mnuWebDMEWorks, this.MenuItem1, this.mnuHelpAbout, this.mnuHelpTipOfTheDay };
            this.mnuHelp.DropDownItems.AddRange(itemArray10);
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new Size(0x2c, 20);
            this.mnuHelp.Text = "&Help";
            this.mnuHelpContents.Name = "mnuHelpContents";
            this.mnuHelpContents.Padding = new Padding(0);
            this.mnuHelpContents.Size = new Size(200, 20);
            this.mnuHelpContents.Text = "&Contents";
            this.mnuHelpBar0.Name = "mnuHelpBar0";
            this.mnuHelpBar0.Size = new Size(0xc5, 6);
            ToolStripItem[] itemArray11 = new ToolStripItem[] { this.mnuWebCMS, this.mnuWebRegionA, this.mnuWebRegionB, this.mnuWebRegionC, this.mnuWebRegionD };
            this.mnuWebMedicare.DropDownItems.AddRange(itemArray11);
            this.mnuWebMedicare.Name = "mnuWebMedicare";
            this.mnuWebMedicare.Padding = new Padding(0);
            this.mnuWebMedicare.Size = new Size(200, 20);
            this.mnuWebMedicare.Text = "Medicare";
            this.mnuWebCMS.Name = "mnuWebCMS";
            this.mnuWebCMS.Size = new Size(0x7a, 0x16);
            this.mnuWebCMS.Text = "CMS";
            this.mnuWebRegionA.Name = "mnuWebRegionA";
            this.mnuWebRegionA.Size = new Size(0x7a, 0x16);
            this.mnuWebRegionA.Text = "Region A";
            this.mnuWebRegionB.Name = "mnuWebRegionB";
            this.mnuWebRegionB.Size = new Size(0x7a, 0x16);
            this.mnuWebRegionB.Text = "Region B";
            this.mnuWebRegionC.Name = "mnuWebRegionC";
            this.mnuWebRegionC.Size = new Size(0x7a, 0x16);
            this.mnuWebRegionC.Text = "Region C";
            this.mnuWebRegionD.Name = "mnuWebRegionD";
            this.mnuWebRegionD.Size = new Size(0x7a, 0x16);
            this.mnuWebRegionD.Text = "Region D";
            this.mnuWebDMEWorks.Name = "mnuWebDMEWorks";
            this.mnuWebDMEWorks.Padding = new Padding(0);
            this.mnuWebDMEWorks.Size = new Size(200, 20);
            this.mnuWebDMEWorks.Text = "DME Works Home page";
            this.MenuItem1.Name = "MenuItem1";
            this.MenuItem1.Size = new Size(0xc5, 6);
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Padding = new Padding(0);
            this.mnuHelpAbout.Size = new Size(200, 20);
            this.mnuHelpAbout.Text = "&About ";
            this.mnuHelpTipOfTheDay.Name = "mnuHelpTipOfTheDay";
            this.mnuHelpTipOfTheDay.Padding = new Padding(0);
            this.mnuHelpTipOfTheDay.Size = new Size(200, 20);
            this.mnuHelpTipOfTheDay.Text = "Tip of the day...";
            ToolStripItem[] itemArray12 = new ToolStripItem[] { this.tsslCompany, this.tsslLocation, this.tsslUser, this.tsslVersion, this.tsslFunctions, this.ToolStripStatusLabel1 };
            this.StatusStrip1.Items.AddRange(itemArray12);
            this.StatusStrip1.Location = new Point(0, 0x1af);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new Size(0x278, 0x16);
            this.StatusStrip1.TabIndex = 3;
            this.tsslCompany.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslCompany.Margin = new Padding(0, 2, 0, 1);
            this.tsslCompany.Name = "tsslCompany";
            this.tsslCompany.Padding = new Padding(1, 0, 1, 0);
            this.tsslCompany.Size = new Size(0x41, 0x13);
            this.tsslCompany.Text = "Company";
            this.tsslLocation.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslLocation.Margin = new Padding(0, 2, 0, 1);
            this.tsslLocation.Name = "tsslLocation";
            this.tsslLocation.Padding = new Padding(1, 0, 1, 0);
            this.tsslLocation.Size = new Size(0x3b, 0x13);
            this.tsslLocation.Text = "Location";
            this.tsslUser.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslUser.Margin = new Padding(0, 2, 0, 1);
            this.tsslUser.Name = "tsslUser";
            this.tsslUser.Padding = new Padding(1, 0, 1, 0);
            this.tsslUser.Size = new Size(0x24, 0x13);
            this.tsslUser.Text = "User";
            this.tsslVersion.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslVersion.Margin = new Padding(0, 2, 0, 1);
            this.tsslVersion.Name = "tsslVersion";
            this.tsslVersion.Padding = new Padding(1, 0, 1, 0);
            this.tsslVersion.Size = new Size(0x33, 0x13);
            this.tsslVersion.Text = "Version";
            this.tsslVersion.Visible = false;
            this.tsslFunctions.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.tsslFunctions.Margin = new Padding(0, 2, 0, 1);
            this.tsslFunctions.Name = "tsslFunctions";
            this.tsslFunctions.Padding = new Padding(1, 0, 1, 0);
            this.tsslFunctions.Size = new Size(0x41, 0x13);
            this.tsslFunctions.Text = "Functions";
            this.tsslFunctions.Visible = false;
            this.ToolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.All;
            this.ToolStripStatusLabel1.Margin = new Padding(0, 2, 0, 1);
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Padding = new Padding(1, 0, 1, 0);
            this.ToolStripStatusLabel1.Size = new Size(0x1c9, 0x13);
            this.ToolStripStatusLabel1.Spring = true;
            ToolStripItem[] itemArray13 = new ToolStripItem[] { this.mnuFile, this.mnuCreateEdit, this.mnuOrdering, this.mnuMaintain, this.mnuProcess, this.mnuWindow, this.mnuTools, this.mnuHelp };
            this.MenuStrip1.Items.AddRange(itemArray13);
            this.MenuStrip1.Location = new Point(0, 0);
            this.MenuStrip1.MdiWindowListItem = this.mnuWindow;
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new Size(0x278, 0x18);
            this.MenuStrip1.TabIndex = 5;
            ToolStripItem[] itemArray14 = new ToolStripItem[10];
            itemArray14[0] = this.tsmiOrderingActiveCompliances;
            itemArray14[1] = this.tsmiOrderingBatchPayments;
            itemArray14[2] = this.tsmiOrderingCmnForm;
            itemArray14[3] = this.tsmiOrderingCompliance;
            itemArray14[4] = this.tsmiOrderingCallbackForm;
            itemArray14[5] = this.tsmiOrderingCustomer;
            itemArray14[6] = this.tsmiOrderingImage;
            itemArray14[7] = this.tsmiOrderingInvoice;
            itemArray14[8] = this.tsmiOrderingMissingInformation;
            itemArray14[9] = this.tsmiOrderingOrder;
            this.mnuOrdering.DropDownItems.AddRange(itemArray14);
            this.mnuOrdering.Name = "mnuOrdering";
            this.mnuOrdering.Size = new Size(0x42, 20);
            this.mnuOrdering.Text = "Ordering";
            this.tsmiOrderingActiveCompliances.Name = "tsmiOrderingActiveCompliances";
            this.tsmiOrderingActiveCompliances.Padding = new Padding(0);
            this.tsmiOrderingActiveCompliances.Size = new Size(0xb5, 20);
            this.tsmiOrderingActiveCompliances.Text = "Active Compliances";
            this.tsmiOrderingBatchPayments.Name = "tsmiOrderingBatchPayments";
            this.tsmiOrderingBatchPayments.Padding = new Padding(0);
            this.tsmiOrderingBatchPayments.Size = new Size(0xb5, 20);
            this.tsmiOrderingBatchPayments.Text = "Batch Payments";
            this.tsmiOrderingCmnForm.Name = "tsmiOrderingCmnForm";
            this.tsmiOrderingCmnForm.Padding = new Padding(0);
            this.tsmiOrderingCmnForm.Size = new Size(0xb5, 20);
            this.tsmiOrderingCmnForm.Text = "CMN Form";
            this.tsmiOrderingCompliance.Name = "tsmiOrderingCompliance";
            this.tsmiOrderingCompliance.Padding = new Padding(0);
            this.tsmiOrderingCompliance.Size = new Size(0xb5, 20);
            this.tsmiOrderingCompliance.Text = "Compliance";
            this.tsmiOrderingCallbackForm.Name = "tsmiOrderingCallbackForm";
            this.tsmiOrderingCallbackForm.Padding = new Padding(0);
            this.tsmiOrderingCallbackForm.Size = new Size(0xb5, 20);
            this.tsmiOrderingCallbackForm.Text = "Callback form";
            this.tsmiOrderingCustomer.Name = "tsmiOrderingCustomer";
            this.tsmiOrderingCustomer.Padding = new Padding(0);
            this.tsmiOrderingCustomer.Size = new Size(0xb5, 20);
            this.tsmiOrderingCustomer.Text = "Customer";
            this.tsmiOrderingImage.Name = "tsmiOrderingImage";
            this.tsmiOrderingImage.Padding = new Padding(0);
            this.tsmiOrderingImage.Size = new Size(0xb5, 20);
            this.tsmiOrderingImage.Text = "Image";
            this.tsmiOrderingInvoice.Name = "tsmiOrderingInvoice";
            this.tsmiOrderingInvoice.Padding = new Padding(0);
            this.tsmiOrderingInvoice.Size = new Size(0xb5, 20);
            this.tsmiOrderingInvoice.Text = "Invoice";
            this.tsmiOrderingMissingInformation.Name = "tsmiOrderingMissingInformation";
            this.tsmiOrderingMissingInformation.Padding = new Padding(0);
            this.tsmiOrderingMissingInformation.Size = new Size(0xb5, 20);
            this.tsmiOrderingMissingInformation.Text = "Missing Information";
            this.tsmiOrderingOrder.Name = "tsmiOrderingOrder";
            this.tsmiOrderingOrder.Padding = new Padding(0);
            this.tsmiOrderingOrder.Size = new Size(0xb5, 20);
            this.tsmiOrderingOrder.Text = "Order";
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new Size(0x2f, 20);
            this.mnuTools.Text = "Tools";
            this.mnuTools.Visible = false;
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 0x3e8;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.StatusStrip1);
            base.Controls.Add(this.MenuStrip1);
            base.IsMdiContainer = true;
            base.MainMenuStrip = this.MenuStrip1;
            base.Name = "FormMain";
            this.Text = "DMEWorks";
            base.WindowState = FormWindowState.Maximized;
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void mnuCompanyChangeLocation_Click(object sender, EventArgs e)
        {
            this.ShowLocationDialog();
        }

        private void mnuCompanyExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mnuCompanyLogin_Click(object sender, EventArgs e)
        {
            this.DoLogin();
        }

        private void mnuCompanyLogoff_Click(object sender, EventArgs e)
        {
            this.DoLogoff();
        }

        private void mnuFileNewUser_Click(object sender, EventArgs e)
        {
            ClassGlobalObjects.ShowForm(FormFactories.FormUser());
        }

        private void mnuFileThrowException_Click(object sender, EventArgs e)
        {
        }

        public void mnuHelpAbout_Click(object eventSender, EventArgs eventArgs)
        {
            using (frmAbout about = new frmAbout())
            {
                about.ShowDialog();
            }
        }

        private void mnuHelpTipOfTheDay_Click(object sender, EventArgs e)
        {
            using (FormTips tips = new FormTips())
            {
                tips.ShowDialog();
            }
        }

        private void mnuWebCMS_Click(object sender, EventArgs e)
        {
            try
            {
                ClassGlobalObjects.OpenUrl("http://www.cms.hhs.gov");
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

        private void mnuWebDMEWorks_Click(object sender, EventArgs e)
        {
            try
            {
                ClassGlobalObjects.OpenUrl("http://www.dmeworks.com");
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

        private void mnuWebRegionA_Click(object sender, EventArgs e)
        {
            try
            {
                ClassGlobalObjects.OpenUrl("https://med.medicarenhic.com/");
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

        private void mnuWebRegionB_Click(object sender, EventArgs e)
        {
            try
            {
                ClassGlobalObjects.OpenUrl("https://med.medicarenhic.com/");
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

        private void mnuWebRegionC_Click(object sender, EventArgs e)
        {
            try
            {
                ClassGlobalObjects.OpenUrl("http://www.cgsmedicare.com/");
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

        private void mnuWebRegionD_Click(object sender, EventArgs e)
        {
            try
            {
                ClassGlobalObjects.OpenUrl("https://www.noridianmedicare.com/dme/");
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

        public void mnuWindowCascade_Click(object eventSender, EventArgs eventArgs)
        {
            base.LayoutMdi(MdiLayout.Cascade);
        }

        public void mnuWindowTileHorizontal_Click(object eventSender, EventArgs eventArgs)
        {
            base.LayoutMdi(MdiLayout.TileHorizontal);
        }

        public void mnuWindowTileVertical_Click(object eventSender, EventArgs eventArgs)
        {
            base.LayoutMdi(MdiLayout.TileVertical);
        }

        private static void SetEnabled(ToolStripItem Item, bool Enabled)
        {
            if (Item != null)
            {
                FormToolStripMenuItem item = Item as FormToolStripMenuItem;
                Item.Enabled = ((item == null) || (item.Factory == null)) ? Enabled : (Enabled && item.Factory.GetPermissions().Allow_VIEW);
                ToolStripDropDownItem item2 = Item as ToolStripDropDownItem;
                if (item2 != null)
                {
                    IEnumerator enumerator;
                    try
                    {
                        enumerator = item2.DropDownItems.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            SetEnabled((ToolStripItem) enumerator.Current, Item.Enabled);
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
            }
        }

        private void ShellLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShellLinkToolStripMenuItem item = sender as ShellLinkToolStripMenuItem;
                if ((item != null) && File.Exists(item.Path))
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.UseShellExecute = true;
                        if (Environment.OSVersion.Version.Major > 5)
                        {
                            process.StartInfo.FileName = item.Path;
                            process.StartInfo.Verb = "open";
                        }
                        else
                        {
                            string path = MsiShortcutParser.ParseShortcut(item.Path);
                            path ??= item.Path;
                            process.StartInfo.FileName = path;
                            using (ShellShortcut shortcut = new ShellShortcut(item.Path))
                            {
                                process.StartInfo.WorkingDirectory = shortcut.WorkingDirectory;
                                process.StartInfo.Arguments = shortcut.Arguments;
                            }
                        }
                        process.Start();
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

        public void ShowEligibilityNotification(int requestId)
        {
            try
            {
                int num;
                string str;
                int num2;
                string str2;
                using (MySqlConnection connection = new MySqlConnection(Globals.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.CommandText = "SELECT\r\n  request.ID\r\n, customer.ID as CustomerID\r\n, CONCAT(customer.LastName, ', ', customer.FirstName) as CustomerName\r\n, CASE WHEN policy.RelationshipCode = '18' \r\n       THEN CONCAT(customer.LastName, ', ', customer.FirstName)\r\n       ELSE CONCAT(policy  .LastName, ', ', policy.FirstName) END as InsuredName\r\n, insco.ID   as InsuranceCompanyID\r\n, insco.Name as InsuranceCompanyName\r\nFROM tbl_ability_eligibility_request as request\r\n     INNER JOIN tbl_customer_insurance as policy ON request.CustomerID          = policy.CustomerID\r\n                                                AND request.CustomerInsuranceID = policy.ID\r\n     INNER JOIN tbl_customer as customer ON request.CustomerID = customer.ID\r\n     LEFT JOIN tbl_insurancecompany as insco ON policy.InsuranceCompanyID = insco.ID\r\nWHERE request.ID = " + Conversions.ToString(requestId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                num = Convert.ToInt32(reader["CustomerID"]);
                                str = Convert.ToString(reader["CustomerName"]);
                                num2 = Convert.ToInt32(reader["InsuranceCompanyID"]);
                                str2 = Convert.ToString(reader["InsuranceCompanyName"]);
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                string text = $"You've got new response from {str2} for {str}" + "\r\n\r\nWould you like to view it?";
                if (MessageBox.Show(this, text, this.Text + " : Eligibility", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    FormParameters @params = new FormParameters {
                        ["CustomerID"] = num,
                        ["InsuranceCompanyID"] = num2
                    };
                    this.ShowForm(FormFactories.FormEligibility(), @params, false);
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception, "Eligibility");
                ProjectData.ClearProjectError();
            }
        }

        public void ShowFileReport(string ReportFileName, ReportParameters Params, bool Modal)
        {
            try
            {
                if (!File.Exists(ReportFileName))
                {
                    throw new FileNotFoundException("Path provided for ReportFileName points to nowhere", ReportFileName);
                }
                DataSource dataSource = new OdbcDataSource(Globals.ODBCDSN, Globals.CompanyDatabase, "DMEUser", "DMEPassword");
                this.ShowReport(ReportFileName, dataSource, Params, Modal);
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

        public void ShowFileReport(string ReportFileName, string DatasetFileName, ReportParameters Params, bool Modal)
        {
            try
            {
                if (!File.Exists(ReportFileName))
                {
                    throw new FileNotFoundException("Path provided for ReportFileName points to nowhere", ReportFileName);
                }
                if (!File.Exists(DatasetFileName))
                {
                    throw new FileNotFoundException("Path provided for DatasetFileName points to nowhere", DatasetFileName);
                }
                DataSource dataSource = new FileDataSource(DatasetFileName);
                this.ShowReport(ReportFileName, dataSource, Params, Modal);
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

        public void ShowForm(FormFactory Factory, FormParameters Params, bool Modal)
        {
            try
            {
                if (Factory == null)
                {
                    throw new ArgumentNullException("Factory");
                }
                if (!Factory.GetPermissions().Allow_VIEW)
                {
                    throw new UserNotifyException("Cannot open form since you do not have such rights");
                }
                Form form = Factory.CreateForm();
                IParameters parameters = form as IParameters;
                if (Modal)
                {
                    form.Owner = this;
                    if (parameters != null)
                    {
                        parameters.SetParameters(Params);
                    }
                    form.ShowDialog();
                }
                else
                {
                    form.MdiParent = this;
                    if (form.Visible)
                    {
                        form.BringToFront();
                    }
                    else
                    {
                        form.Show();
                    }
                    if (parameters != null)
                    {
                        parameters.SetParameters(Params);
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

        public void ShowLocationDialog()
        {
            if (Globals.Connected)
            {
                using (DialogLocation location = new DialogLocation())
                {
                    location.Owner = this;
                    location.StartPosition = FormStartPosition.CenterParent;
                    location.LocationID = Globals.LocationID;
                    if (location.ShowDialog(this) == DialogResult.OK)
                    {
                        Globals.LocationID = location.LocationID;
                        this.tsslLocation.Text = "Location: " + Globals.LocationName;
                    }
                }
            }
        }

        public void ShowMessageNotification(string message)
        {
            string text = "You've got new notification\r\n\r\n" + message;
            MessageBox.Show(this, text, this.Text + " : Message Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void ShowReport(string ReportFileName, ReportParameters Params, bool Modal)
        {
            try
            {
                if (string.IsNullOrEmpty(ReportFileName))
                {
                    throw new UserNotifyException("Crystal report file name is empty.");
                }
                string str = ClassGlobalObjects.FindReport(ReportFileName);
                if (string.IsNullOrEmpty(str))
                {
                    throw new UserNotifyException($"Crystal report file {ReportFileName} does not exists neither in folder 'Reports' nor in folder 'Custom'.");
                }
                this.ShowReport(str, new OdbcDataSource(Globals.ODBCDSN, Globals.CompanyDatabase, "DMEUser", "DMEPassword"), Params, Modal);
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

        public void ShowReport(string FileName, DataSource DataSource, ReportParameters Params, bool Modal)
        {
            ReportViewer viewer = new ReportViewer();
            viewer.LoadReport(FileName, DataSource, Params);
            if (!Modal)
            {
                viewer.MdiParent = this;
                viewer.Show();
            }
            else
            {
                viewer.ShowInTaskbar = true;
                viewer.Owner = this;
                viewer.ShowDialog();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Notification notification = Globals.GetNotification();
            if (notification != null)
            {
                if ("Eligibility".Equals(notification.Type, StringComparison.OrdinalIgnoreCase))
                {
                    int num;
                    if (int.TryParse(notification.Args, out num))
                    {
                        this.ShowEligibilityNotification(num);
                    }
                }
                else if ("Message".Equals(notification.Type, StringComparison.OrdinalIgnoreCase))
                {
                    this.ShowMessageNotification(notification.Args);
                }
                Globals.DismissNotification(notification);
            }
        }

        private void UpdateLoginState()
        {
            bool connected = Globals.Connected;
            this.mnuFileLogin.Enabled = !connected;
            this.mnuFileLogoff.Enabled = connected;
            this.mnuFileChangeLocation.Enabled = connected;
            SetEnabled(this.mnuCreateEdit, connected);
            SetEnabled(this.mnuOrdering, connected);
            SetEnabled(this.mnuMaintain, connected);
            SetEnabled(this.mnuProcess, connected);
            this.mnuMaintainImage.Enabled = this.mnuMaintainImage.Enabled && (Globals.CompanyImagingUri != null);
        }

        [field: AccessedThroughProperty("MenuItem4")]
        private ToolStripMenuItem MenuItem4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuAdminThrowException")]
        private ToolStripMenuItem mnuAdminThrowException { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuCreateEdit")]
        private ToolStripMenuItem mnuCreateEdit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuCreateEditCustomer")]
        private FormToolStripMenuItem mnuCreateEditCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuCreateEditOrder")]
        private FormToolStripMenuItem mnuCreateEditOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuCreateEditOrderConfirm")]
        private FormToolStripMenuItem mnuCreateEditOrderConfirm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuCreateEditOrderNew")]
        private FormToolStripMenuItem mnuCreateEditOrderNew { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFile")]
        private ToolStripMenuItem mnuFile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFileExit")]
        private ToolStripMenuItem mnuFileExit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFileLogin")]
        private ToolStripMenuItem mnuFileLogin { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFileLogoff")]
        private ToolStripMenuItem mnuFileLogoff { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuHelp")]
        private ToolStripMenuItem mnuHelp { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuHelpAbout")]
        private ToolStripMenuItem mnuHelpAbout { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuHelpContents")]
        private ToolStripMenuItem mnuHelpContents { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuHelpTipOfTheDay")]
        private ToolStripMenuItem mnuHelpTipOfTheDay { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintain")]
        private ToolStripMenuItem mnuMaintain { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainAuxilary")]
        private ToolStripMenuItem mnuMaintainAuxilary { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainBillingType")]
        private FormToolStripMenuItem mnuMaintainBillingType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCallback")]
        private FormToolStripMenuItem mnuMaintainCallback { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCMNRX")]
        private FormToolStripMenuItem mnuMaintainCMNRX { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCompany")]
        private FormToolStripMenuItem mnuMaintainCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCompliance")]
        private FormToolStripMenuItem mnuMaintainCompliance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCrystalReport")]
        private FormToolStripMenuItem mnuMaintainCrystalReport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCustomer")]
        private FormToolStripMenuItem mnuMaintainCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCustomerClass")]
        private FormToolStripMenuItem mnuMaintainCustomerClass { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainCustomerType")]
        private FormToolStripMenuItem mnuMaintainCustomerType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainDenial")]
        private FormToolStripMenuItem mnuMaintainDenial { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainDoctor")]
        private FormToolStripMenuItem mnuMaintainDoctor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainDoctorType")]
        private FormToolStripMenuItem mnuMaintainDoctorType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainFacility")]
        private FormToolStripMenuItem mnuMaintainFacility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainHAO")]
        private FormToolStripMenuItem mnuMaintainHAO { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainICD9")]
        private FormToolStripMenuItem mnuMaintainICD9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainICD10")]
        private FormToolStripMenuItem mnuMaintainICD10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainInsuranceCompany")]
        private FormToolStripMenuItem mnuMaintainInsuranceCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainInsuranceType")]
        private FormToolStripMenuItem mnuMaintainInsuranceType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainInventory")]
        private FormToolStripMenuItem mnuMaintainInventory { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainInventoryItem")]
        private FormToolStripMenuItem mnuMaintainInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainInvoice")]
        private FormToolStripMenuItem mnuMaintainInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainInvoiceForm")]
        private FormToolStripMenuItem mnuMaintainInvoiceForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainKit")]
        private FormToolStripMenuItem mnuMaintainKit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainLegalRep")]
        private FormToolStripMenuItem mnuMaintainLegalRep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainLocation")]
        private FormToolStripMenuItem mnuMaintainLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainManufacturer")]
        private FormToolStripMenuItem mnuMaintainManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainMedicalConditions")]
        private FormToolStripMenuItem mnuMaintainMedicalConditions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainOrder")]
        private FormToolStripMenuItem mnuMaintainOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainPOSType")]
        private FormToolStripMenuItem mnuMaintainPOSType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainPredefinedText")]
        private FormToolStripMenuItem mnuMaintainPredefinedText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainPriceCode")]
        private FormToolStripMenuItem mnuMaintainPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainPricing")]
        private FormToolStripMenuItem mnuMaintainPricing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainAuthorizationType")]
        private FormToolStripMenuItem mnuMaintainAuthorizationType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainProductType")]
        private FormToolStripMenuItem mnuMaintainProductType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainProvider")]
        private FormToolStripMenuItem mnuMaintainProvider { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainProviderNumberType")]
        private FormToolStripMenuItem mnuMaintainProviderNumberType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainReferral")]
        private FormToolStripMenuItem mnuMaintainReferral { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainReferralType")]
        private FormToolStripMenuItem mnuMaintainReferralType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainSalesRep")]
        private FormToolStripMenuItem mnuMaintainSalesRep { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainSerial")]
        private FormToolStripMenuItem mnuMaintainSerial { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainSurvey")]
        private FormToolStripMenuItem mnuMaintainSurvey { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainShippingMethod")]
        private FormToolStripMenuItem mnuMaintainShippingMethod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainTaxRate")]
        private FormToolStripMenuItem mnuMaintainTaxRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainUser")]
        private FormToolStripMenuItem mnuMaintainUser { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainVendor")]
        private FormToolStripMenuItem mnuMaintainVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainWarehouse")]
        private FormToolStripMenuItem mnuMaintainWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainZipCode")]
        private FormToolStripMenuItem mnuMaintainZipCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainImage")]
        private FormToolStripMenuItem mnuMaintainImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuMaintainInsuranceCompanyGroup")]
        private FormToolStripMenuItem mnuMaintainInsuranceCompanyGroup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuPointOfSale")]
        private ToolStripMenuItem mnuPointOfSale { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcess")]
        private ToolStripMenuItem mnuProcess { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessActiveCompliances")]
        private FormToolStripMenuItem mnuProcessActiveCompliances { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessMissingInformation")]
        private FormToolStripMenuItem mnuProcessMissingInformation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessOrders")]
        private FormToolStripMenuItem mnuProcessOrders { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessOxygen")]
        private FormToolStripMenuItem mnuProcessOxygen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessOxygenPickup")]
        private FormToolStripMenuItem mnuProcessOxygenPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessOxygenReceive")]
        private FormToolStripMenuItem mnuProcessOxygenReceive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessOxygenRent")]
        private FormToolStripMenuItem mnuProcessOxygenRent { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessOxygenSend")]
        private FormToolStripMenuItem mnuProcessOxygenSend { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessPrintInvoices")]
        private FormToolStripMenuItem mnuProcessPrintInvoices { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessPurchaseOrder")]
        private FormToolStripMenuItem mnuProcessPurchaseOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessReports")]
        private FormToolStripMenuItem mnuProcessReports { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessRetailSales")]
        private FormToolStripMenuItem mnuProcessRetailSales { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessReturnSales")]
        private FormToolStripMenuItem mnuProcessReturnSales { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessSecureCare")]
        private FormToolStripMenuItem mnuProcessSecureCare { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessUpdateAllowables")]
        private FormToolStripMenuItem mnuProcessUpdateAllowables { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessUpdatePriceList")]
        private FormToolStripMenuItem mnuProcessUpdatePriceList { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWebCMS")]
        private ToolStripMenuItem mnuWebCMS { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWebDMEWorks")]
        private ToolStripMenuItem mnuWebDMEWorks { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWebMedicare")]
        private ToolStripMenuItem mnuWebMedicare { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWebRegionA")]
        private ToolStripMenuItem mnuWebRegionA { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWebRegionB")]
        private ToolStripMenuItem mnuWebRegionB { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWebRegionC")]
        private ToolStripMenuItem mnuWebRegionC { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWebRegionD")]
        private ToolStripMenuItem mnuWebRegionD { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWindow")]
        private ToolStripMenuItem mnuWindow { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWindowCascade")]
        private ToolStripMenuItem mnuWindowCascade { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWindowTileHorizontal")]
        private ToolStripMenuItem mnuWindowTileHorizontal { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWindowTileVertical")]
        private ToolStripMenuItem mnuWindowTileVertical { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmuProcessBatchPayments")]
        private FormToolStripMenuItem nmuProcessBatchPayments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("StatusStrip1")]
        private StatusStrip StatusStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslCompany")]
        private ToolStripStatusLabel tsslCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslVersion")]
        private ToolStripStatusLabel tsslVersion { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslUser")]
        private ToolStripStatusLabel tsslUser { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStripStatusLabel1")]
        private ToolStripStatusLabel ToolStripStatusLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFileChangeLocation")]
        private ToolStripMenuItem mnuFileChangeLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslLocation")]
        private ToolStripStatusLabel tsslLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("MenuStrip1")]
        private MenuStrip MenuStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuAdminSeparator3")]
        private ToolStripSeparator mnuAdminSeparator3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("MenuItem3")]
        private ToolStripSeparator MenuItem3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuHelpBar0")]
        private ToolStripSeparator mnuHelpBar0 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("MenuItem1")]
        private ToolStripSeparator MenuItem1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessSearchForImages")]
        private FormToolStripMenuItem mnuProcessSearchForImages { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuWindowSeparator")]
        private ToolStripSeparator mnuWindowSeparator { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessSessions")]
        private FormToolStripMenuItem mnuProcessSessions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessEligibility")]
        private FormToolStripMenuItem mnuProcessEligibility { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsslFunctions")]
        private ToolStripStatusLabel tsslFunctions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuTools")]
        private ToolStripMenuItem mnuTools { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuOrdering")]
        private ToolStripMenuItem mnuOrdering { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingCmnForm")]
        private FormToolStripMenuItem tsmiOrderingCmnForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingCompliance")]
        private FormToolStripMenuItem tsmiOrderingCompliance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingCallbackForm")]
        private FormToolStripMenuItem tsmiOrderingCallbackForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingCustomer")]
        private FormToolStripMenuItem tsmiOrderingCustomer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingImage")]
        private FormToolStripMenuItem tsmiOrderingImage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingInvoice")]
        private FormToolStripMenuItem tsmiOrderingInvoice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingOrder")]
        private FormToolStripMenuItem tsmiOrderingOrder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingActiveCompliances")]
        private FormToolStripMenuItem tsmiOrderingActiveCompliances { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingBatchPayments")]
        private FormToolStripMenuItem tsmiOrderingBatchPayments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiOrderingMissingInformation")]
        private FormToolStripMenuItem tsmiOrderingMissingInformation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Timer1")]
        private Timer Timer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuProcessRentalPickup")]
        private FormToolStripMenuItem mnuProcessRentalPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public delegate void FormRepeater(Form AForm, object Param);
    }
}

