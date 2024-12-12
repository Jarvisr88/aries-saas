namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public static class CursorHelper
    {
        static CursorHelper()
        {
            try
            {
                HandCursor = LoadCursor("Images/Cursors/CursorHand.cur", 11, 13);
            }
            catch (Exception)
            {
            }
            try
            {
                HandDragCursor = LoadCursor("Images/Cursors/CursorHandDrag.cur", 11, 13);
            }
            catch (Exception)
            {
            }
            try
            {
                CrossCursor = LoadCursor("Images/Cursors/CursorCross.cur", 11, 11);
            }
            catch (Exception)
            {
            }
        }

        private static string GetUriString(string cursorFilePath, string rootNamespace) => 
            $"pack://application:,,,/{rootNamespace}{".v19.2"};component/{cursorFilePath}";

        private static Cursor LoadCursor(string filename, byte hotspotx, byte hotspoty) => 
            new Cursor(UpdateCursorHotspot(new Uri(GetUriString(filename, "DevExpress.Xpf.Printing"), UriKind.RelativeOrAbsolute), hotspotx, hotspoty));

        public static void SetCursor(FrameworkElement element, PreviewCursors cursor)
        {
            switch (cursor)
            {
                case PreviewCursors.Hand:
                    HandCursor.Do<Cursor>(x => SetCursor(element, x));
                    return;

                case PreviewCursors.HandDrag:
                    HandDragCursor.Do<Cursor>(x => SetCursor(element, x));
                    return;

                case PreviewCursors.Cross:
                    CrossCursor.Do<Cursor>(x => SetCursor(element, x));
                    return;
            }
            throw new ArgumentOutOfRangeException("cursor");
        }

        public static void SetCursor(FrameworkElement element, Cursor cursor)
        {
            element.Do<FrameworkElement>(x => x.Cursor = cursor);
        }

        private static Stream UpdateCursorHotspot(Uri uri, byte hotspotx, byte hotspoty)
        {
            Stream stream = Application.GetResourceStream(uri).Stream;
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int) stream.Length);
            MemoryStream stream2 = new MemoryStream();
            buffer[10] = hotspotx;
            buffer[12] = hotspoty;
            stream2.Write(buffer, 0, (int) stream.Length);
            stream2.Position = 0L;
            return stream2;
        }

        private static Cursor HandCursor { get; set; }

        private static Cursor HandDragCursor { get; set; }

        private static Cursor CrossCursor { get; set; }
    }
}

