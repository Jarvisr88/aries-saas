namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;

    public class VisibilityHiddenCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                item.Visibility = Visibility.Hidden;
            }
        }
    }
}

