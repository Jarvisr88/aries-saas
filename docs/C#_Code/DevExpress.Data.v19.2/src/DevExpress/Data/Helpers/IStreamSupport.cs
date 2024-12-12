namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.IO;
    using System;

    public interface IStreamSupport
    {
        void Save(TypedBinaryWriter writer);
    }
}

