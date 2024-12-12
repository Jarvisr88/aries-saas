namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;

    internal class MinSizeConverter : SizeConverter
    {
        public MinSizeConverter(bool isWidth) : base(isWidth, 0.0)
        {
        }
    }
}

