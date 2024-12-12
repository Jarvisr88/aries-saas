namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PreviewAreaProvider : ServiceBase, IPreviewAreaProvider
    {
        public double GetScaleX() => 
            base.AssociatedObject.GetScaleX();

        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
            LayoutHelper.FindElementByName(base.AssociatedObject.GetRootVisual(), "PART_PreviewImage").Do<FrameworkElement>(x => this.PreviewArea = x.RenderSize);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnAssociatedObjectLoaded);
        }

        public Size PreviewArea { get; private set; }
    }
}

