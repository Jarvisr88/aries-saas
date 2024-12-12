namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class CustomizationView : LayoutView
    {
        public CustomizationView(IUIElement viewUIElement) : base(viewUIElement)
        {
        }

        protected override void RegisterListeners()
        {
            base.RegisterUIServiceListener(new LayoutViewSelectionListener());
            base.RegisterUIServiceListener(new CustomizationViewUIInteractionListener());
            base.RegisterUIServiceListener(new CustomizationViewClientDraggingListener());
        }

        protected override ILayoutElementFactory ResolveDefaultFactory() => 
            new CustomizationViewElementFactory();
    }
}

