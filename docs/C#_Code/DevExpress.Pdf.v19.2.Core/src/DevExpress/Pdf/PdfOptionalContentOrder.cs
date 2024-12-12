namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOptionalContentOrder : PdfOptionalContent
    {
        private readonly string name;
        private readonly IList<PdfOptionalContent> items;

        internal PdfOptionalContentOrder(PdfObjectCollection objects, IList<object> list) : base(-1)
        {
            this.items = new List<PdfOptionalContent>();
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                object obj2 = objects.TryResolve(list[i], null);
                if (obj2 == null)
                {
                    this.items.Add(null);
                }
                else
                {
                    byte[] buffer = obj2 as byte[];
                    if (buffer != null)
                    {
                        if (i != 0)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.name = PdfDocumentReader.ConvertToString(buffer);
                    }
                    else
                    {
                        IList<object> list2 = obj2 as IList<object>;
                        if (list2 == null)
                        {
                            this.items.Add(objects.GetOptionalContentGroup(obj2, true));
                        }
                        else
                        {
                            this.items.Add(new PdfOptionalContentOrder(objects, list2));
                        }
                    }
                }
            }
        }

        protected internal override object Write(PdfObjectCollection objects)
        {
            IList<object> list = new List<object>();
            if (!string.IsNullOrEmpty(this.name))
            {
                list.Add(this.name);
            }
            foreach (PdfOptionalContent content in this.items)
            {
                list.Add(objects.AddObject((PdfObject) content));
            }
            return list;
        }

        public string Name =>
            this.name;

        public IList<PdfOptionalContent> Items =>
            this.items;
    }
}

