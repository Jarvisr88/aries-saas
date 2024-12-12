namespace DevExpress.Xpf.Docking
{
    using System;

    public class ArrangeIconsCommand : MDIControllerCommand
    {
        protected override bool CanExecuteCore(IMDIController controller, BaseLayoutItem[] items) => 
            items.Length != 0;

        protected override void ExecuteCore(IMDIController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                controller.ArrangeIcons(item);
            }
        }
    }
}

