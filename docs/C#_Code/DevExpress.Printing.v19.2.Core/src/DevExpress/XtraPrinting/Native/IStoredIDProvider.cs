namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Utils.DocumentStoring;
    using System;

    internal interface IStoredIDProvider
    {
        void SetLastID(int id);
        StoredID SetNextID(IStorableObject obj);

        int LastID { get; }
    }
}

