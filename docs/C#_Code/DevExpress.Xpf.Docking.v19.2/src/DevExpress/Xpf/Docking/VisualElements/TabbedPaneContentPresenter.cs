namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_ItemsControl", Type=typeof(psvItemsControl))]
    public class TabbedPaneContentPresenter : BasePanePresenter<TabbedPane, TabbedGroup>
    {
        protected override bool CanSelectTemplate(TabbedGroup group) => 
            group.ActualGroupTemplateSelector != null;

        protected override TabbedGroup ConvertToLogicalItem(object content) => 
            (LayoutItemData.ConvertToBaseLayoutItem(content) as TabbedGroup) ?? base.ConvertToLogicalItem(content);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartItemsContainer = base.GetTemplateChild("PART_ItemsControl") as psvItemsControl;
        }

        protected override void OnDispose()
        {
            if (this.PartItemsContainer != null)
            {
                this.PartItemsContainer.Dispose();
                this.PartItemsContainer = null;
            }
            base.OnDispose();
        }

        protected override DataTemplate SelectTemplateCore(TabbedGroup group) => 
            group.ActualGroupTemplateSelector.SelectTemplate(group, this);

        public psvItemsControl PartItemsContainer { get; private set; }
    }
}

