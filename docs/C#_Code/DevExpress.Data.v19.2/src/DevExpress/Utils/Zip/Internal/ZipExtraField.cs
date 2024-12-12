namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;

    public abstract class ZipExtraField : IZipExtraField
    {
        protected ZipExtraField()
        {
        }

        public abstract void Apply(InternalZipFile zipFile);
        public abstract void AssignRawData(BinaryReader reader);
        public abstract void Write(BinaryWriter writer);

        public abstract short Id { get; }

        public abstract ExtraFieldType Type { get; }

        public abstract short ContentSize { get; }
    }
}

