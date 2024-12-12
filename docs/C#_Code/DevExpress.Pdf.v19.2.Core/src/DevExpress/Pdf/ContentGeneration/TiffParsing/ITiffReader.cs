namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;

    public interface ITiffReader
    {
        byte ReadByte();
        byte[] ReadBytes(int count);
        short ReadInt16();
        int ReadInt32();

        long Position { get; set; }
    }
}

