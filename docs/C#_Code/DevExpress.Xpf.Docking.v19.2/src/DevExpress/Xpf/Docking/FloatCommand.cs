namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class FloatCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            !item.IsFloating && item.AllowFloat;

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            DockLayoutManager container = controller.Container;
            IView view = container.GetView(item.GetRoot());
            ILayoutElement viewElement = container.GetViewElement(item);
            if (container != null)
            {
                container.ViewAdapter.ContextActionService.Execute(view, viewElement, ContextAction.Float);
            }
            else
            {
                controller.Float(item);
            }
        }
    }
}

