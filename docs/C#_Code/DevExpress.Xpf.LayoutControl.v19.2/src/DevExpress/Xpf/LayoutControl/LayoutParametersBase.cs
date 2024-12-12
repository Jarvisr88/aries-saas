namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutParametersBase
    {
        public LayoutParametersBase(double itemSpace)
        {
            this.ItemSpace = itemSpace;
        }

        public double ItemSpace { get; set; }
    }
}

