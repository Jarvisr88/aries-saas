namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;

    public class PdfArray : PdfObject, IEnumerable
    {
        private ArrayList array;
        private int maxRowCount;

        public PdfArray()
        {
            this.array = new ArrayList();
            this.maxRowCount = -1;
        }

        public PdfArray(PdfObjectType type) : base(type)
        {
            this.array = new ArrayList();
            this.maxRowCount = -1;
        }

        public void Add(PdfObject obj)
        {
            if ((obj != null) && (this.IndexOf(obj) < 0))
            {
                this.array.Add(obj);
                obj.Owner = this;
            }
        }

        public void Add(bool value)
        {
            this.Add(new PdfBoolean(value));
        }

        public void Add(int number)
        {
            this.Add(new PdfNumber(number));
        }

        public void Add(float value)
        {
            this.Add(new PdfDouble((double) value));
        }

        public void Add(string name)
        {
            this.Add(new PdfName(name));
        }

        public void AddRange(int[] numbers)
        {
            foreach (int num2 in numbers)
            {
                this.Add(num2);
            }
        }

        public void Clear()
        {
            this.array.Clear();
        }

        public int IndexOf(PdfObject obj)
        {
            if (obj != null)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (ReferenceEquals(this[i], obj))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.array.GetEnumerator();

        protected override void WriteContent(StreamWriter writer)
        {
            writer.Write("[");
            for (int i = 0; i < this.Count; i++)
            {
                this[i].WriteToStream(writer);
                if ((this.maxRowCount > 0) && (((i + 1) % this.maxRowCount) == 0))
                {
                    writer.WriteLine();
                }
                if (i < (this.Count - 1))
                {
                    writer.Write(" ");
                }
            }
            writer.Write("]");
        }

        public int MaxRowCount
        {
            get => 
                this.maxRowCount;
            set => 
                this.maxRowCount = value;
        }

        public int Count =>
            this.array.Count;

        public PdfObject this[int index] =>
            this.array[index] as PdfObject;
    }
}

