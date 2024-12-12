namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using System;

    public class ExpandCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item)
        {
            LayoutGroup group = item as LayoutGroup;
            return ((group != null) && group.AllowExpand);
        }

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            LayoutGroup group = item as LayoutGroup;
            if (group != null)
            {
                group.Expanded = !group.Expanded;
                LayoutView view = controller.Container.GetView(group) as LayoutView;
                if (view != null)
                {
                    view.AdornerHelper.InvalidateSelectionAdorner();
                }
            }
        }
    }
}

