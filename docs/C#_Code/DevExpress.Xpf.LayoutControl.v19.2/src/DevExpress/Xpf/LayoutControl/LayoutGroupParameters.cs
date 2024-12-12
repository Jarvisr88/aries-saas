namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutGroupParameters : LayoutParametersBase
    {
        public LayoutGroupParameters(double itemSpace, ElementSizers itemSizers) : base(itemSpace)
        {
            this.ItemSizers = itemSizers;
        }

        public ElementSizers ItemSizers { get; private set; }
    }
}

