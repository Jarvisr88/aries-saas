namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class XlTableCollection : XlReadonlyCollection<IXlTable, XlTable>, IXlTableCollection, IXlReadonlyCollection<IXlTable>, IEnumerable<IXlTable>, IEnumerable
    {
        public XlTableCollection(List<XlTable> tables) : base(tables)
        {
        }
    }
}

