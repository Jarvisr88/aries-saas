namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class RuntimeCreateNewBarCustomization : RuntimeCollectionCustomization
    {
        private const string namePrefix = "guid167C22D3B3E749C9A0435237A138E3A5";
        private static int index;

        public RuntimeCreateNewBarCustomization();
        public RuntimeCreateNewBarCustomization(string caption);
        protected override bool ApplyOverride(bool silent);
        protected override bool UndoOverride();

        [XtraSerializableProperty]
        public string Caption { get; set; }
    }
}

