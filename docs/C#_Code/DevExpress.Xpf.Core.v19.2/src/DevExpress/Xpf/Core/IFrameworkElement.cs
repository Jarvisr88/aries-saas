namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface IFrameworkElement
    {
        void InvalidateMeasureEx();
        Size Measure(Size availableSize);
    }
}

