namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Windows;

    public abstract class PageVisualizer
    {
        protected PageVisualizer()
        {
        }

        public abstract FrameworkElement Visualize(PSPage page, int pageIndex, int pageCount);
    }
}

