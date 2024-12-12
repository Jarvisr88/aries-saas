namespace DevExpress.Xpf.Docking
{
    using System;

    public class CloseAllButThisCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            !item.IsClosed && ((item.Parent != null) && (item.Parent.Items.Count > 1));

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            controller.CloseAllButThis(item);
        }
    }
}

