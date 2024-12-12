namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Windows;

    public class AddDocumentPreviewToolBarAction : AddBarAction
    {
        protected override void ExecuteCore(DependencyObject context)
        {
            DocumentPreviewBarManagerController controller = TreeHelper.GetParent<DocumentPreviewBarManagerController>(this, null, true, true);
            if (controller != null)
            {
                base.Bar.DockInfo.ContainerName = controller.ToolBarContainerName;
            }
            base.ExecuteCore(context);
        }
    }
}

