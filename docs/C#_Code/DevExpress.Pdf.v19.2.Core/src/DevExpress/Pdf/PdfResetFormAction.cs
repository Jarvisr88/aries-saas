namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfResetFormAction : PdfAction
    {
        internal const string Name = "ResetForm";
        private const string fieldsDictionaryKey = "Fields";
        private const string flagsDictionaryKey = "Flags";
        private const int excludeFieldsValue = 1;
        private const int defaultFlag = 0;
        private readonly int flags;
        private List<PdfInteractiveFormField> fields;
        private IList<object> formFieldObjects;
        private PdfInteractiveFormFieldsList fieldsList;

        internal PdfResetFormAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.formFieldObjects = dictionary.GetArray("Fields");
            if (this.formFieldObjects == null)
            {
                PdfObjectReference objectReference = dictionary.GetObjectReference("Fields");
                if (objectReference != null)
                {
                    this.fieldsList = dictionary.Objects.GetResolvedObject<PdfInteractiveFormFieldsList>(objectReference.Number);
                }
            }
            int? integer = dictionary.GetInteger("Flags");
            this.flags = (integer != null) ? integer.GetValueOrDefault() : 0;
            if (this.flags < 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            this.FillFormFields();
            if ((this.fieldsList == null) && ((this.fields == null) || (this.fields.Count <= 0)))
            {
                if (this.flags == 1)
                {
                    dictionary.Add("Fields", new PdfWritableArray(new object[0]));
                }
            }
            else
            {
                PdfInteractiveFormFieldsList fieldsList = this.fieldsList;
                if (this.fieldsList == null)
                {
                    PdfInteractiveFormFieldsList local1 = this.fieldsList;
                    fieldsList = new PdfDeferredInteractiveFormFieldsList(this.fields);
                }
                dictionary.Add("Fields", fieldsList);
            }
            dictionary.Add("Flags", this.flags, 0);
            return dictionary;
        }

        protected internal override void Execute(IPdfInteractiveOperationController interactiveOperationController, IList<PdfPage> pages)
        {
            IEnumerable<PdfInteractiveFormField> fields = this.Fields;
            if (fields == null)
            {
                interactiveOperationController.ResetForm();
            }
            else if (this.ExcludeFields)
            {
                interactiveOperationController.ResetFormExcludingFields(fields);
            }
            else
            {
                interactiveOperationController.ResetFormFields(fields);
            }
        }

        private void FillFormFields()
        {
            if (this.formFieldObjects != null)
            {
                this.fields = new List<PdfInteractiveFormField>(this.formFieldObjects.Count);
                PdfDocumentCatalog documentCatalog = base.DocumentCatalog;
                PdfObjectCollection objects = documentCatalog.Objects;
                PdfInteractiveForm acroForm = documentCatalog.AcroForm;
                if (acroForm != null)
                {
                    foreach (object obj2 in this.formFieldObjects)
                    {
                        PdfInteractiveFormField resolvedInteractiveFormField;
                        PdfObjectReference reference = obj2 as PdfObjectReference;
                        if (reference != null)
                        {
                            resolvedInteractiveFormField = objects.GetResolvedInteractiveFormField(reference);
                        }
                        else
                        {
                            byte[] buffer = obj2 as byte[];
                            if (buffer == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            string str = PdfDocumentReader.ConvertToString(buffer);
                            resolvedInteractiveFormField = string.IsNullOrEmpty(str) ? null : FindFieldByName(str, acroForm.Fields);
                        }
                        if (resolvedInteractiveFormField != null)
                        {
                            this.fields.Add(resolvedInteractiveFormField);
                        }
                    }
                }
                if (this.fields.Count == 0)
                {
                    this.fields = null;
                }
                this.formFieldObjects = null;
            }
        }

        private static PdfInteractiveFormField FindFieldByName(string fullName, IList<PdfInteractiveFormField> fields)
        {
            if (fields != null)
            {
                using (IEnumerator<PdfInteractiveFormField> enumerator = fields.GetEnumerator())
                {
                    while (true)
                    {
                        PdfInteractiveFormField field2;
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PdfInteractiveFormField current = enumerator.Current;
                        if (current.FullName == fullName)
                        {
                            field2 = current;
                        }
                        else
                        {
                            IList<PdfInteractiveFormField> kids = current.Kids;
                            if (kids == null)
                            {
                                continue;
                            }
                            PdfInteractiveFormField field3 = FindFieldByName(fullName, kids);
                            if (field3 == null)
                            {
                                continue;
                            }
                            field2 = field3;
                        }
                        return field2;
                    }
                }
            }
            return null;
        }

        public bool ExcludeFields =>
            (this.flags & 1) == 1;

        public IEnumerable<PdfInteractiveFormField> Fields
        {
            get
            {
                if (this.fieldsList != null)
                {
                    return this.fieldsList.GetFormFields(base.DocumentCatalog.Objects);
                }
                this.FillFormFields();
                return this.fields;
            }
        }

        protected override string ActionType =>
            "ResetForm";
    }
}

