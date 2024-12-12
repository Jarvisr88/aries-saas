namespace DevExpress.Office
{
    using System;
    using System.IO;

    public interface ISupportsBinaryReadWrite
    {
        void Read(BinaryReader reader);
        void Write(BinaryWriter writer);
    }
}

