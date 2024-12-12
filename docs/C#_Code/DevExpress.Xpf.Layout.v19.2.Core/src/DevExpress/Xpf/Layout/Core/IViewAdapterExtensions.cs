namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class IViewAdapterExtensions
    {
        public static bool GetIsDragging(this IViewAdapter adapter) => 
            (adapter != null) && ((adapter.DragService != null) && ((adapter.DragService.DragItem != null) && (adapter.DragService.OperationType != OperationType.Regular)));
    }
}

