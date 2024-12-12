namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class OfficeCursors
    {
        private static float defaultHighDpiFactor = (DocumentModelDpi.Dpi / ((float) DefaultDeviceDpi));
        private static readonly Dictionary<OfficeCursorType, string> cursorNameTable;
        private static readonly ConcurrentDictionary<CachedOfficeCursorInfo, Cursor> cursorsTable;
        private const string cursorsPath = "DevExpress.Office.Cursors.";
        private const string pathCursorSize32 = "Cursors32.";
        private const string pathCursorSize48 = "Cursors48.";
        private const string pathCursorSize64 = "Cursors64.";
        private const string pathCursorSize96 = "Cursors96.";
        private const string pathCursorSize128 = "Cursors128.";
        private static Lazy<Cursor> iBeamItalic;
        private static Lazy<Cursor> beginRotate;
        private static Lazy<Cursor> rotate;
        private static Lazy<Cursor> selectColumn;
        private static Lazy<Cursor> resizeColumn;
        private static Lazy<Cursor> resizeRow;

        static OfficeCursors()
        {
            Dictionary<OfficeCursorType, string> dictionary1 = new Dictionary<OfficeCursorType, string>();
            dictionary1.Add(OfficeCursorType.IBeamItalic, "IBeamItalic.cur");
            dictionary1.Add(OfficeCursorType.BeginRotate, "BeginRotate.cur");
            dictionary1.Add(OfficeCursorType.Rotate, "Rotate.cur");
            dictionary1.Add(OfficeCursorType.SelectColumn, "SelectColumn.cur");
            dictionary1.Add(OfficeCursorType.ResizeColumn, "ResizeColumn.cur");
            dictionary1.Add(OfficeCursorType.ResizeRow, "ResizeRow.cur");
            cursorNameTable = dictionary1;
            cursorsTable = new ConcurrentDictionary<CachedOfficeCursorInfo, Cursor>();
            iBeamItalic = new Lazy<Cursor>(() => LoadCursor("IBeamItalic.cur"));
            beginRotate = new Lazy<Cursor>(() => LoadCursor("BeginRotate.cur"));
            rotate = new Lazy<Cursor>(() => LoadCursor("Rotate.cur"));
            selectColumn = new Lazy<Cursor>(() => LoadCursor("SelectColumn.cur"));
            resizeColumn = new Lazy<Cursor>(() => LoadCursor("ResizeColumn.cur"));
            resizeRow = new Lazy<Cursor>(() => LoadCursor("ResizeRow.cur"));
        }

        public static string GetPathByHighDpiFactor() => 
            GetPathByHighDpiFactorCore(defaultHighDpiFactor);

        private static string GetPathByHighDpiFactor(int systemDpi, int deviceDpi) => 
            GetPathByHighDpiFactorCore(((float) systemDpi) / ((float) deviceDpi));

        private static string GetPathByHighDpiFactorCore(float highDpiFactor) => 
            (highDpiFactor >= 1.5) ? (((highDpiFactor < 1.5) || (highDpiFactor >= 2f)) ? (((highDpiFactor < 2f) || (highDpiFactor >= 3f)) ? (((highDpiFactor < 3f) || (highDpiFactor >= 4f)) ? "Cursors128." : "Cursors96.") : "Cursors64.") : "Cursors48.") : "Cursors32.";

        public static Cursor LoadCursor(OfficeCursorType cursorType) => 
            LoadCursor(cursorType, DefaultDeviceDpi, DefaultDeviceDpi);

        private static Cursor LoadCursor(string resourceName) => 
            ResourceImageHelper.CreateCursorFromResources("DevExpress.Office.Cursors." + GetPathByHighDpiFactor() + resourceName, Assembly.GetExecutingAssembly());

        public static Cursor LoadCursor(OfficeCursorType cursorType, int systemDpi, int deviceDpi)
        {
            Cursor cursor;
            CachedOfficeCursorInfo key = new CachedOfficeCursorInfo(cursorType, deviceDpi);
            if (!cursorsTable.TryGetValue(key, out cursor))
            {
                cursor = LoadCursor(cursorNameTable[cursorType], systemDpi, deviceDpi);
                cursorsTable.TryAdd(key, cursor);
            }
            return cursor;
        }

        private static Cursor LoadCursor(string resourceName, int systemDpi, int deviceDpi) => 
            ResourceImageHelper.CreateCursorFromResources("DevExpress.Office.Cursors." + GetPathByHighDpiFactor(systemDpi, deviceDpi) + resourceName, Assembly.GetExecutingAssembly());

        public static int DefaultDeviceDpi { get; }

        public static Cursor IBeamItalic =>
            iBeamItalic.Value;

        public static Cursor BeginRotate =>
            beginRotate.Value;

        public static Cursor Rotate =>
            rotate.Value;

        public static Cursor SelectColumn =>
            selectColumn.Value;

        public static Cursor ResizeColumn =>
            resizeColumn.Value;

        public static Cursor ResizeRow =>
            resizeRow.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OfficeCursors.<>c <>9 = new OfficeCursors.<>c();

            internal Cursor <.cctor>b__38_0() => 
                OfficeCursors.LoadCursor("IBeamItalic.cur");

            internal Cursor <.cctor>b__38_1() => 
                OfficeCursors.LoadCursor("BeginRotate.cur");

            internal Cursor <.cctor>b__38_2() => 
                OfficeCursors.LoadCursor("Rotate.cur");

            internal Cursor <.cctor>b__38_3() => 
                OfficeCursors.LoadCursor("SelectColumn.cur");

            internal Cursor <.cctor>b__38_4() => 
                OfficeCursors.LoadCursor("ResizeColumn.cur");

            internal Cursor <.cctor>b__38_5() => 
                OfficeCursors.LoadCursor("ResizeRow.cur");
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CachedOfficeCursorInfo : IEquatable<OfficeCursors.CachedOfficeCursorInfo>
        {
            public CachedOfficeCursorInfo(OfficeCursorType cursorType, int deviceDpi)
            {
                this.<CursorType>k__BackingField = cursorType;
                this.<DeviceDpi>k__BackingField = deviceDpi;
            }

            public OfficeCursorType CursorType { get; }
            public int DeviceDpi { get; }
            public override bool Equals(object obj)
            {
                if (!(obj is OfficeCursors.CachedOfficeCursorInfo))
                {
                    return false;
                }
                OfficeCursors.CachedOfficeCursorInfo other = (OfficeCursors.CachedOfficeCursorInfo) obj;
                return this.Equals(other);
            }

            public bool Equals(OfficeCursors.CachedOfficeCursorInfo other) => 
                (this.CursorType == other.CursorType) && (this.DeviceDpi == other.DeviceDpi);

            public override int GetHashCode() => 
                HashCodeHelper.CalculateGeneric<OfficeCursorType, int>(this.CursorType, this.DeviceDpi);
        }
    }
}

