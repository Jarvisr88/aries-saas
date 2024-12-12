namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using System;

    public interface ISerializationCacheProvider
    {
        void AddBrick(object obj, XtraItemEventArgs e);
        void AddImage(object obj, XtraItemEventArgs e);
        void AddStyle(object obj, XtraItemEventArgs e);
    }
}

