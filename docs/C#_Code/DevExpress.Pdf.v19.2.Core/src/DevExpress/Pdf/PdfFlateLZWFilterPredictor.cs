namespace DevExpress.Pdf
{
    using System;

    public enum PdfFlateLZWFilterPredictor
    {
        NoPrediction = 1,
        TiffPredictor = 2,
        PngNonePrediction = 10,
        PngSubPrediction = 11,
        PngUpPrediction = 12,
        PngAveragePrediction = 13,
        PngPaethPrediction = 14,
        PngOptimumPrediction = 15
    }
}

