namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class XlTableColumnCollection : XlReadonlyCollection<IXlTableColumn, XlTableColumn>, IXlTableColumnCollection, IXlReadonlyCollection<IXlTableColumn>, IEnumerable<IXlTableColumn>, IEnumerable
    {
        public XlTableColumnCollection(List<XlTableColumn> columns) : base(columns)
        {
        }

        public int IndexOf(string name)
        {
            XlTableColumn column = base[name] as XlTableColumn;
            return ((column == null) ? -1 : column.ColumnIndex);
        }
    }
}

