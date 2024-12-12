namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;

    public class PdfXRef
    {
        private ArrayList entiers = new ArrayList();
        private long byteOffset = -1L;

        public PdfXRef()
        {
            this.entiers.Add(new PdfXRefEntryFree());
        }

        private void CalcByteOffset(StreamWriter writer)
        {
            writer.Flush();
            this.byteOffset = writer.BaseStream.Position;
        }

        public void Clear()
        {
            this.entiers.Clear();
        }

        private void RegisterIndirectReference(PdfIndirectReference indirectReference)
        {
            if (indirectReference == null)
            {
                throw new PdfException("Can't register direct object");
            }
            indirectReference.Number ??= this.entiers.Add(new PdfXRefEntry(indirectReference));
        }

        public void RegisterObject(PdfObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentException();
            }
            this.RegisterIndirectReference(obj.IndirectReference);
        }

        public void Write(StreamWriter writer)
        {
            this.CalcByteOffset(writer);
            writer.WriteLine("xref");
            writer.Write("0 " + Convert.ToString(this.Count));
            for (int i = 0; i < this.Count; i++)
            {
                writer.WriteLine();
                this[i].Write(writer);
            }
            writer.WriteLine();
        }

        public int Count =>
            this.entiers.Count;

        public PdfXRefEntryBase this[int index] =>
            (PdfXRefEntryBase) this.entiers[index];

        public long ByteOffset =>
            this.byteOffset;
    }
}

