namespace DevExpress.Pdf
{
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PdfInteractiveFormField : PdfObject
    {
        internal const char FieldNameDelimiter = '.';
        internal const string ParentDictionaryKey = "Parent";
        protected const string ValueDictionaryKey = "V";
        protected const string DefaultValueDictionaryKey = "DV";
        protected const string OptionsDictionaryKey = "Opt";
        protected const double MinFontSize = 4.0;
        private const string formTypeDictionaryKey = "FT";
        private const string kidsDictionaryKey = "Kids";
        private const string nameDictionaryKey = "T";
        private const string alternateNameDictionaryKey = "TU";
        private const string mappingNameDictionaryKey = "TM";
        private const string flagsDictionaryKey = "Ff";
        private const string actionsDictionaryKey = "AA";
        private const string defaultStyleDictionaryKey = "DS";
        private const string richTextDataDictionaryKey = "RV";
        private readonly PdfInteractiveForm form;
        private readonly PdfInteractiveFormField parent;
        private readonly PdfWidgetAnnotation widget;
        private readonly string alternateName;
        private readonly string mappingName;
        private readonly PdfAdditionalActions actions;
        private readonly PdfTextJustification? textJustification;
        private readonly string defaultStyle;
        private readonly string richTextData;
        private readonly PdfInteractiveFormField valuesProvider;
        private PdfInteractiveFormFieldCollection kids;
        private string name;
        private PdfCommandList appearanceCommands;
        private PdfInteractiveFormFieldTextState textState;
        private bool isFormCreated;
        private PdfInteractiveFormFieldFlags? flags;

        internal event EventHandler WidgetAppearanceChanged;

        protected PdfInteractiveFormField(PdfButtonFormField radioGroup, PdfWidgetAnnotation radioGroupWidget)
        {
            this.parent = radioGroup;
            this.flags = new PdfInteractiveFormFieldFlags?(radioGroup.Flags);
            this.widget = radioGroupWidget;
            this.valuesProvider = radioGroup;
            this.widget.InteractiveFormField = this;
            IList<PdfInteractiveFormField> kids = this.parent.kids;
            if (kids == null)
            {
                PdfInteractiveFormFieldCollection fields = new PdfInteractiveFormFieldCollection();
                kids = fields;
                this.parent.kids = fields;
            }
            kids.Add(this);
        }

        protected PdfInteractiveFormField(PdfInteractiveForm form, PdfWidgetAnnotation widget)
        {
            this.form = form;
            this.widget = widget;
            this.valuesProvider = this;
            widget.InteractiveFormField = this;
            form.AddInteractiveFormField(this);
        }

        internal PdfInteractiveFormField(PdfInteractiveForm form, PdfInteractiveFormField parent, PdfReaderDictionary dictionary, PdfObjectReference valueReference) : base(dictionary.Number)
        {
            PdfInteractiveFormFieldFlags? nullable1;
            this.parent = parent;
            this.form = form;
            PdfObjectCollection objects = dictionary.Objects;
            int? integer = dictionary.GetInteger("Ff");
            if (integer != null)
            {
                nullable1 = new PdfInteractiveFormFieldFlags?(integer.Value);
            }
            else
            {
                nullable1 = null;
            }
            this.flags = nullable1;
            this.kids = dictionary.GetDeferredFormFieldCollection("Kids");
            if (this.kids == null)
            {
                IList<object> array = dictionary.GetArray("Kids");
                if (array != null)
                {
                    this.kids = new PdfInteractiveFormFieldCollection(array, form, this, objects);
                }
                else
                {
                    PdfDocumentCatalog documentCatalog = objects.DocumentCatalog;
                    this.widget = documentCatalog.FindWidget(dictionary.Number);
                    if (this.widget == null)
                    {
                        object obj2;
                        PdfPage page = null;
                        if (dictionary.TryGetValue("P", out obj2))
                        {
                            page = documentCatalog.FindPage(obj2);
                        }
                        this.widget = objects.GetAnnotation(page, valueReference) as PdfWidgetAnnotation;
                    }
                    if (this.widget != null)
                    {
                        this.widget.InteractiveFormField = this;
                    }
                }
            }
            this.name = dictionary.GetTextString("T");
            this.alternateName = dictionary.GetTextString("TU");
            this.mappingName = dictionary.GetTextString("TM");
            this.actions = dictionary.GetAdditionalActions(this.widget);
            byte[] bytes = dictionary.GetBytes("DA", true);
            this.appearanceCommands = (bytes == null) ? null : PdfContentStreamParser.GetContent((this.Form == null) ? new PdfResources(objects.DocumentCatalog, false) : this.Form.Resources, bytes);
            int? nullable2 = dictionary.GetInteger("Q");
            this.textJustification = (nullable2 == null) ? ((parent == null) ? ((PdfTextJustification?) 0) : null) : new PdfTextJustification?(PdfEnumToValueConverter.Parse<PdfTextJustification>(new int?(nullable2.Value), 0));
            this.defaultStyle = dictionary.GetTextString("DS");
            this.richTextData = dictionary.GetStringAdvanced("RV");
            this.valuesProvider = ((this.name != null) || (parent == null)) ? this : parent;
        }

        protected PdfInteractiveFormField(PdfInteractiveFormField parent, IPdfExportFontProvider fontSearch, PdfDocument document, PdfAcroFormCommonVisualField visualField) : this(parent, document, visualField, false)
        {
            PdfAcroFormStringAlignment textAlignment = visualField.TextAlignment;
            this.textJustification = (textAlignment == PdfAcroFormStringAlignment.Center) ? ((PdfTextJustification?) 1) : ((textAlignment != PdfAcroFormStringAlignment.Far) ? ((PdfTextJustification?) 0) : ((PdfTextJustification?) 2));
            this.widget = visualField.CreateWidget(document);
            this.widget.InteractiveFormField = this;
            this.form = document.DocumentCatalog.GetExistingOrCreateNewInteractiveForm();
            this.appearanceCommands = visualField.CreateAppearanceCommands(fontSearch, this.form);
            this.actions = visualField.CreateAdditionalActions(document);
        }

        internal PdfInteractiveFormField(PdfInteractiveFormField parent, PdfDocument document, PdfAcroFormField formField, bool createKids)
        {
            this.parent = parent;
            this.form = document.DocumentCatalog.GetExistingOrCreateNewInteractiveForm();
            this.flags = new PdfInteractiveFormFieldFlags?(formField.Flags);
            this.name = formField.Name;
            this.alternateName = formField.ToolTip;
            this.valuesProvider = this;
            this.form.AddInteractiveFormField(this);
            if (createKids)
            {
                this.kids = new PdfInteractiveFormFieldCollection();
            }
        }

        protected internal virtual void Accept(IPdfInteractiveFormFieldVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected virtual bool AcceptValue(object value) => 
            (value == null) || (value is string);

        protected internal virtual double CalcFontSize(string text, PdfRectangle layoutRect, PdfExportFont fontData)
        {
            double num = layoutRect.Height * 0.75;
            return (!string.IsNullOrEmpty(text) ? Math.Max(Math.Min(num, layoutRect.Width / TextWidthHelper.GetTextWidth(text, fontData, 1f, this.textState)), 4.0) : num);
        }

        private void CollectAnnotationsToRemove(Dictionary<PdfPage, HashSet<PdfAnnotation>> removingAnnotations)
        {
            if (this.widget != null)
            {
                PdfPage key = this.widget.Page;
                if (key != null)
                {
                    HashSet<PdfAnnotation> set;
                    if (!removingAnnotations.TryGetValue(key, out set))
                    {
                        set = new HashSet<PdfAnnotation>();
                        removingAnnotations.Add(key, set);
                    }
                    set.Add(this.widget);
                }
            }
            if (this.kids != null)
            {
                foreach (PdfInteractiveFormField field in (IEnumerable<PdfInteractiveFormField>) this.kids)
                {
                    field.CollectAnnotationsToRemove(removingAnnotations);
                }
            }
        }

        protected internal virtual void FillDictionary(PdfWriterDictionary dictionary)
        {
            dictionary.Add("Parent", this.parent);
            dictionary.AddName("FT", this.FieldType);
            dictionary.Add("Kids", this.kids);
            dictionary.Add("T", dictionary.Objects.GetFormFieldName(this.name), null);
            dictionary.Add("TU", this.alternateName, null);
            dictionary.Add("TM", this.mappingName, null);
            if (this.flags != null)
            {
                dictionary.Add("Ff", (int) this.flags.Value, 0);
            }
            if (!dictionary.ContainsKey("AA"))
            {
                dictionary.Add("AA", this.actions);
            }
            if (this.appearanceCommands != null)
            {
                dictionary.Add("DA", this.appearanceCommands.ToByteArray((this.form == null) ? new PdfResources(dictionary.Objects.DocumentCatalog, false) : this.form.Resources));
            }
            if (this.textJustification != null)
            {
                PdfTextJustification? textJustification = this.textJustification;
                PdfTextJustification leftJustified = PdfTextJustification.LeftJustified;
                if ((((PdfTextJustification) textJustification.GetValueOrDefault()) == leftJustified) ? (textJustification == null) : true)
                {
                    dictionary.Add("Q", PdfEnumToValueConverter.Convert<PdfTextJustification>(this.textJustification.Value));
                }
            }
            dictionary.Add("DS", this.defaultStyle, null);
            if (this.richTextData != null)
            {
                if (this.richTextData.Length == 0)
                {
                    dictionary.Add("RV", string.Empty);
                }
                else
                {
                    dictionary.AddStream("RV", this.richTextData);
                }
            }
        }

        internal PdfExportFontInfo GetFontInfo(IPdfExportFontProvider fontSearch)
        {
            PdfExportFont matchingFont = fontSearch.GetMatchingFont(this.TextState.FontCommand);
            double fontSize = this.TextState.FontSize;
            if (fontSize == 0.0)
            {
                PdfRectangle appearanceContentRectangle = this.widget.GetAppearanceContentRectangle();
                string text = this.Value as string;
                if (text != null)
                {
                    fontSize = this.CalcFontSize(text, appearanceContentRectangle, matchingFont);
                }
                else
                {
                    IList<string> list = this.Value as IList<string>;
                    if ((list == null) || (list.Count == 0))
                    {
                        fontSize = 12.0;
                    }
                    else
                    {
                        fontSize = double.MinValue;
                        foreach (string str2 in list)
                        {
                            fontSize = Math.Min(fontSize, this.CalcFontSize(str2, appearanceContentRectangle, matchingFont));
                        }
                    }
                }
            }
            return new PdfExportFontInfo(matchingFont, (float) fontSize, DXFontDecorations.None);
        }

        internal static PdfInteractiveFormField Parse(PdfInteractiveForm form, PdfInteractiveFormField parent, PdfReaderDictionary dictionary, PdfObjectReference reference)
        {
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            string name = dictionary.GetName("FT");
            if ((name == null) && (parent != null))
            {
                name = parent.FieldType;
            }
            return ((name != null) ? ((name == "Btn") ? ((PdfInteractiveFormField) new PdfButtonFormField(form, parent, dictionary, reference)) : ((name == "Tx") ? ((PdfInteractiveFormField) new PdfTextFormField(form, parent, dictionary, reference)) : ((name == "Ch") ? ((PdfInteractiveFormField) new PdfChoiceFormField(form, parent, dictionary, reference)) : ((name == "Sig") ? ((PdfInteractiveFormField) new PdfSignatureFormField(form, parent, dictionary, reference)) : null)))) : (dictionary.ContainsKey("Kids") ? new PdfInteractiveFormField(form, parent, dictionary, reference) : null));
        }

        protected void RaiseFieldChanged(object oldValue, object newValue)
        {
            if (this.form != null)
            {
                this.form.RaiseFormFieldValueChanged(this.FullName, oldValue, newValue);
            }
        }

        protected bool RaiseFieldChanging(PdfInteractiveFormFieldValueChangingEventArgs args) => 
            (this.form == null) || this.form.RaiseFormFieldValueChanging(args);

        internal void Remove(PdfDocumentStateBase documentState, bool flatten)
        {
            if (this.parent == null)
            {
                this.form.Fields.Remove(this);
            }
            else
            {
                this.parent.Kids.Remove(this);
            }
            Dictionary<PdfPage, HashSet<PdfAnnotation>> removingAnnotations = new Dictionary<PdfPage, HashSet<PdfAnnotation>>();
            this.CollectAnnotationsToRemove(removingAnnotations);
            foreach (KeyValuePair<PdfPage, HashSet<PdfAnnotation>> pair in removingAnnotations)
            {
                pair.Key.RemoveWidgetAnnotations(documentState, pair.Value, flatten);
            }
        }

        protected internal virtual void SetExportValue(object value, IPdfExportFontProvider fontSearch)
        {
            this.SetValue(value, fontSearch);
        }

        protected void SetFormModifiedState(IPdfExportFontProvider fontSearch)
        {
            if (this.form != null)
            {
                this.form.NeedAppearances = false;
            }
            if ((fontSearch != null) && (this.widget != null))
            {
                IPdfAnnotationAppearanceBuilder builder = new PdfWidgetAppearanceBuilderFactory(fontSearch).Create(this);
                if (builder != null)
                {
                    PdfForm appearanceForm;
                    if (this.isFormCreated)
                    {
                        appearanceForm = this.widget.GetAppearanceForm(PdfAnnotationAppearanceState.Normal);
                    }
                    else
                    {
                        appearanceForm = this.widget.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal, this.widget.AppearanceName);
                        this.isFormCreated = true;
                    }
                    if (this.form != null)
                    {
                        this.form.Resources.FillWidgetAppearanceResources(appearanceForm.CreateEmptyResources());
                    }
                    builder.RebuildAppearance(appearanceForm);
                }
            }
            if ((this.kids != null) && (this.widget == null))
            {
                foreach (PdfInteractiveFormField field in (IEnumerable<PdfInteractiveFormField>) this.kids)
                {
                    field.SetFormModifiedState(fontSearch);
                }
            }
            if (this.WidgetAppearanceChanged != null)
            {
                this.WidgetAppearanceChanged(this, EventArgs.Empty);
            }
        }

        protected internal virtual void SetValue(object value, IPdfExportFontProvider fontSearch)
        {
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            if (this.widget != null)
            {
                PdfWriterDictionary dictionary2 = this.widget.ToWritableObject(objects) as PdfWriterDictionary;
                if (dictionary2 != null)
                {
                    return dictionary2;
                }
            }
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            this.FillDictionary(dictionary);
            return dictionary;
        }

        public PdfInteractiveForm Form =>
            this.form;

        public PdfInteractiveFormField Parent =>
            this.parent;

        public PdfWidgetAnnotation Widget =>
            this.widget;

        public string AlternateName =>
            this.valuesProvider.alternateName;

        public string MappingName =>
            this.valuesProvider.mappingName;

        public PdfInteractiveFormFieldFlags Flags
        {
            get => 
                (this.flags == null) ? ((this.parent == null) ? PdfInteractiveFormFieldFlags.None : this.parent.Flags) : this.flags.Value;
            set => 
                this.flags = new PdfInteractiveFormFieldFlags?(value);
        }

        public PdfInteractiveFormFieldActions Actions =>
            this.actions?.InteractiveFormFieldActions;

        public PdfTextJustification TextJustification =>
            (this.textJustification != null) ? this.textJustification.Value : this.parent.TextJustification;

        public string DefaultStyle =>
            this.defaultStyle;

        public string RichTextData =>
            this.valuesProvider.richTextData;

        public IList<PdfInteractiveFormField> Kids =>
            this.kids;

        public string Name
        {
            get => 
                this.name;
            internal set => 
                this.name = value;
        }

        public IEnumerable<PdfCommand> AppearanceCommands =>
            this.appearanceCommands;

        internal string FullName
        {
            get
            {
                if (this.parent == null)
                {
                    return this.name;
                }
                string fullName = this.parent.FullName;
                return (!string.IsNullOrEmpty(fullName) ? (string.IsNullOrEmpty(this.name) ? fullName : (fullName + "." + this.name)) : this.name);
            }
        }

        internal PdfInteractiveFormFieldTextState TextState
        {
            get
            {
                this.textState ??= new PdfInteractiveFormFieldTextState(this);
                return this.textState;
            }
        }

        protected PdfInteractiveFormField ValuesProvider =>
            this.valuesProvider;

        protected virtual string FieldType =>
            null;

        protected internal virtual object DefaultValue =>
            null;

        protected internal virtual object Value =>
            null;

        protected internal virtual bool ShouldHighlight =>
            !this.Flags.HasFlag(PdfInteractiveFormFieldFlags.ReadOnly);

        protected internal virtual bool ShouldUseDefaultAppearanceForm =>
            true;
    }
}

