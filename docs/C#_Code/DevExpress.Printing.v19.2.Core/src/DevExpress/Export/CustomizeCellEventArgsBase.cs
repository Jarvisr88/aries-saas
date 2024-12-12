namespace DevExpress.Export
{
    using System;
    using System.Runtime.CompilerServices;

    public class CustomizeCellEventArgsBase : DataAwareEventArgsBase
    {
        public string ColumnFieldName { get; set; }

        public XlFormattingObject Formatting { get; set; }

        public SheetAreaType AreaType { get; set; }
    }
}

