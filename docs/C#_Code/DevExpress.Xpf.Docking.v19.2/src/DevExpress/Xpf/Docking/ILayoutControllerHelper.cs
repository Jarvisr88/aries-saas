namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class ILayoutControllerHelper
    {
        internal static LayoutGroup GetRestoreRoot(this ILayoutController controller, BaseLayoutItem item)
        {
            if ((item == null) || (!item.IsHidden || !item.AllowRestore))
            {
                return null;
            }
            LayoutGroup parent = item.Parent;
            LayoutGroup layoutRoot = parent;
            if (parent == null)
            {
                LayoutGroup local1 = parent;
                if (!controller.Container.LayoutRoot.IsLayoutRoot)
                {
                    return null;
                }
                layoutRoot = controller.Container.LayoutRoot;
            }
            return layoutRoot;
        }
    }
}

