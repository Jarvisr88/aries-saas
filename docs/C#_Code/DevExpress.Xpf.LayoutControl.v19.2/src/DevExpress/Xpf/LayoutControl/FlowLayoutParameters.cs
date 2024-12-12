namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class FlowLayoutParameters : LayoutParametersBase
    {
        public FlowLayoutParameters(double itemSpace, double layerSpace, bool breakFlowToFit, bool stretchContent, bool showLayerSeparators, DevExpress.Xpf.LayoutControl.LayerSeparators layerSeparators) : base(itemSpace)
        {
            this.LayerSpace = layerSpace;
            this.BreakFlowToFit = breakFlowToFit;
            this.StretchContent = stretchContent;
            this.ShowLayerSeparators = showLayerSeparators;
            this.LayerSeparators = layerSeparators;
        }

        public bool BreakFlowToFit { get; private set; }

        public DevExpress.Xpf.LayoutControl.LayerSeparators LayerSeparators { get; private set; }

        public double LayerSpace { get; private set; }

        public bool ShowLayerSeparators { get; private set; }

        public bool StretchContent { get; private set; }
    }
}

