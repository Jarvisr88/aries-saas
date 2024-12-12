namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Utils.DocumentStoring;
    using System;
    using System.Runtime.CompilerServices;

    internal class StoredIDProvider : IStoredIDProvider
    {
        private volatile int lastID;

        public StoredIDProvider();
        public void SetLastID(int id);
        public StoredID SetNextID(IStorableObject obj);

        public int LastID { get; protected set; }
    }
}

