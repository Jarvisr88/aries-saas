namespace DevExpress.Xpf.Docking
{
    using System;

    public class MoveToNextDocumentGroupCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            item.Parent is DocumentGroup;

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            controller.MoveToDocumentGroup(item as LayoutPanel, true);
        }
    }
}

