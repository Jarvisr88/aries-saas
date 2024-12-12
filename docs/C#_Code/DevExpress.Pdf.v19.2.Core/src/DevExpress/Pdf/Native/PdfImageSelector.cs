namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfImageSelector
    {
        private const double selectionPrecision = 0.001;
        private readonly IPdfInteractiveOperationController controller;
        private readonly PdfPageDataCache pageDataCache;
        private readonly PdfDocumentStateBase documentState;
        private readonly PdfSelectionState selectionState;
        private bool selectionInProgress;
        private PdfDocumentPosition selectionStartPosition;
        private int selectionStartImageIndex;

        public PdfImageSelector(IPdfInteractiveOperationController controller, PdfPageDataCache pageDataCache, PdfDocumentStateBase documentState)
        {
            PdfPoint point = new PdfPoint();
            this.selectionStartPosition = new PdfDocumentPosition(0, point);
            this.selectionStartImageIndex = -1;
            this.controller = controller;
            this.pageDataCache = pageDataCache;
            this.documentState = documentState;
            this.selectionState = documentState.SelectionState;
        }

        public void EndSelection()
        {
            this.selectionInProgress = false;
            this.selectionStartImageIndex = -1;
            PdfPoint point = new PdfPoint();
            this.selectionStartPosition = new PdfDocumentPosition(0, point);
        }

        private int FindImageByPosition(PdfDocumentPosition position)
        {
            int pageIndex = position.PageIndex;
            if (pageIndex > -1)
            {
                PdfPoint point = position.Point;
                IList<PdfPageImageData> imageData = this.pageDataCache.GetImageData(pageIndex);
                int count = imageData.Count;
                for (int i = 0; i < count; i++)
                {
                    if (imageData[i].BoundingRectangle.Contains(point))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private static PdfRectangle GetImageSelectionRectangle(PdfRectangle imageRectangle, PdfRectangle rect)
        {
            PdfRectangle rectangle = PdfRectangle.Intersect(imageRectangle, rect);
            if ((rectangle.Width > 0.0) && (rectangle.Height > 0.0))
            {
                return rectangle;
            }
            double left = rectangle.Left;
            double bottom = rectangle.Bottom;
            return new PdfRectangle(left, bottom, left + 1.0, bottom + 1.0);
        }

        public bool HasContent(PdfDocumentPosition position) => 
            this.selectionInProgress || (this.FindImageByPosition(position) != -1);

        public bool PerformSelection(PdfDocumentPosition position)
        {
            if (this.selectionStartImageIndex < 0)
            {
                return false;
            }
            int pageIndex = this.selectionStartPosition.PageIndex;
            PdfPoint point = this.selectionStartPosition.Point;
            double x = point.X;
            double y = point.Y;
            PdfPoint point2 = position.Point;
            if ((Math.Abs((double) (x - point2.X)) < 0.001) || (Math.Abs((double) (y - point2.Y)) < 0.001))
            {
                if (!this.selectionInProgress)
                {
                    this.selectionState.Selection = new PdfImageSelection(pageIndex, this.pageDataCache.GetImageData(pageIndex)[this.selectionStartImageIndex], null);
                    this.selectionStartImageIndex = -1;
                }
            }
            else if (this.selectionInProgress)
            {
                IList<PdfPageImageData> imageData = this.pageDataCache.GetImageData(pageIndex);
                if (imageData.Count > this.selectionStartImageIndex)
                {
                    PdfPageImageData pageImageData = imageData[this.selectionStartImageIndex];
                    if (position.PageIndex == pageIndex)
                    {
                        this.selectionState.Selection = new PdfImageSelection(pageIndex, pageImageData, GetImageSelectionRectangle(pageImageData.BoundingRectangle, new PdfRectangle(point, point2)));
                    }
                    else
                    {
                        PdfPoint clientPoint = this.controller.GetClientPoint(this.selectionStartPosition);
                        double num4 = clientPoint.X;
                        double num5 = clientPoint.Y;
                        PdfPoint point4 = this.controller.GetClientPoint(position);
                        int pageNumber = pageIndex + 1;
                        PdfRectangle boundingRectangle = pageImageData.BoundingRectangle;
                        PdfPoint point5 = this.controller.GetClientPoint(new PdfDocumentPosition(pageNumber, boundingRectangle.TopLeft));
                        PdfPoint point6 = this.controller.GetClientPoint(new PdfDocumentPosition(pageNumber, boundingRectangle.BottomRight));
                        double num7 = Math.Min(point5.X, point6.X);
                        double num8 = Math.Max(point5.X, point6.X);
                        double num9 = num4 - point4.X;
                        double num10 = ((num9 > 0.0) ? Math.Min(num9, num4 - num7) : Math.Max(num9, num4 - num8)) / (num7 - num8);
                        double num11 = Math.Min(point5.Y, point6.Y);
                        double num12 = Math.Max(point5.Y, point6.Y);
                        double num13 = num5 - point4.Y;
                        double num14 = ((num13 > 0.0) ? Math.Min(num13, num5 - num11) : Math.Max(num13, num5 - num12)) / (num12 - num11);
                        int num15 = PdfPageTreeObject.NormalizeRotate(this.documentState.RotationAngle + this.pageDataCache.DocumentPages[pageIndex].Rotate);
                        if (num15 == 90)
                        {
                            num14 = num10 * boundingRectangle.Height;
                            num10 = -num14 * boundingRectangle.Width;
                        }
                        else if (num15 == 180)
                        {
                            num10 *= -boundingRectangle.Width;
                            num14 *= -boundingRectangle.Height;
                        }
                        else if (num15 != 270)
                        {
                            num10 *= boundingRectangle.Width;
                            num14 *= boundingRectangle.Height;
                        }
                        else
                        {
                            num14 = -num10 * boundingRectangle.Height;
                            num10 = num14 * boundingRectangle.Width;
                        }
                        this.selectionState.Selection = new PdfImageSelection(pageIndex, pageImageData, GetImageSelectionRectangle(boundingRectangle, new PdfRectangle(point, new PdfPoint(x + num10, y + num14))));
                    }
                }
            }
            return true;
        }

        public IList<PdfImageSelection> SelectImages(PdfDocumentArea documentArea)
        {
            int pageIndex = documentArea.PageIndex;
            PdfRectangle area = documentArea.Area;
            IList<PdfImageSelection> list = new List<PdfImageSelection>();
            foreach (PdfPageImageData data in this.pageDataCache.GetImageData(pageIndex))
            {
                PdfRectangle boundingRectangle = data.BoundingRectangle;
                if (area.Intersects(boundingRectangle))
                {
                    list.Add(new PdfImageSelection(pageIndex, data, boundingRectangle));
                }
            }
            return ((list.Count == 0) ? null : list);
        }

        public IList<PdfImageSelection> SelectImages(PdfDocumentPosition startPosition, PdfDocumentPosition endPosition)
        {
            int pageIndex = startPosition.PageIndex;
            int num2 = endPosition.PageIndex;
            if (pageIndex == num2)
            {
                return this.SelectImagesOnPageBetweenPoints(startPosition, endPosition);
            }
            if (pageIndex > num2)
            {
                pageIndex = num2;
                num2 = pageIndex;
                double y = endPosition.Point.Y;
                double num5 = startPosition.Point.Y;
            }
            List<PdfImageSelection> list = new List<PdfImageSelection>();
            IList<PdfImageSelection> collection = this.SelectImagesOnPageBetweenPoints(startPosition, null);
            if (collection != null)
            {
                list.AddRange(collection);
            }
            PdfPoint point = new PdfPoint(0.0, 0.0);
            for (int i = pageIndex + 2; i <= num2; i++)
            {
                collection = this.SelectImagesOnPageBetweenPoints(null, new PdfDocumentPosition(i, point));
                if (collection != null)
                {
                    list.AddRange(collection);
                }
            }
            collection = this.SelectImagesOnPageBetweenPoints(null, endPosition);
            if (collection != null)
            {
                list.AddRange(collection);
            }
            return ((list.Count == 0) ? null : list);
        }

        private IList<PdfImageSelection> SelectImagesOnPageBetweenPoints(PdfDocumentPosition startPosition, PdfDocumentPosition endPosition)
        {
            int pageIndex;
            double minValue = double.MinValue;
            double maxValue = double.MaxValue;
            if (startPosition == null)
            {
                if (endPosition == null)
                {
                    return null;
                }
                pageIndex = endPosition.PageIndex;
            }
            else
            {
                pageIndex = startPosition.PageIndex;
                if (endPosition != null)
                {
                    minValue = startPosition.Point.Y;
                    maxValue = endPosition.Point.Y;
                    if (minValue > maxValue)
                    {
                        minValue = maxValue;
                        maxValue = minValue;
                    }
                }
            }
            List<PdfImageSelection> list = new List<PdfImageSelection>();
            foreach (PdfPageImageData data in this.pageDataCache.GetImageData(pageIndex))
            {
                PdfRectangle boundingRectangle = data.BoundingRectangle;
                if ((boundingRectangle.Bottom < maxValue) && (boundingRectangle.Top > minValue))
                {
                    list.Add(new PdfImageSelection(pageIndex, data, boundingRectangle));
                }
            }
            return ((list.Count == 0) ? null : list);
        }

        public bool StartSelection(PdfDocumentPosition position)
        {
            this.selectionStartPosition = position;
            this.selectionStartImageIndex = this.FindImageByPosition(position);
            this.selectionInProgress = this.selectionStartImageIndex != -1;
            return this.selectionInProgress;
        }

        public bool SelectionInProgress
        {
            get => 
                this.selectionInProgress;
            set => 
                this.selectionInProgress = value;
        }
    }
}

