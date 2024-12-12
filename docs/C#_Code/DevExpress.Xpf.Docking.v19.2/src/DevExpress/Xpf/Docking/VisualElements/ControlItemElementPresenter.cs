namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;

    public abstract class ControlItemElementPresenter : BasePanePresenter<ControlItem, LayoutControlItem>
    {
        protected ControlItemElementPresenter()
        {
        }

        protected override LayoutControlItem ConvertToLogicalItem(object content) => 
            (LayoutItemData.ConvertToBaseLayoutItem(content) as LayoutControlItem) ?? base.ConvertToLogicalItem(content);
    }
}

