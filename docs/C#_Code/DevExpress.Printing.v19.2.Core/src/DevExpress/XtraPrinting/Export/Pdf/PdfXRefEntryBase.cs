namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public abstract class PdfXRefEntryBase
    {
        protected PdfXRefEntryBase()
        {
        }

        public void Write(StreamWriter writer)
        {
            string[] textArray1 = new string[] { this.ByteOffset.ToString("D10"), " ", this.Generation.ToString("D5"), " ", this.TypeString };
            writer.Write(string.Concat(textArray1));
        }

        protected abstract string TypeString { get; }

        protected abstract long ByteOffset { get; }

        protected abstract int Generation { get; }
    }
}

