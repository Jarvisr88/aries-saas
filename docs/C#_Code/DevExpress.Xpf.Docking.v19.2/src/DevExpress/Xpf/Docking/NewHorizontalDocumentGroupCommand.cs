namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows.Controls;

    public class NewHorizontalDocumentGroupCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            (item.Parent is DocumentGroup) && (item.Parent.Items.Count > 1);

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            controller.CreateNewDocumentGroup(item as LayoutPanel, Orientation.Horizontal);
        }
    }
}

