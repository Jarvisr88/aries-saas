namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_ButtonsPanel", Type=typeof(ButtonsPanel)), TemplatePart(Name="PART_LayoutTreeView", Type=typeof(LayoutTreeView)), TemplatePart(Name="PART_HiddenItemsPanel", Type=typeof(HiddenItemsPanel)), TemplatePart(Name="PART_OptionsPanel", Type=typeof(OptionsPanel))]
    public class CustomizationControl : psvControl, IControlHost, IUIElement
    {
        private UIChildren uiChildren = new UIChildren();

        static CustomizationControl()
        {
            new DependencyPropertyRegistrator<CustomizationControl>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public FrameworkElement[] GetChildren() => 
            new FrameworkElement[] { this.PartButtonsPanel, this.PartLayoutTreeView, this.PartHiddenItemsPanel, this.PartOptionsPanel };

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartButtonsPanel = base.GetTemplateChild("PART_ButtonsPanel") as ButtonsPanel;
            this.PartLayoutTreeView = base.GetTemplateChild("PART_LayoutTreeView") as LayoutTreeView;
            this.PartHiddenItemsPanel = base.GetTemplateChild("PART_HiddenItemsPanel") as HiddenItemsPanel;
            this.PartOptionsPanel = base.GetTemplateChild("PART_OptionsPanel") as OptionsPanel;
            this.PartTabItemLayoutTree = base.GetTemplateChild("PART_TabItemLayoutTree") as DXTabItem;
            this.PartTabItemHiddenItems = base.GetTemplateChild("PART_TabItemHiddenItems") as DXTabItem;
            if (this.PartTabItemLayoutTree != null)
            {
                this.PartTabItemLayoutTree.Header = DockingLocalizer.GetString(DockingStringId.TitleLayoutTreeView);
            }
            if (this.PartTabItemHiddenItems != null)
            {
                this.PartTabItemHiddenItems.Header = DockingLocalizer.GetString(DockingStringId.TitleHiddenItemsList);
            }
            base.Focusable = false;
        }

        protected override void OnDispose()
        {
            if (this.PartLayoutTreeView != null)
            {
                this.PartLayoutTreeView.Dispose();
                this.PartLayoutTreeView = null;
            }
            base.OnDispose();
        }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetDockLayoutManager(this);

        UIChildren IUIElement.Children
        {
            get
            {
                this.uiChildren ??= new UIChildren();
                return this.uiChildren;
            }
        }

        protected ButtonsPanel PartButtonsPanel { get; private set; }

        protected LayoutTreeView PartLayoutTreeView { get; private set; }

        protected HiddenItemsPanel PartHiddenItemsPanel { get; private set; }

        protected OptionsPanel PartOptionsPanel { get; private set; }

        protected DXTabItem PartTabItemLayoutTree { get; private set; }

        protected DXTabItem PartTabItemHiddenItems { get; private set; }
    }
}

