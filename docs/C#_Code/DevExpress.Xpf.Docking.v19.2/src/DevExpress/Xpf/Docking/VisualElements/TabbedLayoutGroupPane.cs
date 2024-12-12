namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class TabbedLayoutGroupPane : LayoutTabControl
    {
        static TabbedLayoutGroupPane()
        {
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TabbedLayoutGroupPane>.New().OverrideDefaultStyleKey();
        }

        protected override psvSelectorItem CreateSelectorItem() => 
            new TabbedLayoutGroupItem();

        protected override IView GetView(DockLayoutManager container) => 
            null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            base.EnsureSelectedContent();
        }
    }
}

