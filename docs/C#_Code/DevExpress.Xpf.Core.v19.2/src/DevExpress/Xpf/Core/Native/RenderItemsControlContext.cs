namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class RenderItemsControlContext : FrameworkRenderElementContext
    {
        private IList itemsSource;
        private RenderPanel itemPanelTemplate;
        private RenderTemplate itemTemplate;

        public RenderItemsControlContext(RenderItemsControl factory);
        public override void AddChild(FrameworkRenderElementContext child);
        protected override FrameworkRenderElementContext GetRenderChild(int index);

        protected override int RenderChildrenCount { get; }

        public IList ItemsSource { get; set; }

        public RenderPanel ItemPanelTemplate { get; set; }

        public RenderTemplate ItemTemplate { get; set; }

        public RenderPanelContext ItemsHost { get; private set; }
    }
}

