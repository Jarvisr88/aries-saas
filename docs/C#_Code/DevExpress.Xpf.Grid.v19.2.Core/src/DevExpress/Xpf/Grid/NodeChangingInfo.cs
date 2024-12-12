namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;
    using System.Runtime.CompilerServices;

    internal class NodeChangingInfo
    {
        public ColumnNode Node { get; set; }

        public DXPropertyIdentifier PropertyID { get; set; }

        public object NewValue { get; set; }

        public object OldValue { get; set; }

        public object DefaultValue { get; set; }
    }
}

