namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfTextSearchComparer
    {
        private readonly StringComparison comparisonType;

        protected PdfTextSearchComparer(StringComparison comparisonType)
        {
            this.comparisonType = comparisonType;
        }

        private static string CheckSeparators(string pageWord, string searchWord) => 
            (PdfTextUtils.IsSeparator(searchWord) || !PdfTextUtils.IsSeparator(pageWord)) ? pageWord : pageWord.Remove(pageWord.Length - 1);

        public bool Compare(IList<string> searchWords, IList<string> documentWords)
        {
            if (documentWords.Count == 0)
            {
                return false;
            }
            if (searchWords.Count == 1)
            {
                return this.CompareSingleWord(CheckSeparators(documentWords[0], searchWords[0]), searchWords[0], this.comparisonType);
            }
            if (!this.CompareFirstWord(CheckSeparators(documentWords[0], searchWords[0]), searchWords[0], this.comparisonType))
            {
                return false;
            }
            for (int i = 1; i < (searchWords.Count - 1); i++)
            {
                if (!this.CompareMiddleWord(CheckSeparators(documentWords[i], searchWords[i]), searchWords[i], this.comparisonType))
                {
                    return false;
                }
            }
            return this.CompareLastWord(CheckSeparators(documentWords[documentWords.Count - 1], searchWords[searchWords.Count - 1]), searchWords[searchWords.Count - 1], this.comparisonType);
        }

        protected abstract bool CompareFirstWord(string documentWord, string searchWord, StringComparison comparisonType);
        protected abstract bool CompareLastWord(string documentWord, string searchWord, StringComparison comparisonType);
        protected abstract bool CompareMiddleWord(string documentWord, string searchWord, StringComparison comparisonType);
        protected abstract bool CompareSingleWord(string documentWord, string searchWord, StringComparison comparisonType);
        public static PdfTextSearchComparer Create(PdfTextSearchParameters parameters)
        {
            StringComparison comparisonType = parameters.CaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            return (parameters.WholeWords ? ((PdfTextSearchComparer) new PdfTextSearchWholeWordsTextComparer(comparisonType)) : ((PdfTextSearchComparer) new PdfTextSearchGenericComparer(comparisonType)));
        }
    }
}

