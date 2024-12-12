namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PdfInteractiveForm : PdfObject
    {
        private const string fieldsKey = "Fields";
        private const string resourceKey = "DR";
        private const string needAppearancesKey = "NeedAppearances";
        private const string signatureFlagsKey = "SigFlags";
        private const string xfaKey = "XFA";
        private const string calculationOrderKey = "CO";
        private readonly PdfInteractiveFormFieldCollection fields;
        private readonly byte[] defaultAppearanceCommandsData;
        private readonly PdfCommandList defaultAppearanceCommands;
        private readonly PdfTextJustification defaultTextJustification;
        private readonly PdfXFAForm xfaForm;
        private readonly IList<PdfInteractiveFormField> calculationOrder;
        private bool needAppearances;
        private PdfSignatureFlags signatureFlags;
        private PdfResources resources;

        internal event PdfInteractiveFormFieldValueChangedEventHandler FormFieldValueChanged;

        internal event PdfInteractiveFormFieldValueChangingEventHandler FormFieldValueChanging;

        internal PdfInteractiveForm(PdfDocumentCatalog documentCatalog)
        {
            this.fields = new PdfInteractiveFormFieldCollection();
            this.resources = new PdfResources(documentCatalog, false);
        }

        internal PdfInteractiveForm(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            object obj2;
            PdfObjectCollection objects = dictionary.Objects;
            PdfDocumentCatalog documentCatalog = objects.DocumentCatalog;
            this.resources = dictionary.GetResources("DR", null, false, false);
            this.fields = new PdfInteractiveFormFieldCollection(dictionary.GetArray("Fields"), this, null, objects);
            this.calculationOrder = dictionary.GetArray<PdfInteractiveFormField>("CO", o => objects.GetInteractiveFormField(this, null, o));
            bool? boolean = dictionary.GetBoolean("NeedAppearances");
            this.needAppearances = (boolean != null) ? boolean.GetValueOrDefault() : false;
            int? integer = dictionary.GetInteger("SigFlags");
            this.signatureFlags = (integer != null) ? ((PdfSignatureFlags) integer.GetValueOrDefault()) : PdfSignatureFlags.None;
            this.defaultAppearanceCommandsData = dictionary.GetBytes("DA");
            this.defaultAppearanceCommands = dictionary.GetAppearance(this.resources);
            this.defaultTextJustification = dictionary.GetTextJustification();
            if (dictionary.TryGetValue("XFA", out obj2))
            {
                obj2 = objects.TryResolve(obj2, null);
                IList<object> array = obj2 as IList<object>;
                if (array == null)
                {
                    PdfReaderStream stream = obj2 as PdfReaderStream;
                    if (stream == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.xfaForm = new PdfXFAForm(stream.UncompressedData);
                }
                else
                {
                    this.xfaForm = new PdfXFAForm(objects, array);
                }
            }
        }

        internal void AddInteractiveFormField(PdfInteractiveFormField formField)
        {
            this.fields.AddFieldWithAncestors(formField);
        }

        internal void RaiseFormFieldValueChanged(string fieldName, object oldValue, object newValue)
        {
            if (this.FormFieldValueChanged != null)
            {
                this.FormFieldValueChanged(this, new PdfInteractiveFormFieldValueChangedEventArgs(fieldName, oldValue, newValue));
            }
        }

        internal bool RaiseFormFieldValueChanging(PdfInteractiveFormFieldValueChangingEventArgs args)
        {
            if (this.FormFieldValueChanging == null)
            {
                return true;
            }
            this.FormFieldValueChanging(this, args);
            return !args.Cancel;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("DR", this.resources);
            dictionary.Add("NeedAppearances", this.needAppearances, false);
            dictionary.Add("SigFlags", (int) this.signatureFlags, 0);
            dictionary.Add("Fields", this.fields);
            if (this.defaultAppearanceCommands != null)
            {
                dictionary.Add("DA", this.defaultAppearanceCommandsData);
            }
            dictionary.Add("Q", PdfEnumToValueConverter.Convert<PdfTextJustification>(this.defaultTextJustification));
            if (this.xfaForm != null)
            {
                dictionary.Add("XFA", this.xfaForm.Write(objects));
            }
            if (this.calculationOrder != null)
            {
                dictionary.Add("CO", new PdfWritableObjectArray((IEnumerable<PdfObject>) this.calculationOrder, objects));
            }
            return dictionary;
        }

        public IList<PdfInteractiveFormField> Fields =>
            this.fields;

        public IEnumerable<PdfCommand> DefaultAppearanceCommands =>
            this.defaultAppearanceCommands;

        public PdfTextJustification DefaultTextJustification =>
            this.defaultTextJustification;

        public PdfXFAForm XFAForm =>
            this.xfaForm;

        public bool NeedAppearances
        {
            get => 
                this.needAppearances;
            internal set => 
                this.needAppearances = value;
        }

        public PdfSignatureFlags SignatureFlags
        {
            get => 
                this.signatureFlags;
            internal set => 
                this.signatureFlags = value;
        }

        internal byte[] DefaultAppearanceCommandsData =>
            this.defaultAppearanceCommandsData;

        internal IList<PdfInteractiveFormField> CalculationOrder =>
            this.calculationOrder;

        internal PdfResources Resources =>
            this.resources;
    }
}

