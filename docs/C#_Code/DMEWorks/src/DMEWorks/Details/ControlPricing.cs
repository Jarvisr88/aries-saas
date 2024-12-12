namespace DMEWorks.Details
{
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ControlPricing : DmeUserControl
    {
        private IContainer components;
        private static readonly SimpleFilter InventoryItemFilter = new SimpleFilter("ISNULL([Inactive], false) = false");

        public ControlPricing()
        {
            base.Layout += new LayoutEventHandler(this.ControlPricing_Layout);
            base.SetStyle(ControlStyles.FixedHeight, true);
            this.InitializeComponent();
        }

        public void Clear()
        {
            this.cmbInventoryItem.SelectedValue = DBNull.Value;
            this.cmbPriceCode.SelectedValue = DBNull.Value;
        }

        private void cmbInventoryItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Table_PriceCode_Item();
        }

        private void ControlPricing_Layout(object sender, LayoutEventArgs e)
        {
            this.lblInventoryItem.Location = new Point(0, 0);
            this.lblPriceCode.Location = new Point(0, 0x18);
            this.cmbInventoryItem.Location = new Point(0x58, 0);
            this.cmbPriceCode.Location = new Point(0x58, 0x18);
            this.cmbInventoryItem.Width = Math.Max(base.Width - this.cmbInventoryItem.Left, 60);
            this.cmbPriceCode.Width = Math.Max(base.Width - this.cmbPriceCode.Left, 60);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void InitDropdowns()
        {
            Cache.InitDropdown(this.cmbInventoryItem, "tbl_inventoryitem", InventoryItemFilter);
            this.Load_Table_PriceCode_Item();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.cmbInventoryItem = new Combobox();
            this.cmbPriceCode = new Combobox();
            this.lblPriceCode = new Label();
            this.lblInventoryItem = new Label();
            base.SuspendLayout();
            this.cmbInventoryItem.Location = new Point(0x58, 0);
            this.cmbInventoryItem.Name = "cmbInventoryItem";
            this.cmbInventoryItem.Size = new Size(0x110, 0x15);
            this.cmbInventoryItem.TabIndex = 1;
            this.cmbPriceCode.Location = new Point(0x58, 0x18);
            this.cmbPriceCode.Name = "cmbPriceCode";
            this.cmbPriceCode.Size = new Size(0x110, 0x15);
            this.cmbPriceCode.TabIndex = 3;
            this.lblPriceCode.BackColor = Color.Transparent;
            this.lblPriceCode.Location = new Point(0, 0x18);
            this.lblPriceCode.Name = "lblPriceCode";
            this.lblPriceCode.Size = new Size(80, 0x15);
            this.lblPriceCode.TabIndex = 2;
            this.lblPriceCode.Text = "Price Code";
            this.lblPriceCode.TextAlign = ContentAlignment.MiddleRight;
            this.lblInventoryItem.BackColor = Color.Transparent;
            this.lblInventoryItem.Name = "lblInventoryItem";
            this.lblInventoryItem.Size = new Size(80, 0x15);
            this.lblInventoryItem.TabIndex = 0;
            this.lblInventoryItem.Text = "Inventory Item";
            this.lblInventoryItem.TextAlign = ContentAlignment.MiddleRight;
            Control[] controls = new Control[] { this.cmbInventoryItem, this.cmbPriceCode, this.lblPriceCode, this.lblInventoryItem };
            base.Controls.AddRange(controls);
            base.Name = "ControlPricing";
            base.Size = new Size(360, 0x30);
            base.ResumeLayout(false);
        }

        [HandleDatabaseChanged(new string[] { "tbl_pricecode", "tbl_pricecode_item" })]
        protected virtual void Load_Table_PriceCode_Item()
        {
            int? nullable = NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);
            string sql = (nullable == null) ? "SELECT ID, Name FROM tbl_pricecode ORDER BY Name" : $"SELECT tbl_pricecode.ID, tbl_pricecode.Name
FROM tbl_pricecode
     INNER JOIN tbl_pricecode_item ON tbl_pricecode_item.PriceCodeID = tbl_pricecode.ID
WHERE (tbl_pricecode_item.InventoryItemID = {nullable.Value})";
            ClassGlobalObjects.LoadCombobox(this.cmbPriceCode, sql, "Name", "ID");
        }

        public void ProcessBarcode(string Barcode)
        {
            Barcode ??= "";
            this.cmbInventoryItem.SelectItem($"[Barcode] = '{Barcode.Replace("'", "''")}'");
            this.Load_Table_PriceCode_Item();
            this.cmbPriceCode.Select();
        }

        [field: AccessedThroughProperty("cmbInventoryItem")]
        protected virtual Combobox cmbInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbPriceCode")]
        protected virtual Combobox cmbPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPriceCode")]
        private Label lblPriceCode { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblInventoryItem")]
        private Label lblInventoryItem { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public int? InventoryItemID =>
            NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);

        public int? PriceCodeID =>
            NullableConvert.ToInt32(this.cmbPriceCode.SelectedValue);
    }
}

