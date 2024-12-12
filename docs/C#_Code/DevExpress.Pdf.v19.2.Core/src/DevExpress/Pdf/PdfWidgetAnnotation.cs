namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfWidgetAnnotation : PdfAnnotation
    {
        internal const string Type = "Widget";
        private const string appearanceCharacteristicsDictionaryKey = "MK";
        private readonly PdfAnnotationHighlightingMode highlightingMode;
        private readonly PdfAction action;
        private readonly PdfAdditionalActions actions;
        private readonly PdfAnnotationBorderStyle borderStyle;
        private PdfWidgetAppearanceCharacteristics appearanceCharacteristics;
        private PdfInteractiveFormField interactiveFormField;

        internal PdfWidgetAnnotation(PdfPage page, IPdfWidgetAnnotationBuilder widgetBuilder) : base(page, widgetBuilder)
        {
            this.borderStyle = widgetBuilder.CreateBorderStyle();
            this.appearanceCharacteristics = widgetBuilder.CreateAppearanceCharacteristics();
        }

        internal PdfWidgetAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.highlightingMode = dictionary.GetAnnotationHighlightingMode();
            this.action = dictionary.GetAction("A");
            this.actions = dictionary.GetAdditionalActions(this);
            this.borderStyle = PdfAnnotationBorderStyle.Parse(dictionary);
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("MK");
            if (dictionary2 != null)
            {
                this.appearanceCharacteristics = new PdfWidgetAppearanceCharacteristics(dictionary2);
            }
        }

        protected internal override void Accept(IPdfAnnotationVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected internal override IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch)
        {
            PdfInteractiveFormField interactiveFormField = this.InteractiveFormField;
            return ((interactiveFormField == null) ? null : new PdfWidgetAppearanceBuilderFactory(fontSearch).Create(interactiveFormField));
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.AddEnumName<PdfAnnotationHighlightingMode>("H", this.highlightingMode);
            dictionary.Add("A", this.action);
            if (!dictionary.ContainsKey("AA"))
            {
                dictionary.Add("AA", this.actions);
            }
            dictionary.Add("BS", this.borderStyle);
            if (this.appearanceCharacteristics != null)
            {
                dictionary.Add("MK", this.appearanceCharacteristics.ToWritableObject(collection));
            }
            PdfInteractiveFormField interactiveFormField = this.InteractiveFormField;
            if (interactiveFormField != null)
            {
                interactiveFormField.FillDictionary(dictionary);
            }
            return dictionary;
        }

        internal void EnsureAppearance(PdfDocumentStateBase documentState)
        {
            PdfInteractiveFormField interactiveFormField = this.InteractiveFormField;
            if ((interactiveFormField != null) && interactiveFormField.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton))
            {
                PdfForm appearanceForm = base.GetAppearanceForm(PdfAnnotationAppearanceState.Down);
                if (appearanceForm != null)
                {
                    base.EnsureAppearance(PdfAnnotationAppearanceState.Down, documentState, appearanceForm);
                }
            }
            base.EnsureAppearance(PdfAnnotationAppearanceState.Normal, documentState, base.GetAppearanceForm(PdfAnnotationAppearanceState.Normal));
        }

        private PdfWidgetAppearanceCharacteristics EnsureAppearanceCharacteristics()
        {
            this.appearanceCharacteristics ??= new PdfWidgetAppearanceCharacteristics();
            return this.appearanceCharacteristics;
        }

        internal PdfRectangle GetAppearanceContentRectangle()
        {
            PdfRectangle appearanceFormBoundingBox = this.GetAppearanceFormBoundingBox();
            double borderWidth = this.BorderWidth;
            double num2 = appearanceFormBoundingBox.Width - borderWidth;
            double num3 = appearanceFormBoundingBox.Height - borderWidth;
            return new PdfRectangle(borderWidth, borderWidth, (num2 > borderWidth) ? num2 : (borderWidth + 1.0), (num3 > borderWidth) ? num3 : (borderWidth + 1.0));
        }

        protected internal override PdfRectangle GetAppearanceFormBoundingBox()
        {
            if (this.appearanceCharacteristics != null)
            {
                double rotationAngle = this.appearanceCharacteristics.RotationAngle;
                if ((rotationAngle == 90.0) || (rotationAngle == 270.0))
                {
                    PdfRectangle rect = base.Rect;
                    return new PdfRectangle(0.0, 0.0, rect.Height, rect.Width);
                }
            }
            return base.GetAppearanceFormBoundingBox();
        }

        private PdfInteractiveFormField ResolveInteractiveFormField(PdfObjectReference reference)
        {
            PdfDocumentCatalog documentCatalog = base.DocumentCatalog;
            PdfObjectCollection objects = documentCatalog.Objects;
            PdfReaderDictionary dictionary = objects.TryResolve(reference, null) as PdfReaderDictionary;
            if (dictionary == null)
            {
                return null;
            }
            PdfObjectReference objectReference = dictionary.GetObjectReference("Parent");
            return objects.GetInteractiveFormField(documentCatalog.AcroForm, (objectReference == null) ? null : this.ResolveInteractiveFormField(objectReference), reference);
        }

        protected override bool ShouldCreateAppearance(PdfForm form)
        {
            if (base.ShouldCreateAppearance(form))
            {
                return true;
            }
            PdfInteractiveFormField interactiveFormField = this.InteractiveFormField;
            if (interactiveFormField == null)
            {
                return false;
            }
            PdfInteractiveForm form2 = interactiveFormField.Form;
            return ((form2 != null) && form2.NeedAppearances);
        }

        public PdfAnnotationHighlightingMode HighlightingMode =>
            this.highlightingMode;

        public PdfAction Action =>
            this.action;

        public PdfAnnotationActions Actions =>
            this.actions?.AnnotationActions;

        public PdfAnnotationBorderStyle BorderStyle =>
            this.borderStyle;

        public PdfWidgetAppearanceCharacteristics AppearanceCharacteristics =>
            this.appearanceCharacteristics;

        public PdfInteractiveFormField InteractiveFormField
        {
            get
            {
                this.interactiveFormField ??= this.ResolveInteractiveFormField(new PdfObjectReference(base.ObjectNumber));
                return this.interactiveFormField;
            }
            internal set => 
                this.interactiveFormField = value;
        }

        internal PdfColor BackgroundColor =>
            this.appearanceCharacteristics?.BackgroundColor;

        internal string OnAppearanceName
        {
            get
            {
                PdfAnnotationAppearances appearance = base.Appearance;
                if (appearance != null)
                {
                    IList<string> names = appearance.Normal.GetNames(null);
                    if (names.Count == 1)
                    {
                        return names[0];
                    }
                    if (names.Count > 1)
                    {
                        return ((names[0] == "Off") ? names[1] : names[0]);
                    }
                }
                return "Off";
            }
        }

        internal double BorderWidth
        {
            get
            {
                if (this.borderStyle != null)
                {
                    return this.borderStyle.BorderWidth;
                }
                PdfAnnotationBorder border = base.Border;
                return ((border == null) ? 0.0 : border.LineWidth);
            }
        }

        protected override string AnnotationType =>
            "Widget";

        protected override bool ShouldUseDefaultForm
        {
            get
            {
                PdfInteractiveFormField interactiveFormField = this.InteractiveFormField;
                return ((interactiveFormField == null) || interactiveFormField.ShouldUseDefaultAppearanceForm);
            }
        }
    }
}

