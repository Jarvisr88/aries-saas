namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PdfTextSelector
    {
        private readonly IPdfInteractiveOperationController controller;
        private readonly PdfPageDataCache pageDataCache;
        private readonly PdfSelectionState selectionState;
        private readonly PdfDocumentStateBase documentState;
        private double textExpansionFactorX;
        private double textExpansionFactorY;
        private bool selectionInProgress;
        private int selectionStartPageIndex;
        private PdfPoint selectionStartPoint;
        private PdfTextPosition selectionStartTextPosition;

        public PdfTextSelector(IPdfInteractiveOperationController controller, PdfPageDataCache pageDataCache, PdfDocumentStateBase documentState)
        {
            this.controller = controller;
            this.pageDataCache = pageDataCache;
            this.documentState = documentState;
            this.selectionState = documentState.SelectionState;
            this.SetZoomFactor(1.0);
        }

        private void EnsureCaretVisibility()
        {
            PdfCaret caret = this.selectionState.Caret;
            if (caret != null)
            {
                PdfCaretViewData viewData = caret.ViewData;
                double height = viewData.Height;
                double angle = viewData.Angle;
                PdfPoint topLeft = viewData.TopLeft;
                this.controller.EnsureVisible(caret.Position.PageIndex, new PdfRectangle(topLeft, new PdfPoint(topLeft.X + (height * Math.Sin(angle)), topLeft.Y - (height * Math.Cos(angle)))), false);
            }
        }

        private PdfTextPosition FindClosestTextPosition(PdfDocumentPosition position)
        {
            PdfTextPosition position2;
            int pageIndex = position.PageIndex;
            if (pageIndex < 0)
            {
                return null;
            }
            PdfPoint point = position.Point;
            IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(pageIndex);
            PdfTextLine line = null;
            double maxValue = double.MaxValue;
            using (IEnumerator<PdfTextLine> enumerator = pageLines.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfTextLine current = enumerator.Current;
                        PdfOrientedRectangle rectangle = current.Rectangle;
                        if (rectangle.PointIsInRect(point, 0.0, 0.0))
                        {
                            using (IEnumerator<PdfWordPart> enumerator2 = current.WordParts.GetEnumerator())
                            {
                                while (true)
                                {
                                    if (!enumerator2.MoveNext())
                                    {
                                        break;
                                    }
                                    PdfWordPart part = enumerator2.Current;
                                    if (part.Rectangle.PointIsInRect(point, 0.0, 0.0))
                                    {
                                        return current.GetTextPosition(pageIndex, point);
                                    }
                                }
                            }
                        }
                        double num3 = PdfPoint.Distance(rectangle.BoundingRectangle, point);
                        if (num3 < maxValue)
                        {
                            line = current;
                            maxValue = num3;
                        }
                        continue;
                    }
                    else
                    {
                        if (line == null)
                        {
                            for (int i = pageLines.Count - 1; i >= 0; i--)
                            {
                                PdfTextLine line3 = pageLines[i];
                                PdfRectangle boundingRectangle = line3.Rectangle.BoundingRectangle;
                                if ((boundingRectangle.Bottom <= point.Y) && (boundingRectangle.Right >= point.X))
                                {
                                    line = line3;
                                }
                            }
                        }
                        return line?.GetTextPosition(pageIndex, point);
                    }
                    break;
                }
            }
            return position2;
        }

        public PdfTextPosition FindClosestTextPosition(PdfDocumentPosition position, PdfTextPosition textPosition)
        {
            PdfTextPosition position3;
            PdfTextPosition position2 = this.FindClosestTextPosition(position);
            if (position2 != null)
            {
                return position2;
            }
            int pageIndex = position.PageIndex;
            if (pageIndex < 0)
            {
                return null;
            }
            int wordNumber = textPosition.WordNumber;
            using (IEnumerator<PdfTextLine> enumerator = this.pageDataCache.GetPageLines(textPosition.PageIndex).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfTextLine current = enumerator.Current;
                        int num3 = current.WordParts.Count - 1;
                        if ((num3 < 0) || ((wordNumber < current.WordParts[0].WordNumber) || (current.WordParts[num3].WordNumber < wordNumber)))
                        {
                            continue;
                        }
                        position3 = current.GetTextPosition(pageIndex, position.Point);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return position3;
        }

        private PdfTextLine FindLine(PdfTextPosition position)
        {
            PdfTextLine line2;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            int offset = position.Offset;
            int num4 = 0;
            using (IEnumerator<PdfTextLine> enumerator = this.pageDataCache.GetPageLines(pageIndex).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfTextLine current = enumerator.Current;
                        if (!this.IsPositionInLine(pageIndex, num4++, wordNumber, offset))
                        {
                            continue;
                        }
                        line2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return line2;
        }

        private PdfTextLine FindLine(PdfDocumentPosition position)
        {
            PdfTextLine line3;
            int pageIndex = position.PageIndex;
            if (pageIndex < 0)
            {
                return null;
            }
            PdfPoint point = position.Point;
            PdfTextLine line = null;
            using (IEnumerator<PdfTextLine> enumerator = this.pageDataCache.GetPageLines(pageIndex).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfTextLine current = enumerator.Current;
                        PdfOrientedRectangle rectangle = current.Rectangle;
                        IList<PdfWordPart> wordParts = current.WordParts;
                        if (rectangle.PointIsInRect(point, this.textExpansionFactorX, 0.0))
                        {
                            using (IEnumerator<PdfWordPart> enumerator2 = wordParts.GetEnumerator())
                            {
                                while (true)
                                {
                                    if (!enumerator2.MoveNext())
                                    {
                                        break;
                                    }
                                    PdfWordPart part = enumerator2.Current;
                                    if (part.Rectangle.PointIsInRect(point, this.textExpansionFactorX, 0.0))
                                    {
                                        return current;
                                    }
                                }
                            }
                        }
                        if (rectangle.PointIsInRect(point, this.textExpansionFactorX, this.textExpansionFactorY))
                        {
                            foreach (PdfWordPart part2 in wordParts)
                            {
                                if (part2.Rectangle.PointIsInRect(point, this.textExpansionFactorX, this.textExpansionFactorY))
                                {
                                    line = current;
                                }
                            }
                        }
                        continue;
                    }
                    else
                    {
                        return line;
                    }
                    break;
                }
            }
            return line3;
        }

        private PdfTextPosition FindStartTextPosition(PdfDocumentPosition position) => 
            this.FindLine(position)?.GetTextPosition(position.PageIndex, position.Point);

        private PdfCaretViewData GetCaretViewData(PdfTextPosition position)
        {
            PdfCaretViewData data;
            PdfTextLine line = this.FindLine(position);
            if (line == null)
            {
                return new PdfCaretViewData(new PdfPoint(0.0, 0.0), 0.0, 0.0);
            }
            int wordNumber = position.WordNumber;
            int offset = position.Offset;
            using (IEnumerator<PdfWordPart> enumerator = line.WordParts.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        double x;
                        double top;
                        PdfWordPart current = enumerator.Current;
                        if (!current.IsSuitable(wordNumber, offset))
                        {
                            continue;
                        }
                        PdfOrientedRectangle rectangle = current.Rectangle;
                        IList<PdfCharacter> characters = current.Characters;
                        PdfPoint point = (offset == characters.Count) ? characters[offset - 1].Rectangle.TopRight : characters[offset].Rectangle.TopLeft;
                        double angle = rectangle.Angle;
                        if ((angle == 0.0) || (angle == 3.1415926535897931))
                        {
                            x = point.X;
                            top = rectangle.Top;
                        }
                        else if ((angle == 4.71238898038469) || (angle == 1.5707963267948966))
                        {
                            x = rectangle.Left;
                            top = point.Y;
                        }
                        else
                        {
                            double left = rectangle.Left;
                            double top = rectangle.Top;
                            double num8 = Math.Tan(angle);
                            double num9 = Math.Tan(angle + 1.5707963267948966);
                            x = ((((left * num8) - top) - (point.X * num9)) + point.Y) / (num8 - num9);
                            top = (num8 * (x - left)) + top;
                        }
                        data = new PdfCaretViewData(new PdfPoint(x, top), rectangle.Height, line.Rectangle.Angle);
                    }
                    else
                    {
                        return new PdfCaretViewData(new PdfPoint(), 0.0, line.Rectangle.Angle);
                    }
                    break;
                }
            }
            return data;
        }

        private IList<PdfPageTextRange> GetPageTextRanges(PdfTextPosition startTextPosition, PdfTextPosition endTextPosition)
        {
            if (endTextPosition == null)
            {
                return null;
            }
            List<PdfPageTextRange> list = new List<PdfPageTextRange>();
            int pageIndex = startTextPosition.PageIndex;
            int num2 = endTextPosition.PageIndex;
            if (pageIndex == num2)
            {
                int wordNumber = startTextPosition.WordNumber;
                int num4 = endTextPosition.WordNumber;
                if ((wordNumber <= num4) && ((wordNumber != num4) || (startTextPosition.Offset <= endTextPosition.Offset)))
                {
                    list.Add(new PdfPageTextRange(pageIndex, startTextPosition, endTextPosition));
                }
                else
                {
                    list.Add(new PdfPageTextRange(num2, endTextPosition, startTextPosition));
                }
            }
            else
            {
                if (pageIndex > num2)
                {
                    PdfTextPosition position = startTextPosition;
                    startTextPosition = endTextPosition;
                    endTextPosition = position;
                    pageIndex = startTextPosition.PageIndex;
                    num2 = endTextPosition.PageIndex;
                }
                list.Add(new PdfPageTextRange(pageIndex, startTextPosition, new PdfPageTextPosition(0, -1)));
                int num5 = pageIndex + 1;
                while (true)
                {
                    if (num5 >= num2)
                    {
                        list.Add(new PdfPageTextRange(num2, new PdfPageTextPosition(0, 0), endTextPosition));
                        break;
                    }
                    list.Add(new PdfPageTextRange(num5));
                    num5++;
                }
            }
            return list;
        }

        public IList<PdfPageTextRange> GetPageTextRanges(PdfTextPosition startTextPosition, PdfDocumentPosition endPosition) => 
            (startTextPosition == null) ? null : this.GetPageTextRanges(startTextPosition, this.FindClosestTextPosition(endPosition, startTextPosition));

        private PdfTextSelection GetSelection(PdfPageTextRange pageTextRange)
        {
            PdfPageTextRange[] textRange = new PdfPageTextRange[] { pageTextRange };
            return this.GetSelection(textRange);
        }

        public PdfTextSelection GetSelection(PdfDocumentArea documentArea)
        {
            List<PdfPageTextRange> textRange = new List<PdfPageTextRange>();
            PdfRectangle area = documentArea.Area;
            int pageIndex = documentArea.PageIndex;
            foreach (PdfTextLine line in this.pageDataCache.GetPageLines(pageIndex))
            {
                if (area.Intersects(line.Rectangle.BoundingRectangle))
                {
                    PdfPageTextRange item = line.GetTextRange(pageIndex, area);
                    if (item != null)
                    {
                        textRange.Add(item);
                    }
                }
            }
            return this.GetSelection(textRange);
        }

        public PdfTextSelection GetSelection(IList<PdfPageTextRange> textRange)
        {
            PdfTextSelection selection;
            if (textRange == null)
            {
                return null;
            }
            using (IEnumerator<PdfPageTextRange> enumerator = textRange.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfPageTextRange current = enumerator.Current;
                        int startWordNumber = current.StartWordNumber;
                        int endWordNumber = current.EndWordNumber;
                        if (!current.WholePage)
                        {
                            if ((startWordNumber > endWordNumber) && (endWordNumber != 0))
                            {
                                continue;
                            }
                            if ((startWordNumber == endWordNumber) && (current.StartOffset >= current.EndOffset))
                            {
                                continue;
                            }
                        }
                        selection = new PdfTextSelection(this.pageDataCache, textRange);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return selection;
        }

        private PdfWord GetWord(PdfTextPosition textPosition)
        {
            if (textPosition != null)
            {
                int wordNumber = textPosition.WordNumber;
                using (IEnumerator<PdfWord> enumerator = this.pageDataCache[textPosition.PageIndex].Words.GetEnumerator())
                {
                    while (true)
                    {
                        Func<PdfWordPart, bool> <>9__0;
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PdfWord current = enumerator.Current;
                        Func<PdfWordPart, bool> predicate = <>9__0;
                        if (<>9__0 == null)
                        {
                            Func<PdfWordPart, bool> local1 = <>9__0;
                            predicate = <>9__0 = part => part.WordNumber == wordNumber;
                        }
                        if (current.Parts.Any<PdfWordPart>(predicate))
                        {
                            return current;
                        }
                    }
                }
            }
            return null;
        }

        public PdfWord GetWord(PdfDocumentPosition position) => 
            this.GetWord(this.FindStartTextPosition(position));

        private int GetWordEndPosition(PdfTextPosition position)
        {
            int num6;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            int num3 = 0;
            using (IEnumerator<PdfTextLine> enumerator = this.pageDataCache.GetPageLines(pageIndex).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfTextLine current = enumerator.Current;
                        IList<PdfWordPart> wordParts = current.WordParts;
                        int num4 = wordParts.Count - 1;
                        if (num4 >= 0)
                        {
                            int num5 = 0;
                            while (true)
                            {
                                if (num5 < num4)
                                {
                                    PdfWordPart part2 = wordParts[num5];
                                    if (part2.WordNumber != wordNumber)
                                    {
                                        num5++;
                                        continue;
                                    }
                                    return part2.EndWordPosition;
                                }
                                else
                                {
                                    PdfWordPart part = wordParts[num4];
                                    if (part.WordNumber == wordNumber)
                                    {
                                        return part.EndWordPosition;
                                    }
                                }
                                break;
                            }
                        }
                        num3++;
                        continue;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return num6;
        }

        public bool HasContent(PdfDocumentPosition position) => 
            this.selectionInProgress || (this.FindStartTextPosition(position) != null);

        private bool IsPositionInLine(int pageIndex, int lineIndex, int wordNumber, int offset)
        {
            IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(pageIndex);
            PdfTextLine line = pageLines[lineIndex];
            if (!line.IsPositionInLine(wordNumber, offset))
            {
                return false;
            }
            IList<PdfWordPart> wordParts = line.WordParts;
            if (wordParts[wordParts.Count - 1].WordNumber == wordNumber)
            {
                if (lineIndex < (pageLines.Count - 1))
                {
                    return !pageLines[lineIndex + 1].IsPositionInLine(wordNumber, offset);
                }
                int num = this.PageCount - 1;
                while (pageIndex != num)
                {
                    pageLines = this.pageDataCache.GetPageLines(++pageIndex);
                    if ((pageLines != null) && (pageLines.Count != 0))
                    {
                        line = pageLines[0];
                        return (!line.IsPositionInLine(wordNumber, offset) || (line.WordParts[0].WordNumber != wordNumber));
                    }
                }
            }
            return true;
        }

        public void MoveCaret(PdfMovementDirection direction)
        {
            switch (direction)
            {
                case PdfMovementDirection.Left:
                    if (this.MoveCaretToLeft())
                    {
                        break;
                    }
                    this.PerformCaretMoveAction(new Action(this.MoveLeft));
                    return;

                case PdfMovementDirection.Down:
                    this.MoveCaretToRight();
                    this.PerformCaretMoveAction(new Action(this.MoveDown));
                    return;

                case PdfMovementDirection.Right:
                    if (this.MoveCaretToRight() || !this.selectionState.HasCaret)
                    {
                        break;
                    }
                    this.PerformCaretMoveAction(new Action(this.MoveRight));
                    return;

                case PdfMovementDirection.Up:
                    this.MoveCaretToLeft();
                    this.PerformCaretMoveAction(new Action(this.MoveUp));
                    return;

                case PdfMovementDirection.NextWord:
                    this.PerformCaretMoveAction(new Action(this.MoveToNextWord));
                    return;

                case PdfMovementDirection.PreviousWord:
                    this.PerformCaretMoveAction(new Action(this.MoveToPreviousWord));
                    return;

                case PdfMovementDirection.LineStart:
                    this.PerformCaretMoveAction(new Action(this.MoveToLineStart));
                    return;

                case PdfMovementDirection.LineEnd:
                    this.PerformCaretMoveAction(new Action(this.MoveToLineEnd));
                    return;

                case PdfMovementDirection.DocumentStart:
                    this.PerformCaretMoveAction(new Action(this.MoveToDocumentStart));
                    return;

                case PdfMovementDirection.DocumentEnd:
                    this.PerformCaretMoveAction(new Action(this.MoveToDocumentEnd));
                    break;

                default:
                    return;
            }
        }

        private void MoveCaret(PdfTextPosition position)
        {
            PdfCaretViewData caretViewData = this.GetCaretViewData(position);
            this.selectionState.Caret = new PdfCaret(position, caretViewData, caretViewData.TopLeft);
        }

        private void MoveCaretAndEnsureVisibility(PdfTextPosition position)
        {
            this.MoveCaret(position);
            this.EnsureCaretVisibility();
        }

        private bool MoveCaretToLeft()
        {
            PdfTextSelection selection = this.selectionState.Selection as PdfTextSelection;
            if (selection == null)
            {
                return false;
            }
            PdfPageTextRange range = selection.TextRange[0];
            this.UpdateSelection(new PdfTextPosition(range.PageIndex, range.StartTextPosition));
            return true;
        }

        private bool MoveCaretToRight()
        {
            PdfTextSelection selection = this.selectionState.Selection as PdfTextSelection;
            if (selection == null)
            {
                return false;
            }
            IList<PdfPageTextRange> textRange = selection.TextRange;
            PdfPageTextRange range = textRange[textRange.Count - 1];
            this.UpdateSelection(new PdfTextPosition(range.PageIndex, range.EndTextPosition));
            return true;
        }

        private void MoveDown()
        {
            PdfCaret caret = this.selectionState.Caret;
            PdfTextPosition position = caret.Position;
            int pageIndex = position.PageIndex;
            IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(pageIndex);
            int num2 = pageLines.Count - 1;
            if (num2 >= 0)
            {
                PdfPoint startCoordinates = caret.StartCoordinates;
                int wordNumber = position.WordNumber;
                int offset = position.Offset;
                int lineIndex = 0;
                while (true)
                {
                    if (lineIndex >= num2)
                    {
                        int pageCount = this.PageCount;
                        for (int i = pageIndex + 1; i < pageCount; i++)
                        {
                            pageLines = this.pageDataCache.GetPageLines(i);
                            if (pageLines.Count > 0)
                            {
                                this.SetSelectionCaret(pageLines[0].GetTextPosition(i, startCoordinates));
                                return;
                            }
                        }
                        break;
                    }
                    if (this.IsPositionInLine(pageIndex, lineIndex, wordNumber, offset))
                    {
                        this.SetSelectionCaret(pageLines[lineIndex + 1].GetTextPosition(pageIndex, startCoordinates));
                        return;
                    }
                    lineIndex++;
                }
            }
            this.SetSelectionCaret(position);
        }

        private void MoveLeft()
        {
            PdfTextPosition position = this.selectionState.Caret.Position;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            int offset = position.Offset;
            if (offset > 0)
            {
                this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, new PdfPageTextPosition(wordNumber, offset - 1)));
            }
            else if (wordNumber > 1)
            {
                int num4 = wordNumber - 1;
                this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, new PdfPageTextPosition(num4, this.GetWordEndPosition(new PdfTextPosition(pageIndex, num4, 0)))));
            }
            else
            {
                for (int i = pageIndex - 1; i >= 0; i--)
                {
                    IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(i);
                    int num6 = pageLines.Count - 1;
                    if (num6 >= 0)
                    {
                        IList<PdfWordPart> wordParts = pageLines[num6].WordParts;
                        PdfWordPart part = wordParts[wordParts.Count - 1];
                        this.MoveCaretAndEnsureVisibility(new PdfTextPosition(i, new PdfPageTextPosition(part.WordNumber, part.EndWordPosition)));
                        return;
                    }
                }
                this.MoveCaretAndEnsureVisibility(position);
            }
        }

        private void MoveRight()
        {
            PdfTextPosition position = this.selectionState.Caret.Position;
            int pageIndex = position.PageIndex;
            IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(pageIndex);
            int lineIndex = pageLines.Count - 1;
            if (lineIndex >= 0)
            {
                int wordNumber = position.WordNumber;
                int offset = position.Offset;
                int num5 = 0;
                while (true)
                {
                    if (num5 >= lineIndex)
                    {
                        PdfTextLine line = pageLines[lineIndex];
                        if (line.IsPositionInLine(wordNumber, offset))
                        {
                            IList<PdfWordPart> wordParts = line.WordParts;
                            if (this.MoveRight(wordParts, pageIndex, lineIndex, wordNumber, offset, false))
                            {
                                return;
                            }
                            PdfWordPart part = wordParts[wordParts.Count - 1];
                            if (part.IsSuitable(wordNumber, offset))
                            {
                                if (part.EndWordPosition > offset)
                                {
                                    this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, new PdfPageTextPosition(wordNumber, offset + 1)));
                                    return;
                                }
                                int pageCount = this.PageCount;
                                for (int i = pageIndex + 1; i < pageCount; i++)
                                {
                                    if (this.pageDataCache.GetPageLines(i).Count > 0)
                                    {
                                        this.MoveCaretAndEnsureVisibility(new PdfTextPosition(i, new PdfPageTextPosition(1, 0)));
                                        return;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    PdfTextLine line2 = pageLines[num5];
                    if (line2.IsPositionInLine(wordNumber, offset) && this.MoveRight(line2.WordParts, pageIndex, num5, wordNumber, offset, true))
                    {
                        return;
                    }
                    num5++;
                }
            }
            this.MoveCaretAndEnsureVisibility(position);
        }

        private bool MoveRight(IList<PdfWordPart> wordParts, int pageIndex, int lineIndex, int wordNumber, int offset, bool processLastWordPart)
        {
            int num = wordParts.Count - 1;
            int num2 = wordParts[num].WordNumber;
            if (!processLastWordPart)
            {
                num--;
            }
            for (int i = 0; i <= num; i++)
            {
                PdfWordPart part = wordParts[i];
                if (part.IsSuitable(wordNumber, offset))
                {
                    if (part.EndWordPosition > offset)
                    {
                        this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, new PdfPageTextPosition(wordNumber, offset + 1)));
                    }
                    else
                    {
                        this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, new PdfPageTextPosition(wordNumber + 1, part.WordEnded ? 0 : 1)));
                    }
                    return true;
                }
            }
            return false;
        }

        private void MoveToDocumentEnd()
        {
            for (int i = this.PageCount - 1; i >= 0; i--)
            {
                IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(i);
                int count = pageLines.Count;
                if (count > 0)
                {
                    IList<PdfWordPart> wordParts = pageLines[count - 1].WordParts;
                    if (wordParts.Count > 0)
                    {
                        PdfWordPart part = wordParts[wordParts.Count - 1];
                        this.MoveCaretAndEnsureVisibility(new PdfTextPosition(i, new PdfPageTextPosition(part.WordNumber, part.Characters.Count)));
                        return;
                    }
                }
            }
        }

        private void MoveToDocumentStart()
        {
            int pageCount = this.PageCount;
            for (int i = 0; i < pageCount; i++)
            {
                if (this.pageDataCache.GetPageLines(i).Count != 0)
                {
                    this.MoveCaretAndEnsureVisibility(new PdfTextPosition(i, 1, 0));
                    return;
                }
            }
        }

        private void MoveToLineEnd()
        {
            PdfTextPosition position = this.selectionState.Caret.Position;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            int offset = position.Offset;
            foreach (PdfTextLine line in this.pageDataCache.GetPageLines(pageIndex))
            {
                if (line.IsPositionInLine(wordNumber, offset))
                {
                    IList<PdfWordPart> wordParts = line.WordParts;
                    PdfWordPart part = wordParts[wordParts.Count - 1];
                    this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, new PdfPageTextPosition(part.WordNumber, part.Characters.Count)));
                }
            }
        }

        private void MoveToLineStart()
        {
            PdfTextPosition position = this.selectionState.Caret.Position;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            int offset = position.Offset;
            foreach (PdfTextLine line in this.pageDataCache.GetPageLines(pageIndex))
            {
                if (line.IsPositionInLine(wordNumber, offset))
                {
                    this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, new PdfPageTextPosition(line.WordParts[0].WordNumber, 0)));
                }
            }
        }

        private void MoveToNextWord()
        {
            PdfTextPosition position = this.selectionState.Caret.Position;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(pageIndex);
            PdfTextLine line = pageLines[pageLines.Count - 1];
            int count = line.WordParts.Count;
            if ((count == 0) || (line.WordParts[count - 1].WordNumber != wordNumber))
            {
                this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, wordNumber + 1, 0));
            }
            else
            {
                int pageCount = this.PageCount;
                for (int i = pageIndex + 1; i < pageCount; i++)
                {
                    pageLines = this.pageDataCache.GetPageLines(i);
                    if ((pageLines != null) && (pageLines.Count > 0))
                    {
                        this.MoveCaretAndEnsureVisibility(new PdfTextPosition(i, 1, 0));
                        return;
                    }
                }
                int offset = position.Offset;
                foreach (PdfTextLine line2 in this.pageDataCache.GetPageLines(pageIndex))
                {
                    if (line2.IsPositionInLine(wordNumber, offset))
                    {
                        foreach (PdfWordPart part in line2.WordParts)
                        {
                            if (part.WordNumber == wordNumber)
                            {
                                this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, wordNumber, part.Characters.Count));
                            }
                        }
                    }
                }
            }
        }

        private void MoveToPreviousWord()
        {
            PdfTextPosition position = this.selectionState.Caret.Position;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            if (position.Offset != 0)
            {
                this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, wordNumber, 0));
            }
            else if (wordNumber != 1)
            {
                this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, wordNumber - 1, 0));
            }
            else
            {
                while (pageIndex > 0)
                {
                    IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(--pageIndex);
                    int num3 = pageLines.Count - 1;
                    if (num3 >= 0)
                    {
                        IList<PdfWordPart> wordParts = pageLines[num3].WordParts;
                        this.MoveCaretAndEnsureVisibility(new PdfTextPosition(pageIndex, wordParts[wordParts.Count - 1].WordNumber, 0));
                        return;
                    }
                }
            }
        }

        private void MoveUp()
        {
            PdfCaret caret = this.selectionState.Caret;
            PdfPoint startCoordinates = caret.StartCoordinates;
            PdfTextPosition position = caret.Position;
            int pageIndex = position.PageIndex;
            int wordNumber = position.WordNumber;
            int offset = position.Offset;
            IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(pageIndex);
            for (int i = pageLines.Count - 1; i > 0; i--)
            {
                PdfTextLine line = pageLines[i];
                if (line.IsPositionInLine(wordNumber, offset))
                {
                    PdfTextPosition textPosition = pageLines[i - 1].GetTextPosition(pageIndex, startCoordinates);
                    wordNumber = textPosition.WordNumber;
                    offset = textPosition.Offset;
                    this.SetSelectionCaret(new PdfTextPosition(pageIndex, new PdfPageTextPosition(wordNumber, line.IsPositionInLine(wordNumber, offset) ? (offset - 1) : offset)));
                    return;
                }
            }
            for (int j = pageIndex - 1; j >= 0; j--)
            {
                pageLines = this.pageDataCache.GetPageLines(j);
                if (pageLines.Count > 0)
                {
                    this.SetSelectionCaret(pageLines[pageLines.Count - 1].GetTextPosition(j, startCoordinates));
                    return;
                }
            }
            this.SetSelectionCaret(position);
        }

        private void PerformCaretMoveAction(Action moveAction)
        {
            if (this.selectionState.HasCaret && (moveAction != null))
            {
                moveAction();
                this.selectionState.Selection = null;
            }
        }

        public void PerformSelection(PdfDocumentPosition position)
        {
            if (this.selectionInProgress && ((position.PageIndex != this.selectionStartPageIndex) || !position.Point.Equals(this.selectionStartPoint)))
            {
                this.StoreSelectionStartTextPosition();
                IList<PdfPageTextRange> pageTextRanges = this.GetPageTextRanges(this.selectionStartTextPosition, position);
                if (pageTextRanges != null)
                {
                    PdfPageTextRange range = pageTextRanges[pageTextRanges.Count - 1];
                    PdfPageTextPosition endTextPosition = range.EndTextPosition;
                    this.MoveCaret(new PdfTextPosition(range.PageIndex, ReferenceEquals(endTextPosition, this.selectionStartTextPosition) ? range.StartTextPosition : endTextPosition));
                    this.selectionState.Selection = this.GetSelection(pageTextRanges);
                }
            }
        }

        public void Select(PdfDocumentArea documentArea)
        {
            this.SetSelection(this.GetSelection(documentArea));
        }

        public void SelectAllText()
        {
            int pageCount = this.PageCount;
            for (int i = 0; i < pageCount; i++)
            {
                if (this.pageDataCache.GetPageLines(i).Count > 0)
                {
                    IList<PdfPageTextRange> textRange = new List<PdfPageTextRange>();
                    for (int j = 0; j < pageCount; j++)
                    {
                        textRange.Add(new PdfPageTextRange(j));
                    }
                    this.SetSelection(new PdfTextSelection(this.pageDataCache, textRange));
                    return;
                }
            }
        }

        public void SelectLine(PdfDocumentPosition position)
        {
            PdfTextLine line = this.FindLine(position);
            if (line != null)
            {
                IList<PdfWordPart> wordParts = line.WordParts;
                int num = wordParts.Count - 1;
                if (num < 0)
                {
                    this.SetSelection(null);
                }
                else
                {
                    PdfWordPart part = wordParts[num];
                    this.SetSelection(this.GetSelection(new PdfPageTextRange(position.PageIndex, wordParts[0].WordNumber, 0, part.WordNumber, part.Characters.Count)));
                }
            }
        }

        public void SelectPage(PdfDocumentPosition position)
        {
            int pageIndex = position.PageIndex;
            if ((pageIndex >= 0) && (this.FindStartTextPosition(position) != null))
            {
                this.SetSelection(this.GetSelection(new PdfPageTextRange(pageIndex)));
            }
        }

        public void SelectText(PdfPageTextRange pageTextRange)
        {
            this.SetSelection(this.GetSelection(pageTextRange));
        }

        public void SelectText(IList<PdfPageTextRange> textRange)
        {
            this.SetSelection(this.GetSelection(textRange));
        }

        public void SelectWithCaret(PdfMovementDirection direction)
        {
            if (this.selectionState.HasCaret)
            {
                this.StoreSelectionStartTextPosition();
                switch (direction)
                {
                    case PdfMovementDirection.Left:
                        this.MoveLeft();
                        break;

                    case PdfMovementDirection.Down:
                        this.MoveDown();
                        break;

                    case PdfMovementDirection.Right:
                        this.MoveRight();
                        break;

                    case PdfMovementDirection.Up:
                        this.MoveUp();
                        break;

                    case PdfMovementDirection.NextWord:
                        this.MoveToNextWord();
                        break;

                    case PdfMovementDirection.PreviousWord:
                        this.MoveToPreviousWord();
                        break;

                    case PdfMovementDirection.LineStart:
                        this.MoveToLineStart();
                        break;

                    case PdfMovementDirection.LineEnd:
                        this.MoveToLineEnd();
                        break;

                    case PdfMovementDirection.DocumentStart:
                        this.MoveToDocumentStart();
                        break;

                    case PdfMovementDirection.DocumentEnd:
                        this.MoveToDocumentEnd();
                        break;

                    default:
                        break;
                }
                this.selectionState.Selection = this.GetSelection(this.GetPageTextRanges(this.selectionStartTextPosition, this.selectionState.Caret.Position));
                this.EnsureCaretVisibility();
            }
        }

        public void SelectWord(PdfDocumentPosition position)
        {
            PdfTextPosition textPosition = this.FindStartTextPosition(position);
            PdfWord word = this.GetWord(textPosition);
            if ((word != null) && (position != null))
            {
                PdfWordPart part = word.Parts.Last<PdfWordPart>();
                this.SetSelection(this.GetSelection(new PdfPageTextRange(position.PageIndex, word.Parts[0].WordNumber, 0, part.WordNumber, part.Characters.Count)));
            }
        }

        private void SetSelection(PdfTextSelection textSelection)
        {
            if (textSelection != null)
            {
                IList<PdfPageTextRange> textRange = textSelection.TextRange;
                int num = textRange.Count - 1;
                while (num >= 0)
                {
                    PdfPageTextRange range = textRange[num];
                    int pageIndex = range.PageIndex;
                    this.selectionStartTextPosition = new PdfTextPosition(pageIndex, range.StartTextPosition);
                    if (!range.WholePage)
                    {
                        this.MoveCaret(new PdfTextPosition(pageIndex, range.EndTextPosition));
                    }
                    else
                    {
                        IList<PdfTextLine> pageLines = this.pageDataCache.GetPageLines(pageIndex);
                        int num3 = pageLines.Count - 1;
                        if (num3 < 0)
                        {
                            num--;
                            continue;
                        }
                        IList<PdfWordPart> wordParts = pageLines[num3].WordParts;
                        PdfWordPart part = wordParts[wordParts.Count - 1];
                        this.MoveCaret(new PdfTextPosition(pageIndex, new PdfPageTextPosition(part.WordNumber, part.Characters.Count)));
                    }
                    break;
                }
            }
            this.selectionState.Selection = textSelection;
        }

        private void SetSelectionCaret(PdfTextPosition position)
        {
            PdfPoint startCoordinates;
            if (this.selectionState.HasCaret)
            {
                startCoordinates = this.selectionState.Caret.StartCoordinates;
            }
            else
            {
                startCoordinates = new PdfPoint();
            }
            this.selectionState.Caret = new PdfCaret(position, this.GetCaretViewData(position), startCoordinates);
            this.EnsureCaretVisibility();
        }

        public void SetZoomFactor(double zoomFactor)
        {
            this.textExpansionFactorX = 15.0 / zoomFactor;
            this.textExpansionFactorY = 5.0 / zoomFactor;
        }

        public bool StartSelection(PdfDocumentPosition position, bool forceSelection)
        {
            PdfTextPosition objA = this.FindStartTextPosition(position);
            if (ReferenceEquals(objA, null) & forceSelection)
            {
                objA = this.FindClosestTextPosition(position);
            }
            if (objA != null)
            {
                this.selectionInProgress = true;
                this.selectionStartPageIndex = position.PageIndex;
                this.selectionStartPoint = position.Point;
                this.UpdateSelection(objA);
            }
            else
            {
                this.selectionInProgress = false;
                this.selectionStartPageIndex = -1;
                if (this.selectionState.HasCaret)
                {
                    this.selectionState.Caret = null;
                }
            }
            return this.selectionInProgress;
        }

        private void StoreSelectionStartTextPosition()
        {
            if (!this.selectionState.HasSelection && (this.selectionState.Caret != null))
            {
                this.selectionStartTextPosition = this.selectionState.Caret.Position;
            }
        }

        private void UpdateSelection(PdfTextPosition position)
        {
            this.MoveCaret(position);
            if (this.selectionState.HasSelection)
            {
                this.selectionState.Selection = null;
            }
        }

        private int PageCount =>
            this.pageDataCache.DocumentPages.Count;

        public bool SelectionInProgress
        {
            get => 
                this.selectionInProgress;
            set => 
                this.selectionInProgress = value;
        }
    }
}

