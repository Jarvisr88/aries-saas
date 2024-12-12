namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonClone=true)]
    public class FormInventoryItem : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormInventoryItem()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_inventoryitem" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.chbInactive);
            base.ChangesTracker.Subscribe(this.chbO2Tank);
            base.ChangesTracker.Subscribe(this.chbSerialized);
            base.ChangesTracker.Subscribe(this.chbService);
            base.ChangesTracker.Subscribe(this.cmbBasis);
            base.ChangesTracker.Subscribe(this.cmbCommissionPaidAt);
            base.ChangesTracker.Subscribe(this.cmbFrequency);
            base.ChangesTracker.Subscribe(this.cmbManufacturer);
            base.ChangesTracker.Subscribe(this.cmbPredefinedText);
            base.ChangesTracker.Subscribe(this.cmbProductType);
            base.ChangesTracker.Subscribe(this.cmbVendor);
            base.ChangesTracker.Subscribe(this.nmbFlatRateAmount);
            base.ChangesTracker.Subscribe(this.nmbPercentageAmount);
            base.ChangesTracker.Subscribe(this.nmbPurchasePrice);
            base.ChangesTracker.Subscribe(this.rbFlatRate);
            base.ChangesTracker.Subscribe(this.rbPercentage);
            base.ChangesTracker.Subscribe(this.txtBarcode);
            base.ChangesTracker.Subscribe(this.txtBarcodeType);
            base.ChangesTracker.Subscribe(this.txtInventoryCode);
            base.ChangesTracker.Subscribe(this.txtModelNumber);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtUserField1);
            base.ChangesTracker.Subscribe(this.txtUserField2);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.txtInventoryCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtModelNumber, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbManufacturer, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbProductType, DBNull.Value);
            Functions.SetTextBoxText(this.txtBarcode, DBNull.Value);
            Functions.SetTextBoxText(this.txtBarcodeType, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbVendor, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPredefinedText, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbO2Tank, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbService, DBNull.Value);
            Functions.SetCheckBoxChecked(this.chbSerialized, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbFlatRateAmount, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbPercentageAmount, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbPurchasePrice, DBNull.Value);
            Functions.SetRadioChecked(this.rbFlatRate, DBNull.Value);
            Functions.SetRadioChecked(this.rbPercentage, DBNull.Value);
            Functions.SetComboBoxText(this.cmbBasis, DBNull.Value);
            Functions.SetComboBoxText(this.cmbFrequency, DBNull.Value);
            Functions.SetComboBoxText(this.cmbCommissionPaidAt, DBNull.Value);
            Functions.SetTextBoxText(this.txtUserField1, DBNull.Value);
            Functions.SetTextBoxText(this.txtUserField2, DBNull.Value);
            this.ClearPriceCodeList();
        }

        private void ClearPriceCodeList()
        {
            this.lbPriceCode.DataSource = null;
        }

        protected override void CloneObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetComboBoxValue(this.cmbManufacturer, DBNull.Value);
            this.ClearPriceCodeList();
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_inventoryitem"))
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

        protected override void DoCloneClick()
        {
            base.SwitchToWorkArea();
            if (base.SaveOrCancelChanges())
            {
                int? nullable2;
                int? nullable = NullableConvert.ToInt32(this.ObjectID);
                if (nullable == null)
                {
                    throw new UserNotifyException("Object must be saved");
                }
                string text = this.txtName.Text;
                using (DMEWorks.Forms.VBInputBox box = new DMEWorks.Forms.VBInputBox())
                {
                    box.Icon = base.Icon;
                    box.Prompt = "Enter new name";
                    box.Text = this.Text;
                    box.Value = text;
                    box.ValidateValue += new ValidateValueEventHandler(FormInventoryItem.ValidateName);
                    if (box.ShowDialog() == DialogResult.OK)
                    {
                        text = box.Value;
                    }
                    else
                    {
                        return;
                    }
                }
                text ??= "";
                char[] trimChars = new char[] { ' ' };
                text = text.TrimEnd(trimChars);
                if (text.Length == 0)
                {
                    throw new UserNotifyException("Name cannot be empty");
                }
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.Parameters.Add("P_OldInventoryItemID", MySqlType.Int).Value = nullable.Value;
                        command.Parameters.Add("P_NewName", MySqlType.VarChar, 100).Value = text;
                        command.Parameters.Add("P_NewInventoryItemID", MySqlType.Int).Direction = ParameterDirection.Output;
                        connection.Open();
                        command.ExecuteProcedure("InventoryItem_Clone");
                        nullable2 = NullableConvert.ToInt32(command.Parameters["P_NewInventoryItemID"].Value);
                    }
                }
                if (nullable2 == null)
                {
                    throw new UserNotifyException("Unable to clone");
                }
                this.OnTableUpdate();
                this.InvalidateObjectList();
                base.OpenObject(nullable2.Value);
            }
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete inventory item '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Inventory item '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            using (DataTable table = new DataTable("table"))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW COLUMNS FROM tbl_inventoryitem", ClassGlobalObjects.ConnectionString_MySql))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(table);
                }
                Functions.LoadComboBoxItems(this.cmbBasis, table, "Basis");
                Functions.LoadComboBoxItems(this.cmbFrequency, table, "Frequency");
                Functions.LoadComboBoxItems(this.cmbCommissionPaidAt, table, "CommissionPaidAt");
            }
            Cache.InitDropdown(this.cmbManufacturer, "tbl_manufacturer", null);
            Cache.InitDropdown(this.cmbPredefinedText, "tbl_predefinedtext", null);
            Cache.InitDropdown(this.cmbProductType, "tbl_producttype", null);
            Cache.InitDropdown(this.cmbVendor, "tbl_vendor", null);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.gbCommission = new GroupBox();
            this.cmbBasis = new ComboBox();
            this.lblBasis = new Label();
            this.cmbFrequency = new ComboBox();
            this.nmbFlatRateAmount = new NumericBox();
            this.lblFrequency = new Label();
            this.rbFlatRate = new RadioButton();
            this.rbPercentage = new RadioButton();
            this.cmbCommissionPaidAt = new ComboBox();
            this.nmbPercentageAmount = new NumericBox();
            this.lblCommPaidAt = new Label();
            this.chbService = new CheckBox();
            this.chbSerialized = new CheckBox();
            this.chbO2Tank = new CheckBox();
            this.txtBarcode = new TextBox();
            this.lblProductType = new Label();
            this.lblBarcodeType = new Label();
            this.lblBarcode = new Label();
            this.txtBarcodeType = new TextBox();
            this.cmdReorder = new Button();
            this.cmdPricing = new Button();
            this.txtInventoryCode = new TextBox();
            this.txtName = new TextBox();
            this.lblModelNumber = new Label();
            this.lblInventoryCode = new Label();
            this.lblName = new Label();
            this.txtModelNumber = new TextBox();
            this.mnuGotoReorder = new MenuItem();
            this.mnuGotoPricing = new MenuItem();
            this.lblVendor = new Label();
            this.cmbVendor = new Combobox();
            this.cmbProductType = new Combobox();
            this.cmbPredefinedText = new Combobox();
            this.lblPredefinedText = new Label();
            this.lbPriceCode = new ListBox();
            this.lblPriceCode = new Label();
            this.chbInactive = new CheckBox();
            this.cmbManufacturer = new Combobox();
            this.lblManufacturer = new Label();
            this.nmbPurchasePrice = new NumericBox();
            this.lblPurchasePrice = new Label();
            this.mnuGotoInventory = new MenuItem();
            this.gbUserFields = new GroupBox();
            this.txtUserField2 = new TextBox();
            this.txtUserField1 = new TextBox();
            this.lblUserField2 = new Label();
            this.lblUserField1 = new Label();
            base.tpWorkArea.SuspendLayout();
            this.gbCommission.SuspendLayout();
            this.gbUserFields.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.gbUserFields);
            base.tpWorkArea.Controls.Add(this.lblPurchasePrice);
            base.tpWorkArea.Controls.Add(this.nmbPurchasePrice);
            base.tpWorkArea.Controls.Add(this.cmbManufacturer);
            base.tpWorkArea.Controls.Add(this.lblManufacturer);
            base.tpWorkArea.Controls.Add(this.chbInactive);
            base.tpWorkArea.Controls.Add(this.lblPriceCode);
            base.tpWorkArea.Controls.Add(this.lbPriceCode);
            base.tpWorkArea.Controls.Add(this.cmbPredefinedText);
            base.tpWorkArea.Controls.Add(this.lblPredefinedText);
            base.tpWorkArea.Controls.Add(this.cmbProductType);
            base.tpWorkArea.Controls.Add(this.cmbVendor);
            base.tpWorkArea.Controls.Add(this.lblVendor);
            base.tpWorkArea.Controls.Add(this.txtInventoryCode);
            base.tpWorkArea.Controls.Add(this.txtName);
            base.tpWorkArea.Controls.Add(this.txtModelNumber);
            base.tpWorkArea.Controls.Add(this.txtBarcode);
            base.tpWorkArea.Controls.Add(this.txtBarcodeType);
            base.tpWorkArea.Controls.Add(this.lblModelNumber);
            base.tpWorkArea.Controls.Add(this.lblInventoryCode);
            base.tpWorkArea.Controls.Add(this.lblName);
            base.tpWorkArea.Controls.Add(this.lblBarcodeType);
            base.tpWorkArea.Controls.Add(this.cmdReorder);
            base.tpWorkArea.Controls.Add(this.lblProductType);
            base.tpWorkArea.Controls.Add(this.chbService);
            base.tpWorkArea.Controls.Add(this.chbO2Tank);
            base.tpWorkArea.Controls.Add(this.gbCommission);
            base.tpWorkArea.Controls.Add(this.lblBarcode);
            base.tpWorkArea.Controls.Add(this.chbSerialized);
            base.tpWorkArea.Controls.Add(this.cmdPricing);
            base.tpWorkArea.Size = new Size(400, 0x1ca);
            base.tpWorkArea.Visible = true;
            MenuItem[] items = new MenuItem[] { this.mnuGotoInventory, this.mnuGotoPricing, this.mnuGotoReorder };
            base.cmnuGoto.MenuItems.AddRange(items);
            this.gbCommission.Controls.Add(this.cmbBasis);
            this.gbCommission.Controls.Add(this.lblBasis);
            this.gbCommission.Controls.Add(this.cmbFrequency);
            this.gbCommission.Controls.Add(this.nmbFlatRateAmount);
            this.gbCommission.Controls.Add(this.lblFrequency);
            this.gbCommission.Controls.Add(this.rbFlatRate);
            this.gbCommission.Controls.Add(this.rbPercentage);
            this.gbCommission.Controls.Add(this.cmbCommissionPaidAt);
            this.gbCommission.Controls.Add(this.nmbPercentageAmount);
            this.gbCommission.Controls.Add(this.lblCommPaidAt);
            this.gbCommission.Location = new Point(8, 0x120);
            this.gbCommission.Name = "gbCommission";
            this.gbCommission.Size = new Size(0x180, 0x60);
            this.gbCommission.TabIndex = 0x1a;
            this.gbCommission.TabStop = false;
            this.gbCommission.Text = "Sales Person Commission Basis";
            this.cmbBasis.BackColor = SystemColors.Window;
            this.cmbBasis.Cursor = Cursors.Default;
            this.cmbBasis.ForeColor = SystemColors.WindowText;
            this.cmbBasis.Location = new Point(240, 0x10);
            this.cmbBasis.Name = "cmbBasis";
            this.cmbBasis.RightToLeft = RightToLeft.No;
            this.cmbBasis.Size = new Size(0x88, 0x15);
            this.cmbBasis.TabIndex = 5;
            this.lblBasis.Location = new Point(0xa8, 0x10);
            this.lblBasis.Name = "lblBasis";
            this.lblBasis.Size = new Size(0x40, 0x15);
            this.lblBasis.TabIndex = 4;
            this.lblBasis.Text = "Basis";
            this.lblBasis.TextAlign = ContentAlignment.MiddleRight;
            this.cmbFrequency.BackColor = SystemColors.Window;
            this.cmbFrequency.Cursor = Cursors.Default;
            this.cmbFrequency.ForeColor = SystemColors.WindowText;
            this.cmbFrequency.Location = new Point(240, 0x40);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.RightToLeft = RightToLeft.No;
            this.cmbFrequency.Size = new Size(0x88, 0x15);
            this.cmbFrequency.TabIndex = 9;
            this.nmbFlatRateAmount.Location = new Point(0x58, 0x10);
            this.nmbFlatRateAmount.Name = "nmbFlatRateAmount";
            this.nmbFlatRateAmount.Size = new Size(0x40, 0x15);
            this.nmbFlatRateAmount.TabIndex = 1;
            this.lblFrequency.Location = new Point(0xa8, 0x40);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new Size(0x40, 0x15);
            this.lblFrequency.TabIndex = 8;
            this.lblFrequency.Text = "Frequency";
            this.lblFrequency.TextAlign = ContentAlignment.MiddleRight;
            this.rbFlatRate.BackColor = Color.Transparent;
            this.rbFlatRate.Checked = true;
            this.rbFlatRate.Cursor = Cursors.Default;
            this.rbFlatRate.ForeColor = SystemColors.ControlText;
            this.rbFlatRate.Location = new Point(8, 0x10);
            this.rbFlatRate.Name = "rbFlatRate";
            this.rbFlatRate.RightToLeft = RightToLeft.No;
            this.rbFlatRate.Size = new Size(80, 20);
            this.rbFlatRate.TabIndex = 0;
            this.rbFlatRate.TabStop = true;
            this.rbFlatRate.Text = "Flat Rate";
            this.rbFlatRate.UseVisualStyleBackColor = false;
            this.rbPercentage.BackColor = Color.Transparent;
            this.rbPercentage.Cursor = Cursors.Default;
            this.rbPercentage.ForeColor = SystemColors.ControlText;
            this.rbPercentage.Location = new Point(8, 40);
            this.rbPercentage.Name = "rbPercentage";
            this.rbPercentage.RightToLeft = RightToLeft.No;
            this.rbPercentage.Size = new Size(80, 20);
            this.rbPercentage.TabIndex = 2;
            this.rbPercentage.TabStop = true;
            this.rbPercentage.Text = "Percentage";
            this.rbPercentage.UseVisualStyleBackColor = false;
            this.cmbCommissionPaidAt.BackColor = SystemColors.Window;
            this.cmbCommissionPaidAt.Cursor = Cursors.Default;
            this.cmbCommissionPaidAt.ForeColor = SystemColors.WindowText;
            this.cmbCommissionPaidAt.Location = new Point(240, 40);
            this.cmbCommissionPaidAt.Name = "cmbCommissionPaidAt";
            this.cmbCommissionPaidAt.RightToLeft = RightToLeft.No;
            this.cmbCommissionPaidAt.Size = new Size(0x88, 0x15);
            this.cmbCommissionPaidAt.TabIndex = 7;
            this.nmbPercentageAmount.Location = new Point(0x58, 40);
            this.nmbPercentageAmount.Name = "nmbPercentageAmount";
            this.nmbPercentageAmount.Size = new Size(0x40, 0x15);
            this.nmbPercentageAmount.TabIndex = 3;
            this.lblCommPaidAt.Location = new Point(0xa8, 40);
            this.lblCommPaidAt.Name = "lblCommPaidAt";
            this.lblCommPaidAt.Size = new Size(0x40, 0x15);
            this.lblCommPaidAt.TabIndex = 6;
            this.lblCommPaidAt.Text = "Paid at";
            this.lblCommPaidAt.TextAlign = ContentAlignment.MiddleRight;
            this.chbService.BackColor = Color.Transparent;
            this.chbService.CheckAlign = ContentAlignment.MiddleRight;
            this.chbService.Cursor = Cursors.Default;
            this.chbService.ForeColor = SystemColors.ControlText;
            this.chbService.Location = new Point(8, 0xd8);
            this.chbService.Name = "chbService";
            this.chbService.RightToLeft = RightToLeft.No;
            this.chbService.Size = new Size(110, 0x15);
            this.chbService.TabIndex = 0x13;
            this.chbService.Text = "Service ";
            this.chbService.TextAlign = ContentAlignment.MiddleRight;
            this.chbService.UseVisualStyleBackColor = false;
            this.chbSerialized.BackColor = Color.Transparent;
            this.chbSerialized.CheckAlign = ContentAlignment.MiddleRight;
            this.chbSerialized.Cursor = Cursors.Default;
            this.chbSerialized.ForeColor = SystemColors.ControlText;
            this.chbSerialized.Location = new Point(8, 240);
            this.chbSerialized.Name = "chbSerialized";
            this.chbSerialized.RightToLeft = RightToLeft.No;
            this.chbSerialized.Size = new Size(110, 0x15);
            this.chbSerialized.TabIndex = 20;
            this.chbSerialized.Text = "Serialized";
            this.chbSerialized.TextAlign = ContentAlignment.MiddleRight;
            this.chbSerialized.UseVisualStyleBackColor = false;
            this.chbO2Tank.BackColor = Color.Transparent;
            this.chbO2Tank.CheckAlign = ContentAlignment.MiddleRight;
            this.chbO2Tank.Cursor = Cursors.Default;
            this.chbO2Tank.ForeColor = SystemColors.ControlText;
            this.chbO2Tank.Location = new Point(8, 0xc0);
            this.chbO2Tank.Name = "chbO2Tank";
            this.chbO2Tank.Size = new Size(110, 0x15);
            this.chbO2Tank.TabIndex = 0x12;
            this.chbO2Tank.Text = "O2 Tank";
            this.chbO2Tank.TextAlign = ContentAlignment.MiddleRight;
            this.chbO2Tank.UseVisualStyleBackColor = false;
            this.txtBarcode.ForeColor = SystemColors.WindowText;
            this.txtBarcode.Location = new Point(280, 0x70);
            this.txtBarcode.MaxLength = 0;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new Size(0x70, 20);
            this.txtBarcode.TabIndex = 13;
            this.lblProductType.Location = new Point(8, 0x58);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new Size(0x58, 0x15);
            this.lblProductType.TabIndex = 8;
            this.lblProductType.Text = "Product Type";
            this.lblProductType.TextAlign = ContentAlignment.MiddleRight;
            this.lblBarcodeType.Location = new Point(8, 0x70);
            this.lblBarcodeType.Name = "lblBarcodeType";
            this.lblBarcodeType.Size = new Size(0x58, 0x15);
            this.lblBarcodeType.TabIndex = 10;
            this.lblBarcodeType.Text = "Barcode Type";
            this.lblBarcodeType.TextAlign = ContentAlignment.MiddleRight;
            this.lblBarcode.Location = new Point(0xe0, 0x70);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new Size(0x30, 0x15);
            this.lblBarcode.TabIndex = 12;
            this.lblBarcode.Text = "Barcode";
            this.lblBarcode.TextAlign = ContentAlignment.MiddleRight;
            this.txtBarcodeType.ForeColor = SystemColors.WindowText;
            this.txtBarcodeType.Location = new Point(0x68, 0x70);
            this.txtBarcodeType.Name = "txtBarcodeType";
            this.txtBarcodeType.Size = new Size(0x70, 20);
            this.txtBarcodeType.TabIndex = 11;
            this.cmdReorder.BackColor = SystemColors.Control;
            this.cmdReorder.Cursor = Cursors.Default;
            this.cmdReorder.ForeColor = SystemColors.ControlText;
            this.cmdReorder.Location = new Point(0xa8, 0xc0);
            this.cmdReorder.Name = "cmdReorder";
            this.cmdReorder.RightToLeft = RightToLeft.No;
            this.cmdReorder.Size = new Size(0x38, 0x18);
            this.cmdReorder.TabIndex = 0x16;
            this.cmdReorder.Text = "Reorder";
            this.cmdReorder.UseVisualStyleBackColor = false;
            this.cmdPricing.BackColor = SystemColors.Control;
            this.cmdPricing.Cursor = Cursors.Default;
            this.cmdPricing.ForeColor = SystemColors.ControlText;
            this.cmdPricing.Location = new Point(0xa8, 0xe0);
            this.cmdPricing.Name = "cmdPricing";
            this.cmdPricing.RightToLeft = RightToLeft.No;
            this.cmdPricing.Size = new Size(0x38, 0x18);
            this.cmdPricing.TabIndex = 0x17;
            this.cmdPricing.Text = "Pricing";
            this.cmdPricing.UseVisualStyleBackColor = false;
            this.txtInventoryCode.ForeColor = SystemColors.WindowText;
            this.txtInventoryCode.Location = new Point(0x68, 0x20);
            this.txtInventoryCode.MaxLength = 0;
            this.txtInventoryCode.Name = "txtInventoryCode";
            this.txtInventoryCode.Size = new Size(0x70, 20);
            this.txtInventoryCode.TabIndex = 3;
            this.txtName.AcceptsReturn = true;
            this.txtName.BackColor = SystemColors.Window;
            this.txtName.Cursor = Cursors.IBeam;
            this.txtName.ForeColor = SystemColors.WindowText;
            this.txtName.Location = new Point(0x68, 8);
            this.txtName.MaxLength = 0;
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = RightToLeft.No;
            this.txtName.Size = new Size(0x120, 20);
            this.txtName.TabIndex = 1;
            this.lblModelNumber.Cursor = Cursors.Default;
            this.lblModelNumber.ForeColor = SystemColors.ControlText;
            this.lblModelNumber.Location = new Point(0xe0, 0x20);
            this.lblModelNumber.Name = "lblModelNumber";
            this.lblModelNumber.Size = new Size(0x30, 0x16);
            this.lblModelNumber.TabIndex = 4;
            this.lblModelNumber.Text = "Model #";
            this.lblModelNumber.TextAlign = ContentAlignment.MiddleRight;
            this.lblInventoryCode.Location = new Point(8, 0x20);
            this.lblInventoryCode.Name = "lblInventoryCode";
            this.lblInventoryCode.Size = new Size(0x58, 0x15);
            this.lblInventoryCode.TabIndex = 2;
            this.lblInventoryCode.Text = "Inventory Code";
            this.lblInventoryCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblName.Location = new Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x58, 0x15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Item Description";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.txtModelNumber.ForeColor = SystemColors.WindowText;
            this.txtModelNumber.Location = new Point(280, 0x20);
            this.txtModelNumber.MaxLength = 0;
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.Size = new Size(0x70, 20);
            this.txtModelNumber.TabIndex = 5;
            this.mnuGotoReorder.Index = 2;
            this.mnuGotoReorder.Text = "Reorder";
            this.mnuGotoPricing.Index = 1;
            this.mnuGotoPricing.Text = "Pricing";
            this.lblVendor.Location = new Point(8, 160);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new Size(0x58, 0x15);
            this.lblVendor.TabIndex = 0x10;
            this.lblVendor.Text = "Vendor";
            this.lblVendor.TextAlign = ContentAlignment.MiddleRight;
            this.cmbVendor.Location = new Point(0x68, 160);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.Size = new Size(0xd8, 0x15);
            this.cmbVendor.TabIndex = 0x11;
            this.cmbProductType.Location = new Point(0x68, 0x58);
            this.cmbProductType.Name = "cmbProductType";
            this.cmbProductType.Size = new Size(0xd8, 0x15);
            this.cmbProductType.TabIndex = 9;
            this.cmbPredefinedText.Location = new Point(0x68, 0x88);
            this.cmbPredefinedText.Name = "cmbPredefinedText";
            this.cmbPredefinedText.Size = new Size(0xd8, 0x15);
            this.cmbPredefinedText.TabIndex = 15;
            this.lblPredefinedText.Location = new Point(8, 0x88);
            this.lblPredefinedText.Name = "lblPredefinedText";
            this.lblPredefinedText.Size = new Size(0x58, 0x15);
            this.lblPredefinedText.TabIndex = 14;
            this.lblPredefinedText.Text = "Predefined Text";
            this.lblPredefinedText.TextAlign = ContentAlignment.MiddleRight;
            this.lbPriceCode.Location = new Point(0xe8, 0xd8);
            this.lbPriceCode.Name = "lbPriceCode";
            this.lbPriceCode.Size = new Size(160, 0x45);
            this.lbPriceCode.TabIndex = 0x19;
            this.lblPriceCode.BackColor = Color.Silver;
            this.lblPriceCode.Location = new Point(0xe8, 0xc0);
            this.lblPriceCode.Name = "lblPriceCode";
            this.lblPriceCode.Size = new Size(160, 0x18);
            this.lblPriceCode.TabIndex = 0x18;
            this.lblPriceCode.Text = "Pricing (double click to open)";
            this.lblPriceCode.TextAlign = ContentAlignment.MiddleCenter;
            this.chbInactive.BackColor = Color.Transparent;
            this.chbInactive.CheckAlign = ContentAlignment.MiddleRight;
            this.chbInactive.Cursor = Cursors.Default;
            this.chbInactive.ForeColor = SystemColors.ControlText;
            this.chbInactive.Location = new Point(8, 0x108);
            this.chbInactive.Name = "chbInactive";
            this.chbInactive.Size = new Size(110, 0x15);
            this.chbInactive.TabIndex = 0x15;
            this.chbInactive.Text = "Inactive";
            this.chbInactive.TextAlign = ContentAlignment.MiddleRight;
            this.chbInactive.UseVisualStyleBackColor = false;
            this.cmbManufacturer.Location = new Point(0x68, 0x40);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new Size(0xd8, 0x15);
            this.cmbManufacturer.TabIndex = 7;
            this.lblManufacturer.Location = new Point(8, 0x40);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new Size(0x58, 0x15);
            this.lblManufacturer.TabIndex = 6;
            this.lblManufacturer.Text = "Manufacturer";
            this.lblManufacturer.TextAlign = ContentAlignment.MiddleRight;
            this.nmbPurchasePrice.Location = new Point(0x68, 0x1b0);
            this.nmbPurchasePrice.Name = "nmbPurchasePrice";
            this.nmbPurchasePrice.Size = new Size(0x40, 0x15);
            this.nmbPurchasePrice.TabIndex = 0x1d;
            this.lblPurchasePrice.Location = new Point(8, 0x1b0);
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            this.lblPurchasePrice.Size = new Size(0x58, 0x15);
            this.lblPurchasePrice.TabIndex = 0x1c;
            this.lblPurchasePrice.Text = "Purchase Price";
            this.lblPurchasePrice.TextAlign = ContentAlignment.MiddleRight;
            this.mnuGotoInventory.Index = 0;
            this.mnuGotoInventory.Text = "Inventory";
            this.gbUserFields.Controls.Add(this.txtUserField2);
            this.gbUserFields.Controls.Add(this.txtUserField1);
            this.gbUserFields.Controls.Add(this.lblUserField2);
            this.gbUserFields.Controls.Add(this.lblUserField1);
            this.gbUserFields.Location = new Point(8, 0x180);
            this.gbUserFields.Name = "gbUserFields";
            this.gbUserFields.Size = new Size(0x180, 0x2c);
            this.gbUserFields.TabIndex = 0x1b;
            this.gbUserFields.TabStop = false;
            this.gbUserFields.Text = "User Fields";
            this.txtUserField2.Location = new Point(0x100, 0x10);
            this.txtUserField2.MaxLength = 100;
            this.txtUserField2.Name = "txtUserField2";
            this.txtUserField2.Size = new Size(120, 20);
            this.txtUserField2.TabIndex = 3;
            this.txtUserField1.Location = new Point(0x40, 0x10);
            this.txtUserField1.MaxLength = 100;
            this.txtUserField1.Name = "txtUserField1";
            this.txtUserField1.Size = new Size(120, 20);
            this.txtUserField1.TabIndex = 1;
            this.lblUserField2.Location = new Point(200, 0x10);
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
            base.ClientSize = new Size(0x198, 0x215);
            base.Name = "FormInventoryItem";
            this.Text = "Form Inventory Item";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            this.gbCommission.ResumeLayout(false);
            this.gbUserFields.ResumeLayout(false);
            this.gbUserFields.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lbPriceCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (NullableConvert.ToInt32(this.lbPriceCode.SelectedValue) == null)
                {
                    ClassGlobalObjects.ShowForm(FormFactories.FormPricing());
                }
                else
                {
                    FormParameters @params = new FormParameters("ID", Conversions.ToInteger(this.lbPriceCode.SelectedValue));
                    ClassGlobalObjects.ShowForm(FormFactories.FormPricing(), @params);
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

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = $"SELECT * FROM tbl_inventoryitem WHERE ID = {ID}";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.txtInventoryCode, reader["InventoryCode"]);
                            Functions.SetTextBoxText(this.txtModelNumber, reader["ModelNumber"]);
                            Functions.SetComboBoxValue(this.cmbManufacturer, reader["ManufacturerID"]);
                            Functions.SetComboBoxValue(this.cmbProductType, reader["ProductTypeID"]);
                            Functions.SetTextBoxText(this.txtBarcode, reader["Barcode"]);
                            Functions.SetTextBoxText(this.txtBarcodeType, reader["BarcodeType"]);
                            Functions.SetComboBoxValue(this.cmbVendor, reader["VendorID"]);
                            Functions.SetComboBoxValue(this.cmbPredefinedText, reader["PredefinedTextID"]);
                            Functions.SetCheckBoxChecked(this.chbO2Tank, reader["O2Tank"]);
                            Functions.SetCheckBoxChecked(this.chbService, reader["Service"]);
                            Functions.SetCheckBoxChecked(this.chbSerialized, reader["Serialized"]);
                            Functions.SetCheckBoxChecked(this.chbInactive, reader["Inactive"]);
                            Functions.SetNumericBoxValue(this.nmbFlatRateAmount, reader["FlatRateAmount"]);
                            Functions.SetNumericBoxValue(this.nmbPercentageAmount, reader["PercentageAmount"]);
                            Functions.SetNumericBoxValue(this.nmbPurchasePrice, reader["PurchasePrice"]);
                            Functions.SetRadioChecked(this.rbFlatRate, reader["FlatRate"]);
                            Functions.SetRadioChecked(this.rbPercentage, reader["Percentage"]);
                            Functions.SetComboBoxText(this.cmbBasis, reader["Basis"]);
                            Functions.SetComboBoxText(this.cmbFrequency, reader["Frequency"]);
                            Functions.SetComboBoxText(this.cmbCommissionPaidAt, reader["CommissionPaidAt"]);
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
            this.LoadPriceCodeList(ID);
            return true;
        }

        private void LoadPriceCodeList(object InventoryItemID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT tbl_pricecode_item.ID, tbl_pricecode.Name
FROM tbl_pricecode_item
     INNER JOIN tbl_pricecode ON tbl_pricecode_item.PriceCodeID = tbl_pricecode.ID
WHERE (tbl_pricecode_item.InventoryItemID = {InventoryItemID})", ClassGlobalObjects.ConnectionString_MySql))
            {
                DataTable dataTable = new DataTable("tbl_pricecode");
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
                this.lbPriceCode.DataSource = dataTable;
                this.lbPriceCode.DisplayMember = "Name";
                this.lbPriceCode.ValueMember = "ID";
            }
        }

        private void mnuGotoInventory_Click(object sender, EventArgs e)
        {
            if (NullableConvert.ToInt32(this.ObjectID) == null)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormInventory());
            }
            else
            {
                FormParameters @params = new FormParameters("InventoryItemID", Conversions.ToInteger(this.ObjectID));
                ClassGlobalObjects.ShowForm(FormFactories.FormInventory(), @params);
            }
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_inventoryitem" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private void Pricing_Click(object sender, EventArgs e)
        {
            if (NullableConvert.ToInt32(this.ObjectID) == null)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPricing());
            }
            else
            {
                FormParameters @params = new FormParameters("InventoryItemID", Conversions.ToInteger(this.ObjectID));
                ClassGlobalObjects.ShowForm(FormFactories.FormPricing(), @params);
            }
        }

        private void Reorder_Click(object sender, EventArgs e)
        {
            if (NullableConvert.ToInt32(this.ObjectID) == null)
            {
                ClassGlobalObjects.ShowForm(FormFactories.FormPurchaseOrder());
            }
            else
            {
                FormParameters @params = new FormParameters("InventoryItemID", Conversions.ToInteger(this.ObjectID));
                ClassGlobalObjects.ShowForm(FormFactories.FormPurchaseOrder(), @params);
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
                    command.Parameters.Add("Barcode", MySqlType.VarChar, 50).Value = this.txtBarcode.Text;
                    command.Parameters.Add("BarcodeType", MySqlType.VarChar, 50).Value = this.txtBarcodeType.Text;
                    command.Parameters.Add("Basis", MySqlType.Char, 7).Value = this.cmbBasis.Text;
                    command.Parameters.Add("CommissionPaidAt", MySqlType.Char, 7).Value = this.cmbCommissionPaidAt.Text;
                    command.Parameters.Add("FlatRate", MySqlType.Bit).Value = this.rbFlatRate.Checked;
                    command.Parameters.Add("FlatRateAmount", MySqlType.Double).Value = this.nmbFlatRateAmount.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("Frequency", MySqlType.Char, 8).Value = this.cmbFrequency.Text;
                    command.Parameters.Add("InventoryCode", MySqlType.VarChar, 50).Value = this.txtInventoryCode.Text;
                    command.Parameters.Add("ModelNumber", MySqlType.VarChar, 50).Value = this.txtModelNumber.Text;
                    command.Parameters.Add("Name", MySqlType.VarChar, 100).Value = this.txtName.Text;
                    command.Parameters.Add("O2Tank", MySqlType.Bit).Value = this.chbO2Tank.Checked;
                    command.Parameters.Add("Percentage", MySqlType.Bit).Value = this.rbPercentage.Checked;
                    command.Parameters.Add("PurchasePrice", MySqlType.Double).Value = this.nmbPurchasePrice.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("PercentageAmount", MySqlType.Double).Value = this.nmbPercentageAmount.AsDouble.GetValueOrDefault(0.0);
                    command.Parameters.Add("PredefinedTextID", MySqlType.Int).Value = this.cmbPredefinedText.SelectedValue;
                    command.Parameters.Add("ProductTypeID", MySqlType.Int).Value = this.cmbProductType.SelectedValue;
                    command.Parameters.Add("ManufacturerID", MySqlType.Int).Value = this.cmbManufacturer.SelectedValue;
                    command.Parameters.Add("VendorID", MySqlType.Int).Value = this.cmbVendor.SelectedValue;
                    command.Parameters.Add("Serialized", MySqlType.Bit).Value = this.chbSerialized.Checked;
                    command.Parameters.Add("Inactive", MySqlType.Bit).Value = this.chbInactive.Checked;
                    command.Parameters.Add("Service", MySqlType.Bit).Value = this.chbService.Checked;
                    command.Parameters.Add("UserField1", MySqlType.VarChar, 100).Value = this.txtUserField1.Text;
                    command.Parameters.Add("UserField2", MySqlType.VarChar, 100).Value = this.txtUserField2.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_inventoryitem", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_inventoryitem"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_inventoryitem");
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n   ID\r\n  ,Name\r\n  ,InventoryCode\r\n  ,ModelNumber\r\n  ,Inactive\r\nFROM tbl_inventoryitem\r\nORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("Name", "Name", 120);
            appearance.AddTextColumn("InventoryCode", "Inv Code", 80);
            appearance.AddTextColumn("ModelNumber", "Model number", 80);
            appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(Cache.InventoryItem_CellFormatting);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__195-0 e$__- = new _Closure$__195-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        private static void ValidateName(object Sender, ValidateValueEventArgs e)
        {
            char[] trimChars = new char[] { ' ' };
            string str = ((e.Value == null) ? "" : e.Value).TrimEnd(trimChars);
            if (str.Length == 0)
            {
                e.Valid = false;
                e.Message = "Name cannot be empty";
            }
            else if (100 >= str.Length)
            {
                e.Valid = true;
            }
            else
            {
                e.Valid = false;
                e.Message = "Name cannot be longer than 50 characters";
            }
        }

        [field: AccessedThroughProperty("chbO2Tank")]
        private CheckBox chbO2Tank { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbSerialized")]
        private CheckBox chbSerialized { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbService")]
        private CheckBox chbService { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbBasis")]
        private ComboBox cmbBasis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbFrequency")]
        private ComboBox cmbFrequency { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmdPricing")]
        private Button cmdPricing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmdReorder")]
        private Button cmdReorder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbCommission")]
        private GroupBox gbCommission { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBarcode")]
        private Label lblBarcode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBarcodeType")]
        private Label lblBarcodeType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblBasis")]
        private Label lblBasis { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFrequency")]
        private Label lblFrequency { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryCode")]
        private Label lblInventoryCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblModelNumber")]
        private Label lblModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblProductType")]
        private Label lblProductType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoPricing")]
        private MenuItem mnuGotoPricing { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoReorder")]
        private MenuItem mnuGotoReorder { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbFlatRateAmount")]
        private NumericBox nmbFlatRateAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPercentageAmount")]
        private NumericBox nmbPercentageAmount { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbFlatRate")]
        private RadioButton rbFlatRate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("rbPercentage")]
        private RadioButton rbPercentage { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBarcode")]
        private TextBox txtBarcode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtBarcodeType")]
        private TextBox txtBarcodeType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInventoryCode")]
        private TextBox txtInventoryCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtModelNumber")]
        private TextBox txtModelNumber { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCommissionPaidAt")]
        private ComboBox cmbCommissionPaidAt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCommPaidAt")]
        private Label lblCommPaidAt { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblVendor")]
        private Label lblVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbVendor")]
        private Combobox cmbVendor { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbProductType")]
        private Combobox cmbProductType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPredefinedText")]
        private Combobox cmbPredefinedText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPredefinedText")]
        private Label lblPredefinedText { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lbPriceCode")]
        private ListBox lbPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("chbInactive")]
        private CheckBox chbInactive { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbManufacturer")]
        private Combobox cmbManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblManufacturer")]
        private Label lblManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPurchasePrice")]
        private Label lblPurchasePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbPurchasePrice")]
        private NumericBox nmbPurchasePrice { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPriceCode")]
        private Label lblPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoInventory")]
        private MenuItem mnuGotoInventory { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

        [CompilerGenerated]
        internal sealed class _Closure$__195-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

