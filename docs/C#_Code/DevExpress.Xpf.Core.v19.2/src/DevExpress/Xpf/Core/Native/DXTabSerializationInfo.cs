namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DXTabSerializationInfo
    {
        public void Apply();
        public void Assign(DXTabItem tabItem);
        public static DXTabSerializationInfo Create(DXTabItem tabItem, int index);
        public void Unassign();

        [XtraSerializableProperty]
        public string Name { get; set; }

        [XtraSerializableProperty]
        public string Header { get; set; }

        [XtraSerializableProperty]
        public string Content { get; set; }

        [XtraSerializableProperty]
        public bool IsNew { get; set; }

        [XtraSerializableProperty]
        public System.Windows.Visibility Visibility { get; set; }

        [XtraSerializableProperty]
        public int Index { get; set; }

        [XtraSerializableProperty]
        public TabPinMode PinMode { get; set; }

        internal DXTabItem Tab { get; private set; }
    }
}

