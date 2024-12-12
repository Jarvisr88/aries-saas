namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DocumentBandInfo
    {
        public DocumentBandInfo(int[] path, object groupKey, bool isGroupItem);

        public bool IsGroupItem { get; private set; }

        public int[] Path { get; private set; }

        public object GroupKey { get; private set; }
    }
}

