namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;

    internal class MaxSizeConverter : SizeConverter
    {
        public MaxSizeConverter(bool isWidth) : base(isWidth, double.PositiveInfinity)
        {
        }
    }
}

