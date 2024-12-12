namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class DragOverEventSource
    {
        public IDragEventArgs Args { get; set; }

        public DevExpress.Xpf.Core.DragDrop.Native.DropInfo DropInfo { get; set; }

        public DropPosition Position { get; set; }

        public double Ratio { get; set; }
    }
}

