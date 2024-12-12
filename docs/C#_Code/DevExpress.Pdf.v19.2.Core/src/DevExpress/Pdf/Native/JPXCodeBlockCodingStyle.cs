namespace DevExpress.Pdf.Native
{
    using System;

    [Flags]
    public enum JPXCodeBlockCodingStyle
    {
        SelectiveArithmeticCodingBypass = 1,
        ResetContextProbabilities = 2,
        TerminationOnEachCodingPass = 4,
        VerticallyCausalContext = 8,
        PredictableTermination = 0x10,
        UseSegmentationSymbols = 0x20
    }
}

