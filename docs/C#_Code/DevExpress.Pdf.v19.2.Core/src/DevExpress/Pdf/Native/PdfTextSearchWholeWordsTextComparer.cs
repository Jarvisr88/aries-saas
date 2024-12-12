namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfTextSearchWholeWordsTextComparer : PdfTextSearchComparer
    {
        public PdfTextSearchWholeWordsTextComparer(StringComparison comparisonType) : base(comparisonType)
        {
        }

        protected override bool CompareFirstWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.Equals(searchWord, comparisonType);

        protected override bool CompareLastWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.Equals(searchWord, comparisonType);

        protected override bool CompareMiddleWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.Equals(searchWord, comparisonType);

        protected override bool CompareSingleWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.Equals(searchWord, comparisonType);
    }
}

