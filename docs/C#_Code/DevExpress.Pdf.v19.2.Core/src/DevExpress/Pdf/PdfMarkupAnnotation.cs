namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfMarkupAnnotation : PdfAnnotation
    {
        private const string titleDictionaryKey = "T";
        private const string popupDictionaryKey = "Popup";
        private const string opacityDictionaryKey = "CA";
        private const string richTextDictionaryKey = "RC";
        private const string creationDateDictionaryKey = "CreationDate";
        private const string inReplyToDictionaryKey = "IRT";
        private const string subjectDictionaryKey = "Subj";
        private const string replyTypeDictionaryKey = "RT";
        private const string intentDictionaryKey = "IT";
        private const double defaultOpacity = 1.0;
        private readonly string richTextData;
        private readonly string intent;
        private readonly PdfMarkupAnnotationReplyType replyType;
        private readonly int popupAnnotationNumber;
        private readonly int inReplyToAnnotationNumber;
        private string title;
        private DateTimeOffset? creationDate;
        private double opacity;
        private string subject;
        private PdfPopupAnnotation popup;
        private PdfAnnotation inReplyTo;

        protected PdfMarkupAnnotation(PdfPage page, IPdfMarkupAnnotationBuilder builder) : base(page, builder)
        {
            this.creationDate = builder.CreationDate;
            this.subject = builder.Subject;
            this.title = builder.Title;
            this.opacity = builder.Opacity;
            this.popup = new PdfPopupAnnotation(page, this);
        }

        protected PdfMarkupAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            object obj2;
            this.title = dictionary.GetTextString("T");
            if (dictionary.TryGetValue("Popup", out obj2) && (obj2 != null))
            {
                PdfObjectReference reference2 = obj2 as PdfObjectReference;
                if (reference2 != null)
                {
                    this.popupAnnotationNumber = reference2.Number;
                }
                else
                {
                    PdfReaderDictionary dictionary2 = obj2 as PdfReaderDictionary;
                    if (dictionary2 != null)
                    {
                        this.popup = new PdfPopupAnnotation(page, dictionary2);
                        page.Annotations.Add(this.popup);
                    }
                }
            }
            double? number = dictionary.GetNumber("CA");
            this.opacity = (number != null) ? number.GetValueOrDefault() : 1.0;
            this.opacity = Math.Max(0.0, Math.Min(1.0, this.opacity));
            this.richTextData = dictionary.GetStringAdvanced("RC");
            this.creationDate = dictionary.GetDate("CreationDate");
            PdfObjectReference objectReference = dictionary.GetObjectReference("IRT");
            if (objectReference != null)
            {
                this.inReplyToAnnotationNumber = objectReference.Number;
                this.replyType = dictionary.GetEnumName<PdfMarkupAnnotationReplyType>("RT");
            }
            this.subject = dictionary.GetTextString("Subj");
            this.intent = dictionary.GetName("IT");
        }

        protected internal override void Accept(IPdfAnnotationVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.AddIfPresent("T", this.title);
            dictionary.Add("Popup", this.Popup);
            dictionary.Add("CA", this.opacity, 1.0);
            dictionary.AddNotNullOrEmptyString("RC", this.richTextData);
            dictionary.AddIfPresent("CreationDate", this.creationDate);
            if (this.InReplyTo != null)
            {
                dictionary.Add("IRT", this.inReplyTo);
                dictionary.AddEnumName<PdfMarkupAnnotationReplyType>("RT", this.replyType);
            }
            dictionary.AddNotNullOrEmptyString("Subj", this.subject);
            dictionary.AddName("IT", this.intent);
            return dictionary;
        }

        protected internal override void Ensure()
        {
            base.Ensure();
            PdfPage page = base.Page;
            PdfObjectCollection objects = page.DocumentCatalog.Objects;
            if ((this.popup == null) && (this.popupAnnotationNumber != 0))
            {
                this.popup = this.Find(this.popupAnnotationNumber) as PdfPopupAnnotation;
                if (this.popup == null)
                {
                    PdfPopupAnnotation item = objects.GetAnnotation(page, new PdfObjectReference(this.popupAnnotationNumber)) as PdfPopupAnnotation;
                    if ((item != null) && ReferenceEquals(item.Page, page))
                    {
                        page.Annotations.Add(item);
                        this.popup = item;
                    }
                }
            }
            if ((this.inReplyTo == null) && (this.inReplyToAnnotationNumber != 0))
            {
                this.inReplyTo = this.Find(this.inReplyToAnnotationNumber);
                if (this.inReplyTo == null)
                {
                    PdfAnnotation annotation = objects.GetAnnotation(page, new PdfObjectReference(this.inReplyToAnnotationNumber));
                    if ((annotation == null) || !ReferenceEquals(annotation.Page, page))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    page.Annotations.Add(annotation);
                    this.inReplyTo = annotation;
                }
            }
        }

        private PdfAnnotation Find(int number)
        {
            PdfAnnotation annotation2;
            using (IEnumerator<PdfAnnotation> enumerator = base.Page.Annotations.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfAnnotation current = enumerator.Current;
                        if (current.ObjectNumber != number)
                        {
                            continue;
                        }
                        annotation2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return annotation2;
        }

        public string RichTextData =>
            this.richTextData;

        public string Intent =>
            this.intent;

        public PdfMarkupAnnotationReplyType ReplyType =>
            this.replyType;

        public PdfPopupAnnotation Popup
        {
            get
            {
                this.Ensure();
                return this.popup;
            }
        }

        public PdfAnnotation InReplyTo
        {
            get
            {
                this.Ensure();
                return this.inReplyTo;
            }
        }

        public string Title
        {
            get => 
                this.title;
            internal set => 
                this.title = value;
        }

        public double Opacity
        {
            get => 
                this.opacity;
            internal set => 
                this.opacity = value;
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.creationDate;
            internal set => 
                this.creationDate = value;
        }

        public string Subject
        {
            get => 
                this.subject;
            internal set => 
                this.subject = value;
        }
    }
}

