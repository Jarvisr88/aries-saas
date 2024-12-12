namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfWidgetAnnotationRemover : PdfDisposableObject
    {
        private readonly IList<PdfAnnotation> pageAnnotations;

        public PdfWidgetAnnotationRemover(PdfPage page)
        {
            this.pageAnnotations = page.Annotations;
        }

        protected override void Dispose(bool disposing)
        {
        }

        protected virtual bool RemoveWidget(PdfWidgetAnnotation widget) => 
            this.pageAnnotations.Remove(widget);

        public bool RemoveWidgets(ICollection<PdfAnnotation> annotationCollection)
        {
            bool flag = false;
            int num = 0;
            while (num < this.pageAnnotations.Count)
            {
                PdfWidgetAnnotation item = this.pageAnnotations[num] as PdfWidgetAnnotation;
                if ((item == null) || !annotationCollection.Contains(item))
                {
                    num++;
                    continue;
                }
                flag = true;
                this.RemoveWidget(item);
            }
            return flag;
        }
    }
}

