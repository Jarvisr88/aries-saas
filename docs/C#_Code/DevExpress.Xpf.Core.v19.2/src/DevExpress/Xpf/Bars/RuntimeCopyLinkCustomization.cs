namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class RuntimeCopyLinkCustomization : RuntimeCollectionCustomization
    {
        private BarItemLink result;

        public RuntimeCopyLinkCustomization();
        public RuntimeCopyLinkCustomization(IBarItem forcedTarget);
        public RuntimeCopyLinkCustomization(IBarItem forcedTarget, ILinksHolder forcedHolder, BarItemLink insert, bool after);
        protected override bool ApplyOverride(bool silent);
        public BarItemLink CreateLinkClone();
        protected override bool UndoOverride();

        [XtraSerializableProperty]
        public string ResultName { get; set; }

        [XtraSerializableProperty]
        public string InsertAfter { get; set; }

        [XtraSerializableProperty]
        public string InsertBefore { get; set; }

        [XtraSerializableProperty]
        public int InsertIndex { get; set; }
    }
}

