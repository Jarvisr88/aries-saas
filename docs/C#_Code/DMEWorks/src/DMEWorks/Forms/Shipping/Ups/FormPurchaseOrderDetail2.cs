namespace DMEWorks.Forms.Shipping.Ups
{
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormPurchaseOrderDetail2 : FormDetails
    {
        private IContainer components;
        private static readonly Regex regexDigits = new Regex("^[0-9]+$", RegexOptions.Singleline);

        public FormPurchaseOrderDetail2() : this(null)
        {
        }

        public FormPurchaseOrderDetail2(ControlCommodities Parent) : base(Parent)
        {
            this.InitializeComponent();
        }

        protected override void Clear()
        {
            Functions.SetTextBoxText(this.txtDescription, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbPackagingType, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbWeight, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbNumberOfPieces, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbCommodityValue, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbFreightClass, DBNull.Value);
            Functions.SetTextBoxText(this.txtNmfcPrimeCode, DBNull.Value);
            Functions.SetTextBoxText(this.txtNmfcSubCode, DBNull.Value);
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblPackagingType = new Label();
            this.lblNumberOfPieces = new Label();
            this.lblNmfcPrimeCode = new Label();
            this.lblNmfcSubCode = new Label();
            this.lblFreightClass = new Label();
            this.cmbFreightClass = new ComboBox();
            this.nmbNumberOfPieces = new NumericBox();
            this.nmbCommodityValue = new NumericBox();
            this.lblCommodityValue = new Label();
            this.txtDescription = new TextBox();
            this.lblDescription = new Label();
            this.cmbPackagingType = new ComboBox();
            this.nmbWeight = new NumericBox();
            this.lblWeight = new Label();
            this.lblWeightUnits = new Label();
            this.txtNmfcPrimeCode = new TextBox();
            this.txtNmfcSubCode = new TextBox();
            this.lblNmfc = new Label();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            base.SuspendLayout();
            this.btnCancel.Location = new Point(0x120, 0xe0);
            this.btnCancel.TabIndex = 0x12;
            this.btnOK.Location = new Point(0xd0, 0xe0);
            this.btnOK.TabIndex = 0x11;
            this.lblPackagingType.Location = new Point(8, 40);
            this.lblPackagingType.Name = "lblPackagingType";
            this.lblPackagingType.Size = new Size(100, 0x15);
            this.lblPackagingType.TabIndex = 2;
            this.lblPackagingType.Text = "Packaging Type";
            this.lblPackagingType.TextAlign = ContentAlignment.MiddleRight;
            this.lblNumberOfPieces.Location = new Point(8, 0x58);
            this.lblNumberOfPieces.Name = "lblNumberOfPieces";
            this.lblNumberOfPieces.Size = new Size(100, 0x15);
            this.lblNumberOfPieces.TabIndex = 7;
            this.lblNumberOfPieces.Text = "# Pieces";
            this.lblNumberOfPieces.TextAlign = ContentAlignment.MiddleRight;
            this.lblNmfcPrimeCode.Location = new Point(8, 0xc0);
            this.lblNmfcPrimeCode.Name = "lblNmfcPrimeCode";
            this.lblNmfcPrimeCode.Size = new Size(100, 0x15);
            this.lblNmfcPrimeCode.TabIndex = 13;
            this.lblNmfcPrimeCode.Text = "Prime Code";
            this.lblNmfcPrimeCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblNmfcSubCode.Location = new Point(0xd0, 0xc0);
            this.lblNmfcSubCode.Name = "lblNmfcSubCode";
            this.lblNmfcSubCode.Size = new Size(0x40, 0x15);
            this.lblNmfcSubCode.TabIndex = 15;
            this.lblNmfcSubCode.Text = "Sub Code";
            this.lblNmfcSubCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblFreightClass.Location = new Point(8, 0x88);
            this.lblFreightClass.Name = "lblFreightClass";
            this.lblFreightClass.Size = new Size(100, 0x15);
            this.lblFreightClass.TabIndex = 11;
            this.lblFreightClass.Text = "Freight Class";
            this.lblFreightClass.TextAlign = ContentAlignment.MiddleRight;
            this.cmbFreightClass.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFreightClass.Location = new Point(120, 0x88);
            this.cmbFreightClass.Name = "cmbFreightClass";
            this.cmbFreightClass.Size = new Size(240, 0x15);
            this.cmbFreightClass.TabIndex = 12;
            this.nmbNumberOfPieces.BorderStyle = BorderStyle.Fixed3D;
            this.nmbNumberOfPieces.Location = new Point(120, 0x58);
            this.nmbNumberOfPieces.Name = "nmbNumberOfPieces";
            this.nmbNumberOfPieces.Size = new Size(0x60, 0x15);
            this.nmbNumberOfPieces.TabIndex = 8;
            this.nmbNumberOfPieces.TextAlign = HorizontalAlignment.Left;
            this.nmbCommodityValue.BorderStyle = BorderStyle.Fixed3D;
            this.nmbCommodityValue.Location = new Point(120, 0x70);
            this.nmbCommodityValue.Name = "nmbCommodityValue";
            this.nmbCommodityValue.Size = new Size(0x60, 0x15);
            this.nmbCommodityValue.TabIndex = 10;
            this.nmbCommodityValue.TextAlign = HorizontalAlignment.Left;
            this.lblCommodityValue.Location = new Point(8, 0x70);
            this.lblCommodityValue.Name = "lblCommodityValue";
            this.lblCommodityValue.Size = new Size(100, 0x15);
            this.lblCommodityValue.TabIndex = 9;
            this.lblCommodityValue.Text = "Value";
            this.lblCommodityValue.TextAlign = ContentAlignment.MiddleRight;
            this.txtDescription.Location = new Point(120, 8);
            this.txtDescription.MaxLength = 0x2f3;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(240, 20);
            this.txtDescription.TabIndex = 1;
            this.lblDescription.Location = new Point(8, 8);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(100, 0x15);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = ContentAlignment.MiddleRight;
            this.cmbPackagingType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPackagingType.Location = new Point(120, 40);
            this.cmbPackagingType.Name = "cmbPackagingType";
            this.cmbPackagingType.Size = new Size(240, 0x15);
            this.cmbPackagingType.TabIndex = 3;
            this.nmbWeight.BorderStyle = BorderStyle.Fixed3D;
            this.nmbWeight.Location = new Point(120, 0x40);
            this.nmbWeight.Name = "nmbWeight";
            this.nmbWeight.Size = new Size(0x60, 0x15);
            this.nmbWeight.TabIndex = 5;
            this.nmbWeight.TextAlign = HorizontalAlignment.Left;
            this.lblWeight.Location = new Point(8, 0x40);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new Size(100, 0x15);
            this.lblWeight.TabIndex = 4;
            this.lblWeight.Text = "Weight";
            this.lblWeight.TextAlign = ContentAlignment.MiddleRight;
            this.lblWeightUnits.BorderStyle = BorderStyle.Fixed3D;
            this.lblWeightUnits.Location = new Point(0xe0, 0x40);
            this.lblWeightUnits.Name = "lblWeightUnits";
            this.lblWeightUnits.Size = new Size(0x20, 0x15);
            this.lblWeightUnits.TabIndex = 6;
            this.lblWeightUnits.Text = "lbs";
            this.lblWeightUnits.TextAlign = ContentAlignment.MiddleCenter;
            this.txtNmfcPrimeCode.Location = new Point(120, 0xc0);
            this.txtNmfcPrimeCode.MaxLength = 6;
            this.txtNmfcPrimeCode.Name = "txtNmfcPrimeCode";
            this.txtNmfcPrimeCode.Size = new Size(80, 20);
            this.txtNmfcPrimeCode.TabIndex = 14;
            this.txtNmfcSubCode.Location = new Point(280, 0xc0);
            this.txtNmfcSubCode.MaxLength = 2;
            this.txtNmfcSubCode.Name = "txtNmfcSubCode";
            this.txtNmfcSubCode.Size = new Size(80, 20);
            this.txtNmfcSubCode.TabIndex = 0x10;
            this.lblNmfc.BackColor = Color.Navy;
            this.lblNmfc.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.lblNmfc.ForeColor = Color.White;
            this.lblNmfc.Location = new Point(8, 0xa8);
            this.lblNmfc.Name = "lblNmfc";
            this.lblNmfc.Size = new Size(0x160, 0x15);
            this.lblNmfc.TabIndex = 0x27;
            this.lblNmfc.Text = "NMFC";
            this.lblNmfc.TextAlign = ContentAlignment.MiddleLeft;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x170, 0x105);
            base.Controls.Add(this.lblNmfc);
            base.Controls.Add(this.txtNmfcSubCode);
            base.Controls.Add(this.txtNmfcPrimeCode);
            base.Controls.Add(this.lblWeightUnits);
            base.Controls.Add(this.nmbWeight);
            base.Controls.Add(this.lblWeight);
            base.Controls.Add(this.cmbPackagingType);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.lblDescription);
            base.Controls.Add(this.nmbNumberOfPieces);
            base.Controls.Add(this.cmbFreightClass);
            base.Controls.Add(this.lblFreightClass);
            base.Controls.Add(this.lblNmfcSubCode);
            base.Controls.Add(this.lblNmfcPrimeCode);
            base.Controls.Add(this.lblNumberOfPieces);
            base.Controls.Add(this.lblPackagingType);
            base.Controls.Add(this.nmbCommodityValue);
            base.Controls.Add(this.lblCommodityValue);
            base.Name = "FormPurchaseOrderDetail2";
            this.Text = "Commodity";
            base.Controls.SetChildIndex(this.lblCommodityValue, 0);
            base.Controls.SetChildIndex(this.nmbCommodityValue, 0);
            base.Controls.SetChildIndex(this.lblPackagingType, 0);
            base.Controls.SetChildIndex(this.lblNumberOfPieces, 0);
            base.Controls.SetChildIndex(this.lblNmfcPrimeCode, 0);
            base.Controls.SetChildIndex(this.lblNmfcSubCode, 0);
            base.Controls.SetChildIndex(this.lblFreightClass, 0);
            base.Controls.SetChildIndex(this.cmbFreightClass, 0);
            base.Controls.SetChildIndex(this.nmbNumberOfPieces, 0);
            base.Controls.SetChildIndex(this.lblDescription, 0);
            base.Controls.SetChildIndex(this.txtDescription, 0);
            base.Controls.SetChildIndex(this.cmbPackagingType, 0);
            base.Controls.SetChildIndex(this.lblWeight, 0);
            base.Controls.SetChildIndex(this.nmbWeight, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.lblWeightUnits, 0);
            base.Controls.SetChildIndex(this.txtNmfcPrimeCode, 0);
            base.Controls.SetChildIndex(this.txtNmfcSubCode, 0);
            base.Controls.SetChildIndex(this.lblNmfc, 0);
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void Load_Table_UpsShipping_FreightClass()
        {
            Cache.InitDropdown(this.cmbFreightClass, "tbl_upsshipping_freightclass", null);
        }

        public void Load_Table_UpsShipping_PackagingType()
        {
            Cache.InitDropdown(this.cmbPackagingType, "tbl_upsshipping_packagingtype", null);
        }

        public override void LoadComboBoxes()
        {
            this.Load_Table_UpsShipping_FreightClass();
            this.Load_Table_UpsShipping_PackagingType();
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlCommodities.TableCommodities)
            {
                ControlCommodities.TableCommodities table = (ControlCommodities.TableCommodities) Row.Table;
                Functions.SetTextBoxText(this.txtDescription, Row[table.Col_Description]);
                Functions.SetComboBoxValue(this.cmbPackagingType, Row[table.Col_PackagingType]);
                Functions.SetNumericBoxValue(this.nmbWeight, Row[table.Col_Weight]);
                Functions.SetNumericBoxValue(this.nmbNumberOfPieces, Row[table.Col_NumberOfPieces]);
                Functions.SetNumericBoxValue(this.nmbCommodityValue, Row[table.Col_CommodityValue]);
                Functions.SetComboBoxValue(this.cmbFreightClass, Row[table.Col_FreightClass]);
                Functions.SetTextBoxText(this.txtNmfcPrimeCode, Row[table.Col_NmfcPrimeCode]);
                Functions.SetTextBoxText(this.txtNmfcSubCode, Row[table.Col_NmfcSubCode]);
            }
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlCommodities.TableCommodities)
            {
                ControlCommodities.TableCommodities table = (ControlCommodities.TableCommodities) Row.Table;
                Row.BeginEdit();
                try
                {
                    Row[table.Col_Description] = this.txtDescription.Text.Trim();
                    Row[table.Col_PackagingType] = this.cmbPackagingType.SelectedValue;
                    Row[table.Col_Weight] = NullableConvert.ToDb(this.nmbWeight.AsDecimal);
                    Row[table.Col_NumberOfPieces] = NullableConvert.ToDb(this.nmbNumberOfPieces.AsInt32);
                    Row[table.Col_CommodityValue] = NullableConvert.ToDb(this.nmbCommodityValue.AsDecimal);
                    Row[table.Col_FreightClass] = this.cmbFreightClass.SelectedValue;
                    Row[table.Col_NmfcPrimeCode] = this.txtNmfcPrimeCode.Text.Trim();
                    Row[table.Col_NmfcSubCode] = this.txtNmfcSubCode.Text.Trim();
                    Row.EndEdit();
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Row.CancelEdit();
                    throw;
                }
            }
        }

        protected override void ValidateObject()
        {
            if (this.txtDescription.Text.Trim().Length == 0)
            {
                this.ValidationErrors.SetError(this.lblDescription, "Description is required");
            }
            else
            {
                this.ValidationErrors.SetError(this.lblDescription, "");
            }
            string selectedValue = this.cmbPackagingType.SelectedValue as string;
            if (((selectedValue != null) ? selectedValue.Trim() : "").Length == 0)
            {
                this.ValidationErrors.SetError(this.lblPackagingType, "Packaging Type is required");
            }
            else
            {
                this.ValidationErrors.SetError(this.lblPackagingType, "");
            }
            string str2 = this.cmbFreightClass.SelectedValue as string;
            if (((str2 != null) ? str2.Trim() : "").Length == 0)
            {
                this.ValidationErrors.SetError(this.lblFreightClass, "Freight Class is required");
            }
            else
            {
                this.ValidationErrors.SetError(this.lblFreightClass, "");
            }
            decimal? asDecimal = this.nmbWeight.AsDecimal;
            if (asDecimal == null)
            {
                this.ValidationErrors.SetError(this.lblWeight, "Weight is required");
            }
            else if (Convert.ToDouble(asDecimal.Value) <= 0.001)
            {
                this.ValidationErrors.SetError(this.lblWeight, "Weight must be positive");
            }
            else
            {
                this.ValidationErrors.SetError(this.lblWeight, "");
            }
            decimal? nullable2 = this.nmbCommodityValue.AsDecimal;
            if (nullable2 == null)
            {
                this.ValidationErrors.SetError(this.lblCommodityValue, "Value is required");
            }
            else if (Convert.ToDouble(nullable2.Value) <= 0.001)
            {
                this.ValidationErrors.SetError(this.lblCommodityValue, "Value must be positive");
            }
            else
            {
                this.ValidationErrors.SetError(this.lblCommodityValue, "");
            }
            decimal? nullable3 = this.nmbNumberOfPieces.AsDecimal;
            if (nullable3 == null)
            {
                this.ValidationErrors.SetError(this.lblNumberOfPieces, "# Pieces is required");
            }
            else if (Convert.ToDouble(nullable3.Value) <= 0.001)
            {
                this.ValidationErrors.SetError(this.lblNumberOfPieces, "# Pieces must be positive");
            }
            else if (0.001 < Convert.ToDouble(Math.Abs(decimal.Subtract(nullable3.Value, decimal.Round(nullable3.Value)))))
            {
                this.ValidationErrors.SetError(this.lblNumberOfPieces, "# Pieces must be integer");
            }
            else
            {
                this.ValidationErrors.SetError(this.lblNumberOfPieces, "");
            }
            string input = this.txtNmfcPrimeCode.Text.Trim();
            string str4 = this.txtNmfcSubCode.Text.Trim();
            if ((0 >= input.Length) && (0 >= str4.Length))
            {
                this.ValidationErrors.SetError(this.lblNmfcPrimeCode, "");
                this.ValidationErrors.SetError(this.lblNmfcSubCode, "");
            }
            else
            {
                if (input.Length == 0)
                {
                    this.ValidationErrors.SetError(this.lblNmfcPrimeCode, "Prime Code is required");
                }
                else if (input.Length != 6)
                {
                    this.ValidationErrors.SetError(this.lblNmfcPrimeCode, "Prime Code length must be 6 characters");
                }
                else if (!regexDigits.IsMatch(input))
                {
                    this.ValidationErrors.SetError(this.lblNmfcPrimeCode, "Prime Code must contain only digits");
                }
                else
                {
                    this.ValidationErrors.SetError(this.lblNmfcPrimeCode, "");
                }
                if (str4.Length == 0)
                {
                    this.ValidationErrors.SetError(this.lblNmfcSubCode, "Sub Code is required");
                }
                else if (str4.Length != 2)
                {
                    this.ValidationErrors.SetError(this.lblNmfcSubCode, "Sub Code length must be 2 characters");
                }
                else if (!regexDigits.IsMatch(str4))
                {
                    this.ValidationErrors.SetError(this.lblNmfcSubCode, "Sub Code must contain only digits");
                }
                else
                {
                    this.ValidationErrors.SetError(this.lblNmfcSubCode, "");
                }
            }
        }

        [field: AccessedThroughProperty("lblPackagingType")]
        private Label lblPackagingType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNumberOfPieces")]
        private Label lblNumberOfPieces { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNmfcPrimeCode")]
        private Label lblNmfcPrimeCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNmfcSubCode")]
        private Label lblNmfcSubCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblFreightClass")]
        private Label lblFreightClass { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbFreightClass")]
        private ComboBox cmbFreightClass { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbNumberOfPieces")]
        private NumericBox nmbNumberOfPieces { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbCommodityValue")]
        private NumericBox nmbCommodityValue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblCommodityValue")]
        private Label lblCommodityValue { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDescription")]
        private TextBox txtDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDescription")]
        private Label lblDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPackagingType")]
        private ComboBox cmbPackagingType { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbWeight")]
        private NumericBox nmbWeight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWeight")]
        private Label lblWeight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWeightUnits")]
        private Label lblWeightUnits { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNmfcPrimeCode")]
        private TextBox txtNmfcPrimeCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtNmfcSubCode")]
        private TextBox txtNmfcSubCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblNmfc")]
        private Label lblNmfc { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

