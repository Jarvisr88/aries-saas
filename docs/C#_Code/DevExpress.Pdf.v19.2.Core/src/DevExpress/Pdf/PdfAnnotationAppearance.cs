namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfAnnotationAppearance
    {
        private PdfForm defaultForm;
        private Dictionary<string, PdfForm> forms;

        internal PdfAnnotationAppearance()
        {
        }

        internal PdfAnnotationAppearance(PdfDocumentCatalog documentCatalog, PdfRectangle bBox)
        {
            this.defaultForm = new PdfForm(documentCatalog, bBox);
        }

        private PdfAnnotationAppearance(PdfForm defaultForm, Dictionary<string, PdfForm> forms)
        {
            this.defaultForm = defaultForm;
            this.forms = forms;
        }

        internal List<string> GetNames(string defaultName)
        {
            if (this.forms != null)
            {
                return new List<string>(this.forms.Keys);
            }
            List<string> list1 = new List<string>();
            list1.Add(defaultName);
            return list1;
        }

        internal static PdfAnnotationAppearance Parse(PdfReaderDictionary dictionary, string key)
        {
            object obj2;
            if (!dictionary.TryGetValue(key, out obj2))
            {
                return null;
            }
            PdfObjectCollection objects = dictionary.Objects;
            obj2 = objects.TryResolve(obj2, null);
            if (obj2 == null)
            {
                return null;
            }
            PdfForm defaultForm = null;
            Dictionary<string, PdfForm> forms = null;
            PdfReaderStream stream = obj2 as PdfReaderStream;
            if (stream != null)
            {
                defaultForm = objects.GetForm(stream);
                if (defaultForm == null)
                {
                    return null;
                }
            }
            else
            {
                PdfReaderDictionary dictionary3 = obj2 as PdfReaderDictionary;
                if (dictionary3 == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                forms = new Dictionary<string, PdfForm>();
                using (Dictionary<string, object>.Enumerator enumerator = dictionary3.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        KeyValuePair<string, object> current = enumerator.Current;
                        string str = current.Key;
                        obj2 = current.Value;
                        if (obj2 == null)
                        {
                            forms.Add(str, null);
                            continue;
                        }
                        PdfObjectReference reference = current.Value as PdfObjectReference;
                        if (reference != null)
                        {
                            PdfForm form = objects.GetForm(reference);
                            if (form != null)
                            {
                                forms.Add(str, form);
                            }
                            if ((str == "On") || ((defaultForm == null) && (str != "Off")))
                            {
                                defaultForm = form;
                            }
                            continue;
                        }
                        return null;
                    }
                }
            }
            return new PdfAnnotationAppearance(defaultForm, forms);
        }

        internal void SetForm(string name, PdfForm form)
        {
            if (string.IsNullOrEmpty(name))
            {
                this.defaultForm = form;
            }
            else
            {
                this.forms ??= new Dictionary<string, PdfForm>();
                this.forms[name] = form;
            }
        }

        internal object ToWritableObject(PdfObjectCollection collection) => 
            (this.forms == null) ? ((object) collection.AddObject((PdfObject) this.defaultForm)) : ((object) PdfElementsDictionaryWriter.Write(this.forms, value => collection.AddObject((PdfObject) value)));

        public PdfForm DefaultForm =>
            this.defaultForm;

        public IDictionary<string, PdfForm> Forms =>
            this.forms;
    }
}

