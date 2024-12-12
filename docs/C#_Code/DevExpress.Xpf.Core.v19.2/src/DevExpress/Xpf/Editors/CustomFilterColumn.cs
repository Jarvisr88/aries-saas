namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomFilterColumn
    {
        public CustomFilterColumn()
        {
        }

        public CustomFilterColumn(IDataColumnInfo column, DevExpress.Data.Filtering.FilterCondition filterCondition)
        {
            this.Column = column;
            this.FilterCondition = filterCondition;
        }

        public IDataColumnInfo Column { get; set; }

        public DevExpress.Data.Filtering.FilterCondition FilterCondition { get; set; }
    }
}

