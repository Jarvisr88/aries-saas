namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.IO;

    public interface IZipExtraFieldCollection
    {
        void Add(IZipExtraField field);
        short CalculateSize(ExtraFieldType fieldType);
        void Write(BinaryWriter writer, ExtraFieldType fieldType);
    }
}

