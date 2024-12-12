namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;

    public interface IZipExtraField
    {
        void Apply(InternalZipFile zipFile);
        void AssignRawData(BinaryReader reader);
        void Write(BinaryWriter writer);

        ExtraFieldType Type { get; }

        short Id { get; }

        short ContentSize { get; }
    }
}

