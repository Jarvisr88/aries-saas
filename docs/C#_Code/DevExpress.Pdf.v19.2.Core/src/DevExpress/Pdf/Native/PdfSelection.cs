namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfSelection
    {
        protected PdfSelection()
        {
        }

        public static bool AreEqual(PdfSelection selection1, PdfSelection selection2)
        {
            if (selection1 == null)
            {
                return ReferenceEquals(selection2, null);
            }
            if (selection2 == null)
            {
                return false;
            }
            PdfDocumentContentType contentType = selection1.ContentType;
            return ((contentType == selection2.ContentType) ? ((contentType == PdfDocumentContentType.Text) ? PdfTextSelection.AreEqual((PdfTextSelection) selection1, (PdfTextSelection) selection2) : ((contentType == PdfDocumentContentType.Image) ? PdfImageSelection.AreEqual((PdfImageSelection) selection1, (PdfImageSelection) selection2) : true)) : false);
        }

        public bool Contains(PdfDocumentPosition position)
        {
            bool flag;
            int pageIndex = position.PageIndex;
            PdfPoint point = position.Point;
            using (IEnumerator<PdfHighlight> enumerator = this.Highlights.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfHighlight current = enumerator.Current;
                        if (current.PageIndex != pageIndex)
                        {
                            continue;
                        }
                        using (IEnumerator<PdfOrientedRectangle> enumerator2 = current.Rectangles.GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator2.MoveNext())
                                {
                                    break;
                                }
                                PdfOrientedRectangle rectangle = enumerator2.Current;
                                if (rectangle.PointIsInRect(point, 0.0, 0.0))
                                {
                                    return true;
                                }
                            }
                        }
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public abstract PdfDocumentContentType ContentType { get; }

        public abstract IList<PdfHighlight> Highlights { get; }
    }
}

