namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.CrystalReports;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using DMEWorks.Misc;
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
    using System.Xml;

    [DesignerGenerated]
    public class FormPurchaseOrder : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormPurchaseOrder()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_purchaseorder", "tbl_purchaseorderdetails", "tbl_location", "tbl_vendor", "tbl_user_location" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            options = new NavigatorOptions {
                Caption = "Details",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Details_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Details_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Details_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Details_NavigatorRowClick),
                Switchable = false
            };
            string[] textArray2 = new string[] { "tbl_purchaseorderdetails", "tbl_purchaseorder", "tbl_location", "tbl_vendor", "tbl_warehouse", "tbl_inventoryitem", "tbl_manufacturer", "tbl_user_location" };
            options.TableNames = textArray2;
            base.AddNavigator(options);
            Functions.AttachPhoneAutoInput(this.txtShipToPhone);
            base.ChangesTracker.Subscribe(this.nmbCost);
            base.ChangesTracker.Subscribe(this.nmbFreight);
            base.ChangesTracker.Subscribe(this.nmbTax);
            base.ChangesTracker.Subscribe(this.nmbTotalDue);
            base.ChangesTracker.Subscribe(this.CAddressShipTo);
            base.ChangesTracker.Subscribe(this.txtShipToPhone);
            base.ChangesTracker.Subscribe(this.dtbOrderDate);
            base.ChangesTracker.Subscribe(this.txtConfirmationNumber);
        }

        private void Appearance_CellFormatting(object sender, GridCellFormattingEventArgs e)
        {
            try
            {
                DataRow dataRow = e.Row.GetDataRow();
                if (dataRow != null)
                {
                    if (dataRow.Table.Columns.Contains("Approved"))
                    {
                        object obj2 = dataRow["Approved"];
                        if (!(obj2 is DBNull))
                        {
                            if (Convert.ToInt32(obj2) == 0)
                            {
                                e.CellStyle.BackColor = Color.LightYellow;
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (dataRow.Table.Columns.Contains("BackOrdered"))
                    {
                        object obj3 = dataRow["BackOrdered"];
                        if (!(obj3 is DBNull) && (Convert.ToInt32(obj3) == 0))
                        {
                            e.CellStyle.BackColor = Color.LightSteelBlue;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        private void CalcCost()
        {
            this.nmbCost.AsDouble = new double?(this.nmbFreight.AsDouble.GetValueOrDefault(0.0) + this.PurchaseOrderItems.GetTotal());
            this.CalcTotal();
        }

        private void CalcTotal()
        {
            this.nmbTotalDue.AsDouble = new double?(this.nmbCost.AsDouble.GetValueOrDefault(0.0) + this.nmbTax.AsDouble.GetValueOrDefault(0.0));
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtCompanyName, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressCompany.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressCompany.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressCompany.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressCompany.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressCompany.txtZip, DBNull.Value);
            this.LoadCompany();
            Functions.SetTextBoxText(this.txtNumber, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbLocation, NullableConvert.ToDb(Globals.LocationID));
            Functions.SetComboBoxValue(this.cmbVendor, DBNull.Value);
            Functions.SetTextBoxText(this.txtVendorAccountNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtConfirmationNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtShipToName, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressShipTo.txtAddress1, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressShipTo.txtAddress2, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressShipTo.txtCity, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressShipTo.txtState, DBNull.Value);
            Functions.SetTextBoxText(this.CAddressShipTo.txtZip, DBNull.Value);
            Functions.SetTextBoxText(this.txtShipToPhone, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbCost, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbFreight, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbTax, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbTotalDue, DBNull.Value);
            Functions.SetDateBoxValue(this.dtbOrderDate, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbReoccuring, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbApproved, DBNull.Value);
            this.ApprovedState = false;
            this.PurchaseOrderItems.ClearGrid();
            this.ApprovedState = false;
        }

        private void cmbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow selectedRow = this.cmbVendor.SelectedRow;
                if (selectedRow == null)
                {
                    Functions.SetTextBoxText(this.txtVendorAccountNumber, "");
                }
                else
                {
                    Functions.SetTextBoxText(this.txtVendorAccountNumber, selectedRow["AccountNumber"]);
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

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "DELETE po\r\nFROM tbl_purchaseorder as po\r\n     LEFT JOIN tbl_purchaseorderdetails as pod ON po.ID = pod.PurchaseOrderID\r\nWHERE (po.ID = :ID)\r\n  AND (pod.PurchaseOrderID IS NULL)";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteNonQuery())
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
            }
        }

        private void Details_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Details_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(GetDetailsQuery(Globals.LocationID, new short?(Globals.CompanyUserID), this.ArchivedFilter), ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Details_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "Details #", 50);
            appearance.AddTextColumn("PurchaseOrderID", "Order #", 50);
            appearance.AddTextColumn("Number", "Number", 50);
            appearance.AddTextColumn("InventoryItem", "Inv Item", 120);
            appearance.AddTextColumn("Customer", "Customer", 80);
            appearance.AddTextColumn("Manufacturer", "Manufacturer", 80);
            appearance.AddTextColumn("Warehouse", "Warehouse", 80);
            appearance.AddTextColumn("Vendor", "Vendor", 80);
            appearance.AddTextColumn("OrderDate", "Order Date", 80, appearance.DateStyle());
            appearance.AddTextColumn("Location", "Location", 80);
            appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(this.Appearance_CellFormatting);
        }

        private void Details_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            try
            {
                DataRow dataRow = args.GridRow.GetDataRow();
                base.OpenObject(dataRow["PurchaseOrderID"]);
                this.PurchaseOrderItems.ShowDetails(dataRow["ID"]);
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

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected static string GetDetailsQuery(int? LocationID, short? UserID, YesNoAny filter)
        {
            string str = (LocationID == null) ? ((UserID == null) ? "1 = 1" : ("tbl_location.ID IS NULL OR tbl_location.ID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + UserID.Value.ToString() + ")")) : ("tbl_location.ID = " + LocationID.Value.ToString());
            string str2 = (filter != YesNoAny.Yes) ? ((filter != YesNoAny.No) ? "1 = 1" : "tbl_purchaseorder.Archived = 0") : "tbl_purchaseorder.Archived = 1";
            string[] textArray1 = new string[] { "SELECT\r\n  tbl_purchaseorderdetails.ID\r\n, tbl_purchaseorder.Number\r\n, tbl_purchaseorderdetails.PurchaseOrderID\r\n, tbl_purchaseorderdetails.Customer\r\n, tbl_vendor.Name AS Vendor\r\n, tbl_purchaseorder.OrderDate\r\n, IFNULL(tbl_purchaseorderdetails.Ordered, 0) - IFNULL(tbl_purchaseorderdetails.Received, 0) AS BackOrdered\r\n, tbl_warehouse.Name AS Warehouse\r\n, tbl_manufacturer.Name AS Manufacturer\r\n, tbl_inventoryitem.Name AS InventoryItem\r\n, tbl_location.Name AS Location\r\nFROM tbl_purchaseorderdetails\r\n     LEFT JOIN tbl_purchaseorder ON tbl_purchaseorderdetails.PurchaseOrderID = tbl_purchaseorder.ID\r\n     LEFT JOIN tbl_location      ON tbl_purchaseorder.LocationID = tbl_location.ID\r\n     LEFT JOIN tbl_vendor        ON tbl_purchaseorder.VendorID = tbl_vendor.ID\r\n     LEFT JOIN tbl_warehouse     ON tbl_purchaseorderdetails.WarehouseID = tbl_warehouse.ID\r\n     LEFT JOIN tbl_inventoryitem ON tbl_purchaseorderdetails.InventoryItemID = tbl_inventoryitem.ID\r\n     LEFT JOIN tbl_manufacturer  ON tbl_inventoryitem.ManufacturerID = tbl_manufacturer.ID\r\nWHERE (", str2, ")\r\n  AND (", str, ")\r\nORDER BY tbl_purchaseorderdetails.PurchaseOrderID DESC" };
            return string.Concat(textArray1);
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete purchase order #{this.ObjectID}?";
            messages.DeletedSuccessfully = $"purchase order #{this.ObjectID} was successfully deleted.";
            messages.ObjectToBeDeletedIsNotFound = $"Cannot delete purchase order #{this.ObjectID}. It has line items.";
            return messages;
        }

        private static string GetQuery(int? LocationID, short? UserID, YesNoAny filter)
        {
            string str = (LocationID == null) ? ((UserID == null) ? "1 = 1" : ("tbl_location.ID IS NULL OR tbl_location.ID IN (SELECT LocationID FROM tbl_user_location WHERE UserID = " + UserID.Value.ToString() + ")")) : ("tbl_location.ID = " + LocationID.Value.ToString());
            string str2 = (filter != YesNoAny.Yes) ? ((filter != YesNoAny.No) ? "1 = 1" : "tbl_purchaseorder.Archived = 0") : "tbl_purchaseorder.Archived = 1";
            string[] textArray1 = new string[] { "SELECT\r\n  tbl_purchaseorder.ID,\r\n  tbl_purchaseorder.Number,\r\n  tbl_vendor.Name AS Vendor,\r\n  tbl_purchaseorder.OrderDate,\r\n  SUM(IFNULL(tbl_purchaseorderdetails.Ordered, 0) - IFNULL(tbl_purchaseorderdetails.Received, 0)) AS BackOrdered,\r\n  tbl_purchaseorder.SubmittedDate,\r\n  tbl_location.Name AS Location,\r\n  tbl_purchaseorder.Approved\r\nFROM tbl_purchaseorder\r\n     LEFT OUTER JOIN tbl_purchaseorderdetails ON tbl_purchaseorder.ID = tbl_purchaseorderdetails.PurchaseOrderID\r\n     LEFT OUTER JOIN tbl_location ON tbl_purchaseorder.LocationID = tbl_location.ID\r\n     LEFT OUTER JOIN tbl_vendor ON tbl_purchaseorder.VendorID = tbl_vendor.ID\r\nWHERE (", str2, ")\r\n  AND (", str, ")\r\nGROUP BY tbl_purchaseorder.ID, tbl_purchaseorder.Number, tbl_vendor.Name,\r\n         tbl_purchaseorder.OrderDate, tbl_purchaseorder.SubmittedDate,\r\n         tbl_location.Name, tbl_purchaseorder.Approved\r\nORDER BY tbl_purchaseorder.ID DESC" };
            return string.Concat(textArray1);
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbLocation, "tbl_location", null);
            Cache.InitDropdown(this.cmbVendor, "tbl_vendor", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbVendor = new Combobox();
            this.txtVendorAccountNumber = new TextBox();
            this.lblVendorAccountNumber = new Label();
            this.lblVendor = new Label();
            this.gbVendor = new GroupBox();
            this.lblConfirmationNumber = new Label();
            this.txtConfirmationNumber = new TextBox();
            this.lblCost = new Label();
            this.nmbCost = new NumericBox();
            this.nmbFreight = new NumericBox();
            this.lblFreight = new Label();
            this.nmbTax = new NumericBox();
            this.lblTax = new Label();
            this.nmbTotalDue = new NumericBox();
            this.lblTotalDue = new Label();
            this.Panel1 = new Panel();
            this.lblLocation = new Label();
            this.cmbLocation = new Combobox();
            this.lblPurchaseOrderID = new Label();
            this.lblNumber = new Label();
            this.txtNumber = new TextBox();
            this.lblReoccuring = new Label();
            this.chbReoccuring = new CheckBox();
            this.lblApproved = new Label();
            this.dtbOrderDate = new UltraDateTimeEditor();
            this.lblOrderDate = new Label();
            this.chbApproved = new CheckBox();
            this.PurchaseOrderItems = new ControlPurchaseOrderItems();
            this.TabControl1 = new TabControl();
            this.tpDetails = new TabPage();
            this.tpShipTo = new TabPage();
            this.txtShipToName = new TextBox();
            this.lblShipToName = new Label();
            this.txtShipToPhone = new TextBox();
            this.lblShipToPhone = new Label();
            this.CAddressShipTo = new ControlAddress();
            this.tpCompany = new TabPage();
            this.txtCompanyName = new TextBox();
            this.lblCompanyName = new Label();
            this.CAddressCompany = new ControlAddress();
            this.mnuFilterNotArchived = new MenuItem();
            this.mnuFilterAll = new MenuItem();
            this.mnuFilterArchived = new MenuItem();
            this.cmsGridSearch = new ContextMenuStrip(this.components);
            this.tsmiGridSearchArchive = new ToolStripMenuItem();
            this.tsmiGridSearchUnarchive = new ToolStripMenuItem();
            this.mnuActionReceive = new MenuItem();
            base.tpWorkArea.SuspendLayout();
            this.gbVendor.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tpDetails.SuspendLayout();
            this.tpShipTo.SuspendLayout();
            this.tpCompany.SuspendLayout();
            this.cmsGridSearch.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x298, 0x1e2);
            MenuItem[] items = new MenuItem[] { this.mnuActionReceive };
            base.cmnuActions.MenuItems.AddRange(items);
            MenuItem[] itemArray2 = new MenuItem[] { this.mnuFilterNotArchived, this.mnuFilterAll, this.mnuFilterArchived };
            base.cmnuFilter.MenuItems.AddRange(itemArray2);
            this.cmbVendor.BackColor = SystemColors.Control;
            this.cmbVendor.Location = new Point(80, 0x10);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.Size = new Size(240, 0x15);
            this.cmbVendor.TabIndex = 1;
            this.txtVendorAccountNumber.Location = new Point(80, 40);
            this.txtVendorAccountNumber.Name = "txtVendorAccountNumber";
            this.txtVendorAccountNumber.Size = new Size(240, 20);
            this.txtVendorAccountNumber.TabIndex = 3;
            this.lblVendorAccountNumber.Location = new Point(8, 40);
            this.lblVendorAccountNumber.Margin = new Padding(0);
            this.lblVendorAccountNumber.Name = "lblVendorAccountNumber";
            this.lblVendorAccountNumber.Size = new Size(0x40, 0x15);
            this.lblVendorAccountNumber.TabIndex = 2;
            this.lblVendorAccountNumber.Text = "Account #";
            this.lblVendorAccountNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblVendor.Location = new Point(8, 0x10);
            this.lblVendor.Margin = new Padding(0);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new Size(0x40, 0x15);
            this.lblVendor.TabIndex = 0;
            this.lblVendor.Text = "Vendor";
            this.lblVendor.TextAlign = ContentAlignment.MiddleRight;
            this.gbVendor.Controls.Add(this.lblConfirmationNumber);
            this.gbVendor.Controls.Add(this.txtConfirmationNumber);
            this.gbVendor.Controls.Add(this.lblVendor);
            this.gbVendor.Controls.Add(this.cmbVendor);
            this.gbVendor.Controls.Add(this.txtVendorAccountNumber);
            this.gbVendor.Controls.Add(this.lblVendorAccountNumber);
            this.gbVendor.Location = new Point(0x148, 8);
            this.gbVendor.Name = "gbVendor";
            this.gbVendor.Size = new Size(0x148, 0x58);
            this.gbVendor.TabIndex = 15;
            this.gbVendor.TabStop = false;
            this.gbVendor.Text = "Vendor";
            this.lblConfirmationNumber.Location = new Point(8, 0x40);
            this.lblConfirmationNumber.Margin = new Padding(0);
            this.lblConfirmationNumber.Name = "lblConfirmationNumber";
            this.lblConfirmationNumber.Size = new Size(0x40, 0x15);
            this.lblConfirmationNumber.TabIndex = 7;
            this.lblConfirmationNumber.Text = "Confirm #";
            this.lblConfirmationNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtConfirmationNumber.Location = new Point(80, 0x40);
            this.txtConfirmationNumber.Name = "txtConfirmationNumber";
            this.txtConfirmationNumber.Size = new Size(240, 20);
            this.txtConfirmationNumber.TabIndex = 6;
            this.lblCost.Location = new Point(0x18, 0x38);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new Size(0x38, 0x15);
            this.lblCost.TabIndex = 5;
            this.lblCost.Text = "Cost";
            this.lblCost.TextAlign = ContentAlignment.MiddleRight;
            this.nmbCost.Location = new Point(0x58, 0x38);
            this.nmbCost.Name = "nmbCost";
            this.nmbCost.Size = new Size(80, 20);
            this.nmbCost.TabIndex = 6;
            this.nmbFreight.Location = new Point(0x58, 80);
            this.nmbFreight.Name = "nmbFreight";
            this.nmbFreight.Size = new Size(80, 20);
            this.nmbFreight.TabIndex = 8;
            this.lblFreight.Location = new Point(0x18, 80);
            this.lblFreight.Name = "lblFreight";
            this.lblFreight.Size = new Size(0x38, 0x15);
            this.lblFreight.TabIndex = 7;
            this.lblFreight.Text = "Freight";
            this.lblFreight.TextAlign = ContentAlignment.MiddleRight;
            this.nmbTax.Location = new Point(240, 0x38);
            this.nmbTax.Name = "nmbTax";
            this.nmbTax.Size = new Size(80, 20);
            this.nmbTax.TabIndex = 12;
            this.lblTax.Location = new Point(0xb0, 0x38);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new Size(0x38, 0x15);
            this.lblTax.TabIndex = 11;
            this.lblTax.Text = "Tax";
            this.lblTax.TextAlign = ContentAlignment.MiddleRight;
            this.nmbTotalDue.Location = new Point(240, 80);
            this.nmbTotalDue.Name = "nmbTotalDue";
            this.nmbTotalDue.Size = new Size(80, 20);
            this.nmbTotalDue.TabIndex = 14;
            this.lblTotalDue.Location = new Point(0xb0, 80);
            this.lblTotalDue.Name = "lblTotalDue";
            this.lblTotalDue.Size = new Size(0x38, 0x15);
            this.lblTotalDue.TabIndex = 13;
            this.lblTotalDue.Text = "Total Due";
            this.lblTotalDue.TextAlign = ContentAlignment.MiddleRight;
            this.Panel1.Controls.Add(this.lblLocation);
            this.Panel1.Controls.Add(this.cmbLocation);
            this.Panel1.Controls.Add(this.lblPurchaseOrderID);
            this.Panel1.Controls.Add(this.lblNumber);
            this.Panel1.Controls.Add(this.txtNumber);
            this.Panel1.Controls.Add(this.lblReoccuring);
            this.Panel1.Controls.Add(this.chbReoccuring);
            this.Panel1.Controls.Add(this.lblApproved);
            this.Panel1.Controls.Add(this.dtbOrderDate);
            this.Panel1.Controls.Add(this.lblOrderDate);
            this.Panel1.Controls.Add(this.chbApproved);
            this.Panel1.Controls.Add(this.nmbCost);
            this.Panel1.Controls.Add(this.lblCost);
            this.Panel1.Controls.Add(this.nmbTax);
            this.Panel1.Controls.Add(this.nmbTotalDue);
            this.Panel1.Controls.Add(this.lblFreight);
            this.Panel1.Controls.Add(this.lblTotalDue);
            this.Panel1.Controls.Add(this.gbVendor);
            this.Panel1.Controls.Add(this.lblTax);
            this.Panel1.Controls.Add(this.nmbFreight);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x298, 0x88);
            this.Panel1.TabIndex = 0;
            this.lblLocation.Location = new Point(0x14c, 0x68);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new Size(0x48, 0x15);
            this.lblLocation.TabIndex = 20;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = ContentAlignment.MiddleRight;
            this.cmbLocation.Location = new Point(0x198, 0x68);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new Size(240, 0x15);
            this.cmbLocation.TabIndex = 0x15;
            this.lblPurchaseOrderID.BorderStyle = BorderStyle.FixedSingle;
            this.lblPurchaseOrderID.Location = new Point(0x10, 0x68);
            this.lblPurchaseOrderID.Name = "lblPurchaseOrderID";
            this.lblPurchaseOrderID.Size = new Size(100, 0x15);
            this.lblPurchaseOrderID.TabIndex = 0x12;
            this.lblPurchaseOrderID.Text = "PO # 00000";
            this.lblPurchaseOrderID.TextAlign = ContentAlignment.MiddleCenter;
            this.lblNumber.Location = new Point(0xc0, 8);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new Size(40, 0x15);
            this.lblNumber.TabIndex = 0x10;
            this.lblNumber.Text = "PO #";
            this.lblNumber.TextAlign = ContentAlignment.MiddleRight;
            this.txtNumber.Location = new Point(240, 8);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new Size(80, 20);
            this.txtNumber.TabIndex = 0x11;
            this.txtNumber.Text = "(AutoGenerate)";
            this.lblReoccuring.Location = new Point(0xa8, 0x20);
            this.lblReoccuring.Name = "lblReoccuring";
            this.lblReoccuring.Size = new Size(0x40, 0x15);
            this.lblReoccuring.TabIndex = 9;
            this.lblReoccuring.Text = "Reoccuring";
            this.lblReoccuring.TextAlign = ContentAlignment.MiddleRight;
            this.chbReoccuring.Location = new Point(240, 0x20);
            this.chbReoccuring.Name = "chbReoccuring";
            this.chbReoccuring.Size = new Size(0x10, 0x15);
            this.chbReoccuring.TabIndex = 10;
            this.lblApproved.Location = new Point(0x10, 0x20);
            this.lblApproved.Name = "lblApproved";
            this.lblApproved.Size = new Size(0x40, 0x15);
            this.lblApproved.TabIndex = 3;
            this.lblApproved.Text = "Approved";
            this.lblApproved.TextAlign = ContentAlignment.MiddleRight;
            this.dtbOrderDate.Location = new Point(0x58, 8);
            this.dtbOrderDate.Name = "dtbOrderDate";
            this.dtbOrderDate.Size = new Size(0x60, 0x15);
            this.dtbOrderDate.TabIndex = 1;
            this.lblOrderDate.Location = new Point(0x10, 8);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new Size(0x40, 0x15);
            this.lblOrderDate.TabIndex = 0;
            this.lblOrderDate.Text = "Order Date";
            this.lblOrderDate.TextAlign = ContentAlignment.MiddleRight;
            this.chbApproved.Location = new Point(0x58, 0x20);
            this.chbApproved.Name = "chbApproved";
            this.chbApproved.Size = new Size(0x10, 0x15);
            this.chbApproved.TabIndex = 4;
            this.PurchaseOrderItems.Dock = DockStyle.Fill;
            this.PurchaseOrderItems.Location = new Point(0, 0);
            this.PurchaseOrderItems.Name = "PurchaseOrderItems";
            this.PurchaseOrderItems.Size = new Size(0x290, 320);
            this.PurchaseOrderItems.TabIndex = 1;
            this.TabControl1.Controls.Add(this.tpDetails);
            this.TabControl1.Controls.Add(this.tpShipTo);
            this.TabControl1.Controls.Add(this.tpCompany);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.Location = new Point(0, 0x88);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x298, 0x15a);
            this.TabControl1.TabIndex = 2;
            this.tpDetails.Controls.Add(this.PurchaseOrderItems);
            this.tpDetails.Location = new Point(4, 0x16);
            this.tpDetails.Name = "tpDetails";
            this.tpDetails.Size = new Size(0x290, 320);
            this.tpDetails.TabIndex = 0;
            this.tpDetails.Text = "Details";
            this.tpShipTo.Controls.Add(this.txtShipToName);
            this.tpShipTo.Controls.Add(this.lblShipToName);
            this.tpShipTo.Controls.Add(this.txtShipToPhone);
            this.tpShipTo.Controls.Add(this.lblShipToPhone);
            this.tpShipTo.Controls.Add(this.CAddressShipTo);
            this.tpShipTo.Location = new Point(4, 0x16);
            this.tpShipTo.Name = "tpShipTo";
            this.tpShipTo.Size = new Size(0x290, 320);
            this.tpShipTo.TabIndex = 1;
            this.tpShipTo.Text = "Ship To";
            this.txtShipToName.Location = new Point(0x58, 8);
            this.txtShipToName.Name = "txtShipToName";
            this.txtShipToName.Size = new Size(0xe8, 20);
            this.txtShipToName.TabIndex = 1;
            this.lblShipToName.BackColor = Color.Transparent;
            this.lblShipToName.Location = new Point(0x10, 8);
            this.lblShipToName.Name = "lblShipToName";
            this.lblShipToName.Size = new Size(0x40, 0x15);
            this.lblShipToName.TabIndex = 0;
            this.lblShipToName.Text = "Name";
            this.lblShipToName.TextAlign = ContentAlignment.MiddleRight;
            this.txtShipToPhone.Location = new Point(0x58, 0x70);
            this.txtShipToPhone.Name = "txtShipToPhone";
            this.txtShipToPhone.Size = new Size(0xe8, 20);
            this.txtShipToPhone.TabIndex = 4;
            this.lblShipToPhone.Location = new Point(0x10, 0x70);
            this.lblShipToPhone.Name = "lblShipToPhone";
            this.lblShipToPhone.RightToLeft = RightToLeft.No;
            this.lblShipToPhone.Size = new Size(0x40, 0x15);
            this.lblShipToPhone.TabIndex = 3;
            this.lblShipToPhone.Text = "Phone:";
            this.lblShipToPhone.TextAlign = ContentAlignment.MiddleRight;
            this.CAddressShipTo.Location = new Point(0x10, 0x20);
            this.CAddressShipTo.Name = "CAddressShipTo";
            this.CAddressShipTo.Size = new Size(0x130, 0x48);
            this.CAddressShipTo.TabIndex = 2;
            this.tpCompany.Controls.Add(this.txtCompanyName);
            this.tpCompany.Controls.Add(this.lblCompanyName);
            this.tpCompany.Controls.Add(this.CAddressCompany);
            this.tpCompany.Location = new Point(4, 0x16);
            this.tpCompany.Name = "tpCompany";
            this.tpCompany.Size = new Size(0x290, 320);
            this.tpCompany.TabIndex = 2;
            this.tpCompany.Text = "Company";
            this.txtCompanyName.Location = new Point(0x58, 8);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new Size(0xe8, 20);
            this.txtCompanyName.TabIndex = 1;
            this.lblCompanyName.BackColor = Color.Transparent;
            this.lblCompanyName.Location = new Point(0x10, 8);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new Size(0x40, 0x15);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "Name";
            this.lblCompanyName.TextAlign = ContentAlignment.MiddleRight;
            this.CAddressCompany.Location = new Point(0x10, 0x20);
            this.CAddressCompany.Name = "CAddressCompany";
            this.CAddressCompany.Size = new Size(0x130, 0x48);
            this.CAddressCompany.TabIndex = 2;
            this.mnuFilterNotArchived.Checked = true;
            this.mnuFilterNotArchived.Index = 0;
            this.mnuFilterNotArchived.Text = "Not archived";
            this.mnuFilterAll.Index = 1;
            this.mnuFilterAll.Text = "All";
            this.mnuFilterArchived.Index = 2;
            this.mnuFilterArchived.Text = "Archived";
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiGridSearchArchive, this.tsmiGridSearchUnarchive };
            this.cmsGridSearch.Items.AddRange(toolStripItems);
            this.cmsGridSearch.Name = "cmsGridSearch";
            this.cmsGridSearch.Size = new Size(0xa6, 0x30);
            this.tsmiGridSearchArchive.Name = "tsmiGridSearchArchive";
            this.tsmiGridSearchArchive.Size = new Size(0xa5, 0x16);
            this.tsmiGridSearchArchive.Text = "Archive selected";
            this.tsmiGridSearchUnarchive.Name = "tsmiGridSearchUnarchive";
            this.tsmiGridSearchUnarchive.Size = new Size(0xa5, 0x16);
            this.tsmiGridSearchUnarchive.Text = "Unarchive selected";
            this.mnuActionReceive.Index = 0;
            this.mnuActionReceive.Text = "Receive";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x2a0, 0x225);
            base.Name = "FormPurchaseOrder";
            this.Text = "Form Purchase Order";
            base.tpWorkArea.ResumeLayout(false);
            this.gbVendor.ResumeLayout(false);
            this.gbVendor.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.tpDetails.ResumeLayout(false);
            this.tpShipTo.ResumeLayout(false);
            this.tpShipTo.PerformLayout();
            this.tpCompany.ResumeLayout(false);
            this.tpCompany.PerformLayout();
            this.cmsGridSearch.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu {
                MenuItems = { new MenuItem("Selected", new EventHandler(this.mnuPrintSelected_Click)) }
            };
            Cache.AddCategory(menu, "PO", new EventHandler(this.mnuPrintItem_Click));
            base.SetPrintMenu(menu);
        }

        private void LoadCompany()
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_company WHERE ID = 1";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Functions.SetTextBoxText(this.txtCompanyName, reader["Name"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtAddress1, reader["Address1"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtAddress2, reader["Address2"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtCity, reader["City"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtState, reader["State"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtZip, reader["Zip"]);
                        }
                    }
                }
            }
        }

        protected override bool LoadObject(int ID)
        {
            string commandText = $"SELECT tbl_purchaseorder.*,
       tbl_vendor.AccountNumber as VendorAccountNumber
FROM (tbl_purchaseorder
      LEFT JOIN tbl_vendor ON tbl_purchaseorder.VendorID = tbl_vendor.ID)
WHERE tbl_purchaseorder.ID = {ID}";
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                bool flag;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(commandText, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtNumber, reader["Number"]);
                            Functions.SetComboBoxValue(this.cmbLocation, reader["LocationID"]);
                            Functions.SetComboBoxValue(this.cmbVendor, reader["VendorID"]);
                            Functions.SetTextBoxText(this.txtVendorAccountNumber, reader["VendorAccountNumber"]);
                            Functions.SetTextBoxText(this.txtConfirmationNumber, reader["ConfirmationNumber"]);
                            Functions.SetTextBoxText(this.txtCompanyName, reader["CompanyName"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtAddress1, reader["CompanyAddress1"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtAddress2, reader["CompanyAddress2"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtCity, reader["CompanyCity"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtState, reader["CompanyState"]);
                            Functions.SetTextBoxText(this.CAddressCompany.txtZip, reader["CompanyZip"]);
                            Functions.SetTextBoxText(this.txtShipToName, reader["ShipToName"]);
                            Functions.SetTextBoxText(this.CAddressShipTo.txtAddress1, reader["ShipToAddress1"]);
                            Functions.SetTextBoxText(this.CAddressShipTo.txtAddress2, reader["ShipToAddress2"]);
                            Functions.SetTextBoxText(this.CAddressShipTo.txtCity, reader["ShipToCity"]);
                            Functions.SetTextBoxText(this.CAddressShipTo.txtState, reader["ShipToState"]);
                            Functions.SetTextBoxText(this.CAddressShipTo.txtZip, reader["ShipToZip"]);
                            Functions.SetTextBoxText(this.txtShipToPhone, reader["ShipToPhone"]);
                            Functions.SetNumericBoxValue(this.nmbCost, reader["Cost"]);
                            Functions.SetNumericBoxValue(this.nmbFreight, reader["Freight"]);
                            Functions.SetNumericBoxValue(this.nmbTax, reader["Tax"]);
                            Functions.SetNumericBoxValue(this.nmbTotalDue, reader["TotalDue"]);
                            Functions.SetDateBoxValue(this.dtbOrderDate, reader["OrderDate"]);
                            Functions.SetCheckBoxChecked(this.chbReoccuring, reader["Reoccuring"]);
                            Functions.SetCheckBoxChecked(this.chbApproved, reader["Approved"]);
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
                this.ApprovedState = this.chbApproved.Checked;
                this.PurchaseOrderItems.LoadGrid(connection, ID);
                this.ApprovedState = this.chbApproved.Checked;
                return true;
            }
        }

        private void mnuActionReceive_Click(object sender, EventArgs e)
        {
            try
            {
                base.DoSaveClick();
                int? nullable = NullableConvert.ToInt32(this.ObjectID);
                if (nullable != null)
                {
                    using (FormReceivePurchaseOrder order = new FormReceivePurchaseOrder(nullable.Value))
                    {
                        if (order.ShowDialog() == DialogResult.OK)
                        {
                            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                            {
                                connection.Open();
                                this.PurchaseOrderItems.LoadGrid(connection, nullable.Value);
                                ControlPurchaseOrderItems.UpdateSerials(connection, nullable.Value);
                            }
                            base.ResetChangeCount();
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

        private void mnuFilterAll_Click(object sender, EventArgs e)
        {
            this.mnuFilterNotArchived.Checked = false;
            this.mnuFilterAll.Checked = true;
            this.mnuFilterArchived.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuFilterArchived_Click(object sender, EventArgs e)
        {
            this.mnuFilterNotArchived.Checked = false;
            this.mnuFilterAll.Checked = false;
            this.mnuFilterArchived.Checked = true;
            this.InvalidateObjectList();
        }

        private void mnuFilterNotArchived_Click(object sender, EventArgs e)
        {
            this.mnuFilterNotArchived.Checked = true;
            this.mnuFilterAll.Checked = false;
            this.mnuFilterArchived.Checked = false;
            this.InvalidateObjectList();
        }

        private void mnuPrintItem_Click(object sender, EventArgs e)
        {
            ReportMenuItem item = sender as ReportMenuItem;
            if (item != null)
            {
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_purchaseorder.ID}"] = this.ObjectID
                };
                ClassGlobalObjects.ShowReport(item.ReportFileName, @params);
            }
        }

        private void mnuPrintSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (Versioned.IsNumeric(this.ObjectID))
                {
                    if (base.HasUnsavedChanges)
                    {
                        if (MessageBox.Show("Current purchase order has changes that was not saved in the database. Whould you like to save purchase order and print report?", "Purchase order printing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            base.DoSaveClick();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (MessageBox.Show("Current purchase order was not saved. In order to print you should save it. Whould you like to save purchase order and print?", "Purchase order printing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    base.DoSaveClick();
                }
                else
                {
                    return;
                }
                ReportParameters @params = new ReportParameters {
                    ["{?tbl_purchaseorder.ID}"] = this.ObjectID
                };
                ClassGlobalObjects.ShowReport("PurchaseOrder", @params);
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

        private void nmbFreight_ValueChanged(object sender, EventArgs e)
        {
            this.CalcCost();
        }

        private void nmbTax_ValueChanged(object sender, EventArgs e)
        {
            this.CalcTotal();
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_purchaseorder", "tbl_purchaseorderdetails", "tbl_inventory" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private static XmlDocument ParseDocument(string value)
        {
            XmlDocument document;
            if (string.IsNullOrEmpty(value))
            {
                document = null;
            }
            else
            {
                try
                {
                    XmlDocument document1 = new XmlDocument();
                    document1.LoadXml(value);
                    document = document1;
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    document = null;
                    ProjectData.ClearProjectError();
                }
            }
            return document;
        }

        protected void ProcessParameter_Other(FormParameters Params)
        {
            if (Params != null)
            {
                if (Params.ContainsKey("InventoryItemID") && Versioned.IsNumeric(Params["InventoryItemID"]))
                {
                    this.PurchaseOrderItems.AddInventoryItem(Conversions.ToInteger(Params["InventoryItemID"]));
                }
                else if (Params.ContainsKey("OrderXml"))
                {
                    XmlDocument document = ParseDocument(Params["OrderXml"] as string);
                    if (document != null)
                    {
                        this.PurchaseOrderItems.AddInventoryItems(document.DocumentElement);
                    }
                }
            }
        }

        private void PurchaseOrderItems_Changed(object sender, EventArgs e)
        {
            this.CalcCost();
            base.OnObjectChanged(sender);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Approved", MySqlType.Bit).Value = this.chbApproved.Checked;
                    command.Parameters.Add("Reoccuring", MySqlType.Bit).Value = this.chbReoccuring.Checked;
                    command.Parameters.Add("CompanyName", MySqlType.VarChar, 50).Value = this.txtCompanyName.Text;
                    command.Parameters.Add("CompanyAddress1", MySqlType.VarChar, 40).Value = this.CAddressCompany.txtAddress1.Text;
                    command.Parameters.Add("CompanyAddress2", MySqlType.VarChar, 40).Value = this.CAddressCompany.txtAddress2.Text;
                    command.Parameters.Add("CompanyCity", MySqlType.VarChar, 0x19).Value = this.CAddressCompany.txtCity.Text;
                    command.Parameters.Add("CompanyState", MySqlType.Char, 2).Value = this.CAddressCompany.txtState.Text;
                    command.Parameters.Add("CompanyZip", MySqlType.VarChar, 10).Value = this.CAddressCompany.txtZip.Text;
                    command.Parameters.Add("ShipToName", MySqlType.VarChar, 50).Value = this.txtShipToName.Text;
                    command.Parameters.Add("ShipToAddress1", MySqlType.VarChar, 40).Value = this.CAddressShipTo.txtAddress1.Text;
                    command.Parameters.Add("ShipToAddress2", MySqlType.VarChar, 40).Value = this.CAddressShipTo.txtAddress2.Text;
                    command.Parameters.Add("ShipToCity", MySqlType.VarChar, 0x19).Value = this.CAddressShipTo.txtCity.Text;
                    command.Parameters.Add("ShipToState", MySqlType.Char, 2).Value = this.CAddressShipTo.txtState.Text;
                    command.Parameters.Add("ShipToZip", MySqlType.VarChar, 10).Value = this.CAddressShipTo.txtZip.Text;
                    command.Parameters.Add("ShipToPhone", MySqlType.VarChar, 50).Value = this.txtShipToPhone.Text;
                    command.Parameters.Add("Cost", MySqlType.Decimal).Value = this.nmbCost.AsDecimal.GetValueOrDefault(0M);
                    command.Parameters.Add("Freight", MySqlType.Decimal).Value = this.nmbFreight.AsDecimal.GetValueOrDefault(0M);
                    command.Parameters.Add("Tax", MySqlType.Decimal).Value = this.nmbTax.AsDecimal.GetValueOrDefault(0M);
                    command.Parameters.Add("TotalDue", MySqlType.Decimal).Value = this.nmbTotalDue.AsDecimal.GetValueOrDefault(0M);
                    command.Parameters.Add("OrderDate", MySqlType.Date).Value = Functions.GetDateBoxValue(this.dtbOrderDate);
                    command.Parameters.Add("VendorID", MySqlType.Int).Value = this.cmbVendor.SelectedValue;
                    command.Parameters.Add("ConfirmationNumber", MySqlType.VarChar, 50).Value = this.txtConfirmationNumber.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    command.Parameters.Add("LocationID", MySqlType.Int).Value = this.cmbLocation.SelectedValue;
                    command.Parameters.Add("Number", MySqlType.VarChar, 40).Value = this.txtNumber.Text;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        if (((0 >= command.ExecuteUpdate("tbl_purchaseorder", whereParameters)) && (0 < command.ExecuteInsert("tbl_purchaseorder"))) && (string.Compare(this.txtNumber.Text, "(AutoGenerate)", true) == 0))
                        {
                            command.ExecuteCommand("UPDATE tbl_purchaseorder SET Number = CONCAT('PO', ID) WHERE (ID = {0}) AND (Number = '(AutoGenerate)')", ID);
                        }
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_purchaseorder");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                            if (string.Compare(this.txtNumber.Text, "(AutoGenerate)", true) == 0)
                            {
                                command.ExecuteCommand("UPDATE tbl_purchaseorder SET Number = CONCAT('PO', ID) WHERE (ID = {0}) AND (Number = '(AutoGenerate)')", ID);
                            }
                        }
                    }
                }
                this.ApprovedState = this.chbApproved.Checked;
                try
                {
                    this.PurchaseOrderItems.SaveGrid(connection, ID);
                }
                catch (Exception exception1)
                {
                    Exception ex = exception1;
                    ProjectData.SetProjectError(ex);
                    Exception exception = ex;
                    this.ShowException(exception, "Error saving purchase order");
                    ProjectData.ClearProjectError();
                }
                this.ApprovedState = this.chbApproved.Checked;
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(GetQuery(Globals.LocationID, new short?(Globals.CompanyUserID), this.ArchivedFilter), ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.Parameters.Add("LocationID", MySqlType.Int).Value = NullableConvert.ToDb(Globals.LocationID);
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.MultiSelect = true;
            appearance.AddTextColumn("ID", "Order #", 50);
            appearance.AddTextColumn("Number", "Number", 50);
            appearance.AddTextColumn("Vendor", "Vendor", 120);
            appearance.AddTextColumn("OrderDate", "Order Date", 80, appearance.DateStyle());
            appearance.AddTextColumn("SubmittedDate", "Submitted Date", 80, appearance.DateStyle());
            appearance.AddTextColumn("Location", "Location", 80);
            appearance.ContextMenuStrip = this.cmsGridSearch;
            appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(this.Appearance_CellFormatting);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__198-0 e$__- = new _Closure$__198-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void SetParameters(FormParameters Params)
        {
            base.ProcessParameter_EntityCreatedListener(Params);
            base.ProcessParameter_TabPage(Params);
            this.ProcessParameter_Other(Params);
        }

        private void tsmiGridSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GridBase base2 = this.cmsGridSearch.SourceControl<GridBase>();
                if (base2 != null)
                {
                    int[] numArray = base2.GetSelectedRows().GetDataRows().Select<DataRow, int?>(((_Closure$__.$I235-0 == null) ? (_Closure$__.$I235-0 = new Func<DataRow, int?>(_Closure$__.$I._Lambda$__235-0)) : _Closure$__.$I235-0)).Where<int?>(((_Closure$__.$I235-1 == null) ? (_Closure$__.$I235-1 = new Func<int?, bool>(_Closure$__.$I._Lambda$__235-1)) : _Closure$__.$I235-1)).Select<int?, int>(((_Closure$__.$I235-2 == null) ? (_Closure$__.$I235-2 = new Func<int?, int>(_Closure$__.$I._Lambda$__235-2)) : _Closure$__.$I235-2)).ToArray<int>();
                    if (numArray.Length != 0)
                    {
                        bool flag = ReferenceEquals(sender, this.tsmiGridSearchArchive);
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand("", connection))
                            {
                                command.CommandText = !flag ? "UPDATE tbl_purchaseorder SET Archived = 0 WHERE IFNULL(Archived, 0) != 0 AND ID = :ID" : "UPDATE tbl_purchaseorder SET Archived = 1 WHERE IFNULL(Archived, 0) != 1 AND ID = :ID";
                                MySqlParameter parameter = command.Parameters.Add("ID", MySqlType.Int);
                                foreach (int num2 in numArray)
                                {
                                    parameter.Value = num2;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                this.InvalidateObjectList();
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
            if (!Versioned.IsNumeric(this.cmbVendor.SelectedValue))
            {
                base.ValidationErrors.SetError(this.cmbVendor, "You Must Select Vendor");
            }
            base.ValidationErrors.SetError(this.txtShipToPhone, Functions.PhoneValidate(this.txtShipToPhone.Text));
        }

        [field: AccessedThroughProperty("gbVendor")]
        private GroupBox gbVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbVendor")]
        private Combobox cmbVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCost")]
        private Label lblCost { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCost")]
        private NumericBox nmbCost { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbFreight")]
        private NumericBox nmbFreight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFreight")]
        private Label lblFreight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbTax")]
        private NumericBox nmbTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTax")]
        private Label lblTax { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbTotalDue")]
        private NumericBox nmbTotalDue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTotalDue")]
        private Label lblTotalDue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtVendorAccountNumber")]
        private TextBox txtVendorAccountNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblVendorAccountNumber")]
        private Label lblVendorAccountNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblVendor")]
        private Label lblVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbApproved")]
        private CheckBox chbApproved { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("PurchaseOrderItems")]
        private ControlPurchaseOrderItems PurchaseOrderItems { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblOrderDate")]
        private Label lblOrderDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("dtbOrderDate")]
        private UltraDateTimeEditor dtbOrderDate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpDetails")]
        private TabPage tpDetails { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpShipTo")]
        private TabPage tpShipTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpCompany")]
        private TabPage tpCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipToName")]
        private TextBox txtShipToName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipToName")]
        private Label lblShipToName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtShipToPhone")]
        private TextBox txtShipToPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblShipToPhone")]
        private Label lblShipToPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddressShipTo")]
        private ControlAddress CAddressShipTo { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCompanyName")]
        private Label lblCompanyName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("CAddressCompany")]
        private ControlAddress CAddressCompany { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblApproved")]
        private Label lblApproved { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReoccuring")]
        private Label lblReoccuring { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbReoccuring")]
        private CheckBox chbReoccuring { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblConfirmationNumber")]
        private Label lblConfirmationNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtConfirmationNumber")]
        private TextBox txtConfirmationNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNumber")]
        private Label lblNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNumber")]
        private TextBox txtNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPurchaseOrderID")]
        private Label lblPurchaseOrderID { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblLocation")]
        private Label lblLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbLocation")]
        private Combobox cmbLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtCompanyName")]
        private TextBox txtCompanyName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterNotArchived")]
        private MenuItem mnuFilterNotArchived { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterAll")]
        private MenuItem mnuFilterAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuFilterArchived")]
        private MenuItem mnuFilterArchived { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [field: AccessedThroughProperty("mnuActionReceive")]
        private MenuItem mnuActionReceive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        protected YesNoAny ArchivedFilter =>
            !this.mnuFilterArchived.Checked ? (!this.mnuFilterNotArchived.Checked ? YesNoAny.Any : YesNoAny.No) : YesNoAny.Yes;

        private bool ApprovedState
        {
            get => 
                !this.chbApproved.Enabled;
            set
            {
                bool flag = Permissions.FormPurchaseOrder_Approved.Allow_ADD_EDIT;
                this.chbApproved.Enabled = !value | flag;
                this.cmbVendor.Enabled = !value;
                this.cmbLocation.Enabled = !value;
                if (value)
                {
                    this.PurchaseOrderItems.AllowState = AllowStateEnum.AllowEdit00;
                }
                else
                {
                    this.PurchaseOrderItems.AllowState = AllowStateEnum.AllowAll;
                }
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
                    this.lblPurchaseOrderID.Text = "PO # " + Conversions.ToString(Conversions.ToLong(value));
                }
                else
                {
                    this.lblPurchaseOrderID.Text = "PO #";
                }
            }
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormPurchaseOrder._Closure$__ $I = new FormPurchaseOrder._Closure$__();
            public static Func<DataRow, int?> $I235-0;
            public static Func<int?, bool> $I235-1;
            public static Func<int?, int> $I235-2;

            internal int? _Lambda$__235-0(DataRow r) => 
                NullableConvert.ToInt32(r["ID"]);

            internal bool _Lambda$__235-1(int? v) => 
                v != null;

            internal int _Lambda$__235-2(int? v) => 
                v.Value;
        }

        [CompilerGenerated]
        internal sealed class _Closure$__198-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

