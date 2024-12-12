namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class MouseHelper : IMouseHelper
    {
        private Size? cachedCursorSize;

        public Point GetAbsoluteMousePosition() => 
            DragControllerHelper.GetMousePositionOnScreen();

        public Size GetMouseCursorSize()
        {
            if (this.cachedCursorSize == null)
            {
                object[] parameters = new object[] { 0, 0, 0, 0 };
                typeof(Popup).GetMethod("GetMouseCursorSize", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance).Invoke(null, parameters);
                int num = (int) parameters[0];
                int num2 = (int) parameters[1];
                this.cachedCursorSize = new Size((double) Math.Max(0, num - ((int) parameters[2])), (double) Math.Max(0, num2 - ((int) parameters[3])));
            }
            return this.cachedCursorSize.Value;
        }
    }
}

