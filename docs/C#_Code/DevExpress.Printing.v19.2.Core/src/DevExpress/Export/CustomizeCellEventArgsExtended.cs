namespace DevExpress.Export
{
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomizeCellEventArgsExtended : CustomizeCellEventArgs
    {
        public IColumn Column { get; set; }

        public IRowBase Row { get; set; }
    }
}

