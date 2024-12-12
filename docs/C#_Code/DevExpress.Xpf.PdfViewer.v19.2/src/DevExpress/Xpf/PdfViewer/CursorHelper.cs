namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Resources;

    public static class CursorHelper
    {
        private static readonly bool CanUseDPIAwareCtor;

        static CursorHelper()
        {
            Type[] types = new Type[] { typeof(string), typeof(bool) };
            CanUseDPIAwareCtor = typeof(Cursor).GetConstructor(types) != null;
            HandCursor = LoadCursor("Images/Cursors/CursorHand.cur");
            HandDragCursor = LoadCursor("Images/Cursors/CursorHandDrag.cur");
            ZoomInCursor = LoadCursor("Images/Cursors/CursorZoomIn.cur");
            ZoomOutCursor = LoadCursor("Images/Cursors/CursorZoomOut.cur");
            ZoomLimitCursor = LoadCursor("Images/Cursors/CursorZoomLimit.cur");
            CrossCursor = LoadCursor("Images/Cursors/CursorCross.cur");
            TextMarkupCursor = LoadCursor("Images/Cursors/CursorTextMarkup.cur");
            ContextCursor = LoadCursor("Images/Cursors/CursorContext.cur");
        }

        private static Stream GetCursorStream(Uri uri)
        {
            StreamResourceInfo resourceStream = Application.GetResourceStream(uri);
            if (resourceStream != null)
            {
                return resourceStream.Stream;
            }
            StreamResourceInfo local1 = resourceStream;
            return null;
        }

        private static string GetUriString(string cursorFilePath, string rootNamespace) => 
            $"pack://application:,,,/{rootNamespace}{".v19.2"};component/{cursorFilePath}";

        private static Cursor LoadCursor(string filename)
        {
            Cursor arrow;
            try
            {
                Uri uri = new Uri(GetUriString(filename, "DevExpress.Xpf.PdfViewer"), UriKind.RelativeOrAbsolute);
                if (!CanUseDPIAwareCtor)
                {
                    arrow = new Cursor(GetCursorStream(uri));
                }
                else
                {
                    object[] args = new object[] { GetCursorStream(uri), true };
                    arrow = (Cursor) Activator.CreateInstance(typeof(Cursor), args);
                }
            }
            catch
            {
                arrow = Cursors.Arrow;
            }
            return arrow;
        }

        public static void SetCursor(FrameworkElement element, PdfCursors cursor)
        {
            switch (cursor)
            {
                case PdfCursors.HandCursor:
                    SetCursor(element, HandCursor);
                    return;

                case PdfCursors.HandDragCursor:
                    SetCursor(element, HandDragCursor);
                    return;

                case PdfCursors.ZoomInCursor:
                    SetCursor(element, ZoomInCursor);
                    return;

                case PdfCursors.ZoomOutCursor:
                    SetCursor(element, ZoomOutCursor);
                    return;

                case PdfCursors.ZoomLimitCursor:
                    SetCursor(element, ZoomLimitCursor);
                    return;

                case PdfCursors.CrossCursor:
                    SetCursor(element, CrossCursor);
                    return;

                case PdfCursors.TextMarkupCursor:
                    SetCursor(element, TextMarkupCursor);
                    return;

                case PdfCursors.ContextCursor:
                    SetCursor(element, ContextCursor);
                    return;
            }
            throw new ArgumentOutOfRangeException("cursor");
        }

        public static void SetCursor(FrameworkElement element, Cursor cursor)
        {
            element.Do<FrameworkElement>(x => x.Cursor = cursor);
        }

        private static Cursor HandCursor { get; set; }

        private static Cursor HandDragCursor { get; set; }

        private static Cursor ZoomInCursor { get; set; }

        private static Cursor ZoomOutCursor { get; set; }

        private static Cursor ZoomLimitCursor { get; set; }

        private static Cursor CrossCursor { get; set; }

        private static Cursor TextMarkupCursor { get; set; }

        private static Cursor ContextCursor { get; set; }
    }
}

