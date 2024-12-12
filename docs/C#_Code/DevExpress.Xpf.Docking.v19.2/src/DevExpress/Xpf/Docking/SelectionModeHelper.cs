namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;

    internal static class SelectionModeHelper
    {
        public static SelectionMode GetActualSelectionMode() => 
            !KeyHelper.IsCtrlPressed ? (!KeyHelper.IsShiftPressed ? SelectionMode.SingleItem : SelectionMode.ItemRange) : SelectionMode.MultipleItems;
    }
}

