namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;

    internal interface IOfficeArtProperty
    {
        void Write(BinaryWriter writer);

        int Size { get; }

        bool Complex { get; }

        byte[] ComplexData { get; }
    }
}

