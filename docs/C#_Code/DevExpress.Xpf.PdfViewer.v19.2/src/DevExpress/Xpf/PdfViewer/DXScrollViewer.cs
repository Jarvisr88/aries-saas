namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class DXScrollViewer : DevExpress.Xpf.DocumentViewer.DXScrollViewer
    {
        public static readonly DependencyProperty VerticalScrollBarWidthProperty;
        private static readonly DependencyPropertyKey VerticalScrollBarWidthPropertyKey;
        private const string VerticalScrollBarPart = "PART_VerticalScrollBar";

        static DXScrollViewer()
        {
            Type ownerType = typeof(DevExpress.Xpf.PdfViewer.DXScrollViewer);
            VerticalScrollBarWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("VerticalScrollBarWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            VerticalScrollBarWidthProperty = VerticalScrollBarWidthPropertyKey.DependencyProperty;
        }

        public DXScrollViewer()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.SizeChanged -= new SizeChangedEventHandler(this.OnVerticalScrollBarSizeChanged);
            });
            this.VerticalScrollBar = (ScrollBar) base.GetTemplateChild("PART_VerticalScrollBar");
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.SizeChanged += new SizeChangedEventHandler(this.OnVerticalScrollBarSizeChanged);
            });
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
        }

        protected virtual void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateVerticalScrollBarWidth();
        }

        protected virtual void OnVerticalScrollBarSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateVerticalScrollBarWidth();
        }

        private void UpdateVerticalScrollBarWidth()
        {
            this.VerticalScrollBarWidth = this.VerticalScrollBar.DesiredSize.Width;
        }

        public double VerticalScrollBarWidth
        {
            get => 
                (double) base.GetValue(VerticalScrollBarWidthProperty);
            private set => 
                base.SetValue(VerticalScrollBarWidthPropertyKey, value);
        }

        private ScrollBar VerticalScrollBar { get; set; }
    }
}

