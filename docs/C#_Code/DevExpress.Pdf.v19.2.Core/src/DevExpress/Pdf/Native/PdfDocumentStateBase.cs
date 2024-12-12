namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class PdfDocumentStateBase : PdfDisposableObject
    {
        private readonly PdfDocument document;
        private readonly PdfSelectionState selectionState;
        private readonly IDictionary<int, PdfPageState> pageStates = new Dictionary<int, PdfPageState>();
        private readonly IDictionary<string, byte[]> fileAttachmentIconDictionary = new Dictionary<string, byte[]>();
        private PdfFormData formData;
        private List<PdfFileAttachmentListItem> fileAttachments;
        private PdfOutlineTreeListItemCollection outlines;
        private int rotationAngle;
        private bool highlightFormFields;
        private PdfRgbaColor highlightedFormFieldColor = new PdfRgbaColor(0.804, 0.843, 1.0, 1.0);
        private PdfAnnotationState focusedAnnotation;
        private PdfAnnotationState selectedAnnotation;
        private bool isDocumentModified;

        public event PdfDocumentStateChangedEventHandler DocumentStateChanged;

        protected PdfDocumentStateBase(PdfDocument document)
        {
            this.document = document;
            this.selectionState = new PdfSelectionState();
            this.selectionState.SelectionChanged += new EventHandler(this.OnSelectionChanged);
            document.DocumentCatalog.Pages.Changed += new EventHandler(this.OnPagesChanged);
            PdfInteractiveForm acroForm = document.AcroForm;
            if (acroForm != null)
            {
                acroForm.FormFieldValueChanged += new PdfInteractiveFormFieldValueChangedEventHandler(this.OnAnnotationStateChanged);
            }
        }

        public void AddFormFields(IEnumerable<PdfAcroFormField> fields)
        {
            if (fields != null)
            {
                IList<PdfAcroFormFieldNameCollision> list = this.CheckAcroFormFieldNameCollisions(fields);
                if ((list != null) && (list.Count > 0))
                {
                    throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgAcroFormFieldNameDuplication), "name");
                }
                foreach (PdfAcroFormField field in fields)
                {
                    field.CreateFormField(this.FontSearch, this.document, null);
                }
                this.formData = null;
            }
        }

        public PdfTextMarkupAnnotationState AddTextMarkupAnnotation(int pageIndex, IPdfTextMarkupAnnotationBuilder builder)
        {
            PdfPageState pageState = this[pageIndex];
            PdfTextMarkupAnnotation annotation = new PdfTextMarkupAnnotation(pageState.Page, builder);
            annotation.CreateAppearanceBuilder(this.FontSearch).RebuildAppearance(annotation.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal));
            PdfTextMarkupAnnotationState item = new PdfTextMarkupAnnotationState(pageState, annotation);
            pageState.AnnotationStates.Add(item);
            return item;
        }

        public IList<PdfAcroFormFieldNameCollision> CheckAcroFormFieldNameCollisions(IEnumerable<PdfAcroFormField> fields)
        {
            List<PdfAcroFormFieldNameCollision> infos = new List<PdfAcroFormFieldNameCollision>();
            ISet<string> existingRootFormFieldNames = this.document.DocumentCatalog.GetExistingRootFormFieldNames();
            foreach (PdfAcroFormField field in fields)
            {
                if (!existingRootFormFieldNames.Add(field.Name))
                {
                    infos.Add(new PdfAcroFormFieldNameCollision(field, existingRootFormFieldNames));
                }
                field.CollectNameCollisionInfo(infos);
            }
            return infos;
        }

        private void CreateFormData()
        {
            if (this.formData == null)
            {
                PdfInteractiveForm acroForm = this.document.AcroForm;
                if (acroForm == null)
                {
                    this.formData = new PdfFormData(false);
                }
                else
                {
                    this.formData = new PdfFormData();
                    foreach (PdfInteractiveFormField field in acroForm.Fields)
                    {
                        PdfFormData data = this.CreateFormData(field);
                        if (data != null)
                        {
                            this.formData[field.Name] = data;
                        }
                    }
                    this.formData.AllowAddNewKids = false;
                }
                if (acroForm != null)
                {
                    acroForm.NeedAppearances = false;
                }
            }
        }

        private PdfFormData CreateFormData(PdfInteractiveFormField field)
        {
            PdfWidgetAnnotation widget = field.Widget;
            if (widget != null)
            {
                widget.EnsureAppearance(this);
            }
            bool flag = field is PdfButtonFormField;
            if (flag && field.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton))
            {
                return null;
            }
            if (field.Name == null)
            {
                return null;
            }
            PdfFormData data = new PdfFormData(field, this.FontSearch);
            IList<PdfInteractiveFormField> kids = field.Kids;
            if (!flag && (kids != null))
            {
                foreach (PdfInteractiveFormField field2 in kids)
                {
                    PdfFormData kid = this.CreateFormData(field2);
                    if (kid != null)
                    {
                        data.AddKid(field2.Name, kid);
                    }
                }
            }
            return data;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.selectionState.SelectionChanged -= new EventHandler(this.OnSelectionChanged);
                this.document.DocumentCatalog.Pages.Changed -= new EventHandler(this.OnPagesChanged);
                PdfInteractiveForm acroForm = this.document.AcroForm;
                if (acroForm != null)
                {
                    acroForm.FormFieldValueChanged -= new PdfInteractiveFormFieldValueChangedEventHandler(this.OnAnnotationStateChanged);
                }
            }
        }

        public IList<string> GetFieldNames()
        {
            List<string> list = new List<string>();
            PdfInteractiveForm acroForm = this.document.AcroForm;
            if (acroForm != null)
            {
                foreach (PdfInteractiveFormField field in acroForm.Fields)
                {
                    list.AddRange(GetFieldNames(field));
                }
            }
            return list;
        }

        private static IList<string> GetFieldNames(PdfInteractiveFormField field)
        {
            List<string> list = new List<string>();
            string name = field.Name;
            if (!string.IsNullOrEmpty(name))
            {
                IList<PdfInteractiveFormField> kids = field.Kids;
                if (kids != null)
                {
                    foreach (PdfInteractiveFormField field2 in kids)
                    {
                        foreach (string str2 in GetFieldNames(field2))
                        {
                            list.Add(name + "." + str2);
                        }
                    }
                }
                if (list.Count == 0)
                {
                    list.Add(name);
                }
            }
            return list;
        }

        protected abstract byte[] GetFileAttachmentIcon(string extension);
        private void OnAnnotationStateChanged(object sender, EventArgs e)
        {
            this.isDocumentModified = true;
            this.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.Annotation);
        }

        private void OnPagesChanged(object sender, EventArgs e)
        {
            this.formData = null;
            this.pageStates.Clear();
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            this.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.Selection);
        }

        public void RaiseDocumentStateChanged(PdfDocumentStateChangedFlags flags)
        {
            PdfDocumentStateChangedEventHandler documentStateChanged = this.DocumentStateChanged;
            if (documentStateChanged != null)
            {
                documentStateChanged(this, new PdfDocumentStateChangedEventArgs(flags));
            }
        }

        public bool RemoveForm(bool flatten)
        {
            this.CreateFormData();
            bool flag = this.Document.DocumentCatalog.RemoveForm(this, flatten);
            this.formData = null;
            return flag;
        }

        public bool RemoveFormField(string name, bool flatten)
        {
            PdfInteractiveForm acroForm = this.document.AcroForm;
            if ((acroForm != null) && !string.IsNullOrEmpty(name))
            {
                string[] strArray = name.Split(PdfFormData.FieldNameDelimiter, 2);
                string str = strArray[0];
                using (IEnumerator<PdfInteractiveFormField> enumerator = acroForm.Fields.GetEnumerator())
                {
                    PdfInteractiveFormField field2;
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PdfInteractiveFormField current = enumerator.Current;
                            if (current.Name != str)
                            {
                                continue;
                            }
                            if (strArray.Length == 1)
                            {
                                current.Remove(this, flatten);
                                field2 = current;
                                break;
                            }
                            field2 = this.RemoveFormField(current, strArray[1], flatten);
                            if (field2 != null)
                            {
                                break;
                            }
                        }
                        goto TR_0003;
                    }
                    if (!(field2 is PdfButtonFormField) || !field2.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton))
                    {
                        this.formData = null;
                    }
                    return true;
                }
            }
        TR_0003:
            return false;
        }

        private PdfInteractiveFormField RemoveFormField(PdfInteractiveFormField field, string fieldNameToRemove, bool flatten)
        {
            IList<PdfInteractiveFormField> kids = field.Kids;
            if ((kids != null) && (kids.Count > 0))
            {
                string[] strArray = fieldNameToRemove.Split(PdfFormData.FieldNameDelimiter, 2);
                string str = strArray[0];
                using (IEnumerator<PdfInteractiveFormField> enumerator = kids.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PdfInteractiveFormField current = enumerator.Current;
                        if (current.Name == str)
                        {
                            PdfInteractiveFormField field3;
                            if (strArray.Length != 1)
                            {
                                field3 = this.RemoveFormField(current, strArray[1], flatten);
                            }
                            else
                            {
                                current.Remove(this, flatten);
                                field3 = current;
                            }
                            return field3;
                        }
                    }
                }
            }
            return null;
        }

        public PdfDocument Document =>
            this.document;

        public PdfSelectionState SelectionState =>
            this.selectionState;

        public int PageCount =>
            this.document.Pages.Count;

        public PdfPageState this[int pageIndex]
        {
            get
            {
                PdfPageState state;
                IList<PdfPage> pages = this.document.Pages;
                if ((pageIndex < 0) || (pageIndex >= pages.Count))
                {
                    return null;
                }
                if (!this.pageStates.TryGetValue(pageIndex, out state))
                {
                    state = new PdfPageState(this, pages[pageIndex], pageIndex);
                    this.pageStates.Add(pageIndex, state);
                }
                return state;
            }
        }

        public PdfFormData FormData
        {
            get
            {
                this.CreateFormData();
                return this.formData;
            }
        }

        public IList<PdfFileAttachmentListItem> FileAttachments
        {
            get
            {
                if (this.fileAttachments == null)
                {
                    try
                    {
                        this.fileAttachments = new List<PdfFileAttachmentListItem>();
                        foreach (PdfFileAttachment attachment in this.document.FileAttachments)
                        {
                            byte[] fileAttachmentIcon;
                            string extension = Path.GetExtension(attachment.FileName);
                            if (!this.fileAttachmentIconDictionary.TryGetValue(extension, out fileAttachmentIcon))
                            {
                                try
                                {
                                    fileAttachmentIcon = this.GetFileAttachmentIcon(extension);
                                    this.fileAttachmentIconDictionary.Add(extension, fileAttachmentIcon);
                                }
                                catch
                                {
                                    fileAttachmentIcon = null;
                                }
                            }
                            this.fileAttachments.Add(new PdfFileAttachmentListItem(attachment, fileAttachmentIcon));
                        }
                        Comparison<PdfFileAttachmentListItem> comparison = <>c.<>9__25_0;
                        if (<>c.<>9__25_0 == null)
                        {
                            Comparison<PdfFileAttachmentListItem> local2 = <>c.<>9__25_0;
                            comparison = <>c.<>9__25_0 = (item1, item2) => item1.FileName.CompareTo(item2.FileName);
                        }
                        this.fileAttachments.Sort(comparison);
                    }
                    catch
                    {
                        this.fileAttachments = new List<PdfFileAttachmentListItem>();
                    }
                }
                return this.fileAttachments;
            }
        }

        public PdfOutlineTreeListItemCollection Outlines
        {
            get
            {
                this.outlines ??= new PdfOutlineTreeListItemCollection(this.document);
                return this.outlines;
            }
        }

        public int RotationAngle
        {
            get => 
                this.rotationAngle;
            set
            {
                if (value != this.rotationAngle)
                {
                    this.rotationAngle = value;
                    this.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.AllContent);
                }
            }
        }

        public bool HighlightFormFields
        {
            get => 
                this.highlightFormFields;
            set
            {
                if (value != this.highlightFormFields)
                {
                    this.highlightFormFields = value;
                    this.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.Annotation);
                }
            }
        }

        public PdfRgbaColor HighlightedFormFieldColor
        {
            get => 
                this.highlightedFormFieldColor;
            set
            {
                if (!ReferenceEquals(value, this.highlightedFormFieldColor))
                {
                    this.highlightedFormFieldColor = value;
                    this.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.Annotation);
                }
            }
        }

        public PdfRgbaColor ActualHighlightColor =>
            this.HighlightFormFields ? this.HighlightedFormFieldColor : null;

        public PdfAnnotationState FocusedAnnotation
        {
            get => 
                this.focusedAnnotation;
            set
            {
                if (!ReferenceEquals(this.focusedAnnotation, value))
                {
                    this.focusedAnnotation = value;
                    this.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.Annotation);
                }
            }
        }

        public PdfAnnotationState SelectedAnnotation
        {
            get => 
                this.selectedAnnotation;
            set
            {
                if (!ReferenceEquals(this.selectedAnnotation, value))
                {
                    this.selectedAnnotation = value;
                    this.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.Annotation);
                }
            }
        }

        public virtual IPdfExportFontProvider FontSearch =>
            null;

        public bool IsDocumentModified
        {
            get => 
                this.isDocumentModified;
            set => 
                this.isDocumentModified = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfDocumentStateBase.<>c <>9 = new PdfDocumentStateBase.<>c();
            public static Comparison<PdfFileAttachmentListItem> <>9__25_0;

            internal int <get_FileAttachments>b__25_0(PdfFileAttachmentListItem item1, PdfFileAttachmentListItem item2) => 
                item1.FileName.CompareTo(item2.FileName);
        }
    }
}

