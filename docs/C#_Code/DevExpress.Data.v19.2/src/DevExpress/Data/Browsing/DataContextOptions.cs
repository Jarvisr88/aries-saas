namespace DevExpress.Data.Browsing
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DataContextOptions
    {
        private bool showInnerLists;
        private bool useCalculatedFields;
        public bool ShowInnerLists { get; }
        public bool UseCalculatedFields { get; }
        public DataContextOptions(bool showInnerLists, bool useCalculatedFields);
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

