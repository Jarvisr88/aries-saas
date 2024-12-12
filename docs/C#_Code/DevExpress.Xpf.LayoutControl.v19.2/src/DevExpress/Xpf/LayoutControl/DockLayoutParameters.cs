namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class DockLayoutParameters : LayoutParametersBase
    {
        public DockLayoutParameters(double itemSpace, ElementSizers itemSizers) : base(itemSpace)
        {
            this.ItemSizers = itemSizers;
        }

        public ElementSizers ItemSizers { get; private set; }
    }
}

