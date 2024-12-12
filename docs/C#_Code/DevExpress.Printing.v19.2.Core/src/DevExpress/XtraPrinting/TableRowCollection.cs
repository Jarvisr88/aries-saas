namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    public class TableRowCollection : CollectionBase
    {
        public int Add(TableRow row) => 
            base.InnerList.Add(row);

        public TableRow AddRow()
        {
            TableRow row = new TableRow();
            this.Add(row);
            return row;
        }

        [Description("Provides indexed access to individual items in the collection.")]
        public TableRow this[int index]
        {
            get => 
                (TableRow) base.List[index];
            set => 
                base.List[index] = value;
        }
    }
}

