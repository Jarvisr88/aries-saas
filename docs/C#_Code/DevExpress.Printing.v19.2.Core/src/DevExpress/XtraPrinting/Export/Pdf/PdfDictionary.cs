namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.IO;

    public class PdfDictionary : PdfObject
    {
        private ArrayList list;

        public PdfDictionary()
        {
            this.list = new ArrayList();
        }

        public PdfDictionary(PdfObjectType type) : base(type)
        {
            this.list = new ArrayList();
        }

        public void Add(string name, PdfObject value)
        {
            this.list.Add(new Entry(name, value));
            value.Owner = this;
        }

        public void Add(string name, int value)
        {
            this.Add(name, new PdfNumber(value));
        }

        public void Add(string name, string value)
        {
            this.Add(name, new PdfName(value));
        }

        public void AddIfNotNull(string name, PdfObject value)
        {
            if (value != null)
            {
                this.Add(name, value);
            }
        }

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Write("<< ");
            if (this.list.Count > 0)
            {
                writer.WriteLine();
            }
            foreach (Entry entry in this.list)
            {
                entry.Name.WriteToStream(writer);
                writer.Write(" ");
                entry.Value.WriteToStream(writer);
                writer.WriteLine();
            }
            writer.Write(">>");
        }

        private class Entry
        {
            public readonly PdfName Name;
            public readonly PdfObject Value;

            public Entry(string name, PdfObject value)
            {
                this.Name = new PdfName(name);
                this.Value = value;
            }
        }
    }
}

