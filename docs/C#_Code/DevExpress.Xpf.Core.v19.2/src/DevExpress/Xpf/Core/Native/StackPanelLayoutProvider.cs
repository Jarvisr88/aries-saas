namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class StackPanelLayoutProvider : LayoutProvider
    {
        public override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        public override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);

        public System.Windows.Controls.Orientation Orientation { get; set; }
    }
}

