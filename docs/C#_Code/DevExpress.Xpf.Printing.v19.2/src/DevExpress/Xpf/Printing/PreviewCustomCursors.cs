namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class PreviewCustomCursors
    {
        private static HandCustomCursor handCursor;
        private static HandDragCustomCursor handDragCursor;

        public static CustomCursor HandCursor
        {
            get
            {
                handCursor ??= new HandCustomCursor();
                return handCursor;
            }
        }

        public static CustomCursor HandDragCursor
        {
            get
            {
                handDragCursor ??= new HandDragCustomCursor();
                return handDragCursor;
            }
        }

        private class HandCustomCursor : CustomCursor
        {
            public override System.Windows.Media.ImageSource ImageSource =>
                LoadCursorFile("Images/Cursors/CursorHand.png", "DevExpress.Xpf.Printing");

            public override Point HotSpot =>
                new Point(16.0, 16.0);
        }

        private class HandDragCustomCursor : CustomCursor
        {
            public override System.Windows.Media.ImageSource ImageSource =>
                LoadCursorFile("Images/Cursors/CursorHandDrag.png", "DevExpress.Xpf.Printing");

            public override Point HotSpot =>
                new Point(16.0, 16.0);
        }
    }
}

