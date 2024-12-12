namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class XlGroup : IXlGroup
    {
        public int OutlineLevel { get; set; }

        public bool IsCollapsed { get; set; }
    }
}

