namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ListIndexKey
    {
        private readonly int listIndex;
        private readonly string fieldName;
        public int ListIndex =>
            this.listIndex;
        public string FieldName =>
            this.fieldName;
        public ListIndexKey(int listIndex, string fieldName)
        {
            this.listIndex = listIndex;
            this.fieldName = fieldName;
        }

        public override int GetHashCode() => 
            (((0x11 * 0x17) + this.ListIndex.GetHashCode()) * 0x17) + this.FieldName.GetHashCode();
    }
}

