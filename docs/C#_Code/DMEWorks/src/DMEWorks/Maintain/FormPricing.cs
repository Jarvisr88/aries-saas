namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Billing;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonClone=true)]
    public class FormPricing : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormPricing()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_pricecode_item", "tbl_inventoryitem", "tbl_pricecode" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.chbAcceptAssignment);
            base.ChangesTracker.Subscribe(this.chbBillInsurance);
            base.ChangesTracker.Subscribe(this.chbFlatRate);
            base.ChangesTracker.Subscribe(this.chbReoccuringSale);
            base.ChangesTracker.Subscribe(this.chbShowSpanDates);
            base.ChangesTracker.Subscribe(this.chbTaxable);
            base.ChangesTracker.Subscribe(this.cmbAuthorizationType);
            base.ChangesTracker.Subscribe(this.cmbBilledWhen);
            base.ChangesTracker.Subscribe(this.cmbDefaultOrderType);
            base.ChangesTracker.Subscribe(this.cmbInventoryItem);
            base.ChangesTracker.Subscribe(this.cmbOrderedWhen);
            base.ChangesTracker.Subscribe(this.cmbPredefinedText);
            base.ChangesTracker.Subscribe(this.cmbPriceCode);
            base.ChangesTracker.Subscribe(this.cmbRentalType);
            base.ChangesTracker.Subscribe(this.nmbBilledConverter);
            base.ChangesTracker.Subscribe(this.nmbDeliveryConverter);
            base.ChangesTracker.Subscribe(this.nmbOrderedConverter);
            base.ChangesTracker.Subscribe(this.nmbOrderedQuantity);
            base.ChangesTracker.Subscribe(this.nmbRent_AllowablePrice);
            base.ChangesTracker.Subscribe(this.nmbRent_BillablePrice);
            base.ChangesTracker.Subscribe(this.nmbSale_AllowablePrice);
            base.ChangesTracker.Subscribe(this.nmbSale_BillablePrice);
            base.ChangesTracker.Subscribe(this.rbDayOfDelivery);
            base.ChangesTracker.Subscribe(this.rbLastDayOfThePeriod);
            base.ChangesTracker.Subscribe(this.txtBilledUnits);
            base.ChangesTracker.Subscribe(this.txtBillingCode);
            base.ChangesTracker.Subscribe(this.txtDeliveryUnits);
            base.ChangesTracker.Subscribe(this.txtDrugControlNumber);
            base.ChangesTracker.Subscribe(this.txtDrugNoteField);
            base.ChangesTracker.Subscribe(this.txtModifier1);
            base.ChangesTracker.Subscribe(this.txtModifier2);
            base.ChangesTracker.Subscribe(this.txtModifier3);
            base.ChangesTracker.Subscribe(this.txtModifier4);
            base.ChangesTracker.Subscribe(this.txtOrderedUnits);
            base.ChangesTracker.Subscribe(this.txtUserField1);
            base.ChangesTracker.Subscribe(this.txtUserField2);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsNew)
                {
                    if (NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue) == null)
                    {
                        throw new UserNotifyException("Please select inventory item");
                    }
                    if (NullableConvert.ToInt32(this.cmbPriceCode.SelectedValue) == null)
                    {
                        throw new UserNotifyException("Please select price code");
                    }
                    int? key = this.FindID(Conversions.ToInteger(this.cmbInventoryItem.SelectedValue), Conversions.ToInteger(this.cmbPriceCode.SelectedValue));
                    if (key == null)
                    {
                        throw new UserNotifyException("Selected pair (Price code, Inventory Item) do not have pricing record");
                    }
                    base.OpenObject(key);
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

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetComboBoxValue(this.cmbInventoryItem, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPriceCode, DBNull.Value);
            Functions.SetComboBoxText(this.cmbDefaultOrderType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPredefinedText, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbSale_AllowablePrice, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbSale_BillablePrice, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbReoccuringSale, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbRent_AllowablePrice, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbRent_BillablePrice, DBNull.Value);
            Functions.SetComboBoxText(this.cmbRentalType, DBNull.Value);
            Functions.SetTextBoxText(this.txtBillingCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier1, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier2, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier3, DBNull.Value);
            Functions.SetTextBoxText(this.txtModifier4, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbDefaultCMNType, string.Empty);
            Functions.SetComboBoxValue(this.cmbAuthorizationType, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbAcceptAssignment, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbShowSpanDates, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbFlatRate, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbTaxable, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbBillInsurance, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbOrderedQuantity, 1);
            Functions.SetTextBoxText(this.txtOrderedUnits, "EACH");
            Functions.SetComboBoxText(this.cmbOrderedWhen, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbOrderedConverter, 1);
            Functions.SetTextBoxText(this.txtBilledUnits, "EACH");
            Functions.SetComboBoxText(this.cmbBilledWhen, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbBilledConverter, 1);
            Functions.SetTextBoxText(this.txtDeliveryUnits, "EACH");
            Functions.SetNumericBoxValue(this.nmbDeliveryConverter, 1);
            this.BillItemOn = string.Empty;
            Functions.SetTextBoxText(this.txtDrugNoteField, DBNull.Value);
            Functions.SetTextBoxText(this.txtDrugControlNumber, DBNull.Value);
            Functions.SetTextBoxText(this.txtUserField1, DBNull.Value);
            Functions.SetTextBoxText(this.txtUserField2, DBNull.Value);
            this.LoadWarehouseList();
        }

        protected override void CloneObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetComboBoxValue(this.cmbPriceCode, DBNull.Value);
            this.LoadWarehouseList();
        }

        private void cmbDefaultCMNType_DrawItem(object sender, DrawItemEventArgs e)
        {
            object item = null;
            if (0 <= e.Index)
            {
                item = this.cmbDefaultCMNType.Items[e.Index];
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
                e.Graphics.DrawString(this.cmbDefaultCMNType.GetItemText(item), e.Font, brush2, e.Bounds);
            }
            if (((e.State & DrawItemState.Focus) == DrawItemState.Focus) && ((e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect))
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, e.ForeColor, e.BackColor);
            }
        }

        private void cmbInventoryItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadWarehouseList();
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_pricecode_item"))
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

        public int? FindID(int InventoryItemID, int PriceCodeID)
        {
            int? nullable;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT ID
FROM tbl_pricecode_item
WHERE (InventoryItemID = {InventoryItemID})
  AND (PriceCodeID = {PriceCodeID})";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        nullable = !reader.Read() ? null : new int?(Convert.ToInt32(reader["ID"]));
                    }
                }
            }
            return nullable;
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete pricing '{this.cmbInventoryItem.Text} - {this.cmbPriceCode.Text}'?";
            messages.DeletedSuccessfully = $"Pricing '{this.cmbInventoryItem.Text} - {this.cmbPriceCode.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_pricecode_item", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbDefaultOrderType, table, "DefaultOrderType");
                this.Load_Combobox_DefaultCMNType(Functions.ParseMysqlEnum(table, "DefaultCMNType"));
                Functions.LoadComboBoxItems(this.cmbRentalType, table, "RentalType");
                Functions.LoadComboBoxItems(this.cmbBilledWhen, table, "BilledWhen");
                Functions.LoadComboBoxItems(this.cmbOrderedWhen, table, "OrderedWhen");
            }
            Cache.InitDropdown(this.cmbAuthorizationType, "tbl_authorizationtype", null);
            Cache.InitDropdown(this.cmbInventoryItem, "tbl_inventoryitem", null);
            Cache.InitDropdown(this.cmbPriceCode, "tbl_pricecode", null);
            Cache.InitDropdown(this.cmbPredefinedText, "tbl_predefinedtext", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lnkDayOfPickup = new LinkLabel();
            this.rbLastDayOfThePeriod = new RadioButton();
            this.lnkLastDayOfTheMonth = new LinkLabel();
            this.rbDayOfDelivery = new RadioButton();
            this.lstWarehouses = new ListBox();
            this.chbReoccuringSale = new CheckBox();
            this.chbShowSpanDates = new CheckBox();
            this.chbAcceptAssignment = new CheckBox();
            this.chbTaxable = new CheckBox();
            this.chbFlatRate = new CheckBox();
            this.cmbRentalType = new ComboBox();
            this.cmbDefaultOrderType = new ComboBox();
            this.cmbAuthorizationType = new ComboBox();
            this.txtModifier1 = new TextBox();
            this.txtModifier4 = new TextBox();
            this.txtModifier3 = new TextBox();
            this.txtModifier2 = new TextBox();
            this.txtBillingCode = new TextBox();
            this.lblItem = new Label();
            this.lblValidWare = new Label();
            this.lblRent_AllowablePrice = new Label();
            this.lblRent_BillablePrice = new Label();
            this.lblSale_AllowablePrice = new Label();
            this.lblSale_BillablePrice = new Label();
            this.lblRentalType = new Label();
            this.lblDefaultOrderType = new Label();
            this.lblDefaultAuthorizationType = new Label();
            this.lblDefaultCMNRX = new Label();
            this.lblMod = new Label();
            this.lblBillCode = new Label();
            this.lblPriceCode = new Label();
            this.cmbInventoryItem = new Combobox();
            this.cmbPriceCode = new Combobox();
            this.gbBillItemOn = new GroupBox();
            this.gbFrequencyDefaults = new GroupBox();
            this.nmbDeliveryConverter = new NumericBox();
            this.lblDelivery = new Label();
            this.txtDeliveryUnits = new TextBox();
            this.nmbOrderedConverter = new NumericBox();
            this.nmbOrderedQuantity = new NumericBox();
            this.nmbBilledConverter = new NumericBox();
            this.lblConverter = new Label();
            this.lblUnits = new Label();
            this.lblWhen = new Label();
            this.lblQty = new Label();
            this.lblBilled = new Label();
            this.txtOrderedUnits = new TextBox();
            this.txtBilledUnits = new TextBox();
            this.Label3 = new Label();
            this.cmbOrderedWhen = new ComboBox();
            this.cmbBilledWhen = new ComboBox();
            this.gbBillingDefaults = new GroupBox();
            this.chbBillInsurance = new CheckBox();
            this.cmbDefaultCMNType = new ComboBox();
            this.gbRentalInformation = new GroupBox();
            this.nmbRent_AllowablePrice = new NumericBox();
            this.nmbRent_BillablePrice = new NumericBox();
            this.gbSaleInformation = new GroupBox();
            this.nmbSale_AllowablePrice = new NumericBox();
            this.nmbSale_BillablePrice = new NumericBox();
            this.cmbPredefinedText = new Combobox();
            this.lblPredefinedText = new Label();
            this.btnFind = new Button();
            this.gbDrugIdentification = new GroupBox();
            this.txtDrugControlNumber = new TextBox();
            this.txtDrugNoteField = new TextBox();
            this.lblDrugControlNumber = new Label();
            this.lblDrugNoteField = new Label();
            this.gbUserFields = new GroupBox();
            this.txtUserField2 = new TextBox();
            this.txtUserField1 = new TextBox();
            this.lblUserField2 = new Label();
            this.lblUserField1 = new Label();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            this.gbBillItemOn.SuspendLayout();
            this.gbFrequencyDefaults.SuspendLayout();
            this.gbBillingDefaults.SuspendLayout();
            this.gbRentalInformation.SuspendLayout();
            this.gbSaleInformation.SuspendLayout();
            this.gbDrugIdentification.SuspendLayout();
            this.gbUserFields.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.gbUserFields);
            base.tpWorkArea.Controls.Add(this.gbDrugIdentification);
            base.tpWorkArea.Controls.Add(this.btnFind);
            base.tpWorkArea.Controls.Add(this.cmbPredefinedText);
            base.tpWorkArea.Controls.Add(this.lblPredefinedText);
            base.tpWorkArea.Controls.Add(this.gbSaleInformation);
            base.tpWorkArea.Controls.Add(this.gbBillItemOn);
            base.tpWorkArea.Controls.Add(this.gbRentalInformation);
            base.tpWorkArea.Controls.Add(this.gbBillingDefaults);
            base.tpWorkArea.Controls.Add(this.gbFrequencyDefaults);
            base.tpWorkArea.Controls.Add(this.cmbPriceCode);
            base.tpWorkArea.Controls.Add(this.cmbInventoryItem);
            base.tpWorkArea.Controls.Add(this.lstWarehouses);
            base.tpWorkArea.Controls.Add(this.cmbDefaultOrderType);
            base.tpWorkArea.Controls.Add(this.lblItem);
            base.tpWorkArea.Controls.Add(this.lblValidWare);
            base.tpWorkArea.Controls.Add(this.lblDefaultOrderType);
            base.tpWorkArea.Controls.Add(this.lblPriceCode);
            base.tpWorkArea.Size = new Size(0x240, 530);
            this.lnkDayOfPickup.Location = new Point(0x18, 0x40);
            this.lnkDayOfPickup.Name = "lnkDayOfPickup";
            this.lnkDayOfPickup.Size = new Size(120, 0x15);
            this.lnkDayOfPickup.TabIndex = 2;
            this.lnkDayOfPickup.Text = "Bill at Pick-Up";
            this.lnkDayOfPickup.TextAlign = ContentAlignment.MiddleLeft;
            this.rbLastDayOfThePeriod.Location = new Point(8, 40);
            this.rbLastDayOfThePeriod.Name = "rbLastDayOfThePeriod";
            this.rbLastDayOfThePeriod.Size = new Size(0x88, 0x15);
            this.rbLastDayOfThePeriod.TabIndex = 1;
            this.rbLastDayOfThePeriod.TabStop = true;
            this.rbLastDayOfThePeriod.Text = "Last day of the Period";
            this.lnkLastDayOfTheMonth.Location = new Point(0x18, 0x58);
            this.lnkLastDayOfTheMonth.Name = "lnkLastDayOfTheMonth";
            this.lnkLastDayOfTheMonth.Size = new Size(120, 0x15);
            this.lnkLastDayOfTheMonth.TabIndex = 3;
            this.lnkLastDayOfTheMonth.Text = "Last day of the Month";
            this.lnkLastDayOfTheMonth.TextAlign = ContentAlignment.MiddleLeft;
            this.rbDayOfDelivery.Location = new Point(8, 0x10);
            this.rbDayOfDelivery.Name = "rbDayOfDelivery";
            this.rbDayOfDelivery.Size = new Size(0x88, 0x15);
            this.rbDayOfDelivery.TabIndex = 0;
            this.rbDayOfDelivery.TabStop = true;
            this.rbDayOfDelivery.Text = "Day of Delivery";
            this.lstWarehouses.Location = new Point(0x198, 0x68);
            this.lstWarehouses.Name = "lstWarehouses";
            this.lstWarehouses.RightToLeft = RightToLeft.No;
            this.lstWarehouses.Size = new Size(160, 0xc7);
            this.lstWarehouses.TabIndex = 13;
            this.chbReoccuringSale.CheckAlign = ContentAlignment.MiddleRight;
            this.chbReoccuringSale.Location = new Point(8, 0x40);
            this.chbReoccuringSale.Name = "chbReoccuringSale";
            this.chbReoccuringSale.Size = new Size(0x70, 0x15);
            this.chbReoccuringSale.TabIndex = 4;
            this.chbReoccuringSale.Text = "Reoccuring Sale";
            this.chbReoccuringSale.TextAlign = ContentAlignment.MiddleRight;
            this.chbReoccuringSale.UseVisualStyleBackColor = false;
            this.chbShowSpanDates.CheckAlign = ContentAlignment.MiddleRight;
            this.chbShowSpanDates.Location = new Point(0x108, 40);
            this.chbShowSpanDates.Name = "chbShowSpanDates";
            this.chbShowSpanDates.Size = new Size(120, 0x15);
            this.chbShowSpanDates.TabIndex = 12;
            this.chbShowSpanDates.Text = "Show Span Dates";
            this.chbShowSpanDates.TextAlign = ContentAlignment.MiddleRight;
            this.chbAcceptAssignment.CheckAlign = ContentAlignment.MiddleRight;
            this.chbAcceptAssignment.Location = new Point(0x108, 0x10);
            this.chbAcceptAssignment.Name = "chbAcceptAssignment";
            this.chbAcceptAssignment.Size = new Size(120, 0x15);
            this.chbAcceptAssignment.TabIndex = 11;
            this.chbAcceptAssignment.Text = "Accept Assignment";
            this.chbAcceptAssignment.TextAlign = ContentAlignment.MiddleRight;
            this.chbTaxable.CheckAlign = ContentAlignment.MiddleRight;
            this.chbTaxable.Location = new Point(0x108, 0x70);
            this.chbTaxable.Name = "chbTaxable";
            this.chbTaxable.Size = new Size(120, 0x15);
            this.chbTaxable.TabIndex = 15;
            this.chbTaxable.Text = "Taxable";
            this.chbTaxable.TextAlign = ContentAlignment.MiddleRight;
            this.chbFlatRate.CheckAlign = ContentAlignment.MiddleRight;
            this.chbFlatRate.Location = new Point(0x108, 0x40);
            this.chbFlatRate.Name = "chbFlatRate";
            this.chbFlatRate.Size = new Size(120, 0x15);
            this.chbFlatRate.TabIndex = 13;
            this.chbFlatRate.Text = "Flat Rate";
            this.chbFlatRate.TextAlign = ContentAlignment.MiddleRight;
            this.cmbRentalType.Location = new Point(0x68, 0x40);
            this.cmbRentalType.Name = "cmbRentalType";
            this.cmbRentalType.RightToLeft = RightToLeft.No;
            this.cmbRentalType.Size = new Size(80, 0x15);
            this.cmbRentalType.TabIndex = 5;
            this.cmbDefaultOrderType.Location = new Point(0x80, 0x38);
            this.cmbDefaultOrderType.Name = "cmbDefaultOrderType";
            this.cmbDefaultOrderType.Size = new Size(0x71, 0x15);
            this.cmbDefaultOrderType.TabIndex = 6;
            this.cmbAuthorizationType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAuthorizationType.Location = new Point(0x70, 0x58);
            this.cmbAuthorizationType.Name = "cmbAuthorizationType";
            this.cmbAuthorizationType.Size = new Size(0x7c, 0x15);
            this.cmbAuthorizationType.TabIndex = 10;
            this.txtModifier1.Location = new Point(0x70, 40);
            this.txtModifier1.MaxLength = 0;
            this.txtModifier1.Name = "txtModifier1";
            this.txtModifier1.RightToLeft = RightToLeft.No;
            this.txtModifier1.Size = new Size(0x1c, 20);
            this.txtModifier1.TabIndex = 3;
            this.txtModifier4.Location = new Point(0xd0, 40);
            this.txtModifier4.MaxLength = 0;
            this.txtModifier4.Name = "txtModifier4";
            this.txtModifier4.RightToLeft = RightToLeft.No;
            this.txtModifier4.Size = new Size(0x1c, 20);
            this.txtModifier4.TabIndex = 6;
            this.txtModifier3.Location = new Point(0xb0, 40);
            this.txtModifier3.MaxLength = 0;
            this.txtModifier3.Name = "txtModifier3";
            this.txtModifier3.RightToLeft = RightToLeft.No;
            this.txtModifier3.Size = new Size(0x1c, 20);
            this.txtModifier3.TabIndex = 5;
            this.txtModifier2.Location = new Point(0x90, 40);
            this.txtModifier2.MaxLength = 0;
            this.txtModifier2.Name = "txtModifier2";
            this.txtModifier2.RightToLeft = RightToLeft.No;
            this.txtModifier2.Size = new Size(0x1c, 20);
            this.txtModifier2.TabIndex = 4;
            this.txtBillingCode.Location = new Point(0x70, 0x10);
            this.txtBillingCode.MaxLength = 0;
            this.txtBillingCode.Name = "txtBillingCode";
            this.txtBillingCode.RightToLeft = RightToLeft.No;
            this.txtBillingCode.Size = new Size(0x7c, 20);
            this.txtBillingCode.TabIndex = 1;
            this.lblItem.Location = new Point(0x10, 8);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new Size(0x68, 0x15);
            this.lblItem.TabIndex = 0;
            this.lblItem.Text = "Item Description";
            this.lblItem.TextAlign = ContentAlignment.MiddleRight;
            this.lblValidWare.Location = new Point(0x198, 80);
            this.lblValidWare.Name = "lblValidWare";
            this.lblValidWare.RightToLeft = RightToLeft.No;
            this.lblValidWare.Size = new Size(160, 0x18);
            this.lblValidWare.TabIndex = 12;
            this.lblValidWare.Text = "Valid Warehouses";
            this.lblValidWare.TextAlign = ContentAlignment.MiddleCenter;
            this.lblRent_AllowablePrice.Location = new Point(8, 40);
            this.lblRent_AllowablePrice.Name = "lblRent_AllowablePrice";
            this.lblRent_AllowablePrice.Size = new Size(0x58, 0x15);
            this.lblRent_AllowablePrice.TabIndex = 2;
            this.lblRent_AllowablePrice.Text = "Allowable Price";
            this.lblRent_AllowablePrice.TextAlign = ContentAlignment.MiddleRight;
            this.lblRent_BillablePrice.Location = new Point(8, 0x10);
            this.lblRent_BillablePrice.Name = "lblRent_BillablePrice";
            this.lblRent_BillablePrice.Size = new Size(0x58, 0x15);
            this.lblRent_BillablePrice.TabIndex = 0;
            this.lblRent_BillablePrice.Text = "Billable Price";
            this.lblRent_BillablePrice.TextAlign = ContentAlignment.MiddleRight;
            this.lblSale_AllowablePrice.Location = new Point(8, 40);
            this.lblSale_AllowablePrice.Name = "lblSale_AllowablePrice";
            this.lblSale_AllowablePrice.Size = new Size(0x58, 0x15);
            this.lblSale_AllowablePrice.TabIndex = 2;
            this.lblSale_AllowablePrice.Text = "Allowable Price";
            this.lblSale_AllowablePrice.TextAlign = ContentAlignment.MiddleRight;
            this.lblSale_BillablePrice.Location = new Point(8, 0x10);
            this.lblSale_BillablePrice.Name = "lblSale_BillablePrice";
            this.lblSale_BillablePrice.Size = new Size(0x58, 0x15);
            this.lblSale_BillablePrice.TabIndex = 0;
            this.lblSale_BillablePrice.Text = "Billable Price";
            this.lblSale_BillablePrice.TextAlign = ContentAlignment.MiddleRight;
            this.lblRentalType.Location = new Point(8, 0x40);
            this.lblRentalType.Name = "lblRentalType";
            this.lblRentalType.Size = new Size(0x58, 0x15);
            this.lblRentalType.TabIndex = 4;
            this.lblRentalType.Text = "Rental Type";
            this.lblRentalType.TextAlign = ContentAlignment.MiddleRight;
            this.lblDefaultOrderType.Location = new Point(0x10, 0x38);
            this.lblDefaultOrderType.Name = "lblDefaultOrderType";
            this.lblDefaultOrderType.Size = new Size(0x68, 0x15);
            this.lblDefaultOrderType.TabIndex = 5;
            this.lblDefaultOrderType.Text = "Default Order Type";
            this.lblDefaultOrderType.TextAlign = ContentAlignment.MiddleRight;
            this.lblDefaultAuthorizationType.Location = new Point(8, 0x58);
            this.lblDefaultAuthorizationType.Name = "lblDefaultAuthorizationType";
            this.lblDefaultAuthorizationType.Size = new Size(0x60, 0x15);
            this.lblDefaultAuthorizationType.TabIndex = 9;
            this.lblDefaultAuthorizationType.Text = "Default Prior Auth";
            this.lblDefaultAuthorizationType.TextAlign = ContentAlignment.MiddleRight;
            this.lblDefaultCMNRX.Location = new Point(8, 0x40);
            this.lblDefaultCMNRX.Name = "lblDefaultCMNRX";
            this.lblDefaultCMNRX.Size = new Size(0x60, 0x15);
            this.lblDefaultCMNRX.TabIndex = 7;
            this.lblDefaultCMNRX.Text = "Default CMN/RX";
            this.lblDefaultCMNRX.TextAlign = ContentAlignment.MiddleRight;
            this.lblMod.Location = new Point(8, 40);
            this.lblMod.Name = "lblMod";
            this.lblMod.Size = new Size(0x60, 0x15);
            this.lblMod.TabIndex = 2;
            this.lblMod.Text = "Modifiers";
            this.lblMod.TextAlign = ContentAlignment.MiddleRight;
            this.lblBillCode.Location = new Point(8, 0x10);
            this.lblBillCode.Name = "lblBillCode";
            this.lblBillCode.Size = new Size(0x60, 0x15);
            this.lblBillCode.TabIndex = 0;
            this.lblBillCode.Text = "Billing Code";
            this.lblBillCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblPriceCode.Location = new Point(0x10, 0x20);
            this.lblPriceCode.Name = "lblPriceCode";
            this.lblPriceCode.Size = new Size(0x68, 0x15);
            this.lblPriceCode.TabIndex = 2;
            this.lblPriceCode.Text = "Price Code";
            this.lblPriceCode.TextAlign = ContentAlignment.MiddleRight;
            this.cmbInventoryItem.Location = new Point(0x80, 8);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.Size = new Size(0x120, 0x15);
            this.cmbInventoryItem.TabIndex = 1;
            this.cmbPriceCode.Location = new Point(0x80, 0x20);
            this.cmbPriceCode.Name = "cmbPriceCode";
            this.cmbPriceCode.Size = new Size(0x120, 0x15);
            this.cmbPriceCode.TabIndex = 3;
            this.gbBillItemOn.Controls.Add(this.lnkDayOfPickup);
            this.gbBillItemOn.Controls.Add(this.rbLastDayOfThePeriod);
            this.gbBillItemOn.Controls.Add(this.lnkLastDayOfTheMonth);
            this.gbBillItemOn.Controls.Add(this.rbDayOfDelivery);
            this.gbBillItemOn.Location = new Point(0x19b, 0x138);
            this.gbBillItemOn.Name = "gbBillItemOn";
            this.gbBillItemOn.Size = new Size(0x9d, 0x74);
            this.gbBillItemOn.TabIndex = 15;
            this.gbBillItemOn.TabStop = false;
            this.gbBillItemOn.Text = "Bill Item On:";
            this.gbFrequencyDefaults.Controls.Add(this.nmbDeliveryConverter);
            this.gbFrequencyDefaults.Controls.Add(this.lblDelivery);
            this.gbFrequencyDefaults.Controls.Add(this.txtDeliveryUnits);
            this.gbFrequencyDefaults.Controls.Add(this.nmbOrderedConverter);
            this.gbFrequencyDefaults.Controls.Add(this.nmbOrderedQuantity);
            this.gbFrequencyDefaults.Controls.Add(this.nmbBilledConverter);
            this.gbFrequencyDefaults.Controls.Add(this.lblConverter);
            this.gbFrequencyDefaults.Controls.Add(this.lblUnits);
            this.gbFrequencyDefaults.Controls.Add(this.lblWhen);
            this.gbFrequencyDefaults.Controls.Add(this.lblQty);
            this.gbFrequencyDefaults.Controls.Add(this.lblBilled);
            this.gbFrequencyDefaults.Controls.Add(this.txtOrderedUnits);
            this.gbFrequencyDefaults.Controls.Add(this.txtBilledUnits);
            this.gbFrequencyDefaults.Controls.Add(this.Label3);
            this.gbFrequencyDefaults.Controls.Add(this.cmbOrderedWhen);
            this.gbFrequencyDefaults.Controls.Add(this.cmbBilledWhen);
            this.gbFrequencyDefaults.Location = new Point(8, 0x138);
            this.gbFrequencyDefaults.Name = "gbFrequencyDefaults";
            this.gbFrequencyDefaults.Size = new Size(0x188, 0x74);
            this.gbFrequencyDefaults.TabIndex = 14;
            this.gbFrequencyDefaults.TabStop = false;
            this.gbFrequencyDefaults.Text = "Frequency Defaults";
            this.nmbDeliveryConverter.Location = new Point(0x130, 0x58);
            this.nmbDeliveryConverter.Name = "nmbDeliveryConverter";
            this.nmbDeliveryConverter.Size = new Size(0x48, 0x15);
            this.nmbDeliveryConverter.TabIndex = 15;
            this.lblDelivery.Location = new Point(8, 0x58);
            this.lblDelivery.Name = "lblDelivery";
            this.lblDelivery.Size = new Size(0x30, 0x15);
            this.lblDelivery.TabIndex = 13;
            this.lblDelivery.Text = "Delivery";
            this.lblDelivery.TextAlign = ContentAlignment.MiddleRight;
            this.txtDeliveryUnits.Location = new Point(0x90, 0x58);
            this.txtDeliveryUnits.MaxLength = 0;
            this.txtDeliveryUnits.Name = "txtDeliveryUnits";
            this.txtDeliveryUnits.Size = new Size(0x48, 20);
            this.txtDeliveryUnits.TabIndex = 14;
            this.nmbOrderedConverter.Location = new Point(0x130, 40);
            this.nmbOrderedConverter.Name = "nmbOrderedConverter";
            this.nmbOrderedConverter.Size = new Size(0x48, 0x15);
            this.nmbOrderedConverter.TabIndex = 8;
            this.nmbOrderedQuantity.Location = new Point(0x40, 40);
            this.nmbOrderedQuantity.Name = "nmbOrderedQuantity";
            this.nmbOrderedQuantity.Size = new Size(0x48, 0x15);
            this.nmbOrderedQuantity.TabIndex = 5;
            this.nmbBilledConverter.Location = new Point(0x130, 0x40);
            this.nmbBilledConverter.Name = "nmbBilledConverter";
            this.nmbBilledConverter.Size = new Size(0x48, 0x15);
            this.nmbBilledConverter.TabIndex = 12;
            this.lblConverter.Location = new Point(0x130, 0x10);
            this.lblConverter.Name = "lblConverter";
            this.lblConverter.Size = new Size(0x48, 0x15);
            this.lblConverter.TabIndex = 3;
            this.lblConverter.Text = "Converter";
            this.lblConverter.TextAlign = ContentAlignment.MiddleCenter;
            this.lblUnits.Location = new Point(0x90, 0x10);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new Size(0x48, 0x15);
            this.lblUnits.TabIndex = 1;
            this.lblUnits.Text = "Units";
            this.lblUnits.TextAlign = ContentAlignment.MiddleCenter;
            this.lblWhen.Location = new Point(0xe0, 0x10);
            this.lblWhen.Name = "lblWhen";
            this.lblWhen.Size = new Size(0x48, 0x15);
            this.lblWhen.TabIndex = 2;
            this.lblWhen.Text = "When";
            this.lblWhen.TextAlign = ContentAlignment.MiddleCenter;
            this.lblQty.Location = new Point(0x40, 0x10);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new Size(0x48, 0x15);
            this.lblQty.TabIndex = 0;
            this.lblQty.Text = "Quantity";
            this.lblQty.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBilled.Location = new Point(8, 0x40);
            this.lblBilled.Name = "lblBilled";
            this.lblBilled.Size = new Size(0x30, 0x15);
            this.lblBilled.TabIndex = 9;
            this.lblBilled.Text = "Billed";
            this.lblBilled.TextAlign = ContentAlignment.MiddleRight;
            this.txtOrderedUnits.Location = new Point(0x90, 40);
            this.txtOrderedUnits.MaxLength = 0;
            this.txtOrderedUnits.Name = "txtOrderedUnits";
            this.txtOrderedUnits.Size = new Size(0x48, 20);
            this.txtOrderedUnits.TabIndex = 6;
            this.txtBilledUnits.Location = new Point(0x90, 0x40);
            this.txtBilledUnits.MaxLength = 0;
            this.txtBilledUnits.Name = "txtBilledUnits";
            this.txtBilledUnits.Size = new Size(0x48, 20);
            this.txtBilledUnits.TabIndex = 10;
            this.Label3.Location = new Point(8, 40);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x30, 0x15);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Ordered";
            this.Label3.TextAlign = ContentAlignment.MiddleRight;
            this.cmbOrderedWhen.Location = new Point(0xe0, 40);
            this.cmbOrderedWhen.Name = "cmbOrderedWhen";
            this.cmbOrderedWhen.Size = new Size(0x48, 0x15);
            this.cmbOrderedWhen.TabIndex = 7;
            this.cmbBilledWhen.Location = new Point(0xe0, 0x40);
            this.cmbBilledWhen.Name = "cmbBilledWhen";
            this.cmbBilledWhen.Size = new Size(0x48, 0x15);
            this.cmbBilledWhen.TabIndex = 11;
            this.gbBillingDefaults.Controls.Add(this.chbBillInsurance);
            this.gbBillingDefaults.Controls.Add(this.cmbDefaultCMNType);
            this.gbBillingDefaults.Controls.Add(this.cmbAuthorizationType);
            this.gbBillingDefaults.Controls.Add(this.txtModifier1);
            this.gbBillingDefaults.Controls.Add(this.txtModifier4);
            this.gbBillingDefaults.Controls.Add(this.txtModifier3);
            this.gbBillingDefaults.Controls.Add(this.txtModifier2);
            this.gbBillingDefaults.Controls.Add(this.txtBillingCode);
            this.gbBillingDefaults.Controls.Add(this.lblDefaultAuthorizationType);
            this.gbBillingDefaults.Controls.Add(this.lblDefaultCMNRX);
            this.gbBillingDefaults.Controls.Add(this.lblMod);
            this.gbBillingDefaults.Controls.Add(this.lblBillCode);
            this.gbBillingDefaults.Controls.Add(this.chbFlatRate);
            this.gbBillingDefaults.Controls.Add(this.chbShowSpanDates);
            this.gbBillingDefaults.Controls.Add(this.chbAcceptAssignment);
            this.gbBillingDefaults.Controls.Add(this.chbTaxable);
            this.gbBillingDefaults.Location = new Point(8, 0xb0);
            this.gbBillingDefaults.Name = "gbBillingDefaults";
            this.gbBillingDefaults.Size = new Size(0x188, 0x88);
            this.gbBillingDefaults.TabIndex = 11;
            this.gbBillingDefaults.TabStop = false;
            this.gbBillingDefaults.Text = "Billing Defaults";
            this.chbBillInsurance.CheckAlign = ContentAlignment.MiddleRight;
            this.chbBillInsurance.Location = new Point(0x108, 0x58);
            this.chbBillInsurance.Name = "chbBillInsurance";
            this.chbBillInsurance.Size = new Size(120, 0x15);
            this.chbBillInsurance.TabIndex = 14;
            this.chbBillInsurance.Text = "Bill To Insurance";
            this.chbBillInsurance.TextAlign = ContentAlignment.MiddleRight;
            this.cmbDefaultCMNType.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmbDefaultCMNType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDefaultCMNType.DropDownWidth = 360;
            this.cmbDefaultCMNType.Location = new Point(0x70, 0x40);
            this.cmbDefaultCMNType.Name = "cmbDefaultCMNType";
            this.cmbDefaultCMNType.Size = new Size(0x7c, 0x15);
            this.cmbDefaultCMNType.TabIndex = 8;
            this.gbRentalInformation.Controls.Add(this.nmbRent_AllowablePrice);
            this.gbRentalInformation.Controls.Add(this.nmbRent_BillablePrice);
            this.gbRentalInformation.Controls.Add(this.cmbRentalType);
            this.gbRentalInformation.Controls.Add(this.lblRent_AllowablePrice);
            this.gbRentalInformation.Controls.Add(this.lblRent_BillablePrice);
            this.gbRentalInformation.Controls.Add(this.lblRentalType);
            this.gbRentalInformation.Location = new Point(0xd0, 80);
            this.gbRentalInformation.Name = "gbRentalInformation";
            this.gbRentalInformation.Size = new Size(0xc0, 0x5c);
            this.gbRentalInformation.TabIndex = 10;
            this.gbRentalInformation.TabStop = false;
            this.gbRentalInformation.Text = "Rental Information";
            this.nmbRent_AllowablePrice.Location = new Point(0x68, 40);
            this.nmbRent_AllowablePrice.Name = "nmbRent_AllowablePrice";
            this.nmbRent_AllowablePrice.Size = new Size(80, 0x15);
            this.nmbRent_AllowablePrice.TabIndex = 3;
            this.nmbRent_BillablePrice.Location = new Point(0x68, 0x10);
            this.nmbRent_BillablePrice.Name = "nmbRent_BillablePrice";
            this.nmbRent_BillablePrice.Size = new Size(80, 0x15);
            this.nmbRent_BillablePrice.TabIndex = 1;
            this.gbSaleInformation.Controls.Add(this.nmbSale_AllowablePrice);
            this.gbSaleInformation.Controls.Add(this.nmbSale_BillablePrice);
            this.gbSaleInformation.Controls.Add(this.chbReoccuringSale);
            this.gbSaleInformation.Controls.Add(this.lblSale_AllowablePrice);
            this.gbSaleInformation.Controls.Add(this.lblSale_BillablePrice);
            this.gbSaleInformation.Location = new Point(8, 80);
            this.gbSaleInformation.Name = "gbSaleInformation";
            this.gbSaleInformation.Size = new Size(0xc0, 0x5c);
            this.gbSaleInformation.TabIndex = 9;
            this.gbSaleInformation.TabStop = false;
            this.gbSaleInformation.Text = "Sale Information";
            this.nmbSale_AllowablePrice.Location = new Point(0x68, 40);
            this.nmbSale_AllowablePrice.Name = "nmbSale_AllowablePrice";
            this.nmbSale_AllowablePrice.Size = new Size(80, 0x15);
            this.nmbSale_AllowablePrice.TabIndex = 3;
            this.nmbSale_BillablePrice.Location = new Point(0x68, 0x10);
            this.nmbSale_BillablePrice.Name = "nmbSale_BillablePrice";
            this.nmbSale_BillablePrice.Size = new Size(80, 0x15);
            this.nmbSale_BillablePrice.TabIndex = 1;
            this.cmbPredefinedText.Location = new Point(0x158, 0x38);
            this.cmbPredefinedText.Name = "cmbPredefinedText";
            this.cmbPredefinedText.Size = new Size(0xe0, 0x15);
            this.cmbPredefinedText.TabIndex = 8;
            this.lblPredefinedText.Location = new Point(0xf8, 0x38);
            this.lblPredefinedText.Name = "lblPredefinedText";
            this.lblPredefinedText.RightToLeft = RightToLeft.No;
            this.lblPredefinedText.Size = new Size(0x58, 0x15);
            this.lblPredefinedText.TabIndex = 7;
            this.lblPredefinedText.Text = "Predefined Text";
            this.lblPredefinedText.TextAlign = ContentAlignment.MiddleRight;
            this.btnFind.FlatStyle = FlatStyle.Flat;
            this.btnFind.Location = new Point(0x1a8, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(0x37, 0x2d);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Find";
            this.gbDrugIdentification.Controls.Add(this.txtDrugControlNumber);
            this.gbDrugIdentification.Controls.Add(this.txtDrugNoteField);
            this.gbDrugIdentification.Controls.Add(this.lblDrugControlNumber);
            this.gbDrugIdentification.Controls.Add(this.lblDrugNoteField);
            this.gbDrugIdentification.Location = new Point(8, 0x1b0);
            this.gbDrugIdentification.Name = "gbDrugIdentification";
            this.gbDrugIdentification.Size = new Size(560, 0x30);
            this.gbDrugIdentification.TabIndex = 0x10;
            this.gbDrugIdentification.TabStop = false;
            this.gbDrugIdentification.Text = "Drug Identification";
            this.txtDrugControlNumber.Location = new Point(0x158, 0x10);
            this.txtDrugControlNumber.Name = "txtDrugControlNumber";
            this.txtDrugControlNumber.Size = new Size(0xd0, 20);
            this.txtDrugControlNumber.TabIndex = 3;
            this.txtDrugNoteField.Location = new Point(0x48, 0x10);
            this.txtDrugNoteField.Name = "txtDrugNoteField";
            this.txtDrugNoteField.Size = new Size(200, 20);
            this.txtDrugNoteField.TabIndex = 1;
            this.lblDrugControlNumber.Location = new Point(280, 0x10);
            this.lblDrugControlNumber.Name = "lblDrugControlNumber";
            this.lblDrugControlNumber.Size = new Size(0x38, 0x15);
            this.lblDrugControlNumber.TabIndex = 2;
            this.lblDrugControlNumber.Text = "Control #";
            this.lblDrugControlNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblDrugNoteField.Location = new Point(8, 0x10);
            this.lblDrugNoteField.Name = "lblDrugNoteField";
            this.lblDrugNoteField.Size = new Size(0x38, 0x15);
            this.lblDrugNoteField.TabIndex = 0;
            this.lblDrugNoteField.Text = "Note Field";
            this.lblDrugNoteField.TextAlign = ContentAlignment.MiddleRight;
            this.gbUserFields.Controls.Add(this.txtUserField2);
            this.gbUserFields.Controls.Add(this.txtUserField1);
            this.gbUserFields.Controls.Add(this.lblUserField2);
            this.gbUserFields.Controls.Add(this.lblUserField1);
            this.gbUserFields.Location = new Point(8, 480);
            this.gbUserFields.Name = "gbUserFields";
            this.gbUserFields.Size = new Size(560, 0x2c);
            this.gbUserFields.TabIndex = 0x11;
            this.gbUserFields.TabStop = false;
            this.gbUserFields.Text = "User Fields";
            this.txtUserField2.Location = new Point(0x158, 0x10);
            this.txtUserField2.MaxLength = 100;
            this.txtUserField2.Name = "txtUserField2";
            this.txtUserField2.Size = new Size(0xd0, 20);
            this.txtUserField2.TabIndex = 3;
            this.txtUserField1.Location = new Point(0x40, 0x10);
            this.txtUserField1.MaxLength = 100;
            this.txtUserField1.Name = "txtUserField1";
            this.txtUserField1.Size = new Size(0xd0, 20);
            this.txtUserField1.TabIndex = 1;
            this.lblUserField2.Location = new Point(0x120, 0x10);
            this.lblUserField2.Name = "lblUserField2";
            this.lblUserField2.Size = new Size(0x30, 0x15);
            this.lblUserField2.TabIndex = 2;
            this.lblUserField2.Text = "Field #2";
            this.lblUserField2.TextAlign = ContentAlignment.MiddleRight;
            this.lblUserField1.Location = new Point(8, 0x10);
            this.lblUserField1.Name = "lblUserField1";
            this.lblUserField1.Size = new Size(0x30, 0x15);
            this.lblUserField1.TabIndex = 0;
            this.lblUserField1.Text = "Field #1";
            this.lblUserField1.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x248, 0x25d);
            base.Name = "FormPricing";
            this.Text = "Form Pricing";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            this.gbBillItemOn.ResumeLayout(false);
            this.gbFrequencyDefaults.ResumeLayout(false);
            this.gbFrequencyDefaults.PerformLayout();
            this.gbBillingDefaults.ResumeLayout(false);
            this.gbBillingDefaults.PerformLayout();
            this.gbRentalInformation.ResumeLayout(false);
            this.gbSaleInformation.ResumeLayout(false);
            this.gbDrugIdentification.ResumeLayout(false);
            this.gbDrugIdentification.PerformLayout();
            this.gbUserFields.ResumeLayout(false);
            this.gbUserFields.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InternalLoadWarehouseList(object InventoryItemID)
        {
            this.lstWarehouses.BeginUpdate();
            try
            {
                this.lstWarehouses.Items.Clear();
                if (NullableConvert.ToInt32(InventoryItemID) != null)
                {
                    string commandText = $"SELECT DISTINCT tbl_warehouse.ID, tbl_warehouse.Name
FROM (tbl_inventory
      INNER JOIN tbl_warehouse ON tbl_warehouse.ID = tbl_inventory.WarehouseID)
WHERE (tbl_inventory.InventoryItemID = {Conversions.ToInteger(InventoryItemID)})
ORDER BY tbl_warehouse.Name";
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand(commandText, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    this.lstWarehouses.Items.Add(reader["Name"]);
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                this.lstWarehouses.EndUpdate();
            }
        }

        private void lnkDayOfPickup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.cmbRentalType.Text = "One Time Rental";
                this.rbLastDayOfThePeriod.Checked = true;
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

        private void lnkLastDayOfTheMonth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.cmbBilledWhen.Text = "Calendar Monthly";
                this.rbLastDayOfThePeriod.Checked = true;
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

        protected void Load_Combobox_DefaultCMNType(IEnumerable<string> types)
        {
            List<CmnDescription> list = new List<CmnDescription> {
                new CmnDescription((DmercType) 0)
            };
            using (IEnumerator<string> enumerator = types.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    DmercType type = DmercHelper.String2Dmerc(enumerator.Current);
                    if (type != ((DmercType) 0))
                    {
                        list.Add(new CmnDescription(type));
                    }
                }
            }
            list.Sort(new Comparison<CmnDescription>(CmnDescription.Compare));
            this.cmbDefaultCMNType.DataSource = list.ToArray();
            this.cmbDefaultCMNType.DisplayMember = "Description";
            this.cmbDefaultCMNType.ValueMember = "DbType";
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT * FROM tbl_pricecode_item WHERE ID = {ID}";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetComboBoxValue(this.cmbInventoryItem, reader["InventoryItemID"]);
                            Functions.SetComboBoxValue(this.cmbPriceCode, reader["PriceCodeID"]);
                            Functions.SetComboBoxText(this.cmbDefaultOrderType, reader["DefaultOrderType"]);
                            Functions.SetComboBoxValue(this.cmbPredefinedText, reader["PredefinedTextID"]);
                            Functions.SetNumericBoxValue(this.nmbSale_AllowablePrice, reader["Sale_AllowablePrice"]);
                            Functions.SetNumericBoxValue(this.nmbSale_BillablePrice, reader["Sale_BillablePrice"]);
                            Functions.SetCheckBoxChecked(this.chbReoccuringSale, reader["ReoccuringSale"]);
                            Functions.SetNumericBoxValue(this.nmbRent_AllowablePrice, reader["Rent_AllowablePrice"]);
                            Functions.SetNumericBoxValue(this.nmbRent_BillablePrice, reader["Rent_BillablePrice"]);
                            Functions.SetComboBoxText(this.cmbRentalType, reader["RentalType"]);
                            Functions.SetTextBoxText(this.txtBillingCode, reader["BillingCode"]);
                            Functions.SetTextBoxText(this.txtModifier1, reader["Modifier1"]);
                            Functions.SetTextBoxText(this.txtModifier2, reader["Modifier2"]);
                            Functions.SetTextBoxText(this.txtModifier3, reader["Modifier3"]);
                            Functions.SetTextBoxText(this.txtModifier4, reader["Modifier4"]);
                            Functions.SetComboBoxValue(this.cmbDefaultCMNType, reader["DefaultCMNType"]);
                            Functions.SetComboBoxValue(this.cmbAuthorizationType, reader["AuthorizationTypeID"]);
                            Functions.SetCheckBoxChecked(this.chbAcceptAssignment, reader["AcceptAssignment"]);
                            Functions.SetCheckBoxChecked(this.chbShowSpanDates, reader["ShowSpanDates"]);
                            Functions.SetCheckBoxChecked(this.chbFlatRate, reader["FlatRate"]);
                            Functions.SetCheckBoxChecked(this.chbTaxable, reader["Taxable"]);
                            Functions.SetCheckBoxChecked(this.chbBillInsurance, reader["BillInsurance"]);
                            Functions.SetNumericBoxValue(this.nmbOrderedQuantity, reader["OrderedQuantity"]);
                            Functions.SetTextBoxText(this.txtOrderedUnits, reader["OrderedUnits"]);
                            Functions.SetComboBoxText(this.cmbOrderedWhen, reader["OrderedWhen"]);
                            Functions.SetNumericBoxValue(this.nmbOrderedConverter, reader["OrderedConverter"]);
                            Functions.SetTextBoxText(this.txtBilledUnits, reader["BilledUnits"]);
                            Functions.SetComboBoxText(this.cmbBilledWhen, reader["BilledWhen"]);
                            Functions.SetNumericBoxValue(this.nmbBilledConverter, reader["BilledConverter"]);
                            Functions.SetTextBoxText(this.txtDeliveryUnits, reader["DeliveryUnits"]);
                            Functions.SetNumericBoxValue(this.nmbDeliveryConverter, reader["DeliveryConverter"]);
                            this.BillItemOn = Convert.ToString(reader["BillItemOn"]);
                            Functions.SetTextBoxText(this.txtDrugNoteField, reader["DrugNoteField"]);
                            Functions.SetTextBoxText(this.txtDrugControlNumber, reader["DrugControlNumber"]);
                            Functions.SetTextBoxText(this.txtUserField1, reader["UserField1"]);
                            Functions.SetTextBoxText(this.txtUserField2, reader["UserField2"]);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            this.LoadWarehouseList();
            return true;
        }

        [HandleDatabaseChanged(new string[] { "tbl_inventory", "tbl_warehouse" })]
        private void LoadWarehouseList()
        {
            this.InternalLoadWarehouseList(this.cmbInventoryItem.SelectedValue);
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_pricecode_item" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected void ProcessParameter_Other(FormParameters Params)
        {
            if (Params != null)
            {
                if (Params.ContainsKey("ID"))
                {
                    base.OpenObject(Params["ID"]);
                }
                else
                {
                    object obj2 = DBNull.Value;
                    if (!Params.TryGetValue("InventoryItemID", out obj2))
                    {
                        obj2 = DBNull.Value;
                    }
                    object obj3 = DBNull.Value;
                    if (!Params.TryGetValue("PriceCodeID", out obj3))
                    {
                        obj3 = DBNull.Value;
                    }
                    if ((NullableConvert.ToInt32(obj2) != null) && (NullableConvert.ToInt32(obj3) != null))
                    {
                        int? key = this.FindID(Conversions.ToInteger(obj2), Conversions.ToInteger(obj3));
                        if (key != null)
                        {
                            base.OpenObject(key);
                        }
                        else
                        {
                            this.ClearObject();
                            this.cmbInventoryItem.SelectedValue = obj2;
                            this.cmbPriceCode.SelectedValue = obj3;
                        }
                    }
                    else
                    {
                        this.ClearObject();
                        this.cmbInventoryItem.SelectedValue = obj2;
                        this.cmbPriceCode.SelectedValue = obj3;
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
                    command.Parameters.Add("AcceptAssignment", MySqlType.Bit).Value = this.chbAcceptAssignment.Checked;
                    command.Parameters.Add("BillInsurance", MySqlType.Bit).Value = this.chbBillInsurance.Checked;
                    command.Parameters.Add("BillingCode", MySqlType.VarChar, 50).Value = this.txtBillingCode.Text;
                    command.Parameters.Add("BillItemOn", MySqlType.Char, 0x16).Value = this.BillItemOn;
                    command.Parameters.Add("DefaultOrderType", MySqlType.Char, 6).Value = this.cmbDefaultOrderType.Text;
                    command.Parameters.Add("AuthorizationTypeID", MySqlType.Int).Value = this.cmbAuthorizationType.SelectedValue;
                    command.Parameters.Add("FlatRate", MySqlType.Bit).Value = this.chbFlatRate.Checked;
                    command.Parameters.Add("InventoryItemID", MySqlType.Int).Value = this.cmbInventoryItem.SelectedValue;
                    command.Parameters.Add("Modifier1", MySqlType.VarChar, 8).Value = this.txtModifier1.Text;
                    command.Parameters.Add("Modifier2", MySqlType.VarChar, 8).Value = this.txtModifier2.Text;
                    command.Parameters.Add("Modifier3", MySqlType.VarChar, 8).Value = this.txtModifier3.Text;
                    command.Parameters.Add("Modifier4", MySqlType.VarChar, 8).Value = this.txtModifier4.Text;
                    command.Parameters.Add("PriceCodeID", MySqlType.Int).Value = this.cmbPriceCode.SelectedValue;
                    command.Parameters.Add("PredefinedTextID", MySqlType.Int).Value = this.cmbPredefinedText.SelectedValue;
                    command.Parameters.Add("Rent_AllowablePrice", MySqlType.Double).Value = this.nmbRent_AllowablePrice.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Rent_BillablePrice", MySqlType.Double).Value = this.nmbRent_BillablePrice.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("RentalType", MySqlType.Char, 0x16).Value = this.cmbRentalType.Text;
                    command.Parameters.Add("ReoccuringSale", MySqlType.Bit).Value = this.chbReoccuringSale.Checked;
                    command.Parameters.Add("Sale_AllowablePrice", MySqlType.Double).Value = this.nmbSale_AllowablePrice.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Sale_BillablePrice", MySqlType.Double).Value = this.nmbSale_BillablePrice.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("ShowSpanDates", MySqlType.Bit).Value = this.chbShowSpanDates.Checked;
                    command.Parameters.Add("Taxable", MySqlType.Bit).Value = this.chbTaxable.Checked;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.Bit).Value = Globals.CompanyUserID;
                    command.Parameters.Add("DefaultCMNType", MySqlType.VarChar, 20).Value = this.cmbDefaultCMNType.SelectedValue;
                    command.Parameters.Add("OrderedQuantity", MySqlType.Double).Value = this.nmbOrderedQuantity.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("OrderedUnits", MySqlType.VarChar, 50).Value = this.txtOrderedUnits.Text;
                    command.Parameters.Add("OrderedWhen", MySqlType.VarChar, 20).Value = this.cmbOrderedWhen.Text;
                    command.Parameters.Add("OrderedConverter", MySqlType.Double).Value = this.nmbOrderedConverter.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("BilledUnits", MySqlType.VarChar, 50).Value = this.txtBilledUnits.Text;
                    command.Parameters.Add("BilledWhen", MySqlType.VarChar, 20).Value = this.cmbBilledWhen.Text;
                    command.Parameters.Add("BilledConverter", MySqlType.Double).Value = this.nmbBilledConverter.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("DeliveryUnits", MySqlType.VarChar, 50).Value = this.txtDeliveryUnits.Text;
                    command.Parameters.Add("DeliveryConverter", MySqlType.Double).Value = this.nmbDeliveryConverter.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("DrugNoteField", MySqlType.VarChar, 20).Value = this.txtDrugNoteField.Text;
                    command.Parameters.Add("DrugControlNumber", MySqlType.VarChar, 50).Value = this.txtDrugControlNumber.Text;
                    command.Parameters.Add("UserField1", MySqlType.VarChar, 100).Value = this.txtUserField1.Text;
                    command.Parameters.Add("UserField2", MySqlType.VarChar, 100).Value = this.txtUserField2.Text;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_pricecode_item", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_pricecode_item"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_pricecode_item");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT tbl_pricecode_item.ID,\r\n       tbl_inventoryitem.Name as InventoryItem,\r\n       tbl_pricecode_item.BillingCode,\r\n       tbl_pricecode.Name as PriceCode\r\nFROM tbl_pricecode_item\r\n     LEFT JOIN tbl_inventoryitem ON tbl_inventoryitem.ID = tbl_pricecode_item.InventoryItemID\r\n     LEFT JOIN tbl_pricecode ON tbl_pricecode.ID = tbl_pricecode_item.PriceCodeID\r\nORDER BY tbl_inventoryitem.Name, tbl_pricecode.Name", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 50);
            appearance.AddTextColumn("InventoryItem", "Inventory Item", 200);
            appearance.AddTextColumn("BillingCode", "Billing Code", 80);
            appearance.AddTextColumn("PriceCode", "Price Code", 100);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__299-0 e$__- = new _Closure$__299-0 {
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

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (this.nmbBilledConverter.AsDouble.GetValueOrDefault(0.0) < 0.0)
            {
                base.ValidationErrors.SetError(this.nmbBilledConverter, "Billed Converter can'not be negative");
            }
            else if (this.nmbBilledConverter.AsDouble.GetValueOrDefault(0.0) < 1E-09)
            {
                base.ValidationErrors.SetError(this.nmbBilledConverter, "Billed Converter can'not be zero");
            }
            if (this.nmbOrderedConverter.AsDouble.GetValueOrDefault(0.0) < 0.0)
            {
                base.ValidationErrors.SetError(this.nmbOrderedConverter, "Ordered Converter can'not be negative");
            }
            else if (this.nmbOrderedConverter.AsDouble.GetValueOrDefault(0.0) < 1E-09)
            {
                base.ValidationErrors.SetError(this.nmbOrderedConverter, "Ordered Converter can'not be zero");
            }
            if (NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue) == null)
            {
                base.ValidationErrors.SetError(this.cmbInventoryItem, "You must select Inventory Item");
            }
            if (NullableConvert.ToInt32(this.cmbPriceCode.SelectedValue) == null)
            {
                base.ValidationErrors.SetError(this.cmbPriceCode, "You must select Price Code");
            }
            if (string.Equals(this.cmbDefaultOrderType.Text, "Sale", StringComparison.OrdinalIgnoreCase))
            {
                Actualizer actualizer = new Actualizer(this.chbReoccuringSale.Checked ? "Re-occurring Sale" : "One Time Sale", this.BillItemOn, this.cmbOrderedWhen.Text, this.cmbBilledWhen.Text);
                base.ValidationErrors.SetError(this.gbBillItemOn, actualizer.Error_BillItemOn);
                base.ValidationErrors.SetError(this.cmbOrderedWhen, actualizer.Error_OrderedWhen);
                base.ValidationErrors.SetError(this.cmbBilledWhen, actualizer.Error_BilledWhen);
            }
            else if (!string.Equals(this.cmbDefaultOrderType.Text, "Rental", StringComparison.OrdinalIgnoreCase))
            {
                base.ValidationErrors.SetError(this.cmbDefaultOrderType, "You must Default Order Type");
            }
            else
            {
                Actualizer actualizer2 = new Actualizer(this.cmbRentalType.Text, this.BillItemOn, this.cmbOrderedWhen.Text, this.cmbBilledWhen.Text);
                base.ValidationErrors.SetError(this.cmbRentalType, actualizer2.Error_SaleRentType);
                base.ValidationErrors.SetError(this.gbBillItemOn, actualizer2.Error_BillItemOn);
                base.ValidationErrors.SetError(this.cmbOrderedWhen, actualizer2.Error_OrderedWhen);
                base.ValidationErrors.SetError(this.cmbBilledWhen, actualizer2.Error_BilledWhen);
            }
            if ((NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue) != null) && (NullableConvert.ToInt32(this.cmbPriceCode.SelectedValue) != null))
            {
                int? nullable3;
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.CommandText = !IsNew ? $"SELECT COUNT(ID) as `Count`
FROM tbl_pricecode_item
WHERE (InventoryItemID = {Conversions.ToInteger(this.cmbInventoryItem.SelectedValue)})
  AND (PriceCodeID = {Conversions.ToInteger(this.cmbPriceCode.SelectedValue)})
  AND (ID <> {ID})" : $"SELECT COUNT(ID) as `Count`
FROM tbl_pricecode_item
WHERE (InventoryItemID = {Conversions.ToInteger(this.cmbInventoryItem.SelectedValue)})
  AND (PriceCodeID = {Conversions.ToInteger(this.cmbPriceCode.SelectedValue)})";
                        nullable3 = NullableConvert.ToInt32(command.ExecuteScalar());
                    }
                }
                if ((nullable3 != null) && (0 < nullable3.Value))
                {
                    base.ValidationErrors.SetError(this.cmbInventoryItem, "Record for selected pair (Price code, Inventory Item) already exists");
                    base.ValidationErrors.SetError(this.cmbPriceCode, "Record for selected pair (Price code, Inventory Item) already exists");
                }
            }
        }

        [field: AccessedThroughProperty("lblValidWare")]
        private Label lblValidWare { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMod")]
        private Label lblMod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBillCode")]
        private Label lblBillCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPriceCode")]
        private Label lblPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbBillItemOn")]
        private GroupBox gbBillItemOn { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbFrequencyDefaults")]
        private GroupBox gbFrequencyDefaults { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbReoccuringSale")]
        private CheckBox chbReoccuringSale { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbShowSpanDates")]
        private CheckBox chbShowSpanDates { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbAcceptAssignment")]
        private CheckBox chbAcceptAssignment { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbTaxable")]
        private CheckBox chbTaxable { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbFlatRate")]
        private CheckBox chbFlatRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbRentalType")]
        private ComboBox cmbRentalType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDefaultOrderType")]
        private ComboBox cmbDefaultOrderType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier1")]
        private TextBox txtModifier1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier4")]
        private TextBox txtModifier4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier3")]
        private TextBox txtModifier3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModifier2")]
        private TextBox txtModifier2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBillingCode")]
        private TextBox txtBillingCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblItem")]
        private Label lblItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDefaultOrderType")]
        private Label lblDefaultOrderType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDefaultAuthorizationType")]
        private Label lblDefaultAuthorizationType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDefaultCMNRX")]
        private Label lblDefaultCMNRX { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPriceCode")]
        private Combobox cmbPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbBillingDefaults")]
        private GroupBox gbBillingDefaults { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbRentalInformation")]
        private GroupBox gbRentalInformation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbSaleInformation")]
        private GroupBox gbSaleInformation { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lnkDayOfPickup")]
        private LinkLabel lnkDayOfPickup { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbLastDayOfThePeriod")]
        private RadioButton rbLastDayOfThePeriod { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lnkLastDayOfTheMonth")]
        private LinkLabel lnkLastDayOfTheMonth { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbDayOfDelivery")]
        private RadioButton rbDayOfDelivery { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        private Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lstWarehouses")]
        private ListBox lstWarehouses { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbDefaultCMNType")]
        private ComboBox cmbDefaultCMNType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblRent_AllowablePrice")]
        private Label lblRent_AllowablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblRent_BillablePrice")]
        private Label lblRent_BillablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSale_AllowablePrice")]
        private Label lblSale_AllowablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSale_BillablePrice")]
        private Label lblSale_BillablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblRentalType")]
        private Label lblRentalType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbRent_AllowablePrice")]
        private NumericBox nmbRent_AllowablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbRent_BillablePrice")]
        private NumericBox nmbRent_BillablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbSale_AllowablePrice")]
        private NumericBox nmbSale_AllowablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbSale_BillablePrice")]
        private NumericBox nmbSale_BillablePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbAuthorizationType")]
        private ComboBox cmbAuthorizationType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPredefinedText")]
        private Label lblPredefinedText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPredefinedText")]
        private Combobox cmbPredefinedText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnFind")]
        private Button btnFind { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblConverter")]
        private Label lblConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label3")]
        private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbBilledConverter")]
        private NumericBox nmbBilledConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbOrderedQuantity")]
        private NumericBox nmbOrderedQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBilledWhen")]
        private ComboBox cmbBilledWhen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbOrderedWhen")]
        private ComboBox cmbOrderedWhen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBilledUnits")]
        private TextBox txtBilledUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtOrderedUnits")]
        private TextBox txtOrderedUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBilled")]
        private Label lblBilled { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQty")]
        private Label lblQty { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWhen")]
        private Label lblWhen { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUnits")]
        private Label lblUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbOrderedConverter")]
        private NumericBox nmbOrderedConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbDeliveryConverter")]
        private NumericBox nmbDeliveryConverter { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDelivery")]
        private Label lblDelivery { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDeliveryUnits")]
        private TextBox txtDeliveryUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbDrugIdentification")]
        private GroupBox gbDrugIdentification { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDrugControlNumber")]
        private TextBox txtDrugControlNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDrugNoteField")]
        private TextBox txtDrugNoteField { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDrugControlNumber")]
        private Label lblDrugControlNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDrugNoteField")]
        private Label lblDrugNoteField { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbBillInsurance")]
        private CheckBox chbBillInsurance { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbUserFields")]
        private GroupBox gbUserFields { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        protected override bool IsNew
        {
            get => 
                base.IsNew;
            set
            {
                base.IsNew = value;
                this.cmbInventoryItem.Enabled = value;
                this.cmbPriceCode.Enabled = value;
                this.btnFind.Enabled = value;
            }
        }

        protected string BillItemOn
        {
            get => 
                !this.rbDayOfDelivery.Checked ? (!this.rbLastDayOfThePeriod.Checked ? string.Empty : "Last day of the Period") : "Day of Delivery";
            set
            {
                StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
                this.rbDayOfDelivery.Checked = ordinalIgnoreCase.Equals(value, "Day of Delivery");
                this.rbLastDayOfThePeriod.Checked = ordinalIgnoreCase.Equals(value, "Last day of the Period");
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__299-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

