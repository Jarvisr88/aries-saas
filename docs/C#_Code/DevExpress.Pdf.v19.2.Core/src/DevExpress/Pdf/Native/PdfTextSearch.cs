namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class PdfTextSearch
    {
        private static readonly Regex wordSplitter = new Regex("(?<=[\\p{IsKatakana}\\p{IsHiragana}\\p{IsCJKUnifiedIdeographs}\\p{IsCJKUnifiedIdeographsExtensionA},.!@#$%^&*()+_=`\\{\\}\\[\\];:\"<>\\\\/?\\|-])|[\\s]");
        private static readonly Regex whitespaceRemove = new Regex(@"[\s]");
        private static Dictionary<string, string> arabicNumericsReplacements;
        private IList<PdfTextLine> pageLines;
        private IList<PdfWord> pageWords;
        private IList<string> searchWords;
        private PdfTextSearchParameters searchParameters;
        private string searchString;
        private int pageIndex;
        private int wordIndex;
        private int startPageIndex;
        private int startWordIndex;
        private bool searchStart = true;
        private bool hasResults;
        private bool reset;
        private Action MoveNext;
        private PdfPageDataCache cache;
        private PdfTextSearchComparer comparer;

        static PdfTextSearch()
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("٠", "0");
            dictionary1.Add("١", "1");
            dictionary1.Add("٢", "2");
            dictionary1.Add("٣", "3");
            dictionary1.Add("٤", "4");
            dictionary1.Add("٥", "5");
            dictionary1.Add("٦", "6");
            dictionary1.Add("٧", "7");
            dictionary1.Add("٨", "8");
            dictionary1.Add("٩", "9");
            arabicNumericsReplacements = dictionary1;
        }

        public PdfTextSearch(IList<PdfPage> pages)
        {
            this.cache = new PdfPageDataCache(pages, true, true);
        }

        private static void AddRectangle(IList<PdfOrientedRectangle> results, double left, double top, double right, double height, double angle)
        {
            results.Add(new PdfOrientedRectangle(PdfTextUtils.RotatePoint(new PdfPoint(left, top), angle), right - left, height, angle));
        }

        private IList<PdfOrientedRectangle> BuildRectangles(List<PdfWord> foundWords)
        {
            List<PdfOrientedRectangle> results = new List<PdfOrientedRectangle>();
            int count = foundWords.Count;
            if (count != 0)
            {
                PdfTextLine line = null;
                line = FindLine(foundWords[0], this.pageLines);
                if (line == null)
                {
                    return results;
                }
                PdfOrientedRectangle rectangle = foundWords[0].Rectangles[0];
                double angle = rectangle.Angle;
                PdfPoint point = PdfTextUtils.RotatePoint(rectangle.TopLeft, -angle);
                double y = point.Y;
                double x = point.X;
                double right = x + rectangle.Width;
                double height = rectangle.Height;
                for (int i = 1; i < count; i++)
                {
                    PdfOrientedRectangle rectangle2 = foundWords[i].Rectangles[0];
                    if (line.WordParts.Contains(foundWords[i].Parts[0]))
                    {
                        PdfPoint point2 = PdfTextUtils.RotatePoint(rectangle2.TopLeft, -angle);
                        if (point2.X < x)
                        {
                            AddRectangle(results, x, y, right, height, angle);
                            x = point2.X;
                        }
                        right = point2.X + rectangle2.Width;
                        y = PdfMathUtils.Max(y, point2.Y);
                        height = PdfMathUtils.Max(height, rectangle2.Height);
                    }
                    else
                    {
                        AddRectangle(results, x, y, right, height, angle);
                        angle = rectangle2.Angle;
                        IList<PdfOrientedRectangle> list3 = foundWords[i - 1].Rectangles;
                        int num9 = list3.Count;
                        if (num9 <= 1)
                        {
                            PdfPoint point4 = PdfTextUtils.RotatePoint(rectangle2.TopLeft, -angle);
                            y = point4.Y;
                            x = point4.X;
                            right = x + rectangle2.Width;
                            height = rectangle2.Height;
                        }
                        else
                        {
                            int num10 = 1;
                            while (true)
                            {
                                if (num10 >= (num9 - 1))
                                {
                                    PdfOrientedRectangle rectangle3 = list3[num9 - 1];
                                    PdfPoint point3 = PdfTextUtils.RotatePoint(rectangle3.TopLeft, -rectangle3.Angle);
                                    y = point3.Y;
                                    x = point3.X;
                                    right = rectangle2.Left + rectangle2.Width;
                                    height = PdfMathUtils.Max(rectangle2.Height, rectangle3.Height);
                                    break;
                                }
                                results.Add(list3[num10]);
                                num10++;
                            }
                        }
                        if (FindLine(foundWords[i], this.pageLines) == null)
                        {
                            AddRectangle(results, x, y, right, height, angle);
                            return results;
                        }
                    }
                }
                AddRectangle(results, x, y, right, height, angle);
                IList<PdfOrientedRectangle> rectangles = foundWords[count - 1].Rectangles;
                int num7 = rectangles.Count;
                for (int j = 1; j < num7; j++)
                {
                    results.Add(rectangles[j]);
                }
            }
            return results;
        }

        public void ClearCache(int pageIndex)
        {
            this.cache.Clear(pageIndex);
        }

        public PdfTextSearchResults Find(string text, PdfTextSearchParameters parameters, int currentPage) => 
            this.Find(text, parameters, currentPage, currentPage, null);

        public PdfTextSearchResults Find(string text, PdfTextSearchParameters parameters, int searchStartPage, int currentPage, Func<int, bool> terminate)
        {
            List<PdfWord> list;
            if (string.IsNullOrEmpty(text) || (this.cache.DocumentPages.Count == 0))
            {
                return PdfTextSearchResults.NotFound;
            }
            text = PdfTextUtils.NormalizeAndCompose(text);
            PdfTextSearchDirection direction = parameters.Direction;
            this.MoveNext = (direction != PdfTextSearchDirection.Forward) ? new Action(this.StepBackward) : new Action(this.StepForward);
            bool flag = (text != this.searchString) || !parameters.EqualsTo(this.searchParameters);
            if (flag || this.reset)
            {
                this.pageIndex = ((flag || !this.reset) ? searchStartPage : currentPage) - 1;
                this.reset = false;
                this.wordIndex = -1;
                int pageIndex = this.pageIndex;
                this.RecognizeCurrentPage();
                while (true)
                {
                    if ((this.pageWords != null) && (this.pageWords.Count != 0))
                    {
                        this.startPageIndex = this.pageIndex;
                        this.startWordIndex = -1;
                        this.searchString = text;
                        this.searchWords = PrepareString(text);
                        this.searchStart = true;
                        this.hasResults = false;
                        break;
                    }
                    this.MoveNext();
                    this.RecognizeCurrentPage();
                    if (this.pageIndex == pageIndex)
                    {
                        return PdfTextSearchResults.NotFound;
                    }
                }
            }
            if ((this.searchParameters == null) || (this.searchParameters.Direction != direction))
            {
                this.searchStart = true;
            }
            this.searchParameters = parameters.CloneParameters();
            this.comparer = PdfTextSearchComparer.Create(this.searchParameters);
            this.MoveNext = (direction != PdfTextSearchDirection.Forward) ? new Action(this.StepBackward) : new Action(this.StepForward);
            int pageIndex = -1;
            while (true)
            {
                this.MoveNext();
                list = new List<PdfWord>();
                if (!this.TryCompare())
                {
                    if ((this.pageIndex != this.startPageIndex) || (this.wordIndex != this.startWordIndex))
                    {
                        if (this.startWordIndex == -1)
                        {
                            this.startWordIndex = 0;
                        }
                        if (pageIndex != this.pageIndex)
                        {
                            pageIndex = this.pageIndex;
                            if ((terminate != null) && terminate(this.pageIndex))
                            {
                                break;
                            }
                        }
                        if (this.searchStart || ((this.pageIndex != this.startPageIndex) || (this.wordIndex != this.startWordIndex)))
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    int num3 = this.wordIndex + this.searchWords.Count;
                    int wordIndex = this.wordIndex;
                    while (true)
                    {
                        if (wordIndex < num3)
                        {
                            list.Add(this.pageWords[wordIndex]);
                            wordIndex++;
                            continue;
                        }
                        if (this.startWordIndex == -1)
                        {
                            this.startWordIndex = 0;
                        }
                        if (!this.searchStart)
                        {
                            if ((this.pageIndex != this.startPageIndex) || (this.wordIndex != this.startWordIndex))
                            {
                                goto TR_0000;
                            }
                            else
                            {
                                this.searchStart = true;
                                this.wordIndex = (this.searchParameters.Direction == PdfTextSearchDirection.Forward) ? (this.wordIndex - 1) : (this.wordIndex + 1);
                            }
                        }
                        else
                        {
                            this.startWordIndex = this.wordIndex;
                            this.startPageIndex = this.pageIndex;
                            this.searchStart = false;
                            this.hasResults = true;
                            goto TR_0000;
                        }
                        break;
                    }
                }
                break;
            }
            return (this.hasResults ? PdfTextSearchResults.Finished : PdfTextSearchResults.NotFound);
        TR_0000:
            return new PdfTextSearchResults(this.cache.DocumentPages[this.pageIndex], this.pageIndex + 1, list, this.BuildRectangles(list), PdfTextSearchStatus.Found);
        }

        private static PdfTextLine FindLine(PdfWord word, IList<PdfTextLine> pageStrings)
        {
            PdfTextLine line2;
            using (IEnumerator<PdfTextLine> enumerator = pageStrings.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfTextLine current = enumerator.Current;
                        if (!current.WordParts.Contains(word.Parts[0]))
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

        private string GetDocumentText(int wordIndex, int count)
        {
            PdfBidiStringBuilder builder = new PdfBidiStringBuilder();
            int num = 0;
            for (int i = wordIndex; num < count; i++)
            {
                foreach (PdfWordPart part in this.pageWords[i].Parts)
                {
                    foreach (PdfCharacter character in part.Characters)
                    {
                        builder.Append(TryGetNumericReplacement(character.UnicodeData));
                    }
                    if (part.WordEnded)
                    {
                        builder.Append(" ");
                    }
                }
                num++;
            }
            return PdfTextUtils.NormalizeAndCompose(builder.EndCurrentLineAndGetString().TrimEnd(new char[0]));
        }

        private static IList<string> PrepareString(string inputString)
        {
            IList<string> list = new List<string>();
            foreach (string str in wordSplitter.Split(inputString))
            {
                string str2 = whitespaceRemove.Replace(str, "");
                if (!string.IsNullOrEmpty(str2))
                {
                    list.Add(str2);
                }
            }
            return list;
        }

        private void RecognizeCurrentPage()
        {
            PdfPageData data = this.cache[this.pageIndex];
            this.pageLines = data.TextData;
            this.pageWords = data.Words;
        }

        public void Reset()
        {
            this.reset = true;
        }

        private void StepBackward()
        {
            if (this.wordIndex > 0)
            {
                this.wordIndex--;
            }
            else
            {
                this.pageIndex = (this.pageIndex <= 0) ? (this.cache.DocumentPages.Count - 1) : (this.pageIndex - 1);
                this.RecognizeCurrentPage();
                this.wordIndex = this.pageWords.Count - 1;
            }
        }

        private void StepForward()
        {
            if (this.wordIndex < (this.pageWords.Count - 1))
            {
                this.wordIndex++;
            }
            else
            {
                int num2;
                if (this.pageIndex < (this.cache.DocumentPages.Count - 1))
                {
                    num2 = this.pageIndex + 1;
                }
                else
                {
                    num2 = this.pageIndex = 0;
                }
                this.pageIndex = num2;
                this.wordIndex = 0;
                this.RecognizeCurrentPage();
            }
        }

        private bool TryCompare()
        {
            if ((this.pageWords.Count == 0) || ((this.searchWords == null) || (this.searchWords.Count == 0)))
            {
                return false;
            }
            int count = this.searchWords.Count;
            return (((this.wordIndex + count) <= this.pageWords.Count) ? this.comparer.Compare(this.searchWords, PrepareString(this.GetDocumentText(this.wordIndex, count))) : false);
        }

        private static string TryGetNumericReplacement(string ch)
        {
            string str;
            return (!arabicNumericsReplacements.TryGetValue(ch, out str) ? ch : str);
        }

        public PdfPageDataCache Cache =>
            this.cache;
    }
}

