namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomCompleteDragResult
    {
        public CustomCompleteDragResult(bool isCustom, DragDropEffects dragDropEffects)
        {
            this.IsCustom = isCustom;
            this.Effects = dragDropEffects;
        }

        public bool IsCustom { get; private set; }

        public DragDropEffects Effects { get; private set; }
    }
}

