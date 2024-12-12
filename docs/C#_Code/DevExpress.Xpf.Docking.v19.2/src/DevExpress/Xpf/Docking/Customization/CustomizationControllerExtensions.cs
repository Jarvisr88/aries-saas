namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;

    internal static class CustomizationControllerExtensions
    {
        public static void ClearSelection(this ICustomizationController controller)
        {
            if (controller != null)
            {
                LayoutGroup customizationRoot = controller.CustomizationRoot;
                DockLayoutManager container = controller.Container;
                if ((customizationRoot != null) && (container != null))
                {
                    IView view = container.GetView(customizationRoot);
                    container.ViewAdapter.SelectionService.ClearSelection(view);
                }
            }
        }
    }
}

