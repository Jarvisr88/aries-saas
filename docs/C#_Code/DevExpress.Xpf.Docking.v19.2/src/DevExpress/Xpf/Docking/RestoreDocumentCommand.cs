namespace DevExpress.Xpf.Docking
{
    using System;

    public class RestoreDocumentCommand : MDIControllerCommand
    {
        protected override bool CanExecuteCore(IMDIController controller, BaseLayoutItem[] items) => 
            (items.Length != 1) ? (items.Length != 0) : items[0].AllowRestore;

        protected override void ExecuteCore(IMDIController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                controller.Restore(item as DocumentPanel);
            }
        }
    }
}

