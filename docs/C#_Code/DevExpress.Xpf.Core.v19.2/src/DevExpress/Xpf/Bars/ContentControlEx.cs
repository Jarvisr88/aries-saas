namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class ContentControlEx : ContentControl
    {
        public ContentControlEx();
        protected override Size ArrangeOverride(Size arrangeBounds);
        private Size MeasureArrange(Size arrangeBounds, bool arrange);
        protected override Size MeasureOverride(Size constraint);
    }
}

