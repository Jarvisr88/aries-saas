namespace DMEWorks.Forms.Shipping.Ups
{
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;

    [DesignerGenerated]
    public class ControlCommodities : ControlDetails
    {
        private IContainer components;

        public ControlCommodities()
        {
            this.InitializeComponent();
        }

        protected override FormDetails CreateDialog(object param) => 
            base.AddDialog(new FormPurchaseOrderDetail2(this));

        protected override ControlDetails.TableDetails CreateTable() => 
            new TableCommodities();

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected TableCommodities GetTable() => 
            (TableCommodities) base.F_TableDetails;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            base.Name = "ControlCommodities";
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("Description", "Description", 120);
            Appearance.AddTextColumn("NumberOfPieces", "#Pieces", 60);
            Appearance.AddTextColumn("Weight", "Weight", 60);
        }

        public TableCommodities Table()
        {
            TableCommodities commodities1 = this.GetTable().Copy();
            commodities1.AcceptChanges();
            return commodities1;
        }

        public class TableCommodities : ControlDetails.TableDetails
        {
            public DataColumn Col_Description;
            public DataColumn Col_Weight;
            public DataColumn Col_NumberOfPieces;
            public DataColumn Col_PackagingType;
            public DataColumn Col_CommodityValue;
            public DataColumn Col_FreightClass;
            public DataColumn Col_NmfcPrimeCode;
            public DataColumn Col_NmfcSubCode;

            public TableCommodities() : this("tbl_commodities")
            {
            }

            public TableCommodities(string TableName) : base(TableName)
            {
            }

            public ControlCommodities.TableCommodities Copy() => 
                (ControlCommodities.TableCommodities) base.Copy();

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_Description = base.Columns["Description"];
                this.Col_Weight = base.Columns["Weight"];
                this.Col_NumberOfPieces = base.Columns["NumberOfPieces"];
                this.Col_PackagingType = base.Columns["PackagingType"];
                this.Col_CommodityValue = base.Columns["CommodityValue"];
                this.Col_FreightClass = base.Columns["FreightClass"];
                this.Col_NmfcPrimeCode = base.Columns["NmfcPrimeCode"];
                this.Col_NmfcSubCode = base.Columns["NmfcSubCode"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                base.Columns.Add("Description", typeof(string));
                base.Columns.Add("Weight", typeof(decimal));
                base.Columns.Add("NumberOfPieces", typeof(int));
                base.Columns.Add("PackagingType", typeof(string));
                base.Columns.Add("CommodityValue", typeof(decimal));
                base.Columns.Add("FreightClass", typeof(string));
                base.Columns.Add("NmfcPrimeCode", typeof(string));
                base.Columns.Add("NmfcSubCode", typeof(string));
            }
        }
    }
}

