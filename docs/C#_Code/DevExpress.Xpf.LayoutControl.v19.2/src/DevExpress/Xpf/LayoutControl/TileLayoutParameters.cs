namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class TileLayoutParameters : FlowLayoutParameters
    {
        public TileLayoutParameters(double itemSpace, double layerSpace, double groupHeaderSpace, bool breakFlowToFit, TileGroupHeaders groupHeaders) : base(itemSpace, layerSpace, breakFlowToFit, false, false, null)
        {
            this.GroupHeaders = groupHeaders;
            this.GroupHeaderSpace = groupHeaderSpace;
        }

        public TileGroupHeaders GroupHeaders { get; private set; }

        public double GroupHeaderSpace { get; private set; }
    }
}

