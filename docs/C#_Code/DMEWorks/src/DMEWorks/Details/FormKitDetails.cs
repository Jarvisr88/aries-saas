namespace DMEWorks.Details
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
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormKitDetails : FormDetails
    {
        private IContainer components;
        private object FInventoryItemID;
        private object FPriceCodeID;

        public FormKitDetails() : this(null)
        {
        }

        public FormKitDetails(ControlKitDetails Parent) : base(Parent)
        {
            this.InitializeComponent();
        }

        protected override void Clear()
        {
            this.FInventoryItemID = DBNull.Value;
            this.FPriceCodeID = DBNull.Value;
            Functions.SetTextBoxText(this.txtInventoryItem, DBNull.Value);
            Functions.SetTextBoxText(this.txtPriceCode, DBNull.Value);
            Functions.SetComboBoxValue(this.cmbWarehouse, DBNull.Value);
            Functions.SetNumericBoxValue(this.nmbQuantity, DBNull.Value);
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
            this.lblWarehouse = new Label();
            this.cmbWarehouse = new Combobox();
            this.nmbQuantity = new NumericBox();
            this.lblQuantity = new Label();
            this.Label2 = new Label();
            this.Label1 = new Label();
            this.txtPriceCode = new TextBox();
            this.txtInventoryItem = new TextBox();
            ((ISupportInitialize) this.ValidationErrors).BeginInit();
            base.SuspendLayout();
            this.btnCancel.TabIndex = 9;
            this.btnOK.TabIndex = 8;
            this.lblWarehouse.Location = new Point(4, 0x48);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new Size(0x58, 0x15);
            this.lblWarehouse.TabIndex = 4;
            this.lblWarehouse.Text = "Warehouse";
            this.lblWarehouse.TextAlign = ContentAlignment.MiddleRight;
            this.cmbWarehouse.Location = new Point(0x60, 0x48);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new Size(240, 0x15);
            this.cmbWarehouse.TabIndex = 5;
            this.nmbQuantity.BorderStyle = BorderStyle.Fixed3D;
            this.nmbQuantity.Location = new Point(0x60, 0x6c);
            this.nmbQuantity.Name = "nmbQuantity";
            this.nmbQuantity.Size = new Size(0x40, 0x15);
            this.nmbQuantity.TabIndex = 7;
            this.nmbQuantity.TextAlign = HorizontalAlignment.Left;
            this.lblQuantity.ForeColor = SystemColors.ControlText;
            this.lblQuantity.Location = new Point(4, 0x6c);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.RightToLeft = RightToLeft.No;
            this.lblQuantity.Size = new Size(0x58, 0x15);
            this.lblQuantity.TabIndex = 6;
            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.TextAlign = ContentAlignment.MiddleRight;
            this.Label2.Location = new Point(4, 40);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x58, 0x15);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Price Code";
            this.Label2.TextAlign = ContentAlignment.MiddleRight;
            this.Label1.Location = new Point(4, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x58, 0x15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Inventory Item";
            this.Label1.TextAlign = ContentAlignment.MiddleRight;
            this.txtPriceCode.BackColor = SystemColors.Window;
            this.txtPriceCode.Location = new Point(0x60, 40);
            this.txtPriceCode.Name = "txtPriceCode";
            this.txtPriceCode.ReadOnly = true;
            this.txtPriceCode.Size = new Size(240, 20);
            this.txtPriceCode.TabIndex = 3;
            this.txtInventoryItem.BackColor = SystemColors.Window;
            this.txtInventoryItem.Location = new Point(0x60, 8);
            this.txtInventoryItem.Name = "txtInventoryItem";
            this.txtInventoryItem.ReadOnly = true;
            this.txtInventoryItem.Size = new Size(240, 20);
            this.txtInventoryItem.TabIndex = 1;
            base.ClientSize = new Size(360, 0x105);
            base.Controls.Add(this.lblWarehouse);
            base.Controls.Add(this.cmbWarehouse);
            base.Controls.Add(this.nmbQuantity);
            base.Controls.Add(this.lblQuantity);
            base.Controls.Add(this.Label2);
            base.Controls.Add(this.Label1);
            base.Controls.Add(this.txtPriceCode);
            base.Controls.Add(this.txtInventoryItem);
            base.Name = "FormKitDetails";
            this.Text = "Kit Details";
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.txtInventoryItem, 0);
            base.Controls.SetChildIndex(this.txtPriceCode, 0);
            base.Controls.SetChildIndex(this.Label1, 0);
            base.Controls.SetChildIndex(this.Label2, 0);
            base.Controls.SetChildIndex(this.lblQuantity, 0);
            base.Controls.SetChildIndex(this.nmbQuantity, 0);
            base.Controls.SetChildIndex(this.cmbWarehouse, 0);
            base.Controls.SetChildIndex(this.lblWarehouse, 0);
            ((ISupportInitialize) this.ValidationErrors).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void LoadComboBoxes()
        {
            Cache.InitDropdown(this.cmbWarehouse, "tbl_warehouse", null);
        }

        protected override void LoadFromRow(DataRow Row)
        {
            if (Row.Table is ControlKitDetails.TableKitDetails)
            {
                ControlKitDetails.TableKitDetails table = (ControlKitDetails.TableKitDetails) Row.Table;
                this.FInventoryItemID = Row[table.Col_InventoryItemID];
                this.FPriceCodeID = Row[table.Col_PriceCodeID];
                Functions.SetTextBoxText(this.txtInventoryItem, Row[table.Col_InventoryItem]);
                Functions.SetTextBoxText(this.txtPriceCode, Row[table.Col_PriceCode]);
                Functions.SetComboBoxValue(this.cmbWarehouse, Row[table.Col_WarehouseID]);
                Functions.SetNumericBoxValue(this.nmbQuantity, Row[table.Col_Quantity]);
            }
        }

        protected override void SaveToRow(DataRow Row)
        {
            if (Row.Table is ControlKitDetails.TableKitDetails)
            {
                ControlKitDetails.TableKitDetails table = (ControlKitDetails.TableKitDetails) Row.Table;
                Row[table.Col_WarehouseID] = this.cmbWarehouse.SelectedValue;
                Row[table.Col_Warehouse] = this.cmbWarehouse.Text;
                Row[table.Col_Quantity] = this.nmbQuantity.AsInt32.GetValueOrDefault(0);
            }
        }

        protected override void ValidateObject()
        {
            if (base.Visible)
            {
                if (!Versioned.IsNumeric(this.cmbWarehouse.SelectedValue))
                {
                    this.ValidationErrors.SetError(this.cmbWarehouse, "You must select warehouse");
                }
                else
                {
                    this.ValidationErrors.SetError(this.cmbWarehouse, "");
                }
            }
        }

        [field: AccessedThroughProperty("cmbWarehouse")]
        internal virtual Combobox cmbWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nmbQuantity")]
        protected virtual NumericBox nmbQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPriceCode")]
        protected virtual TextBox txtPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtInventoryItem")]
        protected virtual TextBox txtInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblWarehouse")]
        private Label lblWarehouse { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblQuantity")]
        private Label lblQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label2")]
        private Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("Label1")]
        private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

