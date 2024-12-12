namespace DevExpress.Office.Internal
{
    using System;

    public interface IFormatRatingCalculator
    {
        int Calculate(PreprocessedStream preprocessedStream);
    }
}

