namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public interface IOfficeDrawingProperty
    {
        void Execute(OfficeArtPropertiesBase owner);
        void Merge(IOfficeDrawingProperty other);
        void Read(BinaryReader reader);
        void Write(BinaryWriter writer);

        short Id { get; }

        int Size { get; }

        bool Complex { get; }

        byte[] ComplexData { get; }
    }
}

