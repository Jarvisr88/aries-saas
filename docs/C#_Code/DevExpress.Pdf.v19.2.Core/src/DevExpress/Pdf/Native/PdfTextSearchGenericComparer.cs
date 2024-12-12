namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfTextSearchGenericComparer : PdfTextSearchComparer
    {
        public PdfTextSearchGenericComparer(StringComparison comparisonType) : base(comparisonType)
        {
        }

        protected override bool CompareFirstWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.EndsWith(searchWord, comparisonType);

        protected override bool CompareLastWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.StartsWith(searchWord, comparisonType);

        protected override bool CompareMiddleWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.Equals(searchWord, comparisonType);

        protected override bool CompareSingleWord(string documentWord, string searchWord, StringComparison comparisonType) => 
            documentWord.IndexOf(searchWord, comparisonType) >= 0;
    }
}

