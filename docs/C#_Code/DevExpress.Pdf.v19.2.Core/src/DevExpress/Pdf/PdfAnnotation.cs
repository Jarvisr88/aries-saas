namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfAnnotation : PdfObject
    {
        internal const string DictionaryType = "Annot";
        internal const string PageDictionaryKey = "P";
        private const string rectDictionaryKey = "Rect";
        private const string contentsDictionaryKey = "Contents";
        private const string nameDictionaryKey = "NM";
        private const string modifiedDictionaryKey = "M";
        private const string flagsDictionaryKey = "F";
        private const string appearanceNameDictionaryKey = "AS";
        private const string appearanceDictionaryKey = "AP";
        private const string borderDictionaryKey = "Border";
        private const string colorDictionaryKey = "C";
        private const string structParentDictionaryKey = "StructParent";
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly PdfPage page;
        private PdfRectangle rect;
        private string contents;
        private string name;
        private DateTimeOffset? modified;
        private PdfAnnotationFlags flags;
        private PdfAnnotationBorder border;
        private PdfColor color;
        private int? structParent;
        private PdfOptionalContent optionalContent;
        private PdfAnnotationAppearances appearance;
        private string appearanceName;
        private PdfReaderDictionary dictionary;

        protected PdfAnnotation(PdfPage page, IPdfAnnotationBuilder builder)
        {
            this.page = page;
            this.documentCatalog = page.DocumentCatalog;
            this.rect = builder.Rect;
            this.name = builder.Name;
            this.color = (builder.Color != null) ? new PdfColor(builder.Color) : null;
            this.modified = builder.ModificationDate;
            this.contents = builder.Contents;
            this.border = builder.Border;
            this.flags = builder.Flags;
            page.Annotations.Add(this);
        }

        protected PdfAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.page = page;
            this.dictionary = dictionary;
            this.documentCatalog = dictionary.Objects.DocumentCatalog;
        }

        protected internal virtual void Accept(IPdfAnnotationVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected internal virtual IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch) => 
            null;

        internal PdfForm CreateAppearanceForm(PdfAnnotationAppearanceState state) => 
            this.CreateAppearanceForm(state, null);

        protected internal virtual PdfForm CreateAppearanceForm(PdfAnnotationAppearanceState state, string name)
        {
            this.Ensure();
            PdfForm form = new PdfForm(this.documentCatalog, new PdfRectangle(0.0, 0.0, this.rect.Width, this.rect.Height));
            this.appearance ??= new PdfAnnotationAppearances();
            this.appearance.SetForm(state, name, form);
            return form;
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("P", this.page);
            dictionary.Add("Type", new PdfName("Annot"));
            dictionary.Add("Subtype", new PdfName(this.AnnotationType));
            dictionary.Add("Rect", this.rect);
            dictionary.Add("Contents", this.contents, null);
            dictionary.Add("NM", this.name, null);
            dictionary.AddNullable<DateTimeOffset>("M", this.modified);
            dictionary.Add("F", (int) this.flags, 0);
            dictionary.Add("AP", this.appearance);
            dictionary.AddName("AS", this.appearanceName);
            if (!this.border.IsDefault)
            {
                dictionary.Add("Border", this.border.ToWritableObject());
            }
            dictionary.Add("C", this.color);
            dictionary.AddNullable<int>("StructParent", this.structParent);
            dictionary.Add("OC", this.optionalContent);
            return dictionary;
        }

        protected internal virtual void Ensure()
        {
            if (this.dictionary != null)
            {
                this.ResolveDictionary(this.dictionary);
                this.dictionary = null;
            }
        }

        internal PdfForm EnsureAppearance(PdfAnnotationAppearanceState appearanceState, PdfDocumentStateBase documentState)
        {
            PdfForm appearanceForm = this.GetAppearanceForm(appearanceState);
            return (((appearanceState == PdfAnnotationAppearanceState.Normal) || (appearanceForm != null)) ? this.EnsureAppearance(appearanceState, documentState, appearanceForm) : this.EnsureAppearance(PdfAnnotationAppearanceState.Normal, documentState));
        }

        protected PdfForm EnsureAppearance(PdfAnnotationAppearanceState appearanceState, PdfDocumentStateBase documentState, PdfForm form)
        {
            bool flag = false;
            try
            {
                flag = this.ShouldCreateAppearance(form);
            }
            catch
            {
                flag = true;
            }
            if (flag)
            {
                IPdfAnnotationAppearanceBuilder builder = this.CreateAppearanceBuilder(documentState.FontSearch);
                if (builder != null)
                {
                    form ??= this.CreateAppearanceForm(appearanceState);
                    builder.RebuildAppearance(form);
                }
            }
            return form;
        }

        internal PdfForm GetAppearanceForm(PdfAnnotationAppearanceState appearanceState)
        {
            this.Ensure();
            if (this.appearance != null)
            {
                PdfAnnotationAppearance appearance = (appearanceState == PdfAnnotationAppearanceState.Rollover) ? this.appearance.Rollover : ((appearanceState != PdfAnnotationAppearanceState.Down) ? this.appearance.Normal : this.appearance.Down);
                if (appearance != null)
                {
                    PdfForm defaultForm = null;
                    if (!string.IsNullOrEmpty(this.appearanceName) && ((appearance.Forms != null) && !appearance.Forms.TryGetValue(this.appearanceName, out defaultForm)))
                    {
                        defaultForm = null;
                    }
                    if ((defaultForm == null) && this.ShouldUseDefaultForm)
                    {
                        defaultForm = appearance.DefaultForm;
                    }
                    return defaultForm;
                }
            }
            return null;
        }

        protected internal virtual PdfRectangle GetAppearanceFormBoundingBox() => 
            new PdfRectangle(0.0, 0.0, this.Rect.Width, this.Rect.Height);

        internal static PdfAnnotation Parse(PdfPage page, PdfReaderDictionary dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }
            string name = dictionary.GetName("Subtype");
            if (name == null)
            {
                return null;
            }
            uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
            if (num <= 0x812746a9)
            {
                if (num <= 0x1cfdec24)
                {
                    if (num <= 0x60d6239)
                    {
                        if (num == 0x635df1)
                        {
                            if (name == "Watermark")
                            {
                                return new PdfWatermarkAnnotation(page, dictionary);
                            }
                        }
                        else if (num != 0x374274f)
                        {
                            if ((num == 0x60d6239) && (name == "RichMedia"))
                            {
                                return new PdfRichMediaAnnotation(page, dictionary);
                            }
                        }
                        else if (name == "Ink")
                        {
                            return new PdfInkAnnotation(page, dictionary);
                        }
                    }
                    else if (num == 0x1ab33676)
                    {
                        if (name == "Squiggly")
                        {
                            return new PdfTextMarkupAnnotation(page, PdfTextMarkupAnnotationType.Squiggly, dictionary);
                        }
                    }
                    else if (num != 0x1befa772)
                    {
                        if ((num == 0x1cfdec24) && (name == "Stamp"))
                        {
                            return new PdfRubberStampAnnotation(page, dictionary);
                        }
                    }
                    else if (name == "3D")
                    {
                        return new Pdf3dAnnotation(page, dictionary);
                    }
                }
                else if (num <= 0x2e6edf68)
                {
                    if (num == 0x2324f148)
                    {
                        if (name == "Caret")
                        {
                            return new PdfCaretAnnotation(page, dictionary);
                        }
                    }
                    else if (num != 0x2bb3fca1)
                    {
                        if ((num == 0x2e6edf68) && (name == "Redact"))
                        {
                            return new PdfRedactAnnotation(page, dictionary);
                        }
                    }
                    else if (name == "StrikeOut")
                    {
                        return new PdfTextMarkupAnnotation(page, PdfTextMarkupAnnotationType.StrikeOut, dictionary);
                    }
                }
                else if (num <= 0x3e142d5e)
                {
                    if (num != 0x3bb2e9ae)
                    {
                        if ((num == 0x3e142d5e) && (name == "Text"))
                        {
                            return new PdfTextAnnotation(page, dictionary);
                        }
                    }
                    else if (name == "FileAttachment")
                    {
                        return new PdfFileAttachmentAnnotation(page, dictionary);
                    }
                }
                else if (num != 0x78cc9045)
                {
                    if ((num == 0x812746a9) && (name == "Circle"))
                    {
                        return new PdfCircleAnnotation(page, dictionary);
                    }
                }
                else if (name == "TrapNet")
                {
                    return new PdfTrapNetAnnotation(page, dictionary);
                }
            }
            else if (num <= 0x9c69006c)
            {
                if (num <= 0x8e37f629)
                {
                    if (num == 0x8368886f)
                    {
                        if (name == "Movie")
                        {
                            return new PdfMovieAnnotation(page, dictionary);
                        }
                    }
                    else if (num != 0x8e08e589)
                    {
                        if ((num == 0x8e37f629) && (name == "Polygon"))
                        {
                            return new PdfPolygonAnnotation(page, dictionary);
                        }
                    }
                    else if (name == "Link")
                    {
                        return new PdfLinkAnnotation(page, dictionary);
                    }
                }
                else if (num <= 0x9a85011f)
                {
                    if (num != 0x9808f547)
                    {
                        if ((num == 0x9a85011f) && (name == "Underline"))
                        {
                            return new PdfTextMarkupAnnotation(page, PdfTextMarkupAnnotationType.Underline, dictionary);
                        }
                    }
                    else if (name == "Line")
                    {
                        return new PdfLineAnnotation(page, dictionary);
                    }
                }
                else if (num != 0x9bd8c631)
                {
                    if ((num == 0x9c69006c) && (name == "FreeText"))
                    {
                        return new PdfFreeTextAnnotation(page, dictionary);
                    }
                }
                else if (name == "Screen")
                {
                    return new PdfScreenAnnotation(page, dictionary);
                }
            }
            else if (num <= 0xe757b9d5)
            {
                if (num == 0xb3ca9a34)
                {
                    if (name == "Sound")
                    {
                        return new PdfSoundAnnotation(page, dictionary);
                    }
                }
                else if (num != 0xda031f8a)
                {
                    if ((num == 0xe757b9d5) && (name == "PolyLine"))
                    {
                        return new PdfPolyLineAnnotation(page, dictionary);
                    }
                }
                else if (name == "PrinterMark")
                {
                    return new PdfPrinterMarkAnnotation(page, dictionary);
                }
            }
            else if (num <= 0xeaf105c7)
            {
                if (num != 0xe8eb787d)
                {
                    if ((num == 0xeaf105c7) && (name == "Highlight"))
                    {
                        return new PdfTextMarkupAnnotation(page, PdfTextMarkupAnnotationType.Highlight, dictionary);
                    }
                }
                else if (name == "Popup")
                {
                    return new PdfPopupAnnotation(page, dictionary);
                }
            }
            else if (num != 0xef953925)
            {
                if ((num == 0xfac98c66) && (name == "Square"))
                {
                    return new PdfSquareAnnotation(page, dictionary);
                }
            }
            else if (name == "Widget")
            {
                return new PdfWidgetAnnotation(page, dictionary);
            }
            return new PdfCustomAnnotation(page, dictionary);
        }

        internal static PdfColor ParseColor(PdfReaderDictionary dictionary, string key)
        {
            IList<object> array = dictionary.GetArray(key);
            if (array == null)
            {
                return null;
            }
            int count = array.Count;
            if (count == 0)
            {
                return null;
            }
            if ((count != 1) && ((count != 3) && (count != 4)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            double[] components = new double[count];
            for (int i = 0; i < count; i++)
            {
                double num3 = PdfDocumentReader.ConvertToDouble(array[i]);
                if (num3 < 0.0)
                {
                    num3 = 0.0;
                }
                else if (num3 > 1.0)
                {
                    num3 = 1.0;
                }
                components[i] = num3;
            }
            return new PdfColor(components);
        }

        protected virtual void ResolveDictionary(PdfReaderDictionary dictionary)
        {
            object obj2;
            PdfRectangle rectangle = dictionary.GetRectangle("Rect");
            PdfRectangle rectangle2 = rectangle;
            if (rectangle == null)
            {
                PdfRectangle local1 = rectangle;
                rectangle2 = new PdfRectangle(0.0, 0.0, 0.0, 0.0);
            }
            this.rect = rectangle2;
            this.contents = dictionary.GetTextString("Contents");
            this.name = dictionary.GetTextString("NM");
            this.modified = dictionary.GetDate("M");
            int? integer = dictionary.GetInteger("F");
            this.flags = (integer != null) ? ((PdfAnnotationFlags) integer.GetValueOrDefault()) : PdfAnnotationFlags.None;
            this.appearanceName = dictionary.GetName("AS");
            if (dictionary.TryGetValue("AP", out obj2))
            {
                this.appearance = dictionary.Objects.GetAnnotationAppearances(obj2, this.rect);
            }
            IList<object> array = dictionary.GetArray("Border");
            this.border = ((array == null) || (array.Count == 0)) ? new PdfAnnotationBorder() : new PdfAnnotationBorder(array);
            this.color = ParseColor(dictionary, "C");
            this.structParent = dictionary.GetInteger("StructParent");
            this.optionalContent = dictionary.GetOptionalContent();
        }

        protected virtual bool ShouldCreateAppearance(PdfForm form) => 
            (form == null) || ((form.Commands == null) || (form.Commands.Count == 0));

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            this.Ensure();
            return this.CreateDictionary(collection);
        }

        public PdfPage Page =>
            this.page;

        public PdfRectangle Rect
        {
            get
            {
                this.Ensure();
                return this.rect;
            }
            internal set
            {
                this.Ensure();
                this.rect = value;
            }
        }

        public string Contents
        {
            get
            {
                this.Ensure();
                return this.contents;
            }
            internal set
            {
                this.Ensure();
                this.contents = value;
            }
        }

        public string Name
        {
            get
            {
                this.Ensure();
                return this.name;
            }
            internal set
            {
                this.Ensure();
                this.name = value;
            }
        }

        public DateTimeOffset? Modified
        {
            get
            {
                this.Ensure();
                return this.modified;
            }
            internal set
            {
                this.Ensure();
                this.modified = value;
            }
        }

        public PdfAnnotationFlags Flags
        {
            get
            {
                this.Ensure();
                return this.flags;
            }
            set
            {
                this.Ensure();
                this.flags = value;
            }
        }

        public PdfAnnotationBorder Border
        {
            get
            {
                this.Ensure();
                return this.border;
            }
        }

        public PdfColor Color
        {
            get
            {
                this.Ensure();
                return this.color;
            }
            internal set
            {
                this.Ensure();
                this.color = value;
            }
        }

        public int? StructParent
        {
            get
            {
                this.Ensure();
                return this.structParent;
            }
        }

        public PdfOptionalContent OptionalContent
        {
            get
            {
                this.Ensure();
                return this.optionalContent;
            }
        }

        public PdfAnnotationAppearances Appearance
        {
            get
            {
                this.Ensure();
                return this.appearance;
            }
        }

        public string AppearanceName
        {
            get
            {
                this.Ensure();
                return this.appearanceName;
            }
            set
            {
                this.Ensure();
                this.appearanceName = value;
            }
        }

        protected PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;

        protected abstract string AnnotationType { get; }

        protected virtual bool ShouldUseDefaultForm =>
            true;
    }
}

