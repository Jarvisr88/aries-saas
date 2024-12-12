namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Utils.DocumentStoring;
    using System;
    using System.Runtime.CompilerServices;

    internal class FakeBrickOwner : NullBrickOwner
    {
        public FakeBrickOwner(StoredID id, string name);

        public string Name { get; private set; }

        public StoredID Id { get; private set; }
    }
}

