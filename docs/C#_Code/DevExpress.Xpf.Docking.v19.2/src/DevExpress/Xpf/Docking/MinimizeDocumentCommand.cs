namespace DevExpress.Xpf.Docking
{
    using System;

    public class MinimizeDocumentCommand : MDIControllerCommand
    {
        protected override bool CanExecuteCore(IMDIController controller, BaseLayoutItem[] items) => 
            (items.Length != 1) ? (items.Length != 0) : items[0].AllowMinimize;

        protected override void ExecuteCore(IMDIController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                controller.Minimize(item as DocumentPanel);
            }
        }
    }
}

