namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabHeaderOptions : ITabHeaderLayoutOptions
    {
        public TabHeaderOptions(System.Windows.Size size, bool horz, bool autoFill, int scrolIndex, bool selectedRowFirst, bool fixedRows, double offset)
        {
            this.Size = size;
            this.IsHorizontal = horz;
            this.IsAutoFill = autoFill;
            this.ScrollIndex = scrolIndex;
            this.SelectedRowFirst = selectedRowFirst;
            this.FixedRows = fixedRows;
            this.Offset = offset;
        }

        public System.Windows.Size Size { get; private set; }

        public bool IsHorizontal { get; private set; }

        public bool IsAutoFill { get; private set; }

        public bool SelectedRowFirst { get; private set; }

        public int ScrollIndex { get; private set; }

        public bool FixedRows { get; private set; }

        public double Offset { get; private set; }
    }
}

