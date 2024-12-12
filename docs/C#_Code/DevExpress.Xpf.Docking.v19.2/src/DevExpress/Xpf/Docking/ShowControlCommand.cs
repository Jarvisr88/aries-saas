namespace DevExpress.Xpf.Docking
{
    using System;

    public class ShowControlCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                LayoutControlItem item2 = item as LayoutControlItem;
                if (item2 != null)
                {
                    item2.ShowControl = !item2.ShowControl;
                }
            }
            controller.Container.Update();
        }
    }
}

