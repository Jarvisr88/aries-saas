namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    public static class IBoundPropertyExtension
    {
        private const char ColumnSeparateChar = '.';

        public static string GetFullDisplayName(this IBoundProperty self);
        public static string GetFullDisplayNameWithLists(this IBoundProperty self);
        public static string GetFullName(this IBoundProperty self);
        public static string GetFullNameWithLists(this IBoundProperty self);
        public static int GetLevel(this IBoundProperty self);
    }
}

