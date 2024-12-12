namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal static class FilteringUINodeBaseExtensions
    {
        public static string GetDisplayText(this NodeBase<NodeValueInfo> node) => 
            node?.Value.GetDisplayText();

        public static bool UseCustomTemplate(this NodeBase<NodeValueInfo> node) => 
            (node != null) ? (node.Value.DisplayMode == DisplayMode.DisplayText) : false;
    }
}

