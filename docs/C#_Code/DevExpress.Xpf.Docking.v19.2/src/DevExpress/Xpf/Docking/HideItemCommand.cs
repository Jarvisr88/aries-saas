namespace DevExpress.Xpf.Docking
{
    using System;

    public class HideItemCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization && ((items != null) && (items.Length != 0));

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                controller.Hide(item);
            }
        }
    }
}

