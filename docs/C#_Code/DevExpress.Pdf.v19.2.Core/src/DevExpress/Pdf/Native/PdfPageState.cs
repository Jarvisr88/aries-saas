namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfPageState
    {
        private readonly PdfDocumentStateBase documentState;
        private readonly PdfPage page;
        private readonly int pageIndex;
        private readonly Lazy<PdfObservableCollection<PdfAnnotationState>> annotationStates;

        public PdfPageState(PdfDocumentStateBase documentState, PdfPage page, int pageIndex)
        {
            this.documentState = documentState;
            this.page = page;
            this.pageIndex = pageIndex;
            this.annotationStates = new Lazy<PdfObservableCollection<PdfAnnotationState>>(new Func<PdfObservableCollection<PdfAnnotationState>>(this.CreateAnnotationStates));
        }

        private PdfObservableCollection<PdfAnnotationState> CreateAnnotationStates()
        {
            List<PdfAnnotationState> collection = new List<PdfAnnotationState>();
            try
            {
                PdfAnnotationStateFactory factory = new PdfAnnotationStateFactory(this);
                foreach (PdfAnnotation annotation in this.page.Annotations)
                {
                    PdfAnnotationState item = factory.Create(annotation);
                    if (item != null)
                    {
                        collection.Add(item);
                    }
                }
            }
            catch
            {
            }
            return new PdfObservableCollection<PdfAnnotationState>(collection);
        }

        public IList<PdfMarkupAnnotationData> CreateMarkupAnnotationData()
        {
            PdfMarkupAnnotationDataFactory factory = new PdfMarkupAnnotationDataFactory();
            IList<PdfMarkupAnnotationData> list = new List<PdfMarkupAnnotationData>();
            foreach (PdfAnnotationState state in this.AnnotationStates)
            {
                PdfMarkupAnnotationData item = factory.Create(state);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public PdfSize GetViewSize() => 
            this.page.GetSize(this.documentState.RotationAngle);

        public PdfDocumentStateBase DocumentState =>
            this.documentState;

        public PdfPage Page =>
            this.page;

        public int PageIndex =>
            this.pageIndex;

        public PdfObservableCollection<PdfAnnotationState> AnnotationStates =>
            this.annotationStates.Value;
    }
}

