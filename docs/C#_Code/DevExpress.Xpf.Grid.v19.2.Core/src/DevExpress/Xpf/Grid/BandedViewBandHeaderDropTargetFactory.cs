namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class BandedViewBandHeaderDropTargetFactory : ColumnHeaderDropTargetFactory
    {
        protected override IDropTarget CreateDropTargetCore(Panel panel) => 
            new BandedViewBandHeadersDropTarget(panel, this.FixedStyle);

        public DevExpress.Xpf.Grid.FixedStyle FixedStyle { get; set; }
    }
}

