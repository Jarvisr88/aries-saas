namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfAction : PdfObject
    {
        internal const string DictionaryType = "Action";
        internal const string ActionTypeDictionaryKey = "S";
        private const string nextActionDictionaryKey = "Next";
        private readonly PdfDocumentCatalog documentCatalog;
        private List<PdfAction> next;
        private object nextValue;

        protected PdfAction(PdfDocumentCatalog documentCatalog)
        {
            this.documentCatalog = documentCatalog;
        }

        protected PdfAction(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.documentCatalog = dictionary.Objects.DocumentCatalog;
            if (!dictionary.TryGetValue("Next", out this.nextValue))
            {
                this.nextValue = null;
            }
        }

        protected PdfAction(PdfDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            this.documentCatalog = document.DocumentCatalog;
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("Action"));
            dictionary.Add("S", new PdfName(this.ActionType));
            this.EnsureNextActions();
            if (this.next != null)
            {
                if (this.next.Count == 1)
                {
                    dictionary.Add("Next", this.next[0]);
                }
                else
                {
                    dictionary.AddList<PdfAction>("Next", this.next);
                }
            }
            return dictionary;
        }

        private void EnsureNextActions()
        {
            if ((this.next == null) && (this.nextValue != null))
            {
                PdfObjectCollection objects = this.documentCatalog.Objects;
                IList<object> list = objects.TryResolve(this.nextValue, null) as IList<object>;
                if (list == null)
                {
                    this.next = new List<PdfAction>(1);
                    this.next.Add(objects.GetAction(this.nextValue));
                }
                else
                {
                    this.next = new List<PdfAction>(list.Count);
                    foreach (object obj2 in list)
                    {
                        this.next.Add(objects.GetAction(obj2));
                    }
                }
                this.nextValue = null;
            }
        }

        protected internal virtual void Execute(IPdfInteractiveOperationController interactiveOperationController, IList<PdfPage> pages)
        {
        }

        internal static PdfAction Parse(PdfReaderDictionary actionDictionary)
        {
            if (actionDictionary != null)
            {
                string name = actionDictionary.GetName("S");
                if (name != null)
                {
                    uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
                    if (num <= 0x7a696b1d)
                    {
                        if (num <= 0x5bc5628c)
                        {
                            if (num <= 0x37cbb286)
                            {
                                if (num != 0x11011371)
                                {
                                    if ((num == 0x37cbb286) && (name == "Launch"))
                                    {
                                        return new PdfLaunchAction(actionDictionary);
                                    }
                                }
                                else if (name == "Trans")
                                {
                                    return new PdfTransitionAction(actionDictionary);
                                }
                            }
                            else if (num != 0x4e301883)
                            {
                                if ((num == 0x5bc5628c) && (name == "GoTo3DView"))
                                {
                                    return new PdfGoTo3dViewAction(actionDictionary);
                                }
                            }
                            else if (name == "Rendition")
                            {
                                return new PdfRenditionAction(actionDictionary);
                            }
                        }
                        else if (num <= 0x6636d68a)
                        {
                            if (num != 0x6055a346)
                            {
                                if ((num == 0x6636d68a) && (name == "JavaScript"))
                                {
                                    return new PdfJavaScriptAction(actionDictionary);
                                }
                            }
                            else if (name == "Named")
                            {
                                return new PdfNamedAction(actionDictionary);
                            }
                        }
                        else if (num == 0x753e52e7)
                        {
                            if (name == "SubmitForm")
                            {
                                return new PdfSubmitFormAction(actionDictionary);
                            }
                        }
                        else if (num != 0x761f0de6)
                        {
                            if ((num == 0x7a696b1d) && (name == "URI"))
                            {
                                return new PdfUriAction(actionDictionary);
                            }
                        }
                        else if (name == "GoTo")
                        {
                            return new PdfGoToAction(actionDictionary);
                        }
                    }
                    else if (num <= 0x95e27799)
                    {
                        if (num <= 0x91adcebc)
                        {
                            if (num != 0x8368886f)
                            {
                                if ((num == 0x91adcebc) && (name == "ImportData"))
                                {
                                    return new PdfImportDataAction(actionDictionary);
                                }
                            }
                            else if (name == "Movie")
                            {
                                return new PdfMovieAction(actionDictionary);
                            }
                        }
                        else if (num != 0x91e26c5b)
                        {
                            if ((num == 0x95e27799) && (name == "GoToE"))
                            {
                                return new PdfEmbeddedGoToAction(actionDictionary);
                            }
                        }
                        else if (name == "SetOCGState")
                        {
                            return new PdfSetOcgStateAction(actionDictionary);
                        }
                    }
                    else if (num <= 0xa6e2925c)
                    {
                        if (num != 0x99a35366)
                        {
                            if ((num == 0xa6e2925c) && (name == "GoToR"))
                            {
                                return new PdfRemoteGoToAction(actionDictionary);
                            }
                        }
                        else if (name == "ResetForm")
                        {
                            return new PdfResetFormAction(actionDictionary);
                        }
                    }
                    else if (num == 0xb3ca9a34)
                    {
                        if (name == "Sound")
                        {
                            return new PdfSoundAction(actionDictionary);
                        }
                    }
                    else if (num != 0xe10c7bcd)
                    {
                        if ((num == 0xea904991) && (name == "Thread"))
                        {
                            return new PdfThreadAction(actionDictionary);
                        }
                    }
                    else if (name == "Hide")
                    {
                        return new PdfHideAction(actionDictionary);
                    }
                    return null;
                }
            }
            return null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        public IEnumerable<PdfAction> Next
        {
            get
            {
                this.EnsureNextActions();
                return this.next;
            }
        }

        internal PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;

        protected abstract string ActionType { get; }
    }
}

