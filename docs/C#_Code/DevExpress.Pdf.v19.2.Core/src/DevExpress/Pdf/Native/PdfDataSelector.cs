namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class PdfDataSelector
    {
        private readonly PdfDocumentStateBase documentState;
        private readonly PdfSelectionState selectionState;
        private readonly PdfTextSelector textSelector;
        private readonly PdfImageSelector imageSelector;
        private PdfDocumentPosition startSelectionPosition;
        private bool forceTextSelection;

        public event PdfDataSelectionEventHandler SelectionContinued;

        public event PdfDataSelectionEventHandler SelectionEnded;

        public event PdfDataSelectionEventHandler SelectionStarted;

        public PdfDataSelector(IPdfInteractiveOperationController controller, PdfDocumentStateBase documentState, PdfTextExtractionOptions textExtractionOptions = null)
        {
            this.documentState = documentState;
            this.selectionState = documentState.SelectionState;
            PdfPageDataCache pageDataCache = new PdfPageDataCache(documentState.Document.Pages, false, (textExtractionOptions != null) ? textExtractionOptions.ClipToCropBox : true);
            this.textSelector = new PdfTextSelector(controller, pageDataCache, documentState);
            this.imageSelector = new PdfImageSelector(controller, pageDataCache, documentState);
        }

        public void ClearSelection()
        {
            this.selectionState.Selection = null;
            this.textSelector.SelectionInProgress = false;
            this.imageSelector.EndSelection();
        }

        public void EndSelection(PdfMouseAction mouseAction)
        {
            PdfDocumentPosition documentPosition = mouseAction.DocumentPosition;
            if ((this.startSelectionPosition != null) && this.startSelectionPosition.NearTo(documentPosition))
            {
                if (this.selectionState.Contains(documentPosition))
                {
                    this.ClearSelection();
                }
                else
                {
                    this.PerformSelection(mouseAction);
                }
            }
            this.EndSelection(documentPosition);
        }

        public void EndSelection(PdfDocumentPosition position)
        {
            this.textSelector.SelectionInProgress = false;
            this.imageSelector.SelectionInProgress = false;
            this.PerformSelection(position);
        }

        public PdfDocumentContent GetContentInfo(PdfDocumentPosition position)
        {
            PdfPoint cropBoxPoint = position.Point;
            int pageIndex = position.PageIndex;
            bool selected = this.selectionState.Contains(position);
            if ((pageIndex >= 0) && (pageIndex < this.documentState.Document.Pages.Count))
            {
                using (IEnumerator<PdfAnnotationState> enumerator = this.documentState[pageIndex].AnnotationStates.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PdfAnnotationState current = enumerator.Current;
                        if (current.Contains(cropBoxPoint))
                        {
                            return new PdfDocumentContent(position, PdfDocumentContentType.Annotation, selected);
                        }
                    }
                }
            }
            return new PdfDocumentContent(position, !this.textSelector.HasContent(position) ? (!this.imageSelector.HasContent(position) ? PdfDocumentContentType.None : PdfDocumentContentType.Image) : PdfDocumentContentType.Text, selected);
        }

        private PdfSelection GetImageSelection(PdfDocumentArea documentArea)
        {
            IList<PdfImageSelection> imagesSelection = this.GetImagesSelection(documentArea);
            return ((imagesSelection == null) ? null : ((PdfSelection) imagesSelection[0]));
        }

        public IList<PdfImageSelection> GetImagesSelection(PdfDocumentArea documentArea) => 
            this.imageSelector.SelectImages(documentArea);

        public IList<PdfImageSelection> GetImagesSelection(PdfDocumentPosition startPosition, PdfDocumentPosition endPosition) => 
            this.imageSelector.SelectImages(startPosition, endPosition);

        public PdfTextSelection GetTextSelection(PdfDocumentArea documentArea) => 
            this.textSelector.GetSelection(documentArea);

        public PdfTextSelection GetTextSelection(IList<PdfPageTextRange> textRange) => 
            this.textSelector.GetSelection(textRange);

        public PdfTextSelection GetTextSelection(PdfDocumentPosition startPosition, PdfDocumentPosition endPosition) => 
            this.textSelector.GetSelection(this.textSelector.GetPageTextRanges(this.textSelector.FindClosestTextPosition(startPosition, new PdfTextPosition(startPosition.PageIndex, 1, 0)), endPosition));

        public PdfWord GetWord(PdfDocumentPosition position) => 
            this.textSelector.GetWord(position);

        public void HideCaret()
        {
            this.selectionState.Caret = null;
        }

        public void MouseDown(PdfMouseAction action)
        {
            this.StartSelection(action);
            this.RaiseSelectionEvent(this.SelectionStarted, action.DocumentPosition);
        }

        public void MouseMove(PdfMouseAction action)
        {
            if (this.ShouldPerformSelection(action))
            {
                PdfDocumentPosition documentPosition = action.DocumentPosition;
                this.PerformSelection(documentPosition);
                this.RaiseSelectionEvent(this.SelectionContinued, documentPosition);
            }
        }

        public void MouseUp(PdfMouseAction action)
        {
            if (this.ShouldPerformSelection(action))
            {
                this.EndSelection(action);
                this.RaiseSelectionEvent(this.SelectionEnded, action.DocumentPosition);
            }
        }

        public void MoveCaret(PdfMovementDirection direction)
        {
            this.textSelector.MoveCaret(direction);
        }

        private void PerformSelection(PdfMouseAction mouseAction)
        {
            if (!mouseAction.ModifierKeys.HasFlag(PdfModifierKeys.Shift) || (this.selectionState.Caret == null))
            {
                this.StartSelection(mouseAction.DocumentPosition);
            }
            else
            {
                this.textSelector.SelectionInProgress = true;
                this.PerformSelection(mouseAction.DocumentPosition);
            }
        }

        public void PerformSelection(PdfDocumentPosition position)
        {
            if (!this.imageSelector.PerformSelection(position))
            {
                this.textSelector.PerformSelection(position);
            }
        }

        private void RaiseSelectionEvent(PdfDataSelectionEventHandler handler, PdfDocumentPosition position)
        {
            if (handler != null)
            {
                handler(this, new PdfDataSelectionEventArgs(position));
            }
        }

        public void Select(PdfDocumentArea documentArea)
        {
            this.textSelector.Select(documentArea);
            if (!this.selectionState.HasSelection)
            {
                this.selectionState.Selection = this.GetImageSelection(documentArea);
            }
        }

        public void SelectAllText()
        {
            this.textSelector.SelectAllText();
        }

        public void SelectImage(PdfDocumentArea documentArea)
        {
            this.selectionState.Selection = this.GetImageSelection(documentArea);
        }

        public void SelectLine(PdfDocumentPosition position)
        {
            this.textSelector.SelectLine(position);
        }

        public void SelectPage(PdfDocumentPosition position)
        {
            this.textSelector.SelectPage(position);
        }

        public void SelectText(IList<PdfPageTextRange> textRange)
        {
            this.textSelector.SelectText(textRange);
        }

        public void SelectText(PdfDocumentPosition position1, PdfDocumentPosition position2)
        {
            this.selectionState.Selection = this.GetTextSelection(position1, position2);
        }

        public void SelectWithCaret(PdfMovementDirection direction)
        {
            this.textSelector.SelectWithCaret(direction);
        }

        public void SelectWord(PdfDocumentPosition position)
        {
            this.textSelector.SelectWord(position);
        }

        public void SetZoomFactor(double zoomFactor)
        {
            this.textSelector.SetZoomFactor(zoomFactor);
        }

        private bool ShouldPerformSelection(PdfMouseAction action) => 
            (action.Button == PdfMouseButton.Left) && ((action.Clicks <= 1) || (this.GetContentInfo(action.DocumentPosition).ContentType == PdfDocumentContentType.Image));

        public void StartSelection(PdfMouseAction mouseAction)
        {
            this.startSelectionPosition = null;
            PdfDocumentPosition documentPosition = mouseAction.DocumentPosition;
            if (!this.textSelector.HasContent(documentPosition))
            {
                if (this.selectionState.Contains(documentPosition))
                {
                    this.startSelectionPosition = documentPosition;
                }
                else
                {
                    this.StartSelection(documentPosition);
                }
            }
            else
            {
                switch (mouseAction.Clicks)
                {
                    case 2:
                        this.SelectWord(documentPosition);
                        return;

                    case 3:
                        if (this.forceTextSelection)
                        {
                            break;
                        }
                        this.SelectLine(documentPosition);
                        return;

                    case 4:
                        if (this.forceTextSelection)
                        {
                            break;
                        }
                        this.SelectPage(documentPosition);
                        return;

                    default:
                        break;
                }
                if (this.selectionState.Contains(documentPosition))
                {
                    this.startSelectionPosition = documentPosition;
                }
                else
                {
                    this.PerformSelection(mouseAction);
                }
            }
        }

        public void StartSelection(PdfDocumentPosition startPosition)
        {
            this.ClearSelection();
            if (!this.textSelector.StartSelection(startPosition, this.forceTextSelection) && !this.forceTextSelection)
            {
                this.imageSelector.StartSelection(startPosition);
            }
        }

        public bool SelectionInProgress =>
            this.textSelector.SelectionInProgress || this.imageSelector.SelectionInProgress;

        public bool TextSelectionInProgress =>
            this.textSelector.SelectionInProgress;

        public bool ImageSelectionInProgress =>
            this.imageSelector.SelectionInProgress;

        public bool ForceTextSelection
        {
            get => 
                this.forceTextSelection;
            set => 
                this.forceTextSelection = value;
        }
    }
}

