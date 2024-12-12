namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;

    [DesignerGenerated]
    public class ControlPricingForKits : ControlPricing
    {
        private IContainer components;

        public ControlPricingForKits()
        {
            this.InitializeComponent();
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
            this.components = new System.ComponentModel.Container();
        }

        [HandleDatabaseChanged(new string[] { "tbl_pricecode", "tbl_pricecode_item" })]
        protected override void Load_Table_PriceCode_Item()
        {
            int? nullable = NullableConvert.ToInt32(this.cmbInventoryItem.SelectedValue);
            string selectCommandText = (nullable == null) ? "SELECT CAST(null as signed) as ID, '-- Must be selected later --' as Name\r\nFROM dual\r\nUNION ALL\r\nSELECT ID, Name\r\nFROM tbl_pricecode\r\nORDER BY Name" : $"SELECT CAST(null as signed) as ID, '-- Must be selected later --' as Name
FROM dual
UNION ALL
SELECT tbl_pricecode.ID, tbl_pricecode.Name
FROM tbl_pricecode
     INNER JOIN tbl_pricecode_item ON tbl_pricecode_item.PriceCodeID = tbl_pricecode.ID
WHERE (tbl_pricecode_item.InventoryItemID = {nullable.Value})";
            DataTable dataTable = new DataTable("Table");
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommandText, ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.MissingSchemaAction = MissingSchemaAction.Add;
                adapter.Fill(dataTable);
            }
            Functions.AssignDatasource(this.cmbPriceCode, dataTable, "Name", "ID");
        }
    }
}

