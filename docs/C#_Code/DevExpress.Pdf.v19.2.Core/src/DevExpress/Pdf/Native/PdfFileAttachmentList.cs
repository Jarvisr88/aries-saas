namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PdfFileAttachmentList : IEnumerable<PdfFileAttachment>, IEnumerable
    {
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly PdfDeferredSortedDictionary<string, PdfFileSpecification> embeddedFiles;
        private List<PdfFileAttachmentAnnotation> fileAttachmentAnnotations;
        private PdfProgressChangedEventHandler searchAttachmentProgressChanged;

        public PdfFileAttachmentList(PdfDocumentCatalog documentCatalog)
        {
            this.documentCatalog = documentCatalog;
            this.embeddedFiles = (PdfDeferredSortedDictionary<string, PdfFileSpecification>) documentCatalog.Names.EmbeddedFiles;
        }

        public void Add(PdfFileAttachment item)
        {
            string fileName = item.FileName;
            if (string.IsNullOrEmpty(fileName) || this.embeddedFiles.ContainsKey(fileName))
            {
                fileName = PdfNames.NewKey<PdfFileSpecification>(this.embeddedFiles);
            }
            this.embeddedFiles.Add(fileName, item.FileSpecification);
        }

        public bool Delete(PdfFileAttachment item)
        {
            this.SearchFileAttachmentAnnotation();
            string key = null;
            foreach (KeyValuePair<string, PdfFileSpecification> pair in this.embeddedFiles)
            {
                if ((pair.Value != null) && ReferenceEquals(pair.Value.Attachment, item))
                {
                    key = pair.Key;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(key))
            {
                return this.embeddedFiles.Remove(key);
            }
            PdfFileAttachmentAnnotation annotation = null;
            foreach (PdfFileAttachmentAnnotation annotation2 in this.fileAttachmentAnnotations)
            {
                if (ReferenceEquals(annotation2.FileSpecification.Attachment, item))
                {
                    annotation = annotation2;
                    break;
                }
            }
            if (annotation == null)
            {
                return false;
            }
            annotation.Page.Annotations.Remove(annotation);
            return this.fileAttachmentAnnotations.Remove(annotation);
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__11))]
        public IEnumerator<PdfFileAttachment> GetEnumerator()
        {
            IEnumerator<PdfFileSpecification> <>7__wrap2;
            this.SearchFileAttachmentAnnotation();
            List<PdfFileAttachmentAnnotation>.Enumerator enumerator = this.fileAttachmentAnnotations.GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                PdfFileAttachmentAnnotation current = enumerator.Current;
                yield return current.FileSpecification.Attachment;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = new List<PdfFileAttachmentAnnotation>.Enumerator();
                <>7__wrap2 = this.embeddedFiles.Values.GetEnumerator();
            }
            if (!<>7__wrap2.MoveNext())
            {
                <>7__wrap2 = null;
                yield break;
            }
            else
            {
                PdfFileSpecification current = <>7__wrap2.Current;
                yield return current.Attachment;
                yield break;
            }
        }

        public void InvalidateSearchedFileAnnotations()
        {
            this.fileAttachmentAnnotations = null;
        }

        private void SearchFileAttachmentAnnotation()
        {
            if (this.fileAttachmentAnnotations == null)
            {
                List<PdfFileAttachmentAnnotation> list = new List<PdfFileAttachmentAnnotation>();
                int num = 1;
                foreach (PdfPage page in (IEnumerable<PdfPage>) this.documentCatalog.Pages)
                {
                    foreach (PdfAnnotation annotation in page.Annotations)
                    {
                        PdfFileAttachmentAnnotation item = annotation as PdfFileAttachmentAnnotation;
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }
                    if (this.searchAttachmentProgressChanged != null)
                    {
                        this.searchAttachmentProgressChanged(page, new PdfProgressChangedEventArgs(num++));
                    }
                }
                this.fileAttachmentAnnotations = list;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public PdfProgressChangedEventHandler SearchAttachmentProgressChanged
        {
            get => 
                this.searchAttachmentProgressChanged;
            set => 
                this.searchAttachmentProgressChanged = value;
        }

    }
}

