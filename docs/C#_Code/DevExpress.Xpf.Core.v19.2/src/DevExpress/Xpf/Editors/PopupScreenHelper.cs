namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System.Windows;

    public class PopupScreenHelper : ScreenHelper
    {
        public static Point GetPopupScreenPoint(PopupBaseEdit edit)
        {
            if (!edit.IsInVisualTree())
            {
                return new Point();
            }
            if (edit.PopupSettings.PopupResizingStrategy.IsRTL)
            {
                return GetScaledPoint(edit.PointToScreen(new Point(edit.ActualWidth, 0.0)));
            }
            Point point = new Point();
            return GetScaledPoint(edit.PointToScreen(point));
        }
    }
}

