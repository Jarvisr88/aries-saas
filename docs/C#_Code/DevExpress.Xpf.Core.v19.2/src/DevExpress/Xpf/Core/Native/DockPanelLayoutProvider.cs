namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DockPanelLayoutProvider : LayoutProvider
    {
        public override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        private Dock GetDock(FrameworkRenderElementContext element);
        public override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);

        public bool LastChildFill { get; set; }
    }
}

