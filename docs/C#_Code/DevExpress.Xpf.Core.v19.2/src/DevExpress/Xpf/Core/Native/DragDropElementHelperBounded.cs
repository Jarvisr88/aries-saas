namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public class DragDropElementHelperBounded : DragDropElementHelper
    {
        public DragDropElementHelperBounded(ISupportDragDrop supportDragDrop, bool isRelativeMode = true);
        public DragDropElementHelperBounded(ISupportDragDropColumnHeader supportDragDrop, bool isRelativeMode = true);
        private void OnMouseLeave(object sender, MouseEventArgs e);
        protected override void SubscribeEvents();
        protected override void UnsubscribeEvents();
    }
}

