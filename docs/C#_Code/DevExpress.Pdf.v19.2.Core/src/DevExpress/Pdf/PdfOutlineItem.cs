namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfOutlineItem : PdfObject
    {
        private const string firstDictionaryKey = "First";
        private const string lastDictionaryKey = "Last";
        protected const string CountDictionaryKey = "Count";
        private bool closed;
        private PdfOutline first;
        private PdfOutline last;
        private int count;

        protected PdfOutlineItem()
        {
        }

        protected PdfOutlineItem(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            object obj2;
            if (dictionary.TryGetValue("First", out obj2))
            {
                PdfReaderDictionary dictionary2 = dictionary.Objects.TryResolve(obj2, null) as PdfReaderDictionary;
                if ((dictionary2 != null) && (dictionary2.Number != dictionary.Number))
                {
                    this.first = new PdfOutline(this, null, dictionary2);
                    this.last = this.first;
                    PdfReaderDictionary dictionary3 = dictionary2.GetDictionary("Next");
                    while (true)
                    {
                        if (dictionary3 == null)
                        {
                            int? integer = dictionary.GetInteger("Count");
                            this.count = (integer != null) ? integer.GetValueOrDefault() : 0;
                            this.closed = this.count <= 0;
                            this.UpdateCount();
                            break;
                        }
                        PdfOutline outline = new PdfOutline(this, this.last, dictionary3);
                        this.last.Next = outline;
                        this.last = outline;
                        dictionary3 = dictionary3.GetDictionary("Next");
                    }
                }
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            if (this.first == null)
            {
                dictionary.Add("First", this.first);
            }
            else
            {
                int num;
                PdfOutline next = this.first.Next;
                dictionary.Add("First", this.first);
                PdfOutline outline = next;
                for (int i = objects.MarkObjectAsWritten(next); outline != null; i = num)
                {
                    next = outline.Next;
                    num = objects.MarkObjectAsWritten(next);
                    objects.WriteObject(outline, i);
                    outline = next;
                }
            }
            dictionary.Add("Last", this.last);
            return dictionary;
        }

        internal int UpdateCount()
        {
            this.count = 0;
            for (PdfOutline outline = this.first; outline != null; outline = outline.Next)
            {
                this.count++;
                if (!outline.Closed)
                {
                    this.count += outline.UpdateCount();
                }
            }
            return this.count;
        }

        public bool Closed
        {
            get => 
                this.closed;
            set => 
                this.closed = value;
        }

        public PdfOutline First
        {
            get => 
                this.first;
            internal set => 
                this.first = value;
        }

        public PdfOutline Last
        {
            get => 
                this.last;
            internal set => 
                this.last = value;
        }

        public int Count =>
            this.count;
    }
}

