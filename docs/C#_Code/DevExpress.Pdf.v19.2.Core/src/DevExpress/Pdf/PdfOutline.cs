namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfOutline : PdfOutlineItem
    {
        internal const string NextDictionaryKey = "Next";
        private const string titleDictionaryKey = "Title";
        private const string prevDictionaryKey = "Prev";
        private const string destinationDictionaryKey = "Dest";
        private const string actionDictionaryKey = "A";
        private const string colorDictionaryKey = "C";
        private const string flagsDictionaryKey = "F";
        private const int italicFlag = 1;
        private const int boldFlag = 2;
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly string title;
        private readonly PdfOutlineItem parent;
        private readonly PdfDestinationObject destination;
        private readonly PdfAction action;
        private readonly PdfColor color;
        private readonly bool isItalic;
        private readonly bool isBold;
        private PdfOutline prev;
        private PdfOutline next;

        internal PdfOutline(PdfOutlineItem parent, PdfOutline prev, PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            this.parent = parent;
            this.prev = prev;
            PdfObjectCollection objects = dictionary.Objects;
            this.documentCatalog = objects.DocumentCatalog;
            base.ObjectNumber = dictionary.Number;
            if (!dictionary.TryGetValue("Title", out obj2))
            {
                this.title = string.Empty;
            }
            else
            {
                obj2 = objects.TryResolve(obj2, null);
                if (obj2 == null)
                {
                    obj2 = string.Empty;
                }
                else
                {
                    byte[] buffer = obj2 as byte[];
                    if (buffer != null)
                    {
                        this.title = PdfDocumentReader.ConvertToTextString(buffer);
                    }
                    else
                    {
                        PdfName name = obj2 as PdfName;
                        if (name != null)
                        {
                            this.title = name.Name;
                        }
                    }
                }
            }
            this.destination = dictionary.GetDestination("Dest");
            this.action = dictionary.GetAction("A");
            Func<object, double> create = <>c.<>9__45_0;
            if (<>c.<>9__45_0 == null)
            {
                Func<object, double> local1 = <>c.<>9__45_0;
                create = <>c.<>9__45_0 = o => PdfDocumentReader.ConvertToDouble(o);
            }
            IList<double> array = dictionary.GetArray<double>("C", create);
            if (array == null)
            {
                this.color = new PdfColor(new double[3]);
            }
            else
            {
                if (array.Count != 3)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                double[] components = new double[] { array[0], array[1], array[2] };
                this.color = new PdfColor(components);
            }
            int? integer = dictionary.GetInteger("F");
            int num = (integer != null) ? integer.GetValueOrDefault() : 0;
            this.isItalic = (num & 1) > 0;
            this.isBold = (num & 2) > 0;
        }

        private PdfOutline(PdfOutlineItem parent, PdfOutline prev, PdfBookmark bookmark)
        {
            this.parent = parent;
            this.prev = prev;
            this.title = bookmark.Title;
            this.destination = bookmark.DestinationObject;
            this.action = bookmark.Action;
            this.isItalic = bookmark.IsItalic;
            this.isBold = bookmark.IsBold;
            PdfRGBColor textColor = bookmark.TextColor;
            this.color = new PdfColor(textColor);
            base.Closed = bookmark.IsInitiallyClosed;
            base.First = CreateOutlineTree(this, bookmark.Children);
            base.UpdateCount();
        }

        internal static PdfOutline CreateOutlineTree(PdfOutlineItem parent, IList<PdfBookmark> bookmarks)
        {
            int count = bookmarks.Count;
            if (count == 0)
            {
                return null;
            }
            PdfOutline outline = new PdfOutline(parent, null, bookmarks[0]);
            PdfOutline prev = outline;
            for (int i = 1; i < count; i++)
            {
                PdfOutline outline3 = new PdfOutline(parent, prev, bookmarks[i]);
                prev.Next = outline3;
                prev = outline3;
            }
            parent.Last = prev;
            return outline;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            object obj2 = base.ToWritableObject(objects);
            PdfWriterDictionary dictionary = obj2 as PdfWriterDictionary;
            if (dictionary != null)
            {
                dictionary.Add("Title", this.title);
                dictionary.Add("Parent", this.parent);
                dictionary.Add("Prev", this.prev);
                dictionary.Add("Next", this.next);
                dictionary.Add("Count", base.Closed ? -base.Count : base.Count, 0);
                if (this.destination != null)
                {
                    dictionary.Add("Dest", this.destination.ToWriteableObject(this.documentCatalog, objects, true));
                }
                dictionary.Add("A", this.action);
                double[] components = this.color.Components;
                int index = 0;
                while (true)
                {
                    if (index < components.Length)
                    {
                        double num2 = components[index];
                        if (num2 == 0.0)
                        {
                            index++;
                            continue;
                        }
                        dictionary.Add("C", this.color);
                    }
                    dictionary.Add("F", (this.isItalic ? 1 : 0) | (this.isBold ? 2 : 0), 0);
                    break;
                }
            }
            return obj2;
        }

        internal PdfDestinationObject DestinationObject =>
            this.destination;

        internal PdfDestination ActualDestination
        {
            get
            {
                PdfDestination destination = this.Destination;
                if (destination != null)
                {
                    return destination;
                }
                PdfGoToAction action = this.action as PdfGoToAction;
                return action?.Destination;
            }
        }

        public string Title =>
            this.title;

        public PdfOutlineItem Parent =>
            this.parent;

        public PdfDestination Destination =>
            this.destination?.GetDestination(this.documentCatalog, true);

        public PdfAction Action =>
            this.action;

        public PdfColor Color =>
            this.color;

        public bool IsItalic =>
            this.isItalic;

        public bool IsBold =>
            this.isBold;

        public PdfOutline Prev
        {
            get => 
                this.prev;
            internal set => 
                this.prev = value;
        }

        public PdfOutline Next
        {
            get => 
                this.next;
            internal set => 
                this.next = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfOutline.<>c <>9 = new PdfOutline.<>c();
            public static Func<object, double> <>9__45_0;

            internal double <.ctor>b__45_0(object o) => 
                PdfDocumentReader.ConvertToDouble(o);
        }
    }
}

