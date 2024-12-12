namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Linq;

    public class RestoreItemCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization && items.Any<BaseLayoutItem>(x => (controller.GetRestoreRoot(x) != null));

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                controller.Restore(item);
            }
        }
    }
}

