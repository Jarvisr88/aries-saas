namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlHyperlink : XlHyperlinkBase
    {
        public XlCellRange Reference { get; set; }

        public string DisplayText { get; set; }
    }
}

